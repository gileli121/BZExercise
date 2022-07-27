using System;
using System.IO.Pipes;
using System.Security.Principal;
using System.Text.Json;
using System.Threading;
using BzScanner;
using BzScanner.Enums;
using ServiceCommon.Common;
using ServiceCommon.Models;

namespace OutlookScanService.Logic
{
    public class ServiceLogic
    {
        NamedPipeServerStream serverStream;
        StreamString streamString;
        Thread pipeServerThread;
        BZScanner bzScanner;

        public void OnStart(string[] args)
        {

            var apiKey = Environment.GetEnvironmentVariable("VirusTotalAPI");
            bzScanner = new BZScanner(apiKey);

            pipeServerThread = new Thread(PipeServerThread);
            pipeServerThread.Start();
            //return;
            Logger.WriteToFile("Service is started at " + DateTime.Now);
        }

        void ProcessRequestCommand(string requestCommandStr)
        {
            Logger.WriteToFile($"Processing command: ${requestCommandStr}");

            if (requestCommandStr == null)
                throw new Exception("Invalid request command provided. It should not be null.");

            var pipeData = JsonSerializer.Deserialize<PipeData>(requestCommandStr);

            if (pipeData == null)
                throw new Exception("Invalid pipeData provided. It should not be null.");


            switch (pipeData.CommandName)
            {
                case Commands.ScanAttachmentRequest:

                    var scanRequest = JsonSerializer.Deserialize<AttachmentScanRequest>(pipeData.Payload);

                    var scanResult = new AttachmentScanResult();
                    scanResult.ScanResult = bzScanner.CheckFile(scanRequest?.FileHash);

                    streamString.WriteSerialized(scanResult);
                    break;
            }
        }

        void PipeServerThread()
        {

            var pipeSecurity = new PipeSecurity();
            pipeSecurity.AddAccessRule(new PipeAccessRule(
                new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null), PipeAccessRights.ReadWrite,
                System.Security.AccessControl.AccessControlType.Allow));

            // serverStream = new NamedPipeServerStream("PipesEnroll", PipeDirection.InOut, 1);
            serverStream = new NamedPipeServerStream("PipesEnroll", PipeDirection.InOut, 1, PipeTransmissionMode.Byte,
                PipeOptions.Asynchronous, 0, 0, pipeSecurity);
            streamString = new StreamString(serverStream);
            while (true)
            {
                try
                {
                    Logger.WriteToFile("Waiting for new connection");
                    serverStream.WaitForConnection();
                    Logger.WriteToFile("Got connection");

                    while (serverStream.IsConnected)
                    {
                        var requestCommand = streamString.ReadString();
                        if (!serverStream.IsConnected) break;

                        Logger.WriteToFile($"Got command: ${requestCommand}");

                        try
                        {
                            ProcessRequestCommand(requestCommand);
                        }
                        catch (Exception e)
                        {
                            Logger.WriteToFile($"An error occurred while processing the command. Exception: {e}");
                        }
                    }

                    serverStream.Disconnect();
                    Console.WriteLine(1);
                }
                catch (Exception e)
                {
                    // ignored
                    Console.WriteLine(1);
                }
            }
        }

        public void OnStop()
        {
            //return;
            Logger.WriteToFile("Service is stopped at " + DateTime.Now);
        }
    }
}
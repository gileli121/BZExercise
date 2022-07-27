using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using BzScanner.Enums;
using OutlookAddIn1;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using ServiceCommon;
using ServiceCommon.Common;
using ServiceCommon.Models;

namespace OutlookScanAddIn
{
    public partial class ThisAddIn
    {
        Outlook.Inspectors inspectors;
        //PipeClient pipeClient;

        NamedPipeClientStream clientStream;
        StreamString streamString;


        Outlook.NameSpace outlookNameSpace;
        Outlook.MAPIFolder inbox;
        Outlook.Items items;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            outlookNameSpace = this.Application.GetNamespace("MAPI");
            inbox = outlookNameSpace.GetDefaultFolder(
                Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);

            items = inbox.Items;
            items.ItemAdd +=
                new Outlook.ItemsEvents_ItemAddEventHandler(items_ItemAdd);


            clientStream = new NamedPipeClientStream(".", "PipesEnroll", PipeDirection.InOut,
                PipeOptions.None, TokenImpersonationLevel.Impersonation);
            clientStream.Connect(10000);

            streamString = new StreamString(clientStream);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion


        void items_ItemAdd(object Item)
        {

            if (!clientStream.IsConnected)
                return; // Do nothing in case we are not connected

            // string filter = "USED CARS";
            Outlook.MailItem mail = (Outlook.MailItem)Item;
            if (Item != null)
            {
                var attachments = mail.Attachments;


                foreach (Outlook.Attachment attachment in mail.Attachments)
                {
                    // https://stackoverflow.com/questions/827131/c-sharp-outlook-2007-how-do-i-access-attachment-contents-directly-from-my-addi
                    //microsoft schema to get the attachment content
                    string AttachSchema = "http://schemas.microsoft.com/mapi/proptag/0x37010102";
                    byte[] filebyte = (byte[])attachment.PropertyAccessor.GetProperty(AttachSchema);


                    CheckAttachment(filebyte.ToMd5(), attachment.FileName);



                }
            }
        }


        void CheckAttachment(string fileHash, string fileName)
        {
            var pipeData = new PipeData(Commands.ScanAttachmentRequest, new AttachmentScanRequest()
            {
                FileHash = fileHash
            });

            streamString.WriteSerialized(pipeData);
            clientStream.WaitForPipeDrain();
            var scanResultStr = streamString.ReadString();
            var scanResult = JsonSerializer.Deserialize<AttachmentScanResult>(scanResultStr);


            if (scanResult != null)
            {
                switch (scanResult.ScanResult)
                {
                    case ScanResultType.Malicious:
                        Win32Helpers.MessageBox(IntPtr.Zero,
                            $"The attachment \"{fileName}\" found to be malicious",
                            "Malicious attachment found", 0);
                        break;
                    case ScanResultType.Unknown:
                        Win32Helpers.MessageBox(IntPtr.Zero,
                            $"The attachment \"{fileName}\" found to be unknown and may be dangerous",
                            "Unknown attachment found", 0);
                        break;
                    case ScanResultType.Legitimate:
                        Win32Helpers.MessageBox(IntPtr.Zero,
                            $"The attachment \"{fileName}\" found to be legitimate",
                            "legitimate attachment found", 0);
                        break;
                }
            }
        }
    }
}
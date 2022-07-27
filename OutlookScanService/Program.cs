using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using OutlookScanService.Logic;

namespace OutlookScanService
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

#if !DEBUG
            try
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new Service1()
                };
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Application", ex.ToString(), EventLogEntryType.Error);
            }
#else
            var serviceLogic = new ServiceLogic();
            serviceLogic.OnStart(new string[] { });

            while (true)
                System.Threading.Thread.Sleep(1000);
#endif
        }
    }
}

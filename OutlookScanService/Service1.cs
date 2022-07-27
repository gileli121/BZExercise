using BzScanner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using OutlookScanService.Logic;

namespace OutlookScanService
{
    public partial class Service1 : ServiceBase
    {
        ServiceLogic serviceLogic;

        public Service1()
        {
            InitializeComponent();
            serviceLogic = new ServiceLogic();
        }
        protected override void OnStart(string[] args)
        {
            serviceLogic.OnStart(args);
        }
        protected override void OnStop()
        {
            serviceLogic.OnStop();
        }
        
     
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SM.Monitoring.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<MonitoringServiceHost>(s =>
                {
                    s.ConstructUsing(name => new MonitoringServiceHost());                    
                    s.WhenStarted(rs => rs.OnStartUp());
                    s.WhenStopped(rs => rs.OnStop());
                });
                
                x.RunAsLocalSystem();
                x.SetDescription("System Monitoring - Monitoring Service");
                x.SetServiceName("SMMonitoringService");
                x.SetDisplayName("SMMonitoringService");
            });
        }
    }
}

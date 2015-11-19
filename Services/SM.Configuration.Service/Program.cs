using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SM.Configuration.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ConfigurationServiceHost>(s =>
                {
                    s.ConstructUsing(name => new ConfigurationServiceHost());
                    s.WhenStarted(rs => rs.OnStartUp());
                    s.WhenStopped(rs => rs.OnStop());
                });

                x.RunAsLocalSystem();
                x.SetDescription("System Monitoring - Configuration Service");
                x.SetServiceName("SMConfigurationService");
                x.SetDisplayName("SMConfigurationService");
            });
        }
    }
}

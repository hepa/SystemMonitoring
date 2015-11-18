﻿using SM.Monitoring.Service;
using Topshelf;

namespace SM.Starter.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<StarterServiceHost>(s =>
                {
                    s.ConstructUsing(name => new StarterServiceHost());
                    s.WhenStarted(rs => rs.OnStartUp());
                    s.WhenStopped(rs => rs.OnStop());
                });

                x.RunAsLocalSystem();
                x.SetDescription("System Monitoring - Starter Service");
                x.SetServiceName("SMStarterService");
                x.SetDisplayName("SMStarterService");
            });
        }
    }
}

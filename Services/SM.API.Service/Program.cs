using Topshelf;

namespace SM.API.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ApiServiceHost>(s =>
                {
                    s.ConstructUsing(name => new ApiServiceHost());
                    s.WhenStarted(rs => rs.StartService());
                    s.WhenStopped(rs => rs.OnStop());
                });

                x.RunAsLocalSystem();
                x.SetDescription("System Monitoring - API Service");
                x.SetServiceName("SMApiService");
                x.SetDisplayName("SMApiService");
            });
        }
    }
}

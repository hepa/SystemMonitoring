using SM.Core.Parser;
using SM.Core.Services;
using Timer = System.Timers.Timer;

namespace SM.Monitoring.Service
{
    public class MonitoringServiceHost : SMServiceHost
    {
        private Timer _timer;

        public MonitoringServiceHost()
        {
        }

        public override void OnStartUp()
        {
            _timer = new Timer(5000);
            _timer.Elapsed += Elapsed;
            _timer.Start();
        }

        private void Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {            
            JsonParser.Parse();
        }

        public override void OnStop()
        {
            _timer.Stop();
        }
    }
}
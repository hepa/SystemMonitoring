using System;
using SM.Contracts.Models.HWiNFO;
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
            //_timer.Elapsed += Elapsed;
            _timer.Start();
            Elapsed(null, null);
        }

        private void Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var hwInfo = JsonParser.ParseIntoContract();
            var bits = DataType.ConverTo(hwInfo.Memory.VirtualMemoryAvailable, DataUnit.b);
            Console.WriteLine(bits);
        }

        public override void OnStop()
        {
            _timer.Stop();
        }
    }
}
using SM.Core.Services;

namespace SM.Starter.Service
{
    public class StarterServiceHost : SMServiceHost
    {
        private readonly StarterServiceWorker _worker;

        public StarterServiceHost()
        {
            _worker = new StarterServiceWorker();
        }

        public override void OnStartUp()
        {
            _worker.Work();
        }

        public override void OnStop()
        {
            _worker.Stop();
        }
    }
}
using System;

namespace SM.Core.Services
{
    public abstract class SMService : IDisposable
    {
        public bool IsStarted { get; set; }

        public virtual void StartService()
        {
            // 1. Dependency Manager bootup
            // 2.
            OnStartUp();
            // 3. Static Warm-up method
            // 4. Start processing requests
            IsStarted = true;
        }

        public virtual void OnStartUp()
        {
        }

        public virtual void OnStop()
        {
            IsStarted = false;
        }

        public abstract void WarmUp();

        public void Dispose()
        {
            OnStop();
        }
    }
}
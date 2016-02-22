using System;
using SM.Contracts.Enum;
using SM.Contracts.Exceptions;
using SM.Core.Extensions;

namespace SM.Core.Services
{
    public abstract class SMService : IDisposable
    {
        public bool IsStarted { get; set; }

        public virtual void StartService()
        {
            try
            {
                // 1. Dependency Manager bootup
                // 2. Static Warm-up method
                WarmUp();
                // 3. Actual start
                OnStartUp();
                // 4. Start processing requests
                IsStarted = true;
            }            
            catch (Exception ex)
            {
                HandleErrors(ex);
            }
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
            //OnStop();
        }

        //TODO ez itt szar
        internal void HandleErrors(Exception ex)
        {
            var smException = ex as SMException;
            if (smException != null)
            {
                ErrorCodes errorCode;
                string message = "";
                var isSuccess = Enum.TryParse(smException.ErrorCode.ToString(), out errorCode);
                if (isSuccess)
                {
                    message = errorCode.GetDescription();
                }

                Console.WriteLine("{0}: {1} - {2}", smException.ErrorCode, message, smException);
            }
            else
            {
                Console.WriteLine(ex);
            }            
        }
    }
}

using SM.Configuration.Configuration;
using SM.Contracts.Models;
using SM.Contracts.Models.Configuration;

namespace SM.Core.Services
{
    public class SMServiceHost : SMService
    {        
        public SMServiceHost()
        {
            
        }

        public override void WarmUp()
        {
            // Load up the active configurations.
            SMConfigurations.Current = GetConfiguration();
        }

        public ServiceConfiguration GetConfiguration()
        {
            return new ServiceConfiguration
            {
                GlobalConfigurations = new GlobalConfigurations(),
                LogConfigurations = new LogConfigurations(),
                SqlConfigurations = new SqlConfigurations(),
                ApiConfiguration = new ApiConfiguration()
            };
        }
    }
}
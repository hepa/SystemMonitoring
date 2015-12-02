using SM.Contracts.Models;
using SM.Contracts.Models.Configuration;

namespace SM.Configuration.Configuration
{
    public class SMConfigurations
    {
        private static ServiceConfiguration _configuration;

        public static ServiceConfiguration Current
        {
            get
            {
                return _configuration;
            }
            set { _configuration = value; }
        }
    }
}
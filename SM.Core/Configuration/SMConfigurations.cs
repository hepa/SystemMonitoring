using SM.Contracts.Models;

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
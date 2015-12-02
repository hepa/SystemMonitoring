namespace SM.Contracts.Models.Configuration
{
    public class ServiceConfiguration
    {
        public LogConfigurations LogConfigurations { get; set; }

        public GlobalConfigurations GlobalConfigurations { get; set; }

        public SqlConfigurations SqlConfigurations { get; set; }
    }
}
namespace SM.Contracts.Models
{
    public class ServiceConfigurations
    {
        /// <summary>
        /// Represents the refresh rate in milliseconds, in which the service pulls data from the Remote Sensor Monitor.
        /// </summary>
        public int RefreshRateInMilliSeconds { get; set; } = 1000;
    }
}
namespace SM.Contracts.Models.Configuration
{
    public class GlobalConfigurations
    {
        /// <summary>
        /// Represents the refresh rate in milliseconds, in which the service pulls data from the Remote Sensor Monitor.
        /// </summary>
        public int RefreshRateInMilliSeconds { get; set; } = 1000;

        public int ProcessStartRetryRateInMilliSeconds { get; set; } = 5000;
    }
}
using System;

namespace SM.Contracts.Attributes
{
    public class SensorNameAttribute : Attribute
    {
        public string SensorNameRegex { get; set; }

        public SensorNameAttribute(string sensorNameRegex)
        {
            SensorNameRegex = sensorNameRegex;
        }
    }
}
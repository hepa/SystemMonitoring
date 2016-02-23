using System;

namespace SM.Contracts.Attributes
{
    public class SensorClassAttribute : Attribute
    {
         public string ClassNameRegex { get; set; }

        public SensorClassAttribute(string classNameRegex)
        {
            ClassNameRegex = classNameRegex;
        }
    }
}
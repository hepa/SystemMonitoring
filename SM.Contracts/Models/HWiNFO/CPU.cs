using System.Collections.Generic;
using System.Linq;
using SM.Contracts.Attributes;

namespace SM.Contracts.Models.HWiNFO
{
    [SensorClass("CPU \\[\\#.\\]")]
    public class CPU
    {
        public int Cores => VIDS.Count;

        [SensorName("Core #. VID")]
        public List<Data<double, VoltageType>> VIDS { get; set; }

        [SensorName("Core #. Clock")]
        public List<Data<double, FrequencyType>> Clocks { get; set; }

        [SensorName("Bus Clock")]
        public Data<double, FrequencyType> BusClock { get; set; }

        [SensorName("Uncore Clock")]
        public Data<double, FrequencyType> UncoreClock { get; set; }

        [SensorName("Core #. Usage")]
        public List<Data<double, PercentageType>> Usages { get; set; }

        public Data<double, PercentageType> MaxCpuUsage
        {
            get { return Usages.OrderByDescending(usage => usage.Value).First(); }
        }


        [SensorName("Total CPU Usage")]
        public Data<double, PercentageType> TotalCpuUsage { get; set; }

        //TODO use this
        public double TotalCpuUsageCalc
        {
            get { return Usages.Average(u => u.Value); }
        }

        [SensorName("Core #. Thermal Throttling")]
        public Data<string, dynamic> ThermalThrottling { get; set; }

        [SensorName("Core #. Ratio")]
        public List<Data<int, RatioType>> Ratios { get; set; }

        [SensorName("Uncore Ratio")]
        public Data<int, RatioType> UncoreRatio { get; set; }

        [SensorName("^Core #.$")]
        public List<Data<int, TemperatureType>> Temperatures { get; set; }

        [SensorName("Core #. Distance to TjMAX")]
        public List<Data<int, TemperatureType>> TemperatureDistancesToTjMax { get; set; }

        public Data<int, TemperatureType> CoreMax
        {
            get { return Temperatures.OrderByDescending(temp => temp.Value).First(); }
        } 

        public CPU()
        {
            VIDS = new List<Data<double, VoltageType>>();
            Clocks = new List<Data<double, FrequencyType>>();
            BusClock = new Data<double, FrequencyType>();
            UncoreClock = new Data<double, FrequencyType>();
            Usages = new List<Data<double, PercentageType>>();
            TotalCpuUsage = new Data<double, PercentageType>();
            Ratios = new List<Data<int, RatioType>>();
            Temperatures = new List<Data<int, TemperatureType>>();
            TemperatureDistancesToTjMax = new List<Data<int, TemperatureType>>();
        }
    }
}
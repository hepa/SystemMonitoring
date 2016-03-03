using System.Collections.Generic;
using SM.Contracts.Attributes;

namespace SM.Contracts.Models.HWiNFO
{
    [SensorClass("ASRock B85 Killer")]
    public class Motherboard
    {
        [SensorName("^Motherboard$")]
        public Data<double, TemperatureType> Temperature { get; set; }

        [SensorName("^CPU$")]
        public Data<double, TemperatureType> CpuTemperature { get; set; }

        [SensorName("Auxiliary")]
        public Data<double, TemperatureType> Auxiliary { get; set; }

        [SensorName("SYSTIN.")]
        public List<Data<double, TemperatureType>> Systins { get; set; }

        [SensorName("Vccin")]
        public Data<double, TemperatureType> VccIn { get; set; }

        [SensorName("\\+12V")]
        public Data<double, TemperatureType> TwelveVolt { get; set; }

        [SensorName("AVCC")]
        public Data<double, TemperatureType> AVCC { get; set; }

        [SensorName("\\+3\\.3V")]
        public Data<double, TemperatureType> ThreeVolt { get; set; }

        [SensorName("VIN4")]
        public Data<double, TemperatureType> VIN4 { get; set; }

        [SensorName("\\+5V")]
        public Data<double, TemperatureType> FiveVolt { get; set; }

        [SensorName("VIN6")]
        public Data<double, TemperatureType> VIN6 { get; set; }

        [SensorName("3VSB")]
        public Data<double, TemperatureType> VSB { get; set; }

        [SensorName("CPU1\\/2")]
        public Data<int, RotationType> CpuFan { get; set; }

        public Motherboard()
        {
            Temperature = new Data<double, TemperatureType>();
            CpuTemperature = new Data<double, TemperatureType>();
            Auxiliary = new Data<double, TemperatureType>();
            Systins = new List<Data<double, TemperatureType>>();
            VccIn = new Data<double, TemperatureType>();
            TwelveVolt = new Data<double, TemperatureType>();
            AVCC = new Data<double, TemperatureType>();
            ThreeVolt = new Data<double, TemperatureType>();
            VIN4 = new Data<double, TemperatureType>();
            FiveVolt = new Data<double, TemperatureType>();
            VIN6 = new Data<double, TemperatureType>();
            VSB = new Data<double, TemperatureType>();
            CpuFan = new Data<int, RotationType>();
        }
    }
}
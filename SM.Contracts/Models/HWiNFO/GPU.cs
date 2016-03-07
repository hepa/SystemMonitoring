using System.CodeDom;
using System.Collections.Generic;
using SM.Contracts.Attributes;

namespace SM.Contracts.Models.HWiNFO
{
    [SensorClass("GPU \\[\\#[0-9]\\]")]
    public class GPU
    {
        [SensorName("GPU Thermal Diode")]
        public Data<int, TemperatureType> ThermalDiode { get; set; }

        [SensorName("GPU VDDC")]
        public Data<double, VoltageType> VDDC { get; set; }

        [SensorName("GPU Fan")]
        public Data<int, RotationType> FanRPM { get; set; }

        [SensorName("GPU Clock")]
        public Data<double, FrequencyType> Clock { get; set; }

        [SensorName("GPU Memory  Clock")]
        public Data<double, FrequencyType> MemoryClock { get; set; }

        [SensorName("GPU Utilization")]
        public Data<double, PercentageType> Utilization { get; set; }

        [SensorName("GPU D3D Usage")]
        public Data<double, PercentageType> D3DUsage { get; set; }

        [SensorName("GPU Fan Speed")]
        public Data<double, PercentageType> FanSpeed { get; set; }

        [SensorName("GPU D3D Memory Dedicated")]
        public Data<int, PercentageType> MemoryDedicated { get; set; }

        [SensorName("GPU D3D Memory Dynamic")]
        public Data<int, PercentageType> MemoryDynamic { get; set; }

        [SensorName("GPU VRM Temperature[0-9]")]
        public List<Data<double, TemperatureType>> VrmTemperatures { get; set; }

        [SensorName("GPU VRM Voltage Out")]
        public Data<double, VoltageType> VrmVoltageOut { get; set; }

        [SensorName("GPU VRM Voltage In")]
        public Data<double, VoltageType> VrmVoltageIn { get; set; }

        [SensorName("GPU VRM Current Out")]
        public Data<double, VoltageType> VrmCurrentOut { get; set; }

        [SensorName("GPU VRM Current In")]
        public Data<double, VoltageType> VrmCurrentIn { get; set; }

        [SensorName("GPU VRM Power Out")]
        public Data<double, VoltageType> VrmPowerOut { get; set; }
                                            
        [SensorName("GPU VRM Power In")]  
        public Data<double, VoltageType> VrmPowerIn { get; set; }

        public GPU()
        {
            ThermalDiode = new Data<int, TemperatureType>();
            VDDC = new Data<double, VoltageType>();
            FanRPM = new Data<int, RotationType>();
            Clock = new Data<double, FrequencyType>();
            MemoryClock = new Data<double, FrequencyType>();
            Utilization = new Data<double, PercentageType>();
            D3DUsage = new Data<double, PercentageType>();
            FanSpeed = new Data<double, PercentageType>();
            MemoryDedicated = new Data<int, PercentageType>();
            MemoryDynamic = new Data<int, PercentageType>();
            VrmTemperatures = new List<Data<double, TemperatureType>>();
            VrmVoltageOut = new Data<double, VoltageType>();
            VrmVoltageIn = new Data<double, VoltageType>();
            VrmCurrentOut = new Data<double, VoltageType>();
            VrmCurrentIn = new Data<double, VoltageType>();
            VrmPowerOut = new Data<double, VoltageType>();
            VrmPowerOut = new Data<double, VoltageType>();
        }
    }           
}
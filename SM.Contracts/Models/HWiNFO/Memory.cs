using SM.Contracts.Attributes;

namespace SM.Contracts.Models.HWiNFO
{
    [SensorClass("System")]
    public class Memory
    {
        [SensorName("Virtual Memory Commited")]
        public Data<int, DataType> VirtualMemoryCommited { get; set; }

        [SensorName("Virtual Memory Available")]
        public Data<int, DataType> VirtualMemoryAvailable { get; set; }

        [SensorName("Virtual Memory Load")]
        public Data<double, PercentageType> VirtualMemoryLoad { get; set; }

        [SensorName("Physical Memory Used")]
        public Data<int, DataType> PhysicalMemoryUsed { get; set; }

        [SensorName("Physical Memory Available")]
        public Data<int, DataType> PhysicalMemoryAvailable { get; set; }

        [SensorName("Physical Memory Load")]
        public Data<double, PercentageType> PhysicalMemoryLoad { get; set; }
    }
}
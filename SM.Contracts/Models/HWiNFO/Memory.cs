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
    }
}
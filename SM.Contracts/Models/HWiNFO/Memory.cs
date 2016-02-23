using SM.Contracts.Attributes;

namespace SM.Contracts.Models.HWiNFO
{
    [SensorClass("System")]
    public class Memory
    {
        [SensorName("Virtual Memory Commited")]
        public Data<int> VirtualMemoryCommited { get; set; }
    }
}
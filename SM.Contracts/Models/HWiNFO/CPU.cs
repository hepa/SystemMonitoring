using System.Collections.Generic;
using SM.Contracts.Attributes;

namespace SM.Contracts.Models.HWiNFO
{
    [SensorClass("CPU \\[\\#.\\]")]
    public class CPU
    {
        public int Cores => VIDS.Count;

        [SensorName("Core #. VID")]
        public List<Data<double, DataType>> VIDS { get; set; }

        public CPU()
        {
            VIDS = new List<Data<double, DataType>>();
        }
    }
}
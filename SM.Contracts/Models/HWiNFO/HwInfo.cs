using System.Collections.Generic;

namespace SM.Contracts.Models.HWiNFO
{    
    public class HwInfo
    {
        public Memory Memory { get; set; } 

        public List<CPU> CentralProcessingUnits { get; set; }

        public HwInfo()
        {
            Memory = new Memory();
            CentralProcessingUnits = new List<CPU>();
        }
    }
}
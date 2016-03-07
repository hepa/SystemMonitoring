using System.Collections.Generic;

namespace SM.Contracts.Models.HWiNFO
{    
    public class HwInfo
    {
        public Motherboard Motherboard { get; set; }

        public Memory Memory { get; set; } 

        public CPU CentralProcessingUnit { get; set; }

        public List<GPU> GraphicsProcessingUnits { get; set; }

        public HwInfo()
        {
            Memory = new Memory();
            CentralProcessingUnit = new CPU();
            Motherboard = new Motherboard();
            GraphicsProcessingUnits = new List<GPU>();
        }
    }
}
using System;

namespace SM.Contracts.Models.HWiNFO
{
    public class DataType : UnitType<DataUnit>
    {
        public static long ConverTo(Data<int, DataType> _from, DataUnit _to)
        {
            var pow = (DataUnit)System.Enum.Parse(typeof(DataUnit), _from.Unit.Unit.ToString());

            var p = (double)pow - (double) _to;

            var bits = _from.Value * Math.Pow(2, p);
            var res = bits / Math.Pow(2, (double)_to);
            return (long)res;
        }
    }
}
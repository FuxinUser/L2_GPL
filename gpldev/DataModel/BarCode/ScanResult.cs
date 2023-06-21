using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMod.BarCode
{
    [Serializable]
    public class ScanResult
    {
        public string CoilID { get; set; }

        public int SKID { get; set; }

        public ScanResult(int skid, string coilID)
        {
            SKID = skid;
            CoilID = coilID;
        }
    }
}

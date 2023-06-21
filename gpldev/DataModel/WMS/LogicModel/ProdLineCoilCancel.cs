using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMod.WMS.LogicModel
{
    /// <summary>
    /// 產線入料/出料/退料取消
    /// </summary>
    [Serializable]
    public class ProdLineCoilCancel
    {
        public string Flag { get; set; } = "";
        public string CoilNo { get; set; } = "";
        public string Spare { get; set; } = "";

        public ProdLineCoilCancel(string flag, string coilNo, string spare = "")
        {
            Flag = flag;
            CoilNo = coilNo;
            Spare = spare;
        }
    }
}

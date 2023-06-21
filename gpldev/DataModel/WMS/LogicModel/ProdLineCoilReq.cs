using Core.Define;
using System;

namespace DataMod.WMS.LogicModel
{
    /// <summary>
    /// 產線入料/出料/退料要求資料
    /// </summary>
    [Serializable]
    public class ProdLineCoilReq
    {
        public string Flag { get; set; } = "";
        public string CoilNo { get; set; } = "";
        public string CoilTurn { get; set; } = "";
        public string Spare { get; set; } = "";

        public string Pos { get; set; } = "";

        public ProdLineCoilReq(string flag, string coilNo, string pos = "",string coilTurn = "", string spare = "")
        {
            Flag = flag;
            CoilNo = coilNo;
            CoilTurn = coilTurn;
            Spare = spare;
            Pos = pos;
        }


        public string ActionStr { get {

                var action = string.Empty;

                switch (Flag)
                {

                    case WMSSysDef.Cmd.ReqWMSDeliveryCoil:
                        action = "出料";
                        break;


                    case WMSSysDef.Cmd.ReqWMSRejectCoil:
                        action = "回退";
                        break;

                    default:
                        
                        action = "入料";
                        break;
                }


                return action;
            
            } }
    }
}

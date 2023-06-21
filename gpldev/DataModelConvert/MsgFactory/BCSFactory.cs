using Core.Define;
using DataMod.BarCode;
using System;

namespace MsgConvert.Msg
{
    public class BCSFactory
    {

        public static BCSModel.CompareScnResult_SB01 ScanResult(bool compareOK, string coilNo)
        {
  
            var msg = new BCSModel.CompareScnResult_SB01()
            {
                Header = new BCSModel.BCSHeader
                {
                    Message_Id = DeviceDef.CompareScanResultId.PadLeft(4, '0').ToCharArray(),
                    Message_Length = DeviceDef.ScanResultLength.PadLeft(4, '0').ToCharArray(),
                    Message_DateTime = DateTime.Now.ToString("yyyyMMddHHmmss").ToCharArray()
                },

                Result = compareOK ? "1".ToCharArray() : "2".ToCharArray(),
                CoilNo = coilNo.PadLeft(14, ' ').ToCharArray()

            };   
            return msg;
        }

    }
}

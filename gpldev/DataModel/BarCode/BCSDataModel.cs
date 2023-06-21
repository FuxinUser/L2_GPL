using Core.Define;
using System;
using System.Runtime.InteropServices;
using static Core.Define.L2SystemDef;



namespace DataMod.BarCode
{
    /*
     * 承接BarCode機定義Model資料(隔離)
     */
    public class BCSDataModel
    {

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class BarCodeScnContent
        {
            public string ScanCoilNo { get; set; } 
            public SKPOS ScanPosition { get; set; }

            public BarCodeScnContent()
            {

            }

            public BarCodeScnContent(string coilNo, SKPOS pos)
            {
                ScanCoilNo = coilNo;
                ScanPosition = pos;
            }

            public int GetL1Position()
            {

                if (ScanPosition == SKPOS.Entry_SK01)
                    return PlcSysDef.Pos.SK1;

                if (ScanPosition == SKPOS.EntryTOP)
                    return PlcSysDef.Pos.ETOP;

                return PlcSysDef.Pos.POR;

            }

            public int GetWMSPosition()
            {

                if (ScanPosition == SKPOS.Entry_SK01)
                    return WMSSysDef.SkPos.ESK01No;

                if (ScanPosition == SKPOS.EntryTOP)
                    return WMSSysDef.SkPos.ETOPNo;

                return -1;

            }

            public string GetPOSStr()
            {

                if (ScanPosition == SKPOS.Entry_SK01)
                    return "ESK01";

                if (ScanPosition == SKPOS.EntryTOP)
                    return "ETOP";

                if (ScanPosition == SKPOS.POR)
                    return "POR";

                if (ScanPosition == SKPOS.Delivery_SK01)
                    return "DSK01";

                if (ScanPosition == SKPOS.Delivery_SK02)
                    return "DSK02";

                return string.Empty;

            }

            public void SetPosition(string pos)
            {
                switch (pos)
                {
                    case DeviceDef.BCSDefPOS_ETOP:
                        ScanPosition = SKPOS.EntryTOP;
                        break;
                    case DeviceDef.BCSDefPOS_ESK01:
                        ScanPosition = SKPOS.Entry_SK01;
                        break;
                    case DeviceDef.BCSDefPOS_DTOP:
                        ScanPosition = SKPOS.DeliveryTop;
                        break;
                    case DeviceDef.BCSDefPOS_DSK01:
                        ScanPosition = SKPOS.Delivery_SK01;
                        break;
                    case DeviceDef.BCSDefPOS_DSK02:
                        ScanPosition = SKPOS.Delivery_SK02;
                        break;
                }
            }
        }



    }
}

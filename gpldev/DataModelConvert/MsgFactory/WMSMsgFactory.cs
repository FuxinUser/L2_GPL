using Core.Define;
using Core.Util;
using DataMod.BarCode;
using DataMod.WMS.LogicModel;
using DBService.Repository.PDO;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace MsgConvert
{
    public static class WMSMsgFactory
    {
        public static L2_WMS_Snd.GWx1_ScheduleList GW01ScheduleMsg(List<string> coilsOfTable)
        {
            var coilIDStr = string.Join("", coilsOfTable.ToArray());

            var gw01 = new L2_WMS_Snd.GWx1_ScheduleList()
            {
                //Header
                MessageID = WMSSysDef.SndMsgCode.CoilScheduleInfo.ToCharArray(),
                Length = Marshal.SizeOf<L2_WMS_Snd.GWx1_ScheduleList>().ToString("0000").PadRight(4, ' ').ToCharArray(),   //0000 補成四碼
                ProcessDateTime = DateTime.Now.ToString("yyyyMMddHHmmss").PadRight(14, ' ').ToCharArray(),
                IDOfDestinationComputer = WMSSysDef.WMS.ToCChar(2),
                IDOfSourceComputer = L2SystemDef.L2.ToCChar(2),

                //Data
                Count = coilsOfTable.Count.ToString().ToCChar(4),
                CoilNo = coilIDStr.ToCChar(6000),
                Spare = " ".ToCChar(10),
            };

            return gw01;
        }

        public static L2_WMS_Snd.GWx3_CoilInfo GW03CoilInfo(this WMSPdoInfomation pdo){

            var gw03 = new L2_WMS_Snd.GWx3_CoilInfo
            {
                //Header
                MessageID = WMSSysDef.SndMsgCode.CoilPDO.ToCharArray(),
                Length = Marshal.SizeOf<L2_WMS_Snd.GWx3_CoilInfo>().ToString("0000").PadRight(4, ' ').ToCharArray(),   //0000 補成四碼
                ProcessDateTime = DateTime.Now.ToString("yyyyMMddHHmmss").PadRight(14, ' ').ToCharArray(),
                IDOfDestinationComputer = WMSSysDef.WMS.ToCChar(2),
                IDOfSourceComputer = L2SystemDef.L2.ToCChar(2),

                InCoilNo = pdo.In_Mat_No.ToCChar(20),
                OutCoilNo = pdo.Out_Mat_No.ToCChar(20),
                OrderNo = pdo.OrderNo.ToCChar(20),
                PackType = " ".ToCChar(5),
                InnerDia = pdo.Out_Mat_Inner.ToCChar(4),
                OuterDia = pdo.Out_Mat_Outer_Diameter.ToCChar(4),
                CoilThick = pdo.Out_Mat_Thick.ToString("0.000").ToCChar(9),
                CoilWidth = pdo.Out_Mat_Width.ToString("0.0").ToCChar(7),
                CoilWeight = pdo.Out_Mat_Wt.ToString().ToCChar(5),
                CoilTurn = pdo.Winding_Dire.Equals(DeviceDef.WindingDirectionUp) ? WMSSysDef.Cmd.WMSWindingDirectionUp.ToCChar(1) : WMSSysDef.Cmd.WMSWindingDirectionDown.ToCChar(1),
                CoilContainsOil = pdo.Oil_Flag.ToCChar(1),
                Next_WholeBackLog_Code = "CR".ToCChar(2),
                Sleeve_InnerDia = pdo.Sleeve_InnerDia.ToCChar(4),
                Sleeve_Thick = pdo.Sleeve_Thick.ToCChar(9),
                Sleeve_Width = pdo.Sleeve_Width.ToCChar(7),
                Spare = " ".ToCChar(16),
            };

            return gw03;
        }

        public static L2_WMS_Snd.GWx2_TrackingMap GW02TrackInfo(this L1L2Rcv.Msg_105_Trk_Map msg)
        {
            var gw02 = new L2_WMS_Snd.GWx2_TrackingMap
            {
                //Header
                MessageID = WMSSysDef.SndMsgCode.EntryDeliveryTrk.ToCharArray(),
                Length = Marshal.SizeOf<L2_WMS_Snd.GWx2_TrackingMap>().ToString("0000").PadRight(4, ' ').ToCharArray(),   //0000 補成四碼
                ProcessDateTime = DateTime.Now.ToString("yyyyMMddHHmmss").PadRight(14, ' ').ToCharArray(),
                IDOfDestinationComputer = WMSSysDef.WMS.ToCChar(2),
                IDOfSourceComputer = L2SystemDef.L2.ToCChar(2),


                sk11 = msg.Entry_SK01.ToCChar(20),
                sk12 = string.Empty.ToCChar(20),
                sk13 = msg.Entry_TOP.ToCChar(20),
                TopSensor1 = string.Empty.ToCChar(1),
                EntryExit1 = WMSSysDef.Cmd.Wx02TrkEntry.ToCChar(1),

                sk21 = msg.Delivery_SK01.ToCChar(20),
                sk22 = msg.Delivery_SK02.ToCChar(20),
                sk23 = msg.Delivery_TOP.ToCChar(20),
                TopSensor2 = string.Empty.ToCChar(1),
                EntryExit2 = WMSSysDef.Cmd.Wx02TrkDelivery.ToCChar(1),

                sk31 = string.Empty.ToCChar(20),
                sk32 = string.Empty.ToCChar(20),
                sk33 = string.Empty.ToCChar(20),
                TopSensor3 = string.Empty.ToCChar(1),
                EntryExit3 = string.Empty.ToCChar(1),

                LineSatus = string.Empty.ToCChar(1),
                Spare = string.Empty.ToCChar(7),

            };

            return gw02;
        }

        
        public static L2_WMS_Snd.GWx5_FeedingRequest_EntryExitReturn GW05ReqMsg(this ProdLineCoilReq prodLineCoilReq)
        {
            var gw05 = new L2_WMS_Snd.GWx5_FeedingRequest_EntryExitReturn()
            {
                //Header
                MessageID = WMSSysDef.SndMsgCode.ProdLineCoilReq.ToCharArray(),
                Length = Marshal.SizeOf<L2_WMS_Snd.GWx5_FeedingRequest_EntryExitReturn>().ToString("0000").PadLeft(4, ' ').ToCharArray(),
                ProcessDateTime = DateTime.Now.ToString("yyyyMMddHHmmss").ToCharArray(),
                IDOfDestinationComputer = WMSSysDef.WMS.ToCChar(2),
                IDOfSourceComputer = L2SystemDef.L2.ToCChar(2),
                //Data
                Flag = prodLineCoilReq.Flag.ToCChar(1),
                SKIDNo = prodLineCoilReq.Pos.ToCChar(1),   // 待修正
                CoilNo = prodLineCoilReq.CoilNo.ToCChar(20),
                coilTurn = prodLineCoilReq.CoilTurn.ToCChar(1),
                Spare = prodLineCoilReq.Spare.ToCChar(11),
            };

            return gw05;
        }

        public static L2_WMS_Snd.GWx6_ScanCoil GW06ScanCoil(int skIDNo, string coilNo)
        {
            var gw06 = new L2_WMS_Snd.GWx6_ScanCoil();

            gw06.MessageID = WMSSysDef.SndMsgCode.InfoScanID.ToCharArray();
            gw06.Length = Marshal.SizeOf<L2_WMS_Snd.GWx6_ScanCoil>().ToString("0000").PadLeft(4, ' ').ToCharArray();
            gw06.ProcessDateTime = DateTime.Now.ToString("yyyyMMddHHmmss").ToCharArray();
            gw06.IDOfDestinationComputer = WMSSysDef.WMS.ToCChar(2);
            gw06.IDOfSourceComputer = L2SystemDef.L2.ToCChar(2);

            gw06.SkidNo = skIDNo.ToCChar(1);
            gw06.CoilNo = coilNo.ToCChar(20);
            gw06.Spare = string.Empty.ToCChar(13);

            return gw06;

        }


        public static WMSPdoInfomation ConvertWMSPdoInfo(this PDOEntity.TBL_PDO pdo,string SLV_inner,string SLV_thick,string SLV_width)
        {
            var pdoInfo = new WMSPdoInfomation
            {
                Out_Mat_No = pdo.Out_Coil_ID,
                In_Mat_No = pdo.In_Coil_ID,
                OrderNo = pdo.Order_No,
                //PackType = pdo.                                   
                Out_Mat_Inner = pdo.Out_Coil_Inner_Diameter,
                Out_Mat_Outer_Diameter = pdo.Out_Coil_Outer_Diameter,
                Out_Mat_Thick = pdo.Out_Coil_Thick,
                Out_Mat_Width = pdo.Out_Coil_Width,
                //毛重(秤重值)
                //Out_Mat_Wt = (int)pdo.Out_Coil_Theoretical_Weight,
                Out_Mat_Wt = (int)pdo.Out_Coil_Gross_WT,
                Winding_Dire = pdo.Winding_Dire,
                Oil_Flag = pdo.Oil_Flag,
                //套筒內徑
                Sleeve_InnerDia = SLV_inner,
                //套筒厚度
                Sleeve_Thick= SLV_thick,
                //套筒寬度
                Sleeve_Width = SLV_width,
            };

            return pdoInfo;
        }


    }
}

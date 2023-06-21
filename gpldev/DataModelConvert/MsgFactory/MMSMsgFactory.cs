using Core.Define;
using Core.Util;
using DBService.AggregationModel;
using DBService.Repository;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using static DataMod.Common.MMSMsgProResultModel;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using static DBService.Repository.DefectData.CoilDefectDataEntity;
using static DBService.Repository.LineFaultRecords.LineFaultRecordsEntity;
using static DBService.Repository.PDO.PDOEntity;
using static MsgStruct.L2MMSSnd;

namespace MsgConvert
{
    public static class MMSMsgFactory
    {

     
        public static L2MMSSnd.Msg_Coil_Schedule_Changed CoilScheduleChangedMsg(List<string> coilsOfTable)
        {
            var coilIDStr = string.Join("", coilsOfTable.ToArray());
            var CoilSchChange = new L2MMSSnd.Msg_Coil_Schedule_Changed()
            {
                Code = MMSSysDef.SndMsgCode.CoilScheduleChangedCode.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.SysCode.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length = CalculateLength<L2MMSSnd.Msg_Coil_Schedule_Changed>(),

                Number_Of_Coils = coilsOfTable.Count.ToNByteArray(3),
                Entry_Coil_No = coilIDStr.ToCByteArray(6000),
            };
            return CoilSchChange;


        }

        public static Msg_Coil_Loaded_Skid CoilLoadSkidMsg(string planNo, string entryCoilNo)
        {
            var SystemIDCode = "GP";
            var coilLoadSkid = new Msg_Coil_Loaded_Skid()
            {
                Code = MMSSysDef.SndMsgCode.CoilLoadedSkidCode.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.SysCode.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length = CalculateLength<Msg_Coil_Loaded_Skid>(),

                Plan_No = planNo.ToCByteArray(10),
                Entry_Coil_No = entryCoilNo.ToCByteArray(20),
                Loaded_Time = DateTime.Now.ToString("yyyyMMddHHmmss").ToCByteArray(14),
                Unit_Code = SystemIDCode.ToCByteArray(4),//L2SystemDef.SystemIDCode.ToCByteArray(4),


            };

            return coilLoadSkid;


        }

        public static Msg_Coil_Reject_Result CoilRejectResult(CoilRejectReultModel entity)
        {
            var msg = new Msg_Coil_Reject_Result();
            //Header
            msg.Code = MMSSysDef.SndMsgCode.CoilRejectDataCode.ToCByteArray(6);
            msg.FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1);
            msg.SendWho = L2SystemDef.L2.ToCByteArray(2);
            msg.RcvWho = MMSSysDef.SysCode.ToCByteArray(2);
            msg.Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8);
            msg.Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6);
            msg.Length = CalculateLength<L2MMSSnd.Msg_Coil_Reject_Result>();
            //Data
            msg.Reject_Coil_No = entity.CoilRejectResult.Reject_Coil_No.ToCByteArray(20);
            msg.Entry_CoilNo = entity.CoilRejectResult.Entry_CoilNo.ToCByteArray(20);
            msg.Plan_No = entity.CoilRejectResult.Plan_No.ToCByteArray(10);
            msg.Mode_Of_Reject = entity.CoilRejectResult.Mode_Of_Reject.ToCByteArray(1);

            msg.Length_Of_Rejected_Coil = entity.CoilRejectResult.Length_Of_Rejected_Coil.ToNByteArray(5);
            msg.Weight_Of_Rejected_Coil = entity.CoilRejectResult.Weight_Of_Rejected_Coil.ToNByteArray(5);

            msg.Inner_Diameter_Of_RejectedCoil = entity.CoilRejectResult.Inner_Diameter_Of_RejectedCoil.ToNByteArray(4);
            msg.Outer_Diameter_Of_RejectedCoil = entity.CoilRejectResult.Outer_Diameter_Of_RejectedCoil.ToNByteArray(4);
            msg.Reason_Of_Reject = entity.CoilRejectResult.Reason_Of_Reject.ToCByteArray(4);
            msg.Time_Of_Reject = entity.CoilRejectResult.Time_Of_Reject.ToCByteArray(14);
            msg.Shift_Of_Reject = entity.CoilRejectResult.Shift_Of_Reject.ToCByteArray(1);
            msg.Turn_Of_Reject = entity.CoilRejectResult.Turn_Of_Reject.ToCByteArray(1);
            msg.Paper_exit_Code = entity.CoilRejectResult.Paper_exit_Code.ToCByteArray(1);
            msg.Paper_Type = entity.CoilRejectResult.Paper_Type.ToCByteArray(4);
            msg.Finally_Tag = entity.CoilRejectResult.Final_Coil_Flag.ToCByteArray(1);

            msg.Head_Paper_Length = entity.CoilRejectResult.Head_Paper_Length.ToNByteArray(5);
            msg.Tail_Paper_Length = entity.CoilRejectResult.Tail_Paper_Length.ToNByteArray(5);
            msg.Head_Paper_Width = entity.CoilRejectResult.Head_Paper_Width.ToNByteArray(5);
            msg.Tail_Paper_Width = entity.CoilRejectResult.Tail_Paper_Width.ToNByteArray(5);

            // Load Defect
            if (entity.CoilDefects != null && entity.CoilDefects.Count() > 0)
                msg.LoadDefectData(entity.CoilDefects);
            else
                msg.LoadEmptyDefectData();

            return msg;
        }

        public static Msg_PDO ToMMSPDOMsg(this TBL_PDO tblPDO)
        {

            var pdo = new L2MMSSnd.Msg_PDO();

            // Header
            pdo.Code = MMSSysDef.SndMsgCode.CoilPDOCode.ToCByteArray(6);         //6
            pdo.FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1);     //1
            pdo.SendWho = L2SystemDef.L2.ToCByteArray(2);              //2
            pdo.RcvWho = MMSSysDef.SysCode.ToCByteArray(2);              //2
            pdo.Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8); //8
            pdo.Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6);   //6
            pdo.Length = CalculateLength<L2MMSSnd.Msg_PDO>();    //4

            // Data
            pdo.OrderNo = tblPDO.Order_No.ToCByteArray(10);   //10
            pdo.PlanNo = tblPDO.Plan_No.ToCByteArray(10);     //10
            pdo.Out_Mat_No = tblPDO.Out_Coil_ID.ToCByteArray(20); //20
            pdo.In_Mat_No = tblPDO.In_Coil_ID.ToCByteArray(20);   //20
            pdo.StartTime = tblPDO.Start_Time.ToString("yyyyMMddHHmmss").ToCByteArray(14);  //14
            pdo.FinishTime = tblPDO.Finish_Time.ToString("yyyyMMddHHmmss").ToCByteArray(14);    //14
            pdo.Shift = tblPDO.Shift.ToCByteArray(1); //1
            pdo.Team = tblPDO.Team.ToCByteArray(1);//1
            pdo.St_No = tblPDO.St_No.ToCByteArray(8);//8

            pdo.Out_Mat_Outer_Diameter = tblPDO.Out_Coil_Outer_Diameter.ToNByteArray(4);//4
            pdo.Out_Mat_Inner = tblPDO.Out_Coil_Inner_Diameter.ToNByteArray(4);//4
            pdo.Out_Mat_Length = tblPDO.Out_Coil_Length.ToNByteArray(5);//5
            pdo.Out_Mat_Thick = tblPDO.Out_Coil_Thick.ToNByteArray(5,1000);//5

            pdo.Head_C40_Thick = tblPDO.Out_Coil_Head_C40_Thick.ToNByteArray(5,1000);//5
            pdo.Mid_C40_Thick = tblPDO.Out_Coil_Mid_C40_Thick.ToNByteArray(5,1000);//5
            pdo.Tail_C40_Thick = tblPDO.Out_Coil_Tail_C40_Thick.ToNByteArray(5,1000);//5

            pdo.Head_C25_Thick = tblPDO.Out_Coil_Head_C25_Thick.ToNByteArray(5,1000);//5
            pdo.Mid_C25_Thick = tblPDO.Out_Coil_Mid_C25_Thick.ToNByteArray(5, 1000);//5
            pdo.Tail_C25_Thick = tblPDO.Out_Coil_Tail_C25_Thick.ToNByteArray(5, 1000);//5

            pdo.Out_Mat_Width = tblPDO.Out_Coil_Width.ToNByteArray(5,10);//5
            pdo.Out_Mat_Wt = tblPDO.Out_Coil_Act_WT.ToNByteArray(5);//5
            pdo.Out_Mat_Gross_Wt = tblPDO.Out_Coil_Gross_WT.ToNByteArray(5);//5

            pdo.Head_Pass_Num = tblPDO.Head_Pass_Num.ToNByteArray(2);//2
            pdo.Mid_Pass_Num = tblPDO.Mid_Pass_Num.ToNByteArray(2);//2
            pdo.Tail_Pass_Num = tblPDO.Mid_Pass_Num.ToNByteArray(2);//2

            pdo.ExitSleeveUseOrNot = tblPDO.Out_Coil_Use_Sleeve_Flag.ToCByteArray(1);//1
            pdo.ExitSleeveDiameter = tblPDO.Out_Sleeve_Diameter.ToNByteArray(4);//4

            pdo.ExitSleeveCode = tblPDO.Out_Sleeve_Type_Code.ToCByteArray(4);//4
            pdo.Paper_Req_Code = tblPDO.Out_Paper_Req_Code .ToCByteArray(1);//1
            pdo.Paper_Code = tblPDO.Out_Paper_Code.ToCByteArray(3);//3

            pdo.Head_Paper_Length = tblPDO.Head_Paper_Length.ToNByteArray(5);//5
            pdo.Head_Paper_Width = tblPDO.Head_Paper_Width.ToNByteArray(5,10);//5
            pdo.Tail_Paper_Length = tblPDO.Tail_Paper_Length.ToNByteArray(5);//5
            pdo.Tail_Paper_Width = tblPDO.Tail_Paper_Width.ToNByteArray(5,10);//5

            // DefectCode
            pdo.D1_Code = tblPDO.D01_Code.ToCByteArray(3);
            pdo.D1_Origin = tblPDO.D01_Origin.ToCByteArray(3);
            pdo.D1_Sid = tblPDO.D01_Sid.ToCByteArray(1);
            pdo.D1_Pos_W = tblPDO.D01_Pos_W.ToCByteArray(1);
            pdo.D1_Pos_L_Start = tblPDO.D01_Pos_L_Start.ToString().ToCByteArray(4);
            pdo.D1_Pos_L_End = ((int)tblPDO.D01_Pos_L_End).ToString().ToCByteArray(4);
            pdo.D1_Level = tblPDO.D01_Level.ToCByteArray(1);
            pdo.D1_Percent = tblPDO.D01_Percent.ToNByteArray(3);
            pdo.D1_QGrade = tblPDO.D01_QGrade.ToCByteArray(1);


            pdo.D2_Code = tblPDO.D02_Code.ToCByteArray(3);
            pdo.D2_Origin = tblPDO.D02_Origin.ToCByteArray(3);
            pdo.D2_Sid = tblPDO.D02_Sid.ToCByteArray(1);
            pdo.D2_Pos_W = tblPDO.D02_Pos_W.ToCByteArray(1);
            pdo.D2_Pos_L_Start = tblPDO.D02_Pos_L_Start.ToString().ToCByteArray(4);
            pdo.D2_Pos_L_End = ((int)tblPDO.D02_Pos_L_End).ToString().ToCByteArray(4);
            pdo.D2_Level = tblPDO.D02_Level.ToCByteArray(1);
            pdo.D2_Percent = tblPDO.D02_Percent.ToNByteArray(3);
            pdo.D2_QGrade = tblPDO.D02_QGrade.ToCByteArray(1);

            pdo.D3_Code = tblPDO.D03_Code.ToCByteArray(3);
            pdo.D3_Origin = tblPDO.D03_Origin.ToCByteArray(3);
            pdo.D3_Sid = tblPDO.D03_Sid.ToCByteArray(1);
            pdo.D3_Pos_W = tblPDO.D03_Pos_W.ToCByteArray(1);
            pdo.D3_Pos_L_Start = tblPDO.D03_Pos_L_Start.ToString().ToCByteArray(4);
            pdo.D3_Pos_L_End = tblPDO.D03_Pos_L_End.ToString().ToCByteArray(4);
            pdo.D3_Level = tblPDO.D03_Level.ToCByteArray(1);
            pdo.D3_Percent = tblPDO.D03_Percent.ToNByteArray(3);
            pdo.D3_QGrade = tblPDO.D03_QGrade.ToCByteArray(1);

            pdo.D4_Code = tblPDO.D04_Code.ToCByteArray(3);
            pdo.D4_Origin = tblPDO.D04_Origin.ToCByteArray(3);
            pdo.D4_Sid = tblPDO.D04_Sid.ToCByteArray(1);
            pdo.D4_Pos_W = tblPDO.D04_Pos_W.ToCByteArray(1);
            pdo.D4_Pos_L_Start = tblPDO.D04_Pos_L_Start.ToString().ToCByteArray(4);
            pdo.D4_Pos_L_End = tblPDO.D04_Pos_L_End.ToString().ToCByteArray(4);
            pdo.D4_Level = tblPDO.D04_Level.ToCByteArray(1);
            pdo.D4_Percent = tblPDO.D04_Percent.ToNByteArray(3);
            pdo.D4_QGrade = tblPDO.D04_QGrade.ToCByteArray(1);

            pdo.D5_Code = tblPDO.D05_Code.ToCByteArray(3);
            pdo.D5_Origin = tblPDO.D05_Origin.ToCByteArray(3);
            pdo.D5_Sid = tblPDO.D05_Sid.ToCByteArray(1);
            pdo.D5_Pos_W = tblPDO.D05_Pos_W.ToCByteArray(1);
            pdo.D5_Pos_L_Start = tblPDO.D05_Pos_L_Start.ToString().ToCByteArray(4);
            pdo.D5_Pos_L_End = tblPDO.D05_Pos_L_End.ToString().ToCByteArray(4);
            pdo.D5_Level = tblPDO.D05_Level.ToCByteArray(1);
            pdo.D5_Percent = tblPDO.D05_Percent.ToNByteArray(3);
            pdo.D5_QGrade = tblPDO.D05_QGrade.ToCByteArray(1);

            pdo.D6_Code = tblPDO.D06_Code.ToCByteArray(3);
            pdo.D6_Origin = tblPDO.D06_Origin.ToCByteArray(3);
            pdo.D6_Sid = tblPDO.D06_Sid.ToCByteArray(1);
            pdo.D6_Pos_W = tblPDO.D06_Pos_W.ToCByteArray(1);
            pdo.D6_Pos_L_Start = tblPDO.D06_Pos_L_Start.ToString().ToCByteArray(4);
            pdo.D6_Pos_L_End = tblPDO.D06_Pos_L_End.ToString().ToCByteArray(4);
            pdo.D6_Level = tblPDO.D06_Level.ToCByteArray(1);
            pdo.D6_Percent = tblPDO.D06_Percent.ToNByteArray(3);
            pdo.D6_QGrade = tblPDO.D06_QGrade.ToCByteArray(1);

            pdo.D7_Code = tblPDO.D07_Code.ToCByteArray(3);
            pdo.D7_Origin = tblPDO.D07_Origin.ToCByteArray(3);
            pdo.D7_Sid = tblPDO.D07_Sid.ToCByteArray(1);
            pdo.D7_Pos_W = tblPDO.D07_Pos_W.ToCByteArray(1);
            pdo.D7_Pos_L_Start = tblPDO.D07_Pos_L_Start.ToString().ToCByteArray(4);
            pdo.D7_Pos_L_End = tblPDO.D07_Pos_L_End.ToString().ToCByteArray(4);
            pdo.D7_Level = tblPDO.D07_Level.ToCByteArray(1);
            pdo.D7_Percent = tblPDO.D07_Percent.ToNByteArray(3);
            pdo.D7_QGrade = tblPDO.D07_QGrade.ToCByteArray(1);

            pdo.D8_Code = tblPDO.D08_Code.ToCByteArray(3);
            pdo.D8_Origin = tblPDO.D08_Origin.ToCByteArray(3);
            pdo.D8_Sid = tblPDO.D08_Sid.ToCByteArray(1);
            pdo.D8_Pos_W = tblPDO.D08_Pos_W.ToCByteArray(1);
            pdo.D8_Pos_L_Start = tblPDO.D08_Pos_L_Start.ToString().ToCByteArray(4);
            pdo.D8_Pos_L_End = tblPDO.D08_Pos_L_End.ToString().ToCByteArray(4);
            pdo.D8_Level = tblPDO.D08_Level.ToCByteArray(1);
            pdo.D8_Percent = tblPDO.D08_Percent.ToNByteArray(3);
            pdo.D8_QGrade = tblPDO.D08_QGrade.ToCByteArray(1);

            pdo.D9_Code = tblPDO.D09_Code.ToCByteArray(3);
            pdo.D9_Origin = tblPDO.D09_Origin.ToCByteArray(3);
            pdo.D9_Sid = tblPDO.D09_Sid.ToCByteArray(1);
            pdo.D9_Pos_W = tblPDO.D09_Pos_W.ToCByteArray(1);
            pdo.D9_Pos_L_Start = tblPDO.D09_Pos_L_Start.ToString().ToCByteArray(4);
            pdo.D9_Pos_L_End = tblPDO.D09_Pos_L_End.ToString().ToCByteArray(4);
            pdo.D9_Level = tblPDO.D09_Level.ToCByteArray(1);
            pdo.D9_Percent = tblPDO.D09_Percent.ToNByteArray(3);
            pdo.D9_QGrade = tblPDO.D09_QGrade.ToCByteArray(1);

            pdo.D10_Code = tblPDO.D10_Code.ToCByteArray(3);
            pdo.D10_Origin = tblPDO.D10_Origin.ToCByteArray(3);
            pdo.D10_Sid = tblPDO.D10_Sid.ToCByteArray(1);
            pdo.D10_Pos_W = tblPDO.D10_Pos_W.ToCByteArray(1);
            pdo.D10_Pos_L_Start = tblPDO.D10_Pos_L_Start.ToString().ToCByteArray(4);
            pdo.D10_Pos_L_End = tblPDO.D10_Pos_L_End.ToString().ToCByteArray(4);
            pdo.D10_Level = tblPDO.D10_Level.ToCByteArray(1);
            pdo.D10_Percent = tblPDO.D10_Percent.ToNByteArray(3);
            pdo.D10_QGrade = tblPDO.D10_QGrade.ToCByteArray(1);


            pdo.Winding_Dire = tblPDO.Winding_Dire.ToCByteArray(1);//1
            pdo.BaseSurface = tblPDO.Better_Surf_Ward_Code.ToCByteArray(1);//1
            pdo.Inspector = tblPDO.Hold_Maker.ToCByteArray(10);//10
            pdo.Hold_Flag = tblPDO.Hold_Flag.ToCByteArray(1);//1
            pdo.Hold_Cause_Code = tblPDO.Hold_Cause_Code.ToCByteArray(4);//4
            pdo.Sample_Flag = tblPDO.Sample_Flag.ToCByteArray(1);//1
            pdo.Fixed_Wt_Flag = tblPDO.Fixed_Wt_Flag.ToCByteArray(1);//1
            pdo.End_Flag = tblPDO.Final_Coil_Flag.ToCByteArray(1);//1
            pdo.Scrap_Flag = tblPDO.Scrap_Flag.ToCByteArray(1);//1
            pdo.Sample_Frqn_Code = tblPDO.Sample_Pos_Code.ToCByteArray(3);//3
            pdo.Surface_Accu_Code = tblPDO.Surface_Accu_Code.ToCByteArray(2);//2

            pdo.Head_Hole_Position = tblPDO.Head_Hole_Position.ToNByteArray(7,1000);//7
            pdo.Head_LeaderLength = tblPDO.Head_Leader_Length.ToNByteArray(5);//5
            pdo.Head_Leader_Width = tblPDO.Head_Leader_Width.ToNByteArray(5,10);//5
            pdo.Head_Leader_Thickness = tblPDO.Head_Leader_Thickness.ToNByteArray(5,1000);//5

            pdo.Tail_Hole_Position = tblPDO.Tail_Hole_Position.ToNByteArray(7,1000);//7
            pdo.Tail_LeaderLength = tblPDO.Tail_Leader_Length.ToNByteArray(5);//5
            pdo.Tail_Leader_Width = tblPDO.Tail_Leader_Width.ToNByteArray(5,10);//5
            pdo.Tail_Leader_Thickness = tblPDO.Tail_Leader_Thickness.ToNByteArray(5,1000);

            pdo.Head_Leader_St_No = tblPDO.Head_Leader_St_No.ToCByteArray(8);//8
            pdo.Tail_Leader_St_No = tblPDO.Tail_Leader_St_No.ToCByteArray(8);//8

            pdo.Head_Off_Gauge = tblPDO.Head_Off_Gauge.ToNByteArray(5);//5
            pdo.Tail_Off_Gauge = tblPDO.Tail_Off_Gauge.ToNByteArray(5);//5

            pdo.Pre_Grinding_Surface = tblPDO.Pre_Grinding_Surface.ToCByteArray(1);//1
            pdo.Grinding_Count_Out = tblPDO.Grinding_Count_Out.ToString().ToCByteArray(2);//2
            pdo.Grinding_Count_In = tblPDO.Grinding_Count_In.ToString().ToCByteArray(2);//2

            pdo.Appoint_Grinding_Surface = tblPDO.Appoint_Grinding_Surface.ToCByteArray(1);//1

            pdo.Oil_Flag = tblPDO.Oil_Flag.ToCByteArray(1);//1

            pdo.Surface_Accu_Code_In = tblPDO.Surface_Accu_Code_In.ToCByteArray(2);//2
            pdo.Surface_Accu_Code_Out = tblPDO.Surface_Accu_Code_Out.ToCByteArray(2);//2

            pdo.Process_Code = tblPDO.Process_Code.ToCByteArray(6);//6
            //pdo.Head_Rough = tblPDO.Head_Rough.ToNByteArray(4, 100);//4
            //pdo.Mid_Rough = tblPDO.Mid_Rough.ToNByteArray(4, 100);//4
            //pdo.Tail_Rough = tblPDO.Tail_Rough.ToNByteArray(4, 100);//4
            pdo.Head_Rough_Ra = tblPDO.Head_Rough_Ra.ToNByteArray(5, 1000);//5
            pdo.Mid_Rough_Ra = tblPDO.Mid_Rough_Ra.ToNByteArray(5, 1000);//5
            pdo.Tail_Rough_Ra = tblPDO.Tail_Rough_Ra.ToNByteArray(5, 1000);//5
          

            pdo.Uncoil_Direction = tblPDO.Uncoil_Direction.ToCByteArray(1);//1
            //var tmp = 12;
            //tblPDO.Recoiler_ActTen_Avg
            pdo.Recoiler_Actten_Avg = tblPDO.Recoiler_ActTen_Avg.ToNByteArray(7);//4

            //新增
            pdo.Head_Rough_Rz = tblPDO.Head_Rough_Rz.ToNByteArray(5, 1000);//5
            pdo.Mid_Rough_Rz = tblPDO.Mid_Rough_Rz.ToNByteArray(5, 1000);//5
            pdo.Tail_Rough_Rz = tblPDO.Tail_Rough_Rz.ToNByteArray(5, 1000);//5
            pdo.Head_Rough_Rmax = tblPDO.Head_Rough_Rmax.ToNByteArray(5, 1000);//5
            pdo.Mid_Rough_Rmax = tblPDO.Mid_Rough_Rmax.ToNByteArray(5, 1000);//5
            pdo.Tail_Rough_Rmax = tblPDO.Tail_Rough_Rmax.ToNByteArray(5, 1000);//5

            return pdo;


        }

        public static L2MMSSnd.Msg_Res_For_Coil_Schedule CoilSchedRes(ProResult proResult)
        {
            var coilSchedRes = new L2MMSSnd.Msg_Res_For_Coil_Schedule()
            {
                Code = MMSSysDef.SndMsgCode.ResForCoilSchedCode.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.SysCode.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length = CalculateLength<L2MMSSnd.Msg_Res_For_Coil_Schedule>(),

                Requested_Coil_No = proResult.No.ToCByteArray(20),
                Process_Result = proResult.Result.ToCByteArray(1),
                Reject_Cause = proResult.RejectCause.ToCByteArray(80),

            };

            return coilSchedRes;


        }

        public static L2MMSSnd.Msg_Res_For_Coil_PDI CoilPDIProRes(ProResult proResult)
        {
            var coilPDIRes = new L2MMSSnd.Msg_Res_For_Coil_PDI()
            {
                Code = MMSSysDef.SndMsgCode.ResForCoilPDICode.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.SysCode.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length = CalculateLength<L2MMSSnd.Msg_Res_For_Coil_PDI>(),

                RequestedCoilNo = proResult.No.ToCByteArray(20),
                ProcessResult = proResult.Result.ToCByteArray(1),
                RejectCause = proResult.RejectCause.ToCByteArray(80),
            };

            return coilPDIRes;
        }

        public static L2MMSSnd.Msg_Req_Coil_Schedule ReqCoilSchedule(string coilID)
        {

            var askSchedule = new L2MMSSnd.Msg_Req_Coil_Schedule()
            {

                Code = MMSSysDef.SndMsgCode.ReqForCoilSchedCode.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.SysCode.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length = CalculateLength<L2MMSSnd.Msg_Req_Coil_Schedule>(),
                Requested_Coil_No = coilID.ToCByteArray(20)
            };

            return askSchedule;
        }

        public static L2MMSSnd.Msg_Request_Coil_PDI ReqCoilPDI(string coilID)
        {
            var askCoilPDI = new L2MMSSnd.Msg_Request_Coil_PDI
            {
                Code = MMSSysDef.SndMsgCode.ReqForPDICode.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.SysCode.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length =CalculateLength<L2MMSSnd.Msg_Request_Coil_PDI>(),

                Requested_Coil_No = coilID.ToCByteArray(20)
            };

            return askCoilPDI;
        }

        public static L2MMSSnd.Msg_Res_For_PlanNo_Delete ResPlanNoDelete(ProResult proResult)
        {
            var resPlanNoDeleteResult = new L2MMSSnd.Msg_Res_For_PlanNo_Delete
            {
                Code = MMSSysDef.SndMsgCode.ResDeletePlanNoResultCode.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.SysCode.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length =CalculateLength<L2MMSSnd.Msg_Res_For_PlanNo_Delete>(),

                Plan_No = proResult.No.ToCByteArray(10),
                ProcessResult = proResult.Result.ToCByteArray(1),
                RejectCause = proResult.RejectCause.ToCByteArray(80)
            };

            return resPlanNoDeleteResult;
        }
        // 上傳能源消耗訊息
        public static L2MMSSnd.Msg_Energy_Consumption_Info UploadEnergyConsumptionInfo(CS15_Utility value)
        {
            var sndMsg = new L2MMSSnd.Msg_Energy_Consumption_Info();
            foreach (FieldInfo fi in sndMsg.GetType().GetFields())
            {
                if (fi.FieldType == typeof(byte[]))
                {
                    var ma = fi.GetCustomAttribute<MarshalAsAttribute>();
                    fi.SetValue(sndMsg, "".ToNByteArray(ma.SizeConst));

                }
            }
            sndMsg.Code = MMSSysDef.SndMsgCode.EnergyConsumptionInfoCode.ToCByteArray(6);
            sndMsg.FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1);
            sndMsg.SendWho = L2SystemDef.L2.ToCByteArray(2);
            sndMsg.RcvWho = MMSSysDef.MMS.ToCByteArray(2);
            sndMsg.Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8);
            sndMsg.Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6);
            sndMsg.Length = CalculateLength<L2MMSSnd.Msg_Energy_Consumption_Info>();

            sndMsg.Shift_Date = value.Shift_Date.ToCByteArray(8);
            sndMsg.Shift_No = value.Shift_No.ToCByteArray(1);
            sndMsg.Group_No = value.Group_No.ToCByteArray(1);
            sndMsg.Unit_code = value.Unit_code.ToCByteArray(4);

            sndMsg.IDCW_1 = value.TWater.ToNByteArray(20, 1000000);      //冷却水
            sndMsg.CA_1 = value.TAir.ToNByteArray(20, 1000000);          //压缩空气
            sndMsg.Steam_1 = value.TSteam.ToNByteArray(20, 1000000);     //蒸汽
            sndMsg.DeW_1 = value.TRWater.ToNByteArray(20, 1000000);      //冲洗水

            return sndMsg;
        }
        public static L2MMSSnd.Msg_Energy_Consumption_Info EnergyConsumptionInfo(L1L2Rcv.Msg_112_Utility msg)
        {
            var enerfyConInfo = new L2MMSSnd.Msg_Energy_Consumption_Info();
            foreach (FieldInfo fi in enerfyConInfo.GetType().GetFields())
            {
                if (fi.FieldType == typeof(byte[]))
                {
                    var ma = fi.GetCustomAttribute<MarshalAsAttribute>();
                    fi.SetValue(enerfyConInfo, "".PadRight(ma.SizeConst).ToCByteArray(26));
                }
            }
            enerfyConInfo.Code = MMSSysDef.SndMsgCode.EnergyConsumptionInfoCode.ToCByteArray(6);
            enerfyConInfo.FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1);
            enerfyConInfo.SendWho = L2SystemDef.L2.ToCByteArray(2);
            enerfyConInfo.RcvWho = MMSSysDef.SysCode.ToCByteArray(2);
            enerfyConInfo.Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8);
            enerfyConInfo.Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6);
            enerfyConInfo.Length =CalculateLength<L2MMSSnd.Msg_Energy_Consumption_Info>();
            enerfyConInfo.IDCW_1 = msg.IndirectCollingWater.ToNByteArray(26);
            enerfyConInfo.CA_1 = msg.CompressedAir.ToNByteArray(26);
            return enerfyConInfo;
        }

        public static L2MMSSnd.Msg_Coil_Schedule_Delete CoilSchDelMsg(CS03_ScheduleChange message)
        {
            var coilSchDelete = new L2MMSSnd.Msg_Coil_Schedule_Delete()
            {
                //Header
                Code = MMSSysDef.SndMsgCode.CoilScheduleDeleteCode.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.SysCode.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length =CalculateLength<L2MMSSnd.Msg_Coil_Schedule_Delete>(),
                //Data
                EntryCoilNo = message.EntryCoilID.ToCByteArray(20),
                OperatorId = message.OperatorID.ToCByteArray(10),
                ReasonCode = message.ReasonCode.ToCByteArray(4)
            };
            return coilSchDelete;


        }

        public static L2MMSSnd.Msg_Dummy_Coil_List_Result_Request ConvertReqDummyMsg(this CS19_RequestDummy reqDummyCoil)
        {
            var reqDummy = new L2MMSSnd.Msg_Dummy_Coil_List_Result_Request
            {
                //Header
                Code = MMSSysDef.SndMsgCode.ReqDummyCoilCode.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.SysCode.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length =CalculateLength<L2MMSSnd.Msg_Dummy_Coil_List_Result_Request>(),
                //Data
                Mat_No = reqDummyCoil.DummyCoil.ToCByteArray(20)
            };
            return reqDummy;
        }

        public static L2MMSSnd.Msg_Dummy_Coil_List_Delete ConvertDelDummyMsg(this CS20_DeleteDummy delDummyCoil)
        {
            var delDummy = new L2MMSSnd.Msg_Dummy_Coil_List_Delete
            {
                //Header
                Code = MMSSysDef.SndMsgCode.DelDummyCoilCode.ToCByteArray(6),
                FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1),
                SendWho = L2SystemDef.L2.ToCByteArray(2),
                RcvWho = MMSSysDef.SysCode.ToCByteArray(2),
                Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8),
                Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6),
                Length =CalculateLength<L2MMSSnd.Msg_Dummy_Coil_List_Delete>(),
                //Data
                Mat_No = delDummyCoil.DummyCoil.ToCByteArray(20),
                Reason_Code = delDummyCoil.ReasonCode.ToCByteArray(5),
                Delete_Time = delDummyCoil.DeleteTime.ToString("yyyyMMddHHmmss").ToCByteArray(14),


            };
            return delDummy;
        }

        public static Msg_Equipment_Down_Result_Msg ToEquipmentDownResult(this TBL_LineFaultRecords value, string action)
        {
            var msg = new Msg_Equipment_Down_Result_Msg();

            msg.Code = MMSSysDef.SndMsgCode.EqDownResultCode.ToCByteArray(6);
            msg.FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1);
            msg.SendWho = L2SystemDef.L2.ToCByteArray(2);
            msg.RcvWho = MMSSysDef.SysCode.ToCByteArray(2);
            msg.Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8);
            msg.Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6);
            msg.Length = CalculateLength<Msg_Equipment_Down_Result_Msg>();

            // 1-新增 2-删除
            msg.Op_Flag = action.ToCByteArray(1);
            msg.Unit_Code = value.unit_code.ToCByteArray(4);
            // YYYYMMDD
            msg.Prod_Time = value.prod_time.ToString("yyyyMMdd").ToCByteArray(8);
            msg.Prod_Shift_No = value.prod_shift_no.ToCByteArray(1);
            msg.Prod_Shift_Group = value.prod_shift_group.ToCByteArray(1);
            // yyyyMMddHHmmss
            msg.Stop_Start_Time = value.stop_start_time.ToString("yyyyMMddHHmmss").ToCByteArray(14);
            // yyyyMMddHHmmss
            msg.Stop_End_Time = value.stop_end_time.ToString("yyyyMMddHHmmss").ToCByteArray(14);
            msg.Delay_Location = value.delay_location.ToCByteArray(6);
            msg.Delay_Location_Desc = value.delay_location_desc.ToCByteArray(30);
            msg.Stop_Elased_Time = value.stop_elased_timey.ToCByteArray(7);
            msg.Delay_Reason_Code = value.delay_reason_code.ToCByteArray(2);
            msg.Delay_Reason_Desc = value.delay_reason_desc.ToCByteArray(50);
            msg.Delay_Remark = value.delay_remark.ToCByteArray(200);
            msg.Resp_Depart_Delay_Time_m = value.resp_depart_delay_time_m.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_e = value.resp_depart_delay_time_e.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_c = value.resp_depart_delay_time_c.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_p = value.resp_depart_delay_time_p.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_u = value.resp_depart_delay_time_u.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_o = value.resp_depart_delay_time_o.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_r = value.resp_depart_delay_time_r.ToCByteArray(7);
            msg.Resp_Depart_Delay_Time_rs = value.resp_depart_delay_time_rs.ToCByteArray(7);
            msg.Deceleration_Cauese = value.deceleration_cause.ToCByteArray(200);
            msg.Deceleration_Code = value.deceleration_code.ToCByteArray(10);
            return msg;
        }



        //public static L2MMSSnd.Msg_Equipment_Down_Result_Msg ConvertEqDownResultMsg()
        //{
        //    var delDummy = new L2MMSSnd.Msg_Equipment_Down_Result_Msg();

        //    //Header
        //    delDummy.Code = MMSSysDef.RcvMsgCode.EqDownResultCode.ToCByteArray(6);
        //    delDummy.FuncCode = MMSSysDef.DataCode.DataMsg.ToCByteArray(1);
        //    delDummy.SendWho = L2SystemDef.L2.ToCByteArray(2);
        //    delDummy.RcvWho = MMSSysDef.SysCode.ToCByteArray(2);
        //    delDummy.Date = DateTime.Now.ToString("yyyyMMdd").ToCByteArray(8);
        //    delDummy.Time = DateTime.Now.ToString("HHmmss").ToCByteArray(6);
        //    delDummy.Length =CalculateLength<L2MMSSnd.Msg_Equipment_Down_Result_Msg>();

        //    delDummy.Op_Flag = string.Empty.ToCByteArray(1);

        //    delDummy.Unit_Code = string.Empty.ToCByteArray(4);
        //    delDummy.Unit_Code = string.Empty.ToCByteArray(4);
        //    delDummy.Prod_Time = string.Empty.ToCByteArray(8);
        //    delDummy.Prod_Shift_No = string.Empty.ToCByteArray(1);
        //    delDummy.Prod_Shift_Group = string.Empty.ToCByteArray(1);
        //    delDummy.Stop_Start_Time = string.Empty.ToCByteArray(14);
        //    delDummy.Stop_End_Time = string.Empty.ToCByteArray(14);
        //    delDummy.Delay_Location = string.Empty.ToCByteArray(6);
        //    delDummy.Delay_Location_Desc = string.Empty.ToCByteArray(30);
        //    delDummy.Stop_Elased_Time = string.Empty.ToCByteArray(7);
        //    delDummy.Delay_Reason_Code = string.Empty.ToCByteArray(2);
        //    delDummy.Delay_Reason_Desc = "Test".ToCByteArray(50);
        //    delDummy.Delay_Remark = string.Empty.ToCByteArray(200);
        //    delDummy.Resp_Depart_Delay_Time_m = string.Empty.ToNByteArray(7);
        //    delDummy.Resp_Depart_Delay_Time_e = string.Empty.ToNByteArray(7);
        //    delDummy.Resp_Depart_Delay_Time_c = string.Empty.ToNByteArray(7);
        //    delDummy.Resp_Depart_Delay_Time_p = string.Empty.ToNByteArray(7);
        //    delDummy.Resp_Depart_Delay_Time_u = string.Empty.ToNByteArray(7);
        //    delDummy.Resp_Depart_Delay_Time_o = string.Empty.ToNByteArray(7);
        //    delDummy.Resp_Depart_Delay_Time_r = string.Empty.ToNByteArray(7);
        //    delDummy.Resp_Depart_Delay_Time_rs = string.Empty.ToNByteArray(7);
        //    delDummy.Deceleration_Cauese = "Test".ToCByteArray(200);
        //    delDummy.Deceleration_Code = "Test".ToCByteArray(10);

        //    return delDummy;
        //}

        public static string TransferMMSDeffectPosWidthType(this short defectPos)
        {
            if (defectPos == MMSSysDef.Cmd.DefectDriveSide)
                return MMSSysDef.Cmd.DefectPosWidthDriveSide;

            if (defectPos == MMSSysDef.Cmd.DefectCenter)
                return MMSSysDef.Cmd.DefectPosWidthCenter;

            if (defectPos == MMSSysDef.Cmd.DefectWorkSide)
                return MMSSysDef.Cmd.DefectPosWidthWorkSide;

            return string.Empty;
        }

        public static void LoadDefectData(this Msg_Coil_Reject_Result coilRejectResut, IEnumerable<L3L2_TBL_DefectData> defectDatas)
        {
            int cnt = 1;

            foreach (L3L2_TBL_DefectData defectData in defectDatas)
            {
                switch (cnt)
                {
                    case 1:                 
                        coilRejectResut.D1_Code = defectData.DefectCode.ToString().ToCByteArray(3);
                        coilRejectResut.D1_Origin = defectData.DefectOrigin.ToCByteArray(3);
                        coilRejectResut.D1_Sid = defectData.DefectSide.ToCByteArray(1);
                        coilRejectResut.D1_Pos_W = defectData.DefectPositionWidthDirection.ToCByteArray(1);
                        coilRejectResut.D1_Pos_L_Start = defectData.DefectPosLengthStartDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D1_Pos_L_End =  defectData.DefectPoLengthEndDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D1_Level = DefectClass(defectData.DefectLevel).ToCByteArray(1);
                        coilRejectResut.D1_Percent = defectData.DefectPercent.ToCByteArray(3);
                        coilRejectResut.D1_QGrade = string.Empty.ToCByteArray(1);
                        break;

                    case 2:
                        coilRejectResut.D2_Code = defectData.DefectCode.ToString().ToCByteArray(3);
                        coilRejectResut.D2_Origin = defectData.DefectOrigin.ToCByteArray(3);
                        coilRejectResut.D2_Sid = defectData.DefectSide.ToCByteArray(1);
                        coilRejectResut.D2_Pos_W = defectData.DefectPositionWidthDirection.ToCByteArray(1);
                        coilRejectResut.D2_Pos_L_Start = defectData.DefectPosLengthStartDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D2_Pos_L_End = defectData.DefectPoLengthEndDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D2_Level = DefectClass(defectData.DefectLevel).ToCByteArray(1);
                        coilRejectResut.D2_Percent = defectData.DefectPercent.ToCByteArray(3);
                        coilRejectResut.D2_QGrade = string.Empty.ToCByteArray(1);
                        break;

                    case 3:
                        coilRejectResut.D3_Code = defectData.DefectCode.ToString().ToCByteArray(3);
                        coilRejectResut.D3_Origin = defectData.DefectOrigin.ToCByteArray(3);
                        coilRejectResut.D3_Sid = defectData.DefectSide.ToCByteArray(1);
                        coilRejectResut.D3_Pos_W = defectData.DefectPositionWidthDirection.ToCByteArray(1);
                        coilRejectResut.D3_Pos_L_Start = defectData.DefectPosLengthStartDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D3_Pos_L_End = defectData.DefectPoLengthEndDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D3_Level = DefectClass(defectData.DefectLevel).ToCByteArray(1);
                        coilRejectResut.D3_Percent = defectData.DefectPercent.ToCByteArray(3);
                        coilRejectResut.D3_QGrade = string.Empty.ToCByteArray(1);
                        break;

                    case 4:
                        coilRejectResut.D4_Code = defectData.DefectCode.ToString().ToCByteArray(3);
                        coilRejectResut.D4_Origin = defectData.DefectOrigin.ToCByteArray(3);
                        coilRejectResut.D4_Sid = defectData.DefectSide.ToCByteArray(1);
                        coilRejectResut.D4_Pos_W = defectData.DefectPositionWidthDirection.ToCByteArray(1);
                        coilRejectResut.D4_Pos_L_Start = defectData.DefectPosLengthStartDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D4_Pos_L_End = defectData.DefectPoLengthEndDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D4_Level = DefectClass(defectData.DefectLevel).ToCByteArray(1);
                        coilRejectResut.D4_Percent = defectData.DefectPercent.ToCByteArray(3);
                        coilRejectResut.D4_QGrade = string.Empty.ToCByteArray(1);
                        break;

                    case 5:
                        coilRejectResut.D5_Code = defectData.DefectCode.ToString().ToCByteArray(3);
                        coilRejectResut.D5_Origin = defectData.DefectOrigin.ToCByteArray(3);
                        coilRejectResut.D5_Sid = defectData.DefectSide.ToCByteArray(1);
                        coilRejectResut.D5_Pos_W = defectData.DefectPositionWidthDirection.ToCByteArray(1);
                        coilRejectResut.D5_Pos_L_Start = defectData.DefectPosLengthStartDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D5_Pos_L_End = defectData.DefectPoLengthEndDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D5_Level = DefectClass(defectData.DefectLevel).ToCByteArray(1);
                        coilRejectResut.D5_Percent = defectData.DefectPercent.ToCByteArray(3);
                        coilRejectResut.D5_QGrade = string.Empty.ToCByteArray(1);


                        break;

                    case 6:
                        coilRejectResut.D6_Code = defectData.DefectCode.ToString().ToCByteArray(3);
                        coilRejectResut.D6_Origin = defectData.DefectOrigin.ToCByteArray(3);
                        coilRejectResut.D6_Sid = defectData.DefectSide.ToCByteArray(1);
                        coilRejectResut.D6_Pos_W = defectData.DefectPositionWidthDirection.ToCByteArray(1);
                        coilRejectResut.D6_Pos_L_Start = defectData.DefectPosLengthStartDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D6_Pos_L_End = defectData.DefectPoLengthEndDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D6_Level = DefectClass(defectData.DefectLevel).ToCByteArray(1);
                        coilRejectResut.D6_Percent = defectData.DefectPercent.ToCByteArray(3);
                        coilRejectResut.D6_QGrade = string.Empty.ToCByteArray(1);

                        break;

                    case 7:
                        coilRejectResut.D7_Code = defectData.DefectCode.ToString().ToCByteArray(3);
                        coilRejectResut.D7_Origin = defectData.DefectOrigin.ToCByteArray(3);
                        coilRejectResut.D7_Sid = defectData.DefectSide.ToCByteArray(1);
                        coilRejectResut.D7_Pos_W = defectData.DefectPositionWidthDirection.ToCByteArray(1);
                        coilRejectResut.D7_Pos_L_Start = defectData.DefectPosLengthStartDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D7_Pos_L_End = defectData.DefectPoLengthEndDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D7_Level = DefectClass(defectData.DefectLevel).ToCByteArray(1);
                        coilRejectResut.D7_Percent = defectData.DefectPercent.ToCByteArray(3);
                        coilRejectResut.D7_QGrade = string.Empty.ToCByteArray(1);


                        break;

                    case 8:

                        coilRejectResut.D8_Code = defectData.DefectCode.ToString().ToCByteArray(3);
                        coilRejectResut.D8_Origin = defectData.DefectOrigin.ToCByteArray(3);
                        coilRejectResut.D8_Sid = defectData.DefectSide.ToCByteArray(1);
                        coilRejectResut.D8_Pos_W = defectData.DefectPositionWidthDirection.ToCByteArray(1);
                        coilRejectResut.D8_Pos_L_Start = defectData.DefectPosLengthStartDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D8_Pos_L_End = defectData.DefectPoLengthEndDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D8_Level = DefectClass(defectData.DefectLevel).ToCByteArray(1);
                        coilRejectResut.D8_Percent = defectData.DefectPercent.ToCByteArray(3);
                        coilRejectResut.D8_QGrade = string.Empty.ToCByteArray(1);

                        break;

                    case 9:

                        coilRejectResut.D9_Code = defectData.DefectCode.ToString().ToCByteArray(3);
                        coilRejectResut.D9_Origin = defectData.DefectOrigin.ToCByteArray(3);
                        coilRejectResut.D9_Sid = defectData.DefectSide.ToCByteArray(1);
                        coilRejectResut.D9_Pos_W = defectData.DefectPositionWidthDirection.ToCByteArray(1);
                        coilRejectResut.D9_Pos_L_Start = defectData.DefectPosLengthStartDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D9_Pos_L_End = defectData.DefectPoLengthEndDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D9_Level = DefectClass(defectData.DefectLevel).ToCByteArray(1);
                        coilRejectResut.D9_Percent = defectData.DefectPercent.ToCByteArray(3);
                        coilRejectResut.D9_QGrade = string.Empty.ToCByteArray(1);

                        break;

                    case 10:

                        coilRejectResut.D10_Code = defectData.DefectCode.ToString().ToCByteArray(3);
                        coilRejectResut.D10_Origin = defectData.DefectOrigin.ToCByteArray(3);
                        coilRejectResut.D10_Sid = defectData.DefectSide.ToCByteArray(1);
                        coilRejectResut.D10_Pos_W = defectData.DefectPositionWidthDirection.ToCByteArray(1);
                        coilRejectResut.D10_Pos_L_Start = defectData.DefectPosLengthStartDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D10_Pos_L_End = defectData.DefectPoLengthEndDirection.ToString().ToCByteArray(4);
                        coilRejectResut.D10_Level = DefectClass(defectData.DefectLevel).ToCByteArray(1);
                        coilRejectResut.D10_Percent = defectData.DefectPercent.ToCByteArray(3);
                        coilRejectResut.D10_QGrade = string.Empty.ToCByteArray(1);

                        break;
                }

                cnt++;
            }
        }


        public static void LoadEmptyDefectData(this Msg_Coil_Reject_Result coilRejectResut)
        {
            coilRejectResut.D1_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D1_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D1_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D1_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D1_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D1_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D1_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D1_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D1_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D2_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D2_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D2_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D2_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D2_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D2_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D2_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D2_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D2_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D3_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D3_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D3_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D3_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D3_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D3_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D3_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D3_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D3_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D4_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D4_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D4_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D4_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D4_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D4_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D4_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D4_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D4_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D5_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D5_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D5_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D5_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D5_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D5_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D5_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D5_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D5_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D6_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D6_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D6_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D6_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D6_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D6_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D6_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D6_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D6_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D7_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D7_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D7_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D7_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D7_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D7_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D7_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D7_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D7_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D8_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D8_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D8_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D8_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D8_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D8_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D8_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D8_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D8_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D9_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D9_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D9_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D9_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D9_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D9_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D9_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D9_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D9_QGrade = string.Empty.ToCByteArray(1);

            coilRejectResut.D10_Code = string.Empty.ToCByteArray(3);
            coilRejectResut.D10_Origin = string.Empty.ToCByteArray(3);
            coilRejectResut.D10_Sid = string.Empty.ToCByteArray(1);
            coilRejectResut.D10_Pos_W = string.Empty.ToCByteArray(1);
            coilRejectResut.D10_Pos_L_Start = string.Empty.ToCByteArray(4);
            coilRejectResut.D10_Pos_L_End = string.Empty.ToCByteArray(4);
            coilRejectResut.D10_Level = string.Empty.ToCByteArray(1);
            coilRejectResut.D10_Percent = string.Empty.ToCByteArray(3);
            coilRejectResut.D10_QGrade = string.Empty.ToCByteArray(1);

        }



        private static string DefectClass(string defectLevel)
        {
            if (defectLevel.Equals(PlcSysDef.Cmd.DefectGradeA) || defectLevel.Equals(PlcSysDef.Cmd.DefectGradeB))
                return MMSSysDef.Cmd.DefectClassL;

            if (defectLevel.Equals(PlcSysDef.Cmd.DefectGradeC))
                return MMSSysDef.Cmd.DefectClassM;

            if (defectLevel.Equals(PlcSysDef.Cmd.DefectGradeD))
                return MMSSysDef.Cmd.DefectClassH;

            if (defectLevel.Equals(PlcSysDef.Cmd.DefectGradeE))
                return MMSSysDef.Cmd.DefectClassS;

            return string.Empty;
        }

        private static byte[] CalculateLength<T>()
        {
            // +1 為結尾符號長度
            return (Marshal.SizeOf<T>() + MMSSysDef.EndTagLength).ToString("0000").ToNByteArray(MMSSysDef.MsgLenLength);
        } 
    }
}

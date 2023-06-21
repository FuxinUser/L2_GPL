using DBService.Level25Repository.L2L25_CoilMap;
using DBService.Level25Repository.L2L25_CoilPDI;
using DBService.Level25Repository.L2L25_CoilPDO;
using DBService.Level25Repository.L2L25_DownTime;
using DBService.Level25Repository.L2L25_ENGC;
using DBService.Level25Repository.L2L25_L1L2DisConnection;
using DBService.Level25Repository.L2L25_LineSpeedCT;
using DBService.Level25Repository.L2L25_LineTensionCT;
using DBService.Level25Repository.L2L25_LineRunDirectionCT;
using DBService.Level25Repository.L2L25_No1GRAbrasiveBeltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No2GRAbrasiveBeltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No3GRAbrasiveBeltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No4GRAbrasiveBeltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No5GRAbrasiveBeltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No6GRAbrasiveBeltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No1GRAbrasiveBeltSpeedCT;
using DBService.Level25Repository.L2L25_No2GRAbrasiveBeltSpeedCT;
using DBService.Level25Repository.L2L25_No1BrushRollCurrentCT;
using DBService.Level25Repository.L2L25_No2BrushRollCurrentCT;
using DBService.Level25Repository.L2L25_ProcessCT;
using DBService.Level25Repository.L2L25_CurrentPassNumberCT;
using DBService.Level25Repository.L2L25_CurrentSessionCT;
using DBService.Level25Repository.L2L25_GRDLineSpeedCT;
using DBService.Level25Repository.L2L25_No1GRAB_beltKindCT;
using DBService.Level25Repository.L2L25_No1GRAB_beltRoughnessCT;
using DBService.Level25Repository.L2L25_No1GRAB_beltRotateDirectionCT;
using DBService.Level25Repository.L2L25_No1GRAB_beltMotorCurrentCT;
using DBService.Level25Repository.L2L25_No1GRAB_beltSpeedCT;
using DBService.Level25Repository.L2L25_No2GRAB_beltKindCT;
using DBService.Level25Repository.L2L25_No2GRAB_beltRoughnessCT;
using DBService.Level25Repository.L2L25_No2GRAB_beltRotateDirectionCT;
using DBService.Level25Repository.L2L25_No2GRAB_beltCurrentCT;
using DBService.Level25Repository.L2L25_No2GRAB_beltSpeedCT;
using DBService.Level25Repository.L2L25_No3GRAB_beltKindCT;
using DBService.Level25Repository.L2L25_No3GRAB_beltRoughnessCT;
using DBService.Level25Repository.L2L25_No3GRAB_beltRotateDirectionCT;
using DBService.Level25Repository.L2L25_No3GRAB_beltCurrentCT;
using DBService.Level25Repository.L2L25_No4GRAB_beltKindCT;
using DBService.Level25Repository.L2L25_No4GRAB_beltRoughnessCT;
using DBService.Level25Repository.L2L25_No4GRAB_beltRotateDirectionCT;
using DBService.Level25Repository.L2L25_No4GRAB_beltCurrentCT;
using DBService.Level25Repository.L2L25_No5GRAB_beltKindCT;
using DBService.Level25Repository.L2L25_No5GRAB_beltRoughnessCT;
using DBService.Level25Repository.L2L25_No5GRAB_beltRotateDirectionCT;
using DBService.Level25Repository.L2L25_No5GRAB_beltCurrentCT;
using DBService.Level25Repository.L2L25_No6GRAB_beltKindCT;
using DBService.Level25Repository.L2L25_No6GRAB_beltRoughnessCT;
using DBService.Level25Repository.L2L25_No6GRAB_beltRotateDirectionCT;
using DBService.Level25Repository.L2L25_No6GRAB_beltCurrentCT;
using DBService.Level25Repository.L2L25_CoilRejectResult;

using System;
using static Core.Define.DBParaDef.ConnectionSysDef;
using static Core.Define.L25SysDef;
using static DBService.Repository.CoilRejResultEntity;
using static DBService.Repository.ConnectionStatus.ConnectionStatusEntity;
using static DBService.Repository.LineFaultRecords.LineFaultRecordsEntity;
using static DBService.Repository.PDI.PDIEntity;
using static DBService.Repository.PDO.PDOEntity;
using static DBService.Repository.ReturnCoil.ReturnCoilEntity;
using static MsgStruct.L1L2Rcv;
using Core.Util;
using static DBService.Repository.GrindRecords.GrindRecordsEntity;
using static DBService.Repository.BeltPatterns.BeltPatternsEntity;
using static DBService.Repository.BeltPatternsRecords.BeltPatternsRecordEntity;
using static DBService.Repository.CoilScheduleDelete.CoilScheduleDeleteEntity;
using static DBService.Repository.CutReocrd.CoilCutRecordEntity;
using static DBService.Repository.DefectData.CoilDefectDataEntity;
using static DBService.Repository.LookupTblPaper.LkUpTablePaperEntity;
using static DBService.Repository.LookupTblSleeve.LkUpTableSleeveEntity;
using static DBService.Repository.ScheduleDeleteRecordTempEntity;
using static DBService.Repository.StripBrakeSignal.StripBrakeSignalEntity;
using static DBService.Repository.UmountRecord.UmountRecordEntity;
using static DBService.Repository.Utility.UtilityEntity;
using Core.Define;
using DBService.Repository.PDI;
using MsgStruct;
using DBService.Repository.LineStatus;
using DBService.Repository;
using DBService.Repository.Utility;
using DBService.Repository.WieldRecord;
using DataMod.Common;
namespace MsgConvert.DBTable
{
    public static class EntityFactory
    {

		// TBL_RetrunCoil_Temp -> TBL_CoilRejectResult
		public static TBL_CoilRejectResult ToCoilRejectReuslt(this TBL_RetrunCoil_Temp dao)
        {

			var entity = new TBL_CoilRejectResult();
			entity.Reject_Coil_No = dao.Reject_Coil_No;
			entity.Entry_CoilNo = dao.Entry_CoilNo;
			entity.OriPDI_Out_Coil_ID = dao.OriPDI_Out_Coil_ID;
			entity.Plan_No = dao.Plan_No;
			entity.Mode_Of_Reject = dao.Mode_Of_Reject;
			entity.Length_Of_Rejected_Coil = dao.Length_Of_Rejected_Coil;
			entity.Width_Of_RejectedCoil = dao.Width_Of_RejectedCoil;
			entity.Weight_Of_Rejected_Coil = dao.Weight_Of_Rejected_Coil;
			entity.Inner_Diameter_Of_RejectedCoil = dao.Inner_Diameter_Of_RejectedCoil;
			entity.Outer_Diameter_Of_RejectedCoil = dao.Outer_Diameter_Of_RejectedCoil;
			entity.Reason_Of_Reject = dao.Reason_Of_Reject;
			entity.Time_Of_Reject = dao.Time_Of_Reject;
			entity.Shift_Of_Reject = dao.Shift_Of_Reject;
			entity.Turn_Of_Reject = dao.Turn_Of_Reject;
			entity.Paper_exit_Code = dao.Paper_exit_Code;
			entity.Paper_Type = dao.Paper_Type;
			entity.Final_Coil_Flag = dao.Final_Coil_Flag;
			entity.Head_Paper_Length = dao.Head_Paper_Length;
			entity.Head_Paper_Width = dao.Head_Paper_Width;
			entity.Tail_Paper_Length = dao.Tail_Paper_Length;
			entity.Tail_Paper_Width = dao.Tail_Paper_Width;
			entity.Reject_Skid = dao.Reject_Skid;
			entity.UserID = dao.UserID;
			return entity;
		}
        // TBL_RetrunCoil_Temp -> L2L25_CoilRejectResult
        public static L2L25_CoilRejectResult ToL25CoilRejectReuslt(this TBL_RetrunCoil_Temp dao) 
        {
            var entity = new L2L25_CoilRejectResult();
            entity.Reject_Coil_No = dao.Reject_Coil_No;
            entity.Entry_CoilNo = dao.Entry_CoilNo;
            entity.Mode_Of_Reject = dao.Mode_Of_Reject;
            entity.Length_Of_Rejected_Coil = dao.Length_Of_Rejected_Coil;
            entity.Weight_Of_Rejected_Coil = dao.Weight_Of_Rejected_Coil;
            entity.Inner_Diameter_Of_RejectedCoil = dao.Inner_Diameter_Of_RejectedCoil;
            entity.Outer_Diameter_Of_RejectedCoil = dao.Outer_Diameter_Of_RejectedCoil;
            entity.Reason_Of_Reject = dao.Reason_Of_Reject;
            entity.Time_Of_Reject = dao.Time_Of_Reject;
            entity.Shift_Of_Reject = dao.Shift_Of_Reject;
            entity.Turn_Of_Reject = dao.Turn_Of_Reject;
            return entity;
        }

        // Msg_105_Trk_Map -> L2L25_CoilMap
        public static L2L25_CoilMap ToL2L25_CoilMap(this Msg_105_Trk_Map msg)
        {
            var entity = new L2L25_CoilMap();
            entity.Message_Id = MsgCode.Msg147CoilMap;
            entity.Message_Length = MsgLength.Msg147CoilMap;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");

            entity.POR = msg.POR;
            entity.Entry_SK01 = msg.Entry_SK01;
            entity.EntryTOP = msg.Entry_TOP;
            entity.Entry_Car = msg.Entry_Car;
            entity.TR = msg.TR;
            entity.Delivery_SK01 = msg.Delivery_SK01;
            entity.Delivery_SK02 = msg.Delivery_SK02;
            entity.Delivery_TOP = msg.Delivery_TOP;
            entity.Delivery_Car = msg.Delivery_Car;
            return entity;
        }

        // To TBL_ConnectionStatus
        public static TBL_ConnectionStatus ToTblConnectionStatusEntity(ConnectionType connType, string ip, string port, string connectionStatut)
        {
            var tb = new TBL_ConnectionStatus();

            switch (connType)
            {
                //L2->PLC
                case ConnectionType.L2ConnectToPLC:
                    tb.Connection_From = L2;
                    tb.Connection_To = L1;
                    break;
                //L2<- MMS
                case ConnectionType.L2ConnectedByMMS:
                    tb.Connection_From = MMS;
                    tb.Connection_To = L2;
                    break;
                //L2-> MMS
                case ConnectionType.L2ConnectToMMS:
                    tb.Connection_From = L2;
                    tb.Connection_To = MMS;
                    break;
                //L2 <-WMS
                case ConnectionType.L2ConnectedByWMS:
                    tb.Connection_From = WMS;
                    tb.Connection_To = L2;
                    break;
                //L2 -> WMS
                case ConnectionType.L2ConnectToWMS:
                    tb.Connection_From = L2;
                    tb.Connection_To = WMS;
                    break;
                //L2 <- PLC
                case ConnectionType.L2ConnectedByPLC:
                    tb.Connection_From = L1;
                    tb.Connection_To = L2;
                    break;
                //L2 <- PLCCyc
                case ConnectionType.L2ConnectedByPLCCycle:
                    tb.Connection_From = L1_CYC;
                    tb.Connection_To = L2;
                    break;
            }

            tb.Connection_IP = ip;
            tb.Connection_Port = port;
            tb.Connection_Status = connectionStatut;
            tb.Create_DateTime = DateTime.Now;

            return tb;
        }

        // To L2L25_L1L2DisConnection
        public static L2L25_L1L2DisConnection ToL2L25L1L2DisConnection(this ConnectionType connType, string connectionStatuts)
        {
            var entity = new L2L25_L1L2DisConnection();
            entity.Message_Id = MsgCode.Msg148L1L2DisConnection;
            entity.Message_Length = MsgLength.Msg148L1L2DisConnection;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");


            switch (connType)
            {
                //L2->PLC
                case ConnectionType.L2ConnectToPLC:
                    entity.SystemID_3 = connectionStatuts;
                    break;
                //L2<- MMS
                case ConnectionType.L2ConnectedByMMS:
                    entity.SystemID_1 = connectionStatuts;
                    break;
                //L2-> MMS
                case ConnectionType.L2ConnectToMMS:
                    entity.SystemID_1 = connectionStatuts;
                    break;
                //L2 <-WMS
                case ConnectionType.L2ConnectedByWMS:
                    entity.SystemID_4 = connectionStatuts;
                    break;
                //L2 -> WMS
                case ConnectionType.L2ConnectToWMS:
                    entity.SystemID_4 = connectionStatuts;
                    break;
                ////L2 <- PLC
                //case ConnectionType.L2ConnectedByPLC:
                //    entity.SystemID_5 = connectionStatuts;
                //    break;
                ////L2 <- PLCCyc
                //case ConnectionType.L2ConnectedByPLCCycle:
                //    entity.SystemID_6 = connectionStatuts;
                //    break;

            }


            return entity;
        }

        // 316 -> L2L25_ENGC
        public static L2L25_ENGC ToL25EngcEntity(this Msg_112_Utility msg)
        {
            var entity = new L2L25_ENGC();

            entity.Message_Id = MsgCode.Msg104ENGC;
            entity.Message_Length = MsgLength.Msg104ENGC;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");
            entity.CompressedAir = msg.CompressedAir.ToString();
            entity.IndirectCollingWater = msg.IndirectCollingWater.ToString();
            return entity;

        }

        // TBL_LineFaultRecords -> L2L25_DownTime
        public static L2L25_DownTime ToL25DownTimeEntity(this TBL_LineFaultRecords dao)
        {
            var entity = new L2L25_DownTime();

            entity.Message_Id = MsgCode.Msg103DownTime;
            entity.Message_Length = MsgLength.Msg103DownTime;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");
            entity.Time = DateTime.Now.ToString("HHmmss");


            entity.op_flag = "1";   //1:暫時只做新增
            entity.unit_code = dao.unit_code;
            entity.prod_time = dao.prod_time.ToString("yyyyMMdd");
            entity.prod_shift_no = dao.prod_shift_no;
            entity.prod_shift_group = dao.prod_shift_group;
            entity.stop_start_time = dao.stop_start_time.ToString("yyyyMMddHHmmss");
            entity.stop_end_time = dao.stop_end_time.ToString("yyyyMMddHHmmss");
            entity.delay_location = dao.delay_location;
            entity.delay_location_desc = dao.delay_location_desc;
            entity.stop_elased_timey = dao.stop_elased_timey;
            entity.delay_reason_code = dao.delay_reason_code;
            entity.delay_remark = dao.delay_remark;
            entity.unit_code = dao.unit_code;
            entity.delay_reason_desc = dao.delay_reason_desc;
            entity.resp_depart_delay_time_m = dao.resp_depart_delay_time_m;
            entity.resp_depart_delay_time_e = dao.resp_depart_delay_time_e;
            entity.resp_depart_delay_time_c = dao.resp_depart_delay_time_c;
            entity.resp_depart_delay_time_p = dao.resp_depart_delay_time_p;
            entity.resp_depart_delay_time_u = dao.resp_depart_delay_time_u;
            entity.resp_depart_delay_time_o = dao.resp_depart_delay_time_o;
            entity.resp_depart_delay_time_r = dao.resp_depart_delay_time_r;
            entity.resp_depart_delay_time_rs = dao.resp_depart_delay_time_rs;
            entity.deceleration_cause = dao.deceleration_cause;
            entity.deceleration_code = dao.deceleration_code;
            
            return entity;

        }

		// TBL_PDI -> L2L25_CoilPDI
		public static L2L25_CoilPDI ToL25CoilPDIEntity(this TBL_PDI dao)
        {
            var entity = new L2L25_CoilPDI();
			entity.Message_Id = MsgCode.Msg101PDI;
			entity.Message_Length = MsgCode.Msg101PDI;
			entity.Date = DateTime.Now.ToString("yyyyMMdd");
			entity.Time = DateTime.Now.ToString("HHmmss");

			entity.Plan_No = dao.Plan_No;
			entity.Mat_Seq_No = dao.Mat_Seq_No.ToString();
			entity.Plan_Type = dao.Plan_Type;
			entity.In_Coil_ID = dao.In_Coil_ID;
			entity.In_Coil_Thick = dao.In_Coil_Thick.ToString();
			entity.In_Coil_Width = dao.In_Coil_Width.ToString();
			entity.In_Coil_Wt = dao.In_Coil_Wt.ToString();
			entity.In_Coil_Length= dao.In_Coil_Length.ToString();
			entity.In_Coil_Inner_Diameter= dao.In_Coil_Inner_Diameter.ToString();
			entity.In_Coil_Outer_Diameter = dao.In_Coil_Outer_Diameter.ToString();
			entity.In_Paper_Req_Code= dao.In_Paper_Req_Code;
			entity.In_Paper_Code= dao.In_Paper_Code;
			entity.Head_Paper_Length= dao.Head_Paper_Length.ToString();
			entity.Head_Paper_Width= dao.Head_Paper_Width.ToString();
			entity.Tail_Paper_Length= dao.Tail_Paper_Length.ToString();
			entity.Tail_Paper_Width= dao.Tail_Paper_Width.ToString();
			entity.In_Sleeve_Type_Code= dao.In_Sleeve_Type_Code;
			entity.In_Sleeve_Diameter= dao.In_Sleeve_Diameter.ToString();
			entity.St_No= dao.St_No;
			entity.Density= dao.Density.ToString();
			entity.Surface_Accuracy_Acture= dao.Surface_Accuracy_Acture;
			entity.Surface_Accu_Code_Order= dao.Surface_Accu_Code_Order;
			entity.Flatness_Avg_CR= dao.Flatness_Avg_CR.ToString();
			entity.Better_Surf_Ward_Code= dao.Better_Surf_Ward_Code;
			entity.UnCoil_Direction= dao.Uncoil_Direction;
			entity.Origin_Code= dao.Origin_Code;
			entity.Pack_Mode= dao.Pack_Mode.ToString();
			entity.Out_Paper_Req_Code= dao.Out_Paper_Req_Code;
			entity.Out_Paper_Code= dao.Out_Paper_Code;
			entity.Out_Sleeve_Type_Code= dao.Out_Sleeve_Type_Code;
			entity.Out_Sleeve_Diamter= dao.Out_Sleeve_Diamter.ToString();
			entity.Out_Coil_ID= dao.Out_Coil_ID;
			entity.Order_No= dao.Order_No;
			entity.Order_Cust_Code= dao.Order_Cust_Code;
			entity.Prev_Whole_Backlog_Code= dao.Prev_Whole_Backlog_Code;
			entity.Next_Whole_Backlog_Code= dao.Next_Whole_Backlog_Code;
			entity.Head_Leader_Attached= dao.Head_Leader_Attached;
			entity.Head_Hole_Position= dao.Head_Hole_Position.ToString();
			entity.Head_Leader_Thickness= dao.Head_Leader_Thickness.ToString();
			entity.Head_Leader_Width= dao.Head_Leader_Width.ToString();
			entity.Head_Leader_Length= dao.Head_Leader_Length.ToString();
			entity.Tail_Leader_Attached= dao.Tail_Leader_Attached;
			entity.Tail_Hole_Position= dao.Tail_Hole_Position.ToString();
			entity.Tail_Leader_Thickness= dao.Tail_Leader_Thickness.ToString();
			entity.Tail_Leader_Width= dao.Tail_Leader_Width.ToString();
			entity.Tail_Leader_Length= dao.Tail_Leader_Length.ToString();
			entity.Head_Leader_St_No= dao.Head_Leader_St_No;
			entity.Tail_Leader_St_No= dao.Tail_Leader_St_No;
			entity.Out_Coil_Thick= dao.Out_Coil_Thick.ToString();
			entity.Out_Coil_Thick_Min= dao.Out_Coil_Thick_Min.ToString();
			entity.Out_Coil_Thick_Max= dao.Out_Coil_Thick_Max.ToString();
			entity.Out_Coil_Width= dao.Out_Coil_Width.ToString();
			entity.Out_Coil_Inner_Diameter= dao.Out_Coil_Inner_Diameter.ToString();
			entity.Grind_Flag= dao.Grind_Flag;
			entity.Pack_Type_Code= dao.Pack_Type_Code;
			entity.Repair_Type= dao.Repair_Type;
			entity.D01_Code= dao.D01_Code;
			entity.D01_Origin= dao.D01_Origin;
			entity.D01_Sid= dao.D01_Sid;
			entity.D01_Pos_W= dao.D01_Pos_W;
			entity.D01_Pos_L_Start= dao.D01_Pos_L_Start.ToString();
			entity.D01_Pos_L_End= dao.D01_Pos_L_End.ToString();
			entity.D01_Level= dao.D01_Level;
			entity.D01_Percent= dao.D01_Percent.ToString();
			entity.D01_QGrade = dao.D01_QGrade;
			entity.D02_Code= dao.D02_Code;
			entity.D02_Origin= dao.D02_Origin;
			entity.D02_Sid= dao.D02_Sid;
			entity.D02_Pos_W= dao.D02_Pos_W;
			entity.D02_Pos_L_Start= dao.D02_Pos_L_Start.ToString();
			entity.D02_Pos_L_End= dao.D02_Pos_L_End.ToString();
			entity.D02_Level= dao.D02_Level;
			entity.D02_Percent= dao.D02_Percent.ToString();
			entity.D02_QGrade = dao.D02_QGrade;
			entity.D03_Code= dao.D03_Code;
			entity.D03_Origin= dao.D03_Origin;
			entity.D03_Sid= dao.D03_Sid;
			entity.D03_Pos_W= dao.D03_Pos_W;
			entity.D03_Pos_L_Start= dao.D03_Pos_L_Start.ToString();
			entity.D03_Pos_L_End= dao.D03_Pos_L_End.ToString();
			entity.D03_Level= dao.D03_Level;
			entity.D03_Percent= dao.D03_Percent.ToString();
			entity.D03_QGrade = dao.D02_QGrade;
			entity.D04_Code= dao.D04_Code;
			entity.D04_Origin= dao.D04_Origin;
			entity.D04_Sid= dao.D04_Sid;
			entity.D04_Pos_W= dao.D04_Pos_W;
			entity.D04_Pos_L_Start= dao.D04_Pos_L_Start.ToString();
			entity.D04_Pos_L_End= dao.D04_Pos_L_End.ToString();
			entity.D04_Level= dao.D04_Level;
			entity.D04_Percent= dao.D04_Percent.ToString();
			entity.D04_QGrade = dao.D04_QGrade;
			entity.D05_Code= dao.D05_Code;
			entity.D05_Origin= dao.D05_Origin;
			entity.D05_Sid= dao.D05_Sid;
			entity.D05_Pos_W= dao.D05_Pos_W;
			entity.D05_Pos_L_Start= dao.D05_Pos_L_Start.ToString();
			entity.D05_Pos_L_End= dao.D05_Pos_L_End.ToString();
			entity.D05_Level= dao.D05_Level;
			entity.D05_Percent= dao.D05_Percent.ToString();
			entity.D05_QGrade = dao.D05_QGrade;
			entity.D06_Code= dao.D06_Code;
			entity.D06_Origin= dao.D06_Origin;
			entity.D06_Sid= dao.D06_Sid;
			entity.D06_Pos_W= dao.D06_Pos_W;
			entity.D06_Pos_L_Start= dao.D06_Pos_L_Start.ToString();
			entity.D06_Pos_L_End= dao.D06_Pos_L_End.ToString();
			entity.D06_Level= dao.D06_Level;
			entity.D06_Percent= dao.D06_Percent.ToString();
			entity.D06_QGrade = dao.D06_QGrade;
			entity.D07_Code= dao.D07_Code;
			entity.D07_Origin= dao.D07_Origin;
			entity.D07_Sid= dao.D07_Sid;
			entity.D07_Pos_W= dao.D07_Pos_W;
			entity.D07_Pos_L_Start= dao.D07_Pos_L_Start.ToString();
			entity.D07_Pos_L_End= dao.D07_Pos_L_End.ToString();
			entity.D07_Level= dao.D07_Level;
			entity.D07_Percent= dao.D07_Percent.ToString();
			entity.D07_QGrade = dao.D07_QGrade;
			entity.D08_Code= dao.D08_Code;
			entity.D08_Origin= dao.D08_Origin;
			entity.D08_Sid= dao.D08_Sid;
			entity.D08_Pos_W= dao.D08_Pos_W;
			entity.D08_Pos_L_Start= dao.D08_Pos_L_Start.ToString();
			entity.D08_Pos_L_End= dao.D08_Pos_L_End.ToString();
			entity.D08_Level= dao.D08_Level;
			entity.D08_Percent= dao.D08_Percent.ToString();
			entity.D08_QGrade = dao.D08_QGrade;
			entity.D09_Code= dao.D09_Code;
			entity.D09_Origin= dao.D09_Origin;
			entity.D09_Sid= dao.D09_Sid;
			entity.D09_Pos_W= dao.D09_Pos_W;
			entity.D09_Pos_L_Start= dao.D09_Pos_L_Start.ToString();
			entity.D09_Pos_L_End= dao.D09_Pos_L_End.ToString();
			entity.D09_Level= dao.D09_Level;
			entity.D09_Percent= dao.D09_Percent.ToString();
			entity.D09_QGrade = dao.D09_QGrade;
			entity.D10_Code= dao.D10_Code;
			entity.D10_Origin= dao.D10_Origin;
			entity.D10_Sid= dao.D10_Sid;
			entity.D10_Pos_W= dao.D10_Pos_W;
			entity.D10_Pos_L_Start= dao.D10_Pos_L_Start.ToString();
			entity.D10_Pos_L_End= dao.D10_Pos_L_End.ToString();
			entity.D10_Level= dao.D10_Level;
			entity.D10_Percent= dao.D10_Percent.ToString();
			entity.D10_QGrade = dao.D10_QGrade;
			entity.Test_Plan_No= dao.Test_Plan_No;
			entity.Qc_Rmark= dao.Qc_Rmark;
			entity.Head_Off_Gauge= dao.Head_Off_Gauge.ToString();
			entity.Tail_Off_Gauge= dao.Tail_Off_Gauge.ToString();
			entity.Pre_Grinding_Surface= dao.Pre_Grinding_Surface;
			entity.Grinding_Count_Out= dao.Grinding_Count_Out.ToString();
			entity.Grinding_Count_In= dao.Grinding_Count_In.ToString();
			entity.Appoint_Grinding_Surface= dao.Appoint_Grinding_Surface;
			entity.Surface_Accu_Code_In= dao.Surface_Accu_Code_In;
			entity.Surface_Accu_Code_Out= dao.Surface_Accu_Code_Out;
			entity.Repair_Remark= dao.Repair_Remark;
			entity.Skim_Flag= dao.Skim_Flag;
			entity.Polishing_Type= dao.Polishing_Type;
			entity.Sg_Sign= dao.Sg_Sign;
			entity.Process_Code= dao.Process_Code;
			entity.Ys_Stand= dao.Ys_Stand.ToString();
			entity.Ys_Max= dao.Ys_Max.ToString();
			entity.Ys_Min= dao.Ys_Min.ToString();
			entity.Order_Cust_EName= dao.Order_Cust_Ename;
			entity.Order_Cust_CName= dao.Order_Cust_Cname;
			entity.Surface_Accu_Desc_Order= dao.Surface_Accu_Desc_Order;
			entity.Surface_Accuracy_Desc_Acture= dao.Surface_Accuracy_Desc_Acture;
			entity.Surface_Accu_Desc_In= dao.Surface_Accu_Desc_In;
			entity.Surface_Accu_Desc_Out= dao.Surface_Accu_Desc_Out;


            return entity;
        }

		// TBL_PDO -> L2L25_CoilPDO
		public static L2L25_CoilPDO To25CoilPDOEntity(this TBL_PDO dao,int Paper_Wt,float Sleeve_Wt)
        {

			var entity = new L2L25_CoilPDO();
			entity.Message_Id = MsgCode.Msg102PDO;
			entity.Message_Length = MsgLength.Msg102PDO;
			entity.Date = DateTime.Now.ToString("yyyyMMdd");
			entity.Time = DateTime.Now.ToString("HHmmss");

			entity.Order_No = dao.Order_No;
			entity.Plan_No = dao.Plan_No;
			entity.Out_Coil_ID = dao.Out_Coil_ID;
			entity.In_Coil_ID = dao.In_Coil_ID;
			entity.Start_Time = dao.Start_Time.ToString("yyyyMMddHHmmss");
			entity.Finish_Time = dao.Finish_Time.ToString("yyyyMMddHHmmss");
			entity.Shift = dao.Shift;
			entity.Team = dao.Team;
			entity.St_No = dao.St_No;
			entity.Out_Coil_Outer_Diameter = dao.Out_Coil_Outer_Diameter.ToString();
			entity.Out_Coil_Inner_Diameter = dao.Out_Coil_Inner_Diameter.ToString();
			entity.Out_Coil_Length = dao.Out_Coil_Length.ToString();
			entity.Out_Coil_Thick = dao.Out_Coil_Thick.ToString();
			entity.Out_Coil_Head_C40_Thick = dao.Out_Coil_Head_C40_Thick.ToString();
			entity.Out_Coil_Mid_C40_Thick = dao.Out_Coil_Mid_C40_Thick.ToString();
			entity.Out_Coil_Tail_C40_Thick = dao.Out_Coil_Tail_C40_Thick.ToString();
			entity.Out_Coil_Head_C25_Thick = dao.Out_Coil_Head_C25_Thick.ToString();
			entity.Out_Coil_Mid_C25_Thick = dao.Out_Coil_Mid_C25_Thick.ToString();
			entity.Out_Coil_Tail_C25_Thick = dao.Out_Coil_Tail_C25_Thick.ToString();
			entity.Out_Coil_Width = dao.Out_Coil_Width.ToString();
			entity.Out_Coil_Act_WT = dao.Out_Coil_Act_WT.ToString();
			entity.Out_Coil_Gross_WT = dao.Out_Coil_Gross_WT.ToString();
			entity.Head_Pass_Num = dao.Head_Pass_Num.ToString();
			entity.Mid_Pass_Num = dao.Mid_Pass_Num.ToString();
			entity.Tail_Pass_Num = dao.Tail_Pass_Num.ToString();
			entity.Out_Coil_Use_Sleeve_Flag = dao.Out_Coil_Use_Sleeve_Flag;
			entity.Out_Sleeve_Diameter = dao.Out_Sleeve_Diameter.ToString();
			entity.Out_Sleeve_Type_Code = dao.Out_Sleeve_Type_Code;
			entity.Out_Paper_Req_Code = dao.Out_Paper_Req_Code;
			entity.Out_Paper_Code = dao.Out_Paper_Code;
			entity.Head_Paper_Length = dao.Head_Paper_Length.ToString();
			entity.Head_Paper_Width = dao.Head_Paper_Width.ToString();
			entity.Tail_Paper_Length = dao.Tail_Paper_Length.ToString();
			entity.Tail_Paper_Width = dao.Tail_Paper_Width.ToString();
			entity.D01_Code = dao.D01_Code;
			entity.D01_Origin = dao.D01_Origin;
			entity.D01_Sid = dao.D01_Sid;
			entity.D01_Pos_W = dao.D01_Pos_W;
			entity.D01_Pos_L_Start = dao.D01_Pos_L_Start.ToString();
			entity.D01_Pos_L_End = dao.D01_Pos_L_End.ToString();
			entity.D01_Level = dao.D01_Level;
			entity.D01_Percent = dao.D01_Percent.ToString();
			entity.D01_QGrade = dao.D01_QGrade;
			entity.D02_Code = dao.D02_Code;
			entity.D02_Origin = dao.D02_Origin;
			entity.D02_Sid = dao.D02_Sid;
			entity.D02_Pos_W = dao.D02_Pos_W;
			entity.D02_Pos_L_Start = dao.D02_Pos_L_Start.ToString();
			entity.D02_Pos_L_End = dao.D02_Pos_L_End.ToString();
			entity.D02_Level = dao.D02_Level;
			entity.D02_Percent = dao.D02_Percent.ToString();
			entity.D02_QGrade = dao.D02_QGrade;
			entity.D03_Code = dao.D03_Code;
			entity.D03_Origin = dao.D03_Origin;
			entity.D03_Sid = dao.D03_Sid;
			entity.D03_Pos_W = dao.D03_Pos_W;
			entity.D03_Pos_L_Start = dao.D03_Pos_L_Start.ToString();
			entity.D03_Pos_L_End = dao.D03_Pos_L_End.ToString();
			entity.D03_Level = dao.D03_Level;
			entity.D03_Percent = dao.D03_Percent.ToString();
			entity.D03_QGrade = dao.D03_QGrade;
			entity.D04_Code = dao.D04_Code;
			entity.D04_Origin = dao.D04_Origin;
			entity.D04_Sid = dao.D04_Sid;
			entity.D04_Pos_W = dao.D04_Pos_W;
			entity.D04_Pos_L_Start = dao.D04_Pos_L_Start.ToString();
			entity.D04_Pos_L_End = dao.D04_Pos_L_End.ToString();
			entity.D04_Level = dao.D04_Level;
			entity.D04_Percent = dao.D04_Percent.ToString();
			entity.D04_QGrade = dao.D04_QGrade;
			entity.D05_Code = dao.D05_Code;
			entity.D05_Origin = dao.D05_Origin;
			entity.D05_Sid = dao.D05_Sid;
			entity.D05_Pos_W = dao.D05_Pos_W;
			entity.D05_Pos_L_Start = dao.D05_Pos_L_Start.ToString();
			entity.D05_Pos_L_End = dao.D05_Pos_L_End.ToString();
			entity.D05_Level = dao.D05_Level;
			entity.D05_Percent = dao.D05_Percent.ToString();
			entity.D05_QGrade = dao.D05_QGrade;
			entity.D06_Code = dao.D06_Code;
			entity.D06_Origin = dao.D06_Origin;
			entity.D06_Sid = dao.D06_Sid;
			entity.D06_Pos_W = dao.D06_Pos_W;
			entity.D06_Pos_L_Start = dao.D06_Pos_L_Start.ToString();
			entity.D06_Pos_L_End = dao.D06_Pos_L_End.ToString();
			entity.D06_Level = dao.D06_Level;
			entity.D06_Percent = dao.D06_Percent.ToString();
			entity.D06_QGrade = dao.D06_QGrade;
			entity.D07_Code = dao.D07_Code;
			entity.D07_Origin = dao.D07_Origin;
			entity.D07_Sid = dao.D07_Sid;
			entity.D07_Pos_W = dao.D07_Pos_W;
			entity.D07_Pos_L_Start = dao.D07_Pos_L_Start.ToString();
			entity.D07_Pos_L_End = dao.D07_Pos_L_End.ToString();
			entity.D07_Level = dao.D07_Level;
			entity.D07_Percent = dao.D07_Percent.ToString();
			entity.D07_QGrade = dao.D07_QGrade;
			entity.D08_Code = dao.D08_Code;
			entity.D08_Origin = dao.D08_Origin;
			entity.D08_Sid = dao.D08_Sid;
			entity.D08_Pos_W = dao.D08_Pos_W;
			entity.D08_Pos_L_Start = dao.D08_Pos_L_Start.ToString();
			entity.D08_Pos_L_End = dao.D08_Pos_L_End.ToString();
			entity.D08_Level = dao.D08_Level;
			entity.D08_Percent = dao.D08_Percent.ToString();
			entity.D08_QGrade = dao.D08_QGrade;
			entity.D09_Code = dao.D09_Code;
			entity.D09_Origin = dao.D09_Origin;
			entity.D09_Sid = dao.D09_Sid;
			entity.D09_Pos_W = dao.D09_Pos_W;
			entity.D09_Pos_L_Start = dao.D09_Pos_L_Start.ToString();
			entity.D09_Pos_L_End = dao.D09_Pos_L_End.ToString();
			entity.D09_Level = dao.D09_Level;
			entity.D09_Percent = dao.D09_Percent.ToString();
			entity.D09_QGrade = dao.D09_QGrade;
			entity.D10_Code = dao.D10_Code;
			entity.D10_Origin = dao.D10_Origin;
			entity.D10_Sid = dao.D10_Sid;
			entity.D10_Pos_W = dao.D10_Pos_W;
			entity.D10_Pos_L_Start = dao.D10_Pos_L_Start.ToString();
			entity.D10_Pos_L_End = dao.D10_Pos_L_End.ToString();
			entity.D10_Level = dao.D10_Level;
			entity.D10_Percent = dao.D10_Percent.ToString();
			entity.D10_QGrade = dao.D10_QGrade;
			entity.Winding_Dire = dao.Winding_Dire;
			entity.Better_Surf_Ward_Code = dao.Better_Surf_Ward_Code;
			entity.Hold_Maker = dao.Hold_Maker;
			entity.Hold_Flag = dao.Hold_Flag;
			entity.Hold_Cause_Code = dao.Hold_Cause_Code;
			entity.Sample_Flag = dao.Sample_Flag;
			entity.Fixed_Wt_Flag = dao.Fixed_Wt_Flag.ToString();
			entity.Final_Coil_Flag = dao.Final_Coil_Flag;
			entity.Scrap_Flag = dao.Scrap_Flag;
			entity.Sample_Pos_Code = dao.Sample_Pos_Code;
			entity.Surface_Accu_Code = dao.Surface_Accu_Code;
			entity.Head_Hole_Position = dao.Head_Hole_Position.ToString();
			entity.Head_Leader_Length = dao.Head_Leader_Length.ToString();
			entity.Head_Leader_Width = dao.Head_Leader_Width.ToString();
			entity.Head_Leader_Thickness = dao.Head_Leader_Thickness.ToString();
			entity.Tail_Hole_Position = dao.Tail_Hole_Position.ToString();
			entity.Tail_Leader_Length = dao.Tail_Leader_Length.ToString();
			entity.Tail_Leader_Width = dao.Tail_Leader_Width.ToString();
			entity.Tail_Leader_Thickness = dao.Tail_Leader_Thickness.ToString();
			entity.Head_Leader_St_No = dao.Head_Leader_St_No;
			entity.Tail_Leader_St_No = dao.Tail_Leader_St_No;
			entity.Head_Off_Gauge = dao.Head_Off_Gauge.ToString();
			entity.Tail_Off_Gauge = dao.Tail_Off_Gauge.ToString();
			entity.Pre_Grinding_Surface = dao.Pre_Grinding_Surface;
			entity.Grinding_Count_Out = dao.Grinding_Count_Out.ToString();
			entity.Grinding_Count_In = dao.Grinding_Count_In.ToString();
			entity.Appoint_Grinding_Surface = dao.Appoint_Grinding_Surface;
			entity.Oil_Flag = dao.Oil_Flag;
			entity.Surface_Accu_Code_In = dao.Surface_Accu_Code_In;
			entity.Surface_Accu_Code_Out = dao.Surface_Accu_Code_Out;
			entity.Process_Code = dao.Process_Code;
            //entity.Head_Rough = dao.Head_Rough.ToString();
            //entity.Mid_Rough = dao.Mid_Rough.ToString();
            //entity.Tail_Rough = dao.Tail_Rough.ToString();
            entity.Head_Rough = dao.Head_Rough_Ra.ToString();
            entity.Mid_Rough= dao.Mid_Rough_Ra.ToString();
            entity.Tail_Rough= dao.Tail_Rough_Ra.ToString();
            entity.Head_Rough_Rz = dao.Head_Rough_Rz.ToString();
            entity.Mid_Rough_Rz = dao.Mid_Rough_Rz.ToString();
            entity.Tail_Rough_Rz = dao.Tail_Rough_Rz.ToString();
            entity.Head_Rough_Rmax = dao.Head_Rough_Rmax.ToString();
            entity.Mid_Rough_Rmax = dao.Mid_Rough_Rmax.ToString();
            entity.Tail_Rough_Rmax = dao.Tail_Rough_Rmax.ToString();
            entity.Uncoil_Direction = dao.Uncoil_Direction;
			entity.Recoiler_ActTen_Avg = dao.Recoiler_ActTen_Avg.ToString();
            entity.Paper_Wt = Paper_Wt;
            entity.Sleeve_Wt = Sleeve_Wt;

            return entity;

		}

        public static L2L25_ProcessCT ToL25CTEntity(this TBL_PDO pdo, ProcessModel data, L25SysDef.L25CTData dataClssify)
        {
            var entity = new L2L25_ProcessCT();


            switch (dataClssify)
            {
                case L25CTData.LineSpeed:
                    entity.Message_Id = L25SysDef.MsgCode.Msg105LineSpeedCT;
                    entity.Message_Length = L25SysDef.MsgLength.Msg105LineSpeedCT;
                    entity.dataCode = ProcessData.Code.LineSpeed;
                    entity.dataDesc = ProcessData.Desc.Speed;
                    entity.resultUnit = ProcessData.ResultUnit.Speed;
                    entity.DataString = data.LinespeedStr;
                    break;
                case L25CTData.LineTension:
                    entity.Message_Id = L25SysDef.MsgCode.Msg106LineTensionCT;
                    entity.Message_Length = L25SysDef.MsgLength.Msg106LineTensionCT;
                    entity.dataCode = ProcessData.Code.LineTension;
                    entity.dataDesc = ProcessData.Desc.Tension;
                    entity.resultUnit = ProcessData.ResultUnit.Tension;
                    entity.DataString = data.LinetensionStr;
                    break;
                case L25CTData.LineRunDirection:
                    entity.Message_Id = L25SysDef.MsgCode.Msg107LineRunDirectionCT;
                    entity.Message_Length = L25SysDef.MsgLength.Msg107LineRunDirectionCT;
                    entity.dataCode = ProcessData.Code.Linerundirection;
                    entity.dataDesc = ProcessData.Desc.direction;
                    entity.resultUnit = ProcessData.ResultUnit.direction;
                    entity.DataString = data.LinerundirectionStr;
                    break;

                case L25CTData.No1GRAbrasiveBeltMotorCurrent:
                    entity.Message_Id = L25SysDef.MsgCode.Msg108No1GRAbrasiveBeltMotorCurrentCT;
                    entity.Message_Length = L25SysDef.MsgLength.Msg108No1GRAbrasiveBeltMotorCurrentCT;
                    entity.dataCode = ProcessData.Code.No1GRABeltMotorCurrent;
                    entity.dataDesc = ProcessData.Desc.Current;
                    entity.resultUnit = ProcessData.ResultUnit.Current;
                    entity.DataString = data.No1GRAbrasiveBeltMotorCurrentStr;
                    break;
                case L25CTData.No2GRAbrasiveBeltMotorCurrent:
                    entity.Message_Id = L25SysDef.MsgCode.Msg109No2GRAbrasiveBeltMotorCurrentCT;
                    entity.Message_Length = L25SysDef.MsgLength.Msg109No2GRAbrasiveBeltMotorCurrentCT;
                    entity.dataCode = ProcessData.Code.No2GRABeltMotorCurrent;
                    entity.dataDesc = ProcessData.Desc.Current;
                    entity.resultUnit = ProcessData.ResultUnit.Current;
                    entity.DataString = data.No2GRAbrasiveBeltMotorCurrentStr;
                    break;
                case L25CTData.No3GRAbrasiveBeltMotorCurrent:
                    entity.Message_Id = L25SysDef.MsgCode.Msg110No3GRAbrasiveBeltMotorCurrentCT;
                    entity.Message_Length = L25SysDef.MsgLength.Msg110No3GRAbrasiveBeltMotorCurrentCT;
                    entity.dataCode = ProcessData.Code.No3GRABeltMotorCurrent;
                    entity.dataDesc = ProcessData.Desc.Current;
                    entity.resultUnit = ProcessData.ResultUnit.Current;
                    entity.DataString = data.No3GRAbrasiveBeltMotorCurrentStr;
                    break;
                case L25CTData.No4GRAbrasiveBeltMotorCurrent:
                    entity.Message_Id = L25SysDef.MsgCode.Msg111No4GRAbrasiveBeltMotorCurrentCT;
                    entity.Message_Length = L25SysDef.MsgLength.Msg111No4GRAbrasiveBeltMotorCurrentCT;
                    entity.dataCode = ProcessData.Code.No4GRABeltMotorCurrent;
                    entity.dataDesc = ProcessData.Desc.Current;
                    entity.resultUnit = ProcessData.ResultUnit.Current;
                    entity.DataString = data.No4GRAbrasiveBeltMotorCurrentStr;
                    break;
                case L25CTData.No5GRAbrasiveBeltMotorCurrent:
                    entity.Message_Id = L25SysDef.MsgCode.Msg112No5GRAbrasiveBeltMotorCurrentCT;
                    entity.Message_Length = L25SysDef.MsgLength.Msg112No5GRAbrasiveBeltMotorCurrentCT;
                    entity.dataCode = ProcessData.Code.No5GRABeltMotorCurrent;
                    entity.dataDesc = ProcessData.Desc.Current;
                    entity.resultUnit = ProcessData.ResultUnit.Current;
                    entity.DataString = data.No5GRAbrasiveBeltMotorCurrentStr;
                    break;
                case L25CTData.No6GRAbrasiveBeltMotorCurrent:
                    entity.Message_Id = L25SysDef.MsgCode.Msg113No6GRAbrasiveBeltMotorCurrentCT;
                    entity.Message_Length = L25SysDef.MsgLength.Msg113No6GRAbrasiveBeltMotorCurrentCT;
                    entity.dataCode = ProcessData.Code.No6GRABeltMotorCurrent;
                    entity.dataDesc = ProcessData.Desc.Current;
                    entity.resultUnit = ProcessData.ResultUnit.Current;
                    entity.DataString = data.No6GRAbrasiveBeltMotorCurrentStr;
                    break;

                case L25CTData.No1GRAbrasiveBeltSpeed:
                    entity.Message_Id = L25SysDef.MsgCode.Msg114No1GRAbrasiveBeltSpeedCT;
                    entity.Message_Length = L25SysDef.MsgLength.Msg114No1GRAbrasiveBeltSpeedCT;
                    entity.dataCode = ProcessData.Code.No1GRABeltSpeed;
                    entity.dataDesc = ProcessData.Desc.Speed;
                    entity.resultUnit = ProcessData.ResultUnit.Speed;
                    entity.DataString = data.No1GRAbrasiveBeltSpeedStr;
                    break;
                case L25CTData.No2GRAbrasiveBeltSpeed:
                    entity.Message_Id = L25SysDef.MsgCode.Msg115No2GRAbrasiveBeltSpeedCT;
                    entity.Message_Length = L25SysDef.MsgLength.Msg115No2GRAbrasiveBeltSpeedCT;
                    entity.dataCode = ProcessData.Code.No2GRABeltSpeed;
                    entity.dataDesc = ProcessData.Desc.Speed;
                    entity.resultUnit = ProcessData.ResultUnit.Speed;
                    entity.DataString = data.No2GRAbrasiveBeltSpeedStr;
                    break;
                case L25CTData.BrushRollCurrent1:
                    entity.Message_Id = L25SysDef.MsgCode.Msg145No1BrushRollCurrentCT;
                    entity.Message_Length = L25SysDef.MsgLength.Msg145No1BrushRollCurrentCT;
                    entity.dataCode = ProcessData.Code.BrushRollCurrent1;
                    entity.dataDesc = ProcessData.Desc.Current;
                    entity.resultUnit = ProcessData.ResultUnit.Current;
                    entity.DataString = data.BrushRollCurrent1Str;
                    break;
                case L25CTData.BrushRollCurrent2:
                    entity.Message_Id = L25SysDef.MsgCode.Msg146No2BrushRollCurrentCT;
                    entity.Message_Length = L25SysDef.MsgLength.Msg146No2BrushRollCurrentCT;
                    entity.dataCode = ProcessData.Code.BrushRollCurrent2;
                    entity.dataDesc = ProcessData.Desc.Current;
                    entity.resultUnit = ProcessData.ResultUnit.Current;
                    entity.DataString = data.BrushRollCurrent2Str;
                    break;
            }
            //entity.Message_Id = L25SysDef.MsgCode.Msg105LineSpeedCT; 
            //entity.Message_Length = L25SysDef.MsgLength.Msg105LineSpeedCT;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同          
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同  
            //entity.dataCode = ProcessData.Code.LineSpeed;
            //entity.dataDesc = ProcessData.Desc.Speed;
            //entity.resultUnit = ProcessData.ResultUnit.Speed;
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            //entity.DataString = data.LinespeedStr;


            return entity;
        }

        //----------104-------------------//
        public static L2L25_LineSpeedCT ToL25LineSpeedCTEntity(this TBL_PDO pdo, ProcessModel data)
        {
            var entity = new L2L25_LineSpeedCT();

            entity.Message_Id = L25SysDef.MsgCode.Msg105LineSpeedCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg105LineSpeedCT;
            entity.dataCode = ProcessData.Code.LineSpeed;
            entity.dataDesc = ProcessData.Desc.Speed;
            entity.resultUnit = ProcessData.ResultUnit.Speed;
            entity.DataString = data.LinespeedStr;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同          
            entity.plan_no = pdo.Plan_No;             //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同  
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_LineTensionCT ToL25LineTensionCTEntity(this TBL_PDO pdo, ProcessModel data)
        {
            var entity = new L2L25_LineTensionCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg106LineTensionCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg106LineTensionCT;
            entity.dataCode = ProcessData.Code.LineTension;
            entity.dataDesc = ProcessData.Desc.Tension;
            entity.resultUnit = ProcessData.ResultUnit.Tension;
            entity.DataString = data.LinetensionStr;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }

        public static L2L25_LineRunDirectionCT ToL25LineRunDirectionCTEntity(this TBL_PDO pdo, ProcessModel data)
        {
            var entity = new L2L25_LineRunDirectionCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg107LineRunDirectionCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg107LineRunDirectionCT;
            entity.dataCode = ProcessData.Code.Linerundirection;
            entity.dataDesc = ProcessData.Desc.direction;
            entity.resultUnit = ProcessData.ResultUnit.direction;
            entity.DataString = data.LinerundirectionStr;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }

        public static L2L25_No1GRAbrasiveBeltMotorCurrentCT ToL25No1GRAbrasiveBeltMotorCurrentCTEntity(this TBL_PDO pdo, ProcessModel data)
        {
            var entity = new L2L25_No1GRAbrasiveBeltMotorCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg108No1GRAbrasiveBeltMotorCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg108No1GRAbrasiveBeltMotorCurrentCT;
            entity.dataCode = ProcessData.Code.No1GRABeltMotorCurrent;
            entity.dataDesc = ProcessData.Desc.Current;
            entity.resultUnit = ProcessData.ResultUnit.Current;
            entity.DataString = data.No1GRAbrasiveBeltMotorCurrentStr;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No2GRAbrasiveBeltMotorCurrentCT ToL25No2GRAbrasiveBeltMotorCurrentCTEntity(this TBL_PDO pdo, ProcessModel data)
        {
            var entity = new L2L25_No2GRAbrasiveBeltMotorCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg109No2GRAbrasiveBeltMotorCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg109No2GRAbrasiveBeltMotorCurrentCT;
            entity.dataCode = ProcessData.Code.No2GRABeltMotorCurrent;
            entity.dataDesc = ProcessData.Desc.Current;
            entity.resultUnit = ProcessData.ResultUnit.Current;
            entity.DataString = data.No2GRAbrasiveBeltMotorCurrentStr;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No3GRAbrasiveBeltMotorCurrentCT ToL25No3GRAbrasiveBeltMotorCurrentCTEntity(this TBL_PDO pdo, ProcessModel data)
        {
            var entity = new L2L25_No3GRAbrasiveBeltMotorCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg110No3GRAbrasiveBeltMotorCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg110No3GRAbrasiveBeltMotorCurrentCT;
            entity.dataCode = ProcessData.Code.No3GRABeltMotorCurrent;
            entity.dataDesc = ProcessData.Desc.Current;
            entity.resultUnit = ProcessData.ResultUnit.Current;
            entity.DataString = data.No3GRAbrasiveBeltMotorCurrentStr;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No4GRAbrasiveBeltMotorCurrentCT ToL25No4GRAbrasiveBeltMotorCurrentCTEntity(this TBL_PDO pdo, ProcessModel data)
        {
            var entity = new L2L25_No4GRAbrasiveBeltMotorCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg111No4GRAbrasiveBeltMotorCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg111No4GRAbrasiveBeltMotorCurrentCT;
            entity.dataCode = ProcessData.Code.No4GRABeltMotorCurrent;
            entity.dataDesc = ProcessData.Desc.Current;
            entity.resultUnit = ProcessData.ResultUnit.Current;
            entity.DataString = data.No4GRAbrasiveBeltMotorCurrentStr;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No5GRAbrasiveBeltMotorCurrentCT ToL25No5GRAbrasiveBeltMotorCurrentCTEntity(this TBL_PDO pdo, ProcessModel data)
        {
            var entity = new L2L25_No5GRAbrasiveBeltMotorCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg112No5GRAbrasiveBeltMotorCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg112No5GRAbrasiveBeltMotorCurrentCT;
            entity.dataCode = ProcessData.Code.No5GRABeltMotorCurrent;
            entity.dataDesc = ProcessData.Desc.Current;
            entity.resultUnit = ProcessData.ResultUnit.Current;
            entity.DataString = data.No5GRAbrasiveBeltMotorCurrentStr;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同          
            entity.plan_no = pdo.Plan_No;             //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No6GRAbrasiveBeltMotorCurrentCT ToL25No6GRAbrasiveBeltMotorCurrentCTEntity(this TBL_PDO pdo, ProcessModel data)
        {
            var entity = new L2L25_No6GRAbrasiveBeltMotorCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg113No6GRAbrasiveBeltMotorCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg113No6GRAbrasiveBeltMotorCurrentCT;
            entity.dataCode = ProcessData.Code.No6GRABeltMotorCurrent;
            entity.dataDesc = ProcessData.Desc.Current;
            entity.resultUnit = ProcessData.ResultUnit.Current;
            entity.DataString = data.No6GRAbrasiveBeltMotorCurrentStr;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同          
            entity.plan_no = pdo.Plan_No;             //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No1GRAbrasiveBeltSpeedCT ToL25No1GRAbrasiveBeltSpeedCTEntity(this TBL_PDO pdo, ProcessModel data)
        {
            var entity = new L2L25_No1GRAbrasiveBeltSpeedCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg114No1GRAbrasiveBeltSpeedCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg114No1GRAbrasiveBeltSpeedCT;
            entity.dataCode = ProcessData.Code.No1GRABeltSpeed;
            entity.dataDesc = ProcessData.Desc.Speed;
            entity.resultUnit = ProcessData.ResultUnit.Speed;
            entity.DataString = data.No1GRAbrasiveBeltSpeedStr;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同          
            entity.plan_no = pdo.Plan_No;             //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No2GRAbrasiveBeltSpeedCT ToL25No2GRAbrasiveBeltSpeedCTEntity(this TBL_PDO pdo, ProcessModel data)
        {
            var entity = new L2L25_No2GRAbrasiveBeltSpeedCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg115No2GRAbrasiveBeltSpeedCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg115No2GRAbrasiveBeltSpeedCT;
            entity.dataCode = ProcessData.Code.No2GRABeltSpeed;
            entity.dataDesc = ProcessData.Desc.Speed;
            entity.resultUnit = ProcessData.ResultUnit.Speed;
            entity.DataString = data.No2GRAbrasiveBeltSpeedStr;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同          
            entity.plan_no = pdo.Plan_No;             //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No1BrushRollCurrentCT ToL25No1BrushRollCurrentCTEntity(this TBL_PDO pdo, ProcessModel data)
        {
            var entity = new L2L25_No1BrushRollCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg145No1BrushRollCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg145No1BrushRollCurrentCT;
            entity.dataCode = ProcessData.Code.BrushRollCurrent1;
            entity.dataDesc = ProcessData.Desc.Current;
            entity.resultUnit = ProcessData.ResultUnit.Current;
            entity.DataString = data.BrushRollCurrent1Str;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同          
            entity.plan_no = pdo.Plan_No;             //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No2BrushRollCurrentCT ToL25No2BrushRollCurrentCTEntity(this TBL_PDO pdo, ProcessModel data)
        {
            var entity = new L2L25_No2BrushRollCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg146No2BrushRollCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg146No2BrushRollCurrentCT;
            entity.dataCode = ProcessData.Code.BrushRollCurrent2;
            entity.dataDesc = ProcessData.Desc.Current;
            entity.resultUnit = ProcessData.ResultUnit.Current;
            entity.DataString = data.BrushRollCurrent2Str;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = ProcessData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = ProcessData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        //----------107-------------------//
        public static L2L25_CurrentPassNumberCT ToL25CurrentPassNumberCTEntity(this TBL_PDO pdo, GrindDataModel data,int PassNumber,int Session)
        {
            var entity = new L2L25_CurrentPassNumberCT();

            entity.Message_Id = L25SysDef.MsgCode.Msg116CurrentPassNumberCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg116CurrentPassNumberCT;
            entity.dataCode = GrindData.Code.CurrentPassNumber;
            entity.dataDesc = GrindData.Desc.PassNumber;
            entity.resultUnit = GrindData.ResultUnit.PassNumber;
            entity.DataString = data.Current_Pass;
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_CurrentSessionCT ToL25CurrentSessionCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_CurrentSessionCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg117CurrentSessionCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg117CurrentSessionCT;
            entity.dataCode = GrindData.Code.CurrentSession;
            entity.dataDesc = GrindData.Desc.Current;
            entity.resultUnit = GrindData.ResultUnit.Current;
            entity.DataString = data.Current_Senssion;
            
            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同

            return entity;
        }
        public static L2L25_GRDLineSpeedCT ToL25GRDLineSpeedCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_GRDLineSpeedCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg118GRDLineSpeedCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg118GRDLineSpeedCT;
            entity.dataCode = GrindData.Code.GRDLineSpeed;
            entity.dataDesc = GrindData.Desc.Speed;
            entity.resultUnit = GrindData.ResultUnit.Speed;
            entity.DataString = data.Line_Speed;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同

            return entity;
        }
        public static L2L25_No1GRAB_beltKindCT ToL25No1GRAB_beltKindCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No1GRAB_beltKindCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg119No1GRAB_beltKindCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg119No1GRAB_beltKindCT;
            entity.dataCode = GrindData.Code.No1GRAB_beltKind;
            entity.dataDesc = GrindData.Desc.Kind;
            entity.resultUnit = GrindData.ResultUnit.Kind;
            entity.DataString = data.GR1_BELT_KIND;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No1GRAB_beltRoughnessCT ToL25No1GRAB_beltRoughnessCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No1GRAB_beltRoughnessCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg120No1GRAB_beltRoughnessCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg120No1GRAB_beltRoughnessCT;
            entity.dataCode = GrindData.Code.No1GRAB_beltRoughness;
            entity.dataDesc = GrindData.Desc.Roughness;
            entity.resultUnit = GrindData.ResultUnit.Roughness;
            entity.DataString = data.GR1_BELT_PARTICLE_NO;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No1GRAB_beltRotateDirectionCT ToL25No1GRAB_beltRotateDirectionCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No1GRAB_beltRotateDirectionCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg123No1GRAB_beltRotateDirectionCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg123No1GRAB_beltRotateDirectionCT;
            entity.dataCode = GrindData.Code.No1GRAB_beltRotateDireion;
            entity.dataDesc = GrindData.Desc.direction;
            entity.resultUnit = GrindData.ResultUnit.direction;
            entity.DataString = data.GR1_BELT_ROTATE_DIR;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No1GRAB_beltMotorCurrentCT ToL25No1GRAB_beltMotorCurrentCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No1GRAB_beltMotorCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg121No1GRAB_beltMotorCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg121No1GRAB_beltMotorCurrentCT;
            entity.dataCode = GrindData.Code.No1GRAB_beltMotorCurrent;
            entity.dataDesc = GrindData.Desc.Current;
            entity.resultUnit = GrindData.ResultUnit.Current;
            entity.DataString = data.GR1_MOTOR_CURRENT;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No1GRAB_beltSpeedCT ToL25No1GRAB_beltSpeedCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No1GRAB_beltSpeedCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg122No1GRAB_beltSpeedCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg122No1GRAB_beltSpeedCT;
            entity.dataCode = GrindData.Code.No1GRAB_beltSpeed;
            entity.dataDesc = GrindData.Desc.Speed;
            entity.resultUnit = GrindData.ResultUnit.Speed;
            entity.DataString = data.GR1_BELT_SPEED;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No2GRAB_beltKindCT ToL25No2GRAB_beltKindCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No2GRAB_beltKindCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg124No2GRAB_beltKindCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg124No2GRAB_beltKindCT;
            entity.dataCode = GrindData.Code.No2GRAB_beltKind;
            entity.dataDesc = GrindData.Desc.Kind;
            entity.resultUnit = GrindData.ResultUnit.Kind;
            entity.DataString = data.GR2_BELT_KIND;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No2GRAB_beltRoughnessCT ToL25No2GRAB_beltRoughnessCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No2GRAB_beltRoughnessCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg125No2GRAB_beltRoughnessCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg125No2GRAB_beltRoughnessCT;
            entity.dataCode = GrindData.Code.No2GRAB_beltRoughness;
            entity.dataDesc = GrindData.Desc.Roughness;
            entity.resultUnit = GrindData.ResultUnit.Roughness;
            entity.DataString = data.GR2_BELT_PARTICLE_NO;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No2GRAB_beltRotateDirectionCT ToL25No2GRAB_beltRotateDirectionCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No2GRAB_beltRotateDirectionCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg128No2GRAB_beltRotateDirectionCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg128No2GRAB_beltRotateDirectionCT;
            entity.dataCode = GrindData.Code.No2GRAB_beltRotateDireion;
            entity.dataDesc = GrindData.Desc.direction;
            entity.resultUnit = GrindData.ResultUnit.direction;
            entity.DataString = data.GR2_BELT_ROTATE_DIR;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No2GRAB_beltCurrentCT ToL25No2GRAB_beltCurrentCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No2GRAB_beltCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg126No2GRAB_beltCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg126No2GRAB_beltCurrentCT;
            entity.dataCode = GrindData.Code.No2GRAB_beltCurrent;
            entity.dataDesc = GrindData.Desc.Current;
            entity.resultUnit = GrindData.ResultUnit.Current;
            entity.DataString = data.GR2_MOTOR_CURRENT;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }

        public static L2L25_No2GRAB_beltSpeedCT ToL25No2GRAB_beltSpeedCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No2GRAB_beltSpeedCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg127No2GRAB_beltSpeedCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg127No2GRAB_beltSpeedCT;
            entity.dataCode = GrindData.Code.No2GRAB_beltSpeed;
            entity.dataDesc = GrindData.Desc.Speed;
            entity.resultUnit = GrindData.ResultUnit.Speed;
            entity.DataString = data.GR2_BELT_SPEED;


            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No3GRAB_beltKindCT ToL25No3GRAB_beltKindCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No3GRAB_beltKindCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg129No3GRAB_beltKindCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg129No3GRAB_beltKindCT;
            entity.dataCode = GrindData.Code.No3GRAB_beltKind;
            entity.dataDesc = GrindData.Desc.Kind;
            entity.resultUnit = GrindData.ResultUnit.Kind;
            entity.DataString = data.GR3_BELT_KIND;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No3GRAB_beltRoughnessCT ToL25No3GRAB_beltRoughnessCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No3GRAB_beltRoughnessCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg130No3GRAB_beltRoughnessCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg130No3GRAB_beltRoughnessCT;
            entity.dataCode = GrindData.Code.No3GRAB_beltRoughness;
            entity.dataDesc = GrindData.Desc.Roughness;
            entity.resultUnit = GrindData.ResultUnit.Roughness;
            entity.DataString = data.GR3_BELT_PARTICLE_NO;


            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No3GRAB_beltRotateDirectionCT ToL25No3GRAB_beltRotateDirectionCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No3GRAB_beltRotateDirectionCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg132No3GRAB_beltRotateDirectionCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg132No3GRAB_beltRotateDirectionCT;
            entity.dataCode = GrindData.Code.No3GRAB_beltRotateDireion;
            entity.dataDesc = GrindData.Desc.direction;
            entity.resultUnit = GrindData.ResultUnit.direction;
            entity.DataString = data.GR3_BELT_ROTATE_DIR;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No3GRAB_beltCurrentCT ToL25No3GRAB_beltCurrentCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No3GRAB_beltCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg131No3GRAB_beltCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg131No3GRAB_beltCurrentCT;
            entity.dataCode = GrindData.Code.No3GRAB_beltCurrent;
            entity.dataDesc = GrindData.Desc.Current;
            entity.resultUnit = GrindData.ResultUnit.Current;
            entity.DataString = data.GR3_MOTOR_CURRENT;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No4GRAB_beltKindCT ToL25No4GRAB_beltKindCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No4GRAB_beltKindCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg133No4GRAB_beltKindCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg133No4GRAB_beltKindCT;
            entity.dataCode = GrindData.Code.No4GRAB_beltKind;
            entity.dataDesc = GrindData.Desc.Kind;
            entity.resultUnit = GrindData.ResultUnit.Kind;
            entity.DataString = data.GR4_BELT_KIND;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No4GRAB_beltRoughnessCT ToL25No4GRAB_beltRoughnessCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No4GRAB_beltRoughnessCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg134No4GRAB_beltRoughnessCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg134No4GRAB_beltRoughnessCT;
            entity.dataCode = GrindData.Code.No4GRAB_beltRoughness;
            entity.dataDesc = GrindData.Desc.Roughness;
            entity.resultUnit = GrindData.ResultUnit.Roughness;
            entity.DataString = data.GR4_BELT_PARTICLE_NO;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No4GRAB_beltRotateDirectionCT ToL25No4GRAB_beltRotateDirectionCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No4GRAB_beltRotateDirectionCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg136No4GRAB_beltRotateDirectionCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg136No4GRAB_beltRotateDirectionCT;
            entity.dataCode = GrindData.Code.No4GRAB_beltRotateDireion;
            entity.dataDesc = GrindData.Desc.direction;
            entity.resultUnit = GrindData.ResultUnit.direction;
            entity.DataString = data.GR4_BELT_ROTATE_DIR;


            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No4GRAB_beltCurrentCT ToL25No4GRAB_beltCurrentCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No4GRAB_beltCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg135No4GRAB_beltCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg135No4GRAB_beltCurrentCT;
            entity.dataCode = GrindData.Code.No4GRAB_beltCurrent;
            entity.dataDesc = GrindData.Desc.Current;
            entity.resultUnit = GrindData.ResultUnit.Current;
            entity.DataString = data.GR4_MOTOR_CURRENT;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No5GRAB_beltKindCT ToL25No5GRAB_beltKindCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No5GRAB_beltKindCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg137No5GRAB_beltKindCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg137No5GRAB_beltKindCT;
            entity.dataCode = GrindData.Code.No5GRAB_beltKind;
            entity.dataDesc = GrindData.Desc.Kind;
            entity.resultUnit = GrindData.ResultUnit.Kind;
            entity.DataString = data.GR5_BELT_KIND;


            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No5GRAB_beltRoughnessCT ToL25No5GRAB_beltRoughnessCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No5GRAB_beltRoughnessCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg138No5GRAB_beltRoughnessCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg138No5GRAB_beltRoughnessCT;
            entity.dataCode = GrindData.Code.No5GRAB_beltRoughness;
            entity.dataDesc = GrindData.Desc.Roughness;
            entity.resultUnit = GrindData.ResultUnit.Roughness;
            entity.DataString = data.GR5_BELT_PARTICLE_NO;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }

        public static L2L25_No5GRAB_beltRotateDirectionCT ToL25No5GRAB_beltRotateDirectionCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No5GRAB_beltRotateDirectionCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg140No5GRAB_beltRotateDirectionCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg140No5GRAB_beltRotateDirectionCT;
            entity.dataCode = GrindData.Code.No5GRAB_beltRotateDireion;
            entity.dataDesc = GrindData.Desc.direction;
            entity.resultUnit = GrindData.ResultUnit.direction;
            entity.DataString = data.GR5_BELT_ROTATE_DIR;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No5GRAB_beltCurrentCT ToL25No5GRAB_beltCurrentCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No5GRAB_beltCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg139No5GRAB_beltCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg139No5GRAB_beltCurrentCT;
            entity.dataCode = GrindData.Code.No5GRAB_beltCurrent;
            entity.dataDesc = GrindData.Desc.Current;
            entity.resultUnit = GrindData.ResultUnit.Current;
            entity.DataString = data.GR5_MOTOR_CURRENT;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }

        public static L2L25_No6GRAB_beltKindCT ToL25No6GRAB_beltKindCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No6GRAB_beltKindCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg141No6GRAB_beltKindCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg141No6GRAB_beltKindCT;
            entity.dataCode = GrindData.Code.No6GRAB_beltKind;
            entity.dataDesc = GrindData.Desc.Kind;
            entity.resultUnit = GrindData.ResultUnit.Kind;
            entity.DataString = data.GR6_BELT_KIND;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No6GRAB_beltRoughnessCT ToL25No6GRAB_beltRoughnessCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No6GRAB_beltRoughnessCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg142No6GRAB_beltRoughnessCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg142No6GRAB_beltRoughnessCT;
            entity.dataCode = GrindData.Code.No6GRAB_beltRoughness;
            entity.dataDesc = GrindData.Desc.Roughness;
            entity.resultUnit = GrindData.ResultUnit.Roughness;
            entity.DataString = data.GR6_BELT_PARTICLE_NO;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No6GRAB_beltRotateDirectionCT ToL25No6GRAB_beltRotateDirectionCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No6GRAB_beltRotateDirectionCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg144No6GRAB_beltRotateDirectionCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg144No6GRAB_beltRotateDirectionCT;
            entity.dataCode = GrindData.Code.No6GRAB_beltRotateDireion;
            entity.dataDesc = GrindData.Desc.direction;
            entity.resultUnit = GrindData.ResultUnit.direction;
            entity.DataString = data.GR6_BELT_ROTATE_DIR;


            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }
        public static L2L25_No6GRAB_beltCurrentCT ToL25No6GRAB_beltCurrentCTEntity(this TBL_PDO pdo, GrindDataModel data, int PassNumber, int Session)
        {
            var entity = new L2L25_No6GRAB_beltCurrentCT();
            entity.Message_Id = L25SysDef.MsgCode.Msg143No6GRAB_beltCurrentCT;
            entity.Message_Length = L25SysDef.MsgLength.Msg143No6GRAB_beltCurrentCT;
            entity.dataCode = GrindData.Code.No6GRAB_beltCurrent;
            entity.dataDesc = GrindData.Desc.Current;
            entity.resultUnit = GrindData.ResultUnit.Current;
            entity.DataString = data.GR6_MOTOR_CURRENT;

            entity.Date = DateTime.Now.ToString("yyyyMMdd");  //共同
            entity.Time = DateTime.Now.ToString("HHmmss");    //共同
            entity.out_mat_no = pdo.Out_Coil_ID;              //共同
            entity.plan_no = pdo.Plan_No;             //共同
            entity.CurrentPassNumber = PassNumber;    //共同
            entity.CurrentSession = Session;          //共同
            entity.lineCode = L2SystemDef.GPLGroup;   //共同
            entity.seqUnit = GrindData.SeqUnit.LenUnit;   //共同      
            entity.genBeginDate = pdo.Start_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genBeginTime = pdo.Start_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.genStopDate = pdo.Finish_Time.ToString(DBParaDef.DB25DateFromat);  //共同
            entity.genStopTime = pdo.Finish_Time.ToString(DBParaDef.DB25TimeFromat);  //共同
            entity.frequency = GrindData.Frenquency.FSecond;  //共同
            entity.DataCount = data.DataCnt.ToString();  //共同
            return entity;
        }


        // 107 -> TBL_GrindRecords
        public static TBL_GrindRecords ToTblGrindRecords(this Msg_107_Grd_Rpt msg, string planNo)
        {

            var data = new TBL_GrindRecords
            {
                Coil_ID = msg.CoilID.ToStr(),
                Plan_No = planNo,
                Current_Pass = msg.CurrentPassNumber,
                Current_Senssion = msg.CurrentSession,
                Line_Speed = msg.LineSpeed,

                GR1_BELT_KIND = msg.No1GRABBeltKind.ToStr(),
                GR1_BELT_PARTICLE_NO = msg.No1GRABBeltRoughness,
                GR1_BELT_ROTATE_DIR = msg.No1GRABBeltRotateDirection,
                GR1_MOTOR_CURRENT = msg.No1GRABBeltMotorCurrent / 10,
                GR1_BELT_SPEED = msg.No1GRABBeltSpeed,

                GR2_BELT_KIND = msg.No2GRABBeltKind.ToStr(),
                GR2_BELT_PARTICLE_NO = msg.No2GRABBeltRoughness,
                GR2_BELT_ROTATE_DIR = msg.No2GRABBeltRotateDirection,
                GR2_MOTOR_CURRENT = msg.No2GRABBeltCurrent / 10,
                GR2_BELT_SPEED = msg.No2GRABBeltSpeed,

                GR3_BELT_KIND = msg.No3GRABBeltKind.ToStr(),
                GR3_BELT_PARTICLE_NO = msg.No3GRABBeltRoughness,
                GR3_BELT_ROTATE_DIR = msg.No3GRABBeltRotateDirection,
                GR3_MOTOR_CURRENT = msg.No3GRABBeltCurrent / 10,

                GR4_BELT_KIND = msg.No4GRABBeltKind.ToStr(),
                GR4_BELT_PARTICLE_NO = msg.No4GRABBeltRoughness,
                GR4_BELT_ROTATE_DIR = msg.No4GRABBeltRotatedirection,
                GR4_MOTOR_CURRENT = msg.No4GRABBeltCurrent / 10,

                GR5_BELT_KIND = msg.No5GRABBeltKind.ToStr(),
                GR5_BELT_PARTICLE_NO = msg.No5GRABBeltRoughness,
                GR5_BELT_ROTATE_DIR = msg.No5GRABBeltRotateDirection,
                GR5_MOTOR_CURRENT = msg.No5GRABBeltCurrent / 10,

                GR6_BELT_KIND = msg.No5GRABBeltKind.ToStr(),
                GR6_BELT_PARTICLE_NO = msg.No6GRABBeltRoughness,
                GR6_BELT_ROTATE_DIR = msg.No6GRABBeltRotateDirection,
                GR6_MOTOR_CURRENT = msg.No5GRABBeltCurrent / 10,
                Receive_Time = DateTime.Now,
            };

            return data;
        }

        // 108 -> TBL_DefectData
        public static L3L2_TBL_DefectData ToTblDefectData(this Msg_108_Defect_Data msg, float starLength)
        {
            var data = new L3L2_TBL_DefectData
            {
                CoilID = msg.CoilID,
                PassNumber = msg.PassNumber,
                DefectCode = msg.DefectKind,
                CurrentSession = msg.CurrentSession,
                //DefectOrigin
                //DefectSide = msg.DefectPosition,
                DefectPositionWidthDirection = msg.DefectPosition.TransferMMSDeffectPosWidthType(),
                DefectPosLengthStartDirection = starLength,
                DefectPoLengthEndDirection = msg.LengthFromHeadEnd,
                DefectLevel = msg.DefectLevel,
                //DefectPercent
                //PDOFlag               
            };
            return data;
        }

        // 108 -> TBL_DefectData
        public static L3L2_TBL_DefectData ToTblDefectData(this Msg_108_Defect_Data msg)
        {
            var starLength = msg.LengthFromHeadEnd - 1;

            var data = new L3L2_TBL_DefectData
            {
                CoilID = msg.CoilID,
                PassNumber = msg.PassNumber,
                DefectCode = msg.DefectKind,
                CurrentSession = msg.CurrentSession,
                //DefectOrigin
                //DefectSide = msg.DefectPosition,
                DefectPositionWidthDirection = msg.DefectPosition.TransferMMSDeffectPosWidthType(),
                DefectPosLengthStartDirection = starLength < 0 ? 0 : starLength,
                DefectPoLengthEndDirection = msg.LengthFromHeadEnd,
                DefectLevel = msg.DefectLevel,
                //DefectPercent
                //PDOFlag               
            };
            return data;
        }

        // 124 -> TBL_StripBrakeSignal
        public static TBL_StripBrakeSignal ToTblStripBrakeSignal(this Msg_124_StripBrakeSignal msg)
        {
            var data = new TBL_StripBrakeSignal
            {
                // POR
                UncoilerCoil_No = msg.PORCoilNo,
                UncoilerCoil_Thick = msg.UncoilerCoilThickness / 1000,
                UncoilerCoil_Width = msg.UncoilerCoilWidth,
                UncoilerCoil_Length = msg.UncoilerCoilLength,
                UncoilerCoil_InnerDiameter = msg.UncoilerCoilInnerDiameter,
                UncoilerCoil_OuterDiameter = msg.UncoilerCoilOuterDiameter,
                UncoilerCoil_TheorticalWt = msg.UncoilerCoilTheoreticalWeight,

                // TR
                RecoilerCoil_No = msg.TRCoilNo,
                RecoilerCoil_Thick = msg.RecoilerCoilThickness / 1000,
                RecoilerCoil_Width = msg.RecoilerCoilWidth,
                RecoilerCoil_Length = msg.RecoilerCoilLength,
                RecoilerCoil_InnerDiameter = msg.RecoilerCoilInnerDiameter,
                RecoilerCoil_OuterDiameter = msg.RecoilerCoilOuterDiameter,
                RecoilerCoil_TheoreticalWt = msg.RecoilerCoilTheoreticalWeight,

                ReceiveTime = msg.DateTime,
            };
            return data;
        }

        // 111 -> TBL_LineFaultRecords
        public static TBL_LineFaultRecords ToTblTBLLineFaultRecords(this Msg_111_LineFault msg, string nowTeam, int nowShift)
        {
            var entity = new TBL_LineFaultRecords();
            entity.unit_code = "GP";//L2SystemDef.SystemIDCode;     // 機組代碼
            entity.prod_time = msg.DateTime.Date;            // 生產日期.收到當下日期                                                             
            entity.delay_reason_code = string.Empty; //msg.FaultCode.ToString();
            entity.stop_category = msg.StopCategory;

            if (msg.LineStatus == PlcSysDef.Cmd.LineStatusStop)
                entity.stop_start_time = DateTime.Now;//msg.DateTime;
            else
                entity.stop_end_time = DateTime.Now;//msg.DateTime;

            if (nowShift == -1)     
                entity.prod_shift_no = string.Empty;
            else
                entity.prod_shift_no = nowShift.ToString();          // 目前生產班次 1-夜(24:00-8:00)，2-早(8:00-16:00)，3-中(16:00-24:00)
            
            
            entity.prod_shift_group = nowTeam;                   // 當日生產組別 A-甲，B-乙，C-丙，D-丁
            entity.UploadMMS = DBParaDef.NO;


            return entity;
        }
        // 112 -> TBL_Utility
        public static TBL_Utility ToTblUtilityEntity(this L1L2Rcv.Msg_112_Utility msg, string shift, string team)
        {
            var utility = new TBL_Utility()
            {

                Receive_Time = DateTime.Now,
                RinseWater = msg.RinseWater / 60,
                IndirectCoolingWater = msg.IndirectCollingWater / 60,
                Steam = msg.Steam / 60,
                CompressedAir = msg.CompressedAir / 60,
                Shift = shift,
                Team = team
            };

            return utility;
        }

        // 125 -> TBL_Coil_CutRecord
        public static TBL_Coil_CutRecord ToTblCoilCutRecord(this Msg_125_Share_Cut_Data msg)
        {
            var TblCoilCutRecord = new TBL_Coil_CutRecord()
            {
                CoilID = msg.CoilID.ToStr().Trim(),
                CutMode = msg.CutMode,
                CutLength = msg.CutLength,
                //DiamRec  = msg.DiamRec,
                //LengthRec = msg.LengthRec,
                //CalculateWeightRec = msg.CalculateWeightRec,
                CutTime = msg.DateTime,
            };

            return TblCoilCutRecord;
        }

        //124 ->  PDOModel.L2L3_PDO
        public static TBL_PDO GenPDO(this Msg_124_StripBrakeSignal msg, TBL_PDI pdi)
        {

            var pdo = new TBL_PDO
            {
                Start_Time = pdi.StarTime,           // 開始時間
                Finish_Time = DateTime.Now,          // 結束時間
                Order_No = pdi.Order_No,             // 合同號
                Plan_No = pdi.Plan_No,               // 計畫號
                Out_Coil_ID = pdi.Out_Coil_ID,        // 出口鋼捲號
                In_Coil_ID = pdi.In_Coil_ID,       // 入口鋼捲號
                //Shift                             // 班次
                //Team                              // 班別
                St_No = pdi.St_No,
                Out_Coil_Outer_Diameter = msg.UncoilerCoilOuterDiameter,     // 出口卷径
                Out_Coil_Inner_Diameter = msg.UncoilerCoilInnerDiameter,              // 出口卷径
                Out_Coil_Length = msg.RecoilerCoilLength,                    // 出口卷長度
                Out_Coil_Thick = msg.RecoilerCoilThickness,                  // 出口厚度
                //Head_C40_Thick                    // 出口頭部C40厚度
                //Mid_C40_Thick                     // 出口中部C40厚度
                //Tail_C40_Thick                    // 出口尾部C40厚度
                //Head_C25_Thick                    // 出口頭部C25厚度
                //Mid_C25_Thick                     // 出口中部C25厚度
                //Tail_C25_Thick                    // 出口尾部C25厚度
                Out_Coil_Width = msg.RecoilerCoilWidth,   // 出口寬度 預設帶入口捲寬度（L2）
                Out_Coil_Theoretical_Weight = msg.RecoilerCoilTheoreticalWeight,  // 理論重
                //Out_Mat_Wt                        // 出口卷淨重
                //Out_Mat_Gross_Wt                  // 秤重訊號（from L1）
                //Head_Pass_Num                     // 頭部出口實際道次
                //Mid_Pass_Num                      // 中部出口實際道次
                //Tail_Pass_Num                     // 尾部出口實際道次
                //Exit Sleeve Use Or Not            // 出口是否用套筒
                //Exit Sleeve Diameter              // 出口套筒內徑
                //Exit Sleeve Code                  // 出口套筒類型
                //Paper_Req_Code                    // 出口墊紙方式
                //Paper_Code                        // 出口墊紙類型
                //Head_Paper_Length                 // 頭部墊紙長度
                //Head_Paper_Width                  // 頭部墊紙寬度
                //Tail_Paper_Length                 // 尾部墊紙長度
                //Tail_Paper_Width                  // 尾部墊紙寬度

                //Defect Data ....1~10
                //Winding_Dire                      // 捲曲方向 GPL固定下收
                //Base Surface                      // 實際好面朝向
                //Inspector                         // 封鎖責任者
                //Hold_Flag                         // 封鎖標記
                //Hold_Cause_Code                   // 封鎖原因代碼
                //Sample_Flag                       // 取樣標記
                //Fixed_Wt_Flag                     // 分標籤記
                //End_Flag                          // 最終標籤記
                //Scrap_Flag                        // 廢品標記
                //Sample_Frqn_Code                  // 取樣位置
                //Surface_Accu_Code                 // 表面精度代碼
                Head_Hole_Position = pdi.Head_Hole_Position,        // 頭部打孔位置
                Head_Leader_Length = pdi.Head_Leader_Length,         // 頭部導帶長度
                Head_Leader_Width = pdi.Head_Leader_Width,          // 頭部導帶寬度
                Head_Leader_Thickness = pdi.Head_Leader_Thickness,  // 頭部導帶厚度
                Tail_Hole_Position = pdi.Tail_Hole_Position,        // 尾部打孔位置
                Tail_Leader_Length = pdi.Tail_Leader_Length,         // 尾部導帶長度
                Tail_Leader_Width = pdi.Tail_Leader_Width,          // 尾部導帶寬度
                Tail_Leader_Thickness = pdi.Tail_Leader_Thickness,  // 尾部導帶厚度
                Head_Leader_St_No = pdi.Head_Leader_St_No,          // 頭部導帶鋼種
                Tail_Leader_St_No = pdi.Tail_Leader_St_No,          // 尾部導帶鋼種
                Head_Off_Gauge = pdi.Head_Off_Gauge,                // 頭部未軋製區域
                Tail_Off_Gauge = pdi.Tail_Off_Gauge,                // 尾部未軋製區域
                Pre_Grinding_Surface = pdi.Pre_Grinding_Surface,    // 鋼卷上次研磨面
                Grinding_Count_Out = pdi.Grinding_Count_Out,        // 鋼卷外表面研磨次數
                Grinding_Count_In = pdi.Grinding_Count_In,          // 鋼卷內表面研磨次數
                Appoint_Grinding_Surface = pdi.Appoint_Grinding_Surface,    //鋼卷指定研磨面
                //Oil_Flag = pdi.Oil,                            // 鋼卷是否有油
                Surface_Accu_Code_In = pdi.Surface_Accu_Code_In,    // 內表面精度代碼
                Surface_Accu_Code_Out = pdi.Surface_Accu_Code_Out,

                Process_Code = pdi.Process_Code,
                Uncoil_Direction = pdi.Uncoil_Direction,


            };

            return pdo;
        }

        //124 -> TBL_UmountRecord
        public static TBL_UnmountRecord ToTblUmountRecord(this Msg_124_StripBrakeSignal msg)
        {
            var record = new TBL_UnmountRecord
            {
                CoilID = msg.PORCoilNo,
                CoilWeight = msg.UncoilerCoilTheoreticalWeight,
                CoilLength = msg.UncoilerCoilLength,
                Diameter = msg.UncoilerCoilOuterDiameter,
                CoiInsideDiam = msg.UncoilerCoilInnerDiameter,
                Width = msg.UncoilerCoilWidth,
            };
            return record;
        }

        //126 -> TBL_UmountRecord
        public static TBL_UnmountRecord ToTblUmountRecord(this Msg_126_Coil_Unmount_POR msg)
        {
            var record = new TBL_UnmountRecord
            {
                CoilID = msg.CoilID.ToStr(),
                CoilWeight = msg.CoilWeight,
                CoilLength = msg.CoilLength,
                Diameter = msg.Diameter,
                CoiInsideDiam = msg.CoiInsideDiam,
                Width = msg.CoiInsideDiam,
            };
            return record;
        }




        //MMS Sleeve Sync -> TBL_LookupTable_Sleeve
        public static TBL_LookupTable_Sleeve ToTblLKSleeve(this MMSL2Rcv.Msg_Sleeve_Value_Synchronize msg)
        {
            var dbModel = new TBL_LookupTable_Sleeve()
            {
                Sleeve_Code = msg.SleeveCode.ToStr(),
                Out_Mat_Inner_Dia = msg.Out_Mat_Inner_Dia.ToStr().ToNullable<int>() ?? 0,
                Sleeve_Thick = msg.Sleeve_Thick.ToStr().ToNullable<int>() ?? 0,
                Sleeve_Width = msg.Sleeve_Width.ToStr().ToNullable<int>() ?? 0,
                Sleeve_Weight = msg.Sleeve_Wt.ToStr().ToNullable<float>() / 1000 ?? 0,
                Sleeve_Material = msg.Sleeve_Type.ToStr(),
                Out_Mat_Width_Min = msg.Out_Mat_Width_Min.ToStr().ToNullable<float>() / 10 ?? 0,
                Out_Mat_Width_Max = msg.Out_Mat_Width_Max.ToStr().ToNullable<float>() / 10 ?? 0,
            };

            return dbModel;
        }

        //MMS Paper Sync -> TBL_LookupTable_Paper
        public static TBL_LookupTable_Paper ToTblLKPaper(this MMSL2Rcv.Msg_Paper_Value_Synchronize msg)
        {
            var dbModel = new TBL_LookupTable_Paper()
            {
                Paper_Code = msg.PaperCode.ToStr(),
                Paper_Base_Weight = msg.Paper_Wt.ToStr().ToNullable<int>() ?? 0,
                Paper_Width = msg.Paper_Width.ToStr().ToNullable<int>() ?? 0,
                Paper_Min_Thick = msg.Paper_Min_Thick.ToStr().ToNullable<int>() ?? 0,
                Paper_Max_Thick = msg.Paper_Max_Thick.ToStr().ToNullable<int>() ?? 0,
                Paper_Thick = msg.Paper_Thick.ToStr().ToNullable<int>() ?? 0,
            };

            return dbModel;
        }



        // 
        public static TBL_BeltPatterns_Records ToBeltPatternsRecords(this TBL_BeltPatterns beltPattern, string coilID, string PassSection, string PlanNo)
        {
            var grindRecord = new TBL_BeltPatterns_Records
            {
                BeltPattern = beltPattern.BeltPattern,
                Pass_From = beltPattern.Pass_From,
                Pass_To = beltPattern.Pass_To,
                GR_NO = beltPattern.GR_NO,
                GR_Current = beltPattern.GR_Current,
                Belt_MaterialCode = beltPattern.Belt_MaterialCode,
                Belt_ParticalNumber = beltPattern.Belt_ParticalNumber,
                Belt_RotateDir = beltPattern.Belt_RotateDir,
                Belt_Speed = beltPattern.Belt_Speed,
                Pass_Section = PassSection,
                Plan_No = PlanNo,
                Coil_ID = coilID,
            };

            return grindRecord;
        }


        public static L3L2_TBL_ScheduleDelete_CoilReject_Record_Temp ToL3L2TBLScheduleDeleteCoilRejectRecordTemp(this string coilID, string operatorId = "", string reasonCode = "", string remarks = "")
        {
            var deleteCoilScheduleTemp = new L3L2_TBL_ScheduleDelete_CoilReject_Record_Temp
            {
                CoilNo = coilID,
                OperatorId = operatorId,
                ReasonCode = reasonCode,
                Remarks = remarks
            };

            return deleteCoilScheduleTemp;
        }

        public static TBL_CoilScheduleDelete ToTblCoilScheduleDelete(this string coilID, string operatorId = "", string reasonCode = "", string remarks = "")
        {
            var delSchedule = new TBL_CoilScheduleDelete
            {
                CoilNo = coilID,
                OperatorId = operatorId,
                ReasonCode = reasonCode,
                Remarks = remarks
            };

            return delSchedule;
        }

        public static ProcessDataEntity.TBL_ProcessData ToTblProcessData(this L1L2Rcv.Msg_104_ProData msg)
        {
            var tblProcessData = new ProcessDataEntity.TBL_ProcessData
            {
                Receive_Time = DateTime.Now,
                Line_Speed = msg.Linespeed,
                Line_Tension = msg.Linetension / 10,
                Line_run_direction = msg.Linerundirection,
                Tension_Reel_Speed = msg.TensionReelSpeed,
                Threading_Speed = msg.ThreadingSpeed,
                GR1_BeltMotor_Current = msg.No1GRAbrasiveBeltMotorCurrent / 10,
                GR1_Belt_Speed = msg.No1GRAbrasiveBeltSpeed,
                GR2_BeltMotor_Current = msg.No2GRAbrasiveBeltMotorCurrent / 10,
                GR2_Belt_Speed = msg.No2GRAbrasiveBeltSpeed,
                GR3_BeltMotor_Current = msg.No3GRAbrasiveBeltMotorCurrent / 10,
                GR4_BeltMotor_Current = msg.No4GRAbrasiveBeltMotorCurrent / 10,
                GR5_BeltMotor_Current = msg.No5GRAbrasiveBeltMotorCurrent / 10,
                GR6_BeltMotor_Current = msg.No6GRAbrasiveBeltMotorCurrent / 10,
                CoolantTank_Temp = msg.CoolantOilTankTemperature / 10,
                AlkaliTank_Temp = msg.AlkaliSolutionTankTemperature / 10,
                PrimaryRinseTank_Temp = msg.PrimaryRinseWaterTankTemperature / 10,
                FinishRinseTank_Temp = msg.FinishRinseTankTemperature / 10,
                BrushRoll1_Current = msg.BrushRollCurrent1 / 10,
                BrushRoll2_Current = msg.BrushRollCurrent2 / 10,
                StripDryerTemp = msg.StripDryerTemperature / 10
            };
            return tblProcessData;
        }

        public static CoilMapEntity.TBL_CoilMap ConvertTblCoilMap(this L1L2Rcv.Msg_105_Trk_Map msg)
        {
            var coilMap = new CoilMapEntity.TBL_CoilMap()
            {
                UpdateTime = DateTime.Now,
                Entry_Car = msg.Entry_Car,
                Entry_SK01 = msg.Entry_SK01,
                Entry_TOP = msg.Entry_TOP,
                POR = msg.POR,
                TR = msg.TR,
                Delivery_SK01 = msg.Delivery_SK01,
                Delivery_SK02 = msg.Delivery_SK02,
                Delivery_TOP = msg.Delivery_TOP,
                Delivery_Car = msg.Delivery_Car
            };

            return coilMap;
        }

        public static UtilityEntity.TBL_Utility ToTblUtility(this L1L2Rcv.Msg_112_Utility msg)
        {
            var utility = new UtilityEntity.TBL_Utility()
            {
                Receive_Time = DateTime.Now,
                CompressedAir = msg.CompressedAir,
                Steam = msg.Steam,
                RinseWater = msg.RinseWater,
                IndirectCoolingWater = msg.IndirectCollingWater
            };

            return utility;
        }

        public static WeldRecordEntity.TBL_WeldRecords ToWeldRecords(this L1L2Rcv.Msg_106_Weld_Data msg, string planNo)
        {
            var coilMap = new WeldRecordEntity.TBL_WeldRecords()
            {
                Coil_ID = msg.CoilID.ToStr(),
                Plan_No = planNo,
                ReceiveTime = DateTime.Now,
                Voltage_Set = msg.WeldVoltageSetPoint / 10,
                Current_Set = msg.WeldCurrentSetPoint,
                Current_Actual = msg.ActualWeldCurrent,
                WireSpeed_Set = msg.WeldWireSpeedSetPoint / 10,
                WeldSpeed_Set = msg.TorchCarriageWeldSpeedPetPoint,
                WeldSpeed_Actual = msg.ActualTorchCarriageWeldSpeed,
                WeldGap = msg.WeldGapSelected / 10,
                StartPaddleTime = msg.StartPuddleTime / 10,
                EndPaddleTime = msg.StopPuddleTime / 10,
                ScheduleNumber = msg.WeldScheduleNumber
            };


            return coilMap;
        }
    }
}

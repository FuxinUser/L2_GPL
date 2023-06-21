using System;

namespace GPLManager
{
    public class DataBaseTableFactory
    {
        public class TBL_PDI
        {
            public string Plan_No { get; set; }
            public int Mat_Plan_Seq_No { get; set; }
            public string Plan_Type { get; set; }
            public string In_Mat_No { get; set; }
            public float In_Mat_Thick { get; set; }
            public float In_Mat_Width { get; set; }
            public int In_Mat_Wt { get; set; }
            public int In_Mat_Len { get; set; }
            public int In_Mat_Inner_Dia { get; set; }
            public int In_Mat_Outer_Dia { get; set; }
            public string Paper_Req_Code { get; set; }
            public string Paper_Code { get; set; }
            public int Head_Paper_Length { get; set; }
            public float Head_Paper_Width { get; set; }
            public int Tail_Paper_Length { get; set; }
            public float Tail_Paper_Width { get; set; }
            public string Sleeve_Type_Code { get; set; }
            public int Sleeve_Diamter { get; set; }
            public string St_No { get; set; }
            public int Density { get; set; }
            public string Surface_Accuracy { get; set; }
            public string Surface_Accu_Code { get; set; }
            public int Flatness_Avg_Cr { get; set; }
            public string Better_Surf_Ward_Code { get; set; }
            public string Uncoil_Direction { get; set; }
            public string Origin_Code { get; set; }
            public int Pack_Mode { get; set; }
            public string Out_Paper_Req_Code { get; set; }
            public string Out_Paper_Code { get; set; }
            public string Out_Sleeve_Type_Code { get; set; }
            public int Out_Sleeve_Diamter { get; set; }
            public string Out_Mat_No { get; set; }
            public string Order_No { get; set; }
            public string Order_Cust_Code { get; set; }
            public string Prev_Whole_Backlog_Code { get; set; }
            public string Next_Whole_Backlog_Code { get; set; }
            public string Head_Leader_Accached { get; set; }
            public float Head_Hole_Position { get; set; }
            public float Head_Leader_Thick { get; set; }
            public float Head_Leader_Width { get; set; }
            public int Head_Leader_Length { get; set; }
            public string Tail_Leader_Accached { get; set; }
            public float Tail_Hole_Position { get; set; }
            public float Tail_Leader_Thick { get; set; }
            public float Tail_Leader_Width { get; set; }
            public int Tail_Leader_Length { get; set; }
            public string Head_Leader_St_No { get; set; }
            public string Tail_Leader_St_No { get; set; }
            public float Out_Mat_Thick { get; set; }
            public float Out_Mat_Min_Thick { get; set; }
            public float Out_Mat_Max_Thick { get; set; }
            public float Out_Mat_Width { get; set; }
            public int Out_Mat_Inner_Dia { get; set; }
            public string Grinding_Flag { get; set; }
            public string Pack_Type_Code { get; set; }
            public string Repair_Type { get; set; }
            public float Scraped_Weight { get; set; }
            public string D01_Code { get; set; }
            public string D01_Origin { get; set; }
            public string D01_Sid { get; set; }
            public string D01_Pos_W { get; set; }
            public int D01_Pos_L_Start { get; set; }
            public int D01_Pos_L_End { get; set; }
            public string D01_Level { get; set; }
            public int D01_Percent { get; set; }
            public string D01_QGrade { get; set; }
            public string D02_Code { get; set; }
            public string D02_Origin { get; set; }
            public string D02_Sid { get; set; }
            public string D02_Pos_W { get; set; }
            public int D02_Pos_L_Start { get; set; }
            public int D02_Pos_L_End { get; set; }
            public string D02_Level { get; set; }
            public int D02_Percent { get; set; }
            public string D02_QGrade { get; set; }
            public string D03_Code { get; set; }
            public string D03_Origin { get; set; }
            public string D03_Sid { get; set; }
            public string D03_Pos_W { get; set; }
            public int D03_Pos_L_Start { get; set; }
            public int D03_Pos_L_End { get; set; }
            public string D03_Level { get; set; }
            public int D03_Percent { get; set; }
            public string D03_QGrade { get; set; }
            public string D04_Code { get; set; }
            public string D04_Origin { get; set; }
            public string D04_Sid { get; set; }
            public string D04_Pos_W { get; set; }
            public int D04_Pos_L_Start { get; set; }
            public int D04_Pos_L_End { get; set; }
            public string D04_Level { get; set; }
            public int D04_Percent { get; set; }
            public string D04_QGrade { get; set; }
            public string D05_Code { get; set; }
            public string D05_Origin { get; set; }
            public string D05_Sid { get; set; }
            public string D05_Pos_W { get; set; }
            public int D05_Pos_L_Start { get; set; }
            public int D05_Pos_L_End { get; set; }
            public string D05_Level { get; set; }
            public int D05_Percent { get; set; }
            public string D05_QGrade { get; set; }
            public string D06_Code { get; set; }
            public string D06_Origin { get; set; }
            public string D06_Sid { get; set; }
            public string D06_Pos_W { get; set; }
            public int D06_Pos_L_Start { get; set; }
            public int D06_Pos_L_End { get; set; }
            public string D06_Level { get; set; }
            public int D06_Percent { get; set; }
            public string D06_QGrade { get; set; }
            public string D07_Code { get; set; }
            public string D07_Origin { get; set; }
            public string D07_Sid { get; set; }
            public string D07_Pos_W { get; set; }
            public int D07_Pos_L_Start { get; set; }
            public int D07_Pos_L_End { get; set; }
            public string D07_Level { get; set; }
            public int D07_Percent { get; set; }
            public string D07_QGrade { get; set; }
            public string D08_Code { get; set; }
            public string D08_Origin { get; set; }
            public string D08_Sid { get; set; }
            public string D08_Pos_W { get; set; }
            public int D08_Pos_L_Start { get; set; }
            public int D08_Pos_L_End { get; set; }
            public string D08_Level { get; set; }
            public int D08_Percent { get; set; }
            public string D08_QGrade { get; set; }
            public string D09_Code { get; set; }
            public string D09_Origin { get; set; }
            public string D09_Sid { get; set; }
            public string D09_Pos_W { get; set; }
            public int D09_Pos_L_Start { get; set; }
            public int D09_Pos_L_End { get; set; }
            public string D09_Level { get; set; }
            public int D09_Percent { get; set; }
            public string D09_QGrade { get; set; }
            public string D10_Code { get; set; }
            public string D10_Origin { get; set; }
            public string D10_Sid { get; set; }
            public string D10_Pos_W { get; set; }
            public int D10_Pos_L_Start { get; set; }
            public int D10_Pos_L_End { get; set; }
            public string D10_Level { get; set; }
            public int D10_Percent { get; set; }
            public string D10_QGrade { get; set; }
            public string Test_Plan_No { get; set; }
            public string Qc_Remark { get; set; }
            public int Head_Off_Gauge { get; set; }
            public int Tail_Off_Gauge { get; set; }
            public string Pre_Grinding_Surface { get; set; }
            public int Grinding_Count_Out { get; set; }
            public int Grinding_Count_In { get; set; }
            public string Appoint_Grinding_Surface { get; set; }
            public string Surface_Accu_Code_In { get; set; }
            public string Surface_Accu_Code_Out { get; set; }
            public string Repair_Remark { get; set; }
            public string Skim_Flag { get; set; }
            public string Polishing_Type { get; set; }
            public string Sg_Sign { get; set; }
            public string Process_Code { get; set; }
            public float Ys_Stand { get; set; }
            public float Ys_Max { get; set; }
            public float Ys_Min { get; set; }
            public string Order_Cust_Ename { get; set; }
            public string Order_Cust_Cname { get; set; }
            public string CreateUserID { get; set; }
            public string Entry_Scaned_CoilID { get; set; }
            public string Entry_Scaned_UserID { get; set; }
            public string Origin_CoilID { get; set; }
            public string Entry_CoilID_Checked { get; set; }
            public string Is_Delete { get; set; }
            public string Delete_UserID { get; set; }
            public string Create_UserID { get; set; }
            public string Coil_Reject_UserID { get; set; }
            public DateTime Entry_Scaned_Time { get; set; }
            public DateTime Delete_DateTime { get; set; }
            public DateTime Entry_Arrive_Time { get; set; }
            public DateTime Coil_Reject_Time { get; set; }
            public DateTime Product_Start_Time { get; set; }
            public DateTime CreateTime { get; set; }
            public DateTime StarTime { get; set; }
            public string Is_Dummy_Coil { get; set; }
        }
        public class L2L3_PDO
        {
            public string OrderNo { get; set; }
            public string PlanNo { get; set; }
            public string Out_Mat_No { get; set; }
            public string In_Mat_No { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime FinishTime { get; set; }
            public string Shift { get; set; }
            public string Team { get; set; }
            public string St_No { get; set; }
            public int Out_Mat_Outer_Diameter { get; set; }
            public int Out_Mat_Inner { get; set; }
            public int Out_Mat_Length { get; set; }
            public float Out_Mat_Thick { get; set; }
            public float Head_C40_Thick { get; set; }
            public float Mid_C40_Thick { get; set; }
            public float Tail_C40_Thick { get; set; }
            public float Head_C25_Thick { get; set; }
            public float Mid_C25_Thick { get; set; }
            public float Tail_C25_Thick { get; set; }
            public float Out_Mat_Width { get; set; }
            public float Out_Theory_Wt { get; set; }
            public int Out_Mat_Wt { get; set; }
            public int Out_Mat_Gross_Wt { get; set; }
            public int Head_Pass_Num { get; set; }
            public int Mid_Pass_Num { get; set; }
            public int Tail_Pass_Num { get; set; }
            public string ExitSleeveUseOrNot { get; set; }
            public int ExitSleeveDiameter { get; set; }
            public string ExitSleeveCode { get; set; }
            public string Paper_Req_Code { get; set; }
            public string Paper_Code { get; set; }
            public int Head_Paper_Length { get; set; }
            public float Head_Paper_Width { get; set; }
            public int Tail_Paper_Length { get; set; }
            public float Tail_Paper_Width { get; set; }
            public string D01_Code { get; set; }
            public string D01_Origin { get; set; }
            public string D01_Sid { get; set; }
            public string D01_Pos_W { get; set; }
            public int D01_Pos_L_Start { get; set; }
            public int D01_Pos_L_End { get; set; }
            public string D01_Level { get; set; }
            public int D01_Percent { get; set; }
            public string D01_QGrade { get; set; }
            public string D02_Code { get; set; }
            public string D02_Origin { get; set; }
            public string D02_Sid { get; set; }
            public string D02_Pos_W { get; set; }
            public int D02_Pos_L_Start { get; set; }
            public int D02_Pos_L_End { get; set; }
            public string D02_Level { get; set; }
            public int D02_Percent { get; set; }
            public string D02_QGrade { get; set; }
            public string D03_Code { get; set; }
            public string D03_Origin { get; set; }
            public string D03_Sid { get; set; }
            public string D03_Pos_W { get; set; }
            public int D03_Pos_L_Start { get; set; }
            public int D03_Pos_L_End { get; set; }
            public string D03_Level { get; set; }
            public int D03_Percent { get; set; }
            public string D03_QGrade { get; set; }
            public string D04_Code { get; set; }
            public string D04_Origin { get; set; }
            public string D04_Sid { get; set; }
            public string D04_Pos_W { get; set; }
            public int D04_Pos_L_Start { get; set; }
            public int D04_Pos_L_End { get; set; }
            public string D04_Level { get; set; }
            public int D04_Percent { get; set; }
            public string D04_QGrade { get; set; }
            public string D05_Code { get; set; }
            public string D05_Origin { get; set; }
            public string D05_Sid { get; set; }
            public string D05_Pos_W { get; set; }
            public int D05_Pos_L_Start { get; set; }
            public int D05_Pos_L_End { get; set; }
            public string D05_Level { get; set; }
            public int D05_Percent { get; set; }
            public string D05_QGrade { get; set; }
            public string D06_Code { get; set; }
            public string D06_Origin { get; set; }
            public string D06_Sid { get; set; }
            public string D06_Pos_W { get; set; }
            public int D06_Pos_L_Start { get; set; }
            public int D06_Pos_L_End { get; set; }
            public string D06_Level { get; set; }
            public int D06_Percent { get; set; }
            public string D06_QGrade { get; set; }
            public string D07_Code { get; set; }
            public string D07_Origin { get; set; }
            public string D07_Sid { get; set; }
            public string D07_Pos_W { get; set; }
            public int D07_Pos_L_Start { get; set; }
            public int D07_Pos_L_End { get; set; }
            public string D07_Level { get; set; }
            public int D07_Percent { get; set; }
            public string D07_QGrade { get; set; }
            public string D08_Code { get; set; }
            public string D08_Origin { get; set; }
            public string D08_Sid { get; set; }
            public string D08_Pos_W { get; set; }
            public int D08_Pos_L_Start { get; set; }
            public int D08_Pos_L_End { get; set; }
            public string D08_Level { get; set; }
            public int D08_Percent { get; set; }
            public string D08_QGrade { get; set; }
            public string D09_Code { get; set; }
            public string D09_Origin { get; set; }
            public string D09_Sid { get; set; }
            public string D09_Pos_W { get; set; }
            public int D09_Pos_L_Start { get; set; }
            public int D09_Pos_L_End { get; set; }
            public string D09_Level { get; set; }
            public int D09_Percent { get; set; }
            public string D09_QGrade { get; set; }
            public string D10_Code { get; set; }
            public string D10_Origin { get; set; }
            public string D10_Sid { get; set; }
            public string D10_Pos_W { get; set; }
            public int D10_Pos_L_Start { get; set; }
            public int D10_Pos_L_End { get; set; }
            public string D10_Level { get; set; }
            public int D10_Percent { get; set; }
            public string D10_QGrade { get; set; }
            public string Winding_Dire { get; set; }
            public string BaseSurface { get; set; }
            public string Inspector { get; set; }
            public string Hold_Flag { get; set; }
            public string Hold_Cause_Code { get; set; }
            public string Sample_Flag { get; set; }
            public string Fixed_Wt_Flag { get; set; }
            public string End_Flag { get; set; }
            public string Scrap_Flag { get; set; }
            public string Sample_Frqn_Code { get; set; }
            public string Surface_Accu_Code { get; set; }
            public float Head_Hole_Position { get; set; }
            public int Head_LeaderLength { get; set; }
            public float Head_Leader_Width { get; set; }
            public float Head_Leader_Thickness { get; set; }
            public float Tail_Hole_Position { get; set; }
            public int Tail_LeaderLength { get; set; }
            public float Tail_Leader_Width { get; set; }
            public float Tail_Leader_Thickness { get; set; }
            public string Head_Leader_St_No { get; set; }
            public string Tail_Leader_St_No { get; set; }
            public int Head_Off_Gauge { get; set; }
            public int Tail_Off_Gauge { get; set; }
            public string Pre_Grinding_Surface { get; set; }
            public int Grinding_Count_Out { get; set; }
            public int Grinding_Count_In { get; set; }
            public string Appoint_Grinding_Surface { get; set; }
            public string Oil_Flag { get; set; }
            public string Surface_Accu_Code_In { get; set; }
            public string Surface_Accu_Code_Out { get; set; }
            public float Head_Rough { get; set; }
            public float Mid_Rough { get; set; }
            public float Tail_Rough { get; set; }
            public string Decoiler_Direction { get; set; }
            public string ProcessCode { get; set; }
            public string PDO_Uploaded_Flag { get; set; }
            public DateTime PDO_Uploaded_Time { get; set; }
            public string PDO_Uploaded_UserID { get; set; }
            public string Exit_Scaned_CoilID { get; set; }
            public string Exit_Scaned_UserID { get; set; }
            public DateTime Exit_Scaned_Time { get; set; }
            public string Exit_CoilID_Checked { get; set; }
            public DateTime CoilWeight_Time { get; set; }
            public DateTime CreateTime { get; set; }
        }
        public class TBL_Production_Schedule
        {
            public string Coil_ID { get; set; }
            public short Seq_No { get; set; }
            public string Update_Source { get; set; }
            public  DateTime UpdateTime { get; set; }
        }
        public class TBL_CoilMap
        {
            public DateTime UpdateTime { get; set; }
            public string Entry_Car { get; set; }
            public string Entry_TOP { get; set; }
            public string Entry_SK01 { get; set; }
            public string POR { get; set; }
            public string TR { get; set; }
            public string Delivery_SK01 { get; set; }
            public string Delivery_SK02 { get; set; }
            public string Delivery_TOP { get; set; }
            public string Delivery_Car { get; set; }
        }
        public class TBL_RetrunCoil
        {
            public string Coil_ID { get; set; }
            public DateTime Create_DateTime { get; set; }
        }
        public class L3L2_TBL_ScheduleDelete_CoilReject_Record
        {
            public string Coil_ID { get; set; }
            public int ScheduleDelete_CoilReject_GroupNo { get; set; }
            public int ScheduleDelete_CoilReject_Code { get; set; }
            public string Remarks { get; set; }
            public string Create_UserID { get; set; }
            public DateTime CreateTime { get; set; }
        }
        public class L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition
        {
            public int ScheduleDelete_CoilReject_GroupNo { get; set; }
            public int ScheduleDelete_CoilReject_Code { get; set; }
            public string ScheduleDelete_CoilReject_Name { get; set; }
            public string Create_UserID { get; set; }
            public DateTime CreateTime { get; set; }
        }
       
        public class TBL_WorkSchedule
        {
            public DateTime CreateTime { get; set; }
            /// <summary>
            /// 班次 1-夜，2-早，3-中
            /// </summary>
            public string Shift { get; set; }
            /// <summary>
            /// 班別 A-甲，B-乙，C-丙，D-丁
            /// </summary>
            public string Team { get; set; }
            public string ShiftDate { get; set; }
            public string ShiftPerson { get; set; }
        }
        public class TBL_UnmountRecord
        { 
            public string CoilID { get; set; }
            public float CoilWeight { get; set; }
            public int CoilLength { get; set; }
            public float Diameter { get; set; }
            public float CoiInsideDiam { get; set; }
            public float Width { get; set; }
            public DateTime CreateTime { get; set; }

        }
        public class TBL_LineFaultRecords 
        {
            public string op_flag { get; set; }
            public string unit_code { get; set; }
            public DateTime prod_time { get; set; }
            public string prod_shift_no { get; set; }
            public string prod_shift_group { get; set; }
            public DateTime stop_start_time { get; set; }
            public DateTime stop_end_time { get; set; }
            public string delay_location { get; set; }
            public string delay_location_desc { get; set; }
            public string stop_elased_timey { get; set; }
            public int stop_category { get; set; }
            public string delay_reason_code { get; set; }
            public string delay_reason_desc { get; set; }
            public string delay_remark { get; set; }
            public string resp_depart_delay_time_m { get; set; }
            public string resp_depart_delay_time_e { get; set; }
            public string resp_depart_delay_time_c { get; set; }
            public string resp_depart_delay_time_p { get; set; }
            public string resp_depart_delay_time_u { get; set; }
            public string resp_depart_delay_time_o { get; set; }
            public string resp_depart_delay_time_r { get; set; }
            public string resp_depart_delay_time_rs { get; set; }
            public int Fault_Code { get; set; }
            public DateTime CreateTime { get; set; }

        }
        public class TBL_DelayLocation_Definition
        {
            public int Index_No { get; set; }
            public string Delay_LocationCode { get; set; }
            public string Delay_LocationName { get; set; }
            public string Create_UserID { get; set; }
            public DateTime CreateTime { get; set; }
        }
        public class TBL_DelayReasonCode_Definition
        {
            public int Index_No { get; set; }
            public string Delay_ReasonCode { get; set; }
            public string Delay_ReasonName { get; set; }
            public string Delay_GroupCode { get; set; }
            public string Delay_GroupName { get; set; }
            public string Responsible_Department { get; set; }
            public string Create_UserID { get; set; }
            public DateTime CreateTime { get; set; }
        }

        public class TBL_FaultCode
        {
            public int Sequence_No { get; set; }
            public string Fault_Code { get; set; }
            public string Fault_Description { get; set; }
            public DateTime CreateTime { get; set; }
        }

        #region LookupTable
        public class TBL_GradeGroups
        {
            public float SteelGrade { get; set; }
            public float CustomerNo { get; set; }
            public float GradeGroup { get; set; }
        }
        public class TBL_LookupTable_Flattener
        {
            public float Coil_Thickness_Min { get; set; }
            public float Coil_Thickness_Max { get; set; }
            public int Strip_Yield_Stress_Min { get; set; }
            public int Strip_Yield_Stress_Max { get; set; }
            public float Intermesh_Num1 { get; set; }
            public float Intermesh_Num2 { get; set; }
            public DateTime UpdateTime { get; set; }
            public string Material_Grade { get; set; }
        }
        public class TBL_LookupTable_LineTension
        {
            public string Material_Grade { get; set; }
            public int Coil_Width { get; set; }
            public float Coil_Thickness { get; set; }
            public float TRTension { get; set; }
            public int LineSpeed { get; set; }
            public string ProcessType { get; set; }
            public DateTime UpdateTime { get; set; }
        }
      
        public class TBL_GrindPlan
        {   
            public string GradeGroup { get; set; }
            public float Thickness_From { get; set; }
            public float Thickness_To { get; set; }
            public string Pass_Section { get; set; }
            public string BeltPattern { get; set; }
            public int PassNumber { get; set; }
            public int LineSpeed { get; set; }
        }
        public class TBL_GrindPlanHistory
        {
            public string GradeGroup { get; set; }
            public float Thickness_From { get; set; }
            public float Thickness_To { get; set; }
            public string Pass_Section { get; set; }
            public string BeltPattern { get; set; }
            public int PassNumber { get; set; }
            public int LineSpeed { get; set; }
            public string Coil_ID { get; set; }
            public DateTime CreateTime { get; set; }
        }
        public class TBL_GrindRecords
        {
            public string Coil_ID { get; set; }
            public int Current_Pass { get; set; }
            /// <summary>
            /// 1:Head 2:Mid 3:Tail
            /// </summary>
            public int Current_Senssion { get; set; }
            public int Line_Speed { get; set; }
            public string GR1_BELT_KIND { get; set; }
            public int GR1_BELT_PARTICLE_NO { get; set; }
            public int GR1_BELT_ROTATE_DIR { get; set; }
            public float GR1_MOTOR_CURRENT { get; set; }
            public int GR1_BELT_SPEED { get; set; }
            public string GR2_BELT_KIND { get; set; }
            public int GR2_BELT_PARTICLE_NO { get; set; }
            public int GR2_BELT_ROTATE_DIR { get; set; }
            public float GR2_MOTOR_CURRENT { get; set; }
            public int GR2_BELT_SPEED { get; set; }
            public string GR3_BELT_KIND { get; set; }
            public int GR3_BELT_PARTICLE_NO { get; set; }
            public int GR3_BELT_ROTATE_DIR { get; set; }
            public float GR3_MOTOR_CURRENT { get; set; }
            public string GR4_BELT_KIND { get; set; }
            public int GR4_BELT_PARTICLE_NO { get; set; }
            public int GR4_BELT_ROTATE_DIR { get; set; }
            public float GR4_MOTOR_CURRENT { get; set; }
            public string GR5_BELT_KIND { get; set; }
            public int GR5_BELT_PARTICLE_NO { get; set; }
            public int GR5_BELT_ROTATE_DIR { get; set; }
            public float GR5_MOTOR_CURRENT { get; set; }
            public string GR6_BELT_KIND { get; set; }
            public int GR6_BELT_PARTICLE_NO { get; set; }
            public int GR6_BELT_ROTATE_DIR { get; set; }
            public float GR6_MOTOR_CURRENT { get; set; }
            public DateTime Receive_Time { get; set; }

        }
        public class TBL_Belts
        {
            public string Belt_No { get; set; }
            public string Belt_Type { get; set; }
            public int Belt_Particle_Number { get; set; }
            public string Suppler_Code { get; set; }
            public string Material_Code { get; set; }
            public float Total_Grind_Length_Belt { get; set; }
            public float Total_Grind_Length_Strip { get; set; }
            public int Mount_GR_No { get; set; }

        }
        public class TBL_BeltMaterials
        {
            public string MATERIAL_CODE { get; set; }
            public string MATERIAL_NAME { get; set; }
        }
        public class TBL_BeltPatterns
        {
            public string BeltPattern { get; set; }
            public int Pass_From { get; set; }
            public int Pass_To { get; set; }
            public int GR_NO { get; set; }
            public float GR_Current { get; set; }
            public string Belt_MaterialCode { get; set; }
            public int Belt_ParticalNumber { get; set; }
            public int Belt_RotateDir { get; set; }
            public int Belt_Speed { get; set; }
        }
      
        public class TBL_BeltSuppliers
        {
            public string SUPPLIER_CODE { get; set; }
            public string SUPPLIER_NAME { get; set; }
        }
        public class TBL_ProcessData
        {
            public DateTime Receive_Time { get; set; }
            public int Line_Speed { get; set; }
            public float Line_Tension { get; set; }
            public float Line_run_direction { get; set; }
            public float GR1_BeltMotor_Current { get; set; }
            public int GR1_Belt_Speed { get; set; }
            public float GR2_BeltMotor_Current { get; set; }
            public int GR2_Belt_Speed { get; set; }
            public float GR3_BeltMotor_Current { get; set; }
            public float GR4_BeltMotor_Current { get; set; }
            public float GR5_BeltMotor_Current { get; set; }
            public float GR6_BeltMotor_Current { get; set; }
            public float CoolantTank_Temp { get; set; }
            public float AlkaliTank_Temp { get; set; }
            public float PrimaryRinseTank_Temp { get; set; }
            public float FinishRinseTank_Temp { get; set; }
            public float StripDryerTemp { get; set; }
            public float BrushRoll1_Current { get; set; }
            public float BrushRoll2_Current { get; set; }

        }
       
        public class TBL_LookupTable_Sleeve
        {
            /// <summary>
            /// 處置標記 新增 = I / 修改 = U / 刪除 = D
            /// </summary>
            public string Deal_Flag { get; set; }
            public string Sleeve_Code { get; set; }
            public string Sleeve_Material { get; set; }
            public int Sleeve_Width { get; set; }
            public int Sleeve_Thick { get; set; }
            public float Sleeve_Weight { get; set; }
            public int Out_Mat_Inner_Dia { get; set; }
            public int Out_Mat_Width_Min { get; set; }
            public int Out_Mat_Width_Max { get; set; }
        }

        public class TBL_LookupTable_Paper
        {
            /// <summary>
            /// 處置標記 新增 = I / 修改 = U / 刪除 = D
            /// </summary>
            public string Deal_Flag { get; set; }
            public string Paper_Code { get; set; }
            public int Paper_Width { get; set; }
            public int Paper_Thick { get; set; }
            public int Paper_Min_Thick { get; set; }
            public int Paper_Max_Thick { get; set; }

        }
        #endregion

        #region 系統設定
        public class TBL_SystemSetting
        {
            public string Parameter_Group { get; set; }
            public string Parameter { get; set; }
            public string Value { get; set; }
            public string Remark { get; set; }
        }
        
        public class TBL_ComboBoxItems
        {
            public int Cbo_Type { get; set; }
            public int Cbo_Index { get; set; }
            public string Cbo_Value { get; set; }
            public string Cbo_Text { get; set; }
            public DateTime UpdateTime { get; set; }
            public string Spare { get; set; }
        }
        public class TBL_ConnectionStatus
        {
            public string Connection_From { get; set; }
            public string Connection_To { get; set; }
            public string Connection_Type { get; set; }
            public string Connection_IP { get; set; }
            public string Connection_Port { get; set; }
            public string Connection_Status { get; set; }
            public DateTime Create_DateTime { get; set; }
        }

        public class TBL_AuthorityData
        {
            public string User_ID { get; set; }
            public string Password { get; set; }
            public string Department { get; set; }
            public string Team { get; set; }
            public string Authority_Class { get; set; }
            public DateTime Create_DateTime { get; set; }
        }
        public class TBL_AuthorityData_Frame
        {
            public string User_ID { get; set; }
            public string Frame_ID { get; set; }
            public string Frame_Function { get; set; }
            public DateTime Create_DateTime { get; set; }
        }
        #endregion


        #region Language
        public class TBL_LanguageSwitch
        { 
            public string PKey { get; set; }
            public string ZH { get; set; }
            public string EN { get; set; }
            public string UpdateUser { get; set; }
            public string UpdateDateTime { get; set; }
        }
        #endregion
    }
}

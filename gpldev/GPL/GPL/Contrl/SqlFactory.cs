using static GPLManager.DataBaseTableFactory;
using static GPLManager.PublicForms;
using static GPLManager.GlobalVariableHandler;
using DBService.Repository;
using DBService.Repository.Belt;
using DBService.Repository.BeltPatterns;
using DBService.Repository.EventLog;
using DBService.Repository.GradeGroups;
using DBService.Repository.GrindPlan;
using DBService.Repository.GrindPlanHistory;
using DBService.Repository.GrindRecords;
using DBService.Repository.ScheduleDelete_CoilReject_Record;
using DBService.Repository.ReturnCoil;
using DBService.Repository.LineStatus;
using DBService.Repository.LookupTbl;
using DBService.Repository.LookupTblFlattener;
using DBService.Repository.PDO;
using DBService.Repository.SystemSetting;
using DBService.Repository.PDI;
using System.Data;
using System.Text;
using System;
using DBService.Repository.SteelNoToMaterialGrade;
using DBService.Repository.BeltMaterials;
using DBService.Repository.ScheduleDelete_CoilReject_Code;
using DBService.Repository.DelayLocation;
using DBService.Repository.DelayReasonCode;
using DBService.Repository.Utility;
using DBService.Repository.BeltPatternsRecords;
using DBService.Repository.WorkSchedule;
using DBService.Repository.SplitCoils;
using DBService.Repository.CoilScheduleDelete;
using System.Windows.Forms;
using DBService.Repository.LineFaultRecords;
using DBService.L1Repository;
using System.Linq;

namespace GPLManager
{
    public static class SqlFactory
    {
        #region frm_1_1_Schedule
        /// <summary>
        /// 清空Schedule
        /// </summary>
        /// <returns></returns>
        public static string Frm_1_1_ClearSchedule()
        {
            string strSql = "";
            strSql = $"Truncate table [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] "; 
            return strSql;
        }
        public static string Frm_1_1_ImportSchedule(int Seq_No,string Coil_ID)
        {
            string strSql = "";
            strSql = $@"Insert into [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}]
                                   ([{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}],
                                    [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}],
                                    [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Update_Source)}],
                                    [{nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)}],
                                    [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}])
                             Values('{Seq_No}','{Coil_ID}','1','{Instance.getTime}','N')";
            return strSql;
        }
        public static string Frm_1_1_SearchCoil_DB_PDI(string Coil_ID)
        {
            string strSql = "";
            strSql = $"Select [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] From [{nameof(PDIEntity.TBL_PDI)}] Where [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{Coil_ID}'";
            return strSql;
        }
        public static string Frm_1_1_UpdateCoilPDI_DB_PDI(DataTable dt,int RowIndex)
        {
            string strSql = "";
            #region SQL
            strSql = $@"Update [{nameof(PDIEntity.TBL_PDI)}] Set
                                [{nameof(PDIEntity.TBL_PDI.Plan_No)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Plan_No)]}',
                            [{nameof(PDIEntity.TBL_PDI.Mat_Seq_No )}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Mat_Seq_No )]}',
                            [{nameof(PDIEntity.TBL_PDI.Plan_Type)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Plan_Type)]}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_ID)]}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_Thick)]}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Width)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_Width)]}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Wt)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_Wt)]}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Length)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_Length)]}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)]}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)]}',
                            [{nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.In_Paper_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Paper_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Paper_Length)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Paper_Length)]}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Paper_Width)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Paper_Width)]}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)]}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)]}',
                            [{nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)]}',
                            [{nameof(PDIEntity.TBL_PDI.St_No)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.St_No)]}',
                            [{nameof(PDIEntity.TBL_PDI.Density)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Density)]}',
                            [{nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture)]}',
                            [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order)]}',
                            [{nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR)]}',
                            [{nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.Uncoil_Direction)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Uncoil_Direction)]}',
                            [{nameof(PDIEntity.TBL_PDI.Origin_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Origin_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.Pack_Mode)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Pack_Mode)]}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Paper_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Paper_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)]}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Coil_ID)]}',
                            [{nameof(PDIEntity.TBL_PDI.Order_No)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Order_No)]}',
                            [{nameof(PDIEntity.TBL_PDI.Order_Cust_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Order_Cust_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_Attached)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Leader_Attached)]}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Hole_Position)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Hole_Position)]}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness)]}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_Width)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Leader_Width)]}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_Length)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Leader_Length)]}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached)]}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Hole_Position)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Hole_Position)]}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness)]}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Width)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Leader_Width)]}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Length)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Leader_Length)]}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_St_No)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Leader_St_No)]}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No)]}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)]}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)]}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)]}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Coil_Width)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Coil_Width)]}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)]}',
                            [{nameof(PDIEntity.TBL_PDI.Grind_Flag)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Grind_Flag)]}',
                            [{nameof(PDIEntity.TBL_PDI.Pack_Type_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Pack_Type_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.Repair_Type)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Repair_Type)]}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Origin)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Origin)]}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Sid)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Sid)]}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Pos_W)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Pos_W)]}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start)]}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Pos_L_End)]}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Level)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Level)]}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Percent)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Percent)]}',
                            [{nameof(PDIEntity.TBL_PDI.D01_QGrade)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_QGrade)]}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Origin)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Origin)]}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Sid)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Sid)]}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Pos_W)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Pos_W)]}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start)]}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Pos_L_End)]}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Level)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Level)]}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Percent)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Percent)]}',
                            [{nameof(PDIEntity.TBL_PDI.D02_QGrade)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_QGrade)]}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Origin)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Origin)]}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Sid)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Sid)]}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Pos_W)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Pos_W)]}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start)]}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Pos_L_End)]}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Level)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Level)]}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Percent)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Percent)]}',
                            [{nameof(PDIEntity.TBL_PDI.D03_QGrade)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_QGrade)]}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Origin)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Origin)]}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Sid)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Sid)]}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Pos_W)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Pos_W)]}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start)]}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Pos_L_End)]}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Level)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Level)]}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Percent)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Percent)]}',
                            [{nameof(PDIEntity.TBL_PDI.D04_QGrade)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_QGrade)]}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Origin)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Origin)]}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Sid)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Sid)]}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Pos_W)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Pos_W)]}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start)]}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Pos_L_End)]}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Level)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Level)]}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Percent)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Percent)]}',
                            [{nameof(PDIEntity.TBL_PDI.D05_QGrade)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_QGrade)]}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Origin)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Origin)]}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Sid)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Sid)]}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Pos_W)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Pos_W)]}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start)]}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Pos_L_End)]}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Level)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Level)]}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Percent)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Percent)]}',
                            [{nameof(PDIEntity.TBL_PDI.D06_QGrade)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_QGrade)]}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Origin)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Origin)]}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Sid)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Sid)]}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Pos_W)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Pos_W)]}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start)]}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Pos_L_End)]}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Level)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Level)]}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Percent)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Percent)]}',
                            [{nameof(PDIEntity.TBL_PDI.D07_QGrade)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_QGrade)]}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Origin)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Origin)]}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Sid)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Sid)]}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Pos_W)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Pos_W)]}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start)]}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Pos_L_End)]}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Level)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Level)]}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Percent)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Percent)]}',
                            [{nameof(PDIEntity.TBL_PDI.D08_QGrade)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_QGrade)]}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Origin)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Origin)]}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Sid)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Sid)]}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Pos_W)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Pos_W)]}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start)]}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Pos_L_End)]}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Level)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Level)]}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Percent)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Percent)]}',
                            [{nameof(PDIEntity.TBL_PDI.D09_QGrade)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_QGrade)]}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Code)]}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Origin)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Origin)]}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Sid)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Sid)]}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Pos_W)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Pos_W)]}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start)]}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Pos_L_End)]}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Level)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Level)]}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Percent)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Percent)]}',
                            [{nameof(PDIEntity.TBL_PDI.D10_QGrade)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_QGrade)]}',
                            [{nameof(PDIEntity.TBL_PDI.Test_Plan_No)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Test_Plan_No)]}',
                            [{nameof(PDIEntity.TBL_PDI.Qc_Rmark)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Qc_Rmark)]}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Off_Gauge)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Off_Gauge)]}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge)]}',
                            [{nameof(PDIEntity.TBL_PDI.Pre_Grinding_Surface)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Pre_Grinding_Surface)]}',
                            [{nameof(PDIEntity.TBL_PDI.Grinding_Count_Out)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Grinding_Count_Out)]}',
                            [{nameof(PDIEntity.TBL_PDI.Grinding_Count_In)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Grinding_Count_In)]}',
                            [{nameof(PDIEntity.TBL_PDI.Appoint_Grinding_Surface)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Appoint_Grinding_Surface)]}',
                            [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In)]}',
                            [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out)]}',
                            [{nameof(PDIEntity.TBL_PDI.Repair_Remark)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Repair_Remark)]}',
                            [{nameof(PDIEntity.TBL_PDI.Skim_Flag)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Skim_Flag)]}',
                            [{nameof(PDIEntity.TBL_PDI.Polishing_Type)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Polishing_Type)]}',
                            [{nameof(PDIEntity.TBL_PDI.Sg_Sign)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Sg_Sign)]}',
                            [{nameof(PDIEntity.TBL_PDI.Process_Code)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Process_Code)]}'
                          Where [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_ID)]}'";
            #endregion
            return strSql;
        }
        public static string Frm_1_1_InsertCoilPDI_DB_PDI(DataTable dt, int RowIndex)
        {
            string strSql = "";
            #region SQL
            strSql = $@" Insert into [{nameof(PDIEntity.TBL_PDI)}] (
                                    [{nameof(PDIEntity.TBL_PDI.Plan_No)}],
                                    [{nameof(PDIEntity.TBL_PDI.Mat_Seq_No )}],
                                    [{nameof(PDIEntity.TBL_PDI.Plan_Type)}],
                                    [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}],
                                    [{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}],
                                    [{nameof(PDIEntity.TBL_PDI.In_Coil_Width)}],
                                    [{nameof(PDIEntity.TBL_PDI.In_Coil_Wt)}],
                                    [{nameof(PDIEntity.TBL_PDI.In_Coil_Length)}],
                                    [{nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)}],
                                    [{nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)}],
                                    [{nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.In_Paper_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.Head_Paper_Length)}],
                                    [{nameof(PDIEntity.TBL_PDI.Head_Paper_Width)}],
                                    [{nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)}],
                                    [{nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)}],
                                    [{nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)}],
                                    [{nameof(PDIEntity.TBL_PDI.St_No)}],
                                    [{nameof(PDIEntity.TBL_PDI.Density)}],
                                    [{nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture)}],
                                    [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order)}],
                                    [{nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR)}],
                                    [{nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.Uncoil_Direction)}],
                                    [{nameof(PDIEntity.TBL_PDI.Origin_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.Pack_Mode)}],
                                    [{nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.Out_Paper_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)}],
                                    [{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}],
                                    [{nameof(PDIEntity.TBL_PDI.Order_No)}],
                                    [{nameof(PDIEntity.TBL_PDI.Order_Cust_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.Head_Leader_Attached)}],
                                    [{nameof(PDIEntity.TBL_PDI.Head_Hole_Position)}],
                                    [{nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness)}],
                                    [{nameof(PDIEntity.TBL_PDI.Head_Leader_Width)}],
                                    [{nameof(PDIEntity.TBL_PDI.Head_Leader_Length)}],
                                    [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached)}],
                                    [{nameof(PDIEntity.TBL_PDI.Tail_Hole_Position)}],
                                    [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness)}],
                                    [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Width)}],
                                    [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Length)}],
                                    [{nameof(PDIEntity.TBL_PDI.Head_Leader_St_No)}],
                                    [{nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No)}],
                                    [{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)}],
                                    [{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)}],
                                    [{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)}],
                                    [{nameof(PDIEntity.TBL_PDI.Out_Coil_Width)}],
                                    [{nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)}],
                                    [{nameof(PDIEntity.TBL_PDI.Grind_Flag)}],
                                    [{nameof(PDIEntity.TBL_PDI.Pack_Type_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.Repair_Type)}],
                                    [{nameof(PDIEntity.TBL_PDI.D01_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.D01_Origin)}],
                                    [{nameof(PDIEntity.TBL_PDI.D01_Sid)}],
                                    [{nameof(PDIEntity.TBL_PDI.D01_Pos_W)}],
                                    [{nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start)}],
                                    [{nameof(PDIEntity.TBL_PDI.D01_Pos_L_End)}],
                                    [{nameof(PDIEntity.TBL_PDI.D01_Level)}],
                                    [{nameof(PDIEntity.TBL_PDI.D01_Percent)}],
                                    [{nameof(PDIEntity.TBL_PDI.D01_QGrade)}],
                                    [{nameof(PDIEntity.TBL_PDI.D02_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.D02_Origin)}],
                                    [{nameof(PDIEntity.TBL_PDI.D02_Sid)}],
                                    [{nameof(PDIEntity.TBL_PDI.D02_Pos_W)}],
                                    [{nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start)}],
                                    [{nameof(PDIEntity.TBL_PDI.D02_Pos_L_End)}],
                                    [{nameof(PDIEntity.TBL_PDI.D02_Level)}],
                                    [{nameof(PDIEntity.TBL_PDI.D02_Percent)}],
                                    [{nameof(PDIEntity.TBL_PDI.D02_QGrade)}],
                                    [{nameof(PDIEntity.TBL_PDI.D03_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.D03_Origin)}],
                                    [{nameof(PDIEntity.TBL_PDI.D03_Sid)}],
                                    [{nameof(PDIEntity.TBL_PDI.D03_Pos_W)}],
                                    [{nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start)}],
                                    [{nameof(PDIEntity.TBL_PDI.D03_Pos_L_End)}],
                                    [{nameof(PDIEntity.TBL_PDI.D03_Level)}],
                                    [{nameof(PDIEntity.TBL_PDI.D03_Percent)}],
                                    [{nameof(PDIEntity.TBL_PDI.D03_QGrade)}],
                                    [{nameof(PDIEntity.TBL_PDI.D04_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.D04_Origin)}],
                                    [{nameof(PDIEntity.TBL_PDI.D04_Sid)}],
                                    [{nameof(PDIEntity.TBL_PDI.D04_Pos_W)}],
                                    [{nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start)}],
                                    [{nameof(PDIEntity.TBL_PDI.D04_Pos_L_End)}],
                                    [{nameof(PDIEntity.TBL_PDI.D04_Level)}],
                                    [{nameof(PDIEntity.TBL_PDI.D04_Percent)}],
                                    [{nameof(PDIEntity.TBL_PDI.D04_QGrade)}],
                                    [{nameof(PDIEntity.TBL_PDI.D05_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.D05_Origin)}],
                                    [{nameof(PDIEntity.TBL_PDI.D05_Sid)}],
                                    [{nameof(PDIEntity.TBL_PDI.D05_Pos_W)}],
                                    [{nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start)}],
                                    [{nameof(PDIEntity.TBL_PDI.D05_Pos_L_End)}],
                                    [{nameof(PDIEntity.TBL_PDI.D05_Level)}],
                                    [{nameof(PDIEntity.TBL_PDI.D05_Percent)}],
                                    [{nameof(PDIEntity.TBL_PDI.D05_QGrade)}],
                                    [{nameof(PDIEntity.TBL_PDI.D06_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.D06_Origin)}],
                                    [{nameof(PDIEntity.TBL_PDI.D06_Sid)}],
                                    [{nameof(PDIEntity.TBL_PDI.D06_Pos_W)}],
                                    [{nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start)}],
                                    [{nameof(PDIEntity.TBL_PDI.D06_Pos_L_End)}],
                                    [{nameof(PDIEntity.TBL_PDI.D06_Level)}],
                                    [{nameof(PDIEntity.TBL_PDI.D06_Percent)}],
                                    [{nameof(PDIEntity.TBL_PDI.D06_QGrade)}],
                                    [{nameof(PDIEntity.TBL_PDI.D07_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.D07_Origin)}],
                                    [{nameof(PDIEntity.TBL_PDI.D07_Sid)}],
                                    [{nameof(PDIEntity.TBL_PDI.D07_Pos_W)}],
                                    [{nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start)}],
                                    [{nameof(PDIEntity.TBL_PDI.D07_Pos_L_End)}],
                                    [{nameof(PDIEntity.TBL_PDI.D07_Level)}],
                                    [{nameof(PDIEntity.TBL_PDI.D07_Percent)}],
                                    [{nameof(PDIEntity.TBL_PDI.D07_QGrade)}],
                                    [{nameof(PDIEntity.TBL_PDI.D08_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.D08_Origin)}],
                                    [{nameof(PDIEntity.TBL_PDI.D08_Sid)}],
                                    [{nameof(PDIEntity.TBL_PDI.D08_Pos_W)}],
                                    [{nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start)}],
                                    [{nameof(PDIEntity.TBL_PDI.D08_Pos_L_End)}],
                                    [{nameof(PDIEntity.TBL_PDI.D08_Level)}],
                                    [{nameof(PDIEntity.TBL_PDI.D08_Percent)}],
                                    [{nameof(PDIEntity.TBL_PDI.D08_QGrade)}],
                                    [{nameof(PDIEntity.TBL_PDI.D09_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.D09_Origin)}],
                                    [{nameof(PDIEntity.TBL_PDI.D09_Sid)}],
                                    [{nameof(PDIEntity.TBL_PDI.D09_Pos_W)}],
                                    [{nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start)}],
                                    [{nameof(PDIEntity.TBL_PDI.D09_Pos_L_End)}],
                                    [{nameof(PDIEntity.TBL_PDI.D09_Level)}],
                                    [{nameof(PDIEntity.TBL_PDI.D09_Percent)}],
                                    [{nameof(PDIEntity.TBL_PDI.D09_QGrade)}],
                                    [{nameof(PDIEntity.TBL_PDI.D10_Code)}],
                                    [{nameof(PDIEntity.TBL_PDI.D10_Origin)}],
                                    [{nameof(PDIEntity.TBL_PDI.D10_Sid)}],
                                    [{nameof(PDIEntity.TBL_PDI.D10_Pos_W)}],
                                    [{nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start)}],
                                    [{nameof(PDIEntity.TBL_PDI.D10_Pos_L_End)}],
                                    [{nameof(PDIEntity.TBL_PDI.D10_Level)}],
                                    [{nameof(PDIEntity.TBL_PDI.D10_Percent)}],
                                    [{nameof(PDIEntity.TBL_PDI.D10_QGrade)}],
                                    [{nameof(PDIEntity.TBL_PDI.Test_Plan_No)}],
                                    [{nameof(PDIEntity.TBL_PDI.Qc_Rmark)}],
                                    [{nameof(PDIEntity.TBL_PDI.Head_Off_Gauge)}],
                                    [{nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge)}],
                                    [{nameof(PDIEntity.TBL_PDI.Pre_Grinding_Surface)}],
                                    [{nameof(PDIEntity.TBL_PDI.Grinding_Count_Out)}],
                                    [{nameof(PDIEntity.TBL_PDI.Grinding_Count_In)}],
                                    [{nameof(PDIEntity.TBL_PDI.Appoint_Grinding_Surface)}],
                                    [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In)}],
                                    [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out)}],
                                    [{nameof(PDIEntity.TBL_PDI.Repair_Remark)}],
                                    [{nameof(PDIEntity.TBL_PDI.Skim_Flag)}],
                                    [{nameof(PDIEntity.TBL_PDI.Polishing_Type)}],
                                    [{nameof(PDIEntity.TBL_PDI.Sg_Sign)}],
                                    [{nameof(PDIEntity.TBL_PDI.Process_Code)}])
                                Values(
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Plan_No)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Mat_Seq_No )]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Plan_Type)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_ID)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_Thick)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_Width)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_Wt)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_Length)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Paper_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Paper_Length)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Paper_Width)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.St_No)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Density)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Uncoil_Direction)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Origin_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Pack_Mode)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Paper_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Coil_ID)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Order_No)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Order_Cust_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Leader_Attached)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Hole_Position)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Leader_Width)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Leader_Length)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Hole_Position)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Leader_Width)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Leader_Length)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Leader_St_No)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Coil_Width)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Grind_Flag)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Pack_Type_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Repair_Type)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Origin)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Sid)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Pos_W)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Pos_L_End)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Level)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_Percent)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D01_QGrade)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Origin)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Sid)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Pos_W)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Pos_L_End)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Level)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_Percent)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D02_QGrade)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Origin)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Sid)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Pos_W)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Pos_L_End)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Level)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_Percent)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D03_QGrade)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Origin)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Sid)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Pos_W)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Pos_L_End)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Level)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_Percent)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D04_QGrade)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Origin)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Sid)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Pos_W)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Pos_L_End)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Level)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_Percent)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D05_QGrade)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Origin)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Sid)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Pos_W)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Pos_L_End)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Level)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_Percent)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D06_QGrade)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Origin)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Sid)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Pos_W)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Pos_L_End)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Level)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_Percent)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D07_QGrade)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Origin)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Sid)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Pos_W)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Pos_L_End)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Level)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_Percent)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D08_QGrade)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Origin)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Sid)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Pos_W)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Pos_L_End)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Level)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_Percent)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D09_QGrade)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Origin)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Sid)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Pos_W)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Pos_L_End)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Level)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_Percent)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.D10_QGrade)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Test_Plan_No)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Qc_Rmark)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Head_Off_Gauge)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Pre_Grinding_Surface)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Grinding_Count_Out)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Grinding_Count_In)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Appoint_Grinding_Surface)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Repair_Remark)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Skim_Flag)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Polishing_Type)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Sg_Sign)]}' ,
                                 '{dt.Rows[RowIndex][nameof(PDIEntity.TBL_PDI.Process_Code)]}')";

            #endregion
            return strSql;
        }
        /// <summary>
        /// 排程TabPage-排程DGV資料
        /// </summary>
        /// <returns></returns>
        public static string Frm_1_1_SelectSchedule_InitialDataGridView_DB_Schedule_PDI(int intTop = 0)
        {
            string strTop = "";
            if (intTop > 0)
                strTop += $"Top {intTop}";

            string strSql = "";
            #region sql
            strSql = $@"select {strTop}
                      ROW_NUMBER() OVER( ORDER BY   a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}]) AS No,
                      b.[{nameof(PDIEntity.TBL_PDI.Plan_No)}]
                     ,a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}]
                     ,a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Width)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Wt)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Length)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)}] 
                     ,b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.In_Paper_Code)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Head_Paper_Length)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Head_Paper_Width)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.St_No)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Density)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Sg_Sign)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Repair_Type)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Uncoil_Direction)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Out_Paper_Code)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Pack_Mode)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Grind_Flag)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Origin_Code)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Width)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Order_No)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Skim_Flag)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Head_Leader_Attached)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Head_Hole_Position)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Head_Leader_Width)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Head_Leader_Length)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Tail_Hole_Position)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Width)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Length)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Head_Leader_St_No)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Head_Off_Gauge)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Pre_Grinding_Surface)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Grinding_Count_Out)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Grinding_Count_In)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Appoint_Grinding_Surface)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Polishing_Type)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Order_Cust_Code)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Process_Code)}]
                     ,b.[{nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR)}]
                      From [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] a

                     Left join (Select MAX({nameof(PDIEntity.TBL_PDI.CreateTime)}) as LastCreateTime ,{nameof(PDIEntity.TBL_PDI.In_Coil_ID)} 
                                   From {nameof(PDIEntity.TBL_PDI)} Group by {nameof(PDIEntity.TBL_PDI.In_Coil_ID)}) as  temp
                             on a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] =  temp.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] 					
                        Left join  [{nameof(PDIEntity.TBL_PDI)}] b on b.[{nameof(PDIEntity.TBL_PDI.CreateTime)}] = temp.[LastCreateTime] AND b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = temp.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}]

                      Where a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'N' or a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'R'
                      Order by a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}] asc";

           // Left join[{ nameof(CoilPDIModel.TBL_PDI)}] b on a.[{ nameof(CoilScheduleModel.TBL_Production_Schedule.Coil_ID)}] = b.[{ nameof(CoilPDIModel.TBL_PDI.In_Coil_ID)}] 
            #endregion
            return strSql;
        }
        /// <summary>
        /// 排程TabPage-排程DGV資料
        /// </summary>
        /// <returns></returns>
        public static string Frm_1_1_SelectDM_DB_PDI(bool bolSearch = false)
        {
            string strSql = "";
            #region sql
            strSql = $@"select 
                      [{nameof(PDIEntity.TBL_PDI.Plan_No)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Mat_Seq_No )}]
                     ,[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}]
                     ,[{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}]
                     ,[{nameof(PDIEntity.TBL_PDI.In_Coil_Width)}]
                     ,[{nameof(PDIEntity.TBL_PDI.In_Coil_Wt)}]
                     ,[{nameof(PDIEntity.TBL_PDI.In_Coil_Length)}]
                     ,[{nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)}] 
                     ,[{nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)}]
                     ,[{nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)}]
                     ,[{nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)}]
                     ,[{nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)}]
                     ,[{nameof(PDIEntity.TBL_PDI.In_Paper_Code)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Head_Paper_Length)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Head_Paper_Width)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)}]
                     ,[{nameof(PDIEntity.TBL_PDI.St_No)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Density)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Sg_Sign)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Repair_Type)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Uncoil_Direction)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Out_Paper_Code)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Pack_Mode)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Grind_Flag)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Origin_Code)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Out_Coil_Width)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Order_No)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Skim_Flag)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Head_Leader_Attached)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Head_Hole_Position)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Head_Leader_Width)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Head_Leader_Length)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Tail_Hole_Position)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Width)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Length)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Head_Leader_St_No)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Head_Off_Gauge)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Pre_Grinding_Surface)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Grinding_Count_Out)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Grinding_Count_In)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Appoint_Grinding_Surface)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Polishing_Type)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Order_Cust_Code)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Process_Code)}]
                     ,[{nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR)}]
                      From [{nameof(PDIEntity.TBL_PDI)}]
                      Where [{nameof(PDIEntity.TBL_PDI.Is_Dummy_Coil)}] = '1'";
            if (bolSearch) strSql += $" And [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{PublicForms.PDISchl.Cob_Entry_Coil_No_Dummy.Text}'";
            #endregion
            return strSql;
        }
        /// <summary>
        /// 排程TabPage-紀錄原有的Seq_No
        /// </summary>
        /// <returns></returns>
        public static string Frm_1_1_SelectSchedule_SeqNoRecords_DB_Schedule()
        {
            string strSql = "";//Top 40
            strSql = $@"Select  [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}] 
                        From  [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] 
                        Where [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'N' or [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'R'
                        Order by [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}] asc";
            return strSql;
        }
        /// <summary>
        /// 排程TabPage-紀錄原有修改時間
        /// </summary>
        /// <returns></returns>
        public static string Frm_1_1_SelectSchedule_UpdateTimeRecords_DB_Schedule()
        {
            string strSql = "";//Top 40
            strSql = $@"Select  [{nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)}]
                        From [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}]
                        Where [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'N' or [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'R'
                        Order by [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}] asc";
            return strSql;
        }
        /// <summary>
        /// 查詢TabPage-條件查詢
        /// </summary>
        /// <returns></returns>
        public static string Frm_1_1_CoilSearch_DataGridView_DB_PDI_Map()
        {
            string strSql = "";
            #region SQL
            strSql = $@"select
                     ROW_NUMBER() OVER( ORDER BY   a.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}]) AS No,
                      a.[{nameof(PDIEntity.TBL_PDI.Plan_No)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Mat_Seq_No )}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.In_Coil_Width)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.In_Coil_Wt)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.In_Coil_Length)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.In_Paper_Code)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Head_Paper_Length)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Head_Paper_Width)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.St_No)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Density)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Sg_Sign)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Repair_Type)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Uncoil_Direction)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Out_Paper_Code)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Pack_Mode)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Grind_Flag)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Origin_Code)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Width)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Order_No)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Skim_Flag)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Head_Leader_Attached)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Head_Hole_Position)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Head_Leader_Width)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Head_Leader_Length)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Tail_Hole_Position)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Width)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Length)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Head_Leader_St_No)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Head_Off_Gauge)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Pre_Grinding_Surface)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Grinding_Count_Out)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Grinding_Count_In)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Appoint_Grinding_Surface)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Polishing_Type)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Order_Cust_Code)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Process_Code)}]
                     ,a.[{nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR)}]";
            #endregion
           
            return strSql;
        }
        /// <summary>
        /// 查詢TabPage-ComboBox選項
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static string Frm_1_1_CoilSearch_SearchComboBoxItems_DB_PDI(string columns)
        {
            string strSql = "";
            strSql = $@"  select {columns} from [{nameof(PDIEntity.TBL_PDI)}] ";
            return strSql;
        }
        /// <summary>
        /// 修改時間確認
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_1_1_MoveScheduleUpdateTimeCheck_DB_Schedule(string Coil_ID)
        {
            string strSql = "";
            strSql = $@"select [{nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)}] 
                        from [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}]
                        where [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = '{Coil_ID} '";
            return strSql;
        }
        /// <summary>
        /// 修改Schedule順序
        /// </summary>
        /// <param name="Seq_No"></param>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_1_1_UpdateSchedule_DB_Schedule(string Seq_No,string Coil_ID)
        {
            string strSql = "";
            strSql = $@" Update [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] 
                         set [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}] = {Seq_No} 
                            ,[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Update_Source)}] ='1'
                            ,[{nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)}] = '{Main.lblClock.Text}' 
                        where [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = '{Coil_ID}'";
            return strSql;
        }

        public static string Frm_1_1_SQL_Select_SystemSetting_TopScheduleLock()
        {
            string strSql = $@"Select [{nameof(TBL_SystemSetting.Value)}] 
                        From [{nameof(TBL_SystemSetting)}]
                        Where [{nameof(TBL_SystemSetting.Parameter_Group)}] = '{GlobalVariableHandler.Instance.proLine}'
                        AND [{nameof(TBL_SystemSetting.Parameter)}] = 'TopScheduleLock' ";

            return strSql;
        }

        public static string Frm_1_1_SQL_Update_SystemSetting_TopScheduleLock(string strValue)
        {
            string strSql = $@"Update [{nameof(TBL_SystemSetting)}] 
                        Set [{nameof(TBL_SystemSetting.Value)}] = '{strValue}'  
                        Where [{nameof(TBL_SystemSetting.Parameter_Group)}] = '{GlobalVariableHandler.Instance.proLine}'
                        AND [{nameof(TBL_SystemSetting.Parameter)}] = 'TopScheduleLock' ";
            return strSql;
        }

        #endregion

        #region Dummy
        public static string Frm_DummyCoil_DummyList_DB_PDI()
        {
            string strSql = "";
            strSql = $@"Select 
                            [{nameof(PDIEntity.TBL_PDI.Plan_No)}],
                            {nameof(PDIEntity.TBL_PDI)}.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}],
                            Du_Count ,
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}],
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Width)}],
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Wt)}],
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Length)}],
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)}],
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)}]
                       From [{nameof(PDIEntity.TBL_PDI)}]

                        LEFT JOIN (Select  {nameof(PDOEntity.TBL_PDO.In_Coil_ID)} 
                                          ,COUNT({nameof(PDOEntity.TBL_PDO.In_Coil_ID)}) as Du_Count
                        From [{nameof(PDOEntity.TBL_PDO)}]	Group by {nameof(PDOEntity.TBL_PDO.In_Coil_ID)}) as tb_Dummy
                         ON  {nameof(PDIEntity.TBL_PDI)}.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = tb_Dummy.{nameof(PDOEntity.TBL_PDO.In_Coil_ID)} 

                       Where [{nameof(PDIEntity.TBL_PDI.Is_Dummy_Coil)}] = '1' 
                       Order by [{nameof(PDIEntity.TBL_PDI.Mat_Seq_No )}] asc ";
            return strSql;
        }
        public static string Frm_DummyCoil_ScheduleList_DB_Schedule_PDI(int intTop = 40)
        {
            //string strSql = "";
            //strSql = $@" Select Top 40 a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] 
            //            From
            //            ( SELECT [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}], ROW_NUMBER() OVER (ORDER BY [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}]) as ROWNUM
            //              FROM [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}]) a
            //             LEFT JOIN [{nameof(PDIEntity.TBL_PDI)}] b  on a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] 
            //             where ROWNUM > 2";
            string strTop = "";
            if (intTop > 0)
                strTop += $"Top {intTop}";

            string strSql = "";
            #region sql
            strSql = $@"select {strTop}
                      ROW_NUMBER() OVER( ORDER BY   a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}]) AS No,
                      b.[{nameof(PDIEntity.TBL_PDI.Plan_No)}]
                     ,a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}]
                     ,a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}]
                    
                      From [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] a

                     Left join (Select MAX({nameof(PDIEntity.TBL_PDI.CreateTime)}) as LastCreateTime ,{nameof(PDIEntity.TBL_PDI.In_Coil_ID)} 
                                   From {nameof(PDIEntity.TBL_PDI)} Group by {nameof(PDIEntity.TBL_PDI.In_Coil_ID)}) as  temp
                             on a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] =  temp.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] 					
                        Left join  [{nameof(PDIEntity.TBL_PDI)}] b on b.[{nameof(PDIEntity.TBL_PDI.CreateTime)}] = temp.[LastCreateTime] AND b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = temp.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}]

                      Where a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'N' or a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'R'
                      --AND No > 2
                      Order by a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}] asc";
            #endregion
            return strSql;
        }
        public static string Frm_DummyCoil_InsertDummy_DB_Schedule(string DummyCoil, decimal Seq_No)
        {
            string strSql = "";
            strSql = $@"Insert into [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}]
                            ([{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}],[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}],[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}],[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Update_Source)}],[{nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)}])
                        Values
                            ('{DummyCoil}','{Seq_No}','N','1','{Instance.getTime}')";
            return strSql;
        }

        public static string Frm_DummyCoil_UpdateAllScheduleTime()
        {
            string strSql = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" Update [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] ");
            sb.AppendLine($" Set    [{nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)}] = '{Instance.getTime}' ");
            sb.AppendLine($" Where  [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] != 'P' ");

            strSql = sb.ToString();
            return strSql;
        }

        public static string Frm_DummyCoil_UpdateDummy_DB_PDI(string strDummyCoil, string strOld_PlanNo, string strNew_PlanNo)
        {
            string strSql = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" Update [{nameof(PDIEntity.TBL_PDI)}] ");
            sb.AppendLine($" Set    [{nameof(PDIEntity.TBL_PDI.Plan_No)}] = '{strNew_PlanNo}' ");
            sb.AppendLine($" Where  [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{strDummyCoil}' ");
            sb.AppendLine($" AND    [{nameof(PDIEntity.TBL_PDI.Plan_No)}] = '{strOld_PlanNo}' ");

            strSql = sb.ToString();
            return strSql;
        }


        public static string Frm_1_1_DeleteSchedule_DB_Schedule(string Coil_ID)
        {
            string strSql = "";
            strSql = $@"DELETE FROM [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}]
                        Where [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = '{Coil_ID}'";
            return strSql;
        }
        public static string Frm_1_1_SelectDeleteScheduleRecord(string Coil_ID)
        {
            string strSql = $@"Select [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record.Coil_ID)}] 
                                 From [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record)}] 
                                Where [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record.Coil_ID)}] = '{Coil_ID}'";
            return strSql;
        }

        public static string Frm_1_1_DeleteDummyRecord()
        {
            string strSql = $@"Insert into [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record)}]
                                          ([{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record.Coil_ID)}],
                                           [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record.ScheduleDelete_CoilReject_Code)}],
                                           [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record.Remarks)}],
                                           [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record.Create_UserID)}],
                                           [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record.CreateTime)}])
                                    Values
                                         ('{PublicForms.PDISchl.Lbl_DummyCoil.Text}',
                                          '{PublicForms.PDISchl.Cob_DummyDelReason.SelectedValue}',
                                          '{PublicForms.PDISchl.Txt_DelDummySpare.Text}',
                                          '{PublicForms.Main.lblLoginUser.Text}',
                                          '{GlobalVariableHandler.Instance.getTime}')";
            return strSql;
        }
        #endregion

        #region Frm_1_2
        /// <summary>
        /// Select PDI 各欄位資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_1_2_SelectedData_DB_PDI(string Coil_ID,string strPlan_No = "" )
        {
            string strSql = $@"Select * From [{nameof(PDIEntity.TBL_PDI)}] Where [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{Coil_ID}' ";
            if (!string.IsNullOrEmpty(strPlan_No))
                strSql += $@" And [{nameof(PDIEntity.TBL_PDI.Plan_No)}] = '{strPlan_No}' ";
            strSql += $@" ORDER BY   [{nameof(PDIEntity.TBL_PDI.CreateTime)}] DESC";

            #region Old_Sql
            //strSql = $@"SELECT 
            //                 [{nameof(CoilPDIModel.L3L2_PDI.Plan_No)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Mat_Plan_Seq_No)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Plan_Type)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.In_Mat_No)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.In_Mat_Thick)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.In_Mat_Width)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.In_Mat_Wt)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.In_Mat_Len)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.In_Mat_Inner_Dia)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.In_Mat_Outer_Dia)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Sleeve_Type_Code)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Sleeve_Diamter)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Paper_Req_Code)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Paper_Code)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Head_Paper_Length)}] 
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Head_Paper_Width)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Tail_Paper_Length)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Tail_Paper_Width)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.St_No)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Density)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Repair_Type)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Surface_Accu_Code)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Surface_Accuracy)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Better_Surf_Ward_Code)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Uncoil_Direction)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Out_Mat_No)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Out_Paper_Req_Code)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Out_Paper_Code)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Out_Sleeve_Diamter)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Out_Sleeve_Type_Code)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Pack_Mode)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Origin_Code)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Prev_Whole_Backlog_Code)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Next_Whole_Backlog_Code)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Head_Leader_St_No)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Head_Leader_Accached)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Head_Hole_Position)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Head_Leader_Thick)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Head_Leader_Width)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Head_Leader_Length)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Tail_Leader_St_No)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Tail_Leader_Accached)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Tail_Hole_Position)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Tail_Leader_Thick)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Tail_Leader_Width)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Tail_Leader_Length)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Head_Off_Gauge)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Tail_Off_Gauge)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Sg_Sign)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Pre_Grinding_Surface)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Grinding_Count_Out)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Grinding_Count_In)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Appoint_Grinding_Surface)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Out_Mat_Thick)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Out_Mat_Max_Thick)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Out_Mat_Min_Thick)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Out_Mat_Width)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Out_Mat_Inner_Dia)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Skim_Flag)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Polishing_Type)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Grinding_Flag)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Test_Plan_No)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Qc_Rmark)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Order_No)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Order_Cust_Code)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Scraped_Weight)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Ys_Stand)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Ys_Max)}]
            //                ,[{nameof(CoilPDIModel.L3L2_PDI.Ys_Min)}]
            //        FROM [{nameof(CoilPDIModel.L3L2_PDI)}] 
            //        where [{nameof(CoilPDIModel.L3L2_PDI.In_Mat_No)}] = '{Coil_ID}'";
            #endregion
            return strSql;
        }
        /// <summary>
        /// Select 缺陷資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_1_2_SelectedDefectData_DB_PDI(string Coil_ID, string strPlan_No = "")
        {
            string strSql = "";
            #region Sql
            strSql = $@"select
                         [{nameof(PDIEntity.TBL_PDI.Plan_No)}]
                        ,[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D01_Code)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D02_Code)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D03_Code)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D04_Code)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D05_Code)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D06_Code)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D07_Code)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D08_Code)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D09_Code)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D10_Code)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D01_Origin)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D02_Origin)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D03_Origin)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D04_Origin)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D05_Origin)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D06_Origin)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D07_Origin)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D08_Origin)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D09_Origin)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D10_Origin)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D01_Sid)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D02_Sid)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D03_Sid)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D04_Sid)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D05_Sid)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D06_Sid)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D07_Sid)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D08_Sid)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D09_Sid)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D10_Sid)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D01_Pos_W)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D02_Pos_W)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D03_Pos_W)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D04_Pos_W)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D05_Pos_W)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D06_Pos_W)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D07_Pos_W)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D08_Pos_W)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D09_Pos_W)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D10_Pos_W)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D01_Pos_L_End)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D02_Pos_L_End)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D03_Pos_L_End)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D04_Pos_L_End)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D05_Pos_L_End)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D06_Pos_L_End)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D07_Pos_L_End)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D08_Pos_L_End)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D09_Pos_L_End)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D10_Pos_L_End)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D01_Level)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D02_Level)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D03_Level)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D04_Level)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D05_Level)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D06_Level)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D07_Level)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D08_Level)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D09_Level)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D10_Level)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D01_Percent)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D02_Percent)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D03_Percent)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D04_Percent)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D05_Percent)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D06_Percent)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D07_Percent)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D08_Percent)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D09_Percent)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D10_Percent)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D01_QGrade)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D02_QGrade)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D03_QGrade)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D04_QGrade)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D05_QGrade)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D06_QGrade)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D07_QGrade)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D08_QGrade)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D09_QGrade)}]
                        ,[{nameof(PDIEntity.TBL_PDI.D10_QGrade)}]
                       From [{nameof(PDIEntity.TBL_PDI)}]
                       Where [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{Coil_ID}'";

            #endregion
            if (!string.IsNullOrEmpty(strPlan_No))
                strSql += $@" And [{nameof(PDIEntity.TBL_PDI.Plan_No)}] = '{strPlan_No}' ";
            strSql += $@" ORDER BY   [{nameof(PDIEntity.TBL_PDI.CreateTime)}] DESC";
            return strSql;
        }
        /// <summary>
        /// frm_1_2 入口鋼卷號ComboBox選項清單
        /// </summary>
        /// <returns></returns>
        public static string Frm_1_2_CoilComboxItems_DB_PDI()
        {
            string strSql = "";
            strSql = $@"Select [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}]  From [{nameof(PDIEntity.TBL_PDI)}] ";
            strSql += $" GROUP  BY　 [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}]";
            return strSql;
        }
        /// <summary>
        /// 新增PDI
        /// </summary>
        /// <param name="Qc_Remark"></param>
        /// <returns></returns>
        public static string Frm_1_2_InsertPDI_DB_PDI(string Qc_Remark)
        {
            string strSql = "";
            #region SQL
            strSql = $@" Insert into [{nameof(PDIEntity.TBL_PDI)}] (
                             [{nameof(PDIEntity.TBL_PDI.Plan_No)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Mat_Seq_No )}]
                            ,[{nameof(PDIEntity.TBL_PDI.Plan_Type)}]
                            ,[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}]
                            ,[{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}]

                            ,[{nameof(PDIEntity.TBL_PDI.In_Coil_Width)}]
                            ,[{nameof(PDIEntity.TBL_PDI.In_Coil_Wt)}]
                            ,[{nameof(PDIEntity.TBL_PDI.In_Coil_Length)}]
                            ,[{nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)}]
                            ,[{nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)}]

                            ,[{nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)}]
                            ,[{nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.In_Paper_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Head_Paper_Length)}] 

                            ,[{nameof(PDIEntity.TBL_PDI.Head_Paper_Width)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)}]
                            ,[{nameof(PDIEntity.TBL_PDI.St_No)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Density)}]

                            ,[{nameof(PDIEntity.TBL_PDI.Repair_Type)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out)}]

                            ,[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Desc_Order)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Desc_Acture)}]                           
                            , [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Desc_In)}]
                            , [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Desc_Out)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)}]

                            ,[{nameof(PDIEntity.TBL_PDI.Uncoil_Direction)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Order_No)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Order_Cust_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Order_Cust_Ename)}]

                            ,[{nameof(PDIEntity.TBL_PDI.Order_Cust_Cname)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Out_Paper_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}]

                            ,[{nameof(PDIEntity.TBL_PDI.Pack_Mode)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Origin_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Head_Leader_St_No)}]

                            ,[{nameof(PDIEntity.TBL_PDI.Head_Leader_Attached)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Head_Hole_Position)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Head_Leader_Width)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Head_Leader_Length)}]

                            ,[{nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Tail_Hole_Position)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Width)}]

                            ,[{nameof(PDIEntity.TBL_PDI.Tail_Leader_Length)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Out_Coil_Width)}]

                            ,[{nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Qc_Rmark)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Head_Off_Gauge)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Grinding_Count_Out)}]

                            ,[{nameof(PDIEntity.TBL_PDI.Grinding_Count_In)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Skim_Flag)}]
                            ,[{nameof(PDIEntity.TBL_PDI.CreateTime)}]

                            ,[{nameof(PDIEntity.TBL_PDI.D01_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D02_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D03_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D04_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D05_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D06_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D07_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D08_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D09_Code)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D10_Code)}]

                            ,[{nameof(PDIEntity.TBL_PDI.D01_Origin)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D02_Origin)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D03_Origin)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D04_Origin)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D05_Origin)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D06_Origin)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D07_Origin)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D08_Origin)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D09_Origin)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D10_Origin)}]

                            ,[{nameof(PDIEntity.TBL_PDI.D01_Sid)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D02_Sid)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D03_Sid)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D04_Sid)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D05_Sid)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D06_Sid)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D07_Sid)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D08_Sid)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D09_Sid)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D10_Sid)}]

                            ,[{nameof(PDIEntity.TBL_PDI.D01_Pos_W)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D02_Pos_W)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D03_Pos_W)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D04_Pos_W)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D05_Pos_W)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D06_Pos_W)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D07_Pos_W)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D08_Pos_W)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D09_Pos_W)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D10_Pos_W)}]

                            ,[{nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start)}]

                            ,[{nameof(PDIEntity.TBL_PDI.D01_Pos_L_End)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D02_Pos_L_End)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D03_Pos_L_End)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D04_Pos_L_End)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D05_Pos_L_End)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D06_Pos_L_End)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D07_Pos_L_End)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D08_Pos_L_End)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D09_Pos_L_End)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D10_Pos_L_End)}]

                            ,[{nameof(PDIEntity.TBL_PDI.D01_Level)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D02_Level)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D03_Level)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D04_Level)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D05_Level)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D06_Level)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D07_Level)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D08_Level)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D09_Level)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D10_Level)}]

                            ,[{nameof(PDIEntity.TBL_PDI.D01_Percent)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D02_Percent)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D03_Percent)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D04_Percent)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D05_Percent)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D06_Percent)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D07_Percent)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D08_Percent)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D09_Percent)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D10_Percent)}]

                            ,[{nameof(PDIEntity.TBL_PDI.D01_QGrade)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D02_QGrade)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D03_QGrade)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D04_QGrade)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D05_QGrade)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D06_QGrade)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D07_QGrade)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D08_QGrade)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D09_QGrade)}]
                            ,[{nameof(PDIEntity.TBL_PDI.D10_QGrade)}]

                            ,[{nameof(PDIEntity.TBL_PDI.Is_Delete)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Scraped_Weight)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Ys_Stand)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Ys_Max)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Ys_Min)}]
                            ,[{nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR)}])
                        VALUES ( 
                            '{PDIDetail.Txt_Plan_No.Text}'
                            ,'{PDIDetail.Txt_SeqNo.Text}'
                            ,'{PDIDetail.Cob_Plan_Sort.SelectedValue}'
                            ,'{PDIDetail.Txt_In_Coil_ID.Text}'
                            ,'{PDIDetail.Txt_In_Coil_Thick.Text}'

                            ,'{PDIDetail.Txt_In_Coil_Width.Text}'
                            ,'{PDIDetail.Txt_In_Coil_Wt.Text}'
                            ,'{PDIDetail.Txt_In_Coil_Length.Text}'
                            ,'{PDIDetail.Txt_In_Coil_Inner_Diameter.Text}'
                            ,'{PDIDetail.Txt_In_Coil_Outer_Diameter.Text}'

                            ,'{PDIDetail.Cob_Sleeve_Type_Entry.SelectedValue}'
                            ,'{PDIDetail.Txt_Sleeve_Inner_Entry.Text}'
                            ,'{PDIDetail.Cob_PAPER_REQ_CODE.SelectedValue}'
                            ,'{PDIDetail.Cob_In_Paper_Type_Entry.SelectedValue}'
                            ,'{PDIDetail.Txt_In_Paper_Head_Length.Text}'

                            ,'{PDIDetail.Txt_In_Paper_Head_Width.Text}'
                            ,'{PDIDetail.Txt_In_Paper_Tail_Length.Text}'
                            ,'{PDIDetail.Txt_In_Paper_Tail_Width.Text}'
                            ,'{PDIDetail.Txt_St_no.Text}'
                            ,'{PDIDetail.Txt_Density.Text}'

                            ,'{PDIDetail.Cob_Rework_Type.SelectedValue}'
                            ,'{PDIDetail.Cob_Surface_Finishing_Code.SelectedValue}'
                            ,'{PDIDetail.Cob_Surface_Accuracy_Code.SelectedValue}'
                            ,'{PDIDetail.Cob_Surface_Accu_Code_In.SelectedValue}'
                            ,'{PDIDetail.Cob_Surface_Accu_Code_Out.SelectedValue}'

                            ,'{PDIDetail.Txt_Surface_Accu_Desc.Text}'--
                            ,'{PDIDetail.Txt_Surface_Accuracy_Desc.Text}'--
                            ,'{PDIDetail.Txt_Surface_Accu_Desc_In.Text}'--
                            ,'{PDIDetail.Txt_Surface_Accu_Desc_Out.Text}'--
                            ,'{PDIDetail.Cob_Base_Surface.SelectedValue}'

                            ,'{PDIDetail.Cob_Uncoil_Direction.SelectedValue}'
                            ,'{PDIDetail.Txt_Out_Coil_ID.Text}'
                            ,'{PDIDetail.Txt_Order_No.Text}'
                            ,'{PDIDetail.Txt_Order_Cust_Code.Text}'
                            ,'{PDIDetail.Txt_Order_Cust_Ename.Text}'

                            ,'{PDIDetail.Txt_Order_Cust_Cname.Text}'
                            ,'{PDIDetail.Cob_OUT_PAPER_REQ_CODE.SelectedValue}'
                            ,'{PDIDetail.Cob_Paper_Type_Exit.SelectedValue}'
                            ,'{PDIDetail.Txt_Sleeve_Inner_Exit.Text}'
                            ,'{PDIDetail.Cob_Sleeve_Type_Exit.SelectedValue}'

                            ,'{PDIDetail.Txt_Strap.Text}'
                            ,'{PDIDetail.Cob_CoilOrigin.SelectedValue}'
                            ,'{PDIDetail.Txt_Wholebacklog.Text}'
                            ,'{PDIDetail.Txt_NWholebacklog.Text}'
                            ,'{PDIDetail.Txt_HSt_no.Text}'

                            ,'{PDIDetail.Cob_Head_Leader.SelectedValue}'
                            ,'{PDIDetail.Txt_Head_Hole.Text}'
                            ,'{PDIDetail.Txt_HThickness.Text}'
                            ,'{PDIDetail.Txt_HWd.Text}'
                            ,'{PDIDetail.Txt_HLen.Text}'

                            ,'{PDIDetail.Txt_TSt_no.Text}'
                            ,'{PDIDetail.Cob_Tail_Leader.SelectedValue}'
                            ,'{PDIDetail.Txt_Tail_Hole.Text}'
                            ,'{PDIDetail.Txt_TThickness.Text}'
                            ,'{PDIDetail.Txt_TWd.Text}'

                            ,'{PDIDetail.Txt_TLen.Text}'
                            ,'{PDIDetail.Txt_Out_Coil_Thick.Text}'
                            ,'{PDIDetail.Txt_Out_Coil_Thick_Max.Text}'
                            ,'{PDIDetail.Txt_Out_Coil_Thick_Min.Text}'
                            ,'{PDIDetail.Txt_Out_Coil_Width.Text}'

                            ,'{PDIDetail.Txt_OutInner.Text}'
                            ,'{Qc_Remark}'
                            ,'{PDIDetail.Txt_HEAD_OFF_GAUGE.Text}'
                            ,'{PDIDetail.Txt_Tail_off_gauge.Text}'
                            ,'{PDIDetail.Txt_GRINDING_COUNT_OUT.Text}'

                            ,'{PDIDetail.Txt_GRINDING_COUNT_IN.Text}'
                            ,'{PDIDetail.Cob_SKIM_FLAG.SelectedValue}'
                            ,'{Instance.getTime}'

                            ,'{PDIDetail.Txt_Code_D1.Text}'
                            ,'{PDIDetail.Txt_Code_D2.Text}'
                            ,'{PDIDetail.Txt_Code_D3.Text}'
                            ,'{PDIDetail.Txt_Code_D4.Text}'
                            ,'{PDIDetail.Txt_Code_D5.Text}'
                            ,'{PDIDetail.Txt_Code_D6.Text}'
                            ,'{PDIDetail.Txt_Code_D7.Text}'
                            ,'{PDIDetail.Txt_Code_D8.Text}'
                            ,'{PDIDetail.Txt_Code_D9.Text}'
                            ,'{PDIDetail.Txt_Code_D10.Text}'

                            ,'{PDIDetail.Txt_Origin_D1.Text}'
                            ,'{PDIDetail.Txt_Origin_D2.Text}'
                            ,'{PDIDetail.Txt_Origin_D3.Text}'
                            ,'{PDIDetail.Txt_Origin_D4.Text}'
                            ,'{PDIDetail.Txt_Origin_D5.Text}'
                            ,'{PDIDetail.Txt_Origin_D6.Text}'
                            ,'{PDIDetail.Txt_Origin_D7.Text}'
                            ,'{PDIDetail.Txt_Origin_D8.Text}'
                            ,'{PDIDetail.Txt_Origin_D9.Text}'
                            ,'{PDIDetail.Txt_Origin_D10.Text}'

                            ,'{PDIDetail.Cob_Sid_D01.SelectedValue}'
                            ,'{PDIDetail.Cob_Sid_D02.SelectedValue}'
                            ,'{PDIDetail.Cob_Sid_D03.SelectedValue}'
                            ,'{PDIDetail.Cob_Sid_D04.SelectedValue}'
                            ,'{PDIDetail.Cob_Sid_D05.SelectedValue}'
                            ,'{PDIDetail.Cob_Sid_D06.SelectedValue}'
                            ,'{PDIDetail.Cob_Sid_D07.SelectedValue}'
                            ,'{PDIDetail.Cob_Sid_D08.SelectedValue}'
                            ,'{PDIDetail.Cob_Sid_D09.SelectedValue}'
                            ,'{PDIDetail.Cob_Sid_D10.SelectedValue}'

                            ,'{PDIDetail.Cob_PosW_D01.SelectedValue}'
                            ,'{PDIDetail.Cob_PosW_D02.SelectedValue}'
                            ,'{PDIDetail.Cob_PosW_D03.SelectedValue}'
                            ,'{PDIDetail.Cob_PosW_D04.SelectedValue}'
                            ,'{PDIDetail.Cob_PosW_D05.SelectedValue}'
                            ,'{PDIDetail.Cob_PosW_D06.SelectedValue}'
                            ,'{PDIDetail.Cob_PosW_D07.SelectedValue}'
                            ,'{PDIDetail.Cob_PosW_D08.SelectedValue}'
                            ,'{PDIDetail.Cob_PosW_D09.SelectedValue}'
                            ,'{PDIDetail.Cob_PosW_D10.SelectedValue}'

                            ,'{PDIDetail.Txt_Pos_L_Start_D1.Text}'
                            ,'{PDIDetail.Txt_Pos_L_Start_D2.Text}'
                            ,'{PDIDetail.Txt_Pos_L_Start_D3.Text}'
                            ,'{PDIDetail.Txt_Pos_L_Start_D4.Text}'
                            ,'{PDIDetail.Txt_Pos_L_Start_D5.Text}'
                            ,'{PDIDetail.Txt_Pos_L_Start_D6.Text}'
                            ,'{PDIDetail.Txt_Pos_L_Start_D7.Text}'
                            ,'{PDIDetail.Txt_Pos_L_Start_D8.Text}'
                            ,'{PDIDetail.Txt_Pos_L_Start_D9.Text}'
                            ,'{PDIDetail.Txt_Pos_L_Start_D10.Text}'

                            ,'{PDIDetail.Txt_Pos_L_End_D1.Text}'
                            ,'{PDIDetail.Txt_Pos_L_End_D2.Text}'
                            ,'{PDIDetail.Txt_Pos_L_End_D3.Text}'
                            ,'{PDIDetail.Txt_Pos_L_End_D4.Text}'
                            ,'{PDIDetail.Txt_Pos_L_End_D5.Text}'
                            ,'{PDIDetail.Txt_Pos_L_End_D6.Text}'
                            ,'{PDIDetail.Txt_Pos_L_End_D7.Text}'
                            ,'{PDIDetail.Txt_Pos_L_End_D8.Text}'
                            ,'{PDIDetail.Txt_Pos_L_End_D9.Text}'
                            ,'{PDIDetail.Txt_Pos_L_End_D10.Text}'

                            ,'{PDIDetail.Cob_Level_D01.SelectedValue}'
                            ,'{PDIDetail.Cob_Level_D02.SelectedValue}'
                            ,'{PDIDetail.Cob_Level_D03.SelectedValue}'
                            ,'{PDIDetail.Cob_Level_D04.SelectedValue}'
                            ,'{PDIDetail.Cob_Level_D05.SelectedValue}'
                            ,'{PDIDetail.Cob_Level_D06.SelectedValue}'
                            ,'{PDIDetail.Cob_Level_D07.SelectedValue}'
                            ,'{PDIDetail.Cob_Level_D08.SelectedValue}'
                            ,'{PDIDetail.Cob_Level_D09.SelectedValue}'
                            ,'{PDIDetail.Cob_Level_D10.SelectedValue}'

                            ,'{PDIDetail.Txt_Percent_D1.Text}'
                            ,'{PDIDetail.Txt_Percent_D2.Text}'
                            ,'{PDIDetail.Txt_Percent_D3.Text}'
                            ,'{PDIDetail.Txt_Percent_D4.Text}'
                            ,'{PDIDetail.Txt_Percent_D5.Text}'
                            ,'{PDIDetail.Txt_Percent_D6.Text}'
                            ,'{PDIDetail.Txt_Percent_D7.Text}'
                            ,'{PDIDetail.Txt_Percent_D8.Text}'
                            ,'{PDIDetail.Txt_Percent_D9.Text}'
                            ,'{PDIDetail.Txt_Percent_D10.Text}'

                            ,'{PDIDetail.Txt_QGRADE_D1.Text}'
                            ,'{PDIDetail.Txt_QGRADE_D2.Text}'
                            ,'{PDIDetail.Txt_QGRADE_D3.Text}'
                            ,'{PDIDetail.Txt_QGRADE_D4.Text}'
                            ,'{PDIDetail.Txt_QGRADE_D5.Text}'
                            ,'{PDIDetail.Txt_QGRADE_D6.Text}'
                            ,'{PDIDetail.Txt_QGRADE_D7.Text}'
                            ,'{PDIDetail.Txt_QGRADE_D8.Text}'
                            ,'{PDIDetail.Txt_QGRADE_D9.Text}'
                            ,'{PDIDetail.Txt_QGRADE_D10.Text}'

                            ,'0'--Is_Delete 
                            ,'{PDIDetail.Txt_Scraped_Weight.Text}'
                            ,'{PDIDetail.Txt_Act_YS_Stand.Text}'
                            ,'{PDIDetail.Txt_YS_Stand_Max.Text}'
                            ,'{PDIDetail.Txt_YS_Stand_Min.Text}'
                            ,'{PDIDetail.Txt_Flatness_Avg_Crness.Text}')";
            #endregion
            return strSql;
        }
        /// <summary>
        /// 檢查PDI是否有被刪除
        /// </summary>
        /// <returns></returns>
        public static string Frm_1_2_CheckDelete_DB_PDI(string Coil_ID)
        {
            string strSql = "";
            strSql = $@" Select [{nameof(PDIEntity.TBL_PDI.Is_Delete)}] 
                         From [{nameof(PDIEntity.TBL_PDI)}] 
                         Where [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{Coil_ID}'";
            return strSql;
        }
        /// <summary>
        /// 刪除PDI-上[Is_Delete]註記
        /// </summary>
        /// <param name="Plan_No"></param>
        /// <param name="Seq_No"></param>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_1_2_DeletePDI_DB_PDI(string Plan_No, string Seq_No, string Coil_ID)
        {
            string strSql = "";
            strSql = $@"Update [{nameof(PDIEntity.TBL_PDI)}] set [{nameof(PDIEntity.TBL_PDI.Is_Delete)}] = '1' ,[{nameof(PDIEntity.TBL_PDI.Delete_DateTime)}] = '{Instance.getTime }'
                        Where [{nameof(PDIEntity.TBL_PDI.Plan_No)}] = '{Plan_No}'
                        and [{nameof(PDIEntity.TBL_PDI.Mat_Seq_No )}] = '{Seq_No}'
                        and [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{Coil_ID}'";
            return strSql;
        }

        /// <summary>
        /// 儲存PDI
        /// </summary>
        /// <param name="Qc_Remark"></param>
        /// <returns></returns>
        public static string Frm_1_2_SavePDI_DB_PDI(string Qc_Remark)
        {
            string strSql = "";
            #region SQL
            strSql = $@"Update [{nameof(PDIEntity.TBL_PDI)}]  set
                            [{nameof(PDIEntity.TBL_PDI.Plan_No)}]='{PDIDetail.Txt_Plan_No.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Mat_Seq_No )}]='{PDIDetail.Txt_SeqNo.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Plan_Type)}]='{PDIDetail.Cob_Plan_Sort.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{PDIDetail.Txt_In_Coil_ID.Text}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}]='{PDIDetail.Txt_In_Coil_Thick.Text}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Width)}]='{PDIDetail.Txt_In_Coil_Width.Text}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Wt)}]='{ PDIDetail.Txt_In_Coil_Wt.Text}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Length)}]='{PDIDetail.Txt_In_Coil_Length.Text}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)}]='{PDIDetail.Txt_In_Coil_Inner_Diameter.Text}',
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)}]='{PDIDetail.Txt_In_Coil_Outer_Diameter.Text}',
                            [{nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)}]='{PDIDetail.Cob_Sleeve_Type_Entry.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)}]='{PDIDetail.Txt_Sleeve_Inner_Entry.Text}',
                            [{nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)}]='{PDIDetail.Cob_PAPER_REQ_CODE.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.In_Paper_Code)}]='{ PDIDetail.Cob_In_Paper_Type_Entry.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Paper_Length)}]='{PDIDetail.Txt_In_Paper_Head_Length.Text}', 
                            [{nameof(PDIEntity.TBL_PDI.Head_Paper_Width)}]='{PDIDetail.Txt_In_Paper_Head_Width.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)}]='{ PDIDetail.Txt_In_Paper_Tail_Length.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)}]='{PDIDetail.Txt_In_Paper_Tail_Width.Text}',
                            [{nameof(PDIEntity.TBL_PDI.St_No)}]='{ PDIDetail.Txt_St_no.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Density)}]='{PDIDetail.Txt_Density.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Order_No)}] = '{PDIDetail.Txt_Order_No.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Order_Cust_Code)}] = '{PDIDetail.Txt_Order_Cust_Code.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Order_Cust_Ename)}] = '{PDIDetail.Txt_Order_Cust_Ename.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Order_Cust_Cname)}] = '{PDIDetail.Txt_Order_Cust_Cname.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Repair_Type)}]='{PDIDetail.Cob_Rework_Type.SelectedValue}',

                            [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order)}]='{PDIDetail.Cob_Surface_Finishing_Code.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture)}]='{PDIDetail.Cob_Surface_Accuracy_Code.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In)}]='{PDIDetail.Cob_Surface_Accu_Code_In.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out)}]='{PDIDetail.Cob_Surface_Accu_Code_Out.SelectedValue}',

                            [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Desc_Order)}]='{PDIDetail.Txt_Surface_Accu_Desc.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Desc_Acture)}]='{PDIDetail.Txt_Surface_Accuracy_Desc.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Desc_In)}]='{PDIDetail.Txt_Surface_Accu_Desc_In.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Surface_Accu_Desc_Out)}]='{PDIDetail.Txt_Surface_Accu_Desc_Out.Text}',

                            [{nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)}]='{PDIDetail.Cob_Base_Surface.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Uncoil_Direction)}]='{PDIDetail.Cob_Uncoil_Direction.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}]='{PDIDetail.Txt_Out_Coil_ID.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)}]='{PDIDetail.Cob_OUT_PAPER_REQ_CODE.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Paper_Code)}]='{PDIDetail.Cob_Paper_Type_Exit.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)}]='{PDIDetail.Txt_Sleeve_Inner_Exit.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}]='{PDIDetail.Cob_Sleeve_Type_Exit.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Pack_Mode)}]='{PDIDetail.Txt_Strap.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Origin_Code)}]='{PDIDetail.Cob_CoilOrigin.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code)}]='{PDIDetail.Txt_Wholebacklog.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code)}]='{PDIDetail.Txt_NWholebacklog.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_St_No)}]='{PDIDetail.Txt_HSt_no.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_Attached)}]='{PDIDetail.Cob_Head_Leader.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Hole_Position)}]='{PDIDetail.Txt_Head_Hole.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness)}]='{PDIDetail.Txt_HThickness.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_Width)}]='{PDIDetail.Txt_HWd.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_Length)}]='{PDIDetail.Txt_HLen.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No)}]='{PDIDetail.Txt_TSt_no.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached)}]='{PDIDetail.Cob_Tail_Leader.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Hole_Position)}]='{PDIDetail.Txt_Tail_Hole.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness)}]='{PDIDetail.Txt_TThickness.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Width)}]='{PDIDetail.Txt_TWd.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Length)}]='{PDIDetail.Txt_TLen.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Head_Off_Gauge)}]='{PDIDetail.Txt_HEAD_OFF_GAUGE.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge)}]='{PDIDetail.Txt_Tail_off_gauge.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Sg_Sign)}]='{PDIDetail.Txt_SG_Sign.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Pre_Grinding_Surface)}]='{PDIDetail.Cob_PRE_GRINDING_SURFACE.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Grinding_Count_Out)}]='{PDIDetail.Txt_GRINDING_COUNT_OUT.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Grinding_Count_In)}]='{PDIDetail.Txt_GRINDING_COUNT_IN.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Appoint_Grinding_Surface)}]='{PDIDetail.Cob_APPOINT_GRINDING_SURFACE.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)}]='{PDIDetail.Txt_Out_Coil_Thick.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)}]='{PDIDetail.Txt_Out_Coil_Thick_Max.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)}]='{PDIDetail.Txt_Out_Coil_Thick_Min.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Coil_Width)}]='{PDIDetail.Txt_Out_Coil_Width.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)}]='{PDIDetail.Txt_OutInner.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Skim_Flag)}]='{PDIDetail.Cob_SKIM_FLAG.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Polishing_Type)}]='{PDIDetail.Cob_POLISHING_TYPE.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Grind_Flag)}]='{PDIDetail.Cob_GrandFlag.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.Test_Plan_No)}]='{PDIDetail.Txt_TestPlan.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Qc_Rmark)}]='{Qc_Remark}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Code)}]='{ PDIDetail.Txt_Code_D1.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Code)}]='{ PDIDetail.Txt_Code_D2.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Code)}]='{ PDIDetail.Txt_Code_D3.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Code)}]='{ PDIDetail.Txt_Code_D4.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Code)}]='{ PDIDetail.Txt_Code_D5.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Code)}]='{ PDIDetail.Txt_Code_D6.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Code)}]='{ PDIDetail.Txt_Code_D7.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Code)}]='{ PDIDetail.Txt_Code_D8.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Code)}]='{ PDIDetail.Txt_Code_D9.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Code)}]='{ PDIDetail.Txt_Code_D10.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Origin)}]='{ PDIDetail.Txt_Origin_D1.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Origin)}]='{ PDIDetail.Txt_Origin_D2.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Origin)}]='{ PDIDetail.Txt_Origin_D3.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Origin)}]='{ PDIDetail.Txt_Origin_D4.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Origin)}]='{ PDIDetail.Txt_Origin_D5.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Origin)}]='{ PDIDetail.Txt_Origin_D6.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Origin)}]='{ PDIDetail.Txt_Origin_D7.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Origin)}]='{ PDIDetail.Txt_Origin_D8.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Origin)}]='{ PDIDetail.Txt_Origin_D9.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Origin)}]='{ PDIDetail.Txt_Origin_D10.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Sid)}]='{PDIDetail.Cob_Sid_D01.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Sid)}]='{PDIDetail.Cob_Sid_D02.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Sid)}]='{PDIDetail.Cob_Sid_D03.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Sid)}]='{PDIDetail.Cob_Sid_D04.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Sid)}]='{PDIDetail.Cob_Sid_D05.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Sid)}]='{PDIDetail.Cob_Sid_D06.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Sid)}]='{PDIDetail.Cob_Sid_D07.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Sid)}]='{PDIDetail.Cob_Sid_D08.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Sid)}]='{PDIDetail.Cob_Sid_D09.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Sid)}]='{PDIDetail.Cob_Sid_D10.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Pos_W)}]='{PDIDetail.Cob_PosW_D01.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Pos_W)}]='{PDIDetail.Cob_PosW_D02.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Pos_W)}]='{PDIDetail.Cob_PosW_D03.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Pos_W)}]='{PDIDetail.Cob_PosW_D04.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Pos_W)}]='{PDIDetail.Cob_PosW_D05.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Pos_W)}]='{PDIDetail.Cob_PosW_D06.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Pos_W)}]='{PDIDetail.Cob_PosW_D07.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Pos_W)}]='{PDIDetail.Cob_PosW_D08.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Pos_W)}]='{PDIDetail.Cob_PosW_D09.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Pos_W)}]='{PDIDetail.Cob_PosW_D10.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start)}]='{PDIDetail.Txt_Pos_L_Start_D1.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start)}]='{PDIDetail.Txt_Pos_L_Start_D2.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start)}]='{PDIDetail.Txt_Pos_L_Start_D3.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start)}]='{PDIDetail.Txt_Pos_L_Start_D4.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start)}]='{PDIDetail.Txt_Pos_L_Start_D5.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start)}]='{PDIDetail.Txt_Pos_L_Start_D6.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start)}]='{PDIDetail.Txt_Pos_L_Start_D7.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start)}]='{PDIDetail.Txt_Pos_L_Start_D8.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start)}]='{PDIDetail.Txt_Pos_L_Start_D9.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start)}]='{PDIDetail.Txt_Pos_L_Start_D10.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Pos_L_End)}]='{PDIDetail.Txt_Pos_L_End_D1.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Pos_L_End)}]='{PDIDetail.Txt_Pos_L_End_D2.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Pos_L_End)}]='{PDIDetail.Txt_Pos_L_End_D3.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Pos_L_End)}]='{PDIDetail.Txt_Pos_L_End_D4.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Pos_L_End)}]='{PDIDetail.Txt_Pos_L_End_D5.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Pos_L_End)}]='{PDIDetail.Txt_Pos_L_End_D6.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Pos_L_End)}]='{PDIDetail.Txt_Pos_L_End_D7.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Pos_L_End)}]='{PDIDetail.Txt_Pos_L_End_D8.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Pos_L_End)}]='{PDIDetail.Txt_Pos_L_End_D9.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Pos_L_End)}]='{PDIDetail.Txt_Pos_L_End_D10.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Level)}]='{PDIDetail.Cob_Level_D01.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Level)}]='{PDIDetail.Cob_Level_D02.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Level)}]='{PDIDetail.Cob_Level_D03.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Level)}]='{PDIDetail.Cob_Level_D04.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Level)}]='{PDIDetail.Cob_Level_D05.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Level)}]='{PDIDetail.Cob_Level_D06.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Level)}]='{PDIDetail.Cob_Level_D07.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Level)}]='{PDIDetail.Cob_Level_D08.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Level)}]='{PDIDetail.Cob_Level_D09.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Level)}]='{PDIDetail.Cob_Level_D10.SelectedValue}',
                            [{nameof(PDIEntity.TBL_PDI.D01_Percent)}]='{PDIDetail.Txt_Percent_D1.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D02_Percent)}]='{PDIDetail.Txt_Percent_D2.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D03_Percent)}]='{PDIDetail.Txt_Percent_D3.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D04_Percent)}]='{PDIDetail.Txt_Percent_D4.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D05_Percent)}]='{PDIDetail.Txt_Percent_D5.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D06_Percent)}]='{PDIDetail.Txt_Percent_D6.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D07_Percent)}]='{PDIDetail.Txt_Percent_D7.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D08_Percent)}]='{PDIDetail.Txt_Percent_D8.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D09_Percent)}]='{PDIDetail.Txt_Percent_D9.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D10_Percent)}]='{PDIDetail.Txt_Percent_D10.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D01_QGrade)}] ='{PDIDetail.Txt_QGRADE_D1.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D02_QGrade)}] ='{PDIDetail.Txt_QGRADE_D2.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D03_QGrade)}] ='{PDIDetail.Txt_QGRADE_D3.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D04_QGrade)}] ='{PDIDetail.Txt_QGRADE_D4.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D05_QGrade)}] ='{PDIDetail.Txt_QGRADE_D5.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D06_QGrade)}] ='{PDIDetail.Txt_QGRADE_D6.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D07_QGrade)}] ='{PDIDetail.Txt_QGRADE_D7.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D08_QGrade)}] ='{PDIDetail.Txt_QGRADE_D8.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D09_QGrade)}] ='{PDIDetail.Txt_QGRADE_D9.Text}',
                            [{nameof(PDIEntity.TBL_PDI.D10_QGrade)}] ='{PDIDetail.Txt_QGRADE_D10.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Scraped_Weight)}] ='{PDIDetail.Txt_Scraped_Weight.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Ys_Stand)}] ='{PDIDetail.Txt_Act_YS_Stand.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Ys_Max)}] ='{PDIDetail.Txt_YS_Stand_Max.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Ys_Min)}] ='{PDIDetail.Txt_YS_Stand_Min.Text}',
                            [{nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR)}] ='{PDIDetail.Txt_Flatness_Avg_Crness.Text}'
                    where [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}]='{PDIDetail.Txt_In_Coil_ID.Text}'";
            #endregion
            return strSql;
        }
        #endregion

        #region Frm_1_3
        public static string Frm_1_3_SelectDeleteRecord()
        {
            string strSql = $@"Select * 
                               From [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete)}] 
                               Order by [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CreateTime)}] desc";
            return strSql;
        }
        public static string Frm_1_3_QuerrytDeleteRecord()
        {
            string strSql = $@"Select * 
                               From [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete)}]";

            int CheckedCount = 0;

            if (PublicForms.DeleteScheduleRecord.Chk_Coil.Checked.Equals(true))
            {
                strSql += $@"Where [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CoilNo)}] = '{PublicForms.DeleteScheduleRecord.Cob_Entry_Coil_No.Text}'";
                CheckedCount += 1;
            }

            if (PublicForms.DeleteScheduleRecord.Chk_DeleteCode.Checked.Equals(true) && CheckedCount > 0)
            {
                strSql += $@"And [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.ReasonCode)}] = '{PublicForms.DeleteScheduleRecord.Cob_DeleteCode.SelectedValue}'";
                CheckedCount += 1;
            }
            else if (PublicForms.DeleteScheduleRecord.Chk_DeleteCode.Checked.Equals(true) && CheckedCount == 0)
            {
                strSql += $@"Where [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.ReasonCode)}] = '{PublicForms.DeleteScheduleRecord.Cob_DeleteCode.SelectedValue}'";
                CheckedCount += 1;
            }

            if (PublicForms.DeleteScheduleRecord.Chk_Time.Checked.Equals(true) && CheckedCount > 0)
            {
                strSql += $@"And [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CreateTime)}] Between '{PublicForms.DeleteScheduleRecord.Dtp_Start_Time.Value:yyyy-MM-dd HH}:00:00' and '{PublicForms.DeleteScheduleRecord.Dtp_Finish_Time.Value:yyyy-MM-dd HH}:59:59'";
                CheckedCount += 1;
            }
            else if (PublicForms.DeleteScheduleRecord.Chk_Time.Checked.Equals(true) && CheckedCount == 0)
            {
                strSql += $@"Where [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CreateTime)}] Between '{PublicForms.DeleteScheduleRecord.Dtp_Start_Time.Value:yyyy-MM-dd HH}:00:00' and '{PublicForms.DeleteScheduleRecord.Dtp_Finish_Time.Value:yyyy-MM-dd HH}:59:59'";
                CheckedCount += 1;
            }

            if (PublicForms.DeleteScheduleRecord.Chk_Remarks_Type.Checked.Equals(true) && CheckedCount > 0)
            {
                strSql += $@"And [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Remarks)}] = '{PublicForms.DeleteScheduleRecord.Cob_Remarks_Type.Text}'";
                CheckedCount += 1;
            }
            else if (PublicForms.DeleteScheduleRecord.Chk_Remarks_Type.Checked.Equals(true) && CheckedCount == 0)
            {
                strSql += $@"Where [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Remarks)}] = '{PublicForms.DeleteScheduleRecord.Cob_Remarks_Type.Text}'";
                CheckedCount += 1;
            }
            //if (PublicForms.DeleteScheduleRecord.Chk_User.Checked.Equals(true) && CheckedCount > 0)
            //{
            //    strSql += $@"And [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.OperatorId)}] = '{PublicForms.DeleteScheduleRecord.Cob_User.Text}'";
            //    CheckedCount += 1;
            //}
            //else if (PublicForms.DeleteScheduleRecord.Chk_User.Checked.Equals(true) && CheckedCount == 0)
            //{
            //    strSql += $@"Where [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.OperatorId)}] = '{PublicForms.DeleteScheduleRecord.Cob_User.Text}'";
            //    CheckedCount += 1;
            //}

            strSql += $@"Order by [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CreateTime)}] desc";

            return strSql;
        }
        public static string Frm_1_3_UpdateSpare()
        {
            string strSql = $@"Update [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete)}] Set
                                      [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Remarks)}] = N'{PublicForms.DeleteScheduleRecord.Txt_Spare.Text}'
                                Where [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CoilNo)}] = '{PublicForms.DeleteScheduleRecord.Txt_Coil.Text}'";
            return strSql;
        }
        #endregion

        #region Frm_2_1

        /// <summary>
        /// 搜尋整個TrackingMap
        /// </summary>
        /// <returns></returns>
        public static string Frm_2_1_SelectMap_DB_TrackingMap()
        {
            string strSql = "";
            strSql = $"select * from [{nameof(CoilMapEntity.TBL_CoilMap)}]";
            return strSql;
        }
        /// <summary>
        /// 搜尋入口段鋼卷頭/尾段導帶及掃描紀錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_2_1_SelectEntrySkidCoilInfo_DB_PDI(string Coil_ID)
        {
            string strSql = "";
            #region SQL
            strSql = $@"select 
                            [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}],
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_Attached)}],
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_St_No)}],
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_Length)}],
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_Width)}],
                            [{nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness)}],
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached)}],
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No)}],
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Length)}],
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Width)}],
                            [{nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness)}],
                            [{nameof(PDIEntity.TBL_PDI.Entry_Scaned_CoilID)}],
                            [{nameof(PDIEntity.TBL_PDI.Entry_Scaned_UserID)}],
                            [{nameof(PDIEntity.TBL_PDI.Entry_Scaned_Time)}],
                            [{nameof(PDIEntity.TBL_PDI.Origin_CoilID)}],
                            [{nameof(PDIEntity.TBL_PDI.Entry_CoilID_Checked)}]
                        FROM [{nameof(PDIEntity.TBL_PDI)}]
                        Where  [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] ='{Coil_ID}'";
            #endregion
            return strSql;
        }
        /// <summary>
        /// 搜尋出口段掃描及PDO上傳紀錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_2_1_SelectDeliverySkidCoilInfo_DB_PDO(string Coil_ID)
        {
            string strSql = "";
            strSql = $@" Select 
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}],
                            [{nameof(PDOEntity.TBL_PDO.Coil_Check_Result)}],
                            [{nameof(PDOEntity.TBL_PDO.Exit_Scaned_CoilID)}],
                            [{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}]
                         FROM [{nameof(PDOEntity.TBL_PDO)}] 
                         Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] ='{Coil_ID}'";
            return strSql;
        }
        /// <summary>
        /// 未上線鋼卷
        /// </summary>
        /// <returns></returns>
        public static string Frm_2_1_SelectScheduleTop10_CoilInfo_DB_Schedule_PDI()
        {
            string strSql = "";
            #region SQL
            strSql = $@" select TOP 10 
                            a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule .Coil_ID)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Width)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Wt)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Length)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Paper_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Head_Paper_Length)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Head_Paper_Width)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)}],
                            b.[{nameof(PDIEntity.TBL_PDI.St_No)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Density)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Repair_Type)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Uncoil_Direction)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Paper_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Pack_Mode)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Origin_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Width)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Scraped_Weight)}]
                   From [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] a  
                   LEFT JOIN [{nameof(PDIEntity.TBL_PDI)}] b on a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] 
                   Order by a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}] asc";
            #endregion
            return strSql;
        }
        /// <summary>
        /// 線上鋼卷
        /// </summary>
        /// <returns></returns>
        public static string Frm_2_1_TrackingCoilInfo_DB_Map_PDI()
        {
            string strSql = "";
            #region SQL
            strSql = $@" select  
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Width)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Wt)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Length)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Paper_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Head_Paper_Length)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Head_Paper_Width)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)}],
                            b.[{nameof(PDIEntity.TBL_PDI.St_No)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Density)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Repair_Type)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Uncoil_Direction)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Paper_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Pack_Mode)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Test_Plan_No)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Origin_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Width)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Order_No)}],
                            b.[{nameof(PDIEntity.TBL_PDI.Scraped_Weight)}]
                    From  [{nameof(CoilMapEntity.TBL_CoilMap)}] a

                    --Left join  [{nameof(PDIEntity.TBL_PDI)}]  b 

					LEFT JOIN (Select * From (
					                         Select *,ROW_NUMBER() Over (Partition By [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] Order By [{nameof(PDIEntity.TBL_PDI.CreateTime)}] Desc) As Sort
					                         From [{nameof(PDIEntity.TBL_PDI)}]) pdi_temp Where pdi_temp.Sort=1
											 ) b  	

                    on b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_SK01)}]
                    or b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_TOP)}] 
                    or b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.POR)}] 
                    or b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK01)}] 
                    or b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK02)}] 
                    or b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_TOP)}] 
                    or b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.TR)}] ";
            #endregion
            return strSql;
        }
        /// <summary>
        /// 自動入料狀態變更
        /// </summary>
        /// <param name="feedStatus"></param>
        /// <returns></returns>
        public static string Frm_2_1_AutoFeedStatus_DB_TBL_SystemSetting(AutoFeedStatus feedStatus)
        {
            string strSql = "";
            strSql = $@"Update [{nameof(SystemSettingEntity.TBL_SystemSetting)}] set 
                               [{nameof(SystemSettingEntity.TBL_SystemSetting.Value)}] = '{(int)feedStatus}'
                     Where [{nameof(SystemSettingEntity.TBL_SystemSetting.Parameter_Group)}] ='GPL'
                     and [{nameof(SystemSettingEntity.TBL_SystemSetting.Parameter)}] = 'AutoCoilFeed'";
            return strSql;
        }
        /// <summary>
        /// 自動入料狀態搜尋
        /// </summary>
        /// <returns></returns>
        public static string Frm_2_1_SelectAutoFeedStatus_DB_TBL_SystemSetting()
        {
            string strSql = "";
            strSql = $@"Select [{nameof(SystemSettingEntity.TBL_SystemSetting.Value)}] 
                       From [{nameof(SystemSettingEntity.TBL_SystemSetting)}] 
                       Where [{nameof(SystemSettingEntity.TBL_SystemSetting.Parameter_Group)}] ='GPL'
                       and [{nameof(SystemSettingEntity.TBL_SystemSetting.Parameter)}] = 'AutoCoilFeed'";
            return strSql;
        }
        /// <summary>
        /// 搜尋退料紀錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_2_1_SelectReturnCoilRecord_DB_TBL_RetrunCoil(string Coil_ID ,string strPlan_No)
        {
            string strSql = "";
            strSql = $@"Select [{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reject_Coil_No)}] From[{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp)}] Where [{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reject_Coil_No)}] = '{Coil_ID }' AND [Plan_No] = '{strPlan_No}'";
            return strSql;
        }
        /// <summary>
        /// 有退料紀錄修改時間
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_2_1_UpdateReturnCoilRecordCreateTime_DB_TBL_RetrunCoil(string Coil_ID, string strPlan_No)
        {
            string strSql = "";
            strSql = $@" UPDATE [{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp)}] SET [{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.CreateTime)}] = '{Instance.time}'
                         Where  [{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reject_Coil_No)}] = '{Coil_ID }' AND [Plan_No] = '{strPlan_No}' ";
            return strSql;
        }
        /// <summary>
        /// 新增退料紀錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_2_1_InsertReturnCoilRecord_DB_TBL_RetrunCoil()
        {
            //string Coil_ID ,string strPlan_No 
            string strSql = "";
            //strSql = $@" INSERT INTO [{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp)}] ([{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reject_Coil_No)}],[{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.CreateTime)}],[Plan_No]) VALUES ('{Coil_ID }','{Instance.time }','{strPlan_No}') ";
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine($" Insert into [{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp)}] ( ");

            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reject_Coil_No)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.OriPDI_Out_Coil_ID)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Entry_CoilNo)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Plan_No)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Mode_Of_Reject)}], ");

            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Length_Of_Rejected_Coil)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Weight_Of_Rejected_Coil)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Inner_Diameter_Of_RejectedCoil)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Outer_Diameter_Of_RejectedCoil)}], ");
           // sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Width_Of_RejectedCoil)}], ");

            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reason_Of_Reject)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Time_Of_Reject)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Shift_Of_Reject)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Turn_Of_Reject)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Paper_exit_Code)}], ");

            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Paper_Type)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Final_Coil_Flag)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Head_Paper_Length)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Head_Paper_Width)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Tail_Paper_Length)}], ");

            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Tail_Paper_Width)}], ");
            //sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reject_Skid)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.UserID)}], ");
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.CreateTime)}] ) ");

            sbSql.AppendLine($"Values(");

            sbSql.AppendLine($" '{PublicForms.DialogReject.Txt_Coil_ID.Text.Trim()}', ");
            sbSql.AppendLine($" '{PublicForms.DialogReject.OriPDI_Out_Coil_ID}', ");         
            sbSql.AppendLine($" '{PublicForms.DialogReject.Txt_Entry_Coil_ID.Text.Trim()}', ");
            sbSql.AppendLine($" '{PublicForms.DialogReject.Txt_Plan_No.Text.Trim()}', ");
            sbSql.AppendLine($" '{PublicForms.DialogReject.Cob_Mode_Of_Reject.SelectedValue}', ");

            sbSql.AppendLine($" '{PublicForms.DialogReject.Txt_Length_Of_Rejected_Coil.Text.Trim()}', ");
            sbSql.AppendLine($" '{PublicForms.DialogReject.Txt_Weight_Of_Rejected_Coil.Text.Trim()}', ");
            sbSql.AppendLine($" '{PublicForms.DialogReject.Txt_Inner_Diameter_Of_RejectedCoil.Text.Trim()}', ");
            sbSql.AppendLine($" '{PublicForms.DialogReject.Txt_Outer_Diameter_Of_RejectedCoil.Text.Trim()}', ");
            //Width_Of_RejectedCoil 文件沒有寬度欄位

            sbSql.AppendLine($" '{PublicForms.DialogReject.Cob_Reason_Of_Reject.SelectedValue}', ");
            sbSql.AppendLine($" '{PublicForms.DialogReject.Dtp_Time_Of_Reject.Value:yyyyMMddHHmmss}', ");
            sbSql.AppendLine($" '{PublicForms.DialogReject.Cob_Shift_Of_Reject.SelectedValue}', ");
            sbSql.AppendLine($" '{PublicForms.DialogReject.Cob_Turn_Of_Reject.SelectedValue}', ");
            sbSql.AppendLine($" '{PublicForms.DialogReject.Cob_Paper_exit_Code.SelectedValue}', ");

            sbSql.AppendLine($" '{PublicForms.DialogReject.Cob_Paper_Type.SelectedValue}', ");
            sbSql.AppendLine($" '{PublicForms.DialogReject.Cob_FINAL_COIL_FLAG.SelectedValue}', ");            
            sbSql.AppendLine($" '{PublicForms.DialogReject.Txt_HEAD_PAPER_LENGTH.Text.Trim()}', ");
            sbSql.AppendLine($" '{PublicForms.DialogReject.Txt_HEAD_PAPER_WIDTH.Text.Trim()}', ");
            sbSql.AppendLine($" '{PublicForms.DialogReject.Txt_TAIL_PAPER_LENGTH.Text.Trim()}', ");

            sbSql.AppendLine($" '{PublicForms.DialogReject.Txt_TAIL_PAPER_WIDTH.Text.Trim()}', ");        
            //sbSql.AppendLine($" '', ");//   { Skid}
            sbSql.AppendLine($" '{PublicForms.Main.lblLoginUser.Text.Trim()}', ");
            sbSql.AppendLine($" '{Instance.time}' ");
            //  sbSql.AppendLine($" '{DateTime.Now:yyyy-MM-dd HH:mm:ss}' ");//Instance.time
            sbSql.AppendLine($" ) ");

            strSql = sbSql.ToString();
            return strSql;
        }


        /// <summary>
        /// 修改退料紀錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_2_1_UpdateReturnCoilRecord_DB_TBL_RetrunCoil(string Coil_ID, string strPlan_No = "", string strTime_Of_Reject = "")
        {

            string strSql = "";
            //strSql = $@" INSERT INTO [{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp)}] ([{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reject_Coil_No)}],[{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.CreateTime)}],[Plan_No]) VALUES ('{Coil_ID }','{Instance.time }','{strPlan_No}') ";
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine($" Update [{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp)}] set ");

            //sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reject_Coil_No)}] = '{PublicForms.DialogReject.Txt_Coil_ID.Text.Trim()}'");//主Key
            sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.OriPDI_Out_Coil_ID)}] = '{PublicForms.DialogReject.OriPDI_Out_Coil_ID}' ");
            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Entry_CoilNo)}] = '{PublicForms.DialogReject.Txt_Entry_Coil_ID.Text.Trim()}' ");

            if (string.IsNullOrEmpty(strPlan_No))
                sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Plan_No)}] =  '{PublicForms.DialogReject.Txt_Plan_No.Text.Trim()}' ");

            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Mode_Of_Reject)}] = '{PublicForms.DialogReject.Cob_Mode_Of_Reject.SelectedValue}' ");

            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Length_Of_Rejected_Coil)}] = '{PublicForms.DialogReject.Txt_Length_Of_Rejected_Coil.Text.Trim()}' ");
            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Weight_Of_Rejected_Coil)}] = '{PublicForms.DialogReject.Txt_Weight_Of_Rejected_Coil.Text.Trim()}' ");
            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Inner_Diameter_Of_RejectedCoil)}] = '{PublicForms.DialogReject.Txt_Inner_Diameter_Of_RejectedCoil.Text.Trim()}' ");
            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Outer_Diameter_Of_RejectedCoil)}] = '{PublicForms.DialogReject.Txt_Outer_Diameter_Of_RejectedCoil.Text.Trim()}' ");
            //sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Width_Of_RejectedCoil)}], "); //Width_Of_RejectedCoil 文件沒有寬度欄位

            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reason_Of_Reject)}] =  '{PublicForms.DialogReject.Cob_Reason_Of_Reject.SelectedValue}' ");

            //if (string.IsNullOrEmpty(strTime_Of_Reject))
                sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Time_Of_Reject)}] = '{PublicForms.DialogReject.Dtp_Time_Of_Reject.Value:yyyyMMddHHmmss}' ");//主Key

            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Shift_Of_Reject)}] = '{PublicForms.DialogReject.Cob_Shift_Of_Reject.SelectedValue}' ");
            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Turn_Of_Reject)}] =  '{PublicForms.DialogReject.Cob_Turn_Of_Reject.SelectedValue}'");
            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Paper_exit_Code)}] = '{PublicForms.DialogReject.Cob_Paper_exit_Code.SelectedValue}' ");

            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Paper_Type)}] = '{PublicForms.DialogReject.Cob_Paper_Type.SelectedValue}' ");
            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Final_Coil_Flag)}] = '{PublicForms.DialogReject.Cob_FINAL_COIL_FLAG.SelectedValue}'");
            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Head_Paper_Length)}] = '{PublicForms.DialogReject.Txt_HEAD_PAPER_LENGTH.Text.Trim()}' ");
            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Head_Paper_Width)}] = '{PublicForms.DialogReject.Txt_HEAD_PAPER_WIDTH.Text.Trim()}' ");
            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Tail_Paper_Length)}] = '{PublicForms.DialogReject.Txt_TAIL_PAPER_LENGTH.Text.Trim()}' ");

            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Tail_Paper_Width)}] = '{PublicForms.DialogReject.Txt_TAIL_PAPER_WIDTH.Text.Trim()}'");
            //sbSql.AppendLine($" [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reject_Skid)}] =  "); // 文件沒有寬度欄位
            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.UserID)}] = '{PublicForms.Main.lblLoginUser.Text.Trim()}' ");
            sbSql.AppendLine($" ,[{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.CreateTime)}] =  '{DateTime.Now:yyyy-MM-dd HH:mm:ss}' ");

            sbSql.AppendLine($" WHERE [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reject_Coil_No)}] = '{Coil_ID}'");

            if(!string.IsNullOrEmpty(strPlan_No))
                sbSql.AppendLine($" AND  [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Plan_No)}] =  '{strPlan_No}'");

            //if (!string.IsNullOrEmpty(strTime_Of_Reject))
            //    sbSql.AppendLine($" AND [{ nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Time_Of_Reject)}] = '{PublicForms.DialogReject.Dtp_Time_Of_Reject.Value:yyyyMMddHHmmss}' ");

            //sbSql.AppendLine($"");
            strSql = sbSql.ToString();
            return strSql;
        }
       
        /// <summary>
        /// 新增PDI缺陷
        /// </summary>
        /// <returns></returns>
        public static string Frm_2_1_InsertReturnCoilRecord_DB_PDI_Defect()
        {
            string strSql = "";
            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine($" Insert into [{nameof(PDIEntity.TBL_PDI)}] ( ");
            sbSql.AppendLine($" [{ nameof(PDIEntity.TBL_PDI.In_Coil_ID)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.Plan_No)}]  ");
            #region 代码
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Code)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Code)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Code)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Code)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Code)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Code)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Code)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Code)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Code)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Code)}]  ");
            #endregion
            #region 来源             
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Origin)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Origin)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Origin)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Origin)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Origin)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Origin)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Origin)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Origin)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Origin)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Origin)}]");
            #endregion
            #region 表面区分              
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Sid)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Sid)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Sid)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Sid)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Sid)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Sid)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Sid)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Sid)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Sid)}]  ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Sid)}]  ");
            #endregion
            #region 宽向位置               
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Pos_W)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Pos_W)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Pos_W)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Pos_W)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Pos_W)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Pos_W)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Pos_W)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Pos_W)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Pos_W)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Pos_W)}]");
            #endregion
            #region 长向开始位置               
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start)}]");
            #endregion
            #region 长向结束位置             
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Pos_L_End)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Pos_L_End)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Pos_L_End)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Pos_L_End)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Pos_L_End)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Pos_L_End)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Pos_L_End)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Pos_L_End)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Pos_L_End)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Pos_L_End)}]");
            #endregion
            #region 程度              
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Level)}] ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Level)}] ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Level)}] ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Level)}] ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Level)}] ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Level)}] ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Level)}] ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Level)}] ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Level)}] ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Level)}] ");
            #endregion
            #region 比例               
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Percent)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Percent)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Percent)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Percent)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Percent)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Percent)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Percent)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Percent)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Percent)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Percent)}]");
            #endregion
            #region 质量等级              
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_QGrade)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_QGrade)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_QGrade)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_QGrade)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_QGrade)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_QGrade)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_QGrade)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_QGrade)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_QGrade)}]");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_QGrade)}]");
            #endregion
            sbSql.AppendLine($" ) ");
            sbSql.AppendLine($"Values(");
            sbSql.AppendLine($" '{DialogReject.Txt_Entry_Coil_ID.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.Txt_Plan_No.Text}'  ");
            #region 代码
            sbSql.AppendLine($" ,'{DialogReject.txtCode_D01.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtCode_D02.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtCode_D03.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtCode_D04.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtCode_D05.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtCode_D06.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtCode_D07.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtCode_D08.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtCode_D09.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtCode_D10.Text}' ");
            #endregion
            #region 来源
            sbSql.AppendLine($" ,'{DialogReject.txtOrigin_D01.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtOrigin_D02.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtOrigin_D03.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtOrigin_D04.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtOrigin_D05.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtOrigin_D06.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtOrigin_D07.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtOrigin_D08.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtOrigin_D09.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtOrigin_D10.Text}'");
            #endregion
            #region 表面区分
            sbSql.AppendLine($" ,'{DialogReject.cbo_Sid_D01.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Sid_D02.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Sid_D03.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Sid_D04.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Sid_D05.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Sid_D06.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Sid_D07.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Sid_D08.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Sid_D09.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Sid_D10.Text}' ");
            #endregion
            #region 宽向位置
            sbSql.AppendLine($" ,'{DialogReject.cbo_PosW_D01.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_PosW_D02.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_PosW_D03.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_PosW_D04.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_PosW_D05.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_PosW_D06.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_PosW_D07.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_PosW_D08.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_PosW_D09.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_PosW_D10.Text}' ");
            #endregion
            #region 长向开始位置
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_Start_D01.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_Start_D02.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_Start_D03.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_Start_D04.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_Start_D05.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_Start_D06.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_Start_D07.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_Start_D08.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_Start_D09.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_Start_D10.Text}' ");
            #endregion
            #region 长向结束位置
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_End_D01.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_End_D02.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_End_D03.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_End_D04.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_End_D05.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_End_D06.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_End_D07.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_End_D08.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_End_D09.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPos_L_End_D10.Text}' ");
            #endregion
            #region 程度
            sbSql.AppendLine($" ,'{DialogReject.cbo_Level_D01.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Level_D02.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Level_D03.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Level_D04.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Level_D05.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Level_D06.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Level_D07.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Level_D08.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Level_D09.Text}' ");
            sbSql.AppendLine($" ,'{DialogReject.cbo_Level_D10.Text}' ");
            #endregion
            #region 比例
            sbSql.AppendLine($" ,'{DialogReject.txtPercent_D01.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPercent_D02.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPercent_D03.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPercent_D04.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPercent_D05.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPercent_D06.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPercent_D07.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPercent_D08.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPercent_D09.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtPercent_D10.Text}' ");
            #endregion
            #region 质量等级
            sbSql.AppendLine($" ,'{DialogReject.txtQGRADE_D01.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtQGRADE_D02.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtQGRADE_D03.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtQGRADE_D04.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtQGRADE_D05.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtQGRADE_D06.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtQGRADE_D07.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtQGRADE_D08.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtQGRADE_D09.Text}'  ");
            sbSql.AppendLine($" ,'{DialogReject.txtQGRADE_D10.Text}' ");
            #endregion
            sbSql.AppendLine($" ) ");
            strSql = sbSql.ToString();
            return strSql;
        }


        /// <summary>
        /// 新增PDI缺陷
        /// </summary>
        /// <returns></returns>
        public static string Frm_2_1_UpdateReturnCoilRecord_DB_PDI_Defect(string Coil_ID, string strPlan_No = "")
        {
            string strSql = "";
            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine($" Update [{nameof(PDIEntity.TBL_PDI)}] set ");
         
            #region 代码
            sbSql.AppendLine($" [{ nameof(PDIEntity.TBL_PDI.D01_Code)}] = '{DialogReject.txtCode_D01.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Code)}] = '{DialogReject.txtCode_D02.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Code)}] = '{DialogReject.txtCode_D03.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Code)}] = '{DialogReject.txtCode_D04.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Code)}] = '{DialogReject.txtCode_D05.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Code)}] = '{DialogReject.txtCode_D06.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Code)}] = '{DialogReject.txtCode_D07.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Code)}] = '{DialogReject.txtCode_D08.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Code)}] = '{DialogReject.txtCode_D09.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Code)}] = '{DialogReject.txtCode_D10.Text}'");
            #endregion
            #region 来源             
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Origin)}] = '{DialogReject.txtOrigin_D01.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Origin)}] = '{DialogReject.txtOrigin_D02.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Origin)}] = '{DialogReject.txtOrigin_D03.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Origin)}] = '{DialogReject.txtOrigin_D04.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Origin)}] = '{DialogReject.txtOrigin_D05.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Origin)}] = '{DialogReject.txtOrigin_D06.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Origin)}] = '{DialogReject.txtOrigin_D07.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Origin)}] = '{DialogReject.txtOrigin_D08.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Origin)}] = '{DialogReject.txtOrigin_D09.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Origin)}] = '{DialogReject.txtOrigin_D10.Text}'");
            #endregion
            #region 表面区分              
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Sid)}] = '{DialogReject.cbo_Sid_D01.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Sid)}] = '{DialogReject.cbo_Sid_D02.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Sid)}] = '{DialogReject.cbo_Sid_D03.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Sid)}] = '{DialogReject.cbo_Sid_D04.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Sid)}] = '{DialogReject.cbo_Sid_D05.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Sid)}] = '{DialogReject.cbo_Sid_D06.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Sid)}] = '{DialogReject.cbo_Sid_D07.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Sid)}] = '{DialogReject.cbo_Sid_D08.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Sid)}] = '{DialogReject.cbo_Sid_D09.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Sid)}] = '{DialogReject.cbo_Sid_D10.Text}' ");
            #endregion
            #region 宽向位置               
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Pos_W)}] = '{DialogReject.cbo_PosW_D01.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Pos_W)}] = '{DialogReject.cbo_PosW_D02.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Pos_W)}] = '{DialogReject.cbo_PosW_D03.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Pos_W)}] = '{DialogReject.cbo_PosW_D04.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Pos_W)}] = '{DialogReject.cbo_PosW_D05.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Pos_W)}] = '{DialogReject.cbo_PosW_D06.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Pos_W)}] = '{DialogReject.cbo_PosW_D07.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Pos_W)}] = '{DialogReject.cbo_PosW_D08.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Pos_W)}] = '{DialogReject.cbo_PosW_D09.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Pos_W)}] = '{DialogReject.cbo_PosW_D10.Text}'");
            #endregion
            #region 长向开始位置               
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start)}] = '{DialogReject.txtPos_L_Start_D01.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start)}] = '{DialogReject.txtPos_L_Start_D02.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start)}] = '{DialogReject.txtPos_L_Start_D03.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start)}] = '{DialogReject.txtPos_L_Start_D04.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start)}] = '{DialogReject.txtPos_L_Start_D05.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start)}] = '{DialogReject.txtPos_L_Start_D06.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start)}] = '{DialogReject.txtPos_L_Start_D07.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start)}] = '{DialogReject.txtPos_L_Start_D08.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start)}] = '{DialogReject.txtPos_L_Start_D09.Text}'");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start)}] = '{DialogReject.txtPos_L_Start_D10.Text}'");
            #endregion
            #region 长向结束位置             
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Pos_L_End)}] = '{DialogReject.txtPos_L_End_D01.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Pos_L_End)}] = '{DialogReject.txtPos_L_End_D02.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Pos_L_End)}] = '{DialogReject.txtPos_L_End_D03.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Pos_L_End)}] = '{DialogReject.txtPos_L_End_D04.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Pos_L_End)}] = '{DialogReject.txtPos_L_End_D05.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Pos_L_End)}] = '{DialogReject.txtPos_L_End_D06.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Pos_L_End)}] = '{DialogReject.txtPos_L_End_D07.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Pos_L_End)}] = '{DialogReject.txtPos_L_End_D08.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Pos_L_End)}] = '{DialogReject.txtPos_L_End_D09.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Pos_L_End)}] = '{DialogReject.txtPos_L_End_D10.Text}' ");
            #endregion
            #region 程度              
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Level)}] = '{DialogReject.cbo_Level_D01.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Level)}] = '{DialogReject.cbo_Level_D02.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Level)}] = '{DialogReject.cbo_Level_D03.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Level)}] = '{DialogReject.cbo_Level_D04.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Level)}] = '{DialogReject.cbo_Level_D05.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Level)}] = '{DialogReject.cbo_Level_D06.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Level)}] = '{DialogReject.cbo_Level_D07.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Level)}] = '{DialogReject.cbo_Level_D08.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Level)}] = '{DialogReject.cbo_Level_D09.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Level)}] = '{DialogReject.cbo_Level_D10.Text}' ");
            #endregion
            #region 比例               
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_Percent)}] = '{DialogReject.txtPercent_D01.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_Percent)}] = '{DialogReject.txtPercent_D02.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_Percent)}] = '{DialogReject.txtPercent_D03.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_Percent)}] = '{DialogReject.txtPercent_D04.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_Percent)}] = '{DialogReject.txtPercent_D05.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_Percent)}] = '{DialogReject.txtPercent_D06.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_Percent)}] = '{DialogReject.txtPercent_D07.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_Percent)}] = '{DialogReject.txtPercent_D08.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_Percent)}] = '{DialogReject.txtPercent_D09.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_Percent)}] = '{DialogReject.txtPercent_D10.Text}' ");
            #endregion
            #region 质量等级              
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D01_QGrade)}] = '{DialogReject.txtQGRADE_D01.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D02_QGrade)}] = '{DialogReject.txtQGRADE_D02.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D03_QGrade)}] = '{DialogReject.txtQGRADE_D03.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D04_QGrade)}] = '{DialogReject.txtQGRADE_D04.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D05_QGrade)}] = '{DialogReject.txtQGRADE_D05.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D06_QGrade)}] = '{DialogReject.txtQGRADE_D06.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D07_QGrade)}] = '{DialogReject.txtQGRADE_D07.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D08_QGrade)}] = '{DialogReject.txtQGRADE_D08.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D09_QGrade)}] = '{DialogReject.txtQGRADE_D09.Text}' ");
            sbSql.AppendLine($" ,[{ nameof(PDIEntity.TBL_PDI.D10_QGrade)}] = '{DialogReject.txtQGRADE_D10.Text}' ");
            #endregion          
    
            sbSql.AppendLine($" WHERE [{ nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{Coil_ID}' ");

            if(!string.IsNullOrEmpty(strPlan_No))
                sbSql.AppendLine($" AND [{ nameof(PDIEntity.TBL_PDI.Plan_No)}] = '{strPlan_No}' ");
            strSql = sbSql.ToString();
            return strSql;
        }


        /// <summary>
        /// 搜尋退料鋼卷PDI
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_2_1_SelectReturnCoilPDI_DB_PDI(string Coil_ID)
        {
            string strSql = "";
            strSql = $@"Select *
                        From [{nameof(PDIEntity.TBL_PDI)}]
                        Where [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{Coil_ID}'";
            return strSql;
        }
        /// <summary>
        /// 檢查退料紀錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_2_1_CheckRejectCoil_DB_TBL_CoilRejectResult(string Coil_ID)
        {
            string strSql = "";
            strSql = $@"Select [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Reject_Coil_No)}] 
                        From [{nameof(CoilRejResultEntity.TBL_CoilRejectResult)}] 
                        Where [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Reject_Coil_No)}] = '{Coil_ID}'";
            return strSql;
        }

        /// <summary>
        /// 搜尋退料暫存紀錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_2_1_Select_CoilRejectTemp(string Coil_ID, string strPlan_No = "")
        {
            //不設主鍵,以 [Coil_ID] 最新一筆[CreateTime] 為主TBL_ScheduleDelete_CoilReject_Temp
            string strSql = $@"Select * 
                               From [{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp)}] 
                              Where [{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reject_Coil_No)}] = '{Coil_ID }' ";

            if(!string.IsNullOrEmpty(strPlan_No))
                strSql += $@"AND [{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Plan_No)}] = '{strPlan_No}' ";

            strSql += $@" Order by [{nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.CreateTime)}] DESC ";

            return strSql;
        }

        /// <summary>
        /// 斷帶鋼卷資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_2_1_CheckStripBrakeRecord(string Coil_ID)
        {
            string strSql = $@"Select Unm.*,
                                      pdi.*
                                 From [{nameof(TBL_UnmountRecord)}] Unm
                            Left Join [{nameof(PDIEntity.TBL_PDI)}] pdi 
                                   On Unm.[{nameof(TBL_UnmountRecord.CoilID)}] = pdi.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}]
                                Where Unm.[{nameof(TBL_UnmountRecord.CoilID)}] = '{Coil_ID}'";
            return strSql;
        }
      
        /// <summary>
        /// 新增ScheduleDelete_CoilReject_Record紀錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <param name="GroupNo"></param>
        /// <returns></returns>
        public static string Frm_2_1_InsertCoilReject_Record_DB_L3L2_TBL_ScheduleDelete_CoilReject_Record(string Coil_ID, string GroupNo)
        {
            string strSql = "";
            //strSql = $@"Insert into [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record)}] 
            //                   ([{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record.Coil_ID)}],
            //                    [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record.ScheduleDelete_CoilReject_GroupNo)}],
            //                    [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record.ScheduleDelete_CoilReject_Code)}],
            //                    [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record.Remarks)}],
            //                    [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record.Create_UserID)}],
            //                    [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_Record.CreateTime)}]) 
            //            VALUES ('{Coil_ID}',{GroupNo},{Tracking.cbo_ReturnCode.SelectedValue},
            //                    '{Tracking.txtRemark.Text }','{Main.lblLoginUser.Text}','{Instance.time}') ";
            return strSql;
        }

        public static string Frm_2_1_InsertSplitCoils(string Coil_ID,string ParentCoil_ID)
        {
            string strSql = $@"Insert into [{nameof(SplitCoilsEntity.TBL_SplitCoils)}]
                                          ([{nameof(SplitCoilsEntity.TBL_SplitCoils.Coil_ID)}],
                                           [{nameof(SplitCoilsEntity.TBL_SplitCoils.ParentCoil_ID)}])
                                    Values('{Coil_ID}','{ParentCoil_ID}')";
            return strSql;
        }

        /// <summary>
        /// 新增CoilRejectResult紀錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_2_1_InsertRejectResult_DB_TBL_CoilRejectResult(string Coil_ID)
        {
            string strSql = "";
            #region SQL
            //strSql = $@"Insert into [{nameof(CoilRejResultEntity.TBL_CoilRejectResult)}]
            //                   ([{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Reject_Coil_No)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Entry_CoilNo)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Plan_No)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Mode_Of_Reject)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Length_Of_Rejected_Coil)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Weight_Of_Rejected_Coil)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Inner_Diameter_Of_RejectedCoil)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Outer_Diameter_Of_RejectedCoil)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Reason_Of_Reject)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Time_Of_Reject)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Shift_Of_Reject)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Turn_Of_Reject)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Paper_exit_Code)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Paper_Type)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.CreateTime)}],
            //                    [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.UserID)}])
            //            VALUES ('{Coil_ID}','{Coil_ID}','{Tracking.txtPlan_NO.Text}',
            //                   '{Tracking.cbo_Mode_Reject.SelectedValue}',
            //                   '{Tracking.txtRejectLen.Text}',
            //                   '{Tracking.txtRejectWt.Text}',
            //                   '{Tracking.txtRejectInner.Text}',
            //                   '{Tracking.txtRejectOuter.Text}',
            //                   '{Tracking.cbo_ReturnCode.SelectedValue}',
            //                   '{Instance.time_CHAR14}',
            //                   '{Tracking.cbo_Shift.SelectedValue}',
            //                   '{Tracking.cbo_Team.SelectedValue}',
            //                   '{Tracking.cbo_paper_exit_code.SelectedValue}',
            //                   '{Tracking.cbo_paper_exit_type.SelectedValue}',
            //                   '{Instance.time}',
            //                   '{Main.lblLoginUser.Text}')";
            #endregion
            return strSql;
        }

        /// <summary>
        /// 判斷PDO Table有沒有這筆鋼卷
        /// </summary>
        /// <returns></returns>
        public static string Frm_2_1_CheckPDO_CoilID_DB_PDO(string Coil_ID)
        {
            string strSql = "";
            strSql = $@"Select [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}]
                        From [{nameof(PDOEntity.TBL_PDO)}]
                        Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] ='{Coil_ID}'";
            return strSql;
        }

        /// <summary>
        /// 輸入毛重
        /// </summary>
        /// <returns></returns>
        public static string Frm_2_1_CoilWt_DB_TBL_PDO()
        {
            string strSql = "";
            strSql = $@"Update [{nameof(PDOEntity.TBL_PDO)}] set 
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Act_WT)}] = '{Tracking.Txt_Wt.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT_Time)}]='{Instance.time}' 
                        Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}]='{Tracking.Lbl_DSK02_CoilNo.Text}'";
            return strSql;
        }

        /// <summary>
        /// 鞍座右鍵選單-要求PDI檢查
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_2_1_CheckPDI_DB_PDI(string Coil_ID)
        {
            string strSql = "";
            strSql = $@"Select [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] From [{nameof(PDIEntity.TBL_PDI)}] Where [{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}]='{Coil_ID}'";
            return strSql;
        }

        /// <summary>
        /// 收產線速度/張力/產線生產方向(0:暫停 1:FWD(正轉) 2:REV(反轉))
        /// </summary>
        /// <returns></returns>
        public static string Frm_2_1_PROCESS_DATA_DB_TBL_ProcessData()
        {
            string strSql = "";
            strSql = $@"Select Top 1
                            [{nameof(ProcessDataEntity.TBL_ProcessData.Line_Speed)}],
                            [{nameof(ProcessDataEntity.TBL_ProcessData.Line_Tension)}],
                            [{nameof(ProcessDataEntity.TBL_ProcessData.Line_run_direction)}]
                       From [{nameof(ProcessDataEntity.TBL_ProcessData)}]
                       Order by [{nameof(ProcessDataEntity.TBL_ProcessData.Receive_Time)}] desc";
            return strSql;
        }

        /// <summary>
        /// L1每五秒上傳107給L2
        /// 107會存入 [TBL_GrindRecords]
        /// </summary>
        /// <returns></returns>
        public static string Frm_2_1_SelectGrindRecords_DB_TBL_GrindRecords()
        {
            string strSql = "";
            strSql = $@"Select top 1 * 
                        From [{nameof(GrindRecordsEntity.TBL_GrindRecords)}] 
                        order by [{nameof(GrindRecordsEntity.TBL_GrindRecords.Receive_Time)}] desc";
            return strSql;
        }

        public static string Frm_2_1_SelectGrindRecords_DB_TBL_GrindPlanHistory(string Coil_ID,string Section)
        {
            string strSql = "";
            strSql = $@"Select top 1 [{nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory.PassNumber)}]
                          From [{nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory)}] 
                         Where [{nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory.Coil_ID)}] = '{Coil_ID}' 
                    
                         Order by [{nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory.CreateTime)}] desc";
            //       and [{nameof(GrindPlanHistoryModel.TBL_GrindPlanHistory.Pass_Section)}] = '{Section}'
            return strSql;
        }


        /// <summary>
        /// 皮帶資訊
        /// </summary>
        /// <returns></returns>
        public static string Frm_2_1_SelectGR_Belt_DB_TBL_Belt()
        {
            string strSql = "";
            strSql = $@"Select [{nameof(BeltAccEntity.TBL_Belts.Belt_No)}],[{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Belt)}],[{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Strip)}]  
                        From [{nameof(BeltAccEntity.TBL_Belts)}] 
                        Where [{nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)}] > 0
                        Order by [{nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)}] asc";
            return strSql;
        }

        public static string Frm_2_1_DeleteSkidCoil_DB_TBL_CoilMap(string Coil_ID,string Skid)
        {
            string strSql = $@"Update [{nameof(TBL_CoilMap)}] Set [{Skid}] = '' Where [{Skid}] = '{Coil_ID}'";
            return strSql;
        }
        #endregion

        #region Frm_Entry
        public static string Frm_Entry_CoilList_DB_Schedule_PDI()
        {
            string strSql = "";
            strSql = $@"Select 
                            a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Width)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Wt)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Length)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)}],
                            b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)}],
                            b.[{nameof(PDIEntity.TBL_PDI.St_No)}],                                                                      
                            b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}]
                       From [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] a
                       LEFT JOIN [{nameof(PDIEntity.TBL_PDI)}] b on a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] ";
            return strSql;
        }

        public static string Frm_Entry_UpdateMap_DB_TrackingMap(string Skid,string Coil_ID)
        {
            string strSql = "";
            strSql = $@"Update [{nameof(CoilMapEntity.TBL_CoilMap)}] set [{Skid}] = '{Coil_ID}'";
            return strSql;
        }
        #endregion

        #region Frm_2_2
        /// <summary>
        /// Coil List For PDO
        /// Timement : From Load
        /// </summary>
        /// <returns></returns>
        public static string Frm_2_2_FrmLoadDGV_DB_PDO()
        {
            string strSql = "";
            strSql = $@"Select  b.[{nameof(PDIEntity.TBL_PDI.Order_Cust_Code)}],
                                b.[{nameof(PDOEntity.TBL_PDO.Plan_No)}],
                                b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}],
                                b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}],
              Convert(char(19), a.[{nameof(PDOEntity.TBL_PDO.Start_Time)}], 120) Start_Time,
              Convert(char(19), a.[{nameof(PDOEntity.TBL_PDO.Finish_Time)}], 120) Finish_Time,
                                b.[{nameof(PDIEntity.TBL_PDI.St_No)}],
                                b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Length)}],
                                b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Width)}],
                                b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Length)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C40_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C40_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C40_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C25_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C25_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C25_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Act_WT)}] 
                         From [{nameof(PDOEntity.TBL_PDO)}] a 
                   Right join [{nameof(PDIEntity.TBL_PDI)}] b 
                         on b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}] = a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}]";
            return strSql;
        }
        public static string frm_2_2_SelectDGV_DB_PDO()
        {
            string strSql = "";
            strSql = $@"Select b.[{nameof(PDIEntity.TBL_PDI.Order_Cust_Code)}],
                                b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}],
                                b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}],
                   Convert(char(19), a.[{nameof(PDOEntity.TBL_PDO.Start_Time)}], 120) Start_Time,
                   Convert(char(19), a.[{nameof(PDOEntity.TBL_PDO.Finish_Time)}], 120) Finish_Time,
                                b.[{nameof(PDIEntity.TBL_PDI.St_No)}],
                                b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Length)}],
                                b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Width)}],
                                b.[{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Length)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C40_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C40_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C40_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C25_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C25_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C25_Thick)}],
                                a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Act_WT)}] 
                         From [{nameof(PDOEntity.TBL_PDO)}] a Right join [{nameof(PDIEntity.TBL_PDI)}] b on b.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = a.[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}]
                          ";
            //AND b.[{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}] = a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}]

            strSql += $" Where a.[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] is not null ";
            //if (GPLSetupHistory.rdbCoilNo.Checked && GPLSetupHistory.Cbo_CoilID.Text != "")
            //{
            //    strSql += $"a.[{nameof(PDOModel.L2L3_PDO.Out_Mat_No)}] = '{GPLSetupHistory.Cbo_CoilID.Text}'";
            //    if (GPLSetupHistory.chkSteelgcode.Checked) strSql += $"and a.[{nameof(PDOModel.L2L3_PDO.St_No)}] = '{GPLSetupHistory.txtSteelgcode.Text}'";
            //    if (GPLSetupHistory.chkWdt.Checked) strSql += $"and a.[{nameof(PDOModel.L2L3_PDO.Out_Mat_Thick)}] = '{GPLSetupHistory.txtWdt.Text}'";
            //}
            //else
            if (GPLSetupHistory.Rdb_DateTime.Checked)
            {
                strSql += $" AND  a.[{nameof(PDOEntity.TBL_PDO.Start_Time)}] >= '{GPLSetupHistory.Dtp_Start_Time.Value:yyyy-MM-dd HH}:00:00'";
                strSql += $" AND  a.[{nameof(PDOEntity.TBL_PDO.Finish_Time)}] <= '{GPLSetupHistory.Dtp_Finish_Time.Value:yyyy-MM-dd HH}:59:59'";

                //if (GPLSetupHistory.chkSteelgcode.Checked) strSql += $"and a.[{nameof(PDOEntity.TBL_PDO.St_No)}] = '{GPLSetupHistory.txtSteelgcode.Text}'";
                //if (GPLSetupHistory.chkWdt.Checked) strSql += $"and a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}] = '{GPLSetupHistory.txtWdt.Text}'";
            }

            if (GPLSetupHistory.Rdb_CoilNo.Checked)
            {
                strSql += $" AND  a.[{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{GPLSetupHistory.Cob_CoilID.Text}'";
            }

            //else if (GPLSetupHistory.chkWdt.Checked && GPLSetupHistory.chkSteelgcode.Checked == false)
            //    strSql += $"a.[{nameof(PDOModel.L2L3_PDO.Out_Mat_Thick)}] = '{GPLSetupHistory.txtWdt.Text}'";

            //else if (GPLSetupHistory.chkSteelgcode.Checked && GPLSetupHistory.chkWdt.Checked)
            //{
            //    strSql += $"a.[{nameof(PDOModel.L2L3_PDO.St_No)}] = '{GPLSetupHistory.txtSteelgcode.Text}'";
            //    strSql += $"and a.[{nameof(PDOModel.L2L3_PDO.Out_Mat_Thick)}] = '{GPLSetupHistory.txtWdt.Text}'";
            //}
            //}

            return strSql;
        }

        public static string Frm_2_2_Select204205(string strInCoilNo,string strDate,string strTime)
        {
            string strSql = "";           

            StringBuilder sb = new StringBuilder();
            //sb.AppendLine($" USE [FUXIN_GPL_HISTORY]");
            //sb.AppendLine($" GO ");
            //sb.AppendLine($" ");

            sb.AppendLine($" SELECT * ");
            sb.AppendLine($" FROM  {nameof(L2L1MsgDBModel.L2L1_204)} ");//{nameof(L2L1MsgDBModel.L2L1_204.Date)}
            sb.AppendLine($" LEFT JOIN  {nameof(L2L1MsgDBModel.L2L1_205)} ON ");

            sb.AppendLine($" {nameof(L2L1MsgDBModel.L2L1_204)}.{nameof(L2L1MsgDBModel.L2L1_204.DateTime)} = {nameof(L2L1MsgDBModel.L2L1_205)}.{nameof(L2L1MsgDBModel.L2L1_205.DateTime)}");
            sb.AppendLine($" AND {nameof(L2L1MsgDBModel.L2L1_204)}.{nameof(L2L1MsgDBModel.L2L1_204.Date)} = {nameof(L2L1MsgDBModel.L2L1_205)}.{nameof(L2L1MsgDBModel.L2L1_205.Date)}");
            sb.AppendLine($" AND {nameof(L2L1MsgDBModel.L2L1_204)}.{nameof(L2L1MsgDBModel.L2L1_204.Time)} = {nameof(L2L1MsgDBModel.L2L1_205)}.{nameof(L2L1MsgDBModel.L2L1_205.Time)}");
            sb.AppendLine($" AND {nameof(L2L1MsgDBModel.L2L1_204)}.{nameof(L2L1MsgDBModel.L2L1_204.PresetPosition)} = {nameof(L2L1MsgDBModel.L2L1_205)}.{nameof(L2L1MsgDBModel.L2L1_205.PresetPosition)}");
            sb.AppendLine($" WHERE {nameof(L2L1MsgDBModel.L2L1_204)}.{nameof(L2L1MsgDBModel.L2L1_204.CoilId)} IS NOT NULL");
            sb.AppendLine($" AND {nameof(L2L1MsgDBModel.L2L1_204)}.{nameof(L2L1MsgDBModel.L2L1_204.CoilId)} = '{strInCoilNo}'");
            //sb.AppendLine($" AND {nameof(L2L1MsgDBModel.L2L1_204)}.{nameof(L2L1MsgDBModel.L2L1_204.Date)} = {strDate}");
            //sb.AppendLine($" AND {nameof(L2L1MsgDBModel.L2L1_204)}.{nameof(L2L1MsgDBModel.L2L1_204.Time)} = {strTime}");
            sb.AppendLine($" AND {nameof(L2L1MsgDBModel.L2L1_204)}.{nameof(L2L1MsgDBModel.L2L1_204.PresetPosition)} = '0'");


            strSql = sb.ToString();
            return strSql;
        }

        public static string Frm_2_2_SelectGrindPlanRecords_DB_TBL_GrindPlan_Records(string Coil_ID)
        {
            string strSql = "";
            strSql = $@"Select * 
                        From [{nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory)}] 
                        Where [{nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory.Coil_ID)}] = '{Coil_ID}'
                        Order by [{nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory.CreateTime)}] desc";
            return strSql;
        }

        public static string Frm_2_2_SelectSectionGrindPlanHistory(string Coil_ID,string Section)
        { 
            string strSql = $@"Select Top 1 *
                                 From [{nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory)}] 
                                Where [{nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory.Coil_ID)}] = '{Coil_ID}'
                                  And [{nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory.Pass_Section)}] = '{Section}' 
                                Order By [{nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory.CreateTime)}] desc";
            return strSql;
        }

        public static string Frm_2_2_SelectPassFromTo_DB_TBL_BeltPattern(string BeltPattern)
        {
            string strSql = "";
            strSql = $@"Select distinct [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}],[{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}]
                        From [{nameof(BeltPatternsEntity.TBL_BeltPatterns)}]
                        Where [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{BeltPattern}'";
            return strSql;
        }
        public static string Frm_2_2_SelectBeltPatternsRecords_DB_TBL_BeltPatterns_Records(string BeltPattern,string PassFrom, string PassTo,string Coil_ID)
        {
            string strSql = "";
            strSql = $@"Select [{nameof(BeltPatternsRecordEntity.TBL_BeltPatterns_Records.GR_NO)}]
                              ,[{nameof(BeltPatternsRecordEntity.TBL_BeltPatterns_Records.GR_Current)}]
                              ,[{nameof(BeltPatternsRecordEntity.TBL_BeltPatterns_Records.Belt_MaterialCode)}]
                              ,[{nameof(BeltPatternsRecordEntity.TBL_BeltPatterns_Records.Belt_ParticalNumber)}]
                              ,[{nameof(BeltPatternsRecordEntity.TBL_BeltPatterns_Records.Belt_RotateDir)}]
                              ,[{nameof(BeltPatternsRecordEntity.TBL_BeltPatterns_Records.Belt_Speed)}]
                        From [{nameof(BeltPatternsRecordEntity.TBL_BeltPatterns_Records)}]
                        Where [{nameof(BeltPatternsRecordEntity.TBL_BeltPatterns_Records.BeltPattern)}] = '{BeltPattern}'
                        And [{nameof(BeltPatternsRecordEntity.TBL_BeltPatterns_Records.Pass_From)}] = '{PassFrom}'
                        And [{nameof(BeltPatternsRecordEntity.TBL_BeltPatterns_Records.Pass_To)}] = '{PassTo}'
                        And [{nameof(BeltPatternsRecordEntity.TBL_BeltPatterns_Records.Coil_ID)}] = '{Coil_ID}'
                        Order by [{nameof(BeltPatternsRecordEntity.TBL_BeltPatterns_Records.GR_NO)}] asc";
            return strSql;
        }
        public static string Frm_2_2_SelectBeltPatterns_DB_TBL_BeltPatterns(string BeltPattern, string PassFrom, string PassTo)
        {
            string strSql = "";
            strSql = $@"Select [{nameof(TBL_BeltPatterns.GR_NO)}]
                              ,[{nameof(TBL_BeltPatterns.GR_Current)}]
                              ,[{nameof(TBL_BeltPatterns.Belt_MaterialCode)}]
                              ,[{nameof(TBL_BeltPatterns.Belt_ParticalNumber)}]
                              ,[{nameof(TBL_BeltPatterns.Belt_RotateDir)}]
                              ,[{nameof(TBL_BeltPatterns.Belt_Speed)}]
                       From [{nameof(TBL_BeltPatterns)}] 
                      Where [{nameof(TBL_BeltPatterns.BeltPattern)}] = '{BeltPattern}'
                        And [{nameof(TBL_BeltPatterns.Pass_From)}] = '{PassFrom}'
                        And [{nameof(TBL_BeltPatterns.Pass_To)}] = '{PassTo}'
                      Order by [{nameof(TBL_BeltPatterns.GR_NO)}] asc";
            return strSql;
        }


        public static string Frm_2_2_Select_GradeGroups_DB_TBL_GradeGroups(string GradeGroups)
        {
            string strSql = "";
            strSql = $@"Select * From [{nameof(GradeGroupsEntity.TBL_GradeGroups)}] Where [{nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)}] = '{GradeGroups}'";
            return strSql;
        }
        #endregion

        #region Frm_3_1
        /// <summary>
        /// PDO列表
        /// </summary>
        /// <returns></returns>
        public static string Frm_3_1_SelectPDO_DB_PDO()
        {
            string strSql = "";
            strSql = $@"Select * From [{nameof(PDOEntity.TBL_PDO)}] 
                                Where [{nameof(PDOEntity.TBL_PDO.Finish_Time)}] <> '' 
                                   Or [{nameof(PDOEntity.TBL_PDO.Finish_Time)}] <> null";
            return strSql;
        }
        public static string Frm_3_1_SelectComboBoxItems(string Columns, bool bolPDI = false)
        {
            string strSql = "";
            if (bolPDI == false)
            {
                strSql = $@" Select distinct [{Columns}] 
                                From [{nameof(PDOEntity.TBL_PDO)}] 
                                Where [{nameof(PDOEntity.TBL_PDO.Finish_Time)}] <> '' 
                                   Or [{nameof(PDOEntity.TBL_PDO.Finish_Time)}] <> null";
            }
            else if (bolPDI == true)
            {
                strSql = $@"Select distinct pdi.[{Columns}] 
                            From [{nameof(PDOEntity.TBL_PDO)}] pdo Left join [{nameof(PDIEntity.TBL_PDI)}] pdi 
                            On pdo.[{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = pdi.[{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}] ";
            }
            return strSql;
        }

        #endregion

        #region Frm_3_2
        /// <summary>
        /// 出口鋼卷號ComboBox
        /// </summary>
        /// <returns></returns>
        public static string Frm_3_2_CoilIDComboBox_DB_PDO()
        {
            string strSql = "";
            strSql = $"Select [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] From [{nameof(PDOEntity.TBL_PDO)}]";
            return strSql;
        }

        public static string Frm_3_2_Select_LangSwitch_Ctr_List(string strFormName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($" SELECT CtrName,ZH,EN, ColumnName");
            sb.Append($" FROM  [TBL_LangSwitch_Ctr] ");
            sb.Append($" WHERE [FormName] = '{strFormName}' ");
            sb.Append($" AND   [ColumnName] IS NOT NULL ");
            //sb.Append($" SELECT [{nameof(TBL_LangSwitch_Ctr.CtrName)}] ");
            //sb.Append($" ,[{nameof(TBL_LangSwitch_Ctr.ZH)}] ");
            //sb.Append($" ,[{nameof(TBL_LangSwitch_Ctr.EN)}] ");
            //sb.Append($" ,[{nameof(TBL_LangSwitch_Ctr.ColumnName)}] ");
            //sb.Append($" FROM  [{nameof(TBL_LangSwitch_Ctr)}] ");
            //sb.Append($" WHERE [{nameof(TBL_LangSwitch_Ctr.FormName)}] = '{strFormName}' ");
            //sb.Append($" AND   [{nameof(TBL_LangSwitch_Ctr.ColumnName)}] IS NOT NULL ");

            string strSql = sb.ToString();

            return strSql;
        }
        /// <summary>
        /// 新增PDO
        /// </summary>
        /// <returns></returns>
        public static string Frm_3_2_InsertPDO_DB_PDO()
        {
            string strSql = "";
            #region SQL
            strSql = $@"Insert into [{nameof(PDOEntity.TBL_PDO)}]
                                    ([{nameof(PDOEntity.TBL_PDO.Order_No)}],
                                     [{nameof(PDOEntity.TBL_PDO.Plan_No)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}],
                                     [{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}],
                                     [{nameof(PDOEntity.TBL_PDO.Start_Time)}],
                                     [{nameof(PDOEntity.TBL_PDO.Finish_Time)}],
                                     [{nameof(PDOEntity.TBL_PDO.Shift)}],
                                     [{nameof(PDOEntity.TBL_PDO.Team)}],
                                     [{nameof(PDOEntity.TBL_PDO.St_No)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Outer_Diameter)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Inner_Diameter)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Theoretical_Weight)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Act_WT)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Length)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C40_Thick)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C40_Thick)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C40_Thick)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C25_Thick)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C25_Thick)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C25_Thick)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Pass_Num)}],
                                     [{nameof(PDOEntity.TBL_PDO.Mid_Pass_Num)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Pass_Num)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Paper_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Paper_Req_Code )}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Paper_Length)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Paper_Width)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Paper_Length)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Paper_Width)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Use_Sleeve_Flag)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Sleeve_Diameter)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Sleeve_Type_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Hole_Position)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Leader_Length)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Leader_Width)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Leader_Thickness)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Hole_Position)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Length)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Width)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Thickness)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Leader_St_No)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Leader_St_No)}],
                                     [{nameof(PDOEntity.TBL_PDO.Winding_Dire)}],
                                     [{nameof(PDOEntity.TBL_PDO.Better_Surf_Ward_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.Hold_Maker)}],
                                     [{nameof(PDOEntity.TBL_PDO.Hold_Flag)}],
                                     [{nameof(PDOEntity.TBL_PDO.Hold_Cause_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.Sample_Flag)}],
                                     [{nameof(PDOEntity.TBL_PDO.Fixed_Wt_Flag)}],
                                     [{nameof(PDOEntity.TBL_PDO.Final_Coil_Flag)}],
                                     [{nameof(PDOEntity.TBL_PDO.Scrap_Flag)}],
                                     [{nameof(PDOEntity.TBL_PDO.Oil_Flag)}],
                                     [{nameof(PDOEntity.TBL_PDO.Sample_Pos_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Off_Gauge)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Off_Gauge)}],
                                     [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_In)}],
                                     [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_Out)}],
                                     [{nameof(PDOEntity.TBL_PDO.Pre_Grinding_Surface)}],
                                     [{nameof(PDOEntity.TBL_PDO.Grinding_Count_Out)}],
                                     [{nameof(PDOEntity.TBL_PDO.Grinding_Count_In)}],
                                     [{nameof(PDOEntity.TBL_PDO.Appoint_Grinding_Surface)}],
                                     [{nameof(PDOEntity.TBL_PDO.CreateTime)}],
                                     [{nameof(PDOEntity.TBL_PDO.D01_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.D02_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.D03_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.D04_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.D05_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.D06_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.D07_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.D08_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.D09_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.D10_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.D01_Origin)}],
                                     [{nameof(PDOEntity.TBL_PDO.D02_Origin)}],
                                     [{nameof(PDOEntity.TBL_PDO.D03_Origin)}],
                                     [{nameof(PDOEntity.TBL_PDO.D04_Origin)}],
                                     [{nameof(PDOEntity.TBL_PDO.D05_Origin)}],
                                     [{nameof(PDOEntity.TBL_PDO.D06_Origin)}],
                                     [{nameof(PDOEntity.TBL_PDO.D07_Origin)}],
                                     [{nameof(PDOEntity.TBL_PDO.D08_Origin)}],
                                     [{nameof(PDOEntity.TBL_PDO.D09_Origin)}],
                                     [{nameof(PDOEntity.TBL_PDO.D10_Origin)}],
                                     [{nameof(PDOEntity.TBL_PDO.D01_Sid)}],
                                     [{nameof(PDOEntity.TBL_PDO.D02_Sid)}],
                                     [{nameof(PDOEntity.TBL_PDO.D03_Sid)}],
                                     [{nameof(PDOEntity.TBL_PDO.D04_Sid)}],
                                     [{nameof(PDOEntity.TBL_PDO.D05_Sid)}],
                                     [{nameof(PDOEntity.TBL_PDO.D06_Sid)}],
                                     [{nameof(PDOEntity.TBL_PDO.D07_Sid)}],
                                     [{nameof(PDOEntity.TBL_PDO.D08_Sid)}],
                                     [{nameof(PDOEntity.TBL_PDO.D09_Sid)}],
                                     [{nameof(PDOEntity.TBL_PDO.D10_Sid)}],
                                     [{nameof(PDOEntity.TBL_PDO.D01_Pos_W)}],
                                     [{nameof(PDOEntity.TBL_PDO.D02_Pos_W)}],
                                     [{nameof(PDOEntity.TBL_PDO.D03_Pos_W)}],
                                     [{nameof(PDOEntity.TBL_PDO.D04_Pos_W)}],
                                     [{nameof(PDOEntity.TBL_PDO.D05_Pos_W)}],
                                     [{nameof(PDOEntity.TBL_PDO.D06_Pos_W)}],
                                     [{nameof(PDOEntity.TBL_PDO.D07_Pos_W)}],
                                     [{nameof(PDOEntity.TBL_PDO.D08_Pos_W)}],
                                     [{nameof(PDOEntity.TBL_PDO.D09_Pos_W)}],
                                     [{nameof(PDOEntity.TBL_PDO.D10_Pos_W)}],
                                     [{nameof(PDOEntity.TBL_PDO.D01_Pos_L_Start)}],
                                     [{nameof(PDOEntity.TBL_PDO.D02_Pos_L_Start)}],
                                     [{nameof(PDOEntity.TBL_PDO.D03_Pos_L_Start)}],
                                     [{nameof(PDOEntity.TBL_PDO.D04_Pos_L_Start)}],
                                     [{nameof(PDOEntity.TBL_PDO.D05_Pos_L_Start)}],
                                     [{nameof(PDOEntity.TBL_PDO.D06_Pos_L_Start)}],
                                     [{nameof(PDOEntity.TBL_PDO.D07_Pos_L_Start)}],
                                     [{nameof(PDOEntity.TBL_PDO.D08_Pos_L_Start)}],
                                     [{nameof(PDOEntity.TBL_PDO.D09_Pos_L_Start)}],
                                     [{nameof(PDOEntity.TBL_PDO.D10_Pos_L_Start)}],
                                     [{nameof(PDOEntity.TBL_PDO.D01_Pos_L_End)}],
                                     [{nameof(PDOEntity.TBL_PDO.D02_Pos_L_End)}],
                                     [{nameof(PDOEntity.TBL_PDO.D03_Pos_L_End)}],
                                     [{nameof(PDOEntity.TBL_PDO.D04_Pos_L_End)}],
                                     [{nameof(PDOEntity.TBL_PDO.D05_Pos_L_End)}],
                                     [{nameof(PDOEntity.TBL_PDO.D06_Pos_L_End)}],
                                     [{nameof(PDOEntity.TBL_PDO.D07_Pos_L_End)}],
                                     [{nameof(PDOEntity.TBL_PDO.D08_Pos_L_End)}],
                                     [{nameof(PDOEntity.TBL_PDO.D09_Pos_L_End)}],
                                     [{nameof(PDOEntity.TBL_PDO.D10_Pos_L_End)}],
                                     [{nameof(PDOEntity.TBL_PDO.D01_Level)}],
                                     [{nameof(PDOEntity.TBL_PDO.D02_Level)}],
                                     [{nameof(PDOEntity.TBL_PDO.D03_Level)}],
                                     [{nameof(PDOEntity.TBL_PDO.D04_Level)}],
                                     [{nameof(PDOEntity.TBL_PDO.D05_Level)}],
                                     [{nameof(PDOEntity.TBL_PDO.D06_Level)}],
                                     [{nameof(PDOEntity.TBL_PDO.D07_Level)}],
                                     [{nameof(PDOEntity.TBL_PDO.D08_Level)}],
                                     [{nameof(PDOEntity.TBL_PDO.D09_Level)}],
                                     [{nameof(PDOEntity.TBL_PDO.D10_Level)}],
                                     [{nameof(PDOEntity.TBL_PDO.D01_Percent)}],
                                     [{nameof(PDOEntity.TBL_PDO.D02_Percent)}],
                                     [{nameof(PDOEntity.TBL_PDO.D03_Percent)}],
                                     [{nameof(PDOEntity.TBL_PDO.D04_Percent)}],
                                     [{nameof(PDOEntity.TBL_PDO.D05_Percent)}],
                                     [{nameof(PDOEntity.TBL_PDO.D06_Percent)}],
                                     [{nameof(PDOEntity.TBL_PDO.D07_Percent)}],
                                     [{nameof(PDOEntity.TBL_PDO.D08_Percent)}],
                                     [{nameof(PDOEntity.TBL_PDO.D09_Percent)}],
                                     [{nameof(PDOEntity.TBL_PDO.D10_Percent)}],
                                     [{nameof(PDOEntity.TBL_PDO.D01_QGrade)}],
                                     [{nameof(PDOEntity.TBL_PDO.D02_QGrade)}],
                                     [{nameof(PDOEntity.TBL_PDO.D03_QGrade)}],
                                     [{nameof(PDOEntity.TBL_PDO.D04_QGrade)}],
                                     [{nameof(PDOEntity.TBL_PDO.D05_QGrade)}],
                                     [{nameof(PDOEntity.TBL_PDO.D06_QGrade)}],
                                     [{nameof(PDOEntity.TBL_PDO.D07_QGrade)}],
                                     [{nameof(PDOEntity.TBL_PDO.D08_QGrade)}],
                                     [{nameof(PDOEntity.TBL_PDO.D09_QGrade)}],
                                     [{nameof(PDOEntity.TBL_PDO.D10_QGrade)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Rough_Rz)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Rough_Ra)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Rough_Rmax)}],
                                     [{nameof(PDOEntity.TBL_PDO.Mid_Rough_Rz)}],
                                     [{nameof(PDOEntity.TBL_PDO.Mid_Rough_Ra)}],
                                     [{nameof(PDOEntity.TBL_PDO.Mid_Rough_Rmax)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Rough_Rz)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Rough_Ra)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Rough_Rmax)}],
                                     [{nameof(PDOEntity.TBL_PDO.Process_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.Uncoil_Direction)}],
                                     [{nameof(PDOEntity.TBL_PDO.Recoiler_ActTen_Avg)}],
                                     [{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}],
                                     [{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Time)}],
                                     [{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_UserID)}],
                                     [{nameof(PDOEntity.TBL_PDO.Exit_Scaned_CoilID)}],
                                     [{nameof(PDOEntity.TBL_PDO.Exit_Scaned_UserID)}],
                                     [{nameof(PDOEntity.TBL_PDO.Coil_Check_Time)}],
                                     [{nameof(PDOEntity.TBL_PDO.Coil_Check_Result)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT_Time)}])
                            Values (  
                                    '{PDODetail.Txt_Order_No.Text }',
                                    '{PDODetail.Txt_Plan_No.Text}',
                                    '{PDODetail.Txt_Out_Coil_ID.Text}',
                                    '{PDODetail.Txt_In_Coil_ID.Text}',
                                    '{PDODetail.Dtp_StartTime.Value:yyyy-MM-dd HH:mm:ss}',
                                    '{PDODetail.Dtp_FinishTime.Value:yyyy-MM-dd HH:mm:ss}',
                                    '{PDODetail.Cob_Shift.SelectedValue}',
                                    '{PDODetail.Cob_Team.SelectedValue}',
                                    '{PDODetail.Txt_St_No.Text}',
                                    '{PDODetail.Txt_Out_Coil_Outer_Diameter.Text}',
                                    '{PDODetail.Txt_Out_Coil_Inner_Diameter.Text}',
                                    '{PDODetail.Txt_Out_Coil_Theoretical_Weight.Text}',
                                    '{PDODetail.Txt_Out_Coil_Act_WT.Text}',
                                    '{PDODetail.Txt_Out_Coil_Gross_WT.Text}',
                                    '{PDODetail.Txt_Out_Coil_Thick.Text}',
                                    '{PDODetail.Txt_Out_Coil_Width.Text}',
                                    '{PDODetail.Txt_Out_Coil_Length.Text}',
                                    '{PDODetail.Txt_Head_C40_Thick.Text}',
                                    '{PDODetail.Txt_Mid_C40_Thick.Text}',
                                    '{PDODetail.Txt_Tail_C40_Thick.Text}',
                                    '{PDODetail.Txt_Head_C25_Thick.Text}',
                                    '{PDODetail.Txt_Mid_C25_Thick.Text}',
                                    '{PDODetail.Txt_Tail_C25_Thick.Text}',
                                    '{PDODetail.Txt_Head_Pass_Num.Text}',
                                    '{PDODetail.Txt_Mid_Pass_Num.Text}',
                                    '{PDODetail.Txt_Tail_Pass_Num.Text}',
                                    '{PDODetail.Cob_Paper_Code.SelectedValue}',
                                    '{PDODetail.Cob_Paper_Req_Code.SelectedValue}',
                                    '{PDODetail.Txt_Out_Head_Paper_Length.Text}',
                                    '{PDODetail.Txt_Out_Head_Paper_Width.Text}',
                                    '{PDODetail.Txt_Out_Tail_Paper_Length.Text}',
                                    '{PDODetail.Txt_Out_Tail_Paper_Width.Text}',
                                    '{PDODetail.Cob_Out_Coil_Use_Sleeve_Flag.SelectedValue}',
                                    '{PDODetail.Txt_Sleeve_Inner_Exit_Diamter.Text}',
                                    '{PDODetail.Cob_Sleeve_Type_Exit.SelectedValue}',
                                    '{PDODetail.Txt_Head_Hole_Position.Text}',
                                    '{PDODetail.Txt_Head_Leader_Length.Text}',
                                    '{PDODetail.Txt_Head_Leader_Width.Text}',
                                    '{PDODetail.Txt_Head_Leader_Thickness.Text}',
                                    '{PDODetail.Txt_Tail_PunchHole_Position.Text}',
                                    '{PDODetail.Txt_Tail_Leader_Length.Text}',
                                    '{PDODetail.Txt_Tail_Leader_Width.Text}',
                                    '{PDODetail.Txt_Tail_Leader_Thickness.Text}',
                                    '{PDODetail.Txt_Head_Leader_St_No.Text}',
                                    '{PDODetail.Txt_Tail_Leader_St_No.Text}',
                                    '{PDODetail.Cob_Winding_Direction.SelectedValue}',
                                    '{PDODetail.Cob_Base_Surface.SelectedValue}',
                                    '{PDODetail.Txt_Inspector.Text}',
                                    '{PDODetail.Cob_Hold_Flag.SelectedValue}',
                                    '{PDODetail.Txt_Hold_Cause_Code.Text}',
                                    '{PDODetail.Cob_Sample_Flag.SelectedValue}',
                                    '{PDODetail.Cob_Fixed_WT_Flag.SelectedValue}',
                                    '{PDODetail.Cob_End_Flag.SelectedValue}',
                                    '{PDODetail.Cob_Scrap_Flag.SelectedValue}',
                                    '{PDODetail.Cob_Oil_Flag.SelectedValue}',
                                    '{PDODetail.Cob_Sample_Frqn_Code.SelectedValue}',
                                    '{PDODetail.Cob_Surface_Accuracy_Code.SelectedValue}',
                                    '{PDODetail.Txt_Head_Off_Gauge.Text}',
                                    '{PDODetail.Txt_Tail_Off_Gauge.Text}',
                                    '{PDODetail.Cob_Surface_Accu_Code_In.SelectedValue }',
                                    '{PDODetail.Cob_Surface_Accu_Code_Out.SelectedValue}',
                                    '{PDODetail.Cob_Pre_Grinding_Surface.SelectedValue }',
                                    '{PDODetail.Txt_Grinding_Count_Out.Text}',
                                    '{PDODetail.Txt_Grinding_Count_In.Text }',
                                    '{PDODetail.Cob_Appoint_Grinding_Surface.SelectedValue}',

                                    '{Instance.getTime}',
                                    '{PDODetail.Txt_Code_D01.Text }',
                                    '{PDODetail.Txt_Code_D02.Text }',
                                    '{PDODetail.Txt_Code_D03.Text }',
                                    '{PDODetail.Txt_Code_D04.Text }',
                                    '{PDODetail.Txt_Code_D05.Text }',
                                    '{PDODetail.Txt_Code_D06.Text }',
                                    '{PDODetail.Txt_Code_D07.Text }',
                                    '{PDODetail.Txt_Code_D08.Text }',
                                    '{PDODetail.Txt_Code_D09.Text }',
                                    '{PDODetail.Txt_Code_D10.Text}',
                                    '{PDODetail.Txt_Origin_D01.Text }',
                                    '{PDODetail.Txt_Origin_D02.Text }',
                                    '{PDODetail.Txt_Origin_D03.Text }',
                                    '{PDODetail.Txt_Origin_D04.Text }',
                                    '{PDODetail.Txt_Origin_D05.Text }',
                                    '{PDODetail.Txt_Origin_D06.Text }',
                                    '{PDODetail.Txt_Origin_D07.Text }',
                                    '{PDODetail.Txt_Origin_D08.Text }',
                                    '{PDODetail.Txt_Origin_D09.Text }',
                                    '{PDODetail.Txt_Origin_D10.Text}',
                                    '{PDODetail.Cob_Sid_D01.Text}',
                                    '{PDODetail.Cob_Sid_D02.Text}',
                                    '{PDODetail.Cob_Sid_D03.Text}',
                                    '{PDODetail.Cob_Sid_D04.Text}',
                                    '{PDODetail.Cob_Sid_D05.Text}',
                                    '{PDODetail.Cob_Sid_D06.Text}',
                                    '{PDODetail.Cob_Sid_D07.Text}',
                                    '{PDODetail.Cob_Sid_D08.Text}',
                                    '{PDODetail.Cob_Sid_D09.Text}',
                                    '{PDODetail.Cob_Sid_D10.Text}',
                                    '{PDODetail.Cob_PosW_D01.Text}',
                                    '{PDODetail.Cob_PosW_D02.Text}',
                                    '{PDODetail.Cob_PosW_D03.Text}',
                                    '{PDODetail.Cob_PosW_D04.Text}',
                                    '{PDODetail.Cob_PosW_D05.Text}',
                                    '{PDODetail.Cob_PosW_D06.Text}',
                                    '{PDODetail.Cob_PosW_D07.Text}',
                                    '{PDODetail.Cob_PosW_D08.Text}',
                                    '{PDODetail.Cob_PosW_D09.Text}',
                                    '{PDODetail.Cob_PosW_D10.Text}',
                                    '{PDODetail.Txt_Pos_L_Start_D01.Text }',
                                    '{PDODetail.Txt_Pos_L_Start_D02.Text }',
                                    '{PDODetail.Txt_Pos_L_Start_D03.Text }',
                                    '{PDODetail.Txt_Pos_L_Start_D04.Text }',
                                    '{PDODetail.Txt_Pos_L_Start_D05.Text }',
                                    '{PDODetail.Txt_Pos_L_Start_D06.Text }',
                                    '{PDODetail.Txt_Pos_L_Start_D07.Text }',
                                    '{PDODetail.Txt_Pos_L_Start_D08.Text }',
                                    '{PDODetail.Txt_Pos_L_Start_D09.Text }',
                                    '{PDODetail.Txt_Pos_L_Start_D10.Text}',
                                    '{PDODetail.Txt_Pos_L_End_D01.Text }',
                                    '{PDODetail.Txt_Pos_L_End_D02.Text }',
                                    '{PDODetail.Txt_Pos_L_End_D03.Text }',
                                    '{PDODetail.Txt_Pos_L_End_D04.Text }',
                                    '{PDODetail.Txt_Pos_L_End_D05.Text }',
                                    '{PDODetail.Txt_Pos_L_End_D06.Text }',
                                    '{PDODetail.Txt_Pos_L_End_D07.Text }',
                                    '{PDODetail.Txt_Pos_L_End_D08.Text }',
                                    '{PDODetail.Txt_Pos_L_End_D09.Text }',
                                    '{PDODetail.Txt_Pos_L_End_D10.Text}',
                                    '{PDODetail.Cob_Level_D01.Text}',
                                    '{PDODetail.Cob_Level_D02.Text}',
                                    '{PDODetail.Cob_Level_D03.Text}',
                                    '{PDODetail.Cob_Level_D04.Text}',
                                    '{PDODetail.Cob_Level_D05.Text}',
                                    '{PDODetail.Cob_Level_D06.Text}',
                                    '{PDODetail.Cob_Level_D07.Text}',
                                    '{PDODetail.Cob_Level_D08.Text}',
                                    '{PDODetail.Cob_Level_D09.Text}',
                                    '{PDODetail.Cob_Level_D10.Text}',
                                    '{PDODetail.Txt_Percent_D01.Text }',
                                    '{PDODetail.Txt_Percent_D02.Text }',
                                    '{PDODetail.Txt_Percent_D03.Text }',
                                    '{PDODetail.Txt_Percent_D04.Text }',
                                    '{PDODetail.Txt_Percent_D05.Text }',
                                    '{PDODetail.Txt_Percent_D06.Text }',
                                    '{PDODetail.Txt_Percent_D07.Text }',
                                    '{PDODetail.Txt_Percent_D08.Text }',
                                    '{PDODetail.Txt_Percent_D09.Text }',
                                    '{PDODetail.Txt_Percent_D10.Text}',
                                    '{PDODetail.Txt_QGRADE_D01.Text }',
                                    '{PDODetail.Txt_QGRADE_D02.Text }',
                                    '{PDODetail.Txt_QGRADE_D03.Text }',
                                    '{PDODetail.Txt_QGRADE_D04.Text }',
                                    '{PDODetail.Txt_QGRADE_D05.Text }',
                                    '{PDODetail.Txt_QGRADE_D06.Text }',
                                    '{PDODetail.Txt_QGRADE_D07.Text }',
                                    '{PDODetail.Txt_QGRADE_D08.Text }',
                                    '{PDODetail.Txt_QGRADE_D09.Text }',
                                    '{PDODetail.Txt_QGRADE_D10.Text}',
                                    '{PDODetail.Txt_Head_Rough_Rz.Text}',
                                    '{PDODetail.Txt_Head_Rough_Ra.Text}',
                                    '{PDODetail.Txt_Head_Rough_Rmax.Text}',
                                    '{PDODetail.Txt_Mid_Rough_Rz.Text}',
                                    '{PDODetail.Txt_Mid_Rough_Ra.Text}',
                                    '{PDODetail.Txt_Mid_Rough_Rmax.Text}',
                                    '{PDODetail.Txt_Tail_Rough_Rz.Text}', 
                                    '{PDODetail.Txt_Tail_Rough_Ra.Text}', 
                                    '{PDODetail.Txt_Tail_Rough_Rmax.Text}', 
                                    '{PDODetail.Cob_ProcessCode.SelectedValue}',
                                    '{PDODetail.Cob_Decoiler_Direction.SelectedValue}',
                                    '{PDODetail.Txt_Recoiler_Actten_Avg.Text}',
                                    '0',/*PDO_Uploaded_Flag varchar(1)*/
                                    '', /*PDO_Uploaded_Time datetime*/
                                    '', /*PDO_Uploaded_UserID varchar(10)*/
                                    '', /*Exit_Scaned_CoilID varchar(20)*/
                                    '', /*Exit_Scaned_UserID varchar(10)*/
                                    '', /*Exit_Scaned_Time datetime*/
                                    '0',/*Exit_CoilID_Checked varchar(1)*/
                                    '') /*CoilWeight_Time datetime*/ ";
            #endregion
            return strSql;

            // '{PDODetail.Txt_ProcessCode.Text}',
        }

        /// <summary>
        /// 檢查PDO是否已上傳MMS
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_3_2_UpdatePDO_Check_DB_PDO(string Coil_ID)
        {
            string strSql = "";
            strSql = $"Select [{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] From [{nameof(PDOEntity.TBL_PDO)}] Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] ='{Coil_ID}'";
            return strSql;
        }

        /// <summary>
        /// PDO資料儲存
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_3_2_SavePDO_DB_PDO(string Coil_ID, string strPlan_No = null, string strIn_Coil_ID = null)
        {
            string strSql = "";
            if (string.IsNullOrEmpty(strPlan_No))
                strPlan_No = PDODetail.Txt_Plan_No.Text;
            if (string.IsNullOrEmpty(strIn_Coil_ID))
                strIn_Coil_ID = PDODetail.Txt_In_Coil_ID.Text;

            #region SQL
            strSql = $@"Update [{nameof(PDOEntity.TBL_PDO)}] set 
                                [{nameof(PDOEntity.TBL_PDO.Order_No)}] = '{PDODetail.Txt_Order_No.Text}',
                               
                                [{nameof(PDOEntity.TBL_PDO.Start_Time)}] = '{PDODetail.Dtp_StartTime.Value:yyyy-MM-dd HH:mm:ss}',
                                [{nameof(PDOEntity.TBL_PDO.Finish_Time)}] = '{PDODetail.Dtp_FinishTime.Value:yyyy-MM-dd HH:mm:ss}',
                                [{nameof(PDOEntity.TBL_PDO.Shift)}] = '{PDODetail.Cob_Shift.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Team)}] = '{PDODetail.Cob_Team.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.St_No)}] = '{PDODetail.Txt_St_No.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Outer_Diameter)}] = '{PDODetail.Txt_Out_Coil_Outer_Diameter.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Inner_Diameter)}] = '{PDODetail.Txt_Out_Coil_Inner_Diameter.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Theoretical_Weight)}] = '{PDODetail.Txt_Out_Coil_Theoretical_Weight.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Act_WT)}] = '{PDODetail.Txt_Out_Coil_Act_WT.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT)}] = '{PDODetail.Txt_Out_Coil_Gross_WT.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)}] = '{PDODetail.Txt_Out_Coil_Thick.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}] = '{PDODetail.Txt_Out_Coil_Width.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Length)}] = '{PDODetail.Txt_Out_Coil_Length.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C40_Thick)}] = '{PDODetail.Txt_Head_C40_Thick.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C40_Thick)}] = '{PDODetail.Txt_Mid_C40_Thick.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C40_Thick)}] = '{PDODetail.Txt_Tail_C40_Thick.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C25_Thick)}] = '{PDODetail.Txt_Head_C25_Thick.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C25_Thick)}] = '{PDODetail.Txt_Mid_C25_Thick.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C25_Thick)}] = '{PDODetail.Txt_Tail_C25_Thick.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Pass_Num)}] = '{PDODetail.Txt_Head_Pass_Num.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Mid_Pass_Num)}] = '{PDODetail.Txt_Mid_Pass_Num.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Pass_Num)}] = '{PDODetail.Txt_Tail_Pass_Num.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Paper_Code)}] = '{PDODetail.Cob_Paper_Code.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Paper_Req_Code )}] = '{PDODetail.Cob_Paper_Req_Code.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Paper_Length)}] = '{PDODetail.Txt_Out_Head_Paper_Length.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Paper_Width)}] = '{PDODetail.Txt_Out_Head_Paper_Width.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Paper_Length)}] = '{PDODetail.Txt_Out_Tail_Paper_Length.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Paper_Width)}] = '{PDODetail.Txt_Out_Tail_Paper_Width.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Use_Sleeve_Flag)}] = '{PDODetail.Cob_Out_Coil_Use_Sleeve_Flag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Sleeve_Diameter)}] = '{PDODetail.Txt_Sleeve_Inner_Exit_Diamter.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Sleeve_Type_Code)}] = '{PDODetail.Cob_Sleeve_Type_Exit.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Hole_Position)}] = '{PDODetail.Txt_Head_Hole_Position.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Leader_Length)}] = '{PDODetail.Txt_Head_Leader_Length.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Leader_Width)}] = '{PDODetail.Txt_Head_Leader_Width.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Leader_Thickness)}] = '{PDODetail.Txt_Head_Leader_Thickness.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Hole_Position)}] = '{PDODetail.Txt_Tail_PunchHole_Position.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Length)}] = '{PDODetail.Txt_Tail_Leader_Length.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Width)}] = '{PDODetail.Txt_Tail_Leader_Width.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Thickness)}] = '{PDODetail.Txt_Tail_Leader_Thickness.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Leader_St_No)}] = '{PDODetail.Txt_Head_Leader_St_No.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Leader_St_No)}] = '{PDODetail.Txt_Tail_Leader_St_No.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Winding_Dire)}] = '{PDODetail.Cob_Winding_Direction.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Better_Surf_Ward_Code)}] = '{PDODetail.Cob_Base_Surface.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Hold_Maker)}] = '{PDODetail.Txt_Inspector.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Hold_Flag)}] = '{PDODetail.Cob_Hold_Flag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Hold_Cause_Code)}] = '{PDODetail.Txt_Hold_Cause_Code.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Sample_Flag)}] = '{PDODetail.Cob_Sample_Flag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Fixed_Wt_Flag)}] = '{PDODetail.Cob_Fixed_WT_Flag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Final_Coil_Flag)}] = '{PDODetail.Cob_End_Flag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Scrap_Flag)}] = '{PDODetail.Cob_Scrap_Flag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Oil_Flag)}] = '{PDODetail.Cob_Oil_Flag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Sample_Pos_Code)}] = '{PDODetail.Cob_Sample_Frqn_Code.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code)}] = '{PDODetail.Cob_Surface_Accuracy_Code.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Off_Gauge)}] = '{PDODetail.Txt_Head_Off_Gauge.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Off_Gauge)}] = '{PDODetail.Txt_Tail_Off_Gauge.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_In)}] = '{PDODetail.Cob_Surface_Accu_Code_In.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_Out)}] = '{PDODetail.Cob_Surface_Accu_Code_Out.SelectedValue}',

                                [{nameof(PDOEntity.TBL_PDO.Pre_Grinding_Surface)}] = '{PDODetail.Cob_Pre_Grinding_Surface.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Grinding_Count_Out)}] = '{PDODetail.Txt_Grinding_Count_Out.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Grinding_Count_In)}] = '{PDODetail.Txt_Grinding_Count_In.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Appoint_Grinding_Surface)}] = '{PDODetail.Cob_Appoint_Grinding_Surface.SelectedValue}',

                                [{nameof(PDOEntity.TBL_PDO.D01_Code)}] = '{PDODetail.Txt_Code_D01.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D02_Code)}] = '{PDODetail.Txt_Code_D02.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D03_Code)}] = '{PDODetail.Txt_Code_D03.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D04_Code)}] = '{PDODetail.Txt_Code_D04.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D05_Code)}] = '{PDODetail.Txt_Code_D05.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D06_Code)}] = '{PDODetail.Txt_Code_D06.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D07_Code)}] = '{PDODetail.Txt_Code_D07.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D08_Code)}] = '{PDODetail.Txt_Code_D08.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D09_Code)}] = '{PDODetail.Txt_Code_D09.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D10_Code)}] = '{PDODetail.Txt_Code_D10.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D01_Origin)}] = '{PDODetail.Txt_Origin_D01.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D02_Origin)}] = '{PDODetail.Txt_Origin_D02.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D03_Origin)}] = '{PDODetail.Txt_Origin_D03.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D04_Origin)}] = '{PDODetail.Txt_Origin_D04.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D05_Origin)}] = '{PDODetail.Txt_Origin_D05.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D06_Origin)}] = '{PDODetail.Txt_Origin_D06.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D07_Origin)}] = '{PDODetail.Txt_Origin_D07.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D08_Origin)}] = '{PDODetail.Txt_Origin_D08.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D09_Origin)}] = '{PDODetail.Txt_Origin_D09.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D10_Origin)}] = '{PDODetail.Txt_Origin_D10.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D01_Sid)}] = '{PDODetail.Cob_Sid_D01.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D02_Sid)}] = '{PDODetail.Cob_Sid_D02.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D03_Sid)}] = '{PDODetail.Cob_Sid_D03.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D04_Sid)}] = '{PDODetail.Cob_Sid_D04.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D05_Sid)}] = '{PDODetail.Cob_Sid_D05.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D06_Sid)}] = '{PDODetail.Cob_Sid_D06.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D07_Sid)}] = '{PDODetail.Cob_Sid_D07.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D08_Sid)}] = '{PDODetail.Cob_Sid_D08.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D09_Sid)}] = '{PDODetail.Cob_Sid_D09.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D10_Sid)}] = '{PDODetail.Cob_Sid_D10.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D01_Pos_W)}] = '{PDODetail.Cob_PosW_D01.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D02_Pos_W)}] = '{PDODetail.Cob_PosW_D02.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D03_Pos_W)}] = '{PDODetail.Cob_PosW_D03.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D04_Pos_W)}] = '{PDODetail.Cob_PosW_D04.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D05_Pos_W)}] = '{PDODetail.Cob_PosW_D05.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D06_Pos_W)}] = '{PDODetail.Cob_PosW_D06.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D07_Pos_W)}] = '{PDODetail.Cob_PosW_D07.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D08_Pos_W)}] = '{PDODetail.Cob_PosW_D08.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D09_Pos_W)}] = '{PDODetail.Cob_PosW_D09.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D10_Pos_W)}] = '{PDODetail.Cob_PosW_D10.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D01_Pos_L_Start)}] = '{PDODetail.Txt_Pos_L_Start_D01.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D02_Pos_L_Start)}] = '{PDODetail.Txt_Pos_L_Start_D02.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D03_Pos_L_Start)}] = '{PDODetail.Txt_Pos_L_Start_D03.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D04_Pos_L_Start)}] = '{PDODetail.Txt_Pos_L_Start_D04.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D05_Pos_L_Start)}] = '{PDODetail.Txt_Pos_L_Start_D05.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D06_Pos_L_Start)}] = '{PDODetail.Txt_Pos_L_Start_D06.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D07_Pos_L_Start)}] = '{PDODetail.Txt_Pos_L_Start_D07.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D08_Pos_L_Start)}] = '{PDODetail.Txt_Pos_L_Start_D08.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D09_Pos_L_Start)}] = '{PDODetail.Txt_Pos_L_Start_D09.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D10_Pos_L_Start)}] = '{PDODetail.Txt_Pos_L_Start_D10.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D01_Pos_L_End)}] = '{PDODetail.Txt_Pos_L_End_D01.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D02_Pos_L_End)}] = '{PDODetail.Txt_Pos_L_End_D02.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D03_Pos_L_End)}] = '{PDODetail.Txt_Pos_L_End_D03.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D04_Pos_L_End)}] = '{PDODetail.Txt_Pos_L_End_D04.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D05_Pos_L_End)}] = '{PDODetail.Txt_Pos_L_End_D05.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D06_Pos_L_End)}] = '{PDODetail.Txt_Pos_L_End_D06.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D07_Pos_L_End)}] = '{PDODetail.Txt_Pos_L_End_D07.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D08_Pos_L_End)}] = '{PDODetail.Txt_Pos_L_End_D08.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D09_Pos_L_End)}] = '{PDODetail.Txt_Pos_L_End_D09.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D10_Pos_L_End)}] = '{PDODetail.Txt_Pos_L_End_D10.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D01_Level)}] = '{PDODetail.Cob_Level_D01.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D02_Level)}] = '{PDODetail.Cob_Level_D02.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D03_Level)}] = '{PDODetail.Cob_Level_D03.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D04_Level)}] = '{PDODetail.Cob_Level_D04.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D05_Level)}] = '{PDODetail.Cob_Level_D05.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D06_Level)}] = '{PDODetail.Cob_Level_D06.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D07_Level)}] = '{PDODetail.Cob_Level_D07.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D08_Level)}] = '{PDODetail.Cob_Level_D08.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D09_Level)}] = '{PDODetail.Cob_Level_D09.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D10_Level)}] = '{PDODetail.Cob_Level_D10.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D01_Percent)}] = '{PDODetail.Txt_Percent_D01.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D02_Percent)}] = '{PDODetail.Txt_Percent_D02.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D03_Percent)}] = '{PDODetail.Txt_Percent_D03.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D04_Percent)}] = '{PDODetail.Txt_Percent_D04.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D05_Percent)}] = '{PDODetail.Txt_Percent_D05.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D06_Percent)}] = '{PDODetail.Txt_Percent_D06.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D07_Percent)}] = '{PDODetail.Txt_Percent_D07.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D08_Percent)}] = '{PDODetail.Txt_Percent_D08.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D09_Percent)}] = '{PDODetail.Txt_Percent_D09.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D10_Percent)}] = '{PDODetail.Txt_Percent_D10.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D01_QGrade)}] = '{PDODetail.Txt_QGRADE_D01.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D02_QGrade)}] = '{PDODetail.Txt_QGRADE_D02.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D03_QGrade)}] = '{PDODetail.Txt_QGRADE_D03.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D04_QGrade)}] = '{PDODetail.Txt_QGRADE_D04.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D05_QGrade)}] = '{PDODetail.Txt_QGRADE_D05.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D06_QGrade)}] = '{PDODetail.Txt_QGRADE_D06.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D07_QGrade)}] = '{PDODetail.Txt_QGRADE_D07.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D08_QGrade)}] = '{PDODetail.Txt_QGRADE_D08.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D09_QGrade)}] = '{PDODetail.Txt_QGRADE_D09.Text}',
                                [{nameof(PDOEntity.TBL_PDO.D10_QGrade)}] = '{PDODetail.Txt_QGRADE_D10.Text}',
                                [{nameof(PDOEntity.TBL_PDO.CreateTime)}] = '{Instance.getTime}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Rough_Rz)}] = '{PDODetail.Txt_Head_Rough_Rz.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Rough_Ra)}] = '{PDODetail.Txt_Head_Rough_Ra.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Rough_Rmax)}] = '{PDODetail.Txt_Head_Rough_Rmax.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Mid_Rough_Rz)}] = '{PDODetail.Txt_Mid_Rough_Rz.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Mid_Rough_Ra)}] = '{PDODetail.Txt_Mid_Rough_Ra.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Mid_Rough_Rmax)}] = '{PDODetail.Txt_Mid_Rough_Rmax.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Rough_Rz)}] = '{PDODetail.Txt_Tail_Rough_Rz.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Rough_Ra)}] = '{PDODetail.Txt_Tail_Rough_Ra.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Rough_Rmax)}] = '{PDODetail.Txt_Tail_Rough_Rmax.Text}',

                                [{nameof(PDOEntity.TBL_PDO.Process_Code)}] = '{PDODetail.Cob_ProcessCode.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Uncoil_Direction)}] = '{PDODetail.Cob_Decoiler_Direction.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Recoiler_ActTen_Avg)}] = '{PDODetail.Txt_Recoiler_Actten_Avg.Text}'
                          Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{Coil_ID}'
                          AND   [{ nameof(PDOEntity.TBL_PDO.Plan_No)}] = '{strPlan_No}'
                          AND   [{ nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] = '{strIn_Coil_ID}' ";


            //[{ nameof(PDOEntity.TBL_PDO.Process_Code)}] = '{PDODetail.Txt_ProcessCode.Text}',
            //                    [{ nameof(PDOModel.L2L3_PDO.PlanNo)}] = '{PDODetail.Txt_Plan_No.Text}',
            //                    [{ nameof(PDOModel.L2L3_PDO.Out_Mat_No)}] = '{PDODetail.Txt_Out_mat_No.Text}',
            //                    [{ nameof(PDOModel.L2L3_PDO.In_Mat_No)}] = '{PDODetail.Txt_In_mat_No.Text}',
            #endregion
            return strSql;
        }
        /// <summary>
        /// PDO詳細資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_3_2_SelectData_DB_PDO(string Coil_ID, string strPlan_No = null, string strIn_Coil_ID = null,string strFinishTime = null)
        {
            string strSql = "";
            #region SQL
            strSql = $@"Select 
                            [{nameof(PDOEntity.TBL_PDO.Order_No)}],
                            [{nameof(PDOEntity.TBL_PDO.Plan_No)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}],
                            [{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}],
                            Convert(char(19), [{nameof(PDOEntity.TBL_PDO.Start_Time)}], 120) Start_Time,
                            Convert(char(19), [{nameof(PDOEntity.TBL_PDO.Finish_Time)}], 120) Finish_Time,
                            [{nameof(PDOEntity.TBL_PDO.Shift)}],
                            [{nameof(PDOEntity.TBL_PDO.Team)}],
                            [{nameof(PDOEntity.TBL_PDO.St_No)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Outer_Diameter)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Inner_Diameter)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Act_WT)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Theoretical_Weight)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Length)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C40_Thick)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C40_Thick)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C40_Thick)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Head_C25_Thick)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Mid_C25_Thick)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Tail_C25_Thick)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Pass_Num)}],
                            [{nameof(PDOEntity.TBL_PDO.Mid_Pass_Num)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Pass_Num)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Paper_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Paper_Req_Code )}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Paper_Length)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Paper_Width)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Paper_Length)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Paper_Width)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Use_Sleeve_Flag)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Sleeve_Diameter)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Sleeve_Type_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Hole_Position)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Leader_Length)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Leader_Width)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Leader_Thickness)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Hole_Position)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Length)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Width)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Thickness)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Leader_St_No)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Leader_St_No)}],
                            [{nameof(PDOEntity.TBL_PDO.Winding_Dire)}],
                            [{nameof(PDOEntity.TBL_PDO.Better_Surf_Ward_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.Hold_Maker)}],
                            [{nameof(PDOEntity.TBL_PDO.Hold_Flag)}],
                            [{nameof(PDOEntity.TBL_PDO.Hold_Cause_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.Sample_Flag)}],
                            [{nameof(PDOEntity.TBL_PDO.Fixed_Wt_Flag)}],
                            [{nameof(PDOEntity.TBL_PDO.Final_Coil_Flag)}],
                            [{nameof(PDOEntity.TBL_PDO.Scrap_Flag)}],
                            [{nameof(PDOEntity.TBL_PDO.Oil_Flag)}],
                            [{nameof(PDOEntity.TBL_PDO.Sample_Pos_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Off_Gauge)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Off_Gauge)}],
                            [{nameof(PDOEntity.TBL_PDO.Pre_Grinding_Surface)}],
                            [{nameof(PDOEntity.TBL_PDO.Grinding_Count_Out)}],
                            [{nameof(PDOEntity.TBL_PDO.Grinding_Count_In)}],
                            [{nameof(PDOEntity.TBL_PDO.Appoint_Grinding_Surface)}],

                            [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_In)}],
                            [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_Out)}],
                            [{nameof(PDOEntity.TBL_PDO.Uncoil_Direction)}],
                            [{nameof(PDOEntity.TBL_PDO.Recoiler_ActTen_Avg)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Rough_Rz)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Rough_Ra)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Rough_Rmax)}],
                            [{nameof(PDOEntity.TBL_PDO.Mid_Rough_Rz)}],
                            [{nameof(PDOEntity.TBL_PDO.Mid_Rough_Ra)}],
                            [{nameof(PDOEntity.TBL_PDO.Mid_Rough_Rmax)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Rough_Rz)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Rough_Ra)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Rough_Rmax)}],
                            [{nameof(PDOEntity.TBL_PDO.Process_Code)}]
                             , CONVERT(VARCHAR(25), [{ nameof(PDOEntity.TBL_PDO.Finish_Time)}], 121) as FinishTime_Str 
                        From [{nameof(PDOEntity.TBL_PDO)}]
                        Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{Coil_ID}' ";

            if (strPlan_No != null)
                strSql += $@" AND [{ nameof(PDOEntity.TBL_PDO.Plan_No)}] = '{strPlan_No}' ";

            if (strFinishTime != null)
            {
                //strSql += $@" AND [{ nameof(PDOEntity.TBL_PDO.FinishTime)}] =  '{strFinishTime}'";
                strSql += $@" AND CONVERT(VARCHAR(25), [{ nameof(PDOEntity.TBL_PDO.Finish_Time)}], 121) LIKE  '{strFinishTime}%' ";
            }

            if (strIn_Coil_ID != null)
                strSql += $@" AND [{ nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] = '{strIn_Coil_ID}' ";           

            #endregion

            return strSql;
        }
        public static string Frm_3_2_SelectDefectData_DB_PDO(string Coil_ID, string strPlan_No = null)
        {
            string strSql = "";
            #region SQL
            strSql = $@"select
                         [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}]
                        ,[{nameof(PDOEntity.TBL_PDO.Plan_No)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D01_Code)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D02_Code)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D03_Code)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D04_Code)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D05_Code)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D06_Code)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D07_Code)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D08_Code)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D09_Code)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D10_Code)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D01_Origin)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D02_Origin)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D03_Origin)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D04_Origin)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D05_Origin)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D06_Origin)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D07_Origin)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D08_Origin)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D09_Origin)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D10_Origin)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D01_Sid)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D02_Sid)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D03_Sid)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D04_Sid)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D05_Sid)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D06_Sid)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D07_Sid)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D08_Sid)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D09_Sid)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D10_Sid)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D01_Pos_W)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D02_Pos_W)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D03_Pos_W)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D04_Pos_W)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D05_Pos_W)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D06_Pos_W)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D07_Pos_W)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D08_Pos_W)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D09_Pos_W)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D10_Pos_W)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D01_Pos_L_Start)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D02_Pos_L_Start)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D03_Pos_L_Start)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D04_Pos_L_Start)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D05_Pos_L_Start)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D06_Pos_L_Start)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D07_Pos_L_Start)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D08_Pos_L_Start)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D09_Pos_L_Start)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D10_Pos_L_Start)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D01_Pos_L_End)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D02_Pos_L_End)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D03_Pos_L_End)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D04_Pos_L_End)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D05_Pos_L_End)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D06_Pos_L_End)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D07_Pos_L_End)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D08_Pos_L_End)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D09_Pos_L_End)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D10_Pos_L_End)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D01_Level)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D02_Level)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D03_Level)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D04_Level)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D05_Level)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D06_Level)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D07_Level)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D08_Level)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D09_Level)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D10_Level)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D01_Percent)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D02_Percent)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D03_Percent)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D04_Percent)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D05_Percent)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D06_Percent)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D07_Percent)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D08_Percent)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D09_Percent)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D10_Percent)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D01_QGrade)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D02_QGrade)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D03_QGrade)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D04_QGrade)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D05_QGrade)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D06_QGrade)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D07_QGrade)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D08_QGrade)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D09_QGrade)}]
                        ,[{nameof(PDOEntity.TBL_PDO.D10_QGrade)}]
                       From [{nameof(PDOEntity.TBL_PDO)}]
                       Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{Coil_ID}'";
            if (!string.IsNullOrEmpty(strPlan_No))
                strSql += $@" AND [{nameof(PDOEntity.TBL_PDO.Plan_No)}] = '{strPlan_No}'
                                ";

            #endregion
            return strSql;
        }
        /// <summary>
        /// // MWW 2023/3/23 已上傳最終卷
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <param name="FinishTime"></param>
        /// <param name="In_Coil_ID">母捲號</param>
        /// <param name="Plan_No">計劃號</param>
        /// <returns></returns>
        public static string SQL_Select_UploadEndFlag(string In_Coil_ID, string Plan_No)
        {
            string strSql = $"Select [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Finish_Time)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Plan_No)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Final_Coil_Flag)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Fixed_Wt_Flag)}] " +
                            $"From [{nameof(PDOEntity.TBL_PDO)}] " +
                            $"Where [{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] ='{In_Coil_ID}' " +
                            $"AND [{nameof(PDOEntity.TBL_PDO.Plan_No)}] ='{Plan_No}' " +
                            $"AND [{nameof(PDOEntity.TBL_PDO.Final_Coil_Flag)}] ='{1}'" +
                            $"AND [{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] ='{1}'";
            return strSql;
        }
        /// <summary>
        /// MWW 2023/3/23 已上抛過的子卷
        /// </summary>
        /// <param name="In_Coil_ID">母捲號</param>
        /// <param name="Plan_No">計劃號</param>
        /// <returns></returns>
        public static string SQL_Select_SubvolumeCoil(string In_Coil_ID, string Plan_No)
        {
            string strSql = $"Select [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Finish_Time)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Plan_No)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Final_Coil_Flag)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Fixed_Wt_Flag)}] " +
                            $"From [{nameof(PDOEntity.TBL_PDO)}] " +
                            $"Where [{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] ='{In_Coil_ID}' " +
                            $"AND [{nameof(PDOEntity.TBL_PDO.Plan_No)}] ='{Plan_No}' " +
                            $"AND [{nameof(PDOEntity.TBL_PDO.Fixed_Wt_Flag)}] ='{1}' " +
                            $"AND [{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] ='{1}' ";
            //
            //$"AND  [{ nameof(PDOEntity.TBL_PDO.FinishTime)}] = '{FinishTime}'" +
            return strSql;
        }
        #endregion

        #region Frm_3_3

        public static string Frm_3_3_SelectPDO(string strCoil_ID = "", string strPlan_No = "")
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine($" Select * ");
            sbSql.AppendLine($" From  [{nameof(PDOEntity.TBL_PDO)}] ");
            sbSql.AppendLine($" Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{strCoil_ID}' ");
            sbSql.AppendLine($" AND   [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{strCoil_ID}' ");
            sbSql.AppendLine($" AND   [{nameof(PDOEntity.TBL_PDO.Plan_No)}] = '{strPlan_No}' ");
           // sbSql.AppendLine($"  ");
       
            string strSql = sbSql.ToString();

            return strSql;
        }

        public static string Frm_3_3_SelectPassSection(string Coil_ID)
        {
            string strSql = $@"Select distinct PDO.[{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}],
                               GrindPlan.[{nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)}],
                               GrindPlan.[{nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)}],
                               GrindPlan.[{nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)}]
                        From [{nameof(PDOEntity.TBL_PDO)}] PDO
                        Left join [{nameof(PDIEntity.TBL_PDI)}] PDI on PDO.[{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = PDI.[{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)}]
                        Left join [{nameof(GradeGroupsEntity.TBL_GradeGroups)}] Grade on Grade.[{nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)}] = PDI.[{nameof(PDIEntity.TBL_PDI.Order_Cust_Code)}] 
                        and Grade.[{nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)}] = PDO.[{nameof(PDOEntity.TBL_PDO.St_No)}]
                        Left join [{nameof(GrindPlanEntity.TBL_GrindPlan)}] GrindPlan on GrindPlan.[{nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)}] = Grade.[{nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)}]
                        and PDI.[{nameof(PDIEntity.TBL_PDI.In_Coil_Thick)}] between GrindPlan.[{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)}] and  GrindPlan.[{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)}]
                        Where PDO.[{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }
        public static string Frm_3_3_SelectPassCount(string Coil_ID, string Senssion)
        {
            string strSql = $@"Select max([{nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)}])Current_Pass From [{nameof(GrindRecordsEntity.TBL_GrindRecords)}]
                               Where [{nameof(GrindRecordsEntity.TBL_GrindRecords.Coil_ID)}] = '{Coil_ID}'
                               and [{nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)}] = '{Senssion}'";
            return strSql;
        }
        public static string Frm_3_3_SelectCurrent_Senssion_DB_GrindRecords(string Coil_ID, string Plan_No)
        {
            string strSql = "";
            strSql = $@"Select distinct [{nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)}]
                        From [{nameof(GrindRecordsEntity.TBL_GrindRecords)}]
                        Where [{nameof(GrindRecordsEntity.TBL_GrindRecords.Coil_ID)}] = '{Coil_ID}'
                        and [{nameof(GrindRecordsEntity.TBL_GrindRecords.Plan_No)}] = '{Plan_No}'
                        Order by [{nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)}] asc";
            return strSql;
        }
        public static string Frm_3_3_SelectPassNumberRecords_DB_GrindRecords(string Coil_ID,string Section)
        {
            string strSql = "";
            strSql = $@"Select distinct [{nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)}]
                        From [{nameof(GrindRecordsEntity.TBL_GrindRecords)}]
                        Where [{nameof(GrindRecordsEntity.TBL_GrindRecords.Coil_ID)}] = '{Coil_ID}'
                        and [{nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)}] = '{Section}'
                        Order by [{nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)}] asc";
            return strSql;
        }
        public static string Frm_3_3_SelectGRPresetData_Current_DB_GrindRecords(string Coil_ID, string Section, string PassNumber)
        {
            string strSql = "";
            strSql = $@"Select [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_MOTOR_CURRENT)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_MOTOR_CURRENT)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_MOTOR_CURRENT)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_MOTOR_CURRENT)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_MOTOR_CURRENT)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_MOTOR_CURRENT)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_SPEED)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_SPEED)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.Receive_Time)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.Line_Speed)}]
                        From [{nameof(GrindRecordsEntity.TBL_GrindRecords)}]
                        Where [{nameof(GrindRecordsEntity.TBL_GrindRecords.Coil_ID)}] = '{Coil_ID}'
                        and [{nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)}] = '{Section}'
                        and [{nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)}] = '{PassNumber}'";
            return strSql;
        }
        public static string Frm_3_3_SelectGRBeltData_DB_GrindRecords(string Coil_ID, string Section, string PassNumber)
        {
            string strSql = "";
            strSql = $@"Select distinct 
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_KIND)}],[{nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_PARTICLE_NO)}],[{nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_ROTATE_DIR)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_KIND)}],[{nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_PARTICLE_NO)}],[{nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_ROTATE_DIR)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_BELT_KIND)}],[{nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_BELT_PARTICLE_NO)}],[{nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_BELT_ROTATE_DIR)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_BELT_KIND)}],[{nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_BELT_PARTICLE_NO)}],[{nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_BELT_ROTATE_DIR)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_BELT_KIND)}],[{nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_BELT_PARTICLE_NO)}],[{nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_BELT_ROTATE_DIR)}],
                               [{nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_BELT_KIND)}],[{nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_BELT_PARTICLE_NO)}],[{nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_BELT_ROTATE_DIR)}]
                          From [{nameof(GrindRecordsEntity.TBL_GrindRecords)}]
                         Where [{nameof(GrindRecordsEntity.TBL_GrindRecords.Coil_ID)}] = '{Coil_ID}'
                           and [{nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)}] = '{Section}'
                           and [{nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)}] = '{PassNumber}'";
            return strSql;
        }
        #endregion

        #region Frm_4_1
        public static string DBHandler_Select_DownTime_WithCondition()
        {
            string strSql = $@"Select 
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)}],
                                    CONVERT(varchar,[{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}],121) stop_start_time,
                                    CONVERT(varchar,[{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}],121) stop_end_time,
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc)}],
                                    --[{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}],
                                    CONVERT(float, [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}]) as [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_category)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_cause)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)}]
                                 From [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}] 
                                Where [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}]
                              Between '{LineDelayRecord.Dtp_Start_Time.Value:yyyy-MM-dd} 00:00:00' 
                                  And '{LineDelayRecord.Dtp_Finish_Time.Value:yyyy-MM-dd} 23:59:59'";

            // 六分鐘內的停復機紀錄要過濾掉
            strSql += $@"  AND ([{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] = ''  OR ( [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] != '' AND [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] != '' AND CAST([{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}] AS float) >= {LineDelayRecord.Nud_Stop_Time.Value}   ))  ";


            if (LineDelayRecord.Chk_shift_no.Checked)
                strSql += $@" And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)}] = '{LineDelayRecord.Cob_shift_no.SelectedValue}'";
            
            //if (LineDelayRecord.Chk_shift_group.Checked)
            //    strSql += $@" And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)}] = '{LineDelayRecord.Cob_shift_group.SelectedValue}'";

            if (LineDelayRecord.Chk_SendMMS.Checked && LineDelayRecord.Rdb_SendMMS.Checked)
                strSql += $@" And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)}] = 'Y'";
            else if (LineDelayRecord.Chk_SendMMS.Checked && LineDelayRecord.Rdb_UnSendMMS.Checked)
                strSql += $@" And ([{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)}] = 'N' or [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)}] is null )";

            strSql += $@" Order By [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] desc";

            return strSql;
        }

        public static string DBHandler_Select_DownTime_Check(string strProd_time, string strStart_time, string strEnd_time, string strShift_no, string strShift_group)
        {
            string strSql = $@"Select 
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)}],
                                    CONVERT(varchar,[{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}],121) stop_start_time,
                                    CONVERT(varchar,[{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}],121) stop_end_time,
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_category)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_cause)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code)}],
                                    [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)}]
                                 From [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}] 
                                WHERE  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)}] = N'{strProd_time}'
                                  AND  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)}] = N'{strShift_no}' 
                                  AND  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)}] = N'{strShift_group}' 
                                  AND  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] = N'{strStart_time}' 
                                  AND  [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] = N'{strEnd_time}' 
                                ";



            //// 六分鐘內的停復機紀錄要過濾掉
            // strSql += $@"  AND ([{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] = ''  OR ( [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] != '' AND [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] != '' AND CAST([{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}] AS float) >= 6   ))  ";


            if (LineDelayRecord.Chk_shift_no.Checked)
                strSql += $@" And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)}] = '{LineDelayRecord.Cob_shift_no.SelectedValue}'";

            //if (LineDelayRecord.Chk_shift_group.Checked)
            //    strSql += $@" And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)}] = '{LineDelayRecord.Cob_shift_group.SelectedValue}'";

            strSql += $@" Order By [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] desc";

            return strSql;
        }

        public static string Frm_4_1_SelectDelayLocation()
        {
            //string strSql = $@"Select * 
            //                     From [{nameof(DelayLocationModel.TBL_DelayLocation_Definition)}] 
            //                 Order by [{nameof(DelayLocationModel.TBL_DelayLocation_Definition.Index_No)}] asc";
            string strSql = $@"Select [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)}],
                               [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)}] as {nameof(TBL_ComboBoxItems.Cbo_Value)},
                               '[' + [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)}] + ']' + [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)}] as {nameof(TBL_ComboBoxItems.Cbo_Text)}
                               From [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition)}] Order by [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)}] asc ";

            return strSql;
        }
        public static string Frm_4_1_SelectDelayReasonCode()
        {
            //string strSql = $@"Select * 
            //                     From [{nameof(DelayReasonCodeModel.TBL_DelayReasonCode_Definition)}] 
            //                 Order by [{nameof(DelayReasonCodeModel.TBL_DelayReasonCode_Definition.Index_No)}] asc";
            string strSql = $@"Select [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)}],
                               [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)}] as {nameof(TBL_ComboBoxItems.Cbo_Value)},
                               '[' +  [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)}] +']' + [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)}] as {nameof(TBL_ComboBoxItems.Cbo_Text)}
                               From [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition)}] 
                               Order by [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)}] asc ";
            return strSql;
        }
        public static string Frm_4_1_SelectFaultCode()
        {
            string strSql = $@"Select * 
                                 From [{nameof(TBL_FaultCode)}] 
                             Order by [{nameof(TBL_FaultCode.Sequence_No)}] asc";
            
            return strSql;
        }

        #region --- 保留 ---
        public static string Frm_4_1_InsertDelayRecord()
        {
            string strSql = $@"Insert into [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}]
                                          ([{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs)}],

                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_cause)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code)}],
                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)}],

                                           [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.CreateTime)}])
                                    Values
                                          ('{LineDelayRecord.Txt_UnitCode.Text}',
                                           '{LineDelayRecord.Dtp_prod_time.Value:yyyy-MM-dd}',
                                           '{LineDelayRecord.Cob_prod_shift_no.SelectedValue}',
                                           '{LineDelayRecord.Cob_prod_shift_group.SelectedValue}',
                                           '{LineDelayRecord.Dtp_stop_start_time.Value:yyyy-MM-dd HH:mm:ss}',
                                           '{LineDelayRecord.Dtp_stop_end_time.Value:yyyy-MM-dd HH:mm:ss}',
                                           '{LineDelayRecord.Cob_delay_location.SelectedValue}',
                                           '{Fun_GetDesc(LineDelayRecord.Cob_delay_location.Text, "]")}',
                                           '{LineDelayRecord.Txt_Stop_Elased_Time.Text}',
                                           '{LineDelayRecord.Cob_delay_reason_code.SelectedValue}',
                                           '{Fun_GetDesc(LineDelayRecord.Cob_delay_reason_code.Text, "]")}',
                                           '{LineDelayRecord.Txt_Remark.Text}',
                                           '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_m.Text}',
                                           '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_e.Text}',
                                           '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_c.Text}',
                                           '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_p.Text}',
                                           '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_u.Text}',
                                           '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_o.Text}',
                                           '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_r.Text}',
                                           '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_rs.Text}',

                                           '{Fun_GetDesc(LineDelayRecord.Cob_deceleration_code.Text, "]")}',
                                           '{LineDelayRecord.Cob_deceleration_code.SelectedValue}',
                                           'N',

                                           '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'
                                          )";
            return strSql;
        }
        public static string Frm_4_1_DeleteDelayRecord()
        {
            string strSql = $@"Delete From [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}] 
                                     Where [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)}] = '{LineDelayRecord.Dtp_prod_time.Value:yyyy-MM-dd}'
                                       And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] = '{LineDelayRecord.Dtp_stop_start_time.Value:yyyy-MM-dd HH:mm:ss}'";
            return strSql;
        }
        #endregion
        public static string Frm_4_1_UpdateDelayRecord(DataRow drGetEditRow)
        {
            string strSql = $@"Update [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords)}] Set 
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code)}] = '{LineDelayRecord.Txt_UnitCode.Text}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)}] = '{LineDelayRecord.Dtp_prod_time.Value:yyyy-MM-dd}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)}] = '{LineDelayRecord.Cob_prod_shift_no.SelectedValue}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)}] = '{LineDelayRecord.Cob_prod_shift_group.SelectedValue}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] = '{DateTime.Parse(Convert.ToString(drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)])):yyyy-MM-dd HH:mm:ss.fff}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] = '{DateTime.Parse(Convert.ToString(drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)])):yyyy-MM-dd HH:mm:ss.fff}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location)}] = '{LineDelayRecord.Cob_delay_location.SelectedValue}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc)}] = N'{Fun_GetDesc(LineDelayRecord.Cob_delay_location.Text)}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey)}] = '{LineDelayRecord.Txt_Stop_Elased_Time.Text}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code)}] = '{LineDelayRecord.Cob_delay_reason_code.SelectedValue}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc)}] = N'{Fun_GetDesc(LineDelayRecord.Cob_delay_reason_code.Text)}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark)}] = N'{LineDelayRecord.Txt_Remark.Text}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m)}] = '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_m.Text}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e)}] = '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_e.Text}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c)}] = '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_c.Text}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p)}] = '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_p.Text}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u)}] = '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_u.Text}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o)}] = '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_o.Text}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r)}] = '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_r.Text}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs)}] =  '{LineDelayRecord.Txt_Resp_Depart_Delay_Time_rs.Text}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_cause)}] = N'{Fun_GetDesc(LineDelayRecord.Cob_deceleration_code.Text)}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code)}] = N'{LineDelayRecord.Cob_deceleration_code.SelectedValue}',
                                      [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UpdateTime)}] = '{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}'
                                Where [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] = '{DateTime.Parse(Convert.ToString(drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)])):yyyy-MM-dd HH:mm:ss.fff}'
                                  And [{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] = '{DateTime.Parse(Convert.ToString(drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)])):yyyy-MM-dd HH:mm:ss.fff}' ";


            //[{ nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)}] = '{LineDelayRecord.Dtp_stop_start_time.Value:yyyy-MM-dd HH:mm:ss}.{LineDelayRecord.Dtp_stop_start_time.Value.Millisecond}',
            //                          [{ nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)}] = '{LineDelayRecord.Dtp_stop_end_time.Value:yyyy-MM-dd HH:mm:ss}.{LineDelayRecord.Dtp_stop_end_time.Value.Millisecond}',
            return strSql;
        }
      

        public static string Fun_StringCut(string getString)
        {
            int intLocation = getString.IndexOf("-");
            getString = getString.Substring(0, intLocation);
            return getString;
        }
        public static string Fun_StringDescCut(string getString)
        {
            int intLocation = getString.IndexOf("-") + 1;
            string str = getString.Substring(0, intLocation );;
            getString = getString.Replace(str,"");
            return getString;
        }
        public static string Fun_GetDesc(string getString, string strCutValue = "]")
        {
            int intLocation = getString.IndexOf(strCutValue);

            getString = getString.Substring(intLocation + 1);
            return getString;
        }

        #endregion

        #region Frm_4_2
        public static string Frm_4_2_SelectUnity_Team()
        {
            string strSql = $@"Select CONVERT(varchar,[{nameof(UtilityEntity.TBL_Utility.Receive_Time)}],120)Receive_Time,
                                   [{nameof(UtilityEntity.TBL_Utility.Shift)}],
                                   [{nameof(UtilityEntity.TBL_Utility.Team)}],
                                   [{nameof(UtilityEntity.TBL_Utility.IndirectCoolingWater)}],
                                   [{nameof(UtilityEntity.TBL_Utility.RinseWater)}],
                                   [{nameof(UtilityEntity.TBL_Utility.CompressedAir)}],
                                   [{nameof(UtilityEntity.TBL_Utility.Steam)}]
                              From [{nameof(UtilityEntity.TBL_Utility)}]
                             Where [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] >= '{PublicForms.Utility.Dtp_DateShitf.Value:yyyy-MM-dd}' 
                               And [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] < '{PublicForms.Utility.Dtp_DateShitf.Value.AddDays(1):yyyy-MM-dd}' 
                       
                               And [{nameof(UtilityEntity.TBL_Utility.Shift)}] = '{PublicForms.Utility.Cob_Shift_S.SelectedValue}'
                             Order by [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] desc";
            return strSql;

            //      Where [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] Between '{StartTime}' and '{EndTime}' 
        }
        public static string Frm_4_2_SelectUnity_Date()
        {
            string strSql = $@"Select CONVERT(varchar,[{nameof(UtilityEntity.TBL_Utility.Receive_Time)}],120)Receive_Time,
                                   [{nameof(UtilityEntity.TBL_Utility.IndirectCoolingWater)}],
                                   [{nameof(UtilityEntity.TBL_Utility.RinseWater)}],
                                   [{nameof(UtilityEntity.TBL_Utility.CompressedAir)}],
                                   [{nameof(UtilityEntity.TBL_Utility.Steam)}]
                              From [{nameof(UtilityEntity.TBL_Utility)}]
                             Where [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] Between '{Utility.Dtp_DateStart.Value:yyyy-MM-dd HH:mm:ss}' and '{Utility.Dtp_DateEnd.Value:yyyy-MM-dd HH:mm:ss}'
                             Order by [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] desc";
            return strSql;
        }
        public static string Frm_4_2_SelectTotal()
        {
            string strSql = $@"Select sum([{nameof(UtilityEntity.TBL_Utility.CompressedAir)}]) sumCompressedAir ,
                                      sum([{nameof(UtilityEntity.TBL_Utility.IndirectCoolingWater)}]) sumIndirectCoolingWater,
                                      sum([{nameof(UtilityEntity.TBL_Utility.Steam)}]) sumSteam,
                                      sum([{nameof(UtilityEntity.TBL_Utility.RinseWater)}]) sumRinseWater
                                 From [{nameof(UtilityEntity.TBL_Utility)}]
                                Where [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] Between '{Utility.Dtp_DateStart.Value:yyyy-MM-dd HH:mm:ss}' and '{Utility.Dtp_DateEnd.Value:yyyy-MM-dd HH:mm:ss}'";
            return strSql;
        }
        public static string Frm_4_2_InsertUtility()
        {
            string strSql = $@"Insert into [{nameof(UtilityEntity.TBL_Utility)}]
                                          ([{nameof(UtilityEntity.TBL_Utility.Receive_Time)}],
                                           [{nameof(UtilityEntity.TBL_Utility.CompressedAir)}],
                                           [{nameof(UtilityEntity.TBL_Utility.Steam)}],
                                           [{nameof(UtilityEntity.TBL_Utility.RinseWater)}],
                                           [{nameof(UtilityEntity.TBL_Utility.IndirectCoolingWater)}])
                                    Values
                                          ('{Instance.getTime}',
                                           '{Utility.Txt_Steam.Text}',
                                           '{Utility.Txt_RinseWater.Text}',
                                           '{Utility.Txt_CoolingWater.Text}')";
            return strSql;
        }
        public static string Frm_4_2_UpdateUtility()
        {
            string strSql = $@"Update [{nameof(UtilityEntity.TBL_Utility)}] Set
                                      [{nameof(UtilityEntity.TBL_Utility.CompressedAir)}] = '{Utility.Txt_ComeAir.Text}',
                                      [{nameof(UtilityEntity.TBL_Utility.IndirectCoolingWater)}] = '{Utility.Txt_CoolingWater.Text}',
                                      [{nameof(UtilityEntity.TBL_Utility.Steam)}] = '{Utility.Txt_Steam.Text}',
                                      [{nameof(UtilityEntity.TBL_Utility.RinseWater)}] = '{Utility.Txt_RinseWater.Text}'
                                Where [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] = '{Utility.Dtp_UtilityDate.Value:yyyy-MM-dd HH:mm:ss}'";
            return strSql;
        }
        public static string Frm_4_2_DeleteUtility()
        {
            string strSql = $@"Delete From [{nameof(UtilityEntity.TBL_Utility)}]
                                     Where [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] = '{Utility.Dtp_UtilityDate.Value:yyyy-MM-dd HH:mm:ss}'";
            return strSql;
        }

        public static string Frm_4_2_SearchWorkSchedule()
        {
            string strSql = $@"Select * From [{nameof(WorkScheduleEntity.TBL_WorkSchedule)}] 
                                       Where [{nameof(WorkScheduleEntity.TBL_WorkSchedule.Shift)}] = '{Utility.Cob_Shift_S.SelectedValue}'
                                         And [{nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftDate)}] = '{Utility.Dtp_DateShitf.Value:yyyyMMdd}'";

            //string strSql = $@" Select convert(char(23), [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}], 121) Receive_Time,
            //                       [{nameof(UtilityEntity.TBL_Utility.Shift)}],
            //                       [{nameof(UtilityEntity.TBL_Utility.Team)}],
            //                       [{nameof(UtilityEntity.TBL_Utility.CompressedAir)}],
            //                       [{nameof(UtilityEntity.TBL_Utility.IndirectCoolingWater)}]
            //                  From [{nameof(UtilityEntity.TBL_Utility)}]
            //                 Where [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] >= '{PublicForms.Utility.Dtp_DateShitf.Value:yyyy-MM-dd}' 
            //                   And [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] < '{PublicForms.Utility.Dtp_DateShitf.Value.AddDays(1):yyyy-MM-dd}' 
            //                   And [{nameof(UtilityEntity.TBL_Utility.Shift)}] = '{PublicForms.Utility.Cob_Shift_S.SelectedValue}' ";

            return strSql;
        }

        #endregion

        #region Frm_4_3

        /// <summary>
        /// 整平機
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_3_SelectFlattener_DB_TBL_LookupTable_Flattener()
        {
            string strSql = $@"Select [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)}],
                                      [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)}],
                                      [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)}],
                                      [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Max)}],
                                      [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Min)}],
                                      [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)}],
                                      [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)}],
                                      CONVERT(varchar,[{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)}],121)UpdateTime
                                 From [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener)}] ";

            if (DeviceParameters.bolSearch)
            {
                strSql += $"Where [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)}] = '{DeviceParameters.Cob_SteelGrade.Text}' ";
                if (DeviceParameters.Chk_Thick.Checked)
                {
                    if (!DeviceParameters.Txt_Thick_Min.Text.Trim().Equals(string.Empty))
                        strSql += $" And [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)}] >= '{DeviceParameters.Txt_Thick_Min.Text}' ";
                    if (!DeviceParameters.Txt_Thick_Max.Text.Trim().Equals(string.Empty))
                        strSql += $" And [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)}] <= '{DeviceParameters.Txt_Thick_Max.Text}' ";
                }
            }
            strSql += $@"Order by [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)}] asc";
            return strSql;
        }

        /// <summary>
        /// 整平机-新增
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_3_InsertFlattener()
        {
            string strSql = $@"Insert into [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener)}]
                                          ([{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)}],
                                           [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)}],
                                           [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)}],
                                           [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Max)}],
                                           [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Min)}],
                                           [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)}],
                                           [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)}],
                                           [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)}])
                                    Values
                                          ('{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[0].Value.ToString().Trim()}',
                                           '{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[1].Value.ToString().Trim()}',
                                           '{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[2].Value.ToString().Trim()}',
                                           '{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[3].Value.ToString().Trim()}',
                                           '{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[4].Value.ToString().Trim()}',
                                           '{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[5].Value.ToString().Trim()}',
                                           '{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[6].Value.ToString().Trim()}',
                                           '{Instance.getTime}')";
            return strSql;
        }
        /// <summary>
        /// 整平机-修改
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_3_UpdateFlattener()
        {
            string strSql = "";
            strSql = $@"Update [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener)}]
                           Set [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)}] = '{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[0].Value.ToString().Trim()}',
                               [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)}] = '{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[1].Value.ToString().Trim()}',
                               [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)}] = '{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[2].Value.ToString().Trim()}',
                               [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Max)}] = '{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[3].Value.ToString().Trim()}',
                               [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Min)}] = '{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[4].Value.ToString().Trim()}',
                               [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)}] = '{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[5].Value.ToString().Trim()}',
                               [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)}] = '{DeviceParameters.Dgv_FlattenerCurrentRow.Rows[0].Cells[6].Value.ToString().Trim()}',
                               [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)}] = '{Instance.getTime}'
                         Where [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)}] = '{DeviceParameters.drGetFlattenerRow[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)]}'
                           and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)}] = '{DeviceParameters.drGetFlattenerRow[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)]}'
                           and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)}] = '{DeviceParameters.drGetFlattenerRow[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)]}'
                           and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Max)}] = '{DeviceParameters.drGetFlattenerRow[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Max)]}'
                           and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Min)}] = '{DeviceParameters.drGetFlattenerRow[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Min)]}'
                           and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)}] = '{DeviceParameters.drGetFlattenerRow[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)]}'
                           and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)}] = '{DeviceParameters.drGetFlattenerRow[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)]}'
                           and [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)}] = '{DeviceParameters.drGetFlattenerRow[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)]}'";
            return strSql;
        }
        /// <summary>
        /// 整平機-刪除
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_3_DeleteFlattener(DataRow dr)
        {
            string strSql = $@"Delete From [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener)}] 
                                     Where [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade)]}'
                                       And [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max)]}'
                                       And [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min)]}'
                                       And [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Max)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Max)]}'
                                       And [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Min)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Strip_Yield_Stress_Min)]}'
                                       And [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num1)]}'
                                       And [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Intermesh_Num2)]}'
                                       And [{nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)}] = '{dr[nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.UpdateTime)]}'";
            return strSql;
        }

        /// <summary>
        /// 張力機
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_3_SelectTension(string strProcessType)
        {
            string strSql = "";
            strSql = $@"Select [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}]
                              ,[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Min)}]
                              ,[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Max)}]
                              ,[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}]
                              ,[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}]
                              ,[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)}]
                              ,[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.LineSpeed)}]
                              ,CONVERT(varchar,[{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)}],121)UpdateTime
                        From [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension)}] 
                       Where [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.ProcessType)}] = '{strProcessType}' ";
            if (DeviceParameters.bolSearch)
            {
                strSql += $"And [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}] = '{DeviceParameters.Cob_SteelGrade.Text}' ";
                if (DeviceParameters.Chk_Thick.Checked)
                {
                    if (!DeviceParameters.Txt_Thick_Min.Text.Trim().Equals(string.Empty))
                        strSql += $" And [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}] >= '{DeviceParameters.Txt_Thick_Min.Text}' ";
                    if (!DeviceParameters.Txt_Thick_Max.Text.Trim().Equals(string.Empty))
                        strSql += $" And [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}] <= '{DeviceParameters.Txt_Thick_Max.Text}' ";
                }
            }

            strSql += $"Order by [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.ProcessType)}] asc";
            return strSql;
        }

        public static string Frm_4_3_InsertTension()
        {
            string strProcess = Fun_TensionProcessType();
            string strSql = "";
            strSql = $@"Insert into [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension)}]
                               ([{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}],
                                [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Min)}],
                                [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Max)}],
                                [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}],
                                [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}],
                                [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)}],
                                [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.LineSpeed)}],
                                [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.ProcessType)}],
                                [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)}])
                        Values
                              ('{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[0].Value.ToString().Trim()}',
                               '{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[1].Value.ToString().Trim()}',
                               '{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[2].Value.ToString().Trim()}',
                               '{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[3].Value.ToString().Trim()}',
                               '{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[4].Value.ToString().Trim()}',
                               '{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[4].Value.ToString().Trim()}',
                               '{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[4].Value.ToString().Trim()}',
                               '{strProcess}',
                               '{Instance.getTime}')";
            return strSql;
        }
        public static string Frm_4_3_UpdateTension()
        {
            DataRow dr = Fun_TensionData();
            string strProcess = Fun_TensionProcessType();
            string strSql = "";
            strSql = $@"Update [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension)}]
                           Set [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}] = '{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[0].Value.ToString().Trim()}',
                               [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Min)}] = '{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[1].Value.ToString().Trim()}',
                               [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Max)}] = '{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[2].Value.ToString().Trim()}',
                               [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}] = '{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[3].Value.ToString().Trim()}',
                               [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}] = '{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[4].Value.ToString().Trim()}',
                               [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)}] = '{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[5].Value.ToString().Trim()}',
                               [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.LineSpeed)}] = '{DeviceParameters.Dgv_TensionCurrentRow.Rows[0].Cells[6].Value.ToString().Trim()}',
                               [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)}] = '{Instance.getTime}'

                         Where [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Min)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Min)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Max)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Max)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.LineSpeed)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.LineSpeed)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.ProcessType)}] = '{strProcess}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)]}'";
            return strSql;
        }
        public static string Frm_4_3_DeleteTension()
        {
            DataRow dr = Fun_TensionData();
            string strProcess = Fun_TensionProcessType();
            string strSql = $@"Delete From [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension)}]
                         Where [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Min)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Min)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Max)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width_Max)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.LineSpeed)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.LineSpeed)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)]}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.ProcessType)}] = '{strProcess}'
                           and [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)}] = '{dr[nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.UpdateTime)]}'";
            return strSql;
        }
        public static string Fun_TensionProcessType()
        {
            string Process = string.Empty;
            if (DeviceParameters.Tab_TensionControl.SelectedTab == DeviceParameters.Tab_TensionColdRoll_Page)
                Process = "C";
            else if (DeviceParameters.Tab_TensionControl.SelectedTab == DeviceParameters.Tab_TensionHotRoll_Page)
                Process = "H";
            return Process;
        }
        public static DataRow Fun_TensionData()
        {
            DataRow dr = null;
            if (DeviceParameters.Tab_TensionControl.SelectedTab == DeviceParameters.Tab_TensionColdRoll_Page)
                dr = DeviceParameters.dtGetTensionColdRolled.Rows[DeviceParameters.Dgv_ColdRolled.CurrentRow.Index];
            else if(DeviceParameters.Tab_TensionControl.SelectedTab == DeviceParameters.Tab_TensionHotRoll_Page)
                dr = DeviceParameters.dtGetTensionHotRolled.Rows[DeviceParameters.Dgv_HotRolled.CurrentRow.Index];
            return dr;
        }

        public static string Frm_4_3_SelectSteelGrade()
        {
            string strSql = $@"Select distinct [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)}] From [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade)}]";
            return strSql;
        }

        #endregion

        #region Frm_4_4

        /// <summary>
        /// 客戶品質要求表
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_4_SelectGradeGroup_DB_GradeGroups()
        {
            string strSql = "";
            strSql = $"Select * From [{nameof(GradeGroupsEntity.TBL_GradeGroups)}] ";

            if (!GrindPlan.Cob_SteelGrade.Text.Trim().Equals(string.Empty) || !GrindPlan.Cob_Customer.Text.Trim().Equals(string.Empty) || !GrindPlan.Cob_Group.Text.Trim().Equals(string.Empty))
            {
                strSql += " Where ";
                strSql = !GrindPlan.Cob_SteelGrade.Text.Trim().Equals(string.Empty) ? strSql += $" [{nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)}] ='{GrindPlan.Cob_SteelGrade.Text}' " : strSql;
                strSql = (!GrindPlan.Cob_SteelGrade.Text.Trim().Equals(string.Empty) && !GrindPlan.Cob_Customer.Text.Trim().Equals(string.Empty)) ? strSql += " and " : strSql;
                strSql = !GrindPlan.Cob_Customer.Text.Trim().Equals(string.Empty) ? strSql += $" [{nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)}] ='{GrindPlan.Cob_Customer.Text}' " : strSql;
                strSql = (!GrindPlan.Cob_Group.Text.Trim().Equals(string.Empty) && !GrindPlan.Cob_Customer.Text.Trim().Equals(string.Empty)) ? strSql += " and " : strSql;
                strSql = !GrindPlan.Cob_Group.Text.Trim().Equals(string.Empty) ? strSql += $" [{nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)}] ='{GrindPlan.Cob_Group.Text}' " : strSql;
            }

            return strSql;
        }
        /// <summary>
        /// 客戶品質要求表-ComboBox選項
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_4_SelectGradeGroupComboBox_DB_GradeGroups()
        {
            string strSql = "";
            strSql = $@"Select distinct [{nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)}],
                                        [{nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)}],
                                        [{nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)}] 
                        From [{nameof(GradeGroupsEntity.TBL_GradeGroups)}] 
                        Order by  [{nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)}] asc";
            return strSql;
        }
        /// <summary>
        /// 客戶品質要求表-新增
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_4_InsertGradeGroup_DB_GradeGroups()
        {
            string strSql = "";
            strSql = $@"Insert into [{nameof(GradeGroupsEntity.TBL_GradeGroups)}]
                                   ([{nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)}]
                                   ,[{nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)}]
                                   ,[{nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)}])
                            Values (
                                    '{GrindPlan.Txt_SteelGrade.Text}',
                                    '{GrindPlan.Txt_Customer.Text}',
                                    '{GrindPlan.Txt_Group.Text}')";
            return strSql;
        }

        /// <summary>
        /// 客戶品質要求表-修改
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_4_UpdateGradeGroup_DB_GradeGroups()
        {
            string strSql = "";
            strSql = $@"Upate [{nameof(GradeGroupsEntity.TBL_GradeGroups)}] Set
                                     [{nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)}] = '{GrindPlan.Txt_SteelGrade.Text}',
                                     [{nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)}] = '{GrindPlan.Txt_Customer.Text}',
                                     [{nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)}] = '{GrindPlan.Txt_Group.Text}'
                              Where  [{nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)}] = '{GrindPlan.Dgv_GradeGroup.CurrentRow.Cells[0].Value}'
                                and  [{nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)}] = '{GrindPlan.Dgv_GradeGroup.CurrentRow.Cells[1].Value}'   
                                and  [{nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)}] = '{GrindPlan.Dgv_GradeGroup.CurrentRow.Cells[2].Value}'";
            return strSql;
        }

        /// <summary>
        /// 研磨計畫
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_4_SelectGrindPlan_DB_GrindPlan()
        {
            string strSql = "";
            strSql = $@"Select distinct [{nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)}],
                                        [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)}],
                                        [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)}] 
                        From [{nameof(GrindPlanEntity.TBL_GrindPlan)}] ";
            if (!GrindPlan.Cob_GradeGroup_S.Text.Trim().Equals(string.Empty) || !GrindPlan.Txt_ThicknessFrom_S.Text.Trim().Equals(string.Empty) || !GrindPlan.Txt_ThicknessTo_S.Text.Trim().Equals(string.Empty))
            {
                strSql += " Where ";
                strSql = !GrindPlan.Cob_GradeGroup_S.Text.Trim().Equals(string.Empty) ? strSql += $" [{nameof(TBL_GrindPlan.GradeGroup)}] ='{GrindPlan.Cob_GradeGroup_S.Text}' " : strSql;
                strSql = (!GrindPlan.Cob_GradeGroup_S.Text.Trim().Equals(string.Empty) && !GrindPlan.Txt_ThicknessFrom_S.Text.Trim().Equals(string.Empty)) ? strSql += " and " : strSql;
                strSql = !GrindPlan.Txt_ThicknessFrom_S.Text.Trim().Equals(string.Empty) ? strSql += $" [{nameof(TBL_GrindPlan.Thickness_From)}] ='{GrindPlan.Txt_ThicknessFrom_S.Text}' " : strSql;
                strSql = (!GrindPlan.Txt_ThicknessTo_S.Text.Trim().Equals(string.Empty) && !GrindPlan.Txt_ThicknessFrom_S.Text.Trim().Equals(string.Empty)) || (!GrindPlan.Txt_ThicknessTo_S.Text.Trim().Equals(string.Empty) && !GrindPlan.Cob_GradeGroup_S.Text.Trim().Equals(string.Empty)) ? strSql += " and " : strSql;
                strSql = !GrindPlan.Txt_ThicknessTo_S.Text.Trim().Equals(string.Empty) ? strSql += $" [{nameof(TBL_GrindPlan.Thickness_To)}] ='{GrindPlan.Txt_ThicknessTo_S.Text}' " : strSql;
            }

            return strSql;
        }

      
        /// <summary>
        ///  研磨計畫-詳細資料
        /// </summary>
        /// <param name="BeltPattern"></param>
        /// <param name="Pass_From"></param>
        /// <param name="Pass_To"></param>
        /// <returns></returns>
        public static string Frm_4_4_GrindPlanSelectionChange_DB_GrindPlan(string GradeGroup, string Thickness_From, string Thickness_To)
        {
            string strSql = "";
            strSql = $@" Select * 
                         From [{nameof(GrindPlanEntity.TBL_GrindPlan)}]
                         Where [{nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)}] ='{GradeGroup}'
                         and [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)}] = {Thickness_From} 
                         and [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)}] = {Thickness_To}";
            return strSql;
        }
        /// <summary>
        /// 研磨計畫-新增
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_4_InsertGrindPlan_DB_GrindPlan()
        {
            string strSql = "";
            #region SQL
            strSql = $@"Insert into [{nameof(GrindPlanEntity.TBL_GrindPlan)}]
                                  ( [{nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)}],
                                    [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)}],
                                    [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)}],
                                    [{nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)}],
                                    [{nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)}],
                                    [{nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)}],
                                    [{nameof(GrindPlanEntity.TBL_GrindPlan.LineSpeed)}])
                           Values ( '{GrindPlan.Cob_GradeGroup.Text}',
                                    '{GrindPlan.Txt_Thickness_From.Text}',
                                    '{GrindPlan.Txt_Thickness_To.Text}',
                                    'H',
                                    '{GrindPlan.Cob_H_BeltPattern.Text}',
                                    '{GrindPlan.Txt_H_PassNumber.Text}',
                                    '{GrindPlan.Txt_H_LineSpeed.Text}'),
                                  ( '{GrindPlan.Cob_GradeGroup.Text}',
                                    '{GrindPlan.Txt_Thickness_From.Text}',
                                    '{GrindPlan.Txt_Thickness_To.Text}',
                                    'M',
                                    '{GrindPlan.Cob_M_BeltPattern.Text}',
                                    '{GrindPlan.Txt_M_PassNumber.Text}',
                                    '{GrindPlan.Txt_M_LineSpeed.Text}'),
                                  ( '{GrindPlan.Cob_GradeGroup.Text}',
                                    '{GrindPlan.Txt_Thickness_From.Text}',
                                    '{GrindPlan.Txt_Thickness_To.Text}',
                                    'T',
                                    '{GrindPlan.Cob_T_BeltPattern.Text}',
                                    '{GrindPlan.Txt_T_PassNumber.Text}',
                                    '{GrindPlan.Txt_T_LineSpeed.Text}')";
            #endregion
            return strSql;
        }
        /// <summary>
        /// 研磨計畫-修改
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_4_UpdateGrindPlan_DB_GrindPlan(int Postion,DataTable dt)
        {
            string strSql = "";
            #region SQL
            switch (Postion)
            {
                case 0:

                    #region 頭
                    strSql = $@"Update [{nameof(GrindPlanEntity.TBL_GrindPlan)}] set
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)}] = '{GrindPlan.Cob_GradeGroup.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)}] = '{GrindPlan.Txt_Thickness_From.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)}] = '{GrindPlan.Txt_Thickness_To.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)}] = 'H',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)}] = '{GrindPlan.Cob_H_BeltPattern.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)}] = '{GrindPlan.Txt_H_PassNumber.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.LineSpeed)}] = '{GrindPlan.Txt_H_LineSpeed.Text}'
                                Where  [{nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)}] = '{dt.Rows[0][nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)]}'
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)}] = '{dt.Rows[0][nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)]}'  
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)}] = '{dt.Rows[0][nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)]}' 
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)}] = '{dt.Rows[0][nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)]}' 
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)}] = '{dt.Rows[0][nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)]}' 
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)}] = '{dt.Rows[0][nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)]}' 
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.LineSpeed)}] = '{dt.Rows[0][nameof(GrindPlanEntity.TBL_GrindPlan.LineSpeed)]}' ";
                    #endregion

                    break;

                case 1:

                    #region 中
                    strSql = $@"Update [{nameof(GrindPlanEntity.TBL_GrindPlan)}] set
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)}] = '{GrindPlan.Cob_GradeGroup.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)}] = '{GrindPlan.Txt_Thickness_From.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)}] = '{GrindPlan.Txt_Thickness_To.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)}] = 'M',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)}] = '{GrindPlan.Cob_M_BeltPattern.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)}] = '{GrindPlan.Txt_M_PassNumber.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.LineSpeed)}] = '{GrindPlan.Txt_M_LineSpeed.Text}'
                                 Where  [{nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)}] = '{dt.Rows[1][nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)]}'
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)}] = '{dt.Rows[1][nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)]}'  
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)}] = '{dt.Rows[1][nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)]}' 
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)}] = '{dt.Rows[1][nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)]}' 
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)}] = '{dt.Rows[1][nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)]}' 
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)}] = '{dt.Rows[1][nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)]}' 
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.LineSpeed)}] = '{dt.Rows[1][nameof(GrindPlanEntity.TBL_GrindPlan.LineSpeed)]}' ";
                    #endregion

                    break;

                case 2:

                    #region 尾
                    strSql = $@"Update [{nameof(GrindPlanEntity.TBL_GrindPlan)}] set
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)}] = '{GrindPlan.Cob_GradeGroup.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)}] = '{GrindPlan.Txt_Thickness_From.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)}] = '{GrindPlan.Txt_Thickness_To.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)}] = 'T',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)}] = '{GrindPlan.Cob_T_BeltPattern.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)}] = '{GrindPlan.Txt_T_PassNumber.Text}',
                                       [{nameof(GrindPlanEntity.TBL_GrindPlan.LineSpeed)}] = '{GrindPlan.Txt_T_LineSpeed.Text}'
                                Where  [{nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)}] = '{dt.Rows[2][nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)]}'
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)}] = '{dt.Rows[2][nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)]}'  
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)}] = '{dt.Rows[2][nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)]}' 
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)}] = '{dt.Rows[2][nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)]}' 
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)}] = '{dt.Rows[2][nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)]}' 
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)}] = '{dt.Rows[2][nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)]}' 
                                  and  [{nameof(GrindPlanEntity.TBL_GrindPlan.LineSpeed)}] = '{dt.Rows[2][nameof(GrindPlanEntity.TBL_GrindPlan.LineSpeed)]}' ";
                    #endregion

                    break;

                default:
                    break;
            }
            #endregion
            return strSql;
        }
        /// <summary>
        /// 研磨計畫-帶出最大道次
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_4_SelectMaxPass_DB_BeltPatterns(string BeltPattern)
        {
            string strSql = "";
            strSql = $@"Select max([{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}]) as Pass_To
                        From [{nameof(BeltPatternsEntity.TBL_BeltPatterns)}]
                        Where [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{BeltPattern}'
                        Group by [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}]";
            return strSql;
        }
        /// <summary>
        /// 研磨模板
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_4_SelectBeltPatterns_DB_BeltPatterns()
        {
            string strSql = "";
            strSql = $@"Select distinct [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}],
                                        [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}],
                                        [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] 
                        From [{nameof(BeltPatternsEntity.TBL_BeltPatterns)}] ";
            if (!GrindPlan.Cob_BeltPattern_S.Text.Trim().Equals(string.Empty) || !GrindPlan.Txt_Pass_From_S.Text.Trim().Equals(string.Empty) || !GrindPlan.Txt_Pass_To_S.Text.Trim().Equals(string.Empty))
            {
                strSql += " Where ";
                strSql = !GrindPlan.Cob_BeltPattern_S.Text.Trim().Equals(string.Empty) ? strSql += $" [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] ='{GrindPlan.Cob_BeltPattern_S.Text}' " : strSql;
                strSql = (!GrindPlan.Cob_BeltPattern_S.Text.Trim().Equals(string.Empty) && !GrindPlan.Txt_Pass_From_S.Text.Trim().Equals(string.Empty)) ? strSql += " and " : strSql;
                strSql = !GrindPlan.Txt_Pass_From_S.Text.Trim().Equals(string.Empty) ? strSql += $" [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] ='{GrindPlan.Txt_Pass_From_S.Text}' " : strSql;
                strSql = (!GrindPlan.Txt_Pass_To_S.Text.Trim().Equals(string.Empty) && !GrindPlan.Txt_Pass_From_S.Text.Trim().Equals(string.Empty)) || (!GrindPlan.Txt_Pass_To_S.Text.Trim().Equals(string.Empty) && !GrindPlan.Cob_BeltPattern_S.Text.Trim().Equals(string.Empty)) ? strSql += " and " : strSql;
                strSql = !GrindPlan.Txt_Pass_To_S.Text.Trim().Equals(string.Empty) ? strSql += $" [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] ='{GrindPlan.Txt_Pass_To_S.Text}' " : strSql;
            }
            return strSql;
        }
        /// <summary>
        /// 研磨模板-ComboBox
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_4_SelectBeltPatternsComboBox_DB_BeltPatterns()
        {
            string strSql = "";
            strSql = $"Select distinct [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] From [{nameof(BeltPatternsEntity.TBL_BeltPatterns)}] ";
            return strSql;
        }
        /// <summary>
        ///  研磨模板-詳細資料
        /// </summary>
        /// <param name="BeltPattern"></param>
        /// <param name="Pass_From"></param>
        /// <param name="Pass_To"></param>
        /// <returns></returns>
        public static string Frm_4_4_BeltPatternsSelectionChange_DB_BeltPatterns(string BeltPattern, string Pass_From, string Pass_To)
        {
            string strSql = "";
            strSql = $@" Select 
                              [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}], 
                              [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}],
                              [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}],
                              [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}],
                              [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}],
                              [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}] 
                         From [{nameof(BeltPatternsEntity.TBL_BeltPatterns)}] 
                         Where [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{BeltPattern}' 
                         and [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] = {Pass_From} 
                         and [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] = {Pass_To}";
            return strSql;
        }
        /// <summary>
        /// 研磨模板-新增
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_4_InsertBeltPatterns_DB_BeltPatterns()
        {
            string strSql = "";
            #region SQL
            strSql = $@"Insert into [{nameof(BeltPatternsEntity.TBL_BeltPatterns)}]
                                   ([{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}]
                                   ,[{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}]
                                   ,[{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}]
                                   ,[{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}]
                                   ,[{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}]
                                   ,[{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}]
                                   ,[{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}]
                                   ,[{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}]
                                   ,[{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}])
                            Values ('{GrindPlan.Txt_SelectionBeltPattern.Text}','{GrindPlan.Txt_SelectionPassFrom.Text}','{GrindPlan.Txt_SelectionPassTo.Text}',
                                    '1','{GrindPlan.Txt_GR_1_Current.Text}','{GrindPlan.Cob_GR_1_BeltCode.Text}','{GrindPlan.Txt_GR_1_BeltNumber.Text}','{GrindPlan.Cob_GR_1_Belt_RotateDir.SelectedValue}','{GrindPlan.Txt_GR_1_BeltSpeed.Text}'),
                                   ('{GrindPlan.Txt_SelectionBeltPattern.Text}','{GrindPlan.Txt_SelectionPassFrom.Text}','{GrindPlan.Txt_SelectionPassTo.Text}',
                                    '2','{GrindPlan.Txt_GR_2_Current.Text}','{GrindPlan.Cob_GR_2_BeltCode.Text}','{GrindPlan.Txt_GR_2_BeltNumber.Text}','{GrindPlan.Cob_GR_2_Belt_RotateDir.SelectedValue}','{GrindPlan.Txt_GR_2_BeltSpeed.Text}'),
                                   ('{GrindPlan.Txt_SelectionBeltPattern.Text}','{GrindPlan.Txt_SelectionPassFrom.Text}','{GrindPlan.Txt_SelectionPassTo.Text}',
                                    '3','{GrindPlan.Txt_GR_3_Current.Text}','{GrindPlan.Cob_GR_3_BeltCode.Text}','{GrindPlan.Txt_GR_3_BeltNumber.Text}','{GrindPlan.Cob_GR_3_Belt_RotateDir.SelectedValue}','0'),
                                   ('{GrindPlan.Txt_SelectionBeltPattern.Text}','{GrindPlan.Txt_SelectionPassFrom.Text}','{GrindPlan.Txt_SelectionPassTo.Text}',
                                    '4','{GrindPlan.Txt_GR_4_Current.Text}','{GrindPlan.Cob_GR_4_BeltCode.Text}','{GrindPlan.Txt_GR_4_BeltNumber.Text}','{GrindPlan.Cob_GR_4_Belt_RotateDir.SelectedValue}','0'),
                                   ('{GrindPlan.Txt_SelectionBeltPattern.Text}','{GrindPlan.Txt_SelectionPassFrom.Text}','{GrindPlan.Txt_SelectionPassTo.Text}',
                                    '5','{GrindPlan.Txt_GR_5_Current.Text}','{GrindPlan.Cob_GR_5_BeltCode.Text}','{GrindPlan.Txt_GR_5_BeltNumber.Text}','{GrindPlan.Cob_GR_5_Belt_RotateDir.SelectedValue}','0'),
                                   ('{GrindPlan.Txt_SelectionBeltPattern.Text}','{GrindPlan.Txt_SelectionPassFrom.Text}','{GrindPlan.Txt_SelectionPassTo.Text}',
                                    '6','{GrindPlan.Txt_GR_6_Current.Text}','{GrindPlan.Cob_GR_6_BeltCode.Text}','{GrindPlan.Txt_GR_6_BeltNumber.Text}','{GrindPlan.Cob_GR_6_Belt_RotateDir.SelectedValue}','0')";
            #endregion
            return strSql;
        }
       
        /// <summary>
        /// 研磨模板-修改
        /// </summary>
        /// <returns></returns>
        public static string Frm_4_4_UpdateBeltPatterns_DB_BeltPatterns(int GR_NO, DataTable dt)
        {
            string strSql = "";
            #region SQL
            switch (GR_NO)
            {
                case 1:

                    #region GR_1
                    strSql = $@"UPDATE [{nameof(BeltPatternsEntity.TBL_BeltPatterns)}] SET
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{GrindPlan.Txt_SelectionBeltPattern.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] = '{GrindPlan.Txt_SelectionPassFrom.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] = '{GrindPlan.Txt_SelectionPassTo.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}] = '1',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}] = '{GrindPlan.Txt_GR_1_Current.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}] = '{GrindPlan.Cob_GR_1_BeltCode.Text}',                                           
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}] = '{GrindPlan.Txt_GR_1_BeltNumber.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}] = '{GrindPlan.Cob_GR_1_Belt_RotateDir.SelectedValue}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}] = '{GrindPlan.Txt_GR_1_BeltSpeed.Text}'

                              Where    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[0].Value}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[1].Value}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[2].Value}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}] = '{dt.Rows[0][0]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}] = '{dt.Rows[0][1]}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}] = '{dt.Rows[0][2]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}] = '{dt.Rows[0][3]}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}] = '{dt.Rows[0][4]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}] = '{dt.Rows[0][5]}'";
                    #endregion

                    break;
                case 2:

                    #region GR_2
                    strSql = $@"UPDATE [{nameof(BeltPatternsEntity.TBL_BeltPatterns)}] SET
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{GrindPlan.Txt_SelectionBeltPattern.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] = '{GrindPlan.Txt_SelectionPassFrom.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] = '{GrindPlan.Txt_SelectionPassTo.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}] = '2',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}] = '{GrindPlan.Txt_GR_2_Current.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}] = '{GrindPlan.Cob_GR_2_BeltCode.Text}',                                           
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}] = '{GrindPlan.Txt_GR_2_BeltNumber.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}] = '{GrindPlan.Cob_GR_2_Belt_RotateDir.SelectedValue}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}] = '{GrindPlan.Txt_GR_2_BeltSpeed.Text}'

                              Where    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[0].Value}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[1].Value}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[2].Value}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}] = '{dt.Rows[1][0]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}] = '{dt.Rows[1][1]}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}] = '{dt.Rows[1][2]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}] = '{dt.Rows[1][3]}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}] = '{dt.Rows[1][4]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}] = '{dt.Rows[1][5]}'";
                    #endregion

                    break;
                case 3:

                    #region GR_3
                    strSql = $@"UPDATE [{nameof(BeltPatternsEntity.TBL_BeltPatterns)}] SET
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{GrindPlan.Txt_SelectionBeltPattern.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] = '{GrindPlan.Txt_SelectionPassFrom.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] = '{GrindPlan.Txt_SelectionPassTo.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}] = '3',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}] = '{GrindPlan.Txt_GR_3_Current.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}] = '{GrindPlan.Cob_GR_3_BeltCode.Text}',                                           
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}] = '{GrindPlan.Txt_GR_3_BeltNumber.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}] = '{GrindPlan.Cob_GR_3_Belt_RotateDir.SelectedValue}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}] = '0'

                              Where    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[0].Value}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[1].Value}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[2].Value}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}] = '{dt.Rows[2][0]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}] = '{dt.Rows[2][1]}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}] = '{dt.Rows[2][2]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}] = '{dt.Rows[2][3]}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}] = '{dt.Rows[2][4]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}] = '{dt.Rows[2][5]}'";
                    #endregion

                    break;
                case 4:

                    #region GR_4
                    strSql = $@"UPDATE [{nameof(BeltPatternsEntity.TBL_BeltPatterns)}] SET
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{GrindPlan.Txt_SelectionBeltPattern.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] = '{GrindPlan.Txt_SelectionPassFrom.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] = '{GrindPlan.Txt_SelectionPassTo.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}] = '4',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}] = '{GrindPlan.Txt_GR_4_Current.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}] = '{GrindPlan.Cob_GR_4_BeltCode.Text}',                                           
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}] = '{GrindPlan.Txt_GR_4_BeltNumber.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}] = '{GrindPlan.Cob_GR_4_Belt_RotateDir.SelectedValue}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}] = '0'

                              Where    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[0].Value}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[1].Value}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[2].Value}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}] = '{dt.Rows[3][0]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}] = '{dt.Rows[3][1]}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}] = '{dt.Rows[3][2]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}] = '{dt.Rows[3][3]}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}] = '{dt.Rows[3][4]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}] = '{dt.Rows[3][5]}'";
                    #endregion

                    break;
                case 5:

                    #region GR_5
                    strSql = $@"UPDATE [{nameof(BeltPatternsEntity.TBL_BeltPatterns)}] SET
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{GrindPlan.Txt_SelectionBeltPattern.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] = '{GrindPlan.Txt_SelectionPassFrom.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] = '{GrindPlan.Txt_SelectionPassTo.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}] = '5',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}] = '{GrindPlan.Txt_GR_5_Current.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}] = '{GrindPlan.Cob_GR_5_BeltCode.Text}',                                           
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}] = '{GrindPlan.Txt_GR_5_BeltNumber.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}] = '{GrindPlan.Cob_GR_5_Belt_RotateDir.SelectedValue}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}] = '0'

                              Where    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[0].Value}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[1].Value}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[2].Value}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}] = '{dt.Rows[4][0]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}] = '{dt.Rows[4][1]}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}] = '{dt.Rows[4][2]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}] = '{dt.Rows[4][3]}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}] = '{dt.Rows[4][4]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}] = '{dt.Rows[4][5]}'";
                    #endregion

                    break;
                case 6:

                    #region GR_6
                    strSql = $@"UPDATE [{nameof(BeltPatternsEntity.TBL_BeltPatterns)}] SET
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{GrindPlan.Txt_SelectionBeltPattern.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] = '{GrindPlan.Txt_SelectionPassFrom.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] = '{GrindPlan.Txt_SelectionPassTo.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}] = '6',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}] = '{GrindPlan.Txt_GR_6_Current.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}] = '{GrindPlan.Cob_GR_6_BeltCode.Text}',                                           
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}] = '{GrindPlan.Txt_GR_6_BeltNumber.Text}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}] = '{GrindPlan.Cob_GR_6_Belt_RotateDir.SelectedValue}',
                                       [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}] = '0'

                              Where    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[0].Value}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[1].Value}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)}] = '{GrindPlan.Dgv_BeltPattern.CurrentRow.Cells[2].Value}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)}] = '{dt.Rows[5][0]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)}] = '{dt.Rows[5][1]}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)}] = '{dt.Rows[5][2]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)}] = '{dt.Rows[5][3]}'
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)}] = '{dt.Rows[5][4]}'   
                                and    [{nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)}] = '{dt.Rows[5][5]}'";
                    #endregion

                    break;
                default:
                    break;
            }
            #endregion
            return strSql;
        }
        #endregion

        #region Frm_4_5
        public static string Frm_4_5_SelectBeltsComboBox_DB_Belts()
        {
            string strSql = "";
            strSql = $@"Select distinct [{nameof(BeltAccEntity.TBL_Belts.Belt_Particle_Number)}]
                        From [{nameof(BeltAccEntity.TBL_Belts)}]";
            return strSql;
        }
        public static string Frm_4_5_SelectBelts_GR_ComboBox_DB_Belts()
        {
            string strSql = "";
            strSql = $@"Select distinct [{nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)}]
                        From [{nameof(BeltAccEntity.TBL_Belts)}]
                        Order by [{nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)}] asc";
            return strSql;
        }
        public static string Frm_4_5_SelectBeltsKindComboBox_DB_Belts()
        {
            string strSql = "";
            strSql = $@"Select distinct [{nameof(BeltAccEntity.TBL_Belts.Belt_Type)}] From [{nameof(BeltAccEntity.TBL_Belts)}]";
            return strSql;
        }
        public static string Frm_4_5_SelectSuppliers_DB_BeltSuppliers()
        {
            string strSql = "";
            strSql = $@"Select [{nameof(TBL_BeltSuppliers.SUPPLIER_CODE)}],[{nameof(TBL_BeltSuppliers.SUPPLIER_NAME)}] From [{nameof(TBL_BeltSuppliers)}]";
            return strSql;
        }
        public static string Frm_4_5_SelectMaterials_DB_TBL_BeltMaterials()
        {
            string strSql = "";
            strSql = $@"Select [{nameof(TBL_BeltMaterials.MATERIAL_CODE)}],[{nameof(TBL_BeltMaterials.MATERIAL_NAME)}] From [{nameof(TBL_BeltMaterials)}]";
            return strSql;
        }
        public static string Frm_4_5_SelectBelts_DB_TBL_Belts()
        {
            string strSql = "";
            strSql = $@"SELECT a.[{nameof(BeltAccEntity.TBL_Belts.Belt_No)}],
                               a.[{nameof(BeltAccEntity.TBL_Belts.Belt_Type)}],
                               a.[{nameof(BeltAccEntity.TBL_Belts.Belt_Particle_Number)}],
                               b.[{nameof(TBL_BeltSuppliers.SUPPLIER_NAME)}],
                               a.[{nameof(BeltAccEntity.TBL_Belts.Material_Code)}],
                               a.[{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Belt)}],
                               a.[{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Strip)}],
                               a.[{nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)}],
                               a.[{nameof(BeltAccEntity.TBL_Belts.Suppler_Code)}]
                        FROM [{nameof(BeltAccEntity.TBL_Belts)}] a
                        Left join [{nameof(TBL_BeltSuppliers)}] b on a.[{nameof(BeltAccEntity.TBL_Belts.Suppler_Code)}] = b.[{nameof(TBL_BeltSuppliers.SUPPLIER_CODE)}]";
            return strSql;
        }
        public static string Frm_4_5_selectGR_NO_DB_TBL_Belts(string GR_NO)
        {
            string strSql = "";
            strSql = $@"SELECT [{nameof(BeltAccEntity.TBL_Belts.Belt_No)}],[{nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)}]
                        FROM [{nameof(BeltAccEntity.TBL_Belts)}]
                        Where [{nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)}] = '{GR_NO}'";
            return strSql;
        }
        public static string Frm_4_5_InsertBelts_DB_TBL_Belts()
        {
            string strSql = "";
            strSql = $@"Insert into [{nameof(BeltAccEntity.TBL_Belts)}]
                                   ([{nameof(BeltAccEntity.TBL_Belts.Belt_No)}],
                                    [{nameof(BeltAccEntity.TBL_Belts.Belt_Type)}],
                                    [{nameof(BeltAccEntity.TBL_Belts.Belt_Particle_Number)}],
                                    [{nameof(BeltAccEntity.TBL_Belts.Suppler_Code)}],
                                    [{nameof(BeltAccEntity.TBL_Belts.Material_Code)}],
                                    [{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Belt)}],
                                    [{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Strip)}],
                                    [{nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)}])
                              Values
                                   ('{Belts.Txt_Belt_No.Text}',
                                    '{Belts.Cob_BeltsKind.SelectedValue}',
                                    '{Belts.Cob_Particle_Number.SelectedValue}',
                                    '{Belts.Cob_Suppler_Code.SelectedValue}',
                                    '{Belts.Cob_Material_Code.SelectedValue}',
                                    '{Belts.Txt_Total_Grind_Length_Belt.Text}',
                                    '{Belts.Txt_Total_Grind_Length_Strip.Text}',
                                    '{Belts.Cob_Mount_GR_No.SelectedValue}')";
            return strSql;
        }
        public static string Frm_4_5_UpdateBelts_DB_TBL_Belts()
        {
            string strSql = "";
            strSql = $@"Update [{nameof(BeltAccEntity.TBL_Belts)}]
                           Set [{nameof(BeltAccEntity.TBL_Belts.Belt_No)}] = '{Belts.Txt_Belt_No.Text}',
                               [{nameof(BeltAccEntity.TBL_Belts.Belt_Type)}] = '{Belts.Cob_BeltsKind.SelectedValue}',
                               [{nameof(BeltAccEntity.TBL_Belts.Belt_Particle_Number)}] = '{Belts.Cob_Particle_Number.SelectedValue}',
                               [{nameof(BeltAccEntity.TBL_Belts.Suppler_Code)}] = '{Belts.Cob_Suppler_Code.SelectedValue}',
                               [{nameof(BeltAccEntity.TBL_Belts.Material_Code)}] = '{Belts.Cob_Material_Code.SelectedValue}',
                               [{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Belt)}] = '{Belts.Txt_Total_Grind_Length_Belt.Text}',
                               [{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Strip)}] = '{Belts.Txt_Total_Grind_Length_Strip.Text}',
                               [{nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)}] = '{Belts.Cob_Mount_GR_No.SelectedValue}'
                         Where [{nameof(BeltAccEntity.TBL_Belts.Belt_No)}] = '{Belts.Dgv_Belts.CurrentRow.Cells[0].Value}'
                           and [{nameof(BeltAccEntity.TBL_Belts.Belt_Type)}] = '{Belts.Dgv_Belts.CurrentRow.Cells[1].Value}'
                           and [{nameof(BeltAccEntity.TBL_Belts.Belt_Particle_Number)}] = '{Belts.Dgv_Belts.CurrentRow.Cells[2].Value}'
                           and [{nameof(BeltAccEntity.TBL_Belts.Suppler_Code)}] = '{Belts.Dgv_Belts.CurrentRow.Cells[8].Value}'
                           and [{nameof(BeltAccEntity.TBL_Belts.Material_Code)}] = '{Belts.Dgv_Belts.CurrentRow.Cells[4].Value}'
                           and [{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Belt)}] = '{Belts.Dgv_Belts.CurrentRow.Cells[5].Value}'
                           and [{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Strip)}] = '{Belts.Dgv_Belts.CurrentRow.Cells[6].Value}'
                           and [{nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)}] = '{Belts.Dgv_Belts.CurrentRow.Cells[7].Value}'";
            return strSql;
        }

        public static string Frm_4_5_DeleteBelts_DB_TBL_Belts(string strBelt_No)
        {
            string strSql = "";
            strSql = $@"DELETE FROM [{nameof(BeltAccEntity.TBL_Belts)}]                           
                         Where [{nameof(BeltAccEntity.TBL_Belts.Belt_No)}] = '{strBelt_No}'";
            return strSql;
        }

        public static string Frm_4_5_InsertMaterial_DB_TBL_BeltMaterials()
        {
            string strSql = "";
            strSql = $@"Insert into [{nameof(TBL_BeltMaterials)}]
                                   ([{nameof(TBL_BeltMaterials.MATERIAL_CODE)}],[{nameof(TBL_BeltMaterials.MATERIAL_NAME)}])
                             Values('{Belts.Txt_MaterialCode.Text}','{Belts.Txt_MaterialName.Text}')";
            return strSql;
        }
        public static string Frm_4_5_UpdateMaterial_DB_TBL_BeltMaterials()
        {
            string strSql = "";
            strSql = $@"Update [{nameof(TBL_BeltMaterials)}]
                           Set [{nameof(TBL_BeltMaterials.MATERIAL_CODE)}] = '{Belts.Txt_MaterialCode.Text}',
                               [{nameof(TBL_BeltMaterials.MATERIAL_NAME)}] = '{Belts.Txt_MaterialName.Text}'
                         Where [{nameof(TBL_BeltMaterials.MATERIAL_CODE)}] = '{Belts.Dgv_Materials.CurrentRow.Cells[0].Value}'
                           and [{nameof(TBL_BeltMaterials.MATERIAL_NAME)}] = '{Belts.Dgv_Materials.CurrentRow.Cells[1].Value}'";
            return strSql;
        }
        public static string Frm_4_5_InsertSuppliers_DB_TBL_BeltSuppliers()
        {
            string strSql = "";
            strSql = $@"Insert into [{nameof(TBL_BeltSuppliers)}]
                                   ([{nameof(TBL_BeltSuppliers.SUPPLIER_CODE)}],[{nameof(TBL_BeltSuppliers.SUPPLIER_NAME)}])
                             Values('{Belts.Txt_SupplierCode.Text}','{Belts.Txt_SupplierName.Text}')";
            return strSql;
        }
        public static string Frm_4_5_UpdateSuppliers_DB_TBL_BeltSuppliers()
        {
            string strSql = "";
            strSql = $@"Update [{nameof(TBL_BeltSuppliers)}]
                           Set [{nameof(TBL_BeltSuppliers.SUPPLIER_CODE)}] = '{Belts.Txt_SupplierCode.Text}',
                               [{nameof(TBL_BeltSuppliers.SUPPLIER_NAME)}] = '{Belts.Txt_SupplierName.Text}'
                         Where [{nameof(TBL_BeltSuppliers.SUPPLIER_CODE)}] = '{Belts.Dgv_Suppliers.CurrentRow.Cells[0].Value}'
                           and [{nameof(TBL_BeltSuppliers.SUPPLIER_NAME)}] = '{Belts.Dgv_Suppliers.CurrentRow.Cells[1].Value}'";
            return strSql;
        }
        public static string Frm_4_5_SelectBeltsGR_DB_TBL_Belts()
        {
            string strSql = "";
            strSql = $"SELECT [{nameof(BeltAccEntity.TBL_Belts.Belt_No)}],[{nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)}]  FROM [{nameof(BeltAccEntity.TBL_Belts)}]";
            return strSql;
        }
        public static string Frm_4_5_SelectBeltMaterials_DB_TBL_BeltMaterials()
        {
            string strSql = "";
            strSql = $"SELECT [{nameof(TBL_BeltMaterials.MATERIAL_CODE)}],[{nameof(TBL_BeltMaterials.MATERIAL_NAME)}] FROM [{nameof(TBL_BeltMaterials)}]";
            return strSql;
        }
        public static string Frm_4_5_SelectBeltSuppliers_DB_TBL_BeltSuppliers()
        {
            string strSql = "";
            strSql = $"SELECT [{nameof(TBL_BeltSuppliers.SUPPLIER_CODE)}],[{nameof(TBL_BeltSuppliers.SUPPLIER_NAME)}] FROM [{nameof(TBL_BeltSuppliers)}]";
            return strSql;
        }
        #endregion

        #region Frm_5_1
        public static string Frm_5_1_SelectEventLog_DB_EventLog()
        {
            string strSql = "";
            strSql = $@"select  TOP 50 [{nameof(EventLogEntity.TBL_EventLog.System_ID)}],
                                       [{nameof(EventLogEntity.TBL_EventLog.Function_Block)}],
                                       [{nameof(EventLogEntity.TBL_EventLog.FrameGroup_No)}],
                                       [{nameof(EventLogEntity.TBL_EventLog.Frame_No)}] ,
                                       [{nameof(EventLogEntity.TBL_EventLog.Event_Type)}],
                                       [{nameof(EventLogEntity.TBL_EventLog.Event_Description)}],
                                       [{nameof(EventLogEntity.TBL_EventLog.Command)}],
                                       CONVERT(varchar,[{nameof(EventLogEntity.TBL_EventLog.CreateTime)}],120)CreateTime 
                        FROM [{nameof(EventLogEntity.TBL_EventLog)}] 
                        where [{nameof(EventLogEntity.TBL_EventLog.Event_Type)}]='3' 
                        order by [{nameof(EventLogEntity.TBL_EventLog.CreateTime)}] desc";
            return strSql;
        }
        public static string Frm_5_1_SearchEventLog_DB_EventLog(string StartTime, string EndTime, int DataCount)
        {
            string strSQL = "";
            string strTop = "";
            if (!string.IsNullOrEmpty(DataCount.ToString()))
                strTop = $"Top ({DataCount})";
            strSQL = $@"Select {strTop} [{nameof(EventLogEntity.TBL_EventLog.System_ID)}],
                                      [{nameof(EventLogEntity.TBL_EventLog.Function_Block)}],
                                      [{nameof(EventLogEntity.TBL_EventLog.FrameGroup_No)}],
                                      [{nameof(EventLogEntity.TBL_EventLog.Frame_No)}] ,
                                      [{nameof(EventLogEntity.TBL_EventLog.Event_Type)}],
                                      [{nameof(EventLogEntity.TBL_EventLog.Event_Description)}],
                                      [{nameof(EventLogEntity.TBL_EventLog.Command)}],
                                      CONVERT(varchar,[{nameof(EventLogEntity.TBL_EventLog.CreateTime)}],121)CreateTime
                        FROM [{nameof(EventLogEntity.TBL_EventLog)}]
                        Where [{nameof(EventLogEntity.TBL_EventLog.CreateTime)}] between '{StartTime}:00:00' and '{EndTime}:59:59'";

            //int Count = 0;

            ////時間區間
            //if (EventLog.chkTime.Checked && Count.Equals(0))
            //{
            //    strSQL += $"[{nameof(EventLogEntity.TBL_EventLog.CreateTime)}] between '{EventLog.Dtp_Start_Time.Value:yyyy-MM-dd HH}:00:00' and '{EventLog.Dtp_Finish_Time.Value:yyyy-MM-dd HH}:59:59'";
            //    Count += 1;
            //}

            //類別
            if (EventLog.Chk_EventType.Checked)
            {
                strSQL += $" and [{nameof(EventLogEntity.TBL_EventLog.Event_Type)}] ='{EventLog.Cob_EventType.SelectedValue}'";
                //Count += 1;
            }
            //else if (EventLog.Chk_EventType.Checked)
            //{
            //    strSQL += $" and [{nameof(EventLogEntity.TBL_EventLog.Event_Type)}] ='{EventLog.Cob_EventType.SelectedValue}'";
            //    //Count += 1;
            //}

            //系統
            if (EventLog.Chk_System.Checked )
            {
                strSQL += $" and  [{nameof(EventLogEntity.TBL_EventLog.System_ID)}] ='{EventLog.Cob_System_ID.SelectedValue}'";
                //Count += 1;
            }
            //else if (EventLog.Chk_System.Checked)
            //{
            //    strSQL += $" and [{nameof(EventLogEntity.TBL_EventLog.System_ID)}] ='{EventLog.cbo_System_ID.SelectedValue}'";
            //    //Count += 1;
            //}

            //電腦名稱
            if (EventLog.Chk_ComputerName.Checked )
            {
                strSQL += $" and  [{nameof(EventLogEntity.TBL_EventLog.FrameGroup_No)}] ='{EventLog.Cob_ComputerName.Text}'";
                //Count += 1;
            }
            //else if (EventLog.Chk_ComputerName.Checked && Count > 0)
            //{
            //    strSQL += $" and [{nameof(EventLogEntity.TBL_EventLog.FrameGroup_No)}] ='{EventLog.Cob_ComputerName.Text}'";
            //    Count += 1;
            //}

            //關鍵字
            if (EventLog.Chk_Keyword.Checked )
            {
                strSQL += $" and  [{nameof(EventLogEntity.TBL_EventLog.Command)}] like '%{EventLog.Txt_Keyword.Text}%'";
            }
            //else if (EventLog.Chk_Keyword.Checked && Count > 0)
            //{
            //    strSQL += $" and [{nameof(EventLogEntity.TBL_EventLog.Command)}] like '%{EventLog.Txt_Keyword.Text}%'";
            //}

            strSQL += $" order by [{nameof(EventLogEntity.TBL_EventLog.CreateTime)}] desc";
            return strSQL;
        }
        public static string Frm_5_1_SelectComputerName_DB_EventLog()
        {
            string strSQL = "";
            strSQL = $@"Select distinct [{nameof(EventLogEntity.TBL_EventLog.FrameGroup_No)}] From [{nameof(EventLogEntity.TBL_EventLog)}]";
            return strSQL;
        }
        #endregion

        #region Frm_5_2
        public static string Frm_5_2_SelectTable(string TableName , bool bolOther = false,string strWhere = "")
        {
            string strSql;
            if (!bolOther)
            {
                strSql = $@"Select *,
                                      Convert(char(19),[CreateTime], 120) Time
                                From [{TableName}]";
            }
            else
            {
                strSql = $@"Select * From [{TableName}] {strWhere}";    
            }
            return strSql;
        }

        public static string Frm_5_2_Insert_ScheduleDelete_CoilReject_Code()
        {
            string strSql = $@"Insert into [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition)}]
                                          ([{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo)}],
                                           [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)}],
                                           [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)}],
                                           [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.Create_UserID)}],
                                           [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.CreateTime)}])
                                    Values('{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)].Value}',
                                           '{Main.lblLoginUser.Text.Trim()}',
                                           '{Instance.getTime}')";
            return strSql;
        }

        public static string Frm_5_2_Insert_DelayLocation()
        {
            string strSql = $@"Insert into [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition)}]
                                          ([{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)}],
                                           [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)}],
                                           [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)}],
                                           [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Create_UserID)}],
                                           [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.CreateTime)}])
                                    Values('{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)].Value}',
                                           '{Main.lblLoginUser.Text.Trim()}',
                                           '{Instance.getTime}')";
            return strSql;
        }

        public static string Frm_5_2_Insert_DelayReasonCode()
        {
            string strSql = $@"Insert into [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition)}]
                                          ([{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Create_UserID)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.CreateTime)}])
                                    Values('{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department)].Value}',
                                           '{Main.lblLoginUser.Text.Trim()}',
                                           '{Instance.getTime}')";
            return strSql;
        }

        public static string Frm_5_2_Insert_Material()
        {
            string strSql = $@"Insert into [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade)}]
                                          ([{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)}],
                                           [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ys)}],
                                           [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ts)}],
                                           [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Material_Grade)}],
                                           [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.CreateTime)}])
                                    Values('{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ys)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ts)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Material_Grade)].Value}',
                                           '{Instance.getTime}')";
            return strSql;
        }

        public static string Frm_5_2_Insert_GradeGroups()
        {
            string strSql = $@"Insert into [{nameof(GradeGroupsEntity.TBL_GradeGroups)}]
                                          ([{nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)}],
                                           [{nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)}],
                                           [{nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)}])
                                    Values('{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)].Value}',
                                           '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)].Value}')";
            return strSql;
        }

        public static string Frm_5_2_Update_ScheduleDelete_CoilReject_Code()
        {
            string strSql = $@"Update [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition)}]
                                  Set [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo)].Value}',
                                      [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)].Value}',
                                      [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)].Value}',
                                      [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.Create_UserID)}] = '{Main.lblLoginUser.Text.Trim()}',
                                      [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.CreateTime)}] = '{Instance.getTime}'
                                Where [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo)].Value}'
                                  And [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)].Value}'
                                  And [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)].Value}'";
            return strSql;
        }

        public static string Frm_5_2_Update_DelayLocation()
        {
            string strSql = $@"Update [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition)}]
                                  Set [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)].Value}',
                                      [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)].Value}',
                                      [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)].Value}',
                                      [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Create_UserID)}] = '{Main.lblLoginUser.Text.Trim()}',
                                      [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.CreateTime)}] = '{Instance.getTime}'
                                Where [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)].Value}'
                                  And [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)].Value}'
                                  And [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)].Value}'";
            return strSql;
        }

        public static string Frm_5_2_Update_DelayReasonCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"  Update [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition)}]");
            sb.AppendLine($"   Set   [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)].Value}',");
            sb.AppendLine($"         [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)].Value}',");
            sb.AppendLine($"         [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)].Value}',");
            sb.AppendLine($"         [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode)].Value}',");
            sb.AppendLine($"         [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName)].Value}',");
            sb.AppendLine($"         [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department)].Value}',");
            sb.AppendLine($"         [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Create_UserID)}] = '{Main.lblLoginUser.Text.Trim()}',");
            sb.AppendLine($"         [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.CreateTime)}] = '{Instance.getTime}'");
            sb.AppendLine($"   Where [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)].Value}'");
            sb.AppendLine($"     And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)].Value}'");
            sb.AppendLine($"     And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)].Value}'");
            sb.AppendLine($"     And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode)].Value}'");
            sb.AppendLine($"     And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName)].Value}'");
            sb.AppendLine($"     And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department)].Value}'");
         

            string strSql = sb.ToString();
            return strSql;
        }

        public static string Frm_5_2_Update_Material()
        {
            string strSql = $@"Update [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade)}]
                                  Set [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)].Value}',
                                      [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ys)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ys)].Value}',
                                      [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ts)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ts)].Value}',
                                      [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Material_Grade)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Material_Grade)].Value}',
                                      [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.CreateTime)}] = '{Instance.getTime}'
                                Where [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)].Value}'
                                  And [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ys)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ys)].Value}'
                                  And [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ts)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ts)].Value}'";
            return strSql;
        }

        public static string Frm_5_2_Update_GradeGroups()
        {

            string strSql = $@"Update [{nameof(GradeGroupsEntity.TBL_GradeGroups)}]
                                  Set --[{nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)].Value}',
                                      --[{nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)].Value}',
                                      [{nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)}] = '{CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)].Value}' 
                                Where [{nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)].Value}'
                                  And [{nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)].Value}'";
                                 // And [{nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[2].Value}'
            return strSql;
        }

        public static string Frm_5_2_Delete_ScheduleDelete_CoilReject_Code()
        {
            string strSql = $@"Delete From [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition)}]
                                     Where [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo)].Value}'
                                       And [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)].Value}'
                                       And [{nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)].Value}'";
            return strSql;
        }

        public static string Frm_5_2_Delete_DelayLocation()
        {
            string strSql = $@"Delete From [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition)}]
                                     Where [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)].Value}'
                                       And [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)].Value}'
                                       And [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)].Value}'";
            return strSql;
        }

        public static string Frm_5_2_Delete_DelayReasonCode()
        {
            string strSql = $@"Delete From [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition)}]
                                     Where [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)].Value}'
                                       And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)].Value}'
                                       And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)].Value}'
                                       And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode)].Value}'
                                       And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName)].Value}'
                                       And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department)].Value}'";
            return strSql;
        }

        public static string Frm_5_2_Delete_Material()
        {
            string strSql = $@"Delete From [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade)}]
                                     Where [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)].Value}'
                                       And [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ys)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ys)].Value}'
                                       And [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ts)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Ts)].Value}'";
            return strSql;
        }

        public static string Frm_5_2_Delete_GradeGroups()
        {
            string strSql = $@"Delete From [{nameof(GradeGroupsEntity.TBL_GradeGroups)}]
                                     Where [{nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)].Value}'
                                       And [{nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)].Value}'";
                                      // And [{nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)}] = '{CodeMaintain.Dgv_Table.CurrentRow.Cells[2].Value}'
            return strSql;
        }

        #endregion

        #region Frm_5_3
        /// <summary>
        /// 查詢帳號權限
        /// </summary>
        /// <returns></returns>
        public static string Frm_5_3_SelectUser(string strUserID)
        {
            string strSql = $@"Select AuthorityData.* , 
                                      Frame.[{nameof(TBL_AuthorityData_Frame.Frame_ID)}],
                                      Frame.[{nameof(TBL_AuthorityData_Frame.Frame_Function)}]
                               From [{nameof(TBL_AuthorityData)}] AuthorityData
                               Left Join [{nameof(TBL_AuthorityData_Frame)}] Frame
                               On Frame.[{nameof(TBL_AuthorityData_Frame.User_ID)}] = AuthorityData.[{nameof(TBL_AuthorityData.User_ID)}]
                               Where AuthorityData.[{nameof(TBL_AuthorityData.User_ID)}] = '{strUserID}'";
            return strSql;
        }

        /// <summary>
        /// 檢查帳號
        /// </summary>
        /// <returns></returns>
        public static string Frm_5_3_Check_User()
        {
            string strSql = $@"Select User_ID
                               From [{nameof(TBL_AuthorityData)}]
                               Where [{nameof(TBL_AuthorityData.User_ID)}] = '{PublicForms.UserSetup.Txt_UserID.Text}'";
            return strSql;
        }

        /// <summary>
        /// 檢查權限
        /// </summary>
        /// <returns></returns>
        public static string Frm_5_3_Check_AuthorityData_Frame(string strUserID)
        {
            string strSql = $@"Select User_ID
                               From [{nameof(TBL_AuthorityData_Frame)}]
                               Where [{nameof(TBL_AuthorityData_Frame.User_ID)}] = '{strUserID}'";
            return strSql;
        }

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <returns></returns>
        public static string Frm_5_3_InsertUser()
        {
            string strSql = $@"Insert into [{nameof(TBL_AuthorityData)}]
                                          ([{nameof(TBL_AuthorityData.User_ID)}],
                                           [{nameof(TBL_AuthorityData.Password)}],
                                           [{nameof(TBL_AuthorityData.Department)}],
                                           [{nameof(TBL_AuthorityData.Team)}],
                                           [{nameof(TBL_AuthorityData.Authority_Class)}],
                                           [{nameof(TBL_AuthorityData.Create_DateTime)}])
                                     Values
                                          ('{PublicForms.UserSetup.Txt_UserID.Text}',
                                           '{PublicForms.UserSetup.Txt_Password.Text}',
                                           '{PublicForms.UserSetup.Txt_Department.Text}',
                                           '{PublicForms.UserSetup.Cob_Team.SelectedValue}',
                                           '{PublicForms.UserSetup.Cob_Authority_Class.SelectedValue}',
                                           '{GlobalVariableHandler.Instance.getTime}')";
            return strSql;
        }

        /// <summary>
        /// 新增權限
        /// </summary>
        /// <returns></returns>
        public static string Frm_5_3_Insert_Frame_One(string strFrame_ID, string strFrame_Function)
        {
            string strSql = $@"Insert into [{nameof(TBL_AuthorityData_Frame)}]
                                          ([{nameof(TBL_AuthorityData_Frame.User_ID)}],
                                           [{nameof(TBL_AuthorityData_Frame.Frame_ID)}],
                                           [{nameof(TBL_AuthorityData_Frame.Frame_Function)}],
                                           [{nameof(TBL_AuthorityData_Frame.Create_DateTime)}]) Values ";
                     
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','{strFrame_ID}','{strFrame_Function}','{GlobalVariableHandler.Instance.getTime}')";

            return strSql;
        }

        /// <summary>
        /// 新增權限
        /// </summary>
        /// <returns></returns>
        public static string Frm_5_3_InsertFrame()
        {
            string strSql = $@"Insert into [{nameof(TBL_AuthorityData_Frame)}]
                                          ([{nameof(TBL_AuthorityData_Frame.User_ID)}],
                                           [{nameof(TBL_AuthorityData_Frame.Frame_ID)}],
                                           [{nameof(TBL_AuthorityData_Frame.Frame_Function)}],
                                           [{nameof(TBL_AuthorityData_Frame.Create_DateTime)}]) Values ";
            string Frame_Function = "";

            #region Frame

            #region '0-2'
            Frame_Function = (PublicForms.UserSetup.rdoN0_2.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR0_2.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW0_2.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','0-2','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '1-1'
            Frame_Function = (PublicForms.UserSetup.rdoN1_1.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR1_1.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW1_1.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','1-1','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '1-2'
            Frame_Function = (PublicForms.UserSetup.rdoN1_2.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR1_2.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW1_2.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','1-2','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '1-2'
            Frame_Function = (PublicForms.UserSetup.rdoN1_3.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR1_3.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW1_3.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','1-3','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '2-1'
            Frame_Function = (PublicForms.UserSetup.rdoN2_1.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR2_1.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW2_1.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','2-1','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '2-2'
            Frame_Function = (PublicForms.UserSetup.rdoN2_2.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR2_2.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW2_2.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','2-2','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '3-1'
            Frame_Function = (PublicForms.UserSetup.rdoN3_1.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR3_1.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW3_1.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','3-1','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '3-2'
            Frame_Function = (PublicForms.UserSetup.rdoN3_2.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR3_2.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW3_2.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','3-2','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '3-3'
            Frame_Function = (UserSetup.rdoN3_3.Checked) ? "N" :
                (UserSetup.rdoR3_3.Checked) ? "R" :
                (UserSetup.rdoW3_3.Checked) ?"W" : string.Empty;
            strSql += $@"('{UserSetup.Txt_UserID.Text}','3-3','{Frame_Function}','{Instance.getTime}'),";
            #endregion

            #region '3-4'
            Frame_Function = (PublicForms.UserSetup.radioButton1.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.radioButton2.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.radioButton3.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','3-4','{Frame_Function}','{Instance.getTime}'),";
            #endregion

            #region '4-1'
            Frame_Function = (PublicForms.UserSetup.rdoN4_1.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR4_1.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW4_1.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','4-1','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '4-2'
            Frame_Function = (PublicForms.UserSetup.rdoN4_2.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR4_2.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW4_2.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','4-2','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '4-3'
            Frame_Function = (PublicForms.UserSetup.rdoN4_3.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR4_3.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW4_3.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','4-3','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '4-4'
            Frame_Function = (PublicForms.UserSetup.rdoN4_4.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR4_4.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW4_4.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','4-4','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '4-5'
            Frame_Function = (PublicForms.UserSetup.rdoN4_5.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR4_5.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW4_5.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','4-5','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '5-1'
            Frame_Function = (PublicForms.UserSetup.rdoN5_1.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR5_1.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW5_1.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','5-1','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '5-2'
            Frame_Function = (PublicForms.UserSetup.rdoN5_2.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR5_2.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW5_2.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','5-2','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '5-3'
            Frame_Function = (PublicForms.UserSetup.rdoN5_3.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR5_3.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW5_3.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','5-3','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '5-4'
            Frame_Function = (PublicForms.UserSetup.rdoN5_4.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR5_4.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW5_4.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','5-4','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '5-5'
            Frame_Function = (PublicForms.UserSetup.rdoN5_5.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR5_5.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW5_5.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','5-5','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '5-6'
            Frame_Function = (PublicForms.UserSetup.rdoN5_6.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR5_6.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW5_6.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','5-6','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}')";
            #endregion

            #endregion

            return strSql;
        }
        /// <summary>
        /// 修改帳號
        /// </summary>
        /// <returns></returns>
        public static string Frm_5_3_UpdateUser()
        {
            string strSql = $@"Update [{nameof(TBL_AuthorityData)}]
                                  Set [{nameof(TBL_AuthorityData.User_ID)}] = '{PublicForms.UserSetup.Txt_UserID.Text}',
                                      [{nameof(TBL_AuthorityData.Password)}] = '{PublicForms.UserSetup.Txt_Password.Text}',
                                      [{nameof(TBL_AuthorityData.Department)}] =  '{PublicForms.UserSetup.Txt_Department.Text}',
                                      [{nameof(TBL_AuthorityData.Team)}] = '{PublicForms.UserSetup.Cob_Team.SelectedValue}',
                                      [{nameof(TBL_AuthorityData.Authority_Class)}] = '{PublicForms.UserSetup.Cob_Authority_Class.SelectedValue}',
                                      [{nameof(TBL_AuthorityData.Create_DateTime)}] = '{GlobalVariableHandler.Instance.getTime}'
                                Where [{nameof(TBL_AuthorityData.User_ID)}] = '{PublicForms.UserSetup.Txt_UserID.Text}'";
            return strSql;
        }
        /// <summary>
        /// 修改畫面權限
        /// </summary>
        /// <returns></returns>
        public static string Frm_5_3_UpdateFrame(string Frame_ID, string Frame_Function)
        {
            string strSql = $@"Update [{nameof(TBL_AuthorityData_Frame)}]
                                  Set [{nameof(TBL_AuthorityData_Frame.User_ID)}] = '{PublicForms.UserSetup.Txt_UserID.Text}',
                                      [{nameof(TBL_AuthorityData_Frame.Frame_ID)}] = '{Frame_ID}',
                                      [{nameof(TBL_AuthorityData_Frame.Frame_Function)}] = '{Frame_Function}',
                                      [{nameof(TBL_AuthorityData_Frame.Create_DateTime)}] = '{GlobalVariableHandler.Instance.getTime}'
                                Where [{nameof(TBL_AuthorityData_Frame.User_ID)}] = '{PublicForms.UserSetup.Txt_UserID.Text}'
                                  And [{nameof(TBL_AuthorityData_Frame.Frame_ID)}] = '{Frame_ID}'";

            return strSql;
        }
        /// <summary>
        /// 刪除帳號
        /// </summary>
        /// <returns></returns>
        public static string Frm_5_3_DeleteUser(string strUserId)
        {
            string strSql = $@"Delete From [{nameof(TBL_AuthorityData)}] Where [{nameof(TBL_AuthorityData.User_ID)}] = '{strUserId}' ";
            return strSql;
        }
        /// <summary>
        /// 刪除畫面權限
        /// </summary>
        /// <returns></returns>
        public static string Frm_5_3_DeleteFrame()
        {
            string strSql = $@"Delete From [{nameof(TBL_AuthorityData_Frame)}] Where [{nameof(TBL_AuthorityData_Frame.User_ID)}] = '{PublicForms.UserSetup.Txt_UserID.Text}'";
            return strSql;
        }
        #endregion

        #region Frm_5_4
        public static string Frm_5_4_SelectShift(string strDate)
        {
            string strSql = $@"Select * 
                                From [{nameof(TBL_WorkSchedule)}] 
                                Where [{nameof(TBL_WorkSchedule.ShiftDate)}] like '{strDate}%'";
            return strSql;
        }
        public static string Frm_5_4_DeleteOldWorkSchedule(string strDate)
        {
            string strSql = $@"Delete From {nameof(TBL_WorkSchedule)}
                                     Where {nameof(TBL_WorkSchedule.ShiftDate)} Like '{strDate}%' ";
            return strSql;
        }
        /// <summary>
        /// 將DataTable組成InsertSql字串
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="strTableName">資料表名</param>
        /// <returns>Sql字串</returns>
        public static string Fun_GetInsertSqlFromDataTable(DataTable dt, string strTableName)
        {

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("BEGIN");
            foreach (DataRow dr in dt.Rows)
            {
                string strInsert = DBHandlerFuntion.Instance.Fun_GetInsertSqlFromDataRow(dr, strTableName);

                sbSql.AppendLine(strInsert);
            }
            sbSql.AppendLine("END;  ");
            return sbSql.ToString();
        }

        //public static object Fun_UpdateFromDataTable(DataTable dt, string strTableName, string[] strKeyColumn, string conn_str)
        //{
        //    object ObjReturn = null;
        //    try
        //    {
        //        StringBuilder sbSql = new StringBuilder();

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            string strUpdate = Fun_GetUpdateSqlFromDataRow(dr, strTableName, strKeyColumn);

        //            ObjReturn = Fun_UpdateData(strUpdate, conn_str);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        PublicComm.ClientLog.Debug(" ex=" + ex.ToString());
        //        PublicComm.ClientLog.Debug(" ex.Message=" + ex.Message);
        //        PublicComm.ClientLog.Debug(" ex.StackTrace=" + ex.StackTrace);

        //        PublicComm.ExceptionLog.Debug(" ex=" + ex.ToString());
        //        PublicComm.ExceptionLog.Debug(" ex.Message=" + ex.Message);
        //        PublicComm.ExceptionLog.Debug(" ex.StackTrace=" + ex.StackTrace);
        //        ObjReturn = null;
        //        return ObjReturn;
        //    }
        //    return ObjReturn;
        //    //return sbSql.ToString();
        //}

        public static string Fun_GetUpdateSqlFromDataRow(DataRow dr, string strTableName, string[] strKeyColumn)
        {
            try
            {
                // 取得設定值的字串.
                string strSetColum = Fun_GetColumnNameAndValueFromDataRow(dr, strKeyColumn);
                // 取得過濾值得字串.
                string strSetValue = Fun_GetKeyColumnNameAndValueFromDataRow(dr, strKeyColumn);

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" UPDATE " + strTableName);
                sbSql.AppendLine(" SET " + strSetColum);
                sbSql.AppendLine(" WHERE " + strSetValue + " ");

                return sbSql.ToString();
            }
            catch (Exception ex)
            {
                PublicComm.ClientLog.Debug(" ex=" + ex.ToString());
                PublicComm.ClientLog.Debug(" ex.Message=" + ex.Message);
                PublicComm.ClientLog.Debug(" ex.StackTrace=" + ex.StackTrace);

                PublicComm.ExceptionLog.Debug(" ex=" + ex.ToString());
                PublicComm.ExceptionLog.Debug(" ex.Message=" + ex.Message);
                PublicComm.ExceptionLog.Debug(" ex.StackTrace=" + ex.StackTrace);
                return null;
            }
        }

        private static string Fun_GetColumnNameAndValueFromDataRow(DataRow dr, string[] strKeyColumn)
        {
            try
            {
                string strA = ", ";

                DataTable dt = dr.Table;
                string[] strUpdate = { "UPDATED", "UPDATETIME", "CREATETIME" };
                StringBuilder sb = new StringBuilder();

                foreach (DataColumn dc in dt.Columns)
                    if (!strKeyColumn.Contains(dc.ColumnName))
                    {
                        //if (dc.ColumnName.ToUpper() == strUpdate.ToUpper())
                        if (Array.IndexOf(strUpdate, dc.ColumnName.ToUpper()) >= 0)
                        {
                            //if(string.IsNullOrEmpty( dr[dc.ColumnName].ToString()))
                            DateTime dtTime = Convert.ToDateTime(dr[dc.ColumnName]);
                            sb.Append(dc.ColumnName + " = N'" + dtTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + strA);
                        }
                        else
                        {
                            sb.Append(dc.ColumnName + " = N'" + dr[dc.ColumnName] + "'" + strA);
                        }

                    }
                    else if (strKeyColumn.Contains("RTRIM(" + dc.ColumnName + ")"))
                        sb.Append("RTRIM(" + dc.ColumnName + ")" + " = N'" + dr[dc.ColumnName].ToString().Trim() + "'" + strA);

                sb.Remove(sb.Length - strA.Length, strA.Length);

                return sb.ToString();
            }
            catch (Exception ex)
            {
                PublicComm.ClientLog.Debug(" ex=" + ex.ToString());
                PublicComm.ClientLog.Debug(" ex.Message=" + ex.Message);
                PublicComm.ClientLog.Debug(" ex.StackTrace=" + ex.StackTrace);

                PublicComm.ExceptionLog.Debug(" ex=" + ex.ToString());
                PublicComm.ExceptionLog.Debug(" ex.Message=" + ex.Message);
                PublicComm.ExceptionLog.Debug(" ex.StackTrace=" + ex.StackTrace);
                return null;
            }
        }

        private static string Fun_GetKeyColumnNameAndValueFromDataRow(DataRow dr, string[] strKeyColumn)
        {
            try
            {
                string strAnd = " AND ";

                DataTable dtSource = dr.Table;

                StringBuilder sb = new StringBuilder();

                foreach (DataColumn dc in dtSource.Columns)
                {


                    if (strKeyColumn.Contains(dc.ColumnName))
                    {
                        //if (dc.ColumnName == "updated")
                        //{
                        //    DateTime dtTime = Convert.ToDateTime(dr[dc.ColumnName]);
                        //    sb.Append(dc.ColumnName + " = N'" + dtTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + strAnd);
                        //}
                        //else
                        //{

                        sb.Append(dc.ColumnName + " = N'" + dr[dc.ColumnName] + "'" + strAnd);
                        //}
                    }
                    else if (strKeyColumn.Contains("RTRIM(" + dc.ColumnName + ")"))
                    {


                        sb.Append("RTRIM(" + dc.ColumnName + ")" + " = N'" + dr[dc.ColumnName].ToString().Trim() + "'" + strAnd);

                    }
                }
                sb.Remove(sb.Length - strAnd.Length, strAnd.Length);

                return sb.ToString();
            }
            catch (Exception ex)
            {
                PublicComm.ClientLog.Debug(" ex=" + ex.ToString());
                PublicComm.ClientLog.Debug(" ex.Message=" + ex.Message);
                PublicComm.ClientLog.Debug(" ex.StackTrace=" + ex.StackTrace);

                PublicComm.ExceptionLog.Debug(" ex=" + ex.ToString());
                PublicComm.ExceptionLog.Debug(" ex.Message=" + ex.Message);
                PublicComm.ExceptionLog.Debug(" ex.StackTrace=" + ex.StackTrace);
                return null;
            }
        }

        #endregion

        #region Frm_5_6
        public static string Frm_5_6_SelectStatus()
        {
            string strSql = $@"Select * From [{nameof(TBL_ConnectionStatus)}]";
            return strSql;
        }
        #endregion


        #region PDOConfirm
        //public static string Frm_PDOConfirm_CoilIDComboBoxItems_DB_Map_PDO()
        //{
        //    string strSql = "";
        //    strSql = $@"Select a.[{nameof(PDOModel.L2L3_PDO.Out_Mat_No)}]
        //                From [{nameof(CoilMapModel.TBL_CoilMap)}] b
        //                Left join [{nameof(PDOModel.L2L3_PDO)}] a
        //                On b.[{nameof(CoilMapModel.TBL_CoilMap.Delivery_SK01)}] = a.[{nameof(PDOModel.L2L3_PDO.Out_Mat_No)}]
        //                Or b.[{nameof(CoilMapModel.TBL_CoilMap.Delivery_SK02)}] = a.[{nameof(PDOModel.L2L3_PDO.Out_Mat_No)}]
        //                Or b.[{nameof(CoilMapModel.TBL_CoilMap.Delivery_Car)}] = a.[{nameof(PDOModel.L2L3_PDO.Out_Mat_No)}]
        //                Or b.[{nameof(CoilMapModel.TBL_CoilMap.Delivery_TOP)}] = a.[{nameof(PDOModel.L2L3_PDO.Out_Mat_No)}]";
        //    return strSql;
        //}
        //public static string Frm_PDOConfirm_PDOSelect_DB_PDO(string Coil_ID)
        //{
        //    string strSql = "";

        //    #region  - SQL - 
        //    strSql = $@"Select [{nameof(PDOModel.L2L3_PDO.OrderNo)}],
        //                             [{nameof(PDOModel.L2L3_PDO.PlanNo)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Out_Mat_No)}],
        //                             [{nameof(PDOModel.L2L3_PDO.In_Mat_No)}],
        //                             Convert(varchar(20),[{nameof(PDOModel.L2L3_PDO.StartTime)}],120)StartTime,
        //                             Convert(varchar(20),[{nameof(PDOModel.L2L3_PDO.FinishTime)}],120)FinishTime,
        //                             [{nameof(PDOModel.L2L3_PDO.Shift)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Team)}],
        //                             [{nameof(PDOModel.L2L3_PDO.St_No)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Out_Mat_Outer_Diameter)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Out_Mat_Inner)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Out_Mat_Wt)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Out_Mat_Gross_Wt)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Out_Mat_Thick)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Out_Mat_Width)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Out_Mat_Length)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Head_C40_Thick)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Mid_C40_Thick)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Tail_C40_Thick)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Head_C25_Thick)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Mid_C25_Thick)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Tail_C25_Thick)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Head_Pass_Num)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Mid_Pass_Num)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Tail_Pass_Num)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Paper_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Paper_Req_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Head_Paper_Length)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Head_Paper_Width)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Tail_Paper_Length)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Tail_Paper_Width)}],
        //                             [{nameof(PDOModel.L2L3_PDO.ExitSleeveUseOrNot)}],
        //                             [{nameof(PDOModel.L2L3_PDO.ExitSleeveDiameter)}],
        //                             [{nameof(PDOModel.L2L3_PDO.ExitSleeveCode)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Head_Hole_Position)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Head_LeaderLength)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Head_Leader_Width)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Head_Leader_Thickness)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Tail_Hole_Position)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Tail_LeaderLength)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Tail_Leader_Width)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Tail_Leader_Thickness)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Head_Leader_St_No)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Tail_Leader_St_No)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Winding_Dire)}],
        //                             [{nameof(PDOModel.L2L3_PDO.BaseSurface)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Inspector)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Hold_Flag)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Hold_Cause_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Sample_Flag)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Fixed_Wt_Flag)}],
        //                             [{nameof(PDOModel.L2L3_PDO.End_Flag)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Scrap_Flag)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Oil_Flag)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Sample_Frqn_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Surface_Accu_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Head_Off_Gauge)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Tail_Off_Gauge)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Surface_Accu_Code_In)}],
        //                             [{nameof(PDOModel.L2L3_PDO.Surface_Accu_Code_Out)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D01_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D02_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D03_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D04_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D05_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D06_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D07_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D08_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D09_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D10_Code)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D01_Origin)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D02_Origin)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D03_Origin)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D04_Origin)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D05_Origin)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D06_Origin)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D07_Origin)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D08_Origin)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D09_Origin)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D10_Origin)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D01_Sid)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D02_Sid)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D03_Sid)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D04_Sid)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D05_Sid)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D06_Sid)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D07_Sid)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D08_Sid)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D09_Sid)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D10_Sid)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D01_Pos_W)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D02_Pos_W)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D03_Pos_W)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D04_Pos_W)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D05_Pos_W)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D06_Pos_W)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D07_Pos_W)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D08_Pos_W)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D09_Pos_W)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D10_Pos_W)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D01_Pos_L_Start)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D02_Pos_L_Start)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D03_Pos_L_Start)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D04_Pos_L_Start)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D05_Pos_L_Start)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D06_Pos_L_Start)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D07_Pos_L_Start)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D08_Pos_L_Start)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D09_Pos_L_Start)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D10_Pos_L_Start)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D01_Pos_L_End)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D02_Pos_L_End)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D03_Pos_L_End)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D04_Pos_L_End)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D05_Pos_L_End)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D06_Pos_L_End)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D07_Pos_L_End)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D08_Pos_L_End)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D09_Pos_L_End)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D10_Pos_L_End)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D01_Level)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D02_Level)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D03_Level)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D04_Level)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D05_Level)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D06_Level)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D07_Level)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D08_Level)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D09_Level)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D10_Level)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D01_Percent)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D02_Percent)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D03_Percent)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D04_Percent)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D05_Percent)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D06_Percent)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D07_Percent)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D08_Percent)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D09_Percent)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D10_Percent)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D01_QGrade)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D02_QGrade)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D03_QGrade)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D04_QGrade)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D05_QGrade)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D06_QGrade)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D07_QGrade)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D08_QGrade)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D09_QGrade)}],
        //                             [{nameof(PDOModel.L2L3_PDO.D10_QGrade)}],
        //                             [{nameof(L2L3_PDO.Head_Rough)}],
        //                             [{nameof(L2L3_PDO.Mid_Rough)}],
        //                             [{nameof(L2L3_PDO.Tail_Rough)}],
        //                             [{nameof(L2L3_PDO.Decoiler_Direction)}]
        //                From [{nameof(PDOModel.L2L3_PDO)}] 
        //                Where [{nameof(PDOModel.L2L3_PDO.Out_Mat_No)}] = '{Coil_ID}'";
        //    #endregion

        //    return strSql;
        //}
        //public static string Frm_PDOConfirm_PDOSave_DB_PDO()
        //{
        //    string strSql = "";
        //    strSql = $@"Update [{nameof(PDOModel.L2L3_PDO)}]
        //                Set [{nameof(PDOModel.L2L3_PDO.OrderNo)}] = '{_PDOConfirm.txtOrder_No.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.PlanNo)}] = '{_PDOConfirm.txtPlan_No.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Out_Mat_No)}] = '{_PDOConfirm.txtOut_mat_No.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.In_Mat_No)}] = '{_PDOConfirm.txtEntryCoilId.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.StartTime)}] = '{_PDOConfirm.txtStartTime.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.FinishTime)}] = '{_PDOConfirm.txtEndTime.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Shift)}] = '{_PDOConfirm.cbo_Shift.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.Team)}] = '{_PDOConfirm.cbo_Team.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.Sample_Flag)}] = '{_PDOConfirm.cbo_Sample_flag.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.Sample_Frqn_Code)}] = '{_PDOConfirm.cbo_SAMPLE_FRQN_CODE.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Fixed_Wt_Flag)}] = '{_PDOConfirm.cbo_Segement_Flag.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.End_Flag)}] = '{_PDOConfirm.cbo_end_flag.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.Scrap_Flag)}] = '{_PDOConfirm.cbo_SCRAP_FLAG.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.Oil_Flag)}] = '{_PDOConfirm.cbo_Oil.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.Winding_Dire)}] = '{_PDOConfirm.cbo_WINDING_Direction.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.BaseSurface)}] = '{_PDOConfirm.cbo_Base_Surface.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.ExitSleeveUseOrNot)}] = '{_PDOConfirm.cbo_ExitSleeveUseOrNot.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.ExitSleeveCode)}] = '{_PDOConfirm.cbo_Sleeve_Type_Exit.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.ExitSleeveDiameter)}] = '{_PDOConfirm.txt_Sleeve_Inner_Exit.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Out_Mat_Outer_Diameter)}] = '{_PDOConfirm.txtOuterDiameter.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Out_Mat_Inner)}] = '{_PDOConfirm.txtInner.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Out_Theory_Wt)}] = '{_PDOConfirm.txtTheory_Wt.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Out_Mat_Wt)}] = '{_PDOConfirm.txtWt.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Out_Mat_Gross_Wt)}] = '{_PDOConfirm.txtGsWt.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Out_Mat_Thick)}] = '{_PDOConfirm.txtThick.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Out_Mat_Width)}] = '{_PDOConfirm.txtWidth.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Out_Mat_Length)}] = '{_PDOConfirm.txtLength.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.St_No)}] = '{_PDOConfirm.txtSt_No.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Head_C40_Thick)}] = '{_PDOConfirm.txtHead_C40_Thick.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Mid_C40_Thick)}] = '{_PDOConfirm.txtMid_C40_Thick.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Tail_C40_Thick)}] = '{_PDOConfirm.txtTail_C40_Thick.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Head_C25_Thick)}] = '{_PDOConfirm.txtHead_C25_Thick.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Mid_C25_Thick)}] = '{_PDOConfirm.txtMid_C25_Thick.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Tail_C25_Thick)}] = '{_PDOConfirm.txtTail_C25_Thick.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Head_Pass_Num)}] = '{_PDOConfirm.txtHead_Pass_Num.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Mid_Pass_Num)}] = '{_PDOConfirm.txtMid_Pass_Num.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Tail_Pass_Num)}] = '{_PDOConfirm.txtTail_Pass_Num.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Paper_Code)}] = '{_PDOConfirm.cbo_PAPER_CODE.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.Paper_Req_Code)}] = '{_PDOConfirm.cbo_PAPER_REQ_CODE.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.Head_Paper_Length)}] = '{_PDOConfirm.txtOut_Paper_Head_Length.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Head_Paper_Width)}] = '{_PDOConfirm.txtOut_Paper_Head_Width.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Tail_Paper_Length)}] = '{_PDOConfirm.txtOut_Paper_Tail_Length.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Tail_Paper_Width)}] = '{_PDOConfirm.txtOut_Paper_Tail_Width.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Head_Hole_Position)}] = '{_PDOConfirm.txtHead_PunchHole_Position.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Head_LeaderLength)}] = '{_PDOConfirm.txtHead_LeaderLength.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Head_Leader_Width)}] = '{_PDOConfirm.txtHead_Leader_Width.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Head_Leader_Thickness)}] = '{_PDOConfirm.txtHead_Leader_Thickness.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Head_Leader_St_No)}] = '{_PDOConfirm.txtHead_leader_st_no.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Tail_Hole_Position)}] = '{_PDOConfirm.txtTail_PunchHole_Position.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Tail_LeaderLength)}] = '{_PDOConfirm.txtTail_LeaderLength.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Tail_Leader_Width)}] = '{_PDOConfirm.txtTail_Leader_Width.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Tail_Leader_Thickness)}] = '{_PDOConfirm.txtTail_Leader_Thickness.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Tail_Leader_St_No)}] = '{_PDOConfirm.txtTail_Leader_St_No.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Head_Off_Gauge)}] = '{_PDOConfirm.txtHEAD_OFF_GAUGE.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Tail_Off_Gauge)}] = '{_PDOConfirm.txtTail_off_gauge.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Pre_Grinding_Surface)}] = '{_PDOConfirm.cbo_PRE_GRINDING_SURFACE.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.Grinding_Count_Out)}] = '{_PDOConfirm.txtGRINDING_COUNT_OUT.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Grinding_Count_In)}] = '{_PDOConfirm.txtGRINDING_COUNT_IN.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Appoint_Grinding_Surface)}] = '{_PDOConfirm.cbo_APPOINT_GRINDING_SURFACE.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Surface_Accu_Code_In)}] = '{_PDOConfirm.cbo_SURFACE_ACCU_CODE_IN.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.Surface_Accu_Code_Out)}] = '{_PDOConfirm.cbo_SURFACE_ACCU_CODE_OUT.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.Surface_Accu_Code)}] = '{_PDOConfirm.cbo_SURFACE_ACCU_CODE.SelectedValue}',
        //                    [{nameof(PDOModel.L2L3_PDO.Inspector)}] = '{_PDOConfirm.txtInspector.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Hold_Flag)}] = '{_PDOConfirm.cbo_Hold_flag.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.Hold_Cause_Code)}] = '{_PDOConfirm.txtHold_cause_code.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.CreateTime)}] = '{GlobalVariableInstance.getTime}',
        //                    [{nameof(PDOModel.L2L3_PDO.D01_Code)}] = '{_PDOConfirm.txtCode_D1.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D02_Code)}] = '{_PDOConfirm.txtCode_D2.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D03_Code)}] = '{_PDOConfirm.txtCode_D3.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D04_Code)}] = '{_PDOConfirm.txtCode_D4.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D05_Code)}] = '{_PDOConfirm.txtCode_D5.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D06_Code)}] = '{_PDOConfirm.txtCode_D6.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D07_Code)}] = '{_PDOConfirm.txtCode_D7.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D08_Code)}] = '{_PDOConfirm.txtCode_D8.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D09_Code)}] = '{_PDOConfirm.txtCode_D9.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D10_Code)}] = '{_PDOConfirm.txtCode_D10.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D01_Level)}] = '{_PDOConfirm.txtLevel_D1.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D02_Level)}] = '{_PDOConfirm.txtLevel_D2.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D03_Level)}] = '{_PDOConfirm.txtLevel_D3.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D04_Level)}] = '{_PDOConfirm.txtLevel_D4.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D05_Level)}] = '{_PDOConfirm.txtLevel_D5.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D06_Level)}] = '{_PDOConfirm.txtLevel_D6.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D07_Level)}] = '{_PDOConfirm.txtLevel_D7.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D08_Level)}] = '{_PDOConfirm.txtLevel_D8.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D09_Level)}] = '{_PDOConfirm.txtLevel_D9.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D10_Level)}] = '{_PDOConfirm.txtLevel_D10.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D01_Origin)}] = '{_PDOConfirm.txtOrigin_D1.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D02_Origin)}] = '{_PDOConfirm.txtOrigin_D2.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D03_Origin)}] = '{_PDOConfirm.txtOrigin_D3.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D04_Origin)}] = '{_PDOConfirm.txtOrigin_D4.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D05_Origin)}] = '{_PDOConfirm.txtOrigin_D5.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D06_Origin)}] = '{_PDOConfirm.txtOrigin_D6.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D07_Origin)}] = '{_PDOConfirm.txtOrigin_D7.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D08_Origin)}] = '{_PDOConfirm.txtOrigin_D8.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D09_Origin)}] = '{_PDOConfirm.txtOrigin_D9.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D10_Origin)}] = '{_PDOConfirm.txtOrigin_D10.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D01_Percent)}] = '{_PDOConfirm.txtPercent_D1.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D02_Percent)}] = '{_PDOConfirm.txtPercent_D2.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D03_Percent)}] = '{_PDOConfirm.txtPercent_D3.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D04_Percent)}] = '{_PDOConfirm.txtPercent_D4.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D05_Percent)}] = '{_PDOConfirm.txtPercent_D5.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D06_Percent)}] = '{_PDOConfirm.txtPercent_D6.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D07_Percent)}] = '{_PDOConfirm.txtPercent_D7.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D08_Percent)}] = '{_PDOConfirm.txtPercent_D8.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D09_Percent)}] = '{_PDOConfirm.txtPercent_D9.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D10_Percent)}] = '{_PDOConfirm.txtPercent_D10.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D01_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D1.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D02_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D2.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D03_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D3.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D04_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D4.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D05_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D5.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D06_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D6.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D07_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D7.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D08_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D8.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D09_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D9.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D10_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D10.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D01_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D1.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D02_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D2.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D03_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D3.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D04_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D4.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D05_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D5.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D06_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D6.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D07_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D7.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D08_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D8.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D09_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D9.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D10_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D10.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D01_Pos_W)}] = '{_PDOConfirm.txtPos_W_D1.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D02_Pos_W)}] = '{_PDOConfirm.txtPos_W_D2.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D03_Pos_W)}] = '{_PDOConfirm.txtPos_W_D3.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D04_Pos_W)}] = '{_PDOConfirm.txtPos_W_D4.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D05_Pos_W)}] = '{_PDOConfirm.txtPos_W_D5.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D06_Pos_W)}] = '{_PDOConfirm.txtPos_W_D6.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D07_Pos_W)}] = '{_PDOConfirm.txtPos_W_D7.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D08_Pos_W)}] = '{_PDOConfirm.txtPos_W_D8.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D09_Pos_W)}] = '{_PDOConfirm.txtPos_W_D9.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D10_Pos_W)}] = '{_PDOConfirm.txtPos_W_D10.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D01_QGrade)}] = '{_PDOConfirm.txtQGRADE_D1.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D02_QGrade)}] = '{_PDOConfirm.txtQGRADE_D2.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D03_QGrade)}] = '{_PDOConfirm.txtQGRADE_D3.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D04_QGrade)}] = '{_PDOConfirm.txtQGRADE_D4.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D05_QGrade)}] = '{_PDOConfirm.txtQGRADE_D5.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D06_QGrade)}] = '{_PDOConfirm.txtQGRADE_D6.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D07_QGrade)}] = '{_PDOConfirm.txtQGRADE_D7.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D08_QGrade)}] = '{_PDOConfirm.txtQGRADE_D8.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D09_QGrade)}] = '{_PDOConfirm.txtQGRADE_D9.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D10_QGrade)}] = '{_PDOConfirm.txtQGRADE_D10.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D01_Sid)}] = '{_PDOConfirm.txtSid_D1.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D02_Sid)}] = '{_PDOConfirm.txtSid_D2.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D03_Sid)}] = '{_PDOConfirm.txtSid_D3.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D04_Sid)}] = '{_PDOConfirm.txtSid_D4.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D05_Sid)}] = '{_PDOConfirm.txtSid_D5.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D06_Sid)}] = '{_PDOConfirm.txtSid_D6.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D07_Sid)}] = '{_PDOConfirm.txtSid_D7.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D08_Sid)}] = '{_PDOConfirm.txtSid_D8.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D09_Sid)}] = '{_PDOConfirm.txtSid_D9.Text}',
        //                    [{nameof(PDOModel.L2L3_PDO.D10_Sid)}] = '{_PDOConfirm.txtSid_D10.Text}'
        //              Where [{nameof(PDOModel.L2L3_PDO.Out_Mat_No)}] = '{_PDOConfirm.cbo_Out_mat_NO.SelectedText}'";
        //    return strSql;
        //}

        ///// <summary>
        ///// PDO資料儲存
        ///// </summary>
        ///// <param name="Coil_ID"></param>
        ///// <returns></returns>
        //public static string Frm_PDOConfirm_PDOSave_DB_PDO()
        //{
        //    string strSql = "";
        //    #region SQL
        //    strSql = $@"Update [{nameof(PDOModel.L2L3_PDO)}] set 
        //                        [{nameof(PDOModel.L2L3_PDO.OrderNo)}] = '{_PDOConfirm.txtOrder_No.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.PlanNo)}] = '{_PDOConfirm.txtPlan_No.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Out_Mat_No)}] = '{_PDOConfirm.txtOut_mat_No.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.In_Mat_No)}] = '{_PDOConfirm.txtEntryCoilId.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.StartTime)}] = '{_PDOConfirm.txtStartTime.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.FinishTime)}] = '{_PDOConfirm.txtEndTime.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Shift)}] = '{_PDOConfirm.cbo_Shift.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.Team)}] = '{_PDOConfirm.cbo_Team.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.St_No)}] = '{_PDOConfirm.txtSt_No.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Out_Mat_Outer_Diameter)}] = '{_PDOConfirm.txtOuterDiameter.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Out_Mat_Inner)}] = '{_PDOConfirm.txtInner.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Out_Theory_Wt)}] = '{_PDOConfirm.txtTheory_Wt.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Out_Mat_Wt)}] = '{_PDOConfirm.txtWt.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Out_Mat_Gross_Wt)}] = '{_PDOConfirm.txtGsWt.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Out_Mat_Thick)}] = '{_PDOConfirm.txtThick.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Out_Mat_Width)}] = '{_PDOConfirm.txtWidth.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Out_Mat_Length)}] = '{_PDOConfirm.txtLength.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Head_C40_Thick)}] = '{_PDOConfirm.txtHead_C40_Thick.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Mid_C40_Thick)}] = '{_PDOConfirm.txtMid_C40_Thick.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Tail_C40_Thick)}] = '{_PDOConfirm.txtTail_C40_Thick.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Head_C25_Thick)}] = '{_PDOConfirm.txtHead_C25_Thick.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Mid_C25_Thick)}] = '{_PDOConfirm.txtMid_C25_Thick.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Tail_C25_Thick)}] = '{_PDOConfirm.txtTail_C25_Thick.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Head_Pass_Num)}] = '{_PDOConfirm.txtHead_Pass_Num.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Mid_Pass_Num)}] = '{_PDOConfirm.txtMid_Pass_Num.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Tail_Pass_Num)}] = '{_PDOConfirm.txtTail_Pass_Num.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Paper_Code)}] = '{_PDOConfirm.cbo_PAPER_CODE.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Paper_Req_Code)}] = '{_PDOConfirm.cbo_PAPER_REQ_CODE.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Head_Paper_Length)}] = '{_PDOConfirm.txtOut_Paper_Head_Length.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Head_Paper_Width)}] = '{_PDOConfirm.txtOut_Paper_Head_Width.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Tail_Paper_Length)}] = '{_PDOConfirm.txtOut_Paper_Tail_Length.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Tail_Paper_Width)}] = '{_PDOConfirm.txtOut_Paper_Tail_Width.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.ExitSleeveUseOrNot)}] = '{_PDOConfirm.cbo_ExitSleeveUseOrNot.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.ExitSleeveDiameter)}] = '{_PDOConfirm.txt_Sleeve_Inner_Exit.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.ExitSleeveCode)}] = '{_PDOConfirm.cbo_Sleeve_Type_Exit.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.Head_Hole_Position)}] = '{_PDOConfirm.txtHead_PunchHole_Position.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Head_LeaderLength)}] = '{_PDOConfirm.txtHead_LeaderLength.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Head_Leader_Width)}] = '{_PDOConfirm.txtHead_Leader_Width.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Head_Leader_Thickness)}] = '{_PDOConfirm.txtHead_Leader_Thickness.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Tail_Hole_Position)}] = '{_PDOConfirm.txtTail_PunchHole_Position.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Tail_LeaderLength)}] = '{_PDOConfirm.txtTail_LeaderLength.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Tail_Leader_Width)}] = '{_PDOConfirm.txtTail_Leader_Width.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Tail_Leader_Thickness)}] = '{_PDOConfirm.txtTail_Leader_Thickness.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Head_Leader_St_No)}] = '{_PDOConfirm.txtHead_leader_st_no.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Tail_Leader_St_No)}] = '{_PDOConfirm.txtTail_Leader_St_No.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Winding_Dire)}] = '{_PDOConfirm.cbo_WINDING_Direction.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.BaseSurface)}] = '{_PDOConfirm.cbo_Base_Surface.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.Inspector)}] = '{_PDOConfirm.txtInspector.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Hold_Flag)}] = '{_PDOConfirm.cbo_Hold_flag.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.Hold_Cause_Code)}] = '{_PDOConfirm.txtHold_cause_code.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Sample_Flag)}] = '{_PDOConfirm.cbo_Sample_flag.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.Fixed_Wt_Flag)}] = '{_PDOConfirm.cbo_Segement_Flag.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.End_Flag)}] = '{_PDOConfirm.cbo_end_flag.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.Scrap_Flag)}] = '{_PDOConfirm.cbo_SCRAP_FLAG.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.Oil_Flag)}] = '{_PDOConfirm.cbo_Oil.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.Sample_Frqn_Code)}] = '{_PDOConfirm.cbo_SAMPLE_FRQN_CODE.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.Surface_Accu_Code)}] = '{_PDOConfirm.cbo_SURFACE_ACCU_CODE.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.Head_Off_Gauge)}] = '{_PDOConfirm.txtHEAD_OFF_GAUGE.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Tail_Off_Gauge)}] = '{_PDOConfirm.txtTail_off_gauge.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Surface_Accu_Code_In)}] = '{_PDOConfirm.cbo_SURFACE_ACCU_CODE_IN.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.Surface_Accu_Code_Out)}] = '{_PDOConfirm.cbo_SURFACE_ACCU_CODE_OUT.SelectedValue}',
        //                        [{nameof(PDOModel.L2L3_PDO.Grinding_Count_In)}] = '{_PDOConfirm.txtGRINDING_COUNT_IN.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.Grinding_Count_Out)}] = '{_PDOConfirm.txtGRINDING_COUNT_OUT.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D01_Code)}] = '{_PDOConfirm.txtCode_D1.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D02_Code)}] = '{_PDOConfirm.txtCode_D2.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D03_Code)}] = '{_PDOConfirm.txtCode_D3.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D04_Code)}] = '{_PDOConfirm.txtCode_D4.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D05_Code)}] = '{_PDOConfirm.txtCode_D5.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D06_Code)}] = '{_PDOConfirm.txtCode_D6.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D07_Code)}] = '{_PDOConfirm.txtCode_D7.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D08_Code)}] = '{_PDOConfirm.txtCode_D8.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D09_Code)}] = '{_PDOConfirm.txtCode_D9.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D10_Code)}] = '{_PDOConfirm.txtCode_D10.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D01_Origin)}] = '{_PDOConfirm.txtOrigin_D1.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D02_Origin)}] = '{_PDOConfirm.txtOrigin_D2.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D03_Origin)}] = '{_PDOConfirm.txtOrigin_D3.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D04_Origin)}] = '{_PDOConfirm.txtOrigin_D4.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D05_Origin)}] = '{_PDOConfirm.txtOrigin_D5.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D06_Origin)}] = '{_PDOConfirm.txtOrigin_D6.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D07_Origin)}] = '{_PDOConfirm.txtOrigin_D7.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D08_Origin)}] = '{_PDOConfirm.txtOrigin_D8.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D09_Origin)}] = '{_PDOConfirm.txtOrigin_D9.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D10_Origin)}] = '{_PDOConfirm.txtOrigin_D10.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D01_Sid)}] = '{_PDOConfirm.txtSid_D1.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D02_Sid)}] = '{_PDOConfirm.txtSid_D2.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D03_Sid)}] = '{_PDOConfirm.txtSid_D3.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D04_Sid)}] = '{_PDOConfirm.txtSid_D4.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D05_Sid)}] = '{_PDOConfirm.txtSid_D5.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D06_Sid)}] = '{_PDOConfirm.txtSid_D6.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D07_Sid)}] = '{_PDOConfirm.txtSid_D7.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D08_Sid)}] = '{_PDOConfirm.txtSid_D8.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D09_Sid)}] = '{_PDOConfirm.txtSid_D9.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D10_Sid)}] = '{_PDOConfirm.txtSid_D10.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D01_Pos_W)}] = '{_PDOConfirm.txtPos_W_D1.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D02_Pos_W)}] = '{_PDOConfirm.txtPos_W_D2.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D03_Pos_W)}] = '{_PDOConfirm.txtPos_W_D3.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D04_Pos_W)}] = '{_PDOConfirm.txtPos_W_D4.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D05_Pos_W)}] = '{_PDOConfirm.txtPos_W_D5.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D06_Pos_W)}] = '{_PDOConfirm.txtPos_W_D6.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D07_Pos_W)}] = '{_PDOConfirm.txtPos_W_D7.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D08_Pos_W)}] = '{_PDOConfirm.txtPos_W_D8.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D09_Pos_W)}] = '{_PDOConfirm.txtPos_W_D9.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D10_Pos_W)}] = '{_PDOConfirm.txtPos_W_D10.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D01_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D1.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D02_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D2.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D03_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D3.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D04_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D4.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D05_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D5.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D06_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D6.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D07_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D7.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D08_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D8.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D09_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D9.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D10_Pos_L_Start)}] = '{_PDOConfirm.txtPos_L_Start_D10.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D01_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D1.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D02_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D2.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D03_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D3.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D04_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D4.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D05_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D5.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D06_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D6.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D07_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D7.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D08_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D8.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D09_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D9.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D10_Pos_L_End)}] = '{_PDOConfirm.txtPos_L_End_D10.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D01_Level)}] = '{_PDOConfirm.txtLevel_D1.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D02_Level)}] = '{_PDOConfirm.txtLevel_D2.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D03_Level)}] = '{_PDOConfirm.txtLevel_D3.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D04_Level)}] = '{_PDOConfirm.txtLevel_D4.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D05_Level)}] = '{_PDOConfirm.txtLevel_D5.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D06_Level)}] = '{_PDOConfirm.txtLevel_D6.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D07_Level)}] = '{_PDOConfirm.txtLevel_D7.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D08_Level)}] = '{_PDOConfirm.txtLevel_D8.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D09_Level)}] = '{_PDOConfirm.txtLevel_D9.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D10_Level)}] = '{_PDOConfirm.txtLevel_D10.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D01_Percent)}] = '{_PDOConfirm.txtPercent_D1.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D02_Percent)}] = '{_PDOConfirm.txtPercent_D2.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D03_Percent)}] = '{_PDOConfirm.txtPercent_D3.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D04_Percent)}] = '{_PDOConfirm.txtPercent_D4.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D05_Percent)}] = '{_PDOConfirm.txtPercent_D5.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D06_Percent)}] = '{_PDOConfirm.txtPercent_D6.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D07_Percent)}] = '{_PDOConfirm.txtPercent_D7.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D08_Percent)}] = '{_PDOConfirm.txtPercent_D8.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D09_Percent)}] = '{_PDOConfirm.txtPercent_D9.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D10_Percent)}] = '{_PDOConfirm.txtPercent_D10.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D01_QGrade)}] = '{_PDOConfirm.txtQGRADE_D1.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D02_QGrade)}] = '{_PDOConfirm.txtQGRADE_D2.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D03_QGrade)}] = '{_PDOConfirm.txtQGRADE_D3.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D04_QGrade)}] = '{_PDOConfirm.txtQGRADE_D4.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D05_QGrade)}] = '{_PDOConfirm.txtQGRADE_D5.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D06_QGrade)}] = '{_PDOConfirm.txtQGRADE_D6.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D07_QGrade)}] = '{_PDOConfirm.txtQGRADE_D7.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D08_QGrade)}] = '{_PDOConfirm.txtQGRADE_D8.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D09_QGrade)}] = '{_PDOConfirm.txtQGRADE_D9.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.D10_QGrade)}] = '{_PDOConfirm.txtQGRADE_D10.Text}',
        //                        [{nameof(PDOModel.L2L3_PDO.CreateTime)}] = '{Instance.getTime}',
        //                        [{nameof(L2L3_PDO.Head_Rough)}] = '{_PDOConfirm.txtHead_Rough.Text}',
        //                        [{nameof(L2L3_PDO.Mid_Rough)}] = '{_PDOConfirm.txtMid_Rough.Text}',
        //                        [{nameof(L2L3_PDO.Tail_Rough)}] = '{_PDOConfirm.txtTail_Rough.Text}',
        //                        [{nameof(L2L3_PDO.ProcessCode)}] = '{_PDOConfirm.txtProcessCode.Text}',
        //                        [{nameof(L2L3_PDO.Decoiler_Direction)}] = '{_PDOConfirm.cbo_Decoiler.Text}'
        //                  Where [{nameof(PDOModel.L2L3_PDO.Out_Mat_No)}] = '{_PDOConfirm.txtOut_mat_No.Text}'";
        //    #endregion
        //    return strSql;
        //}
        #endregion

        #region Login
        public static string SelectUserLoginInfo(string Password)
        {
            string strSql = $@"Select AuthorityData.* ,Frame.*
                               From [{nameof(TBL_AuthorityData)}] AuthorityData
                               Left Join [{nameof(TBL_AuthorityData_Frame)}] Frame 
                               On Frame.[{nameof(TBL_AuthorityData_Frame.User_ID)}] = AuthorityData.[{nameof(TBL_AuthorityData.User_ID)}]
                               Where AuthorityData.[{nameof(TBL_AuthorityData.User_ID)}] = '{UserSetupHandler.Instance.UserID}'
                               And AuthorityData.[{nameof(TBL_AuthorityData.Password)}] = '{Password}'";
            return strSql;
        }
        public static string SelectUserList()
        {
            string strSql = $@"Select [{nameof(TBL_AuthorityData.User_ID)}] 
                               From [{nameof(TBL_AuthorityData)}] ";
            return strSql;
        }
        #endregion

        #region ComboBoxHandler
        /// <summary>
        /// ComboBox選項
        /// </summary>
        /// <param name="_Type"></param>
        /// <returns></returns>
        public static string SelectComboBoxItems(Cbo_Type _Type)
        {
            string strSql = $@" Select [{nameof(TBL_ComboBoxItems.Cbo_Value)}],[{nameof(TBL_ComboBoxItems.Cbo_Text)}]
                                From [{nameof(TBL_ComboBoxItems)}] 
                                Where [{nameof(TBL_ComboBoxItems.Cbo_Type)}] = {(int)_Type} 
                                Order by [{nameof(TBL_ComboBoxItems.Cbo_Index)}] asc";
            return strSql;
        }
        /// <summary>
        /// 套筒ComboBox選項
        /// </summary>
        /// <returns></returns>
        public static string SelectSleeveComboBoxItems()
        {
            string strSql = $@"Select * From [{nameof(TBL_LookupTable_Sleeve)}] ORDER BY CAST({nameof(TBL_LookupTable_Sleeve.Sleeve_Code)} AS int ) ";
            return strSql;
        }
        /// <summary>
        /// 墊紙ComboBox選項
        /// </summary>
        /// <returns></returns>
        public static string SelectPaperComboBoxItems()
        {
            string strSql = $@"Select * From [{nameof(TBL_LookupTable_Paper)}]  ORDER BY CAST({nameof(TBL_LookupTable_Paper.Paper_Code)} AS int )";
            return strSql;
        }
        public static string SelectDeleteCode()
        {
            string strSql = $@"Select [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)}],
                                      [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)}]
                                 From [{nameof(L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition)}]";
            return strSql;
        }
        public static string SelectDeleteCoilList()
        {
            string strSql = $@"Select [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CoilNo)}] From [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete)}]";
            return strSql;
        }

        /// <summary>
        /// ComboBox選項說明
        /// </summary>
        /// <param name="_Type"></param>
        /// <returns></returns>
        public static string SelectComboBoxSpare(Cbo_Type _Type)
        {
            string strSql = $@" Select [{nameof(TBL_ComboBoxItems.Cbo_Text)}]
                                From [{nameof(TBL_ComboBoxItems)}] 
                                Where [{nameof(TBL_ComboBoxItems.Cbo_Type)}] = {(int)_Type} 
                                Order by [{nameof(TBL_ComboBoxItems.Cbo_Index)}] asc";
            return strSql;
        }

        public static string SelectGradeGroup()
        {
            string strSql = $" Select distinct [{nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)}] From [{nameof(GradeGroupsEntity.TBL_GradeGroups)}] ";
            return strSql;
        }
        public static string SelectGradeGroup_StNo()
        { 
            string strSql = $" Select distinct [{nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)}] From [{nameof(GradeGroupsEntity.TBL_GradeGroups)}] ";
            return strSql;
        }
        public static string SelectGradeGroup_Custmer()
        { 
            string strSql = $" Select distinct [{nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)}] From [{nameof(GradeGroupsEntity.TBL_GradeGroups)}] ";
            return strSql;
        }
        public static string SelectBeltPatterns()
        {
            string strSql = $"Select distinct [{nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)}] From [{nameof(BeltPatternsEntity.TBL_BeltPatterns)}] ";
            return strSql;
        }
        public static string SelectBeltMaterials()
        {
            string strSql = $"Select [{nameof(BeltMaterialEntity.TBL_BeltMaterials.MATERIAL_CODE)}] From [{nameof(BeltMaterialEntity.TBL_BeltMaterials)}]";
            return strSql;
        }
        /// <summary>
        /// 所有ComboBox項目
        /// </summary>
        public static string SelectComboBoxType()
        {
            string strSql = $@" Select distinct [{nameof(TBL_ComboBoxItems.Cbo_Type)}] , [{nameof(TBL_ComboBoxItems.Spare)}]
                                From [{nameof(TBL_ComboBoxItems)}] 
                                Where [{nameof(TBL_ComboBoxItems.Spare)}] <> ''
                                And [{nameof(TBL_ComboBoxItems.Cbo_Type)}] <> '{(int)Cbo_Type.EventLogLevel}'
                                And [{nameof(TBL_ComboBoxItems.Cbo_Type)}] <> '{(int)Cbo_Type.System}'
                                Order by [{nameof(TBL_ComboBoxItems.Cbo_Type)}] asc";
            return strSql;
        }
        #endregion

        #region Language Change
        public static string Fun_SelectLanguageSwitch()
        {
            string strSql = $@"Select * From [{nameof(TBL_LanguageSwitch)}]";
            return strSql;
        }
        #endregion

    }
    public class DBHandlerFuntion
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly DBHandlerFuntion INSTANCE = new DBHandlerFuntion();
        }

        public static DBHandlerFuntion Instance { get { return SingletonHolder.INSTANCE; } }

        /// <summary>
        /// 將DataRow組成InsertSql字串
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="strTableName">資料表名</param>
        /// <returns>字串</returns>
        public string Fun_GetInsertSqlFromDataRow(DataRow dr, string strTableName)
        {
            try
            {
                // 取得dr欄位名稱.
                string strColumns = Fun_GetColumnNameFromDataRow(dr);
                // 取得dr欄位內容.
                string strValues = Fun_GetColumnValueFromDataRow(dr);

                StringBuilder sbSql = new StringBuilder();

                sbSql.AppendLine("INSERT INTO " + strTableName);
                sbSql.AppendLine("(");
                sbSql.AppendLine(strColumns);
                sbSql.AppendLine(") VALUES (");
                sbSql.AppendLine(strValues);
                sbSql.AppendLine("); ");

                return sbSql.ToString();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 取得DataRow的ColumnName組成字串
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <returns>字串</returns>
        private string Fun_GetColumnNameFromDataRow(DataRow dr)
        {

            DataTable dt = dr.Table;

            StringBuilder sb = new StringBuilder();

            foreach (DataColumn dc in dt.Columns)
            {
                //if (dc.DataType == typeof(int)) { continue; }
                sb.Append(" \"" + dc.ColumnName + "\", ");
            }

            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();

        }

        /// <summary>
        /// 取得DataRow的ColumnValue組成字串
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <returns>字串</returns>
        private string Fun_GetColumnValueFromDataRow(DataRow dr)
        {

            DataTable dt = dr.Table;

            StringBuilder sb = new StringBuilder();

            foreach (DataColumn dc in dt.Columns)
            {
                //if (dc.DataType == typeof(int)) { continue; }
                if (dc.DataType.Name == "DateTime")
                {
                    string strDtime = "";
                    if (string.IsNullOrEmpty(dr[dc.ColumnName].ToString()))
                    {
                        strDtime = "NULL";
                        sb.Append(strDtime + ", ");
                    }
                    else
                    {
                        DateTime dtTime = Convert.ToDateTime(dr[dc.ColumnName]);
                        strDtime = dtTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        sb.Append("N'" + strDtime + "', ");
                    }
                   
                    
                }
                else
                {
                    sb.Append("N'" + dr[dc.ColumnName] + "', ");
                }

            }


            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();

        }
    }
}

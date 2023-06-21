using Core.Define;
using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.PDO
{
    public class PDOEntity
    {
        public class TBL_PDO : BaseRepositoryModel
        {
            public string Order_No { get; set; } = string.Empty;
            public string Plan_No { get; set; } = string.Empty;
            [PrimaryKey]
            public string Out_Coil_ID { get; set; } = string.Empty;
            [PrimaryKey]
            public string In_Coil_ID { get; set; } = string.Empty;

            public string OriPDI_Out_Coil_ID { get; set; } = string.Empty;

            public DateTime Start_Time { get; set; } 
            public DateTime Finish_Time { get; set; }
            public string Shift { get; set; } = string.Empty;
            public string Team { get; set; } = string.Empty;
            public string St_No { get; set; } = string.Empty;
            public int Out_Coil_Outer_Diameter { get; set; }

            public int Out_Coil_Inner_Diameter { get; set; }
            public int Out_Coil_Length { get; set; }
            public float Out_Coil_Thick { get; set; }
            public float Out_Coil_Head_C40_Thick { get; set; }
            public float Out_Coil_Mid_C40_Thick { get; set; }
            public float Out_Coil_Tail_C40_Thick { get; set; }
            public float Out_Coil_Head_C25_Thick { get; set; }
            public float Out_Coil_Mid_C25_Thick { get; set; }
            public float Out_Coil_Tail_C25_Thick { get; set; }
            public float Out_Coil_Width { get; set; }
            public float Out_Coil_Theoretical_Weight { get; set; }
            public float Out_Coil_Act_WT { get; set; }
            public float Out_Coil_Gross_WT { get; set; }
            public DateTime Out_Coil_Gross_WT_Time { get; set; }
            public int Head_Pass_Num { get; set; }
            public int Mid_Pass_Num { get; set; }
            public int Tail_Pass_Num { get; set; }
            public string Out_Coil_Use_Sleeve_Flag { get; set; }
            public int Out_Sleeve_Diameter { get; set; }
            public string Out_Sleeve_Type_Code { get; set; } = string.Empty;
            public float Out_Sleeve_Width { get; set; }
            public string Out_Paper_Req_Code  { get; set; } = string.Empty;
            public string Out_Paper_Code { get; set; } = string.Empty;
            public int Head_Paper_Length { get; set; }
            public float Head_Paper_Width { get; set; }
            public int Tail_Paper_Length { get; set; }
            public float Tail_Paper_Width { get; set; }
            public string Winding_Dire { get; set; } = string.Empty;
            public string Better_Surf_Ward_Code { get; set; } = string.Empty;
            public string Hold_Maker { get; set; } = string.Empty;
            public string Hold_Flag { get; set; } = string.Empty;
            public string Hold_Cause_Code { get; set; } = string.Empty;
            public string Sample_Flag { get; set; } = string.Empty;
            public string Fixed_Wt_Flag { get; set; } = string.Empty;
            public string Final_Coil_Flag { get; set; } = string.Empty;
            public string Scrap_Flag { get; set; } = string.Empty;
            public string Sample_Pos_Code { get; set; } = string.Empty;
            public string Surface_Accu_Code { get; set; } = string.Empty;
            public string Surface_Accu_Code_In { get; set; } = string.Empty;
            public string Surface_Accu_Code_Out { get; set; } = string.Empty;
            public float Head_Hole_Position { get; set; }
            public int Head_Leader_Length { get; set; }
            public float Head_Leader_Width { get; set; }
            public float Head_Leader_Thickness { get; set; }
            public string Head_Leader_St_No { get; set; }
            public int Coil_Head_End_Cut_Length { get; set; }
            public float Tail_Hole_Position { get; set; }
            public int Tail_Leader_Length { get; set; }
            public float Tail_Leader_Width { get; set; }
            public float Tail_Leader_Thickness { get; set; }
            public string Tail_Leader_St_No { get; set; }
            public int Coil_Tail_End_Cut_Length { get; set; }
            public string Appoint_Grinding_Surface { get; set; } = string.Empty;
            public int Grinding_Count_Out { get; set; }
            public int Grinding_Count_In { get; set; }
            public int Head_Off_Gauge { get; set; }
            public int Tail_Off_Gauge { get; set; }
            public float Out_Coil_Rough { get; set; }

            //public float Head_Rough { get; set; }
            //public float Mid_Rough { get; set; }
            //public float Tail_Rough { get; set; }

            public float Head_Rough_Rz { get; set; }
            public float Head_Rough_Ra { get; set; }
            public float Head_Rough_Rmax { get; set; }
            public float Mid_Rough_Rz { get; set; }
            public float Mid_Rough_Ra { get; set; }
            public float Mid_Rough_Rmax { get; set; }
            public float Tail_Rough_Rz { get; set; }
            public float Tail_Rough_Ra { get; set; }
            public float Tail_Rough_Rmax { get; set; }

            public string Pre_Grinding_Surface { get; set; }
            public string Oil_Flag { get; set; } = string.Empty;
            public string Process_Code { get; set; }

            public string Coil_Turned_Before_Processing { get; set; }
            public string Uncoil_Direction { get; set; }
            public float Recoiler_ActTen_Avg { get; set; }

            public DateTime Coil_Check_Time { get; set; }
            public string Coil_Check_Result { get; set; } = string.Empty;
            public DateTime PDO_Uploaded_Time { get; set; }
            public string PDO_Uploaded_Flag { get; set; } = string.Empty;  
            public string PDO_Uploaded_UserID { get; set; } = string.Empty;
            public string Exit_Scaned_CoilID { get; set; } = string.Empty;
            public string Exit_Scaned_UserID { get; set; } = string.Empty;

            public string D01_Code { get; set; } = string.Empty;            
            public string D01_Origin { get; set; } = string.Empty;
            public string D01_Sid { get; set; } = string.Empty;
            public string D01_Pos_W { get; set; } = string.Empty;
            public float D01_Pos_L_Start { get; set; }
            public float D01_Pos_L_End { get; set; }
            public string D01_Level { get; set; } = string.Empty;
            public int D01_Percent { get; set; }
            public string D01_QGrade { get; set; } = string.Empty;
            public string D02_Code { get; set; } = string.Empty;
            public string D02_Origin { get; set; } = string.Empty;
            public string D02_Sid { get; set; } = string.Empty;
            public string D02_Pos_W { get; set; } = string.Empty;
            public float D02_Pos_L_Start { get; set; }
            public float D02_Pos_L_End { get; set; }
            public string D02_Level { get; set; } = string.Empty;
            public int D02_Percent { get; set; }
            public string D02_QGrade { get; set; } = string.Empty;
            public string D03_Code { get; set; } = string.Empty;
            public string D03_Origin { get; set; } = string.Empty;
            public string D03_Sid { get; set; } = string.Empty;
            public string D03_Pos_W { get; set; } = string.Empty;
            public float D03_Pos_L_Start { get; set; }
            public float D03_Pos_L_End { get; set; }
            public string D03_Level { get; set; } = string.Empty;
            public int D03_Percent { get; set; }
            public string D03_QGrade { get; set; } = string.Empty;
            public string D04_Code { get; set; } = string.Empty;
            public string D04_Origin { get; set; } = string.Empty;
            public string D04_Sid { get; set; } = string.Empty;
            public string D04_Pos_W { get; set; } = string.Empty;
            public float D04_Pos_L_Start { get; set; }
            public float D04_Pos_L_End { get; set; }
            public string D04_Level { get; set; } = string.Empty;
            public int D04_Percent { get; set; }
            public string D04_QGrade { get; set; } = string.Empty;
            public string D05_Code { get; set; } = string.Empty;
            public string D05_Origin { get; set; } = string.Empty;
            public string D05_Sid { get; set; } = string.Empty;
            public string D05_Pos_W { get; set; } = string.Empty;
            public float D05_Pos_L_Start { get; set; }
            public float D05_Pos_L_End { get; set; }
            public string D05_Level { get; set; } = string.Empty;
            public int D05_Percent { get; set; }
            public string D05_QGrade { get; set; } = string.Empty;
            public string D06_Code { get; set; } = string.Empty;
            public string D06_Origin { get; set; } = string.Empty;
            public string D06_Sid { get; set; } = string.Empty;
            public string D06_Pos_W { get; set; } = string.Empty;
            public float D06_Pos_L_Start { get; set; }
            public float D06_Pos_L_End { get; set; }
            public string D06_Level { get; set; } = string.Empty;
            public int D06_Percent { get; set; }
            public string D06_QGrade { get; set; } = string.Empty;
            public string D07_Code { get; set; } = string.Empty;
            public string D07_Origin { get; set; } = string.Empty;
            public string D07_Sid { get; set; } = string.Empty;
            public string D07_Pos_W { get; set; } = string.Empty;
            public float D07_Pos_L_Start { get; set; }
            public float D07_Pos_L_End { get; set; }
            public string D07_Level { get; set; } = string.Empty;
            public int D07_Percent { get; set; }
            public string D07_QGrade { get; set; } = string.Empty;
            public string D08_Code { get; set; } = string.Empty;
            public string D08_Origin { get; set; } = string.Empty;
            public string D08_Sid { get; set; } = string.Empty;
            public string D08_Pos_W { get; set; } = string.Empty;
            public float D08_Pos_L_Start { get; set; }
            public float D08_Pos_L_End { get; set; }
            public string D08_Level { get; set; } = string.Empty;
            public int D08_Percent { get; set; }
            public string D08_QGrade { get; set; } = string.Empty;
            public string D09_Code { get; set; } = string.Empty;
            public string D09_Origin { get; set; } = string.Empty;
            public string D09_Sid { get; set; } = string.Empty;
            public string D09_Pos_W { get; set; } = string.Empty;
            public float D09_Pos_L_Start { get; set; }
            public float D09_Pos_L_End { get; set; }
            public string D09_Level { get; set; } = string.Empty;
            public int D09_Percent { get; set; }
            public string D09_QGrade { get; set; } = string.Empty;
            public string D10_Code { get; set; } = string.Empty;
            public string D10_Origin { get; set; } = string.Empty;
            public string D10_Sid { get; set; } = string.Empty;
            public string D10_Pos_W { get; set; } = string.Empty;
            public float D10_Pos_L_Start { get; set; }
            public float D10_Pos_L_End { get; set; }
            public string D10_Level { get; set; } = string.Empty;
            public int D10_Percent { get; set; }
            public string D10_QGrade { get; set; } = string.Empty;

            public override DateTime CreateTime { get; set; }
        }
    }
}

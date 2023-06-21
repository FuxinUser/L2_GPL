using Core.Define;
using DBService.Base;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.PDI
{
    public class PDIEntity
    {
        public class TBL_PDI : BaseRepositoryModel
        {
          
            public string Plan_No { get; set; } = string.Empty;
            [PrimaryKey]
            public string In_Coil_ID { get; set; } = string.Empty;
            [PrimaryKey]
            public int Mat_Seq_No  { get; set; }
            public string Plan_Type { get; set; } = string.Empty;
            public float In_Coil_Thick { get; set; }
            public float In_Coil_Width { get; set; }
            public int In_Coil_Wt { get; set; }
            public int In_Coil_Length { get; set; }
            public int In_Coil_Inner_Diameter { get; set; }
            public int In_Coil_Outer_Diameter { get; set; }
            public string In_Paper_Req_Code { get; set; } = string.Empty;
            public string In_Paper_Code { get; set; } = string.Empty;
            public int Head_Paper_Length { get; set; }
            public float Head_Paper_Width { get; set; }
            public int Tail_Paper_Length { get; set; }
            public float Tail_Paper_Width { get; set; }
            public string In_Sleeve_Type_Code { get; set; } = string.Empty;
            public int In_Sleeve_Diameter { get; set; }
            public string St_No { get; set; } = string.Empty;
            public float Density { get; set; }
            public string Surface_Accuracy_Acture { get; set; } = string.Empty;
            public string Surface_Accu_Code_Order { get; set; } = string.Empty;
            public string Surface_Accu_Code_In { get; set; } = string.Empty;
            public string Surface_Accu_Code_Out { get; set; } = string.Empty;
            public string Surface_Accuracy_Desc_Acture { get; set; } = string.Empty;
            public string Surface_Accu_Desc_Order { get; set; } = string.Empty;
            public string Surface_Accu_Desc_Out { get; set; } = string.Empty;
            public string Surface_Accu_Desc_In { get; set; } = string.Empty;

            public int Flatness_Avg_CR { get; set; }
            public string Better_Surf_Ward_Code { get; set; } = string.Empty;
            public string Uncoil_Direction { get; set; } = string.Empty;
            public string Origin_Code { get; set; } = string.Empty;
            public string Out_Coil_ID { get; set; } = string.Empty;
            public float Out_Coil_Thick { get; set; }
            public float Out_Coil_Thick_Min { get; set; }
            public float Out_Coil_Thick_Max { get; set; }
            public float Out_Coil_Width { get; set; }
            public int Out_Coil_Inner_Diameter { get; set; }
            public string Out_Paper_Req_Code { get; set; } = string.Empty;
            public string Out_Paper_Code { get; set; } = string.Empty;
            public string Out_Sleeve_Type_Code { get; set; } = string.Empty;
            public int Out_Sleeve_Diamter { get; set; }
            public int Pack_Mode { get; set; }
            public string Pack_Type_Code { get; set; } = string.Empty;
            public string Prev_Whole_Backlog_Code { get; set; } = string.Empty;
            public string Next_Whole_Backlog_Code { get; set; } = string.Empty;
            public string Head_Leader_Attached { get; set; } = string.Empty;
            public float Head_Hole_Position { get; set; }
            public float Head_Leader_Thickness { get; set; }
            public float Head_Leader_Width { get; set; }
            public int Head_Leader_Length { get; set; }
            public string Head_Leader_St_No { get; set; } = string.Empty;
            public string Tail_Leader_Attached { get; set; } = string.Empty;
            public float Tail_Hole_Position { get; set; }
            public float Tail_Leader_Thickness { get; set; }
            public float Tail_Leader_Width { get; set; }
            public int Tail_Leader_Length { get; set; }
            public string Tail_Leader_St_No { get; set; } = string.Empty;
            public string Grind_Flag { get; set; } = string.Empty;
            public string Appoint_Grinding_Surface { get; set; } = string.Empty;
            public int Grinding_Count_Out { get; set; }
            public int Grinding_Count_In { get; set; }
            public int Head_Off_Gauge { get; set; }
            public int Tail_Off_Gauge { get; set; }
            public string Pre_Grinding_Surface { get; set; } = string.Empty;
            public string Skim_Flag { get; set; } = string.Empty;
            public string Polishing_Type { get; set; } = string.Empty;
            public string Repair_Type { get; set; } = string.Empty;
            public string Repair_Remark { get; set; } = string.Empty;
            public string Test_Plan_No { get; set; } = string.Empty;
            public string Qc_Rmark { get; set; } = string.Empty;
            public string Sg_Sign { get; set; } = string.Empty;
            public string Process_Code { get; set; } = string.Empty;
            public float Ys_Stand { get; set; }
            public float Ys_Max { get; set; }
            public float Ys_Min { get; set; }

            public string Order_No { get; set; } = string.Empty;
            public string Order_Cust_Code { get; set; } = string.Empty;
            public string Order_Cust_Ename { get; set; } = string.Empty;
            public string Order_Cust_Cname { get; set; } = string.Empty;



            #region Defect
            public string D01_Code { get; set; } = string.Empty;
            public string D01_Origin { get; set; } = string.Empty;
            public string D01_Sid { get; set; } = string.Empty;
            public string D01_Pos_W { get; set; } = string.Empty;
            public int D01_Pos_L_Start { get; set; }
            public int D01_Pos_L_End { get; set; }
            public string D01_Level { get; set; } = string.Empty;
            public int D01_Percent { get; set; }
            public string D01_QGrade { get; set; } = string.Empty;
            public string D02_Code { get; set; } = string.Empty;
            public string D02_Origin { get; set; } = string.Empty;
            public string D02_Sid { get; set; } = string.Empty;
            public string D02_Pos_W { get; set; } = string.Empty;
            public int D02_Pos_L_Start { get; set; }
            public int D02_Pos_L_End { get; set; }
            public string D02_Level { get; set; } = string.Empty;
            public int D02_Percent { get; set; }
            public string D02_QGrade { get; set; } = string.Empty;
            public string D03_Code { get; set; } = string.Empty;
            public string D03_Origin { get; set; } = string.Empty;
            public string D03_Sid { get; set; } = string.Empty;
            public string D03_Pos_W { get; set; } = string.Empty;
            public int D03_Pos_L_Start { get; set; }
            public int D03_Pos_L_End { get; set; }
            public string D03_Level { get; set; } = string.Empty;
            public int D03_Percent { get; set; }
            public string D03_QGrade { get; set; } = string.Empty;
            public string D04_Code { get; set; } = string.Empty;
            public string D04_Origin { get; set; } = string.Empty;
            public string D04_Sid { get; set; } = string.Empty;
            public string D04_Pos_W { get; set; } = string.Empty;
            public int D04_Pos_L_Start { get; set; }
            public int D04_Pos_L_End { get; set; }
            public string D04_Level { get; set; }
            public int D04_Percent { get; set; }
            public string D04_QGrade { get; set; } = string.Empty;
            public string D05_Code { get; set; } = string.Empty;
            public string D05_Origin { get; set; } = string.Empty;
            public string D05_Sid { get; set; } = string.Empty;
            public string D05_Pos_W { get; set; } = string.Empty;
            public int D05_Pos_L_Start { get; set; }
            public int D05_Pos_L_End { get; set; }
            public string D05_Level { get; set; } = string.Empty;
            public int D05_Percent { get; set; }
            public string D05_QGrade { get; set; } = string.Empty;
            public string D06_Code { get; set; } = string.Empty;
            public string D06_Origin { get; set; } = string.Empty;
            public string D06_Sid { get; set; } = string.Empty;
            public string D06_Pos_W { get; set; } = string.Empty;
            public int D06_Pos_L_Start { get; set; }
            public int D06_Pos_L_End { get; set; }
            public string D06_Level { get; set; } = string.Empty;
            public int D06_Percent { get; set; }
            public string D06_QGrade { get; set; } = string.Empty;
            public string D07_Code { get; set; } = string.Empty;
            public string D07_Origin { get; set; } = string.Empty;
            public string D07_Sid { get; set; } = string.Empty;
            public string D07_Pos_W { get; set; } = string.Empty;
            public int D07_Pos_L_Start { get; set; }
            public int D07_Pos_L_End { get; set; }
            public string D07_Level { get; set; }
            public int D07_Percent { get; set; }
            public string D07_QGrade { get; set; } = string.Empty;
            public string D08_Code { get; set; } = string.Empty;
            public string D08_Origin { get; set; } = string.Empty;
            public string D08_Sid { get; set; } = string.Empty;
            public string D08_Pos_W { get; set; } = string.Empty;
            public int D08_Pos_L_Start { get; set; }
            public int D08_Pos_L_End { get; set; }
            public string D08_Level { get; set; } = string.Empty;
            public int D08_Percent { get; set; }
            public string D08_QGrade { get; set; } = string.Empty;
            public string D09_Code { get; set; } = string.Empty;
            public string D09_Origin { get; set; } = string.Empty;
            public string D09_Sid { get; set; } = string.Empty;
            public string D09_Pos_W { get; set; } = string.Empty;
            public int D09_Pos_L_Start { get; set; }
            public int D09_Pos_L_End { get; set; }
            public string D09_Level { get; set; } = string.Empty;
            public int D09_Percent { get; set; }
            public string D09_QGrade { get; set; } = string.Empty;
            public string D10_Code { get; set; } = string.Empty;
            public string D10_Origin { get; set; } = string.Empty;
            public string D10_Sid { get; set; } = string.Empty;
            public string D10_Pos_W { get; set; } = string.Empty;
            public int D10_Pos_L_Start { get; set; }
            public int D10_Pos_L_End { get; set; }
            public string D10_Level { get; set; } = string.Empty;
            public int D10_Percent { get; set; }
            public string D10_QGrade { get; set; } = string.Empty;
            #endregion




            public string Create_UserID { get; set; } = string.Empty;


            public override DateTime CreateTime { get; set; }

            public DateTime Entry_Arrive_Time { get; set; }
            public DateTime Entry_Scaned_Time { get; set; }
            public string Entry_CoilID_Checked { get; set; } = string.Empty;

            // 以下非MMS PDI報文相關資料
            public string Entry_Scaned_CoilID { get; set; } = string.Empty;
            public string Entry_Scaned_UserID { get; set; } = string.Empty;
            public string Origin_CoilID { get; set; } = string.Empty;
     
            public string Is_Delete { get; set; } = string.Empty;
            public string Delete_UserID { get; set; } = string.Empty;
         
            public string Coil_Reject_UserID { get; set; } = string.Empty;
    
            public DateTime Delete_DateTime { get; set; }

            public DateTime Coil_Reject_Time { get; set; } 
            public DateTime Product_Start_Time { get; set; }
            public DateTime StarTime { get; set; }
            public DateTime EndTime { get; set; }
            public float Scraped_Weight { get; set; }
            public string HasScraped { get; set; } = string.Empty;

       
         
            public string Is_Dummy_Coil { get; set; }

            public string Is_Info_WMS_Down { get; set; } = "0";
            [IgnoreReflction]
            public int StripYieldStress { get; set; } = 0;          //目前還沒有結論這個值怎麼給


        }

    }

}

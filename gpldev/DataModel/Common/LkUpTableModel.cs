using System.Collections.Generic;

namespace DataMod.PLC
{  
    public class LkUpTableModel
    {
     
        public class GenPDODataPara
        {
            // 頭部切廢長度
            public int ScrapedLengthEntry { get; set; } = 0;
            // 尾部切廢長度
            public int ScrapedLengthExit { get; set; } = 0;
            // 是否有焊接
            public string NoLeaderCode { get; set; } = string.Empty;
            // 墊紙重量
            public float PaperWt { get; set; } = 0.0f;

            public float Paper_Width { get; set; } = 0.0f;
            public float Paper_Thick { get; set; } = 0.0f;

            // 套桶重量
            public float SleeveWt { get; set; } = 0;
            // 頭段導帶重量
            public float HeadLeaderWt { get; set; } = 0.0f;
            // 尾段導帶重量
            public float TailLeaderWt { get; set; } = 0.0f;

            // 頭部出口實際道次
            public int HeadPassNum { get; set; } = 0;         // 查表
            // 中部出口實際道次
            public int MidPassNum { get; set; } = 0;          // 查表
            // 尾部出口實際道次
            public int TailPassNum { get; set; } = 0;         // 查表

            // 分捲標記
            public string FixedWtFlag { get; set; } = "0";      //預設為0


            // 表面精度代碼
            public string NewSurfaceAccuCode { get; set; } = string.Empty;
            public int Grinding_Count_Out { get; set; }         // 鋼卷外表面研磨次數
            public int Grinding_Count_In { get; set; }          // 鋼卷內表面研磨次數
            public string Pre_Grinding_Surface { get; set; }    // 鋼卷上次研磨面

            // 班次
            public string Shift { get; set; } = string.Empty;
            public string Team { get; set; } = string.Empty;

            //卷曲张力
            public float RollTension { get; set; } = 0;

            // 墊紙重量 + 套桶重量 + 頭段導帶重量 + 尾段導帶重量
            public float TotalWt
            {
                get
                {
                    return PaperWt + SleeveWt + HeadLeaderWt + TailLeaderWt;
                }
            }
        }

        public class Preset204 {
            public float FlatenerDepth1 { get; set; } = 0.0f;   // TBL_Lookup_Flattener -> Intermesh_Num1
            public float FlatenerDepth2 { get; set; } = 0.0f;   // TBL_Lookup_Flattener -> Intermesh_Num2
            public int SleeveThickness { get; set; } = 0;       // TBL_LookupTable_Sleeve -> Sleeve_Thickness
            public int SleeveWidth { get; set; } = 0;           // TBL_LookupTable_Sleeve -> Sleeve_Width
            public float UnitTension { get; set; } = 0.0f;           // TBL_LookupTable_LineTension -> UnitTension


          

        }



    }
}

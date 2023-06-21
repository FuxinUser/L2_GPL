using Core.Define;
using Core.Util;
using DataMod.PLC;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using MsgStruct;
using System;
using System.Collections.Generic;
using static DBService.Repository.DefectData.CoilDefectDataEntity;
using static DBService.Repository.PDO.PDOEntity;

namespace MsgConvert.DBTable
{
    public static class PDOEntityFactory
    {
        public static TBL_PDO GenEmptyPDO(this PDIEntity.TBL_PDI pdi)
        {

            var pdo = new TBL_PDO
            {
                Start_Time = DateTime.Now,           // 開始時間
                //FinishTime = DateTime.Now,          // 結束時間
                Order_No = pdi.Order_No,             // 合同號
                Plan_No = pdi.Plan_No,               // 計畫號
                Out_Coil_ID = pdi.Out_Coil_ID,        // 出口鋼捲號
                In_Coil_ID = pdi.In_Coil_ID,       // 入口鋼捲號
                //Shift                             // 班次
                //Team                              // 班別
                St_No = pdi.St_No,
                //Out_Mat_Outer_Diameter            // 出口卷径
                //Out_Mat_Inner                     // 出口卷径
                //Out_Mat_Length                    // 出口卷長度
                //Out_Mat_Thick                     // 出口厚度
                //Head_C40_Thick                    // 出口頭部C40厚度
                //Mid_C40_Thick                     // 出口中部C40厚度
                //Tail_C40_Thick                    // 出口尾部C40厚度
                //Head_C25_Thick                    // 出口頭部C25厚度
                //Mid_C25_Thick                     // 出口中部C25厚度
                //Tail_C25_Thick                    // 出口尾部C25厚度
                //Out_Mat_Width                     // 出口寬度 預設帶入口捲寬度（L2）
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


        public static TBL_PDO CombinationPDO(this TBL_PDO pdo, L1L2Rcv.Msg_102_PDO msg, PDIEntity.TBL_PDI pdi, LkUpTableModel.GenPDODataPara pdoPara, string outCoilNo)
        {

            pdo.Start_Time = pdi.StarTime;             //  開始時間
            pdo.Finish_Time = pdi.EndTime;            //  結束時間
            pdo.Order_No = pdi.Order_No;             //  合同號
            pdo.Plan_No = pdi.Plan_No;               //  計畫號
            pdo.Out_Coil_ID = outCoilNo;             //  出口鋼捲號
            pdo.In_Coil_ID = pdi.In_Coil_ID;          //  入口鋼捲號
            pdo.Shift = pdoPara.Shift;              //  Shift                             // 班次
            pdo.Team = pdoPara.Team;                //  Team                              // 班別
            pdo.St_No = pdi.St_No;

            // L1 102 PDO資料
            pdo.Out_Coil_Outer_Diameter = msg.Coilouterdiameter;                     // 出口卷外徑
            pdo.Out_Coil_Inner_Diameter = msg.CoilInnerdiameter;                              // 出口卷內徑
            pdo.Out_Coil_Length = msg.CoilLength;                              // 出口卷長度 L1 m-> L3 m
            pdo.Out_Coil_Thick = (float)msg.CoilThickness / 1000;                      // 出口捲厚度 : 10^-3  (0.3-5.0)
            #region HMI - 人工輸入
            //Out_Mat_Thick                     // 出口厚度 
            //Head_C40_Thick                    // 出口頭部C40厚度
            //Mid_C40_Thick                     // 出口中部C40厚度
            //Tail_C40_Thick                    // 出口尾部C40厚度
            //Head_C25_Thick                    // 出口頭部C25厚度
            //Mid_C25_Thick                     // 出口中部C25厚度  
            //Tail_C25_Thick                    // 出口尾部C25厚度                
            #endregion

            pdo.Out_Coil_Width = msg.CoilWidth;//pdi.In_Coil_Width;                   // 出口寬度 帶L1給予捲寬度
            pdo.Out_Coil_Theoretical_Weight = msg.Coiltheoreticalweight;              // 理論重
            pdo.Out_Coil_Act_WT = pdi.In_Coil_Wt;                                     // 未收到秤重訊號時  净重 = pdi入料重
            pdo.Out_Coil_Gross_WT = pdi.In_Coil_Wt;                                   // 未收到秤重訊號時  毛重 = pdi入料重


            pdo.Head_Pass_Num = pdoPara.HeadPassNum;          // 頭部出口實際道次
            pdo.Mid_Pass_Num = pdoPara.MidPassNum;            // 中部出口實際道次
            pdo.Tail_Pass_Num = pdoPara.TailPassNum;          // 尾部出口實際道次

            pdo.Out_Coil_Use_Sleeve_Flag = IsUse(msg.SleeveInstallled); // 出口是否用套筒
            pdo.Out_Sleeve_Diameter = msg.CoilInnerdiameter;                 // 出口套筒內徑            
            pdo.Out_Sleeve_Type_Code = pdi.Out_Sleeve_Type_Code;
            pdo.Out_Paper_Req_Code  = msg.PaperInstalled.ToString();        // 出口墊紙方式

            pdo.Out_Paper_Code = string.Empty;//pdi.In_Paper_Code;

            if (msg.PaperInstalled == 1)
            {
                pdo.Out_Paper_Code = pdi.In_Paper_Code;                // 出口墊紙類型
                pdo.Head_Paper_Length = msg.CoilLength;         // 頭部墊紙長度
                pdo.Head_Paper_Width = pdoPara.Paper_Width;                       // 頭部墊紙寬度
                pdo.Tail_Paper_Length =   msg.CoilLength;       // 尾部墊紙長度
                pdo.Tail_Paper_Width = pdoPara.Paper_Width;                        // 尾部墊紙寬度
            }


            pdo.Winding_Dire = DeviceDef.WindingDirectionDown;              // 捲曲方向 GPL固定下收

            pdo.Better_Surf_Ward_Code = BaseSurfaceValue(msg.ActualUncoilDirection, pdi.Appoint_Grinding_Surface);     // 實際好面朝向
            #region HMI - 人工輸入

            //Inspector                         // 封鎖責任者
            pdo.Hold_Maker = string.Empty;
            //Hold_Flag                         // 封鎖標記
            pdo.Hold_Flag = string.Empty;
            //Hold_Cause_Code                   // 封鎖原因代碼
            pdo.Hold_Cause_Code = string.Empty;
            #endregion
            pdo.Sample_Flag = MMSSysDef.Cmd.NotUse;                  // 取樣標記 GPL連續拉入 故不做取樣
            #region HMI - 人工輸入
            pdo.Fixed_Wt_Flag = pdoPara.FixedWtFlag;                      // 分標籤記 GPL不分捲，除非斷帶，由人工判斷輸入; 判斷有無分捲紀錄 (分捲Table)               
                                                                          //End_Flag                                                  // 最終標籤記
                                                                          //Scrap_Flag                                                // 廢品標記
            #endregion
            
            pdo.Sample_Pos_Code = string.Empty; //取樣位置代碼
            //Sample_Frqn_Code                                              // 取樣位置 用户要求增加 100头 001尾
            //pdo.Surface_Accu_Code = pdoPara.NewSurfaceAccuCode;           // 表面精度代碼 根據邏輯判斷（L2）

            if (msg.HeadEndLeader == PlcSysDef.Cmd.Use)
            {
                pdo.Head_Hole_Position = pdi.Head_Hole_Position;                // 頭部打孔位置
                pdo.Head_Leader_Length = pdi.Head_Leader_Length;                 // 頭部導帶長度
                pdo.Head_Leader_Width = pdi.Head_Leader_Width;                  // 頭部導帶寬度
                pdo.Head_Leader_Thickness = pdi.Head_Leader_Thickness;              // 頭部導帶厚度
                pdo.Head_Leader_St_No = pdi.Head_Leader_St_No;                  // 頭部導帶鋼種
            }

            if (msg.TailEndLeader == PlcSysDef.Cmd.Use)
            {
                pdo.Tail_Hole_Position = pdi.Tail_Hole_Position;                // 尾部打孔位置
                pdo.Tail_Leader_Length = pdi.Tail_Leader_Length;                 // 尾部導帶長度
                pdo.Tail_Leader_Width = pdi.Tail_Leader_Width;                  // 尾部導帶寬度
                pdo.Tail_Leader_Thickness = pdi.Tail_Leader_Thickness;              // 尾部導帶厚度       
                pdo.Tail_Leader_St_No = pdi.Tail_Leader_St_No;                  // 尾部導帶鋼種
            }


            pdo.Head_Off_Gauge = pdi.Head_Off_Gauge;                        // 頭部未軋製區域
            pdo.Tail_Off_Gauge = pdi.Tail_Off_Gauge;                        // 尾部未軋製區域


            pdo.Pre_Grinding_Surface = pdoPara.Pre_Grinding_Surface;        // 鋼卷上次研磨面                                                                                                                        
            pdo.Grinding_Count_Out = pdoPara.Grinding_Count_Out;            // 鋼卷外表面研磨次數
            pdo.Grinding_Count_In = pdoPara.Grinding_Count_In;              // 鋼卷內表面研磨次數


            // Out In 需判斷是否有翻面



            pdo.Appoint_Grinding_Surface = pdi.Appoint_Grinding_Surface;    //鋼卷指定研磨面

            pdo.Oil_Flag = msg.DegreasingFlag.ToString();            // 鋼卷是否有油


            pdo.Surface_Accu_Code = pdi.Surface_Accu_Code_Order; //表面精度代碼(订单表面精度代码)
            //pdo.Surface_Accu_Code = pdi.Surface_Accuracy_Acture;//实际表面精度
            // 實際開捲方向 上開 Up => 內側改變
            // 實際開捲方向 下開 Down =>返國來
            pdo.Surface_Accu_Code_In = msg.ActualUncoilDirection == PlcSysDef.Cmd.UnCoilUp ? pdi.Surface_Accu_Code_Out : pdi.Surface_Accu_Code_In; // 內表面精度代碼
            pdo.Surface_Accu_Code_Out = msg.ActualUncoilDirection == PlcSysDef.Cmd.UnCoilUp ? pdi.Surface_Accu_Code_In : pdi.Surface_Accu_Code_Out; // 外表面精度代碼

            pdo.Process_Code = pdi.Process_Code;
            
            //實際開捲方向From L1
            pdo.Uncoil_Direction = msg.ActualUncoilDirection == 0 ? MMSSysDef.Cmd.UnCoilUpStr : MMSSysDef.Cmd.UnCoilDownStr;

            //卷曲张力平均值  暫時先用102報文RecoilerTension值代替
            pdo.Recoiler_ActTen_Avg = pdoPara.RollTension; //msg.RecoilerTension; 


            return pdo;
        }

        public static void LoadDefectData(this TBL_PDO pdo, IEnumerable<L3L2_TBL_DefectData> defectDatas)
        {
            int cnt = 1;

            foreach (L3L2_TBL_DefectData defectData in defectDatas)
            {
                switch (cnt)
                {
                    case 1:
                        pdo.D01_Code = defectData.DefectCode.ToString();        // Defect Code
                        pdo.D01_Origin = defectData.DefectOrigin;               // Defect From
                        pdo.D01_Sid = defectData.DefectSide;

                        pdo.D01_Pos_W = defectData.DefectPositionWidthDirection;                // Defect Position Width
                        pdo.D01_Pos_L_End = defectData.DefectPoLengthEndDirection;         // Defect Length Start
                        pdo.D01_Pos_L_Start = defectData.DefectPosLengthStartDirection;    // Defect Length End
                        pdo.D01_Level = DefectClass(defectData.DefectLevel);                                 // Defect Class
                        pdo.D01_Percent = defectData.DefectPercent.ToNullable<int>() ?? 0;        // Defect Percent 人工輸入?
                                                                                                  //pdo.D01_QGrade = ?  // Defect Grade人工輸入?
                        break;

                    case 2:
                        pdo.D02_Code = defectData.DefectCode.ToString();        // Defect Code
                        pdo.D02_Origin = defectData.DefectOrigin;               // Defect From
                        pdo.D02_Sid = defectData.DefectSide;

                        pdo.D02_Pos_W = defectData.DefectPositionWidthDirection;                // Defect Position Width
                        pdo.D02_Pos_L_End = defectData.DefectPoLengthEndDirection;         // Defect Length Start
                        pdo.D02_Pos_L_Start = defectData.DefectPosLengthStartDirection;    // Defect Length End
                        pdo.D02_Level = DefectClass(defectData.DefectLevel);                                 // Defect Class
                        pdo.D02_Percent = defectData.DefectPercent.ToNullable<int>() ?? 0;        // Defect Percent 人工輸入?
                        //pdo.D02_QGrade = ?  // Defect Grade人工輸入?

                        break;

                    case 3:

                        pdo.D03_Code = defectData.DefectCode.ToString();        // Defect Code
                        pdo.D03_Origin = defectData.DefectOrigin;               // Defect From
                        pdo.D03_Sid = defectData.DefectSide;

                        pdo.D03_Pos_W = defectData.DefectPositionWidthDirection;                // Defect Position Width
                        pdo.D03_Pos_L_End = defectData.DefectPoLengthEndDirection;         // Defect Length Start
                        pdo.D03_Pos_L_Start = defectData.DefectPosLengthStartDirection;    // Defect Length End
                        pdo.D03_Level = DefectClass(defectData.DefectLevel);                                 // Defect Class
                        pdo.D03_Percent = defectData.DefectPercent.ToNullable<int>() ?? 0;        // Defect Percent 人工輸入?
                        //pdo.D03_QGrade = ?  // Defect Grade人工輸入?

                        break;

                    case 4:

                        pdo.D04_Code = defectData.DefectCode.ToString();        // Defect Code
                        pdo.D04_Origin = defectData.DefectOrigin;               // Defect From
                        pdo.D04_Sid = defectData.DefectSide;

                        pdo.D04_Pos_W = defectData.DefectPositionWidthDirection;                // Defect Position Width
                        pdo.D04_Pos_L_End = defectData.DefectPoLengthEndDirection;         // Defect Length Start
                        pdo.D04_Pos_L_Start = defectData.DefectPosLengthStartDirection;    // Defect Length End
                        pdo.D04_Level = DefectClass(defectData.DefectLevel);                                 // Defect Class
                        pdo.D04_Percent = defectData.DefectPercent.ToNullable<int>() ?? 0;        // Defect Percent 人工輸入?
                        //pdo.D04_QGrade = ?  // Defect Grade人工輸入?

                        break;

                    case 5:

                        pdo.D05_Code = defectData.DefectCode.ToString();        // Defect Code
                        pdo.D05_Origin = defectData.DefectOrigin;               // Defect From
                        pdo.D05_Sid = defectData.DefectSide;

                        pdo.D05_Pos_W = defectData.DefectPositionWidthDirection;                // Defect Position Width
                        pdo.D05_Pos_L_End = defectData.DefectPoLengthEndDirection;         // Defect Length Start
                        pdo.D05_Pos_L_Start = defectData.DefectPosLengthStartDirection;    // Defect Length End
                        pdo.D05_Level = DefectClass(defectData.DefectLevel);                                 // Defect Class
                        pdo.D05_Percent = defectData.DefectPercent.ToNullable<int>() ?? 0;        // Defect Percent 人工輸入?
                        //pdo.D05_QGrade = ?  // Defect Grade人工輸入?

                        break;

                    case 6:

                        pdo.D06_Code = defectData.DefectCode.ToString();        // Defect Code
                        pdo.D06_Origin = defectData.DefectOrigin;               // Defect From
                        pdo.D06_Sid = defectData.DefectSide;

                        pdo.D06_Pos_W = defectData.DefectPositionWidthDirection;                // Defect Position Width
                        pdo.D06_Pos_L_End = defectData.DefectPoLengthEndDirection;         // Defect Length Start
                        pdo.D06_Pos_L_Start = defectData.DefectPosLengthStartDirection;    // Defect Length End
                        pdo.D06_Level = DefectClass(defectData.DefectLevel);                                 // Defect Class
                        pdo.D06_Percent = defectData.DefectPercent.ToNullable<int>() ?? 0;        // Defect Percent 人工輸入?
                        //pdo.D06_QGrade = ?  // Defect Grade人工輸入?

                        break;

                    case 7:

                        pdo.D07_Code = defectData.DefectCode.ToString();        // Defect Code
                        pdo.D07_Origin = defectData.DefectOrigin;               // Defect From
                        pdo.D07_Sid = defectData.DefectSide;

                        pdo.D07_Pos_W = defectData.DefectPositionWidthDirection;                // Defect Position Width
                        pdo.D07_Pos_L_End = defectData.DefectPoLengthEndDirection;         // Defect Length Start
                        pdo.D07_Pos_L_Start = defectData.DefectPosLengthStartDirection;    // Defect Length End
                        pdo.D07_Level = DefectClass(defectData.DefectLevel);                                 // Defect Class
                        pdo.D07_Percent = defectData.DefectPercent.ToNullable<int>() ?? 0;        // Defect Percent 人工輸入?
                        //pdo.D07_QGrade = ?  // Defect Grade人工輸入?

                        break;

                    case 8:

                        pdo.D08_Code = defectData.DefectCode.ToString();        // Defect Code
                        pdo.D08_Origin = defectData.DefectOrigin;               // Defect From
                        pdo.D08_Sid = defectData.DefectSide;

                        pdo.D08_Pos_W = defectData.DefectPositionWidthDirection;                // Defect Position Width
                        pdo.D08_Pos_L_End = defectData.DefectPoLengthEndDirection;         // Defect Length Start
                        pdo.D08_Pos_L_Start = defectData.DefectPosLengthStartDirection;    // Defect Length End
                        pdo.D08_Level = DefectClass(defectData.DefectLevel);                                 // Defect Class
                        pdo.D08_Percent = defectData.DefectPercent.ToNullable<int>() ?? 0;        // Defect Percent 人工輸入?
                        //pdo.D08_QGrade = ?  // Defect Grade人工輸入?

                        break;

                    case 9:

                        pdo.D09_Code = defectData.DefectCode.ToString();        // Defect Code
                        pdo.D09_Origin = defectData.DefectOrigin;               // Defect From
                        pdo.D09_Sid = defectData.DefectSide;

                        pdo.D09_Pos_W = defectData.DefectPositionWidthDirection;                // Defect Position Width
                        pdo.D09_Pos_L_End = defectData.DefectPoLengthEndDirection;         // Defect Length Start
                        pdo.D09_Pos_L_Start = defectData.DefectPosLengthStartDirection;    // Defect Length End
                        pdo.D09_Level = DefectClass(defectData.DefectLevel);                               // Defect Class
                        pdo.D09_Percent = defectData.DefectPercent.ToNullable<int>() ?? 0;        // Defect Percent 人工輸入?
                        //pdo.D09_QGrade = ?  // Defect Grade人工輸入?

                        break;

                    case 10:

                        pdo.D10_Code = defectData.DefectCode.ToString();        // Defect Code
                        pdo.D10_Origin = defectData.DefectOrigin;               // Defect From
                        pdo.D10_Sid = defectData.DefectSide;

                        pdo.D10_Pos_W = defectData.DefectPositionWidthDirection;                // Defect Position Width
                        pdo.D10_Pos_L_End = defectData.DefectPoLengthEndDirection;         // Defect Length Start
                        pdo.D10_Pos_L_Start = defectData.DefectPosLengthStartDirection;    // Defect Length End
                        pdo.D10_Level = DefectClass(defectData.DefectLevel);                                 // Defect Class
                        pdo.D10_Percent = defectData.DefectPercent.ToNullable<int>() ?? 0;        // Defect Percent 人工輸入?
                        //pdo.D10_QGrade = ?  // Defect Grade人工輸入?

                        break;
                }

                cnt++;
            }
        }


       

        private static string IsUse(int value)
        {
            return value == PlcSysDef.Cmd.NotUse ? L2SystemDef.NotUseStr : L2SystemDef.UseStr;
        }

        private static string BaseSurfaceValue(int actualUncoilDirection, string AppointGridingSurface)
        {
            // 上開(0)，研磨面為外表面(O)->好面朝內(I)   
            if (actualUncoilDirection == 0 && AppointGridingSurface.Equals("O"))
                return "I";

            return "O";
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
    }
}

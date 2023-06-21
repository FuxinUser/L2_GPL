using DBService.Repository.GrindPlan;
using DBService.Repository.GrindRecords;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using GPLManager.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using static GPLManager.DataBaseTableFactory;


namespace GPLManager
{
    public partial class Frm_3_3_GrindActualPerformance : Form
    {
        #region 變數
        DataTable dtPdo, dtGetGrindPlan, dtPass;
        string  Section, HeadMaxPass, MidMaxPass, TailMaxPass,Coil_ID;

        private const string passpos = "passpos";

        #endregion
        //語系
        private LanguageHandler LanguageHand;
        public Frm_3_3_GrindActualPerformance()
        {
            InitializeComponent();
        }

        private void Frm_3_3_GrindActualPerformance_Load(object sender, EventArgs e)
        {
            if (PublicForms.GrindActualPerformance == null) PublicForms.GrindActualPerformance = this;
            Dtp_Start_Time.Value = Dtp_Start_Time.Value.AddDays(-7);

            Tab_ChartControl.TabPages.Remove(tabPage_GR1);
            Tab_ChartControl.TabPages.Remove(tabPage_GR2);
            Tab_ChartControl.TabPages.Remove(tabPage_GR3);
            Tab_ChartControl.TabPages.Remove(tabPage_GR4);
            Tab_ChartControl.TabPages.Remove(tabPage_GR5);
            Tab_ChartControl.TabPages.Remove(tabPage_GR6);

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            if (Rdb_Date.Checked)
            {
                if (Dtp_Start_Time.DateTimeRangeIsFail(Dtp_End_Time.Value))
                {
                    EventLogHandler.Instance.EventPush_Message($"日期区间起讫时间不正确，请重新确认");
                    return;
                }
            }

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine($" Select * ");
            sbSql.AppendLine($" From  [{nameof(PDOEntity.TBL_PDO)}] ");
            sbSql.AppendLine($" Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] IS NOT NULL ");
                      
            if (Rdb_Out_Coil_ID.Checked)
            {
                sbSql.AppendLine($" AND   [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{Cob_Out_Coil.Text}' ");  
            }
            else if (Rdb_Date.Checked)
            {
                sbSql.AppendLine($" AND [{nameof(PDOEntity.TBL_PDO.Start_Time)}] >= '{Dtp_Start_Time.Value:yyyy-MM-dd HH}:00:00' ");
                sbSql.AppendLine($" AND [{nameof(PDOEntity.TBL_PDO.Finish_Time)}] <= '{Dtp_End_Time.Value:yyyy-MM-dd HH}:59:59' ");               
            }
            string strSql = sbSql.ToString();
            dtPdo = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "DataGridView钢卷资料", "3-4");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_CoilGrindList, dtPdo);
            DGVColumnsHandler.Instance.Frm_3_3CoilList(Dgv_CoilGrindList);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_CoilGrindList);
        }
      
        private void Btn_Export_Click(object sender, EventArgs e)
        {
            //PDI:CG220068660000       GP240109   PDO:CM220068660000
            if (dtGetGrindPlan == null || dtGetGrindPlan.Rows.Count <= 0) return;
            string strCoilNo = dtGetGrindPlan.Rows[0]["Coil_ID"].ToString();
            DialogResult dialogResult = DialogHandler.Instance.Fun_DialogShowOkCancel($"是否汇出 钢卷号:{strCoilNo} 趋势图资料?", "汇出趋势图资料确认", Properties.Resources.dialogQuestion, 1);
            if (dialogResult != DialogResult.OK) return;

            //string dummyFileName = "请选择要保存Excel文件的文件夹路径";           
            //==================================================================
            #region Set SaveFileDialog
            string FileName = $"{Txt_Out_Coil_ID.Text.Trim()}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //設定檔案標題
            saveFileDialog.Title = "匯出Excel檔案";
            //設定檔案型別
            saveFileDialog.Filter = "Excel 工作簿(*.xlsx)|*.xlsx|Excel 97-2003 工作簿(*.xls)|*.xls";
            //設定預設檔案型別顯示順序  
            saveFileDialog.FilterIndex = 1;
            //是否自動在檔名中新增副檔名
            saveFileDialog.AddExtension = true;
            //是否記憶上次開啟的目錄
            saveFileDialog.RestoreDirectory = true;
            //設定預設檔名
            saveFileDialog.FileName = FileName;
            #endregion

            //按下確定選擇的按鈕  
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //獲得檔案路徑 
                string localFilePath = saveFileDialog.FileName.ToString();//Current
                string localFilePath_S = saveFileDialog.FileName.ToString().Replace(".", "_Speed.");//Speed
                //==================================================================
                Cursor.Current = Cursors.WaitCursor;

                // 取得存放文件的資料夾路徑
                string saveFolder = Path.GetDirectoryName(localFilePath);//Current//OutputExcelDialog.FileName
                string saveFolder_S = Path.GetDirectoryName(localFilePath);//Speed
                //先算出 头 中 尾  各有几个道次
                var _HeadMaxPass = dtGetGrindPlan.Compute($"Max({nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)})", $" {nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)} = '1' ");
                var _MidMaxPass = dtGetGrindPlan.Compute($"Max({nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)})", $" {nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)} = '2' ");
                var _TailMaxPass = dtGetGrindPlan.Compute($"Max({nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)})", $" {nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)} = '3' ");
                               
                ///完全自建栏位

                //各部位 道次 总计               
                int.TryParse(_HeadMaxPass.ToString(), out int int_HeadMaxPass);                
                int.TryParse(_MidMaxPass.ToString(), out int int_MidMaxPass);                
                int.TryParse(_TailMaxPass.ToString(), out int int_TailMaxPass);

                // 建立Excel文件
                IWorkbook tempBook = new XSSFWorkbook();//Current//建立活頁簿
                IWorkbook tempBook_S = new XSSFWorkbook();//Speed//建立活頁簿
                #region // 宣告Style
                //// 泛用字型
                //XSSFFont csfont = (XSSFFont)tempBook.CreateFont();
                //csfont.FontHeightInPoints = 10;
                //csfont.FontName = "Arial";

                //// 標準Style(靠左)
                //XSSFCellStyle cs_left = (XSSFCellStyle)tempBook.CreateCellStyle();
                //cs_left.SetFont(csfont);
                //cs_left.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                //cs_left.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                //cs_left.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_left.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_left.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_left.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_left.WrapText = false;

                //// 標準Style(靠右)
                //XSSFCellStyle cs_right = (XSSFCellStyle)tempBook.CreateCellStyle();
                //cs_right.SetFont(csfont);
                //cs_right.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                //cs_right.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                //cs_right.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_right.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_right.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_right.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_right.WrapText = false;

                //// 粗體字型
                //XSSFFont csfont_Bold = (XSSFFont)tempBook.CreateFont();
                //csfont_Bold.FontHeightInPoints = 10;
                //csfont_Bold.FontName = "Arial";
                //csfont_Bold.IsBold = true;

                //// 粗體字Style
                //XSSFCellStyle cs_bold = (XSSFCellStyle)tempBook.CreateCellStyle();
                //cs_bold.SetFont(csfont_Bold);
                //cs_bold.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                //cs_bold.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                //cs_bold.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_bold.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_bold.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_bold.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_bold.WrapText = false;

                //// 紅色字型
                //XSSFFont csfont_Red = (XSSFFont)tempBook.CreateFont();
                //csfont_Red.FontHeightInPoints = 10;
                //csfont_Red.FontName = "Arial";
                //csfont_Red.Color = IndexedColors.Red.Index;

                //// 紅色字Style
                //XSSFCellStyle cs_red = (XSSFCellStyle)tempBook.CreateCellStyle();
                //cs_red.SetFont(csfont_Red);
                //cs_red.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                //cs_red.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                //cs_red.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_red.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_red.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_red.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                //cs_red.WrapText = false;
                #endregion

                if (int_HeadMaxPass > 0)
                {
                    #region Old
                    //for (int i = 0; i < int_HeadMaxPass; i++)
                    //{
                    //    ISheet sheet = tempBook.CreateSheet($"Head_{i + 1}");
                    //    sheet.SetColumnWidth(0, 14 * 256);
                    //    sheet.SetColumnWidth(1, 14 * 256);
                    //    sheet.SetColumnWidth(2, 14 * 256);
                    //    sheet.SetColumnWidth(3, 14 * 256);

                    //    double avg = 0; // 實際出口平均值
                    //    double sd = 0; // 實際出口平均值
                    //    double min = 0; // 實際出口平均值
                    //    double max = 0; // 實際出口平均值

                    //    //List<double> valueList = new List<double>();
                    //    try
                    //    {
                    //        for (int j = 0; j < dtGetGrindPlan.Rows.Count; j++)
                    //        {
                    //            string strPasspos = dtGetGrindPlan.Rows[j]["passpos"].ToString();
                    //            string strValue = dtGetGrindPlan.Rows[j][nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_MOTOR_CURRENT)].ToString();

                    //            //valueList.Add(Convert.ToDouble(value.Split(':')[1]));
                    //            // 第七行以後
                    //            IRow row_x = sheet.CreateRow(6 + j);
                    //            ICell cell_x_1 = row_x.CreateCell(0);
                    //            cell_x_1.SetCellValue(strPasspos);
                    //            cell_x_1.CellStyle.SetFont(csfont);

                    //            ICell cell_x_2 = row_x.CreateCell(1);
                    //            cell_x_2.SetCellValue(strValue);
                    //            cell_x_2.CellStyle.SetFont(csfont);
                    //        }
                    //    }
                    //    catch { }

                    //    // 第一行
                    //    IRow row_1 = sheet.CreateRow(0);
                    //    ICell cell_1 = row_1.CreateCell(0);
                    //    cell_1.SetCellValue($"钢卷编号：{Txt_Out_Coil_ID.Text.Trim()}-TEST");//{processDataDict[dataCode]}
                    //    cell_1.CellStyle = cs_left;
                    //    row_1.CreateCell(1).CellStyle = cs_left;
                    //    row_1.CreateCell(2).CellStyle = cs_left;
                    //    row_1.CreateCell(3).CellStyle = cs_left;
                    //    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 3));

                    //    // 第二行
                    //    IRow row_2 = sheet.CreateRow(1);
                    //    ICell cell_2 = row_2.CreateCell(0);
                    //    cell_2.SetCellValue($"列印时间：{DateTime.Now.ToString("yyyyMMdd-HHmmss")}");
                    //    cell_2.CellStyle = cs_left;
                    //    row_2.CreateCell(1).CellStyle = cs_left;
                    //    row_2.CreateCell(2).CellStyle = cs_left;
                    //    row_2.CreateCell(3).CellStyle = cs_left;
                    //    sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, 3));

                    //    // 第三行
                    //    IRow row_3 = sheet.CreateRow(2);
                    //    ICell cell_3_1 = row_3.CreateCell(0);
                    //    cell_3_1.SetCellValue("实际出口平均值");
                    //    cell_3_1.CellStyle = cs_bold;

                    //    ICell cell_3_2 = row_3.CreateCell(1);
                    //    cell_3_2.SetCellValue(avg.ToString());
                    //    cell_3_2.CellStyle = cs_right;

                    //    ICell cell_3_3 = row_3.CreateCell(2);
                    //    cell_3_3.SetCellValue("实际出口标准差");
                    //    cell_3_3.CellStyle = cs_bold;

                    //    ICell cell_3_4 = row_3.CreateCell(3);
                    //    cell_3_4.SetCellValue(sd.ToString());
                    //    cell_3_4.CellStyle = cs_right;

                    //    // 第四行
                    //    IRow row_4 = sheet.CreateRow(3);
                    //    ICell cell_4_1 = row_4.CreateCell(0);
                    //    cell_4_1.SetCellValue("实际出口最小值");
                    //    cell_4_1.CellStyle = cs_bold;

                    //    ICell cell_4_2 = row_4.CreateCell(1);
                    //    cell_4_2.SetCellValue(min.ToString());
                    //    cell_4_2.CellStyle = cs_right;

                    //    ICell cell_4_3 = row_4.CreateCell(2);
                    //    cell_4_3.SetCellValue("实际出口最大值");
                    //    cell_4_3.CellStyle = cs_bold;

                    //    ICell cell_4_4 = row_4.CreateCell(3);
                    //    cell_4_4.SetCellValue(max.ToString());
                    //    cell_4_4.CellStyle = cs_right;

                    //    // 第五行
                    //    IRow row_5 = sheet.CreateRow(4);
                    //    ICell cell_5 = row_5.CreateCell(0);
                    //    cell_5.SetCellValue("以上统计数据为去头(M)及去尾(M)后计算得出");
                    //    cell_5.CellStyle = cs_red;
                    //    row_5.CreateCell(1).CellStyle = cs_red;
                    //    row_5.CreateCell(2).CellStyle = cs_red;
                    //    row_5.CreateCell(3).CellStyle = cs_red;
                    //    sheet.AddMergedRegion(new CellRangeAddress(4, 4, 0, 3));

                    //    // 第六行
                    //    IRow row_6 = sheet.CreateRow(5);
                    //    ICell cell_6_1 = row_6.CreateCell(0);
                    //    cell_6_1.SetCellValue("米数");
                    //    cell_6_1.CellStyle = cs_bold;

                    //    ICell cell_6_2 = row_6.CreateCell(1);
                    //    cell_6_2.SetCellValue("连续性数值");
                    //    cell_6_2.CellStyle = cs_bold;
                    //}
                    #endregion                    
                    for (int i = 0; i < int_HeadMaxPass; i++)
                    {
                        Fun_SetSheet(1, i + 1, tempBook, "GR1_Head_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_MOTOR_CURRENT));              
                        Fun_SetSheet(1, i + 1, tempBook, "GR2_Head_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_MOTOR_CURRENT));  
                        Fun_SetSheet(1, i + 1, tempBook, "GR3_Head_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_MOTOR_CURRENT));
                        Fun_SetSheet(1, i + 1, tempBook, "GR4_Head_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_MOTOR_CURRENT));
                        Fun_SetSheet(1, i + 1, tempBook, "GR5_Head_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_MOTOR_CURRENT));
                        Fun_SetSheet(1, i + 1, tempBook, "GR6_Head_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_MOTOR_CURRENT));
                        Fun_SetSheet(1, i + 1, tempBook_S, "GR1_Head_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_SPEED));
                        Fun_SetSheet(1, i + 1, tempBook_S, "GR2_Head_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_SPEED));
                    }

                }
                if (int_MidMaxPass > 0) 
                {                    
                    for (int i = 0; i < int_MidMaxPass; i++)
                    {
                        Fun_SetSheet(2, i + 1, tempBook, "GR1_Mid_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_MOTOR_CURRENT)); 
                        Fun_SetSheet(2, i + 1, tempBook, "GR2_Mid_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_MOTOR_CURRENT));
                        Fun_SetSheet(2, i + 1, tempBook, "GR3_Mid_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_MOTOR_CURRENT));
                        Fun_SetSheet(2, i + 1, tempBook, "GR4_Mid_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_MOTOR_CURRENT));
                        Fun_SetSheet(2, i + 1, tempBook, "GR5_Mid_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_MOTOR_CURRENT));
                        Fun_SetSheet(2, i + 1, tempBook, "GR6_Mid_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_MOTOR_CURRENT));
                        Fun_SetSheet(2, i + 1, tempBook_S, "GR1_Mid_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_SPEED));
                        Fun_SetSheet(2, i + 1, tempBook_S, "GR2_Mid_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_SPEED));
                    }
                }
                if (int_TailMaxPass > 0) 
                {                   
                    for (int i = 0; i < int_MidMaxPass; i++)
                    {
                        Fun_SetSheet(3, i + 1, tempBook, "GR1_Tail_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_MOTOR_CURRENT)); 
                        Fun_SetSheet(3, i + 1, tempBook, "GR2_Tail_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_MOTOR_CURRENT));
                        Fun_SetSheet(3, i + 1, tempBook, "GR3_Tail_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_MOTOR_CURRENT));
                        Fun_SetSheet(3, i + 1, tempBook, "GR4_Tail_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_MOTOR_CURRENT));
                        Fun_SetSheet(3, i + 1, tempBook, "GR5_Tail_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_MOTOR_CURRENT));
                        Fun_SetSheet(3, i + 1, tempBook, "GR6_Tail_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_MOTOR_CURRENT));
                        Fun_SetSheet(3, i + 1, tempBook_S, "GR1_Tail_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_SPEED));
                        Fun_SetSheet(3, i + 1, tempBook_S, "GR2_Tail_" + (i + 1), nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_SPEED));    
                    }
                }

                string savePath = Path.Combine(saveFolder, FileName);//Current
                string savePath_S = Path.Combine(saveFolder_S, FileName.Replace(".", "_Speed."));//Speed

                if (File.Exists(savePath))
                    File.Delete(savePath);
                if (File.Exists(savePath_S))
                    File.Delete(savePath_S);
                using (FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                {
                    tempBook.Write(fileStream);
                }
                using (FileStream fileStream_S = new FileStream(savePath_S, FileMode.Create, FileAccess.Write))
                {
                    tempBook_S.Write(fileStream_S);
                }

                DialogHandler.Instance.Fun_DialogShowOk("汇出Excel成功!", "汇出Excel", null, 4);

            }

        }

        private void Fun_SetSheet(int intPart, int intPass, IWorkbook tempBook,string strSenssion,string strColumnName )
        {
            #region // 宣告Style
            // 泛用字型
            XSSFFont csfont = (XSSFFont)tempBook.CreateFont();
            csfont.FontHeightInPoints = 10;
            csfont.FontName = "Arial";

            // 標準Style(靠左)
            XSSFCellStyle cs_left = (XSSFCellStyle)tempBook.CreateCellStyle();
            cs_left.SetFont(csfont);
            cs_left.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
            cs_left.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cs_left.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_left.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_left.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_left.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_left.WrapText = false;

            // 標準Style(靠右)
            XSSFCellStyle cs_right = (XSSFCellStyle)tempBook.CreateCellStyle();
            cs_right.SetFont(csfont);
            cs_right.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
            cs_right.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cs_right.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_right.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_right.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_right.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_right.WrapText = false;

            // 粗體字型
            XSSFFont csfont_Bold = (XSSFFont)tempBook.CreateFont();
            csfont_Bold.FontHeightInPoints = 10;
            csfont_Bold.FontName = "Arial";
            csfont_Bold.IsBold = true;

            // 粗體字Style
            XSSFCellStyle cs_bold = (XSSFCellStyle)tempBook.CreateCellStyle();
            cs_bold.SetFont(csfont_Bold);
            cs_bold.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cs_bold.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cs_bold.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_bold.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_bold.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_bold.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_bold.WrapText = false;

            // 紅色字型
            XSSFFont csfont_Red = (XSSFFont)tempBook.CreateFont();
            csfont_Red.FontHeightInPoints = 10;
            csfont_Red.FontName = "Arial";
            csfont_Red.Color = IndexedColors.Red.Index;

            // 紅色字Style
            XSSFCellStyle cs_red = (XSSFCellStyle)tempBook.CreateCellStyle();
            cs_red.SetFont(csfont_Red);
            cs_red.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cs_red.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cs_red.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_red.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_red.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_red.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cs_red.WrapText = false;
            #endregion

            ISheet sheet = tempBook.CreateSheet($"{strSenssion}"); //_{ i + 1}
            sheet.SetColumnWidth(0, 14 * 256);
            sheet.SetColumnWidth(1, 14 * 256);
            sheet.SetColumnWidth(2, 14 * 256);
            sheet.SetColumnWidth(3, 14 * 256);

            double avg = 0; // 實際出口平均值
            double sd = 0; // 實際出口平均值
            double min = 0; // 實際出口平均值
            double max = 0; // 實際出口平均值

            double[] valueString;
            List<double> valueList = new List<double>();

            DataRow[] dataRows = dtGetGrindPlan.Select($" Current_Senssion = '{intPart}' AND Current_Pass = '{intPass}' ");
            if (dataRows.Length <= 0) return;
            DataTable _dt = dataRows.CopyToDataTable();
            DataView dv = _dt.DefaultView;
            dv.Sort = nameof(GrindRecordsEntity.TBL_GrindRecords.Receive_Time);// "Receive_Time desc";
            DataTable dtSort = dv.ToTable();

            try
            {
                for (int j = 0; j < dtSort.Rows.Count; j++)
                {
                    string strPasspos = dtSort.Rows[j]["passpos"].ToString();
                    string strValue = dtSort.Rows[j][strColumnName].ToString();
                    double.TryParse(strValue, out double douValue);
                    valueList.Add(douValue);
                    //valueList.Add(Convert.ToDouble(value.Split(':')[1]));
                    // 第七行以後
                    IRow row_x = sheet.CreateRow(6 + j);
                    ICell cell_x_1 = row_x.CreateCell(0);
                    cell_x_1.SetCellValue(strPasspos);
                    cell_x_1.CellStyle.SetFont(csfont);

                    ICell cell_x_2 = row_x.CreateCell(1);
                    cell_x_2.SetCellValue(strValue);
                    cell_x_2.CellStyle.SetFont(csfont);
                }

                valueString = valueList.ToArray();
                if (valueString.Length > 0)
                {
                    avg = valueString.Average();
                    double sumOfSquaresOfDifferences = valueString.Select(val => (val - avg) * (val - avg)).Sum();
                    sd = Math.Sqrt(sumOfSquaresOfDifferences / valueString.Length);
                    min = valueString.Min();
                    max = valueString.Max();
                }
            }
            catch { }
            string strShow = Fun_GetTitleShow(strColumnName);
            // 第一行
            IRow row_1 = sheet.CreateRow(0);
            ICell cell_1 = row_1.CreateCell(0);
            cell_1.SetCellValue($"钢卷编号：{Txt_Out_Coil_ID.Text.Trim()}-{strShow}");
            cell_1.CellStyle = cs_left;
            row_1.CreateCell(1).CellStyle = cs_left;
            row_1.CreateCell(2).CellStyle = cs_left;
            row_1.CreateCell(3).CellStyle = cs_left;
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 3));

            // 第二行
            IRow row_2 = sheet.CreateRow(1);
            ICell cell_2 = row_2.CreateCell(0);
            cell_2.SetCellValue($"列印时间：{DateTime.Now.ToString("yyyyMMdd-HHmmss")}");
            cell_2.CellStyle = cs_left;
            row_2.CreateCell(1).CellStyle = cs_left;
            row_2.CreateCell(2).CellStyle = cs_left;
            row_2.CreateCell(3).CellStyle = cs_left;
            sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, 3));

            // 第三行
            IRow row_3 = sheet.CreateRow(2);
            ICell cell_3_1 = row_3.CreateCell(0);
            cell_3_1.SetCellValue("实际出口平均值");
            cell_3_1.CellStyle = cs_bold;

            ICell cell_3_2 = row_3.CreateCell(1);
            cell_3_2.SetCellValue(avg.ToString());
            cell_3_2.CellStyle = cs_right;

            ICell cell_3_3 = row_3.CreateCell(2);
            cell_3_3.SetCellValue("实际出口标准差");
            cell_3_3.CellStyle = cs_bold;

            ICell cell_3_4 = row_3.CreateCell(3);
            cell_3_4.SetCellValue(sd.ToString());
            cell_3_4.CellStyle = cs_right;

            // 第四行
            IRow row_4 = sheet.CreateRow(3);
            ICell cell_4_1 = row_4.CreateCell(0);
            cell_4_1.SetCellValue("实际出口最小值");
            cell_4_1.CellStyle = cs_bold;

            ICell cell_4_2 = row_4.CreateCell(1);
            cell_4_2.SetCellValue(min.ToString());
            cell_4_2.CellStyle = cs_right;

            ICell cell_4_3 = row_4.CreateCell(2);
            cell_4_3.SetCellValue("实际出口最大值");
            cell_4_3.CellStyle = cs_bold;

            ICell cell_4_4 = row_4.CreateCell(3);
            cell_4_4.SetCellValue(max.ToString());
            cell_4_4.CellStyle = cs_right;

            // 第五行
            IRow row_5 = sheet.CreateRow(4);
            ICell cell_5 = row_5.CreateCell(0);
            cell_5.SetCellValue("以上统计数据为去头(M)及去尾(M)后计算得出");
            cell_5.CellStyle = cs_red;
            row_5.CreateCell(1).CellStyle = cs_red;
            row_5.CreateCell(2).CellStyle = cs_red;
            row_5.CreateCell(3).CellStyle = cs_red;
            sheet.AddMergedRegion(new CellRangeAddress(4, 4, 0, 3));

            // 第六行
            IRow row_6 = sheet.CreateRow(5);
            ICell cell_6_1 = row_6.CreateCell(0);
            cell_6_1.SetCellValue("米数");
            cell_6_1.CellStyle = cs_bold;

            ICell cell_6_2 = row_6.CreateCell(1);
            cell_6_2.SetCellValue("连续性数值");
            cell_6_2.CellStyle = cs_bold;

        }

        private string Fun_GetTitleShow(string strColumnName)
        {
            string strShow = "";
            string strCurrent = "研磨机马达电流-107(A)";
            string strSpeed = "研磨机马达转速-107(mpm)";
            switch (strColumnName)
            {
                case nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_MOTOR_CURRENT): strShow = $"No.1{strCurrent}"; break;
                case nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_MOTOR_CURRENT): strShow = $"No.2{strCurrent}"; break;
                case nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_MOTOR_CURRENT): strShow = $"No.3{strCurrent}"; break;
                case nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_MOTOR_CURRENT): strShow = $"No.4{strCurrent}"; break;
                case nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_MOTOR_CURRENT): strShow = $"No.5{strCurrent}"; break;
                case nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_MOTOR_CURRENT): strShow = $"No.6{strCurrent}"; break;
                case nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_SPEED):strShow = $"No.1{strSpeed}"; break;                    
                case nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_SPEED):strShow = $"No.2{strSpeed}"; break;  
                default:
                    break;
            }
            return strShow;
        }

        private void Fun_SampleFile_Export()
        {
            #region 範本版

            string strSampleFile = "../../SampleFile/Export_Chart.xlsx";//D:\K\GitHub_GPL_HMI\gpldev\GPL\GPL\SampleFile\
            string strFilePath = "Excel/Export_Chart.xlsx";
            string strLogShow = $"趋势图纪录";
            try
            {
                //檢查範本路徑是否已有资料夹
                FileInfo fi = new FileInfo(strFilePath);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }

                //檢查範本路徑是否已有檔案存在,沒有就复制
                if (!File.Exists(strFilePath))
                {
                    File.Copy(Path.Combine(strSampleFile), Path.Combine(strFilePath), true);
                }
            }
            catch (FileNotFoundException ffex)
            {
                DialogHandler.Instance.Fun_DialogShowOk(ffex.Message, $"讯息提示", null, 3);
                PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出{strLogShow}失败{ffex.Message}");
                PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出{strLogShow}失败{ffex.Message}");
                return;
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk(ex.Message, $"讯息提示", null, 3);
                PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出{strLogShow}失败{ex.Message}");
                PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出{strLogShow}失败{ex.Message}");
                return;
            }

            //以範本档案建立新文件
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel Sheet|*.xlsx";
            dialog.FileName = "GPL_" + Txt_Out_Coil_ID.Text.Trim() + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".xlsx";
            dialog.Title = "汇出Excel文件";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(dialog.FileName))
                        File.Delete(dialog.FileName);

                    using (FileStream fileStream = File.Open(dialog.FileName, FileMode.Create, FileAccess.Write))
                    {
                        //讀取內建範本
                        IWorkbook workbook;

                        try
                        {
                            using (var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.ReadWrite))
                            {
                                //strFilePath = "Excel/Export_EXCEL.xlsx"
                                //if (Path.GetExtension(dialog.FileName).ToLower() == ".xls")
                                //{
                                //    workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(fs);
                                //}
                                //else
                                //{
                                workbook = new XSSFWorkbook(fs);
                                //}
                            }

                            ISheet sheet = workbook.GetSheet("Sheet1");
                            //sheet.SheetName = "";



                            #region // 宣告Style
                            // 第一列Head使用的Style
                            XSSFFont cs_head_font = (XSSFFont)workbook.CreateFont();
                            cs_head_font.FontHeightInPoints = 14;
                            cs_head_font.FontName = "宋体";

                            XSSFCellStyle cs_head = (XSSFCellStyle)workbook.CreateCellStyle();
                            cs_head.SetFont(cs_head_font);
                            cs_head.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                            cs_head.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                            cs_head.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs_head.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs_head.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs_head.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
                            cs_head.WrapText = false;
                            cs_head.FillForegroundColor = NPOI.SS.UserModel.IndexedColors.PaleBlue.Index;
                            cs_head.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;

                            XSSFFont csfont = (XSSFFont)workbook.CreateFont();
                            csfont.FontHeightInPoints = 12;
                            csfont.FontName = "宋体";

                            XSSFCellStyle cs = (XSSFCellStyle)workbook.CreateCellStyle();
                            cs.SetFont(csfont);
                            cs.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                            cs.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                            cs.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs.WrapText = false;

                            XSSFCellStyle cs_Date = (XSSFCellStyle)workbook.CreateCellStyle();
                            cs_Date.SetFont(csfont);
                            cs_Date.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;//文字水平对齐
                            cs_Date.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐
                            cs_Date.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs_Date.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs_Date.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs_Date.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs_Date.WrapText = false;
                            var cc = NPOI.SS.UserModel.CellType.String;
                            //设置数据显示格式
                            IDataFormat dataFormat = workbook.CreateDataFormat();
                            cs_Date.DataFormat = dataFormat.GetFormat("yyyy/MM/dd HH:mm:ss.fff");//

                            // 未上传 底色 LightYellow
                            XSSFCellStyle cs_mms = (XSSFCellStyle)workbook.CreateCellStyle();
                            cs_mms.SetFont(csfont);
                            cs_mms.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                            cs_mms.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                            cs_mms.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs_mms.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs_mms.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs_mms.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                            cs_mms.WrapText = false;
                            cs_mms.FillForegroundColor = NPOI.SS.UserModel.IndexedColors.LightYellow.Index;
                            cs_mms.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
                            #endregion

                            //將DataSet匯出為Excel
                            if (Dgv_CoilGrindList != null && Dgv_CoilGrindList.SelectedRows.Count > 0)
                            {
                                int rowCount = dtGetGrindPlan.Rows.Count;// downTimeTable.Count;//行數
                                var properties = dtGetGrindPlan.Columns.Count;// downTimeTable.ElementAt(0).GetType().GetProperties();
                                int columnCount = properties;//.Count() - 2;//列數  扣掉error 與 item

                                //設定列頭
                                IRow row = sheet.CreateRow(0);//excel第一行設為列頭
                                try
                                {
                                    //for (int c = 0; c < columnCount - 1; c++)
                                    //{
                                    //    ////取得 物建display text屬性
                                    //    //MemberInfo property = typeof(TBL_DownTime.TBL_DownTimeMD).GetProperty(properties.ElementAt(c).Name);
                                    //    //string displayColumnName = property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName.Trim();

                                    //    string displayColumnName = Dgv_Info.Columns[c].HeaderText;

                                    //    NPOI.SS.UserModel.ICell cell = row.CreateCell(c);
                                    //    cell.SetCellValue(displayColumnName);
                                    //    cell.CellStyle = cs_head;

                                    //    int length = cell.ToString().Length;

                                    //    if (c == 2 || c == 5 || c == 6)
                                    //    {
                                    //        sheet.SetColumnWidth(c, (length + 2) * 1000);
                                    //    }
                                    //    else
                                    //    {
                                    //        sheet.SetColumnWidth(c, length * 1000);
                                    //    }
                                    //}
                                }
                                catch (Exception ex)
                                {
                                    DialogHandler.Instance.Fun_DialogShowOk(ex.Message, $"讯息提示", null, 3);
                                    PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ex.Message}");
                                    PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ex.Message}");
                                    return;
                                }

                                //設定每行每列的單元格,
                                for (int i = 0; i < rowCount; i++)
                                {
                                    //var isUpload = Dgv_Info.Rows[i].Cells[nameof(GrindRecordsEntity.TBL_GrindRecords.UploadMMS)].Value.ToString();// downTimeTable.ElementAt(i).IsUpload;
                                    //bool isSendMMS = isUpload == "未上传" ? true : false;

                                    row = sheet.CreateRow(i + 1);
                                    for (int j = 0; j < columnCount - 1; j++)
                                    {
                                        NPOI.SS.UserModel.ICell cell = row.CreateCell(j);//excel第二行開始寫入資料      

                                        var value = dtGetGrindPlan.Rows[i][j].ToString();
                                        //var value = downTimeTable.ElementAt(i).GetType().GetProperties()[j].GetValue(downTimeTable.ElementAt(i));

                                        if (value == null)
                                        {
                                            cell.SetCellValue("");
                                        }
                                        else
                                        {
                                            //if (j == 2)
                                            //{
                                            //    value = DateTime.TryParse(Dgv_Info.Rows[i].Cells[j].Value.ToString(), out DateTime dateTime) ? dateTime.ToString("yyyy/MM/dd") : Dgv_Info.Rows[i].Cells[j].Value.ToString();
                                            //}
                                            //if (j == 5 || j == 6)
                                            //{
                                            //    value = DateTime.TryParse(Dgv_Info.Rows[i].Cells[j].Value.ToString(), out DateTime dateTime) ? dateTime.ToString("yyyy/MM/dd HH:mm:ss.fff") : Dgv_Info.Rows[i].Cells[j].Value.ToString();
                                            //}
                                            ////時間轉換格式
                                            //cell.SetCellValue(value.ToString().Trim());
                                        }

                                        //if (isSendMMS)
                                        //{
                                        //    cell.CellStyle = cs_mms;
                                        //}
                                        //else
                                        //{
                                        //    cell.CellStyle = cs;
                                        //}
                                    }

                                }
                            }

                            workbook.Write(fileStream);
                        }
                        catch (Exception ex)
                        {
                            DialogHandler.Instance.Fun_DialogShowOk(ex.Message, $"讯息提示", null, 3);
                            PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出{strLogShow}失败{ex.Message}");
                            PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出{strLogShow}失败{ex.Message}");
                            return;
                        }

                    }

                    //log      
                    string strShowText = $"汇出 [{Txt_Out_Coil_ID.Text.Trim()}]{strLogShow} ";

                    DialogHandler.Instance.Fun_DialogShowOk(strShowText + " 成功!", $"讯息提示", null, 4);
                    EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text} {strShowText}");

                    EventLogHandler.Instance.LogInfo("4-1", $"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出{strLogShow} 成功", strShowText);
                    PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} {strShowText}");
                    PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} {strShowText}");

                }
                catch (Exception ex)
                {
                    DialogHandler.Instance.Fun_DialogShowOk(ex.Message, $"讯息提示", null, 3);
                    File.Delete(dialog.FileName);
                    PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出{strLogShow}失败{ex.Message}");
                    PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出{strLogShow}失败{ex.Message}");
                    throw;
                }
            }

            GC.Collect();

            #endregion
        }
        private DataTable Fun_GetPasspos(DataTable dtData)
        {
            dtData.Columns.Add("passpos");
            dtData.Columns["passpos"].DataType = typeof(string);
            
            DataTable dtGetpos = dtData.Clone();
            double coilLength = 0.0;

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (Convert.ToDouble(dtData.Rows[i]["Line_Speed"].ToString()) == 0) { continue; }

                var preCoilLength = coilLength;
                coilLength += Convert.ToDouble(dtData.Rows[i]["Line_Speed"].ToString()) / 60;

                var prePos = Convert.ToInt32(preCoilLength);
                var pos = Convert.ToInt32(coilLength);

                if (prePos == pos)
                    continue;
                dtData.Rows[i]["passpos"] = prePos.ToString();
                // coilLength += Convert.ToDouble(dtGetpos.Rows[i]["Line_Speed"].ToString()) / 60;
                dtGetpos.ImportRow(dtData.Rows[i]);
                //dtGetpos.Rows[i]["passpos"] = coilLength.ToString();
            }

            return dtGetpos;
        }

        private void Dgv_CoilGrindList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) { return; }//列标题不响应CellClick事件
            if (Dgv_CoilGrindList.CurrentRow == null) { return; }

            Cob_Part.Items.Clear();
            Cob_Part.Text = string.Empty;
                       
            DataRow drGetCoilNo = Fun_GetDataRowFromCurrentRow(Dgv_CoilGrindList, dtPdo);// dtPdo.Rows[dgv_CoilGrindList.CurrentRow.Index];
          
            string strInCoilID = drGetCoilNo[nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString();//nameof(PDOEntity.TBL_PDO.In_Coil_ID) //nameof(PDOEntity.TBL_PDO.Out_Coil_ID)
            string strOutCoilID = drGetCoilNo[nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString();
            string strPlanNo = drGetCoilNo[nameof(PDOEntity.TBL_PDO.Plan_No)].ToString();
            //strCoilID = "CM220080410000";
            //strPlanNo = "GP240112";
            DataTable dtGetGrindRecords = Fun_GetGrindDatas(strInCoilID, strPlanNo);
            if (dtGetGrindRecords == null) { return; }
            dtGetGrindPlan = Fun_GetPasspos( dtGetGrindRecords);
            #region 移到Fun_GetPasspos
            //double coilLength = 0.0;
            //for(int i = 0; i < dtGetGrindRecords.Rows.Count; i++)
            //{

            //    if (Convert.ToDouble(dtGetGrindRecords.Rows[i]["Line_Speed"].ToString()) == 0) { continue; }

            //    var preCoilLength = coilLength;
            //    coilLength += Convert.ToDouble(dtGetGrindRecords.Rows[i]["Line_Speed"].ToString()) / 60;

            //    var prePos = Convert.ToInt32(preCoilLength);
            //    var pos = Convert.ToInt32(coilLength);

            //    if (prePos == pos)
            //        continue;
            //    dtGetGrindRecords.Rows[i]["passpos"] = prePos.ToString();
            //    // coilLength += Convert.ToDouble(dtGetGrindPlan.Rows[i]["Line_Speed"].ToString()) / 60;
            //    dtGetGrindPlan.ImportRow(dtGetGrindRecords.Rows[i]);
            //    //dtGetGrindPlan.Rows[i]["passpos"] = coilLength.ToString();
            //}
            #endregion 移到Fun_GetPasspos

            //string strSql = SqlFactory.Frm_3_3_SelectPassSection(drGetCoilNo[nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString());
            ////dtGetGrindPlan = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "研磨机皮带","3-4");

            if (dtGetGrindPlan.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"钢卷号[{drGetCoilNo[nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString().Trim()}]无研磨资料");
                Btn_Export.Enabled = false;
                return;
            }
            else
            {
                Btn_Export.Enabled = true;
            }
            
            #region - 填資料 -

            //鋼卷編號
            Coil_ID = drGetCoilNo[nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString();//給 事件訊息 & 記Log 使用
            Txt_In_Coil_ID.Text = strInCoilID;
            Txt_Out_Coil_ID.Text = Coil_ID.Trim();

            //var _HeadMaxPass = dtGetGrindPlan.Compute("Max(Current_Pass)", $" {nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)} = '{1}' ");
            //var _MidMaxPass = dtGetGrindPlan.Compute("Max(Current_Pass)", $" {nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)} = '{2}' ");
            //var _TailMaxPass = dtGetGrindPlan.Compute("Max(Current_Pass)", $" {nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)} = '{3}' ");

            var _HeadMaxPass = dtGetGrindRecords.Compute($"Max({nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)})", $" {nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)} = '1' ");
            var _MidMaxPass = dtGetGrindRecords.Compute($"Max({nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)})", $" {nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)} = '2' ");
            var _TailMaxPass = dtGetGrindRecords.Compute($"Max({nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)})", $" {nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)} = '3' ");
            //HeadMaxPass = _HeadMaxPass.ToString();
            //MidMaxPass =  _MidMaxPass.ToString();
            //TailMaxPass = _TailMaxPass.ToString();
                      
            Txt_HeadPass.Text = _HeadMaxPass.ToString();// HeadMaxPass;
            Txt_MidPass.Text = _MidMaxPass.ToString(); //MidMaxPass;
            Txt_TailPass.Text = _TailMaxPass.ToString(); //TailMaxPass;
            Fun_GetCbo_Part_Data(dtGetGrindRecords);

            #region old_getMaxPass

            ////各部位實際研磨道次
            ////頭段
            //Fun_SelectPassNumber(drGetCoilNo[nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString(), "1");//Out_Coil_ID
            // if (dtPass != null && dtPass.Rows.Count > 0)
            // {
            //     HeadMaxPass = !dtPass.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)].ToString().IsEmpty() ?
            //   dtPass.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)].ToString() : "0";
            //     //中段
            //     Fun_SelectPassNumber(drGetCoilNo[nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString(), "2");//Out_Coil_ID
            //     MidMaxPass = !dtPass.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)].ToString().IsEmpty() ?
            //         dtPass.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)].ToString() : "0";

            //     //尾段
            //     Fun_SelectPassNumber(drGetCoilNo[nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString(), "3");//Out_Coil_ID
            //     TailMaxPass = !dtPass.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)].ToString().IsEmpty() ?
            //         dtPass.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)].ToString() : "0";
            // }

            ////各部位總道次 (In_Coil_ID)
            //for (int i = 0; i < dtGetGrindPlan.Rows.Count; i++)
            //{
            //    if (dtGetGrindPlan.Rows[i][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)].ToString().Equals("1"))
            //    {
            //        Txt_HeadPass.Text = (dtGetGrindPlan.Rows[i][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)].ToString() != null) ?
            //            $"{HeadMaxPass}/{dtGetGrindPlan.Rows[i][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)]}" : string.Empty;
            //    }
            //    else if (dtGetGrindPlan.Rows[i][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)].ToString() == "2")
            //    {
            //        Txt_MidPass.Text = (dtGetGrindPlan.Rows[i][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)].ToString() != null) ?
            //            $"{MidMaxPass}/{dtGetGrindPlan.Rows[i][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)]}" : string.Empty;
            //    }
            //    else if (dtGetGrindPlan.Rows[i][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)].ToString() == "3")
            //    {
            //        Txt_TailPass.Text = (dtGetGrindPlan.Rows[i][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)].ToString() != null) ?
            //            $"{TailMaxPass}/{dtGetGrindPlan.Rows[i][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)]}" : string.Empty;
            //    }
            //    //cbo_Part.Items.Add(dtGetGrindPlan.Rows[i][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)].ToString());
            //}

            //////各部位總道次_Old (//Out_Coil_ID)
            ////for (int i = 0; i < dtGetGrindPlan.Rows.Count; i++)
            ////{
            ////    if (dtGetGrindPlan.Rows[i][nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)].ToString().Equals("H"))
            ////    {
            ////        txtHeadPass.Text = (dtGetGrindPlan.Rows[i][nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)].ToString() != null) ? 
            ////            $"{HeadPass}/{dtGetGrindPlan.Rows[i][nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)]}": string.Empty;
            ////    }
            ////    else if (dtGetGrindPlan.Rows[i][nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)].ToString() == "M")
            ////    {
            ////        txtMidPass.Text = (dtGetGrindPlan.Rows[i][nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)].ToString() != null) ? 
            ////            $"{MidPass}/{dtGetGrindPlan.Rows[i][nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)]}" : string.Empty;
            ////    }
            ////    else if (dtGetGrindPlan.Rows[i][nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)].ToString() == "T")
            ////    {
            ////        txtTailPass.Text = (dtGetGrindPlan.Rows[i][nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)].ToString() != null) ? 
            ////            $"{TailPass}/{dtGetGrindPlan.Rows[i][nameof(GrindPlanEntity.TBL_GrindPlan.PassNumber)]}" : string.Empty;
            ////    }
            ////    cbo_Part.Items.Add(dtGetGrindPlan.Rows[i][nameof(GrindPlanEntity.TBL_GrindPlan.Pass_Section)].ToString());
            ////}
            #endregion old_getMaxPass

            #endregion

        }

        /// <summary>
        /// 以入口捲號取得 TBL_GrindRecords 資料
        /// </summary>
        /// <param name="strCoilID">入口鋼捲號</param>
        /// <param name="strPlanNo">計畫號</param>
        /// <returns></returns>
        private DataTable Fun_GetGrindDatas(string strCoilID, string strPlanNo)
        {
            DataTable dtGrind = new DataTable();
            var strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT ");
            strBuilder.Append(" * ");
            strBuilder.Append(" FROM ");
            strBuilder.Append($" {nameof(GrindRecordsEntity.TBL_GrindRecords)} ");
            strBuilder.Append(" Where ");
            strBuilder.Append($" {nameof(GrindRecordsEntity.TBL_GrindRecords.Coil_ID)} = '" + strCoilID + "'");
            strBuilder.Append(" AND ");
            strBuilder.Append($" {nameof(GrindRecordsEntity.TBL_GrindRecords.Plan_No)}  = '" + strPlanNo + "'");
            strBuilder.Append(" Order by ");
            strBuilder.Append($" {nameof(GrindRecordsEntity.TBL_GrindRecords.Receive_Time)} ");
            strBuilder.Append(" ASC ");
            string strSql = strBuilder.ToString();
            dtGrind = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "撈取研磨數據GrindData", "3-4");
            return dtGrind.Rows.Count > 0 ? dtGrind : null;
        }
               
        /// <summary>
        /// 將選中的 DataGridViewRow 轉成 DataRow.
        /// </summary>
        /// <param name="dgv">來源DataGridView</param>
        /// <param name="dt">來源DataTable</param>
        /// <returns></returns>
        public DataRow Fun_GetDataRowFromCurrentRow(DataGridView dgv, DataTable dt)
        {
            if (dgv.CurrentRow == null) { return null; }
            if (dgv.SelectedRows.Count <= 0) { return null; }
            DataRowView drv = dgv.SelectedRows[0].DataBoundItem as DataRowView;
            int index = dt.Rows.IndexOf(drv.Row);
            DataRow dr = dt.Rows[index];

            return dr;
        }

        private void Dgv_CoilGrindList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Dgv_CoilGrindList.ClearSelection();
        }

        /// <summary>
        /// 動態更動ComboBox部位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fun_GetCbo_Part_Data(DataTable dtData)
        {
            DataRow drGetCoilNo = Fun_GetDataRowFromCurrentRow(Dgv_CoilGrindList, dtPdo);// dtPdo.Rows[dgv_CoilGrindList.CurrentRow.Index];

            Cob_Part.Text = string.Empty;
            Cob_Part.Items.Clear();

            var _Part = dtData.DefaultView.ToTable(true, new string[] { $"{ nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion) }" });
               
            ////ComboBox部位
            //string strSql = SqlFactory.Frm_3_3_SelectCurrent_Senssion_DB_GrindRecords(drGetCoilNo[nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString(), drGetCoilNo[nameof(PDOEntity.TBL_PDO.Plan_No)].ToString());//Out_Coil_ID
            //DataTable dtGetSenssionNumber = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "部位", "3-4");

            if (_Part.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"钢卷号[{drGetCoilNo[nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString().Trim()}]无部位资料");
                return;
            }

            for (int i = 0; i < _Part.Rows.Count; i++)
            {
                Cob_Part.Items.Add(_Part.Rows[i][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)].ToString());
            }
        }

        /// <summary>
        /// 動態更動ComboBox道次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cbo_Part_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fun_GetCbo_Pass_Data(dtGetGrindPlan);            

            #region Old
                // DataRow drGetCoilNo = Fun_GetDataRowFromCurrentRow(dgv_CoilGrindList, dtPdo);// dtPdo.Rows[dgv_CoilGrindList.CurrentRow.Index];

                // Cob_Pass.Text = string.Empty;
                // Cob_Pass.Items.Clear();
                // Fun_SectionToSectionCode();

                //// var _Part = dtData.DefaultView.ToTable(true, new string[] { $"{ nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion) }" });

                // //ComboBox道次
                // string strSql = SqlFactory.Frm_3_3_SelectPassNumberRecords_DB_GrindRecords(drGetCoilNo[nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString(), Section);//Out_Coil_ID
                // DataTable dtGetPassNumber = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"总道次","3-4");

                // //and[{ nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)}] = '{Section}'

                // if (dtGetPassNumber.IsNull())
                // {
                //     EventLogHandler.Instance.EventPush_Message($"钢卷号[{drGetCoilNo[nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString().Trim()}]无道次资料");
                //     return;
                // } 

                // for (int i = 0; i < dtGetPassNumber.Rows.Count; i++)
                // {
                //     Cob_Pass.Items.Add(dtGetPassNumber.Rows[i][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)].ToString());
                // }
                #endregion Old End
        }

        private void Fun_GetCbo_Pass_Data(DataTable dtData)
        {
            if (dtData.IsNull()) return;
            
            DataRow drGetCoilNo = Fun_GetDataRowFromCurrentRow(Dgv_CoilGrindList, dtPdo);// dtPdo.Rows[dgv_CoilGrindList.CurrentRow.Index];

            Cob_Pass.Text = string.Empty;
            Cob_Pass.Items.Clear();
            Fun_SectionToSectionCode();          

            ////ComboBox道次
            //string strSql = SqlFactory.Frm_3_3_SelectPassNumberRecords_DB_GrindRecords(drGetCoilNo[nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString(), Section);//Out_Coil_ID
            //DataTable dtGetPassNumber = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "总道次", "3-4");
            //and[{ nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)}] = '{Section}'
           
            var _Pass = dtData.DefaultView.ToTable(true, new string[] { $"{ nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass) }" });

            if (_Pass.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"钢卷号[{drGetCoilNo[nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString().Trim()}]无道次资料");
                return;
            }

            if(_Pass.Rows.Count > 0)
            {
                DataView dv = _Pass.DefaultView;
                dv.Sort = nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass);
                _Pass = dv.ToTable();
            }

            for (int i = 0; i < _Pass.Rows.Count; i++)
            {
                Cob_Pass.Items.Add(_Pass.Rows[i][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)].ToString());
            }
        }

        /// <summary>
        /// 位置轉代碼
        /// </summary>
        private void Fun_SectionToSectionCode()
        {
            switch (Cob_Part.Text)
            {
                case "H":
                case "1":
                    Section = "1";
                    break;
                case "M":
                case "2":
                    Section = "2";
                    break;
                case "T":
                case "3":
                    Section = "3";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 道次查詢研磨機資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Pass_SelectedIndexChanged(object sender, EventArgs e)
        {
            SymbolType sItem_Type = SymbolType.None;//菱形

            DataRow[] dataRows = dtGetGrindPlan.Select($" Current_Senssion = '{Cob_Part.Text}' AND Current_Pass = '{Cob_Pass.Text}' ");
            if (dataRows.Length <= 0) return;
            DataTable _dt = dataRows.CopyToDataTable();
            DataView dv = _dt.DefaultView;
            dv.Sort = nameof(GrindRecordsEntity.TBL_GrindRecords.Receive_Time);// "Receive_Time desc";
            DataTable dtSort = dv.ToTable();

            #region GR_1
            ZedGraphSetData zgSetData_Gr1_C = new ZedGraphSetData()
            {
                DtTrend = dtSort,
                StrYpoint = nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_MOTOR_CURRENT),
                StrTitle = Txt_Out_Coil_ID.Text,
                StrXTitle = "米數(m)",
                LineItem_Color = Color.Blue,
                StrTag = nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_MOTOR_CURRENT),
                LineItem_Type = sItem_Type,

                StrYTitle = "电流(A)",// nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_MOTOR_CURRENT),
                StrLineItem_Title = "电流(A)"//nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_MOTOR_CURRENT)
            };          

            ZedGraphSetData zgSetData_Gr1_S = new ZedGraphSetData()
            {
                DtTrend = dtSort,
                StrYpoint = nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_SPEED),
                StrTitle = Txt_Out_Coil_ID.Text,
                StrXTitle = "米數(m)",
                LineItem_Color = Color.DeepPink,
                StrTag = nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_SPEED),
                LineItem_Type = sItem_Type,

                StrYTitle = "研磨速度(mpm)",//nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_SPEED),
                StrLineItem_Title = "研磨速度(mpm)"//nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_SPEED)
            };
            Fun_Create_CoilChart(Zgc_GR_1_Current, Txt_Out_Coil_ID.Text, zgSetData_Gr1_C, zgSetData_Gr1_S);
           
            #endregion GR_1 End

            #region GR_2
            ZedGraphSetData zgSetData_Gr2_C = new ZedGraphSetData()
            {
                DtTrend = dtSort,
                StrYpoint = nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_MOTOR_CURRENT),
                StrTitle = Txt_Out_Coil_ID.Text,
                StrXTitle = "米數(m)",
                LineItem_Color = Color.OrangeRed,
                StrTag = nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_MOTOR_CURRENT),
                LineItem_Type = sItem_Type,

                StrYTitle = "电流(A)",//nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_MOTOR_CURRENT),
                StrLineItem_Title = "电流(A)"//nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_MOTOR_CURRENT)
            };
          
            ZedGraphSetData zgSetData_Gr2_S = new ZedGraphSetData()
            {
                DtTrend = dtSort,
                StrYpoint = nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_SPEED),
                StrTitle = Txt_Out_Coil_ID.Text,
                StrXTitle = "米數(m)",
                LineItem_Color = Color.Green,
                StrTag = nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_SPEED),
                LineItem_Type = sItem_Type,

                StrYTitle = "研磨速度(mpm)",// nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_SPEED),
                StrLineItem_Title = "研磨速度(mpm)"// nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_SPEED)
            };
            Fun_Create_CoilChart(Zgc_GR_2_Current, Txt_Out_Coil_ID.Text, zgSetData_Gr2_C, zgSetData_Gr2_S);
           // Fun_Create_CoilChart(Zgc_GR_2_Speed, Txt_Out_Coil_ID.Text);
            #endregion GR_2 End

            #region GR_3
            ZedGraphSetData zgSetData_Gr3_C = new ZedGraphSetData()
            {
                DtTrend = dtSort,
                StrYpoint = nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_MOTOR_CURRENT),
                StrTitle = Txt_Out_Coil_ID.Text,
                StrXTitle = "米數(m)",
                LineItem_Color = Color.Blue,
                StrTag = nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_MOTOR_CURRENT),
                LineItem_Type = sItem_Type,

                StrYTitle = "电流(A)",//nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_MOTOR_CURRENT),
                StrLineItem_Title = "电流(A)"// nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_MOTOR_CURRENT)
            };
            Fun_Create_CoilChart(Zgc_GR_3, Txt_Out_Coil_ID.Text, zgSetData_Gr3_C);
            #endregion GR_3 End

            #region GR_4
            ZedGraphSetData zgSetData_Gr4_C = new ZedGraphSetData()
            {
                DtTrend = dtSort,
                StrYpoint = nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_MOTOR_CURRENT),
                StrTitle = Txt_Out_Coil_ID.Text,
                StrXTitle = "米數(m)",
                LineItem_Color = Color.Green,
                StrTag = nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_MOTOR_CURRENT),
                LineItem_Type = sItem_Type,

                StrYTitle = "电流(A)",//nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_MOTOR_CURRENT),
                StrLineItem_Title = "电流(A)"// nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_MOTOR_CURRENT)
            };
            Fun_Create_CoilChart(Zgc_GR_4, Txt_Out_Coil_ID.Text, zgSetData_Gr4_C);
            #endregion GR_4 End

            #region GR_5
            ZedGraphSetData zgSetData_Gr5_C = new ZedGraphSetData()
            {
                DtTrend = dtSort,
                StrYpoint = nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_MOTOR_CURRENT),
                StrTitle = Txt_Out_Coil_ID.Text,
                StrXTitle = "米數(m)",
                LineItem_Color = Color.Purple,
                StrTag = nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_MOTOR_CURRENT),
                LineItem_Type = sItem_Type,

                StrYTitle = "电流(A)",//nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_MOTOR_CURRENT),
                StrLineItem_Title = "电流(A)"//nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_MOTOR_CURRENT)
            };
            Fun_Create_CoilChart(Zgc_GR_5, Txt_Out_Coil_ID.Text, zgSetData_Gr5_C);
            #endregion GR_5 End

            #region GR_6
            ZedGraphSetData zgSetData_Gr6_C = new ZedGraphSetData()
            {
                DtTrend = dtSort,
                StrYpoint = nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_MOTOR_CURRENT),
                StrTitle = Txt_Out_Coil_ID.Text,
                StrXTitle = "米數(m)",
                LineItem_Color = Color.Navy,
                StrTag = nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_MOTOR_CURRENT),
                LineItem_Type = sItem_Type,

                StrYTitle = "电流(A)",//nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_MOTOR_CURRENT),
                StrLineItem_Title = "电流(A)"//nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_MOTOR_CURRENT)
            };
            Fun_Create_CoilChart(Zgc_GR_6, Txt_Out_Coil_ID.Text, zgSetData_Gr6_C);
            #endregion GR_6 End
            #region Old Chart
            //  DataRow drGetCoilNo = Fun_GetDataRowFromCurrentRow(dgv_CoilGrindList, dtPdo);// dtPdo.Rows[dgv_CoilGrindList.CurrentRow.Index];

            //  Fun_SectionToSectionCode();
            //  GR1_Current_LineChart.Series[0].Points.Clear();
            //  GR2_Current_LineChart.Series[0].Points.Clear();
            //  GR3_Current_LineChart.Series[0].Points.Clear();
            //  GR4_Current_LineChart.Series[0].Points.Clear();
            //  GR5_Current_LineChart.Series[0].Points.Clear();
            //  GR6_Current_LineChart.Series[0].Points.Clear();
            //  GR1_Speed_LineChart.Series[0].Points.Clear();
            //  GR2_Speed_LineChart.Series[0].Points.Clear();

            //  string strSql = SqlFactory.Frm_3_3_SelectGRPresetData_Current_DB_GrindRecords(drGetCoilNo[nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString(), Section,Cob_Pass.Text);//Out_Coil_ID
            //  DataTable dtGetGR_Current_Data = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"研磨机PresetData","3-4");

            //  DataTable dtGetGR_Current = Fun_GetPasspos(dtGetGR_Current_Data);
            //  if (dtGetGR_Current.IsNull())
            //  {
            //      EventLogHandler.Instance.EventPush_Message($"钢卷号[{drGetCoilNo[nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString().Trim()}]无研磨记录");
            //      return;
            //  } 

            //  for (int i = 0; i < dtGetGR_Current.Rows.Count; i++)
            //  {
            //      GR1_Current_LineChart.Series[0].Points.AddXY(
            //                                                   dtGetGR_Current.Rows[i]["passpos"].ToString(),//nameof(TBL_GrindRecords.Receive_Time)
            //                                                   dtGetGR_Current.Rows[i][nameof(TBL_GrindRecords.GR1_MOTOR_CURRENT)].ToString());
            //      GR1_Speed_LineChart.Series[0].Points.AddXY(
            //                                                  dtGetGR_Current.Rows[i]["passpos"].ToString(),//nameof(TBL_GrindRecords.Receive_Time)
            //                                                  dtGetGR_Current.Rows[i][nameof(TBL_GrindRecords.GR1_BELT_SPEED)].ToString());
            //      GR2_Current_LineChart.Series[0].Points.AddXY(
            //                                                   dtGetGR_Current.Rows[i]["passpos"].ToString(),//nameof(TBL_GrindRecords.Receive_Time)
            //                                                   dtGetGR_Current.Rows[i][nameof(TBL_GrindRecords.GR2_MOTOR_CURRENT)].ToString());
            //      GR2_Speed_LineChart.Series[0].Points.AddXY(
            //                                                  dtGetGR_Current.Rows[i]["passpos"].ToString(),//nameof(TBL_GrindRecords.Receive_Time)
            //                                                  dtGetGR_Current.Rows[i][nameof(TBL_GrindRecords.GR2_BELT_SPEED)].ToString());
            //      GR3_Current_LineChart.Series[0].Points.AddXY(
            //                                                   dtGetGR_Current.Rows[i]["passpos"].ToString(),//nameof(TBL_GrindRecords.Receive_Time)
            //                                                   dtGetGR_Current.Rows[i][nameof(TBL_GrindRecords.GR3_MOTOR_CURRENT)].ToString());
            //      GR4_Current_LineChart.Series[0].Points.AddXY(
            //                                                   dtGetGR_Current.Rows[i]["passpos"].ToString(),//nameof(TBL_GrindRecords.Receive_Time)
            //                                                   dtGetGR_Current.Rows[i][nameof(TBL_GrindRecords.GR4_MOTOR_CURRENT)].ToString());
            //      GR5_Current_LineChart.Series[0].Points.AddXY(
            //                                                   dtGetGR_Current.Rows[i]["passpos"].ToString(),//nameof(TBL_GrindRecords.Receive_Time)
            //                                                   dtGetGR_Current.Rows[i][nameof(TBL_GrindRecords.GR5_MOTOR_CURRENT)].ToString());
            //      GR6_Current_LineChart.Series[0].Points.AddXY(
            //                                                   dtGetGR_Current.Rows[i]["passpos"].ToString(),//nameof(TBL_GrindRecords.Receive_Time)
            //                                                   dtGetGR_Current.Rows[i][nameof(TBL_GrindRecords.GR6_MOTOR_CURRENT)].ToString());
            //  }

            //  strSql = SqlFactory.Frm_3_3_SelectGRBeltData_DB_GrindRecords(drGetCoilNo[nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString(), Section, Cob_Pass.Text); //Out_Coil_ID
            //  DataTable dtGetGR_BeltData = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"研磨机研磨记录","3-4");

            ////  DataTable dtGetGR_BeltData = Fun_GetPasspos(dtGetGR_BeltData_Data);

            //  if (dtGetGR_BeltData.IsNull())
            //  {
            //      EventLogHandler.Instance.EventPush_Message($"钢卷号[{drGetCoilNo[nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString().Trim()}]无研磨机记录");
            //      return;
            //  }

            //  txtGR_1_BeltKind.Text = dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_KIND)].ToString() ?? string.Empty;

            //  txtGR_1_PARTICLE_NO.Text = dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_PARTICLE_NO)].ToString() ?? string.Empty;

            //  txtGR_1_DIR.Text = GR_BeltRotateDir(dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR1_BELT_ROTATE_DIR)].ToString());


            //  txtGR_2_BeltKind.Text = dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_KIND)].ToString() ?? string.Empty;

            //  txtGR_2_PARTICLE_NO.Text = dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_PARTICLE_NO)].ToString() ?? string.Empty;

            //  txtGR_2_DIR.Text = GR_BeltRotateDir(dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR2_BELT_ROTATE_DIR)].ToString());


            //  txtGR_3_BeltKind.Text = dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_BELT_KIND)].ToString() ?? string.Empty;

            //  txtGR_3_PARTICLE_NO.Text = dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_BELT_PARTICLE_NO)].ToString() ?? string.Empty;

            //  txtGR_3_DIR.Text = GR_BeltRotateDir(dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_BELT_ROTATE_DIR)].ToString());


            //  txtGR_4_BeltKind.Text = dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_BELT_KIND)].ToString() ?? string.Empty;

            //  txtGR_4_PARTICLE_NO.Text = dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_BELT_PARTICLE_NO)].ToString() ?? string.Empty;

            //  txtGR_4_DIR.Text = GR_BeltRotateDir(dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR4_BELT_ROTATE_DIR)].ToString());


            //  txtGR_5_BeltKind.Text = dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_BELT_KIND)].ToString() ?? string.Empty;

            //  txtGR_5_PARTICLE_NO.Text = dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_BELT_PARTICLE_NO)].ToString() ?? string.Empty;

            //  txtGR_5_DIR.Text = GR_BeltRotateDir(dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR5_BELT_ROTATE_DIR)].ToString());


            //  txtGR_6_BeltKind.Text = dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_BELT_KIND)].ToString() ?? string.Empty;

            //  txtGR_6_PARTICLE_NO.Text = dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_BELT_PARTICLE_NO)].ToString() ?? string.Empty;

            //  txtGR_6_DIR.Text = GR_BeltRotateDir(dtGetGR_BeltData.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.GR6_BELT_ROTATE_DIR)].ToString());
            #endregion Old Chart End

        }

        private void Fun_Create_CoilChart(ZedGraphControl zgc, string strCoilNo, ZedGraphSetData zgSetData)
        {
            //初始化ZedGraph
            Fun_IniZedGraph(zgc);
            MasterPane masterPane = zgc.MasterPane;
            GraphPane myPane = zgc.GraphPane;
            LineItem myCurve;

            //设置标题和轴标签
            myPane.Title.Text = $"{strCoilNo}";

            //StringBuilder sbTableNoData = new StringBuilder();

            //==============================================================================

            //SymbolType sItem_Type = SymbolType.None;//菱形

            ////DataTable dtPointData = Fun_GetTrendData(Desc_speed, Code_LINE_Speed_Actual, strCoilNo, dtGetPdo);
            ////if (dtPointData.IsNull())
            ////{
            ////    if (LanguageHandler.Instance.DefaultLanguage)
            ////        sbTableNoData.Append($"{LINE_Speed_Actual_cn},");
            ////    else
            ////        sbTableNoData.Append($"{LINE_Speed_Actual_en},");
            ////}
            ////else
            ////{
            //    ZedGraphSetData zgSetData = new ZedGraphSetData()
            //    {
            //        DtTrend = dtChartData,
            //        StrYpoint = nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_MOTOR_CURRENT),
            //        StrTitle = strCoilNo,
            //        StrXTitle = "米數(m)",
            //        LineItem_Color = Color.MediumVioletRed,
            //        StrTag = nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_MOTOR_CURRENT),
            //        LineItem_Type = sItem_Type,

            //        StrYTitle = nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_MOTOR_CURRENT),
            //        StrLineItem_Title = nameof(GrindRecordsEntity.TBL_GrindRecords.GR3_MOTOR_CURRENT)
            //    };
            //    //if (LanguageHandler.Instance.DefaultLanguage)
            //    //{
            //    //    zgSetData.StrYTitle = LINE_Speed_Actual_cn;
            //    //    zgSetData.StrLineItem_Title = LINE_Speed_Actual_cn;
            //    //}
            //    //else
            //    //{
            //    //    zgSetData.StrYTitle = LINE_Speed_Actual_en;
            //    //    zgSetData.StrLineItem_Title = LINE_Speed_Actual_en;
            //    //}

                GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc);
                masterPane.Add(graphs);
            //}

            //==============================================================================
            if (myPane.XAxis != null)
            {
                //显示 x 轴 Title
                myPane.XAxis.Title.Text = "米数(m)";
                //显示 x 轴 网格
                myPane.XAxis.MajorGrid.IsVisible = true;
            }

            if (myPane.YAxis != null)
            {
                //显示 y 轴 网格
                myPane.YAxis.MajorGrid.IsVisible = true;
            }
            //==============================================================================
            //用渐变填充轴背
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);

            zgc.AxisChange();
            zgc.Invalidate();
            zgc.IsShowPointValues = true;// '顯示節點資料
            //恢复默认大小
            zgc.RestoreScale(myPane);

        }

        private void Fun_Create_CoilChart(ZedGraphControl zgc, string strCoilNo, ZedGraphSetData zgSetData1, ZedGraphSetData zgSetData2)
        {
            //初始化ZedGraph
            Fun_IniZedGraph(zgc);
            MasterPane masterPane = zgc.MasterPane;
            GraphPane myPane = zgc.GraphPane;
            LineItem myCurve;

            //设置标题和轴标签
            myPane.Title.Text = $"{strCoilNo}";

            StringBuilder sbTableNoData = new StringBuilder();

            //==============================================================================

            GraphPane graphs1 = Fun_GetGraphPane(zgSetData1, zgc);
            masterPane.Add(graphs1);

            GraphPane graphs2 = Fun_GetGraphPane(zgSetData2, zgc);
            masterPane.Add(graphs2);

            //==============================================================================
            if (myPane.XAxis != null)
            {
                //显示 x 轴 Title
                myPane.XAxis.Title.Text = "米数(m)";
                //显示 x 轴 网格
                myPane.XAxis.MajorGrid.IsVisible = true;
            }

            if (myPane.YAxis != null)
            {
                //显示 y 轴 网格
                myPane.YAxis.MajorGrid.IsVisible = true;
            }
            //==============================================================================
            //用渐变填充轴背
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);

            zgc.AxisChange();
            zgc.Invalidate();
            zgc.IsShowPointValues = true;// '顯示節點資料
            //恢复默认大小
            zgc.RestoreScale(myPane);

        }

        /// <summary>
        /// 初始化_ZedGraphControl
        /// </summary>
        /// <param name="zgc"></param>
        private void Fun_IniZedGraph(ZedGraphControl zgc)
        {
            zgc.GraphPane.CurveList.Clear();
            zgc.GraphPane.GraphObjList.Clear();
            zgc.GraphPane.Y2AxisList.Clear();
            zgc.GraphPane.YAxisList.Clear();
            zgc.Refresh();

            //zgc.GraphPane.YAxisList.Add("New_YAxis");
            //zgc.GraphPane.Y2AxisList.Add("New_Y2Axis");
        }

        /// <summary>
        /// 设定要绘制的图表
        /// </summary>
        /// <param name="ZGsetData">ZedGraph相关设定资料</param>
        /// <param name="dobMax">Y轴刻度最大值</param>
        /// <param name="dobMin">Y轴刻度最小值</param>
        /// <param name="zgCtrl">ZedGraph元件名称</param>
        /// <returns>GraphPane</returns>
        private GraphPane Fun_GetGraphPane(ZedGraphSetData ZGsetData, ZedGraphControl zgCtrl, double dobMax = -1, double dobMin = -1)
        {
            GraphPane gpZGC;
            gpZGC = zgCtrl.GraphPane;

            if (string.IsNullOrEmpty(ZGsetData.StrXpoint))
                ZGsetData.StrXpoint = passpos;

            gpZGC.Title.Text = ZGsetData.StrTitle;
            gpZGC.Tag = ZGsetData.StrTag;

            PointPairList pointList = Fun_GetPointPairList(ZGsetData.DtTrend, ZGsetData.StrYpoint);
            LineItem myCurve = gpZGC.AddCurve(ZGsetData.StrLineItem_Title, pointList, ZGsetData.LineItem_Color, ZGsetData.LineItem_Type);
            myCurve.Line.Width = 3;

            YAxis y2 = new YAxis();
            y2.IsVisible = true;
            y2.Title.Text = ZGsetData.StrYTitle;// ZGsetData.StrLineItem_Title;
            gpZGC.YAxisList.Add(y2);

            //设定Y轴 刻度颜色,字体大小
            y2.Scale.FontSpec.FontColor = ZGsetData.LineItem_Color;
            y2.Scale.FontSpec.Size = 12;
            //设定Y轴 文字颜色,字体大小
            y2.Title.FontSpec.FontColor = ZGsetData.LineItem_Color;
            y2.Title.FontSpec.Size = 12;

            // turn off the opposite tics so the Y2 tics don't show up on the Y axis
            y2.MajorTic.IsInside = false;
            y2.MinorTic.IsInside = false;
            y2.MajorTic.IsOpposite = false;
            y2.MinorTic.IsOpposite = false;
            // Align the Y2 axis labels so they are flush to the axis
            y2.Scale.Align = AlignP.Inside;
            //y2.Type = ZedGraph.AxisType.Log;

            if (dobMax == -1)
                dobMin = pointList.Min(point => point.Y) * 1.1;
            if (dobMax == -1)
                dobMax = pointList.Max(point => point.Y) * 0.9;

            y2.Scale.Max = dobMax;
            y2.Scale.Min = dobMin;
            //gpZGC.XAxis.Title.Text = "My X Axis"; //ZGsetData.StrXTitle;           
            //gpZGC.YAxis.Title.Text =  "My Y Axis";//ZGsetData.StrYTitle;
            return gpZGC;
        }

        /// <summary>
        /// 从DataTable取得PointPairList
        /// </summary>
        /// <param name="dtPointData">资料来源</param>
        /// <param name="strValue">Y轴栏位</param>
        /// <returns>PointPairList</returns>
        private PointPairList Fun_GetPointPairList(DataTable dtPointData, string strValue)
        {
            PointPairList pointList = new PointPairList();
            foreach (DataRow dr in dtPointData.Rows)
            {
                pointList.Add(double.Parse(dr[passpos].ToString()), double.Parse(dr[strValue].ToString()));
            }
            return pointList;
        }

        private void Zgc_GR_Current_MouseLeave(object sender, EventArgs e)
        {
            ZedGraphControl zedGraph = (ZedGraphControl)sender;
            Graphics gc = zedGraph.CreateGraphics();
            Pen pen = new Pen(Color.Orange);
            //設定畫筆寬度
            pen.Width = 1;
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            RectangleF rect = zedGraph.GraphPane.Chart.Rect;

            //zedGraphControl1.AxisChange();
            //zedGraphControl1.IsShowPointValues = true;//顯示節點資料
            zedGraph.Refresh();

            //畫豎線
            gc.DrawLine(pen, 0, 0, 0, 0);
            //畫橫線
            gc.DrawLine(pen, 0, 0, 0, 0);
        }

        private void Zgc_GR_Current_MouseMove(object sender, MouseEventArgs e)
        {
            ZedGraphControl zedGraph = (ZedGraphControl)sender;
            // Save the mouse location
            PointF mousePt = new PointF(e.X, e.Y);

            Graphics gc = zedGraph.CreateGraphics();
            Pen pen = new Pen(Color.Orange);
            //設定畫筆寬度
            pen.Width = 3;
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            RectangleF rect = zedGraph.GraphPane.Chart.Rect;

            //確保在畫圖區域
            if (rect.Contains(e.Location))
            {
                zedGraph.AxisChange();
                zedGraph.IsShowPointValues = true;//顯示節點資料
                zedGraph.Refresh();

                //畫豎線
                gc.DrawLine(pen, e.X, rect.Top, e.X, rect.Bottom);
                //畫橫線
                gc.DrawLine(pen, rect.Left, e.Y, rect.Right, e.Y);

                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            }
            else { }
        }

        private string Zgc_GR_Current_PointValueEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            PointPair pt = curve[iPt];
            return curve.Label.Text + " \n X:" + pt.X.ToString() + " \n Y:" + pt.Y.ToString();
        }

        

        /// <summary>
        /// 钢卷编号ComboBox Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Out_Coil_Click(object sender, EventArgs e)
        {
            string strSql = SqlFactory.Frm_2_2_FrmLoadDGV_DB_PDO();
            DataTable dtGetCoilList = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "钢卷清单", "3-4");

            if (dtGetCoilList.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无出口卷号清单");
                return;
            }

            Cob_Out_Coil.DisplayMember = nameof(PDIEntity.TBL_PDI.Out_Coil_ID);
            Cob_Out_Coil.ValueMember = nameof(PDIEntity.TBL_PDI.Out_Coil_ID);
            Cob_Out_Coil.DataSource = dtGetCoilList;
        }

       

        #region 目前無用到之程式
        /// <summary>
        /// 查詢部位的總道次
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <param name="Senssion"></param>
        private void Fun_SelectPassNumber(string Coil_ID, string Senssion)
        {
            string strSql = SqlFactory.Frm_3_3_SelectPassCount(Coil_ID, Senssion);
            dtPass = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "研磨机皮带", "3-4");
        }

        /// <summary>
        /// 代碼轉中文(0:正转 ; 1:反转)
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static string GR_BeltRotateDir(string dir)
        {
            if (dir.IsEmpty()) dir = string.Empty;
            switch (dir)
            {
                case "0":
                    dir = "正转";
                    break;

                case "1":
                    dir = "反转";
                    break;

                default:
                    break;
            }
            return dir;
        }
        /// <summary>
        /// 第一道次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_FirstPass_Click(object sender, EventArgs e)
        {
            if (Cob_Pass.Items.Count.Equals(0))
            {
                return;
            }

            Cob_Pass.SelectedIndex = 0;

            EventLogHandler.Instance.EventPush_Message($"查阅钢卷号:[{Coil_ID.Trim()}]第一道次");
            EventLogHandler.Instance.LogInfo("3-4", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim() }道次操作", $"查阅钢卷号:[{Coil_ID.Trim()}]第一道次");
            PublicComm.ClientLog.Info($"查閱鋼卷編號:[{Coil_ID.Trim()}]第一道次");
        }
        /// <summary>
        /// 最後一道次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_FinalPass_Click(object sender, EventArgs e)
        {
            if (Cob_Pass.Items.Count.Equals(0)) return;

            Cob_Pass.SelectedIndex = Cob_Pass.Items.Count - 1;

            EventLogHandler.Instance.EventPush_Message($"查阅钢卷号:[{Coil_ID.Trim()}]最后一道次");
            EventLogHandler.Instance.LogInfo("3-4", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}道次操作", $"查阅钢卷号:[{Coil_ID.Trim()}]最后一道次");
            PublicComm.ClientLog.Info($"查閱鋼卷編號:[{Coil_ID.Trim()}]最後一道次");
        }
        /// <summary>
        /// 前一道次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_LastPass_Click(object sender, EventArgs e)
        {
            if (Cob_Pass.Items.Count.Equals(0)) return;

            int ComboBoxIndex = Cob_Pass.SelectedIndex-1;

            if (ComboBoxIndex <= 0) return;

            Cob_Pass.SelectedIndex = ComboBoxIndex;

            EventLogHandler.Instance.LogInfo("3-4", $"使用者:{PublicForms.Main.lblLoginUser.Text }道次操作", $"查阅钢卷号:[{Coil_ID.Trim()}]第[{ComboBoxIndex + 1}]道次");
            PublicComm.ClientLog.Info($"查閱鋼卷編號:[{Coil_ID.Trim()}]第[{ComboBoxIndex+1}]道次");
        }
        /// <summary>
        /// 後一道次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_NextPass_Click(object sender, EventArgs e)
        {
            if (Cob_Pass.Items.Count.Equals(0)) return;

            int ComboBoxIndex = Cob_Pass.SelectedIndex + 1;

            if (ComboBoxIndex > Cob_Pass.Items.Count-1) return;

            Cob_Pass.SelectedIndex = ComboBoxIndex;

            EventLogHandler.Instance.LogInfo("3-4", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim() }道次操作", $"查阅钢卷号:[{Coil_ID.Trim()}]第[{ComboBoxIndex + 1}]道次");
            PublicComm.ClientLog.Info($"查閱鋼卷編號:[{Coil_ID.Trim()}]第[{ComboBoxIndex+1}]道次");
        }
        #endregion

        #region 語系切換,文字調整 Fun_LanguageIsEn_Font
        private void Fun_LanguageIsEn_Font14_9(object sender, EventArgs e)
        {
            FontStyle fs = ((System.Windows.Forms.Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((System.Windows.Forms.Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)9, fs);
        }
        private void Fun_LanguageIsEn_Font14_10(object sender, EventArgs e)
        {
            FontStyle fs = ((System.Windows.Forms.Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((System.Windows.Forms.Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font14_12(object sender, EventArgs e)
        {
            FontStyle fs = ((System.Windows.Forms.Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((System.Windows.Forms.Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)12, fs);
        }
        private void Fun_LanguageIsEn_Font12_10(object sender, EventArgs e)
        {
            FontStyle fs = ((System.Windows.Forms.Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((System.Windows.Forms.Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)12, fs);
            else
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font12_9(object sender, EventArgs e)
        {
            FontStyle fs = ((System.Windows.Forms.Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((System.Windows.Forms.Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)12, fs);
            else
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)9, fs);
        }
        #endregion Fun_LanguageIsEn_Font End
    }
}

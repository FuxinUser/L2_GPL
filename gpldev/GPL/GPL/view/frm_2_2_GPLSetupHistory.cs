using DBService.L1Repository;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using GPLManager.Util;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using static GPLManager.DataBaseTableFactory;

namespace GPLManager
{
    public partial class Frm_2_2_GPLSetupHistory : Form
    {
        DataTable dtCoilList;
        DataTable dtHead_PassList;//頭段道次區間
        DataTable dtMid_PassList;//中段道次區間
        DataTable dtTail_PassList;//尾段道次區間
        
        private LanguageHandler LanguageHand;//語系

        public Frm_2_2_GPLSetupHistory()
        {
            InitializeComponent();
        }

        private void Frm_2_2_GPLSetupHistory_Load(object sender, EventArgs e)
        {
            if (PublicForms.GPLSetupHistory == null) 
                PublicForms.GPLSetupHistory = this;
            Fun_DataGridView_CoilListSelect();
            Fun_TabControl_Page_Clear();

           
            Tab_GR_H_SectionPage.Text = "头段皮带参数模板 :  ";
            Tab_GR_M_SectionPage.Text = "中段皮带参数模板 :  ";
            Tab_GR_T_SectionPage.Text = "尾段皮带参数模板 :  ";

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            if (Rdb_DateTime.Checked)
            {
                if (Dtp_Start_Time.DateTimeRangeIsFail(Dtp_Finish_Time.Value))
                {
                    EventLogHandler.Instance.EventPush_Message($"日期区间起讫时间不正确，请重新确认");
                    return;
                }
            }

            string strSql = SqlFactory.frm_2_2_SelectDGV_DB_PDO(); //SqlFactory.Frm_2_2_Select204();
            Fun_DataGridViewDisplay(strSql);

            EventLogHandler.Instance.LogInfo("2-1", $"历史生产设定", $"历史生产设定查询");
            PublicComm.ClientLog.Info($"歷史生產設定查詢");
        }

        /// <summary>
        /// 钢卷清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_CoilID_Click(object sender, EventArgs e)
        {
            string strSql = SqlFactory.Frm_2_2_FrmLoadDGV_DB_PDO();
            DataTable dtGetCoilList = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "Table[TBL_PDO]", "2-2");

            if (dtGetCoilList.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无钢卷清单");
                return;
            }

            Cob_CoilID.DisplayMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_CoilID.ValueMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_CoilID.DataSource = dtGetCoilList;
        }

        private void Fun_DataGridView_CoilListSelect()
        {
            string strSql = SqlFactory.frm_2_2_SelectDGV_DB_PDO();// Frm_2_2_FrmLoadDGV_DB_PDO();
            Fun_DataGridViewDisplay(strSql);
        }
        private void Dgv_coilList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Fun_TabControl_Page_Clear();
            Fun_GrindPlanRecordsDisplay();
        }
        private void Fun_TabControl_Page_Clear()
        {
            Lbl_Out_Mat_No.Text = "";
            Lbl_Thick.Text = "";
            Lbl_Customer.Text = "";
            Lbl_SteelGrade.Text = "";
            Lbl_GradeGroup.Text = "";

            Tab_GR_H_PassControl.TabPages.Clear();
            Tab_GR_M_PassControl.TabPages.Clear();
            Tab_GR_T_PassControl.TabPages.Clear();
        }
        private void Fun_DataGridViewDisplay(string strSql)
        {
            dtCoilList = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "Table[TBL_PDO]", "2-2");
            //dtCoilList = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL_HISTORY, "FUXIN_GPL_HISTORY Table[L2L1_204]", "2-2");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_coilList, dtCoilList);
            DGVColumnsHandler.Instance.Frm_2_2CoilList(Dgv_coilList);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_coilList);
        }

        /// <summary>
        /// 搜尋[TBL_GrindPlan_Records]
        /// Get [GradeGroup],[Thickness_From],[Thickness_To] [Pass_Section],[BeltPattern]
        /// 條件 [Coil_ID]
        /// </summary>
        private void Fun_GrindPlanRecordsDisplay()
        {          
            string strIn_Mat_No = dtCoilList.Rows[Dgv_coilList.CurrentRow.Index][nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString();//["CoilId"].ToString();// L2L1_204.CoilId 

            string getOut_Mat_No = dtCoilList.Rows[Dgv_coilList.CurrentRow.Index][nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString();//["CoilId"].ToString();// L2L1_204.CoilId 

            DateTime.TryParseExact(dtCoilList.Rows[Dgv_coilList.CurrentRow.Index][nameof(PDOEntity.TBL_PDO.Start_Time)].ToString(), "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out var dDate_Start) ;
             
            string strDate = dDate_Start.ToString("yyyyMMdd");//
            string strTime = dDate_Start.ToString("HHmmss");//
        
            if (! int.TryParse(strDate,out int intDate))
                EventLogHandler.Instance.EventPush_Message($"栏位转换错误");
            if (!int.TryParse(strTime, out int intTime))
                EventLogHandler.Instance.EventPush_Message($"栏位转换错误");
                     

            //PDO 選 CG210432100000     2022-03-20 08:41:09.000
            // L2L1_204 跟 L2L1_205 Table 搜尋條件用 date = '20220320' AND time = '84109 ' AND PressetPosition = '0'

            string strSql_2045 = SqlFactory.Frm_2_2_Select204205(strIn_Mat_No, intDate.ToString(), intTime.ToString());
            DataTable dtGet204205= Data_Access.Fun_SelectDate(strSql_2045, GlobalVariableHandler.Instance.strConn_GPL_HISTORY, "研磨记录", "2-2");
            if (dtGet204205.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无研磨历史资料");
                return;
            }
            #region Old
            //DataTable dtGetGrindPlanRecords = new DataTable();
            //string strSql;

            ////string strSql = SqlFactory.Frm_2_2_SelectGrindPlanRecords_DB_TBL_GrindPlan_Records(getOut_Mat_No);
            ////DataTable dtGetGrindPlanRecords = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"研磨记录","2-2");

            //#region [頭段]
            //strSql = SqlFactory.Frm_2_2_SelectSectionGrindPlanHistory(getOut_Mat_No, "H");
            //DataTable dtGetHead = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "头段研磨记录", "2-2");

            //if (!dtGetHead.IsNull())
            //    dtGetGrindPlanRecords.Merge(dtGetHead);
            //#endregion

            //#region [中段]
            //strSql = SqlFactory.Frm_2_2_SelectSectionGrindPlanHistory(getOut_Mat_No, "M");
            //DataTable dtGetMid = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "中段段研磨记录", "2-2");
            //if (!dtGetMid.IsNull())
            //    dtGetGrindPlanRecords.Merge(dtGetMid);
            //#endregion

            //#region [尾段]
            //strSql = SqlFactory.Frm_2_2_SelectSectionGrindPlanHistory(getOut_Mat_No, "T");
            //DataTable dtGetTail = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "尾段段研磨记录", "2-2");
            //if (!dtGetTail.IsNull())
            //    dtGetGrindPlanRecords.Merge(dtGetTail);
            //#endregion

            //if (dtGetGrindPlanRecords.IsNull())
            //{
            //    EventLogHandler.Instance.EventPush_Message($"无研磨历史资料");
            //    //return;
            //}

            ////設定頭、中、尾段標題 
            //Fun_PassSectionTitleSet(dtGetGrindPlanRecords, getOut_Mat_No);

            #endregion Old end
            DataRow drGetDgCoilList = dtCoilList.Rows[Dgv_coilList.CurrentRow.Index];

            //PDO 入口卷号 = 204 钢卷号
           //品直群组代码 204 刚种抓 地2码开始的四码

            #region 填資料
            //鋼卷號
            Lbl_Out_Mat_No.Text = dtGet204205.Rows[0][nameof(L2L1MsgDBModel.L2L1_204.CoilId)].ToString() ?? string.Empty;
            //出口厚度
            Lbl_Thick.Text =  dtGet204205.Rows[0][nameof(L2L1MsgDBModel.L2L1_204.CoilThickness)].ToString() ?? string.Empty;                     
           
            //客戶代碼
            Lbl_Customer.Text = drGetDgCoilList[nameof(PDIEntity.TBL_PDI.Order_Cust_Code)].ToString() ?? string.Empty;

            //鋼種
            string strSG = dtGet204205.Rows[0][nameof(L2L1MsgDBModel.L2L1_204.SteelGrade)].ToString();
            Lbl_SteelGrade.Text = strSG ?? string.Empty;            
            //客戶品質群組.....品質等級群組
            Lbl_GradeGroup.Text = !string.IsNullOrEmpty(strSG)? strSG.Substring(1, 4) ?? string.Empty : string.Empty;
            #endregion

           
            //設定頭、中、尾段Data 
            Fun_PassSectionTitleSet(dtGet204205);
        }
        private void Fun_PassSectionTitleSet(DataTable dt)
        {
            int intPassNum_H = 0;
            int intPassNum_M = 0;
            int intPassNum_T = 0;
            if (!dt.IsNull())
            {
                int.TryParse(dt.Rows[0][nameof(L2L1MsgDBModel.L2L1_204.PassNumberForCoilHeadGrinding)].ToString(), out intPassNum_H);
                int.TryParse(dt.Rows[0][nameof(L2L1MsgDBModel.L2L1_204.PassNumberForCoilCenterGrinding)].ToString(), out intPassNum_M);
                int.TryParse(dt.Rows[0][nameof(L2L1MsgDBModel.L2L1_204.PassNumberForCoilTailGrinding)].ToString(), out intPassNum_T);
            }
          
           
            Tab_GR_H_SectionPage.Text = $"头段研磨总道次 : {intPassNum_H}";
            Tab_GR_M_SectionPage.Text =  $"中段研磨总道次 : {intPassNum_M}";
            Tab_GR_T_SectionPage.Text = $"尾段研磨总道次 : {intPassNum_T}";

            AddPageHandler.AddPageInstance.AddPage(Tab_GR_H_PassControl,intPassNum_H,"H" ,dt);
            AddPageHandler.AddPageInstance.AddPage(Tab_GR_M_PassControl,intPassNum_M,"M" ,dt);
            AddPageHandler.AddPageInstance.AddPage(Tab_GR_T_PassControl,intPassNum_T,"T" ,dt);
        }
        private void Fun_PassSectionTitleSet(DataTable dt,string Coil_ID)
        {
            string strSql;

            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i][nameof(TBL_GrindPlanHistory.Pass_Section)].ToString())
                {
                    case "H":
                        Tab_GR_H_SectionPage.Text = $"头段研磨总道次 :  {dt.Rows[i][nameof(L2L1MsgDBModel.L2L1_204.PassNumberForCoilHeadGrinding)]}";
                        strSql = SqlFactory.Frm_2_2_SelectPassFromTo_DB_TBL_BeltPattern(dt.Rows[i][nameof(TBL_GrindPlanHistory.BeltPattern)].ToString());
                        dtHead_PassList = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "[TBL_BeltPattern]头段","2-2");

                        if (dtHead_PassList.IsNull())
                        {
                            EventLogHandler.Instance.EventPush_Message($"头段无研磨总道次资料");
                            return;
                        } 

                        AddPageHandler.AddPageInstance.AddPage(Tab_GR_H_PassControl, dtHead_PassList,"H", dt.Rows[i][nameof(TBL_GrindPlanHistory.BeltPattern)].ToString(), Coil_ID);
                        break;
                    case "M":
                        Tab_GR_M_SectionPage.Text = $"中段研磨总道次 :  {dt.Rows[i][nameof(TBL_GrindPlanHistory.BeltPattern)]}";
                        strSql = SqlFactory.Frm_2_2_SelectPassFromTo_DB_TBL_BeltPattern(dt.Rows[i][nameof(TBL_GrindPlanHistory.BeltPattern)].ToString());
                        dtMid_PassList = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "[TBL_BeltPattern]中段","2-2");

                        if (dtMid_PassList.IsNull())
                        {
                            EventLogHandler.Instance.EventPush_Message($"中段无研磨总道次资料");
                            return;
                        }

                        AddPageHandler.AddPageInstance.AddPage(Tab_GR_M_PassControl, dtMid_PassList,"M", dt.Rows[i][nameof(TBL_GrindPlanHistory.BeltPattern)].ToString(), Coil_ID);
                        break;
                    case "T":
                        Tab_GR_T_SectionPage.Text = $"尾段研磨总道次 :  {dt.Rows[i][nameof(TBL_GrindPlanHistory.BeltPattern)]}";
                        strSql = SqlFactory.Frm_2_2_SelectPassFromTo_DB_TBL_BeltPattern(dt.Rows[i][nameof(TBL_GrindPlanHistory.BeltPattern)].ToString());
                        dtTail_PassList = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"[TBL_BeltPattern]尾段", "2-2");

                        if (dtTail_PassList.IsNull())
                        {
                            EventLogHandler.Instance.EventPush_Message($"尾段无研磨总道次资料");
                            return;
                        }

                        AddPageHandler.AddPageInstance.AddPage(Tab_GR_T_PassControl, dtTail_PassList,"T", dt.Rows[i][nameof(TBL_GrindPlanHistory.BeltPattern)].ToString(), Coil_ID);
                        break;
                    default:
                        break;
                }
            }
        }

        private void Tab_GR_SectionControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_GR_SectionControl, e);
        }
        private void Tab_GR_H_PassControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_GR_H_PassControl, e);
        }
        private void Tab_GR_M_PassControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_GR_M_PassControl, e);
        }
        private void Tab_GR_T_PassControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_GR_T_PassControl, e);
        }
                     

      

        #region 語系切換,文字調整 Fun_LanguageIsEn_Font
        private void Fun_LanguageIsEn_Font14_9(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)9, fs);
        }
        private void Fun_LanguageIsEn_Font16_10(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)16, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font16_12(object sender, EventArgs e)
        {

        }
        private void Fun_LanguageIsEn_Font12_10(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font12_9(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)9, fs);
        }
        #endregion Fun_LanguageIsEn_Font End

    }
}

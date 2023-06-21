using DBService.Repository.EventLog;
using GPLManager.Util;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static GPLManager.DataBaseTableFactory;

namespace GPLManager
{
    public partial class Frm_5_1_EventLog : Form
    {
        DataTable dtLog;
        DataTable dtComputerName;
        public string strDateTimeStart = string.Empty;
        public string strDateTimeEnd = string.Empty;
        //語系
        private LanguageHandler LanguageHand;
        public Frm_5_1_EventLog()
        {
            InitializeComponent();
        }
        private void Frm_5_1_EventLog_Shown(object sender, EventArgs e)
        {
            if(PublicForms.EventLog == null) PublicForms.EventLog = this;
            //類別
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.EventLogLevel, Cob_EventType);
            //電腦名稱
            //系統
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.System, Cob_System_ID);
           
            //起時-1小時
            Dtp_Start_Time.Value = DateTime.Now.AddHours(-1);
            //訖時+1小時
            Dtp_Finish_Time.Value = DateTime.Now.AddHours(1);

            //Fun_SelectTop50();

            //Fun_DataGridViewSetup();

            Fun_DataTableEvent_TypeToString();

            Fun_SelectComputerName();
            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }
        private void Fun_DataGridViewSetup()
        {
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_EventLog, dtLog);
            DGVColumnsHandler.Instance.frm_5_1_EventLog(Dgv_EventLog);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_EventLog);
        }
        /// <summary>
        /// 前50笔
        /// </summary>
        private void Fun_SelectTop50()
        {
            string strSql = SqlFactory.Frm_5_1_SelectEventLog_DB_EventLog();
            dtLog = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"事件记录前50笔","5-1");

            if (dtLog.IsNull()) return;
        }

        private void Fun_SelectKeyword()
        {

            //if (chkTime.Checked)
            //{
            if (Dtp_Start_Time.DateTimeRangeIsFail(Dtp_Finish_Time.Value))
            {
                EventLogHandler.Instance.EventPush_Message($"日期区间起讫时间不正确，请重新确认");
                return;
            }
            //}
            //20220718:如果没输入,转换int会跳错误,wei:先不改
            int DataCount = Convert.ToInt32(Txt_SelectTop.Text.Trim());

            string strSql = SqlFactory.Frm_5_1_SearchEventLog_DB_EventLog(Dtp_Start_Time.Value.ToString("yyyy-MM-dd HH"), Dtp_Finish_Time.Value.ToString("yyyy-MM-dd HH"), DataCount);
            if(dtLog!= null && dtLog.Rows.Count >0)
                dtLog.Clear();
            dtLog = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "事件记录查询条件", "5-1");
        }
     
        private void Fun_SelectComputerName()
        {
            string strSql = SqlFactory.Frm_5_1_SelectComputerName_DB_EventLog();
            dtComputerName = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"电脑名称","5-1");

            if (dtComputerName.IsNull()) return;

            Cob_ComputerName.DisplayMember = nameof(EventLogEntity.TBL_EventLog.FrameGroup_No);
            Cob_ComputerName.ValueMember = nameof(EventLogEntity.TBL_EventLog.FrameGroup_No);
            Cob_ComputerName.DataSource = dtComputerName;
            Cob_ComputerName.Text = string.Empty;
        }
        private void Fun_DataTableEvent_TypeToString()
        {

            if (dtLog.IsNull()) return;

            for (int i = 0; i < dtLog.Rows.Count; i++)
            {
                if (Dgv_EventLog.Rows[i].Cells[nameof(EventLogEntity.TBL_EventLog.Event_Type)].Value.ToString().Equals("1"))
                {
                    Dgv_EventLog.Rows[i].Cells[nameof(EventLogEntity.TBL_EventLog.Event_Type)].Value = "Error";
                }
                else if (Dgv_EventLog.Rows[i].Cells[nameof(EventLogEntity.TBL_EventLog.Event_Type)].Value.ToString().Equals("2"))
                {
                    Dgv_EventLog.Rows[i].Cells[nameof(EventLogEntity.TBL_EventLog.Event_Type)].Value = "Alarm";
                }
                else if (Dgv_EventLog.Rows[i].Cells[nameof(EventLogEntity.TBL_EventLog.Event_Type)].Value.ToString().Equals("3"))
                {
                    Dgv_EventLog.Rows[i].Cells[nameof(EventLogEntity.TBL_EventLog.Event_Type)].Value = "Info";
                }
                else if (Dgv_EventLog.Rows[i].Cells[nameof(EventLogEntity.TBL_EventLog.Event_Type)].Value.ToString().Equals("4"))
                {
                    Dgv_EventLog.Rows[i].Cells[nameof(EventLogEntity.TBL_EventLog.Event_Type)].Value = "Debug";
                }
            }
        }
       
        private void Btn_QueryOK_Click(object sender, EventArgs e)
        {
            Fun_SelectKeyword();

            Fun_DataGridViewSetup();

            Fun_DataTableEvent_TypeToString();
        }

        private void Dgv_EventLog_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (Dgv_EventLog.DgvIsNull())
                return;

            if (Dgv_EventLog.Rows.Count != 0)
            {
                //foreach (DataGridViewRow row in Dgv_RollHistory.Rows)
                //{

                //string rollStatus = dtHistory.Rows[e.RowIndex]["RollUse_Status"].ToString();
                string strType = Dgv_EventLog.Rows[e.RowIndex].Cells[nameof(EventLogEntity.TBL_EventLog.Event_Type)].Value.ToString().Trim();

                if (strType == "Error")
                    Dgv_EventLog.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightPink;
                else if (strType == "Alarm")
                    Dgv_EventLog.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 128);
                else if (strType == "Info")
                    Dgv_EventLog.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                else if (strType == "Debug")
                    Dgv_EventLog.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;


            }
        }

        private void Dgv_EventLog_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex <0 || e.RowIndex<0 || Dgv_EventLog.Rows.Count <= 0) { return; }

            Dgv_EventLog.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = (Dgv_EventLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? string.Empty).ToString();

        }
        #region 語系切換,文字調整 Fun_LanguageIsEn_Font
        private void Fun_LanguageIsEn_Font14_12(object sender, EventArgs e)
        {
            FontStyle fs = ((System.Windows.Forms.CheckBox)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((System.Windows.Forms.CheckBox)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((System.Windows.Forms.CheckBox)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((System.Windows.Forms.CheckBox)sender).Font = new Font(ffm, (float)12, fs);
        }
        #endregion Fun_LanguageIsEn_Font End
    }
}

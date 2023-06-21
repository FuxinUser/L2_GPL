
using DBService.Repository.ScheduleDelete_CoilReject_Record;
using GPLManager.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class Frm_4_2_WeighingRecord : Form
    {
        DataTable dtGetRecord;
        //語系
        private LanguageHandler LanguageHand;
        DataTable dtLangSwitch_Column;

        public Frm_4_2_WeighingRecord()
        {
            InitializeComponent();
        }
        private void Frm_4_2_WeighingRecord_Load(object sender, EventArgs e)
        {
            if(PublicForms.Weighing == null)
                PublicForms.Weighing = this;

            //Control[] Frm_1_3_Control = new Control[] {

            //};
            //UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_1_3_Control, UserSetupHandler.Instance.Frm_1_3);

            //ComboBoxIndexHandler.Instance.SelectCoilList(Cob_Entry_Coil_No);

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }
        private void Frm_4_2_WeighingRecord_Shown(object sender, EventArgs e)
        {
            
            //ComboBoxIndexHandler.Instance.SelectCoilList(Cob_Entry_Coil_No);
            //Fun_InitComboBox_Cob_Remarks_Type();
            ////搜尋刪除記錄
            //try
            //{
            //    Fun_SelectDeleteRecord();
            //}
            //catch (Exception ex)
            //{
            //    EventLogHandler.Instance.EventPush_Message($"删除记录查询资料库失败:{ex}");
            //    EventLogHandler.Instance.LogDebug("1-3", $"删除记录", $"查询资料库失败:{ex}");
            //    PublicComm.ClientLog.Debug($"刪除記錄查詢失敗:{ex}");
            //    PublicComm.ExceptionLog.Debug($"刪除記錄查詢失敗:{ex}");
            //    return;
            //}
        }
                

        /// <summary>
        /// 搜寻删除记录
        /// </summary>
        /// <param name="bolCondition"></param>
        private void Fun_SelectDeleteRecord(bool bolCondition = false)
        {
            //string strSql = bolCondition ? SqlFactory.Frm_1_3_QuerrytDeleteRecord() : SqlFactory.Frm_1_3_SelectDeleteRecord();
            //dtGetRecord = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "删除排程记录", "1-3");

            //DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_DeleteSchedule, dtGetRecord);
            //DGVColumnsHandler.Instance.Frm_1_3_DeleteRecordColumns(Dgv_DeleteSchedule);
            //DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_DeleteSchedule);
        }
               
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            //if (Chk_Time.Checked)
            //{
            //    if (Dtp_Start_Time.DateTimeRangeIsFail(Dtp_Finish_Time.Value))
            //    {
            //        EventLogHandler.Instance.EventPush_Message($"日期区间起讫时间不正确，请重新确认");
            //        return;
            //    }
            //}

            ////搜尋刪除記錄
            //try
            //{
            //    Fun_SelectDeleteRecord(true);
            //}
            //catch (Exception ex)
            //{
            //    EventLogHandler.Instance.EventPush_Message($"删除记录查询资料库失败:{ex}");
            //    EventLogHandler.Instance.LogDebug("1-3", $"删除记录", $"查询资料库失败:{ex}");
            //    PublicComm.ClientLog.Debug($"刪除記錄查詢失敗:{ex}");
            //    PublicComm.ExceptionLog.Debug($"刪除記錄查詢失敗:{ex}");
            //    return;
            //}
        }        
        
        private void Fun_LanguageIsEn_Font14_12(object sender, EventArgs e)
        {
            FontStyle fs = ((CheckBox)sender).Font.Style;
            FontFamily ffm = ((CheckBox)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((CheckBox)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((CheckBox)sender).Font = new Font(ffm, (float)12, fs);
        }
    }
}

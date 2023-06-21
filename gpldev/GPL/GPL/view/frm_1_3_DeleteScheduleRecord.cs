
using DBService.Repository.ScheduleDelete_CoilReject_Record;
using GPLManager.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class Frm_1_3_DeleteScheduleRecord : Form
    {
        DataTable dtGetRecord;
        //語系
        private LanguageHandler LanguageHand;
        DataTable dtLangSwitch_Column;

        public Frm_1_3_DeleteScheduleRecord()
        {
            InitializeComponent();
        }
        private void Frm_1_3_DeleteScheduleRecord_Load(object sender, EventArgs e)
        {
            if(PublicForms.DeleteScheduleRecord == null)
                PublicForms.DeleteScheduleRecord = this;

            Control[] Frm_1_3_Control = new Control[] {
                Btn_Update
            };
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_1_3_Control, UserSetupHandler.Instance.Frm_1_3);

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }
        private void Frm_1_3_DeleteCoilRecord_Shown(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.ComboBox_DeleteCode(Cob_DeleteCode);
            ComboBoxIndexHandler.Instance.SelectDeleteUser(Cob_User);
            ComboBoxIndexHandler.Instance.SelectCoilList(Cob_Entry_Coil_No);
            Fun_InitComboBox_Cob_Remarks_Type();
            //搜尋刪除記錄
            try
            {
                Fun_SelectDeleteRecord();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"删除记录查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-3", $"删除记录", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"刪除記錄查詢失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"刪除記錄查詢失敗:{ex}");
                return;
            }
        }

        private void Fun_InitComboBox_Cob_Remarks_Type()
        {
            //ComboBox Data 
            Dictionary<string, string> dicLUT = new Dictionary<string, string>();
            dicLUT.Add("单卷删除", "单卷删除");//

            dicLUT.Add("整计划删除", "整计划删除");//

            dicLUT.Add("回退记录", "回退记录");//

            dicLUT.Add("已产出PDO", "已产出PDO");//

            DataTable dtLUT = Fun_DictionaryToDataTable(dicLUT);
            //Combobox取得资料 
            ComboBoxIndexHandler.Instance.Fun_CobDataFromTable(Cob_Remarks_Type, "SelectKey", "SelectShow", dtLUT, false, false);


        }

        public DataTable Fun_DictionaryToDataTable(Dictionary<string, string> dicAry)
        {
            DataTable dt = new DataTable();
            DataRow dr;

            // 建立欄位
            dt.Columns.Add("SelectKey", typeof(string));
            dt.Columns.Add("SelectShow", typeof(string));

            // 新增資料到DataTable
            foreach (KeyValuePair<string, string> item in dicAry)
            {
                dr = dt.NewRow();
                dr["SelectKey"] = item.Key;
                dr["SelectShow"] = item.Value;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 搜寻删除记录
        /// </summary>
        /// <param name="bolCondition"></param>
        private void Fun_SelectDeleteRecord(bool bolCondition = false)
        {
            string strSql = bolCondition ? SqlFactory.Frm_1_3_QuerrytDeleteRecord() : SqlFactory.Frm_1_3_SelectDeleteRecord();
            dtGetRecord = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "删除排程记录", "1-3");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_DeleteSchedule, dtGetRecord);
            DGVColumnsHandler.Instance.Frm_1_3_DeleteRecordColumns(Dgv_DeleteSchedule);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_DeleteSchedule);
        }            

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            if (Chk_Time.Checked)
            {
                if (Dtp_Start_Time.DateTimeRangeIsFail(Dtp_Finish_Time.Value))
                {
                    EventLogHandler.Instance.EventPush_Message($"日期区间起讫时间不正确，请重新确认");
                    return;
                }
            }

            //搜尋刪除記錄
            try
            {
                Fun_SelectDeleteRecord(true);
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"删除记录查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-3", $"删除记录", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"刪除記錄查詢失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"刪除記錄查詢失敗:{ex}");
                return;
            }
        }

        /// <summary>
        /// 删除原因
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_DeleteCode_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.ComboBox_DeleteCode(Cob_DeleteCode);
        }

        /// <summary>
        /// 钢卷号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Entry_Coil_No_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectCoilList(Cob_Entry_Coil_No);
        }

        /// <summary>
        /// 操作人员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_User_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectDeleteUser(Cob_User);
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

        #region  已停用功能
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dtGetRecord.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无记录可修改");
                return;
            }

            if (Dgv_DeleteSchedule.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无选取记录");
                return;
            }

            Btn_Save.Visible = Btn_Cancel.Visible = true;

            DataRow dr = dtGetRecord.Rows[Dgv_DeleteSchedule.CurrentRow.Index];

            Txt_Coil.Text = dr[nameof(ScheduleDelete_CoilReject_RecordEntity.L3L2_TBL_ScheduleDelete_CoilReject_Record.Coil_ID)].ToString() ?? string.Empty;
            Txt_Spare.Text = dr[nameof(ScheduleDelete_CoilReject_RecordEntity.L3L2_TBL_ScheduleDelete_CoilReject_Record.Remarks)].ToString() ?? string.Empty;
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Txt_Coil.Text = Txt_Spare.Text = string.Empty;
            Btn_Save.Visible = Btn_Cancel.Visible = false;
        }
        /// <summary>
        /// 修改备注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            Btn_Save.Visible = Btn_Cancel.Visible = false;

            string strSql = SqlFactory.Frm_1_3_UpdateSpare();
            try
            {
                Data_Access.GetInstance().ExecuteNonQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL);
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug("1-3", "修改删除排程记录备注", $"修改删除排程记录备注失败:{ex}");
                EventLogHandler.Instance.EventPush_Message($"备注修改失败:{ex}");
                PublicComm.ClientLog.Debug($"修改備註失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"修改備註失敗:{ex}");
                return;
            }
            EventLogHandler.Instance.LogInfo("1-3", "修改删除排程记录备注", $"修改删除排程记录备注成功");

            EventLogHandler.Instance.EventPush_Message($"备注修改成功!");


            //搜尋刪除記錄
            try
            {
                Fun_SelectDeleteRecord();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"删除记录查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-3", $"删除记录", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"刪除記錄查詢失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"刪除記錄查詢失敗:{ex}");
                return;
            }
        }
        #endregion 
    }
}


using Akka.Actor;
using DataModel.HMIServerCom.Msg;
using DBService.Repository.DelayLocation;
using DBService.Repository.DelayReasonCode;
using DBService.Repository.FaultCode;
using DBService.Repository.LineFaultRecords;
using GPLManager.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class Frm_4_1_LineDelayRecord : Form
    {
        DataTable dtGetRecord;
        DataTable dtSelectOne;
        DataTable dtBeforEdit;

        bool bolEditStatus = false;
        // public DataRow drGetCurrentRow;

        //語系
        private LanguageHandler LanguageHand;
        enum TurnDelayFlag
        {
            Insert = 1,
            /// <summary>
            /// 修改 - 停机位置有异动才需发出Update告知Server
            /// </summary>
            Update = 3,
            Delete = 2
        }

        public Frm_4_1_LineDelayRecord()
        {
            InitializeComponent();
        }

        private void Frm_4_1_LineDelayRecord_Load(object sender, EventArgs e)
        {
            PublicForms.LineDelayRecord = this;
            Control[] Frm_4_1_Control = new Control[] {
                //Btn_Delete,
                Btn_MMS,
                Btn_Edit,
                Btn_New,
                Btn_Save
            };//Btn_Cut,
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_4_1_Control, UserSetupHandler.Instance.Frm_4_4);

            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_DataCol, true);

            Nud_Stop_Time.Value = 3;

            Fun_GetDownTime();

            Fun_InitialComboBox();

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        private void Frm_4_1_LineDelayRecord_Shown(object sender, EventArgs e)
        {          

        }

        private void Fun_InitialComboBox()
        {
            //停機位置
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Fun_SelectStopLocation(), Cob_delay_location);
            // Fun_SettingComboBox(Cob_delay_location, Fun_SelectStopLocation(), nameof(DelayLocationModel.TBL_DelayLocation_Definition.Delay_LocationCode), nameof(DelayLocationModel.TBL_DelayLocation_Definition.Delay_LocationName));
            //停機代碼
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Fun_SelectReasonCode(), Cob_delay_reason_code);
            //Fun_SettingComboBox(Cob_delay_reason_code, Fun_SelectReasonCode(), nameof(DelayReasonCodeModel.TBL_DelayReasonCode_Definition.Delay_ReasonCode), nameof(DelayReasonCodeModel.TBL_DelayReasonCode_Definition.Delay_ReasonName));

            //降速代碼
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Deceleration, Cob_deceleration_code);

            //故障代碼
            //Fun_SettingComboBox(Cob_FaultCode, Fun_SelectFaultCode(), nameof(FaultCodeModel.TBL_FaultCode.Fault_Code), nameof(FaultCodeModel.TBL_FaultCode.Fault_Description));
            //班次
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Shift, Cob_shift_no);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Shift, Cob_prod_shift_no);
            //班別
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Team, Cob_shift_group);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Team, Cob_prod_shift_group);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Fun_GetDownTime();

            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_DataCol, true);
        }

        /// <summary>
        /// 搜尋所有停復機紀錄
        /// </summary>
        public void Fun_GetDownTime()
        {
            if (Dtp_Start_Time.DateTimeRangeIsFail(Dtp_Finish_Time.Value))
            {
                EventLogHandler.Instance.EventPush_Message($"日期区间起讫时间不正确，请重新确认");
                return;
            }
            try
            {
                string strSql = SqlFactory.DBHandler_Select_DownTime_WithCondition();
                dtGetRecord = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "停复机记录", "4-1");
                dtSelectOne = dtGetRecord.Clone();
                dtBeforEdit = dtGetRecord.Clone();
            }
            catch (Exception ex)
            {

            }

            Fun_Dt_UploadMMS_ChangeShow(dtGetRecord, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS), 1);

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Info, dtGetRecord);
            DGVColumnsHandler.Instance.Frm_4_1_LineDelayRecord(Dgv_Info);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Info);

            if (dtGetRecord.IsNull())
            {
                //DialogHandler.Instance.Fun_DialogShowOk("查无停复机记录", "查询停复机记录", 0);
                if (UserSetupHandler.Instance.Frm_4_1.Equals("W"))
                    Btn_Edit.Enabled = false;
            }
            else
            {
                string GetEndTime = string.Empty;

                for (int Index = 0; Index < dtGetRecord.Rows.Count; Index++)
                {
                    GetEndTime = Dgv_Info.Rows[Index].Cells[5].Value.ToString();

                    if (GetEndTime.Equals("1970/1/1") || GetEndTime.Equals("1970/1/1 上午 12:00:00"))
                    {
                        Dgv_Info.Rows[Index].Cells[5].Value = string.Empty;
                        //Dgv_Info.Rows[Index].Cells[5].Style.BackColor = Color.Red;
                    }
                }
            }
            //檢查 是否有 正確的停機結束時間,如果沒有則不開放編輯
            if (Dgv_Info.CurrentRow != null)
            {
                string strStop_End_Time = Dgv_Info.CurrentRow.Cells[5].Value.ToString();
                if (UserSetupHandler.Instance.Frm_4_1.Equals("W"))
                    Btn_Edit.Enabled = Fun_CheckStop_End_Time_Null(strStop_End_Time);
            }
            else
            {
                if (UserSetupHandler.Instance.Frm_4_1.Equals("W"))
                    Btn_Edit.Enabled = false;
            }
        }

        //檢查 是否有 正確的停機結束時間,如果沒有則不開放編輯
        private bool Fun_CheckStop_End_Time_Null(string strStop_End_Time)
        {
            if (string.IsNullOrEmpty(strStop_End_Time) || strStop_End_Time.StartsWith("1970") || strStop_End_Time.StartsWith("0001") || strStop_End_Time.StartsWith("999"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_New_Click(object sender, EventArgs e)
        {
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);
            //删除
            Fun_SetBottonEnabled(Btn_Delete, false);
            //儲存
            Btn_Save.Visible = true;
            //取消
            Btn_Cancel.Visible = true; 

            Fun_Control(Grb_DataCol, true);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            //檢查 是否已是編輯狀態 
            if (bolEditStatus) { return; }

            #region 编辑前检查目前栏位上显示的資訊與资料库内是否相符

            if (dtSelectOne != null && dtSelectOne.Rows.Count > 0)
            {

                //檢查 是否有 正確的停機結束時間,如果沒有則不開放編輯
                string strStop_End_Time = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)); //dtSelectOne.Rows[0]["stop_end_time"].ToString();
                if (!Fun_CheckStop_End_Time_Null(strStop_End_Time))
                {
                    DialogHandler.Instance.Fun_DialogShowOk("无停机结束时间，不可编辑！", "提示资讯", null, 0);
                    return;
                }

                //检查是否已上传过, 已上传不可再修改 20220310Z
                string strUploadMMS = dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)].ToString();
                if (strUploadMMS.ToUpper() == "Y" || strUploadMMS == "已上传")
                {
                    DialogHandler.Instance.Fun_DialogShowOk("此记录已上传MMS，不可编辑！", "提示资讯", null, 0);
                    return;
                }

                //編輯前先檢查是否有這筆資料
                string strProd_time = Dtp_prod_time.Value.ToString("yyyy-MM-dd");//日期
                string strStart_time = Dtp_stop_start_time.Value.ToString("yyyy-MM-dd HH:mm:ss")+$".{Dtp_stop_start_time.Value.Millisecond.ToString().PadLeft(3,'0')}";//停機開始時間
                string strEnd_time = Dtp_stop_end_time.Value.ToString("yyyy-MM-dd HH:mm:ss") + $".{Dtp_stop_end_time.Value.Millisecond.ToString().PadLeft(3, '0')}";//停機結束時間          
                string strShift_no = Cob_prod_shift_no.SelectedIndex != -1 ? Cob_prod_shift_no.SelectedValue.ToString() : "";//班次
                string strShift_group = Cob_prod_shift_group.SelectedIndex != -1 ?Cob_prod_shift_group.SelectedValue.ToString():"";//班組
                
                string strFindSql = SqlFactory.DBHandler_Select_DownTime_Check( strProd_time,  strStart_time,  strEnd_time,  strShift_no,  strShift_group);

                DataTable dtCheckData = Data_Access.Fun_SelectDate(strFindSql, GlobalVariableHandler.Instance.strConn_GPL, "停机资料确认");

                if (dtCheckData == null || dtCheckData.Rows.Count == 0)
                {
                    DialogHandler.Instance.Fun_DialogShowOk("请重新选取要编辑的资料！", "提示资讯", null,0);
                    return;
                }
                else
                {
                    //Except()差集
                    var tempExcept = dtSelectOne.AsEnumerable().Except(dtCheckData.AsEnumerable(), DataRowComparer.Default);

                    //如果沒有差異,就繼續執行
                    if (tempExcept.Count() == 0)
                    {
                        // return;
                    }
                    else
                    {
                        dtSelectOne = dtCheckData.Copy();
                        Fun_SetSelectOne(dtSelectOne);

                        
                    }
                }
            }
            else
            {
                //MessageBox.Show("尚未選取資料！");
                DialogHandler.Instance.Fun_DialogShowOk("尚未选取资料！", "提示资讯", null, 0);
                return;
            }

            #endregion



            if (dtSelectOne != null && dtSelectOne.Rows.Count > 0)
            {
                //先備份之前顯示的資料
                dtBeforEdit = dtSelectOne.Copy();

                //Edit Sataus
                bolEditStatus = true;

                //编辑
                Fun_SetBottonEnabled(Btn_Edit, true);
                //儲存
                Btn_Save.Visible = true;
                //取消
                Btn_Cancel.Visible = true;
                //Control Enabled =  true
                Fun_Control(Grb_DataCol, true);

                if (Cob_prod_shift_group.SelectedIndex == -1) Cob_prod_shift_group.Enabled = true;
                if (Cob_prod_shift_no.SelectedIndex == -1) Cob_prod_shift_no.Enabled = true;

                //新增 Visible = false
                Fun_SetBottonEnabled(Btn_New, false);
                //刪除 Visible = false
                Fun_SetBottonEnabled(Btn_Delete, false);
            }
            else
            {
                //MessageBox.Show("尚未選取資料！");
                DialogHandler.Instance.Fun_DialogShowOk("尚未选取资料！", "提示资讯", null, 0);
                return;
            }
            ////檢查 是否已是編輯狀態 
            //if (bolEditStatus) { return; }
            ////新增
            //Fun_SetBottonEnabled(Btn_New, false);
            ////删除
            //Fun_SetBottonEnabled(Btn_Delete, false);
            ////儲存
            //Btn_Save.Visible = true;
            ////取消
            //Btn_Cancel.Visible = true; 

            //Fun_Control(Grb_DataCol, true);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            //新增
            Fun_SetBottonEnabled(Btn_New, false);
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);

            DialogResult actionResult = DialogHandler.Instance.Fun_DialogShowOkCancel($"確定要刪除停復機紀錄日期 [{Dtp_prod_time.Value:yyyy/MM/dd}] 的資料？", "刪除停復機紀錄", Properties.Resources.dialogQuestion, 1); //MessageBox.Show($"確定要刪除停復機紀錄日期 [{Dtp_prod_time.Value:yyyy/MM/dd}] 的資料？", "刪除停復機紀錄",
           // MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (actionResult == DialogResult.Cancel) return;

            string strSql = SqlFactory.Frm_4_1_DeleteDelayRecord();
            Data_Access.GetInstance().Fun_ExecuteQuery(strSql,GlobalVariableHandler.Instance.strConn_GPL,"删除停复机记录","4-1");

            Fun_TurnDelayMessage(TurnDelayFlag.Delete);

            EventLogHandler.Instance.EventPush_Message($"删除日期 [{Dtp_prod_time.Value:yyyy/MM/dd}]停复机记录成功!");
            EventLogHandler.Instance.LogInfo("3-4", $"使用者:{PublicForms.Main.lblLoginUser.Text }道次操作", $"删除日期 [{Dtp_prod_time.Value:yyyy/MM/dd}]停复机记录成功!");
            PublicComm.ClientLog.Info($"刪除日期[{Dtp_prod_time.Value:yyyy/MM/dd}]停復機記錄成功");

            Fun_GetDownTime();
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            //必填欄位            
            if (!Fun_IsColumnsEmpty()) { return; }

            string strSql = string.Empty;

            ////以免必填欄位未填
            //if (Cob_prod_shift_no.Text.IsEmpty() || Cob_prod_shift_group.Text.IsEmpty() || Cob_delay_location.Text.IsEmpty() || Cob_delay_reason_code.Text.IsEmpty())
            //{
            //    EventLogHandler.Instance.EventPush_Message($"栏位未填写完整，请再次确认");
            //    return;
            //}

            if (Btn_New.Enabled)
            {
                strSql = SqlFactory.Frm_4_1_InsertDelayRecord();
                if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "新增停复机记录", "4-1"))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"新增停复机记录失败", "新增停复机记录", null, 0);

                    return;
                }
                
                Fun_TurnDelayMessage(TurnDelayFlag.Insert);
            }
            else if (Btn_Edit.Enabled)
            {
                DataRow drGetEditRow = dtSelectOne.Rows[0];//

                strSql = SqlFactory.Frm_4_1_UpdateDelayRecord(drGetEditRow);
                if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "修改停复机记录", "4-1"))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"修改停复机记录失败", "修改停复机记录", null, 0);

                    return;
                }

                // 日期、班次、班別、開始時間、結束時間任一有異動，則傳送舊資料給Server
                if (!drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)].Equals(Dtp_prod_time.Value.ToString("yyyy-MM-dd")) ||
                    !drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no)].Equals(Cob_prod_shift_no.SelectedValue) ||
                    !drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group)].Equals(Cob_prod_shift_group.SelectedValue) ||
                    !drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)].Equals($"{Dtp_stop_start_time.Value:yyyy-MM-dd HH:mm:ss}") ||
                    !drGetEditRow[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)].Equals($"{Dtp_stop_end_time.Value:yyyy-MM-dd HH:mm:ss}"))
                {
                    Fun_TurnDelayMessage(TurnDelayFlag.Update);
                }
                ////日期、班次、班别、开始时间、结束时间现阶段不开放修改
                //// 日期、班次、班別、開始時間、結束時間任一有異動，則傳送舊資料給Server
                ////if (!drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.prod_time)].Equals(Dtp_prod_time.Value.ToString("yyyy-MM-dd")) ||
                ////    !drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.prod_shift_no)].Equals(Cob_prod_shift_no.SelectedValue) ||
                ////    !drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.prod_shift_group)].Equals(Cob_prod_shift_group.SelectedValue) ||
                ////    !drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.stop_start_time)].Equals($"{Dtp_stop_start_time_D.Value:yyyy-MM-dd HH:mm:ss}") ||
                ////    !drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.stop_end_time)].Equals($"{Dtp_stop_end_time_D.Value:yyyy-MM-dd HH:mm:ss}"))
                ////{
                ////    Fun_TurnDelayMessage(TurnDelayFlag.Upadte);
                ////}

                ////目前停机位置有异动才需发出修改Op_Flag
                //if (!drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.delay_location)].Equals(Cob_delay_location.SelectedValue))
                //{
                //    Fun_TurnDelayMessage(TurnDelayFlag.Upadte);
                //}
                //else
                //{
                //    Fun_TurnDelayMessage(TurnDelayFlag.Insert);
                //}

            }

            //Edit Sataus
            bolEditStatus = false;
            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);
            //儲存
            Btn_Save.Visible = false;
            //取消
            Btn_Cancel.Visible = false;

            //新增 Visible = false
            Fun_SetBottonEnabled(Btn_New, false);
            //刪除 Visible = false
            Fun_SetBottonEnabled(Btn_Delete, false);

            //Control Enabled =  false
            Fun_Control(Grb_DataCol, false);

            //儲存後,重新查詢資料
            Fun_GetDownTime();
            //查詢後,找到儲存的該筆資料
            Fun_SelectedBackRow(Dgv_Info);
        }

        /// <summary>
        /// 通知Server停復機紀錄異動
        /// </summary>
        /// <param name="flag"></param>
        private void Fun_TurnDelayMessage(TurnDelayFlag flag)
        {
            SCCommMsg.CS09_LineFaultData Msg = new SCCommMsg.CS09_LineFaultData
            {
                Op_Flag = ((int)flag).ToString(),
                prod_Shift_no = Cob_prod_shift_no.SelectedValue.ToString(),
                stop_start_time = Dtp_stop_start_time.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                stop_end_time = Dtp_stop_end_time.Value.ToString("yyyy-MM-dd HH:mm:ss")
            };

            //PublicComm.client.Tell(Msg);

            string str = (int)flag == (int)TurnDelayFlag.Insert ? "新增" : (int)flag == (int)TurnDelayFlag.Update ? "修改" : "刪除";

            EventLogHandler.Instance.LogInfo("4-1", $"使用者:{PublicForms.Main.lblLoginUser.Text}{str}停复机记录", $"{str}[{Dtp_prod_time.Value:yyyy-MM-dd}]停复机记录");

            //EventLogHandler.Instance.LogInfo( "4-1", $"使用者:{PublicForms.Main.lblLoginUser.Text}修改停复机记录", $"修改[{Dtp_prod_time.Value:yyyy-MM-dd}]停复机记录");
            //EventLogHandler.Instance.EventPush_Message($"已通知Server修改[{Dtp_prod_time.Value:yyyy-MM-dd}]记录");
            //PublicComm.ClientLog.Info($"已通知Server修改[{Dtp_prod_time.Value:yyyy/MM/dd}]停復機記錄");
            //PublicComm.akkaLog.Info($"已通知Server修改[{Dtp_prod_time.Value:yyyy/MM/dd}]停復機記錄");
        }


        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (dtBeforEdit != null && dtBeforEdit.Rows.Count != 0)
            {
                Fun_SetSelectOne(dtBeforEdit);
                //修改
                Fun_SetBottonEnabled(Btn_Edit, true);

            }
            else
            {
                Fun_ControlClear();
                //修改
                Fun_SetBottonEnabled(Btn_Edit, false);
            }
            //紀錄是否編輯,狀態
            bolEditStatus = false;
            //儲存
            Btn_Save.Visible = false;
            //取消
            Btn_Cancel.Visible = false;
            //Control Enabled =  false
            Fun_Control(Grb_DataCol, false);

            //新增 Visible = false
            Fun_SetBottonEnabled(Btn_New, false);
            //刪除 Visible = false
            Fun_SetBottonEnabled(Btn_Delete, false);

            ////新增
            //Fun_SetBottonEnabled(Btn_New, true);
            ////修改
            //Fun_SetBottonEnabled(Btn_Edit, true);
            ////删除
            //Fun_SetBottonEnabled(Btn_Delete, true);
            ////儲存
            //Btn_Save.Visible = false;
            ////取消
            //Btn_Cancel.Visible = false;

            //Fun_Control(Grb_DataCol, false);
        }

        private void Fun_SelectedBackRow(DataGridView dgv)
        {
            string[] strFindKey = new string[] { nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time) ,
                                                 nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no) ,
                                                 nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group) ,
                                                 nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time) ,
                                                 nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)
                                               };

            int intIndex = -1;

            //foreach (DataGridViewRow dgvr in dgv.Rows)
            //{
            //    if ( dgvr.Cells[strFindKey[0]].Value.ToString().Equals(drArr[0][strFindKey[0]].ToString())&
            //         dgvr.Cells[strFindKey[1]].Value.ToString().Equals(drArr[0][strFindKey[1]].ToString())&
            //         dgvr.Cells[strFindKey[2]].Value.ToString().Equals(drArr[0][strFindKey[2]].ToString())&
            //         dgvr.Cells[strFindKey[3]].Value.ToString().Equals(drArr[0][strFindKey[3]].ToString())&
            //         dgvr.Cells[strFindKey[4]].Value.ToString().Equals(drArr[0][strFindKey[4]].ToString()))
            //        intIndex = dgvr.Index;
            //}

            try
            {
                DataGridViewRow row = dgv.Rows.Cast<DataGridViewRow>().Where(r =>
                          Convert.ToDateTime(r.Cells[strFindKey[0]].Value.ToString()).ToString("yyyy-MM-dd").Equals(Dtp_prod_time.Value.ToString("yyyy-MM-dd"))
                        & r.Cells[strFindKey[1]].Value.ToString().Equals(Cob_prod_shift_no.SelectedValue)
                        & r.Cells[strFindKey[2]].Value.ToString().Equals(Cob_prod_shift_group.SelectedValue)
                        & Convert.ToDateTime(r.Cells[strFindKey[3]].Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss").Equals(Dtp_stop_start_time.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                        & Convert.ToDateTime(r.Cells[strFindKey[4]].Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss").Equals(Dtp_stop_end_time.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                        ).First();
                intIndex = row.Index;
            }
            catch (Exception ee)
            {

            }

            if (intIndex > -1)
            {
                dgv.Rows[intIndex].Selected = true;
                dgv.Rows[intIndex].Cells[0].Selected = true;
            }
            else
                dgv.ClearSelection();
        }

        /// <summary>
        /// 控制編輯區塊
        /// </summary>
        /// <param name="Group"></param>
        /// <param name="bolControl"></param>
        private void Fun_Control(GroupBox Group, bool bolControl)
        {
            Control[] ctrContainer = { Group };
            foreach (Control container in ctrContainer)
            {
                Fun_FindControlReadOnly(container, bolControl);
                foreach (Control ctrl in container.Controls)
                {
                    Fun_FindControlReadOnly(ctrl, bolControl);
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        Fun_FindControlReadOnly(ctrl2, bolControl);
                    }
                }
            }
        }

        /// <summary>
        /// 编辑区ReadOnly控制
        /// </summary>
        /// <param name="container"></param>
        /// <param name="bolControl"></param>
        private void Fun_FindControlReadOnly(Control container, bool bolControl)
        {
            if (container.Controls.Count.Equals(0)) return;

            foreach (Control Ctl1 in container.Controls)
            {
                if (Ctl1 is TextBox)
                {
                    TextBox txtBox = Ctl1 as TextBox;

                    if (txtBox.Name.Equals(Txt_Stop_Elased_Time.Name) || 
                        txtBox.Name.Equals(Txt_delay_location_desc.Name) ||
                        txtBox.Name.Equals(Txt_delay_reason_desc.Name) )
                    {
                        txtBox.Enabled = false;
                    }
                    else
                    {
                        txtBox.Enabled = bolControl;
                    }
                }
                else if (Ctl1 is DateTimePicker)
                {
                    DateTimePicker dtPicker = Ctl1 as DateTimePicker;

                    if (dtPicker.Name.Equals(Dtp_stop_start_time.Name) ||
                        dtPicker.Name.Equals(Dtp_stop_end_time.Name) ||
                        dtPicker.Name.Equals(Dtp_prod_time.Name)
                        )
                    {
                        dtPicker.Enabled = false;
                    }
                    else
                    { 
                        dtPicker.Enabled = bolControl;
                    }
                }
                else if (Ctl1 is ComboBox)
                {
                    ComboBox cbo = Ctl1 as ComboBox;

                    if (cbo.Name.Equals(Cob_prod_shift_no.Name) ||
                        cbo.Name.Equals(Cob_prod_shift_group.Name)
                        )
                    {
                        cbo.Enabled = false;
                    }
                    else
                    {
                        cbo.Enabled = bolControl;
                    }
                }
            }
        }

        private void Fun_ControlClear()
        {
            Cob_prod_shift_no.SelectedIndex = -1;//班次
            Cob_prod_shift_group.SelectedIndex = -1;//班組
            Cob_delay_location.SelectedIndex = -1;//停機位置
            Cob_delay_reason_code.SelectedIndex = -1;//停機原因
            Cob_deceleration_code.SelectedIndex = -1;//降速原因          
                                                     //Cob_unit_code.SelectedIndex = -1;//機組號 基本上是固定不變

            //DateTimePicker 
            Dtp_prod_time.Value = DateTime.Now;
            Dtp_stop_start_time.Value = DateTime.Now;
            Dtp_stop_end_time.Value = DateTime.Now;

            string strClear = string.Empty;
            //停機持續時間(min)
            Txt_Stop_Elased_Time.Text = strClear;
            //停機位置描述
            Txt_delay_location_desc.Text = strClear;
            //停機原因描述
            Txt_delay_reason_desc.Text = strClear;
            //機械部門原因停機時間(min)
            Txt_Resp_Depart_Delay_Time_m.Text = strClear;
            //電氣部門原因停機時間(min)         
            Txt_Resp_Depart_Delay_Time_e.Text = strClear;
            //L3原因停機時間(min)                                                                              
            Txt_Resp_Depart_Delay_Time_c.Text = strClear;
            //生產部門原因停機時間(min)                                                                          
            Txt_Resp_Depart_Delay_Time_p.Text = strClear;
            //正常停機時間(min)                                                                                  
            Txt_Resp_Depart_Delay_Time_u.Text = strClear;
            //其它部門原因停機時間(min)                                                                          
            Txt_Resp_Depart_Delay_Time_o.Text = strClear;
            //換輥原因停機時間(min)                                                                              
            Txt_Resp_Depart_Delay_Time_r.Text = strClear;
            //磨輥原因停機時間(min)            
            Txt_Resp_Depart_Delay_Time_rs.Text = strClear;
        }

        /// <summary>
        /// 停机事件DataGridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Info_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }//列标题不响应CellClick事件
            if (Dgv_Info.Rows.Count.Equals(0)) { return; }
            if (Dgv_Info.CurrentRow == null) { return; }
            if (bolEditStatus) { return; }

            // if (dtGetRecord.IsNull()) return;

            //DataRow drGetCurrentRow = dtGetRecord.Rows[Dgv_Info.CurrentRow.Index];
            DataRow dr = Fun_GetDataRowFromCurrentRow(Dgv_Info, dtGetRecord);
            DataTable dt = dtGetRecord.Clone();
            try
            {
                dt.LoadDataRow(dr.ItemArray, false);
            }
            catch { return; }

            Fun_SetSelectOne(dt);

            //檢查 是否有 正確的停機結束時間,如果沒有則不開放編輯
            string strStop_End_Time = Dgv_Info.CurrentRow.Cells[5].Value.ToString();
            if (UserSetupHandler.Instance.Frm_4_1.Equals("W"))
                Btn_Edit.Enabled = Fun_CheckStop_End_Time_Null(strStop_End_Time);

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

        private void Fun_SetSelectOne(DataTable dt)
        {
            if (dt != null && dt.Rows.Count != 0)
            {
                dtSelectOne = dt.Copy();

                #region [Delay Data Display]

                ////取得资料行
                //drGetCurrentRow = dtGetRecord.Rows[Dgv_Info.CurrentRow.Index];

                //機組號
                Txt_UnitCode.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.unit_code));//drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.unit_code)].ToString() ?? string.Empty;

                //日期
                Dtp_prod_time.Value = DateTime.Parse(Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)));//DateTime.Parse(drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.prod_time)].ToString());

                //班次
                Cob_prod_shift_no.SelectedValue = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_no));//drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.prod_shift_no)].ToString().Trim() ?? string.Empty;

                //班別
                Cob_prod_shift_group.SelectedValue = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_shift_group));//drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.prod_shift_group)].ToString().Trim() ?? string.Empty;

                //停機位置
                Cob_delay_location.SelectedValue = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location));
                //Cob_delay_location.SelectedIndex =Cob_delay_location.FindString(drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.delay_location)].ToString());

                //停機代碼
                Cob_delay_reason_code.SelectedValue = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code));
                //Cob_delay_reason_code.SelectedIndex = Cob_delay_reason_code.FindString(drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.delay_reason_code)].ToString());

                //降速代碼
                Cob_deceleration_code.SelectedValue = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.deceleration_code));

                //故障代碼
                //Cob_FaultCode.SelectedIndex = Cob_FaultCode.FindString(drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.unit_code)].ToString());

                //停機備註
                Txt_Remark.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_remark));//drGetCurrentRow[nameof(LineFaultRecordsModel.TBL_LineFaultRecords.delay_remark)].ToString() ?? string.Empty;

                //停機開始時間
                if (!Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)).IsEmpty() ||
                    !Dgv_Info.CurrentRow.Cells[4].Value.ToString().IsEmpty())
                {
                    Dtp_stop_start_time.Value = DateTime.Parse(Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)));
                }

                //停機結束時間
                if (!Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)).IsEmpty() &&
                    !Dgv_Info.CurrentRow.Cells[5].Value.ToString().IsEmpty())
                {
                    string GetEndTime = Dgv_Info.CurrentRow.Cells[5].Value.ToString();

                    if (GetEndTime.Equals("0001/1/1") || GetEndTime.Equals("0001/1/1 上午 12:00:00"))
                    {
                        Dtp_stop_end_time.Value = DateTimePicker.MaximumDateTime;//DateTime.Parse("1970/1/1 上午 12:00:00"); 
                    }
                    else
                    {
                        Dtp_stop_end_time.Value = DateTime.Parse(Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)));

                    }
                }
                else
                {
                    Dtp_stop_end_time.Value = DateTimePicker.MaximumDateTime;//DateTime.Parse("1970/1/1 上午 12:00:00"); 
                }

                //停機持續時間(min)
                Txt_Stop_Elased_Time.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_elased_timey));// dtSelectOne.Rows[0][].ToString();
                //停機位置描述
                Txt_delay_location_desc.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location_desc));// dtSelectOne.Rows[0][].ToString();
                //停機原因描述
                Txt_delay_reason_desc.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_desc));// dtSelectOne.Rows[0][].ToString();
                //機械部門原因停機時間(min)
                Txt_Resp_Depart_Delay_Time_m.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_m));// dtSelectOne.Rows[0][].ToString();
                //電氣部門原因停機時間(min)         
                Txt_Resp_Depart_Delay_Time_e.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_e));// dtSelectOne.Rows[0][].ToString();
                //L3原因停機時間(min)                                                                              
                Txt_Resp_Depart_Delay_Time_c.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_c));// dtSelectOne.Rows[0][].ToString();
                //生產部門原因停機時間(min)                                                                          
                Txt_Resp_Depart_Delay_Time_p.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_p));// dtSelectOne.Rows[0][].ToString();
                //正常停機時間(min)                                                                                  
                Txt_Resp_Depart_Delay_Time_u.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_u));// dtSelectOne.Rows[0][].ToString();
                //其它部門原因停機時間(min)                                                                          
                Txt_Resp_Depart_Delay_Time_o.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_o));// dtSelectOne.Rows[0][].ToString();
                //換輥原因停機時間(min)                                                                              
                Txt_Resp_Depart_Delay_Time_r.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_r));// dtSelectOne.Rows[0][].ToString();
                //磨輥原因停機時間(min)            
                Txt_Resp_Depart_Delay_Time_rs.Text = Fun_TryDtGetString(dtSelectOne, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.resp_depart_delay_time_rs));// dtSelectOne.Rows[0][].ToString();

                #endregion
            }
            else
            {
                Fun_ControlClear();
            }
        }
        /// <summary>
        /// 停機位置選項
        /// </summary>
        private DataTable Fun_SelectStopLocation()
        {
            string strSql = SqlFactory.Frm_4_1_SelectDelayLocation();
            
            DataTable dtGetLocation = Data_Access.Fun_SelectDate(strSql,GlobalVariableHandler.Instance.strConn_GPL,"停机位置","4-1");
            
            return dtGetLocation;
        }

        /// <summary>
        /// 停機原因代碼選項
        /// </summary>
        /// <returns></returns>
        private DataTable Fun_SelectReasonCode()
        {
            string strSql = SqlFactory.Frm_4_1_SelectDelayReasonCode();
            
            DataTable dtGetReason = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "停机原因代码", "4-1");
            
            return dtGetReason;
        }

        /// <summary>
        /// 故障代碼選項
        /// </summary>
        /// <returns></returns>
        private DataTable Fun_SelectFaultCode()
        {
            string strSql = SqlFactory.Frm_4_1_SelectFaultCode();
            
            DataTable dtGetReason = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "故障代码", "4-1");
            
            return dtGetReason;
        }

        /// <summary>
        /// 設定ComboBox
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="dt"></param>
        /// <param name="strValue"></param>
        /// <param name="strDisplay"></param>
        private void Fun_SettingComboBox(ComboBox cbo, DataTable dt, string strValue, string strDisplay)
        {
            if (dt.IsNull()) return;

            //cbo.DisplayMember = strValue;
            //cbo.ValueMember = strValue;
            //cbo.DataSource = dt;
            //cbo.SelectedValue = string.Empty;


            foreach (DataRow Row in dt.Rows)
            {
                cbo.Items.Add($"{Row[strValue]}-{Row[strDisplay]}");
            }
        }

        /// <summary>
        /// 停机开始时间值变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dtp_stop_start_time_D_ValueChanged(object sender, EventArgs e)
        {
            //檢查 是否已是編輯狀態 , 不是编辑模式不判断
            if (!bolEditStatus) { return; }

            if (Dtp_stop_start_time.DateTimeRangeIsFail(Dtp_stop_end_time.Value))
            {
                EventLogHandler.Instance.EventPush_Message("开始结束时间区间错误，请重新确认!");
                return;
            }

            //计算分钟数
            double GetMin = (DateTime.Parse(Dtp_stop_end_time.Value.ToString("yyyy-MM-dd HH:mm")) - DateTime.Parse(Dtp_stop_start_time.Value.ToString("yyyy-MM-dd HH:mm"))).TotalMinutes;
            
            Txt_Stop_Elased_Time.Text = GetMin.ToString();
        }

        /// <summary>
        /// 停机结束时间值变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dtp_stop_end_time_D_ValueChanged(object sender, EventArgs e)
        {
            //檢查 是否已是編輯狀態 , 不是编辑模式不判断
            if (!bolEditStatus) { return; }

            if (Dtp_stop_start_time.DateTimeRangeIsFail(Dtp_stop_end_time.Value))
            {
                EventLogHandler.Instance.EventPush_Message("开始结束时间区间错误，请重新确认!");
                return;
            }

            //计算分钟数
            double GetMin = (DateTime.Parse(Dtp_stop_end_time.Value.ToString("yyyy-MM-dd HH:mm")) - DateTime.Parse(Dtp_stop_start_time.Value.ToString("yyyy-MM-dd HH:mm"))).TotalMinutes;
            
            Txt_Stop_Elased_Time.Text = GetMin.ToString();
        }

        //設定按鈕啟用狀態並改顏色
        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            //Color colorBack;
            btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
        }

        private string Fun_TryDtGetString(DataTable dt, string strCol)
        {
            string strGet = "";
            if (dtSelectOne != null && dtSelectOne.Rows.Count > 0)
            {
                try
                {
                    strGet = dt.Rows[0][strCol].ToString().Trim();
                }
                catch (ArgumentException ae)
                {
                    DialogHandler.Instance.Fun_DialogShowOk(ae.Message, "错误资讯", null, 3);
                    strGet = "";
                }
            }

            return strGet;
        }

        private string Fun_TryDgvGetString(DataGridView dgv, int intRows, string strCol)
        {
            string strGet = "";
            try
            {
                strGet = dgv.Rows[intRows].Cells[strCol].Value.ToString().Trim();
                //Dgv_Info.Rows[e.RowIndex].Cells[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)].Value.ToString().Trim();
            }
            catch (ArgumentException ae)
            {
                DialogHandler.Instance.Fun_DialogShowOk(ae.Message, "错误资讯", null, 3);
                strGet = "";
            }
            return strGet;
        }

        //檢查必填欄位是否空白
        private bool Fun_IsColumnsEmpty()
        {
            //if (Cob_prod_shift_no.Text.IsEmpty() )
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_prod_shift_no_Title.Text} 栏位请勿空白，请再次确认", "提示资讯", 0);
            //    Cob_prod_shift_no.Focus();
            //    return false;
            //}
            //if (Cob_prod_shift_group.Text.IsEmpty() )
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_prod_shift_group_Title.Text} 栏位请勿空白，请再次确认", "提示资讯", 0);
            //    Cob_prod_shift_group.Focus();
            //    return false;
            //}
            if (Cob_delay_location.Text.IsEmpty())
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_delay_location_Title.Text} 栏位请勿空白，请再次确认", "提示资讯", null, 0);
                Cob_delay_location.Focus();
                return false;
            }
            if (Cob_delay_reason_code.Text.IsEmpty())
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_delay_reason_code_Title.Text} 栏位请勿空白，请再次确认", "提示资讯", null, 0);
                Cob_delay_reason_code.Focus();
                return false;
            }
            if (Cob_deceleration_code.Text.IsEmpty())
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_deceleration_code_Title.Text} 栏位请勿空白，请再次确认", "提示资讯", null, 0);
                Cob_delay_reason_code.Focus();
                return false;
            }

            if (Dtp_stop_start_time.DateTimeRangeIsFail(Dtp_stop_end_time.Value))
            {
               // EventLogHandler.Instance.EventPush_Message("开始结束时间区间错误，请重新确认!");
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_stop_end_time_Title.Text} 与 {Lbl_stop_end_time_Title.Text} 区间错误，请重新确认!", "提示资讯", null, 0);
                Dtp_stop_start_time.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// UploadMMS文字转换显示
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="strColumns">UploadMMS</param>
        /// <param name="intFormat">1:str = "Y"; 2:str = "1";</param>
        /// <returns></returns>
        private DataTable Fun_Dt_UploadMMS_ChangeShow(DataTable dt, string strColumns, int intFormat)
        {
            string strOldShow = "";
            string strNewShow = "";
            switch (intFormat)
            {
                case 1:
                    strOldShow = "Y";
                    strNewShow = "已上传";
                    break;
                case 2:
                    strOldShow = "1";
                    strNewShow = "已上传";
                    break;

                default:
                    break;
            }

            foreach (DataRow dr in dt.Rows)
                foreach (DataColumn dc in dt.Columns)
                    if (dc.ToString() == strColumns)
                    {
                        if (dr[dc].ToString() == strOldShow)
                        {
                            try
                            {
                                dr[dc] = strNewShow;
                            }
                            catch (Exception ee)
                            {
                                dr[dc] = dr[dc].ToString();
                            }
                        }
                        else
                        {
                            dr[dc] = "未上传";
                        }
                    }

            return dt;
        }

        private void Dgv_Info_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (dtGetRecord.IsNull())
                return;
            if (Dgv_Info.DgvIsNull())
                return;

            if (Dgv_Info.Rows.Count != 0)
            {
                string strUploadMMS = Fun_TryDgvGetString(Dgv_Info, e.RowIndex, nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS));

                if (strUploadMMS != "已上传")
                {
                    Dgv_Info.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 128);
                }

            }

        }

        private void Btn_MMS_Click(object sender, EventArgs e)
        {
            if (bolEditStatus == true)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再上传", "上传MMS", null,0);
                return;
            }
            if (Dgv_Info.Rows.Count.Equals(0))
            {
                DialogHandler.Instance.Fun_DialogShowOk("无可上传的停复机记录", "上传MMS", null, 0);
                return;
            }
            if (Dgv_Info.CurrentRow == null)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选取要上传的停复机记录", "上传MMS", null, 0);
                return;
            }
            if (Dgv_Info.SelectedRows == null || Dgv_Info.SelectedRows.Count <= 0)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选取要上传的停复机记录", "上传MMS", null, 0);
                return;
            }

            if (Cob_prod_shift_group.SelectedIndex == -1) 
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_prod_shift_group_Title.Text}为空白,不可上传!", "上传MMS", null, 0);
                return;
            }
            if (Cob_prod_shift_no.SelectedIndex == -1)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_prod_shift_no_Title.Text}为空白,不可上传!", "上传MMS", null, 0);
                return;
            }
           
            if (dtSelectOne == null || dtSelectOne.Rows.Count <= 0) return;

            //  檢查停機位置(Cob_delay_location)
            if (string.IsNullOrEmpty(dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_location)].ToString().Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"停机位置不可空白，资料不完整，不可上传！", "上传MMS", null, 0);
                return;
            }
            //  檢查停机原因代码
            if (string.IsNullOrEmpty(dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.delay_reason_code)].ToString().Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"停机原因代码不可空白，资料不完整，不可上传！", "上传MMS", null, 0);
                return;
            }
            

            string str = "上传";

            StringBuilder sbShow = new StringBuilder();
            sbShow.AppendLine($"请确定是否要上传停复机记录?");
            sbShow.AppendLine($"{Lbl_prod_time_Title.Text}:{Dtp_prod_time.Value.ToString("yyyy-MM-dd")}");
            sbShow.AppendLine($"{Lbl_stop_start_time_Title.Text}:{Dtp_stop_start_time.Value.ToString("yyyy-MM-dd HH:mm:ss")}.{Dtp_stop_start_time.Value.Millisecond}");
            sbShow.AppendLine($"{Lbl_stop_end_time_Title.Text}:{Dtp_stop_end_time.Value.ToString("yyyy-MM-dd HH:mm:ss")}.{Dtp_stop_end_time.Value.Millisecond}");
            string strMessage = sbShow.ToString();//PDO只能上传一次，
            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, $"{str}停复机记录", Properties.Resources.dialogQuestion, 1);

            if (dialogR.Equals(DialogResult.OK))
            {
                if (dtSelectOne != null && dtSelectOne.Rows.Count > 0)
                {
                    string str_stop_start_time = dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)].ToString();
                    string str_stop_end_time = dtSelectOne.Rows[0][nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_end_time)].ToString();
                    SCCommMsg.CS09_LineFaultData Msg = new SCCommMsg.CS09_LineFaultData
                    {
                        prod_Shift_no = Cob_prod_shift_no.SelectedValue.ToString(),
                        stop_start_time = str_stop_start_time,//Dtp_stop_start_time.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                        stop_end_time = str_stop_end_time,// Dtp_stop_end_time.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                        prod_time = Dtp_prod_time.Value.ToString("yyyy-MM-dd")
                    };
                    PublicComm.Client.Tell(Msg);

                    EventLogHandler.Instance.LogInfo("4-1", $"使用者:{PublicForms.Main.lblLoginUser.Text}{str}停复机记录", $"{str}[{Dtp_prod_time.Value:yyyy-MM-dd}]停复机记录");

                    DialogHandler.Instance.Fun_DialogShowOk($"已通知Server{str}[{Dtp_prod_time.Value:yyyy-MM-dd}]记录", $"{str}停复机记录",null, 4);

                    PublicComm.ClientLog.Info($"已通知Server{str}[{Dtp_prod_time.Value:yyyy/MM/dd}]停復機記錄");
                    PublicComm.akkaLog.Info($"已通知Server{str}[{Dtp_prod_time.Value:yyyy/MM/dd}]停復機記錄");
                }
            }

        }

        private void Chk_SendMMS_CheckedChanged(object sender, EventArgs e)
        {
            bool Check = Chk_SendMMS.Checked;

            Rdb_SendMMS.Enabled = Rdb_UnSendMMS.Enabled = Check;
        }

        private void Btn_Export_Click(object sender, EventArgs e)
        {
            if (Dgv_Info == null || Dgv_Info.Rows.Count <= 0) return;

           
            string strSampleFile = "../../SampleFile/Export_EXCEL.xlsx";//D:\K\GitHub_GPL_HMI\gpldev\GPL\GPL\SampleFile\
            string strFilePath = "Excel/Export_EXCEL.xlsx";
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
                PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ffex.Message}");
                PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ffex.Message}");
                return;
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk(ex.Message, $"讯息提示", null, 3);
                PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ex.Message}");
                PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ex.Message}");
                return;
            }

            //以範本档案建立新文件
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel Sheet|*.xlsx";
            dialog.FileName = "停复机记录Excel文件" + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".xlsx";
            dialog.Title = "汇出Excel文件";

            if (dialog.ShowDialog() == DialogResult.OK)
            {               
                try
                {                    
                    if (File.Exists(dialog.FileName))
                        File.Delete(dialog.FileName);

                    using (FileStream fileStream = File.Open(dialog.FileName, FileMode.Create, FileAccess.Write))
                    {
                        //為了 浮水印，改為讀取內建範本
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

                            //XSSFDrawing drawing = (XSSFDrawing)sheet.CreateDrawingPatriarch();
                            //IClientAnchor anchor1 = drawing.CreateAnchor(0, 0, 0, 0, left1, top1, right1, bottom1);

                            //XSSFTextBox textbox1 = drawing.CreateTextbox(anchor1);
                            //XSSFRichTextString xSSFRichTextString = new XSSFRichTextString();
                            //XSSFFont watermark = (XSSFFont)workbook.CreateFont();
                            //watermark.FontName = "宋体";
                            //watermark.FontHeightInPoints = 108;

                            //xSSFRichTextString.Append("福建福欣特殊钢     TLL", watermark);
                            //xSSFRichTextString.
                            //textbox1.SetText("福建福欣特殊钢     TLL");


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
                            if (Dgv_Info != null && Dgv_Info.Rows.Count > 0)
                            {
                                int rowCount = dtGetRecord.Rows.Count;// downTimeTable.Count;//行數
                                var properties = dtGetRecord.Columns.Count;// downTimeTable.ElementAt(0).GetType().GetProperties();
                                int columnCount = properties;//.Count() - 2;//列數  扣掉error 與 item

                                //設定列頭
                                IRow row = sheet.CreateRow(0);//excel第一行設為列頭
                                try
                                {
                                    for (int c = 0; c < columnCount - 1; c++)
                                    {
                                        ////取得 物建display text屬性
                                        //MemberInfo property = typeof(TBL_DownTime.TBL_DownTimeMD).GetProperty(properties.ElementAt(c).Name);
                                        //string displayColumnName = property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName.Trim();

                                        string displayColumnName = Dgv_Info.Columns[c].HeaderText;

                                        NPOI.SS.UserModel.ICell cell = row.CreateCell(c);
                                        cell.SetCellValue(displayColumnName);
                                        cell.CellStyle = cs_head;
                                        
                                        int length = cell.ToString().Length;

                                        if (c == 2 || c == 5 || c == 6)
                                        {
                                            sheet.SetColumnWidth(c, (length + 2) * 1000);
                                        }
                                        else
                                        {
                                            sheet.SetColumnWidth(c, length * 1000);
                                        }                                        
                                    }
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
                                    var isUpload = Dgv_Info.Rows[i].Cells[nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.UploadMMS)].Value.ToString();// downTimeTable.ElementAt(i).IsUpload;
                                    bool isSendMMS = isUpload == "未上传" ? true : false;

                                    row = sheet.CreateRow(i + 1);
                                    for (int j = 0; j < columnCount-1; j++)
                                    {
                                        NPOI.SS.UserModel.ICell cell = row.CreateCell(j);//excel第二行開始寫入資料      

                                        var value = Dgv_Info.Rows[i].Cells[j].Value.ToString();
                                        //var value = downTimeTable.ElementAt(i).GetType().GetProperties()[j].GetValue(downTimeTable.ElementAt(i));

                                        if (value == null)
                                        {
                                            cell.SetCellValue("");
                                        }
                                        else
                                        {
                                            if (j == 2)
                                            {           
                                                value = DateTime.TryParse(Dgv_Info.Rows[i].Cells[j].Value.ToString(), out DateTime dateTime) ? dateTime.ToString("yyyy/MM/dd") : Dgv_Info.Rows[i].Cells[j].Value.ToString();       
                                            }
                                            if (j == 5 || j == 6)
                                            {
                                                value = DateTime.TryParse(Dgv_Info.Rows[i].Cells[j].Value.ToString(), out DateTime dateTime) ? dateTime.ToString("yyyy/MM/dd HH:mm:ss.fff") : Dgv_Info.Rows[i].Cells[j].Value.ToString();
                                            }
                                                //時間轉換格式
                                                cell.SetCellValue(value.ToString().Trim());
                                        }

                                        if (isSendMMS)
                                        {
                                            cell.CellStyle = cs_mms;
                                        }
                                        else
                                        {
                                            cell.CellStyle = cs;
                                        }
                                    }
                                 
                                }
                            }

                            workbook.Write(fileStream);
                        }
                        catch (Exception ex)
                        {
                            DialogHandler.Instance.Fun_DialogShowOk(ex.Message, $"讯息提示", null, 3);
                            PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ex.Message}");
                            PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ex.Message}");
                            return;
                        }
                                               
                    }

                    //log      
                    string strShowText = $"汇出 [{Dtp_Start_Time.Value:yyyy/MM/dd} - {Dtp_Finish_Time.Value:yyyy/MM/dd}]停复机纪录 ";
                   
                    DialogHandler.Instance.Fun_DialogShowOk(strShowText + " 成功!", $"讯息提示", null, 4);
                    EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text} {strShowText}");

                    EventLogHandler.Instance.LogInfo("4-1", $"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录 成功", strShowText);
                    PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} {strShowText}");
                    PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} {strShowText}");                  

                }
                catch (Exception ex)
                {
                    DialogHandler.Instance.Fun_DialogShowOk(ex.Message, $"讯息提示", null, 3);
                    File.Delete(dialog.FileName);
                    PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ex.Message}");
                    PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text} 汇出停复机纪录失败{ex.Message}");
                    throw;
                }
            }

            GC.Collect();
        }

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

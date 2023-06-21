using Akka.Actor;
using DataModel.HMIServerCom.Msg;
using DBService.Repository.Utility;
using DBService.Repository.WorkSchedule;
using GPLManager.Util;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static GPLManager.DataBaseTableFactory;


namespace GPLManager
{
    public partial class frm_4_2_Utility : Form
    {
        DataTable dtGetUtilityList;
        DataRow drGetCurrentRow;
        //語系
        private LanguageHandler LanguageHand;
        public frm_4_2_Utility()
        {
            InitializeComponent();
        }

        private void Frm_4_2_Utility_Load(object sender, EventArgs e)
        {
            PublicForms.Utility = this;
            Control[] Frm_4_2_Control = new Control[] {
                Btn_Cancel,
                Btn_Delete,
                Btn_Edit,
                Btn_New,
                Btn_SendToMMS,
            };

            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_4_2_Control, UserSetupHandler.Instance.Frm_4_2);
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_DataCol,true);
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_Total, true);
            //班次
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Shift, Cob_Shift);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Shift, Cob_Shift_S);
            //班別
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Team, Cob_Team);
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }
        private void Frm_4_2_Utility_Shown(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 查詢能源耗用
        /// </summary>
        private void Fun_GetUtilityData()
        {
            if (UserSetupHandler.Instance.Frm_4_2.Equals("W"))
            { 
                //執行班次搜尋時，才可以上傳MMS
                Btn_SendToMMS.Enabled = Rdo_Shitf.Checked;
            }
            if (Rdo_Shitf.Checked) ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_Total, false);

            //變色，增加辨識度
            Btn_SendToMMS.BackColor = Rdo_Shitf.Checked ? Color.Gold : Color.LightGray;

            string strSql = string.Empty;

            if (Rdo_DateTime.Checked)
            {
                strSql = SqlFactory.Frm_4_2_SelectUnity_Date();
            }
            else if (Rdo_Shitf.Checked)
            {          
                ////查班表的班次 取得 排班模式 & 班次时间
                //string strSql_GetShiftTime = SqlFactory.Frm_4_2_SearchWorkSchedule();
                //DataTable dtGetShiftTime = Data_Access.Fun_SelectDate(strSql_GetShiftTime, GlobalVariableHandler.Instance.strConn_GPL, "能源耗用_查班次", "4 -2");
                               
                //string strShift_StartTime = dtGetShiftTime.Rows[0][nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftStartTime)].ToString();
                //string strShift_EndTime = dtGetShiftTime.Rows[0][nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftEndTime)].ToString();
                //string strSql_Time = "";
                //if (!string.IsNullOrEmpty( strShift_StartTime ) && !string.IsNullOrEmpty(strShift_EndTime))
                //{
                //     strSql_Time = $@"Select CONVERT(varchar,[{nameof(UtilityEntity.TBL_Utility.Receive_Time)}],120)Receive_Time,
                //                   [{nameof(UtilityEntity.TBL_Utility.Shift)}],
                //                   [{nameof(UtilityEntity.TBL_Utility.Team)}],
                //                   [{nameof(UtilityEntity.TBL_Utility.IndirectCoolingWater)}],
                //                   [{nameof(UtilityEntity.TBL_Utility.RinseWater)}],
                //                   [{nameof(UtilityEntity.TBL_Utility.CompressedAir)}],
                //                   [{nameof(UtilityEntity.TBL_Utility.Steam)}]
                //              From [{nameof(UtilityEntity.TBL_Utility)}]
                //             Where [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] >= '{PublicForms.Utility.Dtp_DateShitf.Value:yyyy-MM-dd} {strShift_StartTime}:00' 
                //               And [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] < '{PublicForms.Utility.Dtp_DateShitf.Value.AddDays(1):yyyy-MM-dd} {strShift_EndTime}:00' 
                       
                //              -- And [{nameof(UtilityEntity.TBL_Utility.Shift)}] = '{PublicForms.Utility.Cob_Shift_S.SelectedValue}'
                //             Order by [{nameof(UtilityEntity.TBL_Utility.Receive_Time)}] desc";
                //}
                //strSql = strSql_Time;

                //string ShiftStartTime = Fun_SearchShift(out string ShiftEndTime);



                strSql = SqlFactory.Frm_4_2_SelectUnity_Team();
            }

            dtGetUtilityList = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"能源耗用","4-2");

            Fun_GetUtilityDataTotal(dtGetUtilityList);

            // 把資料導入DataGridView
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Utility, dtGetUtilityList);
            DGVColumnsHandler.Instance.Frm_4_2_Utility(Dgv_Utility);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Utility);

            if (dtGetUtilityList.IsNull())
            {
                if (UserSetupHandler.Instance.Frm_4_2.Equals("W"))
                {
                    //修改
                    Fun_SetBottonEnabled(Btn_Edit, false);
                    //刪除
                    Fun_SetBottonEnabled(Btn_Delete, false);
                }
                   
            }

        }

        private string Fun_SearchShift(out string ShiftEndTime)
        {
            string ShiftStartTime;

            string strSql = SqlFactory.Frm_4_2_SearchWorkSchedule();
            DataTable dtGetWorkSchedule = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "能源耗用_查班次", "4 -2");

            if (dtGetWorkSchedule.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"{Dtp_DateShitf.Value:yyyy/MM/dd} 查无{Cob_Shift_S.SelectedValue}班次");
                ShiftStartTime = ShiftEndTime = string.Empty;
            }
            else
            {
                var Date = Dtp_DateShitf.Value.ToString("yyyy-MM-dd");

                ShiftStartTime = Convert.ToDateTime(Date).AddHours(TimeSpan.Parse(dtGetWorkSchedule.Rows[0][nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftStartTime)].ToString()).TotalHours).ToString("yyyy-MM-dd HH:mm:ss");

                if (dtGetWorkSchedule.Rows[0][nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftEndTime)].ToString().Equals("24:00"))
                {
                    ShiftEndTime = Convert.ToDateTime(Date).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    ShiftEndTime = Convert.ToDateTime(Date).AddHours(TimeSpan.Parse(dtGetWorkSchedule.Rows[0][nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftEndTime)].ToString()).TotalHours).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }

            return ShiftStartTime;
        }

        /// <summary>
        /// 能源耗用統計 : 查詢之後才會統計
        /// </summary>
        /// <param name="dtGetUtility"></param>
        private void Fun_GetUtilityDataTotal(DataTable dtGetUtility)
        {
            float flo_CompressedAir = 0, flo_Steam = 0, flo_RinseWater = 0, flo_IndirectCoolingWater = 0;
            
            if (!dtGetUtility.IsNull())
            {
                foreach (DataRow dr in dtGetUtility.Rows)
                {
                    flo_CompressedAir += Convert.ToSingle(dr[nameof(UtilityEntity.TBL_Utility.CompressedAir)].ToString());
                    flo_Steam += Convert.ToSingle(dr[nameof(UtilityEntity.TBL_Utility.Steam)].ToString());
                    flo_RinseWater += Convert.ToSingle(dr[nameof(UtilityEntity.TBL_Utility.RinseWater)].ToString());
                    flo_IndirectCoolingWater += Convert.ToSingle(dr[nameof(UtilityEntity.TBL_Utility.IndirectCoolingWater)].ToString());
                }
            }
            Txt_CompressedAir_Tol.Text = flo_CompressedAir.ToString();
            Txt_Steam_Tol.Text = flo_Steam.ToString();
            Txt_RinseWater_Tol.Text = flo_RinseWater.ToString();
            Txt_CoolingWater_Tol.Text = flo_IndirectCoolingWater.ToString();
        }

        private void DgvUtility_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Dgv_Utility.CurrentIsNull()) return;
            if (Dgv_Utility.DgvIsNull()) return;

           // Fun_DataGridViewCellsClick();
            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);
        }

        /// <summary>
        /// 選取資料列出
        /// </summary>
        private void Fun_DataGridViewCellsClick()
        {
            if (dtGetUtilityList.IsNull()) return;

            drGetCurrentRow = dtGetUtilityList.Rows[Dgv_Utility.CurrentRow.Index];

            Dtp_UtilityDate.Value = DateTime.Parse(Convert.ToString(drGetCurrentRow[nameof(UtilityEntity.TBL_Utility.Receive_Time)]));

            
            //压缩空气
            Txt_ComeAir.Text = drGetCurrentRow[nameof(UtilityEntity.TBL_Utility.CompressedAir)].ToString() ?? string.Empty;
            //间接冷却水
            Txt_CoolingWater.Text = drGetCurrentRow[nameof(UtilityEntity.TBL_Utility.IndirectCoolingWater)].ToString() ?? string.Empty;
            //蒸汽
            Txt_Steam.Text = drGetCurrentRow[nameof(UtilityEntity.TBL_Utility.Steam)].ToString() ?? string.Empty;
            //冲洗水
            Txt_RinseWater.Text = drGetCurrentRow[nameof(UtilityEntity.TBL_Utility.RinseWater)].ToString() ?? string.Empty;

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQueryData_Click(object sender, EventArgs e)
        {
            Fun_GetUtilityData();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInsert_Click(object sender, EventArgs e)
        { 
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);
            Btn_Save.Visible = true; //儲存
            Btn_Cancel.Visible = true; //取消
            Dtp_UtilityDate.Enabled = true;
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_DataCol, false);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            //新增
            Fun_SetBottonEnabled(Btn_New, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);
            Btn_Save.Visible = true; //儲存
            Btn_Cancel.Visible = true; //取消
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_DataCol, false);
        }

        /// <summary>
        /// 储存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            string strSql = Btn_New.Enabled ? SqlFactory.Frm_4_2_InsertUtility() : SqlFactory.Frm_4_2_UpdateUtility();
            string strMessage = Btn_New.Enabled ? "新增能源耗用记录" : "修改能源耗用记录";

            Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, strMessage, "4-2");

            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);
            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);
            Btn_Save.Visible = false; //儲存
            Btn_Cancel.Visible = false; //取消
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_DataCol, true);
            Fun_GetUtilityData();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);
            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);
            Btn_Save.Visible = false; //儲存
            Btn_Cancel.Visible = false; //取消
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_DataCol, true);

           // Fun_DataGridViewCellsClick();
        }

        /// <summary>
        /// 上传MMS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSendMMS_Click(object sender, EventArgs e)
        {
            string strMessage = "是否要上传MMS?";
            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "上传MMS", Properties.Resources.dialogQuestion, 1);

            if (dialogR.Equals(DialogResult.OK))
            {

                SCCommMsg.CS15_Utility Utility = new SCCommMsg.CS15_Utility
                {
                    TotalCompressedAir = Txt_CompressedAir_Tol.Text.Trim(),
                    TotalCoolingWater = Txt_CoolingWater_Tol.Text.Trim(),
                    TotalRinseWater = Txt_RinseWater_Tol.Text.Trim(),
                    TotalSteam = Txt_Steam_Tol.Text.Trim(),
                    Shift_Date = Dtp_DateShitf.Value.ToString("yyyyMMdd"),
                    Shift_No = Dgv_Utility.CurrentRow.Cells[1].Value.ToString(),
                    Group_No = Dgv_Utility.CurrentRow.Cells[2].Value.ToString(),
                    Unit_code = ""
                };

                PublicComm.Client.Tell(Utility);
                EventLogHandler.Instance.LogInfo("4-2", $"使用者:{PublicForms.Main.lblLoginUser.Text}能源耗用上传MMS", "能源耗用上传MMS");
                EventLogHandler.Instance.EventPush_Message($"已通知Server上传至MMS");
                PublicComm.ClientLog.Info($"通知Server能源耗用上傳MMS");

                DialogHandler.Instance.Fun_DialogShowOk("已通知Server能源耗用上傳MMS", "上传MMS", Properties.Resources.dialogCheck, 4);
            }

            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_Total, true);

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {

            DialogResult actionResult = DialogHandler.Instance.Fun_DialogShowOkCancel($"确定删除[{Dtp_UtilityDate.Value:yyyy-MM-dd HH:mm:ss}]能源耗用记录？", "刪除能源耗用", Properties.Resources.dialogQuestion, 1); //MessageBox.Show($"确定删除[{Dtp_UtilityDate.Value:yyyy-MM-dd HH:mm:ss}]能源耗用记录？", "刪除能源耗用",
            //MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (actionResult == DialogResult.Cancel) return;

            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);
            //新增
            Fun_SetBottonEnabled(Btn_New, false);

            string strSql = SqlFactory.Frm_4_2_DeleteUtility();
            Data_Access.GetInstance().Fun_ExecuteQuery(strSql,GlobalVariableHandler.Instance.strConn_GPL,"删除能源耗用记录","4-2");

            EventLogHandler.Instance.EventPush_Message($"能源耗用删除成功");
            PublicComm.ClientLog.Info($"能源耗用紀錄刪除成功");

            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);
            //新增
            Fun_SetBottonEnabled(Btn_New, true);
        }

        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            //Color colorBack;
            btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            //test
            if (Lbl_RinseWater_Tol_Title.Text == "冲洗水")
                Lbl_RinseWater_Tol_Title.Text = "IndirectCoolingWater";
            else
                Lbl_RinseWater_Tol_Title.Text = "冲洗水";

        }

        private void Lbl_CoolingWater_Tol_Title_VisibleChanged(object sender, EventArgs e)
        {
           
            //FontStyle fs = (Label)sender.Font.Style;
            //FontFamily ffm = Lbl_CoolingWater_Tol_Title.Font.FontFamily;
            //if (Lbl_CoolingWater_Tol_Title.Text.Equals("间接冷却水") || Lbl_CoolingWater_Tol_Title.Text.Length < 7)
            //    Lbl_CoolingWater_Tol_Title.Font = new Font(ffm, (float)14, fs);
            //else
            //    Lbl_CoolingWater_Tol_Title.Font = new Font(ffm, (float)10, fs);
        }
    }
}

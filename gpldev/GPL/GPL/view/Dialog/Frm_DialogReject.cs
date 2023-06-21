using Akka.Actor;
using Controller.Coil;
using DataModel.HMIServerCom.Msg;
using DBService.Repository.PDI;

using DBService.Repository.DefectData;
using System;
using System.Data;
using System.Windows.Forms;
using DBService.Repository.ScheduleDelete_CoilReject_Record_Temp;
using DBService.Repository;
using DBService.Repository.ReturnCoil;
using GPLManager.Util;

namespace GPLManager
{
    public partial class Frm_DialogReject : Form
    {
        public string SkidNumber = string.Empty;
        public string Coil = string.Empty;        
        public string ParentCoil = string.Empty;
        public string ParentPlanNo = string.Empty;
        public string OriPDI_Out_Coil_ID = string.Empty;
        Frm_Defect _Defect = new Frm_Defect();
        public Frm_DialogReject()
        {
            InitializeComponent();
        }


        public void Fun_CoilReject()
        {
            ////檢查TBL_CoilRejectResult是否有記錄
            //if (Fun_CheackReject())
            //{
            //    EventLogHandler.Instance.LogInfo("2-1", $"退料记录查询", $"钢卷号:{Coil}已有退料记录");
            //    EventLogHandler.Instance.EventPush_Message($"此钢卷已有退料记录，请重新确认!");
            //    PublicComm.ClientLog.Info($"[{Coil}]已有退料记录");
            //    return;
            //}

           

        }

        private void Frm_DialogReject_Load(object sender, EventArgs e)
        {

            PublicForms.DialogReject = this;

            //ParentCoil = Coil;
            //ParentPlanNo = ;

            #region -PDI資料ComboBox设定-
            //墊紙方式
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.PAPER_REQ_CODE, Cob_Paper_exit_Code);

            //墊紙類型
            ComboBoxIndexHandler.Instance.SelectPaper(Cob_Paper_Type);

            //班次
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Shift, Cob_Shift_Of_Reject);

            //班別
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Team, Cob_Turn_Of_Reject);

            //最終卷標記
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.End, Cob_FINAL_COIL_FLAG);

            //退料方式
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.ReturnMode, Cob_Mode_Of_Reject);

            //回退时间
            Dtp_Time_Of_Reject.Value = DateTime.Now;


            #endregion

            //退料原因
            ComboBoxIndexHandler.Instance.ComboBox_DeleteCode(Cob_Reason_Of_Reject);

            #region -缺陷資料ComboBox设定-

            //#region - Level -
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D01);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D02);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D03);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D04);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D05);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D06);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D07);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D08);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D09);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D10);
            //#endregion

            //#region - Sid -
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D01);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D02);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D03);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D04);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D05);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D06);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D07);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D08);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D09);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D10);
            //#endregion

            //#region - PosW -
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D01);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D02);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D03);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D04);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D05);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D06);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D07);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D08);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D09);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D10);
            //#endregion

            #endregion

            Fun_CheckStripBrakeRecord();
        }

        /// <summary>
        /// 檢查是否有退料紀錄
        /// </summary>
        private bool Fun_CheackReject()
        {
            bool bolRejectRecord = false;

            string strSql = SqlFactory.Frm_2_1_CheckRejectCoil_DB_TBL_CoilRejectResult(Coil.Trim());
            DataTable dtGetRejectRecord = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "退料记录");

            if (!dtGetRejectRecord.Rows.Count.Equals(0)) 
                bolRejectRecord = true;

            return bolRejectRecord;
        }

        /// <summary>
        /// 檢查是否有斷帶記錄
        /// </summary>
        private void Fun_CheckStripBrakeRecord()
        {
            //资料来源-> Temp = true; ||||  PDI = false;
            bool bolFromTemp = true;
            //要显示在画面上的资料-> dtShowData 
            DataTable dtShowData = new DataTable();
            DataTable dt_Temp = new DataTable();
            DataTable dtPdi = new DataTable();
            string strSql;

            ////20221214.改直接查PDI,不查CoilReject_Temp
            ////钢卷号先查 TBL_ScheduleDelete_CoilReject_Temp
            //string strSql = SqlFactory.Frm_2_1_Select_CoilRejectTemp(Coil);
            //dt_Temp = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "搜尋退料暫存紀錄");

            if (dt_Temp == null || dt_Temp.Rows.Count <= 0)
            {                
                //没退料暂存记录,改查PDI,取最新一笔
                strSql = SqlFactory.Frm_1_2_SelectedData_DB_PDI(Coil);
                dtPdi = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "搜尋PDI紀錄");

                if (dtPdi != null && dtPdi.Rows.Count > 0)
                {
                    dtShowData = dtPdi.Copy();
                    //资料来源是 TBL_RetrunCoil_Temp
                    bolFromTemp = false;

                }
                else 
                { 
                    DialogHandler.Instance.Fun_DialogShowOk($"[{Coil.Trim()}]查无退料暂存记录及PDI资料，请重新确认!", "退料钢卷资料",null, 0);
                   // DialogHandler.Instance.Fun_DialogShowOk($"[{Coil.Trim()}]查無資料，请重新确认!", "退料钢卷资料", 0);

                    PublicComm.ClientLog.Debug($"[{Coil.Trim()}]查无退料暂存记录及PDI资料");
                    return;

                }
            }
            else
            {
                //string strTemp_Entry_Coil_ID = dt_Temp.Rows[0][nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Entry_Coil_ID)].ToString();

                dtShowData = dt_Temp.Copy();
                //资料来源是 TBL_ScheduleDelete_CoilReject_Temp
                bolFromTemp = true;
            }

            try
            {
                ParentCoil = bolFromTemp? dtShowData.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Reject_Coil_No)].ToString():dtShowData.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_ID)].ToString();
                
                ParentPlanNo = bolFromTemp ? dtShowData.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Plan_No)].ToString() : dtShowData.Rows[0][nameof(PDIEntity.TBL_PDI.Plan_No)].ToString();

                OriPDI_Out_Coil_ID = bolFromTemp ? dtShowData.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.OriPDI_Out_Coil_ID)].ToString() : dtShowData.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Coil_ID)].ToString();
            }
            catch (Exception ex)
            {
                PublicComm.ClientLog.Debug($"Exception : Fun_CheckStripBrakeRecord()");
                PublicComm.ClientLog.Debug($"Exception Message : {ex.Message}");
                PublicComm.ClientLog.Debug($"Exception StackTrace : {ex.StackTrace}");
            }
                      
            Fun_RejectDataDisplay(dtShowData, bolFromTemp);

            #region Old
            ////補搜尋原鋼捲PDI出口捲號
            //string strSql = Frm_DialogReject_SqlFactory.SQL_Select_Out_Coil_ID(Coil);
            //DataTable dtGetOut_Coil_ID = DataAccess.Fun_SelectDate(strSql, "退料取得出口卷号");

            //bool bolStripBrake;

            //DataTable dtGetCoilData;

            //// 如果沒有PDI則為斷帶
            //if (dtGetOut_Coil_ID.IsNull())
            //{

            //    PublicComm.ClientLog.Info($"[{Coil}]有斷帶記錄");

            //    //有断带则给定新卷号
            //    //ICoilController _coilController = new CoilController();
            //    //Coil = _coilController.GenSplitChildrenCoilID(Coil);

            //    bolStripBrake = true;

            //    //取得TBL_ScheduleDelete_CoilReject_Temp资料
            //    strSql = Frm_DialogReject_SqlFactory.SQL_Select_BrakeStripRejectData(Coil);
            //    dtGetCoilData = DataAccess.Fun_SelectDate(strSql, "取得断带钢卷资料");

            //    try
            //    {
            //        ParentCoil = dtGetCoilData.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].ToString();
            //        ParentPlanNo = dtGetCoilData.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Plan_No)].ToString();
            //    }
            //    catch (Exception ex)
            //    {
            //        PublicComm.ClientLog.Debug($"Exception : Fun_CheckStripBrakeRecord()");
            //        PublicComm.ClientLog.Debug($"Exception Message : {ex.Message}");
            //        PublicComm.ClientLog.Debug($"Exception StackTrace : {ex.StackTrace}");
            //    }
            //}
            //else
            //{
            //    PublicComm.ClientLog.Info($"[{Coil}]無斷帶記錄");

            //    strSql = Frm_DialogReject_SqlFactory.SQL_Select_Coil_PDI_Data(Coil);
            //    dtGetCoilData = DataAccess.Fun_SelectDate(strSql, "退料钢卷资料");

            //    if (dtGetCoilData.IsNull())
            //    {
            //        DialogHandler.Instance.Fun_DialogShowOk($"[{Coil.Trim()}]查無資料，请重新确认!", "退料钢卷资料", 0);

            //        PublicComm.ClientLog.Debug($"[{Coil.Trim()}]查無資料");
            //        return;
            //    }

            //    bolStripBrake = false;

            //    try
            //    {
            //        ParentCoil = dtGetCoilData.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].ToString();
            //        ParentPlanNo = dtGetCoilData.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Plan_No)].ToString();
            //    }
            //    catch (Exception ex)
            //    {
            //        PublicComm.ClientLog.Debug($"Exception : Fun_CheckStripBrakeRecord()");
            //        PublicComm.ClientLog.Debug($"Exception Message : {ex.Message}");
            //        PublicComm.ClientLog.Debug($"Exception StackTrace : {ex.StackTrace}");
            //    }
            //}

            //OriPDI_Out_Coil_ID = dtGetCoilData.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)].ToString();

            //Fun_RejectDataDisplay(dtGetCoilData, bolStripBrake);
            #endregion

        }

        /// <summary>
        /// 退料钢卷资料
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Saddle"></param> 
        /// <param name="bolTemp"></param>
        private void Fun_RejectDataDisplay(DataTable dt, bool bolTemp)
        {
            Lbl_Skid.Text = $"[{SkidNumber}]";
                       
            #region Columns
            try
            {
                //回退钢卷号
                Txt_Coil_ID.Text = Coil.Trim();

                //入口捲號
                Txt_Entry_Coil_ID.Text = ParentCoil;// dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].ToString();

                //計畫號
                Txt_Plan_No.Text = ParentPlanNo;// dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Plan_No)].ToString();

                //回退方式
                //判斷是否有切/斷自動帶入回退方式
                // 1：無分切記錄，全卷退
                // 2：有分切記錄，半卷退
                Cob_Mode_Of_Reject.SelectedValue = bolTemp && dt.Rows.Count > 0 ? "2" : "1";

                //回退卷长
                Txt_Length_Of_Rejected_Coil.Text = bolTemp ? dt.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Length_Of_Rejected_Coil)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Length)].ToString();

                //回退卷重
                Txt_Weight_Of_Rejected_Coil.Text = bolTemp ? dt.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Weight_Of_Rejected_Coil)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Wt)].ToString();

                //回退內徑
                Txt_Inner_Diameter_Of_RejectedCoil.Text = bolTemp ? dt.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Inner_Diameter_Of_RejectedCoil)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)].ToString();

                //回退外徑
                Txt_Outer_Diameter_Of_RejectedCoil.Text = bolTemp ? dt.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Outer_Diameter_Of_RejectedCoil)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)].ToString();

                //回退原因代码
                Cob_Reason_Of_Reject.SelectedIndex = -1;

                //回退时间
                Dtp_Time_Of_Reject.Value = DateTime.Now;

                if (bolTemp)
                {
                    //班次
                    Cob_Shift_Of_Reject.SelectedValue = dt.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Shift_Of_Reject)].ToString();
                    //班别
                    Cob_Turn_Of_Reject.SelectedValue = dt.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Turn_Of_Reject)].ToString();
                }
                else
                {
                    Cob_Shift_Of_Reject.SelectedIndex = -1;
                    Cob_Turn_Of_Reject.SelectedIndex = -1;
                }             
               
                //墊紙方式
                Cob_Paper_exit_Code.SelectedValue = bolTemp ? dt.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Paper_exit_Code)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)].ToString().Trim();

                //墊紙類型
                Cob_Paper_Type.SelectedValue = bolTemp ? dt.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Paper_Type)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Paper_Code)].ToString().Trim();

                //最终卷标记
                Cob_FINAL_COIL_FLAG.SelectedValue = bolTemp ?Int32.TryParse( dt.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Final_Coil_Flag)].ToString(),out int intFlag)? intFlag:-1 : -1;

                //头部垫纸长度
                Txt_HEAD_PAPER_LENGTH.Text = bolTemp ? dt.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Head_Paper_Length)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Paper_Length)].ToString();
                //头部垫纸宽度
                Txt_HEAD_PAPER_WIDTH.Text = bolTemp ? dt.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Head_Paper_Width)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Paper_Width)].ToString();
                //尾部垫纸长度
                Txt_TAIL_PAPER_LENGTH.Text = bolTemp ? dt.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Tail_Paper_Length)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)].ToString();
                //尾部垫纸宽度
                Txt_TAIL_PAPER_WIDTH.Text = bolTemp ? dt.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Tail_Paper_Width)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)].ToString();


            }
            catch (Exception ex)
            {
                PublicComm.ClientLog.Debug($"Exception : Fun_RejectDataDisplay");
                PublicComm.ClientLog.Debug($"Exception : 欄位給值錯誤");
                PublicComm.ClientLog.Debug($"Exception Message : {ex.Message}");
                PublicComm.ClientLog.Debug($"Exception StackTrace : {ex.StackTrace}");
            }


            #endregion

            // string strPlan_No = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Plan_No)].ToString();
            string strSql = $@"Select Defect.*
                        From [{nameof(PDIEntity.TBL_PDI)}] Defect                      
                        Where Defect.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{Txt_Coil_ID.Text}'
                        And Defect.[{nameof(PDIEntity.TBL_PDI.Plan_No)}] =  '{Txt_Plan_No.Text}' ";
            //string strSql = Frm_DialogReject_SqlFactory.SQL_Select_Coil_Defect_Data(, );//ParentCoil, strPlan_No
            DataTable dtDefData = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "缺陷资料");
            
            if (dtDefData != null && dtDefData.Rows.Count > 0)
            {               
                try
                {
                    #region [Defect]
                    #region  - Code -
                    txtCode_D01.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Code)].ToString();
                    txtCode_D02.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Code)].ToString();
                    txtCode_D03.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Code)].ToString();
                    txtCode_D04.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Code)].ToString();
                    txtCode_D05.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Code)].ToString();
                    txtCode_D06.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Code)].ToString();
                    txtCode_D07.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Code)].ToString();
                    txtCode_D08.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Code)].ToString();
                    txtCode_D09.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Code)].ToString();
                    txtCode_D10.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Code)].ToString();
                    #endregion

                    #region - Origin -
                    txtOrigin_D01.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Origin)].ToString();
                    txtOrigin_D02.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Origin)].ToString();
                    txtOrigin_D03.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Origin)].ToString();
                    txtOrigin_D04.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Origin)].ToString();
                    txtOrigin_D05.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Origin)].ToString();
                    txtOrigin_D06.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Origin)].ToString();
                    txtOrigin_D07.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Origin)].ToString();
                    txtOrigin_D08.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Origin)].ToString();
                    txtOrigin_D09.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Origin)].ToString();
                    txtOrigin_D10.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Origin)].ToString();
                    #endregion

                    #region - Sid (ComboBox)-

                    cbo_Sid_D01.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Sid)].ToString().Trim();
                    cbo_Sid_D02.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Sid)].ToString().Trim();
                    cbo_Sid_D03.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Sid)].ToString().Trim();
                    cbo_Sid_D04.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Sid)].ToString().Trim();
                    cbo_Sid_D05.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Sid)].ToString().Trim();
                    cbo_Sid_D06.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Sid)].ToString().Trim();
                    cbo_Sid_D07.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Sid)].ToString().Trim();
                    cbo_Sid_D08.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Sid)].ToString().Trim();
                    cbo_Sid_D09.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Sid)].ToString().Trim();
                    cbo_Sid_D10.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Sid)].ToString().Trim();

                    #endregion

                    #region - Pos_W (ComboBox)-
                    cbo_PosW_D01.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Pos_W)].ToString().Trim();
                    cbo_PosW_D02.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Pos_W)].ToString().Trim();
                    cbo_PosW_D03.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Pos_W)].ToString().Trim();
                    cbo_PosW_D04.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Pos_W)].ToString().Trim();
                    cbo_PosW_D05.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Pos_W)].ToString().Trim();
                    cbo_PosW_D06.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Pos_W)].ToString().Trim();
                    cbo_PosW_D07.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Pos_W)].ToString().Trim();
                    cbo_PosW_D08.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Pos_W)].ToString().Trim();
                    cbo_PosW_D09.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Pos_W)].ToString().Trim();
                    cbo_PosW_D10.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Pos_W)].ToString().Trim();
                    #endregion

                    #region - Pos_L_Start - 
                    txtPos_L_Start_D01.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start)].ToString();
                    txtPos_L_Start_D02.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start)].ToString();
                    txtPos_L_Start_D03.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start)].ToString();
                    txtPos_L_Start_D04.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start)].ToString();
                    txtPos_L_Start_D05.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start)].ToString();
                    txtPos_L_Start_D06.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start)].ToString();
                    txtPos_L_Start_D07.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start)].ToString();
                    txtPos_L_Start_D08.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start)].ToString();
                    txtPos_L_Start_D09.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start)].ToString();
                    txtPos_L_Start_D10.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start)].ToString();
                    #endregion

                    #region - Pos_L_End -
                    txtPos_L_End_D01.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Pos_L_End)].ToString();
                    txtPos_L_End_D02.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Pos_L_End)].ToString();
                    txtPos_L_End_D03.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Pos_L_End)].ToString();
                    txtPos_L_End_D04.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Pos_L_End)].ToString();
                    txtPos_L_End_D05.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Pos_L_End)].ToString();
                    txtPos_L_End_D06.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Pos_L_End)].ToString();
                    txtPos_L_End_D07.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Pos_L_End)].ToString();
                    txtPos_L_End_D08.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Pos_L_End)].ToString();
                    txtPos_L_End_D09.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Pos_L_End)].ToString();
                    txtPos_L_End_D10.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Pos_L_End)].ToString();
                    #endregion

                    #region - Level (ComboBox)-

                    cbo_Level_D01.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Level)].ToString().Trim();
                    cbo_Level_D02.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Level)].ToString().Trim();
                    cbo_Level_D03.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Level)].ToString().Trim();
                    cbo_Level_D04.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Level)].ToString().Trim();
                    cbo_Level_D05.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Level)].ToString().Trim();
                    cbo_Level_D06.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Level)].ToString().Trim();
                    cbo_Level_D07.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Level)].ToString().Trim();
                    cbo_Level_D08.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Level)].ToString().Trim();
                    cbo_Level_D09.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Level)].ToString().Trim();
                    cbo_Level_D10.SelectedItem = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Level)].ToString().Trim();

                    #endregion

                    #region - Percent -
                    txtPercent_D01.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Percent)].ToString();
                    txtPercent_D02.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Percent)].ToString();
                    txtPercent_D03.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Percent)].ToString();
                    txtPercent_D04.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Percent)].ToString();
                    txtPercent_D05.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Percent)].ToString();
                    txtPercent_D06.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Percent)].ToString();
                    txtPercent_D07.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Percent)].ToString();
                    txtPercent_D08.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Percent)].ToString();
                    txtPercent_D09.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Percent)].ToString();
                    txtPercent_D10.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Percent)].ToString();
                    #endregion

                    #region - QGrade - 
                    txtQGRADE_D01.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D01_QGrade)].ToString();
                    txtQGRADE_D02.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D02_QGrade)].ToString();
                    txtQGRADE_D03.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D03_QGrade)].ToString();
                    txtQGRADE_D04.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D04_QGrade)].ToString();
                    txtQGRADE_D05.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D05_QGrade)].ToString();
                    txtQGRADE_D06.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D06_QGrade)].ToString();
                    txtQGRADE_D07.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D07_QGrade)].ToString();
                    txtQGRADE_D08.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D08_QGrade)].ToString();
                    txtQGRADE_D09.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D09_QGrade)].ToString();
                    txtQGRADE_D10.Text = dtDefData.Rows[0][nameof(PDIEntity.TBL_PDI.D10_QGrade)].ToString();
                    #endregion
                    #endregion
                }
                catch (Exception ex)
                {
                    PublicComm.ClientLog.Debug($"Exception : Fun_RejectDataDisplay");
                    PublicComm.ClientLog.Debug($"Exception : 缺陷欄位給值錯誤");
                    PublicComm.ClientLog.Debug($"Exception Message : {ex.Message}");
                    PublicComm.ClientLog.Debug($"Exception StackTrace : {ex.StackTrace}");
                }
            }
            else
            {
                string strMsg_DefectIsNull = $"无缺陷资料!{Environment.NewLine}若要查询母卷缺陷，请至[缺陷资料]页签使用右下角[母卷缺陷]按钮。";

                Lbl_Defect_IsNull.Text = strMsg_DefectIsNull;

                Lbl_Defect_IsNull.Visible = true;
            }


        }


        private void Btn_RejectOK_Click(object sender, EventArgs e)
        {
            if (Txt_Coil_ID.Text.IsEmpty())
            {
                DialogHandler.Instance.Fun_DialogShowOk("回退钢卷号请勿空白", "退料作业", null, 0);
                Txt_Coil_ID.Focus();
                PublicComm.ClientLog.Info($"回退钢卷号请勿空白");
                return;
            }

            if (Txt_Entry_Coil_ID.Text.IsEmpty())
            {
                DialogHandler.Instance.Fun_DialogShowOk("入口钢卷号请勿空白", "退料作业", null, 0);
                Txt_Entry_Coil_ID.Focus();
                PublicComm.ClientLog.Info($"入口钢卷号请勿空白");
                return;
            }

            if (Cob_Reason_Of_Reject.Text.IsEmpty())
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选择退料代码", "退料作业", null, 0);
                Cob_Reason_Of_Reject.Focus();
                PublicComm.ClientLog.Info($"请选择退料代码");
                return;
            }

            //if (Fun_CheackReject())
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("此钢卷已有退料记录，请重新确认!", "退料作业", null, 0);

            //    PublicComm.ClientLog.Info($"[{Coil.Trim()}]已有退料紀錄");
            //    return;
            //}

            //确认是否要退料(Double Check)
            string strMessage = $"确定对安座[{ SkidNumber }]的钢卷[{Coil.Trim()}]进行退料?";

            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "退料作业", Properties.Resources.dialogQuestion, 1);

            if (dialogR == DialogResult.Cancel) return;

            #region -隱藏-
            //新增退料紀錄
            //strSql = SqlFactory.Frm_2_1_InsertCoilReject_Record_DB_L3L2_TBL_ScheduleDelete_CoilReject_Record(DelReturn_Coil_ID);

            //if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_CPL, "退料记录", "2-1"))
            //{
            //    EventLogHandler.Instance.EventPush_Message($"退料记录新增资料错误");
            //    return;
            //}

            //strSql = SqlFactory.Frm_2_1_InsertRejectResult_DB_L3L2_CoilRejectResult(DelReturn_Coil_ID);

            //if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_CPL, "退料记录", "2-1"))
            //{
            //    EventLogHandler.Instance.EventPush_Message($"退料记录新增资料错误");
            //    return;
            //}

            //分切記錄
            //if (!Coil.Equals(ParentCoil))
            //{
            //    string strSql = SqlFactory.Frm_2_1_InsertSplitCoils(Coil, ParentCoil);
            //    if (!Fun_InsertSplitCoil(strSql)) return;
            //}
            #endregion

            //退料中鋼卷記錄查詢及確認
            Fun_CheckRejectedRecord();

            Fun_AkkaTellServer();

            ////關閉回退視窗前,先關掉母捲缺陷視窗
            //if(_Defect != null)
            //    _Defect.Close();
            Close();
        }
       

        /// <summary>
        /// 檢查退料中鋼捲記錄
        /// </summary>
        /// <returns></returns>
        private void Fun_CheckRejectedRecord()
        {
            string strSql, strSql_Defect;

            // Coil //回退鋼捲號

            //入口捲號
            string strEntry_CoilNo = "";
            //計畫號
            string strPlan_No = string.Empty;

            //回退時間
            string strTime_Of_Reject = Dtp_Time_Of_Reject.Value.ToString("yyyyMMddHHmmss");
            //檢查退料中鋼卷記錄
            strSql = SqlFactory.Frm_2_1_Select_CoilRejectTemp(Coil, strPlan_No);
            DataTable dtCheckReturnCoil = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "退料中钢卷记录查詢");
            if (!dtCheckReturnCoil.IsNull())
            {
                strEntry_CoilNo =  dtCheckReturnCoil.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Entry_CoilNo)].ToString();
                strPlan_No = dtCheckReturnCoil.Rows[0][nameof(ReturnCoilEntity.TBL_RetrunCoil_Temp.Plan_No)].ToString();               
            }
            else
            {
                strEntry_CoilNo = Coil;
            }
            //檢查退料鋼捲缺陷資料
            strSql_Defect = SqlFactory.Frm_1_2_SelectedDefectData_DB_PDI(strEntry_CoilNo, strPlan_No);//Coil
            DataTable dtCheckDefect = Data_Access.Fun_SelectDate(strSql_Defect, GlobalVariableHandler.Instance.strConn_GPL, "退料中钢卷缺陷查詢");
           
            PublicComm.ClientLog.Info($"鋼卷號:[{Coil.Trim()}] 鞍座:[{SkidNumber}]退料:");//
            PublicComm.ClientLog.Info($"檢查退料鋼捲缺陷資料"); 
            PublicComm.ClientLog.Info($"查詢[TBL_PDI]SQL");
            PublicComm.ClientLog.Info($"--Start--");
            PublicComm.ClientLog.Info($"{strSql_Defect}");
            PublicComm.ClientLog.Info($"--End--");
           

            //退料鞍座
            string Reject_skid = Fun_ReturnSkidNumber(SkidNumber);

           

            //沒記錄Insert ; 有記錄Update
            //鋼捲資訊
            strSql = dtCheckReturnCoil.IsNull() ? SqlFactory.Frm_2_1_InsertReturnCoilRecord_DB_TBL_RetrunCoil() : SqlFactory.Frm_2_1_UpdateReturnCoilRecord_DB_TBL_RetrunCoil(Coil,null, strTime_Of_Reject);

            //缺陷
            strSql_Defect = dtCheckDefect.IsNull() ? SqlFactory.Frm_2_1_InsertReturnCoilRecord_DB_PDI_Defect() : SqlFactory.Frm_2_1_UpdateReturnCoilRecord_DB_PDI_Defect(strEntry_CoilNo, strPlan_No);

           //if(dtCheckDefect.IsNull())
           // {
           //     strSql_Defect = 
           // }

            PublicComm.ClientLog.Info($"鋼卷號:[{Coil.Trim()}] 鞍座:[{SkidNumber}]退料:");
            PublicComm.ClientLog.Info($"变更[TBL_ScheduleDelete_CoilReject_Temp]SQL");
            PublicComm.ClientLog.Info($"--Start--");
            PublicComm.ClientLog.Info($"{strSql}");
            PublicComm.ClientLog.Info($"--End--");

            if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "退料中记录-钢卷资料"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"退料中记录-钢卷资料处理失败", "退料中记录-钢卷资料",null, 3);

                return;
            }

            PublicComm.ClientLog.Info($"鋼卷號:[{Coil.Trim()}] 鞍座:[{SkidNumber}]退料:");
            PublicComm.ClientLog.Info($"变更[TBL_Coil_Defect]SQL");
            PublicComm.ClientLog.Info($"--Start--");
            PublicComm.ClientLog.Info($"{strSql_Defect}");
            PublicComm.ClientLog.Info($"--End--");

            if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql_Defect, GlobalVariableHandler.Instance.strConn_GPL, "退料中记录-缺陷资料"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"退料中记录-缺陷资料处理失败", "退料中记录-缺陷资料", null ,3);

                return;
            }

            string strMov = dtCheckReturnCoil.IsNull() ? "新增" : "修改";

            EventLogHandler.Instance.LogDebug("2-1", $"{strMov}资料库", $"{strMov}退料记录 Table[TBL_RetrunCoil]，成功");
            PublicComm.ClientLog.Info($"{strMov}退料记录 Table[TBL_RetrunCoil]，成功");
        }

        private void Fun_AkkaTellServer()
        {
            //通知Server退料作業
            SCCommMsg.CS05_RejectCoil _RejectCoil = new SCCommMsg.CS05_RejectCoil
            {
                Source = "GPL_HMI",
                ID = "RejectCoil",
                CoilID = Coil,
                PlanNo = Txt_Plan_No.Text.Trim(),
                Saddle = SkidNumber
            };

            PublicComm.Client.Tell(_RejectCoil);

            DialogHandler.Instance.Fun_DialogShowOk($"已通知Server退料", "退料作业", null, 4);

            PublicComm.ClientLog.Info($"鋼卷號:[{Coil.Trim()}] 鞍座:[{SkidNumber}]退料:");
            PublicComm.ClientLog.Info($"通知Server讯息:SCCommMsg.CS05_RejectCoil");
            PublicComm.ClientLog.Info($"--Start--");
            PublicComm.ClientLog.Info($"{nameof(_RejectCoil.Source)} = {_RejectCoil.Source} ");
            PublicComm.ClientLog.Info($"{nameof(_RejectCoil.ID)} = {_RejectCoil.ID} ");
            PublicComm.ClientLog.Info($"{nameof(_RejectCoil.CoilID)}  = {_RejectCoil.CoilID} ");
            PublicComm.ClientLog.Info($"{nameof(_RejectCoil.Saddle)}  = {_RejectCoil.Saddle} ");
            PublicComm.ClientLog.Info($"--End--");

            PublicComm.ClientLog.Info($"已通知Server鋼卷號:[{Coil.Trim()}] 鞍座:[{SkidNumber}]退料!");
            EventLogHandler.Instance.LogInfo("2-1", "通知Server退料实绩", $"退料实绩 钢卷编号:{ Coil.Trim() } 鞍座位置: {SkidNumber}");
        }



       

        private void Btn_SearchDefect_Click(object sender, EventArgs e)
        {
            if (ParentCoil.Trim().Equals(""))
            {
                DialogHandler.Instance.Fun_DialogShowOk("无母卷号!", "退料查询母卷缺陷",null, 0);

                return;
            }
            //if (ParentPlanNo.Trim().Equals(""))
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("无计划号!", "退料查询母卷缺陷", 0);

            //    return;
            //}


             _Defect = new Frm_Defect
            {
                Defect_Coil_ID = ParentCoil.Trim(),
                Defect_Plan_No = ParentPlanNo.Trim()
            };

            _Defect.Location = new System.Drawing.Point(this.Location.X+ 280, this.Location.Y -25);
            _Defect.Show();
           
        }


        /// <summary>
        /// 取消按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (_Defect != null)
                _Defect.Close();

            Close();
           
        }


        private void TabReject_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_RejectControl, e);
        }


        private void Cbo_ReturnCode_Click(object sender, EventArgs e)
        {
           // ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_DeleteCode(Cob_Reason_Of_Reject);
          
        }


        /// <summary>
        /// 轉換Skid Value
        /// </summary>
        /// <param name="skid"></param>
        /// <returns></returns>
        private string Fun_ReturnSkidNumber(string skid)
        {
            switch (skid)
            {
                case "ESK01":
                    skid = "1";
                    break;

                case "ESK02":
                    skid = "2";
                    break;

                case "ETOP":
                    skid = "3";
                    break;

                case "DSK01":
                    skid = "4";
                    break;

                case "DSK02":
                    skid = "5";
                    break;

                case "DTOP":
                    skid = "6";
                    break;

                default:
                    skid = string.Empty;
                    break;
            }

            return skid;
        }


        /// <summary>
        /// 新增分切記錄 - 保存
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private bool Fun_InsertSplitCoil(string strSql)
        {
            bool bolSql = true;

            if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "退料记录"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"分切记录新增资料错误", "退料记录", null, 3);

                bolSql = false;
            }

            return bolSql;
        }

        private void cbo_Mode_Reject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Cob_Mode_Of_Reject.SelectedIndex == 1)
            {

            }
        }

        private void Fun_OnlyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //&& (e.KeyChar != (char)Keys.Space) 
            //          數字                                            //backspace                    //Enter             
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Enter)))
            {
                e.Handled = true;
            }
        }
    }
}

using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using DataModel.HMIServerCom.Msg;
using Akka.Actor;
using static GPLManager.DataBaseTableFactory;
using Controller.Coil;
using DBService.Repository.PDI;
using DBService.Repository;
using GPLManager.Util;
using DBService.Repository.PDO;
using DBService.Repository.LookupTblFlattener;
using DBService.Repository.GrindPlanHistory;
using DBService.Repository.GrindRecords;
using DBService;
using DBService.Repository.Belt;
using Core.Define;
using System.Text;

namespace GPLManager
{
    public partial class frm_2_1_Tracking : Form
    {
        #region 變數
        DataTable dt;
        DataTable dtStatus;
        DataTable dtOff;
        DataTable dtOn;
        DataTable dt_EntryCheckSaddle;
        DataTable dt_ExitCheckSaddle;
        DataTable dtGetProcessData;
        DataTable dtGetConnectionStatus = new DataTable();
        string strTip = "";
        ToolTip Tip = new ToolTip();
        private string ParentCoil_ID = "";
        string DelReturn_Saddle = "";
        string DelReturn_Coil_ID = "";
        string MouseDown_CoilID = "";
        public int EntryPostion;

        bool bolDgv_off_ColumnShow = false;
        bool bolDgv_on_ColumnShow = false;

        //語系
        private LanguageHandler LanguageHand;

        #endregion

        public frm_2_1_Tracking()
        {
            InitializeComponent();
        }
        private void Frm_2_1_Tracking_Load(object sender, EventArgs e)
        {
            PublicForms.Tracking = this;
            Control[] Frm_2_1_Control = new Control[] {
            Btn_AutoCoilFeed, //進料模式
            Btn_StripBreakModify,//斷帶修改POR卷號
            Btn_PORPresetL1,//下拋鋼捲生產參數
            Btn_ManualFeed, //ESK02  料
            Btn_ESK01_Entry, //ESK01 入料
            Btn_ESK01_Return, //ESK01 退料
            Btn_ETOP_Return, //ETOP 退料
            Btn_ESK01_PrintLabel, //ESK01 列印標籤
            Btn_ETOP_PrintLabel, //ETOP 列印標籤
            Btn_ESK01_Del, //ESK01 刪除
            Btn_ETOP_Del, //ETOP 刪除
            Btn_PORReturn, //開捲機 退料
            Btn_PDO, //PDO確認
            Btn_DSK02Weight, //DSK02 秤重
            Btn_DSK02_Ready,//DSK02 確
            Btn_DSK01_CoilOut, //DSK01 出料
            Btn_DSK02_CoilOut, //DSK02 出料
            Btn_DTOP_CoilOut,//DTOP 出料
            Btn_DSK01_PrintLabel, //DSK01 列印標籤
            Btn_DSK02_PrintLabel, //DSK02 列印標籤
            Btn_DTOP_PrintLabel, //DTOP 列印標籤
            Btn_DSK01_Del, //DSK01 刪除
            Btn_DSK02_Del, //DSK02 刪除
            Btn_DTOP_Del //DTOP 刪除
            };
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_2_1_Control, UserSetupHandler.Instance.Frm_2_1, PublicForms.Tracking);

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);

        }
        private void Frm_2_1_Tracking_Shown(object sender, EventArgs e)
        {         
            Fun_SelectTrackingData();
            Fun_TimerStart();
            Fun_SkidChangeColor();
        }

        /// <summary>
        /// Tracking Map Form Shown Data
        /// </summary>
        private void Fun_SelectTrackingData()
        {
            //未上線鋼卷詳細資料
            try
            {
                Dgv_offData();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug("2-1", $"未上线钢卷资料", $"未上线钢卷资料查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"未上線鋼卷查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"未上線鋼卷查詢資料庫失敗:{ex}");
            }

            //線上鋼卷詳細資料
            try
            {
                Dgv_onData();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug("2-1", $"线上钢卷资料", $"线上钢卷资料查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"線上鋼卷查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"線上鋼卷查詢資料庫失敗:{ex}");
            }

            //Process Data
            try
            {
                Fun_SelectProcessData();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug( "2-1", $"Process Data", $"Process Data查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"Process Data查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"Process Data查詢資料庫失敗:{ex}");
            }

            //Tracking Map
            try
            {
                Fun_SelectTrackingMap();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug( "2-1", $"钢卷追踪", $"钢卷追踪查询资料库失败(Fun_SelectTrackingData):{ex}");
                PublicComm.ClientLog.Debug($"钢卷追踪查询资料库失败(Fun_SelectTrackingData):{ex}");
                PublicComm.ExceptionLog.Debug($"钢卷追踪查询资料库失败(Fun_SelectTrackingData):{ex}");
            }
                        
            //取得連線狀態
            try
            {
                Fun_SelectNetWorkStatus();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug("2-1", $"2-1连线状态", $"2-1连线状态查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"2-1连线状态查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"2-1连线状态查詢資料庫失敗:{ex}");
            }

            //載入系統設定
            try
            {
                Fun_SelectSystemSetting();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug("2-1", $"2-1系统设定值", $"2-1系统设定值查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"2-1載入系統設定值查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"2-1載入系統設定值查詢資料庫失敗:{ex}");
            }

            //GR資料
            try
            {
                Fun_SelectGR_Data();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug("2-1", $"2-1研磨机资料", $"2-1研磨机资料查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"2-1研磨機資料查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"2-1研磨機資料查詢資料庫失敗:{ex}");
            }
        }

        /// <summary>
        /// Form Shown Timer Start
        /// </summary>
        private void Fun_TimerStart()
        {
            Timer_TrackingMap.Start();
            PublicComm.ClientLog.Info($"--------------開始產線追蹤--------------");

            Timer_DgvUse.Start();
            PublicComm.ClientLog.Info($"--------------線上/排程鋼卷資料刷新開始--------------");

            Timer_Line.Start();
            PublicComm.ClientLog.Info($"--------------Process Data刷新開始--------------");

        }

        /// <summary>
        /// 鞍座鋼卷號變色
        /// </summary>
        private void Fun_SkidChangeColor()
        {
            //入口变色
            try
            {
                EntryChangeColor();
            }
            catch (Exception ex)
            {
                //EventLogHandler.Instance.EventPush_Message($"入口端变色失败:{ex}");
                //EventLogHandler.Instance.LogDebug("2-1", $"入口端变色", $"入口端变色失败:{ex}");
                PublicComm.ClientLog.Debug($"入口端變色失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"入口端變色失敗:{ex}");
            }

            //出口变色
            try
            {
                ExitChangeColor();
            }
            catch (Exception ex)
            {
                //EventLogHandler.Instance.EventPush_Message($"出口端变色失败:{ex}");
                //EventLogHandler.Instance.LogDebug("2-1", $"出口端变色", $"出口端变色失败:{ex}");
                PublicComm.ClientLog.Debug($"出口端變色失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"出口端變色失敗:{ex}");
            }
        }

        /// <summary>
        /// 搜尋Tracking Map Table
        /// </summary>
        public void Fun_SelectTrackingMap()
        {
            #region TrackingMap
            string strSql = SqlFactory.Frm_2_1_SelectMap_DB_TrackingMap();
            try
            {
                dt = Data_Access.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_GPL);
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"钢卷追踪查询资料库失败(Fun_SelectTrackingMap):{ex}");
                EventLogHandler.Instance.LogDebug("2-1", $"钢卷追踪", $"钢卷追踪查询资料库失败(Fun_SelectTrackingMap):{ex}");
                PublicComm.ClientLog.Debug($"钢卷追踪查询资料库失败(Fun_SelectTrackingMap):{ex}");
                PublicComm.ExceptionLog.Debug($"钢卷追踪查询资料库失败(Fun_SelectTrackingMap):{ex}");
            }

            if (dt.IsNull())
            { 
                PublicComm.ClientLog.Debug($"Tracking Map無資料");
                return;
            }

            #region Tracking Map Insert to Lable
            //ECAR
            Lbl_ECar_CoilNo.Text = dt.Rows[0][nameof(TBL_CoilMap.Entry_Car)].ToString().Trim() ?? string.Empty;
            //ESK01
            Lbl_ESK01_CoilNo.Text = dt.Rows[0][nameof(TBL_CoilMap.Entry_SK01)].ToString().Trim() ?? string.Empty;
            //ETOP
            Lbl_ETOP_CoilNo.Text = dt.Rows[0][nameof(TBL_CoilMap.Entry_TOP)].ToString().Trim() ?? string.Empty;
            //POR
            Lbl_POR_CoilNo.Text = dt.Rows[0][nameof(TBL_CoilMap.POR)].ToString().Trim() ?? string.Empty;
            Pic_Track_Picture_W_L.Visible = string.IsNullOrEmpty(Lbl_POR_CoilNo.Text.Trim());
            //TR
            Lbl_TR_CoilNo.Text = dt.Rows[0][nameof(TBL_CoilMap.TR)].ToString().Trim() ?? string.Empty;
            Pic_Track_Picture_W_R.Visible = string.IsNullOrEmpty(Lbl_TR_CoilNo.Text.Trim());
            //DSK01
            Lbl_DSK01_CoilNo.Text = dt.Rows[0][nameof(TBL_CoilMap.Delivery_SK01)].ToString().Trim() ?? string.Empty;
            //DSK02
            Lbl_DSK02_CoilNo.Text = dt.Rows[0][nameof(TBL_CoilMap.Delivery_SK02)].ToString().Trim() ?? string.Empty;
            //DTOP
            Lbl_DTOP_CoilNo.Text = dt.Rows[0][nameof(TBL_CoilMap.Delivery_TOP)].ToString().Trim() ?? string.Empty;
            //DCAR
            Lbl_DCar_CoilNo.Text = dt.Rows[0][nameof(TBL_CoilMap.Delivery_Car)].ToString().Trim() ?? string.Empty;
            #endregion

            #endregion
        }

        #region 入口段變色判斷
        private void EntryChangeColor()
        {
            //ETOP
            if (!Lbl_ETOP_CoilNo.Text.IsEmpty())
            {
                Coil_PDIInfo_Select(Lbl_ETOP_CoilNo.Text, Lbl_ETOP_CoilNo);
                EventLogHandler.Instance.LogDebug("2-1", $"入口段变色(鞍座位置:ETOP 钢卷编号:{Lbl_ETOP_CoilNo.Text.Trim()})", strTip);
                PublicComm.ClientLog.Info($"ETOP [{Lbl_ETOP_CoilNo.Text.Trim()}] {strTip}");
            }
            else
            { LableColorReset(Lbl_ETOP_CoilNo); }

            //ESK01
            if (!Lbl_ESK01_CoilNo.Text.IsEmpty())
            {
                Coil_PDIInfo_Select(Lbl_ESK01_CoilNo.Text, Lbl_ESK01_CoilNo);
                EventLogHandler.Instance.LogDebug("2-1", $"入口段变色(鞍座位置:ESK01 钢卷编号:{Lbl_ESK01_CoilNo.Text.Trim()})", strTip);
                PublicComm.ClientLog.Info($"ESK01 [{Lbl_ESK01_CoilNo.Text.Trim()}] {strTip}");
            }
            else
            { LableColorReset(Lbl_ESK01_CoilNo); }

            //ECAR
            if (!Lbl_ECar_CoilNo.Text.IsEmpty())
            {
                Coil_PDIInfo_Select(Lbl_ECar_CoilNo.Text, Lbl_ECar_CoilNo);
                EventLogHandler.Instance.LogDebug("2-1", $"入口段变色(位置:ECAR 钢卷编号:{Lbl_ECar_CoilNo.Text.Trim()})", strTip);
                PublicComm.ClientLog.Info($"ECar [{Lbl_ECar_CoilNo.Text.Trim()}] {strTip}");
            }
            else
            { LableColorReset(Lbl_ECar_CoilNo); }
        }
        #endregion

        #region 出口段變色判斷
        private void ExitChangeColor()
        {
            //DTOP
            if (!Lbl_DTOP_CoilNo.Text.IsEmpty())
            {
                Coil_PDOInfo_Select(Lbl_DTOP_CoilNo.Text, Lbl_DTOP_CoilNo);
               // EventLogHandler.Instance.LogDebug("2-1", $"出口段变色(鞍座位置:DTOP 钢卷编号:{Lbl_DTOP_CoilNo.Text.Trim()})", strTip);
                PublicComm.ClientLog.Info($"DTOP [{Lbl_DTOP_CoilNo.Text.Trim()}] {strTip}");
            }
            else
            { LableColorReset(Lbl_DTOP_CoilNo); }

            //DSK01
            if (!Lbl_DSK01_CoilNo.Text.IsEmpty())
            {
                Coil_PDOInfo_Select(Lbl_DSK01_CoilNo.Text, Lbl_DSK01_CoilNo);
               // EventLogHandler.Instance.LogDebug("2-1", $"出口段变色(鞍座位置:DSK01 钢卷编号:{Lbl_DSK01_CoilNo.Text.Trim()})", strTip);
                PublicComm.ClientLog.Info($"DSK01 [{Lbl_DSK01_CoilNo.Text.Trim()}] {strTip}");
            }
            else
            { LableColorReset(Lbl_DSK01_CoilNo); }

            //DSK02
            if (!Lbl_DSK02_CoilNo.Text.IsEmpty())
            {
                Coil_PDOInfo_Select(Lbl_DSK02_CoilNo.Text, Lbl_DSK02_CoilNo);
              // EventLogHandler.Instance.LogDebug("2-1", $"出口段变色(鞍座位置:DSK02 钢卷编号:{Lbl_DSK02_CoilNo.Text.Trim()})", strTip);
                PublicComm.ClientLog.Info($"DSK02 [{Lbl_DSK02_CoilNo.Text.Trim()}] {strTip}");
            }
            else
            { LableColorReset(Lbl_DSK02_CoilNo); }

            //DCAR
            if (!Lbl_DCar_CoilNo.Text.IsEmpty())
            {
                Coil_PDOInfo_Select(Lbl_DCar_CoilNo.Text, Lbl_DCar_CoilNo);
               // EventLogHandler.Instance.LogDebug("2-1", $"出口段变色(位置:DCAR 钢卷编号:{lbDelivery_Car.Text.Trim()})", strTip);
                PublicComm.ClientLog.Info($"DCar [{Lbl_DCar_CoilNo.Text.Trim()}] {strTip}");
            }
            else
            { LableColorReset(Lbl_DCar_CoilNo); }
        }
        #endregion

        private void LableColorReset(Label lb)
        {
            if (lb.Text.IsEmpty())
            {
                lb.BackColor = Color.Transparent;
                lb.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// 入口鞍座上鋼卷資料
        /// </summary>
        private void Coil_PDIInfo_Select(string Coil_ID, Label lb)
        {
            string strSql = SqlFactory.Frm_2_1_SelectEntrySkidCoilInfo_DB_PDI(Coil_ID);
            dt_EntryCheckSaddle = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "入口段鞍座钢卷资料","2-1");

            //Tip清空
            strTip = string.Empty;

            //确认PDI及掃描扫描状态
            Scan_PDI_Info_Check();
            //确认导带资料
            Strip_Info_Check();
            //ToolTip setup
            Label_ToolTip(lb);
        }

        /// <summary>
        /// 出口鞍座上鋼卷資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <param name="lb"></param>
        private void Coil_PDOInfo_Select(string Coil_ID, Label lb)
        {
            string strSql = SqlFactory.Frm_2_1_SelectDeliverySkidCoilInfo_DB_PDO(Coil_ID);
            dt_ExitCheckSaddle = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "出口段鞍座钢卷资料","2-1");

            //Tip清空
            strTip = string.Empty;

            //确认PDO扫描状态
            Scan_PDO_Info_Check();
            //确认PDO是否有上传
            PDO_Uploaded_Flag();
            //ToolTip setup
            Label_ToolTip(lb);
        }

        /// <summary>
        /// 判斷導帶資訊是否未輸入
        /// </summary>
        /// <param name="Coil_ID"></param>
        private void Strip_Info_Check()
        {
            
            if (!dt_EntryCheckSaddle.IsNull())
            {
                if (dt_EntryCheckSaddle.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Leader_Attached)].ToString().Equals("1"))
                {
                    if (dt_EntryCheckSaddle.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Leader_St_No)].ToString().IsEmpty())
                    {
                        strTip += "导带资料未输入" + Environment.NewLine;
                    }
                }
            }
        }

        /// <summary>
        /// 判斷PDI鋼卷掃描結果
        /// </summary>
        /// <param name="Coil_ID"></param>
        private void Scan_PDI_Info_Check()
        {
            if (dt_EntryCheckSaddle.IsNull())
            {
                strTip += "钢卷尚未收到PDI" + Environment.NewLine;
                return;
            }

           
            if (!dt_EntryCheckSaddle.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_ID)].ToString().
                Equals(dt_EntryCheckSaddle.Rows[0][nameof(PDIEntity.TBL_PDI.Entry_Scaned_CoilID)].ToString()))
            {
                if (!dt_EntryCheckSaddle.Rows[0][nameof(PDIEntity.TBL_PDI.Entry_CoilID_Checked)].ToString().Trim().Equals("1"))
                {
                    strTip += "钢卷编号扫描确认未完成" + Environment.NewLine;
                }
            }
            
        }
        /// <summary>
        /// 判斷PDO鋼卷掃描結果
        /// </summary>
        private void Scan_PDO_Info_Check()
        {
            if (!dt_ExitCheckSaddle.IsNull())
            {
                if (!dt_ExitCheckSaddle.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString().
                    Equals(dt_ExitCheckSaddle.Rows[0][nameof(PDOEntity.TBL_PDO.Exit_Scaned_CoilID)].ToString()))
                {
                    if (!dt_ExitCheckSaddle.Rows[0][nameof(PDOEntity.TBL_PDO.Coil_Check_Result)].ToString().Trim().Equals("1"))
                    {
                        strTip += "钢卷编号扫描确认未完成" + Environment.NewLine;
                    }
                }
            }
            else if (dt_ExitCheckSaddle.Rows.Count.Equals(0))
            {
                strTip += "钢卷编号未扫描" + Environment.NewLine;
            }
        }

        /// <summary>
        /// PDO上传确认
        /// </summary>
        /// <param name="lb"></param>
        private void PDO_Uploaded_Flag()
        {
            if (!dt_ExitCheckSaddle.Rows.Count.Equals(0))
            {
                if (!dt_ExitCheckSaddle.Rows[0][nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)].ToString().Trim().Equals("1"))
                {
                    strTip += "PDO未上传" + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// ToolTip
        /// </summary>
        private void Label_ToolTip(Label lb)
        {
            lb.BackColor = strTip.IsEmpty() ? Color.Transparent : Color.Red;
            lb.ForeColor = strTip.IsEmpty() ? Color.Black : Color.White;
            //提示視窗類型
            Tip.ToolTipIcon = ToolTipIcon.Info;
            //標題
            Tip.ToolTipTitle = "提示";

            Tip.SetToolTip(lb, strTip);
        }
       

        #region dgv
        private void Dgv_offData()
        {
            //帶前十筆未上線鋼卷排程
            string strSql = SqlFactory.Frm_1_1_SelectSchedule_InitialDataGridView_DB_Schedule_PDI(10);

            dtOff = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "未上线钢卷资料","2-1");
            if (!bolDgv_off_ColumnShow)
            {
                DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_off, dtOff);
                DGVColumnsHandler.Instance.Frm_2_1TrackingDgv_OffColumns(Dgv_off);
                DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_off);
                bolDgv_off_ColumnShow = true;
            }
            if (!dtOff.IsNull())
            {
                DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_off, dtOff);
                DGVColumnsHandler.Instance.Frm_2_1TrackingDgv_OffColumns(Dgv_off);
                DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_off);
            }
        }
        private void Dgv_onData()
        {
            //帶線上鋼卷
            string strSql = SqlFactory.Frm_2_1_TrackingCoilInfo_DB_Map_PDI();
            
            //線上鋼卷詳細資料
            dtOn = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "线上钢卷资料", "2-1");
            if (!bolDgv_on_ColumnShow)
            {
                DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_on, dtOn);
                DGVColumnsHandler.Instance.Frm_2_1TrackingDgv_OnColumns(Dgv_on);
                DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_on);
                bolDgv_on_ColumnShow = true;
            }
            if (!dtOn.IsNull())
            {
                DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_on, dtOn);
                DGVColumnsHandler.Instance.Frm_2_1TrackingDgv_OnColumns(Dgv_on);
                DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_on);
            }
        }
        #endregion

        #region TabControl
        private void TabControl_DataGridView_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_GridControl, e);
        }
        #endregion

        #region 秤重
        private void Btn_DT21Weight_Click(object sender, EventArgs e)
        {

            if (!Lbl_DSK02_CoilNo.Text.IsEmpty())
            {
                Lbl_Wt_Coil.Text = "钢卷编号 : ";
                Pnl_Wt.Visible = true;
                Lbl_Wt_Coil.Text += Lbl_DSK02_CoilNo.Text;
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{ PublicForms.Main.lblLoginUser.Text.Trim()}开启输入毛重画面", $"输入钢卷编号:{Lbl_DSK02_CoilNo.Text.Trim() } 毛重");
                EventLogHandler.Instance.EventPush_Message($"输入钢卷编号:{Lbl_DSK02_CoilNo.Text.Trim() } 毛重");
                PublicComm.ClientLog.Info($"输入钢卷编号:{Lbl_DSK02_CoilNo.Text.Trim() } 毛重");
            }
            else
            {
                EventLogHandler.Instance.EventPush_Message($"鞍座上无钢卷");
                PublicComm.ClientLog.Info($"鞍座上无钢卷");
            }
        }
        private void Btn_Wt_Save_Click(object sender, EventArgs e)
        {
            Pnl_Wt.Visible = false;
        }
        private void Btn_WtCancel_Click(object sender, EventArgs e)
        {
            Pnl_Wt.Visible = false;
        }
        #endregion

        #region 退料
        private void Btn_ESK01Return_Click(object sender, EventArgs e)
        {
            DelReturn_TrackingMapColumns("ESK01", Lbl_ESK01_CoilNo.Text);
        }

        private void Btn_ETOPReturn_Click(object sender, EventArgs e)
        {
            DelReturn_TrackingMapColumns("ETOP", Lbl_ETOP_CoilNo.Text);
        }
        
        /// <summary>
        /// 退料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <param name="Saddle"></param>
        private void DelReturn_TrackingMapColumns(string Saddle, string Coil_ID)
        {
            if (Coil_ID.Trim().Equals(string.Empty))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"[{Saddle}]鞍座无钢卷可退料", "退料作业", null, 0);

                PublicComm.ClientLog.Info($"[{Saddle}]鞍座无钢卷可退料");
                return;
            }

            Frm_DialogReject _DialogReject = new Frm_DialogReject()
            {
                Coil = Coil_ID.Trim(),
                ParentCoil = Coil_ID.Trim(),
                SkidNumber = Saddle
            };

            _DialogReject.Fun_CoilReject();
            _DialogReject.ShowDialog();
            _DialogReject.Dispose();
            #region Old 移至  Frm_DialogReject
            //DelReturn_Coil_ID = ParentCoil_ID = Coil_ID;

            //if (Fun_CheackReject())
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk($"此钢卷已有退料记录，请重新确认!", "退料作业", null, 0);
            //    EventLogHandler.Instance.LogInfo("2-1", $"退料记录查询", $"钢卷号:{Coil_ID.Trim()}已有退料记录");
            //    EventLogHandler.Instance.EventPush_Message($"此钢卷已有退料记录，请重新确认!");
            //    PublicComm.ClientLog.Info($"[{Coil_ID.Trim()}]已有退料记录");
            //    return;
            //}

            ////查询是否有断带
            //string strSql = SqlFactory.Frm_2_1_CheckStripBrakeRecord(DelReturn_Coil_ID);
            //DataTable dtGetUnmountRecord = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "断带记录");

            //if (!dtGetUnmountRecord.Rows.Count.Equals(0))
            //{
            //    PublicComm.ClientLog.Info($"[{Coil_ID.Trim()}]有斷帶記錄");

            //    //有断带则给定新卷号
            //    ICoilController _coilController = new CoilController();
            //    DelReturn_Coil_ID = _coilController.GenSplitChildrenCoilNo(DelReturn_Coil_ID.Trim());
            //    Fun_RejectData(dtGetUnmountRecord,Saddle,true);
            //}
            //else
            //{
            //    PublicComm.ClientLog.Info($"[{Coil_ID.Trim()}]無斷帶記錄");

            //    strSql = SqlFactory.Frm_2_1_SelectReturnCoilPDI_DB_PDI(DelReturn_Coil_ID);
            //    DataTable dtGetRejectCoilData = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "退料钢卷资料");

            //    if (dtGetRejectCoilData.IsNull())
            //    {
            //        EventLogHandler.Instance.EventPush_Message($"[{Coil_ID.Trim()}]查無資料，请重新确认!");
            //        PublicComm.ClientLog.Debug($"[{Coil_ID.Trim()}]查無資料");
            //        return;
            //    }

            //    Fun_RejectData(dtGetRejectCoilData,Saddle,false);

            //}
            #endregion
        }
        #region Old 退料功能移至  Frm_DialogReject
        /// <summary>
        /// 檢查是否有退料紀錄
        /// </summary>
        private bool Fun_CheackReject()
        {
            bool bolRejectRecord = false;

            string strSql = SqlFactory.Frm_2_1_CheckRejectCoil_DB_TBL_CoilRejectResult(DelReturn_Coil_ID);
            DataTable dtGetRejectRecord = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "退料记录");

            if (!dtGetRejectRecord.Rows.Count.Equals(0)) 
                bolRejectRecord = true;

            //return false;
            return bolRejectRecord;
        }

        /// <summary>
        /// 退料钢卷资料
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Saddle"></param>
        /// <param name="bolBreak"></param>
        private void Fun_RejectData(DataTable dt, string Saddle, bool bolBreak)
        {
            //pnlReject.Visible = true;
            //pnlReject.Size = new Size(1280, 483);
            //pnlReject.Location = new Point(372, 375);

            //DelReturn_Saddle = Saddle;
            //lbSKID.Text = $"位置 : {DelReturn_Saddle}";
            //txtCoil_ID.Text = DelReturn_Coil_ID;

            //#region Columns

            ////計畫號
            //txtPlan_NO.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.Plan_No)].ToString() ?? string.Empty;
            ////卷長
            //txtRejectLen.Text = bolBreak ? dt.Rows[0][nameof(TBL_UnmountRecord.CoilLength)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Length)].ToString() ?? string.Empty;
            ////卷重
            //txtRejectWt.Text = bolBreak ? dt.Rows[0][nameof(TBL_UnmountRecord.CoilWeight)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Wt)].ToString() ?? string.Empty;
            ////內徑
            //txtRejectInner.Text = bolBreak ? dt.Rows[0][nameof(TBL_UnmountRecord.CoiInsideDiam)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)].ToString() ?? string.Empty;
            ////外徑
            //txtRejectOuter.Text = bolBreak ? dt.Rows[0][nameof(TBL_UnmountRecord.Diameter)].ToString() : dt.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)].ToString() ?? string.Empty;
            ////墊紙方式
            //cbo_paper_exit_code.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)].ToString().Trim() ?? string.Empty;
            ////墊紙類型
            //cbo_paper_exit_type.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.In_Paper_Code)].ToString().Trim() ?? string.Empty;

            //#region [Defect]

            //#region  - Code -
            //txtCode_D01.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Code)].ToString() ?? string.Empty;
            //txtCode_D02.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Code)].ToString() ?? string.Empty;
            //txtCode_D03.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Code)].ToString() ?? string.Empty;
            //txtCode_D04.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Code)].ToString() ?? string.Empty;
            //txtCode_D05.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Code)].ToString() ?? string.Empty;
            //txtCode_D06.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Code)].ToString() ?? string.Empty;
            //txtCode_D07.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Code)].ToString() ?? string.Empty;
            //txtCode_D08.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Code)].ToString() ?? string.Empty;
            //txtCode_D09.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Code)].ToString() ?? string.Empty;
            //txtCode_D10.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Code)].ToString() ?? string.Empty;
            //#endregion

            //#region - Origin -
            //txtOrigin_D01.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Origin)].ToString() ?? string.Empty;
            //txtOrigin_D02.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Origin)].ToString() ?? string.Empty;
            //txtOrigin_D03.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Origin)].ToString() ?? string.Empty;
            //txtOrigin_D04.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Origin)].ToString() ?? string.Empty;
            //txtOrigin_D05.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Origin)].ToString() ?? string.Empty;
            //txtOrigin_D06.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Origin)].ToString() ?? string.Empty;
            //txtOrigin_D07.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Origin)].ToString() ?? string.Empty;
            //txtOrigin_D08.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Origin)].ToString() ?? string.Empty;
            //txtOrigin_D09.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Origin)].ToString() ?? string.Empty;
            //txtOrigin_D10.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Origin)].ToString() ?? string.Empty;
            //#endregion

            //#region - Sid (ComboBox)-
            //cbo_Sid_D01.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Sid)].ToString().Trim() ?? string.Empty;
            //cbo_Sid_D02.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Sid)].ToString().Trim() ?? string.Empty;
            //cbo_Sid_D03.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Sid)].ToString().Trim() ?? string.Empty;
            //cbo_Sid_D04.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Sid)].ToString().Trim() ?? string.Empty;
            //cbo_Sid_D05.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Sid)].ToString().Trim() ?? string.Empty;
            //cbo_Sid_D06.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Sid)].ToString().Trim() ?? string.Empty;
            //cbo_Sid_D07.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Sid)].ToString().Trim() ?? string.Empty;
            //cbo_Sid_D08.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Sid)].ToString().Trim() ?? string.Empty;
            //cbo_Sid_D09.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Sid)].ToString().Trim() ?? string.Empty;
            //cbo_Sid_D10.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Sid)].ToString().Trim() ?? string.Empty;
            //#endregion

            //#region - Pos_W (ComboBox)-
            //cbo_PosW_D01.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Pos_W)].ToString().Trim() ?? string.Empty;
            //cbo_PosW_D02.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Pos_W)].ToString().Trim() ?? string.Empty;
            //cbo_PosW_D03.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Pos_W)].ToString().Trim() ?? string.Empty;
            //cbo_PosW_D04.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Pos_W)].ToString().Trim() ?? string.Empty;
            //cbo_PosW_D05.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Pos_W)].ToString().Trim() ?? string.Empty;
            //cbo_PosW_D06.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Pos_W)].ToString().Trim() ?? string.Empty;
            //cbo_PosW_D07.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Pos_W)].ToString().Trim() ?? string.Empty;
            //cbo_PosW_D08.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Pos_W)].ToString().Trim() ?? string.Empty;
            //cbo_PosW_D09.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Pos_W)].ToString().Trim() ?? string.Empty;
            //cbo_PosW_D10.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Pos_W)].ToString().Trim() ?? string.Empty;
            //#endregion

            //#region - Pos_L_Start - 
            //txtPos_L_Start_D01.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start)].ToString() ?? string.Empty;
            //txtPos_L_Start_D02.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start)].ToString() ?? string.Empty;
            //txtPos_L_Start_D03.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start)].ToString() ?? string.Empty;
            //txtPos_L_Start_D04.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start)].ToString() ?? string.Empty;
            //txtPos_L_Start_D05.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start)].ToString() ?? string.Empty;
            //txtPos_L_Start_D06.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start)].ToString() ?? string.Empty;
            //txtPos_L_Start_D07.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start)].ToString() ?? string.Empty;
            //txtPos_L_Start_D08.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start)].ToString() ?? string.Empty;
            //txtPos_L_Start_D09.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start)].ToString() ?? string.Empty;
            //txtPos_L_Start_D10.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start)].ToString() ?? string.Empty;
            //#endregion

            //#region - Pos_L_End -
            //txtPos_L_End_D01.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Pos_L_End)].ToString() ?? string.Empty;
            //txtPos_L_End_D02.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Pos_L_End)].ToString() ?? string.Empty;
            //txtPos_L_End_D03.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Pos_L_End)].ToString() ?? string.Empty;
            //txtPos_L_End_D04.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Pos_L_End)].ToString() ?? string.Empty;
            //txtPos_L_End_D05.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Pos_L_End)].ToString() ?? string.Empty;
            //txtPos_L_End_D06.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Pos_L_End)].ToString() ?? string.Empty;
            //txtPos_L_End_D07.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Pos_L_End)].ToString() ?? string.Empty;
            //txtPos_L_End_D08.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Pos_L_End)].ToString() ?? string.Empty;
            //txtPos_L_End_D09.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Pos_L_End)].ToString() ?? string.Empty;
            //txtPos_L_End_D10.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Pos_L_End)].ToString() ?? string.Empty;
            //#endregion

            //#region - Level (ComboBox)-
            //cbo_Level_D01.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Level)].ToString().Trim() ?? string.Empty;
            //cbo_Level_D02.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Level)].ToString().Trim() ?? string.Empty;
            //cbo_Level_D03.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Level)].ToString().Trim() ?? string.Empty;
            //cbo_Level_D04.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Level)].ToString().Trim() ?? string.Empty;
            //cbo_Level_D05.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Level)].ToString().Trim() ?? string.Empty;
            //cbo_Level_D06.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Level)].ToString().Trim() ?? string.Empty;
            //cbo_Level_D07.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Level)].ToString().Trim() ?? string.Empty;
            //cbo_Level_D08.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Level)].ToString().Trim() ?? string.Empty;
            //cbo_Level_D09.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Level)].ToString().Trim() ?? string.Empty;
            //cbo_Level_D10.SelectedValue = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Level)].ToString().Trim() ?? string.Empty;
            //#endregion

            //#region - Percent -
            //txtPercent_D01.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Percent)].ToString() ?? string.Empty;
            //txtPercent_D02.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Percent)].ToString() ?? string.Empty;
            //txtPercent_D03.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Percent)].ToString() ?? string.Empty;
            //txtPercent_D04.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Percent)].ToString() ?? string.Empty;
            //txtPercent_D05.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Percent)].ToString() ?? string.Empty;
            //txtPercent_D06.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Percent)].ToString() ?? string.Empty;
            //txtPercent_D07.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Percent)].ToString() ?? string.Empty;
            //txtPercent_D08.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Percent)].ToString() ?? string.Empty;
            //txtPercent_D09.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Percent)].ToString() ?? string.Empty;
            //txtPercent_D10.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Percent)].ToString() ?? string.Empty;
            //#endregion

            //#region - QGrade - 
            //txtQGRADE_D01.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D01_QGrade)].ToString() ?? string.Empty;
            //txtQGRADE_D02.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D02_QGrade)].ToString() ?? string.Empty;
            //txtQGRADE_D03.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D03_QGrade)].ToString() ?? string.Empty;
            //txtQGRADE_D04.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D04_QGrade)].ToString() ?? string.Empty;
            //txtQGRADE_D05.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D05_QGrade)].ToString() ?? string.Empty;
            //txtQGRADE_D06.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D06_QGrade)].ToString() ?? string.Empty;
            //txtQGRADE_D07.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D07_QGrade)].ToString() ?? string.Empty;
            //txtQGRADE_D08.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D08_QGrade)].ToString() ?? string.Empty;
            //txtQGRADE_D09.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D09_QGrade)].ToString() ?? string.Empty;
            //txtQGRADE_D10.Text = dt.Rows[0][nameof(PDIEntity.TBL_PDI.D10_QGrade)].ToString() ?? string.Empty;
            //#endregion

            //#endregion

            //#endregion

            ////判斷是否有切/斷自動帶入回退方式
            //// 1：無分切記錄，全卷退
            //// 2：有分切記錄，半卷退
            //cbo_Mode_Reject.SelectedValue = bolBreak && dt.Rows.Count > 0 ? "2" : "1";

        }

        /// <summary>
        /// 退料確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_del_Click(object sender, EventArgs e)
        {
            //string strSql = string.Empty;

            //if (cbo_ReturnCode.Text.IsEmpty())
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk($"请选择退料代码", "退料作业", null, 0);
            //    EventLogHandler.Instance.EventPush_Message($"请选择退料代码");
            //    PublicComm.ClientLog.Info($"请选择退料代码");
            //    return;
            //}
            //if (Fun_CheackReject())
            //{ 
            //    EventLogHandler.Instance.EventPush_Message($"此钢卷已有退料记录，请重新确认!");
            //    PublicComm.ClientLog.Info($"[{DelReturn_Coil_ID.Trim()}]已有退料紀錄");
            //    return;
            //}

            //string strShowStr = $"确定对安座[{ DelReturn_Saddle }]的钢卷[{DelReturn_Coil_ID.Trim()}]进行退料?";
           
            //DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel(strShowStr, $" 退料作業", null, 1); 
            ////MessageBox.Show(strShowStr, $" 退料作業", MessageBoxButtons.OKCancel);
            //if (dialogR.Equals(DialogResult.Cancel)) return;
       
            ////新增退料紀錄
            ////strSql = SqlFactory.Frm_2_1_InsertCoilReject_Record_DB_L3L2_TBL_ScheduleDelete_CoilReject_Record(DelReturn_Coil_ID, dtGroupNO.Rows[0][0].ToString());
            ////Fun_InsertUpdate(strSql);
            ////strSql = SqlFactory.Frm_2_1_InsertRejectResult_DB_TBL_CoilRejectResult(DelReturn_Coil_ID);
            ////Fun_InsertUpdate(strSql);

            ////分切記錄
            //if (!DelReturn_Coil_ID.Equals(ParentCoil_ID))
            //{
            //    strSql = SqlFactory.Frm_2_1_InsertSplitCoils(DelReturn_Coil_ID, ParentCoil_ID);
            //    Fun_InsertUpdate(strSql);
            //}

            ////退料中鋼卷記錄查詢及確認
            //Fun_CheckRejectCoilRecord(DelReturn_Coil_ID, DelReturn_Saddle, txtPlan_NO.Text);

            ////刷新MAP
            //try
            //{
            //    Fun_SelectTrackingMap();
            //}
            //catch (Exception ex)
            //{
            //    EventLogHandler.Instance.EventPush_Message($"钢卷追踪查询资料库失败:{ex}");
            //    EventLogHandler.Instance.LogDebug("2-1", $"钢卷追踪", $"钢卷追踪查询资料库失败:{ex}");
            //    PublicComm.ClientLog.Debug($"鋼卷追蹤查詢資料庫失敗:{ex}");
            //    PublicComm.ExceptionLog.Debug($"鋼卷追蹤查詢資料庫失敗:{ex}");
            //}

            ////線上鋼卷詳細資料
            //try
            //{
            //    DgoOnData();
            //}
            //catch (Exception ex)
            //{
            //    EventLogHandler.Instance.EventPush_Message($"线上钢卷资料查询资料库失败:{ex}");
            //    EventLogHandler.Instance.LogDebug( "2-1", $"线上钢卷资料", $"线上钢卷资料查询资料库失败:{ex}");
            //    PublicComm.ClientLog.Debug($"線上鋼卷查詢資料庫失敗:{ex}");
            //    PublicComm.ExceptionLog.Debug($"線上鋼卷查詢資料庫失敗:{ex}");
            //}

            //PublicComm.ClientLog.Info($"退料作業完成");
            //EventLogHandler.Instance.EventPush_Message($"退料作业完成!");
            //EventLogHandler.Instance.LogInfo("2-1", $"使用者:{ PublicForms.Main.lblLoginUser.Text.Trim()}确认退料作业", $"{lbSKID.Text.Trim()}退料钢卷编号:{DelReturn_Coil_ID.Trim()} 原因代码:{cbo_ReturnCode.Text.Trim()}");
           
            //pnlReject.Visible = false;
            
        }

        /// <summary>
        /// 退料中記錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <param name="Saddle"></param>
        private void Fun_CheckRejectCoilRecord(string Coil_ID, string Saddle,string strPlanNo)
        {
            //string strSql = string.Empty;

            ////檢查退料中鋼卷記錄
            //strSql = SqlFactory.Frm_2_1_SelectReturnCoilRecord_DB_TBL_RetrunCoil(Coil_ID, strPlanNo);
            //DataTable dtCheckReturnCoil = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "退料中钢卷记录");

            ////沒記錄Insert ; 有記錄Update時間
            //strSql = dtCheckReturnCoil.IsNull() ? SqlFactory.Frm_2_1_InsertReturnCoilRecord_DB_TBL_RetrunCoil(Coil_ID, strPlanNo) :SqlFactory.Frm_2_1_UpdateReturnCoilRecordCreateTime_DB_TBL_RetrunCoil(Coil_ID, strPlanNo);

            //bool bolSaveOk = Fun_InsertUpdate(strSql);

            //string strMov = dtCheckReturnCoil.IsNull() ? "新增" : "修改";
            //string strResult;
            //if (bolSaveOk)
            //    strResult = "成功";
            //else           
            //    strResult = "失败";
            
            //EventLogHandler.Instance.LogDebug("2-1", $"{strMov}资料库", $"{strMov}退料记录 Table[TBL_RetrunCoil_Temp]，{strResult}");
            //PublicComm.ClientLog.Info($"{strMov}退料记录 Table[TBL_RetrunCoil_Temp]，{strResult}");


            ////通知Server退料作業
            //SCCommMsg.CS05_RejectCoil _RejectCoil = new SCCommMsg.CS05_RejectCoil
            //{
            //    Source = "GPL_HMI",
            //    ID = "RejectCoil",
            //    CoilID = Coil_ID.Trim(),
            //    Saddle = Saddle
            //};
            //PublicComm.Client.Tell(_RejectCoil);           

            //PublicComm.ClientLog.Info($"钢卷编号:[{Coil_ID.Trim()}] 鞍座:[{Saddle}]退料:");
            //PublicComm.ClientLog.Info($"通知Server讯息:SCCommMsg.CS05_RejectCoil");
            //PublicComm.ClientLog.Info($"--Start--");
            //PublicComm.ClientLog.Info($"{nameof(_RejectCoil.Source)} = {_RejectCoil.Source} ");
            //PublicComm.ClientLog.Info($"{nameof(_RejectCoil.ID)} = {_RejectCoil.ID} ");
            //PublicComm.ClientLog.Info($"{nameof(_RejectCoil.CoilID)}  = {_RejectCoil.CoilID} ");
            //PublicComm.ClientLog.Info($"{nameof(_RejectCoil.Saddle)}  = {_RejectCoil.Saddle} ");
            //PublicComm.ClientLog.Info($"--End--");

            //DialogHandler.Instance.Fun_DialogShowOk($"已通知Server钢卷编号:[{Coil_ID.Trim()}] 鞍座:[{Saddle}]退料", "退料作业", null, 4);

            //PublicComm.ClientLog.Info($"已通知Server钢卷编号:[{Coil_ID.Trim()}] 鞍座:[{Saddle}]退料!");
            //EventLogHandler.Instance.LogInfo("2-1", "通知Server退料实绩", $"退料实绩 钢卷编号:{ Coil_ID.Trim() } 鞍座位置: {Saddle}");

            //EventLogHandler.Instance.EventPush_Message($"已通知Server钢卷编号:[{Coil_ID.Trim()}] 鞍座:[{Saddle}]退料");
          
        }

        /// <summary>
        /// 取消退料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            //pnlReject.Visible = false;
        }
        #endregion Old 退料功能移至  Frm_DialogReject
        #endregion 退料

        #region 出料
        private void Btn_DSK01_CoilOut_Click(object sender, EventArgs e)
        {
            DeliveryCoilOut(Lbl_DSK01_CoilNo.Text,SCCommMsg.CoilSkPosition.DSK01);
        }

        private void Btn_DSK02_CoilOut_Click(object sender, EventArgs e)
        {
            DeliveryCoilOut(Lbl_DSK02_CoilNo.Text, SCCommMsg.CoilSkPosition.DSK02);
        }

        private void Btn_DTOP_CoilOut_Click(object sender, EventArgs e)
        {
            DeliveryCoilOut(Lbl_DTOP_CoilNo.Text, SCCommMsg.CoilSkPosition.DTOP);
        }

        private void DeliveryCoilOut(string DeliveryCoil,SCCommMsg.CoilSkPosition SkidPosition)
        {
            if (!DeliveryCoil.IsEmpty())
            {
                SCCommMsg.CS14_DeliveryCoilOut DeliveryCoilOut = new SCCommMsg.CS14_DeliveryCoilOut
                {
                    CoilID = DeliveryCoil,
                    CoilPosition = SkidPosition
                };
                PublicComm.Client.Tell(DeliveryCoilOut);

                DialogHandler.Instance.Fun_DialogShowOk($"已通知Server出口端[{SkidPosition}]{ DeliveryCoil.Trim()}出料完成!", $"出口端天车出料", null, 0);

                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}出口端出料", $"出口端[{SkidPosition}]{ DeliveryCoil.Trim()}出料");
                EventLogHandler.Instance.EventPush_Message($"已通知Server出口端[{SkidPosition}]{ DeliveryCoil.Trim()}出料完成!");
                PublicComm.ClientLog.Info($"通知Server出口端[{SkidPosition}]{ DeliveryCoil.Trim()}出料完成");
                PublicComm.akkaLog.Info($"通知Server出口端[{SkidPosition}]{ DeliveryCoil.Trim()}出料完成");
            }
            else
            {
                DialogHandler.Instance.Fun_DialogShowOk($"目前该鞍座尚无钢卷!", $"出口端天车出料", null,2);

                EventLogHandler.Instance.EventPush_Message($"目前该鞍座尚无钢卷!");
                PublicComm.ClientLog.Info($"目前出口端[{SkidPosition}]無鋼卷");
            }
        }
        #endregion

        #region 自動入料
        private void Btn_AutoCoilFeed_Click(object sender, EventArgs e)
        {
            string strSql;

            Fun_SelectSystemSetting();

            if (dtStatus.IsNull()) return;

            if (dtStatus.Rows[0][nameof(TBL_SystemSetting.Value)].ToString().Equals("0"))
            {
                Lbl_AutoCoilFeed.Text = "Auto";
                strSql = SqlFactory.Frm_2_1_AutoFeedStatus_DB_TBL_SystemSetting(AutoFeedStatus.Auto);
                Fun_InsertUpdate(strSql);
                //開始進料
                Btn_ManualFeed.Visible = false;
            }
            else if (dtStatus.Rows[0][nameof(TBL_SystemSetting.Value)].ToString().Equals("1"))
            {
                Lbl_AutoCoilFeed.Text = "Manual";
                strSql = SqlFactory.Frm_2_1_AutoFeedStatus_DB_TBL_SystemSetting(AutoFeedStatus.Manual);
                Fun_InsertUpdate(strSql);
                //結束進料
                Btn_ManualFeed.Visible = true;
            }
            EventLogHandler.Instance.LogDebug("2-1", "修改资料库", $"修改自动进料状态 Table[TBL_SystemSetting]，成功 状态更变:{Lbl_AutoCoilFeed.Text.Trim()}");
            EventLogHandler.Instance.EventPush_Message($"自动进料状态更变为:{Lbl_AutoCoilFeed.Text.Trim()}");
            PublicComm.ClientLog.Info($"目前進料狀態為:{Lbl_AutoCoilFeed.Text.Trim()}");
            SCCommMsg.CS10_Coil_AutoFeedModeChange _AutoFeedMode = new SCCommMsg.CS10_Coil_AutoFeedModeChange
            {
                Source = "GPL_HMI",
                ID = "AutoFeedModeChange"
            };
            PublicComm.Client.Tell(_AutoFeedMode);
            EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}变更自动入料状态", $"通知Server自动入料状态变更为 :{Lbl_AutoCoilFeed.Text.Trim()}");
            EventLogHandler.Instance.EventPush_Message($"通知Server自动进料状态变更为:{Lbl_AutoCoilFeed.Text.Trim()}");
            PublicComm.ClientLog.Info($"通知Server進料狀態變更為:{Lbl_AutoCoilFeed.Text.Trim()}");
            PublicComm.akkaLog.Info($"通知Server進料狀態變更為:{Lbl_AutoCoilFeed.Text.Trim()}");
        }

        /// <summary>
        /// 系统设定值
        /// </summary>
        private void Fun_SelectSystemSetting()
        {
            string strSql = SqlFactory.Frm_2_1_SelectAutoFeedStatus_DB_TBL_SystemSetting();
            try
            {
                dtStatus = Data_Access.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_GPL);
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"2-1系统设定值查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("2-1", $"2-1系统设定值", $"2-1系统设定值查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"2-1載入系統設定值查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"2-1載入系統設定值查詢資料庫失敗:{ex}");
            }

            if (dtStatus.IsNull()) return;

            Lbl_AutoCoilFeed.Text = dtStatus.Rows[0][nameof(TBL_SystemSetting.Value)].ToString().Equals("1") ? "Auto" : "Manual";

            Btn_ManualFeed.Visible = !dtStatus.Rows[0][nameof(TBL_SystemSetting.Value)].ToString().Equals("1");
        }

        private bool Fun_InsertUpdate(string strSql)
        {
            bool bolSaveOk;
            try
            {
                bolSaveOk = Data_Access.GetInstance().ExecuteNonQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL);
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"异动资料库资料错误:{ex}");
                EventLogHandler.Instance.LogDebug("2-1", $"异动资料库资料", $"异动资料库资料错误:{ex}");
                PublicComm.ClientLog.Debug($"異動資料庫資料失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"異動資料庫資料失敗:{ex}");

                bolSaveOk = false;
            }
            return bolSaveOk;
        }

        #endregion

        #region PDO確認畫面
        private void Btn_PDO_Click(object sender, EventArgs e)
        {
            //frm_PDOConfirm frm_PDO = new frm_PDOConfirm();
            //frm_PDO.Show();
        }
        #endregion
         
        private void Dgv_off_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (Dgv_off.DgvIsNull())
            {
                return;
            }

            for (int RowIndex = 0; RowIndex < dtOff.Rows.Count; RowIndex++)
            {
                if (RowIndex <= 2)
                {
                    Dgv_off.Rows[RowIndex].DefaultCellStyle.BackColor = Color.DimGray;
                }
                Dgv_off.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void Btn_ManualFeed_Click(object sender, EventArgs e)
        {
            if (Lbl_ETOP_CoilNo.Text.IsEmpty())
            {
                //手動進料
                SCCommMsg.CS11_Coil_ManualFeed _ManualFeed = new SCCommMsg.CS11_Coil_ManualFeed
                {
                    Source = "GPL_HMI",
                    CoilID = dtOff.Rows[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].ToString().Trim()
                };

                PublicComm.Client.Tell(_ManualFeed);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"已通知Server告知WMS进料，");
                sb.AppendLine($"钢卷编号【{dtOff.Rows[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].ToString().Trim()}】");

                DialogHandler.Instance.Fun_DialogShowOk(sb.ToString(), "手动入料",null, 4);               
               
                EventLogHandler.Instance.EventPush_Message(sb.ToString());
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{ PublicForms.Main.lblLoginUser.Text.Trim()} 手动入料", $"已通知Server告知WMS进料，入料钢卷编号:{ dtOff.Rows[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].ToString().Trim()}");
                PublicComm.ClientLog.Info($"已通知Server告知WMS进料，钢卷编号【{dtOff.Rows[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].ToString().Trim()}】");
            }
            else
            {
                EventLogHandler.Instance.EventPush_Message($"目前鞍座无空间可供进料");
                PublicComm.ClientLog.Info($"鞍座無空位可進料");
            }
        }

        private void Btn_ESK01Entry_Click(object sender, EventArgs e)
        {
            if (Lbl_ESK01_CoilNo.Text.IsEmpty()) 
                EntryCoil(SCCommMsg.TrkSKId.Name.SKNo1);
            else
            {
                EventLogHandler.Instance.EventPush_Message($"ESK01鞍座尚有鋼卷");
                PublicComm.ClientLog.Info($"ESK01鞍座尚有鋼卷不可進料");
            }
        }

        /// <summary>
        /// 開啟入料畫面
        /// </summary>
        /// <param name="Skid"></param>
        private void EntryCoil(string Skid)
        {
            EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 开启入料画面", $"开启{Skid }入料画面");
            frm_Entry _Entry = new frm_Entry
            {
                Skid = Skid
            };

            _Entry.Show();
        }

        #region "Print Lable"

        #region "入口端列印標籤"
        private void Btn_ESK01_PrintLabel_Click(object sender, EventArgs e)
        {
            Fun_PrintTag(Lbl_ESK01_CoilNo.Text.Trim());
        }
        private void Btn_ETOP_PrintLabel_Click(object sender, EventArgs e)
        {
            Fun_PrintTag(Lbl_ETOP_CoilNo.Text.Trim());
        }
        #endregion

        #region "出口端列印標籤"
        private void Btn_DT21PrintLabel_Click(object sender, EventArgs e)
        {
            Fun_PrintTag(Lbl_DSK01_CoilNo.Text.Trim());

        }
        private void Btn_DT22PrintLabel_Click(object sender, EventArgs e)
        {
            Fun_PrintTag(Lbl_DSK02_CoilNo.Text.Trim());

        }
        private void Btn_DT23PrintLabel_Click(object sender, EventArgs e)
        {
            Fun_PrintTag(Lbl_DTOP_CoilNo.Text.Trim());
        }
        #endregion

        /// <summary>
        /// 列印標籤
        /// </summary>
        /// <param name="Coil_ID"></param>
        private void Fun_PrintTag(string Coil_ID)
        {
            if (!Coil_ID.IsEmpty())
            {
                if (dt.Rows.Count > 0)
                {
                    Frm_PrintLabels frm_Print = new Frm_PrintLabels
                    {
                        Str_Coil_No = Coil_ID.Trim()
                    };
                    frm_Print.ShowDialog();
                    frm_Print.Dispose();

                    if (frm_Print.DialogResult == DialogResult.OK)
                    {
                        string strShowText = $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 打印钢卷号:[{frm_Print.Str_Coil_No}]标签";
                        EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 打印标签作业", $"打印钢卷号:[{frm_Print.Str_Coil_No}]标签");
                        EventLogHandler.Instance.EventPush_Message(strShowText);
                        PublicComm.ClientLog.Info(strShowText);

                    }

                    //SCCommMsg.CS07_PrintLabel _PrintLabel = new SCCommMsg.CS07_PrintLabel
                    //{
                    //    Source = "GPL_HMI",
                    //    ID = "PrintLabel",
                    //    CoilID = Coil_ID
                    //};
                    //PublicComm.Client.Tell(_PrintLabel);
                    //EventLogHandler.Instance.LogInfo( "2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}打印标签作业", $"打印{Coil_ID.Trim()}标签");
                    //EventLogHandler.Instance.EventPush_Message($"列印[{Coil_ID.Trim()}]标签");
                    //PublicComm.ClientLog.Info($"通知Server列印鋼卷號:[{Coil_ID.Trim()}]標籤");

                }
                else
                {
                    EventLogHandler.Instance.EventPush_Message($"尚无钢卷可列印");
                    PublicComm.ClientLog.Info($"尚无钢卷可列印");
                }
            }
            else
            {
                EventLogHandler.Instance.EventPush_Message($"尚无钢卷可列印");
                PublicComm.ClientLog.Info($"尚无钢卷可列印");
            }
        }
        #endregion

        #region "刪除鞍座鋼卷"

        #region '入口端'
        private void Btn_ESK01_Del_Click(object sender, EventArgs e)
        {
            Fun_DeleteSkidCoilId(Lbl_ESK01_CoilNo.Text,nameof(TBL_CoilMap.Entry_SK01));
        }

        private void Btn_ETOP_Del_Click(object sender, EventArgs e)
        {
            Fun_DeleteSkidCoilId(Lbl_ETOP_CoilNo.Text, nameof(TBL_CoilMap.Entry_TOP));
        }
        private void Btn_PORReturn_Click(object sender, EventArgs e)
        {
            Fun_DeleteSkidCoilId(Lbl_POR_CoilNo.Text, nameof(TBL_CoilMap.POR));
        }
        #endregion

        #region '出口端'
        private void Btn_DSK01_Del_Click(object sender, EventArgs e)
        {
            Fun_DeleteSkidCoilId(Lbl_DSK01_CoilNo.Text, nameof(TBL_CoilMap.Delivery_SK01));
        }

        private void Btn_DSK02_Del_Click(object sender, EventArgs e)
        {
            Fun_DeleteSkidCoilId(Lbl_DSK02_CoilNo.Text, nameof(TBL_CoilMap.Delivery_SK02));
        }

        private void Btn_DTOP_Del_Click(object sender, EventArgs e)
        {
            Fun_DeleteSkidCoilId(Lbl_DTOP_CoilNo.Text, nameof(TBL_CoilMap.Delivery_TOP));
        }
        #endregion

        /// <summary>
        /// 刪除鞍座鋼卷號
        /// </summary>
        /// <param name="Coil_ID"></param>
        private void Fun_DeleteSkidCoilId(string DelCoil_ID,string Skid)
        {
            string strSql;

            if (!DelCoil_ID.IsEmpty())
            {
                #region [Postion]
                int pos = 0;
                switch (Skid)
                {
                    case nameof(TBL_CoilMap.Entry_Car):
                        pos = 4;
                        break;
                    case nameof(TBL_CoilMap.Entry_SK01):
                        pos = 2;
                        break;
                    case nameof(TBL_CoilMap.Entry_TOP):
                        pos = 3;
                        break;
                    case nameof(TBL_CoilMap.POR):
                        pos = 1;
                        break;
                    case nameof(TBL_CoilMap.TR):
                        pos = 5;
                        break;
                    case nameof(TBL_CoilMap.Delivery_SK01):
                        pos = 6;
                        break;
                    case nameof(TBL_CoilMap.Delivery_SK02):
                        pos = 7;
                        break;
                    case nameof(TBL_CoilMap.Delivery_TOP):
                        pos = 8;
                        break;
                    case nameof(TBL_CoilMap.Delivery_Car):
                        pos = 9;
                        break;
                    default:
                        break;
                }
                #endregion

                string strMessage = $"是否删除鞍座[{Skid}]钢卷?";

                DialogResult dialog = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "删", Properties.Resources.dialogQuestion, 1);

                if (dialog.Equals(DialogResult.OK))
                {
                    //strSql = SqlFactory.Frm_2_1_DeleteSkidCoil_DB_TBL_CoilMap(DelCoil_ID, Skid);

                    //if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "删除鞍座钢卷号", "2-1"))
                    //{
                    //    EventLogHandler.Instance.EventPush_Message($"删除鞍座钢卷号错误");
                    //    return;
                    //}

                    SCCommMsg.CS13_DeleteSidCoil DeleteSkidCoil = new SCCommMsg.CS13_DeleteSidCoil
                    {
                        DelPos = short.Parse(pos.ToString()),
                        Coil_ID = DelCoil_ID
                    };

                    PublicComm.Client.Tell(DeleteSkidCoil);
                    EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}删除鞍座钢卷号", $"删除[{Skid}]钢卷号{DelCoil_ID.Trim()}");
                    EventLogHandler.Instance.EventPush_Message($"通知Server删除[{Skid}]钢卷号:[{DelCoil_ID.Trim()}]");
                    PublicComm.ClientLog.Info($"通知Server刪除鞍座:[{Skid}]上鋼卷[{DelCoil_ID.Trim()}]");
                }
            }
            else
            {
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}删除鞍座钢卷号", $"目前该鞍座尚无钢卷");
                EventLogHandler.Instance.EventPush_Message($"目前该鞍座尚无钢卷");
                PublicComm.ClientLog.Info($"鞍座上無鋼卷可刪除");
            }
        }

        #endregion

        /// <summary>
        /// TrackingMap刷新 5秒一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_TrackingMap_Tick(object sender, EventArgs e)
        {
            //Tracking Map
            try
            {
                Fun_SelectTrackingMap();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"钢卷追踪查询资料库失败(TrackingMapTimer_Tick):{ex}");
                EventLogHandler.Instance.LogDebug("2-1", $"钢卷追踪", $"钢卷追踪查询资料库失败(TrackingMapTimer_Tick):{ex}");
                PublicComm.ClientLog.Debug($"钢卷追踪查询资料库失败(TrackingMapTimer_Tick):{ex}");
                PublicComm.ExceptionLog.Debug($"钢卷追踪查询资料库失败(TrackingMapTimer_Tick):{ex}");
            }

            Fun_SkidChangeColor();

            //取得連線狀態
            try
            {
                Fun_SelectNetWorkStatus();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug("2-1", $"2-1连线状态", $"2-1连线状态查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"2-1连线状态查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"2-1连线状态查詢資料庫失敗:{ex}");
            }

            //GR資料
            try
            {
                Fun_SelectGR_Data();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug("2-1", $"2-1研磨机资料", $"2-1研磨机资料查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"2-1研磨機資料查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"2-1研磨機資料查詢資料庫失敗:{ex}");
            }
        }              

        /// <summary>
        /// 6台研磨機資料，L1每5秒上拋給L2
        /// </summary>
        private void Fun_SelectGR_Data()
        {
            //皮帶資訊 TBL_Belts
            string strSql = SqlFactory.Frm_2_1_SelectGR_Belt_DB_TBL_Belt();
            DataTable dtGetGR_Belt = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "研磨机皮帶资料", "2-1");

            if (dtGetGR_Belt.IsNull())
            {
                #region 編號 里程 Text = string.Empty
                //編號
                Lbl_GR_1_BeltNo.Text = string.Empty;
                Lbl_GR_2_BeltNo.Text = string.Empty;
                Lbl_GR_3_BeltNo.Text = string.Empty;
                Lbl_GR_4_BeltNo.Text = string.Empty;
                Lbl_GR_5_BeltNo.Text = string.Empty;
                Lbl_GR_6_BeltNo.Text = string.Empty;
                //里程
                Lbl_GR_1_GrindLength.Text = string.Empty;
                Lbl_GR_2_GrindLength.Text = string.Empty;
                Lbl_GR_3_GrindLength.Text = string.Empty;
                Lbl_GR_4_GrindLength.Text = string.Empty;
                Lbl_GR_5_GrindLength.Text = string.Empty;
                Lbl_GR_6_GrindLength.Text = string.Empty;
                #endregion
            }
            else
            {
                #region 編號 里程 Text = dtGetGR_Belt.Rows
                //編號
                Lbl_GR_1_BeltNo.Text = dtGetGR_Belt.Rows.Count > 0 ? dtGetGR_Belt.Rows[0][nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty : string.Empty;
                Lbl_GR_2_BeltNo.Text = dtGetGR_Belt.Rows.Count > 1 ? dtGetGR_Belt.Rows[1][nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty: string.Empty;
                Lbl_GR_3_BeltNo.Text = dtGetGR_Belt.Rows.Count > 2 ? dtGetGR_Belt.Rows[2][nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty: string.Empty;
                Lbl_GR_4_BeltNo.Text = dtGetGR_Belt.Rows.Count > 3 ? dtGetGR_Belt.Rows[3][nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty: string.Empty;
                Lbl_GR_5_BeltNo.Text = dtGetGR_Belt.Rows.Count > 4 ? dtGetGR_Belt.Rows[4][nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty: string.Empty;
                Lbl_GR_6_BeltNo.Text = dtGetGR_Belt.Rows.Count > 5 ? dtGetGR_Belt.Rows[5][nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty: string.Empty;
                //里程
                Lbl_GR_1_GrindLength.Text = Fun_GetGrindLength(dtGetGR_Belt, 0);
                Lbl_GR_2_GrindLength.Text = Fun_GetGrindLength(dtGetGR_Belt, 1);
                Lbl_GR_3_GrindLength.Text = Fun_GetGrindLength(dtGetGR_Belt, 2);
                Lbl_GR_4_GrindLength.Text = Fun_GetGrindLength(dtGetGR_Belt, 3);
                Lbl_GR_5_GrindLength.Text = Fun_GetGrindLength(dtGetGR_Belt, 4);
                Lbl_GR_6_GrindLength.Text = Fun_GetGrindLength(dtGetGR_Belt, 5);
                #endregion
            }

            //研磨紀錄 TBL_GrindRecords
            strSql = SqlFactory.Frm_2_1_SelectGrindRecords_DB_TBL_GrindRecords();
            DataTable dtGetGrindRecords = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "研磨机PresetData", "2-1");

            if (dtGetGrindRecords.IsNull()) 
            {
                #region GR_1 ~ GR_6  Text = string.Empty       
                //皮帶種類
                Lbl_GR_1_BeltKind.Text = string.Empty;
                Lbl_GR_2_BeltKind.Text = string.Empty;
                Lbl_GR_3_BeltKind.Text = string.Empty;
                Lbl_GR_4_BeltKind.Text = string.Empty;
                Lbl_GR_5_BeltKind.Text = string.Empty;
                Lbl_GR_6_BeltKind.Text = string.Empty;
                //号数
                Lbl_GR_1_ParticleNo.Text = string.Empty;
                Lbl_GR_2_ParticleNo.Text = string.Empty;
                Lbl_GR_3_ParticleNo.Text = string.Empty;
                Lbl_GR_4_ParticleNo.Text = string.Empty;
                Lbl_GR_5_ParticleNo.Text = string.Empty;
                Lbl_GR_6_ParticleNo.Text = string.Empty;
                //方向
                Lbl_GR_1_Dir.Text = string.Empty;
                Lbl_GR_2_Dir.Text = string.Empty;
                Lbl_GR_3_Dir.Text = string.Empty;
                Lbl_GR_4_Dir.Text = string.Empty;
                Lbl_GR_5_Dir.Text = string.Empty;
                Lbl_GR_6_Dir.Text = string.Empty;
                //电流(A)
                Lbl_GR_1_Current.Text = string.Empty;
                Lbl_GR_2_Current.Text = string.Empty;
                Lbl_GR_3_Current.Text = string.Empty;
                Lbl_GR_4_Current.Text = string.Empty;
                Lbl_GR_5_Current.Text = string.Empty;
                Lbl_GR_6_Current.Text = string.Empty;
                //速度(mpm)
                Lbl_GR_1_Speed.Text = string.Empty;
                Lbl_GR_2_Speed.Text = string.Empty;
                #endregion
            }
            else
            {
                #region GR_1 ~ GR_6 Text = dtGetGrindRecords.Rows        
                //皮帶種類
                Lbl_GR_1_BeltKind.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR1_BELT_KIND)].ToString() ?? string.Empty;
                Lbl_GR_2_BeltKind.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR2_BELT_KIND)].ToString() ?? string.Empty;
                Lbl_GR_3_BeltKind.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR3_BELT_KIND)].ToString() ?? string.Empty;
                Lbl_GR_4_BeltKind.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR4_BELT_KIND)].ToString() ?? string.Empty;
                Lbl_GR_5_BeltKind.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR5_BELT_KIND)].ToString() ?? string.Empty;
                Lbl_GR_6_BeltKind.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR6_BELT_KIND)].ToString() ?? string.Empty;
                //号数
                Lbl_GR_1_ParticleNo.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR1_BELT_PARTICLE_NO)].ToString() ?? string.Empty;
                Lbl_GR_2_ParticleNo.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR2_BELT_PARTICLE_NO)].ToString() ?? string.Empty;
                Lbl_GR_3_ParticleNo.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR3_BELT_PARTICLE_NO)].ToString() ?? string.Empty;
                Lbl_GR_4_ParticleNo.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR4_BELT_PARTICLE_NO)].ToString() ?? string.Empty;
                Lbl_GR_5_ParticleNo.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR5_BELT_PARTICLE_NO)].ToString() ?? string.Empty;
                Lbl_GR_6_ParticleNo.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR6_BELT_PARTICLE_NO)].ToString() ?? string.Empty;
                //方向
                GR_RotateDirection(dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR1_BELT_ROTATE_DIR)].ToString(), Lbl_GR_1_Dir);
                GR_RotateDirection(dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR2_BELT_ROTATE_DIR)].ToString(), Lbl_GR_2_Dir);
                GR_RotateDirection(dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR3_BELT_ROTATE_DIR)].ToString(), Lbl_GR_3_Dir);
                GR_RotateDirection(dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR4_BELT_ROTATE_DIR)].ToString(), Lbl_GR_4_Dir);
                GR_RotateDirection(dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR5_BELT_ROTATE_DIR)].ToString(), Lbl_GR_5_Dir);
                GR_RotateDirection(dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR6_BELT_ROTATE_DIR)].ToString(), Lbl_GR_6_Dir);
                //电流(A)
                Lbl_GR_1_Current.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR1_MOTOR_CURRENT)].ToString() ?? "0";
                Lbl_GR_2_Current.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR2_MOTOR_CURRENT)].ToString() ?? "0";
                Lbl_GR_3_Current.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR3_MOTOR_CURRENT)].ToString() ?? "0";
                Lbl_GR_4_Current.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR4_MOTOR_CURRENT)].ToString() ?? "0";
                Lbl_GR_5_Current.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR5_MOTOR_CURRENT)].ToString() ?? "0";
                Lbl_GR_6_Current.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR6_MOTOR_CURRENT)].ToString() ?? "0";
                //速度(mpm)
                Lbl_GR_1_Speed.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR1_BELT_SPEED)].ToString() ?? "0";
                Lbl_GR_2_Speed.Text = dtGetGrindRecords.Rows[0][nameof(TBL_GrindRecords.GR2_BELT_SPEED)].ToString() ?? "0";   
                #endregion               
            }
            
            string Coil_ID = dtGetGrindRecords.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.Coil_ID)].ToString();
            //钢卷编号
            Lbl_GrindCoilID.Text = Coil_ID.Trim();

            string strCurrent_Senssion = dtGetGrindRecords.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)].ToString();

            #region 研磨区域 (研磨位置) strCurrent_Senssion 轉換文字
            //研磨区域 (研磨位置)
            switch (strCurrent_Senssion)
            {
                case "1":
                    Lbl_Pass_Section.Text = "头段";
                    break;
                case "2":
                    Lbl_Pass_Section.Text = "中段";
                    break;
                case "3":
                    Lbl_Pass_Section.Text = "尾段";
                    break;
                default:
                    Lbl_Pass_Section.Text = strCurrent_Senssion;
                    break;
            }
            #endregion

            //TBL_GrindPlanHistory [Pass_Section](研磨位置 H-頭部;M-中部;T-尾部;)

            string strPass_Section = strCurrent_Senssion == "1" ? "H" : strCurrent_Senssion == "2" ? "M" : strCurrent_Senssion == "3" ? "T" : string.Empty;

            //歷史研磨計畫 TBL_GrindPlanHistory
            strSql = SqlFactory.Frm_2_1_SelectGrindRecords_DB_TBL_GrindPlanHistory(Coil_ID, strPass_Section);
            DataTable dtGetGrindPassNum = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "研磨总道次", "2-1");
                      
            #region 目前道次/總道次(Now / Total)            

            //當前道次
            string strCurrentPass = !dtGetGrindRecords.IsNull() ? !dtGetGrindRecords.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)].ToString().IsEmpty() ? 
                dtGetGrindRecords.Rows[0][nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)].ToString().Trim() : "0" : string.Empty;
            
            //總道次
            string strPassNumber = !dtGetGrindPassNum.IsNull() ? !dtGetGrindPassNum.Rows[0][nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory.PassNumber)].ToString().IsEmpty() ?
                dtGetGrindPassNum.Rows[0][nameof(GrindPlanHistoryEntity.TBL_GrindPlanHistory.PassNumber)].ToString().Trim() : "0" : string.Empty;

            //道次(Now/Total)
            Lbl_PassNumber.Text = $"{ strCurrentPass } / { strPassNumber }";

            #endregion      
        }
        /// <summary>
        /// 皮帶查詢表 取得 里程(Belt/Strip)
        /// </summary>
        /// <param name="dt_Belt"></param>
        /// <param name="intRow"></param>
        /// <returns></returns>
        private string Fun_GetGrindLength(DataTable dt_Belt, int intRow)
        {
            string strGrindLength = "";

            decimal dec_GrindStrip;
            decimal dec_GrindBelt;
            if (dt_Belt != null && dt_Belt.Rows.Count > intRow)
            {
                if (!dt_Belt.Rows[intRow][nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Belt)].ToString().Equals(null))
                {
                    try
                    {
                        dec_GrindBelt = Math.Round(Convert.ToDecimal(dt_Belt.Rows[intRow][nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Belt)].ToString()), 2);
                    }
                    catch
                    {
                        dec_GrindBelt = 0;
                    }
                }
                else
                {
                    dec_GrindBelt = 0;
                }

                if (!dt_Belt.Rows[intRow][nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Strip)].ToString().Equals(null))
                {
                    try
                    {
                        dec_GrindStrip = Math.Round(Convert.ToDecimal(dt_Belt.Rows[intRow][nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Strip)].ToString()), 2);
                    }
                    catch
                    {
                        dec_GrindStrip = 0;
                    }
                }
                else
                {
                    dec_GrindStrip = 0;
                }
                strGrindLength = $"{dec_GrindBelt}/{dec_GrindStrip}";
            }

            return strGrindLength;
        }

        /// <summary>
        /// 研磨機研磨方向
        /// 0 : FWD(正轉) 1 : REV(反轉)
        /// </summary>
        private void GR_RotateDirection(string Direction,Label lb)
        {
            lb.Text = !string.IsNullOrEmpty(Direction)?(Direction.Trim().Equals("0")) ? "正转" : (Direction.Trim().Equals("1")) ? "反转": Direction.Trim() : string.Empty;

            lb.BackColor = (Direction.Trim().Equals("0")) ? Color.LawnGreen : (Direction.Trim().Equals("1")) ? Color.Magenta : Color.White;
        }

        /// <summary>
        /// 產線方向 0:STOP, 1:FWD(正轉), 2:REV(反轉) / 張力 / 產線速度 報文: L1>L2 104 每一秒會收到一筆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Line_Tick(object sender, EventArgs e)
        {
            //Process Data
            try
            {
                Fun_SelectProcessData();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug("2-1", $"Process Data", $"Process Data查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"Process Data查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"Process Data查詢資料庫失敗:{ex}");
            }
        }

        /// <summary>
        /// 搜尋報文104
        /// </summary>
        private void Fun_SelectProcessData()
        {
            string strSql = SqlFactory.Frm_2_1_PROCESS_DATA_DB_TBL_ProcessData();

            try
            {
                dtGetProcessData = Data_Access.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_GPL);
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"Process Data查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("2-1", $"Process Data", $"Process Data查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"Process Data查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"Process Data查詢資料庫失敗:{ex}");
            }

            if (dtGetProcessData.IsNull())
            {
                Lbl_LineSpeed.Text = Lbl_LineTension.Text = Lbl_RunDirection.Text = string.Empty;
                PublicComm.ClientLog.Debug($"查詢Process Data結果資料數為 [ 0 ]");
                return;
            } 

            //產線速度
            Lbl_LineSpeed.Text = dtGetProcessData.Rows[0][nameof(TBL_ProcessData.Line_Speed)].ToString() ?? string.Empty;
            //產線張力
            Lbl_LineTension.Text = dtGetProcessData.Rows[0][nameof(TBL_ProcessData.Line_Tension)].ToString() ?? string.Empty;
            //產線方向
            Lbl_RunDirection.Text = dtGetProcessData.Rows[0][nameof(TBL_ProcessData.Line_run_direction)].ToString().Equals("0") ? 
                "Stop" : dtGetProcessData.Rows[0][nameof(TBL_ProcessData.Line_run_direction)].ToString().Equals("1") ? 
                "→" : dtGetProcessData.Rows[0][nameof(TBL_ProcessData.Line_run_direction)].ToString().Equals("2") ?
                "←" : string.Empty;
            //字體顏色動態變更
            Lbl_RunDirection.ForeColor = dtGetProcessData.Rows[0][nameof(TBL_ProcessData.Line_run_direction)].ToString().Equals("0") ? Color.Red : Color.RoyalBlue;
            //字體大小動態變更
            Lbl_RunDirection.Font = dtGetProcessData.Rows[0][nameof(TBL_ProcessData.Line_run_direction)].ToString().Equals("0") ?
               new Font("微軟正黑體",28, FontStyle.Bold) : new Font("微軟正黑體", 45, FontStyle.Bold);
        }

        /// <summary>
        /// 選單
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AckPDIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strSql = SqlFactory.Frm_2_1_CheckPDI_DB_PDI(MouseDown_CoilID);
            DataTable dtGetPDI = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "钢卷是否有PDI","2-1");

            
            if (dtGetPDI.IsNull())
            {
                SCCommMsg.CS02_AckPDI Msg = new SCCommMsg.CS02_AckPDI
                {
                    Source = "GPL_HMI",
                    ID = "AckPDI",
                    Coil_ID = MouseDown_CoilID
                };
                PublicComm.Client.Tell(Msg);
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}通知Server Tracking请求PDI 钢卷编号:{MouseDown_CoilID.Trim()}", $"通知Server Tracking请求PDI 钢卷编号:{MouseDown_CoilID.Trim()}");
                EventLogHandler.Instance.EventPush_Message($"已通知Server要求钢卷编号:[{MouseDown_CoilID.Trim() }]PDI!");
                PublicComm.ClientLog.Info($"已通知Server要求[{MouseDown_CoilID.Trim()}] PDI");
                PublicComm.akkaLog.Info($"已通知Server要求[{MouseDown_CoilID.Trim()}] PDI");
            }
            else
            {
                EventLogHandler.Instance.EventPush_Message($"钢卷编号:[{MouseDown_CoilID.Trim() }]已有PDI!");
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}Tracking请求PDI 钢卷编号:{MouseDown_CoilID.Trim()}", $"Tracking请求PDI 钢卷编号:{MouseDown_CoilID.Trim() }该钢卷已有PDI");
                PublicComm.ClientLog.Info($"鋼卷號:[{MouseDown_CoilID.Trim()}]已有PDI");
            }
        }

        /// <summary>
        /// 未上線排程鋼卷右鍵判斷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgoff_MouseDown(object sender, MouseEventArgs e)
        {
            Dgv_MouseDown(e);
        }
        private void Dgv_MouseDown(MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                MouseDown_CoilID = Dgv_off.CurrentRow.Cells[0].Value.ToString();
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}Tracking未上线钢卷资料列请求PDI 钢卷编号:{MouseDown_CoilID.Trim()}", $"Tracking未上线钢卷资料列请求PDI 钢卷编号:{MouseDown_CoilID.Trim()}");
                CS02_AckPDI_Tell();
            }
        }               

        /// <summary>
        /// 左右鍵判斷
        /// 滑鼠左右鍵都會觸發，點擊瞬間就會觸發
        /// 點擊右鍵，預存鞍座上鋼卷編號
        /// </summary>
        /// <param name="e"></param>
        private void SK_MouseDown(Label lb, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                MouseDown_CoilID = lb.Text;
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}Tracking鞍座上请求PDI 钢卷编号:{MouseDown_CoilID.Trim()}", $"Tracking鞍座上请求PDI 钢卷编号:{MouseDown_CoilID.Trim()}");
                CS02_AckPDI_Tell();
            }
        }

        /// <summary>
        /// 要求PDI
        /// </summary>
        private void CS02_AckPDI_Tell()
        {
            SCCommMsg.CS02_AckPDI Msg = new SCCommMsg.CS02_AckPDI
            {
                Source = "GPL_HMI",
                ID = "AckPDI",
                Coil_ID = MouseDown_CoilID
            };
            PublicComm.Client.Tell(Msg);
            EventLogHandler.Instance.LogInfo("2-1", $"使用者:{ PublicForms.Main.lblLoginUser.Text.Trim() }通知Server要求钢卷编号:{ MouseDown_CoilID.Trim()}之PDI", $"钢卷编号:{MouseDown_CoilID.Trim()}");
        }

        /// <summary>
        /// 2-2 歷史研磨設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_LinkFrm_2_2_Click(object sender, EventArgs e)
        {
            Frm_0_0_Main FatherForm = Parent.Parent as Frm_0_0_Main;
            FatherForm.tsMenuItem_2_2.PerformClick();
        }
        private void Timer_DgvUse_Tick(object sender, EventArgs e)
        {
            //未上線鋼卷詳細資料
            try
            {
                Dgv_offData();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"未上线钢卷资料查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("2-1", $"未上线钢卷资料", $"未上线钢卷资料查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"未上線鋼卷查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"未上線鋼卷查詢資料庫失敗:{ex}");
            }

            //線上鋼卷詳細資料
            try
            {
                Dgv_onData();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"线上钢卷资料查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("2-1", $"线上钢卷资料", $"线上钢卷资料查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"線上鋼卷查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"線上鋼卷查詢資料庫失敗:{ex}");
            }
        }
        private void Chk_DGV_Reflash_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_DGV_Reflash.Checked.Equals(true)) Timer_DgvUse.Start();
            else Timer_DgvUse.Stop();
        }

        /// <summary>
        /// 天車入料Server通知，
        /// </summary>
        /// <param name="Message"></param>
        public void Handle_SC06_CraneEntryCoil(SCCommMsg.SC06_CraneEntryCoil Message)
        {
            Pnl_Scan.Visible = true;
            Pnl_Scan.BringToFront();
            Pnl_Scan.Size = new Size(350, 250);
            Pnl_Scan.Location = new Point(715, 336);

            Lbl_CoilNo.Text = Message.CoilID.Trim();
            Lbl_SkidNo.Text = Message.CoilPosition.ToString();
        }

        //測試server通知HMI
        private void Btn_Test_CraneEntryCoil_Click(object sender, EventArgs e)
        {
            SCCommMsg.CoilSkPosition skPosition = SCCommMsg.CoilSkPosition.ESK01;
            if (Txt_Test_CraneEntrySkid.Text.ToString() == "0")
                skPosition = SCCommMsg.CoilSkPosition.ETOP;
            else if (Txt_Test_CraneEntrySkid.Text.ToString() == "1")
                skPosition = SCCommMsg.CoilSkPosition.ESK01;

            SCCommMsg.SC06_CraneEntryCoil Message = new SCCommMsg.SC06_CraneEntryCoil(Txt_Test_CraneEntryCoil.Text.ToString(), skPosition);

            PublicComm.Client.Tell(Message);
        }

        //Server通知天車入料 確認畫面
        private void Btn_ScanYes_Click(object sender, EventArgs e)
        {
            int intPlcPos = -1;
            if (Lbl_SkidNo.Text.ToString() == SCCommMsg.CoilSkPosition.ETOP.ToString())
            {
                intPlcPos = 1;
            }
            else if (Lbl_SkidNo.Text.ToString() == SCCommMsg.CoilSkPosition.ESK01.ToString())
            {
                intPlcPos = 2;
            }

            SCCommMsg.CS18_CarneEntryCoilSelect CarneEntry = new SCCommMsg.CS18_CarneEntryCoilSelect
            {
                coilID = Lbl_CoilNo.Text,
                SKNo = intPlcPos
            };

            PublicComm.Client.Tell(CarneEntry);

            EventLogHandler.Instance.EventPush_Message($"已通知Server天车入料钢卷号选择結果[{Lbl_CoilNo.Text.Trim()}]");
            EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}天车入料钢卷号不一致", $"已通知Server天车入料钢卷号选择結果[{Lbl_CoilNo.Text.Trim()}]");
            PublicComm.ClientLog.Info($"已通知Server天车入料钢卷号选择結果[{Lbl_CoilNo.Text.Trim()}]");
            PublicComm.akkaLog.Info($"已通知Server天车入料钢卷号选择結果[{Lbl_CoilNo.Text.Trim()}]");

            Pnl_Scan.Visible = false;
        }

        private void Btn_ScanNo_Click(object sender, EventArgs e)
        {
            Pnl_Scan.Visible = false;
            Pnl_Scan.SendToBack();
        }

        /// <summary>
        /// 出料确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DSK02_Ready_Click(object sender, EventArgs e)
        {
            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel($"请确认[{Lbl_DSK02_CoilNo.Text.Trim()}]已准备好出料?", "提示", Properties.Resources.dialogQuestion, 1); //MessageBox.Show($"请确认[{Lbl_DSK02_CoilNo.Text.Trim()}]已准备好出料?", "提示", MessageBoxButtons.OKCancel);

            if (dialogR == DialogResult.Cancel) return;

            SCCommMsg.CS21_DeliveryCoilReady DeliveryCoilReady = new SCCommMsg.CS21_DeliveryCoilReady
            {
                Coil_ID = Lbl_DSK02_CoilNo.Text.Trim()
            };

            PublicComm.Client.Tell(DeliveryCoilReady);

            EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 已通知Server钢卷号[{Lbl_DSK02_CoilNo.Text.Trim()}]出料准备就绪");
            EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}出料准备确认", $"已通知Server钢卷号[{Lbl_DSK02_CoilNo.Text.Trim()}]出料准备就绪");
            PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 已通知Server钢卷号[{Lbl_DSK02_CoilNo.Text.Trim()}]出料准备就绪");
            PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 已通知Server钢卷号[{Lbl_DSK02_CoilNo.Text.Trim()}]出料准备就绪");
        }
               
        private void LblETRCoilid_TextChanged(object sender, EventArgs e)
        {
            ////待确认
            //string strSql = $@"Select top 1  Fla.Intermesh_Num1,Fla.Intermesh_Num2
            //                   From {nameof(CoilPDIModel.TBL_PDI)} pdi
            //                   left join TBL_LookupTable_Flattener Fla on pdi.{nameof(CoilPDIModel.TBL_PDI.St_No)} = Fla.Material_Grade
            //                   where pdi.{nameof(CoilPDIModel.TBL_PDI.In_Coil_Thick)} between Fla.Coil_Thickness_Min and Fla.Coil_Thickness_Max
            //                   and pdi.{nameof(CoilPDIModel.TBL_PDI.In_Coil_ID)} = '{Lbl_POR_CoilNo.Text}'";
            //DataTable dt = Data_Access.Fun_SelectDate(strSql,GlobalVariableHandler.Instance.strConn_GPL,"下压量","2-1");

            //if (dt.IsNull()) return;

            ////Lb_Intermesh_1.Text = dt.Rows[0][nameof(LkUpTableFlattenerModel.TBL_LookupTable_Flattener.Intermesh_Num1)].ToString() ?? string.Empty;
            ////Lb_Intermesh_2.Text = dt.Rows[0][nameof(LkUpTableFlattenerModel.TBL_LookupTable_Flattener.Intermesh_Num2)].ToString() ?? string.Empty;
        }
              
        private void BtnReflash_Click(object sender, EventArgs e)
        {
            Fun_SelectTrackingData();
            Fun_SkidChangeColor();
        }

        private void Btn_PORPresetL1_Click(object sender, EventArgs e)
        {
            string strCoil = "";
            string strPlan_No = "";

            if (!string.IsNullOrEmpty(Lbl_POR_CoilNo.Text.Trim()))
            {
                strCoil = Lbl_POR_CoilNo.Text;
            }
            else
            {
                DialogHandler.Instance.Fun_DialogShowOk($"POR上无钢卷可下抛生产参数", "下抛钢卷生产参数",null, 0);
                return;
            }
            string strSql = $@"Select pdi.*
                        From [{nameof(PDIEntity.TBL_PDI)}] pdi                        
                        Where pdi.[{nameof(PDIEntity.TBL_PDI.In_Coil_ID)}] = '{strCoil}'
                        ORDER BY pdi.[{ nameof(PDIEntity.TBL_PDI.CreateTime)}] DESC";
            DataTable dtData = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "PDI_Data");
            if (dtData != null && dtData.Rows.Count > 0)
            {
                strPlan_No = dtData.Rows[0][nameof(PDIEntity.TBL_PDI.Plan_No)].ToString();
            }

            SCCommMsg.CS22_POR_PresetL1 _POR_PresetL1 = new SCCommMsg.CS22_POR_PresetL1
            {
                Coil_ID = strCoil,
                Plan_No = strPlan_No
            };
            PublicComm.Client.Tell(_POR_PresetL1);

            DialogHandler.Instance.Fun_DialogShowOk($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 已通知Server下抛POR钢卷生产参数给L1", "下抛POR钢卷生产参数",null, 4);

            PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 已通知Server下抛POR钢卷 钢卷号:[{Lbl_POR_CoilNo.Text.Trim()}]生产参数给L1");
            EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 已通知Server下抛POR钢卷 钢卷号:[{Lbl_POR_CoilNo.Text.Trim()}]生产参数给L1");
            EventLogHandler.Instance.LogInfo("2-1", "下抛POR钢卷生产参数", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 通知Server下抛POR钢卷 钢卷号:[{Lbl_POR_CoilNo.Text.Trim()}]生产参数给L1");
        }

        private void Btn_BrokenDo_Click(object sender, EventArgs e)
        {
            //以POR钢卷号(ex:A00100) 查PDI,取PDI的出口卷号(ex:B00100)
            //跳一视窗,显示如下
            //Title:手动断带处理
            //POR钢卷号:B00120      TR钢卷号:B00110
            //Btn_确认(通知server;POR:B00120 ,TR:B00110 )      Btn_取消
        }

        private void Btn_StripBreakModify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Lbl_POR_CoilNo.Text.Trim())) { return; }

            string strMessage = $"确定断带修改POR卷号?";
            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "断带修改POR卷号", Properties.Resources.dialogQuestion, 1);
            if (dialogR == DialogResult.Cancel) return;

            //在POR端因為鋼捲斷帶，要指定子捲號，HMI通知Server修改子捲號，Server需下拋205NewPORId通知L1修改POR捲號。
            SCCommMsg.CS23_POR_StripBreakModify _StripBreak = new SCCommMsg.CS23_POR_StripBreakModify
            {
                Coil_ID = Lbl_POR_CoilNo.Text.Trim()
            };

            PublicComm.Client.Tell(_StripBreak);

            DialogHandler.Instance.Fun_DialogShowOk($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 已通知Server发送断带子卷号给L1", "断带修改POR卷号",null, 4);

            PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 已通知Server发送断带子卷钢卷号:[{Lbl_POR_CoilNo.Text.Trim()}]给L1");
            EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 已通知Server发送断带子卷钢卷号:[{Lbl_POR_CoilNo.Text.Trim()}]给L1");
            EventLogHandler.Instance.LogInfo("2-1", "断带修改POR卷号", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 通知Server发送断带子卷钢卷号:[{Lbl_POR_CoilNo.Text.Trim()}]给L1");
        }
        /// <summary>
        /// 連線狀態_取得資料(L1,WMS,MMS)
        /// </summary>
        private void Fun_SelectNetWorkStatus()
        {
            string strSql = SqlFactory.Frm_5_6_SelectStatus();
            dtGetConnectionStatus = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "连线状态", "2-1");

            if (dtGetConnectionStatus.IsNull()) return;

            Fun_NetworkStatusColorControl();
        }
        /// <summary>
        /// 連線狀態_變色(L1,WMS,MMS)
        /// </summary>
        private void Fun_NetworkStatusColorControl()
        {
            for (int Index = 0; Index < dtGetConnectionStatus.Rows.Count; Index++)
            {
                //Send MMS
                if (dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("MMS"))
                {
                    Lbl_SendMMS.BackColor = dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Trim().Equals("1") ?
                        Color.Lime : Color.Red;
                }
                //Rev MMS
                else if (dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_From)].ToString().Trim().Equals("MMS"))
                {
                    Lbl_RevMMS.BackColor = dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ?
                        Color.Lime : Color.Red;
                }
                ////Send L2.5
                //else if (dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("LEVEL25"))
                //{
                //    lbSendL25.BackColor = dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ?
                //        Color.Lime : Color.Red;
                //}
                //Send WMS
                else if (dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("WMS"))
                {
                    Lbl_SendWMS.BackColor = dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ?
                        Color.Lime : Color.Red;
                }
                //Rev WMS
                else if (dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_From)].ToString().Trim().Equals("WMS"))
                {
                    Lbl_RevWMS.BackColor = dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ?
                        Color.Lime : Color.Red;
                }
                //Send PLC
                else if (dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("LEVEL1"))
                {
                    Lbl_SendPLC.BackColor = dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ?
                        Color.Lime : Color.Red;
                }
                //Rev PLC
                else if (dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_From)].ToString().Trim().Equals("LEVEL1"))
                {
                    Lbl_RevPLC.BackColor = dtGetConnectionStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ?
                        Color.Lime : Color.Red;
                }
            }
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
        private void Fun_LanguageIsEn_Font14_10(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font14_12(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
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

        #region 已停用功能
        /// <summary>
        /// 入口鞍座-ESK01 左右鍵判斷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbESK01_MouseDown(object sender, MouseEventArgs e)
        {
            SK_MouseDown(Lbl_ESK01_CoilNo, e);
        }

        /// <summary>
        /// 入口鞍座-ESKTOP 左右鍵判斷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbETOP_MouseDown(object sender, MouseEventArgs e)
        {
            SK_MouseDown(Lbl_ETOP_CoilNo, e);
        }

        /// <summary>
        /// 衬纸类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cbo_paper_exit_type_Click(object sender, EventArgs e)
        {
            ////墊紙類型
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Paper_Type, cbo_paper_exit_type);
        }

        /// <summary>
        /// 退料原因
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cbo_ReturnCode_Click(object sender, EventArgs e)
        {
            //ComboBoxIndexHandler.Instance.ComboBox_DeleteCode(cbo_ReturnCode);
        }
        private void TabReject_DrawItem(object sender, DrawItemEventArgs e)
        {
            //DrawItemHandler.Instance.DrawItem(TabReject, e);
        }

        #endregion

        #region ["Scan"] - 隐藏
        //private void btn_Entry_Scan_ok_Click(object sender, EventArgs e)
        //{
        //    if (chk_Entry_Scan_CoilID.Checked.Equals(true))
        //    {
        //        Scan_RenameCoilID(chk_Entry_Scan_CoilID, EntryPostion);
        //    }
        //    else if (chk_PDI_CoilID.Checked.Equals(true))
        //    {
        //        Scan_RenameCoilID(chk_PDI_CoilID, EntryPostion);
        //    }
        //}
        //private void Scan_RenameCoilID(CheckBox chk, int skid)
        //{
        //    SCCommMsg.CS04_RenameCoil _RenameCoil = new SCCommMsg.CS04_RenameCoil
        //    {
        //        Source = "CPL1_HMI",
        //        ID = "RenameCoil",
        //        Coil_ID = chk.Text,
        //        Postion = skid
        //    };
        //    PublicComm.client.Tell(_RenameCoil);
        //    EventLogHandler.EventLogInstance.Log(System_ID.Client, "2-1", Event_Type.Infro, "使用者:" + PublicForms.Main.lblLoginUser.Text + "通知Server确认扫描结果", "通知Server扫描结果确认 钢卷编号:" + chk.Text);
        //}

        #endregion
    }
}

using System.Windows.Forms;
using System;
using System.Data;
using System.Linq;
using System.Drawing;
using static GPLManager.DataBaseTableFactory;
using DBService.Repository.PDI;
using GPLManager.Util;
using DataModel.HMIServerCom.Msg;
using Akka.Actor;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using System.Text;

namespace GPLManager
{
    public partial class Frm_1_2_PDIDetail : Form
    {
        #region 變數
        DataTable dtSelectOne, dtEntryCoilID_Data;
        DataTable dtBeforeEdit;
        private string strEditStatus = "Read";
        private bool bolEditStatus = false;
        private bool bolPdiIsNull;

        //語系
        private LanguageHandler LanguageHand;

        #endregion

        public Frm_1_2_PDIDetail()
        {
            InitializeComponent();
        }
        private void Frm_1_2_PDIDetail_Load(object sender, EventArgs e)
        {
            PublicForms.PDIDetail = this;
            Control[] Frm_1_2_Control = new Control[] {
            Btn_PrintTag,
            Btn_Delete, //刪除
            Btn_New, //新增
            Btn_Edit //修改
            };
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_1_2_Control, UserSetupHandler.Instance.Frm_1_2);

            // 20211220 不管甚麼權限,都不開放編輯
            Btn_New.Visible = false;
            Btn_Delete.Visible = false;
            Btn_Edit.Visible = false;


            //缺陷说明
            try
            {
                TxtDescription();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"缺陷栏位说明查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-2", $"缺陷栏位说明", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"缺陷欄位說明查詢失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"缺陷欄位說明查詢失敗:{ex}");
            }
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDataPage, true);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDefectPage, true);

            Fun_SelectedData("");

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        private void Frm_1_2_PDIDetail_Shown(object sender, EventArgs e)
        {

            //鋼卷號清單
            try
            {
                Fun_ComboBoxItems();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"钢卷号清单查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-2", $"钢卷号清单", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"剛卷號清單查詢失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"剛卷號清單查詢失敗:{ex}");
            }

            //詳細資料ComboBox欄位
            try
            {
                Fun_ComboBox();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"ComboBox选项查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-1", $"查询ComboBox选项", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"查詢ComboBox選項失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"查詢ComboBox選項失敗:{ex}");
            }
        }

        #region 查詢基本資料
        public void Fun_SelectedData(string Coil_ID,string strPlan_No = "")
        {
            string strSql = SqlFactory.Frm_1_2_SelectedData_DB_PDI(Coil_ID, strPlan_No);
            DataTable dtGetPDI = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "PDI","1-2");

            if (dtGetPDI != null && dtGetPDI.Rows.Count > 1)
            {
                DataTable dtSelectBack = new DataTable();

                Frm_SelectDataOpen frm_SelectOpen = new Frm_SelectDataOpen
                {
                    dtSelectData = dtGetPDI.Copy()
                   ,
                    strDataType = "PDI"
                };
                frm_SelectOpen.ShowDialog();
                frm_SelectOpen.Dispose();

                if (frm_SelectOpen.DialogResult == DialogResult.OK)
                {
                    dtSelectBack = frm_SelectOpen.dtSelectData.Copy();

                    dtGetPDI = dtSelectBack.Copy();
                }
            }


            dtSelectOne = dtGetPDI.Copy();

        }
        #endregion

        #region 基本資料填入
        public void Fun_DisplayPDIDetail()
        {
            if (dtSelectOne.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"PDI无资料");
                return;
            }

            #region 填資料
            //計畫號
            Txt_Plan_No.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Plan_No)].ToString() ?? string.Empty;
            //順序號
            Txt_SeqNo.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Mat_Seq_No )].ToString() ?? string.Empty;
            //計畫種類
            Cob_Plan_Sort.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Plan_Type)].ToString() ?? string.Empty;
            //入口卷號
            Txt_In_Coil_ID.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_ID)].ToString() ?? string.Empty;
            //入料厚度
            Txt_In_Coil_Thick.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Thick)].ToString() ?? string.Empty;
            //入料寬度
            Txt_In_Coil_Width.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Width)].ToString() ?? string.Empty;
            //入料重量
            Txt_In_Coil_Wt.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Wt)].ToString() ?? string.Empty;
            //入料長度
            Txt_In_Coil_Length.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Length)].ToString() ?? string.Empty;
            //入料內徑
            Txt_In_Coil_Inner_Diameter.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Inner_Diameter)].ToString() ?? string.Empty;
            //入料外徑
            Txt_In_Coil_Outer_Diameter.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.In_Coil_Outer_Diameter)].ToString() ?? string.Empty;
            //入口套筒類型
            Cob_Sleeve_Type_Entry.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.In_Sleeve_Type_Code)].ToString() ?? string.Empty;
            //套筒內徑
            Txt_Sleeve_Inner_Entry.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.In_Sleeve_Diameter)].ToString() ?? string.Empty;
            //入口墊紙方式
            Cob_PAPER_REQ_CODE.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.In_Paper_Req_Code)].ToString() ?? string.Empty;
            //入口墊紙類型
            Cob_In_Paper_Type_Entry.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.In_Paper_Code)].ToString() ?? string.Empty;
            //入口頭部墊紙長度
            Txt_In_Paper_Head_Length.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Paper_Length)].ToString() ?? string.Empty;
            //入口頭部墊紙寬度
            Txt_In_Paper_Head_Width.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Paper_Width)].ToString() ?? string.Empty;
            //入口尾部墊紙長度
            Txt_In_Paper_Tail_Length.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Paper_Length)].ToString() ?? string.Empty;
            //入口尾部墊紙寬度
            Txt_In_Paper_Tail_Width.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Paper_Width)].ToString() ?? string.Empty;
            //內部鋼種
            Txt_St_no.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.St_No)].ToString() ?? string.Empty;
            //密度
            Txt_Density.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Density)].ToString() ?? string.Empty;
            //返修類型
            Cob_Rework_Type.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Repair_Type)].ToString() ?? string.Empty;
           
            //訂單表面精度代碼
            Cob_Surface_Finishing_Code.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Order)].ToString() ?? string.Empty;
            //訂單表面精度代碼描述
            Txt_Surface_Accu_Desc.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accu_Desc_Order)].ToString() ?? string.Empty;
            //来料表面精度代碼
            Cob_Surface_Accuracy_Code.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Acture)].ToString() ?? string.Empty;
            //来料表面精度代碼描述
            Txt_Surface_Accuracy_Desc.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accuracy_Desc_Acture)].ToString() ?? string.Empty;
            //內表面精度代碼
            Cob_Surface_Accu_Code_In.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_In)].ToString() ?? string.Empty;
            //內表面精度代碼描述
            Txt_Surface_Accu_Desc_In.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accu_Desc_In)].ToString() ?? string.Empty;
            //外表面精度代碼
            Cob_Surface_Accu_Code_Out.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accu_Code_Out)].ToString() ?? string.Empty;
            //外表面精度代碼描述
            Txt_Surface_Accu_Desc_Out.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Surface_Accu_Desc_Out)].ToString() ?? string.Empty; 
           
            //好面朝向
            Cob_Base_Surface.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Better_Surf_Ward_Code)].ToString() ?? string.Empty;
            //開卷方向
            Cob_Uncoil_Direction.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Uncoil_Direction)].ToString() ?? string.Empty;
            //出口鋼卷號
            Txt_Out_Coil_ID.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Coil_ID)].ToString() ?? string.Empty;
            //出口墊紙方式
            Cob_OUT_PAPER_REQ_CODE.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Paper_Req_Code)].ToString() ?? string.Empty;
            //出口墊紙種類
            Cob_Paper_Type_Exit.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Paper_Code)].ToString() ?? string.Empty;
            //出口套筒內徑
            Txt_Sleeve_Inner_Exit.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Sleeve_Diamter)].ToString() ?? string.Empty;
            //出口套筒類型
            Cob_Sleeve_Type_Exit.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Sleeve_Type_Code)].ToString() ?? string.Empty;
            //出口綁帶方式
            Txt_Strap.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Pack_Mode)].ToString() ?? string.Empty;
            //鋼卷來源
            Cob_CoilOrigin.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Origin_Code)].ToString() ?? string.Empty;
            //上游
            Txt_Wholebacklog.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Prev_Whole_Backlog_Code)].ToString() ?? string.Empty;
            //下游
            Txt_NWholebacklog.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Next_Whole_Backlog_Code)].ToString() ?? string.Empty;
            //头段鋼種
            Txt_HSt_no.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Leader_St_No)].ToString() ?? string.Empty;
            //头段导带标记
            Cob_Head_Leader.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Leader_Attached)].ToString() ?? string.Empty;
            //头段打孔位置
            Txt_Head_Hole.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Hole_Position)].ToString() ?? string.Empty;
            //头段厚度
            Txt_HThickness.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Leader_Thickness)].ToString() ?? string.Empty;
            //头段寬度
            Txt_HWd.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Leader_Width)].ToString() ?? string.Empty;
            //头段長度
            Txt_HLen.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Leader_Length)].ToString() ?? string.Empty;
            //尾段鋼種
            Txt_TSt_no.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Leader_St_No)].ToString() ?? string.Empty;
            //尾段导带标记
            Cob_Tail_Leader.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Leader_Attached)].ToString() ?? string.Empty;
            //尾段打孔位置
            Txt_Tail_Hole.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Hole_Position)].ToString() ?? string.Empty;
            //尾段厚度
            Txt_TThickness.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Leader_Thickness)].ToString() ?? string.Empty;
            //尾段寬度
            Txt_TWd.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Leader_Width)].ToString() ?? string.Empty;
            //尾段長度
            Txt_TLen.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Leader_Length)].ToString() ?? string.Empty;
            //頭部未壓製區域
            Txt_HEAD_OFF_GAUGE.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Head_Off_Gauge)].ToString() ?? string.Empty;
            //尾部未壓製區域
            Txt_Tail_off_gauge.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Tail_Off_Gauge)].ToString() ?? string.Empty;
            //牌号
            Txt_SG_Sign.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Sg_Sign)].ToString() ?? string.Empty;
            //上次研磨面
            Cob_PRE_GRINDING_SURFACE.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Pre_Grinding_Surface)].ToString() ?? string.Empty;
            //外表面研磨次數
            Txt_GRINDING_COUNT_OUT.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Grinding_Count_Out)].ToString() ?? string.Empty;
            //內表面研磨次數
            Txt_GRINDING_COUNT_IN.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Grinding_Count_In)].ToString() ?? string.Empty;
            //指定研磨面
            Cob_APPOINT_GRINDING_SURFACE.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Appoint_Grinding_Surface)].ToString() ?? string.Empty;
            //目標出口厚度
            Txt_Out_Coil_Thick.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Coil_Thick)].ToString() ?? string.Empty;
            //目標厚度最大值
            Txt_Out_Coil_Thick_Max.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Max)].ToString() ?? string.Empty;
            //目標厚度最小值
            Txt_Out_Coil_Thick_Min.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Coil_Thick_Min)].ToString() ?? string.Empty;
            //目標寬度
            Txt_Out_Coil_Width.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Coil_Width)].ToString() ?? string.Empty;
            //出口鋼卷內徑
            Txt_OutInner.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Out_Coil_Inner_Diameter)].ToString() ?? string.Empty;
            //脫脂標記
            Cob_SKIM_FLAG.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Skim_Flag)].ToString() ?? string.Empty;
            //拋光類型
            Cob_POLISHING_TYPE.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Polishing_Type)].ToString() ?? string.Empty;
            //正反研磨标记需求
            Cob_GrandFlag.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Grind_Flag)].ToString() ?? string.Empty;
            //測試計畫號
            Txt_TestPlan.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Test_Plan_No)].ToString() ?? string.Empty;
            //合同號
            Txt_Order_No.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Order_No)].ToString() ?? string.Empty;
            //客戶代碼
            Txt_Order_Cust_Code.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Order_Cust_Code)].ToString() ?? string.Empty;
            //订货用户英文名称
            Txt_Order_Cust_Ename.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Order_Cust_Ename)].ToString() ?? string.Empty;
            //订货客户中文名称
            Txt_Order_Cust_Cname.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Order_Cust_Cname)].ToString() ?? string.Empty;
            ////////廢料重量
            //////txtScraped_Weight.Text = dtSelectOne.Rows[0][nameof(CoilPDIModel.TBL_PDI.Scraped_Weight)].ToString() ?? string.Empty;
            ////////斷帶時間
            ////////todo 釐清是否需要
            //////txtBrakeStripTime.Text = string.Empty;
            //實際屈服值
            Txt_Act_YS_Stand.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Ys_Stand)].ToString() ?? string.Empty;
            //屈服最大值
            Txt_YS_Stand_Max.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Ys_Max)].ToString() ?? string.Empty;
            //屈服最小值
            Txt_YS_Stand_Min.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Ys_Max)].ToString() ?? string.Empty;
            //平坦度平均值
            Txt_Flatness_Avg_Crness.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Flatness_Avg_CR)].ToString() ?? string.Empty;
            #endregion
        }
        #endregion

        #region 查詢缺陷資料
        public void Fun_DisplayDefectData()
        {
            Fun_ClearText(Tab_PDIDefectPage);
            Lbl_EntryCoil_Defect.Text = "";

            if (dtSelectOne.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"PDI无资料");
                return;
            }

            #region 填入资料

            #region  - Code -
            Txt_Code_D1.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Code)].ToString() ?? string.Empty;
            Txt_Code_D2.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Code)].ToString() ?? string.Empty;
            Txt_Code_D3.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Code)].ToString() ?? string.Empty;
            Txt_Code_D4.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Code)].ToString() ?? string.Empty;
            Txt_Code_D5.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Code)].ToString() ?? string.Empty;
            Txt_Code_D6.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Code)].ToString() ?? string.Empty;
            Txt_Code_D7.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Code)].ToString() ?? string.Empty;
            Txt_Code_D8.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Code)].ToString() ?? string.Empty;
            Txt_Code_D9.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Code)].ToString() ?? string.Empty;
            Txt_Code_D10.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Code)].ToString() ?? string.Empty;
            #endregion

            #region - Origin -
            Txt_Origin_D1.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D2.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D3.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D4.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D5.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D6.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D7.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D8.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D9.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Origin)].ToString() ?? string.Empty;
            Txt_Origin_D10.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Origin)].ToString() ?? string.Empty;
            #endregion

            #region - Sid (ComboBox)-
            Cob_Sid_D01.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D02.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D03.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D04.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D05.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D06.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D07.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D08.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D09.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Sid)].ToString().Trim() ?? string.Empty;
            Cob_Sid_D10.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Sid)].ToString().Trim() ?? string.Empty;
            #endregion

            #region - Pos_W (ComboBox)-
            Cob_PosW_D01.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D02.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D03.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D04.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D05.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D06.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D07.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D08.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D09.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Pos_W)].ToString().Trim() ?? string.Empty;
            Cob_PosW_D10.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Pos_W)].ToString().Trim() ?? string.Empty;
            #endregion

            #region - Pos_L_Start - 
            Txt_Pos_L_Start_D1.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Pos_L_Start)].ToString() ?? string.Empty;
            Txt_Pos_L_Start_D2.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Pos_L_Start)].ToString() ?? string.Empty;
            Txt_Pos_L_Start_D3.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Pos_L_Start)].ToString() ?? string.Empty;
            Txt_Pos_L_Start_D4.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Pos_L_Start)].ToString() ?? string.Empty;
            Txt_Pos_L_Start_D5.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Pos_L_Start)].ToString() ?? string.Empty;
            Txt_Pos_L_Start_D6.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Pos_L_Start)].ToString() ?? string.Empty;
            Txt_Pos_L_Start_D7.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Pos_L_Start)].ToString() ?? string.Empty;
            Txt_Pos_L_Start_D8.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Pos_L_Start)].ToString() ?? string.Empty;
            Txt_Pos_L_Start_D9.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Pos_L_Start)].ToString() ?? string.Empty;
            Txt_Pos_L_Start_D10.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Pos_L_Start)].ToString() ?? string.Empty;
            #endregion

            #region - Pos_L_End -
            Txt_Pos_L_End_D1.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Pos_L_End)].ToString() ?? string.Empty;
            Txt_Pos_L_End_D2.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Pos_L_End)].ToString() ?? string.Empty;
            Txt_Pos_L_End_D3.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Pos_L_End)].ToString() ?? string.Empty;
            Txt_Pos_L_End_D4.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Pos_L_End)].ToString() ?? string.Empty;
            Txt_Pos_L_End_D5.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Pos_L_End)].ToString() ?? string.Empty;
            Txt_Pos_L_End_D6.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Pos_L_End)].ToString() ?? string.Empty;
            Txt_Pos_L_End_D7.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Pos_L_End)].ToString() ?? string.Empty;
            Txt_Pos_L_End_D8.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Pos_L_End)].ToString() ?? string.Empty;
            Txt_Pos_L_End_D9.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Pos_L_End)].ToString() ?? string.Empty;
            Txt_Pos_L_End_D10.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Pos_L_End)].ToString() ?? string.Empty;
            #endregion

            #region - Level (ComboBox)-
           Cob_Level_D01.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Level)].ToString().Trim() ?? string.Empty;
           Cob_Level_D02.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Level)].ToString().Trim() ?? string.Empty;
           Cob_Level_D03.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Level)].ToString().Trim() ?? string.Empty;
           Cob_Level_D04.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Level)].ToString().Trim() ?? string.Empty;
           Cob_Level_D05.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Level)].ToString().Trim() ?? string.Empty;
           Cob_Level_D06.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Level)].ToString().Trim() ?? string.Empty;
           Cob_Level_D07.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Level)].ToString().Trim() ?? string.Empty;
           Cob_Level_D08.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Level)].ToString().Trim() ?? string.Empty;
           Cob_Level_D09.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Level)].ToString().Trim() ?? string.Empty;
           Cob_Level_D10.SelectedValue = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Level)].ToString().Trim() ?? string.Empty;
            #endregion

            #region - Percent -
            Txt_Percent_D1.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D01_Percent)].ToString() ?? string.Empty;
            Txt_Percent_D2.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D02_Percent)].ToString() ?? string.Empty;
            Txt_Percent_D3.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D03_Percent)].ToString() ?? string.Empty;
            Txt_Percent_D4.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D04_Percent)].ToString() ?? string.Empty;
            Txt_Percent_D5.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D05_Percent)].ToString() ?? string.Empty;
            Txt_Percent_D6.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D06_Percent)].ToString() ?? string.Empty;
            Txt_Percent_D7.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D07_Percent)].ToString() ?? string.Empty;
            Txt_Percent_D8.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D08_Percent)].ToString() ?? string.Empty;
            Txt_Percent_D9.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D09_Percent)].ToString() ?? string.Empty;
            Txt_Percent_D10.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D10_Percent)].ToString() ?? string.Empty;
            #endregion

            #region - QGrade - 
            Txt_QGRADE_D1.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D01_QGrade)].ToString() ?? string.Empty;
            Txt_QGRADE_D2.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D02_QGrade)].ToString() ?? string.Empty;
            Txt_QGRADE_D3.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D03_QGrade)].ToString() ?? string.Empty;
            Txt_QGRADE_D4.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D04_QGrade)].ToString() ?? string.Empty;
            Txt_QGRADE_D5.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D05_QGrade)].ToString() ?? string.Empty;
            Txt_QGRADE_D6.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D06_QGrade)].ToString() ?? string.Empty;
            Txt_QGRADE_D7.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D07_QGrade)].ToString() ?? string.Empty;
            Txt_QGRADE_D8.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D08_QGrade)].ToString() ?? string.Empty;
            Txt_QGRADE_D9.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D09_QGrade)].ToString() ?? string.Empty;
            Txt_QGRADE_D10.Text = dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.D10_QGrade)].ToString() ?? string.Empty;
            #endregion

            #endregion

        }

        private void Fun_ClearText(TabPage Page)
        {
            foreach (Control control in Page.Controls.OfType<Control>())
            {
                if (control is TextBox)
                {
                    control.Text = string.Empty;
                }

                if (Page.Name.Equals(nameof(Tab_PDIDefectPage)))
                {
                    if (control is ComboBox)
                    {
                        control.Text = string.Empty;
                    }
                }
            }

        }
        #endregion

        #region 查詢ComboBox鋼卷編號清單
        public void Fun_ComboBox()
        {
            //計畫種類
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Plan_Sort, Cob_Plan_Sort);
            //入口套筒類型
            ComboBoxIndexHandler.Instance.SelectSleeve(Cob_Sleeve_Type_Entry);
            //入口墊紙方式
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.PAPER_REQ_CODE, Cob_PAPER_REQ_CODE);
            //入口墊紙類型
            ComboBoxIndexHandler.Instance.SelectPaper(Cob_In_Paper_Type_Entry);
            //反修類型
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Rework_Type, Cob_Rework_Type);
            //訂單表面精度
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, Cob_Surface_Finishing_Code);
            //實際表面精度
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, Cob_Surface_Accuracy_Code);
            //內表面精度
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, Cob_Surface_Accu_Code_In);
            //外表面精度
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, Cob_Surface_Accu_Code_Out);
            //好面朝向
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Base_Surface, Cob_Base_Surface);
            //開卷方向
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Uncoiler_Direction, Cob_Uncoil_Direction);
            //出口墊紙方式
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.PAPER_REQ_CODE, Cob_OUT_PAPER_REQ_CODE);
            //出口墊紙類型
            ComboBoxIndexHandler.Instance.SelectPaper(Cob_Paper_Type_Exit);
            //出口套筒類型
            ComboBoxIndexHandler.Instance.SelectSleeve(Cob_Sleeve_Type_Exit);
            //頭段導帶使用
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Leader_Usage, Cob_Head_Leader);
            //尾段導帶使用
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Leader_Usage, Cob_Tail_Leader);
            //鋼捲來源
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Origin, Cob_CoilOrigin);
            //正反研磨标记
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Grand, Cob_GrandFlag);
            //脫脂標記
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Skim, Cob_SKIM_FLAG);
            //拋光類型
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Polishing, Cob_POLISHING_TYPE);
            //上次研磨面
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.GRINDING_SURFACE, Cob_PRE_GRINDING_SURFACE);
            //指定研磨面
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.GRINDING_SURFACE, Cob_APPOINT_GRINDING_SURFACE);

            #region -缺陷資料ComboBox设定-

            #region - Level -
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, Cob_Level_D01);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, Cob_Level_D02);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, Cob_Level_D03);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, Cob_Level_D04);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, Cob_Level_D05);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, Cob_Level_D06);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, Cob_Level_D07);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, Cob_Level_D08);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, Cob_Level_D09);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, Cob_Level_D10);
            #endregion

            #region - Sid -
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, Cob_Sid_D01);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, Cob_Sid_D02);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, Cob_Sid_D03);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, Cob_Sid_D04);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, Cob_Sid_D05);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, Cob_Sid_D06);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, Cob_Sid_D07);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, Cob_Sid_D08);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, Cob_Sid_D09);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, Cob_Sid_D10);
            #endregion

            #region - PosW -
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, Cob_PosW_D01);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, Cob_PosW_D02);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, Cob_PosW_D03);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, Cob_PosW_D04);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, Cob_PosW_D05);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, Cob_PosW_D06);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, Cob_PosW_D07);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, Cob_PosW_D08);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, Cob_PosW_D09);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, Cob_PosW_D10);
            #endregion

            #endregion


        }
        /// <summary>
        /// 資料欄ComboBox值
        /// </summary>
        private void Fun_ComboBoxItems()
        {
            string strSql = SqlFactory.Frm_1_2_CoilComboxItems_DB_PDI();
            dtEntryCoilID_Data = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "ComboBox入口鋼卷號", "1-2");

            if (dtEntryCoilID_Data.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"无入口卷号清单");
                return;
            }

            Cob_EntryCoilID.DisplayMember = nameof(PDIEntity.TBL_PDI.In_Coil_ID);
            Cob_EntryCoilID.ValueMember = nameof(PDIEntity.TBL_PDI.In_Coil_ID);
            Cob_EntryCoilID.DataSource = dtEntryCoilID_Data;
        }
        #endregion

        private void TabPDI_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_PDIControl, e);
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            if (strEditStatus != "Read")
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再查询", "查询", null, 0);
                return;
            }
            if (bolEditStatus == true)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再查询", "查询", null, 0);
                return;
            }
            try
            {
                Fun_SelectedData(Cob_EntryCoilID.Text);
            }
            catch (Exception _ex)
            {
                EventLogHandler.Instance.EventPush_Message($"查询{Cob_EntryCoilID.Text} PDI失败:{_ex}");
                EventLogHandler.Instance.LogDebug("1-1", $"查询PDI", $"查询{Cob_EntryCoilID.Text} PDI失败:{_ex}");
                PublicComm.ClientLog.Debug($"查詢{Cob_EntryCoilID.Text} PDI失敗:{_ex}");
                PublicComm.ExceptionLog.Debug($"查詢{Cob_EntryCoilID.Text} PDI失敗:{_ex}");
            }

            //缺陷資料 欄位填入
            try
            {
                Fun_DisplayDefectData();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"缺陷资料查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-1", $"查询缺陷资料", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"查詢缺陷資料失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"查詢缺陷資料失敗:{ex}");
            }

            //詳細資料 欄位填入
            try
            {
                Fun_DisplayPDIDetail();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"详细资料填入失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-1", $"详细资料填入", $"详细资料填入失败:{ex}");
                PublicComm.ClientLog.Debug($"詳細資料填入失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"詳細資料填入失敗:{ex}");
            }

        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_New_Click(object sender, EventArgs e)
        {
            //已是编辑状态 return
            if (bolEditStatus) { return; }
            //紀錄是否編輯,狀態
            strEditStatus = "New";
            bolEditStatus = true;

            //先備份之前顯示的資料
            dtBeforeEdit = dtSelectOne.Copy();

            if (dtSelectOne == null || dtSelectOne.Rows.Count <= 0)
            {
                string strSql = SqlFactory.Frm_1_2_SelectedData_DB_PDI("");
                dtSelectOne = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "PDI", "1-2");
            }
            

            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDataPage, false);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDefectPage, false);
            //ReadOnlyHandler.Instance.Fun_PageClear(Tab_PDIDataPage);
            //ReadOnlyHandler.Instance.Fun_PageClear(Tab_PDIDefectPage);
            //ReadOnlyHandler.Instance.Fun_GroupBoxClear(groupGrinding);
            //ReadOnlyHandler.Instance.Fun_GroupBoxClear(groupHeadStrip);
            //ReadOnlyHandler.Instance.Fun_GroupBoxClear(groupTailStrip);

            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);
            //列印標籤
            Fun_SetBottonEnabled(Btn_PrintTag, true);
            //儲存
            Btn_Save.Visible = true;
            //取消
            Btn_Cancel.Visible = true;

            // 新增状态 key清空
            //计划号
            Txt_Plan_No.Text = string.Empty;
            //入口捲號
            Txt_In_Coil_ID.Text = string.Empty;
           
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            //已是编辑状态 return
            if (bolEditStatus) { return; }

            if (Txt_In_Coil_ID.Text.IsEmpty())
            {
                EventLogHandler.Instance.EventPush_Message($"请先查询欲修改之钢卷");
                return;
            }

            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDataPage, false);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDefectPage, false);

            // 修改状态 key不可改
            //计划号
            Txt_Plan_No.ReadOnly = true;
            Txt_Plan_No.BackColor = Color.Gainsboro;
            //入口鋼卷號
            Txt_In_Coil_ID.ReadOnly = true;
            Txt_In_Coil_ID.BackColor = Color.Gainsboro;

            //新增
            Fun_SetBottonEnabled(Btn_New, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);
            //列印標籤
            Fun_SetBottonEnabled(Btn_PrintTag, true);
            //儲存
            Btn_Save.Visible = true;
            //取消
            Btn_Cancel.Visible = true;
           

            //紀錄是否編輯,狀態
            strEditStatus = "Edit";
            bolEditStatus = true;
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            //DialogResult dialogR = MessageBox.Show("请确认是否删除钢卷编号:" + txtEntryCoil.Text + " PDI?", "提示", MessageBoxButtons.OKCancel);

            if (string.IsNullOrEmpty(Txt_In_Coil_ID.Text.Trim()))
            {

            }
            string strMessage = $"是否删除钢卷编号[{Txt_In_Coil_ID.Text.Trim()}]PDI";

            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "删除PDI", Properties.Resources.dialogQuestion, 1);
            if (dialogR == DialogResult.OK)
            {
                //判斷是否已被刪除
                string strSql = SqlFactory.Frm_1_2_CheckDelete_DB_PDI(Txt_In_Coil_ID.Text);
                DataTable Del_Check = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "刪除PDI", "1-2");

                //如果IsDelete = 1 表示已被刪除
                if (Del_Check.IsNull())
                {
                    EventLogHandler.Instance.EventPush_Message($"钢卷号[{Txt_In_Coil_ID.Text.Trim()}]已被删除，请再次确认");
                    PublicComm.ClientLog.Info($"鋼卷號[{Txt_In_Coil_ID.Text.Trim()}]已被刪除，請重新確認");
                }
                else
                {
                    strSql = SqlFactory.Frm_1_2_DeletePDI_DB_PDI(Txt_Plan_No.Text, Txt_SeqNo.Text, Txt_In_Coil_ID.Text);

                    if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "删除PDI", "1-2"))
                    {
                        EventLogHandler.Instance.EventPush_Message($"钢卷号[{ Txt_In_Coil_ID.Text.Trim()}]删除失敗");
                        return;
                    }

                    //重新整理
                    Reload();

                    //詳細資料ComboBox欄位
                    try
                    {
                        Fun_ComboBox();
                    }
                    catch (Exception ex)
                    {
                        EventLogHandler.Instance.EventPush_Message($"ComboBox选项查询资料库失败:{ex}");
                        EventLogHandler.Instance.LogDebug("1-1", $"查询ComboBox选项", $"查询资料库失败:{ex}");
                        PublicComm.ClientLog.Debug($"查詢ComboBox選項失敗:{ex}");
                        PublicComm.ExceptionLog.Debug($"查詢ComboBox選項失敗:{ex}");
                    }

                    Cob_EntryCoilID.Text = string.Empty;
                }
            }
        }

        #region 缺陷 说明栏
        private void TxtDescription()
        {
            #region 缺陷代码
            Txt_Defect_Code_Desc.Text = "第一位：缺陷种类（1st Byte，Defect Sort）" + Environment.NewLine
                         + "   S_表面缺陷（Surface）" + Environment.NewLine
                         + "   E_边缘缺陷 (Edge)" + Environment.NewLine
                         + "   F_板形缺陷 (Shape)" + Environment.NewLine
                         + "   W_盘卷缺陷 (Coil)" + Environment.NewLine
                         + "   D_尺寸/重量缺陷 (Size/Weight)" + Environment.NewLine
                         + "   M_物性缺陷 (Physics)" + Environment.NewLine
                         + "   C_化性/耐蚀性缺陷 (Chemistry/ Corrosion Resistance)" + Environment.NewLine
                         + "   其它缺陷(Others)" + Environment.NewLine
                         + "第二位与第三位：缺陷种类序号 01,02，……" + Environment.NewLine
                         + "(2nd  Byte And 3rd Byte,Sequence No,01,02, …)" + Environment.NewLine;
            #endregion
            #region 缺陷来源
            Txt_Defect_Origin_Desc.Text = "Sm1:1#炼钢厂(Steel Plant)" + Environment.NewLine
                         + "Rf1:1#Rf" + Environment.NewLine
                         + "Hm1:1#Hsm" + Environment.NewLine
                         + "Ba1:1#Baf" + Environment.NewLine
                         + "Hp1:1#Hapl" + Environment.NewLine
                         + "Rc1:1#Rcl" + Environment.NewLine
                         + "Pa1:1#Pak" + Environment.NewLine
                         + "Rs1:1#Roll_Shop" + Environment.NewLine
                         + "Wh1:1#Warehouse" + Environment.NewLine;
            #endregion
            #region 表面区分
            Txt_Defect_Sid_Desc.Text = "T:上(Top)" + Environment.NewLine
                         + "B:下(Bottom)" + Environment.NewLine
                         + "A:上下都有(Both)" + Environment.NewLine;
            #endregion
            #region 宽向位置
            Txt_Defect_Pos_Desc.Text = "W:操作側1/4板宽 " + Environment.NewLine
                     + "  (1/4 Strip Width Of Operate Side)" + Environment.NewLine
                     + "D:驅動側1/4板宽 " + Environment.NewLine
                     + "  (1/4 Strip Width Of Driver Side)" + Environment.NewLine
                     + "C:中央1/2位置(Center)" + Environment.NewLine
                     + "B:兩側皆有 (Both Side)" + Environment.NewLine
                     + "A:宽度方向全面皆有(All Side)" + Environment.NewLine;
            #endregion
            #region 长向位置
            Txt_Defect_Pos_L_Desc.Text = "比如(Example):" + Environment.NewLine
                         + "缺陷于距带钢头部800m，则为0800" + Environment.NewLine
                         + "Distance Of Strip Head 800m,so the Item is 0800." + Environment.NewLine;
            #endregion
            #region 缺陷程度
            Txt_Defect_Level_Desc.Text = "L：轻微(Light)" + Environment.NewLine
                             + "M：中等(Middle)" + Environment.NewLine
                             + "H：严重(Heavy)" + Environment.NewLine
                             + "S：极严重(Serious)" + Environment.NewLine;
            #endregion
            #region 缺陷比例
            Txt_Defect_Percent_Desc.Text = "比如(Example):" + Environment.NewLine
                             + "13.14% Record To 131;" + Environment.NewLine
                             + "3.16% Record To 032;" + Environment.NewLine
                             + "100% Record To 000" + Environment.NewLine;
            #endregion
        }
        private void Btn_Description_Click(object sender, EventArgs e)
        {
            Pnl_Description.Visible = true;          
            Pnl_Description.Location = new Point(403, 117); //
            Pnl_Description.Size = new Size(1010, 571);
        }
        private void Btn_Defect_Close_Desc_Click(object sender, EventArgs e)
        {
            Pnl_Description.Visible = false;
            Pnl_Description.Location = new Point(1530, 647); //
            Pnl_Description.Size = new Size(117, 46);          
        }
        #endregion 
      
        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            //檢查必填欄位            
            if (!Fun_IsColumnsEmpty()) { return; }

            string str = string.Empty,strSql = string.Empty;

            #region Server: L25功能新增-HMI修改PDI時 通知Server存取資料至L25資料庫            
            string entryCoilID = Txt_In_Coil_ID.Text.Trim();
            string planNo = Txt_Plan_No.Text.Trim();
            PublicComm.Client.Tell(new CS23_InfoPDIModify(planNo, entryCoilID));
            #endregion

            string strIn_Coil_ID = Txt_In_Coil_ID.Text.Trim();//
            string strPlan_No = Txt_Plan_No.Text.Trim();//

            string strCheckSql_PDI = SqlFactory.Frm_1_2_SelectedData_DB_PDI(strIn_Coil_ID, strPlan_No);
            DataTable dt_Check_PDI = Data_Access.Fun_SelectDate(strCheckSql_PDI, GlobalVariableHandler.Instance.strConn_GPL, "Check_PDI资料");
            bolPdiIsNull = dt_Check_PDI.IsNull();

            if (Btn_New.Enabled)
            {
                if (bolPdiIsNull)
                {
                    //無此PDI 可新增
                }
                else
                {
                    //已有PDI 不可新增
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"新增PDI资料");
                    sb.AppendLine($"    计划号:{strPlan_No}");                    
                    sb.AppendLine($"    入口钢卷号:{strIn_Coil_ID}"); 
                    sb.AppendLine($"资料已重复，请重新确认！");

                    DialogHandler.Instance.Fun_DialogShowOk(sb.ToString(), "新增PDI", null, 3);
                    return;
                }
              
            }
            else if (Btn_Edit.Enabled)
            {
                if (bolPdiIsNull)
                {
                    //無此PDI 無法更新資料
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"修改PDI资料");
                    sb.AppendLine($"    计划号:{strPlan_No}");                  
                    sb.AppendLine($"    入口钢卷号:{strIn_Coil_ID}");
                    sb.AppendLine($"查无资料，请重新确认！");

                    DialogHandler.Instance.Fun_DialogShowOk(sb.ToString(), "修改PDI", null, 3);
                    return;
                }
                else
                {
                    //有此PDI 可修改
                }
            }

            if (dtSelectOne.Rows.Count <= 0)
            {
                DataRow dr = dtSelectOne.NewRow();

                try
                {
                    dtSelectOne.LoadDataRow(dr.ItemArray, false);
                }
                catch { }
            }
            else { }

            if (Btn_New.Enabled)
            {               
                strSql = SqlFactory.Frm_1_2_InsertPDI_DB_PDI(dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Qc_Rmark)].ToString());
                str = "新增";
            }
            else if (Btn_Edit.Enabled)
            {
                strSql = SqlFactory.Frm_1_2_SavePDI_DB_PDI(dtSelectOne.Rows[0][nameof(PDIEntity.TBL_PDI.Qc_Rmark)].ToString());
                str = "修改";
            }

            try
            {
               if(! Data_Access.GetInstance().ExecuteNonQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"新增/修改PDI失败", "新增/修改PDI", null, 3);
                    return;
                }

                EventLogHandler.Instance.EventPush_Message($"{str}钢卷号[{Txt_In_Coil_ID.Text.Trim()}]储存成功");
                EventLogHandler.Instance.LogInfo("1-2", $"使用者:{PublicForms.Main.lblLoginUser.Text}储存钢卷编号:{Txt_In_Coil_ID.Text.Trim()} PDI", $"储存{str}钢卷编号:[{Txt_In_Coil_ID.Text.Trim()}]PDI");
                PublicComm.ClientLog.Debug($"[{Txt_In_Coil_ID.Text.Trim()}]PDI儲存成功");
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"{str}欄位資料有誤:{ex}]");
                PublicComm.ClientLog.Debug($"[{Txt_In_Coil_ID.Text.Trim()}]PDI儲存失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"[{Txt_In_Coil_ID.Text.Trim()}]PDI儲存失敗:{ex}");
                //有錯誤就還原
                //缺陷資料
                try
                {
                    Fun_SelectedData(Cob_EntryCoilID.Text);
                }
                catch (Exception _ex)
                {
                    EventLogHandler.Instance.EventPush_Message($"查询{Cob_EntryCoilID.Text.Trim()} PDI失败:{_ex}");
                    EventLogHandler.Instance.LogDebug("1-2", $"查询PDI", $"查询{Cob_EntryCoilID.Text.Trim()} PDI失败:{_ex}");
                    PublicComm.ClientLog.Debug($"查詢{Cob_EntryCoilID.Text.Trim()} PDI失敗:{_ex}");
                    PublicComm.ExceptionLog.Debug($"查詢{Cob_EntryCoilID.Text.Trim()} PDI失敗:{_ex}");
                }

                //缺陷資料
                try
                {
                    Fun_DisplayDefectData();
                }
                catch (Exception _ex)
                {
                    EventLogHandler.Instance.EventPush_Message($"缺陷资料查询资料库失败:{_ex}");
                    EventLogHandler.Instance.LogDebug("1-1", $"查询缺陷资料", $"查询资料库失败:{_ex}");
                    PublicComm.ClientLog.Debug($"查詢缺陷資料失敗:{_ex}");
                    PublicComm.ExceptionLog.Debug($"查詢缺陷資料失敗:{_ex}");
                }

                //詳細資料欄位填入
                try
                {
                    Fun_DisplayPDIDetail();
                }
                catch (Exception _ex)
                {
                    EventLogHandler.Instance.EventPush_Message($"详细资料填入失败:{_ex}");
                    EventLogHandler.Instance.LogDebug("1-1", $"详细资料填入", $"详细资料填入失败:{_ex}");
                    PublicComm.ClientLog.Debug($"詳細資料填入失敗:{_ex}");
                    PublicComm.ExceptionLog.Debug($"詳細資料填入失敗:{_ex}");
                }
            }

            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDataPage, true);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDefectPage, true);
           
            //新增
            Fun_SetBottonEnabled(Btn_New, true);           
            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);
            //儲存
            Btn_Save.Visible = false;
            //取消
            Btn_Cancel.Visible = false;

            strEditStatus = "Read";
            bolEditStatus = false;

            if (Pnl_Spare.Visible == true)
                Pnl_Spare.Visible = false;
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            //不是编辑状态 return
            if (!bolEditStatus) { return; }

            

            try
            {
                Fun_SelectedData(Cob_EntryCoilID.Text);
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"查询{Cob_EntryCoilID.Text.Trim()} PDI失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-1", $"查询PDI", $"查询{Cob_EntryCoilID.Text.Trim()} PDI失败:{ex}");
                PublicComm.ClientLog.Debug($"查詢{Cob_EntryCoilID.Text.Trim()} PDI失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"查詢{Cob_EntryCoilID.Text.Trim()} PDI失敗:{ex}");
                return;
            }

            //缺陷資料
            try
            {
                Fun_DisplayDefectData();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"缺陷资料查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-1", $"查询缺陷资料", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"查詢缺陷資料失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"查詢缺陷資料失敗:{ex}");
            }

            //詳細資料欄位填入
            try
            {
                Fun_DisplayPDIDetail();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"详细资料填入失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-1", $"详细资料填入", $"详细资料填入失败:{ex}");
                PublicComm.ClientLog.Debug($"詳細資料填入失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"詳細資料填入失敗:{ex}");
            }

            EventLogHandler.Instance.LogInfo("1-2", $"使用者:{PublicForms.Main.lblLoginUser.Text}取消", $"取消并代回钢卷编号:{Cob_EntryCoilID.Text.Trim()}PDI资料");
            PublicComm.ClientLog.Debug($"取消編輯{Cob_EntryCoilID.Text.Trim()} PDI");
           
            
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDataPage, true);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDefectPage, true);
            Btn_Cancel.Visible = false; //取消
            Btn_Save.Visible = false; //儲存
            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            if (string.IsNullOrEmpty(Txt_In_Coil_ID.Text.Trim()))
            {
                //修改
                Fun_SetBottonEnabled(Btn_Edit, false);
                Fun_SetBottonEnabled(Btn_Delete, false);
            }
            else
            {
                //修改
                Fun_SetBottonEnabled(Btn_Edit, true);
                Fun_SetBottonEnabled(Btn_Delete, true);
            }
            //列印標籤
            Fun_SetBottonEnabled(Btn_PrintTag, true);
            //取消
            Btn_Cancel.Visible = false;
            //儲存
            Btn_Save.Visible = false;

            //取消编辑后,恢复唯读状态
            strEditStatus = "Read";
            bolEditStatus = false;

            if (Pnl_Spare.Visible == true)
                Pnl_Spare.Visible = false;
        }

        /// <summary>
        /// 返回排程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Schedule_Click(object sender, EventArgs e)
        {
            Frm_0_0_Main FatherForm = Parent.Parent as Frm_0_0_Main;
            FatherForm.tsMenuItem_1_1.PerformClick();

            EventLogHandler.Instance.LogInfo("1-2", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}返回1-1入料钢卷排程资讯", "返回1-1入料钢卷排程资讯");
            PublicComm.ClientLog.Info($"返回入料鋼卷排程資訊成功");
        }

        /// <summary>
        /// 重新整理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ReLoad_Click(object sender, EventArgs e)
        {
            try
            {
                Reload();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"重新整理失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-1", $"重新整理", $"重新整理失败:{ex}");
                PublicComm.ClientLog.Debug($"重新整理失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"重新整理失敗:{ex}");
                return;
            }
        }
        
        //檢查必填欄位是否空白
        private bool Fun_IsColumnsEmpty()
        {
            //入口钢卷号
            if (string.IsNullOrEmpty(Txt_In_Coil_ID.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_In_Coil_ID_Title.Text.Replace("*","")} 请勿空白! ", "提示资讯", null, 0);
                Txt_In_Coil_ID.Focus();
                return false;
            }

            //计划号
            if (string.IsNullOrEmpty(Txt_Plan_No.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_Plan_No_Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
                Txt_Plan_No.Focus();
                return false;
            }

            //出口钢卷号
            if (string.IsNullOrEmpty(Txt_Out_Coil_ID.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_Out_Coil_ID_Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
                Txt_Out_Coil_ID.Focus();
                return false;
            }

            //入料厚
            if (string.IsNullOrEmpty(Txt_In_Coil_Thick.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_In_Coil_Thick_Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
                Txt_In_Coil_Thick.Focus();
                return false;
            }

            //入料寬
            if (string.IsNullOrEmpty(Txt_In_Coil_Width.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_In_Coil_Width_Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
                Txt_In_Coil_Width.Focus();
                return false;
            }

            //入料長
            if (string.IsNullOrEmpty(Txt_In_Coil_Length.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_In_Coil_Length_Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
                Txt_In_Coil_Length.Focus();
                return false;
            }                       

            //入料重
            if (string.IsNullOrEmpty(Txt_In_Coil_Wt.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_In_Coil_Wt_Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
                Txt_In_Coil_Wt.Focus();
                return false;
            }

            //入料內徑
            if (string.IsNullOrEmpty(Txt_In_Coil_Inner_Diameter.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_In_Coil_Inner_Diameter_Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
                Txt_In_Coil_Inner_Diameter.Focus();
                return false;
            }

            //入料外徑
            if (string.IsNullOrEmpty(Txt_In_Coil_Outer_Diameter.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_In_Coil_Outer_Diameter_Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
                Txt_In_Coil_Outer_Diameter.Focus();
                return false;
            }

            //目標厚度
            if (string.IsNullOrEmpty(Txt_Out_Coil_Thick.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_Out_Coil_Thick_Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
                Txt_Out_Coil_Thick.Focus();
                return false;
            }

            //厚度最大值
            if (string.IsNullOrEmpty(Txt_Out_Coil_Thick_Max.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_Out_Coil_Thick_Max_Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
                Txt_Out_Coil_Thick_Max.Focus();
                return false;
            }

            //厚度最小值
            if (string.IsNullOrEmpty(Txt_Out_Coil_Thick_Min.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_Out_Coil_Thick_Min_Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
                Txt_Out_Coil_Thick_Min.Focus();
                return false;
            }

            //目標寬度
            if (string.IsNullOrEmpty(Txt_Out_Coil_Width.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_Out_Coil_Width_Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
                Txt_Out_Coil_Width.Focus();
                return false;
            }

            //開捲方向
            if (string.IsNullOrEmpty(Cob_Uncoil_Direction.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Lbl_Uncoil_Direction_Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
                Cob_Uncoil_Direction.Focus();
                return false;
            }           

            ////
            //if (string.IsNullOrEmpty(Txt_.Text.Trim()))
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk($"{Lbl__Title.Text.Replace("*", "")} 请勿空白! ", "提示资讯", null, 0);
            //    Txt_.Focus();
            //    return false;
            //}

            return true;
        }

        #region 重新整理
        /// <summary>
        /// 重新整理
        /// </summary>
        private void Reload()
        {
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDataPage, true);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDefectPage, true);

            #region [清空]
            Txt_Plan_No.Text = string.Empty;
            Txt_SeqNo.Text = string.Empty;
            Cob_Plan_Sort.SelectedIndex = 0;
            Txt_In_Coil_ID.Text = string.Empty;
            Txt_In_Coil_Thick.Text = string.Empty;
            Txt_In_Coil_Width.Text = string.Empty;
            Txt_In_Coil_Wt.Text = string.Empty;
            Txt_In_Coil_Length.Text = string.Empty;
            Txt_In_Coil_Inner_Diameter.Text = string.Empty;
            Txt_In_Coil_Outer_Diameter.Text = string.Empty;
            Cob_Sleeve_Type_Entry.SelectedIndex = 0;
            Txt_Sleeve_Inner_Entry.Text = string.Empty;
            Cob_PAPER_REQ_CODE.SelectedIndex = 0;
            Cob_In_Paper_Type_Entry.SelectedIndex = 0;
            Txt_In_Paper_Head_Length.Text = string.Empty;
            Txt_In_Paper_Head_Width.Text = string.Empty;
            Txt_In_Paper_Tail_Length.Text = string.Empty;
            Txt_In_Paper_Tail_Width.Text = string.Empty;
            Txt_St_no.Text = string.Empty;
            Txt_Density.Text = string.Empty;
            Cob_Rework_Type.SelectedIndex = 0;

            Cob_Surface_Finishing_Code.SelectedIndex = 0;
            Cob_Surface_Accuracy_Code.SelectedIndex = 0;
            Cob_Surface_Accu_Code_In.SelectedIndex = 0;
            Cob_Surface_Accu_Code_Out.SelectedIndex = 0;
            Txt_Surface_Accu_Desc.Text = string.Empty;
            Txt_Surface_Accuracy_Desc.Text = string.Empty;
            Txt_Surface_Accu_Desc_In.Text = string.Empty;
            Txt_Surface_Accu_Desc_Out.Text = string.Empty;

            Cob_Base_Surface.SelectedIndex = 0;
            Cob_Uncoil_Direction.SelectedIndex = 0;
            Txt_Out_Coil_ID.Text = string.Empty;
            Cob_OUT_PAPER_REQ_CODE.SelectedIndex = 0;
            Cob_Paper_Type_Exit.SelectedIndex = 0;
            Txt_Sleeve_Inner_Exit.Text = string.Empty;
            Cob_Sleeve_Type_Exit.SelectedIndex = 0;
            Txt_Strap.Text = string.Empty;
            Cob_CoilOrigin.SelectedIndex = 0;
            Txt_Wholebacklog.Text = string.Empty;
            Txt_NWholebacklog.Text = string.Empty;
            Cob_GrandFlag.SelectedIndex = 0;
            Txt_Out_Coil_Width.Text = string.Empty;
            Txt_Out_Coil_Thick_Max.Text = string.Empty;
            Txt_Out_Coil_Thick_Min.Text = string.Empty;
            Txt_Out_Coil_Thick.Text = string.Empty;
            Txt_OutInner.Text = string.Empty;
            Txt_Order_No.Text = string.Empty;
            Txt_HSt_no.Text = string.Empty;
            Txt_HLen.Text = string.Empty;
            Txt_HWd.Text = string.Empty;
            Txt_HThickness.Text = string.Empty;
            Txt_TSt_no.Text = string.Empty;
            Txt_TLen.Text = string.Empty;
            Txt_TWd.Text = string.Empty;
            Txt_TThickness.Text = string.Empty;
            Txt_HEAD_OFF_GAUGE.Text = string.Empty;
            Txt_Tail_off_gauge.Text = string.Empty;
            #endregion
        }
        #endregion

        /// <summary>
        /// 入口套筒類型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Sleeve_Type_Entry_Click(object sender, EventArgs e)
        {
            Lbl_ComboName_Spare_Title.Text = "入口套筒类型";
            PanelSetting(Cbo_Type.Sleeve_Type);
        }
        /// <summary>
        /// 入口墊紙方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PAPER_REQ_CODE_Click(object sender, EventArgs e)
        {
            Lbl_ComboName_Spare_Title.Text = "入口垫纸方式";
            PanelSetting(Cbo_Type.PAPER_REQ_CODE);
        }
        /// <summary>
        /// 入口墊紙類型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_In_Paper_Type_Entry_Click(object sender, EventArgs e)
        {
            Lbl_ComboName_Spare_Title.Text = "入口垫纸类型";
            PanelSetting(Cbo_Type.Paper_Type);
        }
        /// <summary>
        /// 出口墊紙方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_OUT_PAPER_REQ_CODE_Click(object sender, EventArgs e)
        {
            Lbl_ComboName_Spare_Title.Text = "出口垫纸方式";
            PanelSetting(Cbo_Type.PAPER_REQ_CODE);
        }
        /// <summary>
        /// 出口墊紙種類
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Paper_Type_Exit_Click(object sender, EventArgs e)
        {
            Lbl_ComboName_Spare_Title.Text = "出口垫纸类型";
            PanelSetting(Cbo_Type.Paper_Type);
        }
        /// <summary>
        /// 出口套筒類型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Sleeve_Type_Exit_Click(object sender, EventArgs e)
        {
            Lbl_ComboName_Spare_Title.Text = "出口套筒类型";
            PanelSetting(Cbo_Type.Sleeve_Type);
        }
        /// <summary>
        /// 鋼捲來源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_CoilOrigin_Click(object sender, EventArgs e)
        {
            Lbl_ComboName_Spare_Title.Text = "钢卷来源";
            PanelSetting(Cbo_Type.Origin);
        }
        /// <summary>
        /// 订单表面精度代碼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Surface_Accuracy_Code_Click(object sender, EventArgs e)
        {
            Lbl_ComboName_Spare_Title.Text = "表面精度代码";
            PanelSetting(Cbo_Type.Surface_Accuracy);
        }

        private void PanelSetting(Cbo_Type cbo_Type)
        {
            Pnl_Spare.Visible = true;
            int PointX = 0, PointY = 0;
                       
            //ComboBox 的說明Panel 預設大小
            Pnl_Spare.Size = new Size(456, 226);//456, 226
            Txt_Spare.Size = new Size(438, 174);
            Btn_Close_Spare.Location = new Point(415, 5);
            Txt_Spare.ScrollBars = ScrollBars.None;

            switch (Lbl_ComboName_Spare_Title.Text)
            {
                case "入口套筒类型":
                    PointX = 340;
                    PointY = 380;
                    Txt_Spare.ScrollBars = ScrollBars.Both;
                    ComboBoxIndexHandler.Instance.Fun_SleeveSpareDisplay(Txt_Spare);
                    break;

                case "入口垫纸方式":
                    PointX = 340;
                    PointY = 380;
                    ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
                    break;

                case "入口垫纸类型":
                    PointX = 340;
                    PointY = 380;
                    Pnl_Spare.Size = new Size(575, 226);
                    Txt_Spare.Size = new Size(555, 174);
                    Btn_Close_Spare.Location = new Point(532, 5);
ComboBoxIndexHandler.Instance.Fun_PaperSpareDisplay(Txt_Spare);
                    break;

                case "出口垫纸方式":
                    PointX = 1165;
                    PointY = 30;
                    ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
                    break;

                case "出口垫纸类型":
                    PointX = 1165;
                    PointY = 30;
                    Pnl_Spare.Size = new Size(575, 226);
                    Txt_Spare.Size = new Size(555, 174);
                    Btn_Close_Spare.Location = new Point(532, 5); ComboBoxIndexHandler.Instance.Fun_PaperSpareDisplay(Txt_Spare);

                    break;

                case "出口套筒类型":
                    PointX = 1165;
                    PointY = 30;
                    Txt_Spare.ScrollBars = ScrollBars.Both;
                    ComboBoxIndexHandler.Instance.Fun_SleeveSpareDisplay(Txt_Spare);
                    break;

                case "钢卷来源":
                    PointX = 786;
                    PointY = 63;
                    Txt_Spare.ScrollBars = ScrollBars.Both;
                    ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
                    break;

                case "表面精度代码":
                    PointX = 786;
                    PointY = 407;
                    Txt_Spare.ScrollBars = ScrollBars.Both;
                    ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
                    break;

                default:
                    break;
            }
            Pnl_Spare.Location = new Point(PointX, PointY);
        }
        /// <summary>
        /// 關閉說明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ClosePanel_Click(object sender, EventArgs e)
        {
            Pnl_Spare.Visible = false;
        }

        /// <summary>
        /// 入口套筒类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Sleeve_Type_Entry_Click(object sender, EventArgs e)
        {
            //入口套筒類型
            ComboBoxIndexHandler.Instance.SelectSleeve(Cob_Sleeve_Type_Entry);
        }

        /// <summary>
        /// 出口套筒类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Sleeve_Type_Exit_Click(object sender, EventArgs e)
        {
            //出口套筒類型
            ComboBoxIndexHandler.Instance.SelectSleeve(Cob_Sleeve_Type_Exit);
        }

        /// <summary>
        /// 入口钢卷清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_EntryCoilID_Click(object sender, EventArgs e)
        {
            //鋼卷號清單
            try
            {
                Fun_ComboBoxItems();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"钢卷号清单查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-2", $"钢卷号清单", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"钢卷号清单查询资料库失败:{ex}");
                PublicComm.ExceptionLog.Debug($"钢卷号清单查询资料库失败:{ex}");
            }
        }

        /// <summary>
        /// 入口垫纸类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_In_Paper_Type_Entry_Click(object sender, EventArgs e)
        {
            //入口套筒類型
            ComboBoxIndexHandler.Instance.SelectSleeve(Cob_Sleeve_Type_Entry);
        }

        /// <summary>
        /// 出口垫纸类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Paper_Type_Exit_Click(object sender, EventArgs e)
        {
            //出口墊紙類型
            ComboBoxIndexHandler.Instance.SelectPaper(Cob_Paper_Type_Exit);
        }

        private void Btn_PrintTag_Click(object sender, EventArgs e)
        {
            //if (txtEntryCoil.Text.Trim().IsEmpty())
            //{
            //    EventLogHandler.Instance.EventPush_Message($"请查询欲打印标签之钢卷");
            //    return;
            //}

            Frm_PrintLabels frm_Print = new Frm_PrintLabels
            {
                Str_Coil_No = Txt_In_Coil_ID.Text.Trim()
            };
            frm_Print.ShowDialog();
            frm_Print.Dispose();

            if (frm_Print.DialogResult == DialogResult.OK)
            {
                string strShowText = $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 打印钢卷号:[{frm_Print.Str_Coil_No}]标签";
                EventLogHandler.Instance.LogInfo("1-2", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} 打印标签作业", $"打印钢卷号:[{frm_Print.Str_Coil_No}]标签");
                EventLogHandler.Instance.EventPush_Message(strShowText);
                PublicComm.ClientLog.Info(strShowText);

            }

            ////改由HMI直接列印
            //SCCommMsg.CS07_PrintLabel _PrintLabel = new SCCommMsg.CS07_PrintLabel
            //{
            //    Source = "GPL_HMI",
            //    ID = "PrintLabel",
            //    CoilID = txtEntryCoil.Text.Trim()
            //};
            //PublicComm.Client.Tell(_PrintLabel);
            //EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}打印标签作业", $"打印{txtEntryCoil.Text.Trim()}标签");
            //EventLogHandler.Instance.EventPush_Message($"列印[{txtEntryCoil.Text.Trim()}]标签");
            //PublicComm.ClientLog.Info($"通知Server列印钢卷號:[{txtEntryCoil.Text.Trim()}]标签");
        }

        //設定按鈕啟用狀態並改顏色
        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;
            if ( btn.Name.Equals(Btn_PrintTag.Name))
                btn.BackColor = bolE ? Color.Gold : Color.LightGray;
            else
                //Color colorBack;
                btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
            ////Color colorBack;
            //btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
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

        

        private void Fun_OnlyNumberPoin_KeyPress(object sender, KeyPressEventArgs e)
        {
            //&& (e.KeyChar != (char)Keys.Space) 
            //          數字                                            //backspace                    //Enter             
            //if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Enter)))
            //{
            //    e.Handled = true;
            //}
            base.OnKeyPress(e);
            if (!(
                (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == '.' && (this.Text.IndexOf(e.KeyChar) < 0) && (this.Text.Length > 0)) ||
                (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Enter)))//&& (e.KeyChar != (char)Keys.Space)
            {               //backspace                    //Enter
                e.Handled = true;
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

    }
}

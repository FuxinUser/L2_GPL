using System;
using System.Data;
using System.Windows.Forms;
using Akka.Actor;
using DataModel.HMIServerCom.Msg;
using DBService.Repository.PDO;
using static GPLManager.DataBaseTableFactory;

namespace GPLManager
{
    public partial class frm_PDOConfirm : Form
    {
        #region 變數
        string strSql = "";
        DataTable dt_PDO = new DataTable();
        DataTable dt_Out_mat_No = new DataTable();
        #endregion

        public frm_PDOConfirm()
        {
            InitializeComponent();
        }
        private void frm_PDOConfirm_Load(object sender, EventArgs e)
        {
            ComboBoxLoad();
        }

        #region ComboBox
        /// <summary>
        /// ComboBox Items
        /// </summary>
        private void ComboBoxLoad()
        {
            //班次
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Shift, cbo_Shift);
            //班別
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Team, cbo_Team);
            //取樣要求
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Samp, cbo_Sample_flag);
            //分卷标记
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Trim, cbo_Segement_Flag);
            //最终卷标记
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.End, cbo_end_flag);
            //廢品標記
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Scrap, cbo_SCRAP_FLAG);
            //出口墊紙種類
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Paper_Type, cbo_PAPER_CODE);
            //卷曲方向
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Winding_Direction, cbo_WINDING_Direction);
            //好面朝向
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Base_Surface, cbo_Base_Surface);
            //出口套筒類型
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Sleeve_Type, cbo_Sleeve_Type_Exit);
            //表面精度
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, cbo_SURFACE_ACCU_CODE);
            //内表面精度
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, cbo_SURFACE_ACCU_CODE_IN);
            //外表面精度
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, cbo_SURFACE_ACCU_CODE_OUT);
            //封鎖標記
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Hold, cbo_Hold_flag);

            strSql = SqlFactory.Frm_PDOConfirm_CoilIDComboBoxItems_DB_Map_PDO();
            dt_Out_mat_No = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"PDO确认出口卷号清单");

            if (dt_Out_mat_No == null) return;
            if (dt_Out_mat_No.Rows.Count.Equals(0)) return;

            cbo_Out_mat_NO.DisplayMember = nameof(PDOModel.L2L3_PDO.Out_Mat_No);
            cbo_Out_mat_NO.ValueMember = nameof(PDOModel.L2L3_PDO.Out_Mat_No);
            cbo_Out_mat_NO.DataSource = dt_Out_mat_No;
        }
        #endregion 

        #region PDO資料
        /// <summary>
        /// PDO資料組成
        /// </summary>
        public void PDOselect_Table(string Coil_ID)
        {
            strSql = SqlFactory.Frm_PDOConfirm_PDOSelect_DB_PDO(Coil_ID);
            dt_PDO = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "PDO确认");

            if (dt_PDO == null) return;
            if (dt_PDO.Rows.Count.Equals(0)) return;

            Fun_PDO_FillData();
            PDO_FillDefectData();
        }
        /// <summary>
        /// PDO詳細資料填入
        /// </summary>
        private void Fun_PDO_FillData()
        {
            //合同號
            txtOrder_No.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.OrderNo)].ToString() ?? string.Empty;
            //計畫號
            txtPlan_No.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.PlanNo)].ToString() ?? string.Empty;
            //出口號
            txtOut_mat_No.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Out_Mat_No)].ToString() ?? string.Empty;
            //入口號
            txtEntryCoilId.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.In_Mat_No)].ToString() ?? string.Empty;
            //生產開始時間
            txtStartTime.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.StartTime)].ToString() ?? string.Empty;
            //生產結束時間
            txtEndTime.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.FinishTime)].ToString() ?? string.Empty;
            //班次
            cbo_Shift.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Shift)].ToString().Trim() ?? string.Empty;
            //班別
            cbo_Team.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Team)].ToString().Trim() ?? string.Empty;
            //取樣標記
            cbo_Sample_flag.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Sample_Flag)].ToString().Trim() ?? string.Empty;
            //取樣位置
            cbo_SAMPLE_FRQN_CODE.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Sample_Frqn_Code)].ToString().Trim() ?? string.Empty;
            //分卷標記
            cbo_Segement_Flag.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Fixed_Wt_Flag)].ToString().Trim() ?? string.Empty;
            //最終卷標記
            cbo_end_flag.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.End_Flag)].ToString().Trim() ?? string.Empty;
            //廢品標記
            cbo_SCRAP_FLAG.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Scrap_Flag)].ToString().Trim() ?? string.Empty;
            //是否有油
            cbo_Oil.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Oil_Flag)].ToString().Trim() ?? string.Empty;
            //卷曲方向
            cbo_WINDING_Direction.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Winding_Dire)].ToString().Trim() ?? string.Empty;
            //好面朝向
            cbo_Base_Surface.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.BaseSurface)].ToString().Trim() ?? string.Empty;
            //是否使用套筒
            cbo_ExitSleeveUseOrNot.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.ExitSleeveUseOrNot)].ToString().Trim() ?? string.Empty;
            //套筒類型
            cbo_Sleeve_Type_Exit.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.ExitSleeveCode)].ToString().Trim() ?? string.Empty;
            //套筒內徑
            txt_Sleeve_Inner_Exit.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.ExitSleeveDiameter)].ToString() ?? string.Empty;
            //鋼卷外徑
            txtOuterDiameter.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Out_Mat_Outer_Diameter)].ToString() ?? string.Empty;
            //鋼卷內徑
            txtInner.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Out_Mat_Inner)].ToString() ?? string.Empty;
            //理論重
            txtTheory_Wt.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Out_Theory_Wt)].ToString() ?? string.Empty;
            //淨重
            txtWt.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Out_Mat_Wt)].ToString() ?? string.Empty;
            //毛重
            txtGsWt.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Out_Mat_Gross_Wt)].ToString() ?? string.Empty;
            //出口卷厚度
            txtThick.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Out_Mat_Thick)].ToString() ?? string.Empty;
            //出口卷寬度
            txtWidth.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Out_Mat_Width)].ToString() ?? string.Empty;
            //出口卷長度
            txtLength.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Out_Mat_Length)].ToString() ?? string.Empty;
            //鋼種
            txtSt_No.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.St_No)].ToString() ?? string.Empty;
            //頭部C40厚度
            txtHead_C40_Thick.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Head_C40_Thick)].ToString() ?? string.Empty;
            //中部C40厚度
            txtMid_C40_Thick.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Mid_C40_Thick)].ToString() ?? string.Empty;
            //尾部C40厚度
            txtTail_C40_Thick.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Tail_C40_Thick)].ToString() ?? string.Empty;
            //頭部C25厚度
            txtHead_C25_Thick.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Head_C25_Thick)].ToString() ?? string.Empty;
            txtMid_C25_Thick.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Mid_C25_Thick)].ToString() ?? string.Empty;
            //尾部C25厚度
            txtTail_C25_Thick.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Tail_C25_Thick)].ToString() ?? string.Empty;
            //頭部道次
            txtHead_Pass_Num.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Head_Pass_Num)].ToString() ?? string.Empty;
            //中部道次
            txtMid_Pass_Num.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Mid_Pass_Num)].ToString() ?? string.Empty;
            //尾部道次
            txtTail_Pass_Num.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Tail_Pass_Num)].ToString() ?? string.Empty;
            //出口墊紙类型
            cbo_PAPER_CODE.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Paper_Code)].ToString().Trim() ?? string.Empty;
            //出口墊紙方式
            cbo_PAPER_REQ_CODE.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Paper_Req_Code)].ToString().Trim() ?? string.Empty;
            //頭部墊紙長度
            txtOut_Paper_Head_Length.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Head_Paper_Length)].ToString() ?? string.Empty;
            //頭部墊紙寬度
            txtOut_Paper_Head_Width.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Head_Paper_Width)].ToString() ?? string.Empty;
            //尾部墊紙長度
            txtOut_Paper_Tail_Length.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Tail_Paper_Length)].ToString() ?? string.Empty;
            //尾部墊紙寬度
            txtOut_Paper_Tail_Width.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Tail_Paper_Width)].ToString() ?? string.Empty;
            //頭部打孔位置
            txtHead_PunchHole_Position.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Head_Hole_Position)].ToString() ?? string.Empty;
            //頭部導帶長度
            txtHead_LeaderLength.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Head_LeaderLength)].ToString() ?? string.Empty;
            //頭部導帶寬度
            txtHead_Leader_Width.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Head_Leader_Width)].ToString() ?? string.Empty;
            //頭部導帶厚度
            txtHead_Leader_Thickness.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Head_Leader_Thickness)].ToString() ?? string.Empty;
            //頭部導帶鋼種
            txtHead_leader_st_no.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Head_Leader_St_No)].ToString() ?? string.Empty;
            //尾部打孔位置
            txtTail_PunchHole_Position.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Tail_Hole_Position)].ToString() ?? string.Empty;
            //尾部導帶長度
            txtTail_LeaderLength.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Tail_LeaderLength)].ToString() ?? string.Empty;
            //尾部導帶寬度
            txtTail_Leader_Width.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Tail_Leader_Width)].ToString() ?? string.Empty;
            //尾部導帶厚度
            txtTail_Leader_Thickness.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Tail_Leader_Thickness)].ToString() ?? string.Empty;
            //尾部導帶鋼種
            txtTail_Leader_St_No.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Tail_Leader_St_No)].ToString() ?? string.Empty;
            //頭部未軋製區域
            txtHEAD_OFF_GAUGE.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Head_Off_Gauge)].ToString() ?? string.Empty;
            //尾部未軋製區域
            txtTail_off_gauge.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Tail_Off_Gauge)].ToString() ?? string.Empty;
            //上次研磨面
            cbo_PRE_GRINDING_SURFACE.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Pre_Grinding_Surface)].ToString().Trim() ?? string.Empty;
            //外表面研磨次數
            txtGRINDING_COUNT_OUT.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Grinding_Count_Out)].ToString() ?? string.Empty;
            //內表面研磨次數
            txtGRINDING_COUNT_IN.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Grinding_Count_In)].ToString() ?? string.Empty;
            //指定研磨面
            cbo_APPOINT_GRINDING_SURFACE.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Appoint_Grinding_Surface)].ToString().Trim() ?? string.Empty;
            //内表面精度代碼
            cbo_SURFACE_ACCU_CODE_IN.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Surface_Accu_Code_In)].ToString().Trim() ?? string.Empty;
            //外表面精度代碼
            cbo_SURFACE_ACCU_CODE_OUT.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Surface_Accu_Code_Out)].ToString().Trim() ?? string.Empty;
            //表面精度代碼
            cbo_SURFACE_ACCU_CODE.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Surface_Accu_Code)].ToString().Trim() ?? string.Empty;
            //工序代码
            txtProcessCode.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.ProcessCode)].ToString() ?? string.Empty;
            //頭部粗糙度
            txtHead_Rough.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Head_Rough)].ToString() ?? string.Empty;
            //中部粗糙度
            txtMid_Rough.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Mid_Rough)].ToString() ?? string.Empty;
            //尾部粗糙度
            txtTail_Rough.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Tail_Rough)].ToString() ?? string.Empty;
            //實際開捲方向
            cbo_Decoiler.SelectedValue = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.Decoiler_Direction)].ToString().Trim() ?? string.Empty;
        }
        /// <summary>
        /// PDO缺陷資料填入
        /// </summary>
        private void PDO_FillDefectData()
        {

            #region - Sid -
            txtSid_D1.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D01_Sid)].ToString() ?? string.Empty;
            txtSid_D2.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D02_Sid)].ToString() ?? string.Empty;
            txtSid_D3.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D03_Sid)].ToString() ?? string.Empty;
            txtSid_D4.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D04_Sid)].ToString() ?? string.Empty;
            txtSid_D5.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D05_Sid)].ToString() ?? string.Empty;
            txtSid_D6.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D06_Sid)].ToString() ?? string.Empty;
            txtSid_D7.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D07_Sid)].ToString() ?? string.Empty;
            txtSid_D8.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D08_Sid)].ToString() ?? string.Empty;
            txtSid_D9.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D09_Sid)].ToString() ?? string.Empty;
            txtSid_D10.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D10_Sid)].ToString() ?? string.Empty;
            #endregion

            #region - Pos_W -
            txtPos_W_D1.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D01_Pos_W)].ToString() ?? string.Empty;
            txtPos_W_D2.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D02_Pos_W)].ToString() ?? string.Empty;
            txtPos_W_D3.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D03_Pos_W)].ToString() ?? string.Empty;
            txtPos_W_D4.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D04_Pos_W)].ToString() ?? string.Empty;
            txtPos_W_D5.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D05_Pos_W)].ToString() ?? string.Empty;
            txtPos_W_D6.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D06_Pos_W)].ToString() ?? string.Empty;
            txtPos_W_D7.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D07_Pos_W)].ToString() ?? string.Empty;
            txtPos_W_D8.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D08_Pos_W)].ToString() ?? string.Empty;
            txtPos_W_D9.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D09_Pos_W)].ToString() ?? string.Empty;
            txtPos_W_D10.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D10_Pos_W)].ToString() ?? string.Empty;
            #endregion

            #region - Pos_L_Start - 
            txtPos_L_Start_D1.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D01_Pos_L_Start)].ToString() ?? string.Empty;
            txtPos_L_Start_D2.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D02_Pos_L_Start)].ToString() ?? string.Empty;
            txtPos_L_Start_D3.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D03_Pos_L_Start)].ToString() ?? string.Empty;
            txtPos_L_Start_D4.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D04_Pos_L_Start)].ToString() ?? string.Empty;
            txtPos_L_Start_D5.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D05_Pos_L_Start)].ToString() ?? string.Empty;
            txtPos_L_Start_D6.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D06_Pos_L_Start)].ToString() ?? string.Empty;
            txtPos_L_Start_D7.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D07_Pos_L_Start)].ToString() ?? string.Empty;
            txtPos_L_Start_D8.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D08_Pos_L_Start)].ToString() ?? string.Empty;
            txtPos_L_Start_D9.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D09_Pos_L_Start)].ToString() ?? string.Empty;
            txtPos_L_Start_D10.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D10_Pos_L_Start)].ToString() ?? string.Empty;
            #endregion

            #region - Pos_L_End -
            txtPos_L_End_D1.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D01_Pos_L_End)].ToString() ?? string.Empty;
            txtPos_L_End_D2.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D02_Pos_L_End)].ToString() ?? string.Empty;
            txtPos_L_End_D3.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D03_Pos_L_End)].ToString() ?? string.Empty;
            txtPos_L_End_D4.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D04_Pos_L_End)].ToString() ?? string.Empty;
            txtPos_L_End_D5.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D05_Pos_L_End)].ToString() ?? string.Empty;
            txtPos_L_End_D6.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D06_Pos_L_End)].ToString() ?? string.Empty;
            txtPos_L_End_D7.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D07_Pos_L_End)].ToString() ?? string.Empty;
            txtPos_L_End_D8.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D08_Pos_L_End)].ToString() ?? string.Empty;
            txtPos_L_End_D9.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D09_Pos_L_End)].ToString() ?? string.Empty;
            txtPos_L_End_D10.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D10_Pos_L_End)].ToString() ?? string.Empty;
            #endregion

            #region - Level -
            txtLevel_D1.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D01_Level)].ToString() ?? string.Empty;
            txtLevel_D2.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D02_Level)].ToString() ?? string.Empty;
            txtLevel_D3.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D03_Level)].ToString() ?? string.Empty;
            txtLevel_D4.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D04_Level)].ToString() ?? string.Empty;
            txtLevel_D5.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D05_Level)].ToString() ?? string.Empty;
            txtLevel_D6.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D06_Level)].ToString() ?? string.Empty;
            txtLevel_D7.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D07_Level)].ToString() ?? string.Empty;
            txtLevel_D8.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D08_Level)].ToString() ?? string.Empty;
            txtLevel_D9.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D09_Level)].ToString() ?? string.Empty;
            txtLevel_D10.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D10_Level)].ToString() ?? string.Empty;
            #endregion

            #region - Percent -
            txtPercent_D1.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D01_Percent)].ToString() ?? string.Empty;
            txtPercent_D2.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D02_Percent)].ToString() ?? string.Empty;
            txtPercent_D3.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D03_Percent)].ToString() ?? string.Empty;
            txtPercent_D4.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D04_Percent)].ToString() ?? string.Empty;
            txtPercent_D5.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D05_Percent)].ToString() ?? string.Empty;
            txtPercent_D6.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D06_Percent)].ToString() ?? string.Empty;
            txtPercent_D7.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D07_Percent)].ToString() ?? string.Empty;
            txtPercent_D8.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D08_Percent)].ToString() ?? string.Empty;
            txtPercent_D9.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D09_Percent)].ToString() ?? string.Empty;
            txtPercent_D10.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D10_Percent)].ToString() ?? string.Empty;
            #endregion

            #region - QGrade - 
            txtQGRADE_D1.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D01_QGrade)].ToString() ?? string.Empty;
            txtQGRADE_D2.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D02_QGrade)].ToString() ?? string.Empty;
            txtQGRADE_D3.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D03_QGrade)].ToString() ?? string.Empty;
            txtQGRADE_D4.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D04_QGrade)].ToString() ?? string.Empty;
            txtQGRADE_D5.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D05_QGrade)].ToString() ?? string.Empty;
            txtQGRADE_D6.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D06_QGrade)].ToString() ?? string.Empty;
            txtQGRADE_D7.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D07_QGrade)].ToString() ?? string.Empty;
            txtQGRADE_D8.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D08_QGrade)].ToString() ?? string.Empty;
            txtQGRADE_D9.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D09_QGrade)].ToString() ?? string.Empty;
            txtQGRADE_D10.Text = dt_PDO.Rows[0][nameof(PDOModel.L2L3_PDO.D10_QGrade)].ToString() ?? string.Empty;
            #endregion

        }
        #endregion

        #region Button
        /// <summary>
        /// 上傳PDO -> MMS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Confirm_Click(object sender, EventArgs e)
        {
            DataTable dtUploadFlag = new DataTable();
            strSql = SqlFactory.Frm_3_2_UpdatePDO_Check_DB_PDO(txtOut_mat_No.Text);
            dtUploadFlag = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "上传记录");

            if (dtUploadFlag == null) return;

            if (dtUploadFlag.Rows.Count == 0 || dtUploadFlag.Rows[0][0].ToString() != "1")
            {
                DialogResult dialogR = MessageBox.Show("PDO只能上传一次，请确定是否要传送?", "提示", MessageBoxButtons.OKCancel);
                if (dialogR == DialogResult.OK)
                {
                    SCCommMsg.CS06_SendMMSPDO _SendMMSPDO = new SCCommMsg.CS06_SendMMSPDO
                    {
                        Source = "CPL1_HMI",
                        ID = "SendMMSPDO",
                        Coil_ID = txtOut_mat_No.Text
                    };
                    PublicComm.client.Tell(_SendMMSPDO);
                    EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text}PDO资料确认上传 ","钢卷编号:{txtOut_mat_No.Text}PDO资料确认上传 钢卷编号:{txtOut_mat_No.Text}");
                    EventLogHandler.Instance.EventPush_Message(string.Empty, $"[{DateTime.Now:HH:mm:ss}]信息名称:入料作业 信息:已通知Server钢卷号[{txtOut_mat_No.Text}]PDO上传MMS");
                    PublicComm.ClientLog.Info($"訊息名稱:插入過渡卷 訊息:已通知Server鋼卷號[{txtOut_mat_No.Text}]PDO上傳MMS");
                    PublicComm.akkaLog.Info($"訊息名稱:插入過渡卷 訊息:已通知Server鋼卷號[{txtOut_mat_No.Text}]PDO上傳MMS");
                    Close();
                }
            }
            else
            {
                EventLogHandler.Instance.EventPush_Message(string.Empty, $"[{DateTime.Now:HH:mm:ss}]信息名称:入料作业 信息:已通知Server钢卷号[{txtOut_mat_No.Text}]PDO已上传过MMS");
                PublicComm.ClientLog.Info($"訊息名稱:插入過渡卷 訊息:已通知Server鋼卷號[{txtOut_mat_No.Text}]PDO已上傳過MMS");
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {

            strSql = SqlFactory.Frm_PDOConfirm_PDOSave_DB_PDO();
            Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"修改[{txtOut_mat_No.Text}]PDO");
            btn_Confirm.Visible = true;
        }
        #endregion

        private void TabPDO_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(tabPDO, e);
        }

        private void Btn_PDO_Select_Click(object sender, EventArgs e)
        {
            PDOselect_Table(cbo_Out_mat_NO.Text);
        }
    }
}

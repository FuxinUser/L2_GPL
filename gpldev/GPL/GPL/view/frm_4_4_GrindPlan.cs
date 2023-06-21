using DBService.Repository.BeltPatterns;
using DBService.Repository.GradeGroups;
using DBService.Repository.GrindPlan;
using GPLManager.Util;
using System;
using System.Data;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;
using static GPLManager.DataBaseTableFactory;

namespace GPLManager
{
    public partial class Frm_4_4_GrindPlan : Form
    {
        #region 變數
        bool InsertUpdate ;
        DataTable dtBeltPattern;
        DataTable dtBeltPattern_CBO;
        DataTable dt_BeltPatternSelectionChange ;
        DataTable dtThicknessRelction;
        DataTable dt_ThicknessRelctionSelectionChange;
        DataTable dtGradeGroup;
        DataTable dtMaxPass;
        #endregion

        //語系
        private LanguageHandler LanguageHand;

        public Frm_4_4_GrindPlan()
        {
            InitializeComponent();
        }

        private void Frm_4_4_GrindPlan_Load(object sender, EventArgs e)
        {
            PublicForms.GrindPlan = this;
            Control[] Frm_4_4_Control = new Control[] {
            Btn_BeltPattern_Insert, //BeltPattern 新增
            Btn_BeltPattern_Update, //BeltPattern 修改

            Btn_GradeGroup_Insert, //GradeGroup 新增
            Btn_GradeGroup_Update, //GradeGroup 修改

            Btn_ThicknessRelction_Insert, //ThicknessRelction 新增
            Btn_ThicknessRelction_Update //ThicknessRelction 修改
             };

            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_4_4_Control, UserSetupHandler.Instance.Frm_4_4);
           
            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        private void Frm_4_4_GrindPlan_Shown(object sender, EventArgs e)
        {
            Fun_SelectGradeGroup();

            Fun_SelectThicknessRelction();

            Fun_SelectBeltPattern();

            Fun_DataGridViewClearSecletion(Dgv_GradeGroup);
            Fun_DataGridViewClearSecletion(Dgv_BeltPattern);
            Fun_DataGridViewClearSecletion(Dgv_ThicknessRelction);

            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltMaterials(Cob_GR_1_BeltCode);
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltMaterials(Cob_GR_2_BeltCode);
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltMaterials(Cob_GR_3_BeltCode);
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltMaterials(Cob_GR_4_BeltCode);
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltMaterials(Cob_GR_5_BeltCode);
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltMaterials(Cob_GR_6_BeltCode);

            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Direction,Cob_GR_1_Belt_RotateDir);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Direction,Cob_GR_2_Belt_RotateDir);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Direction,Cob_GR_3_Belt_RotateDir);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Direction,Cob_GR_4_Belt_RotateDir);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Direction,Cob_GR_5_Belt_RotateDir);
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Direction,Cob_GR_6_Belt_RotateDir);

        }


        #region [客户品质等级群组]

        /// <summary>
        /// Thickness Relction DGV
        /// </summary>
        private void Fun_SelectGradeGroup()
        {
            string strSql = SqlFactory.Frm_4_4_SelectGradeGroup_DB_GradeGroups();
            dtGradeGroup = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"客户品质等级群组","4-4");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_GradeGroup, dtGradeGroup);
            DGVColumnsHandler.Instance.Frm_4_4_GradeGroup(Dgv_GradeGroup);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_GradeGroup);


            Txt_Customer.Text = string.Empty;
            Txt_Group.Text = string.Empty;
            Txt_SteelGrade.Text = string.Empty;
        }

      
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_GradeGroupSearch_Click(object sender, EventArgs e)
        {
            string strSql = SqlFactory.Frm_4_4_SelectGradeGroup_DB_GradeGroups();
            dtGradeGroup = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "查询客户品质等级群组", "4-4");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_GradeGroup, dtGradeGroup);
            DGVColumnsHandler.Instance.Frm_4_4_GradeGroup(Dgv_GradeGroup);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_GradeGroup);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_GradeGroupInsert_Click(object sender, EventArgs e)
        {
            //修改
            Fun_SetBottonEnabled(Btn_GradeGroup_Update, false);

            Fun_GradeGroupControl_Visible_Enabled(true);
            Fun_GradeGroupControl_ReadOnly(false);

            Txt_Customer.Text = string.Empty;
            Txt_Group.Text = string.Empty;
            Txt_SteelGrade.Text = string.Empty;

            InsertUpdate = true;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_GradeGroupUpdate_Click(object sender, EventArgs e)
        {
            //新增
            Fun_SetBottonEnabled(Btn_GradeGroup_Insert, false);

            Fun_GradeGroupControl_Visible_Enabled(true);
            Fun_GradeGroupControl_ReadOnly(false);

            InsertUpdate = false;
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_GradeGroupSave_Click(object sender, EventArgs e)
        {
            //修改
            Fun_SetBottonEnabled(Btn_GradeGroup_Update, true);
            //新增
            Fun_SetBottonEnabled(Btn_GradeGroup_Insert, true);

            Fun_GradeGroupControl_Visible_Enabled(false);
            Fun_GradeGroupControl_ReadOnly(true);
            string strSql = InsertUpdate ? SqlFactory.Frm_4_4_InsertGradeGroup_DB_GradeGroups() : SqlFactory.Frm_4_4_UpdateGradeGroup_DB_GradeGroups();
            Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL,"客户品质等级群组","4-4");

            Fun_SelectGradeGroup();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_GradeGroupCancel_Click(object sender, EventArgs e)
        {
            //修改
            Fun_SetBottonEnabled(Btn_GradeGroup_Update, true);
            //新增
            Fun_SetBottonEnabled(Btn_GradeGroup_Insert, true);

            Fun_GradeGroupControl_Visible_Enabled(false);
            Fun_GradeGroupControl_ReadOnly(true);
        }

        private void Fun_GradeGroupControl_Visible_Enabled(bool bol)
        {
            Btn_GradeGroup_Save.Visible = bol;
            Btn_GradeGroup_Cancel.Visible = bol;
        }

        private void Fun_GradeGroupControl_ReadOnly(bool bol)
        {
            Txt_SteelGrade.ReadOnly = bol;
            Txt_Customer.ReadOnly = bol;
            Txt_Group.ReadOnly = bol;
        }

        private void Dgv_GradeGroup_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dtGradeGroup.IsNull()) return;
            if (Dgv_GradeGroup.CurrentIsNull()) return;
            if (Dgv_GradeGroup.DgvIsNull()) return;

            DataRow dr = dtGradeGroup.Rows[Dgv_GradeGroup.CurrentRow.Index];

            Txt_SteelGrade.Text = dr[nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)].ToString() ?? string.Empty;
            Txt_Customer.Text = dr[nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)].ToString() ?? string.Empty;
            Txt_Group.Text = dr[nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)].ToString() ?? string.Empty;
        }

        /// <summary>
        /// 钢种ComboBox Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_SteelGrade_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsST(Cob_SteelGrade);
        }

        /// <summary>
        /// 客户代码ComboBox Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Customer_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsCustomer(Cob_Customer);
        }

        /// <summary>
        /// 客户品质等级群组ComboBox Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Group_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsGradeGroups(Cob_Group);
        }
       
        #endregion


        #region [Thickness Relction]

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ThicknessRelction_Insert_Click(object sender, EventArgs e)
        {
            Fun_ThicknessRelctionControl_Visible_Enabled(true);
            Fun_ThicknessRelctionControl_ReadOnly(false);

            //修改
            Fun_SetBottonEnabled(Btn_ThicknessRelction_Update, false);


            Cob_GradeGroup.Text = string.Empty;
            Txt_Thickness_From.Text = string.Empty;
            Txt_Thickness_To.Text = string.Empty;
            Cob_H_BeltPattern.SelectedText = string.Empty;
            Cob_M_BeltPattern.SelectedText = string.Empty;
            Cob_T_BeltPattern.SelectedText = string.Empty;
            Txt_H_LineSpeed.Text = string.Empty;
            Txt_M_LineSpeed.Text = string.Empty;
            Txt_T_LineSpeed.Text = string.Empty;

            InsertUpdate = true;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ThicknessRelction_Update_Click(object sender, EventArgs e)
        {
            Fun_ThicknessRelctionControl_Visible_Enabled(true);
            Fun_ThicknessRelctionControl_ReadOnly(false);
            //新增
            Fun_SetBottonEnabled(Btn_ThicknessRelction_Insert, false);
            InsertUpdate = false;
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ThicknessRelction_Save_Click(object sender, EventArgs e)
        {
            string strSql = string.Empty;

            if (InsertUpdate)
            {
                strSql = SqlFactory.Frm_4_4_InsertGrindPlan_DB_GrindPlan();
                Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL,"新增研磨计划","4-4");
            }
            else if (InsertUpdate == false)
            {
                for (int Postion = 0; Postion < 3; Postion++)
                {
                    strSql = SqlFactory.Frm_4_4_UpdateGrindPlan_DB_GrindPlan(Postion, dt_ThicknessRelctionSelectionChange);
                    Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "修改研磨计划", "4-4");
                }
            }
            Fun_ThicknessRelctionControl_Visible_Enabled(false);
            Fun_ThicknessRelctionControl_ReadOnly(true);

            //新增
            Fun_SetBottonEnabled(Btn_ThicknessRelction_Insert, true);
            //修改
            Fun_SetBottonEnabled(Btn_ThicknessRelction_Update, true);

            Fun_SelectThicknessRelction();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ThicknessRelction_Cancel_Click(object sender, EventArgs e)
        {
            Fun_ThicknessRelctionControl_Visible_Enabled(false);
            Fun_ThicknessRelctionControl_ReadOnly(true);
            //新增
            Fun_SetBottonEnabled(Btn_ThicknessRelction_Insert, true);
            //修改
            Fun_SetBottonEnabled(Btn_ThicknessRelction_Update, true);

        }

        private void Fun_ThicknessRelctionControl_Visible_Enabled(bool bol)
        {
            Btn_ThicknessRelction_Save.Visible = bol;
            Btn_ThicknessRelction_Cancel.Visible = bol;
            Cob_GradeGroup.Enabled = bol;
            Cob_H_BeltPattern.Enabled = bol;
            Cob_M_BeltPattern.Enabled = bol;
            Cob_T_BeltPattern.Enabled = bol;
        }

        private void Fun_ThicknessRelctionControl_ReadOnly(bool bol)
        {
            Txt_Thickness_From.ReadOnly = bol;
            Txt_Thickness_To.ReadOnly = bol;
            Txt_H_LineSpeed.ReadOnly = bol;
            Txt_M_LineSpeed.ReadOnly = bol;
            Txt_T_LineSpeed.ReadOnly = bol;
        }

       
        private void Btn_ThicknessRelctionSearch_Click(object sender, EventArgs e)
        {
            string strSql = SqlFactory.Frm_4_4_SelectGrindPlan_DB_GrindPlan();
            dtThicknessRelction = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"查询研磨计划","4-4");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_ThicknessRelction, dtThicknessRelction);
            DGVColumnsHandler.Instance.Frm_4_4_GrindPlan(Dgv_ThicknessRelction);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_ThicknessRelction);
        }

        /// <summary>
        /// Thickness Relction DGV
        /// </summary>
        private void Fun_SelectThicknessRelction()
        {
            string strSql = SqlFactory.Frm_4_4_SelectGrindPlan_DB_GrindPlan();
            dtThicknessRelction = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "查询研磨计划", "4-4");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_ThicknessRelction, dtThicknessRelction);
            DGVColumnsHandler.Instance.Frm_4_4_GrindPlan(Dgv_ThicknessRelction);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_ThicknessRelction);

        }

        private void Dgv_ThicknessRelction_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cob_GradeGroup.Text = string.Empty;
            Cob_H_BeltPattern.Text = string.Empty;
            Cob_M_BeltPattern.Text = string.Empty;
            Cob_T_BeltPattern.Text = string.Empty;

            if (dtThicknessRelction.IsNull()) return;
            if (Dgv_ThicknessRelction.DgvIsNull()) return;

            DataRow dr = dtThicknessRelction.Rows[Dgv_ThicknessRelction.CurrentRow.Index];

            string GradeGroup = dr[nameof(GrindPlanEntity.TBL_GrindPlan.GradeGroup)].ToString() ?? string.Empty;

            string Thickness_From = dr[nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)].ToString() ?? string.Empty;

            string Thickness_To = dr[nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)].ToString() ?? string.Empty;

            string strSql = SqlFactory.Frm_4_4_GrindPlanSelectionChange_DB_GrindPlan(GradeGroup, Thickness_From, Thickness_To);
            dt_ThicknessRelctionSelectionChange = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"研磨计划详细资料","4-4");

            if (dt_ThicknessRelctionSelectionChange.IsNull()) return;

            #region 填資料
            Cob_GradeGroup.SelectedText = GradeGroup;
            Txt_Thickness_From.Text = dt_ThicknessRelctionSelectionChange.Rows[0][nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_From)].ToString() ?? string.Empty;
            Txt_Thickness_To.Text = dt_ThicknessRelctionSelectionChange.Rows[0][nameof(GrindPlanEntity.TBL_GrindPlan.Thickness_To)].ToString() ?? string.Empty;
            Cob_H_BeltPattern.SelectedText = dt_ThicknessRelctionSelectionChange.Rows[0][nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)].ToString() ?? string.Empty;
            Cob_M_BeltPattern.SelectedText = dt_ThicknessRelctionSelectionChange.Rows[1][nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)].ToString() ?? string.Empty;
            Cob_T_BeltPattern.SelectedText = dt_ThicknessRelctionSelectionChange.Rows[2][nameof(GrindPlanEntity.TBL_GrindPlan.BeltPattern)].ToString() ?? string.Empty;
            Txt_H_LineSpeed.Text = dt_ThicknessRelctionSelectionChange.Rows[0][nameof(GrindPlanEntity.TBL_GrindPlan.LineSpeed)].ToString() ?? string.Empty;
            Txt_M_LineSpeed.Text = dt_ThicknessRelctionSelectionChange.Rows[1][nameof(GrindPlanEntity.TBL_GrindPlan.LineSpeed)].ToString() ?? string.Empty;
            Txt_T_LineSpeed.Text = dt_ThicknessRelctionSelectionChange.Rows[2][nameof(GrindPlanEntity.TBL_GrindPlan.LineSpeed)].ToString() ?? string.Empty;
            #endregion

        }
       
        /// <summary>
        /// 頭段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_H_BeltPattern_TextChanged(object sender, EventArgs e)
        {
            if (Cob_H_BeltPattern.Text.IsEmpty())
            {
                Txt_H_PassNumber.Text = string.Empty;
                Txt_H_LineSpeed.Text = string.Empty;
                return;
            }

            Fun_Select_BeltPattern_MaxPass(Cob_H_BeltPattern.Text);

            if (dtMaxPass.IsNull()) return;

            Txt_H_PassNumber.Text = dtMaxPass.Rows[0][nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)].ToString() ?? string.Empty;
        }

        /// <summary>
        /// 中段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_M_BeltPattern_TextChanged(object sender, EventArgs e)
        {
            if (Cob_M_BeltPattern.Text.IsEmpty())
            {
                Txt_M_PassNumber.Text = string.Empty;
                Txt_M_LineSpeed.Text = string.Empty;
                return;
            }

            Fun_Select_BeltPattern_MaxPass(Cob_M_BeltPattern.Text);

            if (dtMaxPass.IsNull()) return;

            Txt_M_PassNumber.Text = dtMaxPass.Rows[0][nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)].ToString() ?? string.Empty;
        }

        /// <summary>
        /// 尾段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_T_BeltPattern_TextChanged(object sender, EventArgs e)
        {
            if (Cob_T_BeltPattern.Text.IsEmpty())
            {
                Txt_T_PassNumber.Text = string.Empty;
                Txt_T_LineSpeed.Text = string.Empty;
                return;
            }

            Fun_Select_BeltPattern_MaxPass(Cob_T_BeltPattern.Text);

            if (dtMaxPass.IsNull()) return;

            Txt_T_PassNumber.Text = dtMaxPass.Rows[0][nameof(TBL_BeltPatterns.Pass_To)].ToString();
        }

        private void Fun_Select_BeltPattern_MaxPass(string BeltPattern)
        {
            string strSql = SqlFactory.Frm_4_4_SelectMaxPass_DB_BeltPatterns(BeltPattern);
            dtMaxPass = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"道次","4-4");
        }

        private void Cob_BeltPattern_Click(object sender, EventArgs e)
        {
            Fun_SelectBeltPatternComboBox();
        }

        private void Cob_GradeGroup_S_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsGradeGroups(Cob_GradeGroup_S);
        }

        private void Cob_GradeGroup_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsGradeGroups(Cob_GradeGroup);
        }

        private void Cob_H_BeltPattern_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltPattern(Cob_H_BeltPattern);
        }

        private void Cob_M_BeltPattern_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltPattern(Cob_M_BeltPattern);
        }

        private void Cob_T_BeltPattern_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltPattern(Cob_T_BeltPattern);
        }
        #endregion


        #region  [Belt Pattern] 
        /// <summary>
        /// dgv_BeltPattern-查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_BeltPattern_Search_Click(object sender, EventArgs e)
        {
            string strSql = SqlFactory.Frm_4_4_SelectBeltPatterns_DB_BeltPatterns();
            dtBeltPattern = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "BeltPattern查询作业","4-4");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_BeltPattern, dtBeltPattern);
            DGVColumnsHandler.Instance.Frm_4_4_BeltPatterns(Dgv_BeltPattern);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_BeltPattern);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_BeltPattern_Insert_Click(object sender, EventArgs e)
        {
            Fun_BeltPatternControl_Visible_Enabled(true);
            Fun_BeltPatternControl_ReadOnly(false);
            ReadOnlyHandler.Instance.Fun_PanelClear(Pnl_BeltPattern_Bottom);
            ReadOnlyHandler.Instance.Fun_GroupBoxClear(Grb_GR_1);
            ReadOnlyHandler.Instance.Fun_GroupBoxClear(Grb_GR_2);
            ReadOnlyHandler.Instance.Fun_GroupBoxClear(Grb_GR_3);
            ReadOnlyHandler.Instance.Fun_GroupBoxClear(Grb_GR_4);
            ReadOnlyHandler.Instance.Fun_GroupBoxClear(Grb_GR_5);
            ReadOnlyHandler.Instance.Fun_GroupBoxClear(Grb_GR_6);

            //修改
            Fun_SetBottonEnabled(Btn_BeltPattern_Update, false);


            InsertUpdate = true;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_BeltPattern_Update_Click(object sender, EventArgs e)
        {
            Fun_BeltPatternControl_Visible_Enabled(true);
            Fun_BeltPatternControl_ReadOnly(false);

            //新增
            Fun_SetBottonEnabled(Btn_BeltPattern_Insert, false);

            InsertUpdate = false;
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_BeltPattern_Save_Click(object sender, EventArgs e)
        {
            if (InsertUpdate)
            {
                string strSql = SqlFactory.Frm_4_4_InsertBeltPatterns_DB_BeltPatterns();
                Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "储存BeltPattern", "4-4");
            }
            else if (InsertUpdate == false)
            {
                for (int GR_NO = 1; GR_NO < 7; GR_NO++)
                {
                    string strSql = SqlFactory.Frm_4_4_UpdateBeltPatterns_DB_BeltPatterns(GR_NO, dt_BeltPatternSelectionChange);
                    Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "修改BeltPattern","4-4");
                }
            }

            Fun_BeltPatternControl_Visible_Enabled(false);

            Fun_BeltPatternControl_ReadOnly(true);

            //新增
            Fun_SetBottonEnabled(Btn_BeltPattern_Insert, true);
            //修改
            Fun_SetBottonEnabled(Btn_BeltPattern_Update, true);

            Fun_SelectBeltPattern();

            
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_BeltPattern_Cancel_Click(object sender, EventArgs e)
        {
            Fun_BeltPatternControl_Visible_Enabled(false);

            Fun_BeltPatternControl_ReadOnly(true);
            //新增
            Fun_SetBottonEnabled(Btn_BeltPattern_Insert, true);
            //修改
            Fun_SetBottonEnabled(Btn_BeltPattern_Update, true);

            Fun_BeltPatternDetailDisplay();
        }

        private void Fun_BeltPatternControl_Visible_Enabled(bool bol)
        {
            Btn_BeltPattern_Save.Visible = bol;
            Btn_BeltPattern_Cancel.Visible = bol;

            Cob_GR_1_BeltCode.Enabled = bol;
            Cob_GR_2_BeltCode.Enabled = bol;
            Cob_GR_3_BeltCode.Enabled = bol;
            Cob_GR_4_BeltCode.Enabled = bol;
            Cob_GR_5_BeltCode.Enabled = bol;
            Cob_GR_6_BeltCode.Enabled = bol;
            Cob_GR_1_Belt_RotateDir.Enabled = bol;
            Cob_GR_2_Belt_RotateDir.Enabled = bol;
            Cob_GR_3_Belt_RotateDir.Enabled = bol;
            Cob_GR_4_Belt_RotateDir.Enabled = bol;
            Cob_GR_5_Belt_RotateDir.Enabled = bol;
            Cob_GR_6_Belt_RotateDir.Enabled = bol;
        }

        private void Fun_BeltPatternControl_ReadOnly(bool bol)
        {
            Txt_SelectionPassFrom.ReadOnly = bol;
            Txt_SelectionPassTo.ReadOnly = bol;
            Txt_SelectionBeltPattern.ReadOnly = bol;

            Txt_GR_1_Current.ReadOnly = bol;
            Txt_GR_2_Current.ReadOnly = bol;
            Txt_GR_3_Current.ReadOnly = bol;
            Txt_GR_4_Current.ReadOnly = bol;
            Txt_GR_5_Current.ReadOnly = bol;
            Txt_GR_6_Current.ReadOnly = bol;

            Txt_GR_1_BeltNumber.ReadOnly = bol;
            Txt_GR_2_BeltNumber.ReadOnly = bol;
            Txt_GR_3_BeltNumber.ReadOnly = bol;
            Txt_GR_4_BeltNumber.ReadOnly = bol;
            Txt_GR_5_BeltNumber.ReadOnly = bol;
            Txt_GR_6_BeltNumber.ReadOnly = bol;

            Txt_GR_1_BeltSpeed.ReadOnly = bol;
            Txt_GR_2_BeltSpeed.ReadOnly = bol;
        }

      
        /// <summary>
        /// Belt Pattern DGV
        /// </summary>
        private void Fun_SelectBeltPattern()
        {
            string strSql = SqlFactory.Frm_4_4_SelectBeltPatterns_DB_BeltPatterns();
            dtBeltPattern = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "BeltPattern", "4-4");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_BeltPattern, dtBeltPattern);
            DGVColumnsHandler.Instance.Frm_4_4_BeltPatterns(Dgv_BeltPattern);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_BeltPattern);
        }

        /// <summary>
        /// Belt Pattern ComboBox
        /// </summary>
        private void Fun_SelectBeltPatternComboBox()
        {
            string strSql = SqlFactory.Frm_4_4_SelectBeltPatternsComboBox_DB_BeltPatterns();
            dtBeltPattern_CBO = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "Belt Pattern ComboBox", "4-4");

            if (dtBeltPattern_CBO.Rows.Count.Equals(0)) return;

            Cob_BeltPattern_S.DisplayMember = nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern);
            Cob_BeltPattern_S.ValueMember = nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern);
            Cob_BeltPattern_S.DataSource = dtBeltPattern_CBO;
            Cob_BeltPattern_S.SelectedValue = string.Empty;
        }

        private void Dgv_BeltPattern_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dtBeltPattern.IsNull()) return;
            if (Dgv_BeltPattern.CurrentIsNull()) return;
            if (Dgv_BeltPattern.DgvIsNull()) return;

            Fun_BeltPatternDetailDisplay();
        }

        private void Fun_BeltPatternDetailDisplay()
        {
            DataRow dr = dtBeltPattern.Rows[Dgv_BeltPattern.CurrentRow.Index];
            string BeltPattern = dr[nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern)].ToString() ?? string.Empty;
            string PassFrom = dr[nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From)].ToString() ?? string.Empty;
            string PassTo = dr[nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To)].ToString() ?? string.Empty;

            string strSql = SqlFactory.Frm_4_4_BeltPatternsSelectionChange_DB_BeltPatterns(BeltPattern, PassFrom, PassTo);
            dt_BeltPatternSelectionChange = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "研磨机资讯", "4-4");

            if (dt_BeltPatternSelectionChange.IsNull()) return;

            #region 填資料
            Txt_SelectionBeltPattern.Text = BeltPattern;
            Txt_SelectionPassFrom.Text = PassFrom;
            Txt_SelectionPassTo.Text = PassTo;

            Txt_GR_1_Current.Text = dt_BeltPatternSelectionChange.Rows[0][nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)].ToString() ?? string.Empty;
            Txt_GR_2_Current.Text = dt_BeltPatternSelectionChange.Rows[1][nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)].ToString() ?? string.Empty;
            Txt_GR_3_Current.Text = dt_BeltPatternSelectionChange.Rows[2][nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)].ToString() ?? string.Empty;
            Txt_GR_4_Current.Text = dt_BeltPatternSelectionChange.Rows[3][nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)].ToString() ?? string.Empty;
            Txt_GR_5_Current.Text = dt_BeltPatternSelectionChange.Rows[4][nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)].ToString() ?? string.Empty;
            Txt_GR_6_Current.Text = dt_BeltPatternSelectionChange.Rows[5][nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_Current)].ToString() ?? string.Empty;

            Cob_GR_1_BeltCode.SelectedValue = dt_BeltPatternSelectionChange.Rows[0][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)].ToString().Trim() ?? string.Empty;
            Cob_GR_2_BeltCode.SelectedValue = dt_BeltPatternSelectionChange.Rows[1][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)].ToString().Trim() ?? string.Empty;
            Cob_GR_3_BeltCode.SelectedValue = dt_BeltPatternSelectionChange.Rows[2][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)].ToString().Trim() ?? string.Empty;
            Cob_GR_4_BeltCode.SelectedValue = dt_BeltPatternSelectionChange.Rows[3][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)].ToString().Trim() ?? string.Empty;
            Cob_GR_5_BeltCode.SelectedValue = dt_BeltPatternSelectionChange.Rows[4][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)].ToString().Trim() ?? string.Empty;
            Cob_GR_6_BeltCode.SelectedValue = dt_BeltPatternSelectionChange.Rows[5][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_MaterialCode)].ToString().Trim() ?? string.Empty;

            Txt_GR_1_BeltNumber.Text = dt_BeltPatternSelectionChange.Rows[0][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)].ToString() ?? string.Empty;
            Txt_GR_2_BeltNumber.Text = dt_BeltPatternSelectionChange.Rows[1][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)].ToString() ?? string.Empty;
            Txt_GR_3_BeltNumber.Text = dt_BeltPatternSelectionChange.Rows[2][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)].ToString() ?? string.Empty;
            Txt_GR_4_BeltNumber.Text = dt_BeltPatternSelectionChange.Rows[3][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)].ToString() ?? string.Empty;
            Txt_GR_5_BeltNumber.Text = dt_BeltPatternSelectionChange.Rows[4][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)].ToString() ?? string.Empty;
            Txt_GR_6_BeltNumber.Text = dt_BeltPatternSelectionChange.Rows[5][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_ParticalNumber)].ToString() ?? string.Empty;

            Cob_GR_1_Belt_RotateDir.SelectedValue = dt_BeltPatternSelectionChange.Rows[0][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)].ToString() ?? string.Empty;
            Cob_GR_2_Belt_RotateDir.SelectedValue = dt_BeltPatternSelectionChange.Rows[1][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)].ToString() ?? string.Empty;
            Cob_GR_3_Belt_RotateDir.SelectedValue = dt_BeltPatternSelectionChange.Rows[2][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)].ToString() ?? string.Empty;
            Cob_GR_4_Belt_RotateDir.SelectedValue = dt_BeltPatternSelectionChange.Rows[3][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)].ToString() ?? string.Empty;
            Cob_GR_5_Belt_RotateDir.SelectedValue = dt_BeltPatternSelectionChange.Rows[4][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)].ToString() ?? string.Empty;
            Cob_GR_6_Belt_RotateDir.SelectedValue = dt_BeltPatternSelectionChange.Rows[5][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_RotateDir)].ToString() ?? string.Empty;

            Txt_GR_1_BeltSpeed.Text = dt_BeltPatternSelectionChange.Rows[0][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)].ToString() ?? string.Empty;
            Txt_GR_2_BeltSpeed.Text = dt_BeltPatternSelectionChange.Rows[1][nameof(BeltPatternsEntity.TBL_BeltPatterns.Belt_Speed)].ToString() ?? string.Empty;
            #endregion
        }


        private void Cob_GR_1_BeltCode_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltMaterials(Cob_GR_1_BeltCode);
        }

        private void Cob_GR_2_BeltCode_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltMaterials(Cob_GR_2_BeltCode);
        }

        private void Cob_GR_3_BeltCode_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltMaterials(Cob_GR_3_BeltCode);
        }

        private void Cob_GR_4_BeltCode_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltMaterials(Cob_GR_4_BeltCode);
        }

        private void Cob_GR_5_BeltCode_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltMaterials(Cob_GR_5_BeltCode);
        }

        private void Cob_GR_6_BeltCode_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsBeltMaterials(Cob_GR_6_BeltCode);
        }
        #endregion



        #region TabContro
        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_GrindPlanControl, e);
        }


        #endregion


        //設定按鈕啟用狀態並改顏色
        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

           
            //Color colorBack;
            btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
        }

        /// <summary>
        /// 清掉DataGridView預設的Selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_BeltPattern_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Fun_DataGridViewClearSecletion(Dgv_BeltPattern);
        }
        private void Dgv_ThicknessRelction_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Fun_DataGridViewClearSecletion(Dgv_ThicknessRelction);
        }
        private void Dgv_GradeGroup_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Fun_DataGridViewClearSecletion(Dgv_GradeGroup);
        }

        /// <summary>
        /// 取消預設Selection
        /// </summary>
        /// <param name="dgv"></param>
        private void Fun_DataGridViewClearSecletion(DataGridView dgv)
        {
            dgv.ClearSelection();
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

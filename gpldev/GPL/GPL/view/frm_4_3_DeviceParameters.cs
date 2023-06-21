using DBService.Repository.LookupTbl;
using DBService.Repository.LookupTblFlattener;
using DBService.Repository.SteelNoToMaterialGrade;
using GPLManager.Util;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static GPLManager.DataBaseTableFactory;

namespace GPLManager
{
    public partial class Frm_4_3_DeviceParameters : Form
    {
        public bool bolSearch = false;
        public DataTable dtGetFlattener, dtGetTensionHotRolled, dtGetTensionColdRolled;
        public DataRow drGetFlattenerRow, drGetHotRolledRow, drGetColdRolledRow;

        //語系
        private LanguageHandler LanguageHand;
        public Frm_4_3_DeviceParameters()
        {
            InitializeComponent();
        }
        private void Frm_4_3_DeviceParameters_Load(object sender, EventArgs e)
        {
            PublicForms.DeviceParameters = this;
            Control[] Frm_4_3_Control = new Control[] {
            Btn_FlattenerNew,
            Btn_FlattenerEdit,
            Btn_FlattenerDel,
            Btn_TensionNew,
            Btn_TensionEdit,
            Btn_TensionDel,
            };
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_4_3_Control, UserSetupHandler.Instance.Frm_4_3);

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        private void Frm_4_3_DeviceParameters_Shown(object sender, EventArgs e)
        {
            Fun_TabControlShown();
            Fun_MatericalGradeComboBoxItems();
        }
       
        /// <summary>
        /// 钢种选项
        /// </summary>
        private void Fun_MatericalGradeComboBoxItems()
        {
            string strSql = SqlFactory.Frm_4_3_SelectSteelGrade();
            DataTable dtGetMaterical = Data_Access.Fun_SelectDate(strSql,GlobalVariableHandler.Instance.strConn_GPL,"钢种选项","4-3");

            if (dtGetMaterical.IsNull()) return;

            Cob_SteelGrade.DisplayMember = nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No);
            Cob_SteelGrade.ValueMember = nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No);
            Cob_SteelGrade.DataSource = dtGetMaterical;
        }


        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Query_Click(object sender, EventArgs e)
        {
            string strSql = string.Empty;
            bolSearch = true;

            if (Tab_LUTControl.SelectedTab == Tab_Flattener_Page)
            {
                strSql = SqlFactory.Frm_4_3_SelectFlattener_DB_TBL_LookupTable_Flattener();
                dtGetFlattener = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "整平机设备参数", "4-3");

                DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Flattener, dtGetFlattener);
                DGVColumnsHandler.Instance.Frm_4_3_Flattener(Dgv_Flattener);
                DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Flattener);
            }
            else if (Tab_LUTControl.SelectedTab == Tab_Tension_Page)
            {
                //冷軋
                if (Tab_TensionControl.SelectedTab == Tab_TensionColdRoll_Page)
                {
                    strSql = SqlFactory.Frm_4_3_SelectTension("C");
                    dtGetTensionColdRolled = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"张力机设备参数-冷轧", "4-3");

                    DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_ColdRolled, dtGetTensionColdRolled);
                    DGVColumnsHandler.Instance.Frm_4_3_Tension(Dgv_ColdRolled);
                    DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_ColdRolled);
                }
                //熱軋
                else if (Tab_TensionControl.SelectedTab == Tab_TensionHotRoll_Page)
                {
                    strSql = SqlFactory.Frm_4_3_SelectTension("H");
                    dtGetTensionHotRolled = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"张力机设备参数-热轧", "4-3");

                    DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_HotRolled, dtGetTensionHotRolled);
                    DGVColumnsHandler.Instance.Frm_4_3_Tension(Dgv_HotRolled);
                    DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_HotRolled);
                }
            }

            bolSearch = false;
        }

        /// <summary>
        /// 分页选取事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fun_TabControlShown()
        {
            string strSql = string.Empty;

            //整平机
            if (Tab_LUTControl.SelectedTab == Tab_Flattener_Page)
            {
                if (!dtGetFlattener.IsNull()) return;

                strSql = SqlFactory.Frm_4_3_SelectFlattener_DB_TBL_LookupTable_Flattener();
                dtGetFlattener = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"整平机设备参数","4-3");

                DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Flattener, dtGetFlattener);
                DGVColumnsHandler.Instance.Frm_4_3_Flattener(Dgv_Flattener);
                DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Flattener);
               
            }
            //张力机 有分冷轧/热轧
            else if (Tab_LUTControl.SelectedTab == Tab_Tension_Page)
            {
                //冷軋
                if (Tab_TensionControl.SelectedTab == Tab_TensionColdRoll_Page)
                {
                    if (!dtGetTensionColdRolled.IsNull()) return;

                    strSql = SqlFactory.Frm_4_3_SelectTension("C");
                    dtGetTensionColdRolled = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"张力机设备参数-冷轧", "4-3");

                    DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_ColdRolled, dtGetTensionColdRolled);
                    DGVColumnsHandler.Instance.Frm_4_3_Tension(Dgv_ColdRolled);
                    DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_ColdRolled);
                }
                //熱軋
                else if (Tab_TensionControl.SelectedTab == Tab_TensionHotRoll_Page)
                {
                    if (!dtGetTensionHotRolled.IsNull()) return;

                    strSql = SqlFactory.Frm_4_3_SelectTension("H");
                    dtGetTensionHotRolled = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"张力机设备参数-热轧", "4-3");

                    DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_HotRolled, dtGetTensionHotRolled);
                    DGVColumnsHandler.Instance.Frm_4_3_Tension(Dgv_HotRolled);
                    DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_HotRolled);
                }
            }
        }

        #region - 整平機 -
       
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_FlattenerNew_Click(object sender, EventArgs e)
        {
            Fun_DataGridViewColumnsSetting(Dgv_Flattener, Dgv_FlattenerCurrentRow);
            Dgv_FlattenerCurrentRow.Rows.Add(1);

            Fun_SetBottonEnabled(Btn_FlattenerDel, false);
            Fun_SetBottonEnabled(Btn_FlattenerEdit, false);

            Btn_FlattenerSave.Visible = true;
            Btn_FlattenerCancel.Visible = true;
            Pnl_Flattener.Visible = true;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_FlattenerEdit_Click(object sender, EventArgs e)
        {
            Fun_DataGridViewColumnsSetting(Dgv_Flattener, Dgv_FlattenerCurrentRow);
            DataGridViewRow Row = Dgv_Flattener.CurrentRow;
            Dgv_FlattenerCurrentRow.Rows.Add(Fun_CurrentRow(Row));

            Fun_SetBottonEnabled(Btn_FlattenerDel, false);
            Fun_SetBottonEnabled(Btn_FlattenerNew, false);

            Btn_FlattenerSave.Visible = true;
            Btn_FlattenerCancel.Visible = true;
            Pnl_Flattener.Visible = true;

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_FlattenerDel_Click(object sender, EventArgs e)
        {
            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel("确认删除?", "删除", Properties.Resources.dialogQuestion, 1);

            if (dialogR == DialogResult.Cancel) return;


            DataRow dr = dtGetFlattener.Rows[Dgv_Flattener.CurrentRow.Index];

            string strSql = SqlFactory.Frm_4_3_DeleteFlattener(dr);
            Data_Access.GetInstance().Fun_ExecuteQuery(strSql,GlobalVariableHandler.Instance.strConn_GPL,"删除整平机参数","4-3");

            Fun_TabControlShown();
        }

        /// <summary>
        /// 储存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_FlattenerSave_Click(object sender, EventArgs e)
        {
            string strMessage = string.Empty , strSql = string.Empty;

            if (Btn_FlattenerNew.Enabled)
            {
                strSql = SqlFactory.Frm_4_3_InsertFlattener();
                strMessage = "新增";
            }
            else if (Btn_FlattenerEdit.Enabled)
            {
                strSql = SqlFactory.Frm_4_3_UpdateFlattener();
                strMessage = "修改";
            }

            if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"{strMessage}整平机参数", "4-3")) return;

            //新增
            Fun_SetBottonEnabled(Btn_FlattenerNew, true);
            //刪除
            Fun_SetBottonEnabled(Btn_FlattenerDel, true);
            //修改
            Fun_SetBottonEnabled(Btn_FlattenerEdit, true);

            Btn_FlattenerSave.Visible = false;
            Btn_FlattenerCancel.Visible = false;
            Pnl_Flattener.Visible = false;

            Fun_TabControlShown();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_FlattenerCancel_Click(object sender, EventArgs e)
        {
            //新增
            Fun_SetBottonEnabled(Btn_FlattenerNew, true);
            //刪除
            Fun_SetBottonEnabled(Btn_FlattenerDel, true);
            //修改
            Fun_SetBottonEnabled(Btn_FlattenerEdit, true);
            

            Btn_FlattenerSave.Visible = false;
            Btn_FlattenerCancel.Visible = false;
            Pnl_Flattener.Visible = false;

        }



        #endregion

        #region - 张力机 -

        private void Fun_PanelTensionSetting(bool bolVisable)
        {
            Pnl_Tension.Visible = bolVisable;

            if (bolVisable)
            {
                if (Btn_TensionNew.Visible == false)
                {
                    if (Tab_TensionControl.SelectedTab == Tab_TensionColdRoll_Page)
                    {
                        if (dtGetTensionColdRolled.IsNull()) return;
                        if (Dgv_ColdRolled.CurrentIsNull()) return;

                        drGetColdRolledRow = dtGetTensionColdRolled.Rows[Dgv_ColdRolled.CurrentRow.Index];

                    }
                    else if (Tab_TensionControl.SelectedTab == Tab_TensionHotRoll_Page)
                    {
                        if (dtGetTensionHotRolled.IsNull()) return;
                        if (Dgv_HotRolled.CurrentIsNull()) return;

                        drGetHotRolledRow = dtGetTensionHotRolled.Rows[Dgv_HotRolled.CurrentRow.Index];

                    }
                }
               
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_TensionNew_Click(object sender, EventArgs e)
        {
            if (Tab_TensionControl.SelectedTab == Tab_TensionColdRoll_Page)
            {
                Fun_DataGridViewColumnsSetting(Dgv_ColdRolled, Dgv_TensionCurrentRow);
            }
            else if (Tab_TensionControl.SelectedTab == Tab_TensionHotRoll_Page)
            {
                Fun_DataGridViewColumnsSetting(Dgv_HotRolled, Dgv_TensionCurrentRow);
            }

            Dgv_TensionCurrentRow.Rows.Add(1);

            Fun_SetBottonEnabled(Btn_TensionDel, false);
            Fun_SetBottonEnabled(Btn_TensionEdit, false);

            Btn_TensionSave.Visible = true;
            Btn_TensionCancel.Visible = true;
            Fun_PanelTensionSetting(true);

            Tab_TensionControl.Enabled = false;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_TensionEdit_Click(object sender, EventArgs e)
        {
            DataGridViewRow Row = new DataGridViewRow();

            if (Tab_TensionControl.SelectedTab == Tab_TensionColdRoll_Page)
            {
                Fun_DataGridViewColumnsSetting(Dgv_ColdRolled, Dgv_TensionCurrentRow);
                Row = Dgv_ColdRolled.CurrentRow;
            }
            else if (Tab_TensionControl.SelectedTab == Tab_TensionHotRoll_Page)
            {
                Fun_DataGridViewColumnsSetting(Dgv_HotRolled, Dgv_TensionCurrentRow);
                Row = Dgv_HotRolled.CurrentRow;
            }

            Dgv_TensionCurrentRow.Rows.Add(Fun_CurrentRow(Row));

            Fun_SetBottonEnabled(Btn_TensionNew, false);
            Fun_SetBottonEnabled(Btn_TensionEdit, false);

            Btn_TensionSave.Visible = true;
            Btn_TensionCancel.Visible = true;
            Fun_PanelTensionSetting(true);

            Tab_TensionControl.Enabled = false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_TensionDel_Click(object sender, EventArgs e)
        {
            string strColdHot = Tab_TensionControl.SelectedTab == Tab_TensionColdRoll_Page ? "冷轧" : "热轧";

            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel($"确认删除张力机({strColdHot})参数?", "删除", Properties.Resources.dialogQuestion, 1);
            if (dialogR == DialogResult.Cancel) return;
           
            string strSql = SqlFactory.Frm_4_3_DeleteTension();
            Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"删除张力机({strColdHot})参数", "4-3");

            Fun_TabControlShown();
        }

        /// <summary>
        /// 储存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_TensionSave_Click(object sender, EventArgs e)
        {
            string strMessage = string.Empty, strSql = string.Empty;
            if (Btn_TensionNew.Visible)
            {
                strSql = SqlFactory.Frm_4_3_InsertTension();
                strMessage = "新增";
            }
            else if (Btn_TensionEdit.Visible)
            {
                strSql = SqlFactory.Frm_4_3_UpdateTension();
                strMessage = "修改";
            }

            Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"{strMessage}张力机参数", "4-3");

            //新增
            Fun_SetBottonEnabled(Btn_TensionNew, true);
            //刪除
            Fun_SetBottonEnabled(Btn_TensionDel, true);
            //修改
            Fun_SetBottonEnabled(Btn_TensionEdit, true);

            Btn_TensionSave.Visible = false;
            Btn_TensionCancel.Visible = false;
            Fun_PanelTensionSetting(false);

            Tab_TensionControl.Enabled = true;

            Fun_TabControlShown();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_TensionCancel_Click(object sender, EventArgs e)
        {
            //新增
            Fun_SetBottonEnabled(Btn_TensionNew, true);
            //刪除
            Fun_SetBottonEnabled(Btn_TensionDel, true);
            //修改
            Fun_SetBottonEnabled(Btn_TensionEdit, true);

            Btn_TensionSave.Visible = false;
            Btn_TensionCancel.Visible = false;
            Fun_PanelTensionSetting(false);

            Tab_TensionControl.Enabled = true;
        }

        #endregion

        #region - TabControl -
        private void Tab_LookupTable_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_LUTControl, e);
        }
        private void Tab_Tension_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_TensionControl, e);
        }

        /// <summary>
        /// TabControl Selected事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tab_LookupTable_Selected(object sender, TabControlEventArgs e)
        {
            Fun_TabControlShown();
        }
        private void Tab_Tension_Selected(object sender, TabControlEventArgs e)
        {
            Fun_TabControlShown();
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
        /// dgv_LookupTable = LookupTable
        /// dgv_CurrentRow = 欲編輯資料行
        /// </summary>
        /// <param name="dgv_LookupTable"></param>
        /// <param name="dgv_CurrentRow"></param>
        private void Fun_DataGridViewColumnsSetting(DataGridView dgv_LookupTable, DataGridView dgv_CurrentRow)
        {
            dgv_CurrentRow.Rows.Clear();
            dgv_CurrentRow.Columns.Clear();

            foreach (DataGridViewColumn Column in dgv_LookupTable.Columns)
            {
                dgv_CurrentRow.Columns.Add(Column.Name, Column.HeaderText);
            }

        }

        private DataGridViewRow Fun_CurrentRow(DataGridViewRow Row)
        {
            DataGridViewRow Dgv_Row = (DataGridViewRow)Row.Clone();

            for (int Index = 0; Index < Row.Cells.Count; Index++)
            {
                Dgv_Row.Cells[Index].Value = Row.Cells[Index].Value;
            }
            return Dgv_Row;
        }
    }
}

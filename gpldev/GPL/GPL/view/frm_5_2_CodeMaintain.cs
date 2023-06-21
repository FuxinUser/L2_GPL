using System;
using System.Windows.Forms;
using System.Data;
using DBService.Repository.DelayLocation;
using DBService.Repository.DelayReasonCode;
using System.Drawing;
using DBService.Repository.ScheduleDelete_CoilReject_Code;
using DBService.Repository.SteelNoToMaterialGrade;
using GPLManager.Util;
using DBService.Repository.GradeGroups;
using System.Text;

namespace GPLManager
{
    public partial class Frm_5_2_CodeMaintain : Form
    {

        string strTableName = string.Empty;
        DataTable dtPresetTable;
        DataTable dtSelectOne;
        bool bolEditStatus;
        int Index_No = 0;
        //語系
        private LanguageHandler LanguageHand;
        public enum Action
        {
            Insert,
            Update,
            Delete
        }
        public Frm_5_2_CodeMaintain()
        {
            InitializeComponent();
            if (PublicForms.CodeMaintain == null) PublicForms.CodeMaintain = this;
        }
       
        private void Frm_5_2_CodeMaintain_Load(object sender, EventArgs e)
        {
            Fun_ComboBoxItemsDisplay();
            Control[] Frm_5_2_Control = new Control[] {
                Btn_New,
                Btn_Edit,
                Btn_Delete
            };
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_5_2_Control, UserSetupHandler.Instance.Frm_5_2);
            //新增
            Fun_SetBottonEnabled(Btn_New, false);
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }
       
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            if (bolEditStatus)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑后再查询", "查询代码维护清单",null, 0);
                return;
            }
            if (Cob_Table.SelectedIndex == -1)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选择维护项目", "查询代码维护清单", null, 0);
                return;
            }

            strTableName = Cob_Table.SelectedValue.ToString();

            if (strTableName.IsEmpty())
            {
                DialogHandler.Instance.Fun_DialogShowOk("查无代码维护清单", "查询代码维护清单", null, 0);
                return;
            }
            dtPresetTable = Dt_GetDataTable(strTableName);
            Fun_TableDisplay(dtPresetTable, strTableName, Dgv_Table);
           
        }

        private void Dgv_Table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Dgv_Table.CurrentRow == null) { return; }
            if (e.RowIndex <= -1) { return; }
            if (bolEditStatus) { return; }

            DataRow dr = Fun_GetDataRowFromCurrentRow(Dgv_Table, dtPresetTable);

            DataTable dt = dtPresetTable.Clone();
            try
            {
                dt.LoadDataRow(dr.ItemArray, false);
            }
            catch { return; }

            dtSelectOne = dt.Copy();

            if (UserSetupHandler.Instance.Frm_5_2.Equals("W"))
            {
                //新增
                Fun_SetBottonEnabled(Btn_New, true);
                //修改
                Fun_SetBottonEnabled(Btn_Edit, true);
                //刪除
                Fun_SetBottonEnabled(Btn_Delete, true);
            }
               
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

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_New_Click(object sender, EventArgs e)
        {
            if (bolEditStatus) return;
            bolEditStatus = true;

            Pnl_CurrentRow.Visible = true;
            Dgv_Table.Enabled = false;

            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);
            //儲存
            Btn_Save.Visible = true;
            //取消
            Btn_Cancel.Visible = true;

            Dgv_CurrentRow.DataSource = null;
            DataTable dtNew = dtSelectOne.Clone();
            DataRow dr = dtNew.NewRow();
            dtNew.Rows.Add(dr);
            Fun_TableDisplay(dtNew, strTableName, Dgv_CurrentRow);

            //Fun_DataGridViewColumnsSetting();

            //Dgv_CurrentRow.Rows.Add(1);

            //switch (strTableName)
            //{
            //    case nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition):
            //        DGVColumnsHandler.Instance.Frm_5_2_ScheduleDelete_CoilReject_Code(Dgv_CurrentRow);
            //        break;

            //    case nameof(DelayLocationEntity.TBL_DelayLocation_Definition):
            //        DGVColumnsHandler.Instance.Frm_5_2_DelayLocation(Dgv_CurrentRow);
            //       // Index_No = int.Parse(dt.Rows[dt.Rows.Count - 1][nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)].ToString()) + 1;
            //        break;

            //    case nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition):
            //        DGVColumnsHandler.Instance.Frm_5_2_DelayReasonCode(Dgv_CurrentRow);
            //       // Index_No = int.Parse(dt.Rows[dt.Rows.Count - 1][nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)].ToString()) + 1;
            //        break;

            //    case nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade):
            //        DGVColumnsHandler.Instance.Frm_5_2_MaterialGrade(Dgv_CurrentRow);
            //        break;

            //    default:
            //        break;
            //}
            //DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_CurrentRow);

            //Dgv_CurrentRow.ClearSelection();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            if (bolEditStatus) return;

            if (Dgv_Table.CurrentIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选取代码资料行", "修改代码维护",null, 0);
                return;
            }
            bolEditStatus = true;

            Pnl_CurrentRow.Visible = true;
            Dgv_Table.Enabled = false;

            //新增
            Fun_SetBottonEnabled(Btn_New, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);
            //儲存
            Btn_Save.Visible = true;
            //取消
            Btn_Cancel.Visible = true;

            // Fun_DataGridViewColumnsSetting();

            Dgv_CurrentRow.DataSource = null;

            Fun_TableDisplay(dtSelectOne, strTableName, Dgv_CurrentRow);

            //DataGridViewRow Row = Dgv_Table.CurrentRow;
            //Dgv_CurrentRow.Rows.Add(Fun_CurrentRow(Row));

            Fun_DgvKeyReadOnly(Dgv_CurrentRow, strTableName);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {

            DialogResult actionResult = DialogHandler.Instance.Fun_DialogShowOkCancel("确定要删除？", "删除", Properties.Resources.dialogQuestion, 1);

            if (actionResult == DialogResult.Cancel) return;


            Dgv_Table.Enabled = false;
            //新增
            Fun_SetBottonEnabled(Btn_New, false);
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);


            string str = "刪除";
            string strSql = Fun_TableDataInsertIntoDataBase(Action.Delete);

            try
            {
                Data_Access.GetInstance().ExecuteNonQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL);
                EventLogHandler.Instance.LogInfo("5-2", $"{strTableName.Trim()}代码维护{str}", $"{strTableName.Trim()}{str}成功");
                PublicComm.ClientLog.Debug($"{strTableName.Trim()}{str}成功");
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"{strTableName.Trim()}代码维护{str}失败:{ex}");
                EventLogHandler.Instance.LogDebug("4-1", $"{strTableName.Trim()}代码维护{str}", $"{strTableName.Trim()}代码维护{str}失败:{ex}");
                PublicComm.ClientLog.Debug($"{strTableName.Trim()}代碼維護{str}失敗:{ex}");
                return;
            }

            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);

            Dgv_Table.Enabled = true;
            dtPresetTable = Dt_GetDataTable(strTableName);
            Fun_TableDisplay(dtPresetTable, strTableName, Dgv_Table);
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if (!bolEditStatus) return;

            //检查必填栏位
            bool bolCheckMust = Fun_Check_DgvCurrentRow_MustIsNullShow(strTableName);
            if (!bolCheckMust) return;

            //检查资料是否重复
            bool bolCheckHaveData = Fun_Check_HaveData(strTableName, Dgv_CurrentRow);
            StringBuilder sb = new StringBuilder();
            string strTextData = Fun_GetHaveDataShow(strTableName, Dgv_CurrentRow);
            if (bolCheckHaveData)
            {
                if (Btn_New.Enabled)
                {
                    sb.AppendLine($"新增{strTableName} 参数");
                    sb.Append(strTextData);
                    sb.AppendLine($"资料已重复，请重新确认！");
                    string strShow = sb.ToString();

                    DialogHandler.Instance.Fun_DialogShowOk(strShow, $"代码维护", null, 0);
                    return;
                }
                       


            }
            else
            {
                if (Btn_Edit.Enabled)
                {
                    sb.AppendLine($"修改{strTableName}参数");
                    sb.Append(strTextData);
                    sb.AppendLine($"查无资料，请重新确认！");
                    string strShow = sb.ToString();

                    DialogHandler.Instance.Fun_DialogShowOk(strShow, $"代码维护",null, 0);
                    return;
                }


            }


            string strSql = string.Empty, str = string.Empty;

            if (Btn_New.Enabled)
            {
                str = "新增";
                strSql = Fun_TableDataInsertIntoDataBase(Action.Insert);
            }
            else if (Btn_Edit.Enabled)
            {
                str = "修改";
                strSql = Fun_TableDataInsertIntoDataBase(Action.Update);
            }

            try
            {
                Data_Access.GetInstance().ExecuteNonQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL);
                EventLogHandler.Instance.LogInfo("5-2", $"{Cob_Table.Text.Trim()}代码维护{str}", $"{Cob_Table.Text.Trim()}{str}成功");
                PublicComm.ClientLog.Debug($"{Cob_Table.Text.Trim()}{str}成功");
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"{Cob_Table.Text.Trim()}代码维护{str}失败:{ex}");
                EventLogHandler.Instance.LogDebug("5-2", $"{Cob_Table.Text.Trim()}代码维护{str}", $"{Cob_Table.Text.Trim()}代码维护{str}失败:{ex}");
                PublicComm.ClientLog.Debug($"{Cob_Table.Text.Trim()}代碼維護{str}失敗:{ex}");
                return;
            }
            bolEditStatus = false;
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

            Pnl_CurrentRow.Visible = false;

            Dgv_Table.Enabled = true;
            dtPresetTable = Dt_GetDataTable(strTableName);
            Fun_TableDisplay(dtPresetTable, strTableName, Dgv_Table);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (!bolEditStatus) return;
            bolEditStatus = false;

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

            Pnl_CurrentRow.Visible = false;

            Dgv_Table.Enabled = true;
        }

        /// <summary>
        /// 维护项目ComboBox
        /// </summary>
        private void Fun_ComboBoxItemsDisplay()
        {
            DataTable dtGetTable = new DataTable();
            dtGetTable.Columns.Add(new DataColumn("Table"));
            dtGetTable.Columns.Add(new DataColumn("TableName"));
            DataRow dr = null;

            #region - Table Name -

            dr = dtGetTable.NewRow();
            dr[0] = string.Empty;
            dr[1] = string.Empty;
            dtGetTable.Rows.Add(dr);

            dr = dtGetTable.NewRow();
            dr[0] = nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition);
            dr[1] = "钢卷回退原因";
            dtGetTable.Rows.Add(dr);

            dr = dtGetTable.NewRow();
            dr[0] = nameof(DelayLocationEntity.TBL_DelayLocation_Definition);
            dr[1] = "停机位置";
            dtGetTable.Rows.Add(dr);

            dr = dtGetTable.NewRow();
            dr[0] = nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition);
            dr[1] = "停机原因";
            dtGetTable.Rows.Add(dr);

            dr = dtGetTable.NewRow();
            dr[0] = nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade);
            dr[1] = "钢种和屈服强度对照表";
            dtGetTable.Rows.Add(dr);

            dr = dtGetTable.NewRow();
            dr[0] = nameof(GradeGroupsEntity.TBL_GradeGroups);
            dr[1] = "钢种和客户代码对照表";
            dtGetTable.Rows.Add(dr);
            #endregion

            Cob_Table.DisplayMember = "TableName";
            Cob_Table.ValueMember = "Table";
            Cob_Table.DataSource = dtGetTable;
        }
        /// <summary>
        /// 根據ComboBox維護項目查詢資料表
        /// </summary>
        private DataTable Dt_GetDataTable(string Table)
        {
            string strSql;
            if (Table == nameof(GradeGroupsEntity.TBL_GradeGroups))
            {
                string strOrderby = $@"ORDER BY    {nameof(GradeGroupsEntity.TBL_GradeGroups.SteelGrade)},
                                                   {nameof(GradeGroupsEntity.TBL_GradeGroups.CustomerNo)}, 
                                                   {nameof(GradeGroupsEntity.TBL_GradeGroups.GradeGroup)} ";
                strSql = SqlFactory.Frm_5_2_SelectTable(Table, true, strOrderby);
            }
            else
            {
                strSql = SqlFactory.Frm_5_2_SelectTable(Table);
            }

            DataTable dtGetTable = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"{Table}栏位", "5-2");
            return dtGetTable;
        }

        /// <summary>
        /// 顯示資料表內容
        /// </summary>
        private void Fun_TableDisplay(DataTable dt, string Table, DataGridView Dgv)
        {
            if (dt.IsNull()) return;

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv, dt);
            switch (Table)
            {
                case nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition):
                    DGVColumnsHandler.Instance.Frm_5_2_ScheduleDelete_CoilReject_Code(Dgv);
                    break;

                case nameof(DelayLocationEntity.TBL_DelayLocation_Definition):
                    DGVColumnsHandler.Instance.Frm_5_2_DelayLocation(Dgv);
                   // Index_No = int.Parse(dt.Rows[dt.Rows.Count - 1][nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)].ToString()) + 1;
                    break;

                case nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition):
                    DGVColumnsHandler.Instance.Frm_5_2_DelayReasonCode(Dgv);
                   // Index_No = int.Parse(dt.Rows[dt.Rows.Count - 1][nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)].ToString()) + 1;
                    break;

                case nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade):
                    DGVColumnsHandler.Instance.Frm_5_2_MaterialGrade(Dgv);
                    break;

                case nameof(GradeGroupsEntity.TBL_GradeGroups):
                    DGVColumnsHandler.Instance.Frm_5_2_GradeGroups(Dgv);
                    break;

                default:
                    break;
            }
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv);

            Dgv.ClearSelection();
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

        /// <summary>
        /// Setting Dgv_CurrentRow Columns Title
        /// </summary>
        private void Fun_DataGridViewColumnsSetting()
        {
            Dgv_CurrentRow.Rows.Clear();
            Dgv_CurrentRow.Columns.Clear();

            switch (strTableName)
            {
                case nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition)://排程鋼卷刪除原因
                    DGVColumnsHandler.Instance.Frm_5_2_ScheduleDelete_CoilReject_Code(Dgv_CurrentRow);                    
                    break;

                case nameof(DelayLocationEntity.TBL_DelayLocation_Definition)://停機位置
                    DGVColumnsHandler.Instance.Frm_5_2_DelayLocation(Dgv_CurrentRow);                    
                    break;

                case nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition)://停機原因
                    DGVColumnsHandler.Instance.Frm_5_2_DelayReasonCode(Dgv_CurrentRow);                    
                    break;

                case nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade):
                     DGVColumnsHandler.Instance.Frm_5_2_MaterialGrade(Dgv_CurrentRow);                   
                    break;

                case nameof(GradeGroupsEntity.TBL_GradeGroups):
                     DGVColumnsHandler.Instance.Frm_5_2_GradeGroups(Dgv_CurrentRow);                    
                    break;

                default:
                   
                    break;
            }

        }

        private void Fun_DgvKeyReadOnly(DataGridView Dgv, string strCodeTable)
        {
            /*, params int[] intValues*/

            //用來設定欄位ReadOnly不可修改
            int[] intKeyColumns = new int[] { };
                       
            switch (strCodeTable)
            {
                case nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition)://排程鋼卷刪除原因
                    //DGVColumnsHandler.Instance.Frm_5_2_ScheduleDelete_CoilReject_Code(Dgv);
                    intKeyColumns = new int[] {0,1, 3,4 };
                    break;

                case nameof(DelayLocationEntity.TBL_DelayLocation_Definition)://停機位置
                    //DGVColumnsHandler.Instance.Frm_5_2_DelayLocation(Dgv);
                    intKeyColumns = new int[] { 3, 4 };
                    break;

                case nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition)://停機原因
                    //DGVColumnsHandler.Instance.Frm_5_2_DelayReasonCode(Dgv);
                    intKeyColumns = new int[] {6,7 };
                    break;

                case nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade):
                   // DGVColumnsHandler.Instance.Frm_5_2_MaterialGrade(Dgv);
                    intKeyColumns = new int[] {0,1,2, 4 };
                    break;

                case nameof(GradeGroupsEntity.TBL_GradeGroups):
                   // DGVColumnsHandler.Instance.Frm_5_2_GradeGroups(Dgv);
                    intKeyColumns = new int[] {0,1 };
                    break;

                default:
                    intKeyColumns = new int[] { 0 };
                    break;
            }
            foreach (int i in intKeyColumns)
            {
                Dgv.Columns[i].ReadOnly = true;
                Dgv.Rows[0].Cells[i].Style.BackColor = Color.LightGray;
            }


        }

        /// <summary>
        /// 設定按鈕啟用狀態並改顏色
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="bolE"></param>
        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            //Color colorBack;
            btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
        }

        /// <summary>
        /// 新增、修改語法
        /// </summary>
        /// <returns></returns>
        public string Fun_TableDataInsertIntoDataBase(Action action)
        {
            string strSql = string.Empty;

            switch (strTableName)
            {
                case nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition):
                    strSql = action.Equals(Action.Insert) ? SqlFactory.Frm_5_2_Insert_ScheduleDelete_CoilReject_Code() :
                             action.Equals(Action.Update) ? SqlFactory.Frm_5_2_Update_ScheduleDelete_CoilReject_Code() : SqlFactory.Frm_5_2_Delete_ScheduleDelete_CoilReject_Code();
                    break;

                case nameof(DelayLocationEntity.TBL_DelayLocation_Definition):
                    strSql = action.Equals(Action.Insert) ? SqlFactory.Frm_5_2_Insert_DelayLocation() :
                             action.Equals(Action.Update) ? SqlFactory.Frm_5_2_Update_DelayLocation() : SqlFactory.Frm_5_2_Delete_DelayLocation();
                    break;

                case nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition):
                    strSql = action.Equals(Action.Insert) ? SqlFactory.Frm_5_2_Insert_DelayReasonCode() :
                             action.Equals(Action.Update) ? SqlFactory.Frm_5_2_Update_DelayReasonCode() : SqlFactory.Frm_5_2_Delete_DelayReasonCode();
                    break;

                case nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade):
                    strSql = action.Equals(Action.Insert) ? SqlFactory.Frm_5_2_Insert_Material() :
                             action.Equals(Action.Update) ? SqlFactory.Frm_5_2_Update_Material() : SqlFactory.Frm_5_2_Delete_Material();
                    break; 

                        case nameof(GradeGroupsEntity.TBL_GradeGroups):
                    strSql = action.Equals(Action.Insert) ? SqlFactory.Frm_5_2_Insert_GradeGroups() :
                             action.Equals(Action.Update) ? SqlFactory.Frm_5_2_Update_GradeGroups() : SqlFactory.Frm_5_2_Delete_GradeGroups();
                    break;

                default:
                    break;
            }

            return strSql;
        }


        private bool Fun_Check_DgvCurrentRow_MustIsNullShow(string strTableName)
        {
            string strShowText = " 请勿空白";
            string strTitle = "提示资讯";
            //bool bolCheck = true;
            switch (strTableName)
            {
                #region TBL_ScheduleDelete_CoilReject_CodeDefinition

                case nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition):
                    string strGroupNo = nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo);
                    string strRejectCode = nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code);
                    if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells[strGroupNo].Value.ToString().Trim()))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"{Dgv_CurrentRow.Columns[strGroupNo].HeaderText} {strShowText}", strTitle, null, 3);
                        return false;
                    }

                    if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells[strRejectCode].Value.ToString().Trim()))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"{Dgv_CurrentRow.Columns[strRejectCode].HeaderText} {strShowText}", strTitle, null, 3);
                        return false;
                    }
                    //if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells[2].Value.ToString().Trim())) bolCheck = false;
                    //if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells[3].Value.ToString().Trim())) bolCheck = false;
                    break;

                #endregion

                #region TBL_DelayLocation_Definition

                case nameof(DelayLocationEntity.TBL_DelayLocation_Definition):
                    string strIndexNo_L = nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No);
                    string strLocationCode = nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode);
                    if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells[strIndexNo_L].Value.ToString().Trim()))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"{Dgv_CurrentRow.Columns[strIndexNo_L].HeaderText} {strShowText}", strTitle, null, 3);
                        return false;
                    }
                    if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells[strLocationCode].Value.ToString().Trim()))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"{Dgv_CurrentRow.Columns[strLocationCode].HeaderText} {strShowText}", strTitle, null, 3);
                        return false;
                    }
                    break;
                #endregion

                #region TBL_DelayReasonCode_Definition

                case nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition):
                    string strIndexNo_R = nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No);
                    string strReasonCode = nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode);
                    if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells[strIndexNo_R].Value.ToString().Trim()))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"{Dgv_CurrentRow.Columns[strIndexNo_R].HeaderText} {strShowText}", strTitle, null, 3);
                        return false;
                    }
                    if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells[strReasonCode].Value.ToString().Trim()))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"{Dgv_CurrentRow.Columns[strReasonCode].HeaderText} {strShowText}", strTitle, null, 3);
                        return false;
                    }
                    //if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)].Value.ToString().Trim())) bolCheck = false;
                    //if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells[nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode)].Value.ToString().Trim())) bolCheck = false;
                    break;

                #endregion

                #region TBL_SteelNoToMaterialGrade

                case nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade):
                    string strSt_No = nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No);
                    if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells[strSt_No].Value.ToString().Trim()))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"{Dgv_CurrentRow.Columns[strSt_No].HeaderText} {strShowText}", strTitle,null, 3);
                        return false;
                    }
                    //if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells[nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Material_Grade)].Value.ToString().Trim())) bolCheck = false;                 
                    break;

                #endregion

                default:
                    break;
            }
            return true;
        }
        private string[] Fun_GetMustColumn(string strTable_Name)
        {
            string[] CtlMust = new string[] { };
            switch (strTable_Name)
            {
                #region TBL_ScheduleDelete_CoilReject_CodeDefinition

                case nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition):
                    string strGroupNo = nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo);
                    string strRejectCode = nameof(ScheduleDelete_CoilReject_CodeEntity.L3L2_TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code);
                    CtlMust = new string[] { strGroupNo, strRejectCode };

                    break;

                #endregion

                #region TBL_DelayLocation_Definition

                case nameof(DelayLocationEntity.TBL_DelayLocation_Definition):
                    string strIndexNo_L = nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No);
                    string strLocationCode = nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode);
                    CtlMust = new string[] { strIndexNo_L, strLocationCode };

                    break;
                #endregion

                #region TBL_DelayReasonCode_Definition

                case nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition):
                    string strIndexNo_R = nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No);
                    string strReasonCode = nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode);
                    CtlMust = new string[] { strIndexNo_R, strReasonCode };

                    break;

                #endregion

                #region TBL_SteelNoToMaterialGrade

                case nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade):
                    string strSt_No = nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No);
                    CtlMust = new string[] { strSt_No };

                    break;

                #endregion

                default:
                    break;
            }
            return CtlMust;
        }

        private bool Fun_Check_HaveData(string strTableName, DataGridView dgvCheck)
        {
            string[] CtlMust = Fun_GetMustColumn(strTableName);

            StringBuilder sbFindSql = new StringBuilder();
            sbFindSql.AppendLine($" SELECT * ");
            sbFindSql.AppendLine($" FROM   {strTableName} ");
            sbFindSql.AppendLine($" WHERE {CtlMust[0]} <> N'' ");

            if (CtlMust.Length > 0)
            {
                foreach (string strCol in CtlMust)
                {
                    sbFindSql.AppendLine($" AND {strCol} = N'{dgvCheck.Rows[0].Cells[strCol].Value}' ");
                }
            }

            string strFindSql = sbFindSql.ToString();

            DataTable dtGetTable = Data_Access.Fun_SelectDate(strFindSql, GlobalVariableHandler.Instance.strConn_GPL, $"{strTableName}资料确认", "5-2");
            
            if (dtGetTable != null && dtGetTable.Rows.Count > 0)
            {
                //資料已存在!
                return true;
            }
            else
            {
                return false;
            }

        }

        private string Fun_GetHaveDataShow(string strTableName, DataGridView dgvCheck)
        {
            string[] CtlMust = Fun_GetMustColumn(strTableName);

            StringBuilder sbShow = new StringBuilder();
            //sbShow.AppendLine($"{strTableName}参数");

            if (CtlMust.Length > 0)
            {
                foreach (string strCol in CtlMust)
                {
                    sbShow.AppendLine($"{dgvCheck.Columns[strCol].HeaderText} : { dgvCheck.Rows[0].Cells[strCol].Value.ToString().Trim()}");
                }
            }

            string strShow = sbShow.ToString();
            return strShow;
        }


        private void Dgv_CurrentRow_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            DataGridView dgv = (DataGridView)sender;
            string strHeaderText = dgv.Columns[e.ColumnIndex].HeaderText;
            DialogHandler.Instance.Fun_DialogShowOk($"[{strHeaderText}] 栏位输入格式错误，请重新输入！", $"警告资讯",null, 3);
        }
    }
}

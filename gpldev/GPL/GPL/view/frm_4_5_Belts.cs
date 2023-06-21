
using DBService.Repository.Belt;
using DBService.Repository.BeltMaterials;
using DBService.Repository.BeltSuppliers;
using GPLManager.Util;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static GPLManager.DataBaseTableFactory;

namespace GPLManager
{
    public partial class Frm_4_5_Belts : Form
    {

        #region 變數
        DataTable dtBelts;
        DataTable dtMaterials;
        DataTable dtSuppliers;
        DataTable dtGRBelts;
        DataTable dtGetItems;

        /// <summary>
        /// 新增:True 修改:False
        /// </summary>
        bool BeltsEdit, MaterialsEdit, SupplierEdit;
        #endregion
        
        //語系
        private LanguageHandler LanguageHand;
        public Frm_4_5_Belts()
        {
            InitializeComponent();
        }
        private void Frm_4_5_Belts_Load(object sender, EventArgs e)
        {
            PublicForms.Belts = this;
            Control[] Frm_4_5_Control = new Control[] {
            Btn_Belts_New,//皮帶管理 新增
            Btn_Belts_Edit,//皮帶管理 修改
            Btn_Belts_Del,//皮帶管理 删除

            Btn_Materials_Add,//材料 新增
            Btn_Materials_Edit,//材料 修改

            Btn_Suppliers_Add,//供應商 新增
            Btn_Suppliers_Edit//供應商 修改
            };
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_4_5_Control, UserSetupHandler.Instance.Frm_4_5);

            string strSql = SqlFactory.Frm_4_5_SelectBelts_DB_TBL_Belts();
            Fun_SelectBelts(strSql);
            Fun_SelectMaterials();
            Fun_SelectSuppilers();
            Fun_SelectGRBelts();

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }
        private void Frm_4_5_Belts_Shown(object sender, EventArgs e)
        {
            Fun_SelectComboBox();

        }

        private void Frm_4_5_Belts_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
                Fun_SelectGRBelts();
        }

        private void Tab_BeltsControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Tab_BeltsControl.SelectedIndex == 1)            
                Fun_SelectGRBelts();            
        }

        #region 皮帶管理

        private void Fun_SelectGRBelts()
        {
            string strSql = SqlFactory.Frm_4_5_SelectBeltsGR_DB_TBL_Belts();
            dtGRBelts = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "查询研磨机皮带编号", "4-5");

            if (dtGRBelts.IsNull()) return;

            for (int i = 0; i < dtGRBelts.Rows.Count; i++)
            {
                if (dtGRBelts.Rows[i][nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)].ToString().Equals("1"))
                {
                    Txt_GR1.Text = dtGRBelts.Rows[i][nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty;
                }
                else if (dtGRBelts.Rows[i][nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)].ToString().Equals("2"))
                {
                    Txt_GR2.Text = dtGRBelts.Rows[i][nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty;
                }
                else if (dtGRBelts.Rows[i][nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)].ToString().Equals("3"))
                {
                    Txt_GR3.Text = dtGRBelts.Rows[i][nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty;
                }
                else if (dtGRBelts.Rows[i][nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)].ToString().Equals("4"))
                {
                    Txt_GR4.Text = dtGRBelts.Rows[i][nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty;
                }
                else if (dtGRBelts.Rows[i][nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)].ToString().Equals("5"))
                {
                    Txt_GR5.Text = dtGRBelts.Rows[i][nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty;
                }
                else if (dtGRBelts.Rows[i][nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)].ToString().Equals("6"))
                {
                    Txt_GR6.Text = dtGRBelts.Rows[i][nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// 皮帶管理DGV
        /// </summary>
        private void Fun_SelectBelts(string strSql)
        {
            dtBelts = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "查询皮带", "4-5");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Belts, dtBelts);
            DGVColumnsHandler.Instance.Frm_4_5_Belts(Dgv_Belts);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Belts);
        }

        private void Fun_SelectComboBox()
        {
            //cbo_Belt_Particle_Number.DataSource.Clear();
            //cbo_Particle_Number.Items.Clear();
            //cbo_BeltsKind.Items.Clear();
            //cbo_Mount_GR_No.Items.Clear();

            //皮帶號數
            string strSql = SqlFactory.Frm_4_5_SelectBeltsComboBox_DB_Belts();
            dtGetItems = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"皮带号数ComboBox Items","4-5");

            if (dtGetItems.IsNull()) return;

            Cob_Belt_Particle_Number.DisplayMember = nameof(BeltAccEntity.TBL_Belts.Belt_Particle_Number);
            Cob_Belt_Particle_Number.ValueMember = nameof(BeltAccEntity.TBL_Belts.Belt_Particle_Number);
            Cob_Belt_Particle_Number.DataSource = dtGetItems;

            Cob_Belt_Particle_Number.Text = string.Empty;

            dtGetItems = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "皮带号数ComboBox Items", "4-5");

            if (dtGetItems.IsNull()) return;

            Cob_Particle_Number.DisplayMember = nameof(BeltAccEntity.TBL_Belts.Belt_Particle_Number);
            Cob_Particle_Number.ValueMember = nameof(BeltAccEntity.TBL_Belts.Belt_Particle_Number);
            Cob_Particle_Number.DataSource = dtGetItems;

            Cob_Particle_Number.Text = string.Empty;

            //研磨機號
            strSql = SqlFactory.Frm_4_5_SelectBelts_GR_ComboBox_DB_Belts();
            dtGetItems = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "研磨机号ComboBox Items", "4-5");

            if (dtGetItems.IsNull()) return;

            Cob_Mount_GR_No.DisplayMember = nameof(BeltAccEntity.TBL_Belts.Mount_GR_No);
            Cob_Mount_GR_No.ValueMember = nameof(BeltAccEntity.TBL_Belts.Mount_GR_No);
            Cob_Mount_GR_No.DataSource = dtGetItems;
            Cob_Mount_GR_No.Text = string.Empty;
            
            //皮帶種類
            strSql = SqlFactory.Frm_4_5_SelectBeltsKindComboBox_DB_Belts();
            dtGetItems = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "皮带种类ComboBox Items", "4-5");

            if (dtGetItems.IsNull()) return;

            Cob_BeltsKind.DisplayMember = nameof(BeltAccEntity.TBL_Belts.Belt_Type);
            Cob_BeltsKind.ValueMember = nameof(BeltAccEntity.TBL_Belts.Belt_Type);
            Cob_BeltsKind.DataSource = dtGetItems;
            Cob_BeltsKind.Text = string.Empty;

           
            //供應商
            strSql = SqlFactory.Frm_4_5_SelectSuppliers_DB_BeltSuppliers();
            dtGetItems = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "供应商ComboBox Items", "4-5");

            if (dtGetItems.IsNull()) return;

            Cob_SelectSuppler_Code.DisplayMember = nameof(TBL_BeltSuppliers.SUPPLIER_NAME);
            Cob_SelectSuppler_Code.ValueMember = nameof(TBL_BeltSuppliers.SUPPLIER_CODE);
            Cob_SelectSuppler_Code.DataSource = dtGetItems;
            Cob_SelectSuppler_Code.Text = string.Empty;

            dtGetItems = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "供应商ComboBox Items", "4-5");

            if (dtGetItems.IsNull()) return;

            Cob_Suppler_Code.DisplayMember = nameof(TBL_BeltSuppliers.SUPPLIER_NAME);
            Cob_Suppler_Code.ValueMember = nameof(TBL_BeltSuppliers.SUPPLIER_CODE);
            Cob_Suppler_Code.DataSource = dtGetItems;
            Cob_Suppler_Code.Text = string.Empty;

            //材質代碼
            strSql = SqlFactory.Frm_4_5_SelectMaterials_DB_TBL_BeltMaterials();

            dtGetItems = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "供应商ComboBox Items", "4-5");

            if (dtGetItems.IsNull()) return;

            Cob_Material_Code.DisplayMember = nameof(TBL_BeltMaterials.MATERIAL_NAME);
            Cob_Material_Code.ValueMember = nameof(TBL_BeltMaterials.MATERIAL_CODE);
            Cob_Material_Code.DataSource = dtGetItems;
            Cob_Material_Code.Text = string.Empty;
        }
        
        //皮帶管理-修改
        private void Btn_Belts_Edit_Click(object sender, EventArgs e)
        {
            if (Dgv_Belts.CurrentIsNull()) return;
            if (Dgv_Belts.DgvIsNull()) return;

            //DataRow drGetBeltCurrentRow = dtBelts.Rows[Dgv_Belts.CurrentRow.Index];           
            DataRow drGetBeltCurrentRow = Fun_GetDataRowFromCurrentRow(Dgv_Belts, dtBelts);
            DataTable dt = dtBelts.Clone();
            try
            {
                dt.LoadDataRow(drGetBeltCurrentRow.ItemArray, false);
            }
            catch { return; }


            Pnl_Add.Visible = true;
            Lbl_Belts_Title.Text = "修改";
            BeltsEdit = false;
            Txt_Belt_No.Text = drGetBeltCurrentRow[nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty;
            Txt_Belt_No.ReadOnly = true;
            Cob_BeltsKind.SelectedValue = drGetBeltCurrentRow[nameof(BeltAccEntity.TBL_Belts.Belt_Type)].ToString() ?? string.Empty;

            Cob_Particle_Number.SelectedValue = drGetBeltCurrentRow[nameof(BeltAccEntity.TBL_Belts.Belt_Particle_Number)].ToString() ?? string.Empty;

            Cob_Suppler_Code.SelectedValue = drGetBeltCurrentRow[nameof(BeltAccEntity.TBL_Belts.Suppler_Code)].ToString() ?? string.Empty;

            Cob_Material_Code.SelectedValue = drGetBeltCurrentRow[nameof(BeltAccEntity.TBL_Belts.Material_Code)].ToString() ?? string.Empty;

            Txt_Total_Grind_Length_Belt.Text = drGetBeltCurrentRow[nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Belt)].ToString() ?? string.Empty;

            Txt_Total_Grind_Length_Strip.Text = drGetBeltCurrentRow[nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Strip)].ToString() ?? string.Empty;

            Cob_Mount_GR_No.SelectedValue = drGetBeltCurrentRow[nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)].ToString() ?? string.Empty;

            Fun_SetBottonEnabled(Btn_Belts_New, false);
            Fun_SetBottonEnabled(Btn_Belts_Del, false);
            Btn_Belts_Save.Visible = true;
            Btn_Belts_Cancel.Visible = true;

        }
        //皮帶管理-新增
        private void Btn_Belts_New_Click(object sender, EventArgs e)
        {
            Pnl_Add.Visible = true;
            Lbl_Belts_Title.Text = "新增";
            BeltsEdit = true;

            Txt_Belt_No.Text = "";

            string strNumber = Cob_Belt_Particle_Number.SelectedValue != null ? Cob_Belt_Particle_Number.SelectedValue.ToString() : "";
            string strSuppler = Cob_SelectSuppler_Code.Text;
            Cob_Particle_Number.SelectedIndex = Cob_Particle_Number.FindStringExact(strNumber);
            Cob_Suppler_Code.SelectedIndex = Cob_Suppler_Code.FindStringExact(strSuppler);
                     
            Fun_SetBottonEnabled(Btn_Belts_Edit, false);
            Fun_SetBottonEnabled(Btn_Belts_Del, false);
            Btn_Belts_Save.Visible = true;
            Btn_Belts_Cancel.Visible = true;
        }
        //皮帶管理-取消
        private void Btn_Belts_Cancel_Click(object sender, EventArgs e)
        {
            Pnl_Add.Visible = false;
            Fun_SetBottonEnabled(Btn_Belts_New, true);
            Fun_SetBottonEnabled(Btn_Belts_Edit, true);
            Fun_SetBottonEnabled(Btn_Belts_Del, true);
            Btn_Belts_Save.Visible = false;
            Btn_Belts_Cancel.Visible = false;
        }
        //皮帶管理-儲存
        private void Btn_Belts_Save_Click(object sender, EventArgs e)
        {
            string strSql = string.Empty;

            //檢查研磨機號欄位
            if (Cob_Mount_GR_No.Text != "0")
            {
                strSql = SqlFactory.Frm_4_5_selectGR_NO_DB_TBL_Belts(Cob_Mount_GR_No.Text);
                dtGetItems = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "檢查研磨機皮带编号", "4-5");

                if (dtGetItems.IsNull()) return;

                if (dtGetItems.Rows.Count > 0)
                {
                    if (dtGetItems.Rows[0][nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() != Dgv_Belts.CurrentRow.Cells[0].Value.ToString())
                    {
                        EventLogHandler.Instance.EventPush_Message($"{Cob_Mount_GR_No.Text}号研磨机上已有皮带，请重新确认");
                        EventLogHandler.Instance.LogInfo("4-5", $"使用者:{PublicForms.Main.lblLoginUser.Text}皮带管理", $"{Cob_Mount_GR_No.Text}号研磨机上已有皮带，请重新确认");
                        PublicComm.ClientLog.Info($"{Cob_Mount_GR_No.Text}上已有皮帶，請重新確認");
                        return;
                    }
                }

            }

            //新增
            if (BeltsEdit)
            {
                strSql = SqlFactory.Frm_4_5_InsertBelts_DB_TBL_Belts();
            }
            //修改
            else if (BeltsEdit == false)
            {
                strSql = SqlFactory.Frm_4_5_UpdateBelts_DB_TBL_Belts();
            }

            bool bolSave =  Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL,$"{Lbl_Belts_Title.Text}[TBL_Belts]");

            if (!bolSave) return;

            string strNumber = Cob_Particle_Number.SelectedValue.ToString();
            string strSuppler = Cob_Suppler_Code.Text;
           
            Pnl_Add.Visible = false;

            //strSql = SqlFactory.Frm_4_5_SelectBelts_DB_TBL_Belts();
            //Fun_SelectBelts(strSql);
            Fun_SelectComboBox();

            Cob_Belt_Particle_Number.SelectedIndex = Cob_Belt_Particle_Number.FindStringExact(strNumber);
            Cob_SelectSuppler_Code.SelectedIndex = Cob_SelectSuppler_Code.FindStringExact(strSuppler);

            Btn_Search.PerformClick();

            Fun_SetBottonEnabled(Btn_Belts_New, true);
            Fun_SetBottonEnabled(Btn_Belts_Edit, true);
            Fun_SetBottonEnabled(Btn_Belts_Del, true);
            Btn_Belts_Save.Visible = false;
            Btn_Belts_Cancel.Visible = false;
        }

        private void Btn_Belts_Del_Click(object sender, EventArgs e)
        {
            //操作要求皮帶管理畫面增加刪除功能,與確認提示(彈窗)
            if (Dgv_Belts.Rows.Count.Equals(0)) { return; }
            if (Dgv_Belts.CurrentIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选择要删除的皮带", "删除皮带", Properties.Resources.dialogInformation, 0);
                return;
            }
            if (Dgv_Belts.DgvIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选择要删除的皮带", "删除皮带", Properties.Resources.dialogInformation, 0);
                return;
            }
            DataRow drGetBeltCurrentRow = Fun_GetDataRowFromCurrentRow(Dgv_Belts, dtBelts);
           // DataRow drGetBeltCurrentRow = dtBelts.Rows[Dgv_Belts.CurrentRow.Index];
            string strBelt_No = drGetBeltCurrentRow[nameof(BeltAccEntity.TBL_Belts.Belt_No)].ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(strBelt_No.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选择要删除的皮带", "删除皮带", Properties.Resources.dialogInformation, 0);
                return;
            }

            Fun_SetBottonEnabled(Btn_Belts_New, false);
            Fun_SetBottonEnabled(Btn_Belts_Edit, false);

            string strMessage = $"删除皮带编号[{strBelt_No.Trim()}]";

            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel($"是否{strMessage}", "删除皮带", Properties.Resources.dialogQuestion, 1);
            if(dialogR == DialogResult.OK)
            {
              string  strSql = SqlFactory.Frm_4_5_DeleteBelts_DB_TBL_Belts(strBelt_No);

                if (!Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, "删除皮带", "4-5"))
                {
                    EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} {strMessage}删除失敗");
                    return;
                }
                EventLogHandler.Instance.LogInfo("4-5", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} {strMessage}", $"删除皮带");
                EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} {strMessage}");
                PublicComm.ClientLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} {strMessage}");
                PublicComm.akkaLog.Info($"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()} {strMessage}");
            }

            Fun_SelectComboBox();
            Btn_Search.PerformClick();           

            Fun_SetBottonEnabled(Btn_Belts_New, true);
            Fun_SetBottonEnabled(Btn_Belts_Edit, true);
        }


        #endregion

        #region 材料
        /// <summary>
        /// 材料DGV
        /// </summary>
        private void Fun_SelectMaterials()
        {
            string strSql = SqlFactory.Frm_4_5_SelectBeltMaterials_DB_TBL_BeltMaterials();
            dtMaterials = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "皮帶材料", "4-5");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Materials, dtMaterials);
            DGVColumnsHandler.Instance.Frm_4_5_Materials(Dgv_Materials);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Materials);
        }

        //材料-修改
        private void Btn_Materials_Edit_Click(object sender, EventArgs e)
        {
            if (dtMaterials.IsNull()) return;
            if (Dgv_Materials.CurrentIsNull()) return;
            if (Dgv_Materials.DgvIsNull()) return;

            //DataRow drGetMaterial = dtMaterials.Rows[Dgv_Materials.CurrentRow.Index];
            DataRow drGetMaterial = Fun_GetDataRowFromCurrentRow(Dgv_Materials, dtMaterials);
            //DataTable dt = dtMaterials.Clone();
            //try
            //{
            //    dt.LoadDataRow(drGetMaterial.ItemArray, false);
            //}
            //catch { return; }

            Pnl_Materials.Visible = true;
            MaterialsEdit = false;
            Lbl_Materials_Title.Text = "修改";
            Txt_MaterialCode.Text = drGetMaterial[nameof(BeltMaterialEntity.TBL_BeltMaterials.MATERIAL_CODE)].ToString() ?? string.Empty;
            Txt_MaterialName.Text = drGetMaterial[nameof(BeltMaterialEntity.TBL_BeltMaterials.MATERIAL_NAME)].ToString() ?? string.Empty;

            Fun_SetBottonEnabled(Btn_Materials_Add, false);
            Btn_Materials_Save.Visible = true;
            Btn_Materials_Cancel.Visible = true;
        }
        //材料-新增
        private void Btn_Materials_Add_Click(object sender, EventArgs e)
        {
            Pnl_Materials.Visible = true;
            MaterialsEdit = true;
            Lbl_Materials_Title.Text = "新增";

            Fun_SetBottonEnabled(Btn_Materials_Edit, false);
            Btn_Materials_Save.Visible = true;
            Btn_Materials_Cancel.Visible = true;
        }
        //材料-儲存
        private void Btn_Materials_Save_Click(object sender, EventArgs e)
        {
            string strInsertEdit = string.Empty, strSql = string.Empty;
            //新增
            if (MaterialsEdit)
            {
                strInsertEdit = "新增";
                strSql = SqlFactory.Frm_4_5_InsertMaterial_DB_TBL_BeltMaterials();
            }
            //修改
            else if (MaterialsEdit == false)
            {
                strInsertEdit = "修改";
                strSql = SqlFactory.Frm_4_5_UpdateMaterial_DB_TBL_BeltMaterials();
            }
            Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL,$"{strInsertEdit}[TBL_BeltMaterials]");

            Pnl_Materials.Visible = false;

            Fun_SetBottonEnabled(Btn_Materials_Edit, true);
            Fun_SetBottonEnabled(Btn_Materials_Add, true);
            Btn_Materials_Save.Visible = false;
            Btn_Materials_Cancel.Visible = false;

            Fun_SelectMaterials();
            Fun_SelectComboBox();
        }
        //材料-取消
        private void Btn_Materials_Cancel_Click(object sender, EventArgs e)
        {
            Pnl_Materials.Visible = false;

            Fun_SetBottonEnabled(Btn_Materials_Edit, true);
            Fun_SetBottonEnabled(Btn_Materials_Add, true);
            Btn_Materials_Save.Visible = false;
            Btn_Materials_Cancel.Visible = false;
        }

        private void Btn_Materials_Del_Click(object sender, EventArgs e)
        {
            //操作要求皮帶管理畫面增加刪除功能,與確認提示(彈窗)
        }


        #endregion

        #region 供應商
        /// <summary>
        /// 供應商DGV
        /// </summary>
        private void Fun_SelectSuppilers()
        {
            string strSql = SqlFactory.Frm_4_5_SelectBeltSuppliers_DB_TBL_BeltSuppliers();

            dtSuppliers = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "供应商", "4-5");

            if (dtSuppliers.IsNull()) return;

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Suppliers, dtSuppliers);
            DGVColumnsHandler.Instance.Frm_4_5_BeltSuppliers(Dgv_Suppliers);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Suppliers);
        }

        //供應商-修改
        private void Btn_Suppliers_Edit_Click(object sender, EventArgs e)
        {
            if (dtSuppliers.IsNull()) return;
            if (Dgv_Suppliers.CurrentIsNull()) return;
            if (Dgv_Suppliers.DgvIsNull()) return;

            //DataRow drGetSuppliers = dtSuppliers.Rows[Dgv_Suppliers.CurrentRow.Index];
            DataRow drGetSuppliers = Fun_GetDataRowFromCurrentRow(Dgv_Suppliers, dtSuppliers);
            //DataTable dt = dtSuppliers.Clone();
            //try
            //{
            //    dt.LoadDataRow(drGetSuppliers.ItemArray, false);
            //}
            //catch { return; }

            Pnl_Suppliers.Visible = true;
            Lbl_Supplier_Title.Text = "修改";
            SupplierEdit = false;
            Txt_SupplierCode.Text = drGetSuppliers[nameof(BeltSuppliersEntity.TBL_BeltSuppliers.SUPPLIER_CODE)].ToString() ?? string.Empty;
            Txt_SupplierName.Text = drGetSuppliers[nameof(BeltSuppliersEntity.TBL_BeltSuppliers.SUPPLIER_NAME)].ToString() ?? string.Empty;

            Fun_SetBottonEnabled(Btn_Suppliers_Add, false);
            Btn_Suppliers_Save.Visible = true;
            Btn_Suppliers_Cancel.Visible = true;
        }
        //供應商-新增
        private void Btn_Suppliers_Add_Click(object sender, EventArgs e)
        {
            Pnl_Suppliers.Visible = true;
            Lbl_Supplier_Title.Text = "新增";
            SupplierEdit = true;

            Fun_SetBottonEnabled(Btn_Suppliers_Edit, false);
            Btn_Suppliers_Save.Visible = true;
            Btn_Suppliers_Cancel.Visible = true;
        }
        //供應商-儲存
        private void Btn_Suppliers_Save_Click(object sender, EventArgs e)
        {
            string strSql = string.Empty;

            //新增
            if (SupplierEdit)
            {
                strSql = SqlFactory.Frm_4_5_InsertSuppliers_DB_TBL_BeltSuppliers();
            }
            //修改
            else if (SupplierEdit == false)
            {
                strSql = SqlFactory.Frm_4_5_UpdateSuppliers_DB_TBL_BeltSuppliers();
            }
            Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"{Lbl_Supplier_Title.Text}[TBL_BeltSuppliers]");

            Pnl_Suppliers.Visible = false;

            Fun_SetBottonEnabled(Btn_Suppliers_Add, true);
            Fun_SetBottonEnabled(Btn_Suppliers_Edit, true);
            Btn_Suppliers_Save.Visible = false;
            Btn_Suppliers_Cancel.Visible = false;

            Fun_SelectSuppilers();
            Fun_SelectComboBox();
        }
        //供應商-取消
        private void Btn_Suppliers_Cancel_Click(object sender, EventArgs e)
        {
            Pnl_Suppliers.Visible = false;
            Fun_SetBottonEnabled(Btn_Suppliers_Add, true);
            Fun_SetBottonEnabled(Btn_Suppliers_Edit, true);
            Btn_Suppliers_Save.Visible = false;
            Btn_Suppliers_Cancel.Visible = false;
        }

        private void Btn_Suppliers_Del_Click(object sender, EventArgs e)
        {
           
        }

        #endregion

        #region TabControl
        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_BeltsControl,e);
        }

        #endregion

        private void Btn_Mount_GR_No_Q_Click(object sender, EventArgs e)
        {
            if (Pnl_Spare.Visible == true)
                Pnl_Spare.Visible = false;
            else
            {
                StringBuilder sbShow = new StringBuilder();
                sbShow.AppendLine("0 : 未安裝/未上線");
                sbShow.AppendLine("1 : 1号研磨机");
                sbShow.AppendLine("2 : 2号研磨机");
                sbShow.AppendLine("3 : 3号研磨机");
                sbShow.AppendLine("4 : 4号研磨机");
                sbShow.AppendLine("5 : 5号研磨机");
                sbShow.AppendLine("6 : 6号研磨机");
                Txt_Spare.Text = sbShow.ToString();
                Lbl_Spare_ComboName.Text = "在线研磨机";
                Pnl_Spare.Visible = true;
                Pnl_Spare.BringToFront();
                Txt_Spare.ScrollBars = ScrollBars.None;
                Pnl_Spare.Location = new Point(1153, 418);
                Pnl_Spare.Size = new Size(240, 240);
               
            }
            
            
        }

        private void Btn_Close_Spare_Click(object sender, EventArgs e)
        {
            if (Pnl_Spare.Visible == true)
                Pnl_Spare.Visible = false;
        }

      
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            string strSql = SqlFactory.Frm_4_5_SelectBelts_DB_TBL_Belts();

            if (Cob_Belt_Particle_Number.Text.IsEmpty() && Txt_Total_Grind_Length.Text.IsEmpty() && Cob_SelectSuppler_Code.Text.IsEmpty())
            {
                Fun_SelectBelts(strSql);
            }
            else
            {
                strSql += $" Where {nameof(BeltAccEntity.TBL_Belts.Belt_No)} is not Null ";

                if (!Cob_Belt_Particle_Number.Text.IsEmpty())
                {
                    strSql += $" AND [{nameof(BeltAccEntity.TBL_Belts.Belt_Particle_Number)}] = '{Cob_Belt_Particle_Number.Text}'";
                }

                if (!Txt_Total_Grind_Length.Text.IsEmpty())
                {
                    //if (!cbo_Belt_Particle_Number.Text.IsEmpty())
                    //{
                        strSql += $" AND ([{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Belt)}] <= '{Txt_Total_Grind_Length.Text}' ";
                        strSql += $" or [{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Strip)}] <= '{Txt_Total_Grind_Length.Text}') ";
                    //}
                    //else if (cbo_Belt_Particle_Number.Text.IsEmpty())
                    //{
                    //    strSql += $" AND [{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Belt)}] <= '{txtTotal_Grind_Length.Text}' ";
                    //    strSql += $" or [{nameof(BeltAccEntity.TBL_Belts.Total_Grind_Length_Strip)}] <= '{txtTotal_Grind_Length.Text}' ";
                    //}
                }
                //else if (txtTotal_Grind_Length.Text.IsEmpty())
                //{
                    //if (!cbo_SelectSuppler_Code.Text.IsEmpty())
                    //{
                    //    if (!cbo_Belt_Particle_Number.Text.IsEmpty())
                    //    {
                    //        strSql += $" and [{nameof(BeltAccEntity.TBL_Belts.Suppler_Code)}] = '{cbo_SelectSuppler_Code.SelectedValue}' ";
                    //    }
                    //    else if (!cbo_Belt_Particle_Number.Text.IsEmpty())
                    //    {
                    //        strSql += $" AND [{nameof(BeltAccEntity.TBL_Belts.Suppler_Code)}] = '{cbo_SelectSuppler_Code.SelectedValue}' ";
                    //    }
                    //}
                //}

                if (!Cob_SelectSuppler_Code.Text.IsEmpty())
                {
                    strSql += $" AND [{nameof(BeltAccEntity.TBL_Belts.Suppler_Code)}] = '{Cob_SelectSuppler_Code.SelectedValue}' ";
                }
            }

            Fun_SelectBelts(strSql);
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

        //設定按鈕啟用狀態並改顏色
        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            //Color colorBack;
            btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
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
        private void Fun_LanguageIsEn_Font14_12(object sender, EventArgs e)
        {
            FontStyle fs = ((System.Windows.Forms.Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((System.Windows.Forms.Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)12, fs);
        }    
        #endregion Fun_LanguageIsEn_Font End
    }
}

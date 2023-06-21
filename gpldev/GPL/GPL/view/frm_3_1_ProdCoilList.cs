using DBService.Repository.PDI;
using DBService.Repository.PDO;
using GPLManager.Util;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static GPLManager.DataBaseTableFactory;

namespace GPLManager
{
    public partial class Frm_3_1_ProdCoilList : Form
    {
        #region 變數
        private DataTable dt;
        #endregion

        //語系
        private LanguageHandler LanguageHand;
        public Frm_3_1_ProdCoilList()
        {
            InitializeComponent();
        }

        private void Frm_3_1_ProdCoilList_Load(object sender, EventArgs e)
        {
            if (PublicForms.ProdCoilList == null) PublicForms.ProdCoilList = this;
            ////PDO預先顯示資料
            //Fun_DataGridViewPDO();
            Chk_Finish_Time.Checked = true;

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        private void Frm_3_1_ProdCoilist_Shown(object sender, EventArgs e)
        {
            //班別
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Team, Cob_Team);
            //ComboBox
            Fun_ComboBoxItemsDisplay();
            //計數
            Lbl_CoilCount.Text = dt != null ? dt.Rows.Count.ToString():"" ;
        }

        #region ComboBox

        /// <summary>
        /// ComboBox选项
        /// </summary>
        private void Fun_ComboBoxItemsDisplay()
        {
            Fun_SelectOut_Mat_No();

            Fun_SelectIn_Mat_No();

            Fun_SelectSteelGrade();

            Fun_SelectCustomer();

            Fun_SelectSg_Sign();

            //Process Code
            Cob_ProcessCode.Items.Clear();
            Cob_ProcessCode.Items.Add("GP01");
            Cob_ProcessCode.Items.Add("GP02");
            Cob_ProcessCode.Items.Add("GP03");
            Cob_ProcessCode.Items.Add("GP04");
            Cob_ProcessCode.Items.Add("GP05");
            Cob_ProcessCode.Items.Add("GP06");
            Cob_ProcessCode.Items.Add("GP07");
        }

        /// <summary>
        /// 出口卷號清單
        /// </summary>
        private void Fun_SelectOut_Mat_No()
        {
            string strSql = SqlFactory.Frm_3_1_SelectComboBoxItems(nameof(PDOEntity.TBL_PDO.Out_Coil_ID));
            DataTable dtOut_Mat_No = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"出口卷号清单","3-1");

            if (dtOut_Mat_No.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message("无出口卷清单");
                return;
            } 

            Cob_Exit_Coil_No.DisplayMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_Exit_Coil_No.ValueMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_Exit_Coil_No.DataSource = dtOut_Mat_No;
        }

        /// <summary>
        /// 入口卷號清單
        /// </summary>
        private void Fun_SelectIn_Mat_No()
        {
            string strSql = SqlFactory.Frm_3_1_SelectComboBoxItems(nameof(PDOEntity.TBL_PDO.In_Coil_ID));
            DataTable dtIn_Mat_No = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "入口卷号清单", "3-1");

            if (dtIn_Mat_No.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message("无入口卷清单");
                return;
            }

            Cob_Entry_Coil_No.DisplayMember = nameof(PDOEntity.TBL_PDO.In_Coil_ID);
            Cob_Entry_Coil_No.ValueMember = nameof(PDOEntity.TBL_PDO.In_Coil_ID);
            Cob_Entry_Coil_No.DataSource = dtIn_Mat_No;
        }

        /// <summary>
        /// 鋼種清單
        /// </summary>
        private void Fun_SelectSteelGrade()
        {
            string strSql = SqlFactory.Frm_3_1_SelectComboBoxItems(nameof(PDIEntity.TBL_PDI.St_No), true);
            DataTable dtSt_No = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"钢种清单","3-1");

            if (dtSt_No.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message("无钢种清单");
                return;
            }

            Cob_SteelGrade.DisplayMember = nameof(PDIEntity.TBL_PDI.St_No);
            Cob_SteelGrade.ValueMember = nameof(PDIEntity.TBL_PDI.St_No);
            Cob_SteelGrade.DataSource = dtSt_No;
        }

        /// <summary>
        /// 客戶清單
        /// </summary>
        private void Fun_SelectCustomer()
        {
            string strSql = SqlFactory.Frm_3_1_SelectComboBoxItems(nameof(PDIEntity.TBL_PDI.Order_Cust_Code), true);
            DataTable dtOrder_Cust_Code = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"客户清单","3-1");

            if (dtOrder_Cust_Code.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message("无客户清单");
                return;
            }

            Cob_Customer.DisplayMember = nameof(PDIEntity.TBL_PDI.Order_Cust_Code);
            Cob_Customer.ValueMember = nameof(PDIEntity.TBL_PDI.Order_Cust_Code);
            Cob_Customer.DataSource = dtOrder_Cust_Code;
        }

        /// <summary>
        /// 牌號清單
        /// </summary>
        private void Fun_SelectSg_Sign()
        {
            string strSql = SqlFactory.Frm_3_1_SelectComboBoxItems(nameof(PDIEntity.TBL_PDI.Sg_Sign), true);
            DataTable dtSg_Sign = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"牌号清单","3-1");

            if (dtSg_Sign.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message("无牌号清单");
                return;
            }

            Cob_Sg.DisplayMember = nameof(PDIEntity.TBL_PDI.Sg_Sign);
            Cob_Sg.ValueMember = nameof(PDIEntity.TBL_PDI.Sg_Sign);
            Cob_Sg.DataSource = dtSg_Sign;
        }

        #endregion

        #region DGV
        /// <summary>
        /// PDO DGV
        /// </summary>
        private void Fun_DataGridViewPDO()
        {
            string strSql = SqlFactory.Frm_3_1_SelectPDO_DB_PDO();
            dt = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"PDO","3-1");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Info, dt);
            DGVColumnsHandler.Instance.Frm_3_1PDOColumns(Dgv_Info);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Info);

            Dgv_Info.ClearSelection();

            Fun_ChangeUpdateFlagColor();
        }
        
        /// <summary>
        /// DataGridView是否上传MMS变色处理及上传标记中文化
        /// </summary>
        private void Fun_ChangeUpdateFlagColor()
        {
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                if (Dgv_Info.Rows[RowIndex].Cells[0].Value.ToString().Equals("0"))
                {
                    Dgv_Info.Rows[RowIndex].Cells[0].Value = "未上传";
                    Dgv_Info.Rows[RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                }
                else if (Dgv_Info.Rows[RowIndex].Cells[0].Value.ToString().Equals("1"))
                {
                    Dgv_Info.Rows[RowIndex].Cells[0].Value = "已上传";
                    Dgv_Info.Rows[RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
                else 
                {
                    Dgv_Info.Rows[RowIndex].Cells[0].Value = "Null";
                    Dgv_Info.Rows[RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                }
                Dgv_Info.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }
        #endregion

        #region 入口/出口钢卷号條件單選

        private void Ckb_Exit_Coil_No_CheckedChanged(object sender, EventArgs e)
        {
            Chk_Exit_Coil_No.Checked = true;
            Chk_Entry_Coil_No.Checked = false;
        }

        private void Ckb_Entry_Coil_No_CheckedChanged(object sender, EventArgs e)
        {
            Chk_Exit_Coil_No.Checked = false;
            Chk_Entry_Coil_No.Checked = true;
        }

        #endregion

        /// <summary>
        /// 查询Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            //if (Chk_Finish_Time.Checked == false && Chk_Exit_Coil_No.Checked == false && Chk_Entry_Coil_No.Checked == false && Chk_Team.Checked == false &&
            //    Chk_Out_Width.Checked == false && ckb_SteelGrade.Checked == false && Chk_Out_Thick.Checked == false && ckb_Customer.Checked == false &&
            //    ckb_Sg.Checked == false && ckb_ProcessCode.Checked == false)
            //    return;
            if (Chk_Finish_Time.Checked)
            {
                if (Dtp_Start_Time.DateTimeRangeIsFail(Dtp_Finish_Time.Value))
                {
                    EventLogHandler.Instance.EventPush_Message($"日期区间起讫时间不正确，请重新确认");
                    return;
                }
            }

            string strSql;// = SqlFactory.Frm_3_1_SelectPDO_DB_PDO();
            
            strSql = Fun_ConditionalSentence();

            dt = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"PDO条件","3-1");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Info, dt);
            DGVColumnsHandler.Instance.Frm_3_1PDOColumns(Dgv_Info);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Info);

            Fun_ChangeUpdateFlagColor();

            Lbl_CoilCount.Text = dt.Rows.Count.ToString();
        }
        
        /// <summary>
        /// 查詢條件
        /// </summary>
        private string Fun_ConditionalSentence()
        {
            string strSql;
            strSql = $@"Select * From [{nameof(PDOEntity.TBL_PDO)}] pdo
                                Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] <> '' 
                                  AND [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] is not null ";//<> null";
            #region -- 時間範圍 --
            if (Chk_Finish_Time.Checked)
            {
                strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.Finish_Time)}] between '{Dtp_Start_Time.Value:yyyy-MM-dd HH}:00:00' and '{Dtp_Finish_Time.Value:yyyy-MM-dd HH}:59:59' ";
            }
            #endregion

            #region -- 出口卷號 --
            if (Chk_Exit_Coil_No.Checked)
            {
                strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] ='{Cob_Exit_Coil_No.Text}' ";
            }
            #endregion

            #region -- 入口卷號 --
            if (Chk_Entry_Coil_No.Checked)
            {
                strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] = '{Cob_Entry_Coil_No.Text}' ";
            }
            #endregion

            #region -- 班別 --
            if (Chk_Team.Checked)
            {                
                strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.Team)}] = '{Cob_Team.SelectedValue}' ";
            }
            #endregion

            #region -- 出口寬度 --
            if (Chk_Out_Width.Checked)
            {
                strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}] between '{Txt_Out_Width_Min.Text}' and '{Txt_Out_Width_Max.Text}' ";
            }
            #endregion

            #region -- 出口厚度 --
            if (Chk_Out_Thick.Checked)
            {
                strSql += $" and pdo.[{nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)}] between '{Txt_Out_Thick_Min.Text}' and '{Txt_Out_Thick_Max.Text}' ";
            }
            #endregion

            #region -- 鋼種 --
            //if (ckb_SteelGrade.Checked)
            //{
            //    strSql += $" and pdi.[{nameof(TBL_PDI.St_No)}] = '{cbo_SteelGrade.SelectedValue}'";
            //}
            #endregion

            #region -- 客戶代碼 --
            //if (ckb_Customer.Checked)
            //{
            //    strSql += $" and pdi.[{nameof(TBL_PDI.Order_Cust_Code)}]";
            //}
            #endregion

            #region -- 牌號 --
            //if (ckb_Sg.Checked)
            //{
            //    strSql += $" and pdi.[{nameof(TBL_PDI.Sg_Sign)}] = '{cbo_Sg.SelectedValue}'";
            //}
            #endregion

            #region -- ProcessCode --
            //if (ckb_ProcessCode.Checked)
            //{
            //    strSql += $" and pdi.[{nameof(TBL_PDI.Process_Code)}] = '{cbo_ProcessCode.SelectedText}'";
            //}
            #endregion

            #region -- 上傳MMS --
            if (Chk_SendMMS.Checked)
            {
                if (Rdb_SendMMS.Checked)
                {
                    strSql += $"and pdo.[{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] = '1'";
                }
                else if (Rdb_UnSendMMS.Checked)
                {
                    strSql += $"and pdo.[{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] <> '1'";
                }
            }
            #endregion
            return strSql;
        }
       
        private void Btn_PdiDet_Click(object sender, EventArgs e)
        {
            if (Dgv_Info.DgvIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("无钢卷清单可开启", "开启详细资料",null, 0);
                return;
            }
            if (Dgv_Info.CurrentIsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"请选择要显示的钢卷");
                PublicComm.ClientLog.Info($"未選擇要顯示的鋼卷");
                return;
            }
           
            Fun_PDODetailFormOpen(Dgv_Info.CurrentRow);
        }
        private void Fun_PDODetailFormOpen(DataGridViewRow dgvrData)
        {
            if (dgvrData != null) 
            {
                string strOut_Mat_No = dgvrData.Cells[nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].Value.ToString();//1
                //DateTime dTime = DateTime.Parse(dgvrData.Cells["FinishTime"].Value.ToString());
                string strFinishTime = dgvrData.Cells[nameof(PDOEntity.TBL_PDO.Finish_Time)].FormattedValue.ToString();
                //string strFinishTime = dTime.ToString("yyyy-MM-dd HH:mm:ss");//2
                string strIn_Coil_ID = dgvrData.Cells[nameof(PDOEntity.TBL_PDO.In_Coil_ID)].Value.ToString();//3
                string strPlan_No = dgvrData.Cells[nameof(PDOEntity.TBL_PDO.Plan_No)].Value.ToString();//4

                PublicForms.Main.tsMenuItem_3_2.PerformClick();
                //Frm_0_0_Main FatherForm = Parent.Parent as Frm_0_0_Main;
                //FatherForm.tsMenuItem_3_2.PerformClick();

                PublicForms.PDODetail.Txt_Search_Out_Coil_ID.Text = strOut_Mat_No;//Cbo_Matcoilid

                PublicForms.PDODetail.Fun_SelectCoilPDO(strOut_Mat_No, strPlan_No, null,strIn_Coil_ID);

                EventLogHandler.Instance.LogInfo("3-1", $"产品钢卷资讯", $"产品钢卷资讯 信息:开启钢卷编号:{strOut_Mat_No}PDO详细资料並跳转至3-2产品钢卷详细资料");

                PublicComm.ClientLog.Info($"開啟[{strOut_Mat_No}]PDO詳細資料並跳轉至3-2產品鋼卷詳細資料");

            }

        }

        private void Chk_SendMMS_CheckedChanged(object sender, EventArgs e)
        {
            bool Check = Chk_SendMMS.Checked;
            Rdb_SendMMS.Enabled = Rdb_UnSendMMS.Enabled = Check;
        }

        /// <summary>
        /// 出口钢卷号清单Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Exit_Coil_No_Click(object sender, EventArgs e)
        {
            //Fun_SelectOut_Mat_No();
        }

        /// <summary>
        /// 入口钢卷号清单Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cob_Entry_Coil_No_Click(object sender, EventArgs e)
        {
            //Fun_SelectIn_Mat_No();
        }

        private void Frm_3_1_ProdCoilList_VisibleChanged(object sender, EventArgs e)
        {
            Fun_SelectOut_Mat_No();
            Fun_SelectIn_Mat_No();
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
            FontStyle fs = ((CheckBox)sender).Font.Style;
            FontFamily ffm = ((CheckBox)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((CheckBox)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((CheckBox)sender).Font = new Font(ffm, (float)10, fs);
        }       
        private void Fun_LanguageIsEn_Font14_12(object sender, EventArgs e)
        {
            FontStyle fs = ((CheckBox)sender).Font.Style;
            FontFamily ffm = ((CheckBox)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((CheckBox)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((CheckBox)sender).Font = new Font(ffm, (float)12, fs);
        }
        #endregion Fun_LanguageIsEn_Font End

    }
}

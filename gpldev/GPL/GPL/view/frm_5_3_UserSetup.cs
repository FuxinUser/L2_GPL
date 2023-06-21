using DBService.Repository.AuthorityData;
using DBService.Repository.AuthorityData_Frame;
using GPLManager.Util;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static GPLManager.DataBaseTableFactory;

namespace GPLManager
{
    public partial class Frm_5_3_UserSetup : Form
    {
        //Point FristPoint_Lable = new Point(27, 215);
        //Point FristPoint_TextBox = new Point(167, 214);
        //Point ChangePoint_Lable = new Point(27, 287);
        //Point ChangePoint_TextBox = new Point(167, 286);
        //Point TeamPoint_Lable = new Point(27, 431);
        //Point TeamPoint_ComboBox = new Point(167, 431);
                
        DataTable dtGetUser;
      
        string strStatus = "Read";
        bool bolEditStatus; 
        bool bolHaveFrameData;

        Control[] CtrControlArray;

        DataTable dtSelectOne;//User Data //只取一笔 提供栏位填值 存值
        DataTable dtBeforeEdit;//User Data //编辑前 备份

        //語系
        private LanguageHandler LanguageHand;
        public Frm_5_3_UserSetup()
        {
            InitializeComponent();
        }

        private void Frm_5_3_UserSetup_Load(object sender, System.EventArgs e)
        {
            if (PublicForms.UserSetup == null) PublicForms.UserSetup = this;

            Control[] Frm_5_3_Control = new Control[] {
                Btn_Delete,
                Btn_Edit,
                Btn_New,
                Btn_Save
            };
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_5_3_Control, UserSetupHandler.Instance.Frm_5_3);
            //// 密碼位置
            //Lbl_Password_Title.Location = FristPoint_Lable;
            //Txt_Password.Location = FristPoint_TextBox;
            ////班別欄位移動
            //Lbl_Team_Title.Location = ChangePoint_Lable;
            //cboTeam.Location = ChangePoint_TextBox;
            //帳號清單
            UserSetupHandler.Instance.UserID_List(Cob_UserID_Search);
            //權限等級
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Authority_Class, Cob_Authority_Class);
            //班別
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Team, Cob_Team);
            
            Cob_UserID_Search.SelectedIndex = -1;
            Cob_Authority_Class.SelectedIndex = -1;
            Cob_Team.SelectedIndex = -1;
            Lbl_Updated.Text = "";
            CtrControlArray = new Control[] { Grb_User };
            //人員 欄位清空
            ReadOnlyHandler.Instance.ClearControl(CtrControlArray);
            //人員 欄位ReadOnly
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_User, true);
            //Status = "Read",只顯示的欄位
            Fun_StatusToShowColumns("Read");
            //權限 RadioButton.Enabled = false;
            Fun_AuthorityEnabled(false);

            //一開始載入,沒有查詢資料,只提供新增
            if (UserSetupHandler.Instance.Frm_5_3.Equals("W"))
            { 
                //新增
                Fun_SetBottonEnabled(Btn_New, true);
                //修改
                Fun_SetBottonEnabled(Btn_Edit, false);
                //刪除
                Fun_SetBottonEnabled(Btn_Delete, false);
            }
            //先查一次空白,取得 dtGetUser.Clone()
            string strSql = SqlFactory.Frm_5_3_SelectUser("");
            dtSelectOne = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL, "账号资讯查询","5-3");

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        /// <summary>
        /// 依狀態顯示欄位
        /// </summary>
        /// <param name="strStatus"></param>
        private void Fun_StatusToShowColumns(string strStatus)
        {
            Control[] ctlArr_Lbl;
            Control[] ctlArr_Txt;

            switch (strStatus)
            {
                case "Read":
                    ctlArr_Lbl = new Control[] { Lbl_UserID_Title, Lbl_Department_Title, Lbl_Team_Title, Lbl_Authority_Class_Title, Lbl_Password_Title };
                    ctlArr_Txt = new Control[] { Txt_UserID, Txt_Department, Cob_Team, Cob_Authority_Class, Txt_Password };
                    break;

                case "New":
                    ctlArr_Lbl = new Control[] { Lbl_UserID_Title, Lbl_Department_Title, Lbl_Team_Title, Lbl_Authority_Class_Title, Lbl_Password_Title, Lbl_Password_Chk_Title };
                    ctlArr_Txt = new Control[] { Txt_UserID, Txt_Department, Cob_Team, Cob_Authority_Class, Txt_Password, Txt_Password_Chk };
                    break;

                case "Edit":
                    ctlArr_Lbl = new Control[] { Lbl_UserID_Title, Lbl_Department_Title, Lbl_Team_Title, Lbl_Authority_Class_Title, Lbl_Password_Old_Title, Lbl_Password_Title, Lbl_Password_Chk_Title };
                    ctlArr_Txt = new Control[] { Txt_UserID, Txt_Department, Cob_Team, Cob_Authority_Class, Txt_Password_Old, Txt_Password, Txt_Password_Chk };
                    break;

                default:
                    ctlArr_Lbl = new Control[] { Lbl_UserID_Title, Lbl_Department_Title, Lbl_Team_Title, Lbl_Password_Title };
                    ctlArr_Txt = new Control[] { Txt_UserID, Txt_Department, Cob_Team, Txt_Password };
                    break;
            }
            Fun_LocationShow(ctlArr_Lbl, ctlArr_Txt);
        }

        private void Fun_LocationShow(Control[] ctlArr_Lbl, Control[] ctlArr_Txt)
        {
            #region Before

            //Lbl_name.Location = new Point(50, 50);
            //Txt_name.Location = new Point(190, 50);
            //Lbl_password.Location = new Point(50, 91);
            //Txt_password.Location = new Point(190, 91);
            //Lbl_description.Location = new Point(50, 132);
            //Txt_description.Location = new Point(190, 132);

            //Lbl_name.Visible = true;
            //Txt_name.Visible = true;
            //Lbl_password.Visible = true;
            //Txt_password.Visible = true;
            //Lbl_description.Visible = true;
            //Txt_description.Visible = true;

            //Control[] ctlArr_L = new Control[] { Lbl_name , Txt_name ,
            //                                   Lbl_password , Txt_password ,
            //                                   Lbl_description , Txt_description };

            #endregion
            Fun_NotShow();
            int intLbl_X = 27;
            int intLbl_Y = 35;
            int intTxt_X = 167;
            int intTxt_Y = 35;

            foreach (Control ctl in ctlArr_Lbl)
            {
                ctl.Location = new Point(intLbl_X, intLbl_Y);
                ctl.Visible = true;
                //intLbl_X += 43;
                intLbl_Y += 43;
            }

            foreach (Control ctl in ctlArr_Txt)
            {
                ctl.Location = new Point(intTxt_X, intTxt_Y);
                ctl.Visible = true;
                //intTxt_X += 43;
                intTxt_Y += 43;
            }

            if (strStatus == "Edit")
            {
                /*"Edit"*/
                int intBtn_X = 0;
                int intBtn_Y = 0;

                intBtn_X = Lbl_Password_Title.Location.X + 109;
                intBtn_Y = Lbl_Password_Title.Location.Y + 1;

                Btn_EditPassWord.Location = new Point(intBtn_X, intBtn_Y);
            }

        }
        private void Fun_NotShow()
        {
            Control[] ctlArr_Lbl = new Control[] { /*Lbl_name,*/ Lbl_Password_Old_Title, /*Lbl_password,*/ Lbl_Password_Chk_Title/*, Lbl_description*/ };
            Control[] ctlArr_Txt = new Control[] {/* Txt_name,*/ Txt_Password_Old, /*Txt_password,*/ Txt_Password_Chk/*, Txt_description*/ };

            foreach (Control ctl in ctlArr_Lbl)
                ctl.Visible = false;

            foreach (Control ctl in ctlArr_Txt)
                ctl.Visible = false;

        }

       

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, System.EventArgs e)
        {
            //狀態為 New(新增) & Edit(編輯) 時，不執行後續程式
            if (strStatus == "New" || strStatus == "Edit")
            {
                /*"New"*/  /*"Edit"*/
                DialogHandler.Instance.Fun_DialogShowOk("資料編輯中，請先存檔或取消！", "账号资讯查询",null, 0);
                return;
            }
            Fun_SelectUser();
            //權限 RadioButton.Enabled = false;
            Fun_AuthorityEnabled(false);
        }

        private void Fun_SelectUser()
        {
            string strSql = SqlFactory.Frm_5_3_SelectUser(Cob_UserID_Search.Text.Trim());
            dtGetUser = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"查询人员","5-3");

            if (dtGetUser != null)
            {
                //人員 沒資料
                dtSelectOne = dtGetUser.Clone();

                if (dtGetUser.Rows.Count <= 0)
                {
                    DialogHandler.Instance.Fun_DialogShowOk("查无资料", "账号资讯查询", null, 0);

                    return;
                }
            }

            #region 检查有没有建权限
            string strSql_Frame = SqlFactory.Frm_5_3_Check_AuthorityData_Frame(Cob_UserID_Search.Text);
            DataTable dtFrame = Data_Access.Fun_SelectDate(strSql_Frame, GlobalVariableHandler.Instance.strConn_GPL, "人员权限资讯查询", "5-3");
            if (dtFrame == null || dtFrame.Rows.Count <= 0)
            {
                DialogHandler.Instance.Fun_DialogShowOk("查无人员权限资料，将给予预设值显示！", "人员权限资讯查询", null, 0);
                bolHaveFrameData = false;

                #region  无人员权限资料 给予预设
                rdoN1_1.Checked = true;
                rdoN1_2.Checked = true;
                rdoN1_3.Checked = true;
                rdoN2_1.Checked = true;
                rdoN2_2.Checked = true;
                rdoN3_1.Checked = true;
                rdoN3_2.Checked = true;
                rdoN3_3.Checked = true;
                rdoN3_3.Checked = true;
                rdoN4_1.Checked = true;
                rdoN4_2.Checked = true;
                rdoN4_3.Checked = true;
                rdoN5_1.Checked = true;
                rdoN5_2.Checked = true;
                rdoN5_3.Checked = true;
                rdoN5_4.Checked = true;
                rdoN5_5.Checked = true;
                rdoN5_6.Checked = true;
                #endregion
            }
            else
            {
                bolHaveFrameData = true;
            }
            #endregion

            //人員 有資料
            dtSelectOne = dtGetUser.Copy();

            //人員資料 畫面權限
            Fun_SetUserData(dtSelectOne);

            if (UserSetupHandler.Instance.Frm_5_3.Equals("W"))
            {
                if (dtSelectOne.IsNull())
                {
                    //修改
                    Fun_SetBottonEnabled(Btn_Edit, false);
                    //刪除
                    Fun_SetBottonEnabled(Btn_Delete, false);
                }
                else
                {
                    //修改
                    Fun_SetBottonEnabled(Btn_Edit, true);
                    //刪除
                    Fun_SetBottonEnabled(Btn_Delete, true);
                }
                //新增
                Fun_SetBottonEnabled(Btn_New, true);
            }
                

        }

        private void Fun_SetUserData(DataTable dt)
        {
            //資料填入
            //帳號
            Txt_UserID.Text = dt.Rows[0][nameof(AuthorityDataEntity.TBL_AuthorityData.User_ID)].ToString().Trim() ?? string.Empty;
            //密碼
            Txt_Password.Text = dt.Rows[0][nameof(AuthorityDataEntity.TBL_AuthorityData.Password)].ToString().Trim() ?? string.Empty;
            //舊密碼
            Txt_Password_Old.Text = "";// Txt_Password.Text;
            Txt_Department.Text = dt.Rows[0][nameof(TBL_AuthorityData.Department)].ToString() ?? string.Empty;
            //權限等級
            Cob_Authority_Class.SelectedValue = dt.Rows[0][nameof(AuthorityDataEntity.TBL_AuthorityData.Authority_Class)].ToString().Trim() ?? string.Empty;
            //班別
            Cob_Team.SelectedValue = dt.Rows[0][nameof(AuthorityDataEntity.TBL_AuthorityData.Team)].ToString().Trim() ?? string.Empty;
            //異動時間
            Lbl_Updated.Text = dtGetUser.Rows[0][nameof(AuthorityDataEntity.TBL_AuthorityData.Create_DateTime)].ToString() ?? string.Empty;
            
            //畫面權限
            for (int Index = 0; Index < dtGetUser.Rows.Count; Index++)
            {
                Fun_FrameSetup(dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_ID)].ToString().Trim(), Index);
            }
        }
        /// <summary>
        /// 畫面權限設定-查詢結果
        /// </summary>
        /// <param name="Frame"></param>
        /// <param name="Index"></param>
        private void Fun_FrameSetup(string Frame, int Index)
        {
            switch (Frame)
            {
                //case "0-2":
                //    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                //        rdoN0_2.Checked = true;
                //    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                //        rdoR0_2.Checked = true;
                //    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                //        rdoW0_2.Checked = true;
                //    else
                //        return;
                //    break;
                case "1-1":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN1_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR1_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW1_1.Checked = true;
                    else
                        rdoN1_1.Checked = true;
                    break;
                case "1-2":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN1_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR1_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW1_2.Checked = true;
                    else
                        rdoN1_2.Checked = true;
                    break;
                case "1-3":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN1_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR1_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW1_3.Checked = true;
                    else
                        rdoN1_3.Checked = true;
                    break;
                case "2-1":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN2_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR2_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW2_1.Checked = true;
                    else
                        rdoN2_1.Checked = true;
                    break;
                case "2-2":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN2_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR2_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW2_2.Checked = true;
                    else
                        rdoN2_2.Checked = true;
                    break;
                case "3-1":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN3_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR3_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW3_1.Checked = true;
                    else
                        rdoN3_1.Checked = true;
                    break;
                case "3-2":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN3_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR3_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW3_2.Checked = true;
                    else
                        rdoN3_2.Checked = true;
                    break;
                case "3-3":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN3_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR3_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW3_3.Checked = true;
                    else
                        rdoN3_3.Checked = true;
                    break;
                //case "3-4":
                //    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameModel.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                //        radioButton1.Checked = true;
                //    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameModel.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                //        radioButton2.Checked = true;
                //    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameModel.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                //        radioButton3.Checked = true;
                //    else
                //        return;
                //    break;
                case "4-1":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN4_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR4_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW4_1.Checked = true;
                    else
                        rdoN4_1.Checked = true;
                    break;
                case "4-2":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN4_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR4_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW4_2.Checked = true;
                    else
                        rdoN4_2.Checked = true;
                    break;
                case "4-3":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN4_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR4_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW4_3.Checked = true;
                    else
                        rdoN4_3.Checked = true;
                    break;
                case "4-4":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN4_4.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR4_4.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW4_4.Checked = true;
                    else
                        rdoN4_4.Checked = true;
                    break;
                case "4-5":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN4_5.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR4_5.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW4_5.Checked = true;
                    else
                        rdoN4_5.Checked = true;
                    break;
                case "5-1":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN5_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR5_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW5_1.Checked = true;
                    else
                        rdoN5_1.Checked = true;
                    break;
                case "5-2":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN5_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR5_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW5_2.Checked = true;
                    else
                        rdoN5_2.Checked = true;
                    break;
                case "5-3":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN5_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR5_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW5_3.Checked = true;
                    else
                        rdoN5_3.Checked = true;
                    break;
                case "5-4":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN5_4.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR5_4.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW5_4.Checked = true;
                    else
                        rdoN5_4.Checked = true;
                    break;
                case "5-5":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN5_5.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR5_5.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW5_5.Checked = true;
                    else
                        rdoN5_5.Checked = true;
                    break;
                case "5-6":
                    if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN5_6.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR5_6.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(AuthorityDataFrameEntity.TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW5_6.Checked = true;
                    else
                        rdoN5_6.Checked = true; //return;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_New_Click(object sender, EventArgs e)
        {
            if (bolEditStatus) return;
            //先判断 原先有没有显示资料,有就Copy备份 , 没有就Clone栏位就好
            if (!dtSelectOne.IsNull())
            {
                dtBeforeEdit = dtSelectOne.Copy();
                //dtBeforeEdit_A = dtSelectOne_A.Copy();
            }
            else
            {
                dtBeforeEdit = dtSelectOne.Clone();
                //dtBeforeEdit_A = dtSelectOne_A.Clone();
            }

            //状态:New 顯示基本欄位 +  密码确认栏位
            strStatus = "New";
            bolEditStatus = true;
            Fun_StatusToShowColumns(strStatus);         
            //欄位 权限 启用
            Fun_AuthorityEnabled(true);

            //人員欄位 清空            
            ReadOnlyHandler.Instance.ClearControl(CtrControlArray);
            Lbl_Updated.Text = "";
            Cob_Team.SelectedIndex = -1;
            Cob_Authority_Class.SelectedIndex = -1;

            //人員 欄位ReadOnly
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_User, false);

            //权限 全N
            Fun_AuthorityNewData();

            Btn_Save.Visible = true; //儲存
            Btn_Cancel.Visible = true; //取消
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);
          
            //Txt_UserID.Text = string.Empty;
            //Txt_UserID.ReadOnly = false;
            //Txt_Password.Text = string.Empty;
            //Txt_Password.ReadOnly = false;
            //Cob_Authority_Class.SelectedIndex = 0;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            if (bolEditStatus) return;
            //先判断 原先有没有显示资料,有就Copy备份 , 没有就Clone栏位就好
            if (!dtSelectOne.IsNull())
                dtBeforeEdit = dtSelectOne.Copy();
            else
                dtBeforeEdit = dtSelectOne.Clone();

            //状态:Edit 顯示基本欄位 + 旧密码 & 密码确认栏位
            strStatus = "Edit";
            bolEditStatus = true;
            Fun_StatusToShowColumns(strStatus);

            //欄位 人员 不唯讀  
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_User, false);
            //欄位 权限 启用
            Fun_AuthorityEnabled(true);

            //原  密碼 沒異動時 唯讀
            Txt_Password.Enabled = false;
           // Txt_Password.BackColor = Color.Gainsboro;
            Txt_Password_Old.Enabled = false;
           // Txt_Password_Old.BackColor = Color.Gainsboro;
            Txt_Password_Chk.Enabled = false;
           // Txt_Password_Chk.BackColor = Color.Gainsboro;

            //編輯狀態-Key不可修改
            Txt_UserID.Enabled = false;
           // Txt_UserID.BackColor = Color.Gainsboro;

            //新增
            Fun_SetBottonEnabled(Btn_New, false);
            //編輯
            Fun_SetBottonEnabled(Btn_Edit, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);

            Btn_Save.Visible = true; //儲存
            Btn_Cancel.Visible = true; //取消
            Btn_EditPassWord.Visible = true;

            ////清空密碼
            //Txt_Password.Text = string.Empty;
           
            //////密碼欄位移動
            ////Lbl_Password_Title.Location = ChangePoint_Lable;
            ////Txt_Password.Location = ChangePoint_TextBox;
            //////班別欄位移動
            ////Lbl_Team_Title.Location = TeamPoint_Lable;
            ////cboTeam.Location = TeamPoint_ComboBox;

            //Lbl_Password_Old_Title.Visible = true;
            //Txt_Password_Old.Visible = true;
            //Lbl_Password_Chk_Title.Visible = true;
            //Txt_Password_Chk.Visible = true;
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if (strStatus == "Read") { return; }
            if (!bolEditStatus) return;

            string strPsd_before = "";
            if (!dtBeforeEdit.IsNull())
            {
                strPsd_before = dtBeforeEdit.Rows[0]["Password"].ToString().Trim();
            }

            string strSql = string.Empty;

            //新增
            if (Btn_New.Enabled)
            {
                //检查账号
                if (string.IsNullOrEmpty(Txt_UserID.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk("账号不可为空白！", "人员账号檢查", null, 0);
                    Txt_UserID.Focus();
                    return;
                }
                else
                {
                    //检查账号是否已存在
                    string strSqlOn = SqlFactory.Frm_5_3_Check_User();//name, password, description, Updated, UpdatedProc 

                    DataTable dtCheckUser = Data_Access.Fun_SelectDate(strSqlOn, GlobalVariableHandler.Instance.strConn_GPL, "账号檢查", "5-3");
                    if (!dtCheckUser.IsNull())
                    {
                        DialogHandler.Instance.Fun_DialogShowOk("账号已重复！", "人员账号檢查", null, 0);
                        Txt_UserID.Focus();
                        return;
                    }
                }

                //帳號密碼
                strSql = SqlFactory.Frm_5_3_InsertUser();
                Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"新增{Txt_UserID.Text}账号密码", "5-3");

                //畫面權限
                strSql = SqlFactory.Frm_5_3_InsertFrame();
                Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"新增{Txt_UserID.Text}账号权限", "5-3");

                EventLogHandler.Instance.LogInfo("5-3", $"使用者:{PublicForms.Main.lblLoginUser.Text}人员管理", $"新增{Txt_UserID.Text}账号及权限");
                PublicComm.ClientLog.Info($"新增{Txt_UserID.Text}帳號及權限");
            }
            //修改
            else if (Btn_Edit.Enabled)
            {
                //密碼如果有變更 
                if (strPsd_before != Txt_Password.Text.Trim())
                {
                    //检查 舊密码
                    if (string.IsNullOrEmpty(Txt_Password_Old.Text.Trim()))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk("旧密码请勿为空白！", "人员密码檢查", null,0);
                        Txt_Password_Old.Focus();
                        return;
                    }
                    else
                    {
                        ////检查 舊密码 是否正確
                        //string strSqlOn = $"SELECT * FROM APLA_OperatorInfo WHERE   User_ID = '{Txt_Name.Text.Trim()}' AND Password = N'{Txt_Password_Old.Text.Trim()}' ";//name, password, description, Updated, UpdatedProc 

                        //object obj = PublicComm.portal.DbHand.Fun_GetObject(strSqlOn, PublicSystem.ConnectionString);
                        if (strPsd_before != Txt_Password_Old.Text)
                        {
                            DialogHandler.Instance.Fun_DialogShowOk("旧密码不正確，請重新輸入！", "人员密码檢查", null, 0);
                            Txt_Password_Old.Focus();
                            return;
                        }
                        else
                        {
                            //舊密碼正確 
                        }
                    }

                    if (!Fun_ChackPassword())
                    {
                        return;
                    }
                }

                //帳號密碼
                strSql = SqlFactory.Frm_5_3_UpdateUser();
                Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"修改{Txt_UserID.Text}账号密码", "5-3"); //Fun_InsertUpdateDataBase(strSql);

                if (!bolHaveFrameData)
                {
                    //畫面權限
                    strSql = SqlFactory.Frm_5_3_InsertFrame();
                    Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"新增{Txt_UserID.Text}账号权限", "5-3");
                }
                else
                {
                    //畫面權限
                    UpdateFrameSetup();
                }
                ////新密碼與舊密碼不同且與密碼確認相同
                //if (Txt_Password.Text.Equals(Txt_Password_Chk.Text) && !Txt_Password.Text.Equals(Txt_Password_Old.Text))
                //{
                //    //帳號密碼
                //    strSql = SqlFactory.Frm_5_3_UpdateUser();
                //    Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"修改{Txt_UserID.Text}账号密码", "5-3");

                //    //畫面權限
                //    UpdateFrameSetup();

                //    EventLogHandler.Instance.LogInfo("5-3", $"使用者:{PublicForms.Main.lblLoginUser.Text}人员管理", $"修改{Txt_UserID.Text}账号及权限");
                //    PublicComm.ClientLog.Info($"修改{Txt_UserID.Text}帳號及權限");
                //}
                //else if (Txt_Password.Text.Trim().Equals(Txt_Password_Old.Text.Trim()))
                //{
                //    EventLogHandler.Instance.EventPush_Message($"与旧密码相同，请重新输入!");
                //    EventLogHandler.Instance.LogInfo("5-3", $"使用者:{PublicForms.Main.lblLoginUser.Text}人员管理", $"与旧密码相同，请重新输入!");
                //    PublicComm.ClientLog.Info($"修改{Txt_UserID.Text}帳號密碼，新密碼與舊密碼相同，請重新輸入");
                //}
                //else if (!Txt_Password.Text.Trim().Equals(Txt_Password_Chk.Text.Trim()))
                //{
                //    EventLogHandler.Instance.EventPush_Message($"密码与密码确认不一致，请重新输入!");
                //    EventLogHandler.Instance.LogInfo("5-3", $"使用者:{PublicForms.Main.lblLoginUser.Text}人员管理", $"与旧密码相同，请重新输入!");
                //    PublicComm.ClientLog.Info($"修改{Txt_UserID.Text}帳號密碼，密碼與密碼確認不一致，請重新輸入");
                //}
            }
            //狀態:Read 只顯示基本欄位
            strStatus = "Read";
            bolEditStatus = false;
            Fun_StatusToShowColumns(strStatus);
            //人員 欄位ReadOnly
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_User, true);
            //權限 RadioButton.Enabled = false;
            Fun_AuthorityEnabled(bolEditStatus);

            //帳號清單
            UserSetupHandler.Instance.UserID_List(Cob_UserID_Search);

            Cob_UserID_Search.SelectedValue = Txt_UserID.Text ?? string.Empty;
            Fun_SelectUser();

            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //修改
            Fun_SetBottonEnabled(Btn_New, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);
            Btn_Save.Visible = false; //儲存
            Btn_Cancel.Visible = false; //取消
            Btn_EditPassWord.Visible = false;
            //Txt_UserID.ReadOnly = true;
            //Txt_Password.ReadOnly = true;
            ////密碼欄位移動
            //Lbl_Password_Title.Location = FristPoint_Lable;
            //Txt_Password.Location = FristPoint_TextBox;
            ////班別欄位移動
            //Lbl_Team_Title.Location = ChangePoint_Lable;
            //cboTeam.Location = ChangePoint_TextBox;

            Txt_Password_Old.Text = Txt_Password.Text;

            Lbl_Password_Old_Title.Visible = false;
            Txt_Password_Old.Visible = false;
            Lbl_Password_Chk_Title.Visible = false;
            Txt_Password_Chk.Visible = false;
        }
        private void InsertUpdate(string strSql)
        {
            try
            {
                Data_Access.GetInstance().ExecuteNonQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL);
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text} 新增/修改人员设定资料失败");
                EventLogHandler.Instance.LogDebug("5-3", $"使用者:{PublicForms.Main.lblLoginUser.Text} 新增/修改人员设定资料失败", $"资料有错误:{ex}");
                PublicComm.ClientLog.Debug($"使用者:{PublicForms.Main.lblLoginUser.Text} 新增/修改人员设定资料失败資料有錯誤:{ex}");
                DialogHandler.Instance.Fun_DialogShowOk("人员设定新增/修改资料库失败", "人员设定新增修改资料库", null,3);
                return;
            }
        }

        private bool InsertUpdate(string strFrame_ID, string strSql)
        {
            bool bolSaveStatus;
            try
            {
                bolSaveStatus = Data_Access.GetInstance().ExecuteNonQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL);
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"使用者:{PublicForms.Main.lblLoginUser.Text} 新增/修改人员设定资料失败");
                EventLogHandler.Instance.LogDebug("5-3", $"使用者:{PublicForms.Main.lblLoginUser.Text} 新增/修改人员设定资料失败", $"资料有错误:{ex}");
                PublicComm.ClientLog.Debug($"使用者:{PublicForms.Main.lblLoginUser.Text} 新增/修改人员设定资料失败資料有錯誤:{ex}");
                DialogHandler.Instance.Fun_DialogShowOk("人员设定新增/修改资料库失败", "人员设定新增修改资料库", null, 3);
                return false;
            }
            return bolSaveStatus;
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (!bolEditStatus) return;

            if (!dtBeforeEdit.IsNull())
            {
                //人員資料 畫面權限 資料填入
                Fun_SetUserData(dtBeforeEdit);
                //修改
                Fun_SetBottonEnabled(Btn_Edit, true);
                //刪除
                Fun_SetBottonEnabled(Btn_Delete, true);
            }
            else
            {
                //人員欄位 清空            
                ReadOnlyHandler.Instance.ClearControl(CtrControlArray);
                Lbl_Updated.Text = "";
                Cob_Team.SelectedIndex = -1;
                Cob_Authority_Class.SelectedIndex = -1;
                //权限 全W
                Fun_AuthorityNewData();
                //修改
                Fun_SetBottonEnabled(Btn_Edit, false);
                //刪除
                Fun_SetBottonEnabled(Btn_Delete, false);
            }

            //狀態:Read 只顯示基本欄位
            strStatus = "Read";
            bolEditStatus = false;
            Fun_StatusToShowColumns(strStatus);
            //人員 欄位ReadOnly
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_User, true);
            //權限 RadioButton.Enabled = false;
            Fun_AuthorityEnabled(false);

            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            ////修改
            //Fun_SetBottonEnabled(Btn_New, true);
            ////刪除
            //Fun_SetBottonEnabled(Btn_Delete, true);
            Btn_Save.Visible = false; //儲存
            Btn_Cancel.Visible = false; //取消
            Btn_EditPassWord.Visible = false;

            /*"Edit"*/
            //原 密碼 異動時 不唯讀

          //  Txt_Password.BackColor = SystemColors.Window;// Color.LightYellow;            
            Lbl_Password_Title.Text = "密码";

           // Txt_Password_Old.BackColor = SystemColors.Window;// Color.LightYellow;
          //  Txt_Password_Chk.BackColor = SystemColors.Window;// Color.LightYellow;
            Lbl_Password_Chk_Title.Text = "密码确认";

            //Txt_Password_Old.Text = Txt_Password.Text;
            //Lbl_Password_Old_Title.Visible = false;
            //Txt_Password_Old.Visible = false;
            //Lbl_Password_Chk_Title.Visible = false;
            //Txt_Password_Chk.Visible = false;
            
            //Fun_SelectUser();
            
            ////密碼欄位移動
            //Lbl_Password_Title.Location = FristPoint_Lable;
            //Txt_Password.Location = FristPoint_TextBox;
            ////班別欄位移動
            //Lbl_Team_Title.Location = ChangePoint_Lable;
            //cboTeam.Location = ChangePoint_TextBox;

        }

        /// <summary>
        /// 修改畫面權限
        /// </summary>
        private void UpdateFrameSetup()
        {
            string Frame_Function = string.Empty, strSql = string.Empty;

            #region Frame

            #region '0-2'
            //Frame_Function = rdoN0_2.Checked ? "N" :
            //    rdoR0_2.Checked ? "R" :
            //    rdoW0_2.Checked ? "W" : string.Empty;
            //strSql = SqlFactory.Frm_5_3_UpdateFrame("0-2", Frame_Function);
            //if (!InsertUpdate("0-2", strSql))
            //    InsertUpdate("0-2", SqlFactory.Frm_5_3_Insert_Frame_One("0-2", Frame_Function));

                #endregion

                #region '1-1'
            Frame_Function = rdoN1_1.Checked ? "N" :
                rdoR1_1.Checked ? "R" :
                rdoW1_1.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("1-1", Frame_Function);
            if (!InsertUpdate("1-1", strSql))
                InsertUpdate("1-1", SqlFactory.Frm_5_3_Insert_Frame_One("1-1", Frame_Function));
            #endregion

            #region '1-2'
            Frame_Function = rdoN1_2.Checked ?"N" :
                rdoR1_2.Checked ? "R" :
                rdoW1_2.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("1-2", Frame_Function);
            if (!InsertUpdate("1-2", strSql))
                InsertUpdate("1-2", SqlFactory.Frm_5_3_Insert_Frame_One("1-2", Frame_Function));
            #endregion

            #region '1-3'
            Frame_Function = rdoN1_3.Checked ? "N" :
                rdoR1_3.Checked ? "R" :
                rdoW1_3.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("1-3", Frame_Function);
            if (!InsertUpdate("1-3", strSql))
                InsertUpdate("1-3", SqlFactory.Frm_5_3_Insert_Frame_One("1-3", Frame_Function));
            #endregion

            #region '2-1'
            Frame_Function = rdoN2_1.Checked  ? "N" :
                rdoR2_1.Checked ? "R" :
                rdoW2_1.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("2-1", Frame_Function);
            if (!InsertUpdate("2-1", strSql))
                InsertUpdate("2-1", SqlFactory.Frm_5_3_Insert_Frame_One("2-1", Frame_Function));
            #endregion

            #region '2-2'
            Frame_Function = rdoN2_2.Checked ? "N" :
                rdoR2_2.Checked ? "R" :
                rdoW2_2.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("2-2", Frame_Function);
            if (!InsertUpdate("2-2", strSql))
                InsertUpdate("2-2", SqlFactory.Frm_5_3_Insert_Frame_One("2-2", Frame_Function));
            #endregion

            #region '3-1'
            Frame_Function = rdoN3_1.Checked ? "N" :
                rdoR3_1.Checked ? "R" :
                rdoW3_1.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("3-1", Frame_Function);
            if (!InsertUpdate("3-1", strSql))
                InsertUpdate("3-1", SqlFactory.Frm_5_3_Insert_Frame_One("3-1", Frame_Function)); 
            #endregion

            #region '3-2'
            Frame_Function = rdoN3_2.Checked ? "N" :
                rdoR3_2.Checked ? "R" :
                rdoW3_2.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("3-2", Frame_Function);
            if (!InsertUpdate("3-2", strSql))
                InsertUpdate("3-2", SqlFactory.Frm_5_3_Insert_Frame_One("3-2", Frame_Function));
            #endregion

            #region '3-3'
            Frame_Function = rdoN3_3.Checked ? "N" :
                rdoR3_3.Checked ? "R" :
                rdoW3_3.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("3-3", Frame_Function);
            if (!InsertUpdate("3-3", strSql))
                InsertUpdate("3-3", SqlFactory.Frm_5_3_Insert_Frame_One("3-3", Frame_Function));
            #endregion

            #region '3-4'
            //Frame_Function = radioButton1.Checked ? "N" :
            //    radioButton2.Checked ? "R" :
            //    radioButton3.Checked ? "W" : string.Empty;
            //strSql = SqlFactory.Frm_5_3_UpdateFrame("3-4", Frame_Function);
            //InsertUpdate(strSql);
            #endregion

            #region '4-1'
            Frame_Function = rdoN4_1.Checked ? "N" :
                rdoR4_1.Checked ? "R" :
                rdoW4_1.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("4-1", Frame_Function);
            if (!InsertUpdate("4-1", strSql))
                InsertUpdate("4-1", SqlFactory.Frm_5_3_Insert_Frame_One("4-1", Frame_Function));
            #endregion

            #region '4-2'
            Frame_Function = rdoN4_2.Checked ? "N" :
                rdoR4_2.Checked ? "R" :
                rdoW4_2.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("4-2", Frame_Function);
            if (!InsertUpdate("4-2", strSql))
                InsertUpdate("4-2", SqlFactory.Frm_5_3_Insert_Frame_One("4-2", Frame_Function));
            #endregion

            #region '4-3'
            Frame_Function = rdoN4_3.Checked ? "N" :
                rdoR4_3.Checked ? "R" :
                rdoW4_3.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("4-3", Frame_Function);
            if (!InsertUpdate("4-3", strSql))
                InsertUpdate("4-3", SqlFactory.Frm_5_3_Insert_Frame_One("4-3", Frame_Function));
            #endregion

            #region '4-4'
            Frame_Function = rdoN4_4.Checked ? "N" :
                rdoR4_4.Checked ? "R" :
                rdoW4_4.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("4-4", Frame_Function);
            if (!InsertUpdate("4-4", strSql))
                InsertUpdate("4-4", SqlFactory.Frm_5_3_Insert_Frame_One("4-4", Frame_Function));
            #endregion

            #region '4-5'
            Frame_Function = rdoN4_5.Checked ? "N" :
                rdoR4_5.Checked ? "R" :
                rdoW4_5.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("4-5", Frame_Function);
            if (!InsertUpdate("4-5", strSql))
                InsertUpdate("4-5", SqlFactory.Frm_5_3_Insert_Frame_One("4-5", Frame_Function));
            #endregion

            #region '5-1'
            Frame_Function = rdoN5_1.Checked ? "N" :
                rdoR5_1.Checked ? "R" :
                rdoW5_1.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("5-1", Frame_Function);
            if (!InsertUpdate("5-1", strSql))
                InsertUpdate("5-1", SqlFactory.Frm_5_3_Insert_Frame_One("5-1", Frame_Function));
            #endregion

            #region '5-2'
            Frame_Function = rdoN5_2.Checked ? "N" :
                rdoR5_2.Checked ? "R" :
                rdoW5_2.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("5-2", Frame_Function);
            if (!InsertUpdate("5-2", strSql))
                InsertUpdate("5-2", SqlFactory.Frm_5_3_Insert_Frame_One("5-2", Frame_Function));
            #endregion

            #region '5-3'
            Frame_Function = rdoN5_3.Checked ? "N" :
                rdoR5_3.Checked ? "R" :
                rdoW5_3.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("5-3", Frame_Function);
            if (!InsertUpdate("5-3", strSql))
                InsertUpdate("5-3", SqlFactory.Frm_5_3_Insert_Frame_One("5-3", Frame_Function));
            #endregion

            #region '5-4'
            Frame_Function = rdoN5_4.Checked ? "N" :
                rdoR5_4.Checked ? "R" :
                rdoW5_4.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("5-4", Frame_Function);
            if (!InsertUpdate("5-4", strSql))
                InsertUpdate("5-4", SqlFactory.Frm_5_3_Insert_Frame_One("5-4", Frame_Function));
            #endregion

            #region '5-5'
            Frame_Function = rdoN5_5.Checked ? "N" :
                rdoR5_5.Checked ? "R" :
                rdoW5_5.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("5-5", Frame_Function);
            if (!InsertUpdate("5-5", strSql))
                InsertUpdate("5-5", SqlFactory.Frm_5_3_Insert_Frame_One("5-5", Frame_Function));
            #endregion

            #region '5-6'
            Frame_Function = rdoN5_6.Checked ? "N" :
                rdoR5_6.Checked ? "R" :
                rdoW5_6.Checked ? "W" : string.Empty;
            strSql = SqlFactory.Frm_5_3_UpdateFrame("5-6", Frame_Function);
            if (!InsertUpdate("5-6", strSql))
                InsertUpdate("5-6", SqlFactory.Frm_5_3_Insert_Frame_One("5-6", Frame_Function));
            #endregion

            #endregion

        }
        
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            string strSql = string.Empty;

            if (!Txt_UserID.Text.IsEmpty() && !Txt_Password.Text.IsEmpty())
            {
                //新增
                Fun_SetBottonEnabled(Btn_New, false);
                //修改
                Fun_SetBottonEnabled(Btn_Edit, false);

                DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel($"確認是否刪除該帳號:{Txt_UserID.Text}?", "删除", Properties.Resources.dialogQuestion, 1);

                if (dialogR.Equals(DialogResult.OK))
                {
                    //刪除帳號
                    strSql = SqlFactory.Frm_5_3_DeleteUser(Txt_UserID.Text);
                    Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"刪除{Txt_UserID.Text}账号权限", "5-3");

                    //刪除畫面權限
                    strSql = SqlFactory.Frm_5_3_DeleteFrame();
                    Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL, $"刪除{Txt_UserID.Text}账号权限", "5-3");
                    EventLogHandler.Instance.EventPush_Message($"删除{Txt_UserID.Text}完成");
                    EventLogHandler.Instance.LogInfo("5-3", $"使用者:{PublicForms.Main.lblLoginUser.Text}人员管理", $"删除{Txt_UserID.Text}完成");
                    PublicComm.ClientLog.Info($"刪除{Txt_UserID.Text}完成");

                    //帳號清單
                    UserSetupHandler.Instance.UserID_List(Cob_UserID_Search);
                    Fun_SelectUser();
                }

                //新增
                Fun_SetBottonEnabled(Btn_New, true);
                //修改
                Fun_SetBottonEnabled(Btn_Edit, true);
            }
            else
            {
                EventLogHandler.Instance.EventPush_Message($"未选择欲删除之账号");
                EventLogHandler.Instance.LogInfo("5-3", $"使用者:{PublicForms.Main.lblLoginUser.Text}人员管理", $"未选择欲删除之账号");
                PublicComm.ClientLog.Info($"未選擇欲刪除之鋼捲");
            }
        }

        private bool Fun_ChackPassword()
        {
            //bool bolChk = false;
            #region New & Edit 都要檢查密碼

            //检查 密码
            if (string.IsNullOrEmpty(Txt_Password.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk("密码请勿为空白！", "人员密码檢查", null,0);
                Txt_Password.Focus();
                return false;
            }

            //检查 确认密码
            if (string.IsNullOrEmpty(Txt_Password_Chk.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk("确认密码请勿为空白！", "人员密码檢查", null, 0);
                Txt_Password_Chk.Focus();
                return false;
            }

            //检查密码 是否相符
            if (Txt_Password.Text.Trim() != Txt_Password_Chk.Text.Trim())
            {
                DialogHandler.Instance.Fun_DialogShowOk("密码不相符！", "人员密码檢查", null, 0);
                Txt_Password_Chk.Focus();
                return false;
            }
            else
            {
                return true;
            }

            #endregion

            //return bolChk;
        }

        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            //Color colorBack;
            btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
        }

       

        private void Btn_EditPassWord_Click(object sender, EventArgs e)
        {
            /*"Read"*/
            if (strStatus == "Read") { return; }
            /*"New"*/
            if (strStatus == "New")
            {
                return;
            }
            else if (strStatus == "Edit")
            {
                /*"Edit"*/
                //原 密碼 異動時 不唯讀
                Txt_Password_Old.Enabled = true;
                Txt_Password_Old.BackColor = SystemColors.Window;
                Txt_Password_Old.Text = "";

                Lbl_Password_Title.Text = "新密码";
                Txt_Password.Enabled = true;
                Txt_Password.BackColor = SystemColors.Window;
                Txt_Password.Text = "";

                Lbl_Password_Chk_Title.Text = "新密码确认";
                Txt_Password_Chk.Enabled = true;
                Txt_Password_Chk.BackColor = SystemColors.Window;
                Txt_Password_Chk.Text = "";
            }

        }

        private void Fun_AuthorityNewData()
        {
            foreach (Control Ctl in Grb_AuthoritySetting.Controls)
            {
                if (Ctl is GroupBox)
                {
                    foreach (Control Ctl2 in Ctl.Controls)
                    {
                        if (Ctl2 is RadioButton)
                        {
                            RadioButton rdb = Ctl2 as RadioButton;
                            string strCtl2_FrameFun = rdb.Name.ToString().Substring(3, 1);
                            if (strCtl2_FrameFun.ToUpper() == "N")
                            {
                                rdb.Checked = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 畫面權限設定RadioButton啟用設定
        /// </summary>
        /// <param name="bolEn"></param>
        private void Fun_AuthorityEnabled(bool bolEn)
        {
            foreach (Control Ctl in Grb_AuthoritySetting.Controls)
            {
                if (Ctl is GroupBox)
                {
                    foreach (Control Ctl2 in Ctl.Controls)
                    {
                        if (Ctl2 is RadioButton)
                        {
                            RadioButton rdb = Ctl2 as RadioButton;
                            if (rdb.Checked)
                            {
                                rdb.Enabled = true;
                            }
                            else
                            {
                                rdb.Enabled = bolEn;
                            }
                        }
                    }
                }
            }
        }


        #region 語系切換,文字調整 Fun_LanguageIsEn_Font
        private void Fun_LanguageIsEn_Font14_10(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)10, fs);
        }       
        #endregion Fun_LanguageIsEn_Font End
    }
}

using GPLManager.Contrl;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class Frm_0_1_Login : Form
    {
        public Frm_0_1_Login()
        {
            InitializeComponent();
        }
        /// <summary>
        /// True = 登入成功 / False = 登入失敗
        /// </summary>
        public bool bolLogin;
        private void Frm_0_1_Login_Shown(object sender, EventArgs e)
        {
            PublicForms.Login = this;
            Cob_User.Enabled = true;
            UserSetupHandler.Instance.UserID_List(Cob_User);
            AcceptButton = Btn_Login;
        }
        /// <summary>
        ///     '''  選擇使用者選擇完畢後
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void Cob_User_DropDownClosed(object sender, EventArgs e)
        {
            Txt_Password.Focus();
        }

        /// <summary>
        ///     ''' 按下按鈕，檢查登入者資訊正確性
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void Btn_Login_Click(object sender, EventArgs e)
        {
            UserSetupHandler.Instance.UserLogin(Txt_Password.Text);
            if (bolLogin == true)
            {
                Frm_0_0_Main FatherForm = Parent.Parent as Frm_0_0_Main;
                CommonDef.LoginUser = Cob_User.Text;
                FatherForm.lblLoginUser.Text = Cob_User.Text;

                FatherForm.Btn_Language.Enabled = true;
                FatherForm.Btn_Logout.Enabled = true;
                FatherForm.Btn_Print.Enabled = true;
                FatherForm.ShowMenu();
                EventLogHandler.Instance.LogInfo("0-1", "使用者:" + PublicForms.Main.lblLoginUser.Text.Trim() + "登入", "登入成功");
                EventLogHandler.Instance.EventPush_Message($"<{Cob_User.Text.Trim()}>登入成功");
                PublicComm.ClientLog.Info($"登入成功<{Cob_User.Text.Trim()}>");
                Close();
            }
            else if(bolLogin == false)
            {
                EventLogHandler.Instance.LogInfo("0-1", "使用者:" + PublicForms.Main.lblLoginUser.Text.Trim() + "登入", "登入失败");
                EventLogHandler.Instance.EventPush_Message($"登入失败<{Cob_User.Text.Trim()}>");
                PublicComm.ClientLog.Info($"登入失败<{Cob_User.Text.Trim()}>");
            }
        }

        private void Pic_UserPassword_DoubleClick(object sender, EventArgs e)
        {
            if (Screen.AllScreens.Count() == 2)
            {
                string CurrentScreenName = Screen.FromControl(this).DeviceName;
                for (int i = 0; i < 2; i++)
                {
                    if (Screen.AllScreens[i].DeviceName != CurrentScreenName)
                    {
                        PublicForms.Main.WindowState = FormWindowState.Normal;
                        PublicForms.Main.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                        PublicForms.Main.Location = new System.Drawing.Point(Screen.AllScreens[i].Bounds.X, PublicForms.Main.Location.Y);
                        PublicForms.Main.WindowState = FormWindowState.Maximized;

                    }
                }

            }
            else { }
        }
    }
}
using GPLManager.Util;
using System;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class Frm_Reason : Frm_DialogBase
    {
        public string StrReason;
        public string StrReasonCode;
        public string Coil;

        public Frm_Reason()
        {
            InitializeComponent();
        }

        private void Frm_Reason_Load(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.ComboBox_DeleteCode(Cob_Reason);

            Cob_Reason.Focus();
        }

        public void Fun_CatchTitle(string strFormText = "" , string strTitle1 = "", string strTitle2 = "")
        {
            if (strTitle1.Length > 245)
            {
                Txt_ShowMessage.ScrollBars = ScrollBars.Both;
            }
            Lbl_Tile.Text = strFormText;
            Txt_ShowMessage.Text = strTitle1;
            Lbl_Title2.Text = strTitle2;
            Txt_ShowMessage.Select(0, 0);
            Lbl_Tile.Focus();
        }
       
        private void Btn_OK_Click(object sender, EventArgs e)
        {
            if (!Cob_Reason.SelectedValue.ToString().Trim().IsEmpty())
            {
                StrReasonCode = Cob_Reason.SelectedValue.ToString().Trim();
                StrReason = Cob_Reason.Text.ToString().Trim();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}

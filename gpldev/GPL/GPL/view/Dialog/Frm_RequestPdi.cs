using System;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class Frm_RequestPdi : Frm_DialogBase
    {
        public string StrRequestCoilNo;

        public Frm_RequestPdi()
        {
            InitializeComponent();
        }


        private void Frm_Z_RequestPdi_Load(object sender, EventArgs e)
        {
            Txt_CoilNo.Text = string.Empty;
        }

        public void Fun_CatchTitle(string strTile = "",string strTitle1 = "",string strTitle2 = "")
        {
            Lbl_Tile.Text = strTile;
            Lbl_Title1.Text = strTitle1;
            Lbl_Title2.Text = strTitle2;

        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Txt_CoilNo.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk("请输入钢卷号!","提示", Properties.Resources.dialogInformation, 0);
                Txt_CoilNo.Focus();
            }
            else
            {
                StrRequestCoilNo = Txt_CoilNo.Text.Trim();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

       
    }
}

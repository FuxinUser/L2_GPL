using GPLManager.Contrl;
using System;
using System.Data;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class frm_1_3_CoilSkip : Form
    {
        public frm_1_3_CoilSkip()
        {
            InitializeComponent();
        }

        private void Frm_2_3_CoilSkip_Shown(object sender, EventArgs e)
        {
            DataTable dtskip = new DataTable();
            dtskip = CommonDef.getDemoData(@"demodata\2.3.0.跳軋鋼捲.csv");
            dgCoilSkip.DataSource = dtskip;

            try
            {
                dgCoilSkip.Rows[0].Selected = true;
            }
            catch
            {
            }
        }

        private void DgCoilSkip_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
            }
            else
            {
                dgCoilSkip.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                dgCoilSkip.Rows[e.RowIndex].Selected = true;
            }
        }

        private void DgCoilSkip_CellMouseDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int row = e.RowIndex;
                DataRowView drv = dgCoilSkip.Rows[row].DataBoundItem as DataRowView;

                string coilnumber = drv.Row[1].ToString();
                string reasontext = drv.Row[5].ToString();
                string commenttext = drv.Row[6].ToString();

                cboCoilID.Text = coilnumber;
                cboSkipReasonCode.Text = reasontext;
                txtSkipComment.Text = commenttext;

                dgCoilSkip.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                dgCoilSkip.Rows[e.RowIndex].Selected = true;
            }
        }

        private void BtnQueryData_Click(object sender, EventArgs e)
        {
            pnlQueryData.Visible = true;
        }

        private void BtnClosePanel_Click(object sender, EventArgs e)
        {
            pnlQueryData.Visible = false;
        }
    }
}

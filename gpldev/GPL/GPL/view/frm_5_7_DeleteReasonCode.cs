
using GPLManager.Contrl;
using System;
using System.Data;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class frm_5_7_DeleteReasonCode : Form
    {
        public frm_5_7_DeleteReasonCode()
        {
            InitializeComponent();
        }

        private void frm_5_7_DeleteReasonCode_Shown(object sender, EventArgs e)
        {
            DataTable dtskip = new DataTable();
            dtskip = CommonDef.getDemoData(@"demodata\2.4.0.跳軋代碼.csv");
            dgSkipReasonCode.DataSource = dtskip;

            try
            {
                dgSkipReasonCode.Rows[0].Selected = true;
            }
            catch
            {
            }
        }

        private void DgSkipReasonCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
            }
            else
            {
                dgSkipReasonCode.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                dgSkipReasonCode.Rows[e.RowIndex].Selected = true;
            }
        }

        private void DgSkipReasonCode_CellMouseDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int row = e.RowIndex;
                DataRowView drv = dgSkipReasonCode.Rows[row].DataBoundItem as DataRowView;
                txtSkipReasonCode.Text = drv.Row[0].ToString();
                txtSkipType.Text = drv.Row[1].ToString();
                txtSkipReason.Text = drv.Row[2].ToString();

                dgSkipReasonCode.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                dgSkipReasonCode.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}
using GPLManager.Contrl;
using System;
using System.Data;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class frm_2_2_MillSetup : Form
    {
        public frm_2_2_MillSetup()
        {
            InitializeComponent();
        }

        private void Frm_3_2_MillSetup_Shown(object sender, EventArgs e)
        {
            DataTable dtMillSetup = new DataTable();
            dtMillSetup = CommonDef.getDemoData(@"demodata\3.2.0.軋延資訊.csv");
            //dgcoilList.DataSource = dtMillSetup;

            
            //try
            //{
            //    dgcoilList.Rows[0].Selected = true;
            //}
            //catch
            //{
            //}
        }

        private void DgMillSetup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
            }
            else
            {
                //dgcoilList.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                //dgcoilList.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}

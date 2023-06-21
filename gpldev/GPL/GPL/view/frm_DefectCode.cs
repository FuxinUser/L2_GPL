using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GPLManager.Contrl;

namespace GPLManager
{
    public partial class frm_DefectCode : Form
    {
        public frm_DefectCode()
        {
            InitializeComponent();
        }
        private void frm_DefectCode_Load(object sender, EventArgs e)
        {
            dgv_List.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCheckBoxColumn check_Column = new DataGridViewCheckBoxColumn();
            check_Column.HeaderText = "選取";
            dgv_List.Columns.Insert(0,check_Column);
            DataTable dtOn = new DataTable();
            dtOn = CommonDef.getDemoData(@"demodata\缺陷.csv");
            dgv_List.DataSource = dtOn;
            //修改
            dgv_AlterList.ColumnCount = 14;
            dgv_AlterList.Columns[0].Name = "組別";
            dgv_AlterList.Columns[1].Name = "Defect Code";
            dgv_AlterList.Columns[2].Name = "Defect Origin";
            dgv_AlterList.Columns[3].Name = "Sid";
            dgv_AlterList.Columns[4].Name = "Width Direction";
            dgv_AlterList.Columns[5].Name = "Length Start Direction";
            dgv_AlterList.Columns[6].Name = "Length End Direction";
            dgv_AlterList.Columns[7].Name = "Defect Level";
            dgv_AlterList.Columns[8].Name = "Defect Percent";
            dgv_AlterList.Columns[9].Name = "襯紙代碼";
            dgv_AlterList.Columns[10].Name = "Inspector Code";
            dgv_AlterList.Columns[11].Name = "Hold Flag";
            dgv_AlterList.Columns[12].Name = "Hold Cause Code";
            //預留
            dgv_AlterList.Columns[13].Name = "備註";


            
        }
       //編輯修改
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            
            foreach (DataGridViewRow row in dgv_List.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    string[] dgv_AlterListArray = new string[] { (string)row.Cells[1].Value, (string)row.Cells[2].Value, (string)row.Cells[3].Value, (string)row.Cells[4].Value, (string)row.Cells[5].Value, (string)row.Cells[6].Value, (string)row.Cells[7].Value, (string)row.Cells[8].Value, (string)row.Cells[9].Value, (string)row.Cells[10].Value, (string)row.Cells[11].Value, (string)row.Cells[12].Value, (string)row.Cells[13].Value };
                    dgv_AlterList.Rows.Add(dgv_AlterListArray);
                }
            }
        }
        //儲存修改
        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}

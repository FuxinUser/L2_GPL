using GPLManager.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class Frm_SelectDataOpen : Form
    {
        public DataTable dtSelectData;
        public string strDataType;
        public string strOut_Coil_ID ;//1                
        public string strFinishTime ;//2
        public string strIn_Coil_ID ;//3
        public string strPlan_No ;//4


        public Frm_SelectDataOpen()
        {
            InitializeComponent();
        }

        private void Frm_SelectDataOpen_Load(object sender, EventArgs e)
        {
            Fun_Initial_Dgv_SelectPdo();
        }
        private void Fun_Initial_Dgv_SelectPdo()
        {
            if(dtSelectData != null && dtSelectData.Rows.Count > 0)
            {
                DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_SelectPdo, dtSelectData);
                DGVColumnsHandler.Instance.Frm_DetailOpenColumns(Dgv_SelectPdo, strDataType);
                DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_SelectPdo);
               
            }
            
        }

        private void Dgv_SelectPdo_DoubleClick(object sender, EventArgs e)
        {
            if (Dgv_SelectPdo.DgvIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("无钢卷清单可开启", "开启详细资料",null, 0);
                return;
            }

            if (Dgv_SelectPdo.CurrentIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选取要显示的钢卷", "开启详细资料", null, 0);
                return;
            }
            Btn_SelectPdo_OK.PerformClick();
        }

        private void Btn_SelectPdo_OK_Click(object sender, EventArgs e)
        {
            if (Dgv_SelectPdo.DgvIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("无钢卷清单可开启", "开启详细资料", null, 0);
                return;
            }

            if (Dgv_SelectPdo.CurrentIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选取要显示的钢卷", "开启详细资料", null, 0);
                return;
            }

            DataRow drGetCurrentRow = dtSelectData.Rows[Dgv_SelectPdo.CurrentRow.Index];

            DataTable dt = dtSelectData.Clone();
            try
            {
                dt.LoadDataRow(drGetCurrentRow.ItemArray, false);
            }
            catch { return; }

            dtSelectData = dt.Copy();

            //Frm_GetDetailOpen(Dgv_SelectPdo.CurrentRow);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Frm_GetDetailOpen(DataTable dt)
        {
            if (dt != null)
            {
                //strOut_Coil_ID = dgvrData.Cells["Out_Coil_ID"].ToString();//1                
                //strFinishTime = dgvrData.Cells["FinishTime"].ToString();//2
                //strIn_Coil_ID = dgvrData.Cells["In_Coil_ID"].ToString();//3
                //strPlan_No = dgvrData.Cells["Plan_No"].ToString();//4               
            }



        }

        private void Btn_SelectPdo_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Btn_SelectPdo_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
    }
}

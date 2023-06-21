using Akka.Actor;
//using DataModel.HMIServerCom.Msg;
using System;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class frm_Scan : Form
    {
        string strSql = "";
        public frm_Scan()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void chk_PDI_CoilID_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_PDI_CoilID.Checked == true)
            {
                chk_Scan_CoilID.Checked = false;
            }
        }
        private void chk_Scan_CoilID_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Scan_CoilID.Checked == true)
            {
                chk_PDI_CoilID.Checked = false;
            }
        }

        private void btn_Scan_ok_Click(object sender, EventArgs e)
        {
            if (chk_Scan_CoilID.Checked == true)
            {
                UpdateDB(chk_Scan_CoilID.Text);
            }
            else if (chk_PDI_CoilID.Checked == true)
            {
                UpdateDB(chk_PDI_CoilID.Text);
            }
        }
        private void UpdateDB(string Coil_ID)
        {
            strSql = " Update [L3L2_PDI] set ";
            strSql += " [Entry_Scaned_CoilID] =' "+ Coil_ID + "'";
            strSql += ",[Entry_Scaned_UserID] = '" + PublicForms.Main.lblLoginUser.Text + "'";
            strSql += ",[Entry_Scaned_Time] = '"+PublicForms.Main.lblClock.Text+"'";
            strSql += ",[Origin_CoilID] = '" + chk_PDI_CoilID.Text + "'";
            strSql += ",[Entry_CoilID_Checked] = '1' ";
            strSql += " where [Entry_Coil_No]='"+ chk_PDI_CoilID .Text+ "'";
            try
            {
                Data_Access.GetInstance().ExecuteNonQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL);
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text}确认扫描结果", $"扫描结果确认 钢卷编号:{Coil_ID}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //SCCommMsg.CS04_RenameCoil _RenameCoil = new SCCommMsg.CS04_RenameCoil();
            //_RenameCoil.Source = "CPL1_HMI";
            //_RenameCoil.ID = "RenameCoil";
            //_RenameCoil.Coil_ID = Coil_ID;
            //PublicComm.client.Tell(_RenameCoil);
            EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text}通知Server确认扫描结果", $"通知Server扫描结果确认 钢卷编号:{Coil_ID}");
        }
    }
}

using Akka.Actor;
using DataModel.HMIServerCom.Msg;
using System;
using System.Data;
using System.Windows.Forms;

namespace GPLManager
{
    public partial class frm_Entry : Form
    {
        #region 變數
        string strSql = "";
        DataTable dt = new DataTable();
        public string Skid = "";        
        #endregion

        public frm_Entry()
        {
            InitializeComponent();
        }
        private void Frm_Entry_Load(object sender, EventArgs e)
        {            
            Fun_SelectionMode();
        }
        public void Fun_SelectionMode()
        {
            lbSkid.Text += Skid;
            //strSql = SqlFactory.Frm_Entry_CoilList_DB_Schedule_PDI();
            strSql = SqlFactory.Frm_1_1_SelectSchedule_InitialDataGridView_DB_Schedule_PDI();
            dt = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"入料钢卷清单");

            if (dt == null) return;

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(dgv_off, dt);
            DGVColumnsHandler.Instance.Frm_Entry(dgv_off);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(dgv_off);
        }
       
        /// <summary>
        /// 確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_True_Click(object sender, EventArgs e)
        {
            DialogResult dialogR = MessageBox.Show("確認儲存?", "提示", MessageBoxButtons.OKCancel);
            if (dialogR == DialogResult.OK)
            {
                strSql = SqlFactory.Frm_Entry_UpdateMap_DB_TrackingMap(Skid, dgv_off.CurrentRow.Cells[0].Value.ToString().Trim()) ;
                Data_Access.GetInstance().ExecuteNonQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL);
                SCCommMsg.CS12_Coil_SkidFeed _SkidFeed = new SCCommMsg.CS12_Coil_SkidFeed
                {
                    Source = "GPL_Server",
                    ID = "SkidFeed",
                    Skid = Skid,
                    Coil_ID = dgv_off.CurrentRow.Cells[0].Value.ToString().Trim()
                };
                PublicComm.Client.Tell(_SkidFeed);
                EventLogHandler.Instance.EventPush_Message($"已通知Server钢卷号[{dgv_off.CurrentRow.Cells[0].Value}]入料");
                PublicComm.ClientLog.Info($"訊息名稱:插入過渡卷 訊息:已通知Server鋼卷號[{dgv_off.CurrentRow.Cells[0].Value}]入料");
                PublicComm.akkaLog.Info($"訊息名稱:插入過渡卷 訊息:已通知Server鋼卷號[{dgv_off.CurrentRow.Cells[0].Value}]入料");
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text}入料作业", $"入料鞍座:{Skid} 钢卷编号:{dgv_off.CurrentRow.Cells[0].Value}");

                ////刪除排程
                //strSql = SqlFactory.Frm_1_1_DeleteSchedule_DB_Schedule(dgv_off.CurrentRow.Cells[0].Value.ToString());
                
                //Data_Access.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_GPL,"删除排程");
               
                EventLogHandler.Instance.EventPush_Message($"已通知Server钢卷号[{dgv_off.CurrentRow.Cells[0].Value}]入料");
                PublicComm.ClientLog.Info($"訊息名稱:插入過渡卷 訊息:已通知Server鋼卷號[{dgv_off.CurrentRow.Cells[0].Value}]入料");

                Close();
            }
        }
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

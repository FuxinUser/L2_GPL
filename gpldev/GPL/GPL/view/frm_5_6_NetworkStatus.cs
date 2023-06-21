using GPLManager.Util;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static GPLManager.DataBaseTableFactory;
namespace GPLManager
{
    public partial class frm_5_6_NetworkStatus : Form
    {
        DataTable dtGetStatus = new DataTable();
        //語系
        private LanguageHandler LanguageHand;
        public frm_5_6_NetworkStatus()
        {
            InitializeComponent();
        }
        private void Frm_5_6_NetworkStatus_Load(object sender, System.EventArgs e)
        {
            if (PublicForms.NetworkStatus == null) PublicForms.NetworkStatus = this;
            
            Fun_SelectNetWorkStatus();
            TimerForRefresh.Start();

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        private void TimerForRefresh_Tick(object sender, EventArgs e)
        {
            Fun_SelectNetWorkStatus();
        }

        private void Fun_SelectNetWorkStatus()
        {
            string strSql = SqlFactory.Frm_5_6_SelectStatus();
            dtGetStatus = Data_Access.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_GPL,"连线状态","5-6");

            if (dtGetStatus.IsNull()) return;

            Fun_NetworkStatusColorControl();
        }
        private void Fun_NetworkStatusColorControl()
        {
            for (int Index = 0; Index < dtGetStatus.Rows.Count; Index++)
            {
                //Send MMS
                if (dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("MMS"))
                {
                    Lbl_SendMMS_Color.BackColor = dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Trim().Equals("1") ?
                        Color.Lime : Color.Red;
                }
                //Rev MMS
                else if (dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_From)].ToString().Trim().Equals("MMS"))
                {
                    Lbl_RevMMS_Color.BackColor = dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ?
                        Color.Lime : Color.Red;
                }
                //Send L2.5
                else if (dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("LEVEL25"))
                {
                    Lbl_SendL25_Color.BackColor = dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ?
                        Color.Lime : Color.Red;
                }
                //Send WMS
                else if (dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("WMS"))
                {
                    Lbl_SendWMS_Color.BackColor = dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ?
                        Color.Lime : Color.Red;
                }
                //Rev WMS
                else if (dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_From)].ToString().Trim().Equals("WMS"))
                {
                    Lbl_RevWMS_Color.BackColor = dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ?
                        Color.Lime : Color.Red;
                }
                //Send PLC
                else if (dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("LEVEL1"))
                {
                    Lbl_SendPLC_Color.BackColor = dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ?
                        Color.Lime : Color.Red;
                }
                //Rev PLC
                else if (dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_From)].ToString().Trim().Equals("LEVEL1"))
                {
                    Lbl_RevPLC_Color.BackColor = dtGetStatus.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ?
                        Color.Lime : Color.Red;
                }
            }
        }

       
    }
}

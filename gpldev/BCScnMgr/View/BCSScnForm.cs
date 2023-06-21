using Controller;
using MSMQ.Core.MSMQ;
using System;
using System.Configuration;
using System.Windows.Forms;
using WinformsMVP.Controls.Forms;
using WinformsMVP.Controls.Forms.Base;
using static Core.Define.L2SystemDef;
using static DataMod.BarCode.BCSDataModel;

namespace BCScnMgr.View
{
    public partial class BCSScnForm : BaseForm, BCScnContract.IView
    {
        public BCScnContract.IPresenter Presenter { get; set; }

        public BCSScnForm(BCScnContract.IPresenter presenter)
        {
            InitializeComponent();
            Presenter = presenter;
            Presenter.SetView(this);
        }


        private void BCSScnForm_Load(object sender, EventArgs e)
        {
            EntryComoBox.SelectedIndex = 0;
            DeliveryComboBox.SelectedIndex = 0;
        }

        private void BCSScnForm_Shown(object sender, EventArgs e)
        {
            //網路環境設定(顯示):
            tbLocalIp.Text = ConfigurationManager.AppSettings["LocalIP"];
            tbLocalPort.Text = ConfigurationManager.AppSettings["LocalPort"];
            tbRemoteIp.Text = ConfigurationManager.AppSettings["RemoteIP"];
            tbRemotePort.Text = ConfigurationManager.AppSettings["RemotePort"];            
        }

        private void btnEntryScan_Click(object sender, EventArgs e)
        {
            if (txtEntryCoilNo.Text.Equals(""))
            {
                MessageBox.Show("請輸入鋼捲編號");
                return;
            }

            var bcs = new BarCodeScnContent
            {
                ScanCoilNo = txtEntryCoilNo.Text,
                ScanPosition = DetPos(EntryComoBox.Text),
            };

            MQPoolService.SendToBCScnRcvEdit(InfoBCScn.ScanEntryCoilNo.Data(bcs));
        }

        private void btnDeliveryScn_Click(object sender, EventArgs e)
        {
            if (txtDeliveryCoilNo.Text.Equals(""))
            {
                MessageBox.Show("請輸入鋼捲編號");
                return;
            }

            var bcs = new BarCodeScnContent
            {
                ScanCoilNo = txtDeliveryCoilNo.Text,
                ScanPosition = DetPos(DeliveryComboBox.Text),
            };

            MQPoolService.SendToBCScnRcvEdit(InfoBCScn.ScanDeliveryCoilNo.Data(bcs));
        }

        private SKPOS DetPos(string pos)
        {
            SKPOS POS = SKPOS.EntryTOP;

            switch (pos)
            {
                case "ESK1":
                    POS = SKPOS.Delivery_SK01;
                    break;
                case "ESK2":
                    POS = SKPOS.Delivery_SK02;
                    break;
                case "ETOP":
                    POS = SKPOS.EntryTOP;
                    break;
                case "DSK1":
                    POS = SKPOS.Delivery_SK01;
                    break;
                case "DSK2":
                    POS = SKPOS.Delivery_SK02;
                    break;
                case "DTOP":
                    POS = SKPOS.DeliveryTop;
                    break;
            }

            return POS;
        }

        [Obsolete]
        public void SetPresenter(BaseFormContract.IPresenter presenter)
        {
            throw new NotImplementedException();
        }

     
    }
}

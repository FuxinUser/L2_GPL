using System;
using System.Windows.Forms;
using WinformsMVP.Controls.Forms;
using DataModel.HMIServerCom.Msg;
using static DataMod.BarCode.BCSDataModel;
using MSMQ.Core.MSMQ;
using Controller;
using WinformsMVP.Controls.Forms.Base;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using static Core.Define.L2SystemDef;

/**
* Author: ICSC 余士鵬
* Date: 2019/10/08
* Description: Hmi communicate with server UI
* Reference: 
* Modified: 
*/
namespace PcComm.View
{
    public partial class ClientCoordinatorForm1 : BaseForm, ClientCoordinatorContract.IView
    {
        public ClientCoordinatorContract.IPresenter Presenter { get; set; }

        public ClientCoordinatorForm1(ClientCoordinatorContract.IPresenter presenter)
        {
            InitializeComponent();
            Presenter = presenter;
            Presenter.SetView(this);
        }
        private void ClientCoordinatorForm1_Load(object sender, EventArgs e)
        {
            EntryComoBox.SelectedIndex = 0;
            DeliveryComboBox.SelectedIndex = 0;
        }

     
        //測試用

        // 鋼捲調整
        private void btnCoilScheduleAdj_Click(object sender, EventArgs e)
        {
            var msg = new SCCommMsg.CS03_ScheduleChange();           
            msg.SchStatus = SCCommMsg.ScheduleStatus.ADJUST;
            MQPoolService.SendToCoil(InfoCoil.UpdateCoilSchedule.Data(msg));
        }

        // 鋼捲刪除
        private void btnCoilDelete_Click(object sender, EventArgs e)
        {
            var msg = new SCCommMsg.CS03_ScheduleChange
            {
                Source = "HMI",
                SchStatus = SCCommMsg.ScheduleStatus.DELETE,
                EntryCoilID = "HE1807002700",
                OperatorID = "SP",
                ReasonCode = "0ABC",
            };
            msg.SchStatus = SCCommMsg.ScheduleStatus.DELETE; 
            MQPoolService.SendToCoil(InfoCoil.DeleteCoilSchedule.Data(msg));           
        }

        // 開始供料
        private void btnCoilEnterStar_Click(object sender, EventArgs e)
        {
            var msg = new SCCommMsg.CS10_Coil_AutoFeedModeChange()
            {
                Source = "HMI",
                ID = "Coil_AutoFeedModeChange"
            };
            MQPoolService.SendToTrk(InfoTrk.CheckCoilEnterInfo.Data(msg));
        }

        // 停止供料
        private void btnCoilEnterEnd_Click(object sender, EventArgs e)
        {
        
        }

        // 鋼捲退料
        private void btnReturnCoil_Click(object sender, EventArgs e)
        {
            var msg = new SCCommMsg.CS05_RejectCoil()
            {               
                 Source = "HMI",
                 ID = "0",
                 CoilID = "HE2001103010000",
                 Saddle = "123",
            };


            MQPoolService.SendToTrk(InfoTrk.ReturnCoil.Data(msg));
        }

        // 鋼捲缺陷維護和封鎖標記確認作業
        private void btnDefectCoilSnd_Click(object sender, EventArgs e)
        {
        
        }
        
        


        // 鋼捲追蹤身分確認作業
        private void btnScan_Click(object sender, EventArgs e)
        {

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

            //MQPoolService.SendToBCScnRcvEdit(InfoBCScn.ScanEntryCoilNo.Data(bcs));
        }

        private void btnDeliveryScn_Click(object sender, EventArgs e)
        {
            if (txtEntryCoilNo.Text.Equals(""))
            {
                MessageBox.Show("請輸入鋼捲編號");
                return;
            }

            var bcs = new BarCodeScnContent
            {
                ScanCoilNo = txtDeliveryCoilNo.Text,
                ScanPosition = DetPos(DeliveryComboBox.Text),
            };

            //MQPoolService.SendToBCScnRcvEdit(InfoBCScn.ScanDeliveryCoilNo.Data(bcs));
        }

        private SKPOS DetPos(string pos)
        {
            SKPOS POS = SKPOS.EntryTOP;

            switch (pos)
            {
                case "ESK1":
                    POS = SKPOS.Entry_SK01;
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

        private void btnPrintCheck_Click(object sender, EventArgs e)
        {
//<<<<<<< HEAD
//            var craneEntryCoil = new SC06_CraneEntryCoil("", CoilSkPosition.DSK01);
//            MQPoolUtil.SendToPCCom(InfoHMI.CraneEntryCoil.Data(craneEntryCoil));
//=======
//            var ManualPrintCheck = new SCCommMsg.CS07_PrintLabel
//            {
//                Source = "CPL1",
//                ID = "A1",
//                CoilID = "CE0000000001"
//            };

//            MQPoolService.SendToLpr(InfoLpr.ManualPrint.Data(ManualPrintCheck));
//>>>>>>> 功能修改
        }

        private void btnPDOSnd_Click(object sender, EventArgs e)
        {
            var sndPDO = new SCCommMsg.CS06_SendMMSPDO
            {
                Source = "CPL1HMI",
                ID = "A1",
                Coil_ID = "CM200113010000"
            };

            MQPoolService.SendToCoil(InfoCoil.AskSndPDO.Data(sndPDO));
        }

        private void btnScnFailSelect_Click(object sender, EventArgs e)
        {

        }

        private void btnAskMMSSchedule_Click(object sender, EventArgs e)
        {
            var askSchedule = new SCCommMsg.CS01_AckSchedule
            {
                Source = "CPL1HMI",
                ID = "A1",             
            };

       
            MQPoolService.SendToCoil(InfoCoil.AskCoilSchedule.Data(askSchedule));
        }

        private void btnAskPDI_Click(object sender, EventArgs e)
        {
            var askPDI = new SCCommMsg.CS02_AckPDI
            {
                Source = "CPL1HMI",
                ID = "A1",
            };


            MQPoolService.SendToCoil(InfoCoil.AskPDI.Data(askPDI));
        }



        [Obsolete]
        public void SetPresenter(BaseFormContract.IPresenter presenter)
        {
            throw new NotImplementedException();
        }
    }
}

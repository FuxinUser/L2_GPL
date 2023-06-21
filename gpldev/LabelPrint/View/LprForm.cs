using Akka.Actor;
using AkkaSysBase;
using Core.Help;
using LabelPrint.Actor;
using LabelPrint.Printer;
using System;
using WinformsMVP.Controls.Forms;
using WinformsMVP.Controls.Forms.Base;
using static LabelPrint.Model.ZebraModel;

namespace LabelPrint.View
{
    public partial class LprForm : BaseForm, LprContract.IView
    {

        public LprContract.IPresenter Presenter { get; set; }

        private ISysAkkaManager _akkaManager;

        public LprForm(LprContract.IPresenter presenter, ISysAkkaManager akkaManager)
        {
            InitializeComponent();
            Presenter = presenter;
            Presenter.SetView(this);

            _akkaManager = akkaManager;
        }


        private void LprForm_Shown(object sender, EventArgs e)
        {
            tbRemoteIp.Text = IniSystemHelper.Instance.PrinterRemoteIP;
            tbRemotePort.Text = IniSystemHelper.Instance.PrinterRemotePort.ToString();
        }

        [Obsolete]
        public void SetPresenter(BaseFormContract.IPresenter presenter)
        {
            throw new NotImplementedException();
        }

        private void btnPrinterCoilLabel_Click(object sender, EventArgs e)
        {
            var coilID = textCoil.Text;
            var zcmd = new ZebraCommand();
            zcmd.ZPL = coilID.zplCmd();

            _akkaManager.GetActor(nameof(PinterClient)).Tell(zcmd);
        }


        private void btnPrinterSampleLabel_Click_1(object sender, EventArgs e)
        {
            var coilID = textCoil.Text;
            var thick = textThick.Text + "mm";
            var sampleNo = textSample.Text;

            var zcmd = new ZebraCommand();
            zcmd.ZPL = coilID.zplQRCmd(thick, sampleNo);

            _akkaManager.GetActor(nameof(PinterClient)).Tell(zcmd);
        }
    }
}

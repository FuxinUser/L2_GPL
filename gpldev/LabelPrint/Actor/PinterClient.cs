using AkkaSysBase;
using AkkaSysBase.Base;
using LabelPrint.Model;
using LabelPrint.Printer;
using LabelPrint.View;
using LogSender;
using static LabelPrint.Model.ZebraModel;

namespace LabelPrint.Actor
{
    public class PinterClient : BaseActor
    {

        private Zebra ZebraClient;

        private LprContract.IPresenter Presenter;

        public PrinterStatus Status;

        public PinterClient(ISysAkkaManager akkaManager, LprContract.IPresenter presenter, Zebra zebraClient, ILog log) : base(log)
        {
            Presenter = presenter;

            ZebraClient = zebraClient;
            ZebraClient.StateChange += PrinterStateChanged;

            //Presenter.ContNumber(123);

            Receive<ZebraCommand>(message => Print(message));

        }


        private void PrinterStateChanged()
        {
            Status = new PrinterStatus()
            {
                IsConnected = ZebraClient.State_Connected,
                IsPause = ZebraClient.State_Pause,
                IsPaperOut = ZebraClient.State_PaperOut,
                IsRibbonOut = ZebraClient.State_RibbonOut,
                IsError = ZebraClient.State_Error,
                ErrorNum = ZebraClient.State_ErrorNum,
                IsWarning = ZebraClient.State_Warning,
                WarningNum = ZebraClient.State_WarningNum,
            };

            _log.I("印表機狀態", $"是否連線{Status.IsConnected}");
            _log.I("印表機狀態", $"是否暫停{Status.IsPause}");
            _log.I("印表機狀態", $"是否缺紙{Status.IsPaperOut}");
            _log.I("印表機狀態", $"是否缺碳粉{Status.IsRibbonOut}");
        }

        /// <summary>
        /// 列印
        /// </summary>
        /// <param name="telegram"></param>
        public bool Print(ZebraCommand msg)
        {
            bool isOk = Status.IsConnected && !Status.IsPause;

            if (!isOk)
            {
                _log.E("列印錯誤", $"印表機連線狀態是否連線{Status.IsConnected}, 印表機狀態是否為暫停{Status.IsPause}");
                return false;
            }

            // 列印
            ZebraClient.SendZPL(msg.ZPL);

            return true;
        }

    }
}

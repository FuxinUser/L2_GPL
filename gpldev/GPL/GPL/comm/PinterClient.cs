using Akka.Actor;
using Akka.Event;
using LabelPrint.Model;
using LabelPrint.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabelPrint.Model.ZebraModel;

namespace GPLManager
{
    public class PinterClient : ReceiveActor
    {
        private ILoggingAdapter _log = Logging.GetLogger(Context);
        private Zebra ZebraClient;

        //private LprContract.IPresenter Presenter;

        public PrinterStatus Status;

        public PinterClient(ILoggingAdapter log, Zebra zebraClient)
        {
            _log = log;

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

            _log.Info("印表機狀態", $"是否連線{Status.IsConnected}");
            _log.Info("印表機狀態", $"是否暫停{Status.IsPause}");
            _log.Info("印表機狀態", $"是否缺紙{Status.IsPaperOut}");
            _log.Info("印表機狀態", $"是否缺碳粉{Status.IsRibbonOut}");
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
                _log.Error("列印錯誤", $"印表機連線狀態是否連線{Status.IsConnected}, 印表機狀態是否為暫停{Status.IsPause}");
                return false;
            }

            // 列印
            ZebraClient.SendZPL(msg.ZPL);

            return true;
        }
    }
}

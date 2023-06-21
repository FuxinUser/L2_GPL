using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using LogSender;
using MsgConvert.Msg;
using static DataModel.HMIServerCom.Msg.SCCommMsg;

namespace BCScnMgr
{
    public class BCScnSndEdit : BaseActor
    {

        private IActorRef _conActor;

        public BCScnSndEdit(ISysAkkaManager akkaManager, ILog log) : base(log)
        {
            _conActor = akkaManager.GetActor(nameof(BCScnConn));
            Receive<SC01_ScnBarcodeID>(message => DetScanResult(message));
            ReceiveAny(message => RcvObject(message));
        }


        private void DetScanResult(SC01_ScnBarcodeID scanResult)
        {
            var result = BCSFactory.ScanResult(scanResult.ScanResult == BCScanResult.Sucess, scanResult.CoilNo);
            _conActor.Tell(result);
        }

        private void RcvObject(object msg)
        {
            _log.E("【AThread接收資料-RcvObject】", $"VaildActor 無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }
    }
}

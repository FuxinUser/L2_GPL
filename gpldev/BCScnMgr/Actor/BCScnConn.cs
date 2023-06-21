using Akka.Actor;
using Akka.IO;
using AkkaSysBase;
using AkkaSysBase.Base;
using Core.Util;
using LogSender;
using static DataMod.BarCode.BCSModel;

namespace BCScnMgr
{
    public class BCScnConn : BaseServerClientActor
    {

        private ISysAkkaManager _akkaManager;
        private IActorRef _rcvEditActor;


        public BCScnConn(ISysAkkaManager akkaManager, AkkaSysIP akkaSysIp, ILog log) : base(akkaSysIp, log)
        {
            _akkaManager = akkaManager;
            _rcvEditActor = akkaManager.GetActor(nameof(BCScnRcvEdit));


            Receive<CompareScnResult_SB01>(message => TryFlow(() => SndResult(message)));

            //var t = new BarCodeScnContent("CE00100001", SKPOS.DeliveryTop);
            //InfoWMSScanID(t);
        }

        //private void InfoWMSScanID(BarCodeScnContent msg)
        //{

        //    _log.Info("通知WMS掃描ID", $"掃描位置為{msg.GetPOSStr()}鋼捲ID為{msg.ScanCoilNo}");
        //    var scanResult = new ScanResult(msg.GetScanPosition(), msg.ScanCoilNo);
        //    MQPoolUtil.SendToWMS(InfoWMS.InfoBCSScanID.Data(scanResult));
        //}

        protected override void TcpReceivedData(Tcp.Received msg)
        {
            _log.I("TCP接收資料", "Handle_Tcp_Received. message=" + msg.ToString());
            _log.I("TCP接收資料", "ByteString=" + msg.Data.ToString());
            _log.I("TCP接收資料", "Count=" + msg.Data.Count.ToString());


            //_rcvEditActor = Context.ActorOf(Context.System.DI().Props<BCScnRcvEdit>());
            _rcvEditActor.Tell(msg.Data.ToArray());


            //var bc = BCSFactory.ScanResult(true, "CH100100001");
            //var bytes = MsgAnalUtil.RawSerialize(bc);
            //_connection.Tell(Tcp.Write.Create(ByteString.FromBytes(bytes)));

        }

        private void SndResult(CompareScnResult_SB01 scanResult)
        {

            if (_connection == null) {
                _log.E("TCP發送失敗", "未連結BarCode機,請檢察連線");
                return;
            }
            var bytes = scanResult.RawSerialize();
            _connection.Tell(Tcp.Write.Create(ByteString.FromBytes(bytes)));
        }

    }
}

using Akka.Actor;
using Akka.IO;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller.Sys;
using Core.Define;
using Core.Util;
using DataModel.Common;
using DataModel.WMS;
using LogSender;
using System;
using WMSComm.Model;
using static Core.Define.DBParaDef;
using static Core.Define.EventDef;

namespace WMSComm.Actor
{
    public class WMSSnd : BaseClientActor
    {

        private ISysAkkaManager _akkaManager;
        private ISysController _sysController;
        private AggregateService _agService;
        private ICancelable _tmrHeatbeat;

        public WMSSnd(ISysAkkaManager akkaManager, 
                      ISysController sysController, 
                      AkkaSysIP akkaSysIp, 
                      ILog log, 
                      AggregateService agService) : base(akkaSysIp, log)
        {
            agService.InitMMSHeartBeatMsg();
            _agService = agService;     
            _akkaManager = akkaManager;

            _sysController = sysController;
            _sysController.SetLog(log);

            // 開啟底層Queu發送機制
            InitSndQueuMachinesm(agService.appSetting.ReSndCnt, agService.appSetting.DetectSndQueeTime, CMDSET.DETECT_SND_QUEU);

            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectToWMS,
                                      _agService.appSetting.RemoteIp,
                                      _agService.appSetting.RemotePort.ToString(),
                                      ConnectionSysDef.UnConnect);

            Receive<CMDSET>(msg => ProEventCmd(msg));
            //Receive<CommonMsg>(message => SendData(message.Data.ToByteString()));
            Receive<CommonMsg>(message => _sndQueu.Enqueue(message));
            ReceiveAny(message => RcvObject(message));
        }

        protected override void TCPConnected(Tcp.Connected message)
        {

            base.TCPConnected(message);
            if (_tmrHeatbeat == null)
                HeatBeatTmrStart();

            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectToWMS,
                           _agService.appSetting.RemoteIp,
                           _agService.appSetting.RemotePort.ToString(),
                           ConnectionSysDef.Connect);

        }
        protected override void TcpReceivedData(Tcp.Received message)
        {
            var bytes = message.Data.ToArray();

            //只需判斷是否為ACK報文
            var ackMsg = MsgAnalUtil.RawDeserialize(message.Data.ToArray(), typeof(WMS_ACK_Structure)) as WMS_ACK_Structure;

            //_log.Debug("接收應答電文", "應答電文");
            //_isRcvAck = true;
            //SlefTryDequeue();

            if (ackMsg == null)
            {
                _agService.DumpRawData.DumpMsg(bytes, _agService.appSetting.RcvMsgFilePath);
                _log.E("接收應答電文錯誤", $"無法解析應達電文，長度為{bytes.Length}");
                return;
            }


            if (ackMsg.AcceptedOrNot.Equals(MMSSysDef.DataCode.NotAccept))
            {
                _log.E("接收應答電文", $"對方回絕電文，CODE:{ackMsg.ErrorCode.ToStr()} 原因:{ackMsg.ErrorMsg.ToStr()}");
                ReDeQueuSnd();
                return;
            }

            _log.D("接收應答電文", $"應答電文 CODE:{ackMsg.ErrorCode.ToStr()} 原因:{ackMsg.ErrorMsg.ToStr()}");


            _isRcvAck = true;
            SlefTryDequeue();

        }

        protected override void TcpConnectionClosed(Tcp.ConnectionClosed message)
        {
            // 停止心跳
            HeatBeatTmrStop();

            base.TcpConnectionClosed(message);

            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectToWMS,
                              _agService.appSetting.RemoteIp,
                              _agService.appSetting.RemotePort.ToString(),
                              ConnectionSysDef.UnConnect);
        }

        private void ProEventCmd(EventDef.CMDSET msg)
        {
            switch (msg)
            {
                case CMDSET.CHECK_ACK:
                    break;
                case CMDSET.DETECT_SND_QUEU:
                    DeQueuSnd();
                    break;
                case EventDef.CMDSET.SND_HEARTBEAT:
                    SndHeatbeat();
                    break;

            }
        }


        private void SndHeatbeat()
        {
            _log.D("發送心跳文", $"Snd a Heatbeat");
            var heartBeatMsg = _agService.GetNowHeartBeatMsg();
            var hb = MsgAnalUtil.RawSerialize(heartBeatMsg);
            _connection.Tell(Tcp.Write.Create(ByteString.FromBytes(hb)));
        }

        private void HeatBeatTmrStart()
        {

            //if (!_agService.appSetting.isSndHeartBeat)
            //    return;

            _log.I("心跳文機制", "心跳文發送開啟");

            var interval = TimeSpan.FromSeconds(30);

            if (_tmrHeatbeat == null)
                _tmrHeatbeat = _akkaManager.ActorSystem.Scheduler.ScheduleTellRepeatedlyCancelable(interval, interval, Self, EventDef.CMDSET.SND_HEARTBEAT, Self);

        }


        private void SendData(ByteString bs)
        {

            if (_connection == null)
            {
                Connect();
                return;
            }

            _log.A("發送資料給WMS", "發送資料給WMS");
            _connection.Tell(Tcp.Write.Create(bs));

        }

        private void RcvObject(object msg)
        {
            _log.E("AThread接收資料-RcvObject", $"VaildActor 無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }

        public void HeatBeatTmrStop()
        {
            _log.A("心跳文機制", "心跳文發送關閉");

            if (_tmrHeatbeat != null)
            {
                _tmrHeatbeat.Cancel();
                _tmrHeatbeat = null;
            }

        }


    }
}

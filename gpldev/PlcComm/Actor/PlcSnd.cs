using Akka.Actor;
using Akka.IO;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller.Sys;
using Core.Define;
using Core.Util;
using DataModel.Common;
using LogSender;
using PLCComm.Model;
using System;
using System.Linq;
using static Core.Define.DBParaDef;
using static Core.Define.EventDef;
using System.Threading;

/**
 * Author: ICSC 余士鵬
 * Date: 2019/9/19
 * Description:  Plc接收口-負責發送(Snd角色)
 * Reference: 
 * Modified: 
 */
namespace PLCComm.Actor
{
    public class PlcSnd : BaseClientActor
    {
        private ISysController _sysController;
        private readonly bool _isSndHeartbeat;      // 是否開啟心跳(測試用)
        private AggregateService _agService;        // 聚合服務器
        private ICancelable _tmrAliveMsgSnd;        // L2 Alive 心跳發送

        private bool _isSndFail;
        private bool _isHeartBeatSnd = true;

        public PlcSnd(AkkaSysIP akkaSysIp, ISysController sysController, ILog log, AggregateService agService) : base(akkaSysIp, log)
        {
            _log = log;
            _agService = agService;
            _agService.InitAliveMsg();
            _agService.InitAckMsg();
            _isSndHeartbeat = true;

            _sysController = sysController;
            _sysController.SetLog(log);

            // 開啟底層Queu發送機制
            InitSndQueuMachinesm(agService.appSetting.ReSndCnt, agService.appSetting.DetectSndQueeTime, CMDSET.DETECT_SND_QUEU);


            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectToPLC,
                                     _agService.appSetting.RemoteIp,
                                     _agService.appSetting.RemotePort.ToString(),
                                     ConnectionSysDef.UnConnect);

            Receive<CommonMsg>(message => SendData(message));
            //Receive<CommonMsg>(message => _sndQueu.Enqueue(message));
            Receive<CMDSET>(msg => ProEventCmd(msg));
            ReceiveAny(message => RcvObject(message));
        }


      

        /// <summary>
        /// Tcp連線事件觸發
        /// </summary>
        protected override void TCPConnected(Tcp.Connected message)
        {

            base.TCPConnected(message);

            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectToPLC,
                                         _agService.appSetting.RemoteIp,
                                         _agService.appSetting.RemotePort.ToString(),
                                         ConnectionSysDef.Connect);

            //Send Alive Msg in 5s 
            if (_isSndHeartbeat && _tmrAliveMsgSnd == null) StartAliveMsgSndTmr(5, EventDef.CMDSET.SND_HEARTBEAT);

            //若傳送Connection為null 重新連線候補發送 
            if (!_isSndFail)
                return;

            _isSndFail = false;
        }

        //  Cmd事件觸發
        private void ProEventCmd(EventDef.CMDSET msg)
        {
            switch (msg)
            {
                case EventDef.CMDSET.ACK_TIMEOUT:
                    //L1沒有ACK機制.
                    break;
                case EventDef.CMDSET.SND_HEARTBEAT:
                    // 發送Alive
                    SndAliveMsg();
                    break;

                // 心跳開關
                case EventDef.CMDSET.HEART_BEAT_OPEN:
                    // 開啟心跳發送
                    StartAliveMsgSndTmr(5, EventDef.CMDSET.SND_HEARTBEAT);
                    break;
                case EventDef.CMDSET.HEART_BEAT_CLOSE:
                    // 取消心跳發送
                    CloseAliveMsgSndTmr();
                    break;
                case EventDef.CMDSET.DETECT_SND_QUEU:
                    DeQueuSnd();
                    break;
            }
        }

        /// <summary>
        /// TCP 接收事件
        /// </summary>
        protected override void TcpReceivedData(Tcp.Received msg)
        {

            _log.D("TCP接收資料", "Msg=" + msg.ToString());
         
            var resAck = msg.Data.ToArray();

            if(resAck.SequenceEqual(_agService.L1AckMsg))
            {
                _log.D("接收應答電文", "應答電文");
                
                _isRcvAck = true;

                if (_isHeartBeatSnd)
                {
                    _isHeartBeatSnd = false;
                }
                else
                {
                    SlefTryDequeue();
                }                
            }

        }

        protected override void TcpConnectionClosed(Tcp.ConnectionClosed message)
        {

            if (_tmrAliveMsgSnd != null)
                _tmrAliveMsgSnd.Cancel();

            base.TcpConnectionClosed(message);


            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectToPLC,
                                                   _agService.appSetting.RemoteIp,
                                                   _agService.appSetting.RemotePort.ToString(),
                                                   ConnectionSysDef.UnConnect);
        }

        /// <summary>
        /// 角色接收無法解析資料事件
        /// </summary>
        private void RcvObject(object msg)
        {
            _log.E("AThread接收資料-RcvObject", $"VaildActor 無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }

        /// <summary>
        /// 發送資料給一級
        /// </summary>
        private void SendData(CommonMsg msg)
        {

            if (_connection == null)
            {
                _isSndFail = true;
                Connect();
                return;
            }



            _log.A($"發送資料{msg.Message_Id}給L1", "發送資料給L1");
            SendObjMsg(Tcp.Write.Create(msg.Data.ToByteString()));
            //_connection.Tell(Tcp.Write.Create(msg.Data.ToByteString()));
            // Thread.Sleep(100); //延遲100毫秒
            //if (msg.Message_Id.Trim() == "204" || msg.Message_Id.Trim() == "205")
            //{
            //    _log.I($"發送報文{msg.Message_Id}後延遲一秒","");
            //    Thread.Sleep(1000); //延遲1sec
            //}
        }


        /// <summary>
        /// 開始Aive
        /// </summary>
        private void StartAliveMsgSndTmr(int second, object message)
        {
            var interval = TimeSpan.FromSeconds(second);
            _tmrAliveMsgSnd = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable( interval, interval, Self, message, Self);

        }

        /// <summary>
        /// 發送L2 Alive
        /// </summary>
        private void SndAliveMsg()
        {
            if (_connection == null)
                return;
            var alive = _agService.GetNowAliveMsg().RawSerialize(false);
            SendObjMsg(Tcp.Write.Create(ByteString.FromBytes(alive)));
            //_connection.Tell(Tcp.Write.Create(ByteString.FromBytes(alive)));
            //Thread.Sleep(100); //延遲100毫秒
            //_agService.DumpSndRawData(alive);
            _isHeartBeatSnd = true;
        }

        /// <summary>
        /// 關閉L2 Alive發送Timer
        /// </summary>
        private void CloseAliveMsgSndTmr()
        {
            if (_tmrAliveMsgSnd != null)
                _tmrAliveMsgSnd.Cancel();
        }

        private void SendObjMsg(object Msg)
        {
            _connection.Tell(Msg);
            Thread.Sleep(300); //延遲300毫秒
        }











    }
}

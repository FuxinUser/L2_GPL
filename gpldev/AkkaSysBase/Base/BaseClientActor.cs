using Akka.Actor;
using Akka.IO;
using Core.Define;
using Core.Util;
using DataModel.Common;
using LogSender;
using System;
using System.Collections.Concurrent;
using System.Linq;
using static Core.Define.EventDef;

namespace AkkaSysBase.Base
{
    public class BaseClientActor : BaseActor
    {
        protected IActorRef _connection;
        protected string _actorName;


        private readonly AkkaSysIP _akkaSysIp;

        // Ack 機制使用
        protected int _MAX_SEND_COUNT = 3;                  //  最大重送次數
        protected ICancelable _tmrSnd = null;               //  Scheduler (檢查是否重送訊息)
        protected bool _isDetectAck = false;
        protected bool _isRcvAck = true;                    // 系統初始時是1

        private ICancelable _tryConnection;
        private TCPDef.Statuts _tcpStatuts;

        protected ConcurrentQueue<CommonMsg> _sndQueu;

        public BaseClientActor(AkkaSysIP akkaSysIp, ILog log):base(log)
        {
         
            _akkaSysIp = akkaSysIp;
            _actorName = Context.Self.Path.Name;

            StarTryConnectionTmr();

            Receive<TCPCMDSET>(msg => ProEventCmd(msg));
            Receive<Tcp.Connected>(message => TCPConnected(message));
            Receive<Tcp.Close>(message => TcpClose(message));
            Receive<Tcp.ConnectionClosed>(message => TcpConnectionClosed(message));
            Receive<Tcp.CommandFailed>(message => TcpCommandFailed(message));
            Receive<Tcp.Received>(message => TcpReceivedData(message));
        }

        protected override void PreRestart(Exception reason, object message)
        {
            base.PreRestart(reason, message);
            StopSndTmr();
            StopTryConnectionTmr();         
        }

        /// <summary>
        /// Tcp接收事件觸發
        /// </summary>
        protected virtual void TcpReceivedData(Tcp.Received msg)
        {
            //_log.I("TCP接收資料", "Handle_Tcp_Received. message=" + msg.ToString());
            //_log.I("TCP接收資料", "ByteString=" + msg.Data.ToString());
            _log.I("TCP接收資料", "Count=" + msg.Data.Count.ToString());
        }

        /// <summary>
        /// Tcp連線事件觸發
        /// </summary>
        protected virtual void TCPConnected(Tcp.Connected message)
        {
            _log.I("TCP連線成功", " Connected. message=" + message.ToString());
            _log.I("TCP連線成功", " LocalAddress=" + message.LocalAddress.ToString());
            _log.I("TCP連線成功", " RemoteAddress=" + message.RemoteAddress.ToString());

            StopTryConnectionTmr();
            _tcpStatuts = TCPDef.Statuts.Connectiong;
            _connection = Sender;
            _connection.Tell(new Tcp.Register(Self));

        }

        /// <summary>
        /// Tcp關閉連線事件觸發
        /// </summary>
        protected virtual void TcpConnectionClosed(Tcp.ConnectionClosed message)
        {
            _log.I("TCP連線關閉", " Tcp.ConnectionClosed. message=" + message.ToString());
            _log.I("TCP連線關閉", " Message.Cause=" + message.Cause);
            _log.I("TCP連線關閉", " Message.IsAborted=" + message.IsAborted.ToString());
            _log.I("TCP連線關閉", " Message.IsConfirmed=" + message.IsConfirmed.ToString());
            _log.I("TCP連線關閉", " Message.IsErrorClosed=" + message.IsErrorClosed.ToString());
            _log.I("TCP連線關閉", " Message.IsPeerClosed=" + message.IsPeerClosed.ToString());

            _tcpStatuts = TCPDef.Statuts.Closed;
            StarTryConnectionTmr();
        }

        protected virtual void TcpClose(Tcp.Close message)
        {
            _log.A("TCP連線關閉", " Message.IsPeerClosed=" + message.Event);
            //_tcpStatuts = TCPDef.Statuts.Closed;
        }
        /// <summary>
        /// Tcp操作事件失敗觸發
        /// </summary>
        protected virtual void TcpCommandFailed(Tcp.CommandFailed message)
        {

            _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + message.ToString());
            _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + message.ToString());
            _log.E("TCP操作失敗", " Cmd=" + message.Cmd.ToString());
            _log.E("TCP操作失敗", " Message=" + message.Cmd.FailureMessage);

            _tcpStatuts = TCPDef.Statuts.Error;
        }

        private void ProEventCmd(EventDef.TCPCMDSET msg)
        {
            switch (msg)
            {
                case EventDef.TCPCMDSET.CLIENT_TRY_CONNECTION:
                    Connect();
                    break;
            }

        }

        /// <summary>
        ///     連線
        /// </summary>
        protected void Connect()
        {
            if (_tcpStatuts == TCPDef.Statuts.Connectiong)
                return;

            _log.I("TCP連線操作", " TCP Connect");

            Sender?.Tell(Tcp.Close.Instance, Sender);
            _connection = null;
            Context.System.Tcp().Tell(new Tcp.Connect(_akkaSysIp.RemoteIpEndPoint));

            _tcpStatuts = TCPDef.Statuts.Connectiong;

        }

        protected void DeQueuSnd()
        {
            //if (!_isRcvAck)
            //    return;

            //  若 queue 沒資料了就結束
            if (_sndQueu.Count <= 0)
                return;

            if (_connection == null)
            {
                _log.E("無法發送", "無法發送,未連線成功");
                return;
            }

            // 取出不移除
            var msg = SlefTryPeek();
            if (msg == null)
            {
                _log.E("取SndQueu失敗", $"目前Queu資料數目 = ${_sndQueu.Count}");
                return;
            }

            //  還在等待就計算等待次數 (0 表示不使用)，超過次數就放棄處理下一個
            if (msg.ReSndCnt >= _MAX_SEND_COUNT)
            {
                _log.E("發送失敗超過三次", $"拋棄資料{msg.Message_Id}  MsgLength = {msg.Message_Length}  目前Queu資料數目 = ${_sndQueu.Count}");
                SlefTryDequeue();
                _isRcvAck = true;
                return;
            }

            _log.I($"使用 Queue 發送電文", $"MsgID = {msg.Message_Id} MsgLength = {msg.Message_Length} 實際Cnt = {msg.Data.Count()}");
            _connection.Tell(Tcp.Write.Create(msg.Data.ToByteString()));

            msg.ReSndCnt++;
            _isRcvAck = false;        
        }

        protected void ReDeQueuSnd()
        {
            //  若 queue 沒資料了就結束
            if (_sndQueu.Count <= 0)
                return;

            if (_connection == null)
            {
                _log.E("無法發送", "無法發送,未連線成功");
                return;
            }

            // 取出不移除
            var msg = SlefTryPeek();
            if (msg == null)
            {
                _log.E("取SndQueu失敗", $"目前Queu資料數目 = ${_sndQueu.Count}");
                return;
            }

            //  還在等待就計算等待次數 (0 表示不使用)，超過次數就放棄處理下一個
            if (msg.ReSndCnt >= _MAX_SEND_COUNT)
            {
                _log.E("發送失敗超過三次", $"拋棄資料{msg.Message_Id}  MsgLength = {msg.Message_Length}  目前Queu資料數目 = ${_sndQueu.Count}");
                SlefTryDequeue();
                _isRcvAck = true;
                return;
            }

            _log.I($"使用 Queue 發送電文", $"MsgID = {msg.Message_Id} MsgLength = {msg.Message_Length} 實際Cnt = {msg.Data.Count()}");
            _connection.Tell(Tcp.Write.Create(ByteString.FromBytes(msg.Data)));

            msg.ReSndCnt++;
            _isRcvAck = false;
        }
        protected void InitSndQueuMachinesm(int resndCnt, int timerNum, object akkSekfRcvObject, bool isDetectAck = false)
        {
            _sndQueu = new ConcurrentQueue<CommonMsg>();
            _MAX_SEND_COUNT = resndCnt;
            isDetectAck = _isDetectAck;
            _isRcvAck = true;
            StartSndTmr(akkSekfRcvObject, timerNum);
        }

        /// <summary>
        ///     開始檢查重新發送電文
        /// </summary>
        protected void StartSndTmr(object akkaEventObject, int timerNum)
        {
            if (_tmrSnd == null)
                _tmrSnd = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(
                    TimeSpan.FromSeconds(0),
                    TimeSpan.FromMilliseconds(timerNum),
                    Self,
                    akkaEventObject,
                    Self);
        }

        /// <summary>
        ///     停止檢查重新發送電文
        /// </summary>
        protected void StopSndTmr()
        {
            if (_tmrSnd == null)
                return;

            _tmrSnd.Cancel();
            _tmrSnd = null;
        }


        protected CommonMsg SlefTryPeek()
        {
            CommonMsg commonMsg;
            _sndQueu.TryPeek(out commonMsg);
            return commonMsg;
        }

        protected CommonMsg SlefTryDequeue()
        {
            CommonMsg commonMsg;
            _sndQueu.TryDequeue(out commonMsg);
            return commonMsg;
        }

        private void StarTryConnectionTmr()
        {
            if (_tryConnection != null)
            {
                _log.A("已開啟嘗試連線", "已開啟嘗試連線");
                return;
            }

            _tcpStatuts = TCPDef.Statuts.Open;

            _log.I("開啟嘗試連線", "開啟嘗試連線");

            var interval = TimeSpan.FromSeconds(5);

            _tryConnection = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(interval, interval, Self, EventDef.TCPCMDSET.CLIENT_TRY_CONNECTION, Self);

        }

        public void StopTryConnectionTmr()
        {
            _log.I("開啟嘗試連線關閉", "開啟嘗試連線關閉");


            if (_tryConnection != null)
            {
                _tryConnection.Cancel();
                _tryConnection = null;
            }

        }
    }
}

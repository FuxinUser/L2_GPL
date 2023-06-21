using Akka.Actor;
using Akka.IO;
using LogSender;
using System;

/**
 * Author: ICSC余士鵬
 * Date: 2019/9/19
 * Description: Server Akka底層(TCP操作相關事件處理)
 * Reference: 
 * Modified: 
 */

namespace AkkaSysBase.Base
{
    public class BaseServerActor : BaseActor
    {

        public BaseServerActor(AkkaSysIP akkaSysIp, ILog log) : base(log)
        {
            Context.System.Tcp().Tell(new Tcp.Bind(Self, akkaSysIp.LocalIpEndPoint));

            Receive<Tcp.Bound>(message => TcpBound(message));
            Receive<Tcp.Connected>(message => TCPConnected(message));
            Receive<Tcp.CommandFailed>(msg => TcpCommandFailed(msg));
            Receive<Tcp.ConnectionClosed>(message => TcpConnectionClosed(message));
            Receive<Tcp.Received>(message => TcpReceivedData(message));
        }

        /// <summary>
        /// Tcp監聽事件觸發
        /// </summary>
        private void TcpBound(Tcp.Bound msg)
        {            
            _log.I("TCP監聽", "Tcp.Bound Success. Listening on " + msg.LocalAddress);
        }

        /// <summary>
        /// Tcp接收事件觸發
        /// </summary>
        protected virtual void TcpReceivedData(Tcp.Received msg)
        {
            _log.I("TCP接收資料", "Handle_Tcp_Received. message=" + msg.ToString());
            _log.I("TCP接收資料", "ByteString=" + msg.Data.ToString());
            _log.I("TCP接收資料", "Count=" + msg.Data.Count.ToString());
        }

        /// <summary>
        /// Tcp被連線事件觸發
        /// </summary>
        protected virtual void TCPConnected(Tcp.Connected msg)
        {
            _log.I("TCP已被連線", " Tcp.Connected. message=" + msg.ToString());
            _log.I("TCP已被連線", " message.LocalAddress=" + msg.LocalAddress.ToString());
            _log.I("TCP已被連線", " message.RemoteAddress=" + msg.RemoteAddress.ToString());
            Sender.Tell(new Tcp.Register(Self));
         
        }

        /// <summary>
        /// Tcp關閉連線事件觸發
        /// </summary>
        protected virtual void TcpConnectionClosed(Tcp.ConnectionClosed msg)
        {
            _log.I("TCP連線關閉", " Tcp.ConnectionClosed. message=" + msg.ToString());
            _log.I("TCP連線關閉", " Message.Cause=" + msg.Cause);
            _log.I("TCP連線關閉", " Message.IsAborted=" + msg.IsAborted.ToString());
            _log.I("TCP連線關閉", " Message.IsConfirmed=" + msg.IsConfirmed.ToString());
            _log.I("TCP連線關閉", " Message.IsErrorClosed=" + msg.IsErrorClosed.ToString());
            _log.I("TCP連線關閉", " Message.IsPeerClosed=" + msg.IsPeerClosed.ToString());
        }

        /// <summary>
        /// Tcp操作事件失敗觸發
        /// </summary>
        protected virtual void TcpCommandFailed(Tcp.CommandFailed msg)
        {
            _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + msg.ToString());
            _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + msg.ToString());
            _log.E("TCP操作失敗", " Cmd=" + msg.Cmd.ToString());
            _log.E("TCP操作失敗", " Message=" + msg.Cmd.FailureMessage);
        }

        /* Actor 重啟或停止的調用事件 */

        /// <summary>
        ///     停止時調用
        /// </summary>
        protected override void PostStop()
        {
            base.PostStop();

            if (Self != null)
                Self.Tell(Tcp.Close.Instance);
        }

        /// <summary>
        ///     重啟之後調用
        /// </summary>
        protected override void PostRestart(Exception reason)
        {
            base.PostRestart(reason);

            if (Self != null)
                Self.Tell(Tcp.Close.Instance);
        }

    }
}

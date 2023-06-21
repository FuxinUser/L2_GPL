﻿using Akka.Actor;
using Akka.IO;
using LogSender;

namespace AkkaSysBase.Base
{
    // 單線雙向
    public class BaseServerClientActor : BaseActor
    {
        protected IActorRef _connection;
        protected string _actorName;

        private readonly AkkaSysIP _akkaSysIp;

        public BaseServerClientActor(AkkaSysIP akkaSysIp, ILog log) : base(log)
        {

            _akkaSysIp = akkaSysIp;
            _actorName = Context.Self.Path.Name;

            Context.System.Tcp().Tell(new Tcp.Bind(Self, akkaSysIp.LocalIpEndPoint));

            Receive<Tcp.Bound>(message => TcpBound(message));
            Receive<Tcp.Connected>(message => TCPConnected(message));
            Receive<Tcp.ConnectionClosed>(message => TcpConnectionClosed(message));
            Receive<Tcp.CommandFailed>(message => TcpCommandFailed(message));
            Receive<Tcp.Received>(message => TcpReceivedData(message));
        }

        private void TcpBound(Tcp.Bound msg)
        {
            _log.I("TCP監聽", "Tcp.Bound Success. Listening on " + msg.LocalAddress);
        }
        protected virtual void TcpReceivedData(Tcp.Received msg)
        {
            _log.I("TCP接收資料", "Handle_Tcp_Received. message=" + msg.ToString());
            _log.I("TCP接收資料", "ByteString=" + msg.Data.ToString());
            _log.I("TCP接收資料", "Count=" + msg.Data.Count.ToString());
        }
        protected virtual void TCPConnected(Tcp.Connected message)
        {
            _log.I("TCP連線成功", " Connected. message=" + message.ToString());
            _log.I("TCP連線成功", " LocalAddress=" + message.LocalAddress.ToString());
            _log.I("TCP連線成功", " RemoteAddress=" + message.RemoteAddress.ToString());

            _connection = Sender;
            _connection.Tell(new Tcp.Register(Self));

        }
        protected virtual void TcpConnectionClosed(Tcp.ConnectionClosed message)
        {
            _log.I("TCP連線關閉", " Tcp.ConnectionClosed. message=" + message.ToString());
            _log.I("TCP連線關閉", " Message.Cause=" + message.Cause);
            _log.I("TCP連線關閉", " Message.IsAborted=" + message.IsAborted.ToString());
            _log.I("TCP連線關閉", " Message.IsConfirmed=" + message.IsConfirmed.ToString());
            _log.I("TCP連線關閉", " Message.IsErrorClosed=" + message.IsErrorClosed.ToString());
            _log.I("TCP連線關閉", " Message.IsPeerClosed=" + message.IsPeerClosed.ToString());
            _connection.Tell(Tcp.Close.Instance);
        }
        protected virtual void TcpCommandFailed(Tcp.CommandFailed message)
        {

            _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + message.ToString());
            _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + message.ToString());
            _log.E("TCP操作失敗", " Cmd=" + message.Cmd.ToString());
            _log.E("TCP操作失敗", " Message=" + message.Cmd.FailureMessage);
        }


       
    }
}

using Akka.Actor;
using Akka.IO;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller.Sys;
using Core.Util;
using DataMod.PLC;
using LogSender;
using PLCComm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using static Core.Define.DBParaDef;

/**
 * Author: ICSC余士鵬
 * Date: 2019/9/19
 * Description: Plc接收口-負責接收(Rcv角色)
 * Reference: 
 * Modified: 
 */
namespace PLCComm.Actor
{
    public class PlcCycleRcv : BaseServerActor
    {
        private IActorRef _tcpRcvDataVaildActor;        // 資料驗證角色
        private ISysAkkaManager _akkaManager;           // 系統管理者
        private AggregateService _agService;            // 聚合服務器
        private ISysController _sysController;

        public PlcCycleRcv(ISysAkkaManager akkaManager,
                           ISysController sysController, 
                           AkkaSysIP akkaSysIp, 
                           ILog log, 
                           AggregateService agService) : base(akkaSysIp, log)
        {
            _akkaManager = akkaManager;
            _agService = agService;

            _sysController = sysController;
            _sysController.SetLog(log);

            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectedByPLCCycle,
                                 _agService.appSetting.CycleRcvLocalIp,
                                 _agService.appSetting.CycleRcvLocalPort.ToString(),
                                 ConnectionSysDef.UnConnect);

            Receive<string>(message => _tcpRcvDataVaildActor.Tell(message));
        }

        /// <summary>
        /// Tcp連線事件觸發
        /// </summary>
        protected override void TCPConnected(Tcp.Connected msg)
        {
            _log.I("TCP已被連線", " Tcp.Connected. message=" + msg.ToString());
            _log.I("TCP已被連線", " message.LocalAddress=" + msg.LocalAddress.ToString());
            _log.I("TCP已被連線", " message.RemoteAddress=" + msg.RemoteAddress.ToString());

            // 資料驗證角色停止並取消回收
            if (_tcpRcvDataVaildActor != null)
                Context.Stop(_tcpRcvDataVaildActor);

            // 建立資料驗證角色
            _tcpRcvDataVaildActor = Context.ActorOf(Props.Create(() => new TCPRcvDataVaildActor(_akkaManager, _sysController, _log, Sender, _agService)));

            // TCP註冊驗證角色 (監聽觸發交接授予)
            // 後續接收斷線觸發時間由RcvDataVaildActor觸發
            Sender.Tell(new Tcp.Register(_tcpRcvDataVaildActor));

            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectedByPLCCycle,
                       _agService.appSetting.CycleRcvLocalIp,
                       _agService.appSetting.CycleRcvLocalPort.ToString(),
                       ConnectionSysDef.Connect);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _log.E("AThread生命週期", Context.System.Name + " PreRestart");
            _log.E("AThread生命週期", "Reason:" + reason.Message);

            // 取消(關閉)TCP註冊
            if (Self != null)
                Sender.Tell(Tcp.Close.Instance);

        }

        /// <summary>
        /// 驗證接收資料正確性
        /// </summary>
        public class TCPRcvDataVaildActor : ReceiveActor
        {

            private AggregateService _agService;   // 聚合服務器
            private IActorRef _rcvEditActor;       // 資料解析角色
            private IActorRef _connection;         // TCP角色
            private ILog _log;                     // Nlog
            private ISysController _sysController;

            // 解析PLC報文
            private ByteString _byteKeeper;
            private List<byte> _listByte;
            private bool _isRcvDataHandling;
            public int _analysisFailCnt;


            public TCPRcvDataVaildActor(ISysAkkaManager akkaManager, ISysController sysController, ILog log, IActorRef connection, AggregateService agService)
            {
                _rcvEditActor = akkaManager.GetActor(nameof(PlcRcvEdit));
                _connection = connection;
                _log = log;
                _agService = agService;
                _sysController = sysController;

                _byteKeeper = ByteString.Empty;
                _isRcvDataHandling = false;

                _listByte = new List<byte>();

                Receive<Tcp.Received>(message => TcpReceivedData(message));
                Receive<Tcp.CommandFailed>(message => TcpCommandFailed(message));
                Receive<Tcp.ConnectionClosed>(message => TcpConnectionClosed(message));

                Receive<string>(message => ClearManualBuffer());
                ReceiveAny(message => RcvObject(message));

            }

            private void ClearManualBuffer()
            {
                _listByte.Clear();
                _log.A("清空Buffer", "清空Buffer");
            }

            /// <summary>
            /// TCP 接收事件
            /// </summary>
            private void TcpReceivedData(Tcp.Received msg)
            {

                _log.D("TCP接收資料", "Msg=" + msg.ToString());
                _log.D("TCP接收資料", "ByteString=" + msg.Data.ToString() + "Count=" + msg.Data.Count.ToString());
                ProTCPRcvData(msg);

            }

            /// <summary>
            ///  TCP 連線關閉事件
            /// </summary>
            private void TcpConnectionClosed(Tcp.ConnectionClosed msg)
            {
                _log.I("TCP連線關閉", " Tcp.ConnectionClosed. message=" + msg.ToString());
                _log.I("TCP連線關閉", " Message.Cause=" + msg.Cause);
                _log.I("TCP連線關閉", " Message.IsAborted=" + msg.IsAborted.ToString());
                _log.I("TCP連線關閉", " Message.IsConfirmed=" + msg.IsConfirmed.ToString());
                _log.I("TCP連線關閉", " Message.IsErrorClosed=" + msg.IsErrorClosed.ToString());
                _log.I("TCP連線關閉", " Message.IsPeerClosed=" + msg.IsPeerClosed.ToString());

                _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectedByPLCCycle,
                             _agService.appSetting.CycleRcvLocalIp,
                             _agService.appSetting.CycleRcvLocalPort.ToString(),
                             ConnectionSysDef.UnConnect);
            }

            /// <summary>
            ///   TCP 操作事件(Connect...Send...)
            /// </summary>
            private void TcpCommandFailed(Tcp.CommandFailed msg)
            {
                _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + msg.ToString());
                _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + msg.ToString());
                _log.E("TCP操作失敗", " Cmd=" + msg.Cmd.ToString());
                _log.E("TCP操作失敗", " Message=" + msg.Cmd.FailureMessage);
            }


            /// <summary>
            /// 角色接收無法解析資料事件
            /// </summary>
            private void RcvObject(object msg)
            {
                _log.E("AThread接收資料-RcvObject", $"VaildActor 無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
            }

            private void ProTCPRcvData(Tcp.Received msg)
            {

                // 紀錄接收資料      
                var bytes = msg.Data.ToArray();
                _log.D("TCP接收資料", "Byte=" + Environment.NewLine + bytes);

                _agService.DumpDebugRawData(bytes);
    
                // 加入Buffer  
                _listByte.AddRange(bytes);

                // 應答
                var responese = new byte[2];
                responese[0] = 0x06;
                responese[1] = 0x00;

                _connection.Tell(Tcp.Write.Create(responese.ToByteString()));

                while (_listByte.Count() > 0)
                {
                  
                    var header = MsgAnalUtil.RawDeserialize(_listByte.ToArray(), typeof(PLCHeader)) as PLCHeader;

                    if (header == null)
                    {
                        _agService.DumpFailRawData(bytes);
                        ClearTcpRcvBuffer();
                        break;
                    }
                     

                    var msgLen = header.MessageLength;
                    var msgID = header.MessageId;

                    if (!CheckMsgLenByDoc(msgLen, msgID))
                    {

                        _agService.DumpFailRawData(bytes);
                        _analysisFailCnt++;
                        ClearTcpRcvBuffer();
                        break;
                    }

                    // 報文合法

                    _log.D("驗證報文成功", $"MsgID : {msgID}, Length : {msgLen}");

                    var data = _listByte.GetRange(0, msgLen).ToArray();
                    _agService.DumpRcvRawData(data);
                    _rcvEditActor.Tell(data);

                    _listByte.RemoveRange(0, msgLen);

                }


            }

            private void ClearTcpRcvBuffer()
            {
                // Test
                _analysisFailCnt = 0;
                _listByte.Clear();
                _log.E("清空接收Buffer", $"報文解析不出次數{_analysisFailCnt}過高清空接收Buffer");

                //if (_analysisFailCnt > 3)
                //{
                //    _log.Error("清空接收Buffer", $"報文解析不出次數{_analysisFailCnt}過高清空接收Buffer");
                //    _analysisFailCnt = 0;
                //    _listByte.Clear();
                //}
            }


            /// <summary>
            /// 確定接收訊息長度
            /// </summary>
            private bool CheckMsgLenByDoc(short message_len, short message_id)
            {
                var hasMsgCode = _agService.MsgLengthAndTypeDic.PlcMsgLen.ContainsKey(message_id);
                if (!hasMsgCode)
                {
                    _log.E("驗證報文失敗", $"接收到報文 MsgID:{message_id} 接收Len:{message_len} 系統無此報文編號");
                    return false;
                }

                var msgLen = _agService.MsgLengthAndTypeDic.PlcMsgLen[message_id];

                if (msgLen != message_len)
                {
                    _log.E("驗證報文失敗", $"接收到報文 MsgID:{message_id} 接收Len:{message_len} 合法Len:{msgLen}");
                    return false;
                }

                return true;
            }
        }
     }
}

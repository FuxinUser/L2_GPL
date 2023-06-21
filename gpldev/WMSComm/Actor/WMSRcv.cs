using Akka.Actor;
using Akka.IO;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller.MsgPro;
using Controller.Sys;
using Core.Define;
using Core.Util;
using DataModel.Common;
using DataModel.WMS;
using LogSender;
using System;
using System.Collections.Generic;
using System.Linq;
using WMSComm.Model;
using static Core.Define.DBParaDef;

namespace WMSComm.Actor
{
    public class WMSRcv : BaseServerActor
    {

        private class DoTcpConnet { }

        private IActorRef _tcpRcvDataVaildActor;
        private ISysAkkaManager _akkaManager;
        private IMsgProController _msgProController;
        private ISysController _sysController;
        private ICancelable _tmrTcpConnect;
        private IActorRef _tempSender;


        private AggregateService _agService;
        

        public WMSRcv(ISysAkkaManager akkaManager, 
                      ISysController sysController, 
                      AkkaSysIP akkaSysIp, 
                      ILog log, 
                      AggregateService agService, 
                      IMsgProController msgProController) : base(akkaSysIp, log)
        {
            _msgProController = msgProController;
            _msgProController.SetLog(log);

            _sysController = sysController;
            _sysController.SetLog(log);

            _akkaManager = akkaManager;
            _agService = agService;
            _agService.InitWMSAckMsg();


            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectedByWMS,
                           _agService.appSetting.LocalIp,
                           _agService.appSetting.LocalPort.ToString(),
                           ConnectionSysDef.UnConnect);

            Receive<DoTcpConnet>(msg => Handle_DoTcpConnet());
        }

        protected override void TCPConnected(Tcp.Connected msg)
        {
            _log.I("TCP已被連線", " Tcp.Connected. message=" + msg.ToString());
            _log.I("TCP已被連線", " message.LocalAddress=" + msg.LocalAddress.ToString());
            _log.I("TCP已被連線", " message.RemoteAddress=" + msg.RemoteAddress.ToString());

            TryFlow(() => {
                if (_tcpRcvDataVaildActor != null)
                {
                    Context.Stop(_tcpRcvDataVaildActor);
                    _tcpRcvDataVaildActor = null;
                }

                _tempSender = Sender;

                _tmrTcpConnect = _akkaManager.ActorSystem.Scheduler.ScheduleTellRepeatedlyCancelable(
                    3, 1, Self, new DoTcpConnet(), Self);
            });
        }

        private void Handle_DoTcpConnet()
        {
            _tcpRcvDataVaildActor = Context.ActorOf(Props.Create(() => new TCPRcvDataVaildActor(_akkaManager, _sysController, _log, _tempSender, _agService, _msgProController)));

            //  將 sender 註冊給 akka tcp manager
            _tempSender.Tell(new Tcp.Register(_tcpRcvDataVaildActor));

            _tmrTcpConnect.Cancel();

            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectedByWMS,
                            _agService.appSetting.LocalIp,
                            _agService.appSetting.LocalPort.ToString(),
                            ConnectionSysDef.Connect);
        }


        protected override void PreRestart(Exception reason, object message)
        {
            _log.E("AThread生命週期", Context.System.Name + " PreRestart");
            _log.E("AThread生命週期", "Reason:" + reason.Message);

            if (Self != null)
                Sender.Tell(Tcp.Close.Instance);

        }

        // 驗證接收資料
        public class TCPRcvDataVaildActor : BaseActor
        {
            private AggregateService _agService;
            private IActorRef _rcvEditActor;
            private IActorRef _connection;
            private ILog _log;
            private IMsgProController _msgProController;
            private ISysController _sysController;

            //private ByteString _byteKeeper;
            private List<byte> _listByte;
            private bool _isRcvDataHandling;
            public int _analysisFail;

            public TCPRcvDataVaildActor(ISysAkkaManager akkaManager, 
                                        ISysController sysController, 
                                        ILog log, 
                                        IActorRef connection, 
                                        AggregateService agService, 
                                        IMsgProController msgProController) : base(log)
            {
                _rcvEditActor = akkaManager.GetActor(nameof(WMSRcvEdit));
                _connection = connection;
                _log = log;
                _agService = agService;
                _msgProController = msgProController;
                _sysController = sysController;

                //_byteKeeper = ByteString.Empty;
                _isRcvDataHandling = false;
                _listByte = new List<byte>();

                Receive<Tcp.Received>(message => TcpReceivedData(message));
                Receive<Tcp.CommandFailed>(message => TcpCommandFailed(message));
                Receive<Tcp.ConnectionClosed>(message => TcpConnectionClosed(message));
                ReceiveAny(message => RcvObject(message));

            }

            private void TcpReceivedData(Tcp.Received msg)
            {

                _log.I("TCP接收資料", "Msg=" + msg.ToString());
                _log.D("TCP接收資料", "ByteString=" + msg.Data.ToString() + "Count=" + msg.Data.Count.ToString());


                TryFlow(() =>
                {
                    ProTCPRcvData(msg);
                });
                   
            }

            private void TcpConnectionClosed(Tcp.ConnectionClosed msg)
            {
                _log.D("TCP連線關閉", " Tcp.ConnectionClosed. message=" + msg.ToString());
                _log.D("TCP連線關閉", " Message.Cause=" + msg.Cause);
                _log.D("TCP連線關閉", " Message.IsAborted=" + msg.IsAborted.ToString());
                _log.D("TCP連線關閉", " Message.IsConfirmed=" + msg.IsConfirmed.ToString());
                _log.D("TCP連線關閉", " Message.IsErrorClosed=" + msg.IsErrorClosed.ToString());
                _log.D("TCP連線關閉", " Message.IsPeerClosed=" + msg.IsPeerClosed.ToString());

                _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectedByWMS,
                 _agService.appSetting.LocalIp,
                 _agService.appSetting.LocalPort.ToString(),
                 ConnectionSysDef.UnConnect);
            }

            private void TcpCommandFailed(Tcp.CommandFailed msg)
            {
                _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + msg.ToString());
                _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + msg.ToString());
                _log.E("TCP操作失敗", " Cmd=" + msg.Cmd.ToString());
                _log.E("TCP操作失敗", " Message=" + msg.Cmd.FailureMessage);
            }

            private void RcvObject(object msg)
            {
                _log.E("AThread接收資料-RcvObject", $"VaildActor 無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
            }

            private void ProTCPRcvData(Tcp.Received msg)
            {

                var bytes = msg.Data.ToArray();
                _log.D("TCP接收資料", "Byte=" + Environment.NewLine + bytes);
     
                // 加入Buffer  
                _listByte.AddRange(bytes);

                _agService.DumpDebugRawData(bytes);

                while (_listByte.Count() > 0)
                {

                    var byteData = _listByte.ToArray();
                    var header = MsgAnalUtil.RawDeserialize(byteData, typeof(WMS_Header_Structure)) as WMS_Header_Structure;
                    if (header == null)
                    {
                        _log.D("TCP接收資料-報文標頭解析失敗", "Header == null..");
                        break;
                    }
                    var msgLeng = Convert.ToInt16(header.Length.ToStr());
                    var msgId = header.Message_ID.ToStr().ToUpper();
                    _log.D("TCP接收資料", $"Deserialize This Message'Header :");
                    _log.D("TCP接收資料", $"ID:{msgId}");
                    _log.D("TCP接收資料", $"Length:{msgLeng}");


                    // 判斷收到的報文合法性長度
                    if (!CheckMsgLenByDoc(msgLeng, msgId))
                    {
                        _agService.DumpFailRawData(bytes);
                        _log.E("TCP接收資料", $"MessgID錯誤 => {msgId} Length => {_listByte.Count()}");

                        // X. 收到訊息需回傳ACK:
                        SendAckMsg(msgId, MMSSysDef.DataCode.NotAccept, WMSSysDef.DataCode.WMSErrorMsgLength, $"Error Length {_listByte.Count()}");

                        _analysisFail++;

                        if (_analysisFail > 3)
                        {
                            _log.E("TCP接收資料", "解析錯誤次數過多清空Buffer");
                            _analysisFail = 0;
                            _listByte.Clear();
                        }

                        break;
                    }

                    if (_listByte.Count < msgLeng)
                        break;

                    if (msgId.Equals(WMSSysDef.SndMsgCode.HeartBeatCode))
                    {
                        _log.D("接收心跳電文", "接收心跳電文");
                        //清空暫存
                        _listByte.RemoveRange(0, msgLeng);
                        return;
                    }



                    _log.I("TCP接收資料-驗證報文成功", $"接收報文驗證成功 MsgID : {msgId}, Length : {msgLeng}");

                    _analysisFail = 0;

                    var data = _listByte.GetRange(0, msgLeng).ToArray();
                    _agService.DumpRcvRawData(data);
                    var wmsMsg = new CommonMsg(length: msgLeng.ToString(), id: msgId, data);
                    _rcvEditActor.Tell(wmsMsg);

                    // 收到訊息需回傳ACK:
                    SendAckMsg(msgId, MMSSysDef.DataCode.Accept);

                    //清空暫存
                    _listByte.RemoveRange(0, msgLeng);
                }

            }

            private void SendAckMsg(string msgID, string isAccept, string errorCode = "", string errorMsg = "")
            {

                var ackMsg = _agService.GetNowTimeAckMsg(msgID, isAccept, errorCode, errorMsg);

                var bytes = MsgAnalUtil.RawSerialize(ackMsg);
                var bStr = ByteString.FromBytes(bytes);

                var comMsg = new CommonMsg(id: msgID, bytes: bytes, isAck:true);
                _msgProController.CreateMMSWMSMsg("TBL_WMS_SendRecord", comMsg);

                _connection.Tell(Tcp.Write.Create(bStr));
                _log.D("TCP發送Ack", $"Send An ACK Msg");

            }

  
            /// <summary>
            /// 確定接收訊息長度
            /// </summary>
            private bool CheckMsgLenByDoc(short message_len, string message_id)
            {
                var hasMsgCode = _agService.MsgLengthAndTypeDic.WMSMsgLen.ContainsKey(message_id);
                if (!hasMsgCode)
                {
                    _log.E("驗證報文失敗", $"接收到報文 MsgID:{message_id} 接收Len:{message_len} 系統無此報文編號");
                    return false;
                }

                var msgLen = _agService.MsgLengthAndTypeDic.WMSMsgLen[message_id];

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

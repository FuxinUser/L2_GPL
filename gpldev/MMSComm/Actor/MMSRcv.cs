using Akka.Actor;
using Akka.IO;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller.MsgPro;
using Controller.Sys;
using Core.Define;
using Core.Util;
using DataModel.Common;
using DataModel.MES;
using LogSender;
using MMSComm.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using static Core.Define.DBParaDef;


namespace MMSComm.Actor
{
    public class MMSRcv : BaseServerActor
    {
        private class DoTcpConnet { }

        private IActorRef _tcpRcvDataVaildActor;
        private ISysAkkaManager _akkaManager;
        private IMsgProController _msgProController;
        private ISysController _sysController;

        private AggregateService _agService;

        private ICancelable _tmrTcpConnect;
        private IActorRef _tempSender;

        public MMSRcv(ISysAkkaManager akkaManager, 
                      AkkaSysIP akkaSysIp, 
                      ILog log, 
                      AggregateService agService, 
                      IMsgProController msgProService,
                      ISysController sysController) : base(akkaSysIp, log)
        {

            _akkaManager = akkaManager;
            _agService = agService;
            _sysController = sysController;

            _msgProController = msgProService;
            _msgProController.SetLog(log);
            _sysController.SetLog(log);


            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectedByMMS,
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

            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectedByMMS,
                          _agService.appSetting.LocalIp,
                          _agService.appSetting.LocalPort.ToString(),
                          ConnectionSysDef.Connect);
        }

        //protected override void PreRestart(Exception reason, object message)
        //{
        //    _log.E("AThread生命週期", Context.System.Name + " PreRestart");
        //    _log.E("AThread生命週期", "Reason:" + reason.Message);

        //    if(Self != null)
        //        Sender.Tell(Tcp.Close.Instance);
           
        //}

        // 驗證接收資料
        public class TCPRcvDataVaildActor : BaseActor
        {

            private AggregateService _agService;
            private IActorRef _rcvEditActor;
            private IActorRef _connection;

            private ILog _log;
            private IMsgProController _msgProService;
            private ISysController _sysController;

            private List<byte> _listByte;


            //結尾符(換行) 
            public static readonly byte _endSymbol = 0x0a;

            public int _analysisFail;

            public TCPRcvDataVaildActor(ISysAkkaManager akkaManager, ISysController sysController, ILog log, IActorRef connection, AggregateService agService, IMsgProController msgProService) : base(log)
            {
                _rcvEditActor = akkaManager.GetActor(nameof(MMSRcvEdit));
                _connection = connection;
                _log = log;
                _agService = agService;
                _msgProService = msgProService;
                _sysController = sysController;

                _listByte = new List<byte>();
                _analysisFail = 0;

                Receive<Tcp.Received>(message => TcpReceivedData(message));
                Receive<Tcp.CommandFailed>(message => TcpCommandFailed(message));
                Receive<Tcp.ConnectionClosed>(message => TcpConnectionClosed(message));
                ReceiveAny(message => RcvObject(message));
            }

         
            private void TcpReceivedData(Tcp.Received msg)
            {
                _log.D("TCP接收資料", "Msg=" + msg.ToString());
                TryFlow(() =>
                {
                    ProTCPRcvData(msg);
                });
            }

            private void TcpConnectionClosed(Tcp.ConnectionClosed msg)
            {
                _log.I("【TCP連線關閉】", " Tcp.ConnectionClosed. message=" + msg.ToString());
                _log.I("【TCP連線關閉】", " Message.Cause=" + msg.Cause);
                _log.I("【TCP連線關閉】", " Message.IsAborted=" + msg.IsAborted.ToString());
                _log.I("【TCP連線關閉】", " Message.IsConfirmed=" + msg.IsConfirmed.ToString());
                _log.I("【TCP連線關閉】", " Message.IsErrorClosed=" + msg.IsErrorClosed.ToString());
                _log.I("【TCP連線關閉】", " Message.IsPeerClosed=" + msg.IsPeerClosed.ToString());

                _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectedByMMS,
                            _agService.appSetting.LocalIp,
                            _agService.appSetting.LocalPort.ToString(),
                            ConnectionSysDef.UnConnect);
            }

            private void TcpCommandFailed(Tcp.CommandFailed msg)
            {              
                _log.E("【TCP操作失敗】", " Tcp.CommandFailed. message=" + msg.ToString());
                _log.E("【TCP操作失敗】", " Cmd=" + msg.Cmd.ToString());
                _log.E("【TCP操作失敗】", " Message=" + msg.Cmd.FailureMessage);
            }

            private void RcvObject(object msg)
            {
                _log.E("【AThread接收資料-RcvObject】", $"VaildActor 無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
            }


            private void ProTCPRcvData(Tcp.Received msg)
            {

                var bytes = msg.Data.ToArray();
                _log.D("TCP接收資料", "Cnt="+bytes.Length.ToString());

                // 加入Buffer  
                _listByte.AddRange(bytes);

                _agService.DumpDebugRawData(bytes);              

                while (_listByte.Count() > 0)
                {
                    // 取結尾符號
                    var endTagIndex = Array.IndexOf(_listByte.ToArray(), MMSSysDef.EndTag);

                    // 是否有結尾符號 True : Index > 0 
                    if (endTagIndex <= 0)
                        break;

                    // 解析Header
                    var headerData = _listByte.ToArray();
                    var msgHeader = MsgAnalUtil.RawDeserialize(headerData, typeof(MMS_Header_Structure)) as MMS_Header_Structure;
                    
                    var msgId = msgHeader.Code.ToStr();
                    var msgLength = msgHeader.Length.ToStr().ToNullable<int>() ?? 0;
                    
                    _log.I("解析Header", $"ID:{msgId} Length:{msgLength}");
                   
                    // 判斷收到的報文合法性 
                    if (!CheckMsgLenByDoc(msgLength, msgId))
                    {
                   
                        _analysisFail++;

                        if (_analysisFail > 0)
                        {
                            _log.E("清空接收Buffer", $"報文解析不出次數{_analysisFail}過高清空接收Buffer");
                            _analysisFail = 0;
                            _listByte.Clear();
                        }

                        _agService.DumpFailRawData(bytes);
                        _log.E("TCP接收資料", $"MessgID錯誤 => {msgId} Length => {_listByte.Count()}");
                        SendAckMsg(msgId, MMSSysDef.DataCode.NotAccept);                  
                        break;
                    }


                    if (_listByte.Count < msgLength)
                        break;

                    _analysisFail = 0;

                    if (msgHeader.FuncCode.ToStr().Equals(MMSSysDef.DataCode.HeartMsg))
                    {
                        //心跳電文
                        _log.D("TCP接收資料-心跳電文", $" FuncCode = {msgHeader.FuncCode.ToStr()}  Code = {msgHeader.Code.ToStr()}");
                    }

                    if (msgHeader.FuncCode.ToStr().Equals(MMSSysDef.DataCode.DataMsg))
                    {

                        var data = _listByte.GetRange(0, msgLength).ToArray();

                        _agService.DumpRcvRawData(data);

                        var rcvActualLength = data.Length.ToString();
                        // 去結尾
                        var container = new List<byte>(data);
                        container.Remove(0x0A);
                        data = container.ToArray();
   
                        var mmsMsg = new CommonMsg(rcvActualLength, msgId, data);

                        _rcvEditActor.Tell(mmsMsg);

                        _log.I("驗證報文成功", $"MsgID : {msgId}, Length : {msgLength}");

                        // x.回ACK:
                        SendAckMsg(msgHeader.Code.ToStr(), MMSSysDef.DataCode.Accept);

                    }

                    //清空暫存
                    _listByte.RemoveRange(0, msgLength);
             
                }
               
            }

          
            private void SendAckMsg(string msgID, string isAccept)
            {
                var ackMsg = _agService.GetNowTimeAckMsg(msgID, isAccept);
                var bytes = MsgAnalUtil.RawSerialize(ackMsg);
                var bs = ByteString.FromBytes(bytes);

                var comMsg = new CommonMsg(length: bytes.Length.ToString(), id: ackMsg.Header.Code.ToStr(), bytes: bytes, true);
                _msgProService.CreateMMSWMSMsg("TBL_MMS_SendRecord", comMsg);

                _connection.Tell(Tcp.Write.Create(bs));
                _log.D("TCP發送Ack", $"Send An ACK Msg");
            }


            /// <summary>
            /// 確定接收訊息長度
            /// </summary>
            private bool CheckMsgLenByDoc(int message_len, string message_id)
            {
                
                var hasMsgCode = _agService.MsgLengthAndTypeDic.MMSMsgLen.ContainsKey(message_id);
                if (!hasMsgCode)
                {
                    _log.E("驗證報文失敗", $"接收到報文 MsgID:{message_id} 接收Len:{message_len} 系統無此報文編號");
                    return false;
                }

                var msgLen = _agService.MsgLengthAndTypeDic.MMSMsgLen[message_id];
            
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

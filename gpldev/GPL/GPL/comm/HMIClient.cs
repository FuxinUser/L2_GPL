using System;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Akka.Actor;
using Akka.Event;
using DataModel.HMIServerCom.Msg;

namespace GPLManager
{
    class HMIClient : ReceiveActor
    {
        /// <summary> For every message guid is used as identity </summary>
        public static Guid ClientGuid;
        /// <summary> Save the loaded of nLog </summary>
        private readonly Akka.Event.ILoggingAdapter _log = null;

        /// <summary> Save the ActorSelection of connected server </summary>
        private ActorSelection _server;
        /// <summary> Save the ICancelable element </summary>
        private ICancelable _keepaliveCancel;

        private string _client_IP_Port;

        #region Initialize

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log"> NLog element </param>
        public HMIClient(Akka.Event.ILoggingAdapter log)
        {
            //if (log != null) _log = log;
            //else _log = Akka.Event.Logging.GetLogger(Context);
            _log = log != null ? log : Logging.GetLogger(Context);
            _client_IP_Port = ConfigurationManager.AppSettings["ClientIPPort"];
            var serverAkkaSysName = ConfigurationManager.AppSettings["ServerAkkaSystem"];
            var serverAkkaIPPort = ConfigurationManager.AppSettings["ServerAkkaSystemIP"];

            //  Guid must be used in client server communication as an identity to hmi client
            ClientGuid = Guid.NewGuid();
            //_server = Context.ActorSelection("akka.tcp://CPL1CLIENT@192.168.0.201:8271/user/PCcom");
            //_server = Context.ActorSelection("akka.tcp://GPLCLIENT@127.0.0.1:8271/user/PCcom");
            _server = Context.ActorSelection($"akka.tcp://{serverAkkaSysName}@{serverAkkaIPPort}/user/PCcom");

            //Client Alive
            Receive<SCCommMsg.ClientAliveMsg>(message => HandleSendClientAlive(message));

            #region CtoS
            //要求更新排程
            Receive<SCCommMsg.CS01_AckSchedule>(message => Handle_CS01_AckSchedule(message));
            //要求PDI
            Receive<SCCommMsg.CS02_AckPDI>(message => Handle_CS02_AckPDI(message));
            //排程调整/删除
            Receive<SCCommMsg.CS03_ScheduleChange>(message => Handle_CS03_ScheduleChange(message));
            //入口段鋼卷ID更正
            Receive<SCCommMsg.CS04_RenameCoil>(message => Handle_CS04_RenameCoil(message));
            //退料
            Receive<SCCommMsg.CS05_RejectCoil>(message => Handle_CS05_RejectCoil(message));
            //上传PDO
            Receive<SCCommMsg.CS06_SendMMSPDO>(message => Handle_CS06_SendMMSPDO(message));
            //列印标签
            Receive<SCCommMsg.CS07_PrintLabel>(message => Handle_CS07_PrintLabel(message));
            //更新毛重
            Receive<SCCommMsg.CS08_WeightInput>(message => Handle_CS08_WeightInput(message));
            //停復機記錄
            Receive<SCCommMsg.CS09_LineFaultData>(message => _server.Tell(message));
            //自动入料
            Receive<SCCommMsg.CS10_Coil_AutoFeedModeChange>(message => Handle_AutoEntryCoil(message));
            //手动入料
            Receive<SCCommMsg.CS11_Coil_ManualFeed>(message => Handle_ManualEntry(message));
            //鞍座入料
            Receive<SCCommMsg.CS12_Coil_SkidFeed>(message => _server.Tell(message));
            //刪除鞍座
            Receive<SCCommMsg.CS13_DeleteSidCoil>(message => _server.Tell(message));
            //出料
            Receive<SCCommMsg.CS14_DeliveryCoilOut>(message => _server.Tell(message));
            Receive<SCCommMsg.CS15_Utility>(message => Handle_CS15_Utility(message));
            Receive<SCCommMsg.CS16_FinishLoadSchedule>(message => Handle_CS16_FinishLoadSchedule(message));
            Receive<SCCommMsg.CS17_FinishLoadPDI>(message => Handle_CS17_FinishLoadPDI(message));
            //天車入料WMS鋼卷號與掃描鋼卷號不一致，操作選擇後回傳給Server
            Receive<SCCommMsg.CS18_CarneEntryCoilSelect>(message => _server.Tell(message));
            Receive<SCCommMsg.CS19_RequestDummy>(message => Handle_CS18_RequestDummy(message));
            Receive<SCCommMsg.CS20_DeleteDummy>(message => Handle_CS19_DeleteDummy(message));
            //出料確認
            Receive<SCCommMsg.CS21_DeliveryCoilReady>(message => _server.Tell(message));

            //HMI通知Server下拋POR鋼捲生產參數給L1
            Receive<SCCommMsg.CS22_POR_PresetL1>(message => _server.Tell(message));
            //HMI通知Server修改子捲號，Server需下拋205NewPORId通知L1修改POR捲號。
            Receive<SCCommMsg.CS23_POR_StripBreakModify>(message => _server.Tell(message));
            #endregion

            #region S to C
            frm_2_1_Tracking frm_2_1 = new frm_2_1_Tracking();
            Frm_1_1_PDISchl frm_1_1 = new Frm_1_1_PDISchl();
            //扫描结果不一致
            Receive<SCCommMsg.SC01_ScnBarcodeID>(message => Handle_SC01_EntryBarcodeID(message));
            //通知排程已刷新
            Receive<SCCommMsg.SC04_ScheduleChangeNotice>(message => Handle_SC04_ScheduleChangeNotice(message));
            Receive<SCCommMsg.SC03_EventPush>(message => Handle_SC03_EventPush(message));
            //天車入料WMS鋼卷號與掃描鋼卷號不一致 ( WMS鋼卷號[Empty] / 掃描鋼卷號[有值])
            Receive<SCCommMsg.SC06_CraneEntryCoil>(message => Handle_SC06_CraneEntryCoil(message));
            //PDO上傳的反饋通知 MWW-add 2023.4.25
            Receive<SCCommMsg.SC07_PdoUploadedReply>(message => Handle_SC07_PdoUploadedReply(message));
            #endregion

            ReceiveAny(message => Handle_Any(message));
        }

        #region HMI To Server
        /// <summary>
        /// 向Server發送要求排程訊息給MMS
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS01_AckSchedule(SCCommMsg.CS01_AckSchedule msg)
        {
            _server.Tell(msg);
        }
        /// <summary>
        /// 要求PDI
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS02_AckPDI(SCCommMsg.CS02_AckPDI msg)
        {
            _server.Tell(msg);
        }
        /// <summary>
        /// 排程調整/刪除
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS03_ScheduleChange(SCCommMsg.CS03_ScheduleChange msg)
        {
            _server.Tell(msg);
        }
        /// <summary>
        /// 入口段鋼卷ID更正
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS04_RenameCoil(SCCommMsg.CS04_RenameCoil msg)
        {
            _server.Tell(msg);
        }
        /// <summary>
        /// 自动入料
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_AutoEntryCoil(SCCommMsg.CS10_Coil_AutoFeedModeChange msg)
        {
            _server.Tell(msg);
        }
        /// <summary>
        /// 手动入料
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_ManualEntry(SCCommMsg.CS11_Coil_ManualFeed msg)
        {
            _server.Tell(msg);
        }
        /// <summary>
        /// 退料
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS05_RejectCoil(SCCommMsg.CS05_RejectCoil msg)
        {
            _server.Tell(msg);
        }
        /// <summary>
        /// 上传PDO
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS06_SendMMSPDO(SCCommMsg.CS06_SendMMSPDO msg)
        {
            _server.Tell(msg);
        }
        /// <summary>
        /// 列印标签
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS07_PrintLabel(SCCommMsg.CS07_PrintLabel msg)
        {
            _server.Tell(msg);
        }
        /// <summary>
        /// 更新毛重
        /// </summary>
        /// <param name="msg"></param>
        private void Handle_CS08_WeightInput(SCCommMsg.CS08_WeightInput msg)
        {
            _server.Tell(msg);
        }
        private void Handle_CS16_FinishLoadSchedule(SCCommMsg.CS16_FinishLoadSchedule msg)
        {
            _server.Tell(msg);
        }
        private void Handle_CS17_FinishLoadPDI(SCCommMsg.CS17_FinishLoadPDI msg)
        {
            _server.Tell(msg);
        }
        private void Handle_CS15_Utility(SCCommMsg.CS15_Utility msg)
        {
            _server.Tell(msg);
        }

        private void Handle_CS18_RequestDummy(SCCommMsg.CS19_RequestDummy msg)
        {
            _server.Tell(msg);
        }

        private void Handle_CS19_DeleteDummy(SCCommMsg.CS20_DeleteDummy msg)
        {
            _server.Tell(msg);
        }
        #endregion

        #region Server To HMI
        //Frm_0_0_Main frm_ = new Frm_0_0_Main();

        //frm_Scan frm_Scan = new frm_Scan();

        private void HandleSendClientAlive(SCCommMsg.ClientAliveMsg message)
        {
            //message.ClientGuid = ClientGuid;

            //_log.Error("Send keep alive message to server as Guid:" + message.ClientGuid.ToString());
            //_server.Tell(message);

            _log.Debug("Send keep alive message to server as IP Port:" + message.Client_IP_Port);
            _server.Tell(message);
        }

        /// <summary>
        /// 掃描結果不符，收到通知讓Tracking畫面跳ID選擇畫面給操作選擇
        /// </summary>
        /// <param name="msg"></param>
        public void Handle_SC01_EntryBarcodeID(SCCommMsg.SC01_ScnBarcodeID msg)
        {
            EventLogHandler.Instance.LogInfo("2-1", "扫描结果不相符", "入口段扫描Barcode钢卷编号与TrackingMap鞍座钢卷编号不符");
        }

        /// <summary>
        /// 信息推播
        /// </summary>
        /// <param name="msg"></param>
        public void Handle_SC03_EventPush(SCCommMsg.SC03_EventPush msg)
        {
            try
            {
                PublicForms.Main.Invoke(new Action(() =>
                { PublicForms.Main.Handle_SC03_EventPush(msg);}));
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"信息推播有错误");
                PublicComm.ExceptionLog.Debug($"信息推播有错误:{ex}");
                PublicComm.ClientLog.Debug($"信息推播有错误:[{ex}]");
                PublicComm.akkaLog.Debug($"信息推播有错误:[{ex}]");
            }
        }


        public void Handle_SC04_ScheduleChangeNotice(SCCommMsg.SC04_ScheduleChangeNotice msg)
        {
            try
            {
                PublicForms.Main.Invoke(new Action(() =>
                {
                    //是否开启画面
                    bool openflag = false;
                    Frm_1_1_PDISchl form = new Frm_1_1_PDISchl();
                    foreach (Form fx in PublicForms.Main.Pnl_Main.Controls.Cast<Control>().Where(x => x is Form))
                    {
                        if (fx.Name.Equals(form.Name))
                        {
                            form = fx as Frm_1_1_PDISchl;
                            PublicForms.PDISchl.Handle_SC04_ScheduleChangeNotice(msg);
                            openflag = true;
                        }
                    }
                    if (openflag == false)
                    {
                        EventLogHandler.Instance.EventPush_Message($"已接收到新的生产排程");

                        //Frm_0_0_Main Main = new Frm_0_0_Main
                        //{
                        //    MdiParent = PublicForms.Main,
                        //    Parent = PublicForms.Main.pnl_Main,
                        //    StartPosition = FormStartPosition.Manual,
                        //    Location = new Point(0, 55),
                        //    Width = 1920,
                        //    Height = 980
                        //};

                        //Main.Show();
                        //Main.Focus();

                        //Handle_SC04_ScheduleChangeNotice(msg);
                    }
                }));
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"排程异动通知有错误");
                PublicComm.ExceptionLog.Debug($"排程异动通知有错误:{ex}");
                PublicComm.ClientLog.Debug($"排程异动通知有错误:[{ex}]");
                PublicComm.akkaLog.Debug($"排程异动通知有错误:[{ex}]");
            }
        }

        /// <summary>
        /// 天車入料Server通知，
        /// </summary>
        /// <param name="Message"></param>
        public void Handle_SC06_CraneEntryCoil(SCCommMsg.SC06_CraneEntryCoil Message)
        {
            try
            {
                PublicForms.Main.Invoke(new Action(() =>
                {
                    //是否开启画面
                    bool openflag = false;
                    frm_2_1_Tracking form = new frm_2_1_Tracking();
                    foreach (Form fx in PublicForms.Main.Pnl_Main.Controls.Cast<Control>().Where(x => x is Form))
                    {
                        if (fx.Name.Equals(form.Name))
                        {
                            form = fx as frm_2_1_Tracking;
                            PublicForms.Tracking.Handle_SC06_CraneEntryCoil(Message);
                            openflag = true;
                        }
                    }
                    if (openflag == false)
                    {
                        PublicForms.Main.tsMenuItem_2_1.PerformClick();

                        //frm_0_0_Main Main = new frm_0_0_Main
                        //{
                        //    MdiParent = PublicForms.Main,
                        //    Parent = PublicForms.Main.pnl_Main,
                        //    StartPosition = FormStartPosition.Manual,
                        //    Location = new Point(0, 55),
                        //    Width = 1920,
                        //    Height = 980
                        //};
                        //Main = Main.Parent.Parent as frm_0_0_Main;
                        //Main.tsMenuItem_1_1.PerformClick();

                        //Main.Show();
                        //Main.Focus();

                        Handle_SC06_CraneEntryCoil(Message);
                    }
                }));
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"天车入料通知有错误");
                PublicComm.ExceptionLog.Debug($"天车入料通知有错误:{ex}");
                PublicComm.ClientLog.Debug($"天车入料通知有错误:[{ex}]");
                PublicComm.akkaLog.Debug($"天车入料通知有错误:[{ex}]");
            }
        }
        /// <summary>
        /// PDO上傳的反饋通知 MWW-add 2023.4.25
        /// </summary>
        /// <param name="msg"></param>
        public void Handle_SC07_PdoUploadedReply(SCCommMsg.SC07_PdoUploadedReply msg)
        {
            try
            {
                PublicForms.Main.Invoke(new Action(() => PublicForms.PDODetail?.Handle_SC07_PdoUploadedReply(msg.Message)));
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"顯示上傳PDO");
                PublicComm.ExceptionLog.Debug($"刷新PDO上傳的反饋通知有错误:{ex}");
                PublicComm.ClientLog.Debug($"刷新PDO上傳的反饋通知有错误:[{ex}]");
                PublicComm.akkaLog.Debug($"刷新PDO上傳的反饋通知有错误:[{ex}]");
            }
        }
        private void Invoke(Action action)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region Handle events that when receiving request messages from forms (send to Pccomm)
        /// <summary>
        ///     Handle when receiving an undefined message
        /// </summary>
        /// <param name="message"> Information of undefined type </param>
        private void Handle_Any(object message)
        {
            _log.Error($"Received an unhandled message!!! type:{message.GetType().ToString()} from Sender:{Sender.Path.ToString()}");
        }
        #endregion

        #region Override events
        protected override void PreStart()
        {
            _log.Debug("PreStart. ");

            var alive = new SCCommMsg.ClientAliveMsg()
            {
                Client_IP_Port = _client_IP_Port
            };

            _keepaliveCancel = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(3), Self, alive, Self);

            base.PreStart();
        }


        protected override void PreRestart(Exception reason, object message)
        {
            _log.Debug($"PreRestart. exception message:[{reason.Message}] happened when received, message:[{message.GetType().Name}]");
            if (reason.StackTrace != null) _log.Debug($"PreRestart. exception stackTrace:{reason.StackTrace.ToString()}");

            base.PreRestart(reason, message);
        }


        protected override void PostStop()
        {
            _log.Debug($"PostStop. {Self.Path.Name}");

            base.PostStop();
        }


        protected override void PostRestart(Exception reason)
        {
            _log.Debug($"PostRestart. exception message:[{reason.Message}] happened when received");

            base.PostRestart(reason);
        }
        #endregion
    }
}

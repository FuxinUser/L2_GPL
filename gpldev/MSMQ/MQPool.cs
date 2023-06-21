using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Messaging;
using System.Threading;

namespace MSMQ
{
    public static class MQPool
    {
        public delegate void RcvHandler(object msg);
        public static ConcurrentDictionary<string, MQHelper> _container = new ConcurrentDictionary<string, MQHelper>();

        private static readonly string _LogPort = "LogMgr";
        private static readonly string _L1Port = "PlcSndEdit";
        private static readonly string _PCComPort = "PCcom";
        private static readonly string _MMsPort = "MMSSndEdit";
        private static readonly string _WMsPort = "WMsSndEdit";
        private static readonly string _LprPort = "LprSndEdit";
        private static readonly string _TrkPort = "TrkMgr";
        private static readonly string _CoilPort = "Coilmgr";
        private static readonly string _DtGtrPort = "DtGtrMgr";
        private static readonly string _DtStpPort = "DtStpMgr";
        private static readonly string _DtProGtrPort = "DtProGtr";
        private static readonly string _BCSnPort = "BCScnSndEdit";
        // BarCode
        private static readonly string _BCScnRcvEdit = "BCScnRcvEdit";
       

        [Serializable]
        public class MQMessage
        {
            public int ID;
            public object Data;
        }

        /// <summary>取得MQHelper，可藉由此MQHelper控制OS上的MQ，若指定的MQ不存在會在OS建立</summary>
        /// <param name="key">Queue名稱，可忽略大小寫</param>
        /// <returns></returns>
        public static MQHelper GetMQ(string key)
        {
            string lowerKey = key.ToLower();
            if (!_container.ContainsKey(lowerKey))
            {
                _container[lowerKey] = new MQHelper(lowerKey);
            }
            return _container[lowerKey];
        }

        public class MQHelper
        {
            private MessageQueue _msQueue;
            private readonly object _lockObj = new object();
            private readonly object _safelock = new object();
            private string _mqPath;
            private RcvHandler rcv;

            public MQHelper(string name)
            {
                _mqPath = @".\Private$\" + name.ToLower();
                Create(_mqPath);
                _msQueue = new MessageQueue(_mqPath);
                _msQueue.Formatter = new BinaryMessageFormatter();
            }
            public void Create(string name)
            {
                if (!MessageQueue.Exists(_mqPath))
                {
                    MessageQueue.Create(_mqPath);
                }
            }
            public void Receive(RcvHandler rceiveMessageHandler)
            {
                Create(_mqPath);
                if (rcv == null)
                {
                    rcv = rceiveMessageHandler;
                    // 註冊
                    _msQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(_msQueue_ReceiveCompleted);
                    // 開始接收
                    _msQueue.BeginReceive();
                }
                else
                {
                    // 已註冊過，不接收
                    _msQueue.BeginReceive();
                }
                #region 原先的Code
                //RcvHandler rcv = rceiveMessageHandler;
                //if (rcv != null)
                //{
                //    _msQueue.ReceiveCompleted += (sender, e) =>
                //    {
                //        lock (_lockObj)
                //        {
                //            rcv(e.Message.Body);
                //            _msQueue.BeginReceive();
                //        }
                //    };
                //    _msQueue.BeginReceive();
                //}
                #endregion
            }

            public void CleanMQ()
            {
                lock (_safelock)
                {
                    // 把你執行的程式碼包在這
                    try
                    {
                        _msQueue.Purge();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }

            private void _msQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
            {
                lock (_lockObj)
                {
                    rcv(e.Message.Body);
                    _msQueue.BeginReceive();
                }
            }

            //public void Send(object message)
            //{
            //    var serialAttr = message.GetType().GetCustomAttributes(typeof(SerializableAttribute), false);
            //    if (serialAttr.Length == 0) throw new Exception("this message no declared SerializableAttribute");
            //    Create(_mqPath);
            //    _msQueue.Send(message);
            //}
            public void Send(int messageID, object message)
            {
                //Thread.Sleep(10);
                //Create(_mqPath);
                var box = new MQMessage() { ID = messageID, Data = message };

                lock (_safelock)
                {
                    // 把你執行的程式碼包在這
                    try
                    {
                        _msQueue.Send(box);
                    }
                    catch
                    {
                        throw;
                    }
                }
                //try
                //{
                //    _msQueue.Send(box);
                //}
                //catch
                //{
                //    throw;
                //}



            }

            //private static void DelMQ(string key)
            //{
            //    string lowerKey = key.ToLower();
            //    if (_container.ContainsKey(lowerKey))
            //    {
            //        _container.TryRemove(lowerKey);
            //    }
            //}
        }

        //private static void DelMQ(string key)
        //{
        //    string lowerKey = key.ToLower();
        //    if (_container.ContainsKey(lowerKey))
        //    {
        //        string removeDic; 
        //        _container.TryRemove(lowerKey out removeDic);
        //    }
        //}

        //Rcv
        public static void ReceiveFromWMS(RcvHandler handler)
        {
            GetMQ(_WMsPort).Receive(handler);
        }
        public static void ReceiveFromTrk(RcvHandler handler)
        {
            GetMQ(_TrkPort).Receive(handler);
        }
        public static void ReceiveFromMMS(RcvHandler handler)
        {
            GetMQ(_MMsPort).Receive(handler);
        }
        public static void ReceiveFromL1(RcvHandler handler)
        {
            GetMQ(_L1Port).Receive(handler);
        }
        public static void ReceiveFromPCCom(RcvHandler handler)
        {
            GetMQ(_PCComPort).Receive(handler);
        }
        public static void ReceiveFromLog(RcvHandler handler)
        {
            GetMQ(_LogPort).Receive(handler);
        }
        public static void ReceiveFromDtStp(RcvHandler handler)
        {
            GetMQ(_DtStpPort).Receive(handler);
        }
        public static void ReceiveFromBCSn(RcvHandler handler)
        {
            GetMQ(_BCSnPort).Receive(handler);
        }
        public static void ReceiveFromLpr(RcvHandler handler)
        {
            GetMQ(_LprPort).Receive(handler);
        }

        public static void ReceiveFromDtGtr(RcvHandler handler)
        {
            GetMQ(_DtGtrPort).Receive(handler);
        }

        // Barcode - Demo用
        public static void ReceiveFromBCScnRcvEdit(RcvHandler handler)
        {
            GetMQ(_BCScnRcvEdit).Receive(handler);
        }


        //Snd
        public static void SendToWMS(int id, object msg)
        {
            GetMQ(_WMsPort).Send(id, msg);
        }
        public static void SendToTrk(int id, object msg)
        {
            GetMQ(_TrkPort).Send(id, msg);
        }
        public static void SendToL1(int id, object msg)
        {
            GetMQ(_L1Port).Send(id, msg);
        }
        public static void SendToMMS(int id, object msg)
        {
            GetMQ(_MMsPort).Send(id, msg);
        }
        public static void SendToCoil(int id, object msg)
        {
            GetMQ(_CoilPort).Send(id, msg);
        }
        public static void SendToPCCom(int id, object msg)
        {
            GetMQ(_PCComPort).Send(id, msg);
        }
        public static void SendToDtGtr(int id, object msg)
        {
            GetMQ(_DtGtrPort).Send(id, msg);
        }
        public static void SendToLog(int id, object msg)
        {
            GetMQ(_LogPort).Send(id, msg);
        }
        public static void SendToDtStp(int id, object msg)
        {
            GetMQ(_DtStpPort).Send(id, msg);
        }
        public static void SendToBCsn(int id, object msg)
        {
            GetMQ(_BCSnPort).Send(id, msg);
        }
        public static void SendToLpr(int id, object msg)
        {
            GetMQ(_LprPort).Send(id, msg);
        }
        // Barcode - Demo用
        public static void SendToBCScnRcvEdit(int id, object msg)
        {
            GetMQ(_BCScnRcvEdit).Send(id, msg);
        }
        public static void SendToDtProGtr(int id, object msg)
        {
            GetMQ(_DtProGtrPort).Send(id, msg);
        }

        //Del
        //public static void DelTrkMQ()
        //{
        //    DelMQ(_TrkPort);
        //}
    }
}

using Akka.Actor;
using Akka.Event;
using Core.Define;
using Core.Help;
using DBService.Repository.EventLog;
using LogSender;
using MSMQ;
using System;


/**
 * Author: ICSC SPYUA
 * Date:2019/12/30
 * Desc: Log Thread (純記DB Log)
 **/
namespace LogRecord.Actor
{
    public class LogMgr : ReceiveActor
    {
        private IActorRef _self; 
        private bool _debugLogSwitch;
        private EventLogRepo _eventLogRepo;

        public LogMgr(ILoggingAdapter log) 
        {           
            _self = ActSystem.GetDicCtrl(typeof(LogMgr).Name);            
            _eventLogRepo = new EventLogRepo(DBParaDef.DBConn);


            _debugLogSwitch = true;

            MQPool.GetMQ($"{nameof(LogMgr)}").Receive(x =>
            {
                var msg = (x as MQPool.MQMessage).Data;
                _self.Tell(msg);
            });

            Receive<EventLogEntity.TBL_EventLog>(message => {

                if (!_debugLogSwitch && message.Event_Type.Equals(LogDef.DEBUG))
                    return;

                SaveLogToDB(message);

            });
            Receive<EventDef.CMDSET>(message => SetDebugLogSwitch(message));
        }


        public void SetDebugLogSwitch(EventDef.CMDSET cmd)
        {
            var flag = cmd == EventDef.CMDSET.DEBUG_LOG_OPEN ? true : false;
            _debugLogSwitch = flag;
        }

        public void SaveLogToDB(EventLogEntity.TBL_EventLog log)
        {
          
            try
            {
                bool WritToDB = false;
                switch (log.Event_Description.Trim())
                {
                    case "[加入HMI Group成功]":
                        WritToDB = false;
                        break;
                    case "[更新CoilMap]":
                        WritToDB = false;
                        break;
                    case "[更新L1 Alive最後時間]":
                        WritToDB = false;
                        break;
                    case "[資料蒐集]":
                        WritToDB = false;
                        break;
                    case "[存取Process資料]":
                        WritToDB = false;
                        break;
                    case "[使用 Queue 發送電文]":
                        WritToDB = false;
                        break;
                    case "[傳送Track訊息]":
                        WritToDB = false;
                        break;
                    case "[例外事件發生]":
                        WritToDB = false;
                        break;
                    case "[TCP接收資料]":
                        WritToDB = false;
                        break;
                    case "[報文解析成功]":
                        WritToDB = false;
                        break;
                    case "[存取GRDRPT資料]":
                        WritToDB = false;
                        break;
                    case "[存取L25 Alive訊息]":
                        WritToDB = false;
                        break;
                    case "[驗證報文成功]":
                        WritToDB = false;
                        break;
                    case "[存取2.5 Utility資料]":
                        WritToDB = false;
                        break;
                    case "[存取Utility資料]":
                        WritToDB = false;
                        break;
                    case "[解析Header]":
                        WritToDB = false;
                        break;
                    case "[接收應答電文]":
                        WritToDB = false;
                        break;
                    case "[撈取CoilMap]":
                        WritToDB = false;
                        break;
                    case "[判定系統自動進料參數]":
                        WritToDB = false;
                        break;
                    case "[存取接收報文]":
                        WritToDB = false;
                        break;
                    case "[報文序列化編碼成功]":
                        WritToDB = false;
                        break;
                    case "[將CoilMap新增至L25 CoilMap]":
                        WritToDB = false;
                        break;
                    default:
                        WritToDB = true;
                        break;
                }

                if (WritToDB)
                { 
                  var insertNum = _eventLogRepo.Insert(log);
                  if (insertNum > 0)
                    Monitor.show(log);          
                }

            }
            catch (Exception e)
            {
                //Dump To Local File
                Console.WriteLine(e.Message);
            }
           
        }

    }
}

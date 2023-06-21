using Core.Util;
using LogSender;
using MSMQ;
using System;

namespace Controller
{
    public static class MQPoolService
    {
        private static ILog log = new Log("MQMQ", "MQPoolService");

        public static void SendToWMS(MQPool.MQMessage message)
        {
            try
            {
                log.D("MSMQ傳送", "傳送給WMS App");
                MQPool.SendToWMS(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message);
            }
            
        }
        public static void SendToTrk(MQPool.MQMessage message)
        {
            try
            {
                log.D("MSMQ傳送", "傳送給Trk App");
                MQPool.SendToTrk(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message);
            }           
        }
        public static void SendToL1(MQPool.MQMessage message)
        {
            try
            {
                log.D("MSMQ傳送", "傳送給L1 App");
                MQPool.SendToL1(message.ID, message.Data);
            }catch(Exception e)
            {
                log.E("MSMQ", message.ID + " "+ e.Message);
            }
            
        }
        public static void SendToMMS(MQPool.MQMessage message)
        {
            try
            {
                log.D("MSMQ傳送", "傳送給MMS App");
                MQPool.SendToMMS(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message);
            }           
        }
        public static void SendToCoil(MQPool.MQMessage message)
        {

            try
            {
                log.D("MSMQ傳送", "傳送給Coil App");
                MQPool.SendToCoil(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message);
            }            
        }
        public static void SendToPCCom(MQPool.MQMessage message)
        {
            try
            {
                log.D("MSMQ傳送", "傳送給PCCom App");
                MQPool.SendToPCCom(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message);
            }       
        }
        public static void SendToDtGtr(MQPool.MQMessage message)
        {
            try
            {
                log.D("MSMQ傳送", "傳送給DtGtr App");
                MQPool.SendToDtGtr(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message);
            }
            
        }
        public static void SendToDtStp(MQPool.MQMessage message)
        {
            try
            {
                log.D("MSMQ傳送", "傳送給DtStp App");
                MQPool.SendToDtStp(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message);
            }
            
        }
        public static void SendToLog(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToLog(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message);
            }
            
        }
        public static void SendToBCsn(MQPool.MQMessage message)
        {
            try
            {
                log.D("MSMQ傳送", "傳送給BCSsn App");
                MQPool.SendToBCsn(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message);
            }
            
        }
        public static void SendToLpr(MQPool.MQMessage message)
        {
            try
            {
                log.D("MSMQ傳送", "傳送給Lpr App");
                MQPool.SendToLpr(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message);
            }
            
        }

        public static void SendToDtProGtr(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToDtProGtr(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message);
            }

        }

        [Obsolete]
        public static void SendToBCScnRcvEdit(MQPool.MQMessage message)
        {
            try
            {
                MQPool.SendToBCScnRcvEdit(message.ID, message.Data);
            }
            catch (Exception e)
            {
                log.E("MSMQ", message.ID + " " + e.Message);
            }
            
        }
    }
}

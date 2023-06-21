using Core.Define;
using Core.Util;
using DataModel.Common;
using DBService.MMSRepository;
using DBService.UnitOfWork;
using LogSender;
using System;
using System.Text;

namespace Controller.MsgPro
{
    public class MsgProController : IMsgProController
    {
        //private LogSnd _log;
        private ILog _log;
        private DapperDBContext hisDBContext;
       
        public MsgProController()
        {
         
            hisDBContext  = new DapperDBContext(DBParaDef.HisDBConn);

        }
      
        public void SetLog(ILog log)
        {
            _log = log;
        }

        #region -- Msg Pro --

        public bool CreateMMSWMSMsg(string tableName, CommonMsg data)
        {
        
            var rcvMsg = new MMS_WMS_MsgRecord
            {
                Header = data.Message_Id,
                Length = data.Message_Length, 
                Data = Encoding.UTF8.GetString(data.Data),
                IsAck = data.IsAck?"1":"0",
                CreateTime = DateTime.Now
                
            };

            try
            {
                _log.D("存取接收報文", "存取接收報文至" + tableName);
                var insertNum = hisDBContext.Create(tableName, rcvMsg);             
                return insertNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"存取至{tableName} : False", e.Message.CleanInvalidChar());
                throw;
            }
        }



        public bool CreateMsgToL1HistoryDB(string tableName, object data)
        {         
            try
            {
                var insertNum = hisDBContext.Create(tableName, data);
                //_log.Debug($"存取至{tableName} : {insertNum > 0}", "收發報文歷史存取");
                return insertNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"存取至{tableName} : False", e.Message.CleanInvalidChar());
                throw;
            }

        }

        #endregion
    }
}

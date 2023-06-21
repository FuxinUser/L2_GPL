using Core.Define;
using DBService.Repository.EventLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Logic
{
    public class EventLogLogic
    {

        private EventLogRepo _eventLogRepo;

        public EventLogLogic()
        {
            _eventLogRepo = new EventLogRepo(DBParaDef.DBConn);
         
        }

        public int CreateEventLog(EventLogEntity.TBL_EventLog eventLog)
        {
            try
            {
                var insertNum = _eventLogRepo.Insert(eventLog);
                return insertNum;
            }
            catch 
            {
                throw;
            }   
        }

    }
}

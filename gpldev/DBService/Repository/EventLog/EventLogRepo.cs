using DBService.Base;
using System;

namespace DBService.Repository.EventLog
{
    public class EventLogRepo : BaseRepository<EventLogEntity.TBL_EventLog>
    {

        protected override string TableName => nameof(EventLogEntity.TBL_EventLog);

        protected override string[] PKName => throw new NotImplementedException();


        public EventLogRepo(string connStr) : base(connStr)
        {

        }

    }
}

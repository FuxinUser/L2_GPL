using DBService.Repository.EventLog;
using LogRecord.Form;

namespace LogRecord
{
    public class Monitor
    {
        public static LogContract.IPresenter _CoilPresenter;

        public static void show(EventLogEntity.TBL_EventLog item)
        {
            _CoilPresenter.InsertLog(item);
        }
    }
}

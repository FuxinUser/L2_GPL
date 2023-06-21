using DataModel.Common;
using LogSender;

namespace Controller.MsgPro
{
    public interface IMsgProController
    {
        void SetLog(ILog log);
        bool CreateMsgToL1HistoryDB(string tableName, object data);

        bool CreateMMSWMSMsg(string tableName, CommonMsg data);
    }
}

using LogSender;
using static Core.Define.DBParaDef.ConnectionSysDef;

namespace Controller.Sys
{
    public interface ISysController
    {
        void SetLog(ILog log);
        void UpdateL1LastAliveTime(string time);
        bool UpdateSysValue(string parameterGroup, string parameter, string value);
        int GetSysProCoilWeight();

        bool UpdateConnectionStatuts(ConnectionType type, string ip, string port, string connectionStatuts);
    }
}

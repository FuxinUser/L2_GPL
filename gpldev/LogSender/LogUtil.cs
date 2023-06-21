using Akka.Actor;
using Akka.Event;

namespace LogSender
{
    public static class LogUtil
    {
        public static ILoggingAdapter GetLogger(this ActorSystem akkSys, string logFileName)
        {
            return Logging.GetLogger(akkSys, logFileName);
        }

    }
}

using Akka.Actor;
using Akka.Event;
using Core.Help;
using LabelPrint.Printer;

namespace GPLManager
{
    class PublicComm
    {
        static string actorSystemName = "GPLHMI";

        //public static ActorSystem actorSystem = null;
        public static IActorRef Client = null;
        public static IActorRef lprSndEdit = null;
        public static Akka.Event.ILoggingAdapter akkaLog = null;
        public static Akka.Event.ILoggingAdapter ClientLog = null;
        public static Akka.Event.ILoggingAdapter ExceptionLog = null;

        //public static LogHandler _log = null;   // init at start 

        public static void Start()
        {
            //get db connection string 
            //string connString = ConfigurationManager.ConnectionStrings["FUXIN_CPL"].ConnectionString;

            //init system
            ActSystem.CreateSystem(actorSystemName);
            ActSystem.SetAsClient();
            ActSystem.SetConnectionString(GlobalVariableHandler.Instance.strConn_GPL);  // set db connection string

            //init logging
            akkaLog = Logging.GetLogger(ActSystem._actSystem, "Akkalog");
            akkaLog.Info("akkalog start.....");

            ClientLog = Logging.GetLogger(ActSystem._actSystem, "HMIClientLog");
            ClientLog.Info("hmilog start....");

            ExceptionLog = Logging.GetLogger(ActSystem._actSystem, "ExceptionLog");
            ExceptionLog.Info("hmilog start....");

            //init controller
            Client = ActSystem._actSystem.ActorOf(Props.Create(() => new HMIClient(ClientLog)), "hmiclient");
            // client.Tell(new CPL1.ClientAlive());  // for debug

            string strRemoteIp = GlobalVariableHandler.Printer_IP;// IniSystemHelper.Instance.PrinterRemoteIP;
            int intRemotePort = GlobalVariableHandler.Printer_Port;//IniSystemHelper.Instance.PrinterRemotePort;
            var zebra = new Zebra(strRemoteIp, intRemotePort);
            lprSndEdit = ActSystem._actSystem.ActorOf(Props.Create(() => new PinterClient(ClientLog, zebra)), "HMIPrinter");
            //init portal sequence
            //logger = new LogHandler();
            //portal.Logger._debug = hmiLog.Debug;
            //portal.Logger._error = hmiLog.Error;
            //portal.Logger._info = hmiLog.Info;
            //portal.Logger._warning = hmiLog.Warning;
            //portal.LogInfo();
        }


        public static void Stop()
        {

        }
    }
}

using Akka.Actor;
using Core.Help;
using DBService.Repository.EventLog;
using LogRecord.Actor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WinformsMVP.Controls.Forms;
using LogSender;

/**
* Author: ICSC 余士鵬
* Date: 2019/10/25
* Description: 邏輯組裝區
* Reference: 
* Modified: 
*/
namespace LogRecord.Form
{
    public class LogPresenter : BaseFormPresenter<LogContract.IView, LogContract.IPresenter>, LogContract.IPresenter
    {
        private IActorRef _mgrActor;
       

        public LogPresenter(LogContract.IView view) : base(view)
        {
        }

        #region Flow
        //開啟視窗
        protected override void View_Load(object sender, EventArgs e)
        {

        }

        //視窗載入
        protected override void View_Shown(object sender, EventArgs e)
        {
            initSystem();        
        }

        protected override void View_Closed(object sender, EventArgs e)
        {

        }
        #endregion

        #region System Setting
        public void initSystem()
        {
            InitActorSystem(ConfigurationManager.AppSettings["ActorSystemName"]);
            setDBConnection(ConfigurationManager.AppSettings["DB_Name"]);
            setDicActSelFromAppSetting();
            createManagment();
        }
        private void InitActorSystem(String actSysName)
        {
            // Initialization system
            ActSystem.InitializeDic();
            // Create ActorSystem
            ActSystem.CreateSystem(actSysName);
            // Create akka log
            Akka.Event.ILoggingAdapter akkaLog = Akka.Event.Logging.GetLogger(ActSystem._actSystem, "AkkaLog");
            akkaLog.Info("AkkaLog start.....");
        }
        private void setDBConnection(String dbName)
        {
            // Set db connection string 
            ActSystem.SetConnectionString(ConfigurationManager.ConnectionStrings[dbName].ConnectionString);
        }
        private void setDicActSelFromAppSetting()
        {
            // Set all ActorSelection by AppSettings of the app.config
            List<KeyValuePair<string, string>> listAppSetting = ConfigurationManager.AppSettings.AllKeys
                                                                    .Where(key => key.Contains("Path_"))
                                                                    .Select(key => new KeyValuePair<string, string>(key.Split('_')[1], ConfigurationManager.AppSettings[key]))
                                                                    .ToList();
            ActSystem.SetDicActSels(listAppSetting);
        }
        
        private void createManagment()
        {
            // Create management
            ActSystem.CreateActor<LogMgr>();
            _mgrActor = ActSystem.GetDicCtrl(typeof(LogMgr).Name);
        }
        #endregion


       public void InsertLog(EventLogEntity.TBL_EventLog item)
        {
            if (item.System_ID.Equals(LogDef.SysServer))
            {
                Invoke(() => View.DisplayLog(item));
            }
            else
            {
                Invoke(() => View.DisplayHMILog(item));
            }

          
        }

        public void MgrActorTell(object message)
        {
            if (_mgrActor != null)
                _mgrActor.Tell(message);
        }
    }
}

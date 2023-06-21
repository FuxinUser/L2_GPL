using Akka.Actor;
using System;
using System.Collections.Generic;

namespace Core.Help
{
    public class ActSystem
    {
        /// <summary> Save the created of logs </summary>
        private static Dictionary<string, Akka.Event.ILoggingAdapter> _dicLogs = null;
        /// <summary> Save the created of actor </summary>
        private static Dictionary<string, IActorRef> _dicCtrls = null;
        /// <summary> Save the created of ActorSelection </summary>
        private static Dictionary<string, ActorSelection> _dicActSels = null;

        /// <summary> Record the ActorSystem for this main program </summary>
        public static ActorSystem _actSystem = null;
        /// <summary> Default user role is server, call SetAsClient() to set to false </summary>
        public static bool IsServer = true;
        /// <summary> Connection string for handlers to access database </summary>
        public static string StrConn = string.Empty;

        /// <summary> The ip address of local </summary>
        public static string LocalIP = "0.0.0.0";
        /// <summary> The port of local </summary>
        public static int? LocalPort = null;
        /// <summary> The ip address of remote target </summary>
        public static string RemoteIP = "127.0.0.1";
        /// <summary> The port of remote target </summary>
        public static int? RemotePort = null;

        //TODO 待整理

        #region "Dictionary processing"
        /// <summary>
        ///     Initialize dictionary
        /// </summary>
        public static void InitializeDic()
        {
            if (_dicLogs == null) _dicLogs = new Dictionary<string, Akka.Event.ILoggingAdapter>();
            if (_dicCtrls == null) _dicCtrls = new Dictionary<string, IActorRef>();
            if (_dicActSels == null) _dicActSels = new Dictionary<string, ActorSelection>();
        }


        /// <summary>
        ///     Set the load dictionary
        /// </summary>
        /// <typeparam name="T"> Load tyoe </typeparam>
        /// <param name="dic"> The dictionary to be set </param>
        /// <param name="key"> Keyword for the load dictionary </param>
        /// <param name="tVal"> Add to the value of the dictionary </param>
        /// <returns></returns>
        private static bool SetDic<T>(ref Dictionary<string, T> dic, string key, T tVal)
        {
            if (!dic.ContainsKey(key))
            {
                dic.Add(key, tVal);

                return true;
            }
            else return false;
        }


        #region For _dicLogs
        /// <summary>
        ///     Set log to the dictionary
        /// </summary>
        /// <param name="key"> Keyword for _dicLogs </param>
        /// <param name="log"> NLog element </param>
        /// <returns></returns>
        public static bool SetDicLogs(string key, Akka.Event.ILoggingAdapter log)
        {
            return SetDic<Akka.Event.ILoggingAdapter>(ref _dicLogs, key, log);
        }


        /// <summary>
        ///     Get logs
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Akka.Event.ILoggingAdapter> GetDicLogs()
        {
            return _dicLogs;
        }


        /// <summary>
        ///     Get log by key of the dictionary
        /// </summary>
        /// <param name="key"> Keyword for _dicLogs </param>
        /// <returns></returns>
        public static Akka.Event.ILoggingAdapter GetDicLog(string key)
        {
            if (_dicLogs.ContainsKey(key))
            {
                return _dicLogs[key];
            }
            else
            {
                return null;
            }
        }
        #endregion


        #region For _dicCtrls
        /// <summary>
        ///     Set actor to the dictionary
        /// </summary>
        /// <param name="key"> Keyword for _dicCtrls </param>
        /// <param name="ctrl"> Actor element </param>
        /// <returns></returns>
        public static bool SetDicCtrls(string key, IActorRef ctrl)
        {
            return SetDic<IActorRef>(ref _dicCtrls, key, ctrl);
        }


        /// <summary>
        ///     Get actors
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, IActorRef> GetDicCtrls()
        {
            return _dicCtrls;
        }


        /// <summary>
        ///     Get actor by key of the dictionary
        /// </summary>
        /// <param name="key"> Keyword for _dicCtrls </param>
        /// <returns></returns>
        public static IActorRef GetDicCtrl(string key)
        {
            if (_dicCtrls.ContainsKey(key))
            {
                return _dicCtrls[key];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        ///     Check if the actor by key of the dictionary
        ///     If it exsit, stop the actor and remove it form dictionary
        /// </summary>
        /// <param name="context"> Actor content </param>
        /// <param name="key"> Keyword for _dicCtrls </param>
        public static void CheckStopDicCtrl(IUntypedActorContext context, string key)
        {
            if (_dicCtrls.ContainsKey(key))
            {
                _dicLogs[key].Info($"{key} actor exsit, stop {key} and remove it form dictionary");
                context.Stop(_dicCtrls[key]);
                _dicCtrls.Remove(key);
            }
        }
        #endregion


        #region For _dicActSels
        /// <summary>
        /// Set ActorSelection of the dictionary
        /// </summary>
        /// <param name="key"> Keyword </param>
        /// <param name="actSel"> Builded ActorSelection </param>
        /// <returns></returns>
        public static bool SetDicActSels(string key, ActorSelection actSel)
        {
            return SetDic<ActorSelection>(ref _dicActSels, key, actSel);
        }


        /// <summary>
        ///     Set ActorSelection of the dictionary
        /// </summary>
        /// <param name="list">KeyValuePair item</param>
        public static void SetDicActSels(List<KeyValuePair<string, string>> list)
        {
            foreach (KeyValuePair<string, string> item in list)
            {
                SetDicActSels(item.Key, _actSystem.ActorSelection(item.Value));
            }
        }


        /// <summary>
        ///     Get ActorSelections
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, ActorSelection> GetDicActSels()
        {
            return _dicActSels;
        }


        /// <summary>
        ///     Get ActorSelection by key of the dictionary
        /// </summary>
        /// <param name="key"> Keyword for _dicActSels </param>
        /// <returns></returns>
        public static ActorSelection GetDicActSel(string key)
        {
            if (_dicActSels.ContainsKey(key))
            {
                return _dicActSels[key];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #endregion


        #region "Setup parameter"
        /// <summary>
        ///     Create new ActorSystem
        /// </summary>
        /// <param name="sysName"> The name of this ActorSystem </param>
        public static void CreateSystem(string sysName)
        {
            _actSystem = ActorSystem.Create(sysName);
        }

        /// <summary>
        ///     Set the ConnectionString for this main program
        /// </summary>
        /// <param name="strConn"> Sql ConnectionString text </param>
        public static void SetConnectionString(string strConn)
        {
            StrConn = strConn;
        }

        /// <summary>
        ///     Set if this main program is client
        /// </summary>
        public static void SetAsClient()
        {
            IsServer = false;
        }
        #endregion


        #region "Create actor"
        /// <summary>
        ///     Create actor and save into dictionary
        /// </summary>
        /// <typeparam name="T"> Load type </typeparam>
        /// <param name="ip"> Local or remote ip </param>
        /// <param name="port"> Local or remote port </param>
        /// <returns></returns>
        public static void CreateActor<T>(string ip = null, int? port = null) where T : ActorBase
        {
            //  Get actor name from class name
            string actName = typeof(T).Name;

            //  Create logging in ActorSystem
            SetDicLogs(actName, Akka.Event.Logging.GetLogger(_actSystem, actName + "Log"));
            GetDicLog(actName).Info($"{actName}Logs start.....");

            //  Create actor in ActorSystem
            if (ip != null && port != null)
            {
                SetDicCtrls(actName, _actSystem.ActorOf(Props.Create(() => (T)Activator.CreateInstance(typeof(T), GetDicLog(actName), ip, port)), actName));
            }
            else
            {
                SetDicCtrls(actName, _actSystem.ActorOf(Props.Create(() => (T)Activator.CreateInstance(typeof(T), GetDicLog(actName))), actName));
            }
            GetDicLog(actName).Info($"{actName} actor start.....");
        }


        /// <summary>
        ///     Create actor and save into dictionary
        /// </summary>
        /// <typeparam name="T"> Load type </typeparam>
        /// <param name="context">Parent actor context</param>
        /// <param name="ip"> Local or remote ip </param>
        /// <param name="port"> Local or remote port </param>
        /// <returns></returns>
        public static void CreateActor<T>(IUntypedActorContext context, string ip = null, int? port = null) where T : ActorBase
        {
            //  Get actor name from class name
            string actName = typeof(T).Name;

            //  Create logging in ActorSystem
            SetDicLogs(actName, Akka.Event.Logging.GetLogger(_actSystem, actName + "Log"));
            GetDicLog(actName).Info($"{actName}Logs start.....");
   
            //  Check if actor exsit
            CheckStopDicCtrl(context, actName);

            //  Create actor in ActorSystem
            if (ip != null && port != null)
            {
                SetDicCtrls(actName, context.ActorOf(Props.Create(() => (T)Activator.CreateInstance(typeof(T), GetDicLog(actName), ip, port)), actName));
            }
            else
            {
                SetDicCtrls(actName, context.ActorOf(Props.Create(() => (T)Activator.CreateInstance(typeof(T), GetDicLog(actName))), actName));
            }
            GetDicLog(actName).Info($"{actName} actor start.....");
        }

        #endregion
    }
}

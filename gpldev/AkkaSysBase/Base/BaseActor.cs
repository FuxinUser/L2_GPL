using Akka.Actor;
using System;
using LogSender;

/**
 * Author: ICSC余士鵬
 * Date: 2019/9/19
 * Description: Akka底層(角色生命週期)
 * Reference: 
 * Modified: 
 */
namespace AkkaSysBase.Base
{
    public class BaseActor : ReceiveActor
    {
        protected ILog _log;   
        private string _actorName;

        public BaseActor(ILog log)
        {
            _log = log;
            _actorName = Context.Self.Path.Name;
        }


        /// <summary>
        ///     Try action of flow
        /// </summary>
        protected virtual void TryFlow(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                _log.E("例外事件發生",$"ex.Message={ex.Message}");
                _log.E("例外事件發生",$"ex.StackTrace={ex.StackTrace}");
            }
            finally
            {
            }
        }


        protected override void PreStart()
        {
            _log.I("AThread生命週期", _actorName + "PreStart");
            base.PreStart();         
        }
        protected override void PreRestart(Exception reason, object message)
        {
            _log.E("AThread生命週期", _actorName + " PreRestart");
            _log.E("AThread生命週期", "Reason:" + reason.Message);      
            base.PreRestart(reason, message);


        }
        protected override void PostStop()
        {
            _log.I("AThread生命週期", _actorName + " PostStop");
            base.PostStop();

        }
        protected override void PostRestart(Exception reason)
        {
            _log.E("AThread生命週期", _actorName + " PostRestart");
            _log.E("AThread生命週期", "Reason:" + reason.Message);
            base.PostRestart(reason);
        }
    }
}

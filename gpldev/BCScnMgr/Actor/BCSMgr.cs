using AkkaSysBase;
using AkkaSysBase.Base;
using LogSender;

namespace BCScnMgr
{
    public class BCSMgr : BaseActor
    {
        public BCSMgr(ISysAkkaManager akkaManager, ILog log) : base(log)
        {
            akkaManager.CreateChildActor<BCScnConn>(Context);
            akkaManager.CreateChildActor<BCScnRcvEdit>(Context);
            akkaManager.CreateChildActor<BCScnSndEdit>(Context);

        }

        //protected override SupervisorStrategy SupervisorStrategy()
        //{
        //    // Retries to 10 times within a minute
        //    // This is the number of times the child actor is allowed to restart within the time window specified. 
        //    // The negative value means no limit.
        //    return new OneForOneStrategy(
        //        maxNrOfRetries: 10,
        //        withinTimeRange: TimeSpan.FromMinutes(1),
        //        localOnlyDecider: ex =>
        //        {
        //            if(ex is ArgumentException)
        //            {
        //                return Directive.Resume;
        //            }

        //            if(ex is NullReferenceException)
        //            {
        //                return Directive.Restart;
        //            }

        //            return Directive.Stop;
        //            //return ex switch
        //            //{
        //            //    // Actor Life Operator
        //            //    // the actor will resume as if nothing happened
        //            //    ArgumentException ae => Directive.Resume,
        //            //    //  it will restart the actor and move on. And for any other unknown exception, it will stop the actor.
        //            //    NullReferenceException ne => Directive.Restart, // Restart New Instance

        //            //    _ => Directive.Stop
        //            //};
        //        }
        //        );
        //}

    }
}

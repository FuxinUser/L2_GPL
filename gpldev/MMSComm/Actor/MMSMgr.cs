using AkkaSysBase;
using AkkaSysBase.Base;
using LogSender;

/**
 * Author: ICSC余士鵬
 * Date: 2019/10/09
 * Description: MES Process Actor (Port:8101)
 * Reference: 
 * Modified: 
 */
namespace MMSComm.Actor
{
    public class MMSMgr : BaseActor
    {

        public MMSMgr(ISysAkkaManager akkaManager, ILog log) : base(log)
        {

            log.D("創建AThread", "Create RCV App");
            akkaManager.CreateChildActor<MMSRcv>(Context);

            log.D("創建AThread", "Create RcvEdit App");
            akkaManager.CreateChildActor<MMSRcvEdit>(Context);

            log.D("創建AThread", "Create Snd App");
            akkaManager.CreateChildActor<MMSSnd>(Context);

            log.D("創建AThread", "Create SndEdit App");
            akkaManager.CreateChildActor<MMSSndEdit>(Context);
        }

   
    }
}

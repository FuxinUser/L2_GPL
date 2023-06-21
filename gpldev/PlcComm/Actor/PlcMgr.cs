using AkkaSysBase;
using AkkaSysBase.Base;
using LogSender;

/**
 * Author: ICSC余士鵬
 * Date: 2019/9/19
 * Description: 管理角色(父親)
 
 *           Mgr
 *        /   |  \   \  
 *  Rcv, RcvEdit, Snd, SndEdit
 
 * Reference: 
 * Modified: 
 */
namespace PLCComm.Actor
{
    public class PlcMgr : BaseActor
    {
        public PlcMgr(ISysAkkaManager akkaManager, ILog log) : base(log)
        {
            log.D("創建AThread", "Create RCV App");
            akkaManager.CreateChildActor<PlcRcv>(Context);

            log.D("創建AThread", "Create CycleRCV App");
            akkaManager.CreateChildActor<PlcCycleRcv>(Context);

            log.D("創建AThread", "Create RcvEdit App");
            akkaManager.CreateChildActor<PlcRcvEdit>(Context);

            log.D("創建AThread", "Create Snd App");
            akkaManager.CreateChildActor<PlcSnd>(Context);

            log.D("創建AThread", "Create SndEdit App");
            akkaManager.CreateChildActor<PlcSndEdit>(Context);
        }
  
    }
}

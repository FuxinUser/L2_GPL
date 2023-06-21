using AkkaSysBase.Base;
using AkkaSysBase;
using LogSender;

/**
 * Author: ICSC余士鵬
 * Date: 2019/10/09
 * Description: WMS Process Actor (Port:8111)
 * Reference: 
 * Modified: 
 */
namespace WMSComm.Actor
{
    public class WMSMgr : BaseActor
    {

     
        public WMSMgr(ISysAkkaManager akkaManager, ILog log) : base(log)
        {

            log.D("創建AThread", "Create WMSRCV App");
            akkaManager.CreateChildActor<WMSRcv>(Context);

            log.D("創建AThread", "Create WMSRcvEdit App");
            akkaManager.CreateChildActor<WMSRcvEdit>(Context);

            log.D("創建AThread", "Create WMSSnd App");
            akkaManager.CreateChildActor<WMSSnd>(Context);

            log.D("創建AThread", "Create WMSSndEdit App");
            akkaManager.CreateChildActor<WMSSndEdit>(Context);
        }

        //private void Handle_DoCmd(EventDef.CMDSET msg)
        //{
        //    ActSystem.GetDicActSel(nameof(WMSSnd)).Tell(msg);
        //}

        //private void SetDebugLogSwitch(EventDef.LOGSW msg)
        //{
        //    ActSystem.GetDicActSel(nameof(WMSRcvEdit)).Tell(msg);
        //    ActSystem.GetDicActSel(nameof(WMSSndEdit)).Tell(msg);
        //    ActSystem.GetDicActSel(nameof(WMSRcv)).Tell(msg);
        //    ActSystem.GetDicActSel(nameof(WMSSnd)).Tell(msg);
        //}

     
    }
}

using AkkaSysBase;
using AkkaSysBase.Base;
using LogSender;

namespace LabelPrint.Actor
{
    public class LprMgr : BaseActor
    {
        public LprMgr(ISysAkkaManager akkaManager, ILog log) : base(log)
        {
 
            akkaManager.CreateChildActor<LprSndEdit>(Context);

            akkaManager.CreateChildActor<PinterClient>(Context);

        }
    }
}

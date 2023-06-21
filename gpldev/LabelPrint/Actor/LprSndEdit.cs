using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using LabelPrint.Printer;
using LogSender;
using MSMQ;
using MSMQ.Core.MSMQ;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using static LabelPrint.Model.ZebraModel;

namespace LabelPrint.Actor
{
    public class LprSndEdit : BaseActor
    {
        private IActorRef _selfActor;
        private IActorRef _sndActor;

        public LprSndEdit(ISysAkkaManager akkaManager, ILog log) : base(log)
        {         
            _selfActor = akkaManager.GetActor(GetType().Name);
            _sndActor = akkaManager.GetActor(nameof(PinterClient));

            MQPool.ReceiveFromLpr(x => _selfActor.Tell(x));

            Receive<MQPool.MQMessage>(x => Handle_MQ_Message(x));

      
        }

        private void Handle_MQ_Message(MQPool.MQMessage msg)
        {


            if (msg.ID == InfoLpr.ManualPrint.Event)
            {
                var printLabel = msg.Data as CS07_PrintLabel;
                var coilID = printLabel.CoilID;
                _log.I("標籤手動列印", $"列印鋼捲{coilID}標籤");
                var zcmd = new ZebraCommand();
                zcmd.ZPL = coilID.Trim().zplCmd();
                _sndActor.Tell(zcmd);
                return;
            }


            if (msg.ID == InfoLpr.CoilInExitSK2.Event)
            {
                var coilID = msg.Data as string;
                _log.I("列印鋼捲標籤", $"列印鋼捲{coilID}標籤");
                var zcmd = new ZebraCommand();
                zcmd.ZPL = coilID.Trim().zplCmd();
                _sndActor.Tell(zcmd);

            }
        }

        private string GenPrintZPLFormat(string coilID)
        {
            ZplInstructionUtil zplCmd = new ZplInstructionUtil();
            var zpl = "";
            zplCmd.LabelLength(100)
                .LabelHome(5, 50)
                .FieldOrigin(2, 2, 2)
                .FieldBlock("C")
                .FieldSeparator();
            zpl = zpl + zplCmd.EndZPL().GetZplCode();
            return zpl;
        }
    }
}

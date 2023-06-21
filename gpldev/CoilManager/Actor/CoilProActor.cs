using AkkaSysBase;
using AkkaSysBase.Base;
using Controller;
using Controller.Coil;
using Core.Util;
using LogSender;
using MsgConvert;
using MsgStruct;
using MSMQ.Core.MSMQ;
using Core.Define;

namespace CoilManager.Actor
{
    public class CoilProActor : BaseActor
    {
        private ICoilController _coilService;
        private ICoilController _coilController;

        public CoilProActor(ISysAkkaManager akkaManager, ICoilController coilService, ICoilController coilController, ILog log) : base(log)
        {
            _coilService = coilService;
            _coilService.SetLog(log);
            _coilController = coilController;
            _coilController.SetLog(log);


            // 結算PDO
            Receive<L1L2Rcv.Msg_102_PDO>(message => AccountPDO(message));
            ReceiveAny(message => RcvObject(message));

            //測試
            //var testmsg = new L1L2Rcv.Msg_102_PDO();
            //testmsg.CoilNo = "CG220020470000".ToByteArray();
            //AccountPDO(testmsg);
        }

        /// <summary>
        /// 結算PDO 
        /// </summary>
        private void AccountPDO(L1L2Rcv.Msg_102_PDO msg)
        {
            _log.I("結算PDO", "收到CoilDismount訊息，開始結算PDO");
            _log.D("結算PDO", msg.ToJson());

            ////更新PDI 鋼捲生產結束時間
            //_coilService.UpdatePDIEndTime(msg.CoilIDNo);

            var pdo = _coilService.CalculatePDOResult(msg);
            if (pdo == null)
            {
                _log.E("PDO產生失敗", "PDO計算失敗");
                return;
            }


            //var updateOK = _coilService.UpdatePDO(pdo);
            var insertOK = _coilService.CreatePDO(pdo);
            // GW13 鋼捲產出資訊 - PDO資訊
            if (!insertOK)
            {
                _log.E("PDO存取DB失敗", "PDO產生成功，但存取失敗，請重新操作");
                return;
            }
            var SleeveData = _coilController.GetSleeveData(pdo.Out_Sleeve_Type_Code);
            var wmsPdoInfo = pdo.ConvertWMSPdoInfo(SleeveData.Out_Mat_Inner_Dia.ToString(), SleeveData.Sleeve_Thick.ToString("0.000"), SleeveData.Sleeve_Width.ToString("0.0"));
            MQPoolService.SendToWMS(InfoWMS.InfoiCoilPDOMsg.Data(wmsPdoInfo));

            //狀態變更 ProduceDone_Statuts D-已產出  
            _coilService.UpdateScheduleStatuts(pdo.In_Coil_ID, CoilDef.ScheduleStatuts.ProduceDone_Statuts);

            // 刪除此鋼捲流程
            //_coilController.DeleteCoilScheduleOnly(pdo.In_Coil_ID);
            //_log.I("刪除排程", $"刪除{pdo.In_Coil_ID}排程");


            //發送排程資訊preset
            var coilIDs = _coilController.QueryUnscheduleCoils(40);
            if (coilIDs == null)
            {
                _log.E("通知DataSetup", "撈取40筆鋼捲失敗");
                return;
            }

            // 傳送40筆給Stp
            _log.I("通知DataSetup", "通知DataSetup組40筆Preset資料發送給L1");
            MQPoolService.SendToDtStp(InfoDataSetup.CoilSchedIDTo_204_205Msg.Data(coilIDs));
            // 傳送排程給WMS
            //_log.I("傳送排程資訊給WMS", "通知WMS排程資訊");
            //MQPoolService.SendToWMS(InfoWMS.InfoCoilScheduleMsg.Data(coilIDs));
        }

        private void RcvObject(object message)
        {
            _log.E("AThread接收資料-RcvObject", $"無法解析資料! Type:{message.GetType()} From Sender:{Sender.Path}");
        }
    }
}

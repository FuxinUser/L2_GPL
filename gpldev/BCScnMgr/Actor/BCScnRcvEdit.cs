using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller;
using Controller.Coil;
using Controller.Track;
using Core.Define;
using Core.Util;
using DataMod.BarCode;
using LogSender;
using MSMQ.Core.MSMQ;
using static DataMod.BarCode.BCSDataModel;
using static DataModel.HMIServerCom.Msg.SCCommMsg;




namespace BCScnMgr
{
    public class BCScnRcvEdit : BaseActor
    {
        //private IActorRef _selfActor;
        private IActorRef _sndEditActor;
        private ITrackingController _trkService;
        private ICoilController _coilService;

        public BCScnRcvEdit(ISysAkkaManager akkaManager, ICoilController coilService, ITrackingController trkService, ILog log) : base(log)
        {
            //_selfActor = akkaManager.GetActor(GetType().Name);
            _sndEditActor = akkaManager.GetActor(nameof(BCScnSndEdit));
            _trkService = trkService;
            _coilService = coilService;
            _trkService.SetLog(log);
            _coilService.SetLog(log);

            //MQPool.ReceiveFromBCScnRcvEdit(x => _selfActor.Tell(x));
            //Receive<MQPool.MQMessage>(message => HandleMQMessage(message));

            Receive<byte[]>(message => ParsingByteData(message));
            ReceiveAny(message => RcvObject(message));
        }


        /// <summary>
        /// 解析Barcode掃描到的資料
        /// </summary>
        private void ParsingByteData(byte[] bytes)
        {
            // 抓取BarCode機訊號標頭
            var header = bytes.RawDeserialize(typeof(BCSModel.BCSHeader)) as BCSModel.BCSHeader;

            // 確定訊號標頭是否解析成功
            if (header == null)
            {
                _log.E("TCP接收資料", "解析BarCode機掃描資料標頭失敗");
                return;
            }

            // 解析訊號
            var scanCoil = bytes.RawDeserialize(typeof(BCSModel.BCSScanCoil_BS01)) as BCSModel.BCSScanCoil_BS01;
            if (scanCoil == null)
            {
                _log.E("TCP接收資料", "解析BarCode機掃描資料失敗");
                return;
            }

            // 索取標頭訊號ID
            var msgID = header.Message_Id.ToStr();

            // 根據ID判斷事件
            if (msgID.Equals(DeviceDef.BCSScanCoil))
            {
                // 掃描位置
                var scnPos = scanCoil.CoilPos.ToStr();
                // 掃描剛卷
                var scnCoilNo = scanCoil.CoilNo.ToStr();

                _log.I("BarCode機掃描資料", $"CoilNo:{scnCoilNo} POS ID:{scnPos}");

                // 掃描內容
                var scanContent = GenScnContent(scnPos, scnCoilNo);

                _log.I("BarCode機掃描資料轉譯成Level2使用Model", $"CoilNo:{scanContent.ScanCoilNo} POS ID:{scanContent.ScanPosition}");

                if (IsEntryPos(scnPos))
                {
                    // 入口ID掃描
                    EntryScnPro(scanContent);
                    return;
                }

                // 出口ID掃描
                DeliveryScnPro(scanContent);
                return;
            }

        }

        /// <summary>
        /// 入口鋼卷ID掃描判定處理
        /// </summary>
        private void EntryScnPro(BarCodeScnContent msg)
        {

            var trackMap = _trkService.GetTrackMap();
            // 位置上實際鋼卷
            var trackMapPosCoilNo = trackMap.GetCoilNoFromPOS(msg.ScanPosition);
            var scanPos = msg.GetL1Position();


            // 判斷掃描位置是否為空

            // 空(無鋼捲號) : 天車入料
            if (trackMap.IsPosEmpty(msg.ScanPosition))
            {
                // 通知WMS
                //InfoWMSScanID(msg);



                // 通知BarCode機
                var scanOK = new SC01_ScnBarcodeID(BCScanResult.Sucess, msg.ScanCoilNo, trackMapPosCoilNo, scanPos);
                _sndEditActor.Tell(scanOK);


                // 下发203报文到一级
                MQPoolService.SendToL1(InfoL1.SndPDITM2203Msg.Data(scanOK));
                _sndEditActor.Tell(scanOK);

                return;
            }


            // 有鋼捲號 : 台車入料
            // Scan Coil ID != POS Coil ID

            if (!trackMap.IsSameCoilNo(msg.ScanCoilNo, msg.ScanPosition))
            {
                // 不一致          
                _log.E("掃描入口捲號失敗", $"此位置無此鋼捲ID:{msg.ScanCoilNo} 位置: {msg.ScanPosition} ");
                var scnResultFail = new SC01_ScnBarcodeID(BCScanResult.Error, msg.ScanCoilNo, trackMapPosCoilNo, scanPos);
                
                // 通知掃描結果
                InfoL1ScannEntryResult(scnResultFail, $"掃描鋼卷:{msg.ScanCoilNo} {msg.GetPOSStr()}位置上綱卷:{trackMapPosCoilNo}");

                return;
            }

            // 一致 天車入料 or 台車入料
            // Scan Coil ID == POS Coil ID 

            var updateOK = _coilService.UpdateEntryScanCoilInfo(msg.ScanCoilNo, true);
            var updateCoilCheckOk = updateOK ? BCScanResult.Sucess : BCScanResult.Error;
            //更新排程狀態
            _coilService.UpdateScheduleStatuts(msg.ScanCoilNo, CoilDef.ScheduleStatuts.IdentifyOK_Statuts);
            _log.I($"掃描入口捲號", $"掃描入口捲號是否成功 => {updateCoilCheckOk}");
            var scnResultOk = new SC01_ScnBarcodeID(updateCoilCheckOk, msg.ScanCoilNo, trackMapPosCoilNo, scanPos);
            InfoL1ScannEntryResult(scnResultOk);

        }

        /// <summary>
        /// 出口鋼卷ID掃描判定處理
        /// </summary>
        private void DeliveryScnPro(BarCodeScnContent msg)
        {
            var trackMap = _trkService.GetTrackMap();
            // 位置上實際鋼卷
            var trackMapPosCoilNo = trackMap.GetCoilNoFromPOS(msg.ScanPosition);

            if (!trackMap.IsSameCoilNo(msg.ScanCoilNo, msg.ScanPosition))
            {
                var scnResult = new SC01_ScnBarcodeID(BCScanResult.Error, msg.ScanCoilNo, trackMapPosCoilNo, msg.GetL1Position());
                InfoL1ScannDeliveryResult(scnResult, $"掃描鋼卷:{msg.ScanCoilNo} {msg.GetPOSStr()} 位置上綱卷:{trackMapPosCoilNo}");
            }
            else
            {
                // 與CoilMap鋼捲一致, 更新PDO Exit_CoilID_Checked 狀態為1 
                var updateOK = _coilService.UpdatePDOExCoilIDChecked(msg.ScanCoilNo, L2SystemDef.CheckCoilNo);
                var updateCoilCheckOk = updateOK ? BCScanResult.Sucess : BCScanResult.Error;
                var scnResult = new SC01_ScnBarcodeID(updateCoilCheckOk, msg.ScanCoilNo, trackMapPosCoilNo, msg.GetL1Position());

                // TODO 鋼捲Check資料庫更新失敗通知Barcode機
                var eventMsg = updateOK ? "掃描成功" : "掃描失敗";
                var eventPush = new SC03_EventPush(eventMsg, $"{msg.ScanCoilNo}" + eventMsg);
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

                InfoL1ScannDeliveryResult(scnResult);
            }
        }

        private void InfoL1ScannDeliveryResult(SC01_ScnBarcodeID scnResult, string eventMsg = "")
        {

            MQPoolService.SendToPCCom(InfoHMI.BarcodeScanResult.Data(scnResult));

            // 錯誤 Event Push
            if (!eventMsg.Equals(""))
            {
                var eventPush = new SC03_EventPush("掃描失敗", eventMsg);
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
            }

            MQPoolService.SendToL1(InfoL1.SndDeliveryBCConfirm209Msg.Data(scnResult));

            _sndEditActor.Tell(scnResult);
        }

        private void InfoL1ScannEntryResult(SC01_ScnBarcodeID scnResult, string eventMsg = "")
        {

            MQPoolService.SendToPCCom(InfoHMI.BarcodeScanResult.Data(scnResult));

            // 錯誤 Event Push
            if (!eventMsg.Equals(""))
            {
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("掃描失敗", eventMsg)));

            }
            else
            {
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("入口鋼卷身分確認", scnResult.CoilNo + "掃描成功")));
            }


            MQPoolService.SendToL1(InfoL1.SndPDITM2203Msg.Data(scnResult));

            _sndEditActor.Tell(scnResult);
        }

        private void InfoWMSScanID(BarCodeScnContent msg)
        {

            _log.I("通知WMS掃描ID", $"掃描位置為{msg.GetPOSStr()}鋼捲ID為{msg.ScanCoilNo}");
            var scanResult = new ScanResult(msg.GetWMSPosition(), msg.ScanCoilNo);                      
            MQPoolService.SendToWMS(InfoWMS.InfoBCSScanID.Data(scanResult));
        }


        // 隔離用 pos 1:ESK01 2:ETOP 3:DSK01 4:DSK02 5:DTOP
        private BarCodeScnContent GenScnContent(string pos, string coilNo)
        {
            var scnContent = new BarCodeScnContent();
            scnContent.ScanCoilNo = coilNo;
            scnContent.SetPosition(pos);
            return scnContent;
        }
        private bool IsEntryPos(string pos)
        {
            return pos.Equals(DeviceDef.BCSDefPOS_ESK01) || pos.Equals(DeviceDef.BCSDefPOS_ETOP);
        }
        private void RcvObject(object msg)
        {
            _log.E("【AThread接收資料-RcvObject】", $"VaildActor 無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }

    }
}

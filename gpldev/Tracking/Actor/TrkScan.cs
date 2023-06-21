using Akka.Actor;
using AkkaSysBase.Base;
using Controller;
using Controller.Coil;
using Controller.Track;
using Core.Define;
using DataMod.WMS.LogicModel;
using LogSender;
using MSMQ.Core.MSMQ;
using System;
using static DataModel.HMIServerCom.Msg.SCCommMsg;


namespace Tracking.Actor
{
    public class TrkScan : BaseActor
    {

        private ITrackingController _trkService;
        private ICoilController _coilService;
        private ICancelable _tmrTrkScan;
        private ICoilController _coilController;

        public TrkScan(ITrackingController trkService, ICoilController coilService, ICoilController coilController, ILog log) : base(log)
        {
            _trkService = trkService;
            _coilService = coilService;
            _coilController = coilController;


            _coilController.SetLog(log);


            StartScanTmr(5, EventDef.CMDSET.TRKSCAN);
            Receive<EventDef.CMDSET>(msg => ProEventCmd(msg));
        }

        protected override void PostStop()
        {
            base.PostStop();
            _tmrTrkScan.CancelIfNotNull();
        }

        /// <summary>
        /// 開始Scan
        /// </summary>
        private void StartScanTmr(int second, object message)
        {
            _tmrTrkScan = new Cancelable(Context.System.Scheduler);
            var interval = TimeSpan.FromSeconds(second);
            Context.System.Scheduler.ScheduleTellRepeatedly(interval, interval, Self, message, Self, _tmrTrkScan);
        }

        // Scan觸發Pro
        private void ProEventCmd(EventDef.CMDSET msg)
        {

            var isAutoMode = _trkService.IsSystemAutoValueOn(L2SystemDef.GPLGroup, DBParaDef.SysParaAutoInputFlag);
            if (!isAutoMode)
                return;

            _log.I("產線入料模式", "[自動]"); 
            //// 判定EntryTop 是否有鋼捲
            if (!_trkService.InvaildHasEntryTopCoilID())
            {
                //排程若有入料要求狀態鋼捲則不發入料要求
                var ReqCoilID = _coilController.QueryScheduleRequestCoils(10);
                if (ReqCoilID.Count > 0)
                {
                    //_log.I("目前已有鋼捲排程狀態為R[要求入料]", $"鋼捲號 {ReqCoilID[0]}");
                    return;

                }

                //取得排程第一颗钢卷 排除P(生產中),F(已入料),D(已產出),C(回退)
                var FirstCoil = _coilController.GetFirstCoilSchedule();
                if (!FirstCoil.Trim().Equals(string.Empty))
                {
                    var coilPDI = _coilController.GetPDI(FirstCoil);
                    string coilTurn = string.Empty;
                    switch (coilPDI.Uncoil_Direction)
                    {
                        //上开卷 带尾朝南
                        case "U":
                            coilTurn = "2";
                            break;
                        //下开卷 带尾朝北
                        case "L":
                            coilTurn = "5";
                            break;
                        default:
                            coilTurn = "";
                            break;
                    }

                    // 發GW15 To WMS 入料要求
                    var reqMsg = new ProdLineCoilReq(WMSSysDef.Cmd.ReqWMSEntryCoil, FirstCoil.Trim(), WMSSysDef.SkPos.ETOP, coilTurn);
                    //_log.I("自動入料", "發送入料要求給WMS");
                    _log.I("自動入料", $"發送入料要求給WMS {FirstCoil.Trim()}");
                    MQPoolService.SendToWMS(InfoWMS.InfoCoilEntryOrDeliveryReq.Data(reqMsg));

                    //通知HMI 顯示自動入料資訊
                    var eventPush = new SC03_EventPush("自動入料", $"發送入料要求給WMS,捲號 {FirstCoil.Trim()}");
                    MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

                    // 更新旗標
                    _coilController.UpdateIsInfoWMSDown(true, FirstCoil.Trim());

                    // 更新鋼捲狀態為要求入料
                    _coilController.UpdateScheduleStatuts(FirstCoil.Trim(), CoilDef.ScheduleStatuts.RequestEntryCoil_Statuts);

                };


            }
            //// 索取TrkMap
            //var coilMap = _trkService.GetTrackMap();
            //if (coilMap == null)
            //{
            //    _log.E("抓取Trck失敗", "Track資料為空");
            //    return;
            //}
            //var topCoil = coilMap.Entry_TOP.Trim();

            //// 判斷Top是否為空
            //if (topCoil.Equals(string.Empty))
            //{
            //    //檢查是否有要求入料鋼捲狀態
            //    var ReqCoilID = _coilService.QueryScheduleRequestCoils(1);
            //    if (ReqCoilID.Count == 1)
            //    {
            //        _log.I("目前已有鋼捲排程狀態為R[要求入料]",$"鋼捲號 {ReqCoilID[0]}");
            //        return;

            //    }


            //    // Top為空索取第一筆鋼捲
            //    var coilId = _coilService.QueryUnscheduleCoils(1);
            //    if (coilId.Count == 0)
            //    {
            //        _log.I("目前無鋼捲生產排程", "目前無鋼捲生產排程");
            //        return;
            //    }
            //    var coil = coilId[0].Trim();


            //    // 撈取PDI比對是否已發送過期標
            //    var pdi = _coilService.GetPDI(coil);
            //    if (pdi == null || pdi.Is_Info_WMS_Down.Equals(EventDef.TRUE))
            //        return;


            //    // 更新旗標
            //    _coilService.UpdateIsInfoWMSDown(true, coil);

            //    // 通知WMS
            //    _log.I("TOP點為空", $"通知WMS{coil}入料要求");
            //    var reqMsg = new ProdLineCoilReq(WMSSysDef.Cmd.ReqWMSEntryCoil, coil, WMSSysDef.SkPos.ETOP);
            //    MQPoolService.SendToWMS(InfoWMS.InfoCoilEntryOrDeliveryReq.Data(reqMsg));

            //    // 更新鋼捲狀態為要求入料
            //    _coilController.UpdateScheduleStatuts(coil, CoilDef.ScheduleStatuts.RequestEntryCoil_Statuts);

            //};

        }
    }
}

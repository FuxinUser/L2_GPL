using BLL;
using BLL.Trck;
using Core.Define;
using Core.Util;
using LogSender;
using MsgConvert;
using MsgConvert.DBTable;
using MsgStruct;
using System;
using static DBService.Repository.CoilMapEntity;
using static MsgStruct.L1L2Rcv;

namespace Controller.Track
{
    public class TrackingController : ITrackingController
    {
        private TrackMapProLogic _trakMapPro;
        private SystemSetLogic _systemService;
        private ILog _log;

        public TrackingController()
        {
            _trakMapPro = new TrackMapProLogic();
            _systemService = new SystemSetLogic();           
        }

       
        public TBL_CoilMap GetTrackMap()
        {
            try
            {
                var coilMap = _trakMapPro.GetCoilMap();
                _log.D("撈取CoilMap", $"撈取CoilMap目前鋼捲位置整體狀態 {coilMap.ToJson()}");
                return coilMap;
            }
            catch (Exception e)
            {
                _log.E("撈取CoilMap", TypeConvertUtil.CleanInvalidChar(e.Message));
                return null;
            }              
        }

        public void SetLog(ILog log)
        {
            _log = log;
        }

        public void DeleteCoilNoFromDB(string coilID)
        {
            int removeNum = 0;

            try
            {
                removeNum = _trakMapPro.DeleteCoilNoFromScheduleDB(coilID);
            }
            catch (Exception e)
            {

                _log.E("移除鋼卷排程失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
            }
         
            if (removeNum > 0)
            {
                // TODO 刪除成功
                _log.I("資料庫執行", $"從排程刪除鋼捲{coilID}成功"); 
            }
            else
            {
                // TODO 刪除失敗
                _log.E("資料庫執行", $"從排程刪除鋼捲{coilID}失敗, 無此筆ID");
            }

            
        }

       
        public void UpdateCoilMapPOSCoilID(string coilID, L2SystemDef.SKPOS POS)
        {
            
            try
            {
                _trakMapPro.UpdateMapCoilID(coilID, POS);
                _log.I("更新CoilMap", $"更新CoilMap ID:{coilID}成功");
            }
            catch (Exception e)
            {
                _log.E("更新CoilMap", $"更新CoilMap ID:{coilID}失敗"+TypeConvertUtil.CleanInvalidChar(e.Message));
            }

           
        }

       
        // GPL
        public bool UpdateTrackMap(L1L2Rcv.Msg_105_Trk_Map msg)
        {
            _log.I("更新CoilMap", 
                $"更新鋼捲Map"
                + $"EntryCar={msg.Entry_Car}.\t "
                + $"EntrySK01={msg.Entry_SK01}.\t "
                + $"EntryTOP={msg.Entry_TOP}.\t "
                + $"POR={msg.POR}.\t "
                + $"DeliverySK01={msg.Delivery_SK01}.\t"
                + $"DeliverySK02={msg.Delivery_SK02}.\t"
                + $"DeliveryTOP={msg.Delivery_TOP}.\t"
                + $"DeliveryCar={msg.Delivery_Car}.\t");
            try
            {
                var updateOK = _trakMapPro.UpdateTrkMap(msg.ConvertTblCoilMap());

                _log.I("更新CoilMap", $"更新CoilMap => {updateOK}");
                return updateOK;

            }
            catch (Exception e)
            {
                _log.E("更新CoilMap失敗", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }

        }

        public bool InvaildHasEntryTopCoilID()
        {
            try
            {

                var hasCoil = _trakMapPro.VaildHasEntryTopCoilID();
                _log.I("檢查Entry Top是否有鋼捲", $"Entry Top是否有鋼捲{hasCoil}");
                return hasCoil;
            }
            catch (Exception e)
            {
                _log.E("檢查Entry Top是否有鋼捲", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }

        }

        public bool IsSystemAutoValueOn(string parameterGroup, string parameter)
        {

            try
            {
                var isOn = _systemService.VaildSystemAutoInputOn(parameterGroup, parameter);
                _log.D($"判定系統自動進料參數", $"目前系統是否為自動進料{isOn}");
                return isOn;
            }
            catch (Exception e)
            {
                _log.E($"判定系統自動進料參數", TypeConvertUtil.CleanInvalidChar(e.Message));
                return false;
            }

        }

        public bool Create25CoilMap(Msg_105_Trk_Map msg)
        {

            try
            {
                var entity = msg.ToL2L25_CoilMap();
                var insertOK = _trakMapPro.CreateL25CoilMap(entity);
                _log.D("將CoilMap新增至L25 CoilMap", $"新增=>{insertOK}");
                return insertOK;
                //return true;

                //insertOK = _trakMapPro.CreateL25CoilMapHis(entity);
                //_log.D("將CoilMap新增至L25 CoilMap歷史資料", $"新增=>{insertOK}");


            }
            catch (Exception e)
            {

                _log.E("新增L25 CoilMap 失敗", e.ToString().CleanInvalidChar());
                return false;
            }

        }


    }
}

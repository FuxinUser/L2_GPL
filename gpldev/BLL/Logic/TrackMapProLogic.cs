using Core.Define;
using DBService.Level25Repository.L2L25_CoilMap;
using DBService.Repository;
using System.Linq;
using static DBService.Repository.CoilMapEntity;

/**
 Author: ICSC Spyua
 Desc: Coil Pro BLL
 Date: 2019/12/26
*/

namespace BLL.Trck
{
    public class TrackMapProLogic
    {
        private CoilMapRepo _coilMapRepo;
        private ProductionScheduleRepo _coilScheduleRepo;
        private L2L25_CoilMapRepo _l25CoilMapRepo;

        public TrackMapProLogic()
        {
            _coilMapRepo = new CoilMapRepo(DBParaDef.DBConn);
            _coilScheduleRepo = new ProductionScheduleRepo(DBParaDef.DBConn);
            _l25CoilMapRepo = new L2L25_CoilMapRepo(DBParaDef.Level2_5DBConn);
        }

        /// <summary>
        /// 更新CoilTable
        /// </summary>
        /// <param name="coilMap"></param>
        /// <param name="PositionFlag"></param>
        /// <returns>更新比數</returns>
        public int UpdateCoilMapData(TBL_CoilMap coilMap, L2SystemDef.SKPOS skPOS)
        {
            int updateNum = 0;
            try
            {
                if (skPOS == L2SystemDef.SKPOS.EntryTOP)
                {
                    // TODO Save Log
                    updateNum = _coilMapRepo.UpdateEntryMap(coilMap);
                }
                     
                if (skPOS == L2SystemDef.SKPOS.DeliveryTop)
                {
                    // TODO Save Log
                    updateNum = _coilMapRepo.UpdateDeliveryMap(coilMap);
                }
              
            }catch
            {
                throw;
              
            }

            return updateNum;
        }

       

        public int UpdateMapCoilID(string coilID, L2SystemDef.SKPOS POS)
        {
            int updateNum = 0;

            try
            {

                switch (POS)
                {
                    case L2SystemDef.SKPOS.EntryTOP:
                         updateNum = _coilMapRepo.UpdateEntryTopCoilID(coilID);
                        break;
                    case L2SystemDef.SKPOS.Entry_SK01:
                        updateNum = _coilMapRepo.UpdateEntrySK01CoilID(coilID);
                        break;                  
                    case L2SystemDef.SKPOS.DeliveryTop:
                        updateNum = _coilMapRepo.UpdateEntryTopCoilID(coilID);
                        break;
                    case L2SystemDef.SKPOS.Delivery_SK01:
                        updateNum = _coilMapRepo.UpdateDelivery_SK01CoilID(coilID);
                        break;
                    case L2SystemDef.SKPOS.Delivery_SK02:
                        updateNum = _coilMapRepo.UpdateDelivery_SK02CoilID(coilID);
                        break;
                }

                return updateNum;

            }
            catch
            {
                throw;

            }
        }

       

        public int DeleteCoilNoFromScheduleDB(string coilID)
        {
           try
            {
                var removeNum = _coilScheduleRepo.Delete(new string[] { coilID});
                return removeNum;


            }
            catch
            {
                throw;
            }
        }

        public bool VaildHasEntryTopCoilID()
        {
            try
            {
                var coilMap = _coilMapRepo.GetAll().FirstOrDefault();
                return coilMap.Entry_TOP.Replace(" ",string.Empty).Equals(string.Empty) ? false : true;
            }
            catch
            {
                throw;

            }         
        }

     
        // GPL

        public TBL_CoilMap GetCoilMap()
        {
            try
            {
                var coilMap = _coilMapRepo.GetAll().FirstOrDefault();
                return coilMap;
            }
            catch 
            {

                throw;
            }
          
        }
        public bool UpdateTrkMap(TBL_CoilMap msg)
        {
            try
            {
                var updateNum = _coilMapRepo.Update(msg);
                return updateNum > 0;
            }
            catch
            {
                throw;
            }
        }

        public bool CreateL25CoilMap(L2L25_CoilMap dao)
        {
            try
            {
                return _l25CoilMapRepo.Insert(dao) > 0;
            }
            catch
            {
                throw;
            }
        }

    }
}

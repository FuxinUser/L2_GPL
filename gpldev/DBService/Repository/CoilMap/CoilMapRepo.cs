using System;
using DBService.Base;

/**
 * Author:ICSC 余士鵬
 * Date:2019/12/24
 * Desc:DB Table
 */

namespace DBService.Repository
{
    public class CoilMapRepo : BaseRepository<CoilMapEntity.TBL_CoilMap>
    {
       
        public CoilMapRepo(string connStr) : base(connStr)
        {
        }

        protected override string TableName => nameof(CoilMapEntity.TBL_CoilMap);

        protected override string[] PKName => throw new NotImplementedException();

      
        public int UpdateEntryMap(CoilMapEntity.TBL_CoilMap model)
        {
            model.UpdateTime = DateTime.Now;

            var dbObj = new
            {
                model.Entry_TOP,
                model.Entry_SK01,
                model.POR,
                model.UpdateTime,

            };

            return DBContext.Update(TableName, dbObj, "");
        }

        public int UpdateExitMap(CoilMapEntity.TBL_CoilMap model)
        {
            model.UpdateTime = DateTime.Now;

            var dbObj = new
            {
                model.Delivery_TOP,
                model.Delivery_SK01,
                model.Delivery_SK02,
                model.TR,
                model.UpdateTime,

            };

            return DBContext.Update(TableName, dbObj, "");
        }

        public int UpdateEntryTopCoilID(string coilID)
        {          
            var dbObj = new
            {
                Entry_TOP = coilID,              
                UpdateTime = DateTime.Now,

            };

            return DBContext.Update(TableName, dbObj, "");
        }
             
        public int UpdateEntrySK01CoilID(string coilID)
        {
            var dbObj = new
            {
                Entry_SK01 = coilID,
                UpdateTime = DateTime.Now,

            };

            return DBContext.Update(TableName, dbObj, "");
        }

        public int UpdateEntrySK02CoilID(string coilID)
        {
            var dbObj = new
            {
                Entry_SK02 = coilID,
                UpdateTime = DateTime.Now,

            };

            return DBContext.Update(TableName, dbObj, "");
        }


        public int UpdateDelivery_SK01CoilID(string coilID)
        {
            var dbObj = new
            {
                Delivery_SK01 = coilID,
                UpdateTime = DateTime.Now,

            };

            return DBContext.Update(TableName, dbObj, "");
        }

        public int UpdateDelivery_SK02CoilID(string coilID)
        {
            var dbObj = new
            {
                Delivery_SK02 = coilID,
                UpdateTime = DateTime.Now,

            };

            return DBContext.Update(TableName, dbObj, "");
        }

        public int UpdateDelivery_TOPCoilID(string coilID)
        {
            var dbObj = new
            {
                Delivery_TOP = coilID,
                UpdateTime = DateTime.Now,

            };

            return DBContext.Update(TableName, dbObj, "");
        }

        public int UpdateDeliveryMap(CoilMapEntity.TBL_CoilMap model)
        {
            model.UpdateTime = DateTime.Now;

            var dbObj = new
            {
                model.TR,
                model.Delivery_TOP,
                model.Delivery_SK01,
                model.Delivery_SK02,
                model.UpdateTime,

            };
            return DBContext.Update(TableName, dbObj, "");
        }

    }
}

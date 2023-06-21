using DBService.Base;
using System;

namespace DBService.Repository.PDI
{
    public class CoilPDIRepo : BaseRepository<PDIEntity.TBL_PDI>
    {
        public CoilPDIRepo(string connStr) : base(connStr)
        {
        }

        protected override string TableName => nameof(PDIEntity.TBL_PDI);

        protected override string[] PKName => new string[] { nameof(PDIEntity.TBL_PDI.In_Coil_ID), nameof(PDIEntity.TBL_PDI.Plan_No) };


        public string GetPlanNoByEntryCoilID(string entryCoilID)
        {
            var sql = $"select {nameof(PDIEntity.TBL_PDI.Plan_No)} From {TableName} where {nameof(PDIEntity.TBL_PDI.In_Coil_ID)} = '{entryCoilID}'";
            return DBContext.QuerySingleOrDefault<string>(sql);
        }
       
        public string GetSpeicDataByEntryCoilID(string columnName, string coilID)
        {
            var sql = $"select TOP(1) {columnName} From {TableName} where {nameof(PDIEntity.TBL_PDI.In_Coil_ID)} = '{coilID}' ORDER BY CreateTime DESC";
            return DBContext.QuerySingleOrDefault<string>(sql);
        }

        public int UpdateScrapedWeight(float weight, string entryCoilNo)
        {


            var dbObj = new
            {
                Scraped_Weight = weight,
            };

            return DBContext.Update(TableName, dbObj, $"{nameof(PDIEntity.TBL_PDI.In_Coil_ID)} = '{entryCoilNo}'");
        }

        public int UpdateHasScrapedFlag(string hasScraped, string entryCoilNo)
        {
            var dbObj = new
            {
                HasScraped = hasScraped,

            };

            return DBContext.Update(TableName, dbObj, $"{nameof(PDIEntity.TBL_PDI.In_Coil_ID)} = '{entryCoilNo}'");
        }

        public int UpdateEntryScanCoilInfo(string scanCoilID, string entryCoilIDChecked)
        {
            var dbObj = new
            {
                Entry_Scaned_CoilID = scanCoilID,
                Entry_CoilID_Checked = entryCoilIDChecked,
                Entry_Scaned_Time = DateTime.Now,

            };

            return DBContext.Update(TableName, dbObj, $"{nameof(PDIEntity.TBL_PDI.In_Coil_ID)} = '{scanCoilID}'");
        }

        public int UpdateStarTime(string coilIDNo)
        {
            var dbObj = new
            {
                StarTime = DateTime.Now,
            };
            return DBContext.Update(TableName, dbObj, $"{nameof(PDIEntity.TBL_PDI.In_Coil_ID)} = '{coilIDNo}'");
        }

        public int UpdateFinishTime(string coilIDNo)
        {
            var dbObj = new
            {
                FinishTime = DateTime.Now,
            };
            return DBContext.Update(TableName, dbObj, $"{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)} = '{coilIDNo}' ");
        }


        public int UpdateEndTime(string coilIDNo)
        {
            var dbObj = new
            {
                EndTime = DateTime.Now,
            };
            return DBContext.Update(TableName, dbObj, $"{nameof(PDIEntity.TBL_PDI.Out_Coil_ID)} = '{coilIDNo}'");
        }

        public int UpdateIsInfoWMSDown(string done, string coilIDNo)
        {
            var dbObj = new
            {
                Is_Info_WMS_Down = done,
            };
            return DBContext.Update(TableName, dbObj, $"{nameof(PDIEntity.TBL_PDI.In_Coil_ID)} = '{coilIDNo}'");
        }
    }
}

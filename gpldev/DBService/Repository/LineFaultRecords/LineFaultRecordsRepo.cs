using DBService.Base;
using System;
using System.Text;

namespace DBService.Repository.LineFaultRecords
{
    public class LineFaultRecordsRepo : BaseRepository<LineFaultRecordsEntity.TBL_LineFaultRecords>
    {
        protected override string TableName => nameof(LineFaultRecordsEntity.TBL_LineFaultRecords);

        protected override string[] PKName => new string[] { nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time), nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time) };

        public LineFaultRecordsRepo(string connStr) : base(connStr)
        {

        }


        public int UpdateField(string prodTime, string stopStartTime, object dbObj)
        {
            var pkCondition = new StringBuilder();
            pkCondition.Append($"{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.prod_time)} = '{prodTime}'");
            pkCondition.Append(" AND ");
            pkCondition.Append($"{nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time)} = '{stopStartTime}'");
            return DBContext.Update(TableName, dbObj, pkCondition.ToString());
        }
    }
}

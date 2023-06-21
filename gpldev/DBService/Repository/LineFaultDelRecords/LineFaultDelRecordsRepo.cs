using DBService.Base;
using System;
using System.Text;

namespace DBService.Repository.LineFaultRecords
{
    public class LineFaultDelRecordsRepo : BaseRepository<LineFaultDelRecordsEntity.TBL_LineFaultDelRecords>
    {
        protected override string TableName => nameof(LineFaultDelRecordsEntity.TBL_LineFaultDelRecords);

        protected override string[] PKName => new string[] { nameof(LineFaultDelRecordsEntity.TBL_LineFaultDelRecords.prod_time), nameof(LineFaultRecordsEntity.TBL_LineFaultRecords.stop_start_time) };

        public LineFaultDelRecordsRepo(string connStr) : base(connStr)
        {

        }


        public int UpdateField(string prodTime, string stopStartTime, object dbObj)
        {
            var pkCondition = new StringBuilder();
            pkCondition.Append($"{nameof(LineFaultDelRecordsEntity.TBL_LineFaultDelRecords.prod_time)} = '{prodTime}'");
            pkCondition.Append(" AND ");
            pkCondition.Append($"{nameof(LineFaultDelRecordsEntity.TBL_LineFaultDelRecords.stop_start_time)} = '{stopStartTime}'");
            return DBContext.Update(TableName, dbObj, pkCondition.ToString());
        }
    }
}

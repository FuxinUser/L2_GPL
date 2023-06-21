using DBService.Base;

namespace DBService.Repository
{
    public class ProductionScheduleRepo : BaseRepository<CoilScheduleEntity.TBL_Production_Schedule>
    {

        public ProductionScheduleRepo(string connStr) : base(connStr)
        {
        }
        protected override string TableName => nameof(CoilScheduleEntity.TBL_Production_Schedule);

        protected override string[] PKName => new string[] { nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID) };

        public int UpdateScheduleStatus(string coilNo, string status)
        {
            var dbObj = new
            {
                Schedule_Status = status,
            };
            return DBContext.Update(TableName, dbObj, $"{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)} = '{coilNo}'");
        }

        public int UpdateScheduleSeqNo(string coilNo, short seqNo)
        {
            var dbObj = new
            {
                Seq_No = seqNo
            };
            return DBContext.Update(TableName, dbObj, $"{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)} = '{coilNo}'");
        }
    }
}

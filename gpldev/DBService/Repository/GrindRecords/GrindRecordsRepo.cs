using DBService.Base;

namespace DBService.Repository.GrindRecords
{
    public class GrindRecordsRepo : BaseRepository<GrindRecordsEntity.TBL_GrindRecords>
    {
        protected override string TableName => nameof(GrindRecordsEntity.TBL_GrindRecords);

        protected override string[] PKName => new string[] {  nameof(GrindRecordsEntity.TBL_GrindRecords.Coil_ID)
                                                             ,nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Pass)
                                                             ,nameof(GrindRecordsEntity.TBL_GrindRecords.Current_Senssion)};

        public GrindRecordsRepo(string connStr) : base(connStr)
        {

        }
    }
}

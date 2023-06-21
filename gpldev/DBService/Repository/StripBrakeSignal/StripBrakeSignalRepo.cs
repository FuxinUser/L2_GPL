using DBService.Base;

namespace DBService.Repository.StripBrakeSignal
{
    public class StripBrakeSignalRepo : BaseRepository<StripBrakeSignalEntity.TBL_StripBrakeSignal>
    {
        protected override string TableName => nameof(StripBrakeSignalEntity.TBL_StripBrakeSignal);

        protected override string[] PKName => throw new System.NotImplementedException();   // No PK

        public StripBrakeSignalRepo(string connStr) : base(connStr)
        {

        }
    }
}

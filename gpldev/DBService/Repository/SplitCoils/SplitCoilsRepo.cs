using DBService.Base;

namespace DBService.Repository.SplitCoils
{
    public class SplitCoilsRepo : BaseRepository<SplitCoilsEntity.TBL_SplitCoils>
    {
        protected override string TableName => nameof(SplitCoilsEntity.TBL_SplitCoils);

        protected override string[] PKName => new string[] { nameof(SplitCoilsEntity.TBL_SplitCoils.Coil_ID) };

        public SplitCoilsRepo(string connStr) : base(connStr)
        {

        }
    }
}

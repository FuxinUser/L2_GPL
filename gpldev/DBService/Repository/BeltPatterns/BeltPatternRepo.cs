using DBService.Base;

namespace DBService.Repository.BeltPatterns
{
    public class BeltPatternRepo : BaseRepository<BeltPatternsEntity.TBL_BeltPatterns>
    {
        protected override string TableName => nameof(BeltPatternsEntity.TBL_BeltPatterns);

        protected override string[] PKName => new string[] { nameof(BeltPatternsEntity.TBL_BeltPatterns.BeltPattern),
                                                             nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_From),
                                                             nameof(BeltPatternsEntity.TBL_BeltPatterns.Pass_To),
                                                             nameof(BeltPatternsEntity.TBL_BeltPatterns.GR_NO)};

        public BeltPatternRepo(string connStr) : base(connStr)
        {

        }
    }
}

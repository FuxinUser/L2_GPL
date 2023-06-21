using DBService.Base;
using System;
using static DBService.Repository.DefectData.CoilDefectDataEntity;

namespace DBService.Repository.DefectData
{
    public class DefectDataRepo : BaseRepository<L3L2_TBL_DefectData>
    {
        public DefectDataRepo(string connStr) : base(connStr)
        {
        }

        protected override string TableName => nameof(L3L2_TBL_DefectData);

        // No PK
        protected override string[] PKName => new string[] { nameof(L3L2_TBL_DefectData.CoilID),
                                                              nameof(L3L2_TBL_DefectData.DefectCode),
                                                             nameof(L3L2_TBL_DefectData.DefectPositionWidthDirection),
                                                               nameof(L3L2_TBL_DefectData.DefectLevel),};
    }
}

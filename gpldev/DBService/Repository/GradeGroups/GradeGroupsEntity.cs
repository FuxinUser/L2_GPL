using DBService.Base;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.GradeGroups
{
    public class GradeGroupsEntity
    {
        public class TBL_GradeGroups : BaseRepositoryModel
        {
            [PrimaryKey]
            public string SteelGrade { get; set; }
            [PrimaryKey]
            public string CustomerNo { get; set; }
            public string GradeGroup { get; set; }
        }
    }
}

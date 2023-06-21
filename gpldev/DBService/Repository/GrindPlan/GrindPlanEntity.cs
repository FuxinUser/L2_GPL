using DBService.Base;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.GrindPlan
{
    public class GrindPlanEntity
    {
        public class TBL_GrindPlan : BaseRepositoryModel
        {
            [PrimaryKey]
            public string GradeGroup { get; set; }
            [PrimaryKey]
            public double Thickness_From { get; set; }
            [PrimaryKey]
            public double Thickness_To { get; set; }
            [PrimaryKey]
            public string Pass_Section { get; set; }
            public string BeltPattern { get; set; }
            public byte PassNumber { get; set; }
            public int LineSpeed { get; set; }
        }
    }
}

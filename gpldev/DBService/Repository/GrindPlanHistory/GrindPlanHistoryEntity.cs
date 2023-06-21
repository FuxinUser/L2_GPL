using DBService.Base;
using DBService.Repository.GrindPlan;
using System;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.GrindPlanHistory
{
    public class GrindPlanHistoryEntity 
    {
        public class TBL_GrindPlanHistory : BaseRepositoryModel
        {
            public string GradeGroup { get; set; }
            public double Thickness_From { get; set; }
            public double Thickness_To { get; set; }
            public string Pass_Section { get; set; }
            public string BeltPattern { get; set; }
            public byte PassNumber { get; set; }
            public int LineSpeed { get; set; }
            [PrimaryKey]
            public string Coil_ID { get; set; }
            [PrimaryKey]
            public string Plan_No { get; set; }
            [PrimaryKey]
            public override DateTime CreateTime { get; set; }


            [IgnoreReflction]
            public void LoadBeltPassData(string coilID, string planNo,GrindPlanEntity.TBL_GrindPlan beltPattern)
            {
                GradeGroup = beltPattern.GradeGroup;
                Thickness_From = beltPattern.Thickness_From;
                Thickness_To = beltPattern.Thickness_To;
                BeltPattern = beltPattern.BeltPattern;
                PassNumber = beltPattern.PassNumber;
                LineSpeed = beltPattern.LineSpeed;
                Pass_Section = beltPattern.Pass_Section;
                Coil_ID = coilID;
                Plan_No = planNo;
            }
        }
       
    }
}

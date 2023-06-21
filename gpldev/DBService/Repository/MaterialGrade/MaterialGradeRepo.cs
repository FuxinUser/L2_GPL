using DBService.Base;
using static DBService.Repository.MaterialGrade.MaterialGradeEntity;

namespace DBService.Repository.MaterialGrade
{
    public class MaterialGradeRepo : BaseRepository<TBL_SteelNoToMaterialGrade>
    {
        protected override string TableName => nameof(TBL_SteelNoToMaterialGrade);

        protected override string[] PKName => new string[] { nameof(TBL_SteelNoToMaterialGrade.St_No) };

        public MaterialGradeRepo(string connStr) : base(connStr)
        {

        }
    }
}

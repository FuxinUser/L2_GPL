using DBService.Base;
using static DBService.Base.DBAttributes;

namespace DBService.Repository.SplitCoils
{
    public class SplitCoilsEntity 
    {
        public class TBL_SplitCoils : BaseRepositoryModel
        {
            [PrimaryKey]
            public string Coil_ID { get; set; }
            public string ParentCoil_ID { get; set; }
        }
    }
}

using DBService.Base;

namespace DBService.Repository.Belt
{
    public class BeltAccRepo : BaseRepository<BeltAccEntity.TBL_Belts>
    {
        protected override string TableName => nameof(BeltAccEntity.TBL_Belts);

        protected override string[] PKName => new string[] { nameof(BeltAccEntity.TBL_Belts.Belt_No) };

        public BeltAccRepo(string connStr) : base(connStr)
        {

        }

        public int UpdateBeltAccLengthByGrNo(int grNo, float beltLength, float stLength)
        {
            var dbObj = new
            {
                Total_Grind_Length_Belt = beltLength,
                Total_Grind_Length_Strip = stLength

            };
            return DBContext.Update(TableName, dbObj, $"{nameof(BeltAccEntity.TBL_Belts.Mount_GR_No)} = '{grNo}'");
        }

        public int UpdateAccLengthByBeltNo(string beltNo, float beltLength, float stLength)
        {
            var dbObj = new
            {
                Total_Grind_Length_Belt = beltLength,
                Total_Grind_Length_Strip = stLength

            };
            return DBContext.Update(TableName, dbObj, $"{nameof(BeltAccEntity.TBL_Belts.Belt_No)} = '{beltNo}'");
        }

        public int UpdateGrNoByBeltNo(string beltNo, int grNo)
        {
            var dbObj = new
            {
                Mount_GR_No = grNo,                
            };
            return DBContext.Update(TableName, dbObj, $"{nameof(BeltAccEntity.TBL_Belts.Belt_No)} = '{beltNo}'");
        }
    }
}

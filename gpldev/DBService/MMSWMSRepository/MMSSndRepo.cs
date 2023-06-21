using DBService.Base;
using DBService.MMSRepository;

namespace DBService.MMSWMSRepository
{
    public class MMSSndRepo : BaseRepository<MMS_WMS_MsgRecord>
    {
        protected override string TableName => "TBL_MMS_SendRecord";

        protected override string[] PKName => new string[] { nameof(MMS_WMS_MsgRecord.CreateTime) };


        public MMSSndRepo(string connStr) : base(connStr)
        {

        }

       
    }
}

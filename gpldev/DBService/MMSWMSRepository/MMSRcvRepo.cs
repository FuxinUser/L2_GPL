using DBService.Base;
using DBService.MMSRepository;

namespace DBService.MMSWMSRepository
{
    public class MMSRcvRepo : BaseRepository<MMS_WMS_MsgRecord>
    {
        protected override string TableName => "TBL_MMS_ReceiveRecord";

        protected override string[] PKName => new string[] { nameof(MMS_WMS_MsgRecord.CreateTime) };


        public MMSRcvRepo(string connStr) : base(connStr)
        {

        }
    }
}

using DBService.Base;
using DBService.MMSRepository;

namespace DBService.MMSWMSRepository
{
    public class WMSRcvRepo : BaseRepository<MMS_WMS_MsgRecord>
    {
        protected override string TableName => "TBL_WMS_ReceiveRecord";

        protected override string[] PKName => new string[] { nameof(MMS_WMS_MsgRecord.CreateTime) };


        public WMSRcvRepo(string connStr) : base(connStr)
        {

        }
    }
}

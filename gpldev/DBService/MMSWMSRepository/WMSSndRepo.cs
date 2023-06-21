using DBService.Base;
using DBService.MMSRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.MMSWMSRepository
{
    public class WMSSndRepo : BaseRepository<MMS_WMS_MsgRecord>
    {
        protected override string TableName => "TBL_WMS_SendRecord";

        protected override string[] PKName => new string[] { nameof(MMS_WMS_MsgRecord.CreateTime) };


        public WMSSndRepo(string connStr) : base(connStr)
        {

        }
    }
}

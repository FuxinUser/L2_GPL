using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repository.LineStatus
{
    public class ProcessDataRepo : BaseRepository<ProcessDataEntity.TBL_ProcessData>
    {
        protected override string TableName => nameof(ProcessDataEntity.TBL_ProcessData);

        protected override string[] PKName => new string[] { nameof(ProcessDataEntity.TBL_ProcessData.Receive_Time) };

        public ProcessDataRepo(string connStr) : base(connStr)
        {

        }
    }
}

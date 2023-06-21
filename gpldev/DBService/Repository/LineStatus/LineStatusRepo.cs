using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repository.LineStatus
{
    public class LineStatusRepo : BaseRepository<LineStatusEntity.TBL_LineStatus>
    {
        protected override string TableName => nameof(LineStatusEntity.TBL_LineStatus);

        protected override string[] PKName => throw new NotImplementedException();

        public LineStatusRepo(string connStr) : base(connStr)
        {

        }
    }
}

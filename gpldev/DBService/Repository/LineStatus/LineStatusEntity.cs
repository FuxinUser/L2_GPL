using DBService.Base;
using System;

namespace DBService.Repository.LineStatus
{
    public class LineStatusEntity
    {
        public class TBL_LineStatus : BaseRepositoryModel
        {
            public int LineStatus_Entry { get; set; }
            public int LineStatus_CPL { get; set; }
            public int LineStatus_Exit { get; set; }
            public override DateTime UpdateTime { get; set; }
        }

    }
}

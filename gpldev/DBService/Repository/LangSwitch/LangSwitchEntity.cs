using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repository.LangSwitch
{
    public class LangSwitchEntity
    {
        public class TBL_LangSwitch_Ctr : BaseRepositoryModel
        {
            public string FormName { get; set; }

            public string CtrName { get; set; }

            public string ZH { get; set; }

            public string EN { get; set; }

            public string ColumnName { get; set; }

            public override DateTime UpdateTime { get; set; }
        }

        public class TBL_LangSwitch_Nav : BaseRepositoryModel
        {
            public string PKey { get; set; }
            public string ZH { get; set; }
            public string EN { get; set; }
            public string UpdateUser { get; set; }
            public string UpdateDateTime { get; set; }
        }

        public class TBL_LanguageSwitch
        {
            public string PKey { get; set; }
            public string ZH { get; set; }
            public string EN { get; set; }
            public string UpdateUser { get; set; }
            public string UpdateDateTime { get; set; }
        }
    }
}

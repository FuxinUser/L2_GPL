using DBService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repository.SystemSetting
{
    public class SystemSettingRepo : BaseRepository<SystemSettingEntity.TBL_SystemSetting>
    {

        public SystemSettingRepo(string connStr) : base(connStr)
        {
        }

        protected override string TableName => nameof(SystemSettingEntity.TBL_SystemSetting);

        protected override string[] PKName => new string[] { nameof(SystemSettingEntity.TBL_SystemSetting.Parameter_Group), nameof(SystemSettingEntity.TBL_SystemSetting.Parameter) };
    }
}

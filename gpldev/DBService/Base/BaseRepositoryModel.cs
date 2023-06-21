using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DBService.Base.DBAttributes;

/**
 * Author:ICSC SPYUA
 * Data:2019/12/28
 * Desc:基底Model實作
 */

namespace DBService.Base
{
    [Serializable]
    public abstract class BaseRepositoryModel : IRepositoryModel
    {
        [IgnoreReflction]
        public virtual string Id { get; set; }                //選擇性實作
        [IgnoreReflction]
        public virtual DateTime UpdateTime { get; set; }    //選擇性實作
        [IgnoreReflction]
        public virtual DateTime CreateTime { get; set; }    //選擇性實作
    }
}

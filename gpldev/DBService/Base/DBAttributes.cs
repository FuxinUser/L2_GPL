using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 *Author: ICSC SPYUA
 *Date:2019/12/28
 *Desc:
 */

namespace DBService.Base
{
    public class DBAttributes
    {
        public class PrimaryKey : Attribute
        {
            public PrimaryKey() { }
        }
        public class IdentityKey : Attribute
        {
            public IdentityKey() { }
        }

        [AttributeUsage(AttributeTargets.All, Inherited = false)]
        public class IgnoreReflction : Attribute
        {
            public IgnoreReflction() { }
        }     
    }
}

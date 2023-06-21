using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Common
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Repository.AuthorityData_Frame
{
    public class AuthorityDataFrameEntity
    {
        public class TBL_AuthorityData_Frame
        {
            public string User_ID { get; set; }
            public string Frame_ID { get; set; }
            public string Frame_Function { get; set; }
            public DateTime Create_DateTime { get; set; }
        }
    }
}

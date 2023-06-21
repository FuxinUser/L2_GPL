using System;

namespace DBService.Repository.AuthorityData
{
    public class AuthorityDataEntity
    {
        public class TBL_AuthorityData
        { 
            public string User_ID { get; set; }
            public string Password { get; set; }
            public string Department { get; set; }
            public string Team { get; set; }
            public string Authority_Class { get; set; }
            public DateTime Create_DateTime { get; set; }
        }
    }
}

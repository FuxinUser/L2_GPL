using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//資料庫中繼模組(For 報文轉換使用)
namespace DataModel.Common
{
    public class TblRelyModel
    {

        public class TrackData
        {
            public string sk1;

            public string sk2;

            public string sk3;

            public char[] TopSensor;

            public char[] EntryExit;
        }
        public class CoilLoadData
        {
           
            public string Plan_No;
           
            public string Entry_Coil_No;
           
            public char[] Loaded_Time;
         
            public string Unit_Code;
        }
      
    }
}

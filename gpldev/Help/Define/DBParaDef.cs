using Core.Help;
using System;

namespace Core.Define
{
    public static class DBParaDef
    {
        //public static readonly string DBConn = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["DB_Name"]].ConnectionString;
        //public static readonly string DBConn = "Data Source=10.201.19.90\\MSSQLSERVER1; Initial Catalog=FUXIN_GPL; Persist Security Info=True;User ID=icsc;Password=00000000";
        //public static readonly string GplHisDBConn = "Data Source=10.201.19.90\\MSSQLSERVER1; Initial Catalog=FUXIN_GPL_HISTORY; Persist Security Info=True;User ID=icsc;Password=00000000";

        public static string DBConn = IniSystemHelper.Instance.DBConn;
        public static string HisDBConn = IniSystemHelper.Instance.HisDBConn;
        public static string Level2_5DBConn = IniSystemHelper.Instance.Level2_5_DBConn;

        //public static readonly string DBConn = "Data Source=LAPTOP-EJCI5MRK; Initial Catalog=FUXIN_GPL; Persist Security Info=True;User ID=sa;Password=laser99";
        //public static readonly string HisDBConn = "Data Source=LAPTOP-EJCI5MRK; Initial Catalog=FUXIN_GPL_HISTORY; Persist Security Info=True;User ID=sa;Password=laser99";

        //public static readonly string DBConn = "Data Source=I29042\\SQLEXPRESS; Initial Catalog=FUXIN_GPL; Persist Security Info=True;User ID=sa;Password=laser99";
        //public static readonly string HisDBConn = "Data Source=I29042\\SQLEXPRESS; Initial Catalog=FUXIN_GPL_HISTORY; Persist Security Info=True;User ID=sa;Password=laser99";

        //public static readonly string DBConn = "Data Source=10.201.19.213; Initial Catalog=FUXIN_GPL; Persist Security Info=True;User ID=sa;Password=a@123";
        //public static readonly string GplHisDBConn = "Data Source=10.201.19.213; Initial Catalog=FUXIN_GPL_HISTORY; Persist Security Info=True;User ID=sa;Password=a@123";

        public static readonly DateTime DefaultTime = Convert.ToDateTime("1970/01/01 00:00:00");

        public static readonly short GrindRecordSession_H = 1;
        public static readonly short GrindRecordSession_M = 2;
        public static readonly short GrindRecordSession_T = 3;

        public static readonly string TimeFromat = "yyyy-MM-dd HH:mm:ss.fff";
        //public static readonly string DBDateTimeFromat = "yyyy-MM-dd HH:mm:ss.fff";
        public static readonly string DB25DateFromat = "yyyyMMdd";
        public static readonly string DB25TimeFromat = "HHmmss";


        // Parameter Def
        public static string SysParaGroup = "GPL";
        public const string SysParaAutoInputFlag = "AutoCoilFeed";
        public const string SysParaAutoPrintFlag = "AutoPrint";
        public const string SysParaL1AliveLastTime = "L1_ALIVE_LastTime";
        public const string SysTopScheduleLock = "TopScheduleLock";
        public const string SysParaCoilWeight = "CoilWeight";


        public static readonly string USE = "1";
        public static readonly string NOTUSE = "0";

        public const string YES = "Y";
        public const string NO = "N";

        public class ConnectionSysDef
        {
            public const string Connect = "1";
            public const string UnConnect = "0";


            public enum ConnectionType
            {
                L2ConnectToPLC, L2ConnectedByMMS, L2ConnectToMMS, L2ConnectedByWMS, L2ConnectToWMS, L2ConnectedByPLC, L2ConnectedByPLCCycle
            }

            public const string L2 = "LEVEL2";
            public const string MMS = "MMS";
            public const string WMS = "WMS";
            public const string L1 = "LEVEL1";
            public const string L1_CYC = "LEVEL1_CYCLE";
        }
    }
}

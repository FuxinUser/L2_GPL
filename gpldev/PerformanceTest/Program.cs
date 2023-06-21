using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BLL.Coil;
using Core.Util;
using MsgStruct;
using MsgConvert.Msg;
using System.Data;

namespace PerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //MD5CodeGenTest();
            //string s = "Hello Extension Methods";
            //int i = s.WordCount();
            //var _msgLen = new MsgLengthDitionary();
            //Console.WriteLine(_msgLen);

            //var t = new BeltPatternPerformance();
            //var roop = 10000;

            //var watch = Stopwatch.StartNew();
            //for (int i = 0; i < roop; i++)
            //    t.GetBeltPatterns();
            //watch.Stop();
            //Console.WriteLine("BeltPatternPerformance" + "\t" + watch.ElapsedMilliseconds / roop);
            //var summary = BenchmarkRunner.Run<BeltPatternPerformance>();
            //int ten = 10;
            //int i2 = 2147483647 + ten;
            //checked
            //{
            //    int i3 = 2147483647 + ten;
            //    Console.WriteLine(i3);
            //}

            //var MorningWorkStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
            //var MorningWorkEndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0);

            //var shiftTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 50, 0);

            //Console.WriteLine(MorningWorkStartTime);
            //Console.WriteLine(MorningWorkEndTime);

            ////if(MorningWorkStartTime.CompareTo(shiftTime)<0 && MorningWorkEndTime.CompareTo(MorningWorkEndTime)>0)
            //if (MorningWorkStartTime <= shiftTime  && MorningWorkEndTime >= shiftTime)
            //{
            //    Console.WriteLine("有在區間裡面");
            //}

            //DataTable dataTable = new DataTable();


            ////dataTable.Columns.Add(DBColumnDef.CoilSchedCoilID);         // Coil_ID
            ////dataTable.Columns.Add(DBColumnDef.CoilSchedSeqNo);          //Seq_No
            ////dataTable.Columns.Add(DBColumnDef.CoilSchedUpdateSource);   //Update_Source
            ////dataTable.Columns.Add(DBColumnDef.CoilSchedUpdateTime);      //UpdateTime

            ////var row = dataTable.NewRow();
            ////row[DBColumnDef.CoilSchedCoilID] = "";
            ////row[DBColumnDef.CoilSchedSeqNo] = "";
            ////row[DBColumnDef.CoilSchedUpdateSource] = CoilLogicDef.UpdateSourceMMS;
            ////row[DBColumnDef.CoilSchedUpdateTime] = DateTime.Now;
            ////dataTable.Rows.Add(row);

            ////dataTable = null;

            //Console.WriteLine($"DataTable是否為Null ==> {dataTable.IsNull()}");

            //var shiftInfo = ShiftHelp.GetShift(DateTime.Now);

            TT d = TT.A;
            Console.WriteLine(d);

            //var zebra = new Zebra("10.201.19.25",6101);

            //zebra.SendZPL(zplCmd2("CE0010001"));

            Console.ReadKey();
        }

        public enum TT { A,B,C}

       
        public static readonly DateTime DefaultDDDDTime = Convert.ToDateTime("1970/01/01 00:00:00");

        public static void MD5CodeGenTest()
        {
            var list = new List<string>();
     
            for (int i = 0; i < 10000; i++)
            {
                var md5 = MD5.Create();
                var randomStr = md5.GetMd5Hash(Guid.NewGuid().ToString());
                var str = randomStr.Truncate(8);
                list.Add(randomStr);
            }
            // How many times the elements are repeated
            var query = list.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .ToList();
            Console.WriteLine(query.Count);

        }

        public static string zplCmd(string coilID)
        {
  
            return $@"
                    ^XA
        ^BY4,2,160
          ^FO45,25^BC^FD{coilID}^FS
 ^XZ";
        }

        public static string zplCmd2(string coilID)
        {

            return $@"
                    ^XA

^FO20,0
^BY4,2.0,35
^BQN,2,9
^FDQA,HC210429710010H^FS

^FO220,35
^CFA,45
^FDSUH409L^FS

^FO220,85
^CFA,45
^FDCAPL^FS

^FO420,85
^CFA,45
^FD0.12mm^FS

^FO220,135
^CFA,45
^FD2019/08/02 20:01^FS

^FO220,185
^CFA,45
^FDHC20190801001^FS

^FO15,245
^CFA,45
^FDCoil No:HC210429710010H^FS

^XZ";
        }
    }

   
    public class BeltPatternPerformance
    {
        private CoilProLogic coilPro = new CoilProLogic();
        private L2L1Snd.Msg_204_PDI_TM3 msg204 = new L2L1Snd.Msg_204_PDI_TM3();
        private L2L1Snd.Msg_205_PDI_TM3_2 msg205 = new L2L1Snd.Msg_205_PDI_TM3_2();
        [Benchmark]
        public void GetBeltPatterns()
        {
           var beltPattenIndex = coilPro.QueryBeltPatterns("CE20010001");
            GrindProFactory.LoadBeltPattern(beltPattenIndex, ref msg204, ref msg205);
            //msg204.Load204BeltPattern(beltPattenIndex);
            //msg205.Load205BeltPattern(beltPattenIndex);
        }
    }

}


//namespace ExtensionMethods
//{
//    public static class MyExtensions
//    {
//        public static int WordCount(this String str)
//        {
//            return str.Split(new char[] { ' ', '.', '?' },
//                             StringSplitOptions.RemoveEmptyEntries).Length;
//        }
//    }
//}
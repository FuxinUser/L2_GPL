using System;

namespace Core.Help
{
    public class ShiftHelp
    {
       
        public static int NowShift()
        {

            var MorningWorkTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0).TimeOfDay;
            var MiddleWorkTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0).TimeOfDay;
            var NightWorkTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).TimeOfDay;
            var nowTime = DateTime.Now.TimeOfDay;

            // 1 夜班(24:00-8:00)
            if (NightWorkTime <= nowTime && MorningWorkTime > nowTime)
                return 1;

            // 2 早班(8:00-16:00)
            if (MorningWorkTime<= nowTime && MiddleWorkTime > nowTime)
                return 2;

            // 3 中班(16:00-24:00) 
            if (MiddleWorkTime <= nowTime && NightWorkTime > nowTime)
                return 3;

            return 0;
        }

        public static int GetShift(DateTime time)
        {

            var MorningWorkTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0).TimeOfDay;
            var MiddleWorkTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0).TimeOfDay;
            var NightWorkTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).TimeOfDay;

            var dateTime = time.TimeOfDay;


            // 1 夜班(00:00-8:00)
            if (NightWorkTime <= dateTime && MorningWorkTime > dateTime)
                return 1;

            // 2 早班(8:00-16:00)
            if (MorningWorkTime <= dateTime && MiddleWorkTime > dateTime)
                return 2;

            // 3 中班(16:00-00:00) 
            if (MiddleWorkTime <= dateTime && NightWorkTime > dateTime)
                return 3;

            return 0;
        }
        public static int GetShiftNo(DateTime time)
        {

            var MorningWorkTime = new DateTime(time.Year, time.Month, time.Day, 8, 0, 0);
            var MiddleWorkTime = new DateTime(time.Year, time.Month, time.Day, 16, 0, 0);
            var NightWorkTime = new DateTime(time.Year, time.Month, time.Day, 0, 0, 0);
            int shiftNo = 0;
            var dateTime = time;
            //NightWorkTime = NightWorkTime.AddDays(1);

            // 1 夜班(00:00-8:00)
            if (NightWorkTime <= dateTime && MorningWorkTime > dateTime)
                shiftNo = 1;


            // 2 早班(8:00-16:00)
            if (MorningWorkTime <= dateTime && MiddleWorkTime > dateTime)
                shiftNo = 2;

            // 3 中班(16:00-00:00) 
            if (MiddleWorkTime <= dateTime && NightWorkTime.AddDays(1).AddMilliseconds(-1) > dateTime)
                shiftNo = 3;

            //return 0;
            return shiftNo;

        }
    }
}

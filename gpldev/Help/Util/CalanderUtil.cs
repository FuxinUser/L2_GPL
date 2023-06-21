using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/**
 * Author: ICSC余士鵬
 * Date: 2019/10/09
 * Description: Calandar Help 
 * Reference: 
 * Modified: 
 */
namespace Core.Util
{
    public  class CalanderUtil
    {
       
        public static string ConvertDateFormatStr(string dateStr, string inFormat, string outFormat)
        {
            return DateTime.ParseExact(dateStr, inFormat, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces).ToString(outFormat);
        }
        public static string ConvertTimeFormatStr(string timeStr, string inFormat, string outFormat)
        {
            return DateTime.ParseExact(timeStr, inFormat, CultureInfo.InvariantCulture).ToString(outFormat);
        }

    }
}

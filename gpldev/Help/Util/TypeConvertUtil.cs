using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Util
{
    public static class TypeConvertUtil
    {

        /// <summary>
        /// 根據固定長度做分切
        /// </summary>
        /// <param name="str">需分切字串</param>
        /// <param name="chunkSize">分切每筆長度為多少</param>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<string> StrSplitBySpecificLength(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        /// <summary>
        /// 截短字串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        /// <summary>
        /// 字串中刪除無效的字元
        /// </summary>
        public static string CleanInvalidChar(this string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
        public static T? ToNullable<T>(this string s) where T : struct
        {
            T? result = new T?();
            try
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(s);
                }
            }
            catch { }
            return result != null ? result : default(T);
        }
        public static T? ToNullable<T>(this char[] s) where T : struct
        {
            var str = new string(s);

            T? result = new T?();
            try
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(str);
                }
            }
            catch { }
            return result != null ? result : default(T);
        }
        public static char[] ToNChar(this string data, int totalWidth)
        {
            try
            {
                return data.PadLeft(totalWidth, '0').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "".PadLeft(totalWidth, '0').ToArray(); ;
            }
        }
        public static char[] ToNChar(this float data, int totalWidth)
        {
            try
            {
                return data.ToString().PadLeft(totalWidth, '0').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "".PadLeft(totalWidth, '0').ToArray(); ;
            }
        }
        public static char[] ToNChar(this int data, int totalWidth)
        {
            try
            {
                return data.ToString().PadLeft(totalWidth, '0').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "".PadLeft(totalWidth, '0').ToArray(); ;
            }
        }
        public static char[] ToCChar(this string data, int totalWidth)
        {
            try
            {
                return data.PadRight(totalWidth, ' ').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "".PadRight(totalWidth, ' ').ToArray(); ;
            }
        }
        public static char[] ToCChar(this int data, int totalWidth)
        {
            try
            {
                return data.ToString().PadRight(totalWidth, ' ').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "".PadRight(totalWidth, ' ').ToArray(); ;
            }
        }
        public static char[] ToCChar(this float data, int totalWidth)
        {
            try
            {
                return data.ToString().PadRight(totalWidth, ' ').ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "".PadRight(totalWidth, ' ').ToArray(); ;
            }
        }

        public static string ToStr(this bool isOk)
        {
            return isOk ? "成功" : "失敗";
        }

        public static string ToHasStr(this bool doesHave)
        {
            return doesHave ? "有" : "無";
        }

        public static string ToStr(this char[] data)
        {
            return new string(data).Trim();
        }
        public static string ClearSpaceStr(this string data)
        {
            return data.Replace(" ", string.Empty);
        }

        public static bool IsEmpty(this string msg)
        {
            return string.IsNullOrEmpty(msg);
        }

        public static short ToShort(this int value)
        {
            if (value >= short.MinValue && value <= short.MaxValue)
            {
                return Convert.ToInt16(value);  
            }
            else
            {
                return 0;
            }
        }
        public static short ToShort(this byte value)
        {
            return Convert.ToInt16(value);
        }
        public static short ToShort(this double value)
        {
            if (value >= short.MinValue && value <= short.MaxValue)
            {
                return Convert.ToInt16(value);
            }
            else
            {
                return 0;
            }
        }
        public static short ToShort(this float value)
        {
            if (value >= Int16.MinValue && value <= Int16.MaxValue)
            {
                return Convert.ToInt16(value);
            }
            else
            {
                return 0;
            }
        }
        public static float ToFloat(this short value)
        {
            return Convert.ToSingle(value);
        }

        public static string ToStr(this byte[] data)
        {
            // EnCode UTF8

            // return Encoding.UTF8.GetString(data).Trim();
            return Encoding.UTF8.GetString(data).Trim('\0').Trim();
        }

        public static string ToASCCIIStr(this byte[] data)
        {
            // EnCode ASCII
            return Encoding.ASCII.GetString(data).Trim();
        }

        public static byte[] ToByteArray(this string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        public static byte[] ToCByteArray(this string data, int size)
        {
            return Encoding.UTF8.GetBytes(data.PadRight(size, ' '));
        }

        public static byte[] ToNByteArray(this string data, int size)
        {
            return Encoding.UTF8.GetBytes(data.PadLeft(size, '0'));
        }

        public static byte[] ToNByteArray(this int data, int size)
        {
            return Encoding.UTF8.GetBytes(data.ToString().PadLeft(size, '0'));
        }

        public static byte[] ToNByteArray(this float data, int size, int multiple = 1)
        {
            //var multipleValue = Convert.ToInt32(data * multiple);
            var multipleValue = Convert.ToInt64(data * multiple);
            return Encoding.UTF8.GetBytes(multipleValue.ToString().PadLeft(size, '0'));
        }

    }
}

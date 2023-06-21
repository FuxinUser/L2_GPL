using Akka.IO;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

/**
 * Author: ICSC余士鵬 (Modfiy From 陳志銘學長)
 * Date: 2019/9/24
 * Description: Message Analysis解析 
 * Reference: 
 * Modified: 
 */
namespace Core.Util
{
    public static class MsgAnalUtil
    {
     
        public static ByteString ToByteString(this byte[] data)
        {
            return ByteString.FromBytes(data);
        }

        public static string GetFixedString(string str, int fixedLen, string padWord = " ")
        {
            if (fixedLen == str.Length)
            {
                return str;
            }
            else if (fixedLen < str.Length)
            {
                return str.Substring(0, fixedLen);
            }
            else
            {
                if (string.IsNullOrEmpty(padWord)) padWord = " ";

                while (str.Length < fixedLen) str += padWord;

                return str.Substring(0, fixedLen);
            }
        }

        public static bool ReadBit(byte Data, int BitIndex)
        {
            return new BitArray(new byte[] { Data })[BitIndex];
        }

        /// <summary>
        /// 取得電文的Msg ID
        /// </summary>
        /// <param name="data">電文資料</param>
        /// <returns></returns>
        public static string GetMsgID(byte[] data, bool isReverse = false)
        {
            string strResult = "";
            byte[] tmp = { 0x00, 0x00 };

            if (data.Length >= 4)
            {
                Buffer.BlockCopy(data, 2, tmp, 0, 2);

                if(isReverse)
                    Array.Reverse(tmp);
                
                strResult = BitConverter.ToInt16(tmp, 0).ToString("000");
            }

            return strResult;
        }

        ///// <summary>
        ///// 將byte array解碼為對應型別資料
        ///// </summary>
        ///// <param name="byteData">要反序列化的byte array</param>
        ///// <param name="type">要轉換的型別</param>
        ///// <returns></returns>
        //public static Object RawDeserialize(this byte[] byteData, Type type)
        //{
        //    try
        //    {
        //        int rawsize = Marshal.SizeOf(type);
        //        if (rawsize > byteData.Length)
        //        {
        //            return null;
        //        }

        //        int iCursor = 0;

        //        ConvertEndian(byteData, type, iCursor);

        //        IntPtr buffer = Marshal.AllocHGlobal(rawsize);
        //        Marshal.Copy(byteData, 0, buffer, rawsize);
        //        var structureData = Marshal.PtrToStructure(buffer, type);

        //        //釋放
        //        Marshal.FreeHGlobal(buffer);
        //        return structureData;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static object RawDeserialize(this byte[] byteData, Type type, bool is_ConvertEndian = false)
        {
            try
            {
                int rawsize = Marshal.SizeOf(type);

                if (rawsize > byteData.Length)
                    return null;

                // ' ==============反轉===================
                if (is_ConvertEndian == true)
                    ConvertEndian(byteData, type);
                // ' ====================================

                IntPtr buffer = Marshal.AllocHGlobal(rawsize);
                Marshal.Copy(byteData, 0, buffer, rawsize);
                object structureData = Marshal.PtrToStructure(buffer, type);

                // 釋放
                Marshal.FreeHGlobal(buffer);

                return structureData;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static byte[] RawSerialize(this Object DataToSerialize, bool is_ConvertEndian = false)
        {
            try
            {
                int rawsize = Marshal.SizeOf(DataToSerialize);
                IntPtr buffer = Marshal.AllocHGlobal(rawsize);
                Marshal.StructureToPtr(DataToSerialize, buffer, false);
                byte[] byteData = new byte[rawsize];
                Marshal.Copy(buffer, byteData, 0, rawsize);
                //釋放
                Marshal.FreeHGlobal(buffer);

                int iCursor = 0;
                if(is_ConvertEndian)
                    ConvertEndian(byteData, DataToSerialize.GetType(), iCursor);

                return byteData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        private static byte[] ConvertEndian(byte[] byteData, Type type, int iCursor = 0)
        {
            int iMLength = 0;
            bool bIsArray = false;
            int iSizeConst = 0;
            Type FieldType = null;

            try
            {
                foreach (FieldInfo fi in type.GetFields())
                {
                    FieldType = fi.FieldType;

                    //取得Attribute
                    var marshal = fi.GetCustomAttribute<MarshalAsAttribute>();
                    bIsArray = (marshal.Value == UnmanagedType.ByValArray);
                    iSizeConst = marshal.SizeConst;

                    if (bIsArray)
                    {
                        //FieldType
                        Type elementType = FieldType.GetElementType();
                        for (int i = 0; i < iSizeConst; i++)
                        {
                            switch (elementType)
                            {
                                case Type intType when intType == typeof(int):
                                case Type shortType when shortType == typeof(short):
                                case Type longType when longType == typeof(long):
                                case Type floatType when floatType == typeof(float):
                                case Type doubleType when doubleType == typeof(double):
                                    {
                                        // 需要進行轉換
                                        iMLength = Marshal.SizeOf(elementType);
                                        Array.Reverse(byteData, iCursor, iMLength);
                                        iCursor += iMLength;
                                        break;
                                    }
                                case Type charType when charType == typeof(byte):
                                    {
                                        iCursor += 1;
                                        break;
                                    }
                                case Type charType when charType == typeof(char):
                                    {
                                        iCursor += 1;
                                        break;
                                    }
                                case Type stringType when stringType == typeof(string):
                                    {
                                        break;
                                    }

                                default:
                                    {
                                        ConvertEndian(byteData, elementType, iCursor);
                                        break;
                                    }
                            }
                        }
                    }
                    else
                    {//非陣列
                        switch (FieldType)
                        {
                            case Type intType when intType == typeof(int):
                            case Type shortType when shortType == typeof(short):
                            case Type longType when longType == typeof(long):
                            case Type floatType when floatType == typeof(float):
                            case Type doubleType when doubleType == typeof(double):
                                {
                                    // 需要進行轉換
                                    iMLength = Marshal.SizeOf(FieldType);
                                    Array.Reverse(byteData, iCursor, iMLength);
                                    iCursor += iMLength;
                                    break;
                                }
                            case Type byteType when byteType == typeof(byte[]):
                            case Type charType when charType == typeof(char):
                            case Type stringType when stringType == typeof(string):
                                {
                                    iMLength = iSizeConst;
                                    iCursor += iMLength;
                                    break;
                                }

                            default:
                                {
                                    ConvertEndian(byteData, FieldType, iCursor);
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return byteData;
        }


        ///// <summary>
        ///// 備份電文原始資料到檔案
        ///// </summary>
        ///// <param name="data"></param>
        //public static void DumpDataToFile(this byte[] data, String filePath, string msgID, bool isReverse = false)
        //{
        //    try
        //    {

        //        var fileName = $"{filePath}\\{DateTime.Now.ToString("yyyyMMdd_HHmmss_fff")}_MsgID_{msgID}.blob";
        //        FileStream fs = new FileStream(fileName, FileMode.Create);
        //        BinaryWriter bw = new BinaryWriter(fs);
        //        bw.Write(data);
        //        bw.Flush();
        //        bw.Close();
        //        fs.Close();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// 備份電文原始資料到檔案
        ///// </summary>
        ///// <param name="data"></param>
        //public static void DumpDataToFile(this byte[] data, String filePath)
        //{
        //    try
        //    {
        //        FileStream fs = File.Open($"{filePath}{DateTime.Now.ToString("yyyyMMdd_HHmmss_fff")}_MsgID_{GetMsgID(data)}.blob", FileMode.Create);
        //        fs.Write(data, 0, data.Length);
        //        fs.Close();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// 取得電文的Msg ID
        ///// </summary>
        ///// <param name="data">電文資料</param>
        ///// <returns></returns>
        //public static string GetLevel1MsgIDD(this byte[] data, bool isReverse= false)
        //{
        //    string strResult = "";
        //    byte[] tmp = { 0x00, 0x00 };

        //    if (data.Length >= 4)
        //    {
        //        Buffer.BlockCopy(data, 2, tmp, 0, 2);

        //        if(isReverse)
        //            Array.Reverse(tmp);
                
        //        strResult = BitConverter.ToInt16(tmp, 0).ToString("000");
        //    }

        //    return strResult;
        //}

        ///// <summary>
        ///// 取得電文的Msg ID
        ///// </summary>
        ///// <param name="data">電文資料</param>
        ///// <returns></returns>
        //public static string GetMMSMsgID(this byte[] data)
        //{
        //    string strResult = "";
        //    byte[] tmp = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        //    if (data.Length >= 29)
        //    {
        //        Buffer.BlockCopy(data, 4, tmp, 0, 6);             
        //        strResult = Encoding.UTF8.GetString(tmp).Trim();
        //    }

        //    return strResult;
        //}

        ///// <summary>
        ///// 取得電文的Msg ID
        ///// </summary>
        ///// <param name="data">電文資料</param>
        ///// <returns></returns>
        //public static string GetWMSMsgID(this byte[] data)
        //{
        //    string strResult = "";
        //    byte[] tmp = { 0x00, 0x00, 0x00, 0x00 };

        //    if (data.Length >= 26)
        //    {
        //        Buffer.BlockCopy(data, 0, tmp, 0, 4);
        //        strResult = Encoding.UTF8.GetString(tmp).Trim();
        //    }

        //    return strResult;
        //}


        /// <summary>
        /// 紀錄解析後的電文資料到資料表
        /// </summary>
        /// <param name="data">要紀錄的電文資料結構</param>
        /// <param name="t">電文資料結構型別</param>
        /// <returns></returns>
        public static bool RecordRawDataToDB(object data, Type t, String dbAppConfigSetting)
        {
            bool bResult = false;
            string strColumns = "";
            string strDatas = "";
            string strSQL = "";

            try
            {
                strSQL = GenSQLFromStruct(data, t, ref strColumns, ref strDatas);

                // insert DB
                string strConnectionString = dbAppConfigSetting;
                SqlConnection conn = new SqlConnection(strConnectionString);
                conn.Open();

                SqlCommand sc = new SqlCommand(strSQL, conn);

                if (sc.ExecuteNonQuery() > 0) bResult = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return bResult;
        }
     
        /// <summary>
        /// 根據傳入的資料與型別，產生raw data備份語法
        /// </summary>
        /// <param name="data">資料</param>
        /// <param name="t">資料型別</param>
        /// <param name="columnData"></param>
        /// <param name="valueData"></param>
        /// <param name="iIndex"></param>
        /// <returns></returns>
        public static string GenSQLFromStruct(object data, Type t, ref string columnData, ref string valueData, int iIndex = 0)
        {
            bool bIsArray = false;
            Int32 iSizeConst = 0;
            Type FieldType = null;
            string strProcessDateTime = "";

            foreach (FieldInfo fi in t.GetFields())
            {
                FieldType = fi.FieldType;
                bIsArray = false;
                iSizeConst = 0;

                bIsArray = fi.FieldType.IsArray;
                if (bIsArray)
                {
                    FieldType = Assembly.GetEntryAssembly().GetType(fi.FieldType.FullName.Replace("[]", ""));
                    if (FieldType == null)
                    {
                        FieldType = fi.FieldType.Assembly.GetType(fi.FieldType.FullName.Replace("[]", ""));
                    }

                    Array array = fi.GetValue(data) as Array;
                    iSizeConst = array.Length;

                    for (int ii = 0; ii <= iSizeConst - 1; ii++)
                    {
                        if (FieldType == typeof(int) || FieldType == typeof(short) || FieldType == typeof(long) || FieldType == typeof(Single) || FieldType == typeof(Double))
                        {

                            columnData += $"{fi.Name}_{ii.ToString("00")},";
                            valueData += $"'{array.GetValue(ii).ToString()}',";
                        }
                        else if (FieldType == typeof(char))
                        {
                            if (ii == iSizeConst - 1)
                            {
                                columnData += $"{fi.Name},";
                                valueData += $"'{new string((char[])fi.GetValue(data))}',";
                            }
                        }
                        else
                        {
                            GenSQLFromStruct(array.GetValue(ii), FieldType, ref columnData, ref valueData, ii + 1);
                        }
                    }
                }
                else
                {

                    if (fi.Name == "MsgNo")
                    {
                        // 不儲存
                    }
                    else if (fi.Name == "ProcessDate")
                    {
                        strProcessDateTime = fi.GetValue(data).ToString();
                        if (strProcessDateTime.Length >= 8)
                        {
                            strProcessDateTime = strProcessDateTime.Insert(4, "-").Insert(7, "-") + " ";
                        }
                        else
                        {
                            // error log
                        }
                    }
                    else if (fi.Name == "ProcessTime")
                    {
                        string strDate = fi.GetValue(data).ToString();

                        if (strDate.Length >= 8)
                        {
                            strDate = strDate.Insert(2, ":").Insert(5, ":").Insert(8, ".");
                            strProcessDateTime += strDate;
                        }
                        else
                        {
                            // error log
                        }

                        columnData += "ProcessDateTime,";
                        valueData += $"'{strProcessDateTime}',";
                    }
                    else
                    {
                        if (FieldType == typeof(int) || FieldType == typeof(short) || FieldType == typeof(long) || FieldType == typeof(Single) || FieldType == typeof(Double))
                        {
                            if (iIndex > 0)
                            {
                                columnData += $"{fi.Name}_{iIndex.ToString("00")},";
                            }
                            else
                            {
                                columnData += $"{fi.Name},";
                            }

                            valueData += $"'{fi.GetValue(data).ToString()}',";
                        }
                        else
                        {
                            GenSQLFromStruct(fi.GetValue(data), FieldType, ref columnData, ref valueData);
                        }
                    }
                }
            }

            return $"INSERT INTO {t.Name} (COLUMN_DATA) VALUES(VALUE_DATA)".Replace("COLUMN_DATA", columnData.Substring(0, columnData.Length - 1)).Replace("VALUE_DATA", valueData.Substring(0, valueData.Length - 1));
        }

        #region Byte array convert to data
        /// <summary>
        ///     Check if swap byte array and convert to short
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <param name="isSwap"> Swap flag (true：swap，false：no swap) </param>
        /// <returns></returns>
        public static short BytesToShort(byte[] bytes, bool isSwap = false)
        {
            if (isSwap) Array.Reverse(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }


        /// <summary>
        ///     Check if swap byte array and convert to ushort
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <param name="isSwap"> Swap flag (true：swap，false：no swap) </param>
        /// <returns></returns>
        public static ushort BytesToUShort(byte[] bytes, bool isSwap = false)
        {
            if (isSwap) Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }


        /// <summary>
        ///     Check if swap byte array and convert to int
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <param name="isSwap"> Swap flag (true：swap，false：no swap) </param>
        /// <returns></returns>
        public static int BytesToInt(byte[] bytes, bool isSwap = false)
        {
            if (isSwap) Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }


        /// <summary>
        ///     Check if swap byte array and convert to uint
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <param name="isSwap"> Swap flag (true：swap，false：no swap) </param>
        /// <returns></returns>
        public static uint BytesToUInt(byte[] bytes, bool isSwap = false)
        {
            if (isSwap) Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }


        /// <summary>
        ///     Check if swap byte array and convert to single
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <param name="isSwap"> Swap flag (true：swap，false：no swap) </param>
        /// <returns></returns>
        public static Single BytesToSingle(byte[] bytes, bool isSwap = false)
        {
            if (isSwap) Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }


        /// <summary>
        ///     Check if swap byte array and convert to string
        /// </summary>
        /// <param name="bytes"> Pending byte array </param>
        /// <returns></returns>
        public static string BytesToString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }
        #endregion

        public static string DumpByteInfo(this byte[] bytes)
        {
            string tmpHexString = "";
            string tmpAsciiString = "";
            string retString = Environment.NewLine + "-----------------------------------------------------------------" + Environment.NewLine;

            for (int idx = 0; idx < bytes.Length; idx++)
            {
                string tmpStr = bytes[idx].ToString("X");

                if (tmpStr.Length == 1) tmpStr = "0" + tmpStr;

                tmpHexString += " " + tmpStr;

                if (bytes[idx] < 20)
                {
                    tmpAsciiString += ".";
                }
                else
                {
                    tmpAsciiString += Encoding.ASCII.GetString(bytes, idx, 1);
                }

                if (idx % 16 == 15)
                {
                    retString += tmpHexString + " " + tmpAsciiString + Environment.NewLine;
                    tmpHexString = "";
                    tmpAsciiString = "";
                }
            }

            if ((bytes.Length - 1) % 16 != 15)
            {
                for (int idx = 0; idx < 15 - ((bytes.Length - 1) % 16); idx++)
                {
                    tmpHexString += "   ";
                    tmpAsciiString += " ";
                }

                retString += tmpHexString + " " + tmpAsciiString + Environment.NewLine;
            }
            retString += "-----------------------------------------------------------------" + Environment.NewLine;
            return retString;
        }

    }


}

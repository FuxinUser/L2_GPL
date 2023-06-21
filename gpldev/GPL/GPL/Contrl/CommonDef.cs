using System;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace GPLManager.Contrl
{
    public static class CommonDef
    {
        public struct CoilSampleSt
        {
            public string ProdCoilId;
            public string MatCoilId;
            public string ProdTime;
            public string TimeRollingStart;
        }

        // ===Setting Variable===
        // Log Level
        public static int MyLogLevel = 0;
        // Max Reconnection Count
        public static int MaxConnCount = 3;
        // Keep Log Days
        public static int LogKeepDays = 15;
        // Log Max Size
        public static int LogMaxSize = 3000000;
        // Log Folder
        public static string LogDirName = "Log";
        // Initial Log User Name
        public static string LoginUser = string.Empty;
        // Database Handler
        public static DBHandler MyDB = new DBHandler();
        // Log Handler
        public static LogHandler MyLog = new LogHandler();

        // 'Log模式
        // '[0]	一般事件。
        // '[1]	傳送事件。
        // '[2]	接收事件。
        // '[3]	例外事件。
        // '[4]	其他事件。
        public enum LogMode
        {
            Event = 0,
            Send = 1,
            Receive = 2,
            Exception = 3,
            Temp = 4
        }

        // 'Log等級
        // '[0]	追蹤事件。
        // '[1]	開發事件。
        // '[2]	訊息事件。
        // '[3]	警告事件。
        // '[4]	錯誤事件。
        // '[5]	致命事件。
        public enum LogLevel
        {
            Trace = 0,
            Debug = 1,
            Info = 2,
            Warn = 3,
            Error = 4,
            Fatal = 5
        }

        public static DataTable getDemoData(string FileName)
        {
            DataTable dt = new DataTable();
            string TextLine = "";
            string TextHeader = "";
            string[] SplitLine;
            string[] SplitHeader;

            string fullPath = Environment.CurrentDirectory;
            FileName = Path.Combine(fullPath, FileName);

            try
            {
                if (File.Exists(FileName) == true)
                {
                    StreamReader objReader = new StreamReader(FileName, Encoding.Default);
                    int index = 0;

                    while (objReader.Peek() != -1)
                    {
                        if (index > 0)
                        {
                            TextLine = objReader.ReadLine().Trim();
                            SplitLine = TextLine.Split(',');
                            dt.Rows.Add(SplitLine);
                        }
                        else
                        {
                            TextHeader = objReader.ReadLine().Trim();
                            SplitHeader = TextHeader.Split(',');

                            for (int i = 0; i <= SplitHeader.Length - 1; i++)
                            {
                                if (SplitHeader[i].Trim() != "")
                                {
                                    dt.Columns.Add(SplitHeader[i], typeof(string));
                                }
                            }
                        }
                        index = index + 1;
                    }
                    objReader.Close();
                }
                else
                {
                    MessageBox.Show("File " + FileName + " Does Not Exist");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("開啟檔案( " + FileName + ") 發生錯誤" + ex.Message);
            }

            return dt;
        }
    }
}

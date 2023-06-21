using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace GPLManager.Contrl
{
    public class LogHandler
    {
        #region 變數宣告
        // Event Path
        private static string StrEventLogPath = @"..\" + CommonDef.LogDirName + @"\Event\";
        // Send Path
        private static string StrSendLogPath = @"..\" + CommonDef.LogDirName + @"\Send\";
        // Receive Path
        private static string StrReceiveLogPath = @"..\" + CommonDef.LogDirName + @"\Receive\";
        // Exception Path
        private static string StrExceptionLogPath = @"..\" + CommonDef.LogDirName + @"\Exception\";
        // Temp Path
        private static string StrTempLogPath = @"..\" + CommonDef.LogDirName + @"\Temp\";

        private static LogHandler LogHandlerTemp = null;
        private static object UtilityLock = typeof(LogHandler);

        public static int EventLogNum = 1;
        public static int SendLogNum = 1;
        public static int ReceiveLogNum = 1;
        public static int ExceptionLogNum = 1;
        public static int TempLogNum = 1;

        public int NowEventLogNum
        {
            get
            {
                return EventLogNum;
            }
            set
            {
                EventLogNum = value;
            }
        }

        public int NowSendLogNum
        {
            get
            {
                return SendLogNum;
            }
            set
            {
                SendLogNum = value;
            }
        }

        public int NowReceiveLogNum
        {
            get
            {
                return ReceiveLogNum;
            }
            set
            {
                ReceiveLogNum = value;
            }
        }

        public int NowExceptionLogNum
        {
            get
            {
                return ExceptionLogNum;
            }
            set
            {
                ExceptionLogNum = value;
            }
        }

        public int NowTempLogNum
        {
            get
            {
                return TempLogNum;
            }
            set
            {
                TempLogNum = value;
            }
        }
        #endregion

        public LogHandler()
        {
            if (StrExceptionLogPath == null)
                throw new Exception("StrExceptionLogPath 未設定!");

            if (StrEventLogPath == null)
                throw new Exception("StrEventLogPath 未設定!");

            if (StrSendLogPath == null)
                throw new Exception("StrSendLogPath 未設定!");

            if (StrReceiveLogPath == null)
                throw new Exception("StrReceiveLogPath 未設定!");

            if (StrTempLogPath == null)
                throw new Exception("StrTempLogPath 未設定!");

            EventLogNum = GetLogNum(CommonDef.LogMode.Event);
            SendLogNum = GetLogNum(CommonDef.LogMode.Send);
            ReceiveLogNum = GetLogNum(CommonDef.LogMode.Receive);
            ExceptionLogNum = GetLogNum(CommonDef.LogMode.Exception);
            TempLogNum = GetLogNum(CommonDef.LogMode.Temp);
        }

        public static LogHandler GetInstance()
        {
            lock ((UtilityLock))
            {
                if (LogHandlerTemp == null)
                    LogHandlerTemp = new LogHandler();
                return LogHandlerTemp;
            }
        }

        private int GetLogNum(CommonDef.LogMode Mode)
        {
            string StrLogPath = string.Empty;
            string StrLogNameExtension = string.Empty;
            int ReturnNum = 1;
            try
            {
                switch (Mode)
                {
                    case CommonDef.LogMode.Event:
                        {
                            StrLogPath = StrEventLogPath;
                            StrLogNameExtension = ".event.log";
                            break;
                        }

                    case CommonDef.LogMode.Receive:
                        {
                            StrLogPath = StrReceiveLogPath;
                            StrLogNameExtension = ".receive.log";
                            break;
                        }

                    case CommonDef.LogMode.Send:
                        {
                            StrLogPath = StrSendLogPath;
                            StrLogNameExtension = ".send.log";
                            break;
                        }

                    case CommonDef.LogMode.Exception:
                        {
                            StrLogPath = StrExceptionLogPath;
                            StrLogNameExtension = ".exception.log";
                            break;
                        }

                    case CommonDef.LogMode.Temp:
                        {
                            StrLogPath = StrTempLogPath;
                            StrLogNameExtension = ".temp.log";
                            break;
                        }
                }

                if (File.Exists(StrLogPath + DateTime.Now.ToString("yyyy.MM.dd-HH") + "." + ReturnNum + StrLogNameExtension))
                {
                    FileInfo logfile = new FileInfo(StrLogPath + DateTime.Now.ToString("yyyy.MM.dd-HH") + "." + ReturnNum + StrLogNameExtension);
                    if (logfile.Length > CommonDef.LogMaxSize)
                    {
                        ReturnNum += 1;
                    }
                }
                return ReturnNum;
            }
            catch
            {
                return ReturnNum;
            }
        }

        public string ReSetLogNum(CommonDef.LogMode Mode)
        {
            // Record Full Log File Name
            string FilePath = string.Empty;
            string NewFilePath = string.Empty;
            string StrLogPath = string.Empty;
            string StrLogNameExtension = string.Empty;
            string NowTime = DateTime.Now.ToString("yyyy.MM.dd-HH");

            switch (Mode)
            {
                case CommonDef.LogMode.Event:
                    {
                        StrLogPath = StrEventLogPath;
                        StrLogNameExtension = "." + EventLogNum.ToString() + ".event.log";
                        break;
                    }

                case CommonDef.LogMode.Receive:
                    {
                        StrLogPath = StrReceiveLogPath;
                        StrLogNameExtension = "." + ReceiveLogNum.ToString() + ".receive.log";
                        break;
                    }

                case CommonDef.LogMode.Send:
                    {
                        StrLogPath = StrSendLogPath;
                        StrLogNameExtension = "." + SendLogNum.ToString() + ".send.log";
                        break;
                    }

                case CommonDef.LogMode.Exception:
                    {
                        StrLogPath = StrExceptionLogPath;
                        StrLogNameExtension = "." + ExceptionLogNum.ToString() + ".exception.log";
                        break;
                    }

                case CommonDef.LogMode.Temp:
                    {
                        StrLogPath = StrTempLogPath;
                        StrLogNameExtension = "." + TempLogNum.ToString() + ".temp.log";
                        break;
                    }
            }

            if (StrLogPath.Substring(StrLogPath.Length - 1, 1) != @"\")
            {
                StrLogPath += @"\";
            }

            // Log File Name
            FilePath += StrLogPath;
            FilePath += NowTime;
            FilePath += StrLogNameExtension;

            if (!File.Exists(FilePath))
            {
                // The Log File is not exist, Conbine New File Path(full name)
                switch (Mode)
                {
                    case CommonDef.LogMode.Event:
                        {
                            EventLogNum = 1;
                            NewFilePath = StrLogPath + NowTime + "." + EventLogNum.ToString() + ".event.log";
                            break;
                        }

                    case CommonDef.LogMode.Receive:
                        {
                            ReceiveLogNum = 1;
                            NewFilePath = StrLogPath + NowTime + "." + ReceiveLogNum.ToString() + ".receive.log";
                            break;
                        }

                    case CommonDef.LogMode.Send:
                        {
                            SendLogNum = 1;
                            NewFilePath = StrLogPath + NowTime + "." + SendLogNum.ToString() + ".send.log";
                            break;
                        }

                    case CommonDef.LogMode.Exception:
                        {
                            ExceptionLogNum = 1;
                            NewFilePath = StrLogPath + NowTime + "." + ExceptionLogNum.ToString() + ".exception.log";
                            break;
                        }

                    case CommonDef.LogMode.Temp:
                        {
                            TempLogNum = 1;
                            NewFilePath = StrLogPath + NowTime + "." + TempLogNum.ToString() + ".temp.log";
                            break;
                        }
                }
                return NewFilePath;
            }

            // The Log File is exist, Go to Check if The size of log is over the limit of log
            FileInfo logfile = new FileInfo(FilePath);
            if (logfile.Length > CommonDef.LogMaxSize)
            {
                // Over The Limit
                switch (Mode)
                {
                    case CommonDef.LogMode.Event:
                        {
                            EventLogNum += 1;
                            NewFilePath = StrLogPath + NowTime + "." + EventLogNum.ToString() + ".event.log";
                            break;
                        }

                    case CommonDef.LogMode.Receive:
                        {
                            ReceiveLogNum += 1;
                            NewFilePath = StrLogPath + NowTime + "." + ReceiveLogNum.ToString() + ".receive.log";
                            break;
                        }

                    case CommonDef.LogMode.Send:
                        {
                            SendLogNum += 1;
                            NewFilePath = StrLogPath + NowTime + "." + SendLogNum.ToString() + ".send.log";
                            break;
                        }

                    case CommonDef.LogMode.Exception:
                        {
                            ExceptionLogNum += 1;
                            NewFilePath = StrLogPath + NowTime + "." + ExceptionLogNum.ToString() + ".exception.log";
                            break;
                        }

                    case CommonDef.LogMode.Temp:
                        {
                            TempLogNum += 1;
                            NewFilePath = StrLogPath + NowTime + "." + TempLogNum.ToString() + ".temp.log";
                            break;
                        }
                }
                return NewFilePath;
            }
            else
            {
                return FilePath;
            }
        }

        public void WriteLog(string Comment, CommonDef.LogMode Mode, CommonDef.LogLevel Level, Exception LogException = null)
        {
            StringBuilder SbLogString = new StringBuilder();
            StringBuilder SbLogName = new StringBuilder(128, 256);
            SbLogName.Append(ReSetLogNum(Mode));

            try
            {
                SbLogString.Append("[" + Level.ToString().PadRight(5) + "]" + DateTime.Now.ToString("[yyyyMMddHHmmss.fff]"));
                SbLogString.Append(GetMethodInfo());
                if (Convert.ToInt32(Level) >= CommonDef.MyLogLevel)
                {
                    if (LogException == null)
                        SbLogString.Append("[" + Comment + "]");
                    else
                    {
                        int FramesCount = new System.Diagnostics.StackTrace(LogException, true).GetFrames().Count();
                        int ErrLine = new System.Diagnostics.StackTrace(LogException, true).GetFrame(FramesCount - 1).GetFileLineNumber();
                        SbLogString.Append("[行:" + ErrLine.ToString() + "][" + Comment + "]" + Environment.NewLine + "EX:[" + LogException.Message + "]");
                    }
                }
            }
            catch
            {

            }

            SbLogString = null;
            SbLogName = null;
        }

        public bool CleanLog(string LogDir)
        {
            // Log Path
            string Dir = LogDir;
            string Headline = "Log檢查與清除";

            if (Directory.Exists(Dir))
            {
                DirectoryInfo DirObj = new DirectoryInfo(Dir);
                FileInfo[] Files = DirObj.GetFiles("*.*");
                DateTime LastWriteDate;
                TimeSpan tsTemp;

                foreach (FileInfo Filename in Files)
                {
                    try
                    {
                        LastWriteDate = File.GetCreationTime(Filename.FullName);
                        tsTemp = LastWriteDate.Subtract(DateTime.Now);
                        if (tsTemp.Duration().TotalDays > CommonDef.LogKeepDays)
                            File.Delete(Filename.FullName);
                    }
                    catch (Exception ex)
                    {
                        WriteLog(Headline + "失敗,FileName<" + Filename.Name + ">", CommonDef.LogMode.Exception, CommonDef.LogLevel.Error, ex);
                    }
                }
                return true;
            }
            return false;
        }

        public string GetMethodInfo()
        {
            StackTrace ST = new StackTrace(true);
            return ("[" + ST.GetFrame(2).GetMethod().ReflectedType.Name + "." + ST.GetFrame(2).GetMethod().Name + "()][行:" + ST.GetFrame(2).GetFileLineNumber().ToString() + "]");
        }
    }
}

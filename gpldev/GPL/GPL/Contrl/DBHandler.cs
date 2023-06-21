using System;
using System.Diagnostics;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace GPLManager.Contrl
{
    public class DBHandler : IDisposable    // 使用IDisposable此interface，自行撰寫Dispose()
    {
        // ===變數宣告===
        public string strDBConnectString = string.Empty;
        public string SQLStatement = string.Empty;
        private SqlConnection myconnDB;

        public void ConnOpen()
        {
            CommonDef.MyLog.WriteLog("開始資料庫連線步驟", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
            try
            {
                strDBConnectString = string.Empty;
                CommonDef.MyLog.WriteLog("資料庫連線字串<<" + strDBConnectString + ">>", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                myconnDB = new SqlConnection(strDBConnectString);
                CommonDef.MyLog.WriteLog("建立資料庫連線物件完成", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                myconnDB.Open();
                CommonDef.MyLog.WriteLog("資料庫連線開啟完成", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
            }
            catch (Exception ex)
            {
                CommonDef.MyLog.WriteLog("開啟資料庫<<" + strDBConnectString + ">>連線失敗", CommonDef.LogMode.Exception, CommonDef.LogLevel.Warn, ex);
                CommonDef.MyLog.WriteLog("資料庫連線失敗,將Exveption丟至上一個呼叫Function!", CommonDef.LogMode.Event, CommonDef.LogLevel.Warn);
                throw new ArgumentException();
            }
        }

        public void ConnClose()
        {
            CommonDef.MyLog.WriteLog("開始關閉資料庫連線步驟", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
            if (myconnDB != null)
            {
                myconnDB.Close();
                CommonDef.MyLog.WriteLog("資料庫連線物件關閉完成", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                myconnDB.Dispose();
                CommonDef.MyLog.WriteLog("資料庫連線物件資源釋放完成", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
            }
        }

        public bool ReConn()
        {
            CommonDef.MyLog.WriteLog("開始重新資料庫連線步驟", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
            for (var index = 0; index <= CommonDef.MaxConnCount; index++)
            {
                ConnClose();
                CommonDef.MyLog.WriteLog("關閉資料庫連線步驟", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                ConnOpen();
                if (myconnDB.State == ConnectionState.Open)
                {
                    CommonDef.MyLog.WriteLog("重連資料庫第" + (index + 1).ToString() + "次成功", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                    return true;
                }
                CommonDef.MyLog.WriteLog("重連資料庫第" + (index + 1).ToString() + "次失敗", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                System.Threading.Thread.Sleep(1000);
            }
            CommonDef.MyLog.WriteLog("重新與資料庫連線機制完成,重連結果失敗", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
            return false;
        }

        public DataTable SQLSelect(string strSQL, string Comment = "", bool DoReConn = true)
        {
            SqlDataAdapter mydataAdapter = null;
            DataSet mydataset = new DataSet();
            string MethodName = GetMethodName();
            try
            {
                CommonDef.MyLog.WriteLog(Comment + "-資料庫查詢SQL字串:<" + strSQL + ">", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                mydataAdapter = new SqlDataAdapter(strSQL, myconnDB);
                CommonDef.MyLog.WriteLog(MethodName + "---Create DataAdapter Done", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                mydataAdapter.Fill(mydataset, MethodName);
                CommonDef.MyLog.WriteLog(MethodName + "---Data Into DataSet Done", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);

                // return mydataset.Tables;
                return new DataTable("Empty");
            }
            catch (Exception ex)
            {
                CommonDef.MyLog.WriteLog(Comment + "---取得資料表失敗,資料庫連線SQL字串:<" + strSQL + ">,資料庫重新連線:" + DoReConn.ToString(), CommonDef.LogMode.Exception, CommonDef.LogLevel.Fatal, ex);
                if (DoReConn)
                {
                    if (ReConn())
                    {
                        CommonDef.MyLog.WriteLog(MethodName + "---DB重連成功,再次執行SQLSelect", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                        return SQLSelect(strSQL, Comment, false);
                    }
                    else
                    {
                        CommonDef.MyLog.WriteLog(MethodName + "---DB重連失敗", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                        ConnClose();
                        return new DataTable("Empty");
                    }
                }
                else
                {
                    ConnClose();
                    CommonDef.MyLog.WriteLog(MethodName + "---Close DB Connect Done", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                    ConnOpen();
                    CommonDef.MyLog.WriteLog(MethodName + "---Open DB Connect Done", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                    return new DataTable("Empty");
                }
            }
            finally
            {
                // 釋放資料集資源
                mydataset.Dispose();
                CommonDef.MyLog.WriteLog(MethodName + "---Releaase DataSet Done", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                // 釋放DataAdapter 物件資源
                mydataAdapter.Dispose();
                CommonDef.MyLog.WriteLog(MethodName + "---Releaase DataAdapter Done", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
            }
        }

        public System.Data.DataSet SQLSelectDS(string strSQL, string Comment = "", bool DoReConn = true)
        {
            SqlDataAdapter mydataAdapter = null;
            DataSet mydataset = new DataSet();
            string MethodName = GetMethodName();
            try
            {
                CommonDef.MyLog.WriteLog(Comment + "-資料庫查詢SQL字串:<" + strSQL + ">", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                // 執行SELECT命令，建立一個DataAdapter 物件(橋接器)，使用 Fill 從資料來源將資料載入 DataSet
                mydataAdapter = new SqlDataAdapter(strSQL, myconnDB);
                CommonDef.MyLog.WriteLog(MethodName + "---Create DataAdapter Done", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                mydataAdapter.Fill(mydataset);
                CommonDef.MyLog.WriteLog(MethodName + "---Data Into DataSet Done", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                return mydataset;
            }
            catch (Exception ex)
            {
                CommonDef.MyLog.WriteLog(Comment + "---取得資料表失敗,資料庫連線SQL字串:<" + strSQL + ">,資料庫重新連線:" + DoReConn.ToString(), CommonDef.LogMode.Exception, CommonDef.LogLevel.Fatal, ex);
                if (DoReConn)
                {
                    if (ReConn())
                    {
                        CommonDef.MyLog.WriteLog(MethodName + "---DB重連成功,再次執行SQLSelect", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                        return SQLSelectDS(strSQL, Comment, false);
                    }
                    else
                    {
                        CommonDef.MyLog.WriteLog(MethodName + "---DB重連失敗", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                        ConnClose();
                        return new DataSet("Empty");
                    }
                }
                else
                {
                    ConnClose();
                    CommonDef.MyLog.WriteLog(MethodName + "---Close DB Connect Done", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                    ConnOpen();
                    CommonDef.MyLog.WriteLog(MethodName + "---Open DB Connect Done", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                    return new DataSet("Empty");
                }
            }
            finally
            {
                // 釋放資料集資源
                mydataset.Dispose();
                CommonDef.MyLog.WriteLog(MethodName + "---Releaase DataSet Done", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                // 釋放DataAdapter 物件資源
                mydataAdapter.Dispose();
                CommonDef.MyLog.WriteLog(MethodName + "---Releaase DataAdapter Done", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
            }
        }

        /// <summary>
        ///     ''' 連接資料庫進行資料插入、變更、刪除功能
        ///     ''' </summary>
        ///     ''' <param name="strSQL">欲執行SQL字串</param>
        ///     ''' <param name="Comment">資料插入、變更、刪除備註</param>
        ///     ''' <returns>是否完成任務</returns>
        ///     ''' <remarks></remarks>
        public bool SQLInsertUpdate(string strSQL, string Comment = "")
        {
            // Using myconnDB As New Odbc.OdbcConnection(strDBConnectString)
            SqlCommand myCmd = new SqlCommand();
            SqlTransaction myTransaction;
            string MethodName = GetMethodName();
            // 受影響的資料筆數
            int DataSum = 0;
            CommonDef.MyLog.WriteLog(Comment + "-資料庫變更SQL字串:<" + strSQL + ">", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
            try
            {
                // 設定連線資料庫物件
                myCmd.Connection = myconnDB;
                CommonDef.MyLog.WriteLog(MethodName + "---設定連線資料庫物件完成", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                // 開始交易
                myTransaction = myconnDB.BeginTransaction();
                CommonDef.MyLog.WriteLog(MethodName + "---開始資料庫交易完成", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                // 執行SQL命令
                myCmd.Transaction = myTransaction;
                CommonDef.MyLog.WriteLog(MethodName + "---指定資料庫交易物件完成", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                myCmd.CommandText = strSQL;
                CommonDef.MyLog.WriteLog(MethodName + "---指定資料庫變更字串完成", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                DataSum = myCmd.ExecuteNonQuery();
                CommonDef.MyLog.WriteLog(MethodName + "---取得影響資料庫資料筆數(" + DataSum.ToString() + "完成", CommonDef.LogMode.Event, CommonDef.LogLevel.Debug);
                // 關閉資料庫連結，交易完成
                myTransaction.Commit();
                CommonDef.MyLog.WriteLog(MethodName + "---關閉資料庫連結，交易完成", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                return true;
            }
            catch (Exception ex)
            {
                CommonDef.MyLog.WriteLog(Comment + "---進行資料插入、變更、刪除失敗,資料庫連線SQL字串:<" + strSQL + ">", CommonDef.LogMode.Exception, CommonDef.LogLevel.Fatal, ex);
                return false;
            }
            finally
            {
                // 釋放myCmd資源
                myCmd.Dispose();
                CommonDef.MyLog.WriteLog(MethodName + "---釋放myCmd資源完成", CommonDef.LogMode.Event, CommonDef.LogLevel.Info);
                // 進行重新連線
                ConnClose();
                ConnOpen();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public string GetMethodName()
        {
            StackTrace ST = new StackTrace(true);
            return (ST.GetFrame(ST.GetFrames().Count() - 2).GetMethod().ReflectedType.Name + "." + ST.GetFrame(2).GetMethod().Name + "()");
        }
    }
}

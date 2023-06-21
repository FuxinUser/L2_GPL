using GPLManager.Util;
using System;
using System.Data;
using System.Data.SqlClient;

namespace GPLManager
{
    public class Data_Access
    {
        private static readonly object _syncRoot = new Object();
        private static Data_Access _instance = null;

        /// <summary>
        /// Get DataAccess entity
        /// </summary>
        /// <returns></returns>
        public static Data_Access GetInstance()
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new Data_Access();
                    }
                }
            }

            return _instance;
        }


        #region "Search data"

        /// <summary>
        /// Search data (DataSet) by db ConnectionString
        /// </summary>
        /// <param name="strSql">Sql command text</param>
        /// <param name="strConn">Sql connection text</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        public DataSet SelectDS(string strSql, string strConn, int timeout = 30)
        {
            SqlConnection conn = new SqlConnection(strConn);        // Get connected entity

            DataSet ds;

            try
            {
                ds = SelectDS(strSql, conn, timeout);           // Get DataSet
            }
            catch (Exception ex)
            {
                if (conn.Database.IsEmpty() && conn.DataSource.IsEmpty())
                {
                    PublicComm.ExceptionLog.Debug($"资料库连线有误:{ex}");
                    PublicComm.ClientLog.Debug($"资料库连线有误，语法: {strSql}");
                }
                throw;
            }

            return ds;
        }

        /// <summary>
        /// Search data (DataSet) by SqlConnection
        /// </summary>
        /// <param name="strSql">Sql command text</param>
        /// <param name="conn">Sql connected entity</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        protected DataSet SelectDS(string strSql, SqlConnection conn, int timeout = 30)
        {
            SqlCommand cmd = null;
            SqlDataAdapter adapter = null;
            DataSet ds = null;

            try
            {
                using (conn)
                {
                    if (conn.State == ConnectionState.Closed ||
                        conn.State == ConnectionState.Broken)
                    {
                        conn.Open();

                        using (cmd = new SqlCommand(strSql, conn))
                        {
                            cmd.CommandTimeout = timeout;       // Set timeout

                            using (adapter = new SqlDataAdapter(cmd))
                            {
                                ds = new DataSet();
                                adapter.Fill(ds);
                            }
                        }

                    }
                }
            }
            catch
            {
                if (conn != null) conn.Close();
                throw;
            }
            return ds;
        }

        /// <summary>
        /// Search data (DataTable) by db ConnectionString
        /// </summary>
        /// <param name="strSql">Sql command text</param>
        /// <param name="strConn">Sql connection text</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        public DataTable Select(string strSql, string strConn, int timeout = 30)
        {

            DataSet ds = SelectDS(strSql, strConn, timeout);

            return (ds != null || ds.Tables.Count > 0) ? ds.Tables[0] : null;
        }
        /// <summary>
        /// Search data (DataTable) by SqlConnection
        /// </summary>
        /// <param name="strSql">Sql command text</param>
        /// <param name="conn">Sql connected entity</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        protected DataTable Select(string strSql, SqlConnection conn, int timeout = 30)
        {
            DataSet ds = SelectDS(strSql, conn, timeout);

            return (ds != null || ds.Tables.Count > 0) ? ds.Tables[0] : null;
        }

        #endregion


        #region "Check data"

        /// <summary>
        /// Check if data exist by db connectionString
        /// </summary>
        /// <param name="strSql">Sql command text</param>
        /// <param name="strConn">Sql connection text</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        public bool IsExist(string strSql, string strConn, int timeout = 30)
        {
            DataTable dt = Select(strSql, strConn, timeout);

            return (dt != null || dt.Rows.Count > 0) ? true : false;
        }
        /// <summary>
        /// Check if data exist by SqlConnection
        /// </summary>
        /// <param name="strSql">Sql command text</param>
        /// <param name="conn">Sql connected entity</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        protected bool IsExist(string strSql, SqlConnection conn, int timeout = 30)
        {
            DataTable dt = Select(strSql, conn, timeout);

            return (dt != null || dt.Rows.Count > 0) ? true : false;
        }

        #endregion

        #region "Execute Sql"

        /// <summary>
        /// Execute sql command by db ConnectionString
        /// </summary>
        /// <param name="strSql">Sql command text</param>
        /// <param name="strConn">Sql connection text</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string strSql, string strConn, int timeout = 30)
        {
            SqlConnection conn = new SqlConnection(strConn);        // Get connected entity

            try
            {
                bool result = ExecuteNonQuery(strSql, conn, timeout);
                return result;
            }
            catch(Exception ex)
            {
                if (conn.Database.IsEmpty() && conn.DataSource.IsEmpty())
                {
                    PublicComm.ExceptionLog.Debug($"资料库连线有误:{ex}");
                    PublicComm.ClientLog.Debug($"资料库连线有误，语法: {strSql}");
                }

                return false;
            }

            
        }
        /// <summary>
        /// Execute sql command by SqlConnection
        /// </summary>
        /// <param name="strSql">Sql command text</param>
        /// <param name="conn">Sql connected entity</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        protected bool ExecuteNonQuery(string strSql, SqlConnection conn, int timeout = 30)
        {
            SqlCommand cmd = null;
            int result = 0;

            try
            {
                using (conn)
                {
                    if (conn.State == ConnectionState.Closed ||
                        conn.State == ConnectionState.Broken)
                    {
                        conn.Open();

                        using (cmd = new SqlCommand(strSql, conn))
                        {
                            cmd.CommandTimeout = timeout;       // Set timeout
                            result = cmd.ExecuteNonQuery();     // Execute sql command
                        }
                    }
                }
            }
            catch 
            {
                if (conn != null) conn.Close();
                throw;
            }

            return (result == 0) ? false : true;
        }


        #endregion

        /// <summary>
        /// 搜寻资料库
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="strDbCon"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        public static DataTable Fun_SelectDate(string strSql,string strDbCon,string strMessage,string strFrame = "")
        {
            DataTable dt = new DataTable();

            try
            {
                dt = GetInstance().Select(strSql,strDbCon);
                PublicComm.ClientLog.Info($"{strMessage}查詢資料庫成功");
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"查询错误:{ex.Message}", $"{strMessage}", null, 3);

                PublicComm.ClientLog.Debug($"{strMessage}查询资料库失败:{ex}");
                PublicComm.ExceptionLog.Debug($"{strMessage}查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"{strMessage}查询资料库語法: {strSql}");
            }

            return dt;
        }

        /// <summary>
        /// 新增、修改、删除资料库
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="strDbCon"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        public bool Fun_ExecuteQuery(string strSql, string strDbCon, string strMessage, string strFrame = "")
        {
            try
            {
                bool bol = GetInstance().ExecuteNonQuery(strSql, strDbCon);
                if (bol)
                {
                    PublicComm.ClientLog.Debug($"{strMessage}资料处理成功");
                    return true;
                }
                else
                {

                    PublicComm.ClientLog.Debug($"{strMessage}资料处理失败");
                    PublicComm.ExceptionLog.Debug($"{strMessage}资料处理失败");
                    PublicComm.ClientLog.Debug($"{strMessage}查询资料库语法");
                    return false;
                }


            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"资料处理失败:{ex.Message}", $"{strMessage}",null, 3);

                PublicComm.ClientLog.Debug($"{strMessage}资料处理失败: {ex.Message}");
                PublicComm.ExceptionLog.Debug($"{strMessage}资料处理失败: {ex.Message}");
                PublicComm.ClientLog.Debug($"{strMessage}查询资料库语法: {strSql}");

                return false;
            }
            //try
            //{
            //    GetInstance().ExecuteNonQuery(strSql,strDbCon);
            //    PublicComm.ClientLog.Debug($"{strMessage}資料處理成功");
            //    return true;
            //}
            //catch (Exception ex)
            //{ 
            //    PublicComm.ClientLog.Debug($"{strMessage}資料處理失敗:{ex}");
            //    PublicComm.ExceptionLog.Debug($"{strMessage}資料處理失敗:{ex}");
            //    PublicComm.ClientLog.Debug($"{strMessage}查询资料库語法: {strSql}");
            //    return false;
            //}
        }
    }
}
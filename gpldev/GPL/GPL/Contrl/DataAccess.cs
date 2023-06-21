using System;
using System.Data;
using System.Data.SqlClient;

namespace CPL1HMI
{
    public class DataAccess
    {
        private static readonly object _syncRoot = new Object();
        private static DataAccess _instance = null;

        /// <summary>
        /// Get DataAccess entity
        /// </summary>
        /// <returns></returns>
        public static DataAccess GetInstance()
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new DataAccess();
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
            DataSet ds = SelectDS(strSql, conn, timeout);           // Get DataSet
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
            catch (Exception ex)
            {
                if (conn != null) conn.Close();

                Console.WriteLine(ex.Message);
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
            bool result = ExecuteNonQuery(strSql, conn, timeout);

            return result;
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
            catch (Exception ex)
            {
                if (conn != null) conn.Close();

                Console.WriteLine(ex.Message);
                throw;
            }

            return (result == 0) ? false : true;
        }


        #endregion
    }
}
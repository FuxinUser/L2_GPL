using Dapper;

using DBService.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Z.Dapper.Plus;
/**
* Author : ICSC SPYUA
* Date: 2019/12/24
* Desc: DB Context 實作
* **/

namespace DBService.UnitOfWork
{
    public class DapperDBContext : IDBContext, IDapperMethod
    {
        private readonly int ExcuteFail = -1;
         
        private ISqlGenStrategy _sqlGen;

        /// <summary>
        /// 連線的字串
        /// </summary>
        public string _connectString { get; private set; }
        public DapperDBContext(string connectString)
        {
            //Sql Gen 實作抽換
            _sqlGen = new DapperSqlGen();
            _connectString = connectString;
        }

        #region -- 基礎CURD --      
        public int Create(string table, object data)
        {
            var sql = _sqlGen.Create(table, data);
            //return Execute(sql.ToString(), data);
            return Execute(sql.ToString());            
        }
     
        public int Update(string table, object data, string condition)
        {
            var sql = _sqlGen.Update(table, data, condition);
            //return Execute(sql.ToString(), data);
            return Execute(sql.ToString());
        }
       
        public int Delete(string table, string condition)
        {
            var sql = _sqlGen.Delete(table, condition);
            return Execute(sql.ToString());
        }
       
        public IEnumerable<T> Read<T>(string table, bool isReadAll, int num, string condition = "", string otherCondition = "")
        {
            var sql = _sqlGen.Read(table, isReadAll, num, condition, otherCondition);
            return Query<T>(sql.ToString());           
        }

        public T ReadOne<T>(string table, string condition = "", string otherCondition = "")
        {
            var sql = _sqlGen.Read(table, true, 1, condition, otherCondition);
       
            return QuerySingleOrDefault<T>(sql.ToString());       
        }
        #endregion


        #region -- Dapper OP操做 --


     
        #endregion

        #region -- Dapper Exe --
        public int Execute(string sql, object data)
        {
            int infNum = ExcuteFail;

            var conn = new SqlConnection(_connectString);

            //using (var conn = new SqlConnection(_connectString))
            //{
            //    conn.Open();

            //    using (var tran = conn.BeginTransaction())
            //    {
            //        try
            //        {
            //            conn.Execute(sql, data, tran);
            //            tran.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            tran.Rollback();
            //            // handle the error however you need to.
            //            throw;
            //        }
            //    }
            //}

            try
            {

                using (var trans = new TransactionScope())
                {
                    using (conn)
                    {
                        if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                        {
                            conn.Open();
                        }
                        infNum = conn.Execute(sql, data);
                    }
                    trans.Complete();
                }
                //using (conn)
                //{
                //    if (conn.State == ConnectionState.Closed ||
                //        conn.State == ConnectionState.Broken)
                //    {
                //        conn.Open();
                //    }
                //    infNum = conn.Execute(sql.ToString(), data);
                //}
            }
            catch (Exception ex)
            {
                if (conn != null) conn.Close();
                throw ex;
            }

            return infNum;
        }
        public int Execute(string sql)
        {
            int infNum = ExcuteFail;

            SqlConnection conn = new SqlConnection(_connectString);
            try
            {
                using (conn)
                {
                    if (conn.State == ConnectionState.Closed ||
                        conn.State == ConnectionState.Broken)
                    {
                        conn.Open();
                    }
                    infNum = conn.Execute(sql.ToString());
                }
            }
            catch (Exception ex)
            {
                if (conn != null) conn.Close();
                throw ex;
            }

            return infNum;
        }
        public T ExecuteScalar<T> (string sql)
        {
            T ans = default(T);

            SqlConnection conn = new SqlConnection(_connectString);
            try
            {
                using (conn)
                {
                    if (conn.State == ConnectionState.Closed ||
                        conn.State == ConnectionState.Broken)
                    {
                        conn.Open();
                    }
                    ans = conn.ExecuteScalar<T>(sql.ToString());
                }
            }
            catch (Exception ex)
            {
                if (conn != null) conn.Close();
                throw ex;
            }

            return ans;
        }
       
        public IEnumerable<T> Query<T>(string sql)
        {
            List<T> datas = new List<T>();

            SqlConnection conn = new SqlConnection(_connectString);
            try
            {
                using (conn)
                {
                    if (conn.State == ConnectionState.Closed ||
                        conn.State == ConnectionState.Broken)
                    {
                        conn.Open();
                    }
                    datas = conn.Query<T>(sql.ToString()).ToList();
                }
            }
            catch
            {
                if (conn != null) conn.Close();
                throw;
            }

            return datas;
        }
        public T QueryFirstOrDefault<T>(string sql)
        {
            SqlConnection conn = new SqlConnection(_connectString);        // Get connected entity
            T ans = default(T);
            try
            {
                using (conn)
                {
                    if (conn.State == ConnectionState.Closed ||
                        conn.State == ConnectionState.Broken)
                    {
                        conn.Open();
                    }
                    ans = conn.QueryFirstOrDefault<T>(sql.ToString());
                }
            }
            catch
            {
                if (conn != null) conn.Close();
                throw;
            }

            return ans;
        }
        public T QuerySingleOrDefault<T>(string sql)
        {
            SqlConnection conn = new SqlConnection(_connectString);        // Get connected entity
            T ans = default(T);
            try
            {
                using (conn)
                {
                    if (conn.State == ConnectionState.Closed ||
                        conn.State == ConnectionState.Broken)
                    {
                        conn.Open();
                    }
                    ans = conn.QuerySingleOrDefault<T>(sql.ToString());
                }
            }
            catch
            {
                if (conn != null) conn.Close();
                throw;
            }

            return ans;
        }
        #endregion

        #region -- Dapper Plus -- 

        [Obsolete("套件要錢,捨棄不用",true)]
        public void BulkInsert<T>(object data, string tableName)
        {

            DapperPlusManager.Entity<T>().Table(tableName);

            SqlConnection conn = new SqlConnection(_connectString);
         
            try
            {
                using (conn)
                {
                    if (conn.State == ConnectionState.Closed ||
                        conn.State == ConnectionState.Broken)
                    {
                        conn.Open();
                    }
                    conn.BulkInsert(data);
                }

            }
            catch 
            {
                if (conn != null) conn.Close();
                throw;
            }

        }
        #endregion

        // Sql Copy
        public void SqlBulkCopy(DataTable dt, string tableName)
        {
            SqlConnection conn = new SqlConnection(_connectString);


            using (var tx = new TransactionScope())
            {
                try
                {
                    using (conn)
                    {
                        if (conn.State == ConnectionState.Closed ||
                            conn.State == ConnectionState.Broken)
                        {
                            conn.Open();
                        }

                        using (var sqlBulkCopy = new SqlBulkCopy(conn))
                        {
                            sqlBulkCopy.DestinationTableName = tableName;
                            sqlBulkCopy.WriteToServer(dt);
                        }
                        tx.Complete();
                    }
                }
                catch (Exception ex)
                {
                    if (conn != null) conn.Close();
                    throw ex;
                }
            }  
        }


      

    }
}

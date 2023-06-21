using Core.Handle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static DBService.Base.DBAttributes;

namespace DBService
{
 
    public class DBHandler
    {
        //Connection string for handlers to access database
        private string _strConn = "";
        private LogHandler _logger = null;

        public DBHandler(LogHandler logger, string strConn)
        {
            if (_logger == null) _logger = logger;
            _strConn = strConn;
        }

        //Dapper操作
        public List<T> Query<T>(string sqlStr)
        {
            return DataAccess.GetInstance().Query<T>(sqlStr, _strConn);
        }
        public T QueryOne<T>(string sqlStr)
        {
            return DataAccess.GetInstance().QueryOne<T>(sqlStr, _strConn);
        }
    

        #region "Other method"

        /// <summary>
        /// Convert datatable into class
        /// </summary>
        /// <typeparam name="T">Input and output type</typeparam>
        /// <param name="dt">Datatale</param>
        /// <returns></returns>
        private List<T> TransDtToClass<T>(DataTable dt) where T : new()
        {
            List<T> list = null;

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<T>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        T t = new T();

                        foreach (PropertyInfo prop in t.GetType().GetProperties())
                        {
                            if (Attribute.IsDefined(prop, typeof(IgnoreReflction)))
                                continue;

                            if (prop.PropertyType.Name == dr[prop.Name].GetType().Name)     // If another is also the same type 
                            {
                                prop.SetValue(t, dr[prop.Name]);
                            }
                            else                                                            // If not the same type
                            {
                                switch (prop.PropertyType)
                                {
                                    case Type strType when strType == typeof(string):
                                        {
                                            prop.SetValue(t, dr[prop.Name].ToString());
                                            break;
                                        }
                                    case Type intType when intType == typeof(int):
                                        {
                                            int.TryParse(dr[prop.Name].ToString(), out int val);
                                            prop.SetValue(t, val);
                                            break;
                                        }
                                    case Type shortType when shortType == typeof(short):
                                        {
                                            short.TryParse(dr[prop.Name].ToString(), out short val);
                                            prop.SetValue(t, val);
                                            break;
                                        }
                                    //case Type singleType when singleType == typeof(Single):
                                    //    {
                                    //        Single.TryParse(dr[prop.Name].ToString(), out short val);
                                    //        prop.SetValue(t, val);
                                    //        break;
                                    //    }
                                    case Type floatType when floatType == typeof(float):
                                        {
                                            float.TryParse(dr[prop.Name].ToString(), out float val);
                                            prop.SetValue(t, val);
                                            break;
                                        }
                                    case Type dateType when dateType == typeof(DateTime):
                                        {
                                            DateTime.TryParse(dr[prop.Name].ToString(), out DateTime val);
                                            prop.SetValue(t, val);
                                            break;
                                        }
                                }
                            }
                        }

                        list.Add(t);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TransDtToClass. ex=" + ex.ToString());
                _logger.Error(" ex.Message=" + ex.Message);
                _logger.Error(" ex.StackTrace=" + ex.StackTrace);
            }

            return list;
        }

        /// <summary>
        /// Get sql condition string
        /// </summary>
        /// <param name="dicKey">Sql condition field</param>
        /// <returns></returns>
        private string GetSqlWhere(Dictionary<string, object> dicKey)
        {
            string strWhere = "";

            try
            {
                if (dicKey != null && dicKey.Count > 0)
                {
                    strWhere = "Where ";

                    foreach (KeyValuePair<string, object> item in dicKey)
                    {
                        string condition = (item.Value.GetType().Name == "String") ? "{0}='{1}' And " : "{0}={1} And ";
                        strWhere += string.Format(condition, item.Key, item.Value);
                    }

                    strWhere = strWhere.Substring(0, strWhere.Length - 4);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetSqlWhere. ex=" + ex.ToString());
                _logger.Error(" ex.Message=" + ex.Message);
                _logger.Error(" ex.StackTrace=" + ex.StackTrace);
            }

            return strWhere;
        }

        /// <summary>
        /// Get the insert format command string of the KeyValuePair value of the property
        /// </summary>
        /// <param name="itemValue">KeyValuePair value of the property</param>
        /// <returns></returns>
        private string GetInsertValue(object itemValue)
        {
            switch (itemValue.GetType())
            {
                case Type strType when strType == typeof(string): return string.Format("'{0}',", itemValue);
                case Type dateType when dateType == typeof(DateTime): return string.Format("'{0}',", ((DateTime)itemValue).ToString("yyyy/MM/dd HH:mm:ss.fff"));
                default: return string.Format("{0},", itemValue);
            }
        }

        /// <summary>
        /// Get the update format command string of the KeyValuePair value of the property
        /// </summary>
        /// <param name="itemKey">KeyValuePair key of the property</param>
        /// <param name="itemValue">KeyValuePair value of the property</param>
        /// <returns></returns>
        private string GetUpdateValue(string itemKey, object itemValue)
        {
            switch (itemValue.GetType())
            {
                case Type strType when strType == typeof(string): return string.Format("{0}='{1}',", itemKey, itemValue);
                case Type dateType when dateType == typeof(DateTime): return string.Format("{0}='{1}',", itemKey, ((DateTime)itemValue).ToString("yyyy/MM/dd HH:mm:ss.fff"));
                default: return string.Format("{0}={1},", itemKey, itemValue);
            }
        }

        #endregion


        #region "Constructor"

        public DBHandler(LogHandler logger)
        {
            if (_logger == null) _logger = logger;
        }

        #endregion


        #region "Selct one or all"

        /// <summary>
        /// Select one data
        /// </summary>
        /// <typeparam name="T">Input and output type</typeparam>
        /// <param name="dicKey">Sql condition field</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        public T SelOne<T>(Dictionary<string, object> dicKey, int timeout = 30) where T : new()
        {
            List<T> list = SelAll<T>(dicKey, timeout);

            if (list != null && list.Count() > 0)
            {
                return list[0];
            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// Select all data
        /// </summary>
        /// <typeparam name="T">Input and output type</typeparam>
        /// <param name="dicKey">Sql condition field</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        public List<T> SelAll<T>(Dictionary<string, object> dicKey = null, int timeout = 30) where T : new()
        {
            List<T> list = null;

            try
            {
                string strSql = string.Format("Select * From {0} ", typeof(T).Name);
                strSql += GetSqlWhere(dicKey);
                //_logger.Debug("SelAll. strSql=" + strSql);

                DataTable dt = DataAccess.GetInstance().Select(strSql, _strConn, timeout);
                list = TransDtToClass<T>(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // _logger.Error("SelAll. ex=" + ex.ToString());
                // _logger.Error(" ex.Message=" + ex.Message);
                //_logger.Error(" ex.StackTrace=" + ex.StackTrace);
            }

            return list;
        }

        #endregion


        #region "Insert or update data"

        /// <summary>
        /// Insert or update data
        /// </summary>
        /// <typeparam name="T">Input and output type</typeparam>
        /// <param name="t">The data to be edited</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        public bool InsertOrUpdate<T>(T t, int timeout = 30) where T : new()
        {
            Dictionary<string, object> dicKey = new Dictionary<string, object>();           // Save the where primary and identity field of sql
            Dictionary<string, object> dicPKey = new Dictionary<string, object>();          // Save the where primary field of sql
            Dictionary<string, object> dicEdit = new Dictionary<string, object>();          // Save the edit field of sql

            // Get the properties of the primary key or the edit
            foreach (PropertyInfo prop in t.GetType().GetProperties())
            {
                if (Attribute.IsDefined(prop, typeof(IdentityKey)))     // If is identity key
                {
                    dicKey.Add(prop.Name, prop.GetValue(t));
                }
                else if (Attribute.IsDefined(prop, typeof(PrimaryKey))) // If is primary key
                {
                    dicKey.Add(prop.Name, prop.GetValue(t));
                    dicPKey.Add(prop.Name, prop.GetValue(t));
                }
                else                                                    // If not primary key
                {
                    dicEdit.Add(prop.Name, prop.GetValue(t));
                }
            }

            // Check if insert or update
            T selT = SelOne<T>(dicKey);
            if (EqualityComparer<T>.Default.Equals(selT, default(T)))
            {
                return Insert<T>(dicPKey, dicEdit, timeout);            // Execute insert
            }
            else
            {
                return Update<T>(dicKey, dicEdit, timeout);             // Execute Update
            }
        }
        /// <summary>
        /// Calling insert data by InsertOrUpdate()
        /// </summary>
        /// <typeparam name="T">Input type</typeparam>
        /// <param name="dicPKey"></param>
        /// <param name="dicEdit"></param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        private bool Insert<T>(Dictionary<string, object> dicPKey, Dictionary<string, object> dicEdit, int timeout = 30)
        {
            bool isOk = false;
            string strFix = "";     // Insert fixed columns
            string strVal = "";     // Insert values

            try
            {
                foreach (KeyValuePair<string, object> item in dicPKey)
                {
                    strFix += string.Format("{0},", item.Key);
                    strVal += GetInsertValue(item.Value);
                }
                foreach (KeyValuePair<string, object> item in dicEdit)
                {
                    strFix += string.Format("{0},", item.Key);
                    strVal += GetInsertValue(item.Value);
                }
                strFix = strFix.Substring(0, strFix.Length - 1);
                strVal = strVal.Substring(0, strVal.Length - 1);

                string strSql = string.Format("Insert into {0} ({1}) Values ({2}) ", typeof(T).Name, strFix, strVal);
                _logger.Debug("InsertOrUpdate. strSql=" + strSql);


                isOk = DataAccess.GetInstance().ExecuteNonQuery(strSql, _strConn, timeout);
            }
            catch (Exception ex)
            {
                _logger.Error("InsertOrUpdate. ex=" + ex.ToString());
                _logger.Error(" ex.Message=" + ex.Message);
                _logger.Error(" ex.StackTrace=" + ex.StackTrace);
            }

            return isOk;
        }
        /// <summary>
        /// Calling update data by InsertOrUpdate()
        /// </summary>
        /// <typeparam name="T">Input type</typeparam>
        /// <param name="dicKey"></param>
        /// <param name="dicEdit"></param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        private bool Update<T>(Dictionary<string, object> dicKey, Dictionary<string, object> dicEdit, int timeout = 30)
        {
            bool isOk = false;
            string strUpd = "";

            try
            {
                foreach (KeyValuePair<string, object> item in dicEdit)
                {
                    strUpd += GetUpdateValue(item.Key, item.Value);
                }
                strUpd = strUpd.Substring(0, strUpd.Length - 1);

                string strSql = string.Format("Update {0} Set {1} ", typeof(T).Name, strUpd);
                string strWhere = GetSqlWhere(dicKey);
                _logger.Debug("InsertOrUpdate. strSql=" + strSql + strWhere);

                if (strWhere != "")
                {
                    isOk = DataAccess.GetInstance().ExecuteNonQuery(strSql + strWhere, _strConn, timeout);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("InsertOrUpdate. ex=" + ex.ToString());
                _logger.Error(" ex.Message=" + ex.Message);
                _logger.Error(" ex.StackTrace=" + ex.StackTrace);
            }

            return isOk;
        }

        public bool Insert<T>(T t, int timeout = 30) where T : new()
        {
            Dictionary<string, object> dicKey = new Dictionary<string, object>();           // Save the where primary and identity field of sql
            Dictionary<string, object> dicPKey = new Dictionary<string, object>();          // Save the where primary field of sql
            Dictionary<string, object> dicEdit = new Dictionary<string, object>();          // Save the edit field of sql

            // Get the properties of the primary key or the edit
            foreach (PropertyInfo prop in t.GetType().GetProperties())
            {

                if (Attribute.IsDefined(prop, typeof(IgnoreReflction)))
                    continue;
                    

                if (Attribute.IsDefined(prop, typeof(IdentityKey)))     // If is identity key
                {
                    dicKey.Add(prop.Name, prop.GetValue(t));
                }
                else if (Attribute.IsDefined(prop, typeof(PrimaryKey))) // If is primary key
                {
                    dicKey.Add(prop.Name, prop.GetValue(t));
                    dicPKey.Add(prop.Name, prop.GetValue(t));
                }
                else                                                    // If not primary key
                {
                    dicEdit.Add(prop.Name, prop.GetValue(t));
                }
            }


            return InsertData<T>(dicKey, dicEdit, timeout);             // Execute Update
        }
        /// <summary>
        /// Calling insert data by InsertOrUpdate()
        /// </summary>
        /// <typeparam name="T">Input type</typeparam>
        /// <param name="dicPKey"></param>
        /// <param name="dicEdit"></param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        private bool InsertData<T>(Dictionary<string, object> dicPKey, Dictionary<string, object> dicEdit, int timeout = 30)
        {
            bool isOk = false;
            string strFix = "";     // Insert fixed columns
            string strVal = "";     // Insert values

            try
            {
                foreach (KeyValuePair<string, object> item in dicPKey)
                {
                    strFix += string.Format("{0},", item.Key);
                    strVal += GetInsertValue(item.Value);
                }
                foreach (KeyValuePair<string, object> item in dicEdit)
                {
                    strFix += string.Format("{0},", item.Key);
                    strVal += GetInsertValue(item.Value);
                }
                strFix = strFix.Substring(0, strFix.Length - 1);
                strVal = strVal.Substring(0, strVal.Length - 1);

                string strSql = string.Format("Insert into {0} ({1}) Values ({2}) ", typeof(T).Name, strFix, strVal);
                // _logger.Debug("InsertOrUpdate. strSql=" + strSql);


                isOk = DataAccess.GetInstance().ExecuteNonQuery(strSql, _strConn, timeout);
                // _logger.Debug("Insert success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // _logger.Error("InsertOrUpdate. ex=" + ex.ToString());
                // _logger.Error(" ex.Message=" + ex.Message);
                // _logger.Error(" ex.StackTrace=" + ex.StackTrace);

            }

            return isOk;
        }
        #endregion


        #region "Delete one"

        /// <summary>
        /// Delete one data
        /// </summary>
        /// <typeparam name="T">Input and output type</typeparam>
        /// <param name="dicKey">Sql where condition field</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        public bool DelOne<T>(Dictionary<string, object> dicKey, int timeout = 30)
        {
            bool isOk = false;

            try
            {
                string strSql = string.Format("Delete {0} ", typeof(T).Name);
                strSql += GetSqlWhere(dicKey);
                _logger.Debug("DelOne. strSql=" + strSql);

                isOk = DataAccess.GetInstance().ExecuteNonQuery(strSql, _strConn, timeout);
            }
            catch (Exception ex)
            {
                _logger.Error("DelOne. ex=" + ex.ToString());
                _logger.Error(" ex.Message=" + ex.Message);
                _logger.Error(" ex.StackTrace=" + ex.StackTrace);
            }

            return isOk;
        }

        #endregion


        #region "DataAccess function"

        /// <summary>
        /// Search data (DataSet) by db ConnectionString
        /// </summary>
        /// <param name="strSql">Sql command text</param>
        /// <param name="strConn">Sql connection text</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        public DataSet SelDS(string strSql, string strConn, int timeout = 30)
        {
            DataSet ds = null;

            try
            {
                _logger.Debug("SelDS. strSql=" + strSql);

                ds = DataAccess.GetInstance().SelectDS(strSql, strConn, timeout);
            }
            catch (Exception ex)
            {
                _logger.Error("SelDS. ex=" + ex.ToString());
                _logger.Error(" ex.Message=" + ex.Message);
                _logger.Error(" ex.StackTrace=" + ex.StackTrace);
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
        public DataTable SelDT(string strSql, string strConn, int timeout = 30)
        {
            DataTable dt = null;

            try
            {
                _logger.Debug("SelDT. strSql=" + strSql);

                dt = DataAccess.GetInstance().Select(strSql, strConn, timeout);
            }
            catch (Exception ex)
            {
                _logger.Error("SelDT. ex=" + ex.ToString());
                _logger.Error(" ex.Message=" + ex.Message);
                _logger.Error(" ex.StackTrace=" + ex.StackTrace);
            }

            return dt;
        }
        public List<T> Sel<T>(string strSql, int timeout = 30) where T : new()
        {
            DataTable dt = null;
            List<T> list = null;
            try
            {
                _logger.Debug("SelDT. strSql=" + strSql);

                dt = DataAccess.GetInstance().Select(strSql, _strConn, timeout);
                list = TransDtToClass<T>(dt);
            }
            catch (Exception ex)
            {
                _logger.Error("SelDT. ex=" + ex.ToString());
                _logger.Error(" ex.Message=" + ex.Message);
                _logger.Error(" ex.StackTrace=" + ex.StackTrace);
            }

            return list;
        }
        /// <summary>
        /// Execute sql command by db ConnectionString
        /// </summary>
        /// <param name="strSql">Sql command text</param>
        /// <param name="strConn">Sql connection text</param>
        /// <param name="timeout">Limit timeout</param>
        /// <returns></returns>
        public bool Query(string strSql, string strConn, int timeout = 30)
        {
            bool isOk = false;

            try
            {
                _logger.Debug("Query. strSql=" + strSql);

                isOk = DataAccess.GetInstance().ExecuteNonQuery(strSql, strConn, timeout);
            }
            catch (Exception ex)
            {
                _logger.Error("Query. ex=" + ex.ToString());
                _logger.Error(" ex.Message=" + ex.Message);
                _logger.Error(" ex.StackTrace=" + ex.StackTrace);
            }

            return isOk;
        }

        #endregion
    }
}

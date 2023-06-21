using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static DBService.Base.DBAttributes;

/*
 * Author: ICSC SPYUA
 * Date: 2019/12/24
 * Desc: Dapper Sql Gen
 **/

namespace DBService.DB
{
    public class DapperSqlGen : ISqlGenStrategy
    {
  

        #region -- CURD --

        public string Create(string table, object data)
        {
            ToDic(data, out var dicPKey, out var dicEdit);
            var sqlStr = GetInsertSql(dicPKey, dicEdit, table);

            #region old code

            //int infNum = ExcuteFail;
            //var sql = new StringBuilder();
            //sql.Append("INSERT");
            //sql.Append(" INTO ");
            //sql.Append(table);
            //sql.Append("(");

            //if (data != null)
            //{
            //    // Identity key
            //    string prefix = "";
            //    foreach (var field in data.GetType().GetProperties())
            //    {
            //        //For 測試暫寫-正規操作下插入新資料盡量避免有欄位是有NULL狀況
            //        if (field.GetValue(data) == null)
            //            continue;

            //        if (Attribute.IsDefined(field, typeof(IgnoreReflction)))
            //            continue;

            //        sql.Append(prefix);
            //        prefix = ",";
            //        sql.Append(field.Name);

            //    };
            //    sql.Append(")");


            //    // Identity key value
            //    sql.Append(" VALUES(");
            //    prefix = "";
            //    foreach (var field in data.GetType().GetProperties())
            //    {
            //        //For 測試暫寫-正規操作下插入新資料盡量避免有欄位是有NULL狀況
            //        if (field.GetValue(data) == null)
            //            continue;

            //        if (Attribute.IsDefined(field, typeof(IgnoreReflction)))
            //            continue;

            //        sql.Append(prefix);
            //        prefix = ",";
            //        sql.Append(SqlGenHelp.GetInsertValue(field.GetValue(data)));
            //    };

            //}
            //else
            //{
            //    sql.Append("VALUES (NULL");
            //}

            //sql.Append(")");
            //var sqlStr = sql.ToString();
            //sql = null;
            #endregion

            return sqlStr;
        }

        public string Update(string table,  object updateData, string condition)
        {
            ToDic(updateData, out var dicPKey, out var dicEdit);
            var sql = GetUpdateSql(dicPKey, dicEdit, table);        
            if (!string.IsNullOrEmpty(condition))
            {
                sql+=" WHERE ";
                sql += condition;
            }

            #region old code
            //var sql = new StringBuilder();
            //sql.Append("UPDATE ");
            //sql.Append(table);
            //sql.Append(" SET ");

            //// SET
            //string prefix = ""; 
            //foreach (var field in updateData.GetType().GetProperties())
            //{

            //    if (field.GetValue(updateData) == null)
            //        continue;

            //    if (Attribute.IsDefined(field, typeof(IgnoreReflction)))
            //        continue;

            //    sql.Append(prefix);
            //    prefix = ",";

            //    sql.Append(field.Name);
            //    sql.Append("=");

            //    var parmName = "@" + field.Name;
            //    sql.Append(parmName);

            //};

            //// WHERE
            //if (!string.IsNullOrEmpty(condition))
            //{
            //    sql.Append(" WHERE ");
            //    sql.Append(condition);
            //}
            #endregion
            //Console.WriteLine(sql.ToString());
            return sql.ToString();
        }

        public string Read(string table, bool isReadAll, int num = 1, string condition = "", string otherCondition = "")
        {
            var sql = new StringBuilder();

            if(isReadAll)
                sql.Append("SELECT * FROM ");
            else
                sql.Append($"SELECT TOP {num} * FROM ");

            sql.Append(table);
            // WHERE的條件
            if (!string.IsNullOrEmpty(condition))
            {
                sql.Append(" WHERE ");
                sql.Append(condition);
            }
            // 其餘條件
            if (!string.IsNullOrEmpty(otherCondition))
            {
                sql.Append(string.Format(" {0}", otherCondition));
            }
            //Console.WriteLine(sql.ToString());
            return sql.ToString();
        }
      
        public string Delete(string table, string condition)
        {
            var sql = new StringBuilder();
            sql.Append("DELETE FROM ");
            sql.Append(table);

            if (!condition.Equals(""))
            {
              sql.Append(" WHERE ");
              sql.Append(condition);
            }
                  
            ////Console.WriteLine(sql.ToString());
            return sql.ToString();
        }

        #endregion


        private void ToDic<T>(T t, out Dictionary<string, object> PK, out Dictionary<string, object> edit)
        {
            PK = new Dictionary<string, object>();
            edit = new Dictionary<string, object>();

            var editKey = t.GetType().GetProperties().Where(prop => !prop.IsDefined(typeof(IgnoreReflction)) && !prop.IsDefined(typeof(PrimaryKey))); 
            var pkKey = t.GetType().GetProperties().Where(prop => prop.IsDefined(typeof(PrimaryKey)) && !prop.IsDefined(typeof(IgnoreReflction)));

            foreach (var item in editKey)
            {
                edit.Add(item.Name, item.GetValue(t));
            }
            foreach (var item in pkKey)
            {
                PK.Add(item.Name, item.GetValue(t));
            }
        }

        private string GetInsertSql(Dictionary<string, object> Pks, Dictionary<string, object> edits, string tableName)
        {
            string strFix = "";     // Insert fixed columns
            string strVal = "";     // Insert values

            foreach (KeyValuePair<string, object> item in Pks)
            {
                strFix += string.Format("{0},", item.Key);
                strVal += GetInsertValue(item.Value);
            }
            foreach (KeyValuePair<string, object> item in edits)
            {
                strFix += string.Format("{0},", item.Key);
                strVal += GetInsertValue(item.Value);
            }
            strFix = strFix.Substring(0, strFix.Length - 1);
            strVal = strVal.Substring(0, strVal.Length - 1);

            string strSql = $"Insert into {tableName} ({strFix}) Values ({strVal}) ";      
            return strSql;
        }

        private string GetUpdateSql(Dictionary<string, object> Pks, Dictionary<string, object> edits, string tableName)
        {
            string strUpd = string.Empty;

            foreach (KeyValuePair<string, object> item in edits)
            {
                if (item.Value == null)
                {
                    //strUpd += $"{item.Key} = null,";
                }
                else
                {
                    strUpd += GetUpdateValue(item.Key, item.Value);
                }

            }
            strUpd = strUpd.Substring(0, strUpd.Length - 1);
            var strSql = string.Format("Update {0} Set {1} ", tableName, strUpd);
            //var strWhere = GetSqlWhere(Pks);
            return strSql;
        }

        private string GetUpdateValue(string itemKey, object itemValue)
        {
            if (itemValue == null)
            {
                //return "null,";
                return "'',";
            }
            switch (itemValue.GetType())
            {
                case Type strType when strType == typeof(string):
                    //return string.Format("{0}='{1}',", itemKey, ((string)itemValue));
                    return string.Format("{0}=N'{1}',", itemKey, ((string)itemValue));

                case Type dateType when dateType == typeof(DateTime):

                    if (((DateTime)itemValue).Ticks == 0)
                    {
                        //return $"{itemKey} = '',";
                        //return $"{itemKey} = null,";
                        return string.Empty;    // 不做任何事情
                    }
                    return string.Format("{0}='{1}',", itemKey, ((DateTime)itemValue).ToString("yyyy/MM/dd HH:mm:ss.fff"));
                
                default: 
                    
                    return string.Format("{0}={1},", itemKey, itemValue);
            }
        }

        private string GetSqlWhere(Dictionary<string, object> dicKey)
        {
            string strWhere = "";

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

            return strWhere;
        }


        private string GetInsertValue(object itemValue)
        {
            if (itemValue == null)
            {
                //return "null,";
                return "'',";
            }

            switch (itemValue.GetType())
            {
                case Type strType when strType == typeof(string):

                    //var strValue = string.Format("'{0}',", ((string)itemValue).Trim());
                    var strValue = string.Format("'{0}',", ((string)itemValue));
                    return strValue;                    

                case Type dateType when dateType == typeof(DateTime):
                    
                    if (((DateTime)itemValue).Ticks == 0)
                    {
                        //return "'',";
                        return "null,";
                    }

                    var dateTimeVaule = string.Format("'{0}',", ((DateTime)itemValue).ToString("yyyy/MM/dd HH:mm:ss.fff"));

                    return dateTimeVaule;


                case Type dateType when dateType == typeof(byte[]):

                    var byteData = itemValue as byte[];
                    var result = string.Concat(byteData.Select(b => Convert.ToString(b, 2)));
                    var byteStr =  string.Format("{0},", "0x"+result);
                    return byteStr;


                default: 

                    var value = string.Format("{0},", itemValue);

                    return value;
            }
        }




    }
}

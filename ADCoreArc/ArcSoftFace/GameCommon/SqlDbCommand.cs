using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.GameCommon
{
    public class SqlDbCommand : SqliteConnect
    {
        private SQLiteCommand _sqlComm;
        public SqlDbCommand(string path) : base(path)
        {
            _sqlComm = new SQLiteCommand(sqliteConnect);
            // command .CommandText = $""
        }
        #region 表管理

        /// <summary>
        /// 查询表是否存在
        /// </summary>
        /// <param name="name">表名</param>
        /// <returns></returns>
        public int IsCreateTable(string name)
        {
            try
            {
                var sql = $"select count(*) as c from sqlite_master where type ='table' and name = '{name}' ";
                _sqlComm.CommandText = sql;
                if (_sqlComm.ExecuteScalar().ToString() == "0")
                {
                    return 0;
                }
                else
                {
                    return 1;

                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"数据库表查询异常：{e.Message}");
                return -1;
            }

        }

        /// <summary>
        /// 添加数据库表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">表名</param>
        /// <returns></returns>
        public int CreateTable<T>(string name)
        {
            try
            {
                int isCreateTable = IsCreateTable(name);
                if (isCreateTable == 1)
                {
                    return 1;
                }
                if (isCreateTable == -1)
                {
                    return -1;
                }
                else
                {
                    var type = typeof(T);
                    var taleName = type.Name;
                    var sb = new StringBuilder();
                    sb.Append($"create table {name} (");
                    var properties = type.GetProperties();
                    foreach (var p in properties)
                    {
                        var attribute = p.GetCustomAttribute<ModeHelp>();
                        if (attribute.IsCreated)
                        {
                            sb.Append($"{attribute.FieldName} {attribute.Type} ");
                            if (attribute.IsCanBeNull)
                            {
                                sb.Append(" null ");
                            }
                            else
                            {
                                sb.Append(" not null ");
                            }
                            if (attribute.IsPrinaryKey)
                            {
                                sb.Append(" primary key");
                                if (attribute.IsAutomaticIncrease)
                                {
                                    if (attribute.Type == "integer")
                                    {
                                        sb.Append(" autoincrement");
                                    }
                                }
                            }
                            sb.Append(",");
                        }
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(")");

                    _sqlComm.CommandText = sb.ToString();
                    return _sqlComm.ExecuteNonQuery();
                }

            }
            catch (System.Exception e)
            {
                Console.WriteLine($"数据库表创建异常：{e.Message}");
                return -1;
            }

        }

        /// <summary>
        /// 删除数据库表
        /// </summary>
        /// <param name="name">表名</param>
        /// <returns></returns>
        public int DeleteTable(string name)
        {
            try
            {
                var sql = $"drop table {name}";
                _sqlComm.CommandText = sql;
                return _sqlComm.ExecuteNonQuery();
            }
            catch (System.Exception e)
            {

                Console.WriteLine($"数据库表删除异常：{e.Message}");
                return -1;
            }

        }

        /// <summary>
        /// 获取数据量
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="key">字段名</param>
        /// <returns></returns>
        public object GetKey(string name, string key)
        {
            try
            {
                var sql = $"SELECT max({key}) FROM {name} ";
                _sqlComm.CommandText = sql;
                return _sqlComm.ExecuteScalar();
            }
            catch (System.Exception e)
            {

                Console.WriteLine($"查询Key异常：{e.Message}");
                return -1;
            }
        }

        #endregion


        #region 数据管理(新增)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="name">表名</param>
        /// <returns></returns>
        public int Insert<T>(T t, string name) where T : class
        {
            try
            {
                if (t == default(T))
                {
                    Console.WriteLine("Insert()参数错误!");
                    return -1;
                }
                var type = typeof(T);
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append($"INSERT INTO {name} (");

                var propertys = type.GetProperties();
                foreach (var p in propertys)
                {
                    if (p.GetCustomAttribute<ModeHelp>().IsCreated)
                    {
                        stringBuilder.Append(p.GetCustomAttribute<ModeHelp>().FieldName);
                        stringBuilder.Append(",");
                    }
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append(") VALUES (");
                foreach (var p in propertys)
                {
                    if (p.GetCustomAttribute<ModeHelp>().IsCreated)
                    {
                        if (p.GetCustomAttribute<ModeHelp>().Type == "string")
                        {
                            stringBuilder.Append($"'{p.GetValue(t)}'");
                        }
                        else
                        {
                            stringBuilder.Append(p.GetValue(t));
                        }
                        stringBuilder.Append(",");
                    }
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append(")");

                _sqlComm.CommandText = stringBuilder.ToString();
                return _sqlComm.ExecuteNonQuery();
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"数据库表添加数据异常<T>(<T>, name)：{e.Message}");
                return -1;
            }

        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        /// <param name="name">表名</param>
        /// <returns></returns>
        public int Insert<T>(List<T> tList, string name) where T : class
        {
            try
            {
                if (tList == null || tList.Count == 0)
                {
                    Console.WriteLine("Insert()参数错误!");
                    return -1;
                }
                var type = typeof(T);
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append($"INSERT INTO {name} (");

                var propertys = type.GetProperties();
                foreach (var p in propertys)
                {
                    if (p.GetCustomAttribute<ModeHelp>().IsCreated)
                    {
                        stringBuilder.Append(p.GetCustomAttribute<ModeHelp>().FieldName);
                        stringBuilder.Append(",");
                    }
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append(") VALUES ");
                foreach (var t in tList)
                {
                    stringBuilder.Append("(");
                    foreach (var p in propertys)
                    {
                        if (p.GetCustomAttribute<ModeHelp>().IsCreated)
                        {
                            if (p.GetCustomAttribute<ModeHelp>().Type == "string")
                            {
                                stringBuilder.Append($"'{p.GetValue(t)}'");

                            }
                            else
                            {
                                stringBuilder.Append(p.GetValue(t));

                            }
                            stringBuilder.Append(",");
                        }
                    }
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                    stringBuilder.Append("),");

                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);

                _sqlComm.CommandText = stringBuilder.ToString();
                return _sqlComm.ExecuteNonQuery();
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"数据库表添加数据异常<T>(List<T>, name)：{e.Message}");
                return -1;
            }

        }

        #endregion

        #region 数据管理(删除)

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="sqlWhere"></param>
        public int DeleteBySql(string name, string sqlWhere)
        {
            try
            {
                var sql = $"DELETE FROM {name} where {sqlWhere}";
                _sqlComm.CommandText = sql;
                return _sqlComm.ExecuteNonQuery();
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"数据库表数据删除异常：{e.Message}");
                return -1;
            }

        }

        #endregion


        #region 数据管理(更新/修改)

        /// <summary>
        /// 数据更新/修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="name">表名</param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int Updete<T>(T t, string name, string sqlWhere) where T : class
        {
            try
            {
                if (t == default(T))
                {
                    Console.WriteLine("Update()参数错误!");
                    return -1;
                }

                var type = typeof(T);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"UPDATE {name} set ");
                var propertys = type.GetProperties();

                foreach (var p in propertys)
                {
                    stringBuilder.Append($"{p.GetCustomAttribute<ModeHelp>().FieldName} = ");
                    if (p.GetCustomAttribute<ModeHelp>().Type == "string")
                    {
                        stringBuilder.Append($"'{p.GetValue(t)}'");
                    }
                    else
                    {
                        stringBuilder.Append(p.GetValue(t));

                    }
                    stringBuilder.Append(",");
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append($" where {sqlWhere}");

                _sqlComm.CommandText = stringBuilder.ToString();
                Console.WriteLine(stringBuilder.ToString());
                return _sqlComm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine($"数据库表数据更新异常：{e.Message}");
                return -1;
            }

        }

        #endregion

        #region 数据管理(查询)

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">表名</param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public List<T> SelectBySql<T>(string name, string sqlWhere = "") where T : class
        {
            var ret = new List<T>();
            string sql;
            if (string.IsNullOrEmpty(sqlWhere))
            {
                sql = $"SELECT * FROM {name}";
            }
            else
            {
                sql = $"SELECT * FROM {name} where {sqlWhere}";

            }
            _sqlComm.CommandText = sql;
            var dr = _sqlComm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    var l = DataReaderToData<T>(dr);
                    ret.Add(l);
                }
            }
            return ret;
        }

        private T DataReaderToData<T>(SQLiteDataReader dr) where T : class
        {
            try
            {
                List<string> fieldNames = new List<string>();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    fieldNames.Add(dr.GetName(i));
                }

                var type = typeof(T);
                T data = Activator.CreateInstance<T>();
                var properties = type.GetProperties();

                foreach (var p in properties)
                {
                    if (!p.CanWrite) continue;
                    var fieldName = p.GetCustomAttribute<ModeHelp>().FieldName;
                    if (fieldName.Contains(fieldName) && p.GetCustomAttribute<ModeHelp>().IsCreated)
                    {
                        p.SetValue(data, dr[fieldName]);
                    }
                }
                return data;
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"DataReaderToData()转换出错, 类型{typeof(T).Name}出错, 错误消息：{e.Message}");
                return null;
            }
        }

        #endregion

        public List<T> DbSql<T>(string sql) where T : class
        {
            var ret = new List<T>();
            if (!string.IsNullOrEmpty(sql))
            {
                _sqlComm.CommandText = sql;
            }
            var dr = _sqlComm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    var l = DataReaderToData<T>(dr);
                    ret.Add(l);
                }
            }
            return ret;
        }
    }
}

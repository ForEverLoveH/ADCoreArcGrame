using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Drawing;
using System.Web.Caching;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Data;
using File = System.IO.File;
using NPOI.OpenXmlFormats.Dml;
using System.Windows.Input;
using NPOI.POIFS.FileSystem;

namespace ArcSoftFace 
{
    public class SQLiteHelper
    {
        #region 字段
        /// <summary>
        ///  事务的基类
        /// </summary>
        private DbTransaction dbTransaction;
        /// <summary>
        /// 使用静态变量字典解决多线程实例本类，实现一个数据库对应一个clslock
        /// </summary>
        private static readonly Dictionary<string, ClsLock> rwl = new Dictionary<string, ClsLock>();
        /// <summary>
        ///  数据库地址
        /// </summary>
        private readonly string mDataFilePath;
        /// <summary>
        ///  数据库密码
        /// </summary>
        private readonly string mDbPathPassword;
        private readonly string LockName = null;
        private SQLiteConnection mConnection;
        #endregion
        #region 构造函数
        /// <summary>
        /// 根据数据库地址初始化
        /// </summary>
        /// <param name="dataFile"></param>
        public SQLiteHelper(string dataFile)
        {
            this.mDataFilePath = dataFile ?? throw new ArgumentNullException("dataFile=null");
            this.mDataFilePath = dataFile;
            if (!rwl.ContainsKey(dataFile))
            {
                LockName = dataFile;
                rwl.Add(dataFile, new ClsLock());

            }
        }/// <summary>
         /// 使用密码打开数据库
         /// </summary>
         /// <param name="datafile"></param>
         /// <param name="password"></param>
        public SQLiteHelper(string datafile, string password)
        {
            this.mDataFilePath = datafile ?? throw new ArgumentNullException("datafile is  null ");
            this.mDbPathPassword = password ?? throw new ArgumentNullException("password  is null ");
            this.mDataFilePath = datafile;
            if (!rwl.ContainsKey(datafile))
            {
                LockName = datafile;
                rwl.Add(datafile, new ClsLock());
            }
        }
        #endregion
        #region 打开/关闭数据库
        public void OpenSQLite()
        {
            if (string.IsNullOrEmpty(mDbPathPassword))
            {
                mConnection = OpenConnection(this.mDataFilePath);

            }
            else
            {
                mConnection = OpenConnection(this.mDataFilePath,mDbPathPassword);
            }
            Console.WriteLine("数据库打开成功！！");
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseSQLite()
        {
            if (this.mConnection != null)
            {
                try
                {
                    this.mConnection.Close();
                    if (rwl.ContainsKey(LockName))
                    {
                        rwl.Remove(LockName);
                    }
                }
                catch
                {
                    Console.WriteLine("关闭失败");
                }
            }
            Console.WriteLine("关闭数据库成功");
         }


        #endregion

        #region 事务

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTrain()
        {
            EnsureConnection();
            dbTransaction = mConnection.BeginTransaction();
        }
        /// <summary>
        ///  提交事务
        /// </summary>
        public  void CommitDB()
        {
            try
            {
                dbTransaction.Commit();
            }
            catch(Exception)
            {
                dbTransaction.Rollback();

            }
        }
        #endregion
        #region 工具
        private SQLiteConnection OpenConnection(string dataFile)
        {
            if(dataFile == null)
            {
                throw new ArgumentNullException("dataFiledatafile = null");

            }
            if (!File.Exists(dataFile))
            {
                SQLiteConnection .CreateFile(dataFile);
            }
            SQLiteConnection connection = new SQLiteConnection();
            SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder() { DataSource  = dataFile,};
            connection.ConnectionString  = connectionStringBuilder.ToString();
            connection.Open();
            return connection;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datafile"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private  SQLiteConnection OpenConnection(string datafile, string password)
        {
            if(datafile == null)
            {
                throw new ArgumentNullException("datafile is null");

            }
            if (!File.Exists(Convert.ToString(datafile)))
            {
                SQLiteConnection.CreateFile(datafile);  

            }
            try
            {
                SQLiteConnection connection = new SQLiteConnection();
                SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder()
                {
                    DataSource = datafile,
                    Password = password

                };
                connection.ConnectionString=connectionStringBuilder.ToString();
                connection.Open();
                return connection;
            }
            catch(Exception ex)
            {

                return null;
            }
        }
        /// <summary>
        ///  读取或者设置sqlitemananger 的数据库连接
        /// </summary>
        public  SQLiteConnection Connection
        {
            get { return mConnection; }
            set { mConnection = value??throw new ArgumentException(); }

        }
        /// <summary>
        /// // 保证数据库的连接状态
        /// </summary>

        public void EnsureConnection()
        {
            if(this.mConnection == null)
            {
                throw new Exception("数据库没有连接");

            }
            if(mConnection.State != System.Data.ConnectionState.Open)
            {
                mConnection.Open();
            }
        }
        /// <summary>
        /// 获取数据库的文件路径
        /// </summary>
        /// <returns></returns>
        public  string GetDatafile()
        {
            return this.mDataFilePath;
        }
       
        /// <summary>
        ///  表书否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public bool TableExit(string tableName)
        {
            if ( string.IsNullOrEmpty(tableName))
            {
                return false;
            }
            EnsureConnection();

            SQLiteDataReader dataReader = ExcuteReader("SELECT count(*) as c FROM sqlite_master WHERE type='table' AND name=@tableName ",
                new SQLiteParameter[] { new SQLiteParameter("tableName", tableName) });
            if(dataReader== null)
            {
                return false;
            }
            else
            {
                dataReader.Read();
                int s = dataReader.GetInt32(0);
                dataReader.Close();
                dataReader.Dispose();
                return s == 1;
            }

        }
        /// <summary>
        /// VACUUM 命令（通过复制主数据库中的内容到一个临时数据库文件，然后清空主数据库，并从副本中重新载入原始的数据库文件）
        /// </summary>
        /// <returns></returns>
        public bool Vacuum()
        {
            try
            {
                using (SQLiteCommand Command = new SQLiteCommand("VACUUM", Connection))
                {
                    Command.ExecuteNonQuery();
                }
                return true;
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        }
        #endregion
        #region 执行sql 语句
        /// <summary>
        /// 执行sql，并返回SqliteReader 对象
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public SQLiteDataReader ExcuteReader(string SQL, SQLiteParameter[] parameters)
        {
            if (string.IsNullOrEmpty(SQL))
            {
                throw new ArgumentException("SQL IS NULL");
            }
            EnsureConnection();
            using (rwl[LockName].Read())
            {
                using (SQLiteCommand  command = new SQLiteCommand(SQL, Connection))
                {
                    if(parameters != null)
                    {
                        command.Parameters.AddRange(parameters);

                    }
                    try
                    {
                        SQLiteDataReader dataReader = command.ExecuteReader();
                        command.Parameters.Clear();
                        return dataReader;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询，并返回dataset对象
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="paramArr">参数数组</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string sql, SQLiteParameter[] paramArr)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql=null");
            }
            this.EnsureConnection();
            using (rwl[LockName].Read())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, this.Connection))
                {
                    if (paramArr != null)
                    {
                        cmd.Parameters.AddRange(paramArr);
                    }
                    try
                    {
                        SQLiteDataAdapter da = new SQLiteDataAdapter();
                        DataSet ds = new DataSet();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        cmd.Parameters.Clear();
                        da.Dispose();
                        return ds;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// 执行SQL查询，并返回dataset对象。
        /// </summary>
        /// <param name="strTable">表的名称</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="paramArr">SQL参数数组</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string strTable, string sql, SQLiteParameter[] paramArr)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql=null");
            }
            this.EnsureConnection();
            using (rwl[LockName].Read())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, this.Connection))
                {
                    if (paramArr != null)
                    {
                        cmd.Parameters.AddRange(paramArr);
                    }
                    try
                    {
                        SQLiteDataAdapter da = new SQLiteDataAdapter();
                        DataSet ds = new DataSet();
                        da.SelectCommand = cmd;
                        da.Fill(ds, strTable);
                        cmd.Parameters.Clear();
                        da.Dispose();
                        return ds;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }/// <summary>
        /// 执行sql 返回受影响的函数，可用于执行表的创建语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public  int  ExcuteNonQuery(string sql , SQLiteParameter[] parameters)
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new ArgumentNullException("sql is null");

            }
            EnsureConnection();
            using (rwl[LockName].Read())
            {
                try
                {
                    using(SQLiteCommand command = new SQLiteCommand(sql, Connection))
                    {
                        if(parameters != null)
                        {
                            foreach(SQLiteParameter param in parameters)
                            {
                                command.Parameters.Add(param); 
                            }
                        }
                        int  C = command .ExecuteNonQuery();
                        command .Parameters.Clear();
                        return C;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }
        /// <summary>
         /// 执行SQL，返回结果集第一行，如果结果集为空，那么返回空 List(List.Count=0)， 
         /// </summary>
         /// <param name="SQL"></param>
         /// <param name="parameters"></param>
         /// <returns></returns>
         /// <exception cref="ArgumentException"></exception>
        public object ExecuteScalar(string SQL, SQLiteParameter[] parameters)
        {
            if (string.IsNullOrEmpty(SQL))
            {
                throw new ArgumentException("sql is null ");
            }
            this.EnsureConnection();
            using (rwl[LockName].Read())
            {
                using (SQLiteCommand command = new SQLiteCommand (SQL, Connection))
                {
                    if( parameters != null )
                    {
                        command.Parameters.AddRange(parameters);

                    }
                    try
                    {
                        Object reader  = command.ExecuteScalar();   
                        command .Parameters.Clear();
                        command.Dispose();
                        return reader;
                    }
                    catch(Exception)
                    {
                        return null;
                    }
                }
            }
        }
        /// <summary>
        ///  查询一行记录，无结果时返回为null
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="col"></param>
        /// <param name="conditionVal"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public object  QueryOne(string  tableName,string col,object conditionVal)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("tableName  is  null ");

            }EnsureConnection();
            string sql = "select * from " + tableName;
            if (col != null)
            {
                sql += "where" + col + "=@" + col;

            }
            object result = ExecuteScalar(sql, new SQLiteParameter[] { new SQLiteParameter(col, conditionVal) });
            return result;
        }
        #endregion
        #region 增删改 
        public int InsertData(string  TableName,Dictionary<string ,object > keys)
        {
            if (string.IsNullOrEmpty(TableName))
            {
                throw new ArgumentException("table is null");

            }EnsureConnection ();
            string sql = BuildInsert(TableName, keys);
            return ExcuteNonQuery(sql, BuildParamArray(keys));

        }
        /// <summary>
        ///  更新
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keys"></param>
        /// <param name="where"></param>
        /// <param name="whereParams"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public  int UpdataSqlite(string tableName ,Dictionary<string, object> keys, string where, SQLiteParameter[] whereParams)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException(" table is null ");

            }
            EnsureConnection();
            string sql = BuildUpDate(tableName, keys);
            SQLiteParameter[] parameters  = BuildParamArray(keys);
            if (!string.IsNullOrEmpty(where))
            {
                sql += "where" + where;
                if ( whereParams!=null )
                {
                    SQLiteParameter[] newArr = new SQLiteParameter[(parameters.Length + whereParams.Length)];
                    Array.Copy(whereParams, newArr, whereParams.Length);
                    Array.Copy(whereParams, 0, newArr, parameters.Length, whereParams.Length);
                    parameters = newArr;

                }
            }
            return ExcuteNonQuery (sql, parameters);

        }
        /// <summary>
        /// 将字典类型转成数据修改数据的sql 语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        private string BuildUpDate(string tableName, Dictionary<string, object> keys)
        {
             StringBuilder  buf  =  new StringBuilder();
            buf.Append("update").Append(tableName).Append("set");
            foreach( var key  in keys)
            {
                buf.Append(key).Append("=").Append("@").Append(key).Append(",");

            }
            buf.Remove(buf.Length - 1, 1);
            buf.Append("");
            return buf.ToString();
        }

        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="where"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public int Delete(string tableName, string sqlwhere, SQLiteParameter[] parameters)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("tablename is null");

            }
            EnsureConnection();
            string sql = "delete from" + tableName;
            if (!string.IsNullOrEmpty(sqlwhere))
            {
                sql+="where"+sqlwhere;
            }
            return ExcuteNonQuery(sql, parameters);

        }

         /// <summary>
         /// 
         /// </summary>
         /// <param name="keys"></param>
         /// <returns></returns>
        private SQLiteParameter[] BuildParamArray(Dictionary<string, object> keys)
        {
             List<SQLiteParameter> list = new List<SQLiteParameter>();
            foreach(string  key  in keys.Keys)
            {
                list.Add(new SQLiteParameter(key, keys[key]));  
            }if(list.Count == 0)
            {
                return null;
            }
            return list.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        private string BuildInsert(string tableName, Dictionary<string, object> keys)
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("insert into ").Append(tableName);
            buf.Append(" (");
            foreach (string key in keys.Keys)
            {
                buf.Append(key).Append(",");
            }
            buf.Remove(buf.Length - 1, 1); // 移除最后一个,
            buf.Append(") ");
            buf.Append("values(");
            foreach (string key in keys.Keys)
            {
                buf.Append("@").Append(key).Append(","); // 创建一个参数
            }
            buf.Remove(buf.Length - 1, 1);
            buf.Append(") ");

            return buf.ToString();

        }
        #endregion
    }
}
 

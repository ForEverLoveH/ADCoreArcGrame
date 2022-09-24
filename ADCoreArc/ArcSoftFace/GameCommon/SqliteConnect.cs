using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.GameCommon
{
    public class SqliteConnect : IDisposable
    {
        protected SQLiteConnection sqliteConnect;
        public SqliteConnect(string Path)
        {
            if (!System.IO.File.Exists(Path))
            {
                CreatDbSqlite(Path);

            }
            ConnectDbSqlite(Path);
        }

        private bool ConnectDbSqlite(string path)
        {
            try
            {
                sqliteConnect = new SQLiteConnection(new SQLiteConnectionStringBuilder() { DataSource = path }.ToString());
                sqliteConnect.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("数据库链接异常：" + e.Message);
                return false;
            }
        }

        private bool CreatDbSqlite(string path)
        {
            try
            {
                string dirName = new FileInfo(path).Directory.FullName;
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
                SQLiteConnection.CreateFile(path);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("数据库创建异常：" + e.Message);
                return false;
            }
        }

        public void Dispose()
        {
            sqliteConnect?.Close();
            sqliteConnect?.Dispose();
        }
    }
}

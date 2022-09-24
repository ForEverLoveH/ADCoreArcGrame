using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.GameNet;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ArcSoftFace.GameCommon
{
    public  class Personnel_Import_Sql
    {
        public static Personnel_Import_Sql Instance;

        public void Awake()
        {
            Instance = this;
        }
        /// <summary>
        ///  获取数据量
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetKey(string name, string key)
        {
            IsExistenceUserExcelData(name);
            string dbPath = Application.StartupPath + GameConst.SaveDBPath;
            SqlDbCommand sql = new SqlDbCommand(dbPath);
            object number = sql.GetKey(name, key);
            sql.Dispose();
            return number.ToString();
        }
        bool is_NewImport = false;

        public void New_Import(string name, List<UserExcel> listUserExcel)
        {
            IsExistenceUserExcelData(name);
            string path = Application.StartupPath + GameConst.SaveDBPath;
            SqlDbCommand sql = new SqlDbCommand(path);
            sql.DeleteTable(name);
            sql.CreateTable<UserExcelMode>(name);

            GameMsg msgs = new GameMsg()
            {
                cmd = CMD.Rsp_New_Import,

            };
            if (listUserExcel.Count > 0)
            {
                try
                {
                    sql.Insert<UserExcel>(listUserExcel, name);
                    sql.Dispose();
                    is_NewImport = true;
                }
                catch (Exception e)
                {
                    is_NewImport = false;
                    Console.WriteLine("全新导入出错" + e.Message);
                }
                if (is_NewImport)
                {
                    msgs.rsp_New_Import = new Rsp_New_Import()
                    {
                        IsReq_New_Import = 0,
                    };
                }
                else
                {
                    msgs.rsp_New_Import = new Rsp_New_Import()
                    {
                        IsReq_New_Import = -1,
                    };

                }
            }
            else
            {
                msgs.errorType = ErrorType.New_Import_IsEmpty;
            }
            LocalNetServer localNetServer = new LocalNetServer();
            localNetServer.SendMsg(msgs);


        }

        /// <summary>
        ///  是否存在数据表{name}
        /// </summary>
        /// <param name="name"></param>

        private void IsExistenceUserExcelData(string name)
        {
            try
            {
                string dbPath = Application.StartupPath + GameConst.SaveDBPath;
                SqlDbCommand sql = new SqlDbCommand(dbPath);
                if (sql.IsCreateTable(name) == 1)
                {
                    Console.WriteLine($"数据表{name}已存在！！");
                }
                if (sql.IsCreateTable(name) == 0)
                {
                    Console.WriteLine($"数据库表{name}不存在");
                    sql.CreateTable<UserExcelMode>(name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"数据表{name}查询异常" + e.Message);
            }
        }
        private bool Is_add_Import = false;
        public void Add_Import(string name, List<UserExcel> userExcels)
        {
            IsExistenceUserExcelData(name);
            string path = Application.StartupPath + GameConst.SaveDBPath;
            SqlDbCommand sql = new SqlDbCommand(path);
            GameMsg games = new GameMsg()
            {
                cmd = CMD.Rsp_Add_Import,
            }; if (userExcels.Count > 0)
            {
                try
                {
                    sql.Insert<UserExcel>(userExcels, name);
                    sql.Dispose();
                    Is_add_Import = true;

                }
                catch (Exception e)
                {
                    Console.WriteLine($"新增导入错误！！" + e.Message);
                    Is_add_Import = false;
                }
                if (Is_add_Import)
                {
                    games.rsp_Add_Import = new Rsp_Add_Import()
                    {
                        IsReq_Add_Import = 0,
                    };
                }
                else
                {
                    games.rsp_Add_Import = new Rsp_Add_Import()
                    {
                        IsReq_Add_Import = -1,
                    };
                }
            }
            else
            {
                games.errorType = ErrorType.Add_Import_IsEmptyl;

            }
            LocalNetServer localNetServer = new LocalNetServer();
            localNetServer.SendMsg(games);

        }
    }
}

using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.GameNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.GameCommon
{
    public  class SqliteDB
    {
        public static SqliteDB Instance;
        LocalNetServer locaNetServer = new LocalNetServer();

        public void Awake()
        {
            Instance = this;
        }
        private string DBpath = Application.StartupPath + GameConst.DBPath;


        /// <summary>
        ///  初始化
        /// </summary>
        public void Init()
        {
            if (!System.IO.Directory.Exists(DBpath))
            {
                System.IO.Directory.CreateDirectory(DBpath);
                Console.WriteLine($"数据库文件夹已丢失，已重新创建dataBase文件夹的路径为{DBpath}");
            }
            string dbPath = Application.StartupPath + GameConst.UserDBPath;
            if (File.Exists(dbPath))
            {
                Console.WriteLine("SaveData文件存在");
            }
            else
            {
                SqlDbCommand sql = new SqlDbCommand(dbPath);
                sql.Dispose();
            }
            string UserdbPath = Application.StartupPath + GameConst.SaveDBPath;
            if (File.Exists(UserdbPath))
            {
                Console.WriteLine($" {UserdbPath}文件存在");
            }
            else
            {
                Console.WriteLine($" {UserdbPath}文件不存在");
                SqlDbCommand sql = new SqlDbCommand(UserdbPath);
                sql.Dispose();
            }
            string FacedbPath = Application .StartupPath + GameConst.FaceDBPath;
            if (File.Exists(FacedbPath))
            {
                Console.WriteLine($" {FacedbPath}文件存在");
            }
            else
            {
                Console.WriteLine($" {FacedbPath}文件不存在");
                SqlDbCommand sql = new SqlDbCommand(FacedbPath);
                sql.Dispose();

            }
            Export_database(dbPath, "Init");
        }
        private void Export_database(string ExportPath, string Cmd, bool IsExport = GameConst.IsExport)
        {
            if (IsExport)
            {
                try
                {
                    var dirName = new FileInfo(ExportPath).Directory.FullName;
                    dirName += "/ExportDatabase/";
                    if (!Directory.Exists(dirName))
                    {
                        Directory.CreateDirectory(dirName);
                    }
                    File.Copy(ExportPath, dirName + $"{DateTime.Now.ToString("yyyyMMdd-HH-mm-ss-ms")}_{Cmd}_backups.db", true);//拷贝文件 可以覆盖
                    if (File.Exists(dirName + $"{DateTime.Now.ToString("yyyyMMdd-HH-mm-ss-ms")}_{Cmd}_backups.db"))
                    {
                        Console.WriteLine("数据库文件备份成功,文件路径： " + dirName + $"{DateTime.Now.ToString("yyyyMMdd-HH-mm-ss-ms")}_{Cmd}_backups.db");
                    }

                }
                catch (System.Exception e)
                {
                    Console.WriteLine($"备份数据库异常：{e.Message}");
                }
            }

        }

        /// <summary>
        /// 登录处理
        /// </summary>
        /// <param name="msg"></param>
        public void ReqLogin(GameMsg msg)
        {
            GameMsg LoginMsg = new GameMsg
            {
                cmd = CMD.RspLogin,
            };
            if (string.IsNullOrEmpty(msg.reqLogin.acc) || string.IsNullOrEmpty(msg.reqLogin.password))
            {
                LoginMsg.errorType = ErrorType.LoginAccountPasswordIsOnline;
            }
            else if (!string.IsNullOrEmpty(msg.reqLogin.acc) && !string.IsNullOrEmpty(msg.reqLogin.password))
            {
                LoginMsg.errorType = ErrorType.None;
                LoginSql.Instance.IsExtenceAdminData(GameConst.DBLoginName);
                if (LoginSql.Instance.IsLogin(GameConst.DBLoginName, msg.reqLogin.acc, msg.reqLogin.password))
                {
                    //登录成功
                    PlayerData playerData = new PlayerData() { isLoginSucess = 0 };
                    LoginMsg.rspLogin = new RspLogin()
                    {
                        playerData = playerData
                    };
                }
                else
                {
                    //登录失败
                    PlayerData playerData = new PlayerData() { isLoginSucess = -1 };
                    LoginMsg.rspLogin = new RspLogin
                    {
                        playerData = playerData
                    };
                }
            }
            locaNetServer.SendMsg(LoginMsg);

        }
        /// <summary>
        /// 获取数据量
        /// </summary>
        /// <param name="msg"></param>
        public void Req_GetKeyByPersonImport(GameMsg msg)
        {
            GameMsg Rsp_GetKey_Msg = new GameMsg
            {
                cmd = CMD.Rsp_GetKeyByPersonImport,
            };
            Rsp_GetKey_Msg.rsp_GetKey = new Rsp_GetKeyByPersonImport
            {
                number = Personnel_Import_Sql.Instance.GetKey(GameConst.DBUserExcel, "Id")
            };
            locaNetServer.SendMsg(Rsp_GetKey_Msg);
        }
        /// <summary>
        /// 验证管理员密码
        /// </summary>
        /// <param name="msg"></param>
        public void Req_Verification(GameMsg msg)
        {
            GameMsg Rsp_VerificationMsg = new GameMsg
            {
                cmd = CMD.Rsp_Verification,
            };
            if (string.IsNullOrEmpty(msg.req_Verification.pass))
            {
                Rsp_VerificationMsg.errorType = ErrorType.Verification_PasswordIsEmpty;
            }
            else
            {
                LoginSql.Instance.IsExtenceAdminData(GameConst.DBLoginName);
                if (LoginSql.Instance.IsLogin(GameConst.DBLoginName, GameConst.InitAdminAcct, msg.req_Verification.pass))
                {
                    //登录成功
                    Rsp_VerificationMsg.rsp_Verfication = new Rsp_Verfication
                    {
                        IsVerfication = 0
                    };
                }
                else
                {
                    //登录失败
                    Rsp_VerificationMsg.rsp_Verfication = new Rsp_Verfication
                    {
                        IsVerfication = -1
                    };
                }
            }

            locaNetServer.SendMsg(Rsp_VerificationMsg);
        }

        public void Req_AdminChangeAdminPassword(GameMsg msg)
        {
            LoginSettingSql.Instance.Init();
            LoginSettingSql.Instance.Req_AdminChangeAdminPassword(msg);
        }

        public void Req_AdminChangeUserPassword(GameMsg msg)
        {
            LoginSettingSql.Instance.Init();
            LoginSettingSql.Instance.Req_AdminChangeUserPassword(msg);
        }

        public void Req_UserChangeUserPassword(GameMsg msg)
        {
            LoginSettingSql.Instance.Init();
            LoginSettingSql.Instance.Req_UserChangeUserPassword(msg);
        }

        public void New_Import(GameMsg msg)
        {
            string dbPath = Application.StartupPath + GameConst.SaveDBPath;
            Export_database(dbPath, "New_Import");
            Personnel_Import_Sql.Instance.New_Import(GameConst.DBUserExcel, msg.req_New_Import.ListUserExcel);
        }

        public void Add_Import(GameMsg msg)
        {
            string dbPath = Application.StartupPath + GameConst.SaveDBPath;
            Export_database(dbPath, "New_Import");
            Personnel_Import_Sql.Instance.Add_Import(GameConst.DBUserExcel, msg.req_Add_Import.userExcels);
        }

        public void Req_ExportGrade(GameMsg msg)
        {
            ExportGradeSql.Instance.GetUserExcelData(GameConst.DBUserExcel);
        }

        public void Req_NewGroupGetGroup(GameMsg msg)
        {
            NewGroupSql.Instance.Req_NewGroupGetGroup(msg);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Req_QueryExaminationTime(GameMsg msg)
        {
            StartTestingSql.Instance.Req_QueryExaminationTime(msg);
        }
        /// <summary>
        /// 获取组信息
        /// </summary>
        /// <param name="msg"></param>
        public void Req_GetGroupMent(GameMsg msg)
        {
            StartTestingSql.Instance.Req_GetGroupMent(msg);
        }

        public void Req_TestNumberInquriry(GameMsg msg)
        {
            StartTestingSql.Instance.Req_TestNumberInquriry(msg);
        }

        public void Req_NewGroupUpdateUserExcel(GameMsg msg)
        {
            NewGroupSql.Instance.Req_NewGroupUpdateUserExcel(msg);
        }

        public void Req_GetCurrentGroupMsg(GameMsg msg)
        {
            StartTestingSql.Instance.Req_GetCurrentGroupMsg(msg);
        }

        public void ReqModify_Grades(GameMsg msg)
        {
            StartTestingSql.Instance.ReqModify_Grades(msg);
        }

        public  void Req_GetKeyByExportGrade(GameMsg msg)
        {
            GameMsg Rsp_GetKey_Msg = new GameMsg
            {
                cmd = CMD.Rsp_GetKeyByExportGrade,
            };
            Rsp_GetKey_Msg.rsp_GetKey = new Rsp_GetKeyByPersonImport
            {
                number = Personnel_Import_Sql.Instance.GetKey(GameConst.DBUserExcel, "Id")
            };
            locaNetServer.SendMsg(Rsp_GetKey_Msg);
        }

        public  void Req_GetFaceFeature(GameMsg msg)
        {
            StartTestingSql.Instance.Req_GetFaceFeature(msg);
        }
        /// <summary>
        ///  人脸注册
        /// </summary>
        /// <param name="msg"></param>
        public  void Req_NewGroupFaceRegister(GameMsg msg)
        {
            NewGroupSql.Instance.Req_NewGroupFaceRegister(msg);
        }

        public  void Req_DelectFaceData(GameMsg msg)
        {
            NewGroupSql.Instance.Req_DelectFaceData(msg);
        }

        public  void Req_NewGroupUpdateUserExcelByFile(GameMsg msg)
        {
            NewGroupSql.Instance.Req_NewGroupUpdateUserExcelByFile(msg);
        }
    }
}


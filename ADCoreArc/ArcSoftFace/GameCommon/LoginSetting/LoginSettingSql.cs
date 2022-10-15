using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.GameNet;
using System;
using System.Windows.Forms;

namespace ArcSoftFace.GameCommon
{
    public  class LoginSettingSql
    {
        public static LoginSettingSql Instance;
        public LoginSql loginSql;
        public LocalNetServer localNetServer;
        public void Awake()
        {
            Instance = this;
        }
        public void Init()
        {
            loginSql = LoginSql.Instance;
            localNetServer = LocalNetServer.Instance;
        }
        /// <summary>
        /// 管理员更改自己密码
        /// </summary>
        /// <param name="msg"></param>
        public void Req_AdminChangeAdminPassword(GameMsg msg)
        {
            GameMsg Rsp_Admin_admin_Change_Password_Msg = new GameMsg
            {
                cmd = CMD.Rsp_AdminChangeAdminPassword
            };
            if (string.IsNullOrEmpty(msg. req_AdminChangeAdminPassword.account) != true)
            {
                if (string.IsNullOrEmpty(msg.req_AdminChangeAdminPassword.oldPassword ) != true)
                {
                    if (string.IsNullOrEmpty(msg.req_AdminChangeAdminPassword.newPassword ) != true)
                    {
                        if (msg.req_AdminChangeAdminPassword.oldPassword  != msg.req_AdminChangeAdminPassword.newPassword )
                        {
                            loginSql. IsExtenceAdminData( GameConst.DBLoginName);
                            if (loginSql.IsLogin( GameConst .DBLoginName, msg.req_AdminChangeAdminPassword.account, msg.req_AdminChangeAdminPassword.oldPassword ))
                            {
                                Admin admin = new Admin
                                {
                                    User =  GameConst .InitAdminAcct,
                                    Password = msg.req_AdminChangeAdminPassword.newPassword 
                                };
                                if (SetLoginData(admin, GameConst.DBLoginName, $"User = '{admin.User}' and Password = {msg.req_AdminChangeAdminPassword.newPassword }") != -1)
                                {
                                    Rsp_Admin_admin_Change_Password_Msg. rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword
                                    {
                                        //修改成功
                                        IsSucess = 0,
                                    };
                                }
                                else
                                {
                                    Rsp_Admin_admin_Change_Password_Msg.rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword
                                    {
                                        //修改失败
                                        IsSucess = -6
                                    };
                                }
                            }
                            else
                            {
                                Rsp_Admin_admin_Change_Password_Msg.rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword
                                {
                                    //密码错误
                                    IsSucess = -5
                                };
                            }
                        }
                        else
                        {
                            Rsp_Admin_admin_Change_Password_Msg.rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword
                            {
                                //密码与新密码一致
                                IsSucess  = -4
                            };
                        }

                    }
                    else
                    {
                        Rsp_Admin_admin_Change_Password_Msg.rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword
                        {
                            //新密码为空
                            IsSucess  = -3
                        };
                    }
                }
                else
                {
                    Rsp_Admin_admin_Change_Password_Msg.rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword
                    {
                        //密码为空
                        IsSucess = -2
                    };
                }
            }
            else
            {
                Rsp_Admin_admin_Change_Password_Msg.rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword
                {
                    //账号为空
                    IsSucess  = -1
                };
            }
            localNetServer.SendMsg(Rsp_Admin_admin_Change_Password_Msg);
        }
        /// <summary>
        /// 管理员更改裁判员账号密码
        /// </summary>
        /// <param name="msg"></param>
        public void Req_AdminChangeUserPassword(GameMsg msg)
        {
            GameMsg Rsp_Admin_user_Change_Password_Msg = new GameMsg
            {
                cmd = CMD.Rsp_AdminChangeUserPassword,
            };
            if (string.IsNullOrEmpty(msg.req_AdminChangeUserPassword.account) != true)
            {
                if (string.IsNullOrEmpty(msg.req_AdminChangeUserPassword.oldPassword ) != true)
                {
                    if (string.IsNullOrEmpty(msg.req_AdminChangeUserPassword.newPassword ) != true)
                    {
                        loginSql.IsExtenceAdminData( GameConst.DBLoginName);
                        if (loginSql.IsLogin( GameConst.DBLoginName, msg.req_AdminChangeUserPassword.account , msg.req_AdminChangeUserPassword.oldPassword ))
                        {
                            Admin admin = new Admin
                            {
                                User =GameConst .InitUserAcct,
                                Password = msg.req_AdminChangeUserPassword.newPassword 
                            };
                            if (SetLoginData(admin, GameConst .DBLoginName, $"User = '{admin.User}'") != -1)
                            {
                                Rsp_Admin_user_Change_Password_Msg.rsp_AdminChangeUserPassword = new Rsp_AdminChangeUserPassword
                                {
                                    //修改成功
                                     IsSucess  = 0,
                                };
                            }
                            else
                            {
                                Rsp_Admin_user_Change_Password_Msg.rsp_AdminChangeUserPassword = new Rsp_AdminChangeUserPassword
                                {
                
                                    //修改失败
                                    IsSucess   = -5
                                };
                            }
                        }
                        else
                        {
                            Rsp_Admin_user_Change_Password_Msg.rsp_AdminChangeUserPassword = new Rsp_AdminChangeUserPassword
                            {

                                //修改失败
                                IsSucess = -4
                            };
                        }
                    }
                    else
                    {
                        Rsp_Admin_user_Change_Password_Msg.rsp_AdminChangeUserPassword = new Rsp_AdminChangeUserPassword
                        {

                            //修改失败
                            IsSucess = -3
                        };
                    }
                }
                else
                {
                    Rsp_Admin_user_Change_Password_Msg.rsp_AdminChangeUserPassword = new Rsp_AdminChangeUserPassword
                    {

                        //修改失败
                        IsSucess = -2
                    };
                }
            }
            else
            {
                Rsp_Admin_user_Change_Password_Msg.rsp_AdminChangeUserPassword = new Rsp_AdminChangeUserPassword
                {

                    //修改失败
                    IsSucess = -1
                };
            }
            localNetServer.SendMsg(Rsp_Admin_user_Change_Password_Msg);
        }
        /// <summary>
        ///  裁判员更改自己账号
        /// </summary>
        /// <param name="msg"></param>
        public void Req_UserChangeUserPassword(GameMsg msg)
        {
            GameMsg Rsp_User_Change_Password_Msg = new GameMsg
            {
                cmd = CMD.Rsp_UserChangeUserPassword,
            };
            if (string.IsNullOrEmpty(msg. req_UserChangeUserPassword.account ) != true)
            {
                if (string.IsNullOrEmpty(msg.req_UserChangeUserPassword.oldPassword) != true)
                {
                    if (string.IsNullOrEmpty(msg.req_UserChangeUserPassword.newPassword ) != true)
                    {
                        if (msg.req_UserChangeUserPassword.oldPassword  != msg.req_UserChangeUserPassword.newPassword )
                        {
                            loginSql. IsExtenceAdminData ( GameConst.DBLoginName);
                            if (loginSql.IsLogin( GameConst.DBLoginName, msg.req_UserChangeUserPassword.account , msg.req_UserChangeUserPassword.newPassword ))
                            {
                                Admin admin = new Admin
                                {
                                    User = GameConst.InitUserAcct,
                                    Password = msg.req_UserChangeUserPassword.newPassword
                                };
                                if (SetLoginData(admin, GameConst.DBLoginName, $"User = '{admin.User}' and Password = {msg.req_UserChangeUserPassword.newPassword }") != -1)
                                {
                                    Rsp_User_Change_Password_Msg.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword
                                    {
                                        //修改成功
                                         IsSucess  = 0
                                    };
                                }
                                else
                                {
                                    Rsp_User_Change_Password_Msg.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword
                                    {
                                        //修改失败
                                        IsSucess = -6
                                    };
                                }
                            }
                            else
                            {
                                Rsp_User_Change_Password_Msg.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword
                                {
                                    //修改成功
                                    IsSucess = -5
                                };
                            }
                        }
                        else
                        {
                            Rsp_User_Change_Password_Msg.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword
                            {
                                //修改成功
                                IsSucess = -4
                            };
                        }
                    }
                    else
                    {
                        Rsp_User_Change_Password_Msg.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword
                        {
                            //修改成功
                            IsSucess = -3
                        };
                    }
                }
                else
                {
                    Rsp_User_Change_Password_Msg.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword
                    {
                        //修改成功
                        IsSucess = -2
                    };
                }
            }
            else
            {
                Rsp_User_Change_Password_Msg.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword
                {
                    //修改成功
                    IsSucess = -1
                };
            }
           localNetServer.SendMsg(Rsp_User_Change_Password_Msg);
        }
        /// <summary>
        /// 设置登录信息
        /// </summary>
        /// <param name="admin"></param>
        /// <param name="name"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private int SetLoginData(Admin admin, string name, string sqlWhere)
        {
            string path = Application.StartupPath + GameConst.UserDBPath;
            SqlDbCommand sql = new SqlDbCommand(path);
            return sql.Updete<Admin>(admin, name, sqlWhere);
        }


    }
}
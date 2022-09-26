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
            GameMsg msgs = new GameMsg()
            {
                cmd = CMD.Rsp_AdminChangeAdminPassword,
            };
            var account = msg.req_AdminChangeAdminPassword.account;
            var oldPassword = msg.req_AdminChangeAdminPassword.oldPassword;
            var newPassword = msg.req_AdminChangeAdminPassword.newPassword;
            if (!string.IsNullOrEmpty(account))
            {
                if (!string.IsNullOrEmpty(oldPassword))
                {
                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        if (!string.IsNullOrEmpty(newPassword))
                        {
                            if (oldPassword != newPassword)
                            {
                                LoginSql.Instance.IsExtenceAdminData(GameConst.DBLoginName);
                                if (LoginSql.Instance.IsLogin(GameConst.DBLoginName, msg.req_AdminChangeAdminPassword.oldPassword, msg.req_AdminChangeAdminPassword.newPassword))
                                {
                                    Admin admin = new Admin()
                                    {
                                        User = GameConst.InitAdminAcct,
                                        Password = msg.req_AdminChangeAdminPassword.newPassword,
                                    };
                                    if (SetLoginData(admin, GameConst.DBLoginName, $"User  = '{admin.User}' and Password ='{msg.req_AdminChangeAdminPassword.newPassword}'") != -1)
                                    {
                                        msgs.rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword()
                                        {
                                            IsSucess = 0,
                                        };
                                    }
                                    else
                                    {
                                        msgs.rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword()
                                        {
                                            IsSucess = -6, // 修改失败
                                        };
                                    }
                                }
                                else
                                {
                                    msgs.rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword()
                                    {
                                        IsSucess = -5, // 修改失败,密码失败
                                    };
                                }
                            }
                            else
                            {
                                msgs.rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword()
                                {
                                    IsSucess = -4, // 修改失败,新旧密码一致
                                };
                            }
                        }
                        else
                        {
                            msgs.rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword()
                            {
                                IsSucess = -3, // 修改失败,旧密码为空 
                            };
                        }
                    }
                    else
                    {
                        msgs.rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword()
                        {
                            IsSucess = -2,
                        };
                    }
                }
                else
                {
                    msgs.rsp_AdminChangeAdminPassword = new Rsp_AdminChangeAdminPassword()
                    {
                        IsSucess = -1,
                    };
                }
            }
            localNetServer.SendMsg(msgs);   
        }
        /// <summary>
        /// 管理员更改裁判员账号密码
        /// </summary>
        /// <param name="msg"></param>
        public void Req_AdminChangeUserPassword(GameMsg msg)
        {
            GameMsg gameMsg = new GameMsg()
            {
                cmd = CMD.Rsp_AdminChangeUserPassword,
            };
            var oldPassword = msg.req_AdminChangeUserPassword.oldPassword.Trim();
            var newPassword = msg.req_AdminChangeUserPassword.newPassword.Trim();
            var account = msg.req_AdminChangeUserPassword.account.Trim();
            if (!string.IsNullOrEmpty(oldPassword))
            {
                if (!string.IsNullOrEmpty(newPassword))
                {
                    if (oldPassword != newPassword)
                    {
                        LoginSql.Instance.IsExtenceAdminData(GameConst.DBLoginName);
                        if (LoginSql.Instance.IsLogin(GameConst.DBLoginName, msg.req_AdminChangeUserPassword.account, msg.req_AdminChangeUserPassword.oldPassword))
                        {
                            Admin admin = new Admin()
                            {
                                User = GameConst.InitAdminAcct,
                                Password = msg.req_AdminChangeUserPassword.newPassword,
                            };
                            var s = SetLoginData(admin, GameConst.DBLoginName, $"User='{admin.User}' and  Password = '{admin.Password}'");
                            if ( s != -1)
                            {
                                gameMsg.rsp_AdminChangeUserPassword = new Rsp_AdminChangeUserPassword()
                                {
                                    IsSucess = 0,
                                };
                            }
                            else
                            {
                                gameMsg.rsp_AdminChangeUserPassword = new Rsp_AdminChangeUserPassword()
                                {
                                    IsSucess = -6,
                                };
                            }

                        }
                        else
                        {
                            gameMsg.rsp_AdminChangeUserPassword = new Rsp_AdminChangeUserPassword()
                            {
                                IsSucess = -5,
                            };
                        }
                    }
                    else
                    {
                        gameMsg.rsp_AdminChangeUserPassword = new Rsp_AdminChangeUserPassword()
                        {
                            IsSucess = -4,
                        };
                    }
                }
                else
                {
                    gameMsg.rsp_AdminChangeUserPassword = new Rsp_AdminChangeUserPassword()
                    {
                        IsSucess = -3,
                    };
                }
            }
            else
            {
                gameMsg.rsp_AdminChangeUserPassword = new Rsp_AdminChangeUserPassword()
                {
                    IsSucess = -2,
                };
            }
            localNetServer.SendMsg(gameMsg);
        }
        /// <summary>
        ///  裁判员更改自己账号
        /// </summary>
        /// <param name="msg"></param>
        public void Req_UserChangeUserPassword(GameMsg msg)
        {
            GameMsg gameMsgs = new GameMsg()
            {
                cmd = CMD.Rsp_UserChangeUserPassword,
            };
            string account = msg.req_UserChangeUserPassword.account.Trim();
            string oldPassword = msg.req_UserChangeUserPassword.oldPassword.Trim();
            string newPassword = msg.req_UserChangeUserPassword.newPassword.Trim();
            if (!string.IsNullOrEmpty(account))
            {
                if (!string.IsNullOrEmpty(oldPassword))
                {
                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        if (oldPassword != newPassword)
                        {
                            LoginSql.Instance.IsExtenceAdminData(GameConst.DBLoginName);
                            if (LoginSql.Instance.IsLogin(GameConst.DBLoginName, msg.req_UserChangeUserPassword.account, msg.req_UserChangeUserPassword.oldPassword))
                            {
                                Admin admin = new Admin
                                {
                                    User = GameConst.InitAdminAcct,
                                    Password = msg.req_UserChangeUserPassword.newPassword,
                                };
                                var s  = SetLoginData(admin, GameConst.DBLoginName, $"User='{admin.User}' and Password  = '{admin.Password}'");
                                if ( s != -1)
                                {
                                    gameMsgs.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword()
                                    {
                                        IsSucess = 0,
                                    };
                                }
                                else
                                {
                                    gameMsgs.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword()
                                    {
                                        IsSucess = -6,
                                    };
                                }
                            }
                            else
                            {
                                gameMsgs.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword()
                                {
                                    IsSucess = -5,
                                };
                            }
                        }
                        else
                        {
                            gameMsgs.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword()
                            {
                                IsSucess = -4,
                            };
                        }
                    }
                    else
                    {
                        gameMsgs.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword()
                        {
                            IsSucess = -3,
                        };
                    }
                }
                else
                {
                    gameMsgs.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword()
                    {
                        IsSucess = -2,
                    };
                }
            }
            else
            {
                gameMsgs.rsp_UserChangeUserPassword = new Rsp_UserChangeUserPassword()
                {
                    IsSucess = -1,
                };
            }
            localNetServer.SendMsg(gameMsgs);
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
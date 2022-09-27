using ArcSoftFace.ADCoreSystem.ADCoreGameWindow;
using ArcSoftFace.GameNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem
{
    public class LoginSettingOfAdminSys
    {
        public static LoginSettingOfAdminSys Instance;
        LoginSettingOfAdminWindow loginSettingOfAdminWindow;

        LocalNetClient localNetClient = new LocalNetClient();
        public  void Awake()
        {
            Instance = this;
        }

        public void StartGame(bool IsActive = true)
        {
            if (IsActive)
            {
                if (loginSettingOfAdminWindow == null)
                {
                    loginSettingOfAdminWindow = new LoginSettingOfAdminWindow();
                    loginSettingOfAdminWindow.Show();
                }
                else
                {
                    if (loginSettingOfAdminWindow.IsDisposed)
                    {
                        loginSettingOfAdminWindow = new LoginSettingOfAdminWindow();
                        loginSettingOfAdminWindow.Show();
                    }
                    else
                    {
                        loginSettingOfAdminWindow.Activate();
                    }
                }
            }
            else
            {
                if (loginSettingOfAdminWindow != null)
                {
                    loginSettingOfAdminWindow.Dispose();
                }
            }
        }

         public  void Init()
        {
            StartGame();
        }

        public void Req_AdminChangeAdminPassword(string user, string oldPassword, string newPassword)
        {
            GameMsg game = new GameMsg()
            {
                cmd = CMD.Req_AdminChangeAdminPassword,
                req_AdminChangeAdminPassword = new Req_AdminChangeAdminPassword()
                {
                    account = user,
                    oldPassword = oldPassword,
                    newPassword = newPassword,
                }
            };
            localNetClient.SendMsg(game);   
        }

        public  void Req_AdminChangeUserPassword(string user, string oldPassword, string newPassword)
        {
            GameMsg gameMsg = new GameMsg()
            {
                cmd = CMD.Req_AdminChangeUserPassword,
                req_AdminChangeUserPassword = new Req_AdminChangeUserPassword()
                {
                    oldPassword = oldPassword,
                    newPassword = newPassword
                },
            };
            localNetClient.SendMsg(gameMsg);
        }

        public  void Rsp_AdminChangeUserPassword(GameMsg msg)
        {
            if (msg.rsp_AdminChangeUserPassword.IsSucess == -1)
            {
                MessageBox.Show("管理员登录账号为空！！");
                return;

            }
            if (msg.rsp_AdminChangeUserPassword.IsSucess == 0)
            {
                MessageBox.Show("密码修改成功！！");
                return;
            }
            if (msg.rsp_AdminChangeUserPassword.IsSucess == -2)
            {
                MessageBox.Show("管理员登录密码为空！！");
                return;
            }
            if (msg.rsp_AdminChangeUserPassword.IsSucess == -3)
            {
                MessageBox.Show(" 裁判员登录密码为空！！");
                return;
            }
            if (msg.rsp_AdminChangeUserPassword.IsSucess == -4)
            {
                MessageBox.Show("管理员密码错误！！");
                return ;
            }
            if (msg.rsp_AdminChangeUserPassword.IsSucess == -5)
            {
                MessageBox.Show("裁判员密码修改失败！！");
                return;
            }
        }

        public  void Rsp_AdminChangeAdminPassword(GameMsg msg)
        {
            if (msg.rsp_AdminChangeAdminPassword.IsSucess == -1)
            {
                MessageBox.Show("管理员登录账号为空！！");
                return;

            }
            if (msg.rsp_AdminChangeAdminPassword.IsSucess == 0)
            {
                MessageBox.Show(",密码修改成功！！");
                
            }
            if (msg.rsp_AdminChangeAdminPassword.IsSucess == -2)
            {
                MessageBox.Show("管理员登录密码为空！！");
                return;
            }
            if (msg.rsp_AdminChangeAdminPassword.IsSucess == -3)
            {
                MessageBox.Show("管理员新密码为空！！");
                return ;
            }
            if (msg.rsp_AdminChangeAdminPassword.IsSucess == -4)
            {
                MessageBox.Show("新旧密码应该不一致！！");
                return;
            }
            if (msg.rsp_AdminChangeAdminPassword.IsSucess == -5)
            {
                MessageBox.Show("管理员密码错误！！");
                return;
            }
            if (msg.rsp_AdminChangeAdminPassword.IsSucess == -6)
            {
                MessageBox.Show("管理员密码修改失败！！");
                return;
            }
        }
    }
}

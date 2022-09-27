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
    public class LoginSettingOfUserSys
    {
        public  static LoginSettingOfUserSys Instance;
        public  static LoginSettingOfUserWindow LoginSettingOfUserWindow;
        public  void Awake()
        {
            Instance = this;    
        }
        public  void Init()
        {
            StartGame();
        }
        LocalNetClient localNetClient = new LocalNetClient();
        public void Req_UserChangeUserPassword(string acc, string oldPassword, string newPassword)
        {
            GameMsg gameMsg = new GameMsg()
            {
                cmd = CMD.Req_UserChangeUserPassword,
                req_UserChangeUserPassword = new Req_UserChangeUserPassword()
                {
                    oldPassword = oldPassword,
                    newPassword = newPassword,
                    account = acc,


                }
            };
            localNetClient.SendMsg(gameMsg);
        }

        private void StartGame(bool IsActive=true   )
        {
            if (IsActive)
            {
                if (LoginSettingOfUserWindow == null)
                {
                    LoginSettingOfUserWindow = new LoginSettingOfUserWindow();
                    LoginSettingOfUserWindow.Show();
                }
                else
                {
                    if (LoginSettingOfUserWindow.IsDisposed)
                    {
                        LoginSettingOfUserWindow = new LoginSettingOfUserWindow();
                        LoginSettingOfUserWindow.Show();
                    }
                    else
                    {
                        LoginSettingOfUserWindow.Activate();
                    }
                }
            }
            else
            {
                if (LoginSettingOfUserWindow != null)
                {
                    LoginSettingOfUserWindow.Dispose();
                }
            }
        }

        public  void Rsp_UserChangeUserPassword(GameMsg gameMsg)
        {
            if (gameMsg.rsp_UserChangeUserPassword.IsSucess == 0)
            {
                MessageBox.Show("修改成功");
                return;
            }
            if (gameMsg.rsp_AdminChangeUserPassword.IsSucess == -1)
            {
                MessageBox.Show("裁判员登录账号为空！！");
                return;
            }
            if (gameMsg.rsp_AdminChangeUserPassword.IsSucess == -2)
            {
                MessageBox.Show("裁判员员登录密码为空！！");
                return;
            }
            if (gameMsg.rsp_UserChangeUserPassword.IsSucess == -3)
            {
                MessageBox.Show("裁判员新密码为空！！");
                return;
            }
            if (gameMsg.rsp_UserChangeUserPassword.IsSucess == -4)
            {
                MessageBox.Show("裁判员新密码旧密码应该不一样！！");
                return;
            }
            if (gameMsg.rsp_UserChangeUserPassword.IsSucess == -5)
            {
                MessageBox.Show("裁判员密码错误！！");
                return ;
            }
            if (gameMsg.rsp_UserChangeUserPassword.IsSucess == -6)
            {
                MessageBox.Show("裁判员密码修改失败！！");
                return;
            }
        }
    }
}

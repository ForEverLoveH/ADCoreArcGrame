
using ArcSoftFace.ADCoreSystem.ADCoreGameSys;
using ArcSoftFace.ADCoreSystem.ADCoreGameWindow;
using ArcSoftFace.GameNet;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem
{
    public class LoginSys
    {
        public static LoginWindow loginWindow;
        public static LoginSys Instance;
        
        public LocalNetClient localNetClient = new LocalNetClient();

        public  void Awake()
        {
            Instance = this;

        }
        public  void Init()
        {
            StartGame();

        }
        public void StartGame(bool isActive = true)
        {
            if (isActive)
            {
                if (loginWindow == null)
                {
                    Application.Run(loginWindow = new LoginWindow());
                }
                else
                {
                    if (loginWindow.IsDisposed)
                    {
                        Application.Run(loginWindow = new LoginWindow());
                    }
                    else
                    {
                        loginWindow.Activate();
                    }
                }
            }
            else
            {
                if (loginWindow != null)
                {
                    loginWindow.Dispose();
                }
            }
        }

         public  void Req_Login(string acc, string pass)
        {
            GameMsg msg = new GameMsg()
            {
                cmd = CMD.ReqLogin,
                reqLogin = new ReqLogin()
                {
                     acc = acc, 
                     password = pass,
                },
            };
            localNetClient.SendMsg(msg);

        }

        public void RspLogin(GameMsg msg)
        {
            if (msg.errorType == ErrorType.LoginAccountOrPasswordIsEmpty)
            {
                MessageBox.Show("请填写完整用户信息!!");
            }
            else
            {
                if (msg.rspLogin.playerData.isLoginSucess == 0)
                {
                    MessageBox.Show("登录成功！！");

                    MainSys.Instance.Init();
                    loginWindow.Close();


                }
                else if (msg.rspLogin.playerData.isLoginSucess== -1)
                {
                    MessageBox.Show("账号或者密码错误，请重新输入！！");
                }
                else
                {
                    MessageBox.Show(" 非法登录，请停止！！");
                }

            }
        }

        public  String  GetUserData()
        {
            return loginWindow.GetUserData();
        }
    }
}

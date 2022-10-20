using ArcSoftFace.ADCoreSystem.ADCoreGameSys;
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
    public  class VerificationSys
    {
        public static VerificationSys Instance;
        private static VerificationWindow verificationWindow;
        private LocalNetClient localNetClient = new LocalNetClient();
        private PersonImportSys PersonImportSys = new PersonImportSys();
        // public bool IsCheck = false;
        public void Awake()
        {
            Instance = this;
        }
        public void Init()
        {
            StartGame();
        }

        public void Rsp_Verification(GameMsg msg)
        {
            if (msg.errorType == ErrorType.Verification_PasswordIsEmpty)
            {
                MessageBox.Show("请将管理员密码填写完整！！");
            }
            if (msg.rsp_Verfication.IsVerfication == 0)
            {
                //  
                MessageBox.Show("验证成功！！");
                verificationWindow.SetWindowState();
                New_import();
                // IsCheck = true;

            }
            if (msg.rsp_Verfication.IsVerfication == -1)
            {
                MessageBox.Show("账号密码错误,请重新输入！！");
            }

        }

        private void New_import()
        {
            PersonImportSys.New_import();
        }

        public void Req_Verification(string s)
        {
            GameMsg msg = new GameMsg()
            {
                cmd = CMD.Req_Verification,
                req_Verification = new Req_Verification()
                {
                    pass = s,
                }
            };
            localNetClient.SendMsg(msg);

        }

        private void StartGame(bool IsActive = true)
        {
            if (IsActive)
            {
                if (verificationWindow == null)
                {
                    verificationWindow = new VerificationWindow();
                    verificationWindow.Show();
                }
                else
                {
                    if (verificationWindow.IsDisposed)
                    {
                        verificationWindow = new VerificationWindow();
                        verificationWindow.Show();
                    }
                    else
                    {
                        verificationWindow.Activate();
                    }
                }
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem.ADCoreGameWindow
{
    public partial class LoginWindow : Form
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        public LoginSys loginSys  = new LoginSys();
        public string User;

        private void AdminToggle_CheckedChanged(object sender, EventArgs e)
        {
            userToggle.Checked = false;
            string S = "管理员";
            SetUser(S);
        }

        private void SetUser(string s)
        {
            if (s == "管理员")
            {
                string pl = "admin";
                User = pl;
                SetAccountInput(pl);
            }
            if(s == "裁判员")
            {
                string ps = "user";
                User = ps;
                SetAccountInput(ps);
            }
        }

        private void SetAccountInput(string pl)
        {
            AccountInput.Text = pl;
        }

        private void userToggle_CheckedChanged(object sender, EventArgs e)
        {
            AdminToggle.Checked = false;
            string S = "裁判员";
            SetUser(S);

        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string acc = AccountInput.Text.Trim();
            string pass = PasswordInput.Text.Trim();
            if(!string.IsNullOrEmpty(acc))
            {
                if(!string.IsNullOrEmpty(pass))
                {
                    loginSys.Req_Login(acc, pass);

                }
                else
                {
                    MessageBox.Show("请输入密码！！");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请将信息填写完整");
                return;
            }
        }
    }
}

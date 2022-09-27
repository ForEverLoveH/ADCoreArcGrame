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
    public partial class LoginSettingOfUserWindow : Form
    {
        LoginSettingOfUserSys loginSettingOfUserSys= new LoginSettingOfUserSys();
        public LoginSettingOfUserWindow()
        {
            InitializeComponent();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FixBtn_Click(object sender, EventArgs e)
        {
            string oldPassword = OldPasswordInput.Text.Trim();
            string newPassword  = NewPasswordinput.Text.Trim();
            if(string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("请先将信息填写完整！！");
                return;
            }
            else
            {
                if (oldPassword != newPassword)
                {
                    string acc = "user";
                    loginSettingOfUserSys.Req_UserChangeUserPassword(acc,oldPassword, newPassword);
                }
                else
                {
                    MessageBox.Show("新旧密码应该不一致，请重新输入！！");
                    return ;
                }
            }
        }
    }
}

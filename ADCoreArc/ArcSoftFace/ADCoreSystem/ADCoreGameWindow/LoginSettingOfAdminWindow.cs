using Sunny.UI.Win32;
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
    public partial class LoginSettingOfAdminWindow : Form
    {
        LoginSettingOfAdminSys loginSettingOfAdminSys = new LoginSettingOfAdminSys();
        public LoginSettingOfAdminWindow()
        {
            InitializeComponent();
        }
        string user = null;
        private void uiCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (uiCheckBox2.Checked == true)
            {
                uiCheckBox2.Checked = false;
            }

            user = "admin";

        }

        private void uiCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (uiCheckBox1.Checked == true)
            {
                uiCheckBox1.Checked = false;
            }
            user = "user";

        }

        private void FixBtn_Click(object sender, EventArgs e)
        {
            string oldPassword = OldPasswordInput.Text.Trim();
            string newPassword = NewPassworInput.Text.Trim();
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("请先将信息填写完整！！");
                return;
            }
            else
            {
                if (user == null)
                {
                    MessageBox.Show("请先确定你需要修改的账户信息！！");
                    return;
                }
                else
                {
                    if (oldPassword != newPassword)
                    {
                        if (user == "admin")
                        {
                            loginSettingOfAdminSys.Req_AdminChangeAdminPassword(user, oldPassword, newPassword);
                        }
                        if (user == "user")
                        {
                            loginSettingOfAdminSys.Req_AdminChangeUserPassword(user, oldPassword, newPassword);
                        }
                    }
                    else
                    {
                        MessageBox.Show("新旧密码应该不一致！！");
                        return;
                    }

                }
            }
        }


        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

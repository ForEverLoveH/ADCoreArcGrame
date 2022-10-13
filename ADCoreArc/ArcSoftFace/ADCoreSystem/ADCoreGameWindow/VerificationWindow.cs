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
    public partial class VerificationWindow : Form
    {
        public VerificationWindow()
        {
            InitializeComponent();
        }
        VerificationSys VerificationSys = new VerificationSys();

        private void SureBtn_Click(object sender, EventArgs e)
        {
            CheckPassword();
        }

        private void CheckPassword()
        {
            string pass = adminPasswordInput.Text.Trim();
            if (!string.IsNullOrEmpty(pass))
            {
                VerificationSys.Req_Verification(pass);
            }
            else
            {
                MessageBox.Show("请将密码填写完整！！");
                return;
            }
        }

        private void BacekBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adminPasswordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string pass = adminPasswordInput.Text.Trim();
                if (!string.IsNullOrEmpty(pass))
                {
                    VerificationSys.Req_Verification(pass);
                }
                else
                {
                    MessageBox.Show("请将密码填写完整！！");
                    return;
                }
            }
        }
    }
}

namespace ArcSoftFace.ADCoreSystem.ADCoreGameWindow
{
    partial class LoginWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.ExitBtn = new Sunny.UI.UIButton();
            this.LoginBtn = new Sunny.UI.UIButton();
            this.AdminToggle = new Sunny.UI.UICheckBox();
            this.userToggle = new Sunny.UI.UICheckBox();
            this.AccountInput = new Sunny.UI.UITextBox();
            this.PasswordInput = new Sunny.UI.UITextBox();
            this.SuspendLayout();
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(154, 73);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(100, 29);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "账号：";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(154, 130);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(100, 29);
            this.uiLabel2.TabIndex = 1;
            this.uiLabel2.Text = "密码：";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ExitBtn
            // 
            this.ExitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExitBtn.Location = new System.Drawing.Point(394, 319);
            this.ExitBtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(100, 35);
            this.ExitBtn.TabIndex = 2;
            this.ExitBtn.Text = "退出";
            this.ExitBtn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExitBtn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // LoginBtn
            // 
            this.LoginBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoginBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LoginBtn.Location = new System.Drawing.Point(171, 319);
            this.LoginBtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(100, 35);
            this.LoginBtn.TabIndex = 3;
            this.LoginBtn.Text = "登录";
            this.LoginBtn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LoginBtn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // AdminToggle
            // 
            this.AdminToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AdminToggle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AdminToggle.Location = new System.Drawing.Point(171, 210);
            this.AdminToggle.MinimumSize = new System.Drawing.Size(1, 1);
            this.AdminToggle.Name = "AdminToggle";
            this.AdminToggle.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.AdminToggle.Size = new System.Drawing.Size(124, 29);
            this.AdminToggle.TabIndex = 4;
            this.AdminToggle.Text = "管理员";
            this.AdminToggle.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.AdminToggle.CheckedChanged += new System.EventHandler(this.AdminToggle_CheckedChanged);
            // 
            // userToggle
            // 
            this.userToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.userToggle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userToggle.Location = new System.Drawing.Point(377, 210);
            this.userToggle.MinimumSize = new System.Drawing.Size(1, 1);
            this.userToggle.Name = "userToggle";
            this.userToggle.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.userToggle.Size = new System.Drawing.Size(117, 29);
            this.userToggle.TabIndex = 5;
            this.userToggle.Text = "裁判员";
            this.userToggle.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.userToggle.CheckedChanged += new System.EventHandler(this.userToggle_CheckedChanged);
            // 
            // AccountInput
            // 
            this.AccountInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.AccountInput.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AccountInput.Location = new System.Drawing.Point(283, 73);
            this.AccountInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AccountInput.MinimumSize = new System.Drawing.Size(1, 16);
            this.AccountInput.Name = "AccountInput";
            this.AccountInput.ReadOnly = true;
            this.AccountInput.ShowText = false;
            this.AccountInput.Size = new System.Drawing.Size(211, 29);
            this.AccountInput.TabIndex = 6;
            this.AccountInput.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.AccountInput.Watermark = "";
            this.AccountInput.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // PasswordInput
            // 
            this.PasswordInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PasswordInput.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PasswordInput.Location = new System.Drawing.Point(283, 130);
            this.PasswordInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PasswordInput.MinimumSize = new System.Drawing.Size(1, 16);
            this.PasswordInput.Name = "PasswordInput";
            this.PasswordInput.ShowText = false;
            this.PasswordInput.Size = new System.Drawing.Size(211, 29);
            this.PasswordInput.TabIndex = 7;
            this.PasswordInput.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.PasswordInput.Watermark = "";
            this.PasswordInput.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // LoginWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PasswordInput);
            this.Controls.Add(this.AccountInput);
            this.Controls.Add(this.userToggle);
            this.Controls.Add(this.AdminToggle);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.uiLabel1);
            this.Name = "LoginWindow";
            this.Text = "LoginWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIButton ExitBtn;
        private Sunny.UI.UIButton LoginBtn;
        private Sunny.UI.UICheckBox AdminToggle;
        private Sunny.UI.UICheckBox userToggle;
        private Sunny.UI.UITextBox AccountInput;
        private Sunny.UI.UITextBox PasswordInput;
    }
}
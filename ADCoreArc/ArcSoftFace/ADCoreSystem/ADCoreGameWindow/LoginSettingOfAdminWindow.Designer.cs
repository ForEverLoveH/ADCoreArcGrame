namespace ArcSoftFace.ADCoreSystem.ADCoreGameWindow
{
    partial class LoginSettingOfAdminWindow
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
            this.OldPasswordInput = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.NewPassworInput = new Sunny.UI.UITextBox();
            this.uiCheckBox1 = new Sunny.UI.UICheckBox();
            this.uiCheckBox2 = new Sunny.UI.UICheckBox();
            this.FixBtn = new Sunny.UI.UIButton();
            this.BackBtn = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(225, 55);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(129, 29);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "旧密码：";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // OldPasswordInput
            // 
            this.OldPasswordInput.Cursor = System.Windows.Forms.Cursors.Default;
            this.OldPasswordInput.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OldPasswordInput.Location = new System.Drawing.Point(229, 89);
            this.OldPasswordInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.OldPasswordInput.MinimumSize = new System.Drawing.Size(1, 16);
            this.OldPasswordInput.Name = "OldPasswordInput";
            this.OldPasswordInput.ShowText = false;
            this.OldPasswordInput.Size = new System.Drawing.Size(284, 31);
            this.OldPasswordInput.TabIndex = 1;
            this.OldPasswordInput.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.OldPasswordInput.Watermark = "";
            this.OldPasswordInput.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(229, 149);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(100, 23);
            this.uiLabel2.TabIndex = 2;
            this.uiLabel2.Text = "新密码：";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // NewPassworInput
            // 
            this.NewPassworInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.NewPassworInput.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NewPassworInput.Location = new System.Drawing.Point(229, 194);
            this.NewPassworInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NewPassworInput.MinimumSize = new System.Drawing.Size(1, 16);
            this.NewPassworInput.Name = "NewPassworInput";
            this.NewPassworInput.ShowText = false;
            this.NewPassworInput.Size = new System.Drawing.Size(284, 29);
            this.NewPassworInput.TabIndex = 3;
            this.NewPassworInput.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.NewPassworInput.Watermark = "";
            this.NewPassworInput.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiCheckBox1
            // 
            this.uiCheckBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiCheckBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiCheckBox1.Location = new System.Drawing.Point(233, 285);
            this.uiCheckBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiCheckBox1.Name = "uiCheckBox1";
            this.uiCheckBox1.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.uiCheckBox1.Size = new System.Drawing.Size(150, 29);
            this.uiCheckBox1.TabIndex = 4;
            this.uiCheckBox1.Text = "管理员";
            this.uiCheckBox1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiCheckBox1.CheckedChanged += new System.EventHandler(this.uiCheckBox1_CheckedChanged);
            // 
            // uiCheckBox2
            // 
            this.uiCheckBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiCheckBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiCheckBox2.Location = new System.Drawing.Point(404, 285);
            this.uiCheckBox2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiCheckBox2.Name = "uiCheckBox2";
            this.uiCheckBox2.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.uiCheckBox2.Size = new System.Drawing.Size(109, 29);
            this.uiCheckBox2.TabIndex = 5;
            this.uiCheckBox2.Text = "裁判员";
            this.uiCheckBox2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiCheckBox2.CheckedChanged += new System.EventHandler(this.uiCheckBox2_CheckedChanged);
            // 
            // FixBtn
            // 
            this.FixBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FixBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FixBtn.Location = new System.Drawing.Point(229, 384);
            this.FixBtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.FixBtn.Name = "FixBtn";
            this.FixBtn.Size = new System.Drawing.Size(104, 35);
            this.FixBtn.TabIndex = 6;
            this.FixBtn.Text = "修改";
            this.FixBtn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FixBtn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.FixBtn.Click += new System.EventHandler(this.FixBtn_Click);
            // 
            // BackBtn
            // 
            this.BackBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BackBtn.Location = new System.Drawing.Point(413, 384);
            this.BackBtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(100, 35);
            this.BackBtn.TabIndex = 7;
            this.BackBtn.Text = "返回";
            this.BackBtn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BackBtn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // LoginSettingOfAdminWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 450);
            this.Controls.Add(this.BackBtn);
            this.Controls.Add(this.FixBtn);
            this.Controls.Add(this.uiCheckBox2);
            this.Controls.Add(this.uiCheckBox1);
            this.Controls.Add(this.NewPassworInput);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.OldPasswordInput);
            this.Controls.Add(this.uiLabel1);
            this.Name = "LoginSettingOfAdminWindow";
            this.Text = "LoginSettingOfAdmin";
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox OldPasswordInput;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UITextBox NewPassworInput;
        private Sunny.UI.UICheckBox uiCheckBox1;
        private Sunny.UI.UICheckBox uiCheckBox2;
        private Sunny.UI.UIButton FixBtn;
        private Sunny.UI.UIButton BackBtn;
    }
}
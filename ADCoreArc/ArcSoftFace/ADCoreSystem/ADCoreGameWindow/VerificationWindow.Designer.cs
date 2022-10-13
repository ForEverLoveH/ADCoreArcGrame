namespace ArcSoftFace.ADCoreSystem.ADCoreGameWindow
{
    partial class VerificationWindow
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
            this.SureBtn = new Sunny.UI.UIButton();
            this.BacekBtn = new Sunny.UI.UIButton();
            this.adminPasswordInput = new Sunny.UI.UITextBox();
            this.SuspendLayout();
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(179, 88);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(240, 23);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "请输入管理员密码：";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // SureBtn
            // 
            this.SureBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SureBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SureBtn.Location = new System.Drawing.Point(173, 210);
            this.SureBtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.SureBtn.Name = "SureBtn";
            this.SureBtn.Size = new System.Drawing.Size(100, 35);
            this.SureBtn.TabIndex = 1;
            this.SureBtn.Text = "确定";
            this.SureBtn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SureBtn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.SureBtn.Click += new System.EventHandler(this.SureBtn_Click);
            // 
            // BacekBtn
            // 
            this.BacekBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BacekBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BacekBtn.Location = new System.Drawing.Point(506, 210);
            this.BacekBtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.BacekBtn.Name = "BacekBtn";
            this.BacekBtn.Size = new System.Drawing.Size(100, 35);
            this.BacekBtn.TabIndex = 2;
            this.BacekBtn.Text = "取消";
            this.BacekBtn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BacekBtn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.BacekBtn.Click += new System.EventHandler(this.BacekBtn_Click);
            // 
            // adminPasswordInput
            // 
            this.adminPasswordInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.adminPasswordInput.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.adminPasswordInput.Location = new System.Drawing.Point(173, 127);
            this.adminPasswordInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.adminPasswordInput.MinimumSize = new System.Drawing.Size(1, 16);
            this.adminPasswordInput.Name = "adminPasswordInput";
            this.adminPasswordInput.ShowText = false;
            this.adminPasswordInput.Size = new System.Drawing.Size(433, 38);
            this.adminPasswordInput.TabIndex = 3;
            this.adminPasswordInput.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.adminPasswordInput.Watermark = "";
            this.adminPasswordInput.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.adminPasswordInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.adminPasswordInput_KeyDown);
            // 
            // VerificationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 356);
            this.Controls.Add(this.adminPasswordInput);
            this.Controls.Add(this.BacekBtn);
            this.Controls.Add(this.SureBtn);
            this.Controls.Add(this.uiLabel1);
            this.Name = "VerificationWindow";
            this.Text = "VerificationWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIButton SureBtn;
        private Sunny.UI.UIButton BacekBtn;
        private Sunny.UI.UITextBox adminPasswordInput;
    }
}
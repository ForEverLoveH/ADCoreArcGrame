namespace ArcSoftFace.ADCoreSystem.ADCoreGameWindow
{
    partial class PersonImportWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LoadTable = new Sunny.UI.UIButton();
            this.uiButton2 = new Sunny.UI.UIButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.PersonDataLabel = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.FilePathInput = new Sunny.UI.UITextBox();
            this.ChooseLocalFileBtn = new Sunny.UI.UIButton();
            this.ViewDataBtn = new Sunny.UI.UIButton();
            this.OpenExplemBtn = new Sunny.UI.UIButton();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.SheetDataTableDrop = new Sunny.UI.UIComboBox();
            this.NewImportBtn = new Sunny.UI.UIButton();
            this.AddImportBtn = new Sunny.UI.UIButton();
            this.FileDataGrid = new Sunny.UI.UIDataGridView();
            this.cell = new Sunny.UI.UILabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(305, 158);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // LoadTable
            // 
            this.LoadTable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadTable.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LoadTable.Location = new System.Drawing.Point(12, 118);
            this.LoadTable.MinimumSize = new System.Drawing.Size(1, 1);
            this.LoadTable.Name = "LoadTable";
            this.LoadTable.Size = new System.Drawing.Size(100, 35);
            this.LoadTable.TabIndex = 1;
            this.LoadTable.Text = "下载名单";
            this.LoadTable.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LoadTable.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.LoadTable.Click += new System.EventHandler(this.LoadTable_Click);
            // 
            // uiButton2
            // 
            this.uiButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton2.Location = new System.Drawing.Point(198, 118);
            this.uiButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Size = new System.Drawing.Size(100, 35);
            this.uiButton2.TabIndex = 2;
            this.uiButton2.Text = "服务器设置";
            this.uiButton2.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiButton2.Click += new System.EventHandler(this.uiButton2_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(450, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(780, 158);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(456, 9);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(140, 27);
            this.uiLabel1.TabIndex = 4;
            this.uiLabel1.Text = "人员数据：";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // PersonDataLabel
            // 
            this.PersonDataLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PersonDataLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PersonDataLabel.Location = new System.Drawing.Point(563, 9);
            this.PersonDataLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PersonDataLabel.MinimumSize = new System.Drawing.Size(1, 16);
            this.PersonDataLabel.Name = "PersonDataLabel";
            this.PersonDataLabel.ShowText = false;
            this.PersonDataLabel.Size = new System.Drawing.Size(138, 27);
            this.PersonDataLabel.TabIndex = 5;
            this.PersonDataLabel.Text = "NULL";
            this.PersonDataLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.PersonDataLabel.Watermark = "";
            this.PersonDataLabel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(460, 40);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(168, 23);
            this.uiLabel2.TabIndex = 6;
            this.uiLabel2.Text = "请选择本地文件夹：";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // FilePathInput
            // 
            this.FilePathInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FilePathInput.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FilePathInput.Location = new System.Drawing.Point(617, 40);
            this.FilePathInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FilePathInput.MinimumSize = new System.Drawing.Size(1, 16);
            this.FilePathInput.Name = "FilePathInput";
            this.FilePathInput.ShowText = false;
            this.FilePathInput.Size = new System.Drawing.Size(282, 29);
            this.FilePathInput.TabIndex = 7;
            this.FilePathInput.Text = " ";
            this.FilePathInput.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.FilePathInput.Watermark = "";
            this.FilePathInput.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ChooseLocalFileBtn
            // 
            this.ChooseLocalFileBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChooseLocalFileBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChooseLocalFileBtn.Location = new System.Drawing.Point(934, 40);
            this.ChooseLocalFileBtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.ChooseLocalFileBtn.Name = "ChooseLocalFileBtn";
            this.ChooseLocalFileBtn.Size = new System.Drawing.Size(100, 29);
            this.ChooseLocalFileBtn.TabIndex = 8;
            this.ChooseLocalFileBtn.Text = "选择文件";
            this.ChooseLocalFileBtn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChooseLocalFileBtn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ChooseLocalFileBtn.Click += new System.EventHandler(this.ChooseLocalFileBtn_Click);
            // 
            // ViewDataBtn
            // 
            this.ViewDataBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ViewDataBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ViewDataBtn.Location = new System.Drawing.Point(799, 96);
            this.ViewDataBtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.ViewDataBtn.Name = "ViewDataBtn";
            this.ViewDataBtn.Size = new System.Drawing.Size(100, 29);
            this.ViewDataBtn.TabIndex = 9;
            this.ViewDataBtn.Text = "预览";
            this.ViewDataBtn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ViewDataBtn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ViewDataBtn.Click += new System.EventHandler(this.ViewDataBtn_Click);
            // 
            // OpenExplemBtn
            // 
            this.OpenExplemBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenExplemBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OpenExplemBtn.Location = new System.Drawing.Point(1099, 40);
            this.OpenExplemBtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.OpenExplemBtn.Name = "OpenExplemBtn";
            this.OpenExplemBtn.Size = new System.Drawing.Size(100, 29);
            this.OpenExplemBtn.TabIndex = 10;
            this.OpenExplemBtn.Text = "打开示例";
            this.OpenExplemBtn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OpenExplemBtn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.OpenExplemBtn.Click += new System.EventHandler(this.OpenExplemBtn_Click);
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.Location = new System.Drawing.Point(464, 96);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(100, 23);
            this.uiLabel3.TabIndex = 11;
            this.uiLabel3.Text = "工作表名：";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // SheetDataTableDrop
            // 
            this.SheetDataTableDrop.DataSource = null;
            this.SheetDataTableDrop.FillColor = System.Drawing.Color.White;
            this.SheetDataTableDrop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SheetDataTableDrop.Location = new System.Drawing.Point(607, 95);
            this.SheetDataTableDrop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SheetDataTableDrop.MinimumSize = new System.Drawing.Size(63, 0);
            this.SheetDataTableDrop.Name = "SheetDataTableDrop";
            this.SheetDataTableDrop.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.SheetDataTableDrop.Size = new System.Drawing.Size(185, 29);
            this.SheetDataTableDrop.TabIndex = 12;
            this.SheetDataTableDrop.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.SheetDataTableDrop.Watermark = "";
            this.SheetDataTableDrop.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // NewImportBtn
            // 
            this.NewImportBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NewImportBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NewImportBtn.Location = new System.Drawing.Point(934, 96);
            this.NewImportBtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.NewImportBtn.Name = "NewImportBtn";
            this.NewImportBtn.Size = new System.Drawing.Size(100, 29);
            this.NewImportBtn.TabIndex = 13;
            this.NewImportBtn.Text = "全新导入";
            this.NewImportBtn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NewImportBtn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.NewImportBtn.Click += new System.EventHandler(this.NewImportBtn_Click);
            // 
            // AddImportBtn
            // 
            this.AddImportBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddImportBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddImportBtn.Location = new System.Drawing.Point(1099, 95);
            this.AddImportBtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.AddImportBtn.Name = "AddImportBtn";
            this.AddImportBtn.Size = new System.Drawing.Size(100, 30);
            this.AddImportBtn.TabIndex = 14;
            this.AddImportBtn.Text = "新增导入";
            this.AddImportBtn.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddImportBtn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.AddImportBtn.Click += new System.EventHandler(this.AddImportBtn_Click);
            // 
            // FileDataGrid
            // 
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.FileDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.FileDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.FileDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FileDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.FileDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.FileDataGrid.DefaultCellStyle = dataGridViewCellStyle13;
            this.FileDataGrid.EnableHeadersVisualStyles = false;
            this.FileDataGrid.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FileDataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.FileDataGrid.Location = new System.Drawing.Point(4, 207);
            this.FileDataGrid.Name = "FileDataGrid";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FileDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FileDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.FileDataGrid.RowTemplate.Height = 23;
            this.FileDataGrid.ScrollBarRectColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.FileDataGrid.SelectedIndex = -1;
            this.FileDataGrid.Size = new System.Drawing.Size(1226, 378);
            this.FileDataGrid.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.FileDataGrid.Style = Sunny.UI.UIStyle.Custom;
            this.FileDataGrid.TabIndex = 15;
            this.FileDataGrid.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cell
            // 
            this.cell.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cell.Location = new System.Drawing.Point(23, 178);
            this.cell.Name = "cell";
            this.cell.Size = new System.Drawing.Size(100, 23);
            this.cell.TabIndex = 16;
            this.cell.Text = "文件数据";
            this.cell.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cell.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // PersonImportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 590);
            this.Controls.Add(this.cell);
            this.Controls.Add(this.FileDataGrid);
            this.Controls.Add(this.AddImportBtn);
            this.Controls.Add(this.NewImportBtn);
            this.Controls.Add(this.SheetDataTableDrop);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.OpenExplemBtn);
            this.Controls.Add(this.ViewDataBtn);
            this.Controls.Add(this.ChooseLocalFileBtn);
            this.Controls.Add(this.FilePathInput);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.PersonDataLabel);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.uiButton2);
            this.Controls.Add(this.LoadTable);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PersonImportWindow";
            this.Text = "人员导入";
            this.Load += new System.EventHandler(this.PersonImportWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private Sunny.UI.UIButton LoadTable;
        private Sunny.UI.UIButton uiButton2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox PersonDataLabel;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UITextBox FilePathInput;
        private Sunny.UI.UIButton ChooseLocalFileBtn;
        private Sunny.UI.UIButton ViewDataBtn;
        private Sunny.UI.UIButton OpenExplemBtn;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIComboBox SheetDataTableDrop;
        private Sunny.UI.UIButton NewImportBtn;
        private Sunny.UI.UIButton AddImportBtn;
        private Sunny.UI.UIDataGridView FileDataGrid;
        private Sunny.UI.UILabel cell;
    }
}
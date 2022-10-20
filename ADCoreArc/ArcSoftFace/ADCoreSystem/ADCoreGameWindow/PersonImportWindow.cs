using ArcSoftFace.ADCoreSystem.ADCoreGameSys;
using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.ADCoreSystem.Loading;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem.ADCoreGameWindow
{
    public partial class PersonImportWindow : Form
    {
        public PersonImportWindow()
        {
            InitializeComponent();
        }
        PersonImportSys PersonImportSys = new PersonImportSys();
        

       public void Set_Txt_Title(string num)
        {

            PersonDataLabel.Text = num; 
        }

        private void PersonImportWindow_Load(object sender, EventArgs e)
        {
           PersonImportSys.Req_GetKey();
        }

        List<string> sheetTable = new List<string>();
        private void ChooseLocalFileBtn_Click(object sender, EventArgs e)
        {
            FileDataGrid.DataSource = null;
            FilePathInput.Text = null;
            SheetDataTableDrop.Items.Clear();
            OpenFileName openFileName = new OpenFileName();
            openFileName.structSize = Marshal.SizeOf(openFileName);
            openFileName.filter = "Excel文件(*xlsx/*xls)\0*.xlsx;*.xls\0";
            openFileName.file = new string(new char[1024]);
            openFileName.maxFile = openFileName.file.Length;
            openFileName.fileTitle = new string(new char[1024]);
            openFileName.maxFileTitle = openFileName.fileTitle.Length;
            openFileName.initialDir = Application.StartupPath.Replace('/', '\\');//默认路径
            openFileName.title = "选择*xlsx/*xls文件";
            openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
            if (LocalDialog.GetOpenFileName(openFileName))    // 打开本地文件{
            {
                var s = openFileName.file;
                FilePathInput.Text = s;
                LoadingHelper.ShowLoading("正在从excel 中获取数据，请稍后！！", this, (obj) =>
                {
                    Xlsx xlsx = new Xlsx();
                    sheetTable = xlsx.GetSheet(s);// 会得到一张表
                });
                if (sheetTable.Count > 0)
                {
                    SetSheetData();
                }
                else
                {
                    MessageBox.Show("请选择文件！！");
                    return;
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetSheetData()
        {
            if (SheetDataTableDrop != null)
            {
                foreach (var item in sheetTable)
                {
                    SheetDataTableDrop.Items.Add(item);
                }

            }
            else
            {
                //Number_index = 0;
                FilePathInput.Text= null;
                SheetDataTableDrop.Items.Clear();
                MessageBox.Show("这不是xlsx文件，或xls文件已损坏，也可能是该文件正在被其它应用程序访问");
                return;
            }
        }
    

         /// <summary>
         /// 打开示例
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>

        private void OpenExplemBtn_Click(object sender, EventArgs e)
        {
            string Path = Application.StartupPath + GameConst.ExcelPath;
            string Paths = $"{Path}/{GameConst.ExcelTemplateName}{GameConst.ExcelTemplateType_Xlsx}";
            if (!File.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            if (!Directory.Exists(Paths))
            {
                List<string> ListSheetName = new List<string>();
                ListSheetName.Add("Sheet1");
                Xlsx xlsx = new Xlsx();
                xlsx.EstablishExcel(Path, GameConst.ExcelTemplateName, GameConst.ExcelTemplateType_Xlsx, ListSheetName);
                xlsx.WiteToExcel<UserExcelTemplateMode>(Paths, "Sheet1", GameConst.ExcelTemplateType_Xlsx);
            }
            System.Diagnostics.Process.Start(Paths);

        } 
        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewDataBtn_Click(object sender, EventArgs e)
        {
            var s = FilePathInput.Text;
            var sl = SheetDataTableDrop.Text;
            if (FileDataGrid.DataSource != null)
            {
                FileDataGrid.DataSource = null;
            }
            if (!string.IsNullOrEmpty(s))
            {
                if (!string.IsNullOrEmpty(sl))
                {
                    LoadingHelper.ShowLoading("正在打开excel 数据表", this, (obj) =>
                    {
                        ViewListExamineeDataMode(s, sl);
                    });
                    if (dataTable != null)
                    {
                       FileDataGrid.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("打开excel 失败，请确定文件是否毁坏，或者路径是否正确！");
                        return;

                    }

                }

            }

        }
        /// <summary>
        /// 
        /// </summary>
        DataTable dataTable = new DataTable();
        private void ViewListExamineeDataMode(string s, string  sl)
        {
            DataTable dt = new DataTable();
            ExcelToDataSet excelToDataSet = new ExcelToDataSet();
            dt = excelToDataSet .GetExcelDatable(s, sl);
            dataTable = dt;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewImportBtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(FilePathInput.Text))
            {
                if (!String.IsNullOrEmpty(SheetDataTableDrop.Text))
                {
                    PersonImportSys.f1 = FilePathInput.Text.Trim();
                    PersonImportSys.f2 = SheetDataTableDrop.Text.Trim();
                    VerificationSys.Instance.Init();

                }
            }
            else
            {
                MessageBox.Show("请选择*xls/*xlsx文件！！");
            }

        }

        public  void New_import()
        {
            var s = FilePathInput.Text;
            var sl = SheetDataTableDrop.Text;
            if (!string.IsNullOrEmpty(s))
            {
                if (!String.IsNullOrEmpty(sl))
                {
                    List<UserExcel> userExcels = new List<UserExcel>();
                    //Number_index = 0;
                    userExcels = GetExamineeDataMode(s);
                    if (userExcels.Count > 0)
                    {
                        this.sheetData = userExcels;
                    }
                    if (userExcels.Count > 0)
                    {
                        if (sheetData.Count + PersonImportSys.sqlNumber < GameConst.SqlNumber)
                        {
                            switch (sheetData[0].Err)
                            {
                                case -1:
                                    SetSheetDa setSheetdata = new SetSheetDa(SetSheetDataEmpty);
                                    MessageBox.Show("*xlsx/*xls文件工作表数据为空");
                                    break;
                                case -2:
                                    SetSheetDa setSheetData = new SetSheetDa(SetSheetDataEmpty);
                                    MessageBox.Show("*xlsx/*xls文件数据格式与设定的数据格式不符");
                                    break;
                                case -3:
                                    SetSheetDa setSheetDatas = new SetSheetDa(SetSheetDataEmpty);
                                    MessageBox.Show("*xlsx/*xls文件数据格过大，列已超过100 或者 行已超过100万");
                                    break;
                                default:
                                    ViewListExamineeDataMode(s, sl);
                                    PersonImportSys.Instance.Req_NewImport(sheetData);
                                    break;
                            }
                        }
                    }
                }
            }
        }
         List<UserExcel> sheetData = new List<UserExcel>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddImportBtn_Click(object sender, EventArgs e)
        {
            var s = FilePathInput.Text;
            var sl = SheetDataTableDrop.Text;
            if (!string.IsNullOrEmpty(sl) && !string.IsNullOrEmpty(s))
            {

                List<UserExcel> userExcels = new List<UserExcel>();
                //Number_index = 0;
                userExcels = GetExamineeDataMode(s);
                if (userExcels.Count > 0)
                {
                    this.sheetData = userExcels;
                }
                if (sheetData != null)
                {

                    if (sheetData.Count + PersonImportSys.sqlNumber < GameConst.SqlNumber)
                    {
                        switch (sheetData[0].Err)
                        {
                            case -1:
                                SetSheetDa setSheetdata = new SetSheetDa(SetSheetDataEmpty);
                                MessageBox.Show("*xlsx/*xls文件工作表数据为空");
                                break;
                            case -2:
                                SetSheetDa setSheetData = new SetSheetDa(SetSheetDataEmpty);
                                MessageBox.Show("*xlsx/*xls文件数据格式与设定的数据格式不符");
                                break;
                            case -3:
                                SetSheetDa setSheetDatas = new SetSheetDa(SetSheetDataEmpty);
                                MessageBox.Show("*xlsx/*xls文件数据格过大，列已超过100 或者 行已超过100万");
                                break;
                            default:
                                ViewListExamineeDataMode(s, sl);
                                PersonImportSys.Instance.Req_AddImport(sheetData);
                                break;
                        }
                    }
                }
            }
        }
        delegate void SetSheetDa();
        private void SetSheetDataEmpty()
        {
            SheetDataTableDrop.Items.Clear();
        }
        private List<UserExcel> GetExamineeDataMode(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                Xlsx xlsx = new Xlsx(); // 这里报错线程无法调用其它
                return xlsx.GetSheetData(FilePathInput.Text, SheetDataTableDrop.Text);

            }
            else
            {
                MessageBox.Show("请先选择*xlsx/*xls文件哦");
                return null;
            }
        }

        private void LoadTable_Click(object sender, EventArgs e)
        {
            MessageBox.Show("正在开发中请稍后！！");
            return;
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("正在开发中请稍后！！");
            return;
        }
    }
}

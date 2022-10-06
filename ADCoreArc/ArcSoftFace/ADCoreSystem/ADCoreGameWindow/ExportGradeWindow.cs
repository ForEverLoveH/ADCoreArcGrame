using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.ADCoreSystem.Loading;
using ArcSoftFace.GameCommon;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.Streaming;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem.ADCoreGameWindow
{
    public partial class ExportGradeWindow : Form
    {
        ExportGradeSys gradeSys = new ExportGradeSys();
        public ExportGradeWindow()
        {
            InitializeComponent();
        }

        private void ExportGradeWindow_Load(object sender, EventArgs e)
        {
            gradeSys.Req_GetKey();

        }
        public void Set_Txt_Title(string num)
        {
            DataNumInput.Text = num;
        }

        private void ExportGradeWindow_DragDrop(object sender, DragEventArgs e)
        {
            String path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            FilePathInput.Text = path;
        }

        private void ExportGradeWindow_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        private void ChooseBtn_Click(object sender, EventArgs e)
        {
            OpenDialogDir openDialogDir = new OpenDialogDir();
            openDialogDir.pszDisplayName = new string(new char[2000]);
            openDialogDir.lpszTitle = "选择文件夹";
            IntPtr pid = DllOpenFileDialog.SHBrowseForFolder(openDialogDir);
            char[] ch = new char[2000];
            for (int i = 0; i < 2000; i++)
            {
                ch[i] = '\0';
            }
            DllOpenFileDialog.SHGetPathFromIDList(pid, ch);
            string s = new string(ch);
            s = s.Substring(0, s.IndexOf('\0'));

            FilePathInput.Text = s;
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FilePathInput.Text))
            {
                if (!string.IsNullOrEmpty(filename.Text))
                {
                    gradeSys.Req_ExportGrade();
                }
                else
                {
                    MessageBox.Show("请输入文件名！！");
                }
            }
            else
            {
                MessageBox.Show("请选择或者拖拽文件夹到输入框中！！");
            }
        }
        private UserExcelIndexMode userExcelIndexModes = new UserExcelIndexMode();
        public void Export_Excel(List<UserExcel> userExcel)
        {
            LoadingHelper.ShowLoading("正在写入excel", this, (obj) =>
            {

                Xlsx xlsx = new Xlsx();
                List<String> sheetName = new List<string>();
                sheetName.Add("Sheet1");
                xlsx.EstablishExcel(FilePathInput.Text + "/", filename.Text, ".xlsx", sheetName);
                xlsx.WiteToExcel<UserExcelTemplateMode>($"{FilePathInput.Text}/{filename.Text}.xlsx", "Sheet1", ".xlsx");
                WriteToExcel<UserExcelTemplateMode>($"{FilePathInput.Text}/{filename.Text}.xlsx", "Sheet1", ".xlsx", userExcel);

            });
        }

        /// <summary>
        /// 往excel 中添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="sheetName"></param>
        /// <param name="Type"></param>
        /// <param name="user"></param>
        private void WriteToExcel<T>(string path, string sheetName, string Type, List<UserExcel> userExcelk)
        {
            var type = typeof(T);
            var p = type.GetProperties();
            IWorkbook wok = null;
            FileStream fileStream = null;
            if (Type == ".xls")
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                wok = new HSSFWorkbook(fileStream);

            }
            if (Type == ".xlsx")
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                wok = new XSSFWorkbook(fileStream);
                wok = new SXSSFWorkbook((XSSFWorkbook)wok);


            }
            ISheet sheet = wok.GetSheet(sheetName);
            IRow row = sheet.CreateRow(1);
            ICell cell = null;
            ICellStyle cellStyle = wok.CreateCellStyle();
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            IFont font = wok.CreateFont();
            font.FontHeightInPoints = 11;
            font.FontName = "宋体";
            cellStyle.SetFont(font);
            for (int i = 0; i < p.Length; i++)
            {

                SheetDataIndex(p[i].Name, i);
            }
            foreach (var user in userExcelk)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (j == userExcelIndexModes.Exam_number)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Exam_number);
                    }
                    if (j == userExcelIndexModes.Region)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Region);
                    }
                    if (j == userExcelIndexModes.Venue)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Venue);
                    }
                    if (j == userExcelIndexModes.Project_team)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Project_team);
                    }
                    if (j == userExcelIndexModes.Name)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Name);
                    }
                    if (j == userExcelIndexModes.Sex)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Sex);
                    }
                    if (j == userExcelIndexModes.School)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.School);
                    }
                    if (j == userExcelIndexModes.Grade)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Grade);
                    }
                    if (j == userExcelIndexModes.ClassName)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.ClassName);
                    }
                    if (j == userExcelIndexModes.Number_class)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Number_class);
                    }
                    if (j == userExcelIndexModes.Project)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Project);
                    }
                    if (j == userExcelIndexModes.Exam_date)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Exam_date);
                    }
                    if (j == userExcelIndexModes.Sessions)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Sessions);
                    }
                    if (j == userExcelIndexModes.Group_number)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Group_number);
                    }
                    if (j == userExcelIndexModes.Intra_group_serial_number)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Intra_group_serial_number);
                    }
                    if (j == userExcelIndexModes.Achievement_one)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Achievement_one);
                    }
                    if (j == userExcelIndexModes.Achievement_two)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Achievement_two);
                    }
                    if (j == userExcelIndexModes.Achievement_three)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Achievement_three);
                    }
                    if (j == userExcelIndexModes.Achievement_four)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Achievement_four);
                    }
                    if (j == userExcelIndexModes.Remarks)
                    {
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(user.Remarks);
                    }
                }
                row = sheet.CreateRow(sheet.LastRowNum + 1);
            }
            // 写入文件流
            using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                wok.Write(file);
                wok.Close();
                file.Close();
                file.Dispose();
            }





        }

        private void SheetDataIndex(string name, int index)
        {
            switch (name)
            {
                case "考号":
                    userExcelIndexModes.Exam_number = index;
                    break;
                case "地区":
                    userExcelIndexModes.Region = index;
                    break;
                case "场地":
                    userExcelIndexModes.Venue = index;
                    break;
                case "项目组":
                    userExcelIndexModes.Project_team = index;
                    break;
                case "姓名":
                    userExcelIndexModes.Name = index;
                    break;
                case "性别":
                    userExcelIndexModes.Sex = index;
                    break;
                case "学校":
                    userExcelIndexModes.School = index;
                    break;
                case "年级":
                    userExcelIndexModes.Grade = index;
                    break;
                case "班级":
                    userExcelIndexModes.ClassName = index;
                    break;
                case "班级号数":
                    userExcelIndexModes.Number_class = index;
                    break;
                case "项目":
                    userExcelIndexModes.Project = index;
                    break;
                case "考试日期":
                    userExcelIndexModes.Exam_date = index;
                    break;
                case "场次":
                    userExcelIndexModes.Sessions = index;
                    break;
                case "组号":
                    userExcelIndexModes.Group_number = index;
                    break;
                case "组内序号":
                    userExcelIndexModes.Intra_group_serial_number = index;
                    break;
                case "成绩1":
                    userExcelIndexModes.Achievement_one = index;
                    break;
                case "成绩2":
                    userExcelIndexModes.Achievement_two = index;
                    break;
                case "成绩3 ":
                    userExcelIndexModes.Achievement_three = index;
                    break;
                case "成绩4":
                    userExcelIndexModes.Achievement_four = index;
                    break;
                case "备注":
                    userExcelIndexModes.Remarks = index;
                    break;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}

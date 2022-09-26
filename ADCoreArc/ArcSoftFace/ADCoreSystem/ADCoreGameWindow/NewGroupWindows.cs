using AForge.Imaging;
using ArcFaceSDK;
using ArcFaceSDK.Entity;
using ArcFaceSDK.SDKModels;
using ArcFaceSDK.Utils;
using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.ADCoreSystem.Loading;
using ArcSoftFace.Arcsoft;
using ArcSoftFace.GameCommon;
using ArcSoftFace.Utils;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;
using Image = System.Drawing.Image;

namespace ArcSoftFace.ADCoreSystem.ADCoreGameWindow
{
    public partial class NewGroupWindows : Form
    {
        FaceEngine FaceEngine = new FaceEngine();
        public string FaceDirectory = Application.StartupPath + GameConst.FaceDirectory;
        public string groupDirectory = null;
        public ImageData imageData = new ImageData();
        bool isImportfacedata = false;

        /// <summary>
        /// 用字典类型标记人脸数组 key 为 组号， value 为人脸数据列表
        /// </summary>
        public Dictionary<string, List<string>> FaceData = new Dictionary<string, List<string>>();
        List<string> featureData = new List<string>(); // 人脸特征值

        public NewGroupWindows()
        {
            InitializeComponent();
        }
        public void NewGroup_Load(object sender, EventArgs e)
        {
            ArcFaceManage.Instance.InitEngines();
            var s = FaceEngine.pEngine;

        }
        NewGroupSys NewGroupSys = new NewGroupSys();
        List<String> imagePath = new List<string>();

        List<UserExcel> userExcels = null;
        string groupid;
        /// <summary>
        /// 图像处理引擎对象
        /// </summary>
        private FaceEngine imageEngine = new FaceEngine();
        // <summary>
        /// 人脸库人脸特征列表
        /// </summary>
        private List<FaceFeature> leftImageFeatureList = new List<FaceFeature>();

        bool IsEndOfAddUserDataToList = false;// 是否结束添加数据到列表中
        #region 手动导入
        private void AddStudentToListBtn_Click(object sender, EventArgs e)
        {
            if (IsEndOfAddUserDataToList == false)
            {
                CheckExcelModelToList();
                string groupID = GroupNumText.Text.Trim();  // 组号
                string groupOrder = groupOrderInput.Text.Trim();  // 组内顺序
                string examDate = examTimeDroup.Text.Trim();  // 考试时间
                string name = NameInput.Text.Trim(); // 名字
                string sex = SexDroup.Text.Trim(); // 性别
                string examNum = ExamNumberInput.Text.Trim();// 考号
                string session = RegionInput.Text.Trim();  // 地区
                string className = classNameInput.Text.Trim(); // 班级名字
                string gradeName = GradeInput.Text.Trim(); //  年级
                string classNum = NumClassInput.Text.Trim();//班级号数
                string region = SesetionInput.Text.Trim();  // 场次
                string school = SchoolInput.Text.Trim();
                if (!string.IsNullOrEmpty(groupID) && !string.IsNullOrEmpty(groupOrder) && !string.IsNullOrEmpty(examDate) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(sex) && !string.IsNullOrEmpty(examNum)
                    && !string.IsNullOrEmpty(session) && !string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(gradeName) && !string.IsNullOrEmpty(classNum) && !string.IsNullOrEmpty(region))
                {
                    groupid = groupID;
                    UserExcel userExcelM = new UserExcel()
                    {
                        Exam_number = examNum,
                        Exam_date = examDate,
                        Name = name,
                        Sex = sex,
                        Group_number = groupID,
                        Number_class = classNum,
                        Sessions = region,
                        Intra_group_serial_number = groupOrder,
                        ClassName = className,
                        Grade = gradeName,
                        Region = session,
                        Project = "引体向上",
                        Project_team = "中考引体向上 ", // 项目组
                        Achievement_one = "",
                        Achievement_two = " ",
                        Achievement_three = " ",
                        Achievement_four = " ",
                        Venue = " ",
                        Remarks = " ",
                        School = school,

                    };
                    if (userExcels == null)
                    {
                        userExcels = new List<UserExcel>();
                        ShowUserExcel(userExcelM);
                        userExcels.Add(userExcelM);
                    }
                    else
                    {
                        if (userExcels.Contains(userExcelM))
                        {
                            MessageBox.Show("该成员已经在列表中，无法添加，请重新输入");
                            return;
                        }
                        else if (!userExcels.Contains(userExcelM))
                        {
                            MessageBox.Show("该成员不在列表中,开始添加信息");
                            ShowUserExcel(userExcelM);
                            userExcels.Add(userExcelM);
                        }
                    }
                    AddStudentToListBtn.Enabled = false;
                    UserExcelData.AllowUserToAddRows = false;
                }
            }
        }

        private void ShowUserExcel(UserExcel userExcelMode)
        {
            if (userExcelMode == null) return;
            else
            {
                int i = UserExcelData.Rows.Add();
                UserExcelData.Rows[i].Cells[0].Value = userExcelMode.Intra_group_serial_number;  //  循序
                UserExcelData.Rows[i].Cells[1].Value = userExcelMode.Exam_number; // 考号
                UserExcelData.Rows[i].Cells[2].Value = userExcelMode.Name;// 姓名
                UserExcelData.Rows[i].Cells[3].Value = userExcelMode.Sex; // 性别
                UserExcelData.Rows[i].Cells[4].Value = userExcelMode.School;// 学校
                UserExcelData.Rows[i].Cells[5].Value = userExcelMode.Grade;// 年级
                UserExcelData.Rows[i].Cells[6].Value = userExcelMode.ClassName;  // 班级
                UserExcelData.Rows[i].Cells[7].Value = userExcelMode.Number_class;  // 班级号
                UserExcelData.Rows[i].Cells[8].Value = userExcelMode.Exam_date;// 考试日期
                UserExcelData.Rows[i].Cells[9].Value = userExcelMode.Sessions; // 场次
                UserExcelData.Rows[i].Cells[10].Value = userExcelMode.Project;  // 项目
                UserExcelData.Rows[i].Cells[11].Value = userExcelMode.Project_team;// 项目组
                UserExcelData.Rows[i].Cells[12].Value = userExcelMode.Venue;   // 场地
                UserExcelData.Rows[i].Cells[13].Value = userExcelMode.Achievement_one;//成绩1 
                UserExcelData.Rows[i].Cells[14].Value = userExcelMode.Achievement_two;// 成绩2 
                UserExcelData.Rows[i].Cells[15].Value = userExcelMode.Achievement_three;   //成绩3 
                UserExcelData.Rows[i].Cells[16].Value = userExcelMode.Achievement_four;//成绩4 
                UserExcelData.Rows[i].Cells[17].Value = userExcelMode.Remarks;  // 备注
                UserExcelData.Rows[i].Cells[18].Value = userExcelMode.Region;// 地区
                UserExcelData.Rows[i].Cells[19].Value = userExcelMode.Group_number;
            }
        }

        // 检测列表个数
        private void CheckExcelModelToList()
        {
            int count;
            if (userExcels == null)
            {
                SetGroupOrderInput(0.ToString());
                return;
            }
            else
            {
                count = userExcels.Count;
                SetGroupOrderInput(count.ToString()); return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        private void SetGroupOrderInput(string v)
        {
            groupOrderInput.Text = v;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupNumberInput_MouseLeave(object sender, EventArgs e)
        {
            if (userExcels == null)
            {
                string groupID = GroupNumText.Text.Trim();

                if (!string.IsNullOrEmpty(groupID))
                {
                    NewGroupSys.Req_GetGroup(groupID);
                }
                else
                {
                    MessageBox.Show("请先输入组号！！");
                    return;
                }
            }
            else
            {
                for (int i = 0; i < userExcels.Count; i++)
                {
                    string s = userExcels[i].Exam_number;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            if (userExcels == null)
            {
                MessageBox.Show("清先添加数据");
                return;
            }
            else
            {
                ClearDataInput(); // 清空数据
                if (IsEndOfAddUserDataToList == false)
                {
                    SetGroupID();
                    CheckExcelModelToList();
                    MessageBox.Show("请将剩下的数据填写完整后按下添加按钮！！");
                    AddStudentToListBtn.Enabled = true;
                    UserExcelData.AllowUserToAddRows = true;
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        private void SetGroupID()
        {
            if (userExcels == null)
            {
                return;
            }
            else
            {
                GroupNumText.ReadOnly = true;
                GroupNumText.Text = groupid;
            }
        }
        /// <summary>
        /// 清空数据
        /// </summary>
        private void ClearDataInput()
        {
            GroupNumText.Text = string.Empty;
            groupOrderInput.Text = string.Empty;
            NameInput.Text = string.Empty;
            ExamNumberInput.Text = string.Empty;
            RegionInput.Text = string.Empty;
            classNameInput.Text = string.Empty;
            GradeInput.Text = string.Empty;
            NumClassInput.Text = string.Empty;
            SesetionInput.Text = string.Empty;
            SchoolInput.Text = string.Empty;
        }
        int index;
        UserExcel CurrentStudentUserExcel = null;
        private void UserExcelData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    index = e.RowIndex;
                    CurrentStudentUserExcel = SetUserExcelDatas(UserExcelData, index);
                    if (CurrentStudentUserExcel == null)
                    {
                        MessageBox.Show("请选择考生！！");
                        return;
                    }
                    
                }
                //查找是否已经导入了人脸数据
               //SelectFaceDataInFaceView(index ,);
            }
            
        }


        /// <summary>
        /// 上一位按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopStudentBtn_Click(object sender, EventArgs e)
        {
            if (index == 0)
            {
                MessageBox.Show("当前是第一位考生，无法上一位");
                return;

            }
            else
            {
                CurrentStudentUserExcel = null;
                UserExcelData.Rows[index].DefaultCellStyle.BackColor = Color.White;
                index = index - 1;
                CurrentStudentUserExcel = SetUserExcelDatas(UserExcelData, index);
            }
        }
        /// <summary>
        /// 下一位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextStudentBtn_Click(object sender, EventArgs e)
        {
            if (CurrentStudentUserExcel == null)
            {
                MessageBox.Show("请选择人员数据！！");
                return;
            }
            else
            {
                if (index == userExcels.Count - 1)
                {
                    MessageBox.Show("当前已经是本组最后一个考生数据，无法下一位！！");
                    return;
                }
                else
                {
                    CurrentStudentUserExcel = null;
                    UserExcelData.Rows[index].DefaultCellStyle.BackColor = Color.White;
                    index += index;
                    SetUserExcelDatas(UserExcelData, index);

                }
            }
        }
        /// <summary>
        ///  删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelectCurrentStudentBtn_Click(object sender, EventArgs e)
        {
            if (CurrentStudentUserExcel == null)
            {
                MessageBox.Show("当前选择的数据空，无法删除");
                return;
            }
            else
            {
                imageLists.Images.RemoveAt(index);
                GroupListFaceView.Refresh();
                leftImageFeatureList.RemoveAt(index);
                // 还得有一个操作，删除本地图片文件
                if (groupDirectory == null)
                {
                    return;
                }
                else
                {
                    Directory.Delete(groupDirectory, true);
                }

            }

        }
        /// <summary>
        /// 人员库图片选择 锁对象
        /// </summary>
        private object chooseImgLocker = new object();
        /// <summary>
        /// 保存对比图片的列表
        /// </summary>
        private List<string> imagePathList = new List<string>();
        /// <summary>
        /// 选择人脸数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseFaceDataBtn_Click(object sender, EventArgs e)
        {
            if (CurrentStudentUserExcel == null)
            {
                MessageBox.Show("清先选择人脸数据");
                return;
            }
            else
            {
                List< Sunny.UI.UIButton> btns = new List< Sunny.UI.UIButton>();
                btns.Add(NextStudentBtn);
                btns.Add(AddStudentToListBtn);
                btns.Add(TopStudentBtn);    
                btns.Add(SucessAddStudent);
                btns.Add(ContinueAddBtn);
                btns.Add(ChooseFaceDataBtn);
                btns.Add(ClearFaceGroupBtn);
                btns.Add(DelectCurrentStudentBtn);

                FaceRegister(uiTextBox2, false, imageLists, GroupListFaceView, btns);
            }
        }
        /// <summary>
        /// 禁用按钮
        /// </summary>
        /// <param name="v"></param>
        /// <param name="controls"></param>
        private void ControlsEnable(bool v, params Control[] controls)
        {
            try
            {
                if (controls == null || controls.Length <= 0)
                {
                    return;
                }
                foreach (Control control in controls)
                {
                    control.Enabled = v;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("禁用或者开启按钮异常" + ex.Message);
                return;
            }
        }
        private void ClearFaceGroupBtn_Click(object sender, EventArgs e)
        {

            imageLists.Images.Clear();
            GroupListFaceView.Items.Clear();
            leftImageFeatureList.Clear();
            //  还需要根据组号去删除文件 还没写 
            DelectDirectory(groupid);


            GC.Collect();
        }
        /// <summary>
        ///  删除文件夹
        /// </summary>
        /// <param name="groupid"></param>
        private void DelectDirectory(string groupid)
        {
            string path = FaceDirectory + "/" + groupid;
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            else
            {
                return;
            }
        }
        /// <summary>
        ///  创建对应组的文件夹
        /// </summary>
        /// <param name="groupId"></param>
        public string CreateFaceGroupFile(string groupId)
        {
            if (!System.IO.Directory.Exists(FaceDirectory))
            {
                Directory.CreateDirectory(FaceDirectory);
            }

            String path = FaceDirectory + "/" + groupid;
            var l = Directory.CreateDirectory(path);
            groupDirectory = l.FullName;
            return groupDirectory;

        }
        /// <summary>
        /// 存贮人脸图
        /// </summary>
        /// <param name="groupid">组号</param>
        /// <param name="imagePath">图片路径</param>
        private void SaveImageToFace(string groupid, List<string> imagePath)
        {
            var dic = CreateFaceGroupFile(groupid);
            imageData.SaveImageFileToDestion(dic, imagePath);

        }
        /// <summary>
        /// 完成添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SucessAddStudent_Click(object sender, EventArgs e)
        {
            IsEndOfAddUserDataToList = true;
            groupid = String.Empty;
            GroupNumText.ReadOnly = false;
            if (userExcels == null)
            {
                MessageBox.Show("组列表为空，不能完成添加");
            }
            else
            {
                NewGroupSys.Req_UpDateUserExcel(userExcels);
            }


        }
        #endregion

        #region 文件导入
        List<string> sheetTable = new List<string>();
        private void ChooseFileBtn_Click(object sender, EventArgs e)
        {
            ChooseFileBtn.Enabled = false;
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
            if (LocalDialog.GetOpenFileName(openFileName))    // 打开本地文件
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
            else
            {
                ChooseFileBtn.Enabled = true;
            }



        }

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

                FilePathInput.Text = null;
                SheetDataTableDrop.Items.Clear();
                MessageBox.Show("这不是xlsx文件，或xls文件已损坏，也可能是该文件正在被其它应用程序访问");
                return;
            }
        }
        DataTable dataTable = new DataTable();

        /// <summary>
        /// 浏览按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewDataBtn_Click(object sender, EventArgs e)
        {
            var s = FilePathInput.Text;
            var sl = SheetDataTableDrop.Text;
            if (UserDataView.DataSource != null)
            {
                UserDataView.DataSource = null;
            }
            if (!string.IsNullOrEmpty(s))
            {
                if (!string.IsNullOrEmpty(sl))
                {
                     
                     ViewListExamineeDataMode(s, sl);
                    
                    if (dataTable != null)
                    {
                        UserDataView.DataSource = dataTable;
                        ViewDataBtn.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("打开excel 失败，请确定文件是否毁坏，或者路径是否正确！");
                        return;

                    }
                }
                else
                {
                    MessageBox.Show("请先选择你需要打开的数据表！！");

                    return;
                }
            }
            else
            {
                MessageBox.Show("请先选择文件路径！！");
                return ;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="sl"></param>
        private void ViewListExamineeDataMode(string s, string sl)
        {
            DataTable dt = new DataTable();
            ExcelToDataSet excelToDataSet = new ExcelToDataSet();
            dt = excelToDataSet.GetExcelDatable(s, sl);
            dataTable = dt;
            GetFileImportData();
            GetFileImportDataInGroupID();
        }
        /// <summary>
        /// 获取文件数据中的组信息
        /// </summary>
        private void GetFileImportDataInGroupID()
        {
            if (userExcels == null)
            {
                return;
            }
            else
            {
                for(int i = 0; i < userExcels.Count-1; i++)
                {
                    if (userExcels[i].Group_number != userExcels[i + 1].Group_number)
                    {
                        MessageBox.Show("当前导入的数据为多组的形式，无法导入！！请重新选择");
                        ClearData();
                        return;
                    }
                    else
                    {
                        groupid = userExcels[i].Group_number;
                    }
                }
            }
        }
        /// <summary>
        /// 清除信息
        /// </summary>
        private void ClearData()
        {
            ChooseFileBtn.Enabled = true;
            FilePathInput.Text = string.Empty;
            ViewDataBtn.Enabled = true;
            SheetDataTableDrop.Items.Clear();
            UserDataView.DataSource = null;

        }

        /// <summary>
        /// 获取导入的数据中的组号
        /// </summary>
        private void GetFileImportData()
        {
            if (dataTable == null)
            {
                return;
            }
            else
            {
                userExcels = new List<UserExcel>();
                for (int i = 1; i < dataTable.Rows.Count; i++)
                {
                    CurrentStudentUserExcel = new UserExcel()
                    {
                        Exam_number = dataTable.Rows[i][0].ToString(),
                        Region = dataTable.Rows[i][1].ToString(),
                        Venue = dataTable.Rows[i][2].ToString(),
                        Project_team = dataTable.Rows[i][3].ToString(),
                        Name = dataTable.Rows[i][4].ToString(),
                        Sex = dataTable.Rows[i][5].ToString(),
                        School = dataTable.Rows[i][6].ToString(),
                        Grade = dataTable.Rows[i][7].ToString(),
                        ClassName = dataTable.Rows[i][8].ToString(),
                        Number_class = dataTable.Rows[i][9].ToString(),
                        Project = dataTable.Rows[i][10].ToString(),
                        Exam_date = dataTable.Rows[i][11].ToString(),
                        Sessions = dataTable.Rows[i][12].ToString(),
                        Group_number = dataTable.Rows[i][13].ToString(),
                        Intra_group_serial_number = dataTable.Rows[i][14].ToString(),
                        Achievement_one = dataTable.Rows[i][15].ToString(),
                        Achievement_two = dataTable.Rows[i][16].ToString(),
                        Achievement_three = dataTable.Rows[i][17].ToString(),
                        Achievement_four = dataTable.Rows[i][18].ToString(),
                        Remarks = dataTable.Rows[i][19].ToString(),
                    };
                    userExcels.Add(CurrentStudentUserExcel);
                }
            }   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChoosefileImageBtn_Click(object sender, EventArgs e)
        {
            if (UserDataView.DataSource != null)
            {
                List<Sunny.UI.UIButton> btsn = new List<Sunny.UI.UIButton>();
                btsn.Add(ChooseFileBtn);
                btsn.Add(ViewDataBtn);
                btsn.Add(ChoosefileImageBtn);
                btsn.Add(uiButton2);
                FaceRegister(uiTextBox1, true, imagelist2, listView1, btsn);
            }
            else
            {
                MessageBox.Show("请先导入组数据！！");
                return;
            }
            
        }
      
        /// <summary>
        /// /
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void UserDataView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    index = e.RowIndex;
                    CurrentStudentUserExcel = SetUserExcelDatas(UserDataView, index);
                    if (CurrentStudentUserExcel == null)
                    {
                        MessageBox.Show("请选择考生！！");
                        return;
                    }
                    SetListGroupHightLight(index);
                }
            }
        }
        /// <summary>
        ///  设置人脸数据选中
        /// </summary>
        /// <param name="index"></param>
        private void SetListGroupHightLight(int index)
        {
            if(listView1.Items.Count == 0)
            {
                 return;
            }
            else
            {
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private UserExcel SetUserExcelDatas(DataGridView dataGridView, int index)
        {
            if (index != -1)
            {
                dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.LightBlue;
            }
            UserExcel userExcel = new UserExcel()
            {
                Intra_group_serial_number = dataGridView.Rows[index].Cells[0].Value.ToString(),
                Exam_number = dataGridView.Rows[index].Cells[1].Value.ToString(),
                Name = dataGridView.Rows[index].Cells[2].Value.ToString(),
                Sex = dataGridView.Rows[index].Cells[3].Value.ToString(),
                School = dataGridView.Rows[index].Cells[4].Value.ToString(),
                Grade = dataGridView.Rows[index].Cells[5].Value.ToString(),
                ClassName = dataGridView.Rows[index].Cells[6].Value.ToString(),
                Number_class = dataGridView.Rows[index].Cells[7].Value.ToString(),
                Exam_date = dataGridView.Rows[index].Cells[8].Value.ToString(),
                Sessions = dataGridView.Rows[index].Cells[9].Value.ToString(),
                Project = dataGridView.Rows[index].Cells[10].Value.ToString(),
                Project_team = dataGridView.Rows[index].Cells[11].Value.ToString(),
                Venue = dataGridView.Rows[index].Cells[12].Value.ToString(),
                Achievement_one = dataGridView.Rows[index].Cells[13].Value.ToString(),
                Achievement_two = dataGridView.Rows[index].Cells[14].Value.ToString(),
                Achievement_three = dataGridView.Rows[index].Cells[15].Value.ToString(),
                Achievement_four = dataGridView.Rows[index].Cells[16].Value.ToString(),
                Remarks = dataGridView.Rows[index].Cells[17].Value.ToString(),
                Region = dataGridView.Rows[index].Cells[18].Value.ToString(),
                Group_number = dataGridView.Rows[index].Cells[19].Value.ToString(),
            };
            return userExcel;
        }
        /// <summary>
        ///  人脸注册
        /// </summary>
        /// <param name="uITextBox"></param>
        /// <param name="a"></param>
        /// <param name="imageList"></param>
        /// <param name="listView"></param>
        private void FaceRegister(Sunny.UI.UITextBox uITextBox, bool a, ImageList imageList, ListView listView, List<Sunny.UI.UIButton> btns)
        {
            try
            {
                lock (chooseImgLocker)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Title = "选择图片";
                    openFileDialog.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.png";
                    openFileDialog.Multiselect = a;  // 是否可以选择多个
                    openFileDialog.FileName = string.Empty;
                    GroupListFaceView.Refresh();
                    if (openFileDialog.ShowDialog().Equals(DialogResult.OK))
                    {
                        var s = openFileDialog.FileNames;
                        uITextBox.Text = openFileDialog.FileName;
                        foreach (var sl in s)
                        {
                            imagePath.Add(sl);
                        }
                        //SaveImageToFace(groupid, imagePath);
                        List<string> imagePathListTemp = new List<string>();
                        int isGoodImage = index;
                        int numStart = imagePathList.Count;
                        //人脸检测以及提取人脸特征
                        ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                        {
                            Invoke(new Action(delegate
                            {
                                foreach(var btn in btns)
                                {
                                    ControlsEnable(false, btn); 
                                }

                            }));
                            string[] fileNames = s;
                            //保存图片路径并显示
                            for (int i = 0; i < fileNames.Length; i++)
                            {
                                //图片格式判断
                                if (imageData.CheckImage(fileNames[i]))
                                {
                                    imagePathListTemp.Add(fileNames[i]);
                                }
                            }
                            //人脸检测和剪裁
                            for (int i = 0; i < imagePathListTemp.Count; i++)
                            {
                                Image image = ImageUtil.ReadFromFile(imagePathListTemp[i]);
                                //校验图片宽高
                                imageData.CheckImageWidthAndHeight(ref image);
                                if (image == null)
                                {
                                    continue;
                                }
                                //调整图像宽度，需要宽度为4的倍数
                                if (image.Width % 4 != 0)
                                {
                                    image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
                                }
                                //提取特征判断
                                int featureCode = -1;
                                SingleFaceInfo singleFaceInfo = new SingleFaceInfo();
                                FaceFeature feature = FaceUtil.ExtractFeature(imageEngine, image, out singleFaceInfo, ref featureCode);
                                if (featureCode != 0)
                                {
                                    this.Invoke(new Action(delegate
                                    {
                                        // AppendText("未检测到人脸");
                                    }));
                                    if (image != null)
                                    {
                                        image.Dispose();
                                        continue;
                                    }
                                }
                                //人脸检测
                                MultiFaceInfo multiFaceInfo;
                                int retCode = imageEngine.ASFDetectFacesEx(image, out multiFaceInfo);
                                //判断检测结果
                                if (retCode == 0 && multiFaceInfo.faceNum > 0)
                                {
                                    //多人脸时，默认裁剪第一个人脸
                                    imagePathList.Add(imagePathListTemp[i]);
                                    MRECT rect = multiFaceInfo.faceRects[0];
                                    image = ImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
                                }
                                else
                                {
                                    this.Invoke(new Action(delegate
                                    {
                                        //AppendText("未检测到人脸");
                                    }));
                                    if (image != null)
                                    {

                                        image.Dispose();
                                    }
                                }
                                //显示人脸
                                this.Invoke(new Action(delegate
                                {
                                    if (image == null)
                                    {
                                        image = ImageUtil.ReadFromFile(imagePathListTemp[i]);
                                        //校验图片宽高
                                        imageData.CheckImageWidthAndHeight(ref image);
                                    }
                                    imageList.Images.Add(imagePathListTemp[i], image);
                                    listView.Items.Add((numStart + isGoodImage) + "号", imagePathListTemp[i]);
                                    listView.Refresh();
                                    var sls = $"left:{singleFaceInfo.faceRect.left},right:{singleFaceInfo.faceRect.right},top：{singleFaceInfo.faceRect.top},bottom:{singleFaceInfo.faceRect.bottom},orient:{singleFaceInfo.faceOrient}";
                                    Console.WriteLine($"已提取{(numStart + isGoodImage)}号人脸特征值为:" + s);
                                    isGoodImage++;
                                    if (image != null)
                                    {
                                        image.Dispose();
                                    }
                                }));
                            }
                        }));
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogInfo(GetType(), ex);
            }

        }
        private void uiButton2_Click(object sender, EventArgs e)
        {

        }
        #endregion


    }
}










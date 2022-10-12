using AForge.Math.Metrics;
using ArcFaceSDK;
using ArcFaceSDK.Entity;
using ArcFaceSDK.SDKModels;
using ArcFaceSDK.Utils;
using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.Arcsoft;
using ArcSoftFace.GameCommon;
using ArcSoftFace.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem.ADCoreGameWindow
{
    public partial class StartTestingWindow : Form
    {
        public StartTestingSys StartTestingSys = new StartTestingSys();
        private List<UserExcelMode> userExcel =  new List<UserExcelMode>();
        private CameraConnect cameraConnect= new CameraConnect();
        private ImageData ImageData = new ImageData();
        LocalFile localFile = new LocalFile();
        string path = Application.StartupPath + GameConst.FaceDirectory;
        FaceEngine FaceEngine = new FaceEngine();

        /// <summary>
        /// 人脸库人脸特征列表
        /// </summary>
        private List<FaceFeature> leftImageFeatureList = new List<FaceFeature>();
        public StartTestingWindow()
        {
            InitializeComponent();
        }

        private void StartTestingWindow_Load(object sender, EventArgs e)
        {
            ArcFaceManage.Instance.InitEngines();
            var s = FaceEngine.pEngine;
            StartTestingSys.ReqGetExamTime();
            // 获取外接显示器的数量
            StartTestingSys.GetScreenCount();



        }
        /// <summary>
        ///  查询按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectBtn_Click(object sender, EventArgs e)
        {
             var S  = ExamNumInput.Text.Trim();
            if (string.IsNullOrEmpty(S))
            {
                MessageBox.Show("请先输入考生考号！！");
                return;
            }
            else
            {
                StartTestingSys.Req_TestnumberInquiry(S);
            }
        }
        /// <summary>
        /// 设置时间下拉框
        /// </summary>
        /// <param name="startTestingExamDatas"></param>
        public void SetTimedroup(List<StartTestingExamDataModel> startTestingExamDatas)
        {
            if (startTestingExamDatas != null)
            {
                foreach (var l in startTestingExamDatas)
                {
                    ExamTimeDrop.Items.Add(l.Exam_date);
                }
            }
            else
            {
                MessageBox.Show("数据库已被改变，请重新导入！！");
                return;
            }
        }
        /// <summary>
        ///  浏览数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewDataBtn_Click(object sender, EventArgs e)
        {
            string examTime = ExamTimeDrop.Text.Trim();
            string groupId = GroupIDDrop.Text.Trim();

            if (!string.IsNullOrEmpty(examTime) && !string.IsNullOrEmpty( groupId))
            {
                StartTestingSys.GetCurrentGroupMsg(examTime, groupId);
            }
        }
        /// <summary>
        /// 获取组信息的回调
        /// </summary>
        /// <param name="groupData"></param>
        public void Rsp_GetGroupMent(List<GroupDataModel> groupData)
        {
            if (groupData == null)
            {
                MessageBox.Show("当前数据库可能已经发生改变，没有查找到对应的组的数据");
                return;
            }
            else
            {
                foreach (var item in groupData)
                {
                    GroupIDDrop.Items.Add(item.Group_number);
                }
            }

             
             
        }
        /// <summary>
        /// 根据组 的信息查找人脸数据
        /// </summary>
        private void GetFaceList()
        {
            // 在根据组，拿到人脸特征值以及图片
            var groupid = GroupIDDrop.Text.Trim();
            if (!string.IsNullOrEmpty(groupid))
            {
                StartTestingSys.Req_GetFeature(groupid);
            }
            else
            {
                MessageBox.Show("请确定组号！！");
                return;
            }
        }

        /// <summary>
        /// 获取当前信息的回调
        /// </summary>
        /// <param name="userExcels"></param>
        public  void Rsp_GetCurrentGroupMsg(List<UserExcelMode> user)
        {
            if (user != null || user.Count > 0)
            {
                GroupDataView.DataSource = user;
                userExcel = user;
            }
            else
            {
                MessageBox.Show("当前数据表不存在或者数据库已更改！！");
                return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userExcelModes"></param>
        public void Rsp_TestNumberInquiry(List<UserExcelMode> userExcelModes)
        {
            GroupDataView.DataSource = null;
            if (userExcelModes == null || userExcelModes.Count==0)
            {
                return;
            }
            else
            {
                 
                GroupDataView.DataSource = userExcelModes;
            }
            userExcel = userExcelModes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupIDDrop_DoubleClick(object sender, EventArgs e)
        {
            string examTime = ExamTimeDrop.Text.Trim();
            StartTestingSys.Req_GetGroupMent(examTime);
        }
        /// <summary>
        ///  开始相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartCameraBtn_Click(object sender, EventArgs e)
        {
            cameraConnect.CameraInit();
            ChooseImageBtn.Enabled = false;
            StartCameraBtn.Enabled = false;
            cameraConnect.OtherCamera(rgbVideoSource, irVideoSource, txtThreshold, FaceList, leftImageFeatureList,FaceImageList);

        }
        /// <summary>
        /// 画图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rgbVideoSource_Paint(object sender, PaintEventArgs e)
        {
            cameraConnect.PaintImage(rgbVideoSource,e);
        }

        private void rgbVideoSource_PlayingFinished(object sender, AForge.Video.ReasonToFinishPlaying reason)
        {
            cameraConnect.rgbVideoSource_PlayingFinished();
        }
        public  static int  cameraId= 0 ;
       
        /// <summary>
        /// 保存右侧图片路径
        /// </summary>
        private string image1Path;
        /// <summary>
        /// 图片最大大小限制
        /// </summary>
        private long maxSize = 1024 * 1024 * 2;

        /// <summary>
        /// 最大宽度
        /// </summary>
        private int maxWidth = 1536;
        private FaceEngine faceEngine = new FaceEngine();
        private List<FaceFeature> rightImageFeatureList = new List<FaceFeature>();
        /// <summary>
        ///  选择识别图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseImageBtn_Click(object sender, EventArgs e)
        {
            ChooseImageBtn.Enabled = false;
            StartCameraBtn.Enabled = false;
            try
            {
               if (!faceEngine.GetEngineStatus())
                {
                    MessageBox.Show("请先初始化引擎!");
                    ChooseImageBtn.Enabled = true;
                    StartCameraBtn.Enabled = true;
                    return;
                }
               
                var image1Path = localFile.openLocalFile(false);
                foreach (var image in image1Path)
                {
                    //检测图片格式
                    if (!ImageData.CheckImage(image))
                    {
                        return;
                    }
                    DateTime detectStartTime = DateTime.Now;
                    //获取文件，不能是过大的文件图片
                    FileInfo fileInfo = new FileInfo(image);
                    if (fileInfo.Length > maxSize)
                    {
                        MessageBox.Show("图片文件最大为2MB,请先压缩后导入！！");
                        ChooseImageBtn.Enabled = true;
                        StartCameraBtn.Enabled = true;
                        return;
                    }
                    Image scrImage = ImageUtil.ReadFromFile(image);
                    ImageData.CheckImageWidthAndHeight(ref scrImage);
                    if (scrImage == null)
                    {
                        MessageBox.Show("图片获取失败，请稍后重试！！");
                        ChooseImageBtn.Enabled = true;
                        StartCameraBtn.Enabled = true;

                        return;
                    }//调整图片的宽度，为4 的倍数   
                    if (scrImage.Width % 4 != 0)
                    {
                        scrImage = ImageUtil.ScaleImage(scrImage, scrImage.Width - (scrImage.Width % 4), scrImage.Height);

                    }
                    //人脸检测
                    MultiFaceInfo multiFaceInfo;
                    int retCode = faceEngine.ASFDetectFacesEx(scrImage, out multiFaceInfo);
                    if (retCode != 0)
                    {
                        MessageBox.Show("图像人脸检测失败，请稍后重试!");
                        ChooseImageBtn.Enabled = true;
                        StartCameraBtn.Enabled = true;
                        return;
                    }
                    if (multiFaceInfo.faceNum < 1)
                    {
                        scrImage = ImageUtil.ScaleImage(scrImage, picImageCompare.Width, picImageCompare.Height);
                        //rightImageFeatureList.Clear();
                        picImageCompare.Image = scrImage;
                        leftImageFeatureList.Clear();
                        picImageCompare.Image = scrImage;
                        return;
                    }
                    // 年龄检测
                    int retAge = -1;
                    AgeInfo ageInfo = FaceUtil.AgeEstimation(faceEngine, scrImage, multiFaceInfo, out retAge);

                    //性别检测
                    int retCode_Gender = -1;
                    GenderInfo genderInfo = FaceUtil.GenderEstimation(faceEngine, scrImage, multiFaceInfo, out retCode_Gender);
                    //3DAngle检测
                    int retCode_3DAngle = -1;
                    Face3DAngle face3DAngleInfo = FaceUtil.Face3DAngleDetection(faceEngine, scrImage, multiFaceInfo, out retCode_3DAngle);
                    MRECT[] mrectTemp = new MRECT[multiFaceInfo.faceNum];
                    int[] ageTemp = new int[multiFaceInfo.faceNum];
                    int[] genderTemp = new int[multiFaceInfo.faceNum];
                    SingleFaceInfo singleFaceInfo;

                    //标记出检测到的人脸
                    for (int i = 0; i < multiFaceInfo.faceNum; i++)
                    {
                        MRECT rect = multiFaceInfo.faceRects[i];
                        int orient = multiFaceInfo.faceOrients[i];
                        int age = 0;
                        //年龄检测
                        if (retAge != 0)
                        {
                           Console.WriteLine(string.Format("年龄检测失败，返回{0}!", retAge));
                        }
                        else
                        {
                            age = ageInfo.ageArray[i];
                        }
                        //性别检测
                        int gender = -1;
                        if (retCode_Gender != 0)
                        {
                           Console.WriteLine (string.Format("性别检测失败，返回{0}!", retCode_Gender));
                        }
                        else
                        {
                            gender = genderInfo.genderArray[i];
                        }
                        //3DAngle检测
                        int face3DStatus = -1;
                        float roll = 0f;
                        float pitch = 0f;
                        float yaw = 0f;
                        if (retCode_3DAngle != 0)
                        {
                            // AppendText(string.Format("3DAngle检测失败，返回{0}!", retCode_3DAngle));
                        }
                        else
                        {
                            //角度状态 非0表示人脸不可信
                            face3DStatus = face3DAngleInfo.status[i];
                            //roll为侧倾角，pitch为俯仰角，yaw为偏航角
                            roll = face3DAngleInfo.roll[i];
                            pitch = face3DAngleInfo.pitch[i];
                            yaw = face3DAngleInfo.yaw[i];
                        }
                        FaceFeature feature = FaceUtil.ExtractFeature(faceEngine, scrImage, out singleFaceInfo, ref retCode, i);
                        //提取人脸特征
                        rightImageFeatureList.Add(feature);
                        mrectTemp[i] = rect;
                        ageTemp[i] = age;
                        genderTemp[i] = gender;
                       Console.WriteLine(string.Format("{0} - 第{12}人脸坐标:[left:{1},top:{2},right:{3},bottom:{4},orient:{5},roll:{6},pitch:{7},yaw:{8},status:{11}] Age:{9} Gender:{10}", 
                           DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), rect.left, rect.top, rect.right, rect.bottom, orient, roll, pitch, yaw, age, (gender >= 0 ? gender.ToString() : ""), face3DStatus, i));
                        
                    }
                    Console.WriteLine(string.Format("------------------------------检测结束，时间:{0}------------------------------", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                    
                    //获取缩放比例
                    float scaleRate = ImageUtil.GetWidthAndHeight(scrImage.Width, scrImage.Height, picImageCompare.Width, picImageCompare.Height);
                    //缩放图片
                    scrImage = ImageUtil.ScaleImage(scrImage, picImageCompare.Width, picImageCompare.Height);
                    //添加标记
                    scrImage = ImageUtil.MarkRectAndString(scrImage, mrectTemp, ageTemp, genderTemp, picImageCompare.Width, scaleRate, multiFaceInfo.faceNum);
                    //显示标记后的图像
                    picImageCompare.Image = scrImage;
                    
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        private UserExcelMode CurentUserExcelMode = null;
        /// <summary>
        /// 缺考按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MissingBtn_Click(object sender, EventArgs e)
        {
            string S  = GradeDroup.Text;
            if (string.IsNullOrEmpty(S))
            {
                MessageBox.Show("请选择你需要设置的成绩选项！！");
                return;

            }
            else
            {
                if(CurentUserExcelMode == null)
                {
                    MessageBox.Show("请先选择考生数据！！");
                    return;
                }
                else
                {
                    SetUserGradeData(S, "缺考");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="v"></param>
        private void SetUserGradeData(string s, string v)
        {
             if(CurentUserExcelMode==null)
            {
                MessageBox.Show("请选择考生数据！！");
                return;
            }
            else
            {
                switch(s)
                {
                    case "成绩1":
                        CurentUserExcelMode.Achievement_one = v;
                        break;
                    case "成绩2":
                        CurentUserExcelMode.Achievement_two = v;
                        break;
                    case "成绩3":
                        CurentUserExcelMode.Achievement_three = v;
                        break;
                    case "成绩4":
                        CurentUserExcelMode.Achievement_four = v;
                        break;

                }
                StartTestingSys.ReqModify_Grades(CurentUserExcelMode);
            }
        }

        /// <summary>
        /// 犯规按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FoulBtn_Click(object sender, EventArgs e)
        {
            var S = GradeDroup.Text;
            if (string.IsNullOrEmpty(S))
            {
                MessageBox.Show("请先选择你需要设置的成绩数据！！");
                return;
            }
            else
            {
                if (CurentUserExcelMode == null)
                {
                    MessageBox.Show("请选择考生数据！");
                    return;
                }
                else
                {
                    SetUserGradeData(S, "犯规");
                }
            }
        }
        /// <summary>
        /// 重置按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            var S = GradeDroup.Text;
            if (string.IsNullOrEmpty(S))
            {
                MessageBox.Show("请先选择你需要设置的成绩数据！！");
                return;
            }
            else
            {
                if (CurentUserExcelMode == null)
                {
                    MessageBox.Show("请选择考生数据！");
                    return;
                }
                else
                {
                    SetUserGradeData(S, "null");
                }

            }
        }
        /// <summary>
        /// 弃权按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Waiverbtn_Click(object sender, EventArgs e)
        {
            var S = GradeDroup.Text;
            if (string.IsNullOrEmpty(S))
            {
                MessageBox.Show("请先选择你需要设置的成绩数据！！");
                return;
            }
            else
            {
                if (CurentUserExcelMode == null)
                {
                    MessageBox.Show("请选择考生数据！");
                    return;
                }
                else
                {
                    SetUserGradeData(S, "弃权");
                }
            }
        }
            /// <summary>
            /// 刷新按钮点击
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        private void Refreshbtn_Click(object sender, EventArgs e)
        {
            StartTestingSys.GetCurrentGroupMsg(ExamTimeDrop.Text.Trim(), GroupIDDrop.Text.Trim());
        }
        /// <summary>
        /// 下一位考生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextCandidateBtn_Click(object sender, EventArgs e)
        {
            if (CurentUserExcelMode == null)
            {
                MessageBox.Show("请先选择数据");
                return;
            }
            else
            {
                if (btnName == userExcel.Count - 1)
                {
                    MessageBox.Show("当前已经是最后一个数据，无法下一位");
                    return ;
                }
                else
                {
                    SetGroupDataGridViewWhite(btnName);
                    btnName++;
                    CurentUserExcelMode =  SetCurrentUserExcelData(btnName, GroupDataView);
                }
            }
        }
        /// <summary>
        /// 设置 左侧list 处于不选中状态
        /// </summary>
        /// <param name="btnName"></param>
        
        private void SetGroupDataGridViewWhite(int index)
        {
             GroupDataView.Rows[index].DefaultCellStyle.BackColor = Color.Gray;
             SetFaceGroupListWhite(index);
        }
        /// <summary>
        ///  将人脸信息取消选中
        /// </summary>
        /// <param name="index"></param>
        
        private void SetFaceGroupListWhite(int index)
        {
             
        }

        /// <summary>
        /// 上一位考生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastCandidateBtn_Click(object sender, EventArgs e)
        {

        }
        private int btnName;
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="userExcelMode"></param>
        public  void UpdateExcelDataModeInTable(UserExcelMode userExcelMode)
        {
            int index = btnName;
            userExcel[index] = userExcelMode;
            GroupDataView.DataSource = null;
            StartTestingSys.GetCurrentGroupMsg(ExamTimeDrop.Text, GroupIDDrop.Text);


        }
        int index = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupDataView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex >= -1 && e.ColumnIndex != -1)
                {
                    index = e.RowIndex;
                    btnName = index;
                    CurentUserExcelMode = SetCurrentUserExcelData(index , GroupDataView);
                    ShowCurrentData(CurentUserExcelMode);
                    // 还需要跟 人脸库中的人脸数据进行绑定
                    SetFaceGroupListHight(index,true);
                }

            }

        }
        /// <summary>
        /// 设置facegroupList中的人脸数据选中状态
        /// </summary>
        /// <param name="index"></param>
        private void SetFaceGroupListHight(int index , bool  s)
        {
            if (FaceList.Items.Count == 0)
                return;
            else
            {
                FaceList.Items[index].Selected = s;
            }
        }

        int achieveMent;
        private void ShowCurrentData(UserExcelMode curentUserExcelMode)
        {
            if (curentUserExcelMode == null)
            {
                MessageBox.Show("请先选择考生！！");
                return;
            }
            else
            {
                ExamNum.Text = curentUserExcelMode.Exam_number;
                SchoolInput.Text = curentUserExcelMode.School;
                NameInput.Text = curentUserExcelMode.Name;
                GradeTextInput.Text = curentUserExcelMode.Grade;
                SexInput.Text = curentUserExcelMode.Sex;
                ClassInput.Text = curentUserExcelMode.ClassName;
                if (int.TryParse(curentUserExcelMode.Achievement_one, out achieveMent))
                {
                    GradeOneText.Text = achieveMent.ToString();
                    GradeOneState.Text = "已测试";
                }
                else
                {
                    switch (curentUserExcelMode.Achievement_one)
                    {
                        case "缺考":
                            GradeOneText.Text = "NULL";
                            GradeOneState.Text = "缺考";
                            break;
                        case "弃权":
                            GradeOneText.Text = "NULL";
                            GradeOneState.Text = "弃权";
                            break;
                        case "犯规":
                            GradeOneText.Text = "NULL";
                            GradeOneState.Text = "犯规";
                            break;
                        default:
                            GradeOneText.Text = "NULL";
                            GradeOneState.Text = "没有测试";
                            break;
                    }
                }
                if (int.TryParse(curentUserExcelMode.Achievement_two, out achieveMent))
                {
                    GradeTwoText.Text = achieveMent.ToString();
                    GradetwoState.Text = "已测试";
                }
                else
                {
                    switch (curentUserExcelMode.Achievement_two)
                    {
                        case "缺考":
                            GradeTwoText.Text = "NULL";
                            GradetwoState.Text = "缺考";
                            break;
                        case "弃权":
                            GradeTwoText.Text = "NULL";
                            GradetwoState.Text = "弃权";
                            break;
                        case "犯规":
                            GradeTwoText.Text = "NULL";
                            GradetwoState.Text = "犯规";
                            break;
                        default:
                            GradeTwoText.Text = "NULL";
                            GradetwoState.Text = "没有测试";
                            break;
                    }
                }
                if (int.TryParse(curentUserExcelMode.Achievement_three, out achieveMent))
                {
                    GradeThreeText.Text = achieveMent.ToString();
                    GradeThreeState.Text = "已测试";

                }
                else
                {
                    switch (curentUserExcelMode.Achievement_one)
                    {
                        case "缺考":
                            GradeThreeText.Text = "NULL";
                            GradeThreeState.Text = "缺考";
                            break;
                        case "弃权":
                            GradeThreeText.Text = "NULL";
                            GradeThreeState.Text = "弃权";
                            break;
                        case "犯规":
                            GradeThreeText.Text = "NULL";
                            GradeThreeState.Text = "犯规";
                            break;
                        default:
                            GradeThreeText.Text = "NULL";
                            GradeThreeState.Text = "没有测试";
                            break;
                    }

                }
                if (int.TryParse(curentUserExcelMode.Achievement_four, out achieveMent))
                {
                    GradeFourText.Text = achieveMent.ToString();
                    GradeFourState.Text = "已测试";

                }
                else
                {
                    switch (curentUserExcelMode.Achievement_four)
                    {
                        case "缺考":
                            GradeFourState.Text = "NULL";
                            GradeFourState.Text = "缺考";
                            break;
                        case "弃权":
                            GradeFourState.Text = "NULL";
                            GradeFourState.Text = "弃权";
                            break;
                        case "犯规":
                            GradeFourState.Text = "NULL";
                            GradeFourState.Text = "犯规";
                            break;
                        default:
                            GradeFourState.Text = "NULL";
                            GradeFourState.Text = "没有测试";
                            break;
                    }

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private UserExcelMode SetCurrentUserExcelData(int index, Sunny.UI.UIDataGridView dataTable)
        {
            if (index != -1)
            {
                GroupDataView.Rows[index].DefaultCellStyle.BackColor = Color.LightSkyBlue;

            }
            UserExcelMode userExcel = new UserExcelMode() 
            {
                Id = long.Parse(dataTable.Rows[btnName].Cells[0].Value.ToString()),
                Exam_number = dataTable.Rows[btnName].Cells[1].Value.ToString(),
                Region = dataTable.Rows[btnName].Cells[2].Value.ToString(),
                Venue =dataTable.Rows[btnName].Cells[3].Value.ToString(),
                Project_team = dataTable.Rows[btnName].Cells[4].Value.ToString(),
                Name = dataTable.Rows[btnName].Cells[5].Value.ToString(),
                Sex = dataTable.Rows[btnName].Cells[6].Value.ToString(),
                School = dataTable.Rows[btnName].Cells[7].Value.ToString(),
                Grade = dataTable.Rows[btnName].Cells[8].Value.ToString(),
                ClassName = dataTable.Rows[btnName].Cells[9].Value.ToString(),
                Project = dataTable.Rows[btnName].Cells[10].Value.ToString(),
                Exam_date = dataTable.Rows[btnName].Cells[11].Value.ToString(),
                Sessions = dataTable.Rows[btnName].Cells[12].Value.ToString(),
                Group_number = dataTable.Rows[btnName].Cells[13].Value.ToString(),
                Intra_group_serial_number = dataTable.Rows[btnName].Cells[14].Value.ToString(),
                Achievement_one = dataTable.Rows[btnName].Cells[15].Value.ToString(),
                Achievement_three = dataTable.Rows[btnName].Cells[16].Value.ToString(),
                Achievement_four = dataTable.Rows[btnName].Cells[17].Value.ToString(),
                Remarks = dataTable.Rows[btnName].Cells[18].Value.ToString(),
            };
            return userExcel;
        }
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupID"></param>
        private void GetFaceImageDirectory(string groupID)
        {
            if (string.IsNullOrEmpty(groupID))
            {
                MessageBox.Show("请先确定组号！！");
                return;
            }
            else
            {
                ImageModel imageList = new ImageModel();
                string paths = path + "/" + groupID;
                imageList =   ImageData.GetDirectoryImageFile(paths);
                ShowImageInFaceListView(imageList);
              // leftImageFeatureList = GetImagelistFaceFeature(imageList.images);


            }
        }
        /// <summary>
        ///  将图片显示在listview 上
        /// </summary>
        /// <param name="imageList"></param>
        private void ShowImageInFaceListView( ImageModel imageModel)
        {
            if(imageModel == null)
            {
                return;
            }
            else
            {
                for(int i = 0; i < imageModel.images.Count; i++)
                {
                    string imagePath = imageModel.ImagePath[i];// 图片路径
                    Image image = imageModel.images[i];
                    FaceImageList.Images.Add(imagePath,image);
                    FaceList.Items.Add(i.ToString()+"号", imagePath);
                    
                }
                FaceList.Refresh();
            }
             
        }
        /// <summary>
        /// 获取图片的人脸特征值
        /// </summary>
        /// <param name="imageList"></param>
       
        private List<FaceFeature> GetImagelistFaceFeature(List<Image> imageList)
        {
            List<FaceFeature> features = new List<FaceFeature>();
            for (int i = 0; i < imageList.Count; i++) 
            {
                int featureCode = -1;
                SingleFaceInfo singleFaceInfo = new SingleFaceInfo();
                FaceFeature feature = FaceUtil.ExtractFeature(faceEngine, imageList[i], out singleFaceInfo, ref featureCode);
                features.Add(feature);

            }
            return features;
            
        }

        /// <summary>
        /// 用于标记是否需要清除比对结果
        /// </summary>
       private bool isCompare = false;

        public string Number;

        private void GroupIDDrop_TextChanged(object sender, EventArgs e)
        {
            string groupID = GroupIDDrop.Text;
            GetFaceImageDirectory(groupID);
            StartTestingSys.Req_GetFacefeature(groupID);


        }
        /// <summary>
        ///  对比
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartMatchingBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (leftImageFeatureList.Count == 0)
                {
                    return;

                }
                if (rightImageFeatureList.Count == 0 ||rightImageFeatureList==null)
                {
                    MessageBox.Show("没获取到人脸数据请重新 操作");
                    return;
                }
                isCompare = true;
                for(int i = 0; i < rightImageFeatureList.Count; i++)
                {
                    float compareSimilarity = 0;
                    int compareNum = 0;
                    FaceFeature faceFeature = rightImageFeatureList[i];
                    if (faceFeature.featureSize <= 0)
                    {
                        Console.WriteLine(string.Format("对比第{0}张人脸特征值提取失败", i));
                        continue;
                    }
                    for(int j = 0; j <  leftImageFeatureList.Count; j++)
                    {
                        FaceFeature faceFeatu = leftImageFeatureList[j];
                        float sim = 0f;
                        FaceEngine.ASFFaceFeatureCompare(faceFeature,faceFeatu,out sim);
                        if (sim.ToString().IndexOf("E") > -1)
                        {
                            sim = 0F;
                        }
                        if (sim > compareSimilarity)
                        {
                            compareSimilarity = sim;
                            compareNum = i;
                        }
                        if (compareSimilarity > 0.8)
                        {
                            continue;
                        }

                    }
                    if(compareSimilarity > 0)
                    {
                        Console .WriteLine(String.Format("匹配结果为{0}",compareSimilarity));
                    }
                     
                }
                Console.WriteLine("对比结束！！");

            }
            catch(Exception ex )
            {
                Console.WriteLine("对比异常！！");
                return;
            }
        }
        /// <summary>
        /// 启动测试按钮执行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CameraIDInput.Text))
            {
                if (CurentUserExcelMode != null)
                {
                    cameraId = int.Parse(CameraIDInput.Text);
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = Application.StartupPath + @"\..\Python\SitUp\SitUp.exe";
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.RedirectStandardOutput = false;
                    process.StartInfo.RedirectStandardError = false;
                    process.StartInfo.RedirectStandardInput = false;
                    process.StartInfo.CreateNoWindow = false;
                    process.StartInfo.WorkingDirectory = Application.StartupPath + @"\..\Video\";
                    process.Start();
                }
                else
                {
                    MessageBox.Show("请先选择考生数据！！");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请输入相机id");
                return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="faceDataModes"></param>
        public   void GetLeftFaceFeature(List<FaceDataMode> faceDataModes)
        {
            for(int i = 0; i < faceDataModes.Count; i++)
            {
                byte[]  s = faceDataModes[i].FaceData;
                FaceFeature faceFeature = new FaceFeature()
                {
                    feature = s,
                    featureSize = s.Length,
                };
                leftImageFeatureList.Add(faceFeature);
            }
        }

        public  void SitUpTest(string number)
        {
            Number = number;
        }
        bool IsSitUp = false;

    }
}

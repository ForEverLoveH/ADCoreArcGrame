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
            GetFaceList();
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
            try
            {
               if (!faceEngine.GetEngineStatus())
                {
                    MessageBox.Show("请先初始化引擎!");
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
                        return;
                    }
                    Image scrImage = ImageUtil.ReadFromFile(image);
                    ImageData.CheckImageWidthAndHeight(ref scrImage);
                    if (scrImage == null)
                    {
                        MessageBox.Show("图片获取失败，请稍后重试！！");
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
                        //提取人脸特征
                        rightImageFeatureList.Add(FaceUtil.ExtractFeature(faceEngine, scrImage, out singleFaceInfo, ref retCode, i));
                        mrectTemp[i] = rect;
                        ageTemp[i] = age;
                        genderTemp[i] = gender;
                       Console.WriteLine(string.Format("{0} - 第{12}人脸坐标:[left:{1},top:{2},right:{3},bottom:{4},orient:{5},roll:{6},pitch:{7},yaw:{8},status:{11}] Age:{9} Gender:{10}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), rect.left, rect.top, rect.right, rect.bottom, orient, roll, pitch, yaw, age, (gender >= 0 ? gender.ToString() : ""), face3DStatus, i));
                    }
                    Console.WriteLine(string.Format("------------------------------检测结束，时间:{0}------------------------------", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                    

                    //清空上次的匹配结果
                    /*for (int i = 0; i < leftImageFeatureList.Count; i++)
                    {
                        .Items[i].Text = string.Format("{0}号", i);
                    }*/
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
    }
}

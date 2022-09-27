using ArcFaceSDK.Entity;
using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.GameCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        /// 
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
        /// 
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
            cameraConnect.OtherCamera(rgbVideoSource, irVideoSource, txtThreshold);

        }
    }
}

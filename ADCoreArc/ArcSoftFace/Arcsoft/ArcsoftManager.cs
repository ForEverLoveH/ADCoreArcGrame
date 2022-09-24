using AForge.Video.DirectShow;
using ArcFaceSDK.SDKModels;
using ArcFaceSDK;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArcSoftFace.Utils;

namespace ArcSoftFace.Arcsoft
{
    public class ArcFaceManage
    {
        public static ArcFaceManage Instance;
        public void Awake()
        {
            Instance = this;
        }
        private FaceEngine imageEngine = new FaceEngine();
        /// <summary>
        /// 视频引擎对象
        /// </summary>
        private FaceEngine videoEngine = new FaceEngine();
        /// <summary>
        /// IR视频引擎对象
        /// </summary>
        private FaceEngine videoIRImageEngine = new FaceEngine();
        /// <summary>
        /// RGB视频引擎对象
        /// </summary>
        private FaceEngine videoRGBImageEngine = new FaceEngine();
        /// <summary>
        /// 视频输入设备信息
        /// </summary>
        private FilterInfoCollection filterInfoCollection;
        /// <summary>
        /// 人员库图片选择 锁对象
        /// </summary>
        private object chooseImgLocker = new object();
        /// <summary>
        /// RGB 摄像头索引
        /// </summary>
        private int rgbCameraIndex = 0;
        /// <summary>
        /// IR 摄像头索引
        /// </summary>
        private int irCameraIndex = 0;
        /// <summary>
        /// FR失败重试次数
        /// </summary>
        private int frMatchTime = 30;
        /// <summary>
        /// 活体检测失败重试次数
        /// </summary>
        private int liveMatchTime = 30;

        /// <summary>
        ///  初始化引擎
        /// </summary>
        public void  InitEngines()
        {
            try
            {
                //读取配置文件
                AppSettingsReader reader = new AppSettingsReader();
                string appId = (string)reader.GetValue("APPID", typeof(string));
                string sdkKey64 = (string)reader.GetValue("SDKKEY64", typeof(string));
                string sdkKey32 = (string)reader.GetValue("SDKKEY32", typeof(string));
                rgbCameraIndex = (int)reader.GetValue("RGB_CAMERA_INDEX", typeof(int));
                irCameraIndex = (int)reader.GetValue("IR_CAMERA_INDEX", typeof(int));
                frMatchTime = (int)reader.GetValue("FR_MATCH_TIME", typeof(int));
                liveMatchTime = (int)reader.GetValue("LIVENESS_MATCH_TIME", typeof(int));
                //判断CPU位数
                var is64CPU = Environment.Is64BitProcess;
                if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(is64CPU ? sdkKey64 : sdkKey32))
                {
                    //禁用相关功能按钮
                    //ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
                    MessageBox.Show(string.Format("请在App.config配置文件中先配置APP_ID和SDKKEY{0}!", is64CPU ? "64" : "32"));
                    System.Environment.Exit(0);
                }
                //在线激活引擎    如出现错误，1.请先确认从官网下载的sdk库已放到对应的bin中，2.当前选择的CPU为x86或者x64
                int retCode = 0;
                try
                {
                    retCode = imageEngine.ASFOnlineActivation(appId, is64CPU ? sdkKey64 : sdkKey32);
                    if (retCode != 0 && retCode != 90114)
                    {
                        MessageBox.Show("激活SDK失败,错误码:" + retCode);
                        System.Environment.Exit(0);
                    }
                }
                catch (Exception ex)
                {
                    //禁用相关功能按钮
                    //ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
                    if (ex.Message.Contains("无法加载 DLL"))
                    {
                        MessageBox.Show("请将SDK相关DLL放入bin对应的x86或x64下的文件夹中!");
                    }
                    else
                    {
                        MessageBox.Show("激活SDK失败,请先检查依赖环境及SDK的平台、版本是否正确!");
                    }
                    System.Environment.Exit(0);
                }

                //初始化引擎
                DetectionMode detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
                //Video模式下检测脸部的角度优先值
                ASF_OrientPriority videoDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_ALL_OUT;
                //Image模式下检测脸部的角度优先值
                ASF_OrientPriority imageDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_ALL_OUT;
                //人脸在图片中所占比例，如果需要调整检测人脸尺寸请修改此值，有效数值为2-32
                int detectFaceScaleVal = 16;
                //最大需要检测的人脸个数
                int detectFaceMaxNum = 15;
                //引擎初始化时需要初始化的检测功能组合
                int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE;
                //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
                retCode = imageEngine.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask);
                Console.WriteLine("InitEngine Result:" + retCode);
               Console.WriteLine((retCode == 0) ? "图片引擎初始化成功!" : string.Format("图片引擎初始化失败!错误码为:{0}", retCode));
                if (retCode != 0)
                {
                    //禁用相关功能按钮
                    //ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
                }

                //初始化视频模式下人脸检测引擎
                DetectionMode detectModeVideo = DetectionMode.ASF_DETECT_MODE_VIDEO;
                int combinedMaskVideo = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION;
                retCode = videoEngine.ASFInitEngine(detectModeVideo, videoDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMaskVideo);
                Console.WriteLine((retCode == 0) ? "视频引擎初始化成功!" : string.Format("视频引擎初始化失败!错误码为:{0}", retCode));
                if (retCode != 0)
                {
                    //禁用相关功能按钮
                    //ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
                }

                //RGB视频专用FR引擎
                combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_LIVENESS;
                retCode = videoRGBImageEngine.ASFInitEngine(detectMode, videoDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask);
                Console.WriteLine ((retCode == 0) ? "RGB处理引擎初始化成功!" : string.Format("RGB处理引擎初始化失败!错误码为:{0}", retCode));
                if (retCode != 0)
                {
                    //禁用相关功能按钮
                   // ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
                }
                //设置活体阈值
                videoRGBImageEngine.ASFSetLivenessParam(0.5f);

                //IR视频专用FR引擎
                combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_IR_LIVENESS;
                retCode = videoIRImageEngine.ASFInitEngine(detectModeVideo, videoDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask);
                Console.WriteLine((retCode == 0) ? "IR处理引擎初始化成功!\r\n" : string.Format("IR处理引擎初始化失败!错误码为:{0}\r\n", retCode));
                if (retCode != 0)
                {
                    //禁用相关功能按钮
                   // ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
                }
                //设置活体阈值
                videoIRImageEngine.ASFSetLivenessParam(0.5f, 0.7f);

                InitVideoCamera();
            }
            catch (Exception ex)
            {
                LogUtil.LogInfo(GetType(), ex);
                MessageBox.Show("程序初始化异常,请在App.config中修改日志配置,根据日志查找原因!");
                System.Environment.Exit(0);
            }
        }
             
        

        /// <summary>
        ///  摄像头初始化
        /// </summary>
        public void InitVideoCamera()
        {
            try
            {
                filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            }
            catch (Exception e)
            {
                MessageBox.Show("摄像头初始化失败！！");
                return;
            }
        }
    }
}

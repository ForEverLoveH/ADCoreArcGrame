using AForge.Controls;
using AForge.Video.DirectShow;
using ArcFaceSDK;
using ArcFaceSDK.Entity;
using ArcFaceSDK.SDKModels;
using ArcSoftFace.Entity;
using ArcSoftFace.Utils;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem
{
    public class CameraConnect
    {
        /// <summary>
        /// RGB 特征搜索尝试次数字典
        /// </summary>
        private DictionaryUnit<int, int> rgbFeatureTryDict = new DictionaryUnit<int, int>();

        /// <summary>
        /// RGB 活体检测尝试次数字典
        /// </summary>
        private DictionaryUnit<int, int> rgbLivenessTryDict = new DictionaryUnit<int, int>();

        /// <summary>
        /// IR视频帧图像使用锁
        /// </summary>
        private object irVideoImageLocker = new object();
        /// <summary>
        /// 是否是双目摄像
        /// </summary>
        private bool isDoubleShot = false;
        /// <summary>
        /// IR视频帧图像
        /// </summary>
        private System.Drawing.Bitmap irVideoBitmap = null;
        /// <summary>
        /// RGB摄像头设备
        /// </summary>
        private VideoCaptureDevice rgbDeviceVideo;
        /// <summary>
        /// IR 视频最大人脸追踪检测结果
        /// </summary>
        private FaceTrackUnit trackIRUnit = new FaceTrackUnit();
        /// <summary>
        /// IR摄像头设备
        /// </summary>
        private VideoCaptureDevice irDeviceVideo;

        /// <summary>
        /// RGB 摄像头索引
        /// </summary>
        private int rgbCameraIndex = 0;

        /// <summary>
        /// IR 摄像头索引
        /// </summary>
        private int irCameraIndex = 0;

        /// <summary>
        /// 相似度
        /// </summary>
        private float threshold = 0.8f;

        /// <summary>
        /// 用于标记是否需要清除比对结果
        /// </summary>
        private bool isCompare = false;
        /// <summary>
        /// 关闭FR线程开关
        /// </summary>
        private bool exitVideoRGBFR = false;
        /// <summary>
        /// 关闭活体线程开关
        /// </summary>
        private bool exitVideoRGBLiveness = false;
        /// <summary>
        /// 关闭IR活体和FR线程线程开关
        /// </summary>
        private bool exitVideoIRFRLiveness = false;

        /// <summary>
        /// 视频输入设备信息
        /// </summary>
        private FilterInfoCollection filterInfoCollection;
        /// <summary>
        ///  相机初始化
        /// </summary>
        public  void CameraInit()
        {
            try
            {
                filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            }
            catch(Exception ex)
            {
                Console.WriteLine("初始化相机失败，失败原因是："+ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rgbVideoSource"></param>
        /// <param name="irVideoSource"></param>
        public void OtherCamera(AForge.Controls.VideoSourcePlayer rgbVideoSource, AForge.Controls.VideoSourcePlayer irVideoSource, Sunny.UI.UITextBox txtThreshold, ListView faceList, List<FaceFeature> faceFeatures, ImageList faceImageList)
        {
            try
            {
                //必须保证有可用摄像头
                if (filterInfoCollection.Count == 0)
                {
                    MessageBox.Show("未检测到摄像头，请确保已安装摄像头或驱动!");
                    return;
                }
                if (rgbVideoSource.IsRunning || irVideoSource.IsRunning)
                {
                    // btnStartVideo.Text = "启用摄像头";
                    //关闭摄像头
                    if (irVideoSource.IsRunning)
                    {
                        irVideoSource.SignalToStop();
                        irVideoSource.Hide();
                    }
                    if (rgbVideoSource.IsRunning)
                    {
                        rgbVideoSource.SignalToStop();
                        rgbVideoSource.Hide();
                    }

                    txtThreshold.Enabled = false;
                    exitVideoRGBFR = true;
                    exitVideoRGBLiveness = true;
                    exitVideoIRFRLiveness = true;
                }
                else
                {
                    if (isCompare)
                    {
                        //比对结果清除
                        for (int i = 0; i < faceFeatures.Count; i++)
                        {
                           faceList.Items[i].Text = string.Format("{0}号", i);
                        }
                        // lblCompareInfo.Text = string.Empty;
                        isCompare = false;
                    }
                    //“选择识别图”、“开始匹配”按钮禁用，阈值控件可用，显示摄像头控件
                    txtThreshold.Enabled = true;
                    rgbVideoSource.Show();
                    irVideoSource.Show();
                    //ControlsEnable(false, chooseImgBtn, matchBtn, chooseMultiImgBtn, btnClearFaceList);
                    // btnStartVideo.Text = "关闭摄像头";
                    //获取filterInfoCollection的总数
                    int maxCameraCount = filterInfoCollection.Count;
                    //如果配置了两个不同的摄像头索引
                    if (rgbCameraIndex != irCameraIndex && maxCameraCount >= 2)
                    {
                        //RGB摄像头加载
                        rgbDeviceVideo = new VideoCaptureDevice(filterInfoCollection[rgbCameraIndex < maxCameraCount ? rgbCameraIndex : 0].MonikerString);
                        rgbVideoSource.VideoSource = rgbDeviceVideo;
                        rgbVideoSource.Start();

                        //IR摄像头
                        irDeviceVideo = new VideoCaptureDevice(filterInfoCollection[irCameraIndex < maxCameraCount ? irCameraIndex : 0].MonikerString);
                        irVideoSource.VideoSource = irDeviceVideo;
                        irVideoSource.Start();
                        //双摄标志设为true
                        isDoubleShot = true;
                        //启动检测线程
                        exitVideoIRFRLiveness = false;
                        videoIRLiveness();
                    }
                    else
                    {
                        //仅打开RGB摄像头，IR摄像头控件隐藏
                        rgbDeviceVideo = new VideoCaptureDevice(filterInfoCollection[rgbCameraIndex <= maxCameraCount ? rgbCameraIndex : 0].MonikerString);
                        rgbVideoSource.VideoSource = rgbDeviceVideo;
                        rgbVideoSource.Start();
                        irVideoSource.Hide();
                    }
                    //启动两个检测线程
                    exitVideoRGBFR = false;
                    exitVideoRGBLiveness = false;
                    videoRGBLiveness();
                    videoRGBFR(faceFeatures);

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// FR失败重试次数
        /// </summary>
        private int frMatchTime = 30;
        /// <summary>
        /// 
        /// </summary>
        private void videoRGBFR(List<FaceFeature> leftImageFeatureList)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                while (true)
                {
                    if (exitVideoRGBFR)
                    {
                        return;
                    }
                    if (rgbFeatureTryDict.GetDictCount() <= 0)
                    {
                        continue;
                    }
                    //左侧人脸库为空时，不用进行特征搜索
                    if (leftImageFeatureList.Count <= 0)
                    {
                        continue;
                    }
                    try
                    {
                        if (rgbVideoBitmap == null)
                        {
                            continue;
                        }
                        List<int> faceIdList = new List<int>();
                        faceIdList.AddRange(rgbFeatureTryDict.GetAllElement().Keys);
                        foreach (int tempFaceId in faceIdList)
                        {
                            //待处理队列中不存在，移除
                            if (!rgbFeatureTryDict.ContainsKey(tempFaceId))
                            {
                                continue;
                            }
                            //大于尝试次数，移除
                            int tryTime = rgbFeatureTryDict.GetElementByKey(tempFaceId);
                            if (tryTime >= frMatchTime)
                            {
                                continue;
                            }
                            //无对应的人脸框信息
                            if (!trackRGBUnitDict.ContainsKey(tempFaceId))
                            {
                                continue;
                            }
                            FaceTrackUnit tempFaceTrack = trackRGBUnitDict.GetElementByKey(tempFaceId);
                            tryTime += 1;
                            //特征搜索
                            int faceIndex = -1;
                            float similarity = 0f;
                            Console.WriteLine(string.Format("faceId:{0},特征搜索第{1}次\r\n", tempFaceId, tryTime));
                            //提取人脸特征
                            SingleFaceInfo singleFaceInfo = new SingleFaceInfo();
                            singleFaceInfo.faceID = tempFaceId;
                            singleFaceInfo.faceOrient = tempFaceTrack.FaceOrient;
                            singleFaceInfo.faceRect = tempFaceTrack.Rect;
                            Bitmap bitmapClone = null;
                            try
                            {
                                lock (rgbVideoImageLocker)
                                {
                                    if (rgbVideoBitmap == null)
                                    {
                                        break;
                                    }
                                    bitmapClone = (Bitmap)rgbVideoBitmap.Clone();
                                }
                                FaceFeature feature = FaceUtil.ExtractFeature(videoRGBImageEngine, bitmapClone, singleFaceInfo);
                                if (feature.featureSize <= 0)
                                {
                                    break;
                                }
                                //特征搜索
                                faceIndex = compareFeature(feature, out similarity, leftImageFeatureList);
                                //更新比对结果
                                if (trackRGBUnitDict.ContainsKey(tempFaceId))
                                {
                                    trackRGBUnitDict.GetElementByKey(tempFaceId).SetFaceIndexAndSimilarity(faceIndex, similarity.ToString("#0.00"));
                                    if (faceIndex > -1)
                                    {
                                        tryTime = frMatchTime;
                                    }
                                }
                            }
                            catch (Exception ee)
                            {
                                LogUtil.LogInfo(GetType(), ee);
                            }
                            finally
                            {
                                if (bitmapClone != null)
                                {
                                    bitmapClone.Dispose();
                                }
                            }
                            rgbFeatureTryDict.UpdateDictionaryElement(tempFaceId, tryTime);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogUtil.LogInfo(GetType(), ex);
                    }
                }
            }));
        }
        /// <summary>
        ///  人脸对比
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="similarity"></param>
        /// <returns></returns>
        private int compareFeature(FaceFeature feature, out float similarity, List<FaceFeature> leftImageFeatureList)
        {
            int result = -1;
            similarity = 0f;
            try
            {
                //如果人脸库不为空，则进行人脸匹配
                if (leftImageFeatureList != null && leftImageFeatureList.Count > 0)
                {
                    for (int i = 0; i < leftImageFeatureList.Count; i++)
                    {
                        //调用人脸匹配方法，进行匹配
                        videoRGBImageEngine.ASFFaceFeatureCompare(feature, leftImageFeatureList[i], out similarity);
                        if (similarity >= threshold)
                        {
                            result = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// RGB 摄像头视频人脸追踪检测结果
        /// </summary>
        private DictionaryUnit<int, FaceTrackUnit> trackRGBUnitDict = new DictionaryUnit<int, FaceTrackUnit>();
        /// <summary>
        /// RGB视频帧图像
        /// </summary>
        private Bitmap rgbVideoBitmap = null;
        /// </summary>
        private int liveMatchTime = 30;

        /// <summary>
        /// RGB视频帧图像使用锁
        /// </summary>
        private object rgbVideoImageLocker = new object();
        /// <summary>
        /// RGB视频引擎对象
        /// </summary>
        private FaceEngine videoRGBImageEngine = new FaceEngine();
        /// <summary>
        /// 
        /// </summary>
        public void videoRGBLiveness()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                while (true)
                {
                    if (exitVideoRGBLiveness)
                    {
                        return;
                    }
                    if (rgbLivenessTryDict.GetDictCount() <= 0)
                    {
                        continue;
                    }
                    try
                    {
                        if (rgbVideoBitmap == null)
                        {
                            continue;
                        }
                        List<int> faceIdList = new List<int>();
                        faceIdList.AddRange(rgbLivenessTryDict.GetAllElement().Keys);
                        //遍历人脸Id，进行活体检测
                        foreach (int tempFaceId in faceIdList)
                        {
                            //待处理队列中不存在，移除
                            if (!rgbLivenessTryDict.ContainsKey(tempFaceId))
                            {
                                continue;
                            }
                            //大于尝试次数，移除
                            int tryTime = rgbLivenessTryDict.GetElementByKey(tempFaceId);
                            if (tryTime >= liveMatchTime)
                            {
                                continue;
                            }
                            tryTime += 1;
                            //无对应的人脸框信息
                            if (!trackRGBUnitDict.ContainsKey(tempFaceId))
                            {
                                continue;
                            }
                            FaceTrackUnit tempFaceTrack = trackRGBUnitDict.GetElementByKey(tempFaceId);

                            //RGB活体检测
                            Console.WriteLine(string.Format("faceId:{0},活体检测第{1}次\r\n", tempFaceId, tryTime));
                            SingleFaceInfo singleFaceInfo = new SingleFaceInfo();
                            singleFaceInfo.faceID = tempFaceId;
                            singleFaceInfo.faceOrient = tempFaceTrack.FaceOrient;
                            singleFaceInfo.faceRect = tempFaceTrack.Rect;
                            Bitmap bitmapClone = null;
                            try
                            {
                                lock (rgbVideoImageLocker)
                                {
                                    if (rgbVideoBitmap == null)
                                    {
                                        break;
                                    }
                                    bitmapClone = (Bitmap)rgbVideoBitmap.Clone();
                                }
                                int retCodeLiveness = -1;
                                LivenessInfo liveInfo = FaceUtil.LivenessInfo_RGB(videoRGBImageEngine, bitmapClone, singleFaceInfo, out retCodeLiveness);
                                //更新活体检测结果
                                if (retCodeLiveness.Equals(0) && liveInfo.num > 0 && trackRGBUnitDict.ContainsKey(tempFaceId))
                                {
                                    trackRGBUnitDict.GetElementByKey(tempFaceId).RgbLiveness = liveInfo.isLive[0];
                                    if (liveInfo.isLive[0].Equals(1))
                                    {
                                        tryTime = liveMatchTime;
                                    }
                                }
                            }
                            catch (Exception ee)
                            {
                                LogUtil.LogInfo(GetType(), ee);
                            }
                            finally
                            {
                                if (bitmapClone != null)
                                {
                                    bitmapClone.Dispose();
                                }
                            }
                            rgbLivenessTryDict.UpdateDictionaryElement(tempFaceId, tryTime);
                        }

                    }
                    catch (Exception ex)
                    {
                        LogUtil.LogInfo(GetType(), ex);
                    }
                }
            }));
        }

        /// <summary>
        /// IR视频引擎对象
        /// </summary>
        private FaceEngine videoIRImageEngine = new FaceEngine();
        /// <summary>
        /// IR活体检测线程
        /// </summary>
        private void videoIRLiveness()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                while (true)
                {
                    if (exitVideoIRFRLiveness)
                    {
                        return;
                    }
                    try
                    {
                        System.Drawing.Bitmap bitmapClone = null;
                        try
                        {
                            lock (irVideoImageLocker)
                            {
                                if (irVideoBitmap == null)
                                {
                                    continue;
                                }
                                bitmapClone = (Bitmap)irVideoBitmap.Clone();
                                if (bitmapClone == null)
                                {
                                    continue;
                                }
                            }
                            SingleFaceInfo singleFaceInfo = new SingleFaceInfo();
                            singleFaceInfo.faceID = trackIRUnit.FaceId;
                            singleFaceInfo.faceOrient = trackIRUnit.FaceOrient;
                            singleFaceInfo.faceRect = trackIRUnit.Rect;
                            int retCodeLiveness = -1;
                            LivenessInfo liveInfo = FaceUtil.LivenessInfo_IR(videoIRImageEngine, bitmapClone, singleFaceInfo, out retCodeLiveness);
                            if (retCodeLiveness.Equals(0) && liveInfo.num > 0)
                            {
                                trackIRUnit.IrLiveness = liveInfo.isLive[0];
                            }
                        }
                        catch (Exception ee)
                        {
                            LogUtil.LogInfo(GetType(), ee);
                        }
                        finally
                        {
                            if (bitmapClone != null)
                            {
                                bitmapClone.Dispose();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogUtil.LogInfo(GetType(), ex);
                    }
                }
            }));
        }


        /// <summary>
        /// 视频引擎对象
        /// </summary>
        private FaceEngine videoEngine = new FaceEngine();
        /// <summary>
        /// VideoPlayer 框的字体
        /// </summary>
        private Font font = new Font(FontFamily.GenericSerif, 10f, FontStyle.Bold);
        /// <summary>
        /// 红色画笔
        /// </summary>
        private SolidBrush redBrush = new SolidBrush(Color.Red);

        /// <summary>
        /// 绿色画笔
        /// </summary>
        private SolidBrush greenBrush = new SolidBrush(Color.Green);


        public void PaintImage(VideoSourcePlayer rgbVideoSource, PaintEventArgs e)
        {
            try
            {
                if(!rgbVideoSource.IsRunning)
                {
                    return;
                }
                lock (rgbVideoImageLocker)
                {
                    rgbVideoBitmap = rgbVideoSource.GetCurrentVideoFrame();

                }
                Bitmap bitmapClone = null;
                try
                {
                    lock (rgbVideoImageLocker){
                        if (rgbVideoBitmap == null)
                        {
                            return;
                        }
                        bitmapClone = (Bitmap)rgbVideoBitmap.Clone();
                    }
                    if(bitmapClone == null)
                    {
                        return;
                    }
                    MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(videoEngine, bitmapClone);
                    if (multiFaceInfo.faceNum <= 0)
                    {
                        trackRGBUnitDict.ClearAllElement();
                        return;
                    }
                    Graphics graphics = e.Graphics;
                    float offsetX = rgbVideoSource.Width * 1.0F / bitmapClone.Width;
                    float offsetY = rgbVideoSource.Height * 1.0F / bitmapClone.Height;
                    List<int> tempIdList = new List<int>();
                    for (int index = 0; index < multiFaceInfo.faceNum; index++) {
                        MRECT rect = multiFaceInfo.faceRects[index];
                        float x = rect.left * offsetX;
                        float wid = rect.right * offsetY - x;
                        float  y = rect .top * offsetY;
                        float height = rect.bottom * offsetY - y;
                        int faceid = multiFaceInfo.faceID[index];
                        FaceTrackUnit  currentFaceTrack = trackRGBUnitDict.GetElementByKey(faceid);
                        //根据rect进行画框 // 将上一帧检测到的结果显示到页面上
                        lock (rgbVideoImageLocker) 
                        {
                            if (currentFaceTrack != null)
                            {
                                graphics.DrawRectangle(currentFaceTrack.CertifySuccess() ? Pens.Green : Pens.Red, x, y, wid, height);
                                if (!string.IsNullOrWhiteSpace(currentFaceTrack.GetCombineMessage()) && x > 0 && y > 0)
                                {
                                    graphics.DrawString(currentFaceTrack.GetCombineMessage(), font, currentFaceTrack.CertifySuccess() ? greenBrush : redBrush, x, y - 15);
                                }

                            }
                            else
                            {
                                graphics.DrawRectangle(Pens.Red, x, y, wid, height);
                            }
                        }
                        if (faceid >= 0)
                        {
                            //判断faceid是否加入到等待处理队列
                            if (!rgbFeatureTryDict.ContainsKey(faceid))
                            {
                                rgbFeatureTryDict.AddDictionaryElement(faceid,0);
                            }
                            if (!rgbLivenessTryDict.ContainsKey(faceid))
                            {
                                rgbLivenessTryDict.AddDictionaryElement(faceid,0);
                            }
                            if (!trackRGBUnitDict.ContainsKey(faceid))
                            {
                                trackRGBUnitDict.GetElementByKey(faceid).Rect = rect;
                                trackRGBUnitDict.GetElementByKey(faceid).FaceOrient = multiFaceInfo.faceOrients[index];

                            }
                            else
                            {
                                trackRGBUnitDict.AddDictionaryElement(faceid, new FaceTrackUnit(faceid, rect, multiFaceInfo.faceOrients[index]));

                            }
                            tempIdList.Add(faceid);
                        }
                    }//初始化以及刷新等待处理队列，移除出框的人脸
                    rgbFeatureTryDict.RefershElements(tempIdList);
                    rgbLivenessTryDict.RefershElements(tempIdList);
                    trackRGBUnitDict.RefershElements(tempIdList);



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if(bitmapClone != null)
                    {
                        bitmapClone.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public  void rgbVideoSource_PlayingFinished()
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                exitVideoRGBFR = true;
                exitVideoRGBLiveness = true;
                exitVideoIRFRLiveness = true;
            }
            catch (Exception ex)
            {
                LogUtil.LogInfo(GetType(), ex);
            }
        }
    }
}

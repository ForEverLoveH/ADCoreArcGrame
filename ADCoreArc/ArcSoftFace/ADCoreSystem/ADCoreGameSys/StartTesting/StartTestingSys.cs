using ArcSoftFace.ADCoreSystem.ADCoreGameWindow;
using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.GameNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem 
{
    public  class StartTestingSys
    {
        public static StartTestingSys Instance;
        public static StartTestingWindow startTestingWindow;
        LocalNetClient localNetClient = new LocalNetClient();
        public  List<StartTestingExamDataModel> startTestingExamDatas;
        public  List<GroupDataModel> groupData;
        public  void Awake()
        {
            Instance = this;
        }
        public void Init()
        {
            StartGame();
        }

        private void StartGame(bool IsActive = true)
        {

            if (IsActive)
            {
                if (startTestingWindow == null)
                {
                    startTestingWindow = new StartTestingWindow();
                    startTestingWindow.Show();
                }
                else
                {
                    if (startTestingWindow.IsDisposed)
                    {
                        startTestingWindow = new StartTestingWindow();
                        startTestingWindow.Show();
                    }
                    else
                    {
                        startTestingWindow.Activate();
                    }
                }
            }
            else
            {
                if (startTestingWindow != null)
                {
                    startTestingWindow.Dispose();
                }
            }
        }
        /// <summary>
        /// 获取考试时间
        /// </summary>
        public  void ReqGetExamTime()
        {
            GameMsg msge = new GameMsg()
            {
                cmd = CMD.Req_QueryExaminationTime,
            };
            localNetClient.SendMsg(msge);
        }
        /// <summary>
        ///  获取考试时间的回调
        /// </summary>
        /// <param name="msg"></param>
        public void Rsp_QueryExaminationTime(GameMsg msg)
        {
            if (msg.rsp_QueryExaminationTime.startTestingExamDataModels != null)
            {
                startTestingExamDatas = msg.rsp_QueryExaminationTime.startTestingExamDataModels;
                startTestingWindow.SetTimedroup(startTestingExamDatas);
            }
            else
            {
                startTestingExamDatas = null;
            }
        }
        /// <summary>
        /// 获取组的回调
        /// </summary>
        /// <param name="msg"></param>
        public void Rsp_GetGroupMent(GameMsg msg)
        {
            if (msg.rsp_GetGroupMent._groupDataModels.Count > 0)
            {
                groupData = msg.rsp_GetGroupMent._groupDataModels;
                startTestingWindow.Rsp_GetGroupMent(groupData);

            }
            else
            {
                groupData = null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Rsp_TestNumberInquiry(GameMsg msg)
        {
            if (msg.rsp_TestNumberInquriey.userExcelModes.Count > 0)
            {
                startTestingWindow.Rsp_TestNumberInquiry(msg.rsp_TestNumberInquriey.userExcelModes);
            }
            else
            {
                MessageBox.Show(" 没有找到考生数据！！");
            }
        }
        /// <summary>
        /// 获取当前信息的回调
        /// </summary>
        /// <param name="msg"></param>
        public void Rsp_GetCurrentGroupMsg(GameMsg msg)
        {
            if (msg.rsp_GetCurrentGroupMsg.userExcelModes != null)
            {
                var s = msg.rsp_GetCurrentGroupMsg.userExcelModes;
                startTestingWindow.Rsp_GetCurrentGroupMsg(s);
            }
            else
            {
                MessageBox.Show("查找的信息不存在，或者当前数据库已被更改！！");
                return;
            }
        }

        public void RspModify_Grades(GameMsg msg)
        {
            if (msg.errorType == ErrorType.Failed_to_modify_grade)
            {
                MessageBox.Show("操作失败！！！");
            }
            else
            {
                MessageBox.Show("操作成功！！");
                startTestingWindow.UpdateExcelDataModeInTable(msg.rspModify_Grades.userExcelMode);
            }
        }
        /// <summary>
        ///  获取组号
        /// </summary>
        /// <param name="examtime"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Req_GetGroupMent(string examtime)
        {
            GameMsg msg = new GameMsg()
            {
                cmd = CMD.Req_GetGroupMent,
                req_GetGroupMent = new Req_GetGroupMent()
                {
                    examTime = examtime,
                },
            };
            localNetClient.SendMsg(msg);
        }
        /// <summary>
        /// 获取当前的信息
        /// </summary>
        /// <param name="examTime"></param>
        /// <param name="groupId"></param>
        /// <exception cref="NotImplementedException"></exception>
        public  void GetCurrentGroupMsg(string examTime, string groupId)
        {
            GameMsg msg = new GameMsg()
            {
                cmd = CMD.Req_GetCurrentGroupMsg,
                req_GetCurrentGroupMsg = new Req_GetCurrentGroupMsg()
                {
                    examTime = examTime,
                    groupNum = groupId,
                }
            };
            localNetClient.SendMsg(msg);
        }
        /// <summary>
        ///  获取人脸信息
        /// </summary>
        /// <param name="groupid"></param>
        public void Req_GetFeature(string groupid)
        {
            GameMsg gameMsg = new GameMsg()
            {
                cmd = CMD.Req_GetFaceFeature,
                req_GetFaceFeature = new Req_GetFaceFeature()
                {
                    groupID = groupid,
                }
            };
            localNetClient.SendMsg(gameMsg);
        }

        public void Rsp_GetFaceFeature(GameMsg msg)
        {
            if (msg.rsp_GetFaceFeature.faceDataModes == null)
            {
                MessageBox.Show("当前没有找到对应的人脸特征值，请重试！！");
                return;
            }
            else
            {
                startTestingWindow.GetLeftFaceFeature(msg.rsp_GetFaceFeature.faceDataModes);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="examNumber"></param>
         
        public void Req_TestnumberInquiry(string examNumber)
        {
            GameMsg msg = new GameMsg()
            {
                cmd = CMD.Req_TestNumberInquriry,
                req_TestNumberInquriry = new Req_TestNumberInquriry()
                {
                    examNum = examNumber,
                },

            };
            localNetClient.SendMsg(msg);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUserExcelList"></param>
        public void ReqModify_Grades(UserExcelMode currentUserExcelList)
        {
            GameMsg gameMsg = new GameMsg()
            {
                cmd = CMD.ReqModify_Grades,
                reqModify_Grades = new ReqModify_Grades()
                {
                    userExcelModes = currentUserExcelList,
                }
            };
            localNetClient.SendMsg(gameMsg);

        }

        public  void GetScreenCount()
        {
            try
            {
                int num = Screen.AllScreens.Count();
                for(int i=0; i < num; i++)
                {
                     
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("获取外接的显示器异常"+ex.Message);
            }
        }
        /// <summary>
        ///  根据组号获取人脸特征值
        /// </summary>
        /// <param name="groupID"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Req_GetFacefeature(string groupID)
        {
            if (string.IsNullOrEmpty(groupID))
            {
                MessageBox.Show("请确定组号！！");
                return;
            }
            else
            {
                GameMsg gameMsg = new GameMsg() { 
                    cmd  = CMD.Req_GetFaceFeature,
                    req_GetFaceFeature = new Req_GetFaceFeature()
                    {
                        groupID = groupID,
                    }
                };
                localNetClient.SendMsg(gameMsg);

            }
        }
    }
}

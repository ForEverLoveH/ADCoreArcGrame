using ArcFaceSDK.Entity;
using ArcSoftFace.ADCoreSystem.ADCoreGameWindow;
using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.ADCoreSystem.ADCoreModel;
using ArcSoftFace.GameCommon;
using ArcSoftFace.GameNet;
using Org.BouncyCastle.Asn1.Pkcs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem 
{
    public  class NewGroupSys
    {
        public static NewGroupSys Instance;
        public bool IsCanCreateGroup = false;  // 是否可以创建
        static NewGroup newGroupWindow;
        LocalNetClient localNetClient = new LocalNetClient();



        static string groupId;
        public void Awake()
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
                if (newGroupWindow == null)
                {
                    newGroupWindow = new NewGroup();
                    newGroupWindow.Show();
                }
                else
                {
                    if (newGroupWindow.IsDisposed)
                    {
                        newGroupWindow = new NewGroup();
                        newGroupWindow.Show();
                    }
                    else
                    {
                        newGroupWindow.Activate();
                    }
                }
            }
            else
            {
                if (newGroupWindow != null)
                {
                    newGroupWindow.Dispose();
                }
            }
        }
        /// <summary>
        ///  获取组
        /// </summary>
        /// <param name="groupID"></param>
        public void Req_GetGroup(string groupID)
        {
            groupId = groupID;
            GameMsg msg = new GameMsg()
            {
                cmd = CMD.Req_NewGroupGetGroup,
                req_NewGroupGetGroup = new Req_NewGroupGetGroup()
                {
                    groupID = groupID,
                },

            };
            localNetClient.SendMsg(msg);
        }

        public void Rsp_NewGroupGetGroup(GameMsg msg)
        {
            if (msg.rsp_NewGroupGetGroup.IsCanCreatGroup == false)
            {
                MessageBox.Show("该组已经在数据表中无法创建,请重新输入！！");
                IsCanCreateGroup = false;
            }
            else
            {
                MessageBox.Show("该组不在数据表中可以创建");
                 

            }
        }
        public void Req_UpDateUserExcelByFile(List<UserExcel> userExcels)
        {
            GameMsg msg = new GameMsg()
            {
                cmd = CMD.Req_NewGroupUpdateUserExcelByFile,
                req_NewGroupUpdateUserExcelByFile = new Req_NewGroupUpdateUserExcelByFile()
                {
                    userExcelModes = userExcels,
                },
            };
            localNetClient.SendMsg(msg);
        }

        public void Rsp_NewGroupUpdateUserExcelByFile(GameMsg msg)
        {
            if (msg.rsp_NewGroupUpdateUserExcelByFile.IsSucess == 0)
            {
                MessageBox.Show("数据表更新失败！！");
                return;
            }
            else if (msg.rsp_NewGroupUpdateUserExcelByFile.IsSucess == 1)
            {
                MessageBox.Show("数据表更新成功！！");

                newGroupWindow.SetDataByFileEmpty();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userExcelModes"></param>
        public void Req_UpDateUserExcel(List<UserExcel> userExcelModes)
        {
            GameMsg msg = new GameMsg()
            {
                cmd = CMD.Req_NewGroupUpdateUserExcel,
                req_NewGroupUpdateUserExcel = new Req_NewGroupUpdateUserExcel()
                {
                    userExcelModes = userExcelModes,
                },
            };
            localNetClient.SendMsg(msg);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Rsp_NewGroupUpdateUserExcel(GameMsg msg)
        {
            if (msg.rsp_NewGroupUpdateUserExcel.IsSucess == 0)
            {
                MessageBox.Show("数据表更新失败！！");
                return;
            }
            else if (msg.rsp_NewGroupUpdateUserExcel.IsSucess == 1)
            {
                MessageBox.Show("数据表更新成功！！");
                return;
            }
        }
        /// <summary>
        ///  人脸注册通过输入
        /// </summary>
        /// <param name="faceData">key 是组号， var字典，对应学生信息中的名字以及人脸信息</param>
        public void Req_RegisterFaceDataByInput(Dictionary<string, List<StudentFaceData>> faceData)
        {
            if (faceData == null)
            {
                return;
            }
            else
            {
                List<StudentFaceData> list = new List<StudentFaceData>();
                List<string> keys = new List<string>();
                for (int i = 0; i < faceData.Count; i++)
                {
                    foreach (var key in faceData.Keys)
                    {
                        keys.Add(key);
                    }
                }
                for (int i = 0; i < keys.Count; i++)
                {
                    faceData.TryGetValue(keys[i], out list);
                }
                List<StudentFaceData> faceDatas = new List<StudentFaceData>();
                for (int j = 0; j < list.Count; j++)
                {
                    StudentFaceData studentFaceData = new StudentFaceData()
                    {
                        groupID = list[j].groupID,
                        faceFeature = list[j].faceFeature,
                        Name = list[j].Name,
                    };
                    faceDatas.Add(studentFaceData);
                }
                GameMsg gameMsg = new GameMsg()
                {
                        cmd = CMD.Req_NewGroupFaceRegister,
                        req_NewGroupFaceRegister = new Req_NewGroupFaceRegister()
                        {
                            faces = faceDatas,
                        }
                };
                localNetClient.SendMsg(gameMsg);
                
                
            }
        }

        public  void Rsp_NewGrroupFaceRegister(GameMsg msg)
        {
            if (msg.rsp_NewGroupFaceRegister.Issucess == true)
            {
                MessageBox.Show("注册成功");
                newGroupWindow.SetButtonActive();

            }
            else
            {
                MessageBox.Show("注册失败！！");
            }
             
        }
        /// <summary>
        ///  删除 该组所有的人脸信息
        /// </summary>
        /// <param name="groupid"></param>
         
        public  void Req_DelectFaceData(UserExcel userExcel, FaceFeature faceFeature)
        {
            if (userExcel == null)
            {
                return;
            }
            else
            {
                GameMsg gameMsg = new GameMsg()
                {
                    cmd = CMD.Req_DelectFaceData,
                    req_DelectFaxeData = new Req_DelectFaceData()
                    {
                         userExcel  = userExcel,
                         faceData  = new FaceData()
                         {
                             groupID   =  userExcel.Group_number,
                             Name  = userExcel.Name,
                             faceFeature = faceFeature
                         }

                    }
                };
                localNetClient.SendMsg(gameMsg);

            }
        }

        public  void Rsp_DelectFaceData(GameMsg msg)
        {
            
        }

       
    }
}


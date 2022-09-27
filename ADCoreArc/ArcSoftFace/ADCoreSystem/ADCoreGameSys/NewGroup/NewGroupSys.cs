using ArcSoftFace.ADCoreSystem.ADCoreGameWindow;
using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.GameCommon;
using ArcSoftFace.GameNet;
using System;
using System.Collections.Generic;
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
        static NewGroupWindows newGroupWindow;
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
                    newGroupWindow = new NewGroupWindows();
                    newGroupWindow.Show();
                }
                else
                {
                    if (newGroupWindow.IsDisposed)
                    {
                        newGroupWindow = new NewGroupWindows();
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


    }
}


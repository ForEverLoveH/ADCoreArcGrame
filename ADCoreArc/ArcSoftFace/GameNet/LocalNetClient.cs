using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.ADCoreSystem.ADCoreGameSys;
using ArcSoftFace.GameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.GameNet
{
    public class LocalNetClient
    {
        public static LocalNetClient Instance;
        private LocalNetServer locaNetServer;
        public void Awake()
        {
            Instance = this;

        }
        private Queue<GameMsg> gameMsgs = new Queue<GameMsg>();
        public void Init()
        {
            gameMsgs = new Queue<GameMsg>();
            locaNetServer = LocalNetServer.Instance;

        }
        public void AddMsgQue(GameMsg msg)
        {
            lock (gameMsgs)
            {
                gameMsgs.Enqueue(msg);
            }
            Update();
        }


        public void Update()
        {
            while (gameMsgs.Count > 0)
            {
                lock (gameMsgs)
                {
                    GameMsg msg = gameMsgs.Dequeue();
                    HandelMsg(msg);
                }
            }
        }
        private void HandelMsg(GameMsg msg)
        {
            switch (msg.cmd)
            {
                case CMD.None:
                    break;
                case CMD.RspLogin:
                    LoginSys.Instance.RspLogin(msg);
                    break;
                case CMD.Rsp_GetKeyByPersonImport:
                    PersonImportSys.Instance.Rsp_GetKey(msg);
                    break;
                case CMD.Rsp_GetKeyByExportGrade:
                   // ExportGradeSys.Instance.Rsp_GetKey(msg);
                    break;
                case CMD.Rsp_Add_Import:
                    PersonImportSys.Instance.Rsp_Add_Import(msg);
                    break;
                case CMD.Rsp_New_Import:
                    PersonImportSys.Instance.Rsp_New_Import(msg);
                    break;
                case CMD.Rsp_Verification:
                    VerificationSys.Instance.Rsp_Verification(msg);
                    break;
                case CMD.Rsp_AdminChangeAdminPassword:
                  //  LoginSettingSys.Instance.Rsp_AdminChangeAdminPassword(msg);
                    break;
                case CMD.Rsp_AdminChangeUserPassword:
                    //LoginSettingSys.Instance.Rsp_AdminChangeUserPassword(msg);
                    break;
                case CMD.Rsp_UserChangeUserPassword:
                   // LoginSettingSys.Instance.Rsp_UserChangeUserPassword(msg);
                    break;
                case CMD.Rsp_ExportGrade:
                   // ExportGradeSys.Instance.Rsp_ExportGrade(msg);
                    break;
                case CMD.Rsp_NewGroupGetGroup:
                    NewGroupSys.Instance.Rsp_NewGroupGetGroup(msg);
                    break;
                case CMD.Rsp_NewGroupUpdateUserExcel:
                    NewGroupSys.Instance.Rsp_NewGroupUpdateUserExcel(msg);
                    break;
                case CMD.Rsp_QueryExaminationTime:
                    //StartTestingSys.Instance.Rsp_QueryExaminationTime(msg);
                    break;

                case CMD.Rsp_GetGroupMent:
                   // StartTestingSys.Instance.Rsp_GetGroupMent(msg);
                    break;
                case CMD.Rsp_TestNumberInquiry:
                   // StartTestingSys.Instance.Rsp_TestNumberInquiry(msg);
                    break;
                case CMD.Rsp_GetCurrentGroupMsg:
                   // StartTestingSys.Instance.Rsp_GetCurrentGroupMsg(msg);
                    break;
                case CMD.RspModify_Grades:
                   // StartTestingSys.Instance.RspModify_Grades(msg);
                    break;

            }
        }
        public void SendMsg(GameMsg msg)
        {
            locaNetServer = new LocalNetServer();
            locaNetServer.OnRecieve(msg);

        }
        public void OnRecieve(GameMsg msg)
        {
            Instance.AddMsgQue(msg);
        }
    }
}

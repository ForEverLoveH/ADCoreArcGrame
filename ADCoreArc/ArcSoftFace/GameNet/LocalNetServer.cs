using ArcSoftFace.GameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.GameNet
{
    public class LocalNetServer
    {
        public static LocalNetServer Instance;
        LocalNetClient localNetClient;
        


        public void Awake()
        {
            Instance = this;

        }
        Queue<GameMsg> gameMsgs = new Queue<GameMsg>();

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
                case CMD.ReqLogin:
                    SqliteDB.Instance.ReqLogin(msg);
                    break;
                case CMD.Req_GetKeyByPersonImport:
                    SqliteDB.Instance.Req_GetKeyByPersonImport(msg);
                    break;
                case CMD.Req_GetKeyByExportGrade:
                    SqliteDB.Instance.Req_GetKeyByExportGrade(msg);
                    break;
                case CMD.Req_New_Import:
                    SqliteDB.Instance.New_Import(msg);
                    break;
                case CMD.Req_Add_Import:
                    SqliteDB.Instance.Add_Import(msg);
                    break;
                case CMD.Req_Verification:
                    SqliteDB.Instance.Req_Verification(msg);
                    break;
                case CMD.Req_ExportGrade:
                    SqliteDB.Instance.Req_ExportGrade(msg);
                    break;
                case CMD.Req_AdminChangeAdminPassword:
                    SqliteDB.Instance.Req_AdminChangeAdminPassword(msg);
                    break;
                case CMD.Req_AdminChangeUserPassword:
                    SqliteDB.Instance.Req_AdminChangeUserPassword(msg);
                    break;

                case CMD.Req_UserChangeUserPassword:
                    SqliteDB.Instance.Req_UserChangeUserPassword(msg);
                    break;
                case CMD.Req_NewGroupGetGroup:
                    SqliteDB.Instance.Req_NewGroupGetGroup(msg);
                    break;
                case CMD.Req_NewGroupUpdateUserExcel:
                    SqliteDB.Instance.Req_NewGroupUpdateUserExcel(msg);
                    break;
                case CMD.Req_NewGroupFaceRegister:
                    SqliteDB.Instance.Req_NewGroupFaceRegister(msg);
                    break;
                case CMD.Req_QueryExaminationTime:
                    SqliteDB.Instance.Req_QueryExaminationTime(msg);
                    break;
                case CMD.Req_GetGroupMent:
                    SqliteDB.Instance.Req_GetGroupMent(msg);
                    break;
                case CMD.Req_TestNumberInquriry:
                    SqliteDB.Instance.Req_TestNumberInquriry(msg);
                    break;
                case CMD.Req_GetCurrentGroupMsg:
                    SqliteDB.Instance.Req_GetCurrentGroupMsg(msg);
                    break;
                case CMD.ReqModify_Grades:
                    SqliteDB.Instance.ReqModify_Grades(msg);
                    break;
                case CMD.Req_GetFaceFeature:
                    SqliteDB.Instance.Req_GetFaceFeature(msg);
                    break;
            }
        }
        public void SendMsg(GameMsg msg)
        {
            localNetClient = new LocalNetClient();
            localNetClient.OnRecieve(msg);

        }
        public void OnRecieve(GameMsg msg)
        {
            Instance.AddMsgQue(msg);
        }

        private void AddMsgQue(GameMsg msg)
        {
            lock (gameMsgs)
            {
                gameMsgs.Enqueue(msg);
            }
            Update();
        }
    }
}


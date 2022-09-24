using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.GameNet;
using System.Windows.Forms;

namespace ArcSoftFace.GameCommon
{
    public class StartTestingSql
    {
        public static StartTestingSql Instance;
        public LocalNetServer localNetServer = new LocalNetServer();
        public void Awake()
        {
            Instance = this;
        }

        public void Req_QueryExaminationTime(GameMsg msg)
        {
            GameMsg msgs = new GameMsg()
            {
                cmd = CMD.Rsp_QueryExaminationTime,
            };
            string path = Application.StartupPath + GameConst.SaveDBPath;
            SqlDbCommand sqlDbCommand = new SqlDbCommand(path);
            var s = sqlDbCommand.DbSql<StartTestingExamDataModel>("SELECT distinct Exam_date FROM UserExcel");
            msgs.rsp_QueryExaminationTime = new Rsp_QueryExaminationTime()
            {
                startTestingExamDataModels = s,
            };
            sqlDbCommand.Dispose();
            localNetServer.SendMsg(msgs);


        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="msg"></param>

        public void Req_GetGroupMent(GameMsg msg)
        {
            GameMsg Rsp_Query_group_number = new GameMsg
            {
                cmd = CMD.Rsp_GetGroupMent,
            };
            string dbPath = Application.StartupPath + GameConst.SaveDBPath;
            SqlDbCommand sql = new SqlDbCommand(dbPath);
            var groupDataModels = sql.DbSql<GroupDataModel>($"SELECT distinct Group_number FROM UserExcel where Exam_date = '{msg.req_GetGroupMent.examTime}'");
            Rsp_Query_group_number.rsp_GetGroupMent = new Rsp_GetGroupMent()
            {
                _groupDataModels = groupDataModels
            };
            sql.Dispose();
            localNetServer.SendMsg(Rsp_Query_group_number);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>

        public void Req_TestNumberInquriry(GameMsg msg)
        {
            GameMsg msgs = new GameMsg()
            {
                cmd = CMD.Rsp_TestNumberInquiry,

            };
            string path = Application.StartupPath + GameConst.SaveDBPath;
            SqlDbCommand cmd = new SqlDbCommand(path);

            var userExcelData = cmd.SelectBySql<UserExcelMode>("UserExcel", $"Exam_number = '{msg.req_TestNumberInquriry.examNum}'");
            msgs.rsp_TestNumberInquriey = new Rsp_TestNumberInquriey()
            {
                userExcelModes = userExcelData,
            };
            cmd.Dispose();
            localNetServer.SendMsg(msgs);
        }

        public void Req_GetCurrentGroupMsg(GameMsg msg)
        {
            GameMsg msh = new GameMsg()
            {
                cmd = CMD.Rsp_GetCurrentGroupMsg,
            };
            string path = Application.StartupPath + GameConst.SaveDBPath;
            SqlDbCommand cmd = new SqlDbCommand(path);
            var s = cmd.SelectBySql<UserExcelMode>("UserExcel", $"Exam_date = '{msg.req_GetCurrentGroupMsg.examTime}'and Group_number ='{msg.req_GetCurrentGroupMsg.groupNum}'");
            msh.rsp_GetCurrentGroupMsg = new Rsp_GetCurrentGroupMsg()
            {
                userExcelModes = s,
            };
            cmd.Dispose();
            localNetServer.SendMsg(msh);

        }

        public void ReqModify_Grades(GameMsg msg)
        {
            GameMsg msgs = new GameMsg()
            {
                cmd = CMD.RspModify_Grades,
            };
            string dbpath = Application.StartupPath + GameConst.SaveDBPath;
            SqlDbCommand sqlDbCommand = new SqlDbCommand(dbpath);
            var code = sqlDbCommand.Updete<UserExcelMode>(msg.reqModify_Grades.userExcelModes, "UserExcel", $"Id ={msg.reqModify_Grades.userExcelModes.Id}");
            if (code != 1)
            {
                msgs.errorType = ErrorType.Failed_to_modify_grade;
            }
            else
            {
                msgs.rspModify_Grades = new RspModify_Grades()
                {
                    userExcelMode = sqlDbCommand.SelectBySql<UserExcelMode>("UserExcel", $"Id={msg.reqModify_Grades.userExcelModes.Id}")[0]
                };

            }
            sqlDbCommand.Dispose();
            localNetServer.SendMsg(msgs);
        }
    }
}
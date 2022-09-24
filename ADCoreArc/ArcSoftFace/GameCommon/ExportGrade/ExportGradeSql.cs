using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.GameNet;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ArcSoftFace.GameCommon
{
    public  class ExportGradeSql
    {
        public static ExportGradeSql Instance;
        public LocalNetServer locaNetServer = new LocalNetServer();
        List<UserExcel> userExcels = new List<UserExcel>();

        public void Awake()
        {
            Instance = this;
        }

        public void GetUserExcelData(string name)
        {
            IsExitenceUserExcelData(name);
            userExcels.Clear();
            string path = Application.StartupPath + GameConst.SaveDBPath;
            SqlDbCommand sqlcommand = new SqlDbCommand(path);
            object num = sqlcommand.GetKey(name, "Id");
            int nb = 0;
            int.TryParse(num.ToString(), out nb);

            if (nb > GameConst.SqlNumber)
            {
                MessageBox.Show("数据量过大！！");

            }
            else
            {
                userExcels = sqlcommand.SelectBySql<UserExcel>(name);
                sqlcommand.Dispose();
            }


            GameMsg msg = new GameMsg()
            {
                cmd = CMD.Rsp_ExportGrade
            };
            if (userExcels != null)
            {
                msg.rsp_ExportGrade = new Rsp_ExportGrade()
                {
                    userExcel = userExcels,
                };
            }
            else
            {
                msg.errorType = ErrorType.userDataIsEmpty;

            }
            locaNetServer.SendMsg(msg);


        }

        private void IsExitenceUserExcelData(string name)
        {
            String Path = Application.StartupPath + GameConst.SaveDBPath;
            SqlDbCommand sqlcommand = new SqlDbCommand(Path);
            int s = sqlcommand.IsCreateTable(name);
            if (s == 0)
            {
                Console.WriteLine($"数据表{name}不存在！！");
                sqlcommand.CreateTable<UserExcelMode>(name);

            }
            if (s == 1)
            {
                Console.WriteLine($"数据表{name}存在");
            }
            sqlcommand.Dispose();
        }
    }
}

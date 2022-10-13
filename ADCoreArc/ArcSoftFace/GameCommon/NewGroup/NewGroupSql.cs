using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.GameNet;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace ArcSoftFace.GameCommon
{
    public  class NewGroupSql
    {
        public static NewGroupSql Instance;
        FaceRegisterSql FaceRegisterSql= new FaceRegisterSql();
        LocalNetServer netServer = new LocalNetServer();
        public void Awake()
        {
            Instance = this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Req_NewGroupGetGroup(GameMsg msg)
        {
            GameMsg msgs = new GameMsg()
            {
                cmd = CMD.Rsp_NewGroupGetGroup,
            };
            IsExitenceUserExcelData(GameConst.DBUserExcel);
            try
            {
                String path = Application.StartupPath + GameConst.SaveDBPath;
                SqlDbCommand sqlcommand = new SqlDbCommand(path);
                List<GroupDataModel> l = sqlcommand.DbSql<GroupDataModel>("SELECT distinct Group_number FROM UserExcel");
                if (l != null && l.Count > 0)
                {
                    List<string> sl = new List<string>();
                    foreach (GroupDataModel item in l)
                    {
                        sl.Add(item.Group_number);
                    }
                    if (sl.Count > 0)
                    {
                        if (sl.Contains(msg.req_NewGroupGetGroup.groupID))
                        {
                            msgs.rsp_NewGroupGetGroup = new Rsp_NewGroupGetGroup()
                            {
                                IsCanCreatGroup = false,
                            };
                        }
                        else
                        {
                            msgs.rsp_NewGroupGetGroup = new Rsp_NewGroupGetGroup()
                            {
                                IsCanCreatGroup = true,
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("组号查找异常！！" + ex.Message);
            }
            netServer.SendMsg(msgs);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="msg"></param>
        public void Req_NewGroupUpdateUserExcel(GameMsg msg)
        {
            GameMsg gameMsg = new GameMsg()
            {
                cmd = CMD.Rsp_NewGroupUpdateUserExcel,
            };
            IsExitenceUserExcelData(GameConst.DBUserExcel);
            try
            {
                string path = Application.StartupPath + GameConst.SaveDBPath;
                SqlDbCommand sqlcommand = new SqlDbCommand(path);
                var sl = sqlcommand.Insert<UserExcel>(msg.req_NewGroupUpdateUserExcel.userExcelModes, GameConst.DBUserExcel);
                if (sl == 0)
                {
                    gameMsg.rsp_NewGroupUpdateUserExcel = new Rsp_NewGroupUpdateUserExcel() 
                    { 
                        IsSucess = 0 
                    };

                }
                else if (sl >= 1)
                {
                    gameMsg.rsp_NewGroupUpdateUserExcel = new Rsp_NewGroupUpdateUserExcel() 
                    {
                        IsSucess = 1 
                    };
                }
                sqlcommand.Dispose();

            }
            catch (Exception e)
            {
                Console.WriteLine("数据插入异常" + e.Message);

            }
            
            netServer.SendMsg(gameMsg);

        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsExitenceUserExcelData(string name)
        {
            string path = Application.StartupPath + GameConst.SaveDBPath;
            SqlDbCommand sqlcommand = new SqlDbCommand(path);
            int s = sqlcommand.IsCreateTable(name);
            if (s == 0)  // 表不存在
            {
                sqlcommand.CreateTable<UserExcelMode>(name);
                sqlcommand.Dispose();
                return true;
            }
            else            // 表存在
            {
                sqlcommand.Dispose();
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Req_NewGroupFaceRegister(GameMsg msg)
        {
            GameMsg gameMsg = new GameMsg()
            {
                cmd = CMD.Rsp_NewGrroupFaceRegister,
            };
             int sl  = AddUserExcelToDB(msg.req_NewGroupFaceRegister.users);
            if(sl == 1)
            {
                FaceRegisterSql faceRegisterSql = new FaceRegisterSql();
                bool s = faceRegisterSql.AddFaceData(msg.req_NewGroupFaceRegister.faces);
                gameMsg.rsp_NewGroupFaceRegister = new Rsp_NewGrroupFaceRegister()
                {
                    Issucess = s,
                };
                netServer.SendMsg(gameMsg);
            }
            
        }

        private   int   AddUserExcelToDB(UserExcel users)
        {
            IsExitenceUserExcelData(GameConst.DBUserExcel);
            try
            {
                String path = Application.StartupPath + GameConst.SaveDBPath;
                SqlDbCommand sqlDbCommand = new SqlDbCommand(path);
                var sl = sqlDbCommand.Insert<UserExcel>(users, GameConst.DBUserExcel);
                if (sl == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Req_DelectFaceData(GameMsg msg)
        {
            GameMsg gameMsg = new GameMsg()
            {
                cmd = CMD.Rsp_DelectFaceData,

            };
           int  s= DelectUserExcel(msg.req_DelectFaxeData.userExcel);
            if (s == 0)
            {
                FaceRegisterSql faceRegisterSql = new FaceRegisterSql();
                int S = faceRegisterSql.DelectFaceFeature(msg);
                if(S == 0)
                gameMsg.rsp_DelectFaceData = new Rsp_DelectFaceData()
                {
                    IsSucess = S,
                };
                netServer.SendMsg(gameMsg);
            }
            else
            {
                Console.WriteLine("userExcel 删除失败！！");
            }
        }
        /// <summary>
        ///  删除userexcel 中的数据
        /// </summary>
        /// <param name="userExcel"></param>
        private int  DelectUserExcel(UserExcel userExcel)
        { 
            string path = Application.StartupPath + GameConst.SaveDBPath;
            SQLiteHelper helper = new SQLiteHelper(path);
            helper.OpenSQLite();
            if (helper.TableExit(GameConst.DBUserExcel))
            {
                int t = DelectUserExcelByHelper(helper, userExcel);
                helper.CloseSQLite();
                return t;
            }
            else
            {
                return -1;
            }


        }

        private  int  DelectUserExcelByHelper(SQLiteHelper helper, UserExcel userExcel)
        {
            helper.EnsureConnection();
            string sql = string.Format("Exam_number={0},Region ={1},Venue={2},Project ={3},Name={4},Sex={5},School={6},Grade={7},ClassName={8},Number_class={9},Project={10},Exam_data={11},Session ={12},Group_number={13},Intra_group_serial_number ={14},Achievement_one={15},Achievement_two={16},Achievement_three={17},Achievement_four={18},Remarks ={19}",
                userExcel.Exam_number, userExcel.Region, userExcel.Venue, userExcel.Project, userExcel.Name, userExcel.Sex, userExcel.School, userExcel.Grade, userExcel.ClassName, userExcel.Number_class
                ,userExcel.Project, userExcel.Exam_date, userExcel.Sessions, userExcel.Group_number, userExcel.Intra_group_serial_number, userExcel.Achievement_one, userExcel.Achievement_two, userExcel.Achievement_three, userExcel.Achievement_four, userExcel.Remarks );
            SQLiteParameter[] parameter = new SQLiteParameter[]
            {
                new SQLiteParameter("Exam_number",userExcel.Exam_number),
                new SQLiteParameter("Region",userExcel.Region),
                new SQLiteParameter("Venue",userExcel.Venue),
                new SQLiteParameter("Project",userExcel.Project),
                new SQLiteParameter("Name",userExcel.Name),
                new SQLiteParameter("Sex",userExcel.Sex),
                new SQLiteParameter("School",userExcel.School),
                new SQLiteParameter( "Grade",userExcel.Grade),
                new SQLiteParameter("ClassName",userExcel.ClassName ),
                new SQLiteParameter("Number_class",userExcel.Number_class ),
                new SQLiteParameter("Project",userExcel.Project ),
                new SQLiteParameter("Exam_data",userExcel .Exam_date),
                new SQLiteParameter("Session",userExcel.Sessions),
                new SQLiteParameter("Group_number",userExcel.Group_number),
                new SQLiteParameter("Intra_group_serial_number" ,userExcel.Intra_group_serial_number ),
                new SQLiteParameter("Achievement_one ",userExcel .Achievement_one),
                new SQLiteParameter("Achievement_two",userExcel.Achievement_two),
                new SQLiteParameter("Achievement_three",userExcel .Achievement_three),
                new SQLiteParameter("Achievement_four",userExcel .Achievement_four),
                new SQLiteParameter("Remarks",userExcel.Remarks),
            };
           return helper.Delete(GameConst.DBUserExcel, sql, parameter);
        }

        public  void Req_NewGroupUpdateUserExcelByFile(GameMsg msg)
        {
            GameMsg gameMsg = new GameMsg()
            {
                cmd = CMD.Rsp_NewGroupUpdateUserExcelByFile,
            };
            IsExitenceUserExcelData(GameConst.DBUserExcel);
            try
            {
                string path = Application.StartupPath + GameConst.SaveDBPath;
                SqlDbCommand sqlcommand = new SqlDbCommand(path);
                var sl = sqlcommand.Insert<UserExcel>(msg.req_NewGroupUpdateUserExcelByFile.userExcelModes, GameConst.DBUserExcel);
                if (sl == 0)
                {
                    gameMsg.rsp_NewGroupUpdateUserExcelByFile = new  Rsp_NewGroupUpdateUserExcelByFile()
                    {
                        IsSucess = 0
                    };

                }
                else if (sl >= 1)
                {
                    gameMsg.rsp_NewGroupUpdateUserExcelByFile = new Rsp_NewGroupUpdateUserExcelByFile()
                    {
                        IsSucess = 1
                    };
                }
                sqlcommand.Dispose();

            }
            catch (Exception e)
            {
                Console.WriteLine("数据插入异常" + e.Message);

            }

            netServer.SendMsg(gameMsg);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public  void Req_NewGroupFaceRegisterByFile(GameMsg msg)
        {
            GameMsg  gameMsg= new GameMsg()
            {
                cmd = CMD.Rsp_NewGroupFaceRegisterByFile,
            };
            FaceRegisterSql faceRegisterSql = new FaceRegisterSql();
            bool s = faceRegisterSql.AddFaceData(msg.req_NewGroupFaceRegisterFileByFile.face);
            gameMsg.rsp_NewGroupFaceRegisterByFile = new Rsp_NewGroupFaceRegisterByFile()
            {
                IsSucecc = s,
            };
            netServer.SendMsg(gameMsg);
        }
    }
    
}

using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.ADCoreSystem 
{
    public  class Xlsx:ExcelMo
    {
        UserExcelIndexMode userExcelIndexMode = new UserExcelIndexMode();

        /// <summary>
        /// 获取工作表名字
        /// </summary>
        /// <param name="Path"></param>
        public List<string> GetSheet(string Path)
        {
            try
            {
                if (string.IsNullOrEmpty(Path) != true)
                {
                    FileInfo file = new FileInfo(Path);
                    List<string> SheetName = new List<string>();
                    using (var stream = File.Open(Path, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet();

                            foreach (var table in result.Tables)
                            {
                                SheetName.Add(table.ToString());
                            }
                            reader.Close();
                        }
                    }

                    return SheetName;
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"读取xlsx工作表出错：{e.Message}");
                return null;
            }

        }
         
        /// <summary>
        /// 获取工作表数据
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="SheerName"></param>
        public List<UserExcel> GetSheetData(string Path, string SheerName)
        {
            try
            {
                if (string.IsNullOrEmpty(Path) != true)
                {
                    FileInfo file = new FileInfo(Path);
                    List<UserExcel> ListUserExcels = new List<UserExcel>();
                    using (var stream = File.Open(Path, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet();
                            DataTableCollection tableCollection = result.Tables;
                            DataTable tb = tableCollection[SheerName];


                            int columns = tb.Columns.Count;
                            int rows = tb.Rows.Count;
                            if (columns == 0 || rows == 0)
                            {
                                //Excel数据为空
                                UserExcel userExcelErr = new UserExcel() { Err = -1 };
                                ListUserExcels.Add(userExcelErr);
                                return ListUserExcels;
                            }
                            if (columns > 100 || rows > GameConst.SqlNumber)
                            {
                                //数据过大
                                UserExcel userExcelErr = new UserExcel() { Err = -3 };
                                ListUserExcels.Add(userExcelErr);
                                return ListUserExcels;
                            }

                            for (int i = 0; i < columns; i++)
                            {
                                string columnsName = tb.Rows[0][i].ToString();
                                SheetDataIndex(columnsName, i);
                            }
                            if (IsSheetDataIndex() == false)
                            {
                                //Excel数据格式与设定数据格式不符
                                UserExcel userExcelErr = new UserExcel() { Err = -2 };
                                ListUserExcels.Add(userExcelErr);
                                return ListUserExcels;
                            }

                            ListUserExcels.Clear();
                            for (int i = 1; i < rows; i++)
                            {
                                UserExcel userExcel = new UserExcel();
                                for (int j = 0; j < columns; j++)
                                {
                                    string nvalue = tb.Rows[i][j].ToString();
                                    if (j == userExcelIndexMode.Exam_number)
                                    {
                                        userExcel.Exam_number = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Region)
                                    {
                                        userExcel.Region = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Venue)
                                    {
                                        userExcel.Venue = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Project_team)
                                    {
                                        userExcel.Project_team = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Name)
                                    {
                                        userExcel.Name = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Sex)
                                    {
                                        userExcel.Sex = nvalue;
                                    }
                                    if (j == userExcelIndexMode.School)
                                    {
                                        userExcel.School = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Grade)
                                    {
                                        userExcel.Grade = nvalue;
                                    }
                                    if (j == userExcelIndexMode.ClassName)
                                    {
                                        userExcel.ClassName = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Number_class)
                                    {
                                        userExcel.Number_class = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Project)
                                    {
                                        userExcel.Project = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Exam_date)
                                    {
                                        userExcel.Exam_date = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Sessions)
                                    {
                                        userExcel.Sessions = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Group_number)
                                    {
                                        userExcel.Group_number = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Intra_group_serial_number)
                                    {
                                        userExcel.Intra_group_serial_number = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Achievement_one)
                                    {
                                        userExcel.Achievement_one = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Achievement_two)
                                    {
                                        userExcel.Achievement_two = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Achievement_three)
                                    {
                                        userExcel.Achievement_three = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Achievement_four)
                                    {
                                        userExcel.Achievement_four = nvalue;
                                    }
                                    if (j == userExcelIndexMode.Remarks)
                                    {
                                        userExcel.Remarks = nvalue;
                                    }
                                }
                                ListUserExcels.Add(userExcel);
                            }
                            reader.Close();
                        }
                    }

                    return ListUserExcels;
                }
                else
                {
                    //路径为空
                    return null;
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"读取xlsx工作表数据出错：{e.Message}");
                return null;
            }
        }

        public void SheetDataIndex(string name, int index)
        {
            switch (name)
            {
                case "考号":
                    userExcelIndexMode.Exam_number = index;
                    break;
                case "地区":
                    userExcelIndexMode.Region = index;
                    break;
                case "场地":
                    userExcelIndexMode.Venue = index;
                    break;
                case "项目组":
                    userExcelIndexMode.Project_team = index;
                    break;
                case "姓名":
                    userExcelIndexMode.Name = index;
                    break;
                case "性别":
                    userExcelIndexMode.Sex = index;
                    break;
                case "学校":
                    userExcelIndexMode.School = index;
                    break;
                case "年级":
                    userExcelIndexMode.Grade = index;
                    break;
                case "班级":
                    userExcelIndexMode.ClassName = index;
                    break;
                case "班级号数":
                    userExcelIndexMode.Number_class = index;
                    break;
                case "项目":
                    userExcelIndexMode.Project = index;
                    break;
                case "考试日期":
                    userExcelIndexMode.Exam_date = index;
                    break;
                case "场次":
                    userExcelIndexMode.Sessions = index;
                    break;
                case "组号":
                    userExcelIndexMode.Group_number = index;
                    break;
                case "组内序号":
                    userExcelIndexMode.Intra_group_serial_number = index;
                    break;
                case "成绩1":
                    userExcelIndexMode.Achievement_one = index;
                    break;
                case "成绩2":
                    userExcelIndexMode.Achievement_two = index;
                    break;
                case "成绩3":
                    userExcelIndexMode.Achievement_three = index;
                    break;
                case "成绩4":
                    userExcelIndexMode.Achievement_four = index;
                    break;
                case "备注":
                    userExcelIndexMode.Remarks = index;
                    break;
            }

        }

        /// <summary>
        /// 是否与设定的数据相符
        /// </summary>
        /// <returns></returns>
        public bool IsSheetDataIndex()
        {
            if (userExcelIndexMode.Exam_number != -1 && userExcelIndexMode.Region != -1 && userExcelIndexMode.Venue != -1 && userExcelIndexMode.Project_team != -1 && userExcelIndexMode.Name != -1 && userExcelIndexMode.Sex != -1 && userExcelIndexMode.School != -1
                && userExcelIndexMode.Grade != -1 && userExcelIndexMode.Number_class != -1 && userExcelIndexMode.Project != -1 && userExcelIndexMode.Exam_date != -1 && userExcelIndexMode.Sessions != -1 && userExcelIndexMode.Group_number != -1
                && userExcelIndexMode.Intra_group_serial_number != -1 && userExcelIndexMode.Achievement_one != -1 && userExcelIndexMode.Achievement_two != -1 && userExcelIndexMode.Achievement_three != -1 && userExcelIndexMode.Achievement_four != -1
                && userExcelIndexMode.Remarks != -1)
            {
                return true;
            }

            return false;
        }
    }
}

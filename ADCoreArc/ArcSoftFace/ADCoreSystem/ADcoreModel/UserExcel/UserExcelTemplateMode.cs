using ArcSoftFace.GameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.ADCoreSystem.ADcoreModel 
{
    public class UserExcelTemplateMode
    {
        /// <summary>
        ///  考生数据模板
        /// </summary>
         
            /// <summary>
            /// 考号
            /// </summary>
            private string _exam_number;

            /// <summary>
            /// 地区
            /// </summary>
            private string _region;

            /// <summary>
            /// 场地
            /// </summary>
            private string _venue;

            /// <summary>
            /// 项目组
            /// </summary>
            private string _project_team;

            /// <summary>
            /// 姓名
            /// </summary>
            private string _name;

            /// <summary>
            /// 性别
            /// </summary>
            private string _sex;

            /// <summary>
            /// 学校
            /// </summary>
            private string _school;

            /// <summary>
            /// 年级
            /// </summary>
            private string _grade;

            /// <summary>
            /// 班级
            /// </summary>
            private string _className;

            /// <summary>
            /// 班级号数
            /// </summary>
            private string _number_class;

            /// <summary>
            /// 项目
            /// </summary>
            private string _project;

            /// <summary>
            /// 考试日期
            /// </summary>
            private string _exam_date;

            /// <summary>
            /// 场次
            /// </summary>
            private string _sessions;

            /// <summary>
            /// 组号
            /// </summary>
            private string _group_number;

            /// <summary>
            /// 组内序号
            /// </summary>
            private string _intra_group_serial_number;

            /// <summary>
            /// 成绩1
            /// </summary>
            private string _achievement_one;

            /// <summary>
            /// 成绩2
            /// </summary>
            private string _achievement_two;

            /// <summary>
            /// 成绩3
            /// </summary>
            private string _achievement_three;

            /// <summary>
            /// 成绩4
            /// </summary>
            private string _achievement_four;

            /// <summary>
            /// 备注
            /// </summary>
            private string _remarks;





            /// <summary>
            /// 考号
            /// </summary>
            [ModeHelp(true, "Exam_number", "string", false, false)]
            public string 考号 { get { return _exam_number; } set { _exam_number = value; } }

            /// <summary>
            /// 地区
            /// </summary>
            [ModeHelp(true, "Region", "string", false, false)]
            public string 地区 { get { return _region; } set { _region = value; } }

            /// <summary>
            /// 场地
            /// </summary>
            [ModeHelp(true, "Venue", "string", false, false)]
            public string 场地 { get { return _venue; } set { _venue = value; } }

            /// <summary>
            /// 项目组
            /// </summary>
            [ModeHelp(true, "Project_team", "string", false, false)]
            public string 项目组 { get { return _project_team; } set { _project_team = value; } }

            /// <summary>
            /// 姓名
            /// </summary>
            [ModeHelp(true, "Name", "string", false, false)]
            public string 姓名 { get { return _name; } set { _name = value; } }

            /// <summary>
            /// 性别
            /// </summary>
            [ModeHelp(true, "Sex", "string", false, false)]
            public string 性别 { get { return _sex; } set { _sex = value; } }

            /// <summary>
            /// 学校
            /// </summary>
            [ModeHelp(true, "School", "string", false, false)]
            public string 学校 { get { return _school; } set { _school = value; } }

            /// <summary>
            /// 年级
            /// </summary>
            [ModeHelp(true, "Grade", "string", false, false)]
            public string 年级 { get { return _grade; } set { _grade = value; } }

            /// <summary>
            /// 班级
            /// </summary>
            [ModeHelp(true, "ClassName", "string", false, false)]
            public string 班级 { get { return _className; } set { _className = value; } }

            /// <summary>
            /// 班级号数
            /// </summary>
            [ModeHelp(true, "Number_class", "string", false, false)]
            public string 班级号数 { get { return _number_class; } set { _number_class = value; } }

            /// <summary>
            /// 项目
            /// </summary>
            [ModeHelp(true, "Project", "string", false, false)]
            public string 项目 { get { return _project; } set { _project = value; } }

            /// <summary>
            /// 考试日期
            /// </summary>
            [ModeHelp(true, "Exam_date", "string", false, false)]
            public string 考试日期 { get { return _exam_date; } set { _exam_date = value; } }

            /// <summary>
            /// 场次
            /// </summary>
            [ModeHelp(true, "Sessions", "string", false, false)]
            public string 场次 { get { return _sessions; } set { _sessions = value; } }

            /// <summary>
            /// 组号
            /// </summary>
            [ModeHelp(true, "Group_number", "string", false, false)]
            public string 组号 { get { return _group_number; } set { _group_number = value; } }

            /// <summary>
            /// 组内序号
            /// </summary>
            [ModeHelp(true, "Intra_group_serial_number", "string", false, false)]
            public string 组内序号 { get { return _intra_group_serial_number; } set { _intra_group_serial_number = value; } }

            /// <summary>
            /// 成绩1
            /// </summary>
            [ModeHelp(true, "Achievement_one", "string", false, false)]
            public string 成绩1 { get { return _achievement_one; } set { _achievement_one = value; } }

            /// <summary>
            /// 成绩2
            /// </summary>
            [ModeHelp(true, "Achievement_two", "string", false, false)]
            public string 成绩2 { get { return _achievement_two; } set { _achievement_two = value; } }

            /// <summary>
            /// 成绩3
            /// </summary>
            [ModeHelp(true, "Achievement_three", "string", false, false)]
            public string 成绩3 { get { return _achievement_three; } set { _achievement_three = value; } }

            /// <summary>
            /// 成绩4
            /// </summary>
            [ModeHelp(true, "Achievement_four", "string", false, false)]
            public string 成绩4 { get { return _achievement_four; } set { _achievement_four = value; } }

            /// <summary>
            /// 备注
            /// </summary>
            [ModeHelp(true, "Remarks", "string", false, false)]
            public string 备注 { get { return _remarks; } set { _remarks = value; } }


    }
}


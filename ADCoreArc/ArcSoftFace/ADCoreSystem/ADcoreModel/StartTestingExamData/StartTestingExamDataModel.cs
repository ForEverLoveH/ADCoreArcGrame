using ArcSoftFace.GameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.ADCoreSystem.ADcoreModel
{
    public class StartTestingExamDataModel
    {
        /// <summary>
        /// 考试日期
        /// </summary>
        private string _exam_date;

        /// <summary>
        /// 考试日期
        /// </summary>
        [ModeHelp(true, "Exam_date", "string", false, false)]
        public string Exam_date { get { return _exam_date; } set { _exam_date = value; } }
    }
}

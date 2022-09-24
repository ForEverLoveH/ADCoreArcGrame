using ArcSoftFace.GameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.ADCoreSystem.ADcoreModel 
{
    public  class GroupDataModel
    {
        /// <summary>
        /// 组号
        /// </summary>
        private string _group_number;

        /// <summary>
        /// 组号
        /// </summary>
        [ModeHelp(true, "Group_number", "string", false, false)]
        public string Group_number
        {
            get { return _group_number; }
            set { _group_number = value; }
        }
    }
}

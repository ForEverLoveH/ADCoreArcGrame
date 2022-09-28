using ArcSoftFace.GameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.ADCoreSystem.ADcoreModel 
{
    /// <summary>
    ///  人脸数据模型
    /// </summary>
    public  class FaceDataModel
    { 
        private long _id;
        private string groupID;
        private string name;
        private byte[] facefeature;
        /// <summary>
        /// Id
        /// </summary>
        [ModeHelp(true, "ID", "integer", false, true, true)]
        public long ID { get { return _id; } set { _id = value; } }
        [ModeHelp(true ,"GroupID","string",false,false)]
        public string GroupID { get { return groupID; } set { groupID = value; } }
        [ModeHelp(true ,"Name","string ",false,false)]
        public string Name { get { return name; } set { name = value; } }
        [ModeHelp(true, "FaceFeature","blob", false, false)]
        public byte[] FaceFeature
        {
            get { return facefeature; } set { facefeature = value; } 
        }

    }
}

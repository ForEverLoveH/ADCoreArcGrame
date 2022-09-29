using ArcSoftFace.GameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.ADCoreSystem
{
    public  class FaceData
    {
        private string groupID;
        private string name;
        private byte[] facefeature;

        [ModeHelp(true, "GroupID", "string", false, false)]
        public string GroupID { get { return groupID; } set { groupID = value; } }
        [ModeHelp(true, "Name", "string ", false, false)]
        public string Name { get { return name; } set { name = value; } }
        [ModeHelp(true, "FaceFeature", "blob", false, false)]
        public byte[] FaceFeature
        {
            get { return facefeature; }
            set { facefeature = value; }
        }
    }
}

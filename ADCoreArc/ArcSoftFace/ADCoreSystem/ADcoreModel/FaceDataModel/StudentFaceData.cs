using ArcFaceSDK.Entity;
using ArcSoftFace.GameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.ADCoreSystem.ADCoreModel
{
    public  class StudentFaceData
    {
          
         public  string groupID { get; set; }
         public  string Name { get; set; }
         public FaceFeature faceFeature { get; set; }
    }
    public   class FaceDataModel
    {

        private int id { get;set; }
        private  string groupID { get; set; }
        private  string name { get; set; }

        private string facedata { get; set; }
        [ModeHelp(false, "ID", "integer", false, false)]
        public  int ID { get => id; set => value = id; }
        [ModeHelp(false ,"GroupID","string",false ,false )]
        public string GroupID { get => groupID; set => value = groupID; }
        [ModeHelp(false, "Name", "string", false, false)]
        public string Name { get => name; set => value = name; }
        [ModeHelp(false, "Facedata", "string", false, false)]
        public  string Facedata { get => facedata; set => value = facedata; }
        



    }
    public  class FaceData
    {
        private string groupID { get; set; }
        private string name { get; set; }

        private string facedata { get; set; }
        
        [ModeHelp(false, "GroupID", "string", false, false)]
        public string GroupID { get => groupID; set => value = groupID; }
        [ModeHelp(false, "Name", "string", false, false)]
        public string Name { get => name; set => value = name; }
        [ModeHelp(false, "Facedata", "string", false, false)]
        public string Facedata { get => facedata; set => value = facedata; }
    }
}

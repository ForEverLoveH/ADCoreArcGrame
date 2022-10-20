using ArcSoftFace.GameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.ADCoreSystem.ADcoreModel
{
    public  class AdminModel
    {
        private int _id;
        private string _user;
        private string _Phone;
        private string _password;
        [ModeHelp(true, "Id", "interger", false, true, true)]
        public int Id { get { return _id; } set { _id = value; } }
        [ModeHelp(true, "User", "string", false, false)]
        public string User { get { return _user; } set { _user = value; } }
        [ModeHelp(true ,"Phone","string",false,true ,true )]
        public  string phone { get => _Phone;set=> _Phone = value; }
        [ModeHelp(true, "Password", "string", false, false)]
        public string Password { get { return _password; } set { _password = value; } }
    }

}

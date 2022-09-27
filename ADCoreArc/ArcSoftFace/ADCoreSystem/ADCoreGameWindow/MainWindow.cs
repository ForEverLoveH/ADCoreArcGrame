using ArcSoftFace.ADCoreSystem.ADCoreGameSys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem.ADCoreGameWindow
{
    public partial class MainWindow : Form
    {
        MainSys mainSys= new MainSys();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PersonImportBtn_Click(object sender, EventArgs e)
        {
            PersonImportSys personImportSys = new PersonImportSys();
            personImportSys.Init();
        }

        private void NewGroupBtn_Click(object sender, EventArgs e)
        {
            NewGroupSys newGroupSys = new NewGroupSys();
            newGroupSys.Init();
        }

        private void StartTestingBtn_Click(object sender, EventArgs e)
        {
            StartTestingSys startTestingSys = new StartTestingSys();
            startTestingSys.Init(); 
        }

        private void ExportGradeBtn_Click(object sender, EventArgs e)
        {
            ExportGradeSys  exportGradeSys= new ExportGradeSys();
            exportGradeSys.Init();
        }

        private void LoginSettingBtn_Click(object sender, EventArgs e)
        {
            var s = mainSys.GetLoginData();
           if ( s == "admin")
           {
                LoginSettingOfAdminSys loginSettingOfAdminSys = new LoginSettingOfAdminSys();
                loginSettingOfAdminSys.Init();

           }
            else if(s == "user")
            {
                LoginSettingOfUserSys  loginSettingOfUserSys = new LoginSettingOfUserSys();
                loginSettingOfUserSys.Init();

            }

        }
    }
}

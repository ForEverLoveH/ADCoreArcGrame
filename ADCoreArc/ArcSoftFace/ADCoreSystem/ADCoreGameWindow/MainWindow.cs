using ArcSoftFace.ADCoreSystem.ADCoreGameSys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private void HelpBtn_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + GameConst.helpDocPath;
            if (File.Exists(path))
                System.Diagnostics.Process.Start(path);
            else
                MessageBox.Show("帮助文件丢失\r\n" + path);
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + GameConst.GenDocPath;
            if(File.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
            }
            else
                MessageBox.Show("驱动文件丢失\r\n" + path);
        }
    }
}

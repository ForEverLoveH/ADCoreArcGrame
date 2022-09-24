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

        }
    }
}

using ArcSoftFace.ADCoreSystem.ADCoreGameWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.ADCoreSystem.ADCoreGameSys
{
     public class MainSys
    {
        public static MainSys Instance;

        public MainWindow mainWindow;
        public void Awake()
        {
            Instance = this;
        }

        public void Init()
        {
            StartGame();
        }
        public void StartGame(bool isActive = true)
        {
            if (isActive)
            {
                if (mainWindow == null)
                {
                    mainWindow = new MainWindow();
                    mainWindow.ShowDialog();
                }
                else
                {
                    if (mainWindow.IsDisposed)
                    {
                        mainWindow = new MainWindow();
                        mainWindow.ShowDialog();

                    }
                    else
                    {
                        mainWindow.Activate();
                    }
                }
            }
            else
            {
                if (mainWindow != null)
                {
                    mainWindow.Dispose();
                }
            }
        }
    }
}

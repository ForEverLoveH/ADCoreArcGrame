using ArcSoftFace.ADCoreSystem.ADCoreGameWindow;
using ArcSoftFace.GameNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem 
{
     public class ExportGradeSys
    {
        public static ExportGradeSys Instance;
        public static ExportGradeWindow exportGradeWindow;
        LocalNetClient localNetClient = new LocalNetClient();
        public string Num;
        public int sqlNumber = 0;
        public void Awake()
        {
            Instance = this;

        }
        public void Init()
        {
            StartGame();
        }

        public void Req_GetKey()
        {
            GameMsg msg = new GameMsg()
            {
                cmd = CMD.Req_GetKeyByExportGrade,
            };
            localNetClient.SendMsg(msg);
        }

        public void Rsp_GetKey(GameMsg msg)
        {
            if (string.IsNullOrEmpty(msg.rsp_GetKey.number))
            {
                Num = "人员数据:0 条";
                exportGradeWindow.Set_Txt_Title(Num);
            }
            else
            {
                Num = $"人员数据:{msg.rsp_GetKey.number}条";
                exportGradeWindow.Set_Txt_Title(Num);
                int.TryParse(msg.rsp_GetKey.number.ToString(), out sqlNumber);
            }
        }


        public void Req_ExportGrade()
        {
            GameMsg msg = new GameMsg
            {
                cmd = CMD.Req_ExportGrade,
            };
            localNetClient.SendMsg(msg);

        }

        private void StartGame(bool isActive = true)
        {
            if (isActive)
            {
                if (exportGradeWindow == null)
                {
                    exportGradeWindow = new ExportGradeWindow();
                    exportGradeWindow.Show();
                }
                else
                {
                    if (exportGradeWindow.IsDisposed)
                    {
                        exportGradeWindow = new ExportGradeWindow();
                        exportGradeWindow.Show();
                    }
                    else
                    {
                        exportGradeWindow.Activate();
                    }
                }
            }
            else
            {
                if (exportGradeWindow != null)
                {
                    exportGradeWindow.Dispose();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Rsp_ExportGrade(GameMsg msg)
        {
            if (msg.errorType == ErrorType.userDataIsEmpty)
            {
                MessageBox.Show("没有成绩数据，或者成绩数据已经超过百万！！");
                return;
            }
            else
            {
                exportGradeWindow.Export_Excel(msg.rsp_ExportGrade.userExcel);
            }
        }
    }
}

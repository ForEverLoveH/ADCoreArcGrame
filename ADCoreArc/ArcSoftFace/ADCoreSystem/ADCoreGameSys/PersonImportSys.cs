using ArcSoftFace.ADCoreSystem.ADCoreGameWindow;
using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.GameNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem.ADCoreGameSys
{
     public class PersonImportSys
    {
        public static PersonImportSys Instance;
        public static PersonImportWindow personImportWindow;
        LoginSys loginSys = new LoginSys();
        LocalNetClient localNetClient = new LocalNetClient();
        public int sqlNumber = 0;
        public static string num;

        public string f1;
        public string f2;
        public void Awake()
        {
            Instance = this;
        }
        public void Init()
        {
            StartGame();
        }
        public void StartGame(bool IsActive = true)
        {
            if (IsActive)
            {
                if (personImportWindow == null)
                {
                    personImportWindow = new PersonImportWindow();
                    personImportWindow.Show();
                    // loginSys.CloseLoginWindow();
                }
                else
                {
                    if (personImportWindow.IsDisposed)
                    {
                        personImportWindow = new PersonImportWindow();
                        personImportWindow.Show();
                         
                    }
                    else
                    {
                        personImportWindow.Activate();
                    }
                }
            }
            else
            {
                if (personImportWindow != null)
                {
                    personImportWindow.Dispose();
                }
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        public void Req_GetKey()
        {
            GameMsg msg = new GameMsg
            {
                cmd = CMD.Req_GetKeyByPersonImport,
            };
            localNetClient.SendMsg(msg);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Rsp_GetKey(GameMsg msg)
        {
            if (string.IsNullOrEmpty(msg.rsp_GetKey.number))
            {
                num = "人员数据:0 条";
                personImportWindow.Set_Txt_Title(num);
            }
            else
            {
                num = $"人员数据:{msg.rsp_GetKey.number}条";
                personImportWindow.Set_Txt_Title(num);
                int.TryParse(msg.rsp_GetKey.number.ToString(), out sqlNumber);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userExcels"></param>
        public void Req_NewImport(List<UserExcel> userExcels)
        {
            GameMsg MSG = new GameMsg()
            {
                cmd = CMD.Req_New_Import,
                req_New_Import = new Req_New_Import()
                {
                    ListUserExcel = userExcels,
                },
            };
            localNetClient.SendMsg(MSG);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetData"></param>
        public void Req_AddImport(List<UserExcel> sheetData)
        {
            GameMsg msg = new GameMsg()
            {
                cmd = CMD.Req_Add_Import,

                req_Add_Import = new Req_Add_Import()
                {
                    userExcels = sheetData,
                }
            };
            localNetClient.SendMsg(msg);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Rsp_New_Import(GameMsg msg)
        {
            if (msg.errorType == ErrorType.New_Import_IsEmpty)
            {
                Console.WriteLine("导入的*xlsx/*xls数据不能为空");
            }
            else if (msg . rsp_New_Import .IsReq_New_Import==0)
            {
                Console.WriteLine("全新导入成功！！");
                MessageBox.Show("全新导入成功！！");
            }
            else if (msg.rsp_New_Import.IsReq_New_Import == -1)
            {
                Console.WriteLine("全新导入不成功！！");
            }
            Req_GetKey();
        }
        /// <summary>
        /// 
        /// </summary>
        public void New_import()
        {
            personImportWindow.New_import();
        }

        public void Rsp_Add_Import(GameMsg msg)
        {
            if (msg.errorType == ErrorType.Add_Import_IsEmptyl)
            {
                MessageBox.Show("导入的*xls/*xlsx数据不能为空");
            }
            else if (msg.rsp_Add_Import.IsReq_Add_Import == 0)
            {
                MessageBox.Show("新增导入成功！！");
            }
            else if (msg.rsp_Add_Import.IsReq_Add_Import == -1)
            {
                MessageBox.Show("新增导入不成功！！");
            }
            Req_GetKey();
        }
    }
}


using ArcFaceSDK;
using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.ADCoreSystem.ADCoreGameSys;
using ArcSoftFace.Arcsoft;
using ArcSoftFace.GameCommon;
using ArcSoftFace.GameNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace
{
    public class GameRoot
    {
        static   ArcFaceManage arcFaceManage = new ArcFaceManage();
        static  LoginSys LoginSys = new LoginSys();
        static MainSys MainSys = new MainSys();
        static  PersonImportSys PersonImportSys = new PersonImportSys();
        static VerificationSys VerificationSys = new VerificationSys(); 
        static NewGroupSys NewGroupSys = new NewGroupSys(); 
        static LoginSettingOfAdminSys LoginSettingOfAdminSys = new LoginSettingOfAdminSys();
        static LoginSettingOfUserSys LoginSettingOfUserSys = new LoginSettingOfUserSys();
        static ExportGradeSys ExportGradeSys = new ExportGradeSys();
        static StartTestingSys  StartTestingSys   = new StartTestingSys();

        static LoginSql LoginSql = new LoginSql();  
        static Personnel_Import_Sql Personnel_Import_Sql=new Personnel_Import_Sql();
        static  NewGroupSql NewGroupSql = new NewGroupSql();  
        static  LoginSettingSql  LoginSettingSql = new LoginSettingSql();  
        static ExportGradeSql ExportGradeSql = new ExportGradeSql();
        static StartTestingSql  startTestingSql = new StartTestingSql();
       


        static LocalNetClient localNetClient = new LocalNetClient();
        static LocalNetServer locaNetServer = new LocalNetServer();
        static SqliteDB sqliteDB = new SqliteDB();

        public  void StartGame()
        {

            Awake();
            Start();
        }

        private  void Start()
        {
             
            sqliteDB.Init();
            LoginSys.Init();
             

        }

        private  void Awake()
        {
            arcFaceManage.Awake();
            LoginSys .Awake();
            MainSys .Awake();
            PersonImportSys.Awake();
            VerificationSys.Awake();
            NewGroupSys.Awake();
            LoginSettingOfAdminSys.Awake();
            LoginSettingOfUserSys.Awake();
            ExportGradeSys.Awake();
            StartTestingSys.Awake();



            localNetClient.Awake();
            locaNetServer.Awake();
            sqliteDB .Awake();
            
            LoginSql .Awake();
            Personnel_Import_Sql.Awake();
            NewGroupSql .Awake();
            LoginSettingSql .Awake();
            NewGroupSql.Awake ();
            startTestingSql.Awake();
        }
    }
}

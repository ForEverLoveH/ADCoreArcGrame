using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.ADCoreSystem.ADcoreModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.GameCommon
{
    public class FaceRegisterSql
    {
        /// <summary>
        /// 添加人脸数据
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="Name"></param>
        /// <param name="faceFeature"></param>
        public  void  AddFaceData(string groupId,string Name, byte[] faceFeature)
        {
            string path = Application.StartupPath + GameConst.FaceDBPath;
            SqlDbCommand sql = new SqlDbCommand(path);
            int s = sql.IsCreateTable(GameConst.DBFaceData);
            if(s== 0)
            {
                Console.WriteLine("数据表不存在！！");
                CreateTable(GameConst.DBFaceData);
            }
            InsertFaceData(groupId, Name, faceFeature);
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="name"></param>
        /// <param name="faceFeature"></param>
        /// <returns></returns>
        private int  InsertFaceData(string groupID, string name, byte[] faceFeature)
        {   

            try
            {


                return 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine("数据库插入异常"+ex.Message);
                return -1;
            }
        }
        private void CreateTable(string name )
        {
            try
            {
                SQLiteConnection liteConnection = new SQLiteConnection("DataSource = FaceData.sqlite;Version=3;");
                liteConnection.SetPassword("");
                liteConnection.Open();
                string st = $"Create table {name}(Id interger , GroupID  VARCHAR ,Name VARCHAR,FaceData blob)";
                SQLiteCommand cmd = new SQLiteCommand(st, liteConnection);

                

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
    }
}

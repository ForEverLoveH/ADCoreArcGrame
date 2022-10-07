using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.ADCoreSystem.ADcoreModel;
using ArcSoftFace.ADCoreSystem.ADCoreModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
            SqlDbCommand    sql = new SqlDbCommand (path);
            int isExten = sql.IsCreateTable(GameConst.DBFaceData);
            if (isExten == 0)
            {
                Console.WriteLine("数据库不存在存在");
                sql.CreateTable<FaceDataModel>(GameConst.DBFaceData);
                 

            }
           else
           {
                Console.WriteLine("数据表已经存在！！");
                 
           }
            FaceData faceData = new FaceData()
            {
                Name = Name,
                GroupID = groupId,
                Facedata = Encoding.Unicode.GetString(faceFeature),
            };
            sql.Insert<FaceData>(faceData, GameConst.DBFaceData);



        }
         
         // 创建表
        private void CreateFaceDataTable(string tableName)
        {
             
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="name"></param>
        /// <param name="faceFeature"></param>
        /// <returns></returns>
        private int  InsertFaceData(string groupID, string name, byte[] faceFeature,string tableName)
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
         
    }
}

using ArcFaceSDK.Entity;
using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.ADCoreSystem.ADCoreModel;
using ArcSoftFace.GameNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace ArcSoftFace.GameCommon
{
    public class FaceRegisterSql
    {

        SQLiteHelper sqlhelper = null;

        /// <summary>
        /// 添加人脸数据
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="Name"></param>
        /// <param name="faceFeature"></param>
        public bool AddFaceData(List<StudentFaceData> faceDatas)
        {
            String Path = Application.StartupPath + GameConst.FaceDBPath;
            sqlhelper = new SQLiteHelper(Path);
            sqlhelper.OpenSQLite();
            if (sqlhelper.TableExit(GameConst.DBFaceData))
            {
                for (int i = 0; i < faceDatas.Count; i++)
                {
                    InsertDataToFaceTable(faceDatas[i].groupID, faceDatas[i].Name, faceDatas[i].faceFeature.feature, sqlhelper);
                }
                sqlhelper.CloseSQLite();
                return true ;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除人脸数据
        /// </summary>
        /// <param name="msg"></param>
        public  int DelectFaceFeature(GameMsg msg)
        {
            String Path  = Application.StartupPath +GameConst.FaceDBPath;
            sqlhelper = new SQLiteHelper(Path);
            sqlhelper.OpenSQLite();
            if (sqlhelper.TableExit(GameConst.DBFaceData))
            {
                int s = DelectFaceData(msg, sqlhelper);
                sqlhelper.CloseSQLite();
                return  s ;
            }
            else
            {
                return -1;
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="helper"></param>
        /// <returns></returns>
        public  DataSet Req_GetFaceFeature(GameMsg msg,  string path)
        { 
            SQLiteHelper sqlhelper = new SQLiteHelper(path);
            sqlhelper.OpenSQLite();
            if (sqlhelper.TableExit(GameConst.DBFaceData))
            {
                var s = GetFaceFeature(msg, sqlhelper);
                sqlhelper.CloseSQLite();
                return   s  ;
            }
            else
            {
                return null;
            }
        }

        private DataSet GetFaceFeature(GameMsg msg, SQLiteHelper helper)
        {
            helper.EnsureConnection();
            string sql = $"select * from {GameConst.DBFaceData} WHERE GroupID ={msg.req_GetFaceFeature.groupID}";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("GroupID",msg.req_GetFaceFeature.groupID)
            };
            DataSet dataSet = helper.ExecuteDataSet(sql, parameters);
            if (dataSet != null)
            {
                return dataSet;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="msg"></param>
        private int  DelectFaceData(GameMsg msg ,SQLiteHelper sQLiteHelper)
        {
            String SQLwhere = $"GroupID={msg.req_DelectFaxeData.userExcel.Group_number},Name ={msg.req_DelectFaxeData.userExcel.Name } ,FaceData = {msg.req_DelectFaxeData. faceData.faceFeature}";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
               new SQLiteParameter("GroupID",msg.req_DelectFaxeData.userExcel.Group_number),
               new SQLiteParameter("Name",msg.req_DelectFaxeData.userExcel.Name),
               new SQLiteParameter("FaceData",msg.req_DelectFaxeData. faceData.faceFeature),
            };
           return sQLiteHelper.Delete(GameConst.DBFaceData, SQLwhere, parameters);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="name"></param>
        /// <param name="faceFeature"></param>
        /// <param name="sqlhelper"></param>
        /// <returns></returns>
        private  bool  InsertDataToFaceTable(string groupId, string name, byte[] faceFeature, SQLiteHelper sqlhelper)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("GroupID", groupId);
            data.Add("Name", name);
            data.Add("FaceData", faceFeature);
            int result = sqlhelper.InsertData(GameConst.DBFaceData, data);
            if(result == 0)
            {
                return false;
            }
            else
            {
                return true; 
            }
        }
    }
}
         

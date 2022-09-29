using ArcSoftFace.ADCoreSystem;
using ArcSoftFace.ADCoreSystem.ADcoreModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.GameCommon
{
    public class FaceRegisterSql
    {
        /// <summary>
        ///  是都存在表
        /// </summary>
        /// <param name="faceDataName"></param>
        public  bool  IsExtenFaceData(string faceDataName)
        {
            string path = Application.StartupPath + GameConst.FaceDBPath;
            SqlDbCommand sql = new SqlDbCommand(path);
            int isExistenceAdminData = sql.IsCreateTable(faceDataName);
            if(isExistenceAdminData == 1)
            {
                Console.WriteLine("数据表存在");
                return true;
            }
            else
            {
                sql.CreateTable<FaceDataModel>(path);
                sql.Dispose();
                return true;
            }
        }
    }
}

using System.Data.OleDb;
using System.Data;
using System;

namespace ArcSoftFace.ADCoreSystem
{
    public class ExcelToDataSet
    {
        public DataTable GetExcelDatable(string filePath, string table)
        {
            const string cmdText = "Provider=Microsoft.Ace.OleDb.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            System.Data.DataTable dt = null;
            OleDbConnection dbConnection = new OleDbConnection(string.Format(cmdText, filePath));
            try
            {
                if (dbConnection.State == ConnectionState.Broken || dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                    System.Data.DataTable schemaTable = dbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    //获取Excel的第一个Sheet名称

                    string sheetName = schemaTable.Rows[0]["TABLE_NAME"].ToString().Trim();
                    string strsql = "SELECT *FROM [" + sheetName + "]";
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(strsql, dbConnection);
                    DataSet dataSet = new DataSet();
                    oleDbDataAdapter.Fill(dataSet, table);
                    dt = dataSet.Tables[0];
                    return dt;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();

            }
            return null;
        }
    }

}
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.Streaming;
using NPOI.XSSF.UserModel;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sunny.UI.UIDataGridView;

namespace ArcSoftFace.ADCoreSystem
{
    public  class ExcelMo
    {
        /// <summary>
        /// 创建Excel
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="name"></param>
        public void EstablishExcel(string Path, string Name, string Type, List<string> ListSheetName)
        {
            IWorkbook workbook = null;
            FileStream filestream = null;
            if (Type == ".xls")
            {
                workbook = new HSSFWorkbook();
                filestream = new FileStream(Path + "/" + Name + ".xls", FileMode.Create);
            }
            if (Type == ".xlsx")
            {
                workbook = new XSSFWorkbook();
                workbook = new SXSSFWorkbook((XSSFWorkbook)workbook);
                filestream = new FileStream(Path + "/" + Name + ".xlsx", FileMode.Create);

            }

            // 新增表
            foreach (string sheetName in ListSheetName)
            {
                workbook.CreateSheet(sheetName);
            }
            workbook.Write(filestream);
            workbook.Close();
            filestream.Close();
            filestream.Dispose();
        }


        /// <summary>
        /// Excel中添加标题
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="SheetName"></param>
        /// <param name="Type"></param>
        public void WiteToExcel<T>(string Path, string SheetName, string Type)
        {
            var type = typeof(T);
            var propertes = type.GetProperties();
            IWorkbook workbook = null;
            FileStream filestream = null;
            if (Type == ".xls")
            {
                filestream = new FileStream(Path, FileMode.Open, FileAccess.Read);
                workbook = new HSSFWorkbook(filestream);
            }
            if (Type == ".xlsx")
            {
                filestream = new FileStream(Path, FileMode.Open, FileAccess.Read);
                workbook = new XSSFWorkbook(filestream);
                workbook = new SXSSFWorkbook((XSSFWorkbook)workbook);
            }

            ISheet sheet = workbook.GetSheet(SheetName);
            IRow row = sheet.CreateRow(sheet.LastRowNum);
            ICell cell;
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 12; // 字体大小  直接对应Excel中的字体大小
            font.FontName = "宋体"; //跟Excel中的字体值一样，直接写对应的名称即可
            font.Color = IndexedColors.Red.Index;
            font.Boldweight = (short)FontBoldWeight.Bold;
            cellStyle.SetFont(font);

            for (int i = 0; i < propertes.Length; i++)
            {
                cell = row.CreateCell(i);
                cell.CellStyle = cellStyle;
                cell.SetCellValue(propertes[i].Name);
            }

            // 第三步：写入文件流
            using (FileStream stream = new FileStream(Path, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(stream);
                workbook.Close();
                filestream.Close();
                filestream.Dispose();
            }
        }


    }
}


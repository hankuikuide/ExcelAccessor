using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han.DataAccess.Excel.Npoi
{
    public class NpoiExcelAccess : IExcelAccess
    {
        /// <summary>
        /// 根据文件扩展名，获取workbook实例
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        private IWorkbook GetWorkBook(string ext, Stream stream)
        {
            IWorkbook workbook = null;

            switch (ext)
            {
                case ".xlsx":
                    workbook = new XSSFWorkbook(stream);
                    break;
                case ".xls":
                    workbook = new HSSFWorkbook(stream);
                    break;
                default:
                    break;
            }

            return workbook;
        }

        /// <summary>
        /// 加载数据，可设置跳过前几行
        /// </summary>
        /// <param name="path"></param>
        /// <param name="skipRows"></param>
        /// <returns></returns>
        public DataSet Load(ExcelConfig config)
        {
            using (var fileStream = new FileStream(config.Path, FileMode.Open, FileAccess.Read))
            {
                var ds = new DataSet();
                var ext = Path.GetExtension(config.Path);

                // 新建IWorkbook对象 
                var workbook = this.GetWorkBook(ext, fileStream);

                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    DataTable dt = GetDataTable(sheet, config.HeaderIndex);
                    ds.Tables.Add(dt);
                }

                return ds;
            }
        }

        private DataTable GetDataTable(ISheet sheet, int headerIndex)
        {
            var dt = new DataTable();
            // 获取表头行
            var headerRow = sheet.GetRow(headerIndex);
            var cellCount = GetCellCount(sheet, headerIndex);

            // 设置表头
            for (int i = 0; i < cellCount; i++)
            {
                if (headerRow.GetCell(i) != null)
                {
                    dt.Columns.Add(headerRow.GetCell(i).StringCellValue, typeof(string));
                }
            }
            
            for (int i = headerIndex + 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dr = dt.NewRow();
                FillDataRow(row, ref dr);
                dt.Rows.Add(dr);
            }

            dt.TableName = sheet.SheetName;
            return dt;

        }

        private void FillDataRow(IRow row, ref DataRow dr)
        {
            if (row != null)
            {
                for (int j = 0; j < dr.Table.Columns.Count; j++)
                {
                    ICell cell = row.GetCell(j);

                    if (cell != null)
                    {
                        switch (cell.CellType)
                        {
                            case CellType.Blank:
                                dr[j] = DBNull.Value;
                                break;
                            case CellType.Boolean:
                                dr[j] = cell.BooleanCellValue;
                                break;
                            case CellType.Numeric:
                                if (DateUtil.IsCellDateFormatted(cell))
                                {
                                    dr[j] = cell.DateCellValue;
                                }
                                else
                                {
                                    dr[j] = cell.NumericCellValue;
                                }
                                break;
                            case CellType.String:
                                dr[j] = cell.StringCellValue;
                                break;
                            case CellType.Error:
                                dr[j] = cell.ErrorCellValue;
                                break;
                            case CellType.Formula:
                                // cell = evaluator.EvaluateInCell(cell) as HSSFCell;
                                dr[j] = cell.ToString();
                                break;
                            default:
                                throw new NotSupportedException(string.Format("Catched unhandle CellType[{0}]", cell.CellType));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取表头列数
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="skipRows"></param>
        /// <returns></returns>
        private int GetCellCount(ISheet sheet, int headerIndex)
        {
            var headerRow = sheet.GetRow(headerIndex);

            return headerRow.LastCellNum;
        }
    }
}

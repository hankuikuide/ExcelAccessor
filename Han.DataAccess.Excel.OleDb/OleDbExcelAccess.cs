using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han.DataAccess.Excel.OleDb
{
    public class OleDbExcelAccess : IExcelAccess
    {
        private string strConn;

        public OleDbExcelAccess()
        {
            //TODO 需要根据扩展名，来决定 使用哪一种连接字符串
            strConn = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = {0};Extended Properties='Excel 8.0;IMEX=1;HDR=NO'";
        }

        public DataSet Load(ExcelConfig config)
        {
            var ds = new DataSet();
            var sheets = GetSheetNames(config.Path);
            var strConnTmp = string.Format(strConn, config.Path);

            foreach (string sheet in sheets)
            {
                using (var oleConn = new OleDbConnection(strConnTmp))
                {
                    var strsql = "SELECT * FROM [" + sheet + "]";
                    var oleDaExcel = new OleDbDataAdapter(strsql, oleConn);
                    oleConn.Open();
                    oleDaExcel.Fill(ds, sheet);
                }
            }

            return ds;
        }

        /// <summary>
        /// 获取Excel的所有的sheet
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string[] GetSheetNames(string path)
        {
            DataTable dt = new DataTable();
            if (File.Exists(path))
            {
                string strConnTmp = string.Format(strConn, path);
                using (var conn = new OleDbConnection(strConnTmp))
                {
                    conn.Open();
                    //返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等    
                    dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
                }
                //包含excel中表名的字符串数组
                var sheetNames = new List<string>();
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    string tableName = dt.Rows[k]["TABLE_NAME"].ToString();
                    //修正出现兼容性视图名称时过滤表名
                    if (!tableName.Substring(tableName.Length - 1).Equals("_"))
                    {
                        sheetNames.Add(tableName);
                    }
                }
                return sheetNames.ToArray();

            }
            return null;

        }
    }


}

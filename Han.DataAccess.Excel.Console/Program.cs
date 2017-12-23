using Han.DataAccess.Excel.Npoi;
using Han.DataAccess.Excel.OleDb;
using Han.DataAccess.Extension;
using Han.DataAccess.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han.DataAccess.Excel.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            TestPropertyMapper();

            TestColumnMapper();

            TestJsonMapper();
        }

        public static void TestJsonMapper()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("PatientName", typeof(string));
            dt.Columns.Add("BillDate", typeof(DateTime));

            dt.Rows.Add(1, "HKK", DateTime.Now);
            dt.Rows.Add(2, "WQ", DateTime.Now);
            dt.Rows.Add(3, "HS", DateTime.Now);

            var jsonBills = dt.ToJson(rowMapper: new JsonMapper());
        }

        public static void TestPropertyMapper()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("PatientName", typeof(string));
            dt.Columns.Add("BillDate", typeof(DateTime));

            dt.Rows.Add(1, "HKK", DateTime.Now);
            dt.Rows.Add(2, "WQ", DateTime.Now);
            dt.Rows.Add(3, "HS", DateTime.Now);

            List<Bill> bills = dt.ToList<Bill>(rowMapper: new PropertyColumnMapper<Bill>());
        }
        private static void TestColumnMapper()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("结算日期", typeof(DateTime));

            dt.Rows.Add(1, "HKK", DateTime.Now);
            dt.Rows.Add(2, "WQ", DateTime.Now);
            dt.Rows.Add(3, "HS", DateTime.Now);

            List<Bill> bills = dt.ToList<Bill>(rowMapper: new ColumnAttributeMapper<Bill>());
        }

        private void TestExcelImport()
        {
            var excelFile = Path.Combine("C:\\Users\\hankk\\Desktop\\ICON", "03行人工抽查.xls");

            ExcelConfig config = new ExcelConfig()
            {
                Path = excelFile,
                HeaderIndex = 1
            };

            IExcelAccess npoiAccess = new NpoiExcelAccess();
            var npoiDs = npoiAccess.Load(config);

            IExcelAccess oleDbAccess = new OleDbExcelAccess();
            var oleDs = oleDbAccess.Load(config);
        }
    }
}

using Han.DataAccess.Excel.Npoi;
using Han.DataAccess.Excel.OleDb;
using System;
using System.Collections.Generic;
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
            var excelFile = Path.Combine("C:\\Users\\hankk\\Desktop\\ICON", "03行人工抽查.xls");

            ExcelConfig config = new ExcelConfig()
            {
                Path= excelFile,
                HeaderIndex = 1
            };

            IExcelAccess npoiAccess = new NpoiExcelAccess();
            var npoiDs = npoiAccess.Load(config);

            IExcelAccess oleDbAccess = new OleDbExcelAccess();
            var oleDs = oleDbAccess.Load(config);
        }
    }
}

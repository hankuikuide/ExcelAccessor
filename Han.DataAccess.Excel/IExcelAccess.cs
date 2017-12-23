using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han.DataAccess.Excel
{
    public interface IExcelAccess
    {
        DataSet Load(ExcelConfig config);
    }
}

#region copyright
// <copyright file="Bill"  company="CIS"> 
// Copyright (c) CIS. All Right Reserved
// </copyright>
// <author>hankk</author>
// <datecreated>2017/12/23 23:39:26</datecreated>
#endregion
using Han.DataAccess.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han.DataAccess.Excel.Console
{
    public class Bill
    {
        [Column("序号")]
        public int Id { get; set; }

        [Column("姓名")]
        public string PatientName { get; set; }

        [Column("结算日期")]
        public DateTime BillDate  { get; set; }

    }
}

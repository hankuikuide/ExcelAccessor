#region copyright
// <copyright file="ExcelConfig"  company="CIS"> 
// Copyright (c) CIS. All Right Reserved
// </copyright>
// <author>hankk</author>
// <datecreated>2017/12/23 15:18:32</datecreated>
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han.DataAccess.Excel
{
    public class ExcelConfig
    {
        /// <summary>
        /// �ļ��洢·��
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// �ļ���ͷ����������
        /// </summary>
        public int HeaderIndex { get; set; }
    }
}

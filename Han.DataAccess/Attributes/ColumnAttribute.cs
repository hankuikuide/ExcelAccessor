#region copyright
// <copyright file="DataFieldAttribute"  company="CIS"> 
// Copyright (c) CIS. All Right Reserved
// </copyright>
// <author>hankk</author>
// <datecreated>2017/12/23 23:58:54</datecreated>
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han.DataAccess.Attributes
{
    public class ColumnAttribute : Attribute
    {
        public string Name { get; set; }
        

        public ColumnAttribute(string fieldName)
        {
            this.Name = fieldName;
        }

    }
}

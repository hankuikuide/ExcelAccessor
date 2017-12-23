#region copyright
// <copyright file="PropertyColumnMapper"  company="CIS"> 
// Copyright (c) CIS. All Right Reserved
// </copyright>
// <author>hankk</author>
// <datecreated>2017/12/24 1:26:25</datecreated>
#endregion
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Han.DataAccess.Mapper
{
    public class PropertyColumnMapper<T> : IRowMapper<T>
    {
        public T MapRow(DataRow dr)
        {
            T t = (T)Activator.CreateInstance(typeof(T));
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                PropertyInfo propertyInfo = t.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                if (propertyInfo != null && dr[i] != DBNull.Value)
                    propertyInfo.SetValue(t, dr[i], null);
            }

            return t;
        }
    }
}

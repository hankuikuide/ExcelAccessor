#region copyright
// <copyright file="ColumnRowMapper"  company="CIS"> 
// Copyright (c) CIS. All Right Reserved
// </copyright>
// <author>hankk</author>
// <datecreated>2017/12/24 1:23:01</datecreated>
#endregion
using Han.DataAccess.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han.DataAccess.Mapper
{
    public class ColumnAttributeMapper<T> : IRowMapper<T>
    {
        public T MapRow(DataRow dr)
        {
            T t = (T)Activator.CreateInstance(typeof(T));
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                var props = t.GetType().GetProperties();

                foreach (var prop in props)
                {
                    var p = prop.GetCustomAttributes(true).OfType<ColumnAttribute>().FirstOrDefault();

                    if (p != null && p.Name == dr.Table.Columns[i].ColumnName)
                    {
                        if (dr[i] != DBNull.Value)
                            prop.SetValue(t, dr[i], null);
                    }
                }
                
            }

            return t;
        }
    }
}

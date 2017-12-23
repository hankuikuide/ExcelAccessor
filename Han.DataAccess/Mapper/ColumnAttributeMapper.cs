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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Han.DataAccess.Mapper
{
    public class ColumnAttributeMapper<T> : IRowMapper<T>
    {
        private static Dictionary<string, Dictionary<string, string>> ColumnPropertyMapper= new Dictionary<string, Dictionary<string, string>>();

        public ColumnAttributeMapper()
        {
            if (!ColumnPropertyMapper.ContainsKey(typeof(T).Name))
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();

                var props = typeof(T).GetProperties();

                foreach (var prop in props)
                {
                    var attribute = prop.GetCustomAttributes(true).OfType<ColumnAttribute>().FirstOrDefault();
                    dict.Add(attribute.Name, prop.Name);

                }
                ColumnPropertyMapper.Add(typeof(T).Name, dict);
            }

        }

        public T MapRow(DataRow dr)
        {
            T t = (T)Activator.CreateInstance(typeof(T));
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                if (ColumnPropertyMapper.ContainsKey(t.GetType().Name))
                {
                    var dict = ColumnPropertyMapper[t.GetType().Name];
                    var property = dict[dr.Table.Columns[i].ColumnName];

                    PropertyInfo propertyInfo = t.GetType().GetProperty(property);
                    if (propertyInfo != null && dr[i] != DBNull.Value)
                        propertyInfo.SetValue(t, dr[i], null);
                }
            }

            return t;
        }
    }
}

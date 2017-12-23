#region copyright
// <copyright file="DataTableExtension"  company="CIS"> 
// Copyright (c) CIS. All Right Reserved
// </copyright>
// <author>hankk</author>
// <datecreated>2017/12/23 23:15:58</datecreated>
#endregion
using Han.DataAccess.Attributes;
using Han.DataAccess.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Han.DataAccess.Extension
{
    public static class DataTableExtension
    {
        public static List<T> ToList<T>(this DataTable dt, IRowMapper<T> rowMapper)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            List<T> entites = new List<T>();
            
            foreach (DataRow dr in dt.Rows)
            {
                var t = rowMapper.MapRow(dr);
                
                entites.Add(t);
            }
            return entites;
        }
        
    }
}

#region copyright
// <copyright file="JsonMapper"  company="CIS"> 
// Copyright (c) CIS. All Right Reserved
// </copyright>
// <author>hankk</author>
// <datecreated>2017/12/24 5:59:32</datecreated>
#endregion
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Han.DataAccess.Mapper
{
    public class JsonMapper: IRowMapper<StringBuilder>
    {
        public StringBuilder MapRow(DataRow dr)
        {
            StringBuilder strBuilder = new StringBuilder();
            StringWriter strWriter = new StringWriter(strBuilder);
            
            using (JsonWriter writer = new JsonTextWriter(strWriter))
            {
                writer.WriteStartObject();

                foreach (var col in dr.Table.Columns)
                {
                    var value = dr.Field<object>(col.ToString());
                 
                    writer.WritePropertyName(col.ToString());
                    writer.WriteValue(value);
                }

                writer.WriteEndObject();

            }
            return strBuilder;

        }
    }
}

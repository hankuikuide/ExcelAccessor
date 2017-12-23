#region copyright
// <copyright file="ModelHandler"  company="CIS"> 
// Copyright (c) CIS. All Right Reserved
// </copyright>
// <author>hankk</author>
// <datecreated>2017/12/23 22:59:13</datecreated>
#endregion
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Han.DataAccess
{
    /// <summary>
    /// DataTable��ʵ���໥��ת��
    /// </summary>
    /// <typeparam name="T">ʵ����</typeparam>
    public class ModelHandler<T> where T : new()
    {
        #region DataTableת����ʵ����

        /// <summary>
        /// �������б���DataSet�ĵ�һ�������ʵ����
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        public List<T> FillModel(DataSet ds)
        {
            if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return FillModel(ds.Tables[0]);
            }
        }

        /// <summary>  
        /// �������б���DataSet�ĵ�index�������ʵ����
        /// </summary>  
        public List<T> FillModel(DataSet ds, int index)
        {
            if (ds == null || ds.Tables.Count <= index || ds.Tables[index].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return FillModel(ds.Tables[index]);
            }
        }

        /// <summary>  
        /// �������б���DataTable���ʵ����
        /// </summary>  
        public List<T> FillModel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            List<T> entites = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                //T model = (T)Activator.CreateInstance(typeof(T));  
                T model = new T();
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                    if (propertyInfo != null && dr[i] != DBNull.Value)
                        propertyInfo.SetValue(model, dr[i], null);
                }

                entites.Add(model);
            }
            return entites;
        }

        /// <summary>  
        /// ��������DataRow���ʵ����
        /// </summary>  
        public T FillModel(DataRow dr)
        {
            if (dr == null)
            {
                return default(T);
            }

            //T model = (T)Activator.CreateInstance(typeof(T));  
            T model = new T();

            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                if (propertyInfo != null && dr[i] != DBNull.Value)
                    propertyInfo.SetValue(model, dr[i], null);
            }
            return model;
        }

        #endregion

        #region ʵ����ת����DataTable

        /// <summary>
        /// ʵ����ת����DataSet
        /// </summary>
        /// <param name="entites">ʵ�����б�</param>
        /// <returns></returns>
        public DataSet FillDataSet(List<T> entites)
        {
            if (entites == null || entites.Count == 0)
            {
                return null;
            }
            else
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(FillDataTable(entites));
                return ds;
            }
        }

        /// <summary>
        /// ʵ����ת����DataTable
        /// </summary>
        /// <param name="entites">ʵ�����б�</param>
        /// <returns></returns>
        public DataTable FillDataTable(List<T> entites)
        {
            if (entites == null || entites.Count == 0)
            {
                return null;
            }
            DataTable dt = CreateData(entites[0]);

            foreach (T model in entites)
            {
                DataRow dataRow = dt.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(model, null);
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// ����ʵ����õ���ṹ
        /// </summary>
        /// <param name="t">ʵ����</param>
        /// <returns></returns>
        private DataTable CreateData(T t)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
            }
            return dataTable;
        }

        #endregion
    }
}

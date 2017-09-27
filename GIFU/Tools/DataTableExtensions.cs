using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eLibrary.Common
{
    public class DataTableExtensions
    {
        public static T GetModel<T>(DataRow row) where T : class, new()
         {
             if (row == null) return null;
 
             T result = new T();
 
             Type type = typeof(T);
 
             System.Reflection.PropertyInfo[] ps = type.GetProperties();
 
             foreach (System.Reflection.PropertyInfo p in ps)
             {
                 if (row.Table.Columns.Contains(p.Name))
                 {
                     object value = row[p.Name];
 
                     if (!(value is DBNull))
                     {
                         try
                         {
                             p.SetValue(result, value, null);
                         }
                         catch
                         {
 
                         }
                     }
                 }
             }
 
             return result;
         }
         public static List<T> GetModelList<T>(DataTable dt) where T : class, new()
         {
             List<T> list = new List<T>();
             foreach (DataRow row in dt.Rows)
             {
                 list.Add(GetModel<T>(row));
             }
             return list;
         }
 
         public static List<T> GetModelList<T>(IEnumerable<DataRow> rows) where T : class, new()
         {
             List<T> list = new List<T>();
             foreach (DataRow row in rows)
             {
                 list.Add(GetModel<T>(row));
             }
             return list;
         }
  }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Acciona.App_Code
{
    public static class MetodosExtensiones
    {
        /// <summary>
        /// Obtiene el nombre de la PC del usuario (cliente)
        /// </summary>
        /// <param name="request">Permite leer los valores HTTP enviados por un cliente durante una solicitud web</param>
        /// <returns>String con el nombre de la máquina del cliente</returns>
        public static string ObtenerUsuarioPC(HttpRequest request)
        {
            return System.Net.Dns.GetHostEntry(request.ServerVariables["REMOTE_ADDR"]).HostName;
        }

        /// <summary>
        /// Obtiene el ip de la PC del usuario (cliente)
        /// </summary>
        /// <param name="request">Permite leer los valores HTTP enviados por un cliente durante una solicitud web</param>
        /// <returns>String con el nombre de la máquina del cliente</returns>
        public static string ObtenerUsuarioIP(HttpRequest request)
        {
            return request.ServerVariables["REMOTE_ADDR"];
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }

            return ds;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace tNext
{
    public static class DataExtensions
    {
        public static void SetItemFromRow<T>(this DataRow row, T item)
            where T : new()
        {
            if (row != null)
            {
                foreach (DataColumn c in row.Table.Columns)
                {
                    PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                    if (p != null && row[c] != DBNull.Value)
                    {
                        p.SetValue(item, row[c], null);
                    }
                }
            }

        }

        public static T CreateItemFromRow<T>(this DataRow row)
            where T : new()
        {
            T item = new T();
            if (row != null)
            {
                row.SetItemFromRow(item);
            }
            return item;
        }

        public static List<T> CreateListFromTable<T>(this DataTable tbl)
            where T : new()
        {
            List<T> lst = new List<T>();

            if (tbl != null)
            {
                foreach (DataRow r in tbl.Rows)
                {
                    lst.Add(r.CreateItemFromRow<T>());
                }
            }

            return lst;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AssociationBids.GlobalUtilities
{
    public static class AssoBidsUtility
    {
        public static string ConvertDataTableTojSonString(DataTable dataTable)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                   new System.Web.Script.Serialization.JavaScriptSerializer();

            List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();

            Dictionary<String, Object> row;

            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<String, Object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    if (Convert.ToString(col.DataType.Name) == "Byte[]")
                    {
                        if (dr[col] != DBNull.Value)
                        {
                            try
                            {
                                row.Add(col.ColumnName, "data:image;base64," + Convert.ToBase64String((byte[])dr[col]));
                            }
                            catch (Exception ex)
                            {
                                row.Add(col.ColumnName, ex.Message);
                            }
                        }
                        else
                        {
                            row.Add(col.ColumnName, "");
                        }
                    }
                    else
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                }
                tableRows.Add(row);
            }
            return serializer.Serialize(tableRows);
        }
        public static List<Dictionary<String, Object>> ConvertDataTableToListForJson(DataTable dataTable)
        {

            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                   new System.Web.Script.Serialization.JavaScriptSerializer();

            List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();

            Dictionary<String, Object> row;

            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<String, Object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                tableRows.Add(row);
            }
            return (tableRows);
            //return serializer.Serialize(tableRows);
        }


    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BusinessLayer
{
    public static class ExtensionMethods
    {
        public static object ToDBNull(this object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return DBNull.Value;
            }

            return value;
        }

        public static object ToDBInt(this object value)
        {
            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return 0;
            }

            return value;
        }


        //public static T CastTo<T>(this object x) where T : struct
        //{
        //    return (T) (x == null ? null : (T?)Convert.ChangeType(x, typeof(T)));
        //}
        public static T CastTo<T>(this object value)
        {
            if(value == DBNull.Value)
            {
                value = null;
            }

            if (typeof(T) == typeof(int?))
            {
                int? h;
                try
                {
                    h = (int?)value;
                }
                catch(Exception es)
                {
                    value = Convert.ToInt32(value);
                    h = (int?)value;
                }
                value = h;

            }
            if (typeof(T) == typeof(int))
            {
                int h = Convert.ToInt32(value);
                value = h;

            }
            if (typeof(T) == typeof(Int16))
            {
                Int16 h = Convert.ToInt16(value);
                value = h;

            }
            if (typeof(T) == typeof(Int16?))
            {

                Int16? h;
                try
                {
                    h = (Int16?)value;
                }
                catch (Exception es)
                {
                    value = Convert.ToInt16(value);
                    h = (Int16?)value;
                }

                value = h;

            }
            if (typeof(T) == typeof(byte))
            {
                byte h = Convert.ToByte(value);
                value = h;

            }
            if (typeof(T) == typeof(byte?))
            {

                byte? h;
                try
                {
                    h = (byte?)value;
                }
                catch (Exception es)
                {
                    value = Convert.ToByte(value);
                    h = (byte?)value;
                }
             
              
                value = h;

            }
            if (typeof(T) == typeof(double?))
            {
              
                double? h;
                try
                {
                    h = (double?)value;
                }
                catch (Exception es)
                {
                    value = Convert.ToDouble(value);
                    h = (double?)value;
                }

                value = h;

            }
            if (typeof(T) == typeof(double))
            {
                double h = Convert.ToDouble(value);
                value = h;

            }
            if (typeof(T) == typeof(bool))
            {
                bool h=true;
                if (string.IsNullOrEmpty(Convert.ToString(value)) || Convert.ToString(value).ToLower() =="false")
                {
                    h = false;
                }
       
               
                value = h;

            }
            if (typeof(T) == typeof(DateTime?))
            {

                DateTime? h;
                try
                {
                    h = (DateTime?)value;
                }
                catch (Exception es)
                {
                    //value = Convert.ToDateTime(value);
                    value= DateTime.ParseExact(Convert.ToString(value), "yyyy-MM-dd hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    h = (DateTime?)value;
                }

                value = h;

            }
            if (typeof(T) == typeof(DateTime))
            {
                value = DateTime.ParseExact(Convert.ToString(value), "yyyy-MM-dd hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);

            }
            if (typeof(T) == typeof(string))
            {
                value = Convert.ToString(value); 

            }

            return (T)value;

        }

        public static void ValidateDictonary(this Dictionary<string,object> dic,params string[] keys)
        {
            try
            {
                bool found = false;
                for(int i = 0; i < keys.Length; i++)
                {
                    found = false;
                    foreach (string k in dic.Keys)
                    {
                             if(k== keys[i])
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        throw new Exception("Property \"" + keys[i] + "\" is missing in request.");
                    }

                }

            }
            catch(Exception es)
            {
                throw es;
            }

        }



        public static object ToMMDDYY(this string Date)
        {

            if (string.IsNullOrEmpty(Date))
                return DBNull.Value;
            else
                return DateTime.ParseExact(Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

        }


        public static object ReturnBlobImage(this byte[] data)
        {

            try
            {

                string image = string.Empty;

                image = "data:image/png;base64," + Convert.ToBase64String(data);


                return image;

            }
            catch (Exception es)
            {
                throw es;
            }



        }

        public static object ToXML(this DataTable data)
        {
            System.IO.StringWriter writer = new System.IO.StringWriter();
            data.WriteXml(writer);
            string result = writer.ToString();

            return result;
        }

        public static DataTable JsonArrayToDataTable(this object jsonArray)
        {
            string jsonString= JsonConvert.SerializeObject(JsonConvert.DeserializeObject(Convert.ToString(jsonArray)));
            DataTable dt = new DataTable();
            dt.TableName = "Table1";
            string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();
            foreach (string jSA in jsonStringArray)
            {
                string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");

                        if (idx != -1)
                        {
                            string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                            if (!ColumnsName.Contains(ColumnsNameString))
                            {
                                ColumnsName.Add(ColumnsNameString);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {
                dt.Columns.Add(AddColumnName);
            }
            foreach (string jSA in jsonStringArray)
            {
                string h = jSA.Replace("\\", "&");
                string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", "").Replace("\\","&"), "r");
               // string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        if (idx != -1)
                        {
                            string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                            string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                            nr[RowColumns] = RowDataString;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                dt.Rows.Add(nr);
            }
            return dt;
        }

        public static object ConvertToJson(this object data)
        {
            try
            {
                return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(data));

            }
            catch(Exception es)
            {
                throw new Exception( "Error while parsing data to json : " +es.Message);
            }
        }
       

        public static DataTable GetDataTableFromJsonString(this object jsonArray)
        {
            string json = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(Convert.ToString(jsonArray)));
            var jsonLinq = JArray.Parse(json);

            // Find the first array using Linq
            var srcArray = jsonLinq;
            var trgArray = new JArray();
            foreach (JObject row in srcArray.Children<JObject>())
            {
                var cleanRow = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    // Only include JValue types
                    if (column.Value is JValue)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(column.Value)))
                        {
                            cleanRow.Add(column.Name, null);
                        }
                        else
                        {
                            cleanRow.Add(column.Name, Convert.ToString(column.Value));
                        }
                      
                    }
                }
                trgArray.Add(cleanRow);
            }
            DataTable dt= new DataTable();
            
             dt=JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
            dt.TableName = "Table1";
            return dt;
        }

        public static void ValidateTableColumns(this DataTable DT,params string[] Columns)
        {
            try
            {

                int len = Columns.Length;
                bool found = false;

                foreach(string column in Columns)
                {
                    found = false;
                    foreach (DataColumn dc in DT.Columns)
                    {
                        if(column == dc.ColumnName)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                        throw new Exception("Following Column " + column + " not found");
                }

            }
            catch(Exception es)
            {
                throw es;
            }
        }
    }

  
}
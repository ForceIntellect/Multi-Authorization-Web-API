//*****************************************************************************
//    File           : 
//    Desc           : This class is used 
//    Author         : Yusuf
//    Date           : Sept 06, 2008
//*******************************************************************************
//      Change History
//*******************************************************************************
//      Date:       Author:             Description:
//      --------    --------            -----------------------------------------
// 01) 24/09/08     karry               Code Review and changes for three tier artit.
// 02)
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using MiddleLayerArchitecture.DataSetWebReference;

namespace BaseArchitecture.DataAccessLayer
{
    public class ClsDbProc : BaseArchitecture.DataBaseLayer.ClsDbConfig
    {

        protected SqlConnection conn;
        protected SqlTransaction tran;

        /// <summary>
        /// method to open the transaction to database
        /// </summary>
        /// <returns>null</returns>
        public void BeginTransaction()
        {
            try
            {
                conn = new SqlConnection();
                //using (conn = new SqlConnection())
                //{
                conn.ConnectionString = Connection();
                conn.Open();
                tran = conn.BeginTransaction();
                //}
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Transaction Error " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error " + ex);
            }
        }
        /// <summary>
        /// method to open the transaction to database
        /// </summary>
        /// <returns>null</returns>
        public void BeginTransaction(DatabaseName dbName)
        {
            try
            {
                conn = new SqlConnection();
                //using (conn = new SqlConnection())
                //{
                conn.ConnectionString = Connection(dbName);
                conn.Open();
                tran = conn.BeginTransaction();
                //}
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Transaction Error " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error " + ex);
            }
        }
        /// <summary>
        /// method to commit the transaction to database
        /// </summary>
        /// <returns>null</returns>
        public void CommitTransaction()
        {
            try
            {
                tran.Commit();
                tran = null;

                conn.Close();
                conn = null;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Transaction Error " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error " + ex);
            }
        }

        /// <summary>
        /// method to rollback the transaction to database
        /// </summary>
        /// <returns>null</returns>
        public void RollbackTransaction()
        {
            try
            {
                tran.Rollback();
                tran = null;

                conn.Close();
                conn = null;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Transaction Error " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error " + ex);
            }
        }

        /// <summary>
        /// method to close the transaction to database
        /// </summary>
        /// <returns>null</returns>
        public void CloseTransaction()
        {
            try
            {
                tran = null;
                if (conn != null)
                {
                    conn.Close();
                    conn = null;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Transaction Error " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error " + ex);
            }
        }

        /// <summary>
        /// method to open the connection to database
        /// </summary>
        /// <returns>null</returns>
        public void ConnectionOpen(SqlConnection con)
        {
            try
            {
                con.ConnectionString = Connection();
                con.Open();
            }
            catch (SqlException ex)
            {
                // Console.WriteLine("Connection Error " + ex);
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error " + ex);
            }
        }

        /// <summary>
        /// method to open the connection to database
        /// </summary>
        /// <returns>null</returns>
        public void ConnectionOpen(SqlConnection con, String conStr)
        {
            try
            {
                con.ConnectionString = conStr;
                con.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Connection Error " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error " + ex);
            }
        }

        /// <summary>
        /// method to excute query of sql
        /// </summary>
        /// <param name="sql">sql query</param>
        protected DataSet StrExecuteQuery(string sqlQuery)
        {
            using (SqlConnection _connect = new SqlConnection())
            {
                try
                {
                    ConnectionOpen(_connect);

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, _connect))
                    {
                        cmd.ExecuteNonQuery();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        _connect.Close();
                        return ds;
                    }
                }
                catch (Exception ex)
                {
                    _connect.Close();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// method to excute query of sql
        /// </summary>
        /// <param name="sql">sql query</param>
        protected DataSet StrExecuteQuery(string sqlQuery, SqlConnection con, SqlTransaction trans)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con, trans))
                {
                    cmd.ExecuteNonQuery();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// method to excute query of sql
        /// </summary>
        /// <param name="sql">sql query</param>
        protected object StrExecuteScalar(string sqlQuery)
        {
            using (SqlConnection _connect = new SqlConnection())
            {
                try
                {
                    ConnectionOpen(_connect);

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, _connect))
                    {
                        return cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _connect.Close();
                }
            }
        }
        /// <summary>
        /// method to excute query of sql
        /// </summary>
        /// <param name="sql">sql query</param>
        protected object StrExecuteScalar(string sqlQuery, SqlConnection con, SqlTransaction trans)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con, trans))
                {
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected DataSet DictionaryExecuteProc(string mName, Dictionary<string, object> mFieldsDict)
        {
            using (SqlConnection _connect = new SqlConnection())
            {

                try
                {
                    ConnectionOpen(_connect);

                    using (SqlCommand cmd = new SqlCommand(mName, _connect))
                    {
                        DataSet ds = Cmd_DictionaryExecuteProc(mName, mFieldsDict, cmd);

                        _connect.Close();
                        return ds;
                    }
                }
                catch (Exception ex)
                {
                    _connect.Close();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// First it open a connection then we loop through the rows of array 
        /// Using array we pass the parameter name and its value to the procedure
        /// </summary>
        /// <returns>DataSet which can be used at front end</returns>
        protected DataSet ArrayExecuteProc(string mName, object[,] mFieldsArray)
        {
            using (SqlConnection _connect = new SqlConnection())
            {

                try
                {
                    ConnectionOpen(_connect);

                    using (SqlCommand cmd = new SqlCommand(mName, _connect))
                    {
                        DataSet ds = Cmd_ArrayExecuteProc(mName, mFieldsArray, cmd);

                        _connect.Close();
                        return ds;
                    }
                }
                catch (Exception ex)
                {
                    _connect.Close();
                    throw ex;
                }
            }
        }


        private DataSet Cmd_ArrayExecuteProc(string mName, object[,] mFieldsArray, SqlCommand cmd)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 90000;

            if (mFieldsArray != null)
            {
                for (int row = 0; row < (mFieldsArray.Length / 2); row++)//number 2 is no of columns in array
                {
                    object paramValue;
                    string paramName = mFieldsArray[row, 1].ToString().Trim();

                    if (Convert.ToString(mFieldsArray[row, 0]).Trim() == "")
                        paramValue = DBNull.Value;
                    else
                        paramValue = mFieldsArray[row, 0];

                    if (System.Text.RegularExpressions.Regex.Match(paramName, "Image$").Success || (paramName == "@EmpPhoto"))         //Because of image uploading problem 
                    {
                        cmd.Parameters.Add(paramName, SqlDbType.Image);
                        cmd.Parameters[paramName].Value = paramValue;
                    }
                    else if (System.Text.RegularExpressions.Regex.Match(paramName, "_xml$").Success)         //Because of xml uploading data
                    {
                        cmd.Parameters.Add(paramName, SqlDbType.Xml);
                        cmd.Parameters[paramName].Value = paramValue;
                    }
                    else
                        cmd.Parameters.AddWithValue(paramName, paramValue);

                }
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        private DataSet Cmd_DictionaryExecuteProc(string mName, Dictionary<string, object> mFieldsDict, SqlCommand cmd)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 90000;

            object paramValue;
            string paramName;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(mName);

            object[][] arr = new object[mFieldsDict.Count][];

            if (mFieldsDict != null)
            {
                List<string> keys = new List<string>(mFieldsDict.Keys);
                for (int i = 0; i < keys.Count; i++)
                {
                    paramName = keys[i];
                    if (Convert.ToString(mFieldsDict[keys[i]]).Trim() == "")
                    {
                        paramValue = DBNull.Value;
                        sb.AppendLine("," + paramName + " = " + "NULL" + "");
                        arr[i] = new object[2] { "", paramName };
                    }
                    else if (mFieldsDict[keys[i]].GetType().FullName == "System.String")
                    {
                        paramValue = Convert.ToString(mFieldsDict[keys[i]]).Trim();
                        sb.AppendLine("," + paramName + " = '" + Convert.ToString(paramValue).Replace("'", "''") + "'");
                        arr[i] = new object[2] { paramValue, paramName };
                    }
                    else
                    {
                        paramValue = mFieldsDict[keys[i]];
                        sb.AppendLine("," + paramName + " = '" + Convert.ToString(paramValue).Replace("'", "''") + "'");
                        arr[i] = new object[2] { paramValue, paramName };
                    }

                    if (System.Text.RegularExpressions.Regex.Match(paramName, "Image").Success || (paramName == "@EmpPhoto"))         //Because of image uploading problem 
                    {
                        cmd.Parameters.Add(paramName, SqlDbType.Image);
                        cmd.Parameters[paramName].Value = paramValue;
                    }
                    else if (paramName.Contains("UserDefine"))         //Because of image uploading problem 
                    {
                        cmd.Parameters.Add(paramName, SqlDbType.Structured);
                        cmd.Parameters[paramName].Value = paramValue;
                    }
                    else if (System.Text.RegularExpressions.Regex.Match(paramName, "_xml").Success)         //Because of xml uploading data
                    {
                        cmd.Parameters.Add(paramName, SqlDbType.Xml);
                        cmd.Parameters[paramName].Value = paramValue;
                    }
                    else
                        cmd.Parameters.AddWithValue(paramName, paramValue);
                }
            }

            if (globalVar.Trace_Enable)
            {
                string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Trace.sql";

                string line = null;
                int LineOfFile = 0;

                if (File.Exists(filepath))
                {
                    StreamReader fs = new StreamReader(filepath);

                    LineOfFile = 0;
                    do
                    {
                        line = fs.ReadLine();

                        if (line != null)
                        {
                            sb.AppendLine(line.Trim());
                        }

                        LineOfFile += 1;

                    }
                    while (!(line == null));
                    fs.Close();
                }

                using (FileStream fs = File.Open(filepath, FileMode.Open, FileAccess.Write, FileShare.None))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(sb.ToString());
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            //ds.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection), LoadOption.PreserveChanges, new string[] { "1", "2" });

            DataSet ds;

            //Console.WriteLine("1:" + mName + DateTime.Now.ToLongTimeString());

            //MainServiceClass obj = new MainServiceClass();
            //ds = obj.GetCompany("192.168.12.11", globalVar.DatabaseName, globalVar.UserName, globalVar.Password, arr, mName);

            //Console.WriteLine("2:" + mName + DateTime.Now.ToLongTimeString());

            ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            //Console.WriteLine("3:" + mName + DateTime.Now.ToLongTimeString());

            /*
            DataSet ds;
            try
            {
                MainServiceClass obj = new MainServiceClass();
                ds = obj.GetCompany("192.168.12.11", globalVar.DatabaseName, globalVar.UserName, globalVar.Password, arr, mName);
            }
            catch (Exception)
            {

                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            */
            return ds;
        }

        /// <summary>
        /// use this procedure while saving /updating deleting data from table
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        protected DataSet DictionaryExecuteProc(string mName, Dictionary<string, object> mFieldsDict, SqlConnection con, SqlTransaction trans)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(mName, con, trans))
                {

                    DataSet ds = Cmd_DictionaryExecuteProc(mName, mFieldsDict, cmd);

                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// use this procedure while saving /updating deleting data from table
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        protected DataSet ArrayExecuteProc(string mName, object[,] mFieldsArray, SqlConnection con, SqlTransaction trans)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(mName, con, trans))
                {

                    DataSet ds = Cmd_ArrayExecuteProc(mName, mFieldsArray, cmd);

                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataSet Cmd_DictionaryExecuteProcDataReader(string mName, Dictionary<string, object> mFieldsDict, SqlCommand cmd, short NoOftable)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 90000;

            object paramValue;
            string paramName;

            if (mFieldsDict != null)
            {
                List<string> keys = new List<string>(mFieldsDict.Keys);
                for (int i = 0; i < keys.Count; i++)
                {
                    paramName = keys[i];
                    if (Convert.ToString(mFieldsDict[keys[i]]).Trim() == "")
                        paramValue = DBNull.Value;
                    else if (mFieldsDict[keys[i]].GetType().FullName == "System.String")
                    {
                        paramValue = Convert.ToString(mFieldsDict[keys[i]]).Trim();
                    }
                    else
                        paramValue = mFieldsDict[keys[i]];

                    if (System.Text.RegularExpressions.Regex.Match(paramName, "Image$").Success || (paramName == "@EmpPhoto"))         //Because of image uploading problem 
                    {
                        cmd.Parameters.Add(paramName, SqlDbType.Image);
                        cmd.Parameters[paramName].Value = paramValue;
                    }
                    else if (System.Text.RegularExpressions.Regex.Match(paramName, "_xml$").Success)         //Because of xml uploading data
                    {
                        cmd.Parameters.Add(paramName, SqlDbType.Xml);
                        cmd.Parameters[paramName].Value = paramValue;
                    }
                    else if (paramName.Contains("UserDefine"))         //Because of image uploading problem 
                    {
                        cmd.Parameters.Add(paramName, SqlDbType.Structured);
                        cmd.Parameters[paramName].Value = paramValue;
                    }
                    else
                        cmd.Parameters.AddWithValue(paramName, paramValue);

                }
            }

            SqlDataReader dr = cmd.ExecuteReader();
            DataSet ds = new DataSet();
            string[] strtable = new string[NoOftable];
            int iCount = 0;
            while (NoOftable > iCount)
            {
                strtable[iCount] = "Table" + iCount.ToString();
                iCount = iCount + 1;
            }
            ds.Load(dr, LoadOption.OverwriteChanges, strtable);
            return ds;
        }

        /// <summary>
        /// use this procedure while saving /updating deleting data from table
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        protected DataSet DictionaryExecuteProcReader(string mName, Dictionary<string, object> mFieldsDict, SqlConnection con, SqlTransaction trans, short NoOftable)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(mName, con, trans))
                {

                    DataSet ds = Cmd_DictionaryExecuteProcDataReader(mName, mFieldsDict, cmd, NoOftable);

                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// use this procedure while saving /updating deleting data from table
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        protected DataSet DictionaryExecuteProcReader(string mName, Dictionary<string, object> mFieldsDict, short NoOfTable)
        {

            using (SqlConnection _connect = new SqlConnection())
            {

                try
                {
                    ConnectionOpen(_connect);

                    SqlCommand cmd = new SqlCommand(mName, _connect);
                    {
                        SqlConnection.ClearPool(_connect);
                        DataSet ds = Cmd_DictionaryExecuteProcDataReader(mName, mFieldsDict, cmd, NoOfTable);


                        return ds;
                    }
                }
                catch (Exception ex)
                {
                    _connect.Close();
                    throw ex;
                }
            }
        }
    }
}




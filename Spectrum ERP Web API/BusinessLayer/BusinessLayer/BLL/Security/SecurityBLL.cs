using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SecurityBLL
    {
        DataAccessLayer.DTSecurity ObjDAL;
        Response response = new Response();
        public SecurityBLL()
        {
            ObjDAL = new DataAccessLayer.DTSecurity();
        }

        public object LoginSelect(Dictionary<string, object> Dic)
        {
            try
            {
                Dic.ValidateDictonary("LoginName", "Pwd");
                ObjDAL.LoginName = Dic["LoginName"].CastTo<string>();
                ObjDAL.Pwd = Dic["Pwd"].CastTo<string>();

                DataSet ds = ObjDAL.LoginSelect();


                if (ds.Tables[0].Rows.Count <= 0)
                    throw new Exception("Invalid Username/Password.");


                //DataTableReader reader = ds.Tables[0].CreateDataReader();

                //if (reader.HasRows)
                //{
                //    while (reader.Read())
                //    {
                //        if (Convert.ToString(reader[0]) == "0")
                //        {
                //            throw new Exception(Convert.ToString(reader[1]));
                //        }
                //    }

                //    reader.Close();
                //}

                response.StatusNo = 1;
                response.Message = "Success";

                DataTable table = ds.Tables[0];
                DataRow[] rows = table.Select();

                for (int i = 0; i < rows.Length; i++)
                {
                    response.Data = rows[i]["LoginNo"];
                }

               // response.Data = ds;
                //return ds;

            }
            catch (Exception es)
            {
                response.StatusNo = 0;
                response.Message = es.Message;

                throw es;
            }

            return response;
        }

        public object LoginFillValues(Dictionary<string, object> Dic)

        {
            try {
                Dic.ValidateDictonary("LoginNo");
                ObjDAL.LoginNo= Dic["LoginNo"].CastTo<Int16>();
                DataSet ds = ObjDAL.LoginFillValues();


                DataTableReader reader = ds.Tables[ds.Tables.Count - 1].CreateDataReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (Convert.ToString(reader[0]) == "0")
                        {
                            throw new Exception(Convert.ToString(reader[1]));
                        }
                   }

                   reader.Close();
               }

                return ds;
                
              }
          catch(Exception es)
              {
		          throw es;
              }    
      }

        public object AuthMenuSelect(Dictionary<string, object> Dic)

        {
            try
            {
                Dic.ValidateDictonary("LoginNo");
                ObjDAL.LoginNo = Dic["LoginNo"].CastTo<Int16>();
                DataSet ds = ObjDAL.AuthMenuSelect();


                DataTableReader reader = ds.Tables[ds.Tables.Count - 1].CreateDataReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (Convert.ToString(reader[0]) == "0")
                        {
                            throw new Exception(Convert.ToString(reader[1]));
                        }
                    }

                    reader.Close();
                }

                return ds;

            }
            catch (Exception es)
            {
                throw es;
            }
        }

    }
}

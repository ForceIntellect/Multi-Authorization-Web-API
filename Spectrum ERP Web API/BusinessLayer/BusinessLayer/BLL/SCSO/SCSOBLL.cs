using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Types;
using System.Data;

namespace BusinessLayer
{
    public class SCSOBLL : IBaseType
    {
        DataAccessLayer.DTSCSO ObjDAL;
        Response response = new Response();

        public SCSOBLL()
        {
            ObjDAL = new DataAccessLayer.DTSCSO();
        }
        public object Delete(Dictionary<string, object> Dic)
        {
            throw new NotImplementedException();
        }

        public object Fillvalues(Dictionary<string, object> Dic)
        {
            try
            {
                Dic.ValidateDictonary("LoginNo");
                ObjDAL.LoginNo = Dic["LoginNo"].CastTo<Int16>();
                DataSet ds = ObjDAL.AuthorizationSearchFillValues();


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

        public object LoadData(Dictionary<string, object> Dic)
        {
            try
            {
                Dic.ValidateDictonary("SCOrderAmendmentNo", "LoginNo");
                ObjDAL.SCOrderAmendmentNo = Dic["SCOrderAmendmentNo"].CastTo<int>();
                ObjDAL.LoginNo = Dic["LoginNo"].CastTo<int>();
                DataSet ds = ObjDAL.LoadPOForAuthorization();


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

        public object Save(Dictionary<string, object> Dic)
        {

            throw new NotImplementedException();

        }

        public object SearchData(Dictionary<string, object> Dic)
        {
            try
            {
                Dic.ValidateDictonary("LoginNo", "Where");
                ObjDAL.LoginNo = Dic["LoginNo"].CastTo<Int16>();
                ObjDAL.Where = Dic["Where"].CastTo<string>();
                DataSet ds = ObjDAL.AuthorizationSearchData();


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

        public object UpdateStatus(Dictionary<string, object> Dic)
        {
            throw new NotImplementedException();
        }
    }
}

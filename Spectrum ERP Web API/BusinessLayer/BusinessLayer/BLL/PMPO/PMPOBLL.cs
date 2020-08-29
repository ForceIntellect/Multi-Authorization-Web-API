using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Types;
using System.Data;
using System.Web.Script.Serialization;


namespace BusinessLayer
{
    public class PMPOBLL : IBaseType
    {
        DataAccessLayer.DTPMPO ObjDAL;
        Response response = new Response();

        public PMPOBLL()
        {
            ObjDAL = new DataAccessLayer.DTPMPO();
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
                Dic.ValidateDictonary("POAmendmentNo", "LoginNo");
                ObjDAL.POAmendmentNo = Dic["POAmendmentNo"].CastTo<int>();
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


        public object DownloadFile(Dictionary<string, object> Dic)
        {
            try
            {
                Dic.ValidateDictonary("DocNo", "Version");
                ObjDAL.DocNo = Dic["DocNo"].CastTo<Int32>();
                ObjDAL.Version = Dic["Version"].CastTo<Int16>();
                DataSet ds = ObjDAL.GetDocument();

                byte[] Docbytes = null;
                string DocName = "", DocType = "";

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Docbytes = (byte[])row["DocData"];
                    DocName = (string)row["DocName"];
                    DocType = (string)row["DocType"];
                }

                string FileName = DocName + DocType;




                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = int.MaxValue;
                DownloadDocument FileData = new DownloadDocument();
                //FileData.DocData = Docbytes;
                FileData.FileName = FileName;
                FileData.DocString = js.Serialize(Docbytes);


                return FileData;
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

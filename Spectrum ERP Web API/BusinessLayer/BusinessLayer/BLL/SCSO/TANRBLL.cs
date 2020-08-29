using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Types;
using System.Data;


namespace BusinessLayer
{
    public class TANRBLL : IBaseType
    {
        DataAccessLayer.DTTANR ObjDAL;
        Response response = new Response();
        

       public TANRBLL()
        {
            ObjDAL = new DataAccessLayer.DTTANR();
        }
        public object Delete(Dictionary<string, object> Dic)
        {
            throw new NotImplementedException();
        }

        public object Fillvalues(Dictionary<string, object> Dic)
        {
            throw new NotImplementedException();
        }

        public object LoadData(Dictionary<string, object> Dic)
        {
            throw new NotImplementedException();
        }

        public object Save(Dictionary<string, object> Dic)
        {
            try
            {
                Dic.ValidateDictonary("AuthDetailNo", "StatusNo", "Comment", "StatusChangeByLoginNo");
                ObjDAL.AuthDetailNo = Dic["AuthDetailNo"].CastTo<int>();
                ObjDAL.StatusNo = Dic["StatusNo"].CastTo<Byte>();
                ObjDAL.Comment = Dic["Comment"].CastTo<string>();
                ObjDAL.StatusChangeByLoginNo = Dic["StatusChangeByLoginNo"].CastTo<Int16>();
                DataSet ds = ObjDAL.SaveDocumentAuthorization();


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

                response.StatusNo = 1;
                response.Message = "Saved Successfully.";

                return response;

            }
            catch (Exception es)
            {
                throw es;
            }


        }

        public object SearchData(Dictionary<string, object> Dic)
        {
            throw new NotImplementedException();
        }
    }
}

using BusinessLayer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class DashboardBLL : IBaseType
    {

        DataAccessLayer.DTDashboard ObjDAL;
        Response response = new Response();

        public DashboardBLL()
        {
            ObjDAL = new DataAccessLayer.DTDashboard();
        }

        object IBaseType.Delete(Dictionary<string, object> Dic)
        {
            throw new NotImplementedException();
        }

        object IBaseType.Fillvalues(Dictionary<string, object> Dic)
        {
            throw new NotImplementedException();
        }

        object IBaseType.LoadData(Dictionary<string, object> Dic)
        {
            throw new NotImplementedException();
        }

        object IBaseType.Save(Dictionary<string, object> Dic)
        {
            throw new NotImplementedException();
        }

        object IBaseType.SearchData(Dictionary<string, object> Dic)
        {
            throw new NotImplementedException();
        }


        public object DrawDashboard(Dictionary<string, object> Dic)
        {
            try
            {
                Dic.ValidateDictonary("LoginNo");
                ObjDAL.LoginNo = Dic["LoginNo"].CastTo<Int16>();

                Dic.ValidateDictonary("DashboardNo");
                ObjDAL.DashboardNo = Dic["DashboardNo"].CastTo<Int16>();
                DataSet ds = ObjDAL.DrawDashboard();

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

        public object GetDataForTopNItem(Dictionary<string, object> Dic)
        {
            try
            {
                Dic.ValidateDictonary("LoginNo");
                ObjDAL.LoginNo = Dic["LoginNo"].CastTo<Int16>();

                Dic.ValidateDictonary("CardNo");
                ObjDAL.CardNo = Dic["CardNo"].CastTo<Int16>();

                Dic.ValidateDictonary("CompanyNos");
                ObjDAL.CompanyNos = Convert.ToString(Dic["CompanyNos"]);

                Dic.ValidateDictonary("FromDate");
                ObjDAL.FromDate = Convert.ToDateTime(Dic["FromDate"]); //Dic["FromDate"].CastTo<DateTime>();

                Dic.ValidateDictonary("ToDate");
                ObjDAL.ToDate = Convert.ToDateTime(Dic["ToDate"]);

                DataSet ds = ObjDAL.GetDataForTopNItem();

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

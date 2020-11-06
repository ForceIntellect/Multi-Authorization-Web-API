using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal class DADashboard
    {

        internal DataSet DrawDashboard(DTDashboard ODTDashboard)
        {
            Dictionary<string, object> dicPara = new Dictionary<string, object>();

            dicPara.Add(DAODashboard.LoginNo, ODTDashboard.LoginNo);
            dicPara.Add(DAODashboard.DashboardNo, ODTDashboard.DashboardNo);

            return ODTDashboard.ExecuteProc(DAODashboard.DrawDashboard, dicPara);
        }

        internal DataSet GetDataForTopNItem(DTDashboard ODTDashboard)
        {
            Dictionary<string, object> dicPara = new Dictionary<string, object>();

            dicPara.Add(DAODashboard.LoginNo, ODTDashboard.LoginNo);
            dicPara.Add(DAODashboard.CompanyNos, ODTDashboard.CompanyNos);
            dicPara.Add(DAODashboard.FromDate, ODTDashboard.FromDate);
            dicPara.Add(DAODashboard.ToDate, ODTDashboard.ToDate);
            dicPara.Add(DAODashboard.CardNo, ODTDashboard.CardNo);

            return ODTDashboard.ExecuteProc(DAODashboard.GetDataForTopNItem, dicPara);
        }

    }
}

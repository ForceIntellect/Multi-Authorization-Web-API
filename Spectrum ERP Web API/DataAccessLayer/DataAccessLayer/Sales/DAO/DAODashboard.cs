using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataAccessLayer
{
    internal class DAODashboard
    {

        //Stored Procedure
        internal static readonly string DrawDashboard = "Dashboard.DrawDashboard";
        internal static readonly string GetDataForTopNItem = "Dashboard.TopNItemValueWise";

        //variables
        internal static readonly string LoginNo = "@LoginNo";
        internal static readonly string CompanyNos = "@CompanyNos";
        internal static readonly string FromDate = "@FromDate";
        internal static readonly string ToDate = "@ToDate";
        internal static readonly string CardNo = "@CardNo";
        internal static readonly string DashboardNo = "@DashboardNo";
    }
}

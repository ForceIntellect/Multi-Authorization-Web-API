using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BusinessLayer;
using Spectrum_ERP_Web_API.Filters;


namespace Spectrum_ERP_Web_API.Controllers
{
    [ErrorFilter]
    public class DashboardController : ApiController
    {

        DashboardBLL DashboardBLL;
        public DashboardController()
        { 
            DashboardBLL = new DashboardBLL();
        }

        [HttpPost]
        public object DrawDashboard(Dictionary<string, object> Dic)
        {
            try
            {
                return Ok(DashboardBLL.DrawDashboard(Dic));

            }
            catch (Exception es)
            {
                throw es;
            }
        }

        [HttpPost]
        public object GetDataForTopNItem(Dictionary<string, object> Dic)
        {
            try
            {
                //
                return Ok(DashboardBLL.GetDataForTopNItem(Dic));
            }
            catch (Exception es)
            {
                throw es;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;

namespace Spectrum_ERP_Web_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}

using Spectrum_ERP_Web_API.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Cors;

namespace Spectrum_ERP_Web_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.MessageHandlers.Add(new MsgHandler());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalErrorHandler());
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
          //  config.Filters.Add(new ErrorFilter());
          

            // Web API routes
            config.MapHttpAttributeRoutes();

         

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

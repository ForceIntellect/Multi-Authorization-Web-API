using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Net;

namespace Spectrum_ERP_Web_API.Filters
{
    public class ErrorFilter : ExceptionFilterAttribute
    {

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {

            string Message = actionExecutedContext.Exception.Message;

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest,
                new
                {
                    StatusNo = 0,
                    Message = actionExecutedContext.Exception.Message

                });



        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Spectrum_ERP_Web_API.Filters
{
    public class GlobalErrorHandler:ExceptionHandler
    {

      
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            // Access Exception using context.Exception;  
            const string errorMessage = "An unexpected error occured";
            var response = context.Request.CreateResponse(HttpStatusCode.BadRequest,
                new
                {
                    StatusNo = 0,
                    Message = context.ExceptionContext.Exception.Message

                });
            response.Headers.Add("X-Error", errorMessage);
           context.Result = new ResponseMessageResult(response);

            return Task.FromResult(0);
        }
    }

    public class MsgHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Process request");
            // Call the inner handler.
            var response = await base.SendAsync(request, cancellationToken);

           
            Debug.WriteLine("Process response");
            return response;
        }
    }


   

}
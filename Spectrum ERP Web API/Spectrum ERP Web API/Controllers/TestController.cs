using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Spectrum_ERP_Web_API.Controllers
{
    public class TestController : ApiController
    {

        public int getUserCounts(int user)
        {

            return 5 * user;              
        }
    }
}

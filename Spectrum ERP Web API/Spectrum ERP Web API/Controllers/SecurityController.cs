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
    // [Route("[controller]/[action]")]

    [ErrorFilter]
    
  //  [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class SecurityController : ApiController
    {
        
            SecurityBLL SecurityBLL;

            public SecurityController()
            {
              SecurityBLL = new SecurityBLL();  
            }

            [HttpPost]
            public object LoginSelect(Dictionary<string, object> Dic)
            {
                try
                {
                    //throw new Exception("sdfsdf");

                    return Ok(SecurityBLL.LoginSelect(Dic));

                }
                catch (Exception es)
                {

                    throw es;
                }
            }

        public object LoginFillValues(Dictionary<string, object> Dic)
        {
            try
            {
                //throw new Exception("sdfsdf");

                return Ok(SecurityBLL.LoginFillValues(Dic));

            }
            catch (Exception es)
            {

                throw es;
            }
        }

        public object AuthMenuSelect(Dictionary<string, object> Dic)
        {
            try
            {
                //throw new Exception("sdfsdf");

                return Ok(SecurityBLL.AuthMenuSelect(Dic));

            }
            catch (Exception es)
            {

                throw es;
            }
        }
    }
}

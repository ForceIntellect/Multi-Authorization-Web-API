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
   // [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SCOrderController : ApiController
    {

        TANRBLL TANRBLL;
        SCSOBLL SCSOBLL;

        public SCOrderController()
        {

            TANRBLL = new TANRBLL();
            SCSOBLL = new SCSOBLL();

        }

        [HttpPost]
        public object LoadSCOrder(Dictionary<string, object> Dic)
        {
            try
            {
                //throw new Exception("sdfsdf");

                return Ok(SCSOBLL.LoadData(Dic));

            }
            catch (Exception es)
            {

                throw es;
            }
        }

        [HttpPost]
        public object UpdateStatus(Dictionary<string, object> Dic)
        {
            try
            {
                return Ok(TANRBLL.Save(Dic));

            }
            catch (Exception es)
            {
                throw es;
            }
        }

        [HttpPost]
        public object SearchData(Dictionary<string, object> Dic)
        {
            try
            {
                return Ok(SCSOBLL.SearchData(Dic));

            }
            catch (Exception es)
            {
                throw es;
            }
        }

        [HttpPost]
        public object SearchDataFillValues(Dictionary<string, object> Dic)
        {
            try
            {
                return Ok(SCSOBLL.Fillvalues(Dic));

            }
            catch (Exception es)
            {
                throw es;
            }
        }

    }
}

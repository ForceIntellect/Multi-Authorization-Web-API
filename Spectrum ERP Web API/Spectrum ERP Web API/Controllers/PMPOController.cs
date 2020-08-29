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
    public class PMPOController : ApiController
    {

        TANRBLL TANRBLL;
        PMPOBLL PMPOBLL;

        public PMPOController()
        {

            TANRBLL = new TANRBLL();
            PMPOBLL = new PMPOBLL();

        }

        [HttpPost]
        public object LoadPurchaseOrder(Dictionary<string, object> Dic)
        {
            try
            {
                //throw new Exception("sdfsdf");

                return Ok(PMPOBLL.LoadData(Dic));

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
                return Ok(PMPOBLL.SearchData(Dic));

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
                return Ok(PMPOBLL.Fillvalues(Dic));

            }
            catch (Exception es)
            {
                throw es;
            }
        }

        [HttpPost]
        public object DownloadFile(Dictionary<string, object> Dic)
        {
            try
            {
                return Ok(PMPOBLL.DownloadFile(Dic));
            }
            catch (Exception es)
            {
                throw es;
            }
        }

    }
}

using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    internal class DASCSO
    {

        internal DataSet AuthorizationSearchData(DTSCSO ODTSCSO)
        {
            Dictionary<string, object> dicPara = new Dictionary<string, object>();

            dicPara.Add(DAOSCSO.LoginNo, ODTSCSO.LoginNo);
            dicPara.Add(DAOSCSO.Where, ODTSCSO.Where);

            return ODTSCSO.ExecuteProc(DAOSCSO.AuthorizationSearchData, dicPara);
        }
        internal DataSet LoadPOForAuthorization(DTSCSO ODTSCSO)
        {
            Dictionary<string, object> dicPara = new Dictionary<string, object>();

            dicPara.Add(DAOSCSO.SCOrderAmendmentNo, ODTSCSO.SCOrderAmendmentNo);
            dicPara.Add(DAOSCSO.LoginNo, ODTSCSO.LoginNo);

            return ODTSCSO.ExecuteProc(DAOSCSO.LoadPOForAuthorization, dicPara);
        }

        internal DataSet AuthorizationSearchFillValues(DTSCSO ODTSCSO)
        {
            Dictionary<string, object> dicPara = new Dictionary<string, object>();

            dicPara.Add(DAOSCSO.LoginNo, ODTSCSO.LoginNo);
            return ODTSCSO.ExecuteProc(DAOSCSO.AuthorizationSearchFillValues, dicPara);
        }
    }
}

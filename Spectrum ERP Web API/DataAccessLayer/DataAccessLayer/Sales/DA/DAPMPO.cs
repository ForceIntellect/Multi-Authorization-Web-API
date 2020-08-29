using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    internal class DAPMPO
    {

        internal DataSet AuthorizationSearchData(DTPMPO ODTPMPO)
        {
            Dictionary<string, object> dicPara = new Dictionary<string, object>();

            dicPara.Add(DAOPMPO.LoginNo, ODTPMPO.LoginNo);
            dicPara.Add(DAOPMPO.Where, ODTPMPO.Where);

            return ODTPMPO.ExecuteProc(DAOPMPO.AuthorizationSearchData, dicPara);
        }
        internal DataSet LoadPOForAuthorization(DTPMPO ODTPMPO)
        {
            Dictionary<string, object> dicPara = new Dictionary<string, object>();

            dicPara.Add(DAOPMPO.POAmendmentNo, ODTPMPO.POAmendmentNo);
            dicPara.Add(DAOPMPO.LoginNo, ODTPMPO.LoginNo);

            return ODTPMPO.ExecuteProc(DAOPMPO.LoadPOForAuthorization, dicPara);
        }

        internal DataSet AuthorizationSearchFillValues(DTPMPO ODTPMPO)
        {
            Dictionary<string, object> dicPara = new Dictionary<string, object>();

            dicPara.Add(DAOPMPO.LoginNo, ODTPMPO.LoginNo);
            return ODTPMPO.ExecuteProc(DAOPMPO.AuthorizationSearchFillValues, dicPara);
        }

        internal DataSet GetDocument(DTPMPO ODTPMPO)
        {
            Dictionary<string, object> dicPara = new Dictionary<string, object>();

            dicPara.Add(DAOPMPO.DocNo, ODTPMPO.DocNo);
            dicPara.Add(DAOPMPO.Version, ODTPMPO.Version);
            return ODTPMPO.ExecuteProc(DAOPMPO.GetDocument, dicPara);
        }
    }
}

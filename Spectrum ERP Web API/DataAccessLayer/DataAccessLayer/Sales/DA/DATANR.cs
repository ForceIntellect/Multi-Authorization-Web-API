using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    internal class DATANR
    {
        internal DataSet SaveDocumentAuthorization(DTTANR ODTTANR)
        {
            Dictionary<string, object> dicPara = new Dictionary<string, object>();

            dicPara.Add(DAOTANR.AuthDetailNo,ODTTANR.AuthDetailNo );
            dicPara.Add(DAOTANR.StatusNo,ODTTANR.StatusNo );

            dicPara.Add(DAOTANR.Comment,ODTTANR.Comment );
            dicPara.Add(DAOTANR.StatusChangeByLoginNo,ODTTANR.StatusChangeByLoginNo );

            return ODTTANR.ExecuteProc(DAOTANR.SaveDocumentAuthorization, dicPara);
        }
    }
}

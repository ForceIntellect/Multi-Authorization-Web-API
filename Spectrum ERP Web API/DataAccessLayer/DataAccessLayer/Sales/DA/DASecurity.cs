using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer

{
    internal class DASecurity
    {

        internal DataSet LoginSelect(DTSecurity ODTSecurity)
        {
            Dictionary<string, object> dicPara = new Dictionary<string, object>();

            dicPara.Add(DAOSecurity.LoginName, ODTSecurity.LoginName);
            dicPara.Add(DAOSecurity.Pwd, ODTSecurity.Pwd);

            return ODTSecurity.ExecuteProc(DAOSecurity.LoginSelect, dicPara);
        }

        internal DataSet LoginFillValues(DTSecurity ODTSecurity)
        {
            Dictionary<string, object> dicPara = new Dictionary<string, object>();

            dicPara.Add(DAOSecurity.LoginNo, ODTSecurity.LoginNo);

            return ODTSecurity.ExecuteProc(DAOSecurity.LoginFillValues, dicPara);


        }

        internal DataSet AuthMenuSelect(DTSecurity ODTSecurity)
        {
            Dictionary<string, object> dicPara = new Dictionary<string, object>();

            dicPara.Add(DAOSecurity.LoginNo, ODTSecurity.LoginNo);

            return ODTSecurity.ExecuteProc(DAOSecurity.AuthMenuSelect, dicPara);
        }


   }
}

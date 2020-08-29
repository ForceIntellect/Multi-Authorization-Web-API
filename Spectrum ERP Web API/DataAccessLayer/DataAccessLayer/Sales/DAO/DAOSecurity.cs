using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataAccessLayer
{
    internal class DAOSecurity
    {

            //Stored Procedure
        internal static readonly string LoginSelect = "Security.LoginSelect";
        internal static readonly string LoginFillValues = "Purchase.SCSOAuthorizationLoginFillValues";
        internal static readonly string AuthMenuSelect = "Masterdata.AuthMenuSelect";

            //variables
        internal static readonly string LoginName = "@LoginName";
        internal static readonly string Pwd = "@Pwd";
        internal static readonly string LoginNo = "@LoginNo";
 
   }
}



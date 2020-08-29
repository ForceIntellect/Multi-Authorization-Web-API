using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccessLayer
{
    internal class DAOPMPO
    {
        //Stored Procedure
        internal static readonly string SaveDocumentAuthorization = "Notifications.TANRSaveDocumentAuthorization";
        internal static readonly string AuthorizationSearchData = "Purchase.PMPOAuthorizationSearchData";
        internal static readonly string LoadPOForAuthorization = "Purchase.PMPOLoadPOForAuthorization";
        internal static readonly string AuthorizationSearchFillValues = "Purchase.PMPOAuthorizationSearchFillValues";
        internal static readonly string GetDocument = "Utility.GetDocument";


        //variables
        internal static readonly string AuthDetailNo = "@AuthDetailNo";
        internal static readonly string Comment = "@Comment";
        internal static readonly string LoginNo = "@LoginNo";
        internal static readonly string POAmendmentNo = "@POAmendmentNo";
        internal static readonly string StatusChangeByLoginNo = "@StatusChangeByLoginNo";
        internal static readonly string StatusNo = "@StatusNo";
        internal static readonly string Where = "@Where";
        internal static readonly string DocNo = "@DocNo";
        internal static readonly string Version = "@Version";


    }
}

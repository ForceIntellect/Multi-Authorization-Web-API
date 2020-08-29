using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccessLayer
{
    internal class DAOSCSO
    {
        //Stored Procedure
        internal static readonly string SaveDocumentAuthorization = "Notifications.TANRSaveDocumentAuthorization";
        internal static readonly string AuthorizationSearchData = "Purchase.SCSOAuthorizationSearchData";
        internal static readonly string LoadPOForAuthorization = "Purchase.SCSOLoadPOForAuthorization";
        internal static readonly string AuthorizationSearchFillValues = "Purchase.SCSOAuthorizationSearchFillValues";


        //variables
        internal static readonly string AuthDetailNo = "@AuthDetailNo";
        internal static readonly string Comment = "@Comment";
        internal static readonly string LoginNo = "@LoginNo";
        internal static readonly string SCOrderAmendmentNo = "@SCOrderAmendmentNo";
        internal static readonly string StatusChangeByLoginNo = "@StatusChangeByLoginNo";
        internal static readonly string StatusNo = "@StatusNo";
        internal static readonly string Where = "@Where";


   }
}

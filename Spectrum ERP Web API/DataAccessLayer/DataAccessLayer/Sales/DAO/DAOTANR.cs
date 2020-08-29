using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccessLayer
{
    internal class DAOTANR
    {
    
       //Stored Procedure
       internal static readonly string SaveDocumentAuthorization = "Notifications.TANRSaveDocumentAuthorization";


       //variables
        internal static readonly string AuthDetailNo = "@AuthDetailNo";
        internal static readonly string Comment = "@Comment";
        internal static readonly string StatusChangeByLoginNo = "@StatusChangeByLoginNo";
        internal static readonly string StatusNo = "@StatusNo";
   }
}

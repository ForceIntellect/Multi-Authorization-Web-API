using System;
using System.Collections.Generic;

namespace BaseArchitecture.BusinessLogicLayer
{
    public struct ClsGlobalVariables
    {
        public DateTime WorkingDate;
        public System.Drawing.Color CompulsoryFieldColor;
        public System.Drawing.Color RowEditTemplateColor;

        public byte CompanyNo;
        public byte HeadOfficeNo;

        public short LoginNo;
        //public byte ModuleNo;

        public byte YearNo;
        public DateTime FinancialStartDate;
        public DateTime FinancialEndDate;
        
        public string DatabaseServer;
        public string DatabaseName;
        public string UserName;
        public string Password;

        public enum DocumentStatus : byte
        {
            Initial = 10,
            Release = 20,
            Authorized = 30,
        }

        public Dictionary<byte, string> CrDr;

        #region For Trace of procedures

        public bool Trace_Enable;

        #endregion

        #region For Load testing

        public bool LoadTesting_Enable;
        public bool LoadTesting_BoundryValues_Enable;
        public bool bIsAutoAuthorization;
        public Object ActiveFormInternalID;
        public string ActiveFormCode;
        #endregion

        public byte MailTypeNo;
    }
}

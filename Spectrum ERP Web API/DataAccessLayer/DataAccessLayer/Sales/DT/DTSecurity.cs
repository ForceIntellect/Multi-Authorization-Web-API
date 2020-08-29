using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataAccessLayer
{
    public class DTSecurity : BaseArchitecture.BusinessLogicLayer.ClsCommonFunction
    {

        #region private

        private string strLoginName;
        private string strPwd;
        private short sLoginNo;

        #endregion


        #region public


        public string LoginName
        {
            get { return strLoginName; }
            set { strLoginName = value; }
        }
        public string Pwd
        {
            get { return strPwd; }
            set { strPwd = value; }
        }

        public short LoginNo
        {
            get { return sLoginNo; }
            set { sLoginNo = value; }
        }

        #endregion



        public DataSet LoginSelect()
        {
            DASecurity objDASecurity = new DASecurity();
            return objDASecurity.LoginSelect(this);
        }

        public DataSet LoginFillValues()
        {
            DASecurity objDASecurity = new DASecurity();
            return objDASecurity.LoginFillValues(this);
        }

        public DataSet AuthMenuSelect()
        {
            DASecurity objDASecurity = new DASecurity();
            return objDASecurity.AuthMenuSelect(this);
        }

   


   }
}

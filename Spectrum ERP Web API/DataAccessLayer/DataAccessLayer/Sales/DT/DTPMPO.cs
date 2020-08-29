using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccessLayer
{
    public class DTPMPO : BaseArchitecture.BusinessLogicLayer.ClsCommonFunction
    {
        #region private


        private byte btStatusNo;
        private int iAuthDetailNo;
        private int iLoginNo;
        private int iPOAmendmentNo;
        private short sLoginNo;
        private short sStatusChangeByLoginNo;
        private string strComment;
        private string strWhere;
        private int iDocNo;
        private short sVersion;

        #endregion


        #region public


        public byte StatusNo
        {
            get { return btStatusNo; }
            set { btStatusNo = value; }
        }
        public int AuthDetailNo
        {
            get { return iAuthDetailNo; }
            set { iAuthDetailNo = value; }
        }
        public int LoginNo
        {
            get { return iLoginNo; }
            set { iLoginNo = value; }
        }
        public int POAmendmentNo
        {
            get { return iPOAmendmentNo; }
            set { iPOAmendmentNo = value; }
        }

        public short StatusChangeByLoginNo
        {
            get { return sStatusChangeByLoginNo; }
            set { sStatusChangeByLoginNo = value; }
        }
        public string Comment
        {
            get { return strComment; }
            set { strComment = value; }
        }
        public string Where
        {
            get { return strWhere; }
            set { strWhere = value; }
        }

        public int DocNo
        {
            get { return iDocNo; }
            set { iDocNo = value; }
        }

        public short Version
        {
            get { return sVersion; }
            set { sVersion = value; }
        }

        #endregion



        public DataSet AuthorizationSearchData()
        {
            DAPMPO objDAPMPO = new DAPMPO();
            return objDAPMPO.AuthorizationSearchData(this);
        }


        public DataSet LoadPOForAuthorization()
        {
            DAPMPO objDAPMPO = new DAPMPO();
            return objDAPMPO.LoadPOForAuthorization(this);
        }

        public DataSet AuthorizationSearchFillValues()
        {
            DAPMPO objDAPMPO = new DAPMPO();
            return objDAPMPO.AuthorizationSearchFillValues(this);
        }

        public DataSet GetDocument()
        {
            DAPMPO objDAPMPO = new DAPMPO();
            return objDAPMPO.GetDocument(this);
        }
    }
}

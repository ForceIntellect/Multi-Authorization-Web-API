using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccessLayer
{
   public class DTSCSO : BaseArchitecture.BusinessLogicLayer.ClsCommonFunction
    {
        #region private


        private byte btStatusNo;
        private int iAuthDetailNo;
        private int iLoginNo;
        private int iSCOrderAmendmentNo;
        private short sLoginNo;
        private short sStatusChangeByLoginNo;
        private string strComment;
        private string strWhere;


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
        public int SCOrderAmendmentNo
        {
            get { return iSCOrderAmendmentNo; }
            set { iSCOrderAmendmentNo = value; }
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


        #endregion



        public DataSet AuthorizationSearchData()
        {
            DASCSO objDASCSO = new DASCSO();
            return objDASCSO.AuthorizationSearchData(this);
        }


        public DataSet LoadPOForAuthorization()
        {
            DASCSO objDASCSO = new DASCSO();
            return objDASCSO.LoadPOForAuthorization(this);
        }

        public DataSet AuthorizationSearchFillValues()
        {
            DASCSO objDASCSO = new DASCSO();
            return objDASCSO.AuthorizationSearchFillValues(this);
        }


    }
}

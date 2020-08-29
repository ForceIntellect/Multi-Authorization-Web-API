using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataAccessLayer
{
   public class DTTANR : BaseArchitecture.BusinessLogicLayer.ClsCommonFunction
    {
       #region private


        private byte btStatusNo;
        private int iAuthDetailNo;
        private short sStatusChangeByLoginNo;
        private string strComment;


      #endregion


       # region public


        public byte StatusNo
        {
            get { return btStatusNo; }
            set { btStatusNo= value; }
        }
        public int AuthDetailNo
        {
            get { return iAuthDetailNo; }
            set { iAuthDetailNo= value; }
        }
        public short StatusChangeByLoginNo
        {
            get { return sStatusChangeByLoginNo; }
            set { sStatusChangeByLoginNo= value; }
        }
        public string Comment
        {
            get { return strComment; }
            set { strComment= value; }
        }


     #endregion


       public DataSet SaveDocumentAuthorization()
        {
           DATANR objDATANR = new DATANR();
           return objDATANR.SaveDocumentAuthorization(this);
        }


   }
}

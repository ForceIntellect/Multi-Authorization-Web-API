using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DTDashboard : BaseArchitecture.BusinessLogicLayer.ClsCommonFunction
    {
        #region private
        private short sLoginNo;
        private short sCardNo;
        private string strCompanyNos;
        private DateTime dteFromDate;
        private DateTime dteToDate;
        private short sDashboardNo;
        #endregion


        #region public


        public short LoginNo
        {
            get { return sLoginNo; }
            set { sLoginNo = value; }
        }
        public string CompanyNos
        {
            get { return strCompanyNos; }
            set { strCompanyNos = value; }
        }

        public short DashboardNo
        {
            get { return sDashboardNo; }
            set { sDashboardNo = value; }
        }

        public short CardNo
        {
            get { return sCardNo; }
            set { sCardNo = value; }
        }

        public DateTime FromDate
        {
            get { return dteFromDate; }
            set { dteFromDate = value; }
        }

        public DateTime ToDate
        {
            get { return dteToDate; }
            set { dteToDate = value; }
        }
        #endregion

        public DataSet DrawDashboard()
        {
            DADashboard objDADashboard = new DADashboard();
            return objDADashboard.DrawDashboard(this);
        }

        public DataSet GetDataForTopNItem()
        {
            DADashboard objDADashboard = new DADashboard();
            return objDADashboard.GetDataForTopNItem(this);
        }

    }
}
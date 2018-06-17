using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// AirCosting 的摘要说明
/// </summary>
namespace C2
{
    public class AirCosting : BaseObject
    {
        private int id;
        private string refNo;
        private string jobNo;
        private string jobType;
        private string vendorId;
        private string chgCode;
        private string chgCodeDes;
        private string remark;
        private decimal saleQty;
        private decimal salePrice;
        private string unit;
        private string saleCurrency;
        private decimal saleExRate;
        private decimal saleDocAmt;
        private decimal saleLocAmt;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private decimal costPrice;
        private decimal costDocAmt;
        private decimal costLocAmt;
        private string splitType;
        private string costCurrency;
        private decimal costExRate;
        private decimal costQty;
        private decimal saleGst;
        private decimal costGst;
        private string payInd;
        private string verifryInd;
        private string docNo;
        private string salesman;

        public int Id
        {
            get { return this.id; }
        }

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }

        public string VendorId
        {
            get { return this.vendorId; }
            set { this.vendorId = value; }
        }

        public string ChgCode
        {
            get { return this.chgCode; }
            set { this.chgCode = value; }
        }

        public string ChgCodeDes
        {
            get { return this.chgCodeDes; }
            set { this.chgCodeDes = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public decimal SaleQty
        {
            get { return this.saleQty; }
            set { this.saleQty = value; }
        }

        public decimal SalePrice
        {
            get { return this.salePrice; }
            set { this.salePrice = value; }
        }

        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

        public string SaleCurrency
        {
            get { return this.saleCurrency; }
            set { this.saleCurrency = value; }
        }

        public decimal SaleExRate
        {
            get { return this.saleExRate; }
            set { this.saleExRate = value; }
        }

        public decimal SaleDocAmt
        {
            get { return this.saleDocAmt; }
            set { this.saleDocAmt = value; }
        }

        public decimal SaleLocAmt
        {
            get { return this.saleLocAmt; }
            set { this.saleLocAmt = value; }
        }

        public string CreateBy
        {
            get { return this.createBy; }
            set { this.createBy = value; }
        }

        public DateTime CreateDateTime
        {
            get { return this.createDateTime; }
            set { this.createDateTime = value; }
        }

        public string UpdateBy
        {
            get { return this.updateBy; }
            set { this.updateBy = value; }
        }

        public DateTime UpdateDateTime
        {
            get { return this.updateDateTime; }
            set { this.updateDateTime = value; }
        }

        public decimal CostPrice
        {
            get { return this.costPrice; }
            set { this.costPrice = value; }
        }

        public decimal CostDocAmt
        {
            get { return this.costDocAmt; }
            set { this.costDocAmt = value; }
        }

        public decimal CostLocAmt
        {
            get { return this.costLocAmt; }
            set { this.costLocAmt = value; }
        }

        public string SplitType
        {
            get { return this.splitType; }
            set { this.splitType = value; }
        }

        public string CostCurrency
        {
            get { return this.costCurrency; }
            set { this.costCurrency = value; }
        }

        public decimal CostExRate
        {
            get { return this.costExRate; }
            set { this.costExRate = value; }
        }

        public decimal CostQty
        {
            get { return this.costQty; }
            set { this.costQty = value; }
        }

        public decimal SaleGst
        {
            get { return this.saleGst; }
            set { this.saleGst = value; }
        }

        public decimal CostGst
        {
            get { return this.costGst; }
            set { this.costGst = value; }
        }

        public string PayInd
        {
            get { return this.payInd; }
            set { this.payInd = value; }
        }

        public string VerifryInd
        {
            get { return this.verifryInd; }
            set { this.verifryInd = value; }
        }

        public string DocNo
        {
            get { return this.docNo; }
            set { this.docNo = value; }
        }

        public string Salesman
        {
            get { return this.salesman; }
            set { this.salesman = value; }
        }
        public string StatusCode
        {
            get
            {
                string s = "N";
                string sql = "select StatusCode from air_job where JobNo='" + this.jobNo + "'";

                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
                return s;
            }
        }
        public string RefStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from air_ref where RefNo='" + this.refNo + "'";

                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
	}
}
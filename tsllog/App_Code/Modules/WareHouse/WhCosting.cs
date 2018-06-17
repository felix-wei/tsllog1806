using System;

namespace C2
{
    public class WhCosting
    {

        private int id;
        private string refNo;
        private string jobType;
        private string chgCode;
        private string chgCodeDes;
        private string remark;
        private decimal costQty;
        private decimal costPrice;
        private string unit;
        private string costCurrency;
        private decimal costExRate;
        private decimal costDocAmt;
        private decimal costLocAmt;
        private decimal costGst;
        public int Id
        {
            get { return this.id; }
        }
        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }
        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }
        public string StatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from wh_do where Dono='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        public string DoStatus
        {
            get
            {
                string s = "USE";
                string sql = "select DoStatus from Wh_Trans where DoNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "Draft");
                return s;
            }
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
        public decimal CostQty
        {
            get { return this.costQty; }
            set { this.costQty = value; }
        }

        public decimal CostPrice
        {
            get { return this.costPrice; }
            set { this.costPrice = value; }
        }

        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
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


        public decimal CostGst
        {
            get { return this.costGst; }
            set { this.costGst = value; }
        }
        public string IsInv
        {
            get
            {
                string s = "none";
                string sql = string.Format(@"select top 1 docno from XAArInvoiceDet where MastRefNo='{0}' and ChgCode='{1}'", refNo, chgCode);
               string  doNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
               if (doNo.Length > 0)
               {
                   s = "block";
                }
                return s;
            }
        }
        private string costGstType;
        public string CostGstType
        {
            get { return this.costGstType; }
            set { this.costGstType = value; }
        }
    }
}

using System;

namespace C2
{
    public class CostDet
    {

        private int sequenceId;
        private int parentId;

        private string _RowCreateUser;
        private DateTime _RowCreateTime;
        private string _RowUpdateUser;
        private DateTime _RowUpdateTime;
        private DateTime _CostDate;

        private string jobType;
        private string jobNo;
        private string vendorId;
        private string chgCode;
        private string chgCodeDes;
        private string remark;
        private decimal saleQty;
        private decimal costQty;
        private decimal salePrice;
        private decimal costPrice;
        private string unit;
        private string saleCurrency;
        private decimal saleExRate;
        private decimal saleDocAmt;
        private decimal saleLocAmt;
        private string costCurrency;
        private decimal costExRate;
        private decimal costDocAmt;
        private decimal costLocAmt;
        private string payInd;
        private string verifryInd;
        private string docNo;
        private string salesman;
        public int SequenceId
        {
            get { return this.sequenceId; }
        }
        public int ParentId
        {
            get { return this.parentId; }
            set { this.parentId = value; }
        }
        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }
        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
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

        public string VendorId
        {
            get { return this.vendorId; }
            set { this.vendorId = value; }
        }



        public decimal SaleQty
        {
            get { return this.saleQty; }
            set { this.saleQty = value; }
        }
        public decimal CostQty
        {
            get { return this.costQty; }
            set { this.costQty = value; }
        }

        public decimal SalePrice
        {
            get { return this.salePrice; }
            set { this.salePrice = value; }
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
        private string splitType;
        public string SplitType
        {
            get { return this.splitType; }
            set { this.splitType = value; }
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

        public string RowCreateUser
        {
            get { return this._RowCreateUser; }
            set { this._RowCreateUser = value; }
        }

        public DateTime RowCreateTime
        {
            get { return this._RowCreateTime; }
            set { this._RowCreateTime = value; }
        }

        public string RowUpdateUser
        {
            get { return this._RowUpdateUser; }
            set { this._RowUpdateUser = value; }
        }

        public DateTime RowUpdateTime
        {
            get { return this._RowUpdateTime; }
            set { this._RowUpdateTime = value; }
        }

        public DateTime CostDate
        {
            get { return this._CostDate; }
            set { this._CostDate = value; }
        }


        private string status1;
        private string status2;
        private string status3;
        private string status4;




        public string Status1
        {
            get { return SafeValue.SafeString(this.status1); }
            set { this.status1 = SafeValue.SafeString(value); }
        }

        public string Status2
        {
            get { return SafeValue.SafeString(this.status2); }
            set { this.status2 = SafeValue.SafeString(value); }
        }

        public string Status3
        {
            get { return SafeValue.SafeString(this.status3); }
            set { this.status3 = SafeValue.SafeString(value); }
        }

        public string Status4
        {
            get { return SafeValue.SafeString(this.status4); }
            set { this.status4 = SafeValue.SafeString(value); }
        }



        private DateTime date1;
        private DateTime date2;
        private DateTime date3;
        private DateTime date4;





        public DateTime Date1
        {
            get { return SafeValue.SafeDate(this.date1,new DateTime(1753,01,01)); }
            set { this.date1 = SafeValue.SafeDate(value, new DateTime(1753, 01, 01)); }
        }

        public DateTime Date2
        {
            get { return SafeValue.SafeDate(this.date2, new DateTime(1753, 01, 01)); }
            set { this.date2 = SafeValue.SafeDate(value, new DateTime(1753, 01, 01)); }
        }

        public DateTime Date3
        {
            get { return SafeValue.SafeDate(this.date3, new DateTime(1753, 01, 01)); }
            set { this.date3 = SafeValue.SafeDate(value, new DateTime(1753, 01, 01)); }
        }

        public DateTime Date4
        {
            get { return SafeValue.SafeDate(this.date4, new DateTime(1753, 01, 01)); }
            set { this.date4 = SafeValue.SafeDate(value, new DateTime(1753, 01, 01)); }
        }


    }
}

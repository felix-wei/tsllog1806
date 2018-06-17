using System;

namespace C2
{
    public class RefLc
    {
        private int id;
        private string lcNo;
        private string lcTempNo;
        private string lcType;
        private string lcRef;
        private DateTime lcAppDate;
        private DateTime lcExpirtDate;
        private string lcExpirtPlace;
        private string lcEntityCode;
        private string lcEntityName;
        private string lcEntityAddress;
        private string lcBeneCode;
        private string lcBeneName;
        private string lcBeneAddress;
        private string bankCode;
        private string bankName;
        private string bankBranch;
        private string bankAddress;
        private string bankRef;
        private int bankTenor;
        private decimal lcAmount;
        private string lcCurrency;
        private decimal lcExRate;
        private string lcMode;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string statusCode;

        public int Id
        {
            get { return this.id; }
        }

        public string LcNo
        {
            get { return this.lcNo; }
            set { this.lcNo = value; }
        }

        public string LcTempNo
        {
            get { return this.lcTempNo; }
            set { this.lcTempNo = value; }
        }

        public string LcType
        {
            get { return this.lcType; }
            set { this.lcType = value; }
        }

        public string LcRef
        {
            get { return this.lcRef; }
            set { this.lcRef = value; }
        }

        public DateTime LcAppDate
        {
            get { return this.lcAppDate; }
            set { this.lcAppDate = value; }
        }

        public DateTime LcExpirtDate
        {
            get { return this.lcExpirtDate; }
            set { this.lcExpirtDate = value; }
        }

        public string LcExpirtPlace
        {
            get { return this.lcExpirtPlace; }
            set { this.lcExpirtPlace = value; }
        }

        public string LcEntityCode
        {
            get { return this.lcEntityCode; }
            set { this.lcEntityCode = value; }
        }

        public string LcEntityName
        {
            get { return this.lcEntityName; }
            set { this.lcEntityName = value; }
        }

        public string LcEntityAddress
        {
            get { return this.lcEntityAddress; }
            set { this.lcEntityAddress = value; }
        }

        public string LcBeneCode
        {
            get { return this.lcBeneCode; }
            set { this.lcBeneCode = value; }
        }

        public string LcBeneName
        {
            get { return this.lcBeneName; }
            set { this.lcBeneName = value; }
        }

        public string LcBeneAddress
        {
            get { return this.lcBeneAddress; }
            set { this.lcBeneAddress = value; }
        }

        public string BankCode
        {
            get { return this.bankCode; }
            set { this.bankCode = value; }
        }

        public string BankName
        {
            get { return this.bankName; }
            set { this.bankName = value; }
        }

        public string BankBranch
        {
            get { return this.bankBranch; }
            set { this.bankBranch = value; }
        }

        public string BankAddress
        {
            get { return this.bankAddress; }
            set { this.bankAddress = value; }
        }

        public string BankRef
        {
            get { return this.bankRef; }
            set { this.bankRef = value; }
        }

        public int BankTenor
        {
            get { return this.bankTenor; }
            set { this.bankTenor = value; }
        }

        public decimal LcAmount
        {
            get { return this.lcAmount; }
            set { this.lcAmount = value; }
        }

        public string LcCurrency
        {
            get { return this.lcCurrency; }
            set { this.lcCurrency = value; }
        }

        public decimal LcExRate
        {
            get { return this.lcExRate; }
            set { this.lcExRate = value; }
        }

        public string LcMode
        {
            get { return this.lcMode; }
            set { this.lcMode = value; }
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
        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }
    }
}

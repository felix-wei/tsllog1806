using System;

namespace C2
{
    public class LcBankFund
    {
        private int id;
        private string bankCode;
        private string bankName;
        private string companyName;
        private decimal loanFund;
        private DateTime lendingDate;
        private DateTime expiryDate;
        private string remark;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string statusCode;
        public int Id
        {
            get { return this.id; }
        }

        public string BankCode
        {
            get { return this.bankCode; }
            set { this.bankCode = value; }
        }
        public string CompanyName
        {
            get { return this.companyName; }
            set { this.companyName = value; }
        }

        public string BankName
        {
            get { return this.bankName; }
            set { this.bankName = value; }
        }

        public decimal LoanFund
        {
            get { return this.loanFund; }
            set { this.loanFund = value; }
        }

        public DateTime LendingDate
        {
            get { return this.lendingDate; }
            set { this.lendingDate = value; }
        }

        public DateTime ExpiryDate
        {
            get { return this.expiryDate; }
            set { this.expiryDate = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
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

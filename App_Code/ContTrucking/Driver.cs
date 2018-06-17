using System;

namespace C2
{
    public class CtmDriver
    {
        private int id;
        private string code;
        private string name;
        private string tel;
        private string iCNo;
        private string attendant;
        private string remark;
        private string isstaff;
        private string towheaderCode;
        private string serviceLevel;
        private string statusCode;
        private string teamNo;
        private string licenseNo;
        private DateTime licenseExpiry;
        private string bankAccount;
        private decimal salaryBasic;
        private decimal salaryAllowance;
        private string salaryRemark;

        public int Id
        {
            get { return this.id; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Tel
        {
            get { return this.tel; }
            set { this.tel = value; }
        }

        public string ICNo
        {
            get { return this.iCNo; }
            set { this.iCNo = value; }
        }

        public string Attendant
        {
            get { return this.attendant; }
            set { this.attendant = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public string Isstaff
        {
            get { return this.isstaff; }
            set { this.isstaff = value; }
        }

        public string TowheaderCode
        {
            get { return this.towheaderCode; }
            set { this.towheaderCode = value; }
        }

        public string ServiceLevel
        {
            get { return this.serviceLevel; }
            set { this.serviceLevel = value; }
        }

        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }

        public string TeamNo
        {
            get { return this.teamNo; }
            set { this.teamNo = value; }
        }

        public string LicenseNo
        {
            get { return this.licenseNo; }
            set { this.licenseNo = value; }
        }

        public DateTime LicenseExpiry
        {
            get { return this.licenseExpiry; }
            set { this.licenseExpiry = value; }
        }

        public string BankAccount
        {
            get { return this.bankAccount; }
            set { this.bankAccount = value; }
        }

        public decimal SalaryBasic
        {
            get { return this.salaryBasic; }
            set { this.salaryBasic = value; }
        }

        public decimal SalaryAllowance
        {
            get { return this.salaryAllowance; }
            set { this.salaryAllowance = value; }
        }

        public string SalaryRemark
        {
            get { return this.salaryRemark; }
            set { this.salaryRemark = value; }
        }
        private string subContract_Ind;
        public string SubContract_Ind
        {
            get { return this.subContract_Ind; }
            set { this.subContract_Ind = value; }
        }
    }
}

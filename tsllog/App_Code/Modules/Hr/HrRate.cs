using System;

namespace C2
{
    public class HrRate
    {
        private int id;
        private string payItem;
        private decimal rate;
        private decimal employeeRate;
        private decimal employerRate;
        private decimal employeeRate55;
        private decimal employerRate55;
        private decimal employeeRate60;
        private decimal employerRate60;
        private decimal employeeRate65;
        private decimal employerRate65;
        private string remark;
        private DateTime fromDate;
        private DateTime toDate;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

        public int Id
        {
            get { return this.id; }
        }

        public string PayItem
        {
            get { return this.payItem; }
            set { this.payItem = value; }
        }
        public decimal Rate
        {
            get { return this.rate; }
            set { this.rate = value; }
        }

        public decimal EmployeeRate
        {
            get { return this.employeeRate; }
            set { this.employeeRate = value; }
        }

        public decimal EmployerRate
        {
            get { return this.employerRate; }
            set { this.employerRate = value; }
        }

        public decimal EmployeeRate55
        {
            get { return this.employeeRate55; }
            set { this.employeeRate55 = value; }
        }

        public decimal EmployerRate55
        {
            get { return this.employerRate55; }
            set { this.employerRate55 = value; }
        }

        public decimal EmployeeRate60
        {
            get { return this.employeeRate60; }
            set { this.employeeRate60 = value; }
        }

        public decimal EmployerRate60
        {
            get { return this.employerRate60; }
            set { this.employerRate60 = value; }
        }

        public decimal EmployeeRate65
        {
            get { return this.employeeRate65; }
            set { this.employeeRate65 = value; }
        }

        public decimal EmployerRate65
        {
            get { return this.employerRate65; }
            set { this.employerRate65 = value; }
        }
		
		
		
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public DateTime FromDate
        {
            get { return this.fromDate; }
            set { this.fromDate = value; }
        }

        public DateTime ToDate
        {
            get { return this.toDate; }
            set { this.toDate = value; }
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
        private int age;
        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }
        private int age1;
        public int Age1
        {
            get { return this.age1; }
            set { this.age1 = value; }
        }
        private string rateType;
        public string RateType
        {
            get { return this.rateType; }
            set { this.rateType = value; }
        }
    }
}

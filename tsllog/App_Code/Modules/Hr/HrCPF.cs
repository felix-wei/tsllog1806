using System;

namespace C2
{
    public class HrCpf
    {
        private int id;
        private int person;
        private DateTime fromDate;
        private DateTime toDate;
        private decimal cpf1;
        private decimal cpf2;

        public int Id
        {
            get { return this.id; }
        }

        public int Person
        {
            get { return this.person; }
            set { this.person = value; }
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

        public decimal Cpf1
        {
            get { return this.cpf1; }
            set { this.cpf1 = value; }
        }

        public decimal Cpf2
        {
            get { return this.cpf2; }
            set { this.cpf2 = value; }
        }

        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string statusCode;

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
    }
}

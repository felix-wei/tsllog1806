using System;

namespace C2
{
	public class HrPersonDet1
	{
		private int id;
		private int person;
		private DateTime beginDate;
        private DateTime resignDate;
		private decimal salary;
		private string remark;

		public int Id
		{
			get { return this.id; }
		}

		public int Person
		{
			get { return this.person; }
			set { this.person = value; }
		}

        public DateTime BeginDate
		{
			get { return this.beginDate; }
			set { this.beginDate = value; }
		}

        public DateTime ResignDate
		{
			get { return this.resignDate; }
			set { this.resignDate = value; }
		}

		public decimal Salary
		{
			get { return this.salary; }
			set { this.salary = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
        }

        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

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

        private decimal startSalary;
        public decimal StartSalary
        {
            get { return this.startSalary; }
            set { this.startSalary = value; }
        }

	}
}

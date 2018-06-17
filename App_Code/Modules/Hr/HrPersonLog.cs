using System;

namespace C2
{
	public class HrPersonLog
	{
		private int id;
		private int person;
		private DateTime logDate;
		private string logTime;
		private string status;
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

		public DateTime LogDate
		{
			get { return this.logDate; }
			set { this.logDate = value; }
		}

		public string LogTime
		{
			get { return this.logTime; }
			set { this.logTime = value; }
		}

		public string Status
		{
			get { return this.status; }
			set { this.status = value; }
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
	}
}

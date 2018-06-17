using System;
using System.Web;

namespace C2
{
	public class HrTask
	{
		private int id;
		private int person;
        private DateTime date;
		private string time;
		private string refNo;
		private string remark;
		private string status;

		public int Id
		{
			get { return this.id; }
			set { this.id = value; }
		}

		public int Person
		{
			get { return this.person; }
			set { this.person = value; }
		}

        public DateTime Date
		{
			get { return this.date; }
			set { this.date = value; }
		}

		public string Time
		{
			get { return this.time; }
			set { this.time = value; }
		}

		public string RefNo
		{
			get { return this.refNo; }
			set { this.refNo = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
		}

		public string Status
		{
			get { return this.status; }
			set { this.status = value; }
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

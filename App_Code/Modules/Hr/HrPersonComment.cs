using System;

namespace C2
{
	public class HrPersonComment
	{
		private int id;
		private int person;
        private DateTime date;
		private string status;
		private string manager;
		private string rating;
		private string remark;
		private string remark1;
		private string remark2;

		public int Id
		{
			get { return this.id; }
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

		public string Status
		{
			get { return this.status; }
			set { this.status = value; }
		}

		public string Manager
		{
			get { return this.manager; }
			set { this.manager = value; }
		}

		public string Rating
		{
			get { return this.rating; }
			set { this.rating = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
		}

		public string Remark1
		{
			get { return this.remark1; }
			set { this.remark1 = value; }
		}

		public string Remark2
		{
			get { return this.remark2; }
			set { this.remark2 = value; }
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

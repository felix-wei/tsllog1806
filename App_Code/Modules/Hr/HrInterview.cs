using System;

namespace C2
{
	public class HrInterview
	{
		private int id;
        private DateTime date;
		private int pic;
		private string department;
		private string remark;
        private string statusCode;
		private string remark1;

		public int Id
		{
			get { return this.id; }
		}

        public DateTime Date
		{
			get { return this.date; }
			set { this.date = value; }
		}

		public int Pic
		{
			get { return this.pic; }
			set { this.pic = value; }
		}

		public string Department
		{
			get { return this.department; }
			set { this.department = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
		}

        public string StatusCode
		{
            get { return this.statusCode; }
            set { this.statusCode = value; }
		}

		public string Remark1
		{
			get { return this.remark1; }
			set { this.remark1 = value; }
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

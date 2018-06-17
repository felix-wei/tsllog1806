using System;

namespace C2
{
	public class HrRecruitment
	{
		private int id;
		private string no;
        private DateTime date;
		private string department;
        private string statusCode;
		private int pic;
		private string remark1;
		private string remark2;

		public int Id
		{
			get { return this.id; }
		}

        public string No
		{
			get { return this.no; }
			set { this.no = value; }
		}

        public DateTime Date
		{
			get { return this.date; }
			set { this.date = value; }
		}

		public string Department
		{
			get { return this.department; }
			set { this.department = value; }
		}

        public string StatusCode
		{
            get { return this.statusCode; }
            set { this.statusCode = value; }
		}

		public int Pic
		{
			get { return this.pic; }
			set { this.pic = value; }
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

using System;

namespace C2
{
	public class HrContract
	{
		private int id;
		private string no;
		private DateTime date;
		private int person;
		private int pic;
		private string remark;
		private string remark1;
		private string remark2;
        private string remark3;

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

		public int Person
		{
			get { return this.person; }
			set { this.person = value; }
		}

		public int Pic
		{
			get { return this.pic; }
			set { this.pic = value; }
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

		public string Remark3
		{
			get { return this.remark3; }
			set { this.remark3 = value; }
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
        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }
	}
}

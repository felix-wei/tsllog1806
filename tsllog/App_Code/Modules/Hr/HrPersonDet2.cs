using System;

namespace C2
{
	public class HrPersonDet2
	{
		private int id;
		private int person;
		private string contactName;
		private string gender;
        private DateTime dob;
		private string phone;
		private string mobile;
		private string email;
		private string relationship;
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

		public string ContactName
		{
			get { return this.contactName; }
			set { this.contactName = value; }
		}

		public string Gender
		{
			get { return this.gender; }
			set { this.gender = value; }
		}

		public DateTime Dob
		{
			get { return this.dob; }
			set { this.dob = value; }
		}

		public string Phone
		{
			get { return this.phone; }
			set { this.phone = value; }
		}

		public string Mobile
		{
			get { return this.mobile; }
			set { this.mobile = value; }
		}

		public string Email
		{
			get { return this.email; }
			set { this.email = value; }
		}

		public string Relationship
		{
			get { return this.relationship; }
			set { this.relationship = value; }
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

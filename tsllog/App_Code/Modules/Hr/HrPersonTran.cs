using System;
using System.Web;

namespace C2
{
	public class HrPersonTran
	{
		private int id;
		private string type;
		private int person;
        private DateTime date1;
		private string time1;
        private DateTime date2;
		private string time2;
		private string hrs;
		private decimal amt;
		private int pic;
		private string remark;

		public int Id
		{
			get { return this.id; }
		}

		public string Type
		{
			get { return this.type; }
			set { this.type = value; }
		}

        public int Person
		{
			get { return this.person; }
			set { this.person = value; }
		}

        public DateTime Date1
		{
			get { return this.date1; }
			set { this.date1 = value; }
		}

		public string Time1
		{
			get { return this.time1; }
			set { this.time1 = value; }
		}

        public DateTime Date2
		{
			get { return this.date2; }
			set { this.date2 = value; }
		}

		public string Time2
		{
			get { return this.time2; }
			set { this.time2 = value; }
		}

		public string Hrs
		{
            get { return this.hrs; }
            set { this.hrs = value; }
		}

		public decimal Amt
		{
			get { return this.amt; }
			set { this.amt = value; }
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

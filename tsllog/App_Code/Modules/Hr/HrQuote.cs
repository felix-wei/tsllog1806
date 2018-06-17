using System;
using System.Web;

namespace C2
{
	public class HrQuote
	{
		private int person;
		private string payItem;
		private decimal amt;
		private string remark;
		private int id;

        public int Person
		{
			get { return this.person; }
			set { this.person = value; }
		}

		public string PayItem
		{
			get { return this.payItem; }
			set { this.payItem = value; }
		}

		public decimal Amt
		{
			get { return this.amt; }
			set { this.amt = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
		}

		public int Id
		{
			get { return this.id; }
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

        private string isCal;
        public string IsCal
        {
            get { return this.isCal; }
            set { this.isCal = value; }
        }
	}
}

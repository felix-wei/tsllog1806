using System;

namespace C2
{
	public class HrPersonNews
	{
		private int id;
		private string title;
		private string note;
		private string createBy;
		private DateTime createDateTime;
		private string updateBy;
		private DateTime updateDateTime;
        private DateTime expireDateTime;

		public int Id
		{
			get { return this.id; }
		}

		public string Title
		{
			get { return this.title; }
			set { this.title = value; }
		}

		public string Note
		{
			get { return this.note; }
			set { this.note = value; }
		}

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

        public DateTime ExpireDateTime
		{
            get { return this.expireDateTime; }
            set { this.expireDateTime = value; }
		}
	}
}

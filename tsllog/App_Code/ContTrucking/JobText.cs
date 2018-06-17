using System;

namespace C2
{
	public class JobText
	{
		private int id;
		private string content;
		private string rowStatus;
		private string rowCreateUser;
		private DateTime rowCreateTime;
		private string rowUpdateUser;
		private DateTime rowUpdateTime;

		public int Id
		{
			get { return this.id; }
		}

		public string Content
		{
			get { return this.content; }
			set { this.content = value; }
		}

		public string RowStatus
		{
			get { return this.rowStatus; }
			set { this.rowStatus = value; }
		}

		public string RowCreateUser
		{
			get { return this.rowCreateUser; }
			set { this.rowCreateUser = value; }
		}

		public DateTime RowCreateTime
		{
			get { return this.rowCreateTime; }
			set { this.rowCreateTime = value; }
		}

		public string RowUpdateUser
		{
			get { return this.rowUpdateUser; }
			set { this.rowUpdateUser = value; }
		}

		public DateTime RowUpdateTime
		{
			get { return this.rowUpdateTime; }
			set { this.rowUpdateTime = value; }
		}
	}
}

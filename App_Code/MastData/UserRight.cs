using System;

namespace C2
{
	public class UserRight
	{
		private int id;
		private int userid;
		private string moduleCode;
		private string pageCode;
		private string actionCode;
		private string accessLevel;
		private string statusCode;
		private string remark;

		public int Id
		{
			get { return this.id; }
		}

		public int Userid
		{
			get { return this.userid; }
			set { this.userid = value; }
		}

		public string ModuleCode
		{
			get { return this.moduleCode; }
			set { this.moduleCode = value; }
		}

		public string PageCode
		{
			get { return this.pageCode; }
			set { this.pageCode = value; }
		}

		public string ActionCode
		{
			get { return this.actionCode; }
			set { this.actionCode = value; }
		}

		public string AccessLevel
		{
			get { return this.accessLevel; }
			set { this.accessLevel = value; }
		}

		public string StatusCode
		{
			get { return this.statusCode; }
			set { this.statusCode = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
		}
	}
}

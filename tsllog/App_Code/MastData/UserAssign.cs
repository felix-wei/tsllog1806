using System;

namespace C2
{
	public class UserAssign
	{
		private int id;
		private int userid;
        private int userAssignId;
		private DateTime timeStart;
		private DateTime timeEnd;
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

        public int UserAssignId
		{
            get { return this.userAssignId; }
            set { this.userAssignId = value; }
		}

		public DateTime TimeStart
		{
			get { return this.timeStart; }
			set { this.timeStart = value; }
		}

		public DateTime TimeEnd
		{
			get { return this.timeEnd; }
			set { this.timeEnd = value; }
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

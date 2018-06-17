using System;

namespace C2
{
	public class SysAlert
	{
		private int id;
		private string refNo;
		private string jobNo;
		private string jobType;
		private string docNo;
		private string docType;
		private string sendType;
		private string sendMethod;
		private string sendFrom;
		private string sendTo;
		private string subject;
		private string message;
		private string statusCode;
		private string createUser;
		private DateTime createTime;
        private string alertType;

		public int Id
		{
			get { return this.id; }
		}

		public string RefNo
		{
			get { return this.refNo; }
			set { this.refNo = value; }
		}

		public string JobNo
		{
			get { return this.jobNo; }
			set { this.jobNo = value; }
		}

		public string JobType
		{
			get { return this.jobType; }
			set { this.jobType = value; }
		}

		public string DocNo
		{
			get { return this.docNo; }
			set { this.docNo = value; }
		}

		public string DocType
		{
			get { return this.docType; }
			set { this.docType = value; }
		}

		public string SendType
		{
			get { return this.sendType; }
			set { this.sendType = value; }
		}

		public string SendMethod
		{
			get { return this.sendMethod; }
			set { this.sendMethod = value; }
		}

		public string SendFrom
		{
			get { return this.sendFrom; }
			set { this.sendFrom = value; }
		}

		public string SendTo
		{
			get { return this.sendTo; }
			set { this.sendTo = value; }
		}

		public string Subject
		{
			get { return this.subject; }
			set { this.subject = value; }
		}

		public string Message
		{
			get { return this.message; }
			set { this.message = value; }
		}

		public string StatusCode
		{
			get { return this.statusCode; }
			set { this.statusCode = value; }
		}

		public string CreateUser
		{
			get { return this.createUser; }
			set { this.createUser = value; }
		}

		public DateTime CreateTime
		{
			get { return this.createTime; }
			set { this.createTime = value; }
		}

        public string AlertType
        {
            get { return this.alertType; }
            set { this.alertType = value; }
        }
	}
}

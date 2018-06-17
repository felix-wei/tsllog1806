using System;

namespace C2
{
	public class SysAlertRule
	{
		private int id;
		private string alertSql;
		private string alertTo;
		private string alertCc;
		private string alertBcc;
		private string alertMobile;
		private string alertBody;
		private string alertSubject;
		private string alertSms;
        private string masterId;
        private string alertColumns;
        private string code;
        private string subjectPosition;
        private string bodyPosition;

		public int Id
		{
			get { return this.id; }
		}

		public string AlertSql
		{
			get { return this.alertSql; }
			set { this.alertSql = value; }
		}

		public string AlertTo
		{
			get { return this.alertTo; }
			set { this.alertTo = value; }
		}

		public string AlertCc
		{
			get { return this.alertCc; }
			set { this.alertCc = value; }
		}

		public string AlertBcc
		{
			get { return this.alertBcc; }
			set { this.alertBcc = value; }
		}

		public string AlertMobile
		{
			get { return this.alertMobile; }
			set { this.alertMobile = value; }
		}

		public string AlertBody
		{
			get { return this.alertBody; }
			set { this.alertBody = value; }
		}

		public string AlertSubject
		{
			get { return this.alertSubject; }
			set { this.alertSubject = value; }
		}

		public string AlertSms
		{
			get { return this.alertSms; }
			set { this.alertSms = value; }
		}

        public string Code 
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string MasterId
        {
            get { return this.masterId; }
            set { this.masterId = value; }
        }

        public string AlertColumns {
            get { return this.alertColumns; }
            set { this.alertColumns = value; }
        }

        public string SubjectPosition
        {
            get { return this.subjectPosition; }
            set { this.subjectPosition = value; }
        }

        public string BodyPosition
        {
            get { return this.bodyPosition; }
            set { this.bodyPosition = value; }
        }
	}
}

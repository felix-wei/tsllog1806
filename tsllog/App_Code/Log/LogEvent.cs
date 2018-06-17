using System;

namespace C2
{
	public class LogEvent
	{
		private int id;
        private string refNo;
        private string jobNo;
        private string refType;
        private string action;
        private string createBy;
        private DateTime createDateTime;

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

        public string RefType
        {
            get { return this.refType; }
            set { this.refType = value; }
        }
        public string Action
        {
            get { return this.action; }
            set { this.action = value; }
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
        private string remark;
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
	}
}

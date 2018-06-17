using System;

namespace C2
{
	public class RefProcess
	{
		private int id;
        private string processModule;
        private string processType;
		private string processRef;
		private string processStatus;
		private string processBy;
		private DateTime processDateTime;
		private string processRemark;
		private string approveStatus;
		private string approveBy;
		private DateTime approveDateTime;
		private string approveRemark;

		public int Id
		{
			get { return this.id; }
		}

        public string ProcessModule
		{
            get { return this.processModule; }
            set { this.processModule = value; }
		}

		public string ProcessType
		{
			get { return this.processType; }
			set { this.processType = value; }
		}

		public string ProcessRef
		{
			get { return this.processRef; }
			set { this.processRef = value; }
		}

		public string ProcessStatus
		{
			get { return this.processStatus; }
			set { this.processStatus = value; }
		}

		public string ProcessBy
		{
			get { return this.processBy; }
			set { this.processBy = value; }
		}

		public DateTime ProcessDateTime
		{
			get { return this.processDateTime; }
			set { this.processDateTime = value; }
		}

		public string ProcessRemark
		{
			get { return this.processRemark; }
			set { this.processRemark = value; }
		}

		public string ApproveStatus
		{
			get { return this.approveStatus; }
			set { this.approveStatus = value; }
		}

		public string ApproveBy
		{
			get { return this.approveBy; }
			set { this.approveBy = value; }
		}

		public DateTime ApproveDateTime
		{
			get { return this.approveDateTime; }
			set { this.approveDateTime = value; }
		}

		public string ApproveRemark
		{
			get { return this.approveRemark; }
			set { this.approveRemark = value; }
        }

        private string createBy;
        private DateTime createDateTime;

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
	}
}

using System;

namespace C2
{
    public class JobMessage
    {
        private int id;
        private string refNo;
        private string mTitle;
        private string mType;
        private string mBody;
        private string createdBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

        public int Id
        {
            get { return this.id; }
        }

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }

        public string MTitle
        {
            get { return this.mTitle; }
            set { this.mTitle = value; }
        }

        public string MType
        {
            get { return this.mType; }
            set { this.mType = value; }
        }

        public string MBody
        {
            get { return this.mBody; }
            set { this.mBody = value; }
        }

        public string CreatedBy
        {
            get { return this.createdBy; }
            set { this.createdBy = value; }
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
		public string JobStatus
        {
            get
            {
                string s = "USE";
                string sql = "select JobStatus from JobInfo where JobNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
		private string jobStage;
        public string JobStage
        {
            get { return this.jobStage; }
            set { this.jobStage = value; }
        }
        private string isRead;
        public string IsRead
        {
            get { return this.isRead; }
            set { this.isRead = value; }
        }
    }
}

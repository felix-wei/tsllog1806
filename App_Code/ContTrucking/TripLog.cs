using System;

namespace C2
{
    public class CtmTripLog
    {
        private int id;
        private string jobNo;
        private string jobType;
        private string driver;
        private string status;
        private DateTime logDate;
        private string logTime;
        private string remark;
        private int tripId;
        private DateTime createDateTime;
        private string createBy;
        private DateTime updateDateTime;
        private string updateBy;

        public int Id
        {
            get { return this.id; }
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

        public string Driver
        {
            get { return this.driver; }
            set { this.driver = value; }
        }

        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public DateTime LogDate
        {
            get { return this.logDate; }
            set { this.logDate = value; }
        }

        public string LogTime
        {
            get { return this.logTime; }
            set { this.logTime = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public int TripId
        {
            get { return this.tripId; }
            set { this.tripId = value; }
        }

        public DateTime CreateDateTime
        {
            get { return this.createDateTime; }
            set { this.createDateTime = value; }
        }

        public string CreateBy
        {
            get { return this.createBy; }
            set { this.createBy = value; }
        }

        public DateTime UpdateDateTime
        {
            get { return this.updateDateTime; }
            set { this.updateDateTime = value; }
        }

        public string UpdateBy
        {
            get { return this.updateBy; }
            set { this.updateBy = value; }
        }

    }
}

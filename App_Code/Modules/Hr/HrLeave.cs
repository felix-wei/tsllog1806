using System;

namespace C2
{
	public class HrLeave
	{
		private int id;
		private int person;
		private DateTime date1;
		private string time1;
        private DateTime date2;
		private string time2;
        private string days;
		private string remark;

		public int Id
		{
			get { return this.id; }
		}

		public int Person
		{
			get { return this.person; }
			set { this.person = value; }
		}

        public DateTime Date1
		{
			get { return this.date1; }
			set { this.date1 = value; }
		}

		public string Time1
		{
			get { return this.time1; }
			set { this.time1 = value; }
		}

        public DateTime Date2
		{
			get { return this.date2; }
			set { this.date2 = value; }
		}

		public string Time2
		{
			get { return this.time2; }
			set { this.time2 = value; }
		}

        public string Days
		{
			get { return this.days; }
			set { this.days = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
		}

        //2014-1-21 12:29:54
        private DateTime applyDateTime;
        public DateTime ApplyDateTime
        {
            get { return this.applyDateTime; }
            set { this.applyDateTime = value; }
        }
        private int approveBy;
        public int ApproveBy
        {
            get { return this.approveBy; }
            set { this.approveBy = value; }
        }
        private string approveStatus;
        public string ApproveStatus
        {
            get { return this.approveStatus; }
            set { this.approveStatus = value; }
        }
        private DateTime approveDate;
        public DateTime ApproveDate
        {
            get { return this.approveDate; }
            set { this.approveDate = value; }
        }
        private string approveTime;
        public string ApproveTime
        {
            get { return this.approveTime; }
            set { this.approveTime = value; }
        }
        private string approveRemark;
        public string ApproveRemark
        {
            get { return this.approveRemark; }
            set { this.approveRemark = value; }
        }

        private string leaveType;
        public string LeaveType
        {
            get { return this.leaveType; }
            set { this.leaveType = value; }
        }
	}
}

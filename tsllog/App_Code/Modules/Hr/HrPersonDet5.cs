using System;

namespace C2
{
	public class HrPersonDet5
	{
		private int id;
		private int person;
		private string employerName;
		private string positionHold;
		private DateTime dateFrom;
		private DateTime dateTo;
		private decimal salary;
		private string allowance;
		private string leaveStatus;
		private string reasonForLeaving;
		private string remark;
		private string createBy;
		private DateTime createDateTime;
		private string updateBy;
		private DateTime updateDateTime;

		public int Id
		{
			get { return this.id; }
		}

		public int Person
		{
			get { return this.person; }
			set { this.person = value; }
		}

		public string EmployerName
		{
			get { return this.employerName; }
			set { this.employerName = value; }
		}

		public string PositionHold
		{
			get { return this.positionHold; }
			set { this.positionHold = value; }
		}

		public DateTime DateFrom
		{
			get { return this.dateFrom; }
			set { this.dateFrom = value; }
		}

		public DateTime DateTo
		{
			get { return this.dateTo; }
			set { this.dateTo = value; }
		}

		public decimal Salary
		{
			get { return this.salary; }
			set { this.salary = value; }
		}

		public string Allowance
		{
			get { return this.allowance; }
			set { this.allowance = value; }
		}

		public string LeaveStatus
		{
			get { return this.leaveStatus; }
			set { this.leaveStatus = value; }
		}

		public string ReasonForLeaving
		{
			get { return this.reasonForLeaving; }
			set { this.reasonForLeaving = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
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
	}
}

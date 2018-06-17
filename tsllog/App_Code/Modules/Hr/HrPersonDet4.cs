using System;

namespace C2
{
	public class HrPersonDet4
	{
		private int id;
		private int person;
		private string schoolName;
		private DateTime dateFrom;
		private DateTime dateTo;
		private string highestLevel;
		private string status;
		private int schoolYear;
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

		public string SchoolName
		{
			get { return this.schoolName; }
			set { this.schoolName = value; }
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

		public string HighestLevel
		{
			get { return this.highestLevel; }
			set { this.highestLevel = value; }
		}

		public string Status
		{
			get { return this.status; }
			set { this.status = value; }
		}

		public int SchoolYear
		{
			get { return this.schoolYear; }
			set { this.schoolYear = value; }
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

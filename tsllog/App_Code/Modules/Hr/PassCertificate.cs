using System;

namespace C2
{
	public class PassCertificate
	{
		private int id;
		private int employee;
		private string description;
		private DateTime expiryDate;
		private string typeCode;
		private string remark;
		private string createBy;
		private DateTime createDateTime;
		private string updateBy;
		private DateTime updateDateTime;

		public int Id
		{
			get { return this.id; }
		}

		public int Employee
		{
			get { return this.employee; }
			set { this.employee = value; }
		}

		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		public DateTime ExpiryDate
		{
			get { return this.expiryDate; }
			set { this.expiryDate = value; }
		}

		public string TypeCode
		{
			get { return this.typeCode; }
			set { this.typeCode = value; }
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

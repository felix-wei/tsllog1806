using System;

namespace C2
{
	public class HrPersonDet6
	{
		private int id;
		private int person;
		private string name;
		private string relationship;
		private int age;
		private string occupation;
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

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		public string Relationship
		{
			get { return this.relationship; }
			set { this.relationship = value; }
		}

		public int Age
		{
			get { return this.age; }
			set { this.age = value; }
		}

		public string Occupation
		{
			get { return this.occupation; }
			set { this.occupation = value; }
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

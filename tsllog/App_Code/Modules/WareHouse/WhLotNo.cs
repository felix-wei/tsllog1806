using System;

namespace C2
{
	public class WhLotNo
	{
		private int id;
		private string code;
        private string description;
		private string createBy;
		private DateTime createDateTime;
		private string updateBy;
		private DateTime updateDateTime;

		public int Id
		{
			get { return this.id; }
		}

		public string Code
		{
			get { return this.code; }
			set { this.code = value; }
		}

        public string Description
		{
            get { return this.description; }
            set { this.description = value; }
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

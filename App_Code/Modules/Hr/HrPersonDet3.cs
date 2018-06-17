using System;

namespace C2
{
	public class HrPersonDet3
	{
		private int id;
		private int person;
		private string bankCode;
		private string bankName;
		private string branchCode;
		private string swiftCode;
		private string accNo;
		private bool isPayroll;
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

		public string BankCode
		{
			get { return this.bankCode; }
			set { this.bankCode = value; }
		}

		public string BankName
		{
			get { return this.bankName; }
			set { this.bankName = value; }
		}

		public string BranchCode
		{
			get { return this.branchCode; }
			set { this.branchCode = value; }
		}

		public string SwiftCode
		{
			get { return this.swiftCode; }
			set { this.swiftCode = value; }
		}

		public string AccNo
		{
			get { return this.accNo; }
			set { this.accNo = value; }
		}

		public bool IsPayroll
		{
			get { return this.isPayroll; }
			set { this.isPayroll = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
        }

        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

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

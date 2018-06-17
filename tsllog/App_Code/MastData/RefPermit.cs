using System;

namespace C2
{
	public class RefPermit
	{
		private int id;
		private string permitNo;
		private DateTime permitDate;
		private string permitBy;
		private string permitRemark;
		private string partyInvNo;
		private decimal gstAmt;
		private string paymentStatus;
		private string incoTerm;

		public int Id
		{
			get { return this.id; }
		}

		public string PermitNo
		{
			get { return this.permitNo; }
			set { this.permitNo = value; }
		}

		public DateTime PermitDate
		{
			get { return this.permitDate; }
			set { this.permitDate = value; }
		}

		public string PermitBy
		{
			get { return this.permitBy; }
			set { this.permitBy = value; }
		}

		public string PermitRemark
		{
			get { return this.permitRemark; }
			set { this.permitRemark = value; }
		}

		public string PartyInvNo
		{
			get { return this.partyInvNo; }
			set { this.partyInvNo = value; }
		}

		public decimal GstAmt
		{
			get { return this.gstAmt; }
			set { this.gstAmt = value; }
		}

		public string PaymentStatus
		{
			get { return this.paymentStatus; }
			set { this.paymentStatus = value; }
		}

		public string IncoTerm
		{
			get { return this.incoTerm; }
			set { this.incoTerm = value; }
		}
        private string jobNo;
        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }
        private int contId;
        public int ContId
        {
            get { return this.contId; }
            set { this.contId = value; }
        }
        private string hblNo;
        public string HblNo
        {
            get { return this.hblNo; }
            set { this.hblNo = value; }
        }
	}
}

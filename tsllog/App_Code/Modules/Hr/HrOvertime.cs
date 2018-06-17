using System;

namespace C2
{
	public class HrOvertime
	{
		private int id;
		private decimal hour;
		private decimal hoursRate;
		private int person;
		private decimal time;
		private decimal totalAmt;
		private string typeId;

		public int Id
		{
			get { return this.id; }
		}

		public decimal Hour
		{
			get { return this.hour; }
			set { this.hour = value; }
		}

		public decimal HoursRate
		{
			get { return this.hoursRate; }
			set { this.hoursRate = value; }
		}

		public int Person
		{
			get { return this.person; }
			set { this.person = value; }
		}

		public decimal Time
		{
			get { return this.time; }
			set { this.time = value; }
		}

		public decimal TotalAmt
		{
			get { return this.totalAmt; }
			set { this.totalAmt = value; }
		}

		public string TypeId
		{
			get { return this.typeId; }
			set { this.typeId = value; }
		}

        private DateTime fromDate;
        private DateTime toDate;
        public DateTime FromDate
        {
            get { return this.fromDate; }
            set { this.fromDate = value; }
        }
        public DateTime ToDate
        {
            get { return this.toDate; }
            set { this.toDate = value; }
        }
	}
}

using System;

namespace C2
{
	public class XXAccStatus
	{
		private int sequenceId;
		private int year;
		private int acPeriod;
		private string acCode;
		private decimal currBal;
		private decimal openBal;
		private decimal closeBal;
		private string acDorc;
		private string acDesc;

		public int SequenceId
		{
			get { return this.sequenceId; }
		}

		public int Year
		{
			get { return this.year; }
			set { this.year = value; }
		}

		public string AcCode
		{
			get { return this.acCode; }
			set { this.acCode = value; }
		}

		public decimal CurrBal
		{
			get { return this.currBal; }
			set { this.currBal = value; }
		}

		public decimal OpenBal
		{
			get { return this.openBal; }
			set { this.openBal = value; }
		}
		public decimal CloseBal
		{
			get { return this.closeBal; }
			set { this.closeBal = value; }
		}

		public int AcPeriod
		{
			get { return this.acPeriod; }
			set { this.acPeriod = value; }
		}

		public string AcDorc
		{
			get { return this.acDorc; }
			set { this.acDorc = value; }
		}

		public string AcDesc
		{
			get { return this.acDesc; }
			set { this.acDesc = value; }
		}
	}
}

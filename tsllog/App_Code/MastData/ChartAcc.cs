using System;

namespace C2
{
	public class XXChartAcc
	{
		private int sequenceId;
		private string code;
		private string acDesc;
		private string acType;
		private string acDorc;
		private string acBank;
		private string acCurrency;
		private string acSubType;
		private string gNo;

		public int SequenceId
		{
			get { return this.sequenceId; }
		}

		public string Code
		{
			get { return this.code; }
			set { this.code = value; }
		}

		public string AcDesc
		{
			get { return this.acDesc; }
			set { this.acDesc = value; }
		}

		public string AcType
		{
			get { return this.acType; }
			set { this.acType = value; }
		}

		public string AcDorc
		{
			get { return this.acDorc; }
			set { this.acDorc = value; }
		}

		public string AcBank
		{
			get { return this.acBank; }
			set { this.acBank = value; }
		}

		public string AcCurrency
		{
			get { return this.acCurrency; }
			set { this.acCurrency = value; }
		}

		public string AcSubType
		{
			get { return this.acSubType; }
			set { this.acSubType = value; }
		}

		public string GNo
		{
			get { return this.gNo; }
			set { this.gNo = value; }
		}

	}
}

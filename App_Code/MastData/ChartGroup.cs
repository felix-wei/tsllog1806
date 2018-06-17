using System;

namespace C2
{
	public class XXChartGroup
	{
		private int sequenceId;
		private string code;
		private string name;

		public int SequenceId
		{
			get { return this.sequenceId; }
		}

		public string Code
		{
			get { return this.code; }
			set { this.code = value; }
		}

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}
	}
}

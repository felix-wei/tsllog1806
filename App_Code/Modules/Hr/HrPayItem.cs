using System;

namespace C2
{
	public class HrPayItem
	{
		private string code;
		private string description;
		private string isCpf;

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
		public string IsCpf
        {
            get { return this.isCpf; }
            set { this.isCpf = value; }
		}
	}
}

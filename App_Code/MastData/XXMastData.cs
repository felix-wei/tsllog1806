using System;
using Wilson.ORMapper;

namespace C2
{
	public class XXMastData 
	{
		private string code;
		private string description;
        private string codeType;
        private int id;

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


		public string CodeType
		{
			get { return this.codeType; }
			set { this.codeType = value; }
		}

	}
}

using System;

namespace C2
{
	public class XXPort
	{
        private int id;
		private string code;
		private string name;

        public int Id
        {
            get { return this.id; }
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
        private string userId;
		public string UserId
		{
			get { return this.userId; }
			set { this.userId = value; }
		}
        private DateTime entryDate;
        public DateTime EntryDate
        {
            get { return this.entryDate; }
            set { this.entryDate = value; }
        }
        private string airCode;
        public string AirCode
        {
            get { return this.airCode; }
            set { this.airCode = value; }
        }
        private string countryCode;
        public string CountryCode
        {
            get { return this.countryCode; }
            set { this.countryCode = value; }
        }
	}
}

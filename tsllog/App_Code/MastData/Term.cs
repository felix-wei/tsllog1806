using System;

namespace C2
{
	public class XXTerm
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
        //private string userId;
        //public string UserId
        //{
        //    get { return this.userId; }
        //    set { this.userId = value; }
        //}
        //private DateTime entryDate;
        //public DateTime EntryDate
        //{
        //    get { return this.entryDate; }
        //    set { this.entryDate = value; }
        //}
        private int creditDay;
        public int CreditDay
        {
            get { return this.creditDay; }
            set { this.creditDay = value; }
        }

	}
}

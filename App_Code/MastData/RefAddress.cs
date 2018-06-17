using System;

namespace C2
{
	public class RefAddress
	{
		private int id;
		private string address;
		private string typeId;
		private string partyId;
        private string partyName;

		public int Id
		{
			get { return this.id; }
		}

		public string Address
		{
			get { return this.address; }
			set { this.address = value; }
		}

		public string TypeId
		{
			get { return this.typeId; }
			set { this.typeId = value; }
		}

        public string PartyName
        {
            get { return this.partyName; }
            set { this.partyName = value; }
        }

		public string PartyId
		{
			get { return this.partyId; }
			set { this.partyId = value; }
		}
        private string address1;
        public string Address1
        {
            get { return this.address1; }
            set { this.address1 = value; }
        }
        private string address2;
        public string Address2
        {
            get { return this.address2; }
            set { this.address2 = value; }
        }
        private string address3;
        public string Address3
        {
            get { return this.address3; }
            set { this.address3 = value; }
        }
        private string address4;
        public string Address4
        {
            get { return this.address4; }
            set { this.address4 = value; }
        }
        private string address5;
        public string Address5
        {
            get { return this.address5; }
            set { this.address5 = value; }
        }
        private string address6;
        public string Address6
        {
            get { return this.address6; }
            set { this.address6 = value; }
        }
        private string postcode;
        public string Postcode
        {
            get { return this.postcode; }
            set { this.postcode = value; }
        }
        private string location;
        public string Location
        {
            get { return this.location; }
            set { this.location = value; }
        }
	}
}

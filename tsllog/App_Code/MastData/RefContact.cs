using System;

namespace C2
{
	public class RefContact
	{
		private int id;
		private string address;
		private string name;
		private string partyId;
		private string partyName;
		private bool isDefault;
		private string tel;
		private string fax;
		private string email;
		private string mobile;
		private string remark;

		public int Id
		{
			get { return this.id; }
		}

		public string Address
		{
			get { return this.address; }
			set { this.address = value; }
		}

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		public string PartyId
		{
			get { return this.partyId; }
			set { this.partyId = value; }
		}

		public string PartyName
		{
			get { return this.partyName; }
			set { this.partyName = value; }
		}

		public bool IsDefault
		{
			get { return this.isDefault; }
			set { this.isDefault = value; }
		}

		public string Tel
		{
			get { return this.tel; }
			set { this.tel = value; }
		}

		public string Fax
		{
			get { return this.fax; }
			set { this.fax = value; }
		}

		public string Email
		{
			get { return this.email; }
			set { this.email = value; }
		}

		public string Mobile
		{
			get { return this.mobile; }
			set { this.mobile = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
		}
        private string department;
        public string Department
        {
            get { return this.department; }
            set { this.department = value; }
        }
	}
}

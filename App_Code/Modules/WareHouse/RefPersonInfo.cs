using System;

namespace C2
{
    public class RefPersonInfo
    {
        private int id;
        private string name;
        private string partyId;
        private string iCNo;
        private string accountNo;
        private string type;
        private string address;

        public int Id
        {
            get { return this.id; }
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

        public string ICNo
        {
            get { return this.iCNo; }
            set { this.iCNo = value; }
        }

        public string AccountNo
        {
            get { return this.accountNo; }
            set { this.accountNo = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        private string tel;
        private string contact;
        private string country;
        public string Tel
        {
            get { return this.city; }
            set { this.city = value; }
        }
        public string Contact
        {
            get { return this.contact; }
            set { this.contact = value; }
        }
        public string Country
        {
            get { return this.country; }
            set { this.country = value; }
        }
        private string city;
        public string City
        {
            get { return this.city; }
            set { this.city = value; }
        }
        private string relationId;
        public string RelationId
        {
            get { return this.relationId; }
            set { this.relationId = value; }
        }
        private string zipCode;
        public string ZipCode
        {
            get { return this.zipCode; }
            set { this.zipCode = value; }
        }
    }
}
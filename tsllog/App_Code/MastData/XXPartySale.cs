using System;

namespace C2
{
    public class XXPartySale
    {
        private int id;
        private string partyId;
        private string salesman;
        private string defaultInd;

        public int Id
        {
            get { return this.id; }
        }

        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string Salesman
        {
            get { return this.salesman; }
            set { this.salesman = value; }
        }

        public string DefaultInd
        {
            get { return this.defaultInd; }
            set { this.defaultInd = value; }
        }
        public string Status
        {
            get
            {
                string s = "USE";
                string sql = "select Status from XXParty where PartyId='" + this.partyId + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
    }
}

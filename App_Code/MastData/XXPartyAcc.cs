using System;

namespace C2
{
    public class XXPartyAcc
    {
        private int sequenceId;
        private string currencyId;
        private string partyId;
        private string arCode;
        private string apCode;

        public int SequenceId
        {
            get { return this.sequenceId; }
            set { this.sequenceId = value; }
        }

        public string CurrencyId
        {
            get { return this.currencyId; }
            set { this.currencyId = value; }
        }

        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string ArCode
        {
            get { return this.arCode; }
            set { this.arCode = value; }
        }

        public string ApCode
        {
            get { return this.apCode; }
            set { this.apCode = value; }
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

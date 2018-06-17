using System;

namespace C2
{
    public class RefPartyLog
    {
        private int id;
        private string partyId;
        private string name;
        private DateTime logDateTime;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string remark;

        public int Id
        {
            get { return this.id; }
        }

        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public DateTime LogDateTime
        {
            get { return this.logDateTime; }
            set { this.logDateTime = value; }
        }

        public string CreateBy
        {
            get { return this.createBy; }
            set { this.createBy = value; }
        }

        public DateTime CreateDateTime
        {
            get { return this.createDateTime; }
            set { this.createDateTime = value; }
        }

        public string UpdateBy
        {
            get { return this.updateBy; }
            set { this.updateBy = value; }
        }

        public DateTime UpdateDateTime
        {
            get { return this.updateDateTime; }
            set { this.updateDateTime = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
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

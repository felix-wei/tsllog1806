using System;

namespace C2
{
    public class LcActivity
    {
        private int id;
        private string refNo;
        private string jobNo;
        private string lcEvent;
        private string refType;
        private string actionNote;
        private string createBy;
        private DateTime createDateTime;
        private string infoNote;
        private string status;

        public int Id
        {
            get { return this.id; }
        }

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public string LcEvent
        {
            get { return this.lcEvent; }
            set { this.lcEvent = value; }
        }

        public string RefType
        {
            get { return this.refType; }
            set { this.refType = value; }
        }

        public string ActionNote
        {
            get { return this.actionNote; }
            set { this.actionNote = value; }
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

        public string InfoNote
        {
            get { return this.infoNote; }
            set { this.infoNote = value; }
        }

        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        public string RefStatus
        {
            get
            {
                string s = "USE";
                string sql = "select Status from XXParty where PartyId='" + this.RefNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
    }
}

using System;

namespace C2
{
    public class WhTransfer
    {
        private int id;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string transferNo;
        private string requestPerson;
        private DateTime requestDate;
        private DateTime transferDate;
        private string pic;
        private string statusCode;
        private string confirmPerson;
        private DateTime confirmDate;

        public int Id
        {
            get { return this.id; }
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

        public string TransferNo
        {
            get { return this.transferNo; }
            set { this.transferNo = value; }
        }

        public string RequestPerson
        {
            get { return this.requestPerson; }
            set { this.requestPerson = value; }
        }

        public DateTime RequestDate
        {
            get { return this.requestDate; }
            set { this.requestDate = value; }
        }

        public DateTime TransferDate
        {
            get { return this.transferDate; }
            set { this.transferDate = value; }
        }

        public string Pic
        {
            get { return this.pic; }
            set { this.pic = value; }
        }

        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }

        public string ConfirmPerson
        {
            get { return this.confirmPerson; }
            set { this.confirmPerson = value; }
        }

        public DateTime ConfirmDate
        {
            get { return this.confirmDate; }
            set { this.confirmDate = value; }
        }
        private string partyId;
        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }
        private string partyName;
        public string PartyName
        {
            get { return this.partyName; }
            set { this.partyName = value; }
        }
    }
}

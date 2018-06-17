using System;

namespace C2
{
    public class WhContract
    {
        private int id;
        private string whCode;
        private string contractNo;
        private DateTime contractDate;
        private DateTime expireDate;
        private DateTime startDate;
        private string partyId;
        private string statusCode;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

        public int Id
        {
            get { return this.id; }
        }

        public string WhCode
        {
            get { return this.whCode; }
            set { this.whCode = value; }
        }

        public string ContractNo
        {
            get { return this.contractNo; }
            set { this.contractNo = value; }
        }

        public DateTime ContractDate
        {
            get { return this.contractDate; }
            set { this.contractDate = value; }
        }
        public DateTime ExpireDate
        {
            get { return this.expireDate; }
            set { this.expireDate = value; }
        }
        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }

        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
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
        private string remark;
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
        private string storageType;
        public string StorageType
        {
            get { return this.storageType; }
            set { this.storageType = value; }
        }

    }
}

using System;

namespace C2
{
    public class StockCount
    {
        private int id;

        private DateTime stockDate;
        private string partyId;
        private string partyName;
        private string partyAdd;
        private string wareHouseId;
        private string location;
        private string remark;
        private string createdBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

        public int Id
        {
            get { return this.id; }
        }

        public DateTime StockDate
        {
            get { return this.stockDate; }
            set { this.stockDate = value; }
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

        public string PartyAdd
        {
            get { return this.partyAdd; }
            set { this.partyAdd = value; }
        }
        public string WareHouseId
        {
            get { return this.wareHouseId; }
            set { this.wareHouseId = value; }
        }
        public string Location
        {
            get { return this.location; }
            set { this.location = value; }
        }
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public string CreatedBy
        {
            get { return this.createdBy; }
            set { this.createdBy = value; }
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
        private string stockNo;
        public string StockNo
        {
            get { return this.stockNo; }
            set { this.stockNo = value; }
        }
    }
}

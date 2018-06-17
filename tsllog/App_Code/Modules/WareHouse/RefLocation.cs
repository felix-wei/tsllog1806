using System;

namespace C2
{
    public class RefLocation
    {
        private int id;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string code;
        private string name;
        private string warehouseCode;
        private string zoneCode;
        private string storeCode;
        private string partyId;
        private string remark;
        private decimal length;
        private decimal width;
        private decimal height;
        private decimal spaceM3;
        private string loclevel;

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

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string WarehouseCode
        {
            get { return this.warehouseCode; }
            set { this.warehouseCode = value; }
        }

        public string ZoneCode
        {
            get { return this.zoneCode; }
            set { this.zoneCode = value; }
        }

        public string StoreCode
        {
            get { return this.storeCode; }
            set { this.storeCode = value; }
        }

        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public decimal Length
        {
            get { return this.length; }
            set { this.length = value; }
        }

        public decimal Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public decimal Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public decimal SpaceM3
        {
            get { return this.spaceM3; }
            set { this.spaceM3 = value; }
        }

        public string Loclevel
        {
            get { return this.loclevel; }
            set { this.loclevel = value; }
        }
    }
}
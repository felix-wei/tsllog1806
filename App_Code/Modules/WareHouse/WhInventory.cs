using System;

namespace C2
{
    public class WhInventory
    {
        private int id;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string inventoryNo;
        private DateTime inventoryDate;
        private string inventoryUser;

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

        public string InventoryNo
        {
            get { return this.inventoryNo; }
            set { this.inventoryNo = value; }
        }

        public DateTime InventoryDate
        {
            get { return this.inventoryDate; }
            set { this.inventoryDate = value; }
        }

        public string InventoryUser
        {
            get { return this.inventoryUser; }
            set { this.inventoryUser = value; }
        }
    }
}

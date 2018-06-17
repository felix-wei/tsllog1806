using System;

namespace C2
{
    public class JobItemList
    {
        private int id;
        private string refNo;
        private string jobNo;
        private string itemType;
        private string itemName;
        private int itemQty;
        private decimal itemValue;
        private string itemMark;
        private string itemNote;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

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

        public string ItemType
        {
            get { return this.itemType; }
            set { this.itemType = value; }
        }

        public string ItemName
        {
            get { return this.itemName; }
            set { this.itemName = value; }
        }

        public int ItemQty
        {
            get { return this.itemQty; }
            set { this.itemQty = value; }
        }

        public decimal ItemValue
        {
            get { return this.itemValue; }
            set { this.itemValue = value; }
        }

        public string ItemMark
        {
            get { return this.itemMark; }
            set { this.itemMark = value; }
        }

        public string ItemNote
        {
            get { return this.itemNote; }
            set { this.itemNote = value; }
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
	    public string JobStatus
        {
            get
            {
                string s = "USE";
                string sql = "select JobStatus from JobInfo where JobNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
    }
}

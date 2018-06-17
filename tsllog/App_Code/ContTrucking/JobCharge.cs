using System;

namespace C2
{
    public class CtmJobCharge
    {
        private int id;
        private string jobNo;
        private string jobType;
        private string itemName;
        private string itemType;
        private decimal cost;
        private DateTime createDateTime;
        private string note1;
        private string note2;
        private string note3;
        private string note4;

        public int Id
        {
            get { return this.id; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }

        public string ItemName
        {
            get { return this.itemName; }
            set { this.itemName = value; }
        }

        public string ItemType
        {
            get { return this.itemType; }
            set { this.itemType = value; }
        }

        public decimal Cost
        {
            get { return this.cost; }
            set { this.cost = value; }
        }

        public DateTime CreateDateTime
        {
            get { return this.createDateTime; }
            set { this.createDateTime = value; }
        }

        public string Note1
        {
            get { return this.note1; }
            set { this.note1 = value; }
        }

        public string Note2
        {
            get { return this.note2; }
            set { this.note2 = value; }
        }

        public string Note3
        {
            get { return this.note3; }
            set { this.note3 = value; }
        }

        public string Note4
        {
            get { return this.note4; }
            set { this.note4 = value; }
        }
    }
}

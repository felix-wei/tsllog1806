using System;

namespace C2
{
    public class WhSchedule
    {
        private int id;
        private int balQty;
        private int firstQty;
        private string partyId;
        private string product;
        private string doNo;
        private DateTime scheduleDate;

        public int Id
        {
            get { return this.id; }
        }


        public int FirstQty
        {
            get { return this.firstQty; }
            set { this.firstQty = value; }
        }
        public int BalQty
        {
            get { return this.balQty; }
            set { this.balQty = value; }
        }

        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string Product
        {
            get { return this.product; }
            set { this.product = value; }
        }

        public string DoNo
        {
            get { return this.doNo; }
            set { this.doNo = value; }
        }

        public DateTime ScheduleDate
        {
            get { return this.scheduleDate; }
            set { this.scheduleDate = value; }
        }
    }
}

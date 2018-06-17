using System;

namespace C2
{
    public class VehicleFuelLog
    {
        private int id;
        private string vehicleNo;
        private DateTime createDateTime;
        private string type;
        private decimal volume;
        private decimal amount;
        private string note;
        private string createBy;
        private DateTime reportDate;

        public int Id
        {
            get { return this.id; }
        }

        public string VehicleNo
        {
            get { return this.vehicleNo; }
            set { this.vehicleNo = value; }
        }

        public DateTime CreateDateTime
        {
            get { return this.createDateTime; }
            set { this.createDateTime = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public decimal Volume
        {
            get { return this.volume; }
            set { this.volume = value; }
        }

        public decimal Amount
        {
            get { return this.amount; }
            set { this.amount = value; }
        }

        public string Note
        {
            get { return this.note; }
            set { this.note = value; }
        }

        public string CreateBy
        {
            get { return this.createBy; }
            set { this.createBy = value; }
        }

        public DateTime ReportDate
        {
            get { return this.reportDate; }
            set { this.reportDate = value; }
        }
    }
}

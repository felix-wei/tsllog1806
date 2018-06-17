using System;

namespace C2
{
    public class VehicleMileage
    {
        private int id;
        private string vehicleNo;
        private DateTime createDateTime;
        private decimal value;
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

        public decimal Value
        {
            get { return this.value; }
            set { this.value = value; }
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

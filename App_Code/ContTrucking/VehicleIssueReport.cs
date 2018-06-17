using System;

namespace C2
{
    public class VehicleIssueReport
    {
        private int id;
        private string vehicleNo;
        private DateTime createDateTime;
        private string description;
        private string actionTaken;
        private string note;
        private string createBy;
        private DateTime reportDate;
        private string actionType;
        private decimal mileage;

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

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public string ActionTaken
        {
            get { return this.actionTaken; }
            set { this.actionTaken = value; }
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

        public string ActionType
        {
            get { return this.actionType; }
            set { this.actionType = value; }
        }

        public decimal Mileage
        {
            get { return this.mileage; }
            set { this.mileage = value; }
        }
    }
}

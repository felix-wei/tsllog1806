using System;

namespace C2
{
    public class PackingLotItem
    {
        private int id;
        private string pLCode;
        private DateTime createDate;
        private string jobType;
        private int tripId;
        private string continerNo;
        private string driverCode;
        private string trailer;
        private string statusCode;
        private string note1;
        private string note2;
        private string note3;
        private string note4;
        private string note5;

        public int Id
        {
            get { return this.id; }
        }

        public string PLCode
        {
            get { return this.pLCode; }
            set { this.pLCode = value; }
        }

        public DateTime CreateDate
        {
            get { return this.createDate; }
            set { this.createDate = value; }
        }

        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }

        public int TripId
        {
            get { return this.tripId; }
            set { this.tripId = value; }
        }

        public string ContinerNo
        {
            get { return this.continerNo; }
            set { this.continerNo = value; }
        }

        public string DriverCode
        {
            get { return this.driverCode; }
            set { this.driverCode = value; }
        }

        public string Trailer
        {
            get { return this.trailer; }
            set { this.trailer = value; }
        }

        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
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

        public string Note5
        {
            get { return this.note5; }
            set { this.note5 = value; }
        }
    }
}

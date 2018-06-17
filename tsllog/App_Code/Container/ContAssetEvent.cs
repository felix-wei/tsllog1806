using System;

namespace C2
{
    public class ContAssetEvent
    {
        private int id;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string docNo;
        private string docType;
        private string jobNo;
        private string eventCode;
        private DateTime eventDateTime;
        private string eventPort;
        private string eventDepot;
        private string pol;
        private string pod;
        private string vehicleNo;
        private DateTime returnDate;
        private DateTime receiveDate;
        private string state;
        private DateTime releaseDate;
        private string insturction;
        private string containerNo;
        private string containerType;

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

        public string DocNo
        {
            get { return this.docNo; }
            set { this.docNo = SafeValue.SafeString(value); }
        }

        public string DocType
        {
            get { return this.docType; }
            set { this.docType = SafeValue.SafeString(value); }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = SafeValue.SafeString(value); }
        }

        public string EventCode
        {
            get { return this.eventCode; }
            set { this.eventCode = SafeValue.SafeString(value); }
        }

        public DateTime EventDateTime
        {
            get { return this.eventDateTime; }
            set { this.eventDateTime = value; }
        }

        public string EventPort
        {
            get { return this.eventPort; }
            set { this.eventPort = SafeValue.SafeString(value); }
        }

        public string EventDepot
        {
            get { return this.eventDepot; }
            set { this.eventDepot = SafeValue.SafeString(value); }
        }

        public string Pol
        {
            get { return this.pol; }
            set { this.pol = SafeValue.SafeString(value); }
        }

        public string Pod
        {
            get { return this.pod; }
            set { this.pod = SafeValue.SafeString(value); }
        }

        public string VehicleNo
        {
            get { return this.vehicleNo; }
            set { this.vehicleNo = SafeValue.SafeString(value); }
        }

        public DateTime ReturnDate
        {
            get { return this.returnDate; }
            set { this.returnDate = value; }
        }

        public DateTime ReceiveDate
        {
            get { return this.receiveDate; }
            set { this.receiveDate = value; }
        }

        public string State
        {
            get { return this.state; }
            set { this.state = SafeValue.SafeString(value); }
        }

        public DateTime ReleaseDate
        {
            get { return this.releaseDate; }
            set { this.releaseDate = value; }
        }

        public string Insturction
        {
            get { return this.insturction; }
            set { this.insturction = SafeValue.SafeString(value); }
        }

        public string ContainerNo
        {
            get { return this.containerNo; }
            set { this.containerNo = SafeValue.SafeString(value); }
        }

        public string ContainerType
        {
            get { return this.containerType; }
            set { this.containerType = SafeValue.SafeString(value); }
        }

    }
}

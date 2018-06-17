using System;

namespace C2
{
    public class ContAsset
    {
        private int id;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string docNo;
        private string docType;
        private string jobNo;
        private string depotCode;
        private string shipType;
        private string shipVessel;
        private string shipVoyage;
        private string shipPol;
        private string shipPod;
        private string shipFinDest;
        private DateTime shipEta;
        private DateTime shipEtd;
        private string shipNote;
        private string shipCarrierCode;
        private string shipCarrierRef;
        private string demurrageCode;
        private int demurrageFreeDay;
        private DateTime demurrageStartDate;
        private string demurrageRef;
        private string detentionCode;
        private int detentionFreeDay;
        private DateTime detentionStartDate;
        private string detentionRef;
        private string releaseType;
        private string returnType;
        private string shipperCode;
        private string shipperName;
        private string consigneeCode;
        private string consigneeName;
        private string instruction;
        private string haulierCode;
        private string haulierRef;
        private DateTime haulierCompleteDate;
        private string statusCode;

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
            set { this.docNo = value; }
        }

        public string DocType
        {
            get { return this.docType; }
            set { this.docType = value; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public string DepotCode
        {
            get { return this.depotCode; }
            set { this.depotCode = value; }
        }

        public string ShipType
        {
            get { return this.shipType; }
            set { this.shipType = value; }
        }

        public string ShipVessel
        {
            get { return this.shipVessel; }
            set { this.shipVessel = value; }
        }

        public string ShipVoyage
        {
            get { return this.shipVoyage; }
            set { this.shipVoyage = value; }
        }

        public string ShipPol
        {
            get { return this.shipPol; }
            set { this.shipPol = value; }
        }

        public string ShipPod
        {
            get { return this.shipPod; }
            set { this.shipPod = value; }
        }

        public string ShipFinDest
        {
            get { return this.shipFinDest; }
            set { this.shipFinDest = value; }
        }

        public DateTime ShipEta
        {
            get { return this.shipEta; }
            set { this.shipEta = value; }
        }

        public DateTime ShipEtd
        {
            get { return this.shipEtd; }
            set { this.shipEtd = value; }
        }

        public string ShipNote
        {
            get { return this.shipNote; }
            set { this.shipNote = value; }
        }

        public string ShipCarrierCode
        {
            get { return this.shipCarrierCode; }
            set { this.shipCarrierCode = value; }
        }

        public string ShipCarrierRef
        {
            get { return this.shipCarrierRef; }
            set { this.shipCarrierRef = value; }
        }

        public string DemurrageCode
        {
            get { return this.demurrageCode; }
            set { this.demurrageCode = value; }
        }

        public int DemurrageFreeDay
        {
            get { return this.demurrageFreeDay; }
            set { this.demurrageFreeDay = value; }
        }

        public DateTime DemurrageStartDate
        {
            get { return this.demurrageStartDate; }
            set { this.demurrageStartDate = value; }
        }

        public string DemurrageRef
        {
            get { return this.demurrageRef; }
            set { this.demurrageRef = value; }
        }

        public string DetentionCode
        {
            get { return this.detentionCode; }
            set { this.detentionCode = value; }
        }

        public int DetentionFreeDay
        {
            get { return this.detentionFreeDay; }
            set { this.detentionFreeDay = value; }
        }

        public DateTime DetentionStartDate
        {
            get { return this.detentionStartDate; }
            set { this.detentionStartDate = value; }
        }

        public string DetentionRef
        {
            get { return this.detentionRef; }
            set { this.detentionRef = value; }
        }

        public string ReleaseType
        {
            get { return this.releaseType; }
            set { this.releaseType = value; }
        }

        public string ReturnType
        {
            get { return this.returnType; }
            set { this.returnType = value; }
        }

        public string ShipperCode
        {
            get { return this.shipperCode; }
            set { this.shipperCode = value; }
        }

        public string ShipperName
        {
            get { return this.shipperName; }
            set { this.shipperName = value; }
        }

        public string ConsigneeCode
        {
            get { return this.consigneeCode; }
            set { this.consigneeCode = value; }
        }

        public string ConsigneeName
        {
            get { return this.consigneeName; }
            set { this.consigneeName = value; }
        }

        public string Instruction
        {
            get { return this.instruction; }
            set { this.instruction = value; }
        }

        public string HaulierCode
        {
            get { return this.haulierCode; }
            set { this.haulierCode = value; }
        }

        public string HaulierRef
        {
            get { return this.haulierRef; }
            set { this.haulierRef = value; }
        }

        public DateTime HaulierCompleteDate
        {
            get { return this.haulierCompleteDate; }
            set { this.haulierCompleteDate = value; }
        }

        public string StatusCode
        {
            get { return SafeValue.SafeString( this.statusCode,"USE"); }
            set { this.statusCode = value; }
        }
        public bool saveEnable
        {
            get
            {
                if (this.StatusCode == "USE")
                {
                    return true;
                }
                return false;
            }
        }
        public bool closeEnable
        {
            get
            {
                if (this.StatusCode == "USE" || this.StatusCode == "CLS")
                {
                    return true;
                }
                return false;
            }
        }
        public bool voidEnable
        {
            get
            {
                if (this.StatusCode == "USE" || this.StatusCode == "CNL")
                {
                    return true;
                }
                return false;
            }
        }

    }
}

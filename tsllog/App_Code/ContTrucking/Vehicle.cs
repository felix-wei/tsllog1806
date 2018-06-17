using System;

namespace C2
{
    public class RefVehicle
    {
        private int id;
        private string vehicleCode;
        private string vehicleType;
        private string vehicleStatus;
        private string contractNo;
        private string contractType;
        private DateTime contractDate;
        private DateTime contractExpiryDate;
        private string licenseNo;
        private DateTime licenseExpiryDate;
        private string supplierCode;
        private string remark;
        private DateTime date1;
        private DateTime date2;

        public int Id
        {
            get { return this.id; }
        }

        public string VehicleCode
        {
            get { return this.vehicleCode; }
            set { this.vehicleCode = value; }
        }

        public string VehicleType
        {
            get { return this.vehicleType; }
            set { this.vehicleType = value; }
        }

        public string VehicleStatus
        {
            get { return this.vehicleStatus; }
            set { this.vehicleStatus = value; }
        }

        public string ContractNo
        {
            get { return this.contractNo; }
            set { this.contractNo = value; }
        }

        public string ContractType
        {
            get { return this.contractType; }
            set { this.contractType = value; }
        }

        public DateTime ContractDate
        {
            get { return this.contractDate; }
            set { this.contractDate = value; }
        }

        public DateTime ContractExpiryDate
        {
            get { return this.contractExpiryDate; }
            set { this.contractExpiryDate = value; }
        }

        public string LicenseNo
        {
            get { return this.licenseNo; }
            set { this.licenseNo = value; }
        }

        public DateTime LicenseExpiryDate
        {
            get { return this.licenseExpiryDate; }
            set { this.licenseExpiryDate = value; }
        }

        public string SupplierCode
        {
            get { return this.supplierCode; }
            set { this.supplierCode = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public DateTime Date1
        {
            get { return this.date1; }
            set { this.date1 = value; }
        }

        public DateTime Date2
        {
            get { return this.date2; }
            set { this.date2 = value; }
        }

        private string veicleSize;
        private string vehicleUse;
        public string VeicleSize
        {
            get { return this.veicleSize; }
            set { this.veicleSize = value; }
        }
        public string VehicleUse
        {
            get { return this.vehicleUse; }
            set { this.vehicleUse = value; }
        }
        private DateTime insuranceExpiryDate;
        private DateTime roadTaxExpiryDate;
        public DateTime InsuranceExpiryDate
        {
            get { return this.insuranceExpiryDate; }
            set { this.insuranceExpiryDate = value; }
        }
        public DateTime RoadTaxExpiryDate
        {
            get { return this.roadTaxExpiryDate; }
            set { this.roadTaxExpiryDate = value; }
        }
        private DateTime craneLGCertExpiryDate;
        private DateTime craneLHCertExpiryDate;
        private DateTime vpcExpiryDate;
        private DateTime lastInternalInspectionDate;
        public DateTime CraneLGCertExpiryDate
        {
            get { return this.craneLGCertExpiryDate; }
            set { this.craneLGCertExpiryDate = value; }
        }
        public DateTime CraneLHCertExpiryDate
        {
            get { return this.craneLHCertExpiryDate; }
            set { this.craneLHCertExpiryDate = value; }
        }
        public DateTime VpcExpiryDate
        {
            get { return this.vpcExpiryDate; }
            set { this.vpcExpiryDate = value; }
        }
        public DateTime LastInternalInspectionDate
        {
            get { return this.lastInternalInspectionDate; }
            set { this.lastInternalInspectionDate = value; }
        }
        
    }

    //=======================================================================
    //===================================================RefVehicleFuel
    public class RefVehicleFuel
    {
        private int id;
        private DateTime eventDate;
        private string driverCode;
        private string vehicleCode;
        private string description;
        private decimal openLiter;
        private decimal closeLiter;
        private decimal newLiter;
        private decimal totalPrice;
        private string docNo;
        private string supplierCode;
        private string remark;

        public int Id
        {
            get { return this.id; }
        }

        public DateTime EventDate
        {
            get { return this.eventDate; }
            set { this.eventDate = value; }
        }

        public string DriverCode
        {
            get { return this.driverCode; }
            set { this.driverCode = value; }
        }

        public string VehicleCode
        {
            get { return this.vehicleCode; }
            set { this.vehicleCode = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public decimal OpenLiter
        {
            get { return this.openLiter; }
            set { this.openLiter = value; }
        }

        public decimal CloseLiter
        {
            get { return this.closeLiter; }
            set { this.closeLiter = value; }
        }

        public decimal NewLiter
        {
            get { return this.newLiter; }
            set { this.newLiter = value; }
        }

        public decimal TotalPrice
        {
            get { return this.totalPrice; }
            set { this.totalPrice = value; }
        }

        public string DocNo
        {
            get { return this.docNo; }
            set { this.docNo = value; }
        }

        public string SupplierCode
        {
            get { return this.supplierCode; }
            set { this.supplierCode = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
    }

    //=====================================================================
    //================================================RefVehicleLog
    public class RefVehicleLog
    {
        private int id;
        private string eventCode;
        private DateTime eventDate;
        private string driverCode;
        private string vehicleCode;
        private string description;
        private string eventType;
        private decimal totalPrice;
        private string docNo;
        private string supplierCode;
        private string remark;

        public int Id
        {
            get { return this.id; }
        }

        public string EventCode
        {
            get { return this.eventCode; }
            set { this.eventCode = value; }
        }

        public DateTime EventDate
        {
            get { return this.eventDate; }
            set { this.eventDate = value; }
        }

        public string DriverCode
        {
            get { return this.driverCode; }
            set { this.driverCode = value; }
        }

        public string VehicleCode
        {
            get { return this.vehicleCode; }
            set { this.vehicleCode = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public string EventType
        {
            get { return this.eventType; }
            set { this.eventType = value; }
        }

        public decimal TotalPrice
        {
            get { return this.totalPrice; }
            set { this.totalPrice = value; }
        }

        public string DocNo
        {
            get { return this.docNo; }
            set { this.docNo = value; }
        }

        public string SupplierCode
        {
            get { return this.supplierCode; }
            set { this.supplierCode = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        private DateTime insuranceExpiryDate;
        private DateTime roadTaxExpiryDate;
        public DateTime InsuranceExpiryDate
        {
            get { return this.insuranceExpiryDate; }
            set { this.insuranceExpiryDate = value; }
        }
        public DateTime RoadTaxExpiryDate
        {
            get { return this.roadTaxExpiryDate; }
            set { this.roadTaxExpiryDate = value; }
        }
        private string eventStatus;
        public string EventStatus
        {
            get { return this.eventStatus; }
            set { this.eventStatus = value; }
        }
    }

    //======================================================================
    //====================================================RefDriverCash
    public class RefDriverCash
    {
        private int id;
        private DateTime eventDate;
        private string eventType;
        private DateTime eventExpiryDate;
        private string description;
        private string driverCode;
        private decimal totalAmount;
        private string staffCode;
        private string docNo;
        private decimal docAmt;
        private string remark;
        private string jobNo;
        private string contNo;
        private string tripNo;
        private string tripType;
        public int Id
        {
            get { return this.id; }
        }

        public DateTime EventDate
        {
            get { return this.eventDate; }
            set { this.eventDate = value; }
        }

        public string EventType
        {
            get { return this.eventType; }
            set { this.eventType = value; }
        }

        public DateTime EventExpiryDate
        {
            get { return this.eventExpiryDate; }
            set { this.eventExpiryDate = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public string DriverCode
        {
            get { return this.driverCode; }
            set { this.driverCode = value; }
        }

        public decimal TotalAmount
        {
            get { return this.totalAmount; }
            set { this.totalAmount = value; }
        }

        public string StaffCode
        {
            get { return this.staffCode; }
            set { this.staffCode = value; }
        }

        public string DocNo
        {
            get { return this.docNo; }
            set { this.docNo = value; }
        }

        public decimal DocAmt
        {
            get { return this.docAmt; }
            set { this.docAmt = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }
        public string ContNo
        {
            get { return this.contNo; }
            set { this.contNo = value; }
        }
        public string TripNo
        {
            get { return this.tripNo; }
            set { this.tripNo = value; }
        }
        public string TripType
        {
            get { return this.tripType; }
            set { this.tripType = value; }
        }
    }
}

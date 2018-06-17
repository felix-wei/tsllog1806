using System;

namespace C2
{
    public class Container
    {
        private int id;
        private string containerNo;
        private DateTime commDate;
        private string tankCat;
        private string lessor;
        private DateTime manuDate;
        private string manufacturer;
        private string plateNo;
        private string containerType;
        private decimal externalLength;
        private decimal externalBreadth;
        private decimal externalHeight;
        private decimal internalLength;
        private decimal internalBreadth;
        private decimal internalHeight;
        private string material;
        private string externalCoat;
        private decimal capacity;
        private decimal maxGrossWeight;
        private decimal tareWeight;
        private decimal maxPayload;
        private decimal testPress;
        private decimal thickness;
        private string approvals;
        private DateTime onHireDateTime;
        private DateTime offHireDateTime;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string statusCode;

        public int Id
        {
            get { return this.id; }
        }

        public string ContainerNo
        {
            get { return this.containerNo; }
            set { this.containerNo = value; }
        }

        public DateTime CommDate
        {
            get { return this.commDate; }
            set { this.commDate = value; }
        }

        public string TankCat
        {
            get { return this.tankCat; }
            set { this.tankCat = value; }
        }

        public string Lessor
        {
            get { return this.lessor; }
            set { this.lessor = value; }
        }

        public DateTime ManuDate
        {
            get { return this.manuDate; }
            set { this.manuDate = value; }
        }

        public string Manufacturer
        {
            get { return this.manufacturer; }
            set { this.manufacturer = value; }
        }

        public string PlateNo
        {
            get { return this.plateNo; }
            set { this.plateNo = value; }
        }

        public string ContainerType
        {
            get { return this.containerType; }
            set { this.containerType = value; }
        }

        public decimal ExternalLength
        {
            get { return this.externalLength; }
            set { this.externalLength = value; }
        }

        public decimal ExternalBreadth
        {
            get { return this.externalBreadth; }
            set { this.externalBreadth = value; }
        }

        public decimal ExternalHeight
        {
            get { return this.externalHeight; }
            set { this.externalHeight = value; }
        }

        public decimal InternalLength
        {
            get { return this.internalLength; }
            set { this.internalLength = value; }
        }

        public decimal InternalBreadth
        {
            get { return this.internalBreadth; }
            set { this.internalBreadth = value; }
        }

        public decimal InternalHeight
        {
            get { return this.internalHeight; }
            set { this.internalHeight = value; }
        }

        public string Material
        {
            get { return this.material; }
            set { this.material = value; }
        }

        public string ExternalCoat
        {
            get { return this.externalCoat; }
            set { this.externalCoat = value; }
        }

        public decimal Capacity
        {
            get { return this.capacity; }
            set { this.capacity = value; }
        }

        public decimal MaxGrossWeight
        {
            get { return this.maxGrossWeight; }
            set { this.maxGrossWeight = value; }
        }

        public decimal TareWeight
        {
            get { return this.tareWeight; }
            set { this.tareWeight = value; }
        }

        public decimal MaxPayload
        {
            get { return this.maxPayload; }
            set { this.maxPayload = value; }
        }

        public decimal TestPress
        {
            get { return this.testPress; }
            set { this.testPress = value; }
        }

        public decimal Thickness
        {
            get { return this.thickness; }
            set { this.thickness = value; }
        }

        public string Approvals
        {
            get { return this.approvals; }
            set { this.approvals = value; }
        }

        public DateTime OnHireDateTime
        {
            get { return this.onHireDateTime; }
            set { this.onHireDateTime = value; }
        }

        public DateTime OffHireDateTime
        {
            get { return this.offHireDateTime; }
            set { this.offHireDateTime = value; }
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

        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }
    }
}

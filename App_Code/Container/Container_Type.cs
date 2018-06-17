using System;

namespace C2
{
    public class Container_Type
    {
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
        private string approvals;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string statusCode;
        private int id;
        private string containerType;
        private string stacking;
        private decimal testPress;

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

        public string Approvals
        {
            get { return this.approvals; }
            set { this.approvals = value; }
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

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string ContainerType
        {
            get { return this.containerType; }
            set { this.containerType = value; }
        }

        public string Stacking
        {
            get { return this.stacking; }
            set { this.stacking = value; }
        }

        public decimal TestPress
        {
            get { return this.testPress; }
            set { this.testPress = value; }
        }
    }
}

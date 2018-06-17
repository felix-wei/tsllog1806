using System;

namespace C2
{
    public class XXCustVendor
    {
        private int sequenceId;
        private string custId;
        private string vendorId;

        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public string CustId
        {
            get { return this.custId; }
            set { this.custId = value; }
        }

        public string VendorId
        {
            get { return this.vendorId; }
            set { this.vendorId = value; }
        }
    }
}

using System;

namespace C2
{
    public class XXGstAccount
    {
        private int sequenceId;
        private string acCode;
        private string gstSrc;
        private string gstType;
        private decimal gstValue;

        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public string AcCode
        {
            get { return this.acCode; }
            set { this.acCode = value; }
        }

        public string GstSrc
        {
            get { return this.gstSrc; }
            set { this.gstSrc = value; }
        }

        public string GstType
        {
            get { return this.gstType; }
            set { this.gstType = value; }
        }

        public decimal GstValue
        {
            get { return this.gstValue; }
            set { this.gstValue = value; }
        }

    }
}

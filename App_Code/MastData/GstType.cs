using System;

namespace C2
{
    public class XXGstType
    {
        private int sequenceId;
        private string code;
        private string src;
        private string des;
        private decimal gstValue;

        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Src
        {
            get { return this.src; }
            set { this.src = value; }
        }

        public string Des
        {
            get { return this.des; }
            set { this.des= value; }
        }

        public decimal GstValue
        {
            get { return this.gstValue; }
            set { this.gstValue = value; }
        }

    }
}

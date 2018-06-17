using System;

namespace C2
{
    public class XXBank
    {
        private int sequenceId;
        private string bankCode;
        private string bankName;

        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public string BankCode
        {
            get { return this.bankCode; }
            set { this.bankCode = value; }
        }

        public string BankName
        {
            get { return this.bankName; }
            set { this.bankName = value; }
        }

    }
}

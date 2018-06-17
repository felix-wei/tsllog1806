using System;

namespace C2
{
    public class WhPutAway
    {
        private int dodetId;
        private string doNo;
        private string doType;
        private string code;
        private string name;
        private int qty;
        private string remark;

        public int DodetId
        {
            get { return this.dodetId; }
            set { this.dodetId = value; }
        }

        public string DoNo
        {
            get { return this.doNo; }
            set { this.doNo = value; }
        }

        public string DoType
        {
            get { return this.doType; }
            set { this.doType = value; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
    }
}

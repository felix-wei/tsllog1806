using System;

namespace C2
{
    public class CtmParkingZone
    {
        private int id;
        private string code;
        private decimal size20;
        private decimal size40;
        private decimal doubleMT;
        private string note;

        public int Id
        {
            get { return this.id; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public decimal Size20
        {
            get { return this.size20; }
            set { this.size20 = value; }
        }

        public decimal Size40
        {
            get { return this.size40; }
            set { this.size40 = value; }
        }

        public decimal DoubleMT
        {
            get { return this.doubleMT; }
            set { this.doubleMT = value; }
        }

        public string Note
        {
            get { return this.note; }
            set { this.note = value; }
        }
    }
}

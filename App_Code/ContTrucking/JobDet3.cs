using System;

namespace C2
{
    public class CtmDriverLog
    {
        private int id;
        private DateTime date;
        private string driver;
        private string towhead;
        private string isActive;
        private string teamNo;

        public int Id
        {
            get { return this.id; }
        }

        public DateTime Date
        {
            get { return this.date; }
            set { this.date = value; }
        }

        public string Driver
        {
            get { return this.driver; }
            set { this.driver = value; }
        }

        public string Towhead
        {
            get { return this.towhead; }
            set { this.towhead = value; }
        }

        public string IsActive
        {
            get { return this.isActive; }
            set { this.isActive = value; }
        }
        public string TeamNo
        {
            get { return this.teamNo; }
            set { this.teamNo = value; }
        }
    }
}

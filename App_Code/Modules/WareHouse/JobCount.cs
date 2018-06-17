using System;

namespace C2
{
    public class JobCount
    {
        private int id;
        private int count;
        private DateTime dateFrom;
        private DateTime dateTo;

        public int Id
        {
            get { return this.id; }
        }

        public int Count
        {
            get { return this.count; }
            set { this.count = value; }
        }

        public DateTime DateFrom
        {
            get { return this.dateFrom; }
            set { this.dateFrom = value; }
        }

        public DateTime DateTo
        {
            get { return this.dateTo; }
            set { this.dateTo = value; }
        }
    }
}

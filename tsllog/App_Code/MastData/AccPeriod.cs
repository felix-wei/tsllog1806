using System;

namespace C2
{
    public class XXAccPeriod
    {
        private int sequenceId;
        private int year;
        private int period;
        private DateTime startDate;
        private DateTime endDate;
        private string closeInd;

        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public int Year
        {
            get { return this.year; }
            set { this.year = value; }
        }

        public int Period
        {
            get { return this.period; }
            set { this.period = value; }
        }

        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }

        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }

        public string CloseInd
        {
            get { return this.closeInd; }
            set { this.closeInd = value; }
        }
    }
}

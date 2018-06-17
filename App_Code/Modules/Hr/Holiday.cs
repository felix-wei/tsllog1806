using System;

namespace C2
{
    public class Holiday
    {
        private int id;
        private decimal day;
        private int fromMonth;
        private int fromDay;
        private int toMonth;
        private int toDay;
        private int person;
        private string department;
        private string remark;

        public int Id
        {
            get { return this.id; }
        }

        public decimal Day
        {
            get { return this.day; }
            set { this.day = value; }
        }

        public int FromMonth
        {
            get { return this.fromMonth; }
            set { this.fromMonth = value; }
        }

        public int FromDay
        {
            get { return this.fromDay; }
            set { this.fromDay = value; }
        }

        public int ToMonth
        {
            get { return this.toMonth; }
            set { this.toMonth = value; }
        }

        public int ToDay
        {
            get { return this.toDay; }
            set { this.toDay = value; }
        }

        public int Person
        {
            get { return this.person; }
            set { this.person = value; }
        }

        public string Department
        {
            get { return this.department; }
            set { this.department = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
    }
}

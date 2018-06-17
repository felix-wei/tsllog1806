using System;

namespace C2
{
    public class HrMastData
    {
        private int id;
        private string code;
        private string description;
        private string type;

        public int Id
        {
            get { return this.id; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        private decimal workDays;
        public decimal WorkDays
        {
            get { return this.workDays; }
            set { this.workDays = value; }
        }
    }
}

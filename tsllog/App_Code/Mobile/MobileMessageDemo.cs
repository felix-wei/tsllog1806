using System;

namespace C2
{
    public class MobileMessageDemo
    {
        private int id;
        private string code;
        private string detail;
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

        public string Detail
        {
            get { return this.detail; }
            set { this.detail = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
    }
}

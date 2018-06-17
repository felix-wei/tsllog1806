using System;

namespace C2
{
    public class PackingLot
    {
        private int id;
        private string code;
        private string name;
        private string address;
        private string lat;
        private string lng;
        private int size20;
        private int size40;
        private string note;
        private string note1;

        public int Id
        {
            get { return this.id; }
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

        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        public string Lat
        {
            get { return this.lat; }
            set { this.lat = value; }
        }

        public string Lng
        {
            get { return this.lng; }
            set { this.lng = value; }
        }

        public int Size20
        {
            get { return this.size20; }
            set { this.size20 = value; }
        }

        public int Size40
        {
            get { return this.size40; }
            set { this.size40 = value; }
        }

        public string Note
        {
            get { return this.note; }
            set { this.note = value; }
        }

        public string Note1
        {
            get { return this.note1; }
            set { this.note1 = value; }
        }
    }
}

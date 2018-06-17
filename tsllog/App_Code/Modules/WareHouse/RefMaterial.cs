using System;

namespace C2
{
    public class RefMaterial
    {
        private int id;
        private string code;
        private string shortCode;
        private string name;
        private string unit;
		private decimal wholeLoose;
        private string looseUnit;
        private string note1;
        private string note2;
        private string note3;
        private string note4;
        private string note5;

        public int Id
        {
            get { return this.id; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string ShortCode
        {
            get { return this.shortCode; }
            set { this.shortCode = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

        public decimal WholeLoose
        {
            get { return this.wholeLoose; }
            set { this.wholeLoose = value; }
        }

        public string LooseUnit
        {
            get { return this.looseUnit; }
            set { this.looseUnit = value; }
        }
		
        public string Note1
        {
            get { return this.note1; }
            set { this.note1 = value; }
        }

        public string Note2
        {
            get { return this.note2; }
            set { this.note2 = value; }
        }

        public string Note3
        {
            get { return this.note3; }
            set { this.note3 = value; }
        }

        public string Note4
        {
            get { return this.note4; }
            set { this.note4 = value; }
        }

        public string Note5
        {
            get { return this.note5; }
            set { this.note5 = value; }
        }
    }
}

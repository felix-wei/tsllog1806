using System;

namespace C2
{
    public class RefContract
    {
        private int id;
        private int indexNo;
        private string section;
        private string title;
        private string centerContent;
        private string createBy;
        private DateTime createDateTime;
        private string refNo;

        public int Id
        {
            get { return this.id; }
        }

        public int IndexNo
        {
            get { return this.indexNo; }
            set { this.indexNo = value; }
        }

        public string Section
        {
            get { return this.section; }
            set { this.section = value; }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string CenterContent
        {
            get { return this.centerContent; }
            set { this.centerContent = value; }
        }

        public string CreateBy
        {
            get { return this.createBy; }
            set { this.createBy = value; }
        }

        public DateTime CreateDateTime
        {
            get { return this.createDateTime; }
            set { this.createDateTime = value; }
        }

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }
    }
}

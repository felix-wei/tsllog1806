using System;

namespace C2
{
    public class HrFile
    {
        private int id;
        private string fileName;
        private string type;
        private string size;
        private string path;
        private string byWho;
        private string status;
        private string docType;
        private string docNo;
        private string categoryCode;
        private string folderCode;

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string FileName
        {
            get { return this.fileName; }
            set { this.fileName = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public string Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }

        public string ByWho
        {
            get { return this.byWho; }
            set { this.byWho = value; }
        }

        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public string DocType
        {
            get { return this.docType; }
            set { this.docType = value; }
        }

        public string DocNo
        {
            get { return this.docNo; }
            set { this.docNo = value; }
        }

        public string CategoryCode
        {
            get { return this.categoryCode; }
            set { this.categoryCode = value; }
        }

        public string FolderCode
        {
            get { return this.folderCode; }
            set { this.folderCode = value; }
        }
    }
}

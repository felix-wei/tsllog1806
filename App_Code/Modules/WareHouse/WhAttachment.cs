using System;

namespace C2
{
    public class WhAttachment
    {
        private int id;
        private string jobType;
        private string refNo;
        private string fileType;
        private string fileName;
        private string filePath;
        private string fileNote;
        private DateTime fileDate;
        private string fileSize;
        private string fileStatus;
        private string createdBy;
        private DateTime createDateTime;

        public int Id
        {
            get { return this.id; }
        }

        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }

        public string FileType
        {
            get { return this.fileType; }
            set { this.fileType = value; }
        }

        public string FileName
        {
            get { return this.fileName; }
            set { this.fileName = value; }
        }

        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }

        public string FileNote
        {
            get { return this.fileNote; }
            set { this.fileNote = value; }
        }

        public DateTime FileDate
        {
            get { return this.fileDate; }
            set { this.fileDate = value; }
        }

        public string FileSize
        {
            get { return this.fileSize; }
            set { this.fileSize = value; }
        }

        public string FileStatus
        {
            get { return this.fileStatus; }
            set { this.fileStatus = value; }
        }

        public string CreatedBy
        {
            get { return this.createdBy; }
            set { this.createdBy = value; }
        }

        public DateTime CreateDateTime
        {
            get { return this.createDateTime; }
            set { this.createDateTime = value; }
        }

        private string containerNo;
        public string ContainerNo 
        {
            get { return this.containerNo; }
            set { this.containerNo = value; }
        }
        public string Path
        {
            get
            {
                if (filePath != null && filePath.Length > 0)
                    return "/Photos/" + this.filePath.Replace("\\", "/");
                return "";
            }
        }
        public string ImgPath
        {
            get
            {
                if (filePath != null && filePath.Length > 0)
                {
                    if (this.fileType != null && fileType.ToLower() == "image")
                        return "/Photos/" + this.filePath.Replace("\\", "/");
                    else
                        return "/images/File.jpg";
                }
                return "";
            }
        }
        public string StatusCode
        {
            get
            {
                string s = "USE";
                string sql = "";
                if (jobType == "C")
                {
                    sql = "select StatusCode from wh_Contract where ContractNo='" + this.refNo + "'";
                }
                if (jobType == "DO")
                {
                    sql = "select StatusCode from Wh_DO where DoNo='" + this.refNo + "'";
                }
                if (jobType == "PO")
                {
                    sql = "select StatusCode from wh_PO where PoNo='" + this.refNo + "'";
                }
                if (jobType == "POR")
                {
                    sql = "select StatusCode from wh_POReceipt where ReceiptNo='" + this.refNo + "'";
                }
                if (jobType == "SO")
                {
                    sql = "select StatusCode from wh_So where SoNo='" + this.refNo + "'";
                }
                if (jobType == "SOR")
                {
                    sql = "select StatusCode from wh_SORelease where ReleaseNo='" + this.refNo + "'";
                }
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
    }
}

using System;

namespace C2
{
    public class SeaAttachment
    {
        private int sequenceId;
        private string jobClass;
        private string refNo;
        private string jobNo;
        private string containerNo;
        private string fileType;
        private string fileName;
        private string filePath;
        private string fileNote;
        private DateTime fileDate;
        private string fileSize;
        private string fileStatus;
        private string userId;
        private DateTime entryDate;

        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public string JobClass
        {
            get { return this.jobClass; }
            set { this.jobClass = value; }
        }

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public string ContainerNo
        {
            get { return this.containerNo; }
            set { this.containerNo = value; }
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
        public string RefStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaExportRef where RefNo='" + this.refNo + "'";
                if (jobClass == "I")
                    sql = "select StatusCode from SeaImportRef where RefNo='" + this.refNo + "'";

                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        public string JobStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaImport where JobNo='" + this.refNo + "'";
                if (jobClass == "E")
                    sql = "select StatusCode from SeaExport where JobNo='" + this.refNo + "'";

                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }

        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

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

        public string UpdateBy
        {
            get { return this.updateBy; }
            set { this.updateBy = value; }
        }

        public DateTime UpdateDateTime
        {
            get { return this.updateDateTime; }
            set { this.updateDateTime = value; }
        }
    }
}

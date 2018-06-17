using System;
using System.Data;

namespace C2
{
    public class CtmAttachment
    {
        private int id;
        private string jobType;
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
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private int tripId;

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
            get
            {
                string res = this.fileNote;

                return res;
            }
            set { this.fileNote = value; }
        }

        public string FileNote1
        {
            get
            {
                string res = this.fileNote;
                if (this.tripId != null && this.tripId > 0)
                {
                    string sql = string.Format(@"select ContainerNo,DriverCode,ChessisCode from ctm_jobdet2 where Id={0}", this.tripId);
                    DataTable dt = ConnectSql.GetTab(sql);
                    if (dt.Rows.Count > 0)
                    {
                        string Cont = SafeValue.SafeString(dt.Rows[0]["ContainerNo"]);
                        string Driver = SafeValue.SafeString(dt.Rows[0]["DriverCode"]);
                        string Trailer = SafeValue.SafeString(dt.Rows[0]["ChessisCode"]);
                        res += " (Container:" + Cont + ",Driver:" + Driver + ",Trailer:" + Trailer + ")";
                    }
                }

                return res;
            }
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

        private int costId;
        public int CostId
        {
            get { return this.costId; }
            set { this.costId = value; }
        }
        public string attachType;
        public string AttachType
        {
            get { return this.attachType; }
            set { this.attachType = value; }
        }
        private string attachStatus;
        public string AttachStatus
        {
            get { return this.attachStatus; }
            set { this.attachStatus = value; }
        }
        private int employee;
        public int Employee
        {
            get { return this.employee; }
            set { this.employee = value; }
        }
        private string typeCode;
        public string TypeCode
        {
            get { return this.typeCode; }
            set { this.typeCode = value; }
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
                    if (this.filePath.ToLower().Trim().StartsWith("http://"))
                    {
                        return this.filePath;
                    }
                    if (this.fileType != null &&( fileType.ToLower() == "image" || fileType.ToLower() == "signature"))
                        return "/Photos/" + this.filePath.Replace("\\", "/");
                    else
                    {
                        if (this.fileType != null && (fileType.ToLower() == "mobileimg" ))
                        {
                            return "/Mobile/" + this.filePath.Replace("\\", "/");
                        }
                        else
                        {
                            return "/images/File.jpg";
                        }
                    }
                }
                return "";
            }
        }
        public string JobStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from CTM_Job where jobNo='" + this.refNo + "'";

                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        public string TripIndex
        {
            get {
                string res = "";
                if (this.tripId > 0)
                {
                    string sql = string.Format(@"select TripIndex from ctm_jobdet2 where Id={0}", this.tripId);
                    res = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
                }
                return res;
            }
        }
        public int TripId
        {
            get { return this.tripId; }
            set { this.tripId = value; }
        }
    }
}

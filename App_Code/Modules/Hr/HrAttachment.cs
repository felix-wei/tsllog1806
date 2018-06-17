using System;

namespace C2
{
	public class HrAttachment
	{
		private int id;
		private int person;
		private string fileType;
		private string fileName;
		private string filePath;
		private string fileNote;
		private DateTime fileDate;
		private string fileSize;
		private string fileStatus;
		private string createBy;
		private DateTime createDateTime;

		public int Id
		{
			get { return this.id; }
		}

		public int Person
		{
			get { return this.person; }
            set { this.person = value; }
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
                        return "/Photos/Hr/" + this.filePath.Replace("\\", "/");
                    else
                        return "/images/File.jpg";
                }
                return "";
            }
        }

        private string category;
        public string Category
        {
            get { return this.category; }
            set { this.category = value; }
        }

    }
}

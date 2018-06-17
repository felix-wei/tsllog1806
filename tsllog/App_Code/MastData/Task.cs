using System;

namespace C2
{
    public class Tasks
    {
        private int id;
        private string taskName;
        private string description;
        private string taskType;
        private DateTime dueDate;
        private string assignTo;
        private string partyId;
        private string statusCode;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

        public int Id
        {
            get { return this.id; }
        }

        public string TaskName
        {
            get { return this.taskName; }
            set { this.taskName = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public string TaskType
        {
            get { return this.taskType; }
            set { this.taskType = value; }
        }

        public DateTime DueDate
        {
            get { return this.dueDate; }
            set { this.dueDate = value; }
        }

        public string AssignTo
        {
            get { return this.assignTo; }
            set { this.assignTo = value; }
        }

        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
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
    }
}

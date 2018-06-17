using System;
namespace C2
{
    public class User
    {
        private int sequenceId;
        private string name;
        private string pwd;
        private string email;
        private string tel;
        private string role;
        private bool isActive;
        private string custId;
        private string port;
        private string eventDepot;

        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Pwd
        {
            get { return this.pwd; }
            set { this.pwd = value; }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public string Tel
        {
            get { return this.tel; }
            set { this.tel = value; }
        }

        public string Role
        {
            get { return this.role; }
            set { this.role = value; }
        }

        public bool IsActive
        {
            get { return this.isActive; }
            set { this.isActive = value; }
        }

        public string CustId
        {
            get { return this.custId; }
            set { this.custId = value; }
        }

        public string Port
        {
            get { return this.port; }
            set { this.port = value; }
        }

        public string EventDepot
        {
            get { return this.eventDepot; }
            set { this.eventDepot = value; }
        }
    }

    public class Role
    {
        private int sequenceId;
        private string code;
        private string description;

        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
    }
}

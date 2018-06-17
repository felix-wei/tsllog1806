using System;

namespace C2
{
    public class MobileControl
    {
        private int id;
        private string code;
        private string type;
        private bool isActive;
        private string roleName;

        public int Id
        {
            get { return this.id; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public bool IsActive
        {
            get { return this.isActive; }
            set { this.isActive = value; }
        }

        public string RoleName
        {
            get { return this.roleName; }
            set { this.roleName = value; }
        }
    }
}

using System;

namespace C2
{
    public class XXChgCode
    {
        private int sequenceId;
        private string chgcodeId;
        private string chgcodeDe;
        private string chgUnit;
        private string gstTypeId;
        private decimal gstP;
        private string chgTypeId;
        private string arCode;
        private string apCode;
        private string impExpInd;
        private decimal sortIndex;

        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public string ChgcodeId
        {
            get { return this.chgcodeId; }
            set { this.chgcodeId = value; }
        }

        public string ChgcodeDe
        {
            get { return this.chgcodeDe; }
            set { this.chgcodeDe = value; }
        }

        public string ChgUnit
        {
            get { return this.chgUnit; }
            set { this.chgUnit = value; }
        }

        public string GstTypeId
        {
            get { return this.gstTypeId; }
            set { this.gstTypeId = value; }
        }

        public decimal GstP
        {
            get { return this.gstP; }
            set { this.gstP = value; }
        }


        public string ChgTypeId
        {
            get { return this.chgTypeId; }
            set { this.chgTypeId = value; }
        }

        public string ArCode
        {
            get { return this.arCode; }
            set { this.arCode = value; }
        }

        public string ApCode
        {
            get { return this.apCode; }
            set { this.apCode = value; }
        }
        public string ImpExpInd
        {
            get { return this.impExpInd; }
            set { this.impExpInd = value; }
        }
        //add 2014-3-24
        private string arShortcutCode;
        public string ArShortcutCode
        {
            get { return this.arShortcutCode; }
            set { this.arShortcutCode = value; }
        }
        private string apShortcutCode;
        public string ApShortcutCode
        {
            get { return this.apShortcutCode; }
            set { this.apShortcutCode = value; }
        }
        private string quotation_Ind;
        public string Quotation_Ind
        {
            get { return this.quotation_Ind; }
            set { this.quotation_Ind = value; }
        }

        public decimal SortIndex
        {
            get { return this.sortIndex; }
            set { this.sortIndex = value; }
        }
    }
}

using System;

namespace C2
{
    public class bill_line
    {

        private int id;
        private int docId;
        private int docLineNo;
        private string acCode;
        private string acSource;
        private string chgCode;
        private string chgDes1;
        private string chgDes2;
        private string chgDes3;
        private string gstType;
        private decimal qty;
        private decimal price;
        private string unit;
        private string currency;
        private decimal exRate;
        private decimal gst;    
                               //amt =qty*price  :only 2 decimal
        private decimal gstAmt;//gstAmt=amt*gst  2 decimal
        private decimal docAmt;//docAmt=amt+gstAmt  2 decimal
        private decimal locAmt;//locAmt=docAmt*ExRate  2 decimal
                               //lineLocAmt=locAmt*bill.ExRate
        public int Id
        {
            get { return this.id; }
        }
        public string Display
        {
            get
            {
                string flag = "block";
                if (this.docLineNo > 0)
                {
                    string sql = "select ExportInd from bill_doc where Id='" + docId +"'";
                    flag = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
                    if (flag == "Y")
                        flag = "none";
                    else
                    {
                        //sql = "select DocDate from XAArinvoice where SequenceId='" + docId + "'";
                        //DateTime docDate = SafeValue.SafeDate(C2.Manager.ORManager.ExecuteScalar(sql), DateTime.Today);
                        //string user = System.Web.HttpContext.Current.User.Identity.Name;
                        //string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("SELECT Role FROM [User] where Name='" + user + "'"), "Staff").ToLower();
                        //if (role.IndexOf("staff") != -1)
                        //{

                        //    int day = SafeValue.SafeInt(System.Configuration.ConfigurationManager.AppSettings["AccountModifyDay"], 15);
                        //    if ((DateTime.Today - docDate).TotalDays > day)
                        //    {
                        //        flag = "none";
                        //    }
                        //}
                    }
                }
                return flag;
            }
        }
        public string CancelInd
        {
            get {
                string sql = "select CancelInd from bill_doc where Id=" + docId;
                string res = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
                if (res.ToUpper() == "Y")
                {
                    return "none";
                }
                return "block";
            }
        }
        public int DocId
        {
            get { return this.docId; }
            set { this.docId = value; }
        }

        public int DocLineNo
        {
            get { return this.docLineNo; }
            set { this.docLineNo = value; }
        }

        public string ChgCode
        {
            get { return this.chgCode; }
            set { this.chgCode = value; }
        }
        public string AcCode
        {
            get { return this.acCode; }
            set { this.acCode = value; }
        }
        public string AcCodeStr
        {
            get
            {
                string name = "";
                if (this.acCode.Length > 0)
                {
                    name = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select acDesc from XXChartAcc where Code='" + this.acCode + "'"), "");
                }
                return name;
            }
        }

        public string ChgDes1
        {
            get { return this.chgDes1; }
            set { this.chgDes1 = value; }
        }

        public string ChgDes2
        {
            get { return this.chgDes2; }
            set { this.chgDes2 = value; }
        }

        public string ChgDes3
        {
            get { return this.chgDes3; }
            set { this.chgDes3 = value; }
        }


        public string GstType
        {
            get { return this.gstType; }
            set { this.gstType = value; }
        }

        public decimal Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

        public string Currency
        {
            get { return this.currency; }
            set { this.currency = value; }
        }

        public decimal ExRate
        {
            get { return this.exRate; }
            set { this.exRate = value; }
        }

        public decimal Gst
        {
            get { return this.gst; }
            set { this.gst = value; }
        }

        public decimal GstAmt
        {
            get { return this.gstAmt; }
            set { this.gstAmt = value; }
        }

        public decimal DocAmt
        {
            get { return this.docAmt; }
            set { this.docAmt = value; }
        }
        public decimal LocAmt
        {
            get { return this.locAmt; }
            set { this.locAmt = value; }
        }

        public decimal DocAmt1
        {
            get
            {
                if (this.acSource == "CR" && (this.docType == "IV"||this.docType=="DN"))
                    return this.docAmt;
                else if (this.acSource == "DB" && this.docType == "CN")
                    return this.docAmt;
                else return -this.docAmt;
            }
        }
        public decimal LocAmt1
        {
            get
            {
                if (this.acSource == "CR" && (this.docType == "IV" || this.docType == "DN"))
                    return this.locAmt;
                else if (this.acSource == "DB" && this.docType == "CN")
                    return this.locAmt;
                else return -this.locAmt;
            }
        }
        public string AcSource
        {
            get { return this.acSource; }
            set { this.acSource = value; }
        }
        private string docNo;
        public string DocNo
        {
            get { return this.docNo; }
            set { this.docNo = value; }
        }
        private string docType;
        public string DocType
        {
            get { return this.docType; }
            set { this.docType = value; }
        }

        private decimal lineLocAmt;
        public decimal LineLocAmt
        {
            get { return this.lineLocAmt; }
            set { this.lineLocAmt = value; }
        }
        private string mastRefNo;
        private string jobRefNo;
        private string mastType;
        private string splitType;
        public string MastRefNo
        {
            get { return this.mastRefNo; }
            set { this.mastRefNo = value; }
        }

        public string JobRefNo
        {
            get { return this.jobRefNo; }
            set { this.jobRefNo = value; }
        }

        public string MastType
        {
            get { return this.mastType; }
            set { this.mastType = value; }
        }
        public string SplitType
        {
            get { return this.splitType; }
            set { this.splitType = value; }
        }
        private decimal otherAmt;
        public decimal OtherAmt
        {
            get { return this.otherAmt; }
            set { this.otherAmt = value; }
        }
    }
}

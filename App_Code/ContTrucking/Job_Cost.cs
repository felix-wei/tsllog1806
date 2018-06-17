using System;

namespace C2
{
    public class Job_Cost
    {
        private int id;
        private int lineId;
        private string lineType;
        private string lineStatus;
        private string jobNo;
        private string jobType;
        private string refNo;
        private string contNo;
        private string contType;
        private string tripNo;
        private string vendorId;
        private string chgCode;
        private string chgCodeDe;
        private string remark;
        private decimal qty;
        private decimal price;
        private string unit;
        private string currencyId;
        private decimal exRate;
        private decimal docAmt;
        private decimal locAmt;
        private int companyId;
        private string rowStatus;
        private string rowCreateUser;
        private DateTime rowCreateTime;
        private string rowUpdateUser;
        private DateTime rowUpdateTime;
        private string lineSource;
        private string billScope;
        private string billType;
        private string billClass;


        public int Id
        {
            get { return this.id; }
        }

        public int LineId
        {
            get { return this.lineId; }
            set { this.lineId = value; }
        }

        public string LineType
        {
            get { return this.lineType; }
            set { this.lineType = value; }
        }

        public string LineStatus
        {
            get { return this.lineStatus; }
            set { this.lineStatus = value; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
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

        public string ContNo
        {
            get { return this.contNo; }
            set { this.contNo = value; }
        }
        public string ContType
        {
            get { return this.contType; }
            set { this.contType = value; }
        }
        public string TripNo
        {
            get { return this.tripNo; }
            set { this.tripNo = value; }
        }

        public string VendorId
        {
            get { return this.vendorId; }
            set { this.vendorId = value; }
        }

        public string ChgCode
        {
            get { return this.chgCode; }
            set { this.chgCode = value; }
        }

        public string ChgCodeDe
        {
            get { return this.chgCodeDe; }
            set { this.chgCodeDe = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
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

        public string CurrencyId
        {
            get { return this.currencyId; }
            set { this.currencyId = value; }
        }

        public decimal ExRate
        {
            get { return this.exRate; }
            set { this.exRate = value; }
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

        public int CompanyId
        {
            get { return this.companyId; }
            set { this.companyId = value; }
        }

        public string RowStatus
        {
            get { return this.rowStatus; }
            set { this.rowStatus = value; }
        }

        public string RowCreateUser
        {
            get { return this.rowCreateUser; }
            set { this.rowCreateUser = value; }
        }

        public DateTime RowCreateTime
        {
            get { return this.rowCreateTime; }
            set { this.rowCreateTime = value; }
        }

        public string RowUpdateUser
        {
            get { return this.rowUpdateUser; }
            set { this.rowUpdateUser = value; }
        }

        public DateTime RowUpdateTime
        {
            get { return this.rowUpdateTime; }
            set { this.rowUpdateTime = value; }
        }
        public string LineSource
        {
            get { return this.lineSource; }
            set { this.lineSource = value; }
        }

        public string BillScope
        {
            get { return this.billScope; }
            set { this.billScope = value; }
        }

        public string BillType
        {
            get { return this.billType; }
            set { this.billType = value; }
        }

        public string BillClass
        {
            get { return this.billClass; }
            set { this.billClass = value; }
        }
        
        public string JobStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from CTM_Job where JobNo='" + this.refNo + "'";

                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        public string InvoiceNo
        {
            get {
                string sql = string.Format("select DocNo from XAArInvoiceDet where SequenceId={0}",this.lineId);
                return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql),"");
            }
        }
        public DateTime InvoiceDate
        {
            get
            {
                string sql = string.Format("select mast.DocDate from XAArInvoiceDet det inner join XAArInvoice mast on det.DocId=mast.SequenceId where det.SequenceId={0}", this.lineId);
                return SafeValue.SafeDate(C2.Manager.ORManager.ExecuteScalar(sql),DateTime.Today);
            }
        }
        public decimal InvoiceAmt
        {
            get
            {
                string sql = string.Format("select LocAmt from XAArInvoiceDet where SequenceId={0}", this.lineId);
                return SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql),0);
            }
        }
        public string InvoiceGstType 
        {
            get
            {
                string sql = string.Format("select GstType from XAArInvoiceDet where SequenceId={0}", this.lineId);
                return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql),"");
            }
        }

        private decimal gst;
        private decimal gstAmt;
        private string gstType;
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
        public string GstType
        {
            get { return this.gstType; }
            set { this.gstType = value; }
        }
        private int lineIndex;
        public int LineIndex
        {
            get { return this.lineIndex; }
            set { this.lineIndex = value; }
        }
        private string subJobNo;
        public string SubJobNo
        {
            get { return this.subJobNo; }
            set { this.subJobNo = value; }
        }

        private string pay_Ind;
        public string Pay_Ind
        {
            get { return this.pay_Ind; }
            set { this.pay_Ind = value; }
        }
        public bool Is_Pay {
            get {
                bool action = false;
                if (this.pay_Ind == "Y")
                    action = true;
                return action;
            }
        }
        private string receiptNo;
        public string ReceiptNo
        {
            get { return this.receiptNo; }
            set { this.receiptNo = value; }
        }
        private string receiptRemark;
        public string ReceiptRemark
        {
            get { return this.receiptRemark; }
            set { this.receiptRemark = value; }
        }
		private string vehicleNo;
        public string VehicleNo
        {
            get { return this.vehicleNo; }
            set { this.vehicleNo = value; }
        }
        private decimal first30Days;
        private decimal after30Days;
        public decimal First30Days
        {
            get { return this.first30Days; }
            set { this.first30Days = value; }
        }
        public decimal After30Days
        {
            get { return this.after30Days; }
            set { this.after30Days = value; }
        }
        private string relaId;
        private DateTime billStartDate;
        public string RelaId
        {
            get { return this.relaId; }
            set { this.relaId = value; }
        }
        public DateTime BillStartDate
        {
            get { return this.billStartDate; }
            set { this.billStartDate = value; }
        }
        private DateTime eventDate;
        private string eventType;
        private DateTime expiryDate;
        private string tripType;
        public DateTime EventDate
        {
            get { return this.eventDate; }
            set { this.eventDate = value; }
        }

        public string EventType
        {
            get { return this.eventType; }
            set { this.eventType = value; }
        }

        public DateTime ExpiryDate
        {
            get { return this.expiryDate; }
            set { this.expiryDate = value; }
        }
        public string TripType
        {
            get { return this.tripType; }
            set { this.tripType = value; }
        }
        private string driverCode;
        public string DriverCode
        {
            get { return this.driverCode; }
            set { this.driverCode = value; }
        }
        private string groupBy;
        public string GroupBy
        {
            get { return this.groupBy; }
            set { this.groupBy = value; }
        }


        private string notBuildCustomer;
        public string NotBuildCustomer
        {
            get { return this.notBuildCustomer; }
            set { this.notBuildCustomer = value; }
        }
    }
}

using System;

namespace C2
{
    public class SeaCertificate:BaseObject
    {
        private int id;
        private string refNo;
        private string jobNo;
        private string refType;
        private string gstPermitNo;
        private decimal gstPaidAmt;
        private string handingAgent;
        private DateTime cerDate;
        private string currency;
        private decimal exRate;
        private string serialNo;

        public int Id
        {
            get { return this.id; }
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

        public string RefType
        {
            get { return this.refType; }
            set { this.refType = value; }
        }

        public string GstPermitNo
        {
            get { return this.gstPermitNo; }
            set { this.gstPermitNo = value; }
        }

        public decimal GstPaidAmt
        {
            get { return this.gstPaidAmt; }
            set { this.gstPaidAmt = value; }
        }

        public string HandingAgent
        {
            get { return this.handingAgent; }
            set { this.handingAgent = value; }
        }

        public DateTime CerDate
        {
            get { return this.cerDate; }
            set { this.cerDate = value; }
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

        public string SerialNo
        {
            get { return this.serialNo; }
            set { this.serialNo = value; }
        }
        public string PartyName
        {
            get
            {
                string name = "";
                if (SafeValue.SafeString(handingAgent).Length > 0)
                {
                    name = EzshipHelper.GetPartyName(handingAgent);
                }
                else
                    name = EzshipHelper.GetPartyName(handingAgent);


                return name;
            }
        }
        public string JobStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaExport where JobNo='" + this.jobNo + "'";
                if (refType == "SI")
                    sql = "select StatusCode from SeaImport where JobNo='" + this.jobNo + "'";
                else if (refType == "AE" || refType == "AI")
                    sql = "select StatusCode from air_job where JobNo='" + this.jobNo + "'";

                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");

                return s;
            }
        }

        public string RefStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaImportRef where RefNo='" + this.refNo + "'";
                if (refType == "AE" || refType == "AI")
                    sql = "select StatusCode from air_ref where RefNo='" + this.refNo + "'";
                else if(refType=="SE")
                    sql = "select StatusCode from SeaExportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
    }
}

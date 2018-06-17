using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace C2
{
    public class CtmJob: ICloneable
    {
        private int id;
        private string jobNo;
        private DateTime jobDate;
        private string partyId;
        private string vessel;
        private string voyage;
        private string pol;
        private string pod;
        private DateTime etaDate;
        private string etaTime;
        private DateTime etdDate;
        private string etdTime;
        private string carrierId;
        private string carrierBlNo;
        private string carrierBkgNo;
        private string pickupFrom;
        private string deliveryTo;
        private string remark;
        private string specialInstruction;
        private string statusCode;
        private string terminalcode;

        private string jobType;
        private string clientId;
        private string clientRefNo;
        private string haulierId;

        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private DateTime codDate;
        private string codTime;
        private string warehouseAddress;
        private string permitNo;

        private string yardRef;
        private string portnetRef;
        private string operatorCode;

        private string emailAddress;
        private string contractor;


        public int Id
        {
            get { return this.id; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public DateTime JobDate
        {
            get { return this.jobDate; }
            set { this.jobDate = value; }
        }

        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string Vessel
        {
            get { return this.vessel; }
            set { this.vessel = value; }
        }

        public string Voyage
        {
            get { return this.voyage; }
            set { this.voyage = value; }
        }

        public string Pol
        {
            get { return this.pol; }
            set { this.pol = value; }
        }

        public string Pod
        {
            get { return this.pod; }
            set { this.pod = value; }
        }

        public DateTime EtaDate
        {
            get { return this.etaDate; }
            set { this.etaDate = value; }
        }

        public string EtaTime
        {
            get { return this.etaTime; }
            set { this.etaTime = value; }
        }

        public DateTime EtdDate
        {
            get { return this.etdDate; }
            set { this.etdDate = value; }
        }

        public string EtdTime
        {
            get { return this.etdTime; }
            set { this.etdTime = value; }
        }

        public string CarrierId
        {
            get { return this.carrierId; }
            set { this.carrierId = value; }
        }

        public string CarrierBlNo
        {
            get { return this.carrierBlNo; }
            set { this.carrierBlNo = value; }
        }

        public string CarrierBkgNo
        {
            get { return this.carrierBkgNo; }
            set { this.carrierBkgNo = value; }
        }

        public string PickupFrom
        {
            get { return this.pickupFrom; }
            set { this.pickupFrom = value; }
        }

        public string DeliveryTo
        {
            get { return this.deliveryTo; }
            set { this.deliveryTo = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public string SpecialInstruction
        {
            get { return this.specialInstruction; }
            set { this.specialInstruction = value; }
        }

        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }

        public string Terminalcode
        {
            get { return this.terminalcode; }
            set { this.terminalcode = value; }
        }

        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }
        public string ClientId
        {
            get { return this.clientId; }
            set { this.clientId = value; }
        }
        public string ClientRefNo
        {
            get { return this.clientRefNo; }
            set { this.clientRefNo = value; }
        }
        public string HaulierId
        {
            get { return this.haulierId; }
            set { this.haulierId = value; }
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

        public DateTime CodDate
        {
            get { return this.codDate; }
            set { this.codDate = value; }
        }

        public string CodTime
        {
            get { return this.codTime; }
            set { this.codTime = value; }
        }

        public string WarehouseAddress
        {
            get { return this.warehouseAddress; }
            set { this.warehouseAddress = value; }
        }


        public string IsImport
        {
            get
            {
                string re = "";
                string temp = this.jobType;
                if (temp == "KD-IMP" || temp == "FCL-IMP")
                {
                    re = "none";
                }
                return re;
            }
        }

        public string PermitNo
        {
            get { return this.permitNo; }
            set { this.permitNo = value; }
        }

        public string YardRef
        {
            get { return this.yardRef; }
            set { this.yardRef = value; }
        }

        public string PortnetRef
        {
            get { return this.portnetRef; }
            set { this.portnetRef = value; }
        }

        public string OperatorCode
        {
            get { return this.operatorCode; }
            set { this.operatorCode = value; }
        }
        private string isTrucking;
        public string IsTrucking
        {
            get { return this.isTrucking; }
            set { this.isTrucking = value; }
        }
        private string isWarehouse;
        public string IsWarehouse
        {
            get { return this.isWarehouse; }
            set { this.isWarehouse = value; }
        }
        private string isFreight;
        public string IsFreight
        {
            get { return this.isFreight; }
            set { this.isFreight = value; }
        }
        private string isLocal;
        public string IsLocal
        {
            get { return this.isLocal; }
            set { this.isLocal = value; }
        }
        private string isAdhoc;
        public string IsAdhoc
        {
            get { return this.isAdhoc; }
            set { this.isAdhoc = value; }
        }
        private string isOthers;
        public string IsOthers
        {
            get { return this.isOthers; }
            set { this.isOthers = value; }
        }
        public string EmailAddress
        {
            get { return this.emailAddress; }
            set { this.emailAddress = value; }
        }
        public string Contractor
        {
            get { return this.contractor; }
            set { this.contractor = value; }
        }

        private string subClientId;
        public string SubClientId
        {
            get { return this.subClientId; }
            set { this.subClientId = value; }
        }
        private string wareHouseCode;
        public string WareHouseCode
        {
            get { return this.wareHouseCode; }
            set { this.wareHouseCode = value; }
        }
        private string whPermitNo;
        private DateTime permitDate;
        private string permitBy;
        private string partyInvNo;
        private string incoTerm;
        private decimal gstAmt;
        private string paymentRemark;
        private string paymentStatus;
        public string WhPermitNo
        {
            get { return this.whPermitNo; }
            set { this.whPermitNo = value; }
        }

        public DateTime PermitDate
        {
            get { return this.permitDate; }
            set { this.permitDate = value; }
        }

        public string PermitBy
        {
            get { return this.permitBy; }
            set { this.permitBy = value; }
        }

        public string PartyInvNo
        {
            get { return this.partyInvNo; }
            set { this.partyInvNo = value; }
        }

        public string IncoTerm
        {
            get { return this.incoTerm; }
            set { this.incoTerm = value; }
        }

        public decimal GstAmt
        {
            get { return this.gstAmt; }
            set { this.gstAmt = value; }
        }

        public string PaymentRemark
        {
            get { return this.paymentRemark; }
            set { this.paymentRemark = value; }
        }

        public string PaymentStatus
        {
            get { return this.paymentStatus; }
            set { this.paymentStatus = value; }
        }
        private string clientContact;
        private string jobStatus;
        private string quoteNo;
        private string quoteStatus;
        private string quoteRemark;
        private DateTime quoteDate;
        public string ClientContact
        {
            get { return this.clientContact; }
            set { this.clientContact = value; }
        }
        public string JobStatus
        {
            get { return this.jobStatus; }
            set { this.jobStatus = value; }
        }

        public string QuoteNo
        {
            get { return this.quoteNo; }
            set { this.quoteNo = value; }
        }

        public string QuoteStatus
        {
            get { return this.quoteStatus; }
            set { this.quoteStatus = value; }
        }

        public string QuoteRemark
        {
            get { return this.quoteRemark; }
            set { this.quoteRemark = value; }
        }

        public DateTime QuoteDate
        {
            get { return this.quoteDate; }
            set { this.quoteDate = value; }
        }
        private string jobDes;
        public string JobDes
        {
            get { return this.jobDes; }
            set { this.jobDes = value; }
        }
        private string terminalRemark;
        public string TerminalRemark
        {
            get { return this.terminalRemark; }
            set { this.terminalRemark = value; }
        }
        private string internalRemark;
        public string InternalRemark
        {
            get { return this.internalRemark; }
            set { this.internalRemark = value; }
        }
        private string lumSumRemark;
        public string LumSumRemark
        {
            get { return this.lumSumRemark; }
            set { this.lumSumRemark = value; }
        }
        private string additionalRemark;
        public string AdditionalRemark
        {
            get { return this.additionalRemark; }
            set { this.additionalRemark = value; }
        }

        private string billingType;
        public string BillingType
        {
            get { return this.billingType; }
            set { this.billingType = value; }
        }
        public string ContNo {
            get {
                return "";
            }
        }
        public string SealNo
        {
            get
            {
                return "";
            }
        }

        public int Ft20 {
            get {
                return 0;
            }
        }
        public int Ft40
        {
            get
            {
                return 0;
            }
        }
        public int Ft45
        {
            get
            {
                return 0;
            }
        }
        public string FtType
        {
            get { 
                return "";
            
            }
        }

        private string tallyDone;
        public string TallyDone
        {
            get { return this.tallyDone; }
            set { this.tallyDone = value; }
        }
        private string issuedBy;
        public string IssuedBy
        {
            get { return this.issuedBy; }
            set { this.issuedBy = value; }
        }

        private DateTime permitExpiry;
        public DateTime PermitExpiry 
        {
            get { return this.permitExpiry; }
            set { this.permitExpiry = value; }
        }

        private string orderType;
        public string OrderType
        {
            get { return this.orderType; }
            set { this.orderType = value; }
        }

        private string agentId;
        public string AgentId
        {
            get { return this.agentId; }
            set { this.agentId = value; }
        }
        private string depotCode;
        public string DepotCode
        {
            get { return this.depotCode; }
            set { this.depotCode = value; }
        }
        private string masterJobNo;
        public string MasterJobNo
        {
            get { return this.masterJobNo; }
            set { this.masterJobNo = value; }
        }
        private string escort_Ind;
        public string Escort_Ind
        {
            get { return this.escort_Ind; }
            set { this.escort_Ind = value; }
        }

        private string escort_Remark;
        public string Escort_Remark
        {
            get { return this.escort_Remark; }
            set { this.escort_Remark = value; }
        }

        private string fumigation_Ind;
        public string Fumigation_Ind{
         get { return this.fumigation_Ind; }
            set { this.fumigation_Ind = value; }
        }
        private string fumigationRemark;
        public string FumigationRemark
        {
            get { return this.fumigationRemark; }
            set { this.fumigationRemark = value; }
        }
        private string fumigationStatus;
        public string FumigationStatus
        {
            get { return this.fumigationStatus; }
            set { this.fumigationStatus = value; }
        }
        private string stamping_Ind;
        public string Stamping_Ind
        {
            get { return this.stamping_Ind; }
            set { this.stamping_Ind = value; }
        }
        private string stampingRemark;
        public string StampingRemark
        {
            get { return this.stampingRemark; }
            set { this.stampingRemark = value; }
        }
        private string stampingStatus;
        public string StampingStatus
        {
            get { return this.stampingStatus; }
            set { this.stampingStatus = value; }
        }
        private string billingRefNo;
        public string BillingRefNo
        {
            get { return this.billingRefNo; }
            set { this.billingRefNo = value; }
        }
        private string showInvoice_Ind;
        public string ShowInvoice_Ind
        {
            get { return this.showInvoice_Ind; }
            set { this.showInvoice_Ind = value; }
        }
        private DateTime returnLastDate;
        public DateTime ReturnLastDate
        {
            get { return this.returnLastDate; }
            set { this.returnLastDate = value; }
        }
        
        private string departmentId;
        public string DepartmentId
        {
            get { return this.departmentId; }
            set { this.departmentId = value; }
        }


        private string releaseToHaulierRemark;

        public string ReleaseToHaulierRemark
        {
            get { return this.releaseToHaulierRemark; }
            set { this.releaseToHaulierRemark = value; }
        }




        public static void jobCost_Create(string JobNo, string JobType, string ClientId, string BillClass)
        {
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            string sql = string.Format(@"select ChgCode,ChgCodeDes,Qty,Price,BillClass,BillScope,CurrencyId,ExRate 
from job_rate where ClientId=@ClientId and BillScope=@LineType and JobType=@JobType and BillClass=@BillClass and LineStatus=@LineStatus");
            list.Add(new ConnectSql_mb.cmdParameters("@ClientId", ClientId, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@LineType", "JOB", SqlDbType.NVarChar, 30));
            list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
            list.Add(new ConnectSql_mb.cmdParameters("@LineStatus", "N", SqlDbType.NVarChar, 30));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count == 0)
            {
                #region No JobType
                list = new List<ConnectSql_mb.cmdParameters>();
                sql = string.Format(@"select ChgCode,ChgCodeDes,Qty,Price,BillClass,BillScope,CurrencyId,ExRate 
from job_rate where ClientId=@ClientId and BillScope=@LineType and BillClass=@BillClass and LineStatus=@LineStatus");
                list.Add(new ConnectSql_mb.cmdParameters("@ClientId", ClientId, SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "JOB", SqlDbType.NVarChar, 30));
                list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                list.Add(new ConnectSql_mb.cmdParameters("@LineStatus", "N", SqlDbType.NVarChar, 30));
                dt = ConnectSql_mb.GetDataTable(sql, list);
                #endregion
                if (dt.Rows.Count == 0)
                {
                    #region Client=CASH
                    list = new List<ConnectSql_mb.cmdParameters>();
                    sql = string.Format(@"select ChgCode,ChgCodeDes,Qty,Price,BillClass,BillScope,CurrencyId,ExRate 
from job_rate where ClientId=@ClientId and BillScope=@LineType and BillClass=@BillClass and LineStatus=@LineStatus");
                    list.Add(new ConnectSql_mb.cmdParameters("@ClientId", "CASH", SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@LineType", "JOB", SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@LineStatus", "N", SqlDbType.NVarChar, 30));
                    dt = ConnectSql_mb.GetDataTable(sql, list);
                    #endregion
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string chgCode = SafeValue.SafeString(dt.Rows[i]["ChgCode"]);
                string chgCodeDes = SafeValue.SafeString(dt.Rows[i]["ChgCodeDes"]);
                decimal price = SafeValue.SafeDecimal(dt.Rows[i]["Price"]);
                decimal qty = SafeValue.SafeDecimal(dt.Rows[i]["Qty"]);
                string scope = SafeValue.SafeString(dt.Rows[i]["BillScope"]);
                string billClass = SafeValue.SafeString(dt.Rows[i]["BillClass"]);

                list = new List<ConnectSql_mb.cmdParameters>();
                sql = string.Format(@"select Id from job_cost where LineType=@LineType and JobNo=@JobNo and BillClass=@BillClass and JobType=@JobType and LineSource=@LineSource and ChgCode=@ChgCode");
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "JOB", SqlDbType.NVarChar, 30));
                list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                list.Add(new ConnectSql_mb.cmdParameters("@LineSource", "S", SqlDbType.NVarChar, 10));
                list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", chgCode, SqlDbType.NVarChar, 100));
                dt = ConnectSql_mb.GetDataTable(sql, list);
                if (dt.Rows.Count <= 0)
                {
                    string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where ChgcodeId='{0}'", chgCode);
                    DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
                    decimal gst = 0;
                    string gstType = "";
                    string chgTypeId = "";
                    if (dt_chgCode.Rows.Count > 0)
                    {
                        gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
                        gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
                        chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
                    }
                    sql = string.Format(@"insert into job_cost (JobNo,JobType,ChgCode,ChgCodeDes,Price,Qty,CurrencyId,ExRate,LocAmt,LineSource,LineType,BillClass,ContNo,ContType,RowCreateUser,RowCreateTime,RowUpdateUser,RowUpdateTime,LineId,DocAmt,Gst,GstAmt,GstType)
values(@JobNo,@JobType,@ChgCode,@ChgCodeDes,@Price,@Qty,@CurrencyId,@ExRate,@LocAmt,@LineSource,@LineType,@BillClass,@ContainerNo,@ContainerType,@UserId,@DateTime,@UserId,@DateTime,0,@DocAmt,@Gst,@GstAmt,@GstType)");
                    list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@LineType", "JOB", SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@LineSource", "S", SqlDbType.NVarChar, 10));
                    list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", "", SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@ContainerType", "", SqlDbType.NVarChar, 100));

                    list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", chgCode, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@ChgCodeDes", chgCodeDes, SqlDbType.NVarChar, 300));
                    list.Add(new ConnectSql_mb.cmdParameters("@Price", price, SqlDbType.Decimal));
                    list.Add(new ConnectSql_mb.cmdParameters("@Qty", 1, SqlDbType.Int));
                    decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
                    decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
                    decimal docAmt = amt ;
                    decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(1, 1), 2);
                    list.Add(new ConnectSql_mb.cmdParameters("@LocAmt", locAmt, SqlDbType.Decimal));
                    list.Add(new ConnectSql_mb.cmdParameters("@CurrencyId", System.Configuration.ConfigurationManager.AppSettings["Currency"], SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@ExRate", 1, SqlDbType.Decimal));
                    string userId = HttpContext.Current.User.Identity.Name;
                    list.Add(new ConnectSql_mb.cmdParameters("@UserId", userId, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@DateTime", DateTime.Now, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@DocAmt", docAmt, SqlDbType.Decimal));
                    list.Add(new ConnectSql_mb.cmdParameters("@Gst", gst, SqlDbType.Decimal));
                    list.Add(new ConnectSql_mb.cmdParameters("@GstAmt", gstAmt, SqlDbType.Decimal));
                    list.Add(new ConnectSql_mb.cmdParameters("@GstType", gstType, SqlDbType.NVarChar));
                    ConnectSql_mb.ExecuteNonQuery(sql, list);
                }

            }

        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

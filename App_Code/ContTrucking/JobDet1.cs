using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace C2
{
    public class CtmJobDet1
    {
        private int id;
        private string jobNo;
        private string containerNo;
        private string containerType;
        private string sealNo;
        private decimal weight;
        private decimal volume;
        private int qty;
        private string packageType;
        private string dgClass;
        private DateTime requestDate;
        private DateTime scheduleDate;
        private string scheduleTime;
        private DateTime cfsInDate;
        private DateTime cfsOutDate;
        private DateTime yardPickupDate;
        private DateTime yardReturnDate;
        private string portnetStatus;
        private string f5Ind;
        private string urgentInd;
        private string remark;

        private DateTime cdtDate;
        private string cdtTime;
        private string terminalLocation;
        private DateTime yardExpiryDate;
        private string yardExpiryTime;
        private string yardAddress;



        private string remark1;
        private string remark2;
        private string permit;
        private string tTTime;
        private string warehouseStatus;
        private string br;
        private string emailInd;

        public int Id
        {
            get { return this.id; }
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

        public string ContainerType
        {
            get { return this.containerType; }
            set { this.containerType = value; }
        }

        public string SealNo
        {
            get { return this.sealNo; }
            set { this.sealNo = value; }
        }

        public decimal Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }

        public decimal Volume
        {
            get { return this.volume; }
            set { this.volume = value; }
        }

        public int Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }

        public string PackageType
        {
            get { return this.packageType; }
            set { this.packageType = value; }
        }

        public string DgClass
        {
            get { return this.dgClass; }
            set { this.dgClass = value; }
        }

        public DateTime RequestDate
        {
            get { return this.requestDate; }
            set { this.requestDate = value; }
        }

        public DateTime ScheduleDate
        {
            get { return this.scheduleDate; }
            set { this.scheduleDate = value; }
        }
        public string ScheduleTime
        {
            get { return this.scheduleTime; }
            set { this.scheduleTime = value; }
        }

        public DateTime CfsInDate
        {
            get { return this.cfsInDate; }
            set { this.cfsInDate = value; }
        }

        public DateTime CfsOutDate
        {
            get { return this.cfsOutDate; }
            set { this.cfsOutDate = value; }
        }

        public DateTime YardPickupDate
        {
            get { return this.yardPickupDate; }
            set { this.yardPickupDate = value; }
        }

        public DateTime YardReturnDate
        {
            get { return this.yardReturnDate; }
            set { this.yardReturnDate = value; }
        }

        public string canChange
        {
            get
            {
                string sql = "select StatusCode from CTM_Job where JobNo='" + this.jobNo + "'";
                System.Data.DataTable dt = ConnectSql.GetTab(sql);
                string result = "";
                if (dt.Rows.Count > 0)
                {
                    result = SafeValue.SafeString(dt.Rows[0][0], "CLS");
                }
                if (result != "USE")
                {
                    return "none";
                }
                return "";
            }
        }

        public string PortnetStatus
        {
            get { return this.portnetStatus; }
            set { this.portnetStatus = value; }
        }
        public string F5Ind
        {
            get { return this.f5Ind; }
            set { this.f5Ind = value; }
        }
        public string UrgentInd
        {
            get { return this.urgentInd; }
            set { this.urgentInd = value; }
        }
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        private string statusCode;
        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }

        public DateTime CdtDate
        {
            get { return this.cdtDate; }
            set { this.cdtDate = value; }
        }

        public string CdtTime
        {
            get { return this.cdtTime; }
            set { this.cdtTime = value; }
        }

        public string TerminalLocation
        {
            get { return this.terminalLocation; }
            set { this.terminalLocation = value; }
        }

        public DateTime YardExpiryDate
        {
            get { return this.yardExpiryDate; }
            set { this.yardExpiryDate = value; }
        }

        public string YardExpiryTime
        {
            get { return this.yardExpiryTime; }
            set { this.yardExpiryTime = value; }
        }

        public string YardAddress
        {
            get { return this.yardAddress; }
            set { this.yardAddress = value; }
        }


        public bool GetPromiss_AutoInvoice
        {
            get
            {
                bool re = false;
                try
                {
                    string sql = string.Format(@"select StatusCode,(select count(*) from XAArInvoice where MastRefNo=ctm_job.JobNo and XAArInvoice.JobRefNo='{0}' ) as C From ctm_job where jobno='{1}'", this.containerNo, this.jobNo);
                    System.Data.DataTable dt = ConnectSql.GetTab(sql);
                    if (dt.Rows.Count > 0)
                    {
                        string status = dt.Rows[0]["StatusCode"].ToString();
                        int c = SafeValue.SafeInt(dt.Rows[0]["C"], 0);
                        if (status == "USE" && c == 0)
                        {
                            re = true;
                        }
                    }
                }
                catch { }
                return re;
            }
        }


        public string Remark1
        {
            get { return this.remark1; }
            set { this.remark1 = value; }
        }

        public string Remark2
        {
            get { return this.remark2; }
            set { this.remark2 = value; }
        }

        public string Permit
        {
            get { return this.permit; }
            set { this.permit = value; }
        }

        public string TTTime
        {
            get { return this.tTTime; }
            set { this.tTTime = value; }
        }

        public string WarehouseStatus
        {
            get { return this.warehouseStatus; }
            set { this.warehouseStatus = value; }
        }

        public string IsImport
        {
            get
            {
                string sql = string.Format(@"select JobType from ctm_job where jobno='{0}'", this.jobNo);
                System.Data.DataTable dt = ConnectSql.GetTab(sql);
                string re = "";
                if (dt.Rows.Count > 0)
                {
                    string temp = dt.Rows[0]["JobType"].ToString();
                    if (temp == "KD-IMP" || temp == "FCL-IMP")
                    {
                        re = "none";
                    }
                }
                return re;
            }
        }

        public string Br
        {
            get { return this.br; }
            set { this.br = value; }
        }
        public string EmailInd
        {
            get { return this.emailInd; }
            set { this.emailInd = value; }
        }
        string kdydInd;
        public string KdydInd
        {
            get { return this.kdydInd; }
            set { this.kdydInd = value; }
        }
        private string cfsStatus;//for Warehouse Satus
        public string CfsStatus
        {
            get { return this.cfsStatus; }
            set { this.cfsStatus = value; }
        }
        private string oogInd;
        public string OogInd
        {
            get { return this.oogInd; }
            set { this.oogInd = value; }
        }
        private string dischargeCell;
        public string DischargeCell
        {
            get { return this.dischargeCell; }
            set { this.dischargeCell = value; }
        }
        private DateTime scheduleStartDate;
        public DateTime ScheduleStartDate
        {
            get { return this.scheduleStartDate; }
            set { this.scheduleStartDate = value; }
        }
        private string scheduleStartTime;
        public string ScheduleStartTime
        {
            get { return this.scheduleStartTime; }
            set { this.scheduleStartTime = value; }
        }
        private DateTime completionDate;
        public DateTime CompletionDate
        {
            get { return this.completionDate; }
            set { this.completionDate = value; }
        }
        private string completionTime;
        public string CompletionTime
        {
            get { return this.completionTime; }
            set { this.completionTime = value; }
        }
        private string containerCategory;
        public string ContainerCategory
        {
            get { return this.containerCategory; }
            set { this.containerCategory = value; }
        }
        private string vehicleType;
        public string VehicleType
        {
            get { return this.vehicleType; }
            set { this.vehicleType = value; }
        }
        private string contractor;
        private string subContract_Ind;
        public string Contractor
        {
            get { return this.contractor; }
            set { this.contractor = value; }
        }
        public string SubContract_Ind
        {
            get { return this.subContract_Ind; }
            set { this.subContract_Ind = value; }
        }
        private string jobType;
        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }
        private string depotBkgRefNo;
        public string DepotBkgRefNo
        {
            get { return this.depotBkgRefNo; }
            set { this.depotBkgRefNo = value; }
        }
        private string stuff_Ind;
        public string Stuff_Ind
        {
            get { return this.stuff_Ind; }
            set { this.stuff_Ind = value; }
        }
        private string permitNo;
        private DateTime permitDate;
        private string permitBy;
        private string partyInvNo;
        private string incoTerms;
        private decimal gstAmt;
        private string permitRemark;
        private string paymentStatus;
        public string PermitNo
        {
            get { return this.permitNo; }
            set { this.permitNo = value; }
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

        public string IncoTerms
        {
            get { return this.incoTerms; }
            set { this.incoTerms = value; }
        }

        public decimal GstAmt
        {
            get { return this.gstAmt; }
            set { this.gstAmt = value; }
        }

        public string PermitRemark
        {
            get { return this.permitRemark; }
            set { this.permitRemark = value; }
        }

        public string PaymentStatus
        {
            get { return this.paymentStatus; }
            set { this.paymentStatus = value; }
        }


        private string whsReadyInd;
        private string whsReadyTime;
        private string whsReadyLocation;
        private decimal whsReadyWeight;

        public string WhsReadyInd
        {
            get { return this.whsReadyInd; }
            set { this.whsReadyInd = value; }
        }

        public string WhsReadyTime
        {
            get { return this.whsReadyTime; }
            set { this.whsReadyTime = value; }
        }

        public string WhsReadyLocation
        {
            get { return this.whsReadyLocation; }
            set { this.whsReadyLocation = value; }
        }

        public decimal WhsReadyWeight
        {
            get { return this.whsReadyWeight; }
            set { this.whsReadyWeight = value; }
        }


        private string releaseToHaulierRemark;
        public string ReleaseToHaulierRemark
        {
            get { return this.releaseToHaulierRemark; }
            set { this.releaseToHaulierRemark = value; }
        }
		
		private string serviceType;
        public string ServiceType
        {
            get { return this.serviceType; }
            set { this.serviceType = value; }
        }

		private decimal contWeight;
        public decimal ContWeight
        {
            get { return this.contWeight; }
            set { this.contWeight = value; }
        }

        private decimal cargoWeight;
        public decimal CargoWeight
        {
            get { return this.cargoWeight; }
            set { this.cargoWeight = value; }
        }

        public static bool contTruckingStatusChanged(int contId)
        {
            bool res = false;
            string sql = string.Format(@"select JobNo,CfsStatus,StatusCode from ctm_jobdet1 where Id=@contId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@contId", contId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                string status = dt.Rows[0]["StatusCode"].ToString();
                string JobNo = dt.Rows[0]["JobNo"].ToString();
                if (status == "Completed")
                {
                    res = contTruckingStatusCompleted_createCost(contId);
                }
            }
            return res;
        }
        public static bool contWarehouseStatusChanged(int contId)
        {
            bool res = false;
            string sql = string.Format(@"select det1.JobNo,det1.CfsStatus,det1.StatusCode,job.IsWarehouse,job.JobType from ctm_jobdet1 det1 inner join ctm_job job on det1.JobNo=job.JobNo where det1.Id=@contId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@contId", contId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                string status = dt.Rows[0]["CfsStatus"].ToString();
                string JobNo = dt.Rows[0]["JobNo"].ToString();
                string jobType = dt.Rows[0]["JobType"].ToString();
                string isWarehouse = dt.Rows[0]["IsWarehouse"].ToString();
                if (status == "Completed")
                {
                    res = contWarehouseStatusCompleted_createCost(contId);
                }
                contStatus_update(contId, jobType, status, isWarehouse);
            }
            return res;
        }

        static bool contTruckingStatusCompleted_createCost(int contId)
        {
            return contStatusCompleted_createCost(contId, "TRUCKING");
        }
        static bool contWarehouseStatusCompleted_createCost(int contId)
        {
            return contStatusCompleted_createCost(contId, "WAREHOUSE");
        }
        static bool contStatusCompleted_createCost(int contId, string BillClass)
        {
            bool res = false;
            string sql = null;
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();

            sql = string.Format(@"select det1.JobNo,job.JobType,job.ClientId,det1.ContainerNo,det1.ContainerType,det1.BillType 
from ctm_jobdet1 as det1
left outer join ctm_job as job on det1.jobno=job.JobNo
where det1.Id=@contId");
            list.Add(new ConnectSql_mb.cmdParameters("@contId", contId, SqlDbType.Int));
            DataTable dt_jobdet1 = ConnectSql_mb.GetDataTable(sql, list);
            if (dt_jobdet1.Rows.Count > 0)
            {
                res = true;
                string JobNo = dt_jobdet1.Rows[0]["JobNo"].ToString();
                string JobType = dt_jobdet1.Rows[0]["JobType"].ToString();
                string ClientId = dt_jobdet1.Rows[0]["ClientId"].ToString();
                string BillType = dt_jobdet1.Rows[0]["BillType"].ToString();


                //=========== job level cost creat
                CtmJob.jobCost_Create(JobNo, JobType, ClientId, BillClass);
                decimal gst = 0;
                string gstType = "";
                string chgTypeId = "";
                string lineSource = "S";
                #region container level cost

                string ContainerNo = dt_jobdet1.Rows[0]["ContainerNo"].ToString().Replace(" ", "");
                string ContainerType = dt_jobdet1.Rows[0]["ContainerType"].ToString().Replace(" ", "");

                #region JobType
                string sql_rate = string.Format(@"select Id,ChgCode,ChgCodeDes,Qty,Price,GstType,BillClass,BillScope,CurrencyId,ExRate,ContSize,ContType,JobType from job_rate where ClientId=@ClientId and BillScope=@LineType and BillClass=@BillClass and BillType=@BillType and JobType=@JobType and JobNo='-1' and LineStatus=@LineStatus");
                list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@ClientId", ClientId, SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CONT", SqlDbType.NVarChar, 30));
                list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                list.Add(new ConnectSql_mb.cmdParameters("@BillType", BillType, SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@LineStatus", "N", SqlDbType.NVarChar, 30));
                DataTable dt_rate = ConnectSql_mb.GetDataTable(sql_rate, list);
                if (dt_rate.Rows.Count==0)
                {
                    #region CLient=STD
                    sql_rate = string.Format(@"select Id,ChgCode,ChgCodeDes,Qty,Price,GstType,BillClass,BillScope,CurrencyId,ExRate,ContSize,ContType,JobType from job_rate where ClientId=@ClientId and BillScope=@LineType and BillClass=@BillClass and BillType=@BillType and JobType=@JobType and JobNo='-1' and LineStatus=@LineStatus");
                    list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@ClientId", "STD", SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CONT", SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@BillType", BillType, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@LineStatus", "N", SqlDbType.NVarChar, 30));
                    dt_rate = ConnectSql_mb.GetDataTable(sql_rate, list);
                    lineSource = "D";
                    #endregion
                }
                for (int i = 0; i < dt_rate.Rows.Count; i++)
                {
                    string chgCode = SafeValue.SafeString(dt_rate.Rows[i]["ChgCode"]);
                    string chgCodeDes = SafeValue.SafeString(dt_rate.Rows[i]["ChgCodeDes"]);
                    gstType = SafeValue.SafeString(dt_rate.Rows[i]["GstType"]);
                    string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where ChgcodeId='{0}'", chgCode);
                    DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);

                    if (dt_chgCode.Rows.Count > 0)
                    {
                        gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
                        gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
                        chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
                    }
                    int id = SafeValue.SafeInt(dt_rate.Rows[i]["Id"], 0);
                    string contType = SafeValue.SafeString(dt_rate.Rows[i]["ContType"]);
                    string contSize = SafeValue.SafeString(dt_rate.Rows[i]["ContSize"]);
                    string ContSize = (ContainerType.Length >= 2 ? ContainerType.Substring(0, 2) : "0");
                    string jobType = SafeValue.SafeString(dt_rate.Rows[i]["JobType"]);
                    sql = string.Format(@"select Id from job_cost where LineType=@LineType and JobNo=@JobNo and BillClass=@BillClass and JobType=@JobType and LineSource=@LineSource and ContNo=@ContainerNo and ChgCode=@ChgCode");
                    list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CONT", SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@LineSource", "S", SqlDbType.NVarChar, 10));
                    list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", ContainerNo, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", chgCode, SqlDbType.NVarChar, 100));
                    DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
                    if (dt.Rows.Count <= 0)
                    {

                        if (contSize.Length > 0 && contType.Length > 0)
                        {
                            #region TRUCKING
                            if (chgCodeDes.ToUpper().Contains("TRUCKING"))
                            {
                                if (ContainerType == contType && (ContainerType == "20HD" || ContainerType == "40HD"))
                                {
                                    Create_Cost(dt_rate.Rows[i], JobNo, ContainerNo, ContainerType, BillType, ClientId, JobType, BillClass, gst, gstType,lineSource);
                                }
                                else if ((ContainerType.Substring(0, 2) == contSize && ContainerType != "20HD") || (ContainerType.Substring(0, 2) == contSize && ContainerType != "40HD"))
                                {
                                    Create_Cost(dt_rate.Rows[i], JobNo, ContainerNo, ContainerType, BillType, ClientId, JobType, BillClass, gst, gstType,lineSource);
                                }
                            }
                            else
                            {
                                Create_Cost(dt_rate.Rows[i], JobNo, ContainerNo, ContainerType, BillType, ClientId, JobType, BillClass, gst, gstType,lineSource);
                            }
                            #endregion
                        }
                        else if (contSize.Length > 0)
                        {
                            #region WAREHOUSE
                            if (ContainerType.Length>2&&ContainerType.Substring(0, 2) == contSize)
                            {
                                Create_Cost(dt_rate.Rows[i], JobNo, ContainerNo, ContainerType, BillType, ClientId, JobType, BillClass, gst, gstType,lineSource);
                            }
                            #endregion
                        }
                        else
                        {
                            Create_Cost(dt_rate.Rows[i], JobNo, ContainerNo, ContainerType, BillType, ClientId, JobType, BillClass, gst, gstType, lineSource);
                        }
                    }
                }
                #endregion

                #region No JobType
                list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@ClientId", ClientId, SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CONT", SqlDbType.NVarChar, 30));
                list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                list.Add(new ConnectSql_mb.cmdParameters("@BillType", BillType, SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@JobType", "", SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@LineStatus", "N", SqlDbType.NVarChar, 30));
                dt_rate = ConnectSql_mb.GetDataTable(sql_rate, list);
                if (dt_rate.Rows.Count == 0)
                {
                    #region CLient=STD
                    lineSource = "D";
                    sql_rate = string.Format(@"select Id,ChgCode,ChgCodeDes,Qty,Price,GstType,BillClass,BillScope,CurrencyId,ExRate,ContSize,ContType,JobType from job_rate where ClientId=@ClientId and BillScope=@LineType and BillClass=@BillClass and BillType=@BillType and JobType=@JobType and JobNo='-1' and LineStatus=@LineStatus");
                    list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@ClientId", "STD", SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CONT", SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@BillType", BillType, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@JobType", "", SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@LineStatus", "N", SqlDbType.NVarChar, 30));
                    dt_rate = ConnectSql_mb.GetDataTable(sql_rate, list);
                    #endregion
                }
                for (int i = 0; i < dt_rate.Rows.Count; i++)
                {
                    string chgCode = SafeValue.SafeString(dt_rate.Rows[i]["ChgCode"]);
                    string chgCodeDes = SafeValue.SafeString(dt_rate.Rows[i]["ChgCodeDes"]);
                    int id = SafeValue.SafeInt(dt_rate.Rows[i]["Id"], 0);
                    string contType = SafeValue.SafeString(dt_rate.Rows[i]["ContType"]);
                    string contSize = SafeValue.SafeString(dt_rate.Rows[i]["ContSize"]);
                    string ContSize = (ContainerType.Length >= 2 ? ContainerType.Substring(0, 2) : "0");
                    string jobType = SafeValue.SafeString(dt_rate.Rows[i]["JobType"]);
                    sql = string.Format(@"select Id from job_cost where LineType=@LineType and JobNo=@JobNo and BillClass=@BillClass and JobType=@JobType and LineSource=@LineSource and ContNo=@ContainerNo and ChgCode=@ChgCode");
                    list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CONT", SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@LineSource", "S", SqlDbType.NVarChar, 10));
                    list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", ContainerNo, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", chgCode, SqlDbType.NVarChar, 100));
                    DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
                    if (dt.Rows.Count <= 0)
                    {
                        Create_Cost(dt_rate.Rows[i], JobNo, ContainerNo, ContainerType, BillType, ClientId, JobType, BillClass, gst, gstType, lineSource);
                    }
                }
                #endregion

                #endregion
            }
            Create_PSALOLO(contId, BillClass);
            return res;
        }
        static bool Create_PSALOLO(int contId, string BillClass)
        {
            bool res = false;
            string sql = null;
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            sql = string.Format(@"select det1.JobNo,job.JobType,job.ClientId,det1.ContainerNo,det1.ContainerType,det1.BillType,det1.StatusCode
from ctm_jobdet1 as det1
left outer join ctm_job as job on det1.jobno=job.JobNo
where det1.Id=@contId");
            list.Add(new ConnectSql_mb.cmdParameters("@contId", contId, SqlDbType.Int));
            DataTable dt_jobdet1 = ConnectSql_mb.GetDataTable(sql, list);
            if (dt_jobdet1.Rows.Count > 0)
            {
                res = true;
                string ContainerNo = dt_jobdet1.Rows[0]["ContainerNo"].ToString().Replace(" ", "");
                string ContainerType = dt_jobdet1.Rows[0]["ContainerType"].ToString().Replace(" ", "");
                string JobNo = dt_jobdet1.Rows[0]["JobNo"].ToString();
                string JobType = dt_jobdet1.Rows[0]["JobType"].ToString();
                string ClientId = dt_jobdet1.Rows[0]["ClientId"].ToString();
                string status = dt_jobdet1.Rows[0]["StatusCode"].ToString();
                string str = ContainerType.Length >= 2 ? ContainerType.Substring(0, 2) : "";
                if (status == "Completed")
                {
                    #region
                    sql = string.Format(@"select count(*) from job_cost where ContNo='{0}' and JobNo='{1}' and ChgCodeDes like '%LOLO%'", ContainerNo, JobNo);
                    int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
                    if (n == 0)
                    {
                        string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where ChgcodeDes like '%LOLO {0}%'", str);
                        DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
                        decimal gst = 0;
                        string gstType = "";
                        string chgTypeId = "";
                        string chgCodeDes = "";
                        string chgCode = "";
                        if (dt_chgCode.Rows.Count > 0)
                        {
                            gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
                            gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
                            chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
                            chgCode = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgcodeId"]);
                            chgCodeDes = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgcodeDes"]);
                        }
                        decimal price = 0;
                        if (str == "20")
                        {
                            price = 55;
                        }
                        if (str == "40")
                        {
                            price = SafeValue.SafeDecimal(82.5);
                        }

                        sql = string.Format(@"insert into job_cost (JobNo,JobType,ChgCode,ChgCodeDes,Price,Qty,CurrencyId,ExRate,LocAmt,LineSource,LineType,BillClass,ContNo,ContType,RowCreateUser,RowCreateTime,RowUpdateUser,RowUpdateTime,LineId,DocAmt,Gst,GstAmt,GstType)
values(@JobNo,@JobType,@ChgCode,@ChgCodeDes,@Price,@Qty,@CurrencyId,@ExRate,@LocAmt,@LineSource,@LineType,@BillClass,@ContainerNo,@ContainerType,@UserId,@DateTime,@UserId,@DateTime,0,@DocAmt,@Gst,@GstAmt,@GstType)");
                        list = new List<ConnectSql_mb.cmdParameters>();
                        list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CONT", SqlDbType.NVarChar, 30));
                        list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                        list.Add(new ConnectSql_mb.cmdParameters("@LineSource", "S", SqlDbType.NVarChar, 10));
                        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
                        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));
                        list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", ContainerNo, SqlDbType.NVarChar, 100));
                        list.Add(new ConnectSql_mb.cmdParameters("@ContainerType", ContainerType, SqlDbType.NVarChar, 100));

                        list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", chgCode, SqlDbType.NVarChar, 100));
                        list.Add(new ConnectSql_mb.cmdParameters("@ChgCodeDes", chgCodeDes, SqlDbType.NVarChar, 300));
                        list.Add(new ConnectSql_mb.cmdParameters("@Price", price, SqlDbType.Decimal));
                        list.Add(new ConnectSql_mb.cmdParameters("@Qty", 1, SqlDbType.Int));
                        decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
                        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
                        decimal docAmt = amt;
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

                    #endregion
                }
            }
            return res;
        }
        static bool Create_Cost(DataRow row, string JobNo, string ContainerNo, string ContainerType, string BillType,
            string ClientId, string JobType, string BillClass, decimal gst, string gstType,string lineSource)
        {
            string chgCode = SafeValue.SafeString(row["ChgCode"]);
            string chgCodeDes = SafeValue.SafeString(row["ChgCodeDes"]);
            decimal price = SafeValue.SafeDecimal(row["Price"]);
            decimal qty = SafeValue.SafeDecimal(row["Qty"]);
            string scope = SafeValue.SafeString(row["BillScope"]);
            string billClass = SafeValue.SafeString(row["BillClass"]);

            string sql = string.Format(@"insert into job_cost (JobNo,JobType,ChgCode,ChgCodeDes,Price,Qty,CurrencyId,ExRate,LocAmt,LineSource,LineType,BillClass,ContNo,ContType,RowCreateUser,RowCreateTime,RowUpdateUser,RowUpdateTime,LineId,DocAmt,Gst,GstAmt,GstType)
values(@JobNo,@JobType,@ChgCode,@ChgCodeDes,@Price,@Qty,@CurrencyId,@ExRate,@LocAmt,@LineSource,@LineType,@BillClass,@ContainerNo,@ContainerType,@UserId,@DateTime,@UserId,@DateTime,0,@DocAmt,@Gst,@GstAmt,@GstType)");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CONT", SqlDbType.NVarChar, 30));
            list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
            list.Add(new ConnectSql_mb.cmdParameters("@LineSource", lineSource, SqlDbType.NVarChar, 10));
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", ContainerNo, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContainerType", ContainerType, SqlDbType.NVarChar, 100));

            list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", chgCode, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ChgCodeDes", chgCodeDes, SqlDbType.NVarChar, 300));
            list.Add(new ConnectSql_mb.cmdParameters("@Price", price, SqlDbType.Decimal));
            list.Add(new ConnectSql_mb.cmdParameters("@Qty", 1, SqlDbType.Int));
            decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
            decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
            decimal docAmt = amt;
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

            return true;
        }
        public static bool contStatus_update(int contId, string tripCode, string statusCode, string isWarehouse)
        {
            bool res = false;
            string sql = "";
            string status = "";
            if (isWarehouse == "Yes")
            {
                if (tripCode == "IMP")
                {
                    if (statusCode == "Completed")
                        status = "WHS-MT";
                    else
                        status = "WHS-LD";
                }
                if (tripCode == "EXP")
                {
                    if (statusCode == "Completed")
                        status = "WHS-LD";
                    else
                        status = "WHS-MT";
                }
                if (tripCode == "WDO")
                {
                    if (statusCode == "Completed")
                        status = "LCL-LD";
                }
                if (tripCode == "WGR")
                {
                    if (statusCode == "Completed")
                        status = "LCL-MT";
                }
                if (status != "")
                {
                    sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", contId, status);
                    ConnectSql_mb.ExecuteNonQuery(sql);
                }
                res = true;
            }
            return res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace C2
{
    public class CtmJobDet2
    {
        #region columns
        private int id;
        private string jobNo;
        private string containerNo;
        private string driverCode;
        private string driverCode2;
        private string driverCode11;
        private string driverCode12;
        private string driverCode3;
        private string towheadCode;
        private string chessisCode;
        private string remark;
        private string fromCode;
        private DateTime fromDate;
        private string fromTime;
        private string toCode;
        private DateTime toDate;
        private string toTime;
        private string cfsCode;
        private string bayCode;
        private string subletFlag;
        private string subletHauliername;
        private string statuscode;
        private string tripCode;
        private string stageCode;
        private string stageStatus;
        private string loadCode;
        private string overtime;
        private string overDistance;
        private string carpark;
        private decimal price;
        private string remark1;
        private string remark2;
        private string remark3;
        private decimal overTimePrice;
        private string fromParkingLot;
        private string toParkingLot;
        private int fromParkingLotType;
        private int toParkingLotType;
        private string parkingZone;
        private string doubleMounting;
        private DateTime bookingDate;
        private string bookingTime;
        private string bookingTime2;
        private decimal totalHour;
        private string bookingRemark;
        private decimal otHour;
        private string byUser;
        private string createUser;
        private DateTime createTime;
        private string updateUser;
        private DateTime updateTime;
        private string billingRemark;
        private decimal qty;
        private string packageType;
        private decimal weight;
        private decimal volume;
        private string deliveryRemark;
        private string satisfaction;
        private string podType;

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

        public string DriverCode
        {
            get { return this.driverCode; }
            set { this.driverCode = value; }
        }

        public string DriverCode2
        {
            get { return this.driverCode2; }
            set { this.driverCode2 = value; }
        }

        public string DriverCode11
        {
            get { return this.driverCode11; }
            set { this.driverCode11 = value; }
        }

        public string DriverCode12
        {
            get { return this.driverCode12; }
            set { this.driverCode12 = value; }
        }
		

        public string DriverCode3
        {
            get { return this.driverCode3; }
            set { this.driverCode3 = value; }
        }

        public string TowheadCode
        {
            get { return this.towheadCode; }
            set { this.towheadCode = value; }
        }

        public string ChessisCode
        {
            get { return this.chessisCode; }
            set { this.chessisCode = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public string FromCode
        {
            get { return this.fromCode; }
            set { this.fromCode = value; }
        }

        public DateTime FromDate
        {
            get { return this.fromDate; }
            set { this.fromDate = value; }
        }

        public string FromTime
        {
            get { return this.fromTime; }
            set { this.fromTime = value; }
        }

        public string ToCode
        {
            get { return this.toCode; }
            set { this.toCode = value; }
        }

        public DateTime ToDate
        {
            get { return this.toDate; }
            set { this.toDate = value; }
        }

        public string ToTime
        {
            get { return this.toTime; }
            set { this.toTime = value; }
        }

        public string CfsCode
        {
            get { return this.cfsCode; }
            set { this.cfsCode = value; }
        }

        public string BayCode
        {
            get { return this.bayCode; }
            set { this.bayCode = value; }
        }

        public string SubletFlag
        {
            get { return this.subletFlag; }
            set { this.subletFlag = value; }
        }

        public string SubletHauliername
        {
            get { return this.subletHauliername; }
            set { this.subletHauliername = value; }
        }

        public string Statuscode
        {
            get { return this.statuscode; }
            set { this.statuscode = value; }
        }

        public string canChange
        {
            get
            {
                string sql = "select StatusCode from CTM_Job where JobNo='" + this.jobNo + "'";
                string result = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql), "CLS");
                if (result != "USE")
                {
                    return "none";
                }
                else if(this.tripStatus=="LOCKED"|| this.tripStatus == "PAID")
                    return "none";
                return "";
            }
        }

        public string TripCode
        {
            get { return this.tripCode; }
            set { this.tripCode = value; }
        }

        private int det1Id;
        public int Det1Id
        {
            get { return this.det1Id; }
            set { this.det1Id = value; }
        }

        public string StageCode
        {
            get { return this.stageCode; }
            set { this.stageCode = value; }
        }

        public string StageCode_show
        {
            get
            {
                string re = this.stageCode;
                switch (re)
                {
                    case "Port":
                        re = "PT";
                        break;
                    case "Warehouse":
                        re = "WH";
                        break;
                    case "Yard":
                        re = "YD";
                        break;
                    case "Park1":
                        re = "P1";
                        break;
                    case "Park2":
                        re = "P2";
                        break;
                }

                return re;
            }
        }

        public int StageCode_Index
        {
            get
            {
                string sql = "select JobType from CTM_Job where JobNo='" + this.jobNo + "'";
                string type = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql), "");
                int re = 0;
                if (type.IndexOf("IMP") > -1)
                {
                    switch (this.stageCode)
                    {
                        case "Port":
                            re = 1;
                            break;
                        case "Park1":
                            re = 2;
                            break;
                        case "Warehouse":
                            re = 3;
                            break;
                        case "Park2":
                            re = 4;
                            break;
                        case "Yard":
                            re = 5;
                            break;
                        case "Completed":
                            re = 6;
                            break;
                    }
                }
                else
                {
                    switch (this.stageCode)
                    {
                        case "Port":
                            re = 5;
                            break;
                        case "Park1":
                            re = 4;
                            break;
                        case "Warehouse":
                            re = 3;
                            break;
                        case "Park2":
                            re = 2;
                            break;
                        case "Yard":
                            re = 1;
                            break;
                        case "Completed":
                            re = 6;
                            break;
                    }
                }

                return re;
            }
        }

        public string StageStatus
        {
            get { return this.stageStatus; }
            set { this.stageStatus = value; }
        }

        public string LoadCode
        {
            get { return this.loadCode; }
            set { this.loadCode = value; }
        }
        public string Overtime
        {
            get { return this.overtime; }
            set { this.overtime = value; }
        }

        public string OverDistance
        {
            get { return this.overDistance; }
            set { this.overDistance = value; }
        }

        public string Carpark
        {
            get { return this.carpark; }
            set { this.carpark = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
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

        public string Remark3
        {
            get { return this.remark3; }
            set { this.remark3 = value; }
        }

        public decimal OverTimePrice
        {
            get { return this.overTimePrice; }
            set { this.overTimePrice = value; }
        }

        public string FromParkingLot
        {
            get { return this.fromParkingLot; }
            set { this.fromParkingLot = value; }
        }

        public string ToParkingLot
        {
            get { return this.toParkingLot; }
            set { this.toParkingLot = value; }
        }

        public int FromParkingLotType
        {
            get { return this.fromParkingLotType; }
            set { this.fromParkingLotType = value; }
        }

        public int ToParkingLotType
        {
            get { return this.toParkingLotType; }
            set { this.toParkingLotType = value; }
        }
        public string ParkingZone
        {
            get { return this.parkingZone; }
            set { this.parkingZone = value; }
        }
        public string DoubleMounting
        {
            get { return this.doubleMounting; }
            set { this.doubleMounting = value; }
        }


        public DateTime BookingDate
        {
            get { return this.bookingDate; }
            set { this.bookingDate = value; }
        }

        public string BookingTime
        {
            get { return this.bookingTime; }
            set { this.bookingTime = value; }
        }

        public string BookingTime2
        {
            get { return this.bookingTime2; }
            set { this.bookingTime2 = value; }
        }

        public decimal TotalHour
        {
            get { return this.totalHour; }
            set { this.totalHour = value; }
        }

        public string BookingRemark
        {
            get { return this.bookingRemark; }
            set { this.bookingRemark = value; }
        }

        public decimal OtHour
        {
            get { return this.otHour; }
            set { this.otHour = value; }
        }

        public string ByUser
        {
            get { return this.byUser; }
            set { this.byUser = value; }
        }

        public string CreateUser
        {
            get { return this.createUser; }
            set { this.createUser = value; }
        }

        public DateTime CreateTime
        {
            get { return this.createTime; }
            set { this.createTime = value; }
        }

        public string UpdateUser
        {
            get { return this.updateUser; }
            set { this.updateUser = value; }
        }

        public DateTime UpdateTime
        {
            get { return this.updateTime; }
            set { this.updateTime = value; }
        }

        public decimal BillingTrip
        {
            get
            {
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 10));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "CustBill", SqlDbType.NVarChar, 10));
                decimal res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }

        public decimal BillingOT
        {
            get
            {
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 10));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "CustOT", SqlDbType.NVarChar, 10));
                decimal res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal BillingPermit
        {
            get
            {
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 10));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "CustPermit", SqlDbType.NVarChar, 10));
                decimal res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }

        public string BillingRemark
        {
            get { return this.billingRemark; }
            set { this.billingRemark = value; }
        }
        public decimal Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }

        public string PackageType
        {
            get { return this.packageType; }
            set { this.packageType = value; }
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
        public string DeliveryRemark
        {
            get { return this.deliveryRemark; }
            set { this.deliveryRemark = value; }
        }

        public string Satisfaction
        {
            get { return this.satisfaction; }
            set { this.satisfaction = value; }
        }

        public string PodType
        {
            get { return this.podType; }
            set { this.podType = value; }
        }

        private string serviceType;
        public string ServiceType
        {
            get { return this.serviceType; }
            set { this.serviceType = value; }
        }

		private string clientRefNo;
        public string ClientRefNo
        {
            get { return this.clientRefNo; }
            set { this.clientRefNo = value; }
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
        private string subCon_Code;
        public string SubCon_Code
        {
            get { return this.subCon_Code; }
            set { this.subCon_Code = value; }
        }
        private string subCon_Ind;
        public string SubCon_Ind
        {
            get { return this.subCon_Ind; }
            set { this.subCon_Ind = value; }
        }
        private string self_Ind;
        public string Self_Ind
        {
            get { return this.self_Ind; }
            set { this.self_Ind = value; }
        }
        private string jobType;
        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }
        private string requestVehicleType;
        public string RequestVehicleType
        {
            get { return this.requestVehicleType; }
            set { this.requestVehicleType = value; }
        }
        private string requestTrailerType;
        public string RequestTrailerType
        {
            get { return this.requestTrailerType; }
            set { this.requestTrailerType = value; }
        }
        private string agentId;
        public string AgentId 
        {
            get { return this.agentId; }
            set { this.agentId = value; }
        }
        private string agentName;
        public string AgentName
        {
            get { return this.agentName; }
            set { this.agentName = value; }
        }

        private string epodCB1;
        private string epodCB2;
        private string epodCB3;
        private string epodCB4;
        private string epodCB5;
        private string epodCB6;
        
        public string EpodCB1
        {
            get { return this.epodCB1; }
            set { this.epodCB1 = value; }
        }

        public string EpodCB2
        {
            get { return this.epodCB2; }
            set { this.epodCB2 = value; }
        }

        public string EpodCB3
        {
            get { return this.epodCB3; }
            set { this.epodCB3 = value; }
        }

        public string EpodCB4
        {
            get { return this.epodCB4; }
            set { this.epodCB4 = value; }
        }

        public string EpodCB5
        {
            get { return this.epodCB5; }
            set { this.epodCB5 = value; }
        }

        public string EpodCB6
        {
            get { return this.epodCB6; }
            set { this.epodCB6 = value; }
        }

        private string warehouseStatus;
        public string WarehouseStatus
        {
            get { return this.warehouseStatus; }
            set { this.warehouseStatus = value; }
        }
        private string warehouseRemark;
        public string WarehouseRemark
        {
            get { return this.warehouseRemark; }
            set { this.warehouseRemark = value; }
        }

        private DateTime warehouseScheduleDate;
        public DateTime WarehouseScheduleDate
        {
            get { return this.warehouseScheduleDate; }
            set { this.warehouseScheduleDate = value; }
        }
        private DateTime warehouseStartDate;
        public DateTime WarehouseStartDate
        {
            get { return this.warehouseStartDate; }
            set { this.warehouseStartDate = value; }
        }
        private DateTime warehouseEndDate;
        public DateTime WarehouseEndDate 
        {
            get { return this.warehouseEndDate; }
            set { this.warehouseEndDate = value; }
        }
        private string tripIndex;
        public string TripIndex
        {
            get { return this.tripIndex; }
            set { this.tripIndex = value; }
        }

        private string permitNo;
        public string PermitNo
        {
            get { return this.permitNo; }
            set { this.permitNo = value; }
        }
        private string permitBy;
        public string PermitBy
        {
            get { return this.permitBy; }
            set { this.permitBy = value; }
        }
        private DateTime permitDate;
        public DateTime PermitDate
        {
            get { return this.permitDate; }
            set { this.permitDate = value; }
        }
        private string permitRemark;
        public string PermitRemark
        {
            get { return this.permitRemark; }
            set { this.permitRemark = value; }
        }
        private string partyInvNo;
        public string PartyInvNo
        {
            get { return this.partyInvNo; }
            set { this.partyInvNo = value; }
        }
        private string incoTerm;
        public string IncoTerm
        {
            get { return this.incoTerm; }
            set { this.incoTerm = value; }
        }
        private decimal gstAmt;
        public decimal GstAmt
        {
            get { return this.gstAmt; }
            set { this.gstAmt = value; }
        }
        private string paymentStatus;
        public string PaymentStatus
        {
            get { return this.paymentStatus; }
            set { this.paymentStatus = value; }
        }
        private int manPowerNo;
        public int ManPowerNo
        {
            get { return this.manPowerNo; }
            set { this.manPowerNo = value; }
        }
        private string excludeLunch;
        public string ExcludeLunch
        {
            get { return this.excludeLunch; }
            set { this.excludeLunch = value; }
        }
        private string returnType;
        public string ReturnType
        {
            get { return this.returnType; }
            set { this.returnType = value; }
        }
        private DateTime returnLastDate;
        public DateTime ReturnLastDate
        {
            get { return this.returnLastDate; }
            set { this.returnLastDate = value; }
        }
        private string cargoVerify;
        public string CargoVerify
        {
            get { return this.cargoVerify; }
            set { this.cargoVerify = value; }
        }
        private string deliveryResult;
        public string DeliveryResult
        {
            get { return this.deliveryResult; }
            set { this.deliveryResult = value; }
        }
        private string epodHardCopy;
        public string EpodHardCopy
        {
            get { return this.epodHardCopy; }
            set { this.epodHardCopy = value; }
        }

        private string directInf;
        public string DirectInf
        {
            get { return this.directInf; }
            set { this.directInf = value; }
        }
        private string tripStatus;
        public string TripStatus
        {
            get { return this.tripStatus; }
            set { this.tripStatus = value; }
        }
        private string manualDo;
        public string ManualDo
        {
            get { return this.manualDo; }
            set { this.manualDo = value; }
        }
        #endregion

        #region Incentive/Claims
        public string Incentive
        {
            get
            {
                string res = "";
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "DP", SqlDbType.NVarChar, 2));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context).ToString("n");
                return res;
            }
        }
        public decimal Incentive1
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode and (isnull(DriverCode,'')='' or DriverCode=@DriverCode)");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "DP", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "Trip", SqlDbType.NVarChar, 30));
                list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", this.DriverCode, SqlDbType.NVarChar, 100));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Incentive2
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "DP", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "OverTime", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Incentive3
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "DP", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "Standby", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Incentive4
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "DP", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "PSA", SqlDbType.NVarChar, 30));
                //res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
                res = SafeValue.SafeDecimal(string.Format("{0:F0}", dt.Rows[0][0]));
                return res;
            }
        }

        public string Claims
        {
            get
            {
                string res = "";
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context).ToString("n");
                return res;
            }
        }
        public decimal Charge1
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "DHC", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Charge2
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "WEIGHING", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Charge3
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "WASHING", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Charge4
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "REPAIR", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Charge5
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "DETENTION", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Charge6
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "DEMURRAGE", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Charge7
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "LIFT_ON_OFF", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Charge8
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "C_SHIPMENT", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Charge9
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "EMF", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Charge10
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "OTHER", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }

        public decimal Charge_EXPENSE
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "EXPENSE", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }

        public decimal Charge_ERP
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "ERP", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }

        public decimal Charge_ParkingFee
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "ParkingFee", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }

        public decimal Charge_LiftingTeamOverTime
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "LiftingTeamOverTime", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Charge_WorkerOvertime
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "WorkerOvertime", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }

        public decimal Billing_CraneCharges
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 10));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "Trip", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Billing_CraneOvertime
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 10));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "OverTime", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Billing_ConcreteBucket
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "ConcreteBucket", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Billing_SandBucket
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "SandBucket", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Billing_LiftingSupervisor
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "LiftingSupervisor", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Billing_Ringer
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "Rigger", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Billing_Signal
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "Signal", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Billing_LightEquipment
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "LiftingEquipment", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }
        public decimal Billing_Labour
        {
            get
            {
                decimal res = 0;
                string sql = string.Format(@"select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=@TripId and LineType=@LineType and ChgCode=@ChgCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", this.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 2));
                list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", "Labour", SqlDbType.NVarChar, 30));
                res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql, list).context);
                return res;
            }
        }



        //public static bool Incentive_Save(int TripId, decimal Trip, decimal OverTime, decimal Standby, decimal PSA)
        public static bool Incentive_Save(int TripId, Dictionary<string, decimal> d)
        {
            bool res = false;
            List<ConnectSql_mb.cmdParameters> list = null;
            string sql = string.Format(@"select SequenceId as Id,ChgcodeId as c,ChgcodeDes as n from XXChgCode where ChgTypeId='INCENTIVE'");
            DataTable dt_incentive = ConnectSql_mb.GetDataTable(sql);

            sql = string.Format(@"select det2.Id,job.JobNo,job.JobType,det1.ContainerNo,det1.ContainerType,det2.DriverCode
from ctm_jobdet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.Id=@TripId");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@TripId", TripId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", dt.Rows[0]["JobNo"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@JobType", dt.Rows[0]["JobType"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContNo", dt.Rows[0]["ContainerNo"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContType", dt.Rows[0]["ContainerType"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", dt.Rows[0]["DriverCode"], SqlDbType.NVarChar, 100));


            ConnectSql_mb.cmdParameters ChgCode = new ConnectSql_mb.cmdParameters("@ChgCode", "", SqlDbType.NVarChar, 100);
            list.Add(ChgCode);
            ConnectSql_mb.cmdParameters ChgCodeDes = new ConnectSql_mb.cmdParameters("@ChgCodeDes", "", SqlDbType.NVarChar, 100);
            list.Add(ChgCodeDes);
            ConnectSql_mb.cmdParameters Price = new ConnectSql_mb.cmdParameters("@Price", 0, SqlDbType.Decimal);
            list.Add(Price);
            ConnectSql_mb.cmdParameters LineType = new ConnectSql_mb.cmdParameters("@LineType", "DP", SqlDbType.NVarChar, 10);
            list.Add(LineType);

            cost_row2list_foreach(d, dt_incentive, list, ChgCode, ChgCodeDes, Price,true);

            return res;
        }


        public static bool Incentive_Save_ByDriver(int TripId, Dictionary<string, decimal> d,string DriverCode)
        {
            bool res = false;
            if (DriverCode == null || DriverCode.Equals(""))
            {
                return res;
            }
            List<ConnectSql_mb.cmdParameters> list = null;
            string sql = string.Format(@"select SequenceId as Id,ChgcodeId as c,ChgcodeDes as n from XXChgCode where ChgTypeId='INCENTIVE'");
            DataTable dt_incentive = ConnectSql_mb.GetDataTable(sql);

            sql = string.Format(@"select det2.Id,job.JobNo,job.JobType,det1.ContainerNo,det1.ContainerType,det2.DriverCode
from ctm_jobdet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.Id=@TripId");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@TripId", TripId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", dt.Rows[0]["JobNo"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@JobType", dt.Rows[0]["JobType"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContNo", dt.Rows[0]["ContainerNo"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContType", dt.Rows[0]["ContainerType"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", DriverCode, SqlDbType.NVarChar, 100));


            ConnectSql_mb.cmdParameters ChgCode = new ConnectSql_mb.cmdParameters("@ChgCode", "", SqlDbType.NVarChar, 100);
            list.Add(ChgCode);
            ConnectSql_mb.cmdParameters ChgCodeDes = new ConnectSql_mb.cmdParameters("@ChgCodeDes", "", SqlDbType.NVarChar, 100);
            list.Add(ChgCodeDes);
            ConnectSql_mb.cmdParameters Price = new ConnectSql_mb.cmdParameters("@Price", 0, SqlDbType.Decimal);
            list.Add(Price);
            ConnectSql_mb.cmdParameters LineType = new ConnectSql_mb.cmdParameters("@LineType", "DP", SqlDbType.NVarChar, 10);
            list.Add(LineType);

            cost_row2list_foreach(d, dt_incentive, list, ChgCode, ChgCodeDes, Price, true);

            return res;
        }

        //public static bool Claims_Save(int TripId, decimal DHC, decimal WEIGHING, decimal WASHING, decimal REPAIR, decimal DETENTION, decimal DEMURRAGE, decimal LIFT_ON_OFF, decimal C_SHIPMENT, decimal EMF, decimal OTHER)
        public static bool Claims_Save(int TripId, Dictionary<string, decimal> d)
        {
            bool res = false;
            List<ConnectSql_mb.cmdParameters> list = null;
            string sql = string.Format(@"select SequenceId as Id,ChgcodeId as c,ChgcodeDes as n from XXChgCode where ChgTypeId='CLAIMS'");
            DataTable dt_claims = ConnectSql_mb.GetDataTable(sql);

            sql = string.Format(@"select det2.Id,job.JobNo,job.JobType,det1.ContainerNo,det1.ContainerType,det2.DriverCode
from ctm_jobdet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.Id=@TripId");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@TripId", TripId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", dt.Rows[0]["JobNo"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@JobType", dt.Rows[0]["JobType"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContNo", dt.Rows[0]["ContainerNo"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContType", dt.Rows[0]["ContainerType"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", dt.Rows[0]["DriverCode"], SqlDbType.NVarChar, 100));


            ConnectSql_mb.cmdParameters ChgCode = new ConnectSql_mb.cmdParameters("@ChgCode", "", SqlDbType.NVarChar, 100);
            list.Add(ChgCode);
            ConnectSql_mb.cmdParameters ChgCodeDes = new ConnectSql_mb.cmdParameters("@ChgCodeDes", "", SqlDbType.NVarChar, 100);
            list.Add(ChgCodeDes);
            ConnectSql_mb.cmdParameters Price = new ConnectSql_mb.cmdParameters("@Price", 0, SqlDbType.Decimal);
            list.Add(Price);
            ConnectSql_mb.cmdParameters LineType = new ConnectSql_mb.cmdParameters("@LineType", "CL", SqlDbType.NVarChar, 10);
            list.Add(LineType);

            cost_row2list_foreach(d, dt_claims, list, ChgCode, ChgCodeDes, Price,true);

            return res;
        }

        public static bool Billing_Save(int TripId, Dictionary<string, decimal> d)
        {
            bool res = false;
            List<ConnectSql_mb.cmdParameters> list = null;
            string sql = string.Format(@"select SequenceId as Id,ChgcodeId as c,ChgcodeDes as n from XXChgCode where ChgTypeId='CLAIMS'");
            DataTable dt_claims = ConnectSql_mb.GetDataTable(sql);

            sql = string.Format(@"select det2.Id,job.JobNo,job.JobType,det1.ContainerNo,det1.ContainerType,det2.TotalHour,det2.OtHour,job.ClientId,job.JobType,det2.DriverCode
from ctm_jobdet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.Id=@TripId");
            list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@TripId", TripId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);


            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", dt.Rows[0]["JobNo"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@JobType", dt.Rows[0]["JobType"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContNo", dt.Rows[0]["ContainerNo"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ContType", dt.Rows[0]["ContainerType"], SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", "", SqlDbType.NVarChar, 100));

            ConnectSql_mb.cmdParameters ChgCode = new ConnectSql_mb.cmdParameters("@ChgCode", "", SqlDbType.NVarChar, 100);
            list.Add(ChgCode);
            ConnectSql_mb.cmdParameters ChgCodeDes = new ConnectSql_mb.cmdParameters("@ChgCodeDes", "", SqlDbType.NVarChar, 100);
            list.Add(ChgCodeDes);
            ConnectSql_mb.cmdParameters Price = new ConnectSql_mb.cmdParameters("@Price", 0, SqlDbType.Decimal);
            list.Add(Price);
            ConnectSql_mb.cmdParameters LineType = new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 10);
            list.Add(LineType);

            cost_row2list_foreach(d, dt_claims, list, ChgCode, ChgCodeDes, Price,false);

            return res;
        }

        private static void cost_row2list_foreach(Dictionary<string, decimal> d, DataTable dt_cost, List<ConnectSql_mb.cmdParameters> list, ConnectSql_mb.cmdParameters ChgCode, ConnectSql_mb.cmdParameters ChgCodeDes, ConnectSql_mb.cmdParameters Price,Boolean hasDriver)
        {
            string sql_select = string.Format(@"select Price from job_cost where ChgCode=@ChgCode and LineType=@LineType and TripNo=@TripId");
            string sql_insert = string.Format(@"insert into job_cost 
(LineId,LineType,JobNo,JobType,ContNo,ContType,TripNo,VendorId,ChgCode,ChgCodeDes,Remark,Qty,Price,CurrencyId,ExRate,DocAmt,LocAmt,CompanyId,LineSource,GstType,DriverCode)
values(0,@LineType,@JobNo,@JobType,@ContNo,@ContType,@TripId,'',@ChgCode,@ChgCodeDes,'',1,@Price,'SGD',1,@Price,@Price,0,'S',@GstType,@DriverCode)");
            string sql_delete = string.Format(@"delete from job_cost where ChgCode=@ChgCode and LineType=@LineType and TripNo=@TripId");
            string sql_update = string.Format(@"update job_cost set Qty=1,Price=@Price,DocAmt=@Price,LocAmt=@Price where ChgCode=@ChgCode and LineType=@LineType and TripNo=@TripId");


            if (hasDriver) {
                sql_select = string.Format(@"select Price from job_cost where ChgCode=@ChgCode and LineType=@LineType and TripNo=@TripId and (DriverCode=@DriverCode or isnull(DriverCode,'')='')");
                sql_delete = string.Format(@"delete from job_cost where ChgCode=@ChgCode and LineType=@LineType and TripNo=@TripId and (DriverCode=@DriverCode or isnull(DriverCode,'')='')");
                sql_update = string.Format(@"update job_cost set Qty=1,Price=@Price,DocAmt=@Price,LocAmt=@Price,DriverCode=@DriverCode where ChgCode=@ChgCode and LineType=@LineType and TripNo=@TripId and (DriverCode=@DriverCode or isnull(DriverCode,'')='')");
            }
            DataTable dt = null;
            ConnectSql_mb.cmdParameters gstType = new ConnectSql_mb.cmdParameters("@GstType", "", SqlDbType.NVarChar, 10);
            list.Add(gstType);
            foreach (string key in d.Keys)
            {
                decimal val = d[key];

                ChgCode.value = key;
                string key_des = key;

                string sql_chgCode = string.Format(@"select top 1 ChgcodeDes,GstTypeId from XXChgCode where ChgcodeId=@ChgcodeId");
                List<ConnectSql_mb.cmdParameters> list_chgcode = new List<ConnectSql_mb.cmdParameters>();
                list_chgcode.Add(new ConnectSql_mb.cmdParameters("@ChgcodeId", key, SqlDbType.NVarChar, 100));
                DataTable dt_chgCode = ConnectSql_mb.GetDataTable(sql_chgCode, list_chgcode);
                if (dt_chgCode.Rows.Count > 0)
                {
                    DataRow row = dt_chgCode.Rows[0];
                    key_des = row["ChgcodeDes"].ToString();
                    gstType.value = row["GstTypeId"].ToString();
                }

                for (int i = 0; i < dt_cost.Rows.Count; i++)
                {
                    if (key == dt_cost.Rows[i]["c"].ToString())
                    {
                        key_des = dt_cost.Rows[i]["n"].ToString();
                    }
                }
                ChgCodeDes.value = key_des;
                Price.value = val;

                dt = ConnectSql_mb.GetDataTable(sql_select, list);
                if (val != 0)
                {
                    if (dt.Rows.Count != 1)
                    {
                        if (dt.Rows.Count > 1)
                        {
                            ConnectSql_mb.ExecuteNonQuery(sql_delete, list);
                        }
                        ConnectSql_mb.ExecuteNonQuery(sql_insert, list);
                    }
                    else
                    {
                        if (val != SafeValue.SafeDecimal(dt.Rows[0]["Price"], 0))
                        {
                            ConnectSql_mb.ExecuteNonQuery(sql_update, list);
                        }
                    }
                }
                else
                {
                    if (dt.Rows.Count != 0)
                    {
                        ConnectSql_mb.ExecuteNonQuery(sql_delete, list);
                    }
                }
            }

        }


        public static void cost_ClaimBuildToCustomer(int tripId, Dictionary<string, string> d)
        {
            cost_buildToCustomer(tripId, "CL", d);
        }
        private static void cost_buildToCustomer(int tripId,string type,Dictionary<string,string> d)
        {
            string sql = string.Format(@"update job_cost set NotBuildCustomer=@NotBuildCustomer where TripNo=@TripId and LineType=@LineType and ChgCode=@Code");
            foreach(string key in d.Keys)
            {
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", tripId, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", type, SqlDbType.NVarChar, 10));
                list.Add(new ConnectSql_mb.cmdParameters("@Code", key, SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@NotBuildCustomer", d[key], SqlDbType.NVarChar, 10));
                ConnectSql_mb.ExecuteNonQuery(sql, list);
            }
        }

        #endregion

        public static bool tripStatusChanged(int tripId)
        {
            bool res = false;
            string sql = string.Format(@"select det2.Id,det2.Statuscode,job.JobType,job.IsWarehouse,det2.TripCode,det2.Det1Id
from CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.Id=@tripId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

            if (dt.Rows.Count > 0)
            {
                string JobType = dt.Rows[0]["JobType"].ToString();
                string status = dt.Rows[0]["Statuscode"].ToString();
                string isWarehouse = dt.Rows[0]["IsWarehouse"].ToString();
                string tripCode = dt.Rows[0]["TripCode"].ToString();
                int contId = Helper.Safe.SafeInt(dt.Rows[0]["Det1Id"]);
                if (JobType == "CRA")
                {
                    res = tripStatusChanged_Crane(tripId);
                }
                else
                {
                    if (status == "C")
                    {
                        res = tripStatusCompleted_createCode(tripId);
                    }
                    if (JobType == "IMP" || JobType == "EXP" || JobType == "LOC")
                        contStatus_update(contId, tripCode, status, isWarehouse);
                    if (JobType == "WGR" || JobType == "WDO")
                        trailerStatus_update(contId, tripCode, status, JobType);
                    if (JobType == "TPT")
                    {
                        transport_Status_update(contId, tripCode, status, JobType);
                    }
                }
            }
            return res;
        }

        private static bool tripStatusCompleted_createCode(int tripId)
        {
            bool res = false;
            string sql = null;
            sql = string.Format(@"select det2.Id,det2.Statuscode,job.JobType,det1.ContainerNo,det1.ContainerType,det2.JobNo,job.ClientId 
from CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
left outer join CTM_JobDet1 as det1 on det2.det1Id=det1.Id
where det2.Id=@tripId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
            DataTable dt_job = ConnectSql_mb.GetDataTable(sql, list);
            if (dt_job.Rows.Count > 0)
            {
                res = true;
                string JobNo = dt_job.Rows[0]["JobNo"].ToString();
                string JobType = dt_job.Rows[0]["JobType"].ToString();
                string ClientId = dt_job.Rows[0]["ClientId"].ToString();
                string ContainerNo = dt_job.Rows[0]["ContainerNo"].ToString();
                string ContainerType = dt_job.Rows[0]["ContainerType"].ToString();
                string BillClass = "TRUCKING";
                if (JobType == "WGR" || JobType == "WDO" || JobType == "TPT")
                {
                    BillClass = "TRANSPORT";
                }
                //if (JobType == "CRA")
                //{
                //    BillClass = "CRANE";
                //}



                //=========== job level cost creat
                if (JobType == "WGR" || JobType == "WDO" || JobType == "TPT")
                {
                    CtmJob.jobCost_Create(JobNo, JobType, ClientId, BillClass);
                }

                //=========== trip level cost 
                try
                {
                    sql = string.Format(@"select Id from job_cost where LineType=@LineType and JobNo=@JobNo and BillClass=@BillClass and JobType=@JobType and LineSource=@LineSource");
                    list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@LineSource", "S", SqlDbType.NVarChar, 10));
                    list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));

                    DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
                    if (dt.Rows.Count <= 0)
                    {
                        list = new List<ConnectSql_mb.cmdParameters>();
                        sql = string.Format(@"select ChgCode,ChgCodeDes,Qty,Price,BillClass,BillScope,CurrencyId,ExRate 
from job_rate where ClientId=@ClientId and BillScope=@LineType and JobType=@JobType and BillClass=@BillClass and LineStatus=@LineStatus");
                        list.Add(new ConnectSql_mb.cmdParameters("@ClientId", ClientId, SqlDbType.NVarChar, 100));
                        list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 30));
                        list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                        list.Add(new ConnectSql_mb.cmdParameters("@LineStatus", "N", SqlDbType.NVarChar, 10));
                        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));
                        dt = ConnectSql_mb.GetDataTable(sql, list);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string chgCode = SafeValue.SafeString(dt.Rows[i]["ChgCode"]);
                            string chgCodeDes = SafeValue.SafeString(dt.Rows[i]["ChgCodeDes"]);
                            decimal price = SafeValue.SafeDecimal(dt.Rows[i]["Price"]);
                            decimal qty = SafeValue.SafeDecimal(dt.Rows[i]["Qty"]);
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
                            list = new List<ConnectSql_mb.cmdParameters>();
                            sql = string.Format(@"insert into job_cost (JobNo,JobType,ChgCode,ChgCodeDes,Price,Qty,CurrencyId,ExRate,LocAmt,LineSource,LineType,BillClass,ContNo,ContType)
values(@JobNo,@JobType,@ChgCode,@ChgCodeDes,@Price,@Qty,@CurrencyId,@ExRate,@LocAmt,@LineSource,@LineType,@BillClass,@ContainerNo,@ContainerType)");
                            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
                            list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));
                            list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", chgCode, SqlDbType.NVarChar, 100));
                            list.Add(new ConnectSql_mb.cmdParameters("@ChgCodeDes", chgCodeDes, SqlDbType.NVarChar, 300));
                            list.Add(new ConnectSql_mb.cmdParameters("@Price", price, SqlDbType.Decimal));
                            list.Add(new ConnectSql_mb.cmdParameters("@Qty", 1, SqlDbType.Int));
                            decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
                            decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
                            decimal docAmt = amt;
                            decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(1, 1), 2);
                            list.Add(new ConnectSql_mb.cmdParameters("@LocAmt", locAmt, SqlDbType.Decimal));
                            list.Add(new ConnectSql_mb.cmdParameters("@CurrencyId", System.Configuration.ConfigurationManager.AppSettings["Currency"], SqlDbType.NVarChar, 3));
                            list.Add(new ConnectSql_mb.cmdParameters("@ExRate", 1, SqlDbType.Decimal));
                            list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", ContainerNo, SqlDbType.NVarChar, 100));
                            list.Add(new ConnectSql_mb.cmdParameters("@ContainerType", ContainerType, SqlDbType.NVarChar, 100));
                            list.Add(new ConnectSql_mb.cmdParameters("@LineSource", "S", SqlDbType.NVarChar, 100));
                            list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 100));
                            list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 100));
                            ConnectSql_mb.ExecuteNonQuery(sql, list);

                        }


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            return res;
        }

        static bool tripStatusChanged_Crane(int tripId)
        {
            bool res = false;
            string sql = null;
            sql = string.Format(@"select det2.Id,det2.Statuscode,job.JobType,det1.ContainerNo,det1.ContainerType,det2.TotalHour,det2.OtHour,det2.JobNo,job.ClientId,TowheadCode 
from CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo
left outer join CTM_JobDet1 as det1 on det2.det1Id=det1.Id
where det2.Id=@tripId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
            DataTable dt_job = ConnectSql_mb.GetDataTable(sql, list);
            if (dt_job.Rows.Count > 0)
            {
                res = true;
                string JobNo = dt_job.Rows[0]["JobNo"].ToString();
                string JobType = dt_job.Rows[0]["JobType"].ToString();
                string ClientId = dt_job.Rows[0]["ClientId"].ToString();
                string ContainerNo = dt_job.Rows[0]["ContainerNo"].ToString();
                string ContainerType = dt_job.Rows[0]["ContainerType"].ToString();
                string BillClass = "CRANE";

                decimal TotalHour = SafeValue.SafeDecimal(dt_job.Rows[0]["TotalHour"]);
                decimal OtHour = SafeValue.SafeDecimal(dt_job.Rows[0]["OtHour"]);
                string towheadCode = dt_job.Rows[0]["TowheadCode"].ToString();


                //=========== job level cost creat
                CtmJob.jobCost_Create(JobNo, JobType, ClientId, BillClass);

                //=========== trip level cost 

                string sql_vehicle = string.Format(@"select ContractNo from Ref_Vehicle where VehicleCode='{0}'", towheadCode);
                string vehicle = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql_vehicle));
                sql = string.Format(@"select ChgCode,ChgCodeDes,Qty,Price,BillClass,BillScope,CurrencyId,ExRate,VehicleType
from job_rate where ClientId=@ClientId and BillScope=@LineType and JobType=@JobType and BillClass=@BillClass and LineStatus=@LineStatus and substring(VehicleType,1,2)=@VehicleType");
                list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@ClientId", ClientId, SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 30));
                list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@LineStatus", "N", SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@VehicleType", (vehicle.Length >= 2 ? vehicle.Substring(0, 2) : ""), SqlDbType.NVarChar, 100));
                DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string chgCode = SafeValue.SafeString(dt.Rows[i]["ChgCode"]);
                    string chgCodeDes = SafeValue.SafeString(dt.Rows[i]["ChgCodeDes"]);
                    decimal price = SafeValue.SafeDecimal(dt.Rows[i]["Price"]);
                    decimal qty = SafeValue.SafeDecimal(dt.Rows[i]["Qty"]);
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
                    string sql_c = string.Format(@"select Id from job_cost where LineType=@LineType and JobNo=@JobNo and JobType=@JobType and LineSource=@LineSource and ChgCode=@ChgCode");
                    list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 30));
                    list.Add(new ConnectSql_mb.cmdParameters("@LineSource", "S", SqlDbType.NVarChar, 10));
                    list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@ChgCode", chgCode, SqlDbType.NVarChar, 100));
                    DataTable dt_cost = ConnectSql_mb.GetDataTable(sql_c, list);
                    if (dt_cost.Rows.Count <= 0)
                    {


                        sql = string.Format(@"insert into job_cost (JobNo,JobType,ChgCode,ChgCodeDes,Price,Qty,CurrencyId,ExRate,LocAmt,LineSource,LineType,BillClass,ContNo,ContType,RowCreateUser,RowCreateTime,RowUpdateUser,RowUpdateTime,LineId,DocAmt,Gst,GstAmt,GstType,TripNo)
values(@JobNo,@JobType,@ChgCode,@ChgCodeDes,@Price,@Qty,@CurrencyId,@ExRate,@LocAmt,@LineSource,@LineType,@BillClass,@ContainerNo,@ContainerType,@UserId,@DateTime,@UserId,@DateTime,0,@DocAmt,@Gst,@GstAmt,@GstType,@TripNo)");
                        list = new List<ConnectSql_mb.cmdParameters>();
                        list.Add(new ConnectSql_mb.cmdParameters("@LineType", "TRIP", SqlDbType.NVarChar, 30));
                        list.Add(new ConnectSql_mb.cmdParameters("@BillClass", BillClass, SqlDbType.NVarChar, 30));
                        list.Add(new ConnectSql_mb.cmdParameters("@LineSource", "S", SqlDbType.NVarChar, 10));
                        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
                        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 100));

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
                        list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", ContainerNo, SqlDbType.NVarChar, 100));
                        list.Add(new ConnectSql_mb.cmdParameters("@ContainerType", ContainerType, SqlDbType.NVarChar, 100));
                        string userId = HttpContext.Current.User.Identity.Name;
                        list.Add(new ConnectSql_mb.cmdParameters("@UserId", userId, SqlDbType.NVarChar, 100));
                        list.Add(new ConnectSql_mb.cmdParameters("@DateTime", DateTime.Now, SqlDbType.NVarChar, 100));
                        list.Add(new ConnectSql_mb.cmdParameters("@DocAmt", docAmt, SqlDbType.Decimal));
                        list.Add(new ConnectSql_mb.cmdParameters("@Gst", gst, SqlDbType.Decimal));
                        list.Add(new ConnectSql_mb.cmdParameters("@GstAmt", gstAmt, SqlDbType.Decimal));
                        list.Add(new ConnectSql_mb.cmdParameters("@GstType", gstType, SqlDbType.NVarChar));
                        list.Add(new ConnectSql_mb.cmdParameters("@TripNo", tripId, SqlDbType.NVarChar));

                        ConnectSql_mb.ExecuteNonQuery(sql, list);
                    }


                }

                //================= total hour, Over time hours


            }


            return res;
        }
        public static bool contStatus_update(int contId, string tripCode, string statusCode, string isWarehouse)
        {
            bool res = false;
            string sql = "";
            string status = "";
            if (isWarehouse == "Yes")
            {
                if (tripCode == "COL")
                {
                    if (statusCode == "C")
                        status = "WHS-MT";
                }
                if (tripCode == "IMP")
                {
                    if (statusCode == "C")
                        status = "WHS-LD";
                }
                if (tripCode == "EXP")
                {
                    if (statusCode == "C")
                        status = "Completed";
                }
                if (tripCode == "RET")
                {
                    if (statusCode == "S")
                        status = "Return";
                    if (statusCode == "C")
                        status = "Completed";
                }
                if (status.Length > 0)
                {
                    sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", contId, status);
                    ConnectSql_mb.ExecuteNonQuery(sql);

                    edi_contStatus_update(contId, status);

                    res = true;
                }
            }
            if (isWarehouse == "No")
            {
                if (tripCode == "IMP")
                {
                    if (statusCode == "C")
                        status = "Customer-LD";
                    else
                        status = "Import";
                }
                if (tripCode == "EXP")
                {
                    if (statusCode == "C")
                        status = "Customer-MT";
                    else
                        status = "Collection";
                }
                if (tripCode == "RET")
                {
                    if (statusCode == "S")
                        status = "Return";
                    if (statusCode == "C")
                        status = "Completed";
                }
                if (status.Length > 0)
                {
                    sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", contId, status);
                    ConnectSql_mb.ExecuteNonQuery(sql);

                    edi_contStatus_update(contId, status);
                    res = true;
                }
            }

            return res;
        }
        public static bool trailerStatus_update(int contId, string tripCode, string statusCode, string jobType)
        {
            bool res = false;
            string sql = "";
            string status = "";
            if (jobType == "WGR")
            {
                if (tripCode == "LOC")
                {
                    if (statusCode == "C")
                        status = "Delivered";
                }
                if (tripCode == "SHF")
                {
                    if (statusCode == "C")
                        status = "Returned";
                }
                if (status.Length > 0)
                {
                    sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", contId, status);
                    ConnectSql_mb.ExecuteNonQuery(sql);

                    edi_contStatus_update(contId, status);

                    res = true;
                }
            }
            if (jobType == "WDO")
            {
                if (tripCode == "SHF")
                {
                    if (statusCode == "C")
                        status = "Arrival";
                }
                if (tripCode == "LOC")
                {
                    if (statusCode == "C")
                        status = "Delivered";
                }
                if (status.Length > 0)
                {
                    sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", contId, status);
                    ConnectSql_mb.ExecuteNonQuery(sql);

                    edi_contStatus_update(contId, status);
                    res = true;
                }
            }

            return res;
        }
        public static bool transport_Status_update(int contId, string tripCode, string statusCode, string jobType)
        {
            bool res = false;
            string sql = "";
            string status = "";
            if (statusCode == "C")
            {
                status = "Completed";
            }
            else
            {
                status = "Start";
            }
            if (status.Length > 0)
            {
                sql = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", contId, status);
                ConnectSql_mb.ExecuteNonQuery(sql);

                edi_contStatus_update(contId, status);

                res = true;
            }

            return res;
        }
        static bool edi_contStatus_update(int contId, string status)
        {
            bool res = false;
            string sql = string.Format(@"select det1.ContainerNo,job.ClientRefNo from CTM_JobDet1 det1 inner join CTM_Job job on det1.JobNo=job.JobNo where det1.Id=@contId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@contId", contId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                string contNo = SafeValue.SafeString(dt.Rows[0]["ContainerNo"]);
                string clientRefNo = SafeValue.SafeString(dt.Rows[0]["ClientRefNo"]);

                sql = string.Format(@"select det1.Id from ctm_jobdet1 det1 inner join ctm_job job on det1.JobNo=job.JobNo where ContainerNo=@ContainerNo and job.JobNo=@JobNo");
                List<ConnectEdi.cmdParameters> list1 = new List<ConnectEdi.cmdParameters>();
                list1.Add(new ConnectEdi.cmdParameters("@ContainerNo", contNo, SqlDbType.VarChar));
                list1.Add(new ConnectEdi.cmdParameters("@JobNo", clientRefNo, SqlDbType.VarChar));
                ConnectEdi.sqlResult sql_res = ConnectEdi.ExecuteScalar_n(sql, list1);
                if (sql_res.status)
                {
                    int edi_contId = SafeValue.SafeInt(sql_res.context, 0);
                    string sql_update = string.Format(@"update ctm_jobdet1 set StatusCode='{1}' where Id={0}", edi_contId, status);
                    ConnectEdi.ExecuteNonQuery(sql_update);
                    res = true;
                }
            }
            return res;
        }
        public static string getTripIndex(string jobno, string trip_JobType)
        {
            string res = "";
            res = jobno.Substring(0, 4) + jobno.Substring(7, 4) + jobno.Substring(13);
            string sql = "";
            if (trip_JobType == "WGR" || trip_JobType == "WDO" || trip_JobType == "TPT")
            {
                sql = string.Format(@"select max(right(TripIndex,2)) from CTM_JobDet2 where JobType in ('WGR','WDO','TPT') and JobNo=@JobNo");
            }
            if (trip_JobType == "CRA")
            {
                sql = string.Format(@"select max(right(TripIndex,2)) from CTM_JobDet2 where JobType=@JobType and JobNo=@JobNo");
            }
            
            if (sql != "")
            {
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobno, SqlDbType.NVarChar, 100));
                list.Add(new ConnectSql_mb.cmdParameters("@JobType", trip_JobType, SqlDbType.NVarChar, 100));
                string maxIdex = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql, list).context, "//00");
                int n = SafeValue.SafeInt(maxIdex.Substring(maxIdex.LastIndexOf("/") + 1), 0) + 1;
                string str = (100 + n).ToString().Substring(1);
                res = res + "/" + trip_JobType + "/" + str;
            }
            return res;
        }
    }

}

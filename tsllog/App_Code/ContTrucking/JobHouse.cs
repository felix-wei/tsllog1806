using System;
using System.Data;

namespace C2
{
	public class JobHouse
	{
		private int id;
		private string refNo;
		private string jobNo;
		private string contNo;
		private int lineId;
		private string jobType;
		private string transJobOrder;
		private string transImpId;
		private string bookingNo;
		private string hblNo;
		private string doNo;
		private string skuCode;
		private string uomCode;
		private decimal weight;
		private decimal volume;
        private decimal qty;
		private string cargoType;
		private string marking1;
		private string marking2;
		private string remark1;
		private string remark2;
		private string desc1;
		private string desc2;
		private string tallyBox;
		private bool isDg;
		private bool isOverLength;
		private string dgClass;
		private DateTime entryDate;
		private string cargoStatus;
		private bool isHold;
		private bool isBill;
		private bool isNormal;
        private decimal qtyOrig;
		private string packTypeOrig;
		private decimal weightOrig;
		private decimal volumeOrig;
		private bool isLong;
		private string recDate;
		private string recUser;
		private string recNote;
		private string checkInDate;
		private string commodity;
		private string commodityRmk;
		private string landStatus;
		private int balanceQty1;
		private string pod;
		private string clientId;
		private string clientContact;
		private string clientEmail;
		private string shipperInfo;
		private string shipperRemark;
		private string shipperEmail;
		private string consigneeInfo;
		private string consigneeRemark;
		private string consigneeEmail;
		private string notifyInfo;
		private string notifyRemark;
		private string notifyEmail;
		private string transportNeed;
		private string transportCode;
		private string transportRemark;
		private string transportEmail;
		private string vessel;
		private string voyage;
		private string pol;
		private DateTime eta;
		private DateTime etd;
		private string carrierCode;
		private string email1;
		private string email2;
		private string prepaidInd;
		private decimal collectAmount1;
		private decimal collectAmount2;
		private string dutyPayment;
		private string incoterm;
		private string etaTime;
		private string etdTime;
		private string tel1;
		private string tel2;
		private string mobile1;
		private string mobile2;
		private string responsible;

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

		public string ContNo
		{
			get { return this.contNo; }
			set { this.contNo = value; }
		}

		public int LineId
		{
			get { return this.lineId; }
			set { this.lineId = value; }
		}

		public string JobType
		{
			get { return this.jobType; }
			set { this.jobType = value; }
		}

		public string TransJobOrder
		{
			get { return this.transJobOrder; }
			set { this.transJobOrder = value; }
		}

		public string TransImpId
		{
			get { return this.transImpId; }
			set { this.transImpId = value; }
		}

		public string BookingNo
		{
			get { return this.bookingNo; }
			set { this.bookingNo = value; }
		}

		public string HblNo
		{
			get { return this.hblNo; }
			set { this.hblNo = value; }
		}

		public string DoNo
		{
			get { return this.doNo; }
			set { this.doNo = value; }
		}

		public string SkuCode
		{
			get { return this.skuCode; }
			set { this.skuCode = value; }
		}

		public string UomCode
		{
			get { return this.uomCode; }
			set { this.uomCode = value; }
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

        public decimal Qty
		{
			get { return this.qty; }
			set { this.qty = value; }
		}

		public string CargoType
		{
			get { return this.cargoType; }
			set { this.cargoType = value; }
		}

		public string Marking1
		{
			get { return this.marking1; }
			set { this.marking1 = value; }
		}

		public string Marking2
		{
			get { return this.marking2; }
			set { this.marking2 = value; }
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

		public string Desc1
		{
			get { return this.desc1; }
			set { this.desc1 = value; }
		}

		public string Desc2
		{
			get { return this.desc2; }
			set { this.desc2 = value; }
		}

		public string TallyBox
		{
			get { return this.tallyBox; }
			set { this.tallyBox = value; }
		}

		public bool IsDg
		{
			get { return this.isDg; }
			set { this.isDg = value; }
		}

		public bool IsOverLength
		{
			get { return this.isOverLength; }
			set { this.isOverLength = value; }
		}

		public string DgClass
		{
			get { return this.dgClass; }
			set { this.dgClass = value; }
		}

		public DateTime EntryDate
		{
			get { return this.entryDate; }
			set { this.entryDate = value; }
		}

		public string CargoStatus
		{
			get { return this.cargoStatus; }
			set { this.cargoStatus = value; }
		}

		public bool IsHold
		{
			get { return this.isHold; }
			set { this.isHold = value; }
		}

		public bool IsBill
		{
			get { return this.isBill; }
			set { this.isBill = value; }
		}

		public bool IsNormal
		{
			get { return this.isNormal; }
			set { this.isNormal = value; }
		}

        public decimal QtyOrig
		{
			get { return this.qtyOrig; }
			set { this.qtyOrig = value; }
		}

		public string PackTypeOrig
		{
			get { return this.packTypeOrig; }
			set { this.packTypeOrig = value; }
		}

		public decimal WeightOrig
		{
			get { return this.weightOrig; }
			set { this.weightOrig = value; }
		}

		public decimal VolumeOrig
		{
			get { return this.volumeOrig; }
			set { this.volumeOrig = value; }
		}

		public bool IsLong
		{
			get { return this.isLong; }
			set { this.isLong = value; }
		}

		public string RecDate
		{
			get { return this.recDate; }
			set { this.recDate = value; }
		}

		public string RecUser
		{
			get { return this.recUser; }
			set { this.recUser = value; }
		}

		public string RecNote
		{
			get { return this.recNote; }
			set { this.recNote = value; }
		}

		public string CheckInDate
		{
			get { return this.checkInDate; }
			set { this.checkInDate = value; }
		}

		public string Commodity
		{
			get { return this.commodity; }
			set { this.commodity = value; }
		}

		public string CommodityRmk
		{
			get { return this.commodityRmk; }
			set { this.commodityRmk = value; }
		}

		public string LandStatus
		{
			get { return this.landStatus; }
			set { this.landStatus = value; }
		}

		public int BalanceQty1
		{
			get { return this.balanceQty1; }
			set { this.balanceQty1 = value; }
		}

		public string Pod
		{
			get { return this.pod; }
			set { this.pod = value; }
		}

		public string ClientId
		{
			get { return this.clientId; }
			set { this.clientId = value; }
		}

		public string ClientContact
		{
			get { return this.clientContact; }
			set { this.clientContact = value; }
		}

		public string ClientEmail
		{
			get { return this.clientEmail; }
			set { this.clientEmail = value; }
		}

		public string ShipperInfo
		{
			get { return this.shipperInfo; }
			set { this.shipperInfo = value; }
		}

		public string ShipperRemark
		{
			get { return this.shipperRemark; }
			set { this.shipperRemark = value; }
		}

		public string ShipperEmail
		{
			get { return this.shipperEmail; }
			set { this.shipperEmail = value; }
		}

		public string ConsigneeInfo
		{
			get { return this.consigneeInfo; }
			set { this.consigneeInfo = value; }
		}

		public string ConsigneeRemark
		{
			get { return this.consigneeRemark; }
			set { this.consigneeRemark = value; }
		}

		public string ConsigneeEmail
		{
			get { return this.consigneeEmail; }
			set { this.consigneeEmail = value; }
		}

		public string NotifyInfo
		{
			get { return this.notifyInfo; }
			set { this.notifyInfo = value; }
		}

		public string NotifyRemark
		{
			get { return this.notifyRemark; }
			set { this.notifyRemark = value; }
		}

		public string NotifyEmail
		{
			get { return this.notifyEmail; }
			set { this.notifyEmail = value; }
		}

		public string TransportNeed
		{
			get { return this.transportNeed; }
			set { this.transportNeed = value; }
		}

		public string TransportCode
		{
			get { return this.transportCode; }
			set { this.transportCode = value; }
		}

		public string TransportRemark
		{
			get { return this.transportRemark; }
			set { this.transportRemark = value; }
		}

		public string TransportEmail
		{
			get { return this.transportEmail; }
			set { this.transportEmail = value; }
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

		public DateTime Eta
		{
			get { return this.eta; }
			set { this.eta = value; }
		}

		public DateTime Etd
		{
			get { return this.etd; }
			set { this.etd = value; }
		}

		public string CarrierCode
		{
			get { return this.carrierCode; }
			set { this.carrierCode = value; }
		}

		public string Email1
		{
			get { return this.email1; }
			set { this.email1 = value; }
		}

		public string Email2
		{
			get { return this.email2; }
			set { this.email2 = value; }
		}

		public string PrepaidInd
		{
			get { return this.prepaidInd; }
			set { this.prepaidInd = value; }
		}

		public decimal CollectAmount1
		{
			get { return this.collectAmount1; }
			set { this.collectAmount1 = value; }
		}

		public decimal CollectAmount2
		{
			get { return this.collectAmount2; }
			set { this.collectAmount2 = value; }
		}

		public string DutyPayment
		{
			get { return this.dutyPayment; }
			set { this.dutyPayment = value; }
		}

		public string Incoterm
		{
			get { return this.incoterm; }
			set { this.incoterm = value; }
		}

		public string EtaTime
		{
			get { return this.etaTime; }
			set { this.etaTime = value; }
		}

		public string EtdTime
		{
			get { return this.etdTime; }
			set { this.etdTime = value; }
		}

		public string Tel1
		{
			get { return this.tel1; }
			set { this.tel1 = value; }
		}

		public string Tel2
		{
			get { return this.tel2; }
			set { this.tel2 = value; }
		}

		public string Mobile1
		{
			get { return this.mobile1; }
			set { this.mobile1 = value; }
		}

		public string Mobile2
		{
			get { return this.mobile2; }
			set { this.mobile2 = value; }
		}

		public string Responsible
		{
			get { return this.responsible; }
			set { this.responsible = value; }
		}
        private string opsType;
        public string OpsType
        {
            get { return this.opsType; }
            set { this.opsType = value; }
        }
        private decimal packQty;
        public decimal PackQty
        {
            get { return this.packQty; }
            set { this.packQty = value; }
        }
        private string packUom;
        public string PackUom
        {
            get { return this.packUom; }
            set { this.packUom = value; }
        }
        private decimal wholeQty;
        public decimal WholeQty
        {
            get { return this.wholeQty; }
            set { this.wholeQty = value; }
        }
        private string wholeUom;
        public string WholeUom
        {
            get { return this.wholeUom; }
            set { this.wholeUom = value; }
        }
        private string balanceUom;
        public string BalanceUom
        {
            get { return this.balanceUom; }
            set { this.balanceUom = value; }
        }
        private string location;
        public string Location
        {
            get { return this.location; }
            set { this.location = value; }
        }
        private decimal lengthPack;
        private decimal widthPack;
        private decimal heightPack;
        public decimal LengthPack
        {
            get { return this.lengthPack; }
            set { this.lengthPack = value; }
        }
        public decimal WidthPack
        {
            get { return this.widthPack; }
            set { this.widthPack = value; }
        }
        public decimal HeightPack
        {
            get { return this.heightPack; }
            set { this.heightPack = value; }
        }
        private string damagedStatus;
        public string DamagedStatus
        {
            get { return this.damagedStatus; }
            set { this.damagedStatus = value; }
        }

        private string transferNo;
        public string TransferNo {
            get { return this.transferNo; }
            set { this.transferNo = value; }
        }

        private string orderId;
        public string OrderId
        {
            get { return this.orderId; }
            set { this.orderId = value; }
        }

        private string userId;
        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }
        private decimal gstFee;
        public decimal GstFee
        {
            get { return this.gstFee; }
            set { this.gstFee = value; }
        }
        private decimal permitFee;
        public decimal PermitFee
        {
            get { return this.permitFee; }
            set { this.permitFee = value; }
        }
        private decimal handlingFee;
        public decimal HandlingFee
        {
            get { return this.handlingFee; }
            set { this.handlingFee = value; }
        }

        private decimal otherFee;
        public decimal OtherFee
        {
            get { return this.otherFee; }
            set { this.otherFee = value; }
        }

        private string currency;
        public string Currency
        {
            get { return this.currency; }
            set { this.currency = value; }
        }

        private string bkgSKuCode;
        public string BkgSKuCode
        {
            get { return this.bkgSKuCode; }
            set { this.bkgSKuCode = value; }
        }

        private decimal bkgSkuQty;
        public decimal BkgSkuQty
        {
            get { return this.bkgSkuQty; }
            set { this.bkgSkuQty = value; }
        }
        private string bkgSkuUnit;
        public string BkgSkuUnit
        {
            get { return this.bkgSkuUnit; }
            set { this.bkgSkuUnit = value; }
        }

        private string permitBy;
        public string PermitBy
        {
            get { return this.permitBy; }
            set { this.permitBy = value; }
        }
        private decimal ocean_Freight;
        public decimal Ocean_Freight
        {
            get { return this.ocean_Freight; }
            set { this.ocean_Freight = value; }
        }
        private string sealNo;
        public string SealNo
        {
            get { return this.sealNo; }
            set { this.sealNo = value; }
        }
        private int contId;
        public int ContId
        {
            get { return this.contId; }
            set { this.contId = value; }
        }
        private string bookingItem;
        public string BookingItem
        {
            get { return this.bookingItem; }
            set { this.bookingItem = value; }
        }
        private string actualItem;
        public string ActualItem
        {
            get { return this.actualItem; }
            set { this.actualItem = value; }
        }
        private string tripIndex;
        public string TripIndex
        {
            get { return this.tripIndex; }
            set { this.tripIndex = value; }
        }
        private int tripId;
        public int TripId
        {
            get { return this.tripId; }
            set { this.tripId = value; }
        }
        private string permitNo;
        public string PermitNo
        {
            get {
                string sql = string.Format(@"select PermitNo from ref_permit where HblNo='{0}' and JobNo='{1}'",this.hblNo,this.jobNo);
                this.permitNo = ConnectSql_mb.ExecuteScalar(sql);
                return permitNo;
            }
            set { this.permitNo = value; }
        }
        private string storageType;
        public string StorageType
        {
            get { return this.storageType; }
            set { this.storageType = value; }
        }
        private DateTime stockDate;
        public DateTime StockDate
        {
            get { return this.stockDate; }
            set { this.stockDate = value; }
        }
        private DateTime nextBillDate;
        public DateTime NextBillDate
        {
            get { return this.nextBillDate; }
            set { this.nextBillDate = value; }
        }
        private decimal length;
        private decimal width;
        private decimal height;
        public decimal Length
        {
            get { return this.length; }
            set { this.length = value; }
        }
        public decimal Width
        {
            get { return this.width; }
            set { this.width= value; }
        }
        public decimal Height
        {
            get { return this.height; }
            set { this.height = value; }
        }
        private string palletNo;
        public string PalletNo
        {
            get { return this.palletNo; }
            set { this.palletNo = value; }
        }
        private string cartonNo;
        public string CartonNo
        {
            get { return this.cartonNo; }
            set { this.cartonNo = value; }
        }
        private string mft_LotNo;
        public string Mft_LotNo
        {
            get { return this.mft_LotNo; }
            set { this.mft_LotNo = value; }
        }
        private DateTime mft_LotDate;
        public DateTime Mft_LotDate
        {
            get { return this.mft_LotDate; }
            set { this.mft_LotDate = value; }
        }
        private DateTime mft_ExpiryDate;
        public DateTime Mft_ExpiryDate
        {
            get { return this.mft_ExpiryDate; }
            set { this.mft_ExpiryDate = value; }
        }
        private string onHold;
        public string OnHold
        {
            get { return this.onHold; }
            set { this.onHold = value; }
        }
        private string poNo;
        public string PoNo
        {
            get { return this.poNo; }
            set { this.poNo = value; }
        }

        private DateTime billingStartDate;
        public DateTime BillingStartDate
        {
            get { return this.billingStartDate; }
            set { this.billingStartDate = value; }
        }

        private string inventoryId;
        public string InventoryId
        {
            get { return this.inventoryId; }
            set { this.inventoryId = value; }
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
        public string Role
        {
            get
            {
                string flag = "";
                if (this.jobNo != null)
                {
                    string name = System.Web.HttpContext.Current.User.Identity.Name;
                    if (name.Length > 0)
                    {
                        flag = SafeValue.SafeString(Helper.Sql.One("select Role from [dbo].[User] where Name='" + name + "'"), "");
                    }
                }
                return flag;
            }
        }
        public string Is_Marubeni
        {
            get
            {
                string flag = "N";
                if (this.clientId== "Mar-02"|| this.clientId == "2-Mar")
                {
                    flag = "Y";
                }
                return flag;
            }
        }
        public static void updateLineId(int id)
        {
            string sql = string.Format(@"update job_house set LineId={0} where Id={0}", id);
            ConnectSql.ExecuteSql(sql);
        }
        public decimal BalanceQty
        {
            get
            {
                string sql = string.Format(@"select (tab_in.BalQty-isnull(tab_out.BalQty,0)) as  BalQty  from job_house mast
inner join (select sum(QtyOrig) as BalQty,LineId from job_house where CargoType='IN' and CargoStatus='C'  group by LineId) as tab_in on tab_in.LineId=mast.LineId
left join (select sum(QtyOrig) as BalQty,LineId from job_house where CargoType='OUT' and CargoStatus='C'  group by LineId) as tab_out on tab_out.LineId=mast.LineId
where mast.LineId={0}", this.lineId);
                return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
            }
        }
        public decimal BalanceSkuQty
        {
            get
            {
                string sql = string.Format(@"select (tab_in.BalQty-isnull(tab_out.BalQty,0)) as  BalQty  from job_house mast
inner join (select sum(PackQty) as BalQty,LineId from job_house where CargoType='IN' and CargoStatus='C'  group by LineId) as tab_in on tab_in.LineId=mast.LineId
left join (select sum(PackQty) as BalQty,LineId from job_house where CargoType='OUT' and CargoStatus='C'  group by LineId) as tab_out on tab_out.LineId=mast.LineId
where mast.LineId={0}", this.lineId);
                return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
            }
        }
        
        public decimal BalanceWeight
        {
            get
            {
                string sql = string.Format(@"select (tab_in.BalQty-isnull(tab_out.BalQty,0)) as  BalQty  from job_house mast
inner join (select sum(WeightOrig) as BalQty,LineId from job_house where CargoType='IN' and CargoStatus='C'  group by LineId) as tab_in on tab_in.LineId=mast.LineId
left join (select sum(WeightOrig) as BalQty,LineId from job_house where CargoType='OUT' and CargoStatus='C'  group by LineId) as tab_out on tab_out.LineId=mast.LineId
where mast.LineId={0}", this.lineId);
                return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
            }
        }
        public decimal BalanceVolume
        {
            get
            {
                string sql = string.Format(@"select (tab_in.BalQty-isnull(tab_out.BalQty,0)) as  BalQty  from job_house mast
inner join (select sum(VolumeOrig) as BalQty,LineId from job_house where CargoType='IN' and CargoStatus='C'  group by LineId) as tab_in on tab_in.LineId=mast.LineId
left join (select sum(VolumeOrig) as BalQty,LineId from job_house where CargoType='OUT' and CargoStatus='C'  group by LineId) as tab_out on tab_out.LineId=mast.LineId
where mast.LineId={0}", this.id);
                return SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
            }
        }
        #region Is Invoice
        public string IsHadInvoice
        {
            get
            {
                string value = "NO";
                string partyTo = EzshipHelper.GetPartyId(this.ConsigneeInfo);
                string sql = string.Format(@"select count(*) from XAArInvoice where MastRefNo='{0}' and PartyTo='{1}'", this.jobNo, partyTo);
                int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                if (n > 0)
                {
                    value = "YES";
                }
                sql = string.Format(@"select count(*) from XXParty where PartyId='{0}' and GroupId='CFS'", partyTo);
                n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
                if (n > 0)
                {
                    value = "NO";
                }
                else
                {
                    value = "YES";
                }
                return value;
            }

        }
        #endregion

        #region Stock
        public static DataTable getStockBalance(string jobNo,string client,string date,string hblNo,string lotNo,string sku,string warehouse,string location,string cargoType,string key1,string key2){

            string sql = string.Format(@"select distinct row_number() over (order by mast.Id) as No,job.JobNo,job.JobDate,job.WareHouseCode,tab_wh.IsBonded,(select top 1 IsBonded from ref_warehouse where Code=job.WareHouseCode) as WhsType,job.PartyId,(select top 1 Name from XXParty where PartyId=job.PartyId) as PartyName,
(case when LEN(job.WhPermitNo)>0 then job.WhPermitNo else job.PermitNo end) as PermitNo,job.ClientId,(select top 1 Name from XXParty where PartyId=job.ClientId) as ClientName,job.Vessel,job.Voyage,mast.StockDate,mast.NextBillDate,mast.StorageType,
job.PickupFrom,job.DeliveryTo,mast.Qty,mast.PackTypeOrig,mast.ContNo,mast.BookingNo,mast.OpsType,mast.HblNo,mast.Location,mast.PackQty,mast.RefNo,tab_weight.BalanceWeight,tab_volume.BalanceVolume,mast.LineId,mast.ContId,tab_cont.ContainerType,
mast.PackUom,mast.QtyOrig,mast.WeightOrig,mast.VolumeOrig,mast.LengthPack,mast.WidthPack,mast.HeightPack,mast.SkuCode,mast.Marking1,mast.Marking2,mast.Remark1,mast.LandStatus,mast.DgClass,mast.DamagedStatus,mast.Remark2,tab_bal.BalQty,tab_sku.SkuQty,Desc1,ActualItem,mast.InventoryId  
from job_house mast left join ctm_job job on mast.JobNo=job.JobNo and job.JobStatus!='Voided'
left join (select top 1 IsBonded,Code from ref_warehouse f) as tab_wh on tab_wh.Code=job.WareHouseCode
left join (select top 1 Id,ContainerType,ContainerNo from CTM_JobDet1) as tab_cont on tab_cont.Id=mast.ContId
left join (select top 1 Id from CTM_JobDet2) as tab_trip on tab_trip.Id=mast.TripId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then QtyOrig else -QtyOrig end) as BalQty,LineId from job_house group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then PackQty else -PackQty end) as SkuQty,LineId from job_house  group by LineId) as tab_sku on tab_sku.LineId=mast.LineId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then WeightOrig else -WeightOrig end) as BalanceWeight,LineId from job_house  group by LineId) as tab_weight on tab_weight.LineId=mast.LineId 
left join (select sum(case when CargoType='IN' and CargoStatus='C' then VolumeOrig else -VolumeOrig end) as BalanceVolume,LineId from job_house  group by LineId) as tab_volume on tab_volume.LineId=mast.LineId 
where CargoType='IN' and BalQty>0 ");
            if (jobNo.Length > 0)
            {
                sql += string.Format(" and job.JobNo like '%{0}%'", jobNo);
            }
            else
            {
                if(key1.Length>0)
                    sql += string.Format("and IsBonded='{0}'", key1);
                if(key2.Length>0)
                    sql += string.Format("and LineId={0}", key2);
                if (lotNo.Length > 0)
                    sql += string.Format("and mast.BookingNo='{0}'", lotNo);
                if (hblNo.Length > 0)
                    sql += string.Format("and mast.HblNo='{0}'", hblNo);
                if (sku.Length > 0)
                    sql += string.Format("and mast.SkuCode='{0}'", sku);
                if (cargoType.Length > 0)
                    sql += string.Format("and mast.OpsType='{0}'", cargoType);
                if (client.Length > 0)
                {
                    sql += string.Format("and job.PartyId='{0}'", client);
                }
                if (warehouse.Length > 0)
                {
                    sql += string.Format("and job.WareHouseCode like '%{0}%'", warehouse);
                }
                if (date.Length > 0)
                {
                    sql += string.Format(" and mast.StockDate <= '{0}'", date);
                }

                if (location.Length > 0)
                {
                    sql += string.Format(" and mast.Location like '%{0}%'", location);
                }
            }
            //throw new Exception(sql);
            DataTable dt = ConnectSql_mb.GetDataTable(sql);
            return dt;
        }
        public static DataTable getStockBalance_New(string jobNo, string client, string date, string hblNo, string lotNo, string sku, string warehouse, string location, string cargoType, string isBonded, string lineId,string marking)
        {

            string sql = string.Format(@"select distinct row_number() over (order by mast.Id) as No,job.JobNo,job.JobDate,job.WareHouseCode,tab_wh.IsBonded,(select top 1 IsBonded from ref_warehouse where Code=job.WareHouseCode) as WhsType,job.PartyId,(select top 1 Name from XXParty where PartyId=job.PartyId) as PartyName,
(case when LEN(job.WhPermitNo)>0 then job.WhPermitNo else job.PermitNo end) as PermitNo,job.ClientId,(select top 1 Name from XXParty where PartyId=job.ClientId) as ClientName,job.Vessel,job.Voyage,mast.StockDate,mast.NextBillDate,mast.StorageType,mast.StockDate,
job.PickupFrom,job.DeliveryTo,mast.Qty,mast.PackTypeOrig,mast.ContNo,mast.BookingNo,mast.OpsType,mast.HblNo,mast.Location,mast.PackQty,mast.RefNo,tab_weight.BalanceWeight,tab_volume.BalanceVolume,mast.LineId,mast.ContId,tab_cont.ContainerType,
mast.PackUom,mast.QtyOrig,mast.WeightOrig,mast.VolumeOrig,mast.LengthPack,mast.WidthPack,mast.HeightPack,mast.SkuCode,mast.Marking1,mast.Marking2,mast.Remark1,mast.LandStatus,mast.DgClass,mast.DamagedStatus,mast.Remark2,tab_bal.BalQty,tab_sku.SkuQty,Desc1,ActualItem,mast.InventoryId
from job_house mast left join ctm_job job on mast.JobNo=job.JobNo and job.JobStatus!='Voided'
left join (select top 1 IsBonded,Code from ref_warehouse f) as tab_wh on tab_wh.Code=job.WareHouseCode
left join (select top 1 Id,ContainerType,ContainerNo from CTM_JobDet1) as tab_cont on tab_cont.Id=mast.ContId
left join (select top 1 Id from CTM_JobDet2) as tab_trip on tab_trip.Id=mast.TripId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then QtyOrig else -QtyOrig end) as BalQty,LineId from job_house group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then PackQty else -PackQty end) as SkuQty,LineId from job_house  group by LineId) as tab_sku on tab_sku.LineId=mast.LineId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then WeightOrig else -WeightOrig end) as BalanceWeight,LineId from job_house  group by LineId) as tab_weight on tab_weight.LineId=mast.LineId 
left join (select sum(case when CargoType='IN' and CargoStatus='C' then VolumeOrig else -VolumeOrig end) as BalanceVolume,LineId from job_house  group by LineId) as tab_volume on tab_volume.LineId=mast.LineId 
where CargoType='IN' and BalQty>0 ");
            if (jobNo.Length > 0)
            {
                sql += string.Format(" and job.JobNo like '%{0}%'", jobNo);
            }
            else
            {
                if (isBonded.Length > 0)
                    sql += string.Format("and IsBonded='{0}'", isBonded);
                if (lineId.Length > 0)
                    sql += string.Format("and mast.LineId={0}", lineId);
                if (marking.Length > 0)
                    sql += string.Format("and mast.Marking1='{0}'", marking);
                if (lotNo.Length > 0)
                    sql += string.Format("and mast.BookingNo='{0}'", lotNo);
                if (hblNo.Length > 0)
                    sql += string.Format("and mast.HblNo='{0}'", hblNo);
                if (sku.Length > 0)
                    sql += string.Format("and mast.SkuCode='{0}'", sku);
                if (cargoType.Length > 0)
                    sql += string.Format("and mast.OpsType='{0}'", cargoType);
                if (client.Length > 0)
                {
                    sql += string.Format("and job.PartyId='{0}'", client);
                }
                if (warehouse.Length > 0)
                {
                    sql += string.Format("and job.WareHouseCode like '%{0}%'", warehouse);
                }
                if (date.Length > 0)
                {
                    sql += string.Format(" and mast.StockDate  <= '{0}'", date);
                }

                if (location.Length > 0)
                {
                    sql += string.Format(" and mast.Location like '%{0}%'", location);
                }
            }
            //throw new Exception(sql);
            DataTable dt = ConnectSql_mb.GetDataTable(sql);
            return dt;
        }
        public static DataTable getStockBalance_New_G(string jobNo, string client, string date1_from, string date1_to, string date2_from, string date2_to, string hblNo, string lotNo, string sku, string warehouse, string location
    , string mft_LotNo, string partNo, string onHold)
        {

            string sql = string.Format(@"select distinct row_number() over (order by mast.Id) as No,job.JobNo,job.JobType,job.JobDate,job.WareHouseCode,(select top 1 IsBonded from ref_warehouse where Code=job.WareHouseCode) as WhsType,
(case when LEN(job.WhPermitNo)>0 then job.WhPermitNo else job.PermitNo end) as PermitNo,job.ClientId,(select Name from XXParty where PartyId=job.ClientId) as PartyName,job.Vessel,job.Voyage,mast.StockDate,mast.NextBillDate,mast.StorageType,
job.PickupFrom,job.DeliveryTo,mast.Qty,mast.PackTypeOrig,mast.ContNo,mast.BookingNo,mast.OpsType,mast.HblNo,mast.Location,mast.PackQty,mast.RefNo,tab_weight.BalanceWeight,tab_volume.BalanceVolume,mast.LineId,mast.ContId,tab_cont.ContainerType,p.PartNo,
mast.PackUom,mast.QtyOrig,mast.WeightOrig,mast.VolumeOrig,mast.LengthPack,mast.WidthPack,mast.HeightPack,mast.SkuCode,mast.Marking1,mast.Marking2,mast.Remark1,mast.LandStatus,mast.DgClass,mast.DamagedStatus,mast.Remark2,tab_bal.BalQty,tab_sku.SkuQty,Desc1,ActualItem  
from job_house mast left join ctm_job job on mast.JobNo=job.JobNo
left join (select Id,ContainerType,ContainerNo from CTM_JobDet1) as tab_cont on tab_cont.Id=mast.ContId
left join ref_product p on ActualItem=p.Code
left join (select Id from CTM_JobDet2) as tab_trip on tab_trip.Id=mast.TripId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then QtyOrig when CargoType='OUT' and CargoStatus='C' then -QtyOrig else 0 end) as BalQty,LineId from job_house group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then PackQty when CargoType='OUT' and CargoStatus='C' then -PackQty else 0 end) as SkuQty,LineId from job_house  group by LineId) as tab_sku on tab_sku.LineId=mast.LineId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then WeightOrig when CargoType='OUT' and CargoStatus='C' then -WeightOrig else 0 end) as BalanceWeight,LineId from job_house  group by LineId) as tab_weight on tab_weight.LineId=mast.LineId 
left join (select sum(case when CargoType='IN' and CargoStatus='C' then VolumeOrig when CargoType='OUT' and CargoStatus='C' then -VolumeOrig else 0 end) as BalanceVolume,LineId from job_house  group by LineId) as tab_volume on tab_volume.LineId=mast.LineId 
where CargoType='IN'");
            if (jobNo.Length > 0)
            {
                sql += string.Format(" and job.JobNo like '%{0}%'", jobNo);
            }
            else
            {
                if (lotNo.Length > 0)
                    sql += string.Format("and mast.BookingNo='{0}'", lotNo);
                if (hblNo.Length > 0)
                    sql += string.Format("and mast.HblNo='{0}'", hblNo);
                if (sku.Length > 0)
                    sql += string.Format("and (mast.SkuCode like '%{0}%' or ActualItem like '%{0}%')", sku);

                if (client.Length > 0)
                {
                    sql += string.Format("and job.ClientId='{0}'", client);
                }
                if (warehouse.Length > 0)
                {
                    sql += string.Format("and job.WareHouseCode like '%{0}%'", warehouse);
                }
                if (date1_from.Length > 0 && date1_to.Length > 0)
                {
                    sql += string.Format(" and (mast.Mft_LotDate>='{0}' and mast.Mft_LotDate<'{1}')", date1_from, date1_to);
                }
                if (date2_from.Length > 0 && date2_to.Length > 0)
                {
                    sql += string.Format(" and (mast.Mft_ExpiryDate>='{0}' and mast.Mft_ExpiryDate<'{1}')", date2_from, date2_to);
                }
                if (location.Length > 0)
                {
                    sql += string.Format(" and mast.Location like '%{0}%'", location);
                }
                if (mft_LotNo.Length > 0)
                {
                    sql += string.Format(" and mast.Mft_LotNo like '%{0}%'", mft_LotNo);
                }
                if (partNo.Length > 0)
                {
                    sql += string.Format(" and p.PartNo like '%{0}%'", partNo);
                }
                if (onHold.Length > 0)
                {
                    sql += string.Format(" and mast.OnHold like '%{0}%'", onHold);
                }
            }
            //throw new Exception(sql);
            DataTable dt = ConnectSql_mb.GetDataTable(sql);
            return dt;
        }
        public static DataTable getStockProcess(string jobNo, string client, string date, string hblNo, string lotNo, string sku, string warehouse, string location)
        {

            string sql = string.Format(@"select distinct row_number() over (order by p.Id) as No,p.*,mast.Qty,mast.PackTypeOrig,mast.ContNo,mast.BookingNo,mast.OpsType,mast.HblNo,mast.Location,mast.PackQty,mast.RefNo,mast.LineId,job.PartyId,
mast.PackUom,mast.QtyOrig,mast.WeightOrig,mast.VolumeOrig,mast.LengthPack,mast.WidthPack,mast.HeightPack,mast.SkuCode,mast.Marking1,mast.Marking2,mast.Remark1,mast.LandStatus,mast.DgClass,mast.DamagedStatus,mast.Remark2 from job_process p inner join job_house mast on  p.HouseId=mast.Id inner join ctm_job job on job.JobNo=mast.JobNo
where CargoType='IN' ");
            if (lotNo.Length > 0)
                sql += string.Format("and mast.BookingNo='{0}'", lotNo);
            if (hblNo.Length > 0)
                sql += string.Format("and mast.HblNo='{0}'", hblNo);
            if (sku.Length > 0)
                sql += string.Format("and mast.SkuCode='{0}'", sku);

            if (client.Length > 0)
            {
                sql += string.Format("and job.PartyId='{0}'", client);
            }
            if (warehouse.Length > 0)
            {
                sql += string.Format("and job.WareHouseCode like '%{0}%'", warehouse);
            }
            if (sku.Length > 0)
            {
                sql += string.Format(" and job.JobDate < '{0}'", date);
            }
            if (jobNo.Length > 0)
            {
                sql += string.Format(" and job.JobNo like '%{0}%'", jobNo);
            }
            if (location.Length > 0)
            {
                sql += string.Format(" and mast.Location like '%{0}%'", location);
            }
            DataTable dt = ConnectSql.GetTab(sql);
            return dt;
        }
        public static DataTable getStockMove(string client, string date1,string date2, string hblNo, string lotNo, string sku, string warehouse, string location,string contNo)
        {
            string where = "";
            string sql = string.Format(@"select * from(select distinct ROW_NUMBER()over (order by mast.Id) as No,job.JobNo,job.JobDate,mast.CargoType,mast.LineId,mast.ActualItem,job.PartyId,mast.StockDate,
(select top 1 WareHouseCode from CTM_Job j where mast.JobNo=j.JobNo) as WareHouseCode,(select top 1 IsBonded from ref_warehouse where Code=job.WareHouseCode) as WhsType,(case when LEN(job.WhPermitNo)>0 then job.WhPermitNo else job.PermitNo end) as PermitNo,job.ClientId,
job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,mast.Qty,mast.PackTypeOrig,mast.ContNo,mast.BookingNo,mast.OpsType,mast.HblNo,mast.Location,mast.PackQty,mast.QtyOrig,
mast.WeightOrig,mast.VolumeOrig,mast.LengthPack,mast.WidthPack,mast.HeightPack,mast.SkuCode,mast.Marking1,mast.Marking2,mast.Remark1,mast.LandStatus,mast.DgClass,mast.DamagedStatus,mast.Remark2,mast.RefNo,
isnull(PackQty,0) as SkuQty,PackUom from job_house mast inner join ctm_job job on mast.JobNo=job.JobNo and CargoType in ('IN','OUT')
)as tab ");
            if (lotNo.Length > 0)
                where = GetWhere(where, string.Format(" BookingNo='{0}'", lotNo));
            if (hblNo.Length > 0)
                where = GetWhere(where, string.Format(" HblNo='{0}'", hblNo));
            if (sku.Length > 0)
                where = GetWhere(where, string.Format(" SkuCode='{0}'", sku));

            if (client.Length > 0)
            {
                where = GetWhere(where, string.Format(" PartyId='{0}'", client));
            }
            if (warehouse.Length > 0)
            {
                where = GetWhere(where, string.Format(" WareHouseCode like '%{0}%'", warehouse));
            }
            if (sku.Length > 0)
            {
                where = GetWhere(where, string.Format("( StockDate  between '{0}' and '{1}')", date1, date2));
            }
            if (contNo.Length > 0)
            {
                where = GetWhere(where, string.Format(" ContNo like '%{0}%'", contNo));
            }
            if (location.Length > 0)
            {
                where = GetWhere(where, string.Format(" Location like '%{0}%'", location));
            }
            if (where.Length > 0)
            {
                sql += " where " + where + " order by LineId";
            }
            DataTable dt = ConnectSql.GetTab(sql);
            return dt;
        }
        public static DataTable getStockMove_New(string client, string date1_from, string date1_to, string date2_from, string date2_to, string hblNo, string lotNo, string sku, string warehouse, string location, string contNo
    , string mft_LotNo, string partNo, string onHold, string direction, string jobDateFrom, string jobDateTo)
        {
            string where = "";
            string sql = string.Format(@"select * from(select distinct ROW_NUMBER()over (order by LineId) as No,job.JobNo,job.JobDate,mast.CargoType,mast.LineId,mast.ActualItem,
(select top 1 WareHouseCode from CTM_Job j where mast.JobNo=j.JobNo) as WareHouseCode,(select top 1 IsBonded from ref_warehouse where Code=job.WareHouseCode) as WhsType,(case when LEN(job.WhPermitNo)>0 then job.WhPermitNo else job.PermitNo end) as PermitNo,job.ClientId,
job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,mast.Qty,mast.PackTypeOrig,mast.ContNo,mast.BookingNo,mast.OpsType,mast.HblNo,mast.Location,mast.PackQty,mast.QtyOrig,
mast.WeightOrig,mast.VolumeOrig,mast.LengthPack,mast.WidthPack,mast.HeightPack,mast.SkuCode,mast.Marking1,mast.Marking2,mast.Remark1,mast.LandStatus,mast.DgClass,mast.DamagedStatus,mast.Remark2,mast.RefNo,
isnull(PackQty,0) as SkuQty,PackUom,p.PartNo,Mft_ExpiryDate,Mft_LotDate,Mft_LotNo,OnHold from job_house mast left join ref_product p on ActualItem=p.Code inner join ctm_job job on mast.JobNo=job.JobNo and CargoType in ('IN','OUT')
)as tab ");
            if (lotNo.Length > 0)
                where = GetWhere(where, string.Format(" BookingNo like '%{0}%'", lotNo));
            else
            {
                if (hblNo.Length > 0)
                    where = GetWhere(where, string.Format(" HblNo like '%{0}%'", hblNo));
                if (sku.Length > 0)
                    where = GetWhere(where, string.Format(" (SkuCode like '%{0}%' or ActualItem like '%{0}%')", sku));
                if (client.Length > 0)
                {
                    where = GetWhere(where, string.Format(" ClientId='{0}'", client));
                }
                if (warehouse.Length > 0)
                {
                    where = GetWhere(where, string.Format(" WareHouseCode='{0}'", warehouse));
                }
                if (date1_from.Length > 0 && date1_to.Length > 0)
                {
                    where = GetWhere(where, string.Format(" (Mft_LotDate>='{0}' and Mft_LotDate<'{1}')", date1_from, date1_to));
                }
                if (date2_from.Length > 0 && date2_to.Length > 0)
                {
                    where = GetWhere(where, string.Format(" (Mft_ExpiryDate>='{0}' and Mft_ExpiryDate<'{1}')", date2_from, date2_to));
                }
                if (jobDateFrom.Length > 0 && jobDateTo.Length > 0)
                {
                    where = GetWhere(where, string.Format(" JobDate>='{0}' and JobDate<='{1}'", jobDateFrom, jobDateTo));
                }
                if (contNo.Length > 0)
                {
                    where = GetWhere(where, string.Format(" ContNo like '%{0}%'", contNo.Trim()));
                }
                if (location.Length > 0)
                {
                    where = GetWhere(where, string.Format(" Location like '%{0}%'", location));
                }
                if (mft_LotNo.Length > 0)
                {
                    where = GetWhere(where, string.Format(" Mft_LotNo like '%{0}%'", mft_LotNo.Trim()));
                }
                if (partNo.Length > 0)
                {
                    where = GetWhere(where, string.Format(" PartNo like '%{0}%'", partNo.Trim()));
                }
                if (onHold.Length > 0)
                {
                    where = GetWhere(where, string.Format(" OnHold like '%{0}%'", onHold));
                }
                if (direction.Length > 0)
                {
                    where = GetWhere(where, string.Format(" CargoType='{0}'", direction));
                }
            }
            if (where.Length > 0)
            {
                sql += " where " + where + " order by LineId";
            }
            DataTable dt = ConnectSql.GetTab(sql);
            return dt;
        }
        public static DataTable getStockMove_cost(string jobNo,string client, string date1, string date2, string hblNo, string lotNo, string sku, string warehouse, string location, string contNo)
        {
            string where = "";
            string sql = string.Format(@"select * from(select distinct ROW_NUMBER()over (order by mast.Id) as No,job.JobNo,job.JobDate,mast.CargoType,mast.LineId,mast.ActualItem,job.PartyId,mast.StockDate,
(select top 1 WareHouseCode from CTM_Job j where mast.JobNo=j.JobNo) as WareHouseCode,(select top 1 IsBonded from ref_warehouse where Code=job.WareHouseCode) as WhsType,(case when LEN(job.WhPermitNo)>0 then job.WhPermitNo else job.PermitNo end) as PermitNo,job.ClientId,
job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,mast.Qty,mast.PackTypeOrig,mast.ContNo,mast.BookingNo,mast.OpsType,mast.HblNo,mast.Location,mast.PackQty,mast.QtyOrig,
mast.WeightOrig,mast.VolumeOrig,mast.LengthPack,mast.WidthPack,mast.HeightPack,mast.SkuCode,mast.Marking1,mast.Marking2,mast.Remark1,mast.LandStatus,mast.DgClass,mast.DamagedStatus,mast.Remark2,mast.RefNo,
isnull(PackQty,0) as SkuQty,PackUom,tab_t.ManualDo from job_house mast inner join ctm_job job on mast.JobNo=job.JobNo and CargoType in ('IN','OUT')
left join (select ManualDo,TripIndex from CTM_JobDet2 ) as tab_t on tab_t.TripIndex=mast.TripIndex
)as tab ");
            if (jobNo.Length > 0)
                where = GetWhere(where, string.Format(" JobNo='{0}'", jobNo));
            if (lotNo.Length > 0)
                where = GetWhere(where, string.Format(" BookingNo='{0}'", lotNo));
            if (hblNo.Length > 0)
                where = GetWhere(where, string.Format(" HblNo='{0}'", hblNo));
            if (sku.Length > 0)
                where = GetWhere(where, string.Format(" SkuCode='{0}'", sku));

            if (client.Length > 0)
            {
                where = GetWhere(where, string.Format(" PartyId='{0}'", client));
            }
            if (warehouse.Length > 0)
            {
                where = GetWhere(where, string.Format(" WareHouseCode like '%{0}%'", warehouse));
            }
            if (sku.Length > 0)
            {
                where = GetWhere(where, string.Format("( StockDate  between '{0}' and '{1}')", date1, date2));
            }
            if (contNo.Length > 0)
            {
                where = GetWhere(where, string.Format(" ContNo like '%{0}%'", contNo));
            }
            if (location.Length > 0)
            {
                where = GetWhere(where, string.Format(" Location like '%{0}%'", location));
            }
            if (where.Length > 0)
            {
                sql += " where " + where + " order by LineId";
            }
            DataTable dt = ConnectSql.GetTab(sql);
            return dt;
        }
        private static string GetWhere(string where, string s)
        {
            if (where.Length > 0)
                where += " and " + s;
            else
                where = s;
            return where;
        }
        #endregion

        #region Update Actual Info
        #endregion

        #region Update Container
        public static void Update_Container(int contId,string contNo) {
            string sql = string.Format(@"update job_house set ContNo='{1}' where ContId={0}",contId,contNo);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
        #endregion
    }
}

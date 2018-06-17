<%@ WebService Language="C#" Class="TruckingService" %>

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Wilson.ORMapper;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class TruckingService  : System.Web.Services.WebService {

    [WebMethod]
    public void Get_Data()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string jobNo = info["JobNo"].ToString();
        string CargoType = info["CargoType"].ToString();

        string sql_wh=string.Format(@"select WareHouseCode,ClientId from ctm_job where JobNo='{0}'",jobNo);
        DataTable dt_job = ConnectSql_mb.GetDataTable(sql_wh);
        string whsCode=SafeValue.SafeString(dt_job.Rows[0]["WareHouseCode"]);
        string clientId=SafeValue.SafeString(dt_job.Rows[0]["ClientId"]);

        #region House
        string sql = string.Format(@"select *,1 as Count from job_house h 
left join(select sum(case when CargoType='IN' then QtyOrig else isnull(-QtyOrig,0) end) as BalQty,
sum(case when CargoType='IN' then PackQty  else -PackQty end) as BalPack,
sum(case when CargoType='IN' then WeightOrig else -WeightOrig end) as BalWeight,
sum(case  when CargoType='IN' then VolumeOrig else -VolumeOrig end) as BalVolume,
LineId from job_house where CargoStatus='C' group by LineId) as tab_bal on tab_bal.LineId=h.LineId
 where JobNo=@JobNo and  CargoType=@CargoType  order by h.Id asc");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@CargoType", CargoType, SqlDbType.NVarChar));
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string DataHouse = Common.DataTableToJson(dt);
        #endregion

        #region Container
        sql = string.Format(@"select ContainerNo,Id from ctm_jobdet1 where JobNo='{0}'",jobNo);
        dt = ConnectSql_mb.GetDataTable(sql);
        string DataCont = Common.DataTableToJson(dt);
        #endregion

        #region Trip
        sql = string.Format(@"select TripIndex,Id,FromCode,ToCode,DriverCode,ChessisCode,FromDate,ToDate from ctm_jobdet2 where JobNo='{0}' and TripCode in ('TPT','WDO','WGR')",jobNo);
        dt = ConnectSql_mb.GetDataTable(sql);
        string DataTrip = Common.DataTableToJson(dt);
        #endregion

        #region CargoStatus
        sql = string.Format(@"select Id,Code from XXUom where CodeType='5'");
        dt = ConnectSql_mb.GetDataTable(sql);
        string DataType = Common.DataTableToJson(dt);
        #endregion

        #region Uom
        sql = string.Format(@"select Id,Code from XXUom where CodeType='2'");
        dt = ConnectSql_mb.GetDataTable(sql);
        string DataUom = Common.DataTableToJson(dt);
        #endregion

        #region Uom
        sql = string.Format(@"select Id,Code,Name,Description from ref_product");
        dt = ConnectSql_mb.GetDataTable(sql);
        string DataSKU = Common.DataTableToJson(dt);
        #endregion

        #region Location
        sql = string.Format(@"select Id,Code,Name from ref_location where WarehouseCode='{0}'",whsCode);
        dt = ConnectSql_mb.GetDataTable(sql);
        if(dt.Rows.Count==0){
            sql = string.Format(@"select Id,Code,Name from ref_location");
            dt = ConnectSql_mb.GetDataTable(sql);
        }
        string DataLoc = Common.DataTableToJson(dt);
        #endregion

        string context = string.Format("{0}\"list\":{2},\"cont\":{3},\"trip\":{4},\"type\":{5},\"uom\":{6},\"sku\":{7},\"location\":{8}{1}", "{", "}", DataHouse,DataCont,DataTrip,DataType,DataUom,DataSKU,DataLoc);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void Get_Type() {
        string sql = string.Format(@"select Id,Code from XXUom where CodeType='5'");
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string Data = Common.DataTableToJson(dt);
        string context = string.Format("{0}\"list\":{2}{1}", "{", "}", Data);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void Add_Cargo()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string jobNo = info["JobNo"].ToString();
        string cargoType = info["CargoType"].ToString();
        string client = SafeValue.SafeString(info["Client"]);
        string lotNo = "";
        string[] srs = jobNo.Split('-');
        for (int i = 0; i < srs.Length; i++)
        {
            string c = srs[2].Substring(0, 4);
            lotNo = srs[0] + "-" + c + "-" + srs[3];
        }
        string sql = string.Format(@"insert into job_house(BookingNo,Marking2,Qty,Weight,Volume,JobNo,RefNo,CargoType,Commodity,CargoStatus,ClientId,Location) values(@BookingNo,@Marking2,@Qty,@Weight,@Volume,@JobNo,@RefNo,@CargoType,@Commodity,@CargoStatus,@ClientId,@Location) select @@IDENTITY");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@BookingNo", lotNo, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@RefNo", jobNo, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@Marking2", "", SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@Qty", 1, SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Weight", 0, SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@Volume", 0, SqlDbType.Decimal));
        list.Add(new ConnectSql_mb.cmdParameters("@CargoType", cargoType, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@Commodity", cargoType, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@CargoStatus", "P", SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@ClientId", client, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@Location", "HOLDING", SqlDbType.NVarChar));
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql, list);

        string houseId = "";
        if (res.status)
            houseId = res.context;

        sql = string.Format(@"update job_house set LineId={0},InventoryId='{0}' where Id={0}",houseId);
        ConnectSql_mb.ExecuteScalar(sql, list);

        string context = string.Format("{0}", jobNo);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void Copy_Cargo()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string jobNo = info["JobNo"].ToString();
        int count =SafeValue.SafeInt(info["Count"],0);
        string id = info["Id"].ToString();
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + id + "'");
        C2.JobHouse job = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
        if (job != null)
        {
            for (int i = 0; i < count; i++)
            {
                job.Qty = 0;
                job.QtyOrig = 0;

                C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(job);

                job.InventoryId=SafeValue.SafeString(job.Id);
                job.LineId = job.Id;
                C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(job);

                Wilson.ORMapper.OPathQuery query1 = new Wilson.ORMapper.OPathQuery(typeof(C2.Dimension), "HouseId='" + id + "'");
                ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query1);
                for (int j = 0; j < objSet.Count; j++)
                {
                    C2.Dimension d = objSet[i] as C2.Dimension;
                    d.HouseId = job.Id;

                    C2.Manager.ORManager.StartTracking(d, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(d);
                }
            }
        }
        string context = string.Format("{0}", jobNo);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void Delete_Cargo()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string id = info["Id"].ToString();
        string sql = string.Format(@"delete from job_house where Id={0}", id);
        int res = ConnectSql_mb.ExecuteNonQuery(sql);
        string context = string.Format("{0}", res);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void Add_SKU() {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string Code = info["Code"].ToString();
        string Name = info["Name"].ToString();
        string JobNo = info["JobNo"].ToString();
        string sql_wh = string.Format(@"select ClientId from ctm_job where JobNo='{0}'", JobNo);
        DataTable dt_job = ConnectSql_mb.GetDataTable(sql_wh);
        string clientId = SafeValue.SafeString(dt_job.Rows[0]["ClientId"]);
        string sql = string.Format(@"insert into ref_product(Code,Name,Description,PartyId) values(@Code,@Name,@Description,@PartyId)");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Code", Code, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@Name", Name, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@Description", Name, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@PartyId", clientId, SqlDbType.NVarChar));
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql, list);

        sql = string.Format(@"select Id,Code,Name from ref_product where PartyId='{0}'", clientId);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        if (dt.Rows.Count == 0)
        {
            sql = string.Format(@"select Id,Code,Name from ref_product");
            dt = ConnectSql_mb.GetDataTable(sql);
        }
        string DataSKU = Common.DataTableToJson(dt);

        string context = string.Format("{0}\"list\":{2}{1}", "{", "}", DataSKU);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void Save_SKU() {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string Code = info["Code"].ToString();
        string Name = info["Name"].ToString();
        string Id = info["Id"].ToString();
        string sql = string.Format(@"update ref_product set Code=@Code,Name=@Name,Description=@Description where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Code", Code, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@Name", Name, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@Description", Name, SqlDbType.NVarChar));
        list.Add(new ConnectSql_mb.cmdParameters("@Id", Id, SqlDbType.NVarChar));
        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql, list);

        sql = string.Format(@"select Id,Code,Name from ref_product where Id={0}",Id);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string DataSKU = Common.DataRowToJson(dt);

        string context = string.Format("{0}\"list\":{2}{1}", "{", "}", DataSKU);
        Common.WriteJson(true, context);
    }
    [WebMethod]
    public void Save_Data()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        JArray jArray = (JArray)JsonConvert.DeserializeObject(info["list"].ToString());
        string client = SafeValue.SafeString(info["Client"]);
        string jobNo = "";
        if (jArray.Count > 0)
        {
            string sql = "";
            for (int i = 0; i < jArray.Count; i++)
            {
                JObject house = (JObject)jArray[i];
                #region Column
                jobNo = SafeValue.SafeString(house["JobNo"]);
                string houseId = SafeValue.SafeString(house["Id"],"0");
                string BookingNo = SafeValue.SafeString(house["BookingNo"],"");
                string Commodity = SafeValue.SafeString(house["Commodity"],"");
                string Marking1 = SafeValue.SafeString(house["Marking1"],"");
                string Marking2 = SafeValue.SafeString(house["Marking2"],"");
                decimal Qty = SafeValue.SafeDecimal(house["Qty"],0);
                decimal Weight = SafeValue.SafeDecimal(house["Weight"], 0);
                decimal Length = SafeValue.SafeDecimal(house["Length"], 0);
                decimal Width = SafeValue.SafeDecimal(house["Width"], 0);
                decimal Height = SafeValue.SafeDecimal(house["Height"], 0);
                decimal Volume = SafeValue.SafeDecimal(house["Volume"], 0);
                if(Length>0&&Height>0&&Width>0)
                    Volume = SafeValue.ChinaRound(Length*Height*Width*Qty, 3);
                string HblNo = SafeValue.SafeString(house["HblNo"],"");
                string TripIndex = SafeValue.SafeString(house["TripIndex"], "");
                int TripId = SafeValue.SafeInt(house["TripId"], 0);
                string ContNo = SafeValue.SafeString(house["ContNo"], "");
                int ContId = SafeValue.SafeInt(house["ContId"], 0);
                string BookingItem = SafeValue.SafeString(house["BookingItem"],"");
                string ActualItem = SafeValue.SafeString(house["ActualItem"],"");
                string UomCode = SafeValue.SafeString(house["UomCode"],"");
                string OpsType = SafeValue.SafeString(house["OpsType"],"");
                string PoNo = SafeValue.SafeString(house["PoNo"],"");
                string CargoStatus = SafeValue.SafeString(house["CargoStatus"],"");
                string BkgSKuCode = SafeValue.SafeString(house["BkgSKuCode"],"");
                decimal BkgSkuQty = SafeValue.SafeDecimal(house["BkgSkuQty"],0);
                string BkgSkuUnit = SafeValue.SafeString(house["BkgSkuUnit"],"");
                decimal LengthPack = SafeValue.SafeDecimal(house["LengthPack"], 0);
                decimal WidthPack = SafeValue.SafeDecimal(house["WidthPack"], 0);
                decimal HeightPack = SafeValue.SafeDecimal(house["HeightPack"], 0);
                decimal QtyOrig = SafeValue.SafeDecimal(house["QtyOrig"],0);
                decimal PackTypeOrig = SafeValue.SafeDecimal(house["PackTypeOrig"],0);
                decimal WeightOrig = SafeValue.SafeDecimal(house["WeightOrig"],0);
                decimal VolumeOrig = SafeValue.SafeDecimal(house["VolumeOrig"],0);
                if(LengthPack>0&&WidthPack>0&&HeightPack>0)
                    VolumeOrig = SafeValue.ChinaRound(LengthPack*WidthPack*HeightPack*QtyOrig,3);
                string SkuCode = SafeValue.SafeString(house["SkuCode"],"");
                decimal PackQty = SafeValue.SafeDecimal(house["PackQty"],0);
                string PackUom = SafeValue.SafeString(house["PackUom"],"");
                string Location = SafeValue.SafeString(house["Location"],"");
                string InventoryId = SafeValue.SafeString(house["InventoryId"],"");
                DateTime StockDate =Helper.Safe.SafeDate(house["StockDate"]);
                string StorageType = SafeValue.SafeString(house["StorageType"], "");
                DateTime BillingStartDate = Helper.Safe.SafeDate(house["BillingStartDate"]);
                DateTime NextBillDate = Helper.Safe.SafeDate(house["NextBillDate"]);
                decimal Collect_Amount1 = Helper.Safe.SafeDecimal(house["Collect_Amount1"]);
                if (CargoStatus == "C")
                {
                    NextBillDate = Helper.Safe.SafeDate(house["BillingStartDate"]);
                }

                string Remark1 = SafeValue.SafeString(house["Remark1"],"");
                string LandStatus = SafeValue.SafeString(house["LandStatus"],"");
                string DgClass = SafeValue.SafeString(house["DgClass"],"");
                string DamagedStatus = SafeValue.SafeString(house["DamagedStatus"],"");
                string Remark2 = SafeValue.SafeString(house["Remark2"],"");
                #endregion
                //                string sql_part = string.Format(@" update job_house set BookingNo='{1}',Marking1='{2}',Marking2='{3}',
                //Qty={4},Weight={5},Volume={6},HblNo='{7}',TripIndex='{8}',ContNo='{9}',BookingItem='{10}',ActualItem='{11}',
                //UomCode='{12}',OpsType='{13}',PoNo='{14}',CargoStatus='{15}',BkgSKuCode='{16}',BkgSkuQty={17},BkgSkuUnit='{18}',
                //Length={19},Width={20},Height={21},QtyOrig={22},PackTypeOrig='{23}',WeightOrig={24},VolumeOrig={25}, 
                //SkuCode='{26}',PackQty={27},PackUom='{28}',Location='{29}',LengthPack={30},WidthPack={31},HeightPack={32},
                //StockDate='{33}',StorageType='{34}',NextBillDate='{35}',Remark1='{36}',LandStatus='{37}',DgClass='{38}',
                //DamagedStatus='{39}',Remark2='{40}',TripId={41},ContId={42},BillingStartDate='{43}' where Id='{0}'",houseId,BookingNo,
                //Marking1,Marking2,Qty,Weight,Volume,HblNo,TripIndex,ContNo,BookingItem,ActualItem,UomCode,OpsType,PoNo,CargoStatus,
                //BkgSKuCode,BkgSkuQty,BkgSkuUnit,Length,Width,Height,QtyOrig,PackTypeOrig,WeightOrig,VolumeOrig,SkuCode,PackQty,PackUom,
                //Location,LengthPack,WidthPack,HeightPack,StockDate,StorageType,NextBillDate,Remark1,LandStatus,DgClass,DamagedStatus,
                //Remark2,TripId,ContId,BillingStartDate);
                sql=string.Format(@" update job_house set ClientId=@ClientId,BookingNo=@BookingNo,Marking1=@Marking1,Marking2=@Marking2,
                Qty=@Qty,Weight=@Weight,Volume=@Volume,HblNo=@HblNo,TripIndex=@TripIndex,ContNo=@ContNo,BookingItem=@BookingItem,ActualItem=@ActualItem,
                UomCode=@UomCode,OpsType=@OpsType,PoNo=@PoNo,CargoStatus=@CargoStatus,BkgSKuCode=@BkgSkuCode,BkgSkuQty=@BkgSkuQty,BkgSkuUnit=@BkgSkuUnit,
                Length=@Length,Width=@Width,Height=@Height,QtyOrig=@QtyOrig,PackTypeOrig=@PackTypeOrig,WeightOrig=@WeightOrig,VolumeOrig=@VolumeOrig, 
                SkuCode=@SkuCode,PackQty=@PackQty,PackUom=@PackUom,Location=@Location,LengthPack=@LengthPack,WidthPack=@WidthPack,HeightPack=@HeightPack,
                StockDate=@StockDate,StorageType=@StorageType,NextBillDate=@NextBillDate,Remark1=@Remark1,LandStatus=@LandStatus,DgClass=@DgClass,
                DamagedStatus=@DamagedStatus,Remark2=@Remark2,TripId=@TripId,ContId=@ContId,BillingStartDate=@BillingStartDate,InventoryId=@InventoryId,Commodity=@Commodity,Collect_Amount1=@Collect_Amount1 where Id=@Id");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@Id", houseId, SqlDbType.Int));
                #region
                list.Add(new ConnectSql_mb.cmdParameters("@ClientId", client, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@BookingNo", BookingNo, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@Marking1", Marking1, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@Marking2", Marking2, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@Qty", Qty, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@Weight", Weight, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@Volume", Volume, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@HblNo", HblNo, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@TripIndex", TripIndex, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@ContNo", ContNo, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@BookingItem", BookingItem, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@ActualItem", ActualItem, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@UomCode", UomCode, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@OpsType", OpsType, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@PoNo", PoNo, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@CargoStatus", CargoStatus, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@BkgSKuCode", BkgSKuCode, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@BkgSkuQty", BkgSkuQty, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@BkgSkuUnit", BkgSkuUnit, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@Length", Length, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@Width", Width, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@Height", Height, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@QtyOrig", QtyOrig, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@PackTypeOrig", PackTypeOrig, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@WeightOrig", WeightOrig, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@VolumeOrig", VolumeOrig, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@SkuCode", SkuCode, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@PackQty", PackQty, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@PackUom", PackUom, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@Location", Location, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@LengthPack", LengthPack, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@WidthPack", WidthPack, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@HeightPack", HeightPack, SqlDbType.Decimal));
                list.Add(new ConnectSql_mb.cmdParameters("@StockDate", StockDate, SqlDbType.DateTime));
                list.Add(new ConnectSql_mb.cmdParameters("@StorageType", StorageType, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@NextBillDate", NextBillDate, SqlDbType.DateTime));
                list.Add(new ConnectSql_mb.cmdParameters("@Remark1", Remark1, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@LandStatus", LandStatus, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@DgClass", DgClass, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@DamagedStatus", DamagedStatus, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@Remark2", Remark2, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", TripId, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@ContId", ContId, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@BillingStartDate", BillingStartDate, SqlDbType.DateTime));
                list.Add(new ConnectSql_mb.cmdParameters("@InventoryId", InventoryId, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@Commodity", Commodity, SqlDbType.NVarChar));
                list.Add(new ConnectSql_mb.cmdParameters("@Collect_Amount1", Collect_Amount1, SqlDbType.Decimal));
                #endregion
                ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql, list);
                //sql += sql + sql_part;
            }
            //ConnectSql_mb.ExecuteNonQuery(sql);
        }
        string json = Common.StringToJson(jobNo);
        Common.WriteJson(true, json);
    }
    [WebMethod]
    public void Copy_Item()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string Id = SafeValue.SafeString(info["Id"]);
        string res = "0";
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + Id + "'");
        C2.JobHouse job = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
        if (job != null)
        {
            job.SkuCode = job.BkgSKuCode;
            job.PackQty = job.BkgSkuQty;
            job.PackUom = job.BkgSkuUnit;
            job.WeightOrig = job.Weight;
            job.VolumeOrig = job.Volume;
            job.LengthPack = job.Length;
            job.HeightPack = job.Height;
            job.WidthPack = job.Width;
            job.ActualItem = job.BookingItem;
            C2.Manager.ORManager.StartTracking(job, Wilson.ORMapper.InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(job);

            res = "1";
        }
        string json = Common.StringToJson(res.ToString());
        Common.WriteJson(true, json);
    }
    [WebMethod]
    public void mutilpeUpdateContainer()
    {
        JObject info = (JObject)JsonConvert.DeserializeObject(HttpUtility.UrlDecode(Context.Request.Form.ToString()));
        string JobNo = SafeValue.SafeString(info["JobNo"]);
        string sql = string.Format(@"select * from job_house where JobNo=@JobNo and isnull(ContId,0)=0");//and isnull(TripId,0)=0
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
        DataTable dt_cargo = ConnectSql_mb.GetDataTable(sql, list);

        sql = string.Format(@"select Id,ContainerNo from ctm_jobdet1 where JobNo=@JobNo and ContainerNo<>''");
        DataTable dt_cont = ConnectSql_mb.GetDataTable(sql, list);
        int res = 0;
        if (dt_cargo.Rows.Count > 0 && dt_cont.Rows.Count > 0)
        {
            int ContId = 0;
            string ContNo = "";
            DataTable Cont_cargo = null;
            for (int i = 0; i < dt_cont.Rows.Count; i++)
            {
                ContId = SafeValue.SafeInt(dt_cont.Rows[i]["Id"], 0);
                ContNo = dt_cont.Rows[i]["ContainerNo"].ToString();

                sql = string.Format("@select * from job_house where JobNo=@JobNo and ContId=@ContId");
                list.Add(new ConnectSql_mb.cmdParameters("@ContId", ContId, SqlDbType.Int));
                Cont_cargo = ConnectSql_mb.GetDataTable(sql, list);

                for (int c = 0; c < dt_cargo.Rows.Count; c++)
                {
                    string cargoCode = SafeValue.SafeString(dt_cargo.Rows[c]["ActualItem"]);
                    if (!mutilpeUpdateContainer_verifyCargoExisted_inCont(Cont_cargo, cargoCode))
                    {
                        //========== insert cargo
                        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobHouse), "Id='" + dt_cargo.Rows[c]["Id"] + "'");
                        C2.JobHouse cargo = C2.Manager.ORManager.GetObject(query) as C2.JobHouse;
                        cargo.ContId = ContId;
                        cargo.ContNo = ContNo;
                        C2.Manager.ORManager.StartTracking(cargo, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(cargo);

                    }
                }
            }
            clearCargo_Cont0Trip0(JobNo);
            res = 1;

        }else
        {
            res = 2;
        }
        string json = Common.StringToJson(res.ToString());
        Common.WriteJson(true, json);
    }

    public bool mutilpeUpdateContainer_verifyCargoExisted_inCont(DataTable dt,string cargoCode)
    {
        bool res = false;
        if (cargoCode.Length > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ActualItem"].ToString() == cargoCode)
                {
                    res = true;
                    break;
                }
            }
        }
        return res;
    }

    public bool clearCargo_Cont0Trip0(string JobNo)
    {
        string sql = string.Format(@"delete job_house where JobNo=@JobNo and isnull(ContId,0)=0 and isnull(TripId,0)=0");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
        return ConnectSql_mb.ExecuteNonQuery(sql, list).status;
    }
};
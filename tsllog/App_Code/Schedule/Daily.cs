using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using C2;
/// <summary>
/// Daily 的摘要说明
/// </summary>
namespace C2
{
    public class Daily
    {
        public Daily()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private bool is_created(string id, DateTime billDate) {
            bool action = true;
            string sql = string.Format(@"select count(*) from job_cost where RelaId=@RelaId and CONVERT(varchar(100), BillStartDate, 23)=@BillStartDate");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@RelaId", id, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@BillStartDate",billDate.Date.ToString("yyyy-MM-dd"),SqlDbType.DateTime));
            ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteScalar(sql,list);
            int  n = 0;
            if (res.status)
                n =SafeValue.SafeInt(res.context,0);
            if (n>=1)
                action = false;
            return action;
        }
        public void auto_insert_job_cost_old()
        {
            //one customer only have one invoice
            string sql = string.Format(@"select distinct row_number() over (order by mast.JobNo) as No,mast.JobNo,max(BookingNo) BookingNo,
max(ClientId) ClientId,max(BookingItem) ItemCode ,max(mast.NextBillDate) NextBillDate,max(BillingStartDate) BillingStartDate,max(SkuCode) SkuCode,max(mast.StorageType) StorageType,sum(tab_bal.BalQty) Qty,sum(tab_sku.SkuQty) SkuQty,
sum(tab_volume.BalanceVolume) BalVolume,sum(tab_weight.BalanceWeight) BalWeight from job_house mast 
left join (select sum(case when CargoType='IN' and CargoStatus='C' then QtyOrig else -QtyOrig end) as BalQty,LineId from job_house group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then PackQty else -PackQty end) as SkuQty,LineId from job_house  group by LineId) as tab_sku on tab_sku.LineId=mast.LineId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then WeightOrig else -WeightOrig end) as BalanceWeight,LineId from job_house  group by LineId) as tab_weight on tab_weight.LineId=mast.LineId 
left join (select sum(case when CargoType='IN' and CargoStatus='C' then VolumeOrig else -VolumeOrig end) as BalanceVolume,LineId from job_house  group by LineId) as tab_volume on tab_volume.LineId=mast.LineId 
where CargoType='IN' and OpsType='Storage' and BalQty>0 group by mast.JobNo order by No");
            DataTable tab = ConnectSql_mb.GetDataTable(sql);
            
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                bool action = false;
                decimal price = 0;
                string clientId = SafeValue.SafeString(tab.Rows[i]["ClientId"]);
                string jobNo = SafeValue.SafeString(tab.Rows[i]["JobNo"]);
                string lotNo = SafeValue.SafeString(tab.Rows[i]["BookingNo"]);
                decimal qty = SafeValue.SafeDecimal(tab.Rows[i]["Qty"], 0);
                decimal skuQty= SafeValue.SafeDecimal(tab.Rows[i]["SkuQty"], 0);
                string itemCode = SafeValue.SafeString(tab.Rows[i]["ItemCode"]);
                string skuCode = SafeValue.SafeString(tab.Rows[i]["SkuCode"]);
                DateTime nextBillDate =Helper.Safe.SafeDate(tab.Rows[i]["NextBillDate"]);
                DateTime billingStartDate = Helper.Safe.SafeDate(tab.Rows[i]["BillingStartDate"]);
                string storageType = SafeValue.SafeString(tab.Rows[i]["StorageType"]);

                int dailyNo = 0;

                DateTime dt = DateTime.Today;
                DataTable tab1 = GetWarehouseRate(itemCode, clientId, storageType);
                TimeSpan span = nextBillDate - dt;
                int n = 1;
                for (int j = 0; j < tab1.Rows.Count; j++)
                {
                    storageType = SafeValue.SafeString(tab1.Rows[j]["StorageType"]);
                    string chgCode = SafeValue.SafeString(tab1.Rows[j]["ChgCode"]);
                    string chgDes = SafeValue.SafeString(tab1.Rows[j]["ChgCodeDes"]);
                    #region get sch and price
                    if (storageType == "Yearly")
                    {
                        if (span.Days <= 0)
                        {
                            if (nextBillDate.Date > (new DateTime(2000, 01, 01)))
                            {
                                //如果没有创建过 is_created返回False
                                if (!is_created(jobNo, nextBillDate))
                                {
                                    action = true;
                                }
                            }
                            //n = SafeValue.SafeInt(dt.Year - doDate.Year, 0);
                            price = SafeValue.SafeDecimal(tab1.Rows[j]["Price"]);
                            nextBillDate = nextBillDate.AddYears(1);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (storageType == "Monthly")
                    {
                        if (span.Days <= 0)
                        {
                            if (nextBillDate.Date > (new DateTime(2000, 01, 01)))
                            {
                                //如果没有创建过 is_created返回False
                                if (!is_created(jobNo, nextBillDate))
                                {
                                    action = true;
                                }
                            }
                            price = SafeValue.SafeDecimal(tab1.Rows[j]["Price"]);
                            //int year = dt.Year;
                            //n = (dt.Year - doDate.Year) * 12 + dt.Month - doDate.Month;
                            nextBillDate = nextBillDate.AddMonths(1);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (storageType == "Weekly")
                    {
                        if (span.Days <= 0)
                        {
                            if (nextBillDate.Date > (new DateTime(2000, 01, 01)))
                            {
                                //如果没有创建过 is_created返回False
                                if (!is_created(jobNo, nextBillDate))
                                {
                                    action = true;
                                }
                            }
                            price = SafeValue.SafeDecimal(tab1.Rows[j]["Price"]);
                            nextBillDate = nextBillDate.AddDays(7);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (storageType == "Biweekly")
                    {
                        if (span.Days <= 0)
                        {
                            if (nextBillDate.Date > (new DateTime(2000, 01, 01)))
                            {
                                //如果没有创建过 is_created返回False
                                if (!is_created(jobNo, nextBillDate))
                                {
                                    action = true;
                                }
                            }
                            price = SafeValue.SafeDecimal(tab1.Rows[j]["Price"]);
                            nextBillDate = nextBillDate.AddDays(14);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (storageType == "Daily")
                    {

                        dailyNo = SafeValue.SafeInt(tab1.Rows[j]["DailyNo"], 1);
                        if (dailyNo == 0)
                            dailyNo = 1;

                        if (span.Days <= 0)
                        {
                            if (nextBillDate.Date > (new DateTime(2000, 01, 01)))
                            {
                                //如果没有创建过 is_created返回False
                                if (!is_created(jobNo, nextBillDate))
                                {
                                    action = true;
                                }
                            }
                            price = SafeValue.SafeDecimal(tab1.Rows[j]["Price"]);
                            nextBillDate = nextBillDate.AddDays(1);
                        }
                        else
                        {
                            continue;
                        }
                    }

                    #endregion
                    string mastType = "WAREHOUSE";
                    if (action)
                    {
                        #region create det
                        InsertJob_Cost(jobNo, chgCode, chgDes, mastType, qty * n, price, itemCode, lotNo, skuCode, billingStartDate,"");
                        Update_NextBillDate(jobNo,itemCode,skuCode, nextBillDate);
                        #endregion
                    }
                }
            }

        }
        public void auto_insert_job_cost()
        {
            //one customer only have one invoice
             string sql = string.Format(@"select distinct row_number() over (order by mast.JobNo) as No,mast.Id,job.JobNo,job.JobDate,job.WareHouseCode,(select top 1 IsBonded from ref_warehouse where Code=job.WareHouseCode) as WhsType,mast.SkuCode,
(case when LEN(job.WhPermitNo)>0 then job.WhPermitNo else job.PermitNo end) as PermitNo,job.ClientId,(select top 1 Name from XXParty where PartyId=job.ClientId) as PartyName,job.Vessel,job.Voyage,mast.StockDate,mast.BillingStartDate,mast.NextBillDate,mast.StorageType,job.PartyId,
job.PickupFrom,job.DeliveryTo,mast.Qty,mast.PackTypeOrig,mast.ContNo,mast.BookingNo,mast.OpsType,mast.HblNo,mast.Location,mast.PackQty,mast.RefNo,tab_weight.BalanceWeight,tab_volume.BalanceVolume,mast.LineId,mast.ContId,tab_cont.ContainerType,
mast.PackUom,mast.QtyOrig,mast.WeightOrig,mast.VolumeOrig,mast.LengthPack,mast.WidthPack,mast.HeightPack,mast.SkuCode,mast.Marking1,mast.Marking2,mast.Remark1,mast.LandStatus,mast.DgClass,mast.DamagedStatus,mast.Remark2,tab_bal.BalQty,tab_sku.SkuQty,Desc1,ActualItem  
from job_house mast left join ctm_job job on mast.JobNo=job.JobNo
left join (select Id,ContainerType,ContainerNo from CTM_JobDet1) as tab_cont on tab_cont.Id=mast.ContId
left join (select Id from CTM_JobDet2) as tab_trip on tab_trip.Id=mast.TripId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then QtyOrig else -QtyOrig end) as BalQty,LineId from job_house group by LineId) as tab_bal on tab_bal.LineId=mast.LineId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then PackQty else -PackQty end) as SkuQty,LineId from job_house  group by LineId) as tab_sku on tab_sku.LineId=mast.LineId
left join (select sum(case when CargoType='IN' and CargoStatus='C' then WeightOrig else -WeightOrig end) as BalanceWeight,LineId from job_house  group by LineId) as tab_weight on tab_weight.LineId=mast.LineId 
left join (select sum(case when CargoType='IN' and CargoStatus='C' then VolumeOrig else -VolumeOrig end) as BalanceVolume,LineId from job_house  group by LineId) as tab_volume on tab_volume.LineId=mast.LineId 
where CargoType='IN' and OpsType='Storage' and BalQty>0 order by No");
             DataTable tab = ConnectSql_mb.GetDataTable(sql);
            string lastPartyId = "";
            string lastJobNo = "";
            string id = "";
            string lineId = "";
            string jobNo = "";
            string sku = "";
            string lotNo = "";
            string contNo = "";
            DateTime doDate = DateTime.Today;
            string partyId ="";
            decimal qty = 0;
            decimal skuQty = 0;
            string marking1 ="";
            string marking2 = "";
            string hblNo = "";
            string contType = "";
            string location = "";
            string warehouse = "";
            string unit = "";
            string itemCode = "";
            string skuCode = "";
            DateTime stockDate = DateTime.Today;
            DateTime nextBillDate = DateTime.Today;
            DateTime billingStartDate = DateTime.Today;
            string storageType = "";
            decimal price = 0;
            bool action = false;
            for (int i = 0; i < tab.Rows.Count; i++)   
            {
                id = SafeValue.SafeString(tab.Rows[i]["Id"]);
                lineId = SafeValue.SafeString(tab.Rows[i]["LineId"]);
                jobNo = SafeValue.SafeString(tab.Rows[i]["JobNo"]);
                sku = SafeValue.SafeString(tab.Rows[i]["SkuCode"]);
                lotNo = SafeValue.SafeString(tab.Rows[i]["BookingNo"]);
                contNo = SafeValue.SafeString(tab.Rows[i]["ContNo"]);
                doDate = SafeValue.SafeDate(tab.Rows[i]["JobDate"], DateTime.Today);
                partyId = SafeValue.SafeString(tab.Rows[i]["PartyId"]);
                qty = SafeValue.SafeDecimal(tab.Rows[i]["BalQty"], 0);
                skuQty = SafeValue.SafeDecimal(tab.Rows[i]["SkuQty"], 0);
                marking1 = SafeValue.SafeString(tab.Rows[i]["Marking1"]);
                marking2 = SafeValue.SafeString(tab.Rows[i]["Marking2"]);
                hblNo = SafeValue.SafeString(tab.Rows[i]["HblNo"]);
                contType = SafeValue.SafeString(tab.Rows[i]["ContainerType"]);
                location = SafeValue.SafeString(tab.Rows[i]["Location"]);
                warehouse = SafeValue.SafeString(tab.Rows[i]["WareHouseCode"]);
                unit = SafeValue.SafeString(tab.Rows[i]["PackTypeOrig"]);
                itemCode = SafeValue.SafeString(tab.Rows[i]["ActualItem"]);
                stockDate = SafeValue.SafeDate(tab.Rows[i]["StockDate"], DateTime.Today);
                nextBillDate = SafeValue.SafeDate(tab.Rows[i]["NextBillDate"], DateTime.Today);
                billingStartDate= SafeValue.SafeDate(tab.Rows[i]["BillingStartDate"], DateTime.Today);
                storageType = SafeValue.SafeString(tab.Rows[i]["StorageType"]);
                skuCode = SafeValue.SafeString(tab.Rows[i]["SkuCode"]);
                int dailyNo = 0;
                
                DateTime dt = DateTime.Today;
                DataTable tab1 = GetWarehouseRate(sku, partyId, storageType);
                TimeSpan span = nextBillDate- dt;
                int n = 1;
                for (int j = 0; j < tab1.Rows.Count; j++)
                {
                    storageType = SafeValue.SafeString(tab1.Rows[j]["StorageType"]);
                    string chgCode = SafeValue.SafeString(tab1.Rows[j]["ChgCode"]);
                    string chgDes = SafeValue.SafeString(tab1.Rows[j]["ChgCodeDes"]);
                    #region get sch and price
                    if (storageType == "Yearly")
                    {
                        if(span.Days<=0)
                        {
                            if (nextBillDate.Date > (new DateTime(2000, 01, 01)))
                            {
                                //如果没有创建过 is_created返回False
                                if (is_created(lineId, nextBillDate))
                                {
                                    action = true;
                                }
                            }
                            //n = SafeValue.SafeInt(dt.Year - doDate.Year, 0);
                            price = SafeValue.SafeDecimal(tab1.Rows[j]["Price"]);
                            nextBillDate = nextBillDate.AddYears(1);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (storageType == "Monthly")
                    {
                        if (span.Days <= 0)
                        {
                            if (nextBillDate.Date > (new DateTime(2000, 01, 01)))
                            {
                                //如果没有创建过 is_created返回False
                                if (is_created(lineId, nextBillDate))
                                {
                                    action = true;
                                }
                            }
                            price = SafeValue.SafeDecimal(tab1.Rows[j]["Price"]);
                            //int year = dt.Year;
                            //n = (dt.Year - doDate.Year) * 12 + dt.Month - doDate.Month;
                            nextBillDate = nextBillDate.AddMonths(1);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (storageType == "Weekly")
                    {
                        if (span.Days <= 0)
                        {
                            if (nextBillDate.Date > (new DateTime(2000, 01, 01)))
                            {
                                //如果没有创建过 is_created返回False
                                if (is_created(lineId, nextBillDate))
                                {
                                    action = true;
                                }
                            }
                            price = SafeValue.SafeDecimal(tab1.Rows[j]["Price"]);
                            nextBillDate = nextBillDate.AddDays(7);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (storageType == "Biweekly")
                    {
                        if (span.Days <= 0)
                        {
                            if (nextBillDate.Date > (new DateTime(2000, 01, 01)))
                            {
                                //如果没有创建过 is_created返回False
                                if (is_created(lineId, nextBillDate))
                                {
                                    action = true;
                                }
                            }
                            price = SafeValue.SafeDecimal(tab1.Rows[j]["Price"]);
                            nextBillDate = nextBillDate.AddDays(14);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (storageType == "Daily")
                    {
                        
                        dailyNo = SafeValue.SafeInt(tab1.Rows[j]["DailyNo"], 1);
                        if (dailyNo == 0)
                            dailyNo = 1;
                        
                        if (span.Days <= 0)
                        {
                            if (nextBillDate.Date > (new DateTime(2000, 01, 01)))
                            {
                                //如果没有创建过 is_created返回False
                                if (is_created(lineId, nextBillDate))
                                {
                                    action = true;
                                }
                            }
                            price = SafeValue.SafeDecimal(tab1.Rows[j]["Price"]);
                           nextBillDate= nextBillDate.AddDays(1);
                        }
                        else
                        {
                            continue;
                        }
                    }

                    #endregion
                    string mastType = "WAREHOUSE";
                    lastPartyId = partyId;
                    if (action)
                    {
                        #region create det
                        InsertJob_Cost(jobNo, chgCode, chgDes, mastType, qty * n, price, itemCode, lotNo, skuCode, billingStartDate,id);
                        Update_NextBillDate(jobNo, itemCode, skuCode, nextBillDate);
                        #endregion
                    }
                }
                lastJobNo = jobNo;
            }

        }
        private void InsertJob_Cost(string jobNo, string chgCode,string chgDes,string mastType, 
            decimal qty, decimal price,string itemCode,string lotNo,string skuCode,DateTime nextBillDate,string id)
        {

            Job_Cost cost = new Job_Cost();
            cost.RelaId = id;
            cost.BillStartDate = nextBillDate;
            cost.JobNo = jobNo;
            cost.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
            cost.ExRate = new decimal(1.0);
            cost.ContNo = "";
            cost.ContType = "";
            cost.ChgCode = chgCode;
            cost.ChgCodeDe = chgDes;
            if(itemCode.Length>0)
              cost.Remark = lotNo+"/" + itemCode;
            if (skuCode.Length > 0)
                cost.Remark = lotNo+"/" + skuCode;
            cost.LineSource = "S";
            cost.Qty = qty;
            cost.Unit = "";
            cost.LineType = "STORAGE";
            cost.LocAmt = SafeValue.ChinaRound(qty * SafeValue.SafeDecimal(price, 0), 2);
            cost.Price = price;
            cost.RowCreateTime = DateTime.Now;
            cost.RowCreateUser = "SYSTEM";
            cost.RowUpdateTime = DateTime.Now;
            cost.RowUpdateUser = "SYSTEM";
            Manager.ORManager.StartTracking(cost, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(cost);
        }
        private void Update_NextBillDate(string jobNo,string itemCode,string skuCode,DateTime date) {
            string sql = string.Format(@"update job_house set NextBillDate=@NextBillDate where JobNo=@JobNo and BookingItem=@BookingItem and SkuCode=@SkuCode");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@NextBillDate", date, SqlDbType.DateTime));
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", jobNo, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@BookingItem", itemCode, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@SkuCode", skuCode, SqlDbType.NVarChar));
            ConnectSql_mb.ExecuteNonQuery(sql,list);
        }
        private DateTime GetMondayByDate(DateTime d)
        {
            if (d.DayOfWeek == DayOfWeek.Monday)
                return d;
            else if (d.DayOfWeek == DayOfWeek.Tuesday)
                return d.AddDays(-1);

            else if (d.DayOfWeek == DayOfWeek.Wednesday)
                return d.AddDays(-2);

            else if (d.DayOfWeek == DayOfWeek.Thursday)
                return d.AddDays(-3);

            else if (d.DayOfWeek == DayOfWeek.Friday)
                return d.AddDays(-4);

            else if (d.DayOfWeek == DayOfWeek.Saturday)
                return d.AddDays(-5);

            else if (d.DayOfWeek == DayOfWeek.Sunday)
                return d.AddDays(-6);
            return d;
        }
        private DataTable GetWarehouseRate(string sku, string client,string storageType)
        {
            string sql = "";
            DataTable tab = null;
            sql = string.Format(@"select top 1 r.* from job_rate r left join ref_product p on r.SkuCode=p.Code and r.SkuClass=p.ProductClass where ClientId=@ClientId and SkuCode=@SkuCode and BillClass='WAREHOUSE' and r.StorageType=@StorageType order by Id desc");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@ClientId", client, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@SkuCode", sku, SqlDbType.NVarChar));
            list.Add(new ConnectSql_mb.cmdParameters("@StorageType", storageType, SqlDbType.NVarChar));
			tab = ConnectSql_mb.GetDataTable(sql,list);
            if (tab.Rows.Count > 0)
                return tab;
			else
			{
			   list = new List<ConnectSql_mb.cmdParameters>();
               list.Add(new ConnectSql_mb.cmdParameters("@ClientId", client, SqlDbType.NVarChar));
               list.Add(new ConnectSql_mb.cmdParameters("@StorageType", storageType, SqlDbType.NVarChar));
			   sql = string.Format(@"select top 1 r.* from job_rate r left join ref_product p on r.SkuCode=p.Code and r.SkuClass=p.ProductClass where ClientId=@ClientId and BillClass='WAREHOUSE' and r.StorageType=@StorageType order by Id desc");
               tab = ConnectSql_mb.GetDataTable(sql,list);
				
			}

            if (tab.Rows.Count == 0)
            {
				list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@StorageType", storageType, SqlDbType.NVarChar));
                sql = string.Format(@"select top 1 r.* from job_rate r left join ref_product p on  r.SkuClass=p.ProductClass where ClientId='STD' and BillClass='WAREHOUSE' and r.StorageType=@StorageType order by Id desc");
                tab = ConnectSql_mb.GetDataTable(sql,list);
            }
            return tab;
        }
    }
}
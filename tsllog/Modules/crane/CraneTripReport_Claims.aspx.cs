using C2;
using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Report_TripReport_Claims : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            search_DateFrom.Date = DateTime.Now.AddDays(-7);
            search_DateTo.Date = DateTime.Now.AddDays(-1);
			cmb_voucher_mode.Text= "New";
			date_voucher.Date = DateTime.Today;
        }
        OnLoad();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"with pri as (
select * from ctm_MastData where [Type]='tripcode'
),
tb1 as (
select det2.Id,det2.JobNo,det2.ContainerNo,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Trip') as Incentive1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime') as Incentive2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Standby') as Incentive3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='PSA') as Incentive4,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DHC') as Charge1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WEIGHING') as Charge2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WASHING') as Charge3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='REPAIR') as Charge4,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DETENTION') as Charge5,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DEMURRAGE') as Charge6,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='LIFT_ON_OFF') as Charge7,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='C_SHIPMENT') as Charge8,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='EMF') as Charge9,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='OTHER') as Charge10,
det2.FromDate,det2.FromTime,det2.ToDate,det2.ToTime,det2.TripCode,det2.TowheadCode,det2.Overtime,det2.OverDistance,
det2.ChessisCode,det2.FromCode,det2.ToCode,job.JobType,det2.ParkingZone,
isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code=det2.TripCode),0) as TripCodePrice,
isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code=det2.Overtime),0) as OverTimePrice,
case when det2.OverDistance='Y' then isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code='OJ'),0) else 0 end as QJPrice,(select top 1 PayNo from XAApPaymentDet where JobRefNo=det2.Id) as DocNo,(select count(*) from XAApPaymentDet where JobRefNo=det2.Id) as Cnt
from CTM_JobDet2 as det2
left outer join CTM_Job as job on job.jobNo=det2.JobNo
left outer join CTM_Driver as dri on dri.Code=det2.DriverCode
");
        string sql_part1 = string.Format(@")
select *,TripCodePrice+OverTimePrice+QJPrice as Total,isnull(Charge1,0)+isnull(Charge2,0)+isnull(Charge3,0)+isnull(Charge4,0)+isnull(Charge5,0)+isnull(Charge6,0)+isnull(Charge7,0)+isnull(Charge8,0)+isnull(Charge9,0)+isnull(Charge10,0)  as TotalCharge from tb1 where isnull(Charge1,0)+isnull(Charge2,0)+isnull(Charge3,0)+isnull(Charge4,0)+isnull(Charge5,0)+isnull(Charge6,0)+isnull(Charge7,0)+isnull(Charge8,0)+isnull(Charge9,0)+isnull(Charge10,0)>0");
        if (search_DateFrom.Date < new DateTime(1900, 1, 1))
        {
            search_DateFrom.Date = DateTime.Now.AddDays(-15);
        }
        if (search_DateTo.Date < new DateTime(1900, 1, 1))
        {
            search_DateTo.Date = DateTime.Now.AddDays(8);
        }

        string where = string.Format(@" where det2.Statuscode='C' and job.JobType='CRA' and DATEDIFF(d,det2.ToDate,'{0}')<=0 and DATEDIFF(d,det2.ToDate,'{1}')>=0", search_DateFrom.Date, search_DateTo.Date);
        if (search_Driver.Text.Trim().Length > 0)
        {
            where += string.Format(@" and det2.DriverCode='{0}'", search_Driver.Text);
        }
        if (search_TowheadCode.Text.Trim().Length > 0)
        {
            where += string.Format(@" and det2.TowheadCode='{0}'", search_TowheadCode.Text);
        }
        if (cbb_Trip_TripCode.Text.Trim() != "")
        {
            where += string.Format(@" and det2.TripCode='{0}'", cbb_Trip_TripCode.Text);
        }
        if (cbb_zone.Text.Trim() != "")
        {
            where += string.Format(@" and det2.ParkingZone='{0}'", cbb_zone.Text);
        }
        string new_sql = sql + where + sql_part1;
        grid_Transport.DataSource = ConnectSql.GetTab(new_sql);
        grid_Transport.DataBind();
    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("TripReport_Claims" + (search_Driver.Text.Length > 0 ? "_" + search_Driver.Text : ""), true);
    }
    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Save") {
            if (search_Driver.Text.Trim() != "")
            {
                if (list.Count > 0)
                {
                    InsertPayment();
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Error";
                }
            }
            else
            {
                e.Result = "Driver";
            }
        }
    }
    private void InsertPayment() {
        
        for (int a = 0; a < list.Count; a++)
        {
            int trpId=list[a].id;
            #region Select Trip
            string sql = string.Format(@"with pri as (
select * from ctm_MastData where [Type]='tripcode'
),
tb1 as (
select det2.Id,det2.JobNo,det2.ContainerNo,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Trip') as Incentive1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime') as Incentive2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Standby') as Incentive3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='PSA') as Incentive4,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DHC') as Charge1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WEIGHING') as Charge2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WASHING') as Charge3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='REPAIR') as Charge4,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DETENTION') as Charge5,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DEMURRAGE') as Charge6,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='LIFT_ON_OFF') as Charge7,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='C_SHIPMENT') as Charge8,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='EMF') as Charge9,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='OTHER') as Charge10,
det1.SealNo,det1.ContainerType,det1.ScheduleDate,det2.FromDate,det2.FromTime,det2.ToDate,det2.ToTime,det2.TripCode,det2.TowheadCode,det2.Overtime,det2.OverDistance,
det2.ChessisCode,det2.FromCode,det2.ToCode,job.JobType,det2.ParkingZone,det2.DriverCode,
isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code=det2.TripCode),0) as TripCodePrice,
isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code=det2.Overtime),0) as OverTimePrice,
case when det2.OverDistance='Y' then isnull((select top 1 case dri.ServiceLevel when 'Level1' then pri.Price1 when 'Level2' then pri.Price2 when 'Level3' then pri.Price3 else 0 end from pri where pri.Code='OJ'),0) else 0 end as QJPrice
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on job.jobNo=det2.JobNo
left outer join CTM_Driver as dri on dri.Code=det2.DriverCode ");
            if (search_DateFrom.Date < new DateTime(1900, 1, 1))
            {
                search_DateFrom.Date = DateTime.Now.AddDays(-15);
            }
            if (search_DateTo.Date < new DateTime(1900, 1, 1))
            {
                search_DateTo.Date = DateTime.Now.AddDays(8);
            }

            string sql_part1 = string.Format(@")
select *,TripCodePrice+OverTimePrice+QJPrice as Total,isnull(Charge1,0)+isnull(Charge2,0)+isnull(Charge3,0)+isnull(Charge4,0)+isnull(Charge5,0)+isnull(Charge6,0)+isnull(Charge7,0)+isnull(Charge8,0)+isnull(Charge9,0)+isnull(Charge10,0)  as TotalCharge from tb1");
            string where = string.Format(@" where det2.Id={2} and det2.Statuscode='C' and DATEDIFF(d,det2.ToDate,'{0}')<=0 and DATEDIFF(d,det2.ToDate,'{1}')>=0", search_DateFrom.Date, search_DateTo.Date,trpId);
            if (search_Driver.Text.Trim().Length > 0)
            {
                where += string.Format(@" and det2.DriverCode='{0}'", search_Driver.Text);
            }
            if (search_TowheadCode.Text.Trim().Length > 0)
            {
                where += string.Format(@" and det2.TowheadCode='{0}'", search_TowheadCode.Text);
            }
            if (cbb_Trip_TripCode.Text.Trim() != "")
            {
                where += string.Format(@" and det2.TripCode='{0}'", cbb_Trip_TripCode.Text);
            }
            if (cbb_zone.Text.Trim() != "")
            {
                where += string.Format(@" and det2.ParkingZone='{0}'", cbb_zone.Text);
            }
            #endregion
            string new_sql = sql + where + sql_part1;
            DataTable tab = ConnectSql.GetTab(new_sql);
            int id = 0;
            string str = "";
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                #region 
                string tripId = SafeValue.SafeString(tab.Rows[i]["Id"]);
                DateTime toDate = SafeValue.SafeDate(tab.Rows[i]["ToDate"], DateTime.Today);
                string contNo = SafeValue.SafeString(tab.Rows[i]["ContainerNo"]);
                string cntType = SafeValue.SafeString(tab.Rows[i]["ContainerType"]);
                string primeMover = SafeValue.SafeString(tab.Rows[i]["TowheadCode"]);
                string trailer = SafeValue.SafeString(tab.Rows[i]["ChessisCode"]);
                string driver = SafeValue.SafeString(tab.Rows[i]["DriverCode"]);
                string jobNo = SafeValue.SafeString(tab.Rows[i]["JobNo"]);
                decimal dhc = SafeValue.SafeDecimal(tab.Rows[i]["Charge1"], 0);
                decimal weight = SafeValue.SafeDecimal(tab.Rows[i]["Charge2"], 0);
                decimal washing = SafeValue.SafeDecimal(tab.Rows[i]["Charge3"], 0);
                decimal repair = SafeValue.SafeDecimal(tab.Rows[i]["Charge4"], 0);
                decimal detention = SafeValue.SafeDecimal(tab.Rows[i]["Charge5"], 0);
                decimal demurrage = SafeValue.SafeDecimal(tab.Rows[i]["Charge6"], 0);
                decimal lift = SafeValue.SafeDecimal(tab.Rows[i]["Charge7"], 0);
                decimal c_Shipment = SafeValue.SafeDecimal(tab.Rows[i]["Charge8"], 0);
                decimal emf = SafeValue.SafeDecimal(tab.Rows[i]["Charge10"], 0);
                decimal other = SafeValue.SafeDecimal(tab.Rows[i]["Charge9"], 0);

				DateTime voucher_date = date_voucher.Date;
				
                if (dhc > 0 || weight > 0 || washing > 0 || repair > 0 || detention > 0 || demurrage > 0 || lift > 0 || c_Shipment > 0 || emf > 0 || other > 0)
                {


                    sql = string.Format(@"select count(*) from XAApPayment where PartyTo='CASH' and OtherPartyName='{0}'  and convert(nvarchar(100),DocDate,23)='{1}'", driver, voucher_date.ToString("yyyy-MM-dd"));
                    int cnt = SafeValue.SafeInt(Helper.Sql.One(sql), 0);
					if(cmb_voucher_mode.Value.ToString() == "New" && a==0)
						cnt = 0;
					
                    XAApPayment inv = null;
                    if (cnt == 0)
                    {
                        #region Payment
                        inv = new C2.XAApPayment();

                        inv.PartyTo = "CASH";
                        inv.OtherPartyName = SafeValue.SafeString(driver);
						string invN = C2Setup.GetNextNo("PS", "AP-PAYMENT-Cash", voucher_date);
                        inv.DocNo = invN;
                        inv.DocType = "PS";
                        inv.DocType1 = "Cash";
                        inv.DocDate = voucher_date;
                        string[] currentPeriod = EzshipHelper.GetAccPeriod(	voucher_date);

                        inv.AcYear = SafeValue.SafeInt(currentPeriod[1], voucher_date.Year);
                        inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], voucher_date.Month);
                        inv.ChqDate = voucher_date;
                        inv.Remark = "";
                        inv.CurrencyId = "SGD";
                        inv.ExRate = 1;
                        inv.PoNo = "";
                        if (inv.ExRate <= 0)
                            inv.ExRate = 1;
                        inv.AcCode = System.Configuration.ConfigurationManager.AppSettings["DefaultBankCode"];
                        inv.AcSource = "CR";
                        if (inv.DocType1.ToLower() == "refund")
                            inv.AcSource = "DB";
                        //inv.BankName = bankName.Text;
                        inv.ChqNo = "";
                        inv.ExportInd = "N";
                        inv.DocAmt = 0;
                        inv.LocAmt = inv.DocAmt * inv.ExRate;
                        inv.Pic = "";
                        inv.BankRec = "";
                        inv.BankDate = new DateTime(1900, 1, 1);
                        inv.CancelDate = new DateTime(1900, 1, 1);
                        inv.CancelInd = "N";
                        inv.CreateBy = HttpContext.Current.User.Identity.Name;
                        inv.CreateDateTime = DateTime.Now;
                        inv.UpdateBy = HttpContext.Current.User.Identity.Name;
                        inv.UpdateDateTime = DateTime.Now;
                        inv.PostBy = "";
                        inv.PostDateTime = new DateTime(1900, 1, 1);
                        inv.GenerateInd = "N";
                        try
                        {
                            C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(inv);
                            //inv.DocNo = inv.SequenceId.ToString();
                            C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Updated);
                            C2.Manager.ORManager.PersistChanges(inv);
                            C2Setup.SetNextNo("","AP-PAYMENT-Cash", inv.DocNo,voucher_date);
                        }
                        catch
                        {
                        }
                        #endregion
                        #region det
                        if (dhc > 0)
                        {
                            Insert_Det(inv.SequenceId, inv.DocNo, inv.DocType, 1, dhc, "DHC", contNo, jobNo, tripId);
                        }
                        if (weight > 0)
                        {
                            Insert_Det(inv.SequenceId, inv.DocNo, inv.DocType, 1, weight, "WEIGHING", contNo, jobNo, tripId);
                        }
                        if (washing > 0)
                        {
                            Insert_Det(inv.SequenceId, inv.DocNo, inv.DocType, 1, washing, "WASHING", contNo, jobNo, tripId);
                        }
                        if (repair > 0)
                        {
                            Insert_Det(inv.SequenceId, inv.DocNo, inv.DocType, 1, repair, "REPAIR", contNo, jobNo, tripId);
                        }
                        if (detention > 0)
                        {
                            Insert_Det(inv.SequenceId, inv.DocNo, inv.DocType, 1, detention, "DETENTION", contNo, jobNo, tripId);
                        }
                        if (demurrage > 0)
                        {
                            Insert_Det(inv.SequenceId, inv.DocNo, inv.DocType, 1, demurrage, "DEMURRAGE", contNo, jobNo, tripId);
                        }
                        if (lift > 0)
                        {
                            Insert_Det(inv.SequenceId, inv.DocNo, inv.DocType, 1, lift, "LIFT ON/OFF", contNo, jobNo, tripId);
                        }
                        if (c_Shipment > 0)
                        {
                            Insert_Det(inv.SequenceId, inv.DocNo, inv.DocType, 1, c_Shipment, "C/SHIPMENT", contNo, jobNo, tripId);
                        }
                        if (emf > 0)
                        {
                            Insert_Det(inv.SequenceId, inv.DocNo, inv.DocType, 1, emf, "EMF", contNo, jobNo, tripId);
                        }
                        if (other > 0)
                        {
                            Insert_Det(inv.SequenceId, inv.DocNo, inv.DocType, 1, other, "OTHER", contNo, jobNo, tripId);
                        }
                        #endregion
                        str = jobNo + " | " + contNo + " | " + cntType;
                        UpdateApPayment(inv.SequenceId);
                        id = inv.SequenceId;

                    }
                    else
                    {
                        str += "\n" + jobNo + " | " + contNo + " | " + cntType;
                        sql = string.Format(@"select top 1 SequenceId from XAApPayment where PartyTo='CASH' and OtherPartyName='{0}' and convert(nvarchar(100),DocDate,23)='{1}'  order by sequenceid desc", driver, voucher_date.ToString("yyyy-MM-dd"));
                        id = SafeValue.SafeInt(Helper.Sql.One(sql), 0);
                        #region det
                        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.XAApPayment), "SequenceId=" + id + "");
                        C2.XAApPayment inv1 = C2.Manager.ORManager.GetObject(query) as C2.XAApPayment;
                        if (dhc > 0)
                        {
                            Insert_Det(inv1.SequenceId, inv1.DocNo, inv1.DocType, 1, dhc, "DHC", contNo, jobNo, tripId);
                        }
                        if (weight > 0)
                        {
                            Insert_Det(inv1.SequenceId, inv1.DocNo, inv1.DocType, 1, weight, "WEIGHING", contNo, jobNo, tripId);
                        }
                        if (washing > 0)
                        {
                            Insert_Det(inv1.SequenceId, inv1.DocNo, inv1.DocType, 1, washing, "WASHING", contNo, jobNo, tripId);
                        }
                        if (repair > 0)
                        {
                            Insert_Det(inv1.SequenceId, inv1.DocNo, inv1.DocType, 1, repair, "REPAIR", contNo, jobNo, tripId);
                        }
                        if (detention > 0)
                        {
                            Insert_Det(inv1.SequenceId, inv1.DocNo, inv1.DocType, 1, detention, "DETENTION", contNo, jobNo, tripId);
                        }
                        if (demurrage > 0)
                        {
                            Insert_Det(inv1.SequenceId, inv1.DocNo, inv1.DocType, 1, demurrage, "DEMURRAGE", contNo, jobNo, tripId);
                        }
                        if (lift > 0)
                        {
                            Insert_Det(inv1.SequenceId, inv1.DocNo, inv1.DocType, 1, lift, "LIFT ON/OFF", contNo, jobNo, tripId);
                        }
                        if (c_Shipment > 0)
                        {
                            Insert_Det(inv1.SequenceId, inv1.DocNo, inv1.DocType, 1, c_Shipment, "C/SHIPMENT", contNo, jobNo, tripId);
                        }
                        if (emf > 0)
                        {
                            Insert_Det(inv1.SequenceId, inv1.DocNo, inv1.DocType, 1, emf, "EMF", contNo, jobNo, tripId);
                        }
                        if (other > 0)
                        {
                            Insert_Det(inv1.SequenceId, inv1.DocNo, inv1.DocType, 1, other, "OTHER", contNo, jobNo, tripId);
                        }
                        #endregion



                        inv1.Remark = "";//str;
                        C2.Manager.ORManager.StartTracking(inv1, Wilson.ORMapper.InitialState.Updated);
                        C2.Manager.ORManager.PersistChanges(inv1);

                        UpdateApPayment(id);
                    }
                }
                else
                {
                    //sql = string.Format(@"select SequenceId,DocId,DocType from XAApPayment where PartyTo='{0}' and DocDate='{1}'", driver, DateTime.Now);
                    //DataTable in = ConnectSql.GetTab(sql);
                    //int id = SafeValue.SafeInt(Helper.Sql.One(sql), 0);
                }
                #endregion
            }
        }
    }
    private void Insert_Det(int docId, string docNo, string docType, int index, decimal price,string code,string cntNo,string jobNo,string tripId)
    {
        try
        {
            //string sql = string.Format(@"select count(*) from XAApPaymentDet where PayId={0} and JobRefNo='{1}' and ", docId,tripId);
            //int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql),0);
            //if (n == 0)
            //{
                C2.XAApPaymentDet det = new C2.XAApPaymentDet();
                det.DocDate = new DateTime(1900, 1, 1);
                det.DocId = docId;
                det.DocNo = docNo;
                det.PayId = docId;
                det.PayNo = docNo;
                det.DocType = docType;
                det.PayLineNo = index;
                det.AcCode = System.Configuration.ConfigurationManager.AppSettings["DefaultLineCode"];
                det.AcSource = "DB";
                det.Remark1 = code;
                det.Remark2 = "";
                det.Remark3 = cntNo;
                det.MastRefNo = jobNo;
                det.JobRefNo = tripId;
                det.Currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                det.ExRate = 1;
                det.DocAmt = price;
                if (det.ExRate == 0)
                    det.ExRate = 1;
                det.LocAmt = price;
                C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(det);
            //}
        }
        catch
        {
        }
    }
    private void Update_Det(XAApPayment inv,int index, decimal price, string code, string cntNo, string jobNo, string tripId)
    {
        try
        {
            string sql = string.Format(@"select SequenceId from XAApPaymentDet where Remark1='{0}'",code);
            int id = SafeValue.SafeInt(Helper.Sql.One(sql), 0);
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.XAApPaymentDet), "SequenceId=" + id + "");
            C2.XAApPaymentDet det = C2.Manager.ORManager.GetObject(query) as C2.XAApPaymentDet;
            if (det != null)
            {
                det.DocDate = new DateTime(1900, 1, 1);
                det.PayId = inv.SequenceId;
                det.PayNo = inv.DocNo;
                det.DocType = inv.DocType;
                det.PayLineNo = index;
                det.AcCode = System.Configuration.ConfigurationManager.AppSettings["DefaultCash"];
                det.AcSource = "DB";
                det.Remark1 = code;
                det.Remark2 = "";
                det.Remark3 = cntNo;
                det.MastRefNo = jobNo;
                det.JobRefNo = tripId;
                det.Currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                det.ExRate = 1;
                det.DocAmt += price;
                if (det.ExRate == 0)
                    det.ExRate = 1;
                det.LocAmt = price;
                C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(det);
            }
        }
        catch
        {
        }
    }
    private void UpdateApPayment(int docId)
    {
        string sql = "select SUM(docAmt) from XAApPaymentDet where PayId ='" + docId + "' and DocType='PS'";
        decimal docAmt = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        string sql_invoice = string.Format("update XAApPayment set DocAmt={1},LocAmt={2} where SequenceId={0}", docId,docAmt,docAmt);
        int x = Manager.ORManager.ExecuteCommand(sql_invoice);

    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int id =0;
        public Record(int _id)
        {
            id = _id;
        }
    }
    private void OnLoad()
    {
        int start = 0;
        int end = 10000;
        for (int i = start; i < end; i++)
        {
            
            ASPxCheckBox isPay = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox id = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Id"], "txt_Id") as ASPxTextBox;
            if (id != null && isPay != null)
            {
                //string sql = string.Format(@"select count(*) from XAApPaymentDet where JobRefNo='{0}'", id.Text);
                //int n = SafeValue.SafeInt(id.Text, 0);
                //if (n > 0)
                //{
                //    isPay.Visible = false;
                //}
                if (isPay.Checked)
                {
                    list.Add(new Record(SafeValue.SafeInt(id.Text, 0)));
                }

            }
            else if (id == null)
                break; ;
        }
    }
}
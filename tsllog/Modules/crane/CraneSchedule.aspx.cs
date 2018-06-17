using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Job_CraneSchedule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_search_dateFrom.Date = DateTime.Now.AddDays(-7);
            txt_search_dateTo.Date = DateTime.Now;
            btn_search_Click(null, null);
        }
    }

	public string GetStatus(string code)
	{
		if(code == "P")
		{
			return "<div style='background:yellow;font-weight:bold;padding:4px;border:1px solid #ddd;width:80px;text-align:center;'>Pending</div>";
		}
		if(code == "S")
		{
			return "<div style='background:green;color:white;font-weight:bold;padding:4px;border:1px solid #ddd;width:80px;text-align:center;'>Started</div>";
		}
		if(code == "C")
		{
			return "<div style='background:blue;color:white;font-weight:bold;padding:4px;border:1px solid #ddd;width:80px;text-align:center;'>Complete</div>";
		}
		return "";
	}
	
    protected void btn_search_Click(object sender, EventArgs e)
    {

        string From = txt_search_dateFrom.Date.ToString("yyyyMMdd");
        string To = txt_search_dateTo.Date.ToString("yyyyMMdd");
        //string ContNo = txt_search_ContNo.Text;
        //bool ContStauts_N = cb_ContStatus1.Checked;
        //bool ContStauts_Int = cb_ContStatus2.Checked;
        //bool ContStauts_C = cb_ContStatus3.Checked;
        //bool ContStatus_UnC = cb_ContStatus4.Checked;
        string JobNo = txt_search_jobNo.Text;
        //string JobType = search_JobType.Text;
        //string Vessel = txt_Vessel.Text;
        string Client = btn_ClientId.Text;
        //string NextTrip = SafeValue.SafeString(search_NextTrip.Value);
        //string WhStatus = SafeValue.SafeString(cbb_warehouseStatus.Text);
        //string Warehouse=SafeValue.SafeString(txt_WareHouseId.Text);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", From, SqlDbType.NVarChar, 8));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", To, SqlDbType.NVarChar, 8));
        //list.Add(new ConnectSql_mb.cmdParameters("@ContNo", "%" + ContNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", "%" + JobNo + "%", SqlDbType.NVarChar, 100));
        //list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 10));
        //list.Add(new ConnectSql_mb.cmdParameters("@Vessel", Vessel, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));
        //list.Add(new ConnectSql_mb.cmdParameters("@WhStatus", WhStatus, SqlDbType.NVarChar, 10));
        //list.Add(new ConnectSql_mb.cmdParameters("@NextTrip", NextTrip, SqlDbType.NVarChar, 10));
        //list.Add(new ConnectSql_mb.cmdParameters("@Warehouse",Warehouse,SqlDbType.NVarChar,100));


        string sql_where = "";
        if (JobNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.JobNo like @JobNo");
        }
        if (sql_where.Equals(""))
        {
            sql_where = " datediff(d,@DateFrom,det2.BookingDate)>=0 and datediff(d,@DateTo,det2.BookingDate)<=0";
            if (Client.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.clientId=@Client");
            }
        }

        string sql = string.Format(@"select det2.Id,job.JobNo,job.JobType,job.JobDate,job.ClientId,(select top 1 Name from XXParty where PartyId=job.ClientId) as ClientName,
det2.Statuscode,det2.ToCode,det2.DriverCode,det2.TowheadCode,RequestVehicleType as Ton,job.PickupFrom,job.DeliveryTo,
BookingDate,BookingTime,BookingTime2,datename(weekday,BookingDate) as BKDAY,det2.BookingRemark,job.IsTrucking,IsWarehouse,IsLocal,IsAdhoc,
FromDate,FromTime,ToDate,ToTime,TotalHour,det2.Remark,det2.DeliveryRemark,det2.OtHour,det2.ByUser
from CTM_JobDet2 as det2
left outer join CTM_Job as job on det2.JobNo=job.JobNo");
        sql += " where det2.JobType='CRA' and " + sql_where;
        sql += " order by det2.BookingDate";

        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        //throw new Exception(sql.ToString());
        this.grid_Transport.DataSource = dt;
        this.grid_Transport.DataBind();


    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void btn_save2excel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("CraneSchedule_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"), true);
    }
}
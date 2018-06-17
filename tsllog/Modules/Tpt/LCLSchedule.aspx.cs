using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Tpt_LCLSchedule : System.Web.UI.Page
{
    public int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_search_dateFrom.Date = DateTime.Now.AddDays(-7);
            txt_search_dateTo.Date = DateTime.Now;
            //btn_search_Click(null, null);
        }
        count = this.grid_Transport.VisibleRowCount;
        OnLoad();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {

        string From = txt_search_dateFrom.Date.ToString("yyyy-MM-dd");
        string To = txt_search_dateTo.Date.AddDays(1).ToString("yyyy-MM-dd");
        string ContNo = txt_search_ContNo.Text;
        //bool ContStauts_N = cb_ContStatus1.Checked;
        //bool ContStauts_Int = cb_ContStatus2.Checked;
        //bool ContStauts_C = cb_ContStatus3.Checked;
        //bool ContStatus_UnC = cb_ContStatus4.Checked;
        string JobNo = txt_search_jobNo.Text;
        string JobType = search_JobType.Text;
        //string Vessel = txt_Vessel.Text;
        string Client = btn_ClientId.Text;
        //string NextTrip = SafeValue.SafeString(search_NextTrip.Value);
        string tripsStatus = SafeValue.SafeString(cbb_tripsStatus.Text);
        string whStatus = SafeValue.SafeString(cbb_warehouseStatus.Text);
        string Warehouse = SafeValue.SafeString(txt_WareHouseId.Text);

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", From, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", To, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@ContNo", ContNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@JobType", JobType, SqlDbType.NVarChar, 10));
        //list.Add(new ConnectSql_mb.cmdParameters("@Vessel", Vessel, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Client", Client, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", tripsStatus, SqlDbType.NVarChar, 10));
        //list.Add(new ConnectSql_mb.cmdParameters("@NextTrip", NextTrip, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@WhStatus", whStatus, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@Warehouse", Warehouse, SqlDbType.NVarChar, 100));

        string sql_where = "";
        if (ContNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "det1.ContainerNo like @ContNo");
        }
        if (JobNo.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.JobNo like @JobNo");
            sql_where = GetWhere(sql_where, " det1.JobType in ('WGR','WDO')");
        }
        if (sql_where.Equals(""))
        {
            sql_where = " det1.WarehouseScheduleDate >= @DateFrom and det1.WarehouseScheduleDate <= @DateTo ";
            if (JobType.Length > 0 && !JobType.Equals("ALL"))
            {
                sql_where = GetWhere(sql_where, "det1.TripCode=@JobType");
            }
            else
            {
                sql_where = GetWhere(sql_where, " det1.JobType in ('WGR','WDO')");
            }
            if (Client.Length > 0)
            {
                sql_where = GetWhere(sql_where, "job.clientId=@Client");
            }
            if (tripsStatus.Length > 0 && !tripsStatus.Equals("ALL"))
            {
                sql_where = GetWhere(sql_where, "det1.StatusCode=@StatusCode");
            }
            if (whStatus.Length > 0 && !whStatus.Equals("ALL"))
            {
                sql_where = GetWhere(sql_where, "det1.WarehouseStatus=@WhStatus");
            }
            else
            {
                //sql_where = GetWhere(sql_where, "det1.WarehouseStatus in ('Scheduled','Started','Completed')");
            }
        }
        if (Warehouse.Length > 0)
        {
            sql_where = GetWhere(sql_where, "job.WareHouseCode=@Warehouse");
        }
        else
        {
            //sql_where = GetWhere(sql_where, "job.WareHouseCode<>''");
        }

        string sql = string.Format(@"select det1.Id,det1.JobNo,job.JobType,job.ClientId,det1.WarehouseStatus as CfsStatus,det1.StatusCode,det1.TripCode,det1.TowheadCode,det1.ChessisCode,det1.DriverCode,
det1.BookingDate,det1.BookingTime,det1.RequestTrailerType,det1.WarehouseEndDate,det1.WarehouseScheduleDate,
job.IsTrucking, job.IsWarehouse, job.IsLocal, job.IsAdhoc,det1.Self_Ind as Self_Ind,det1.FromCode,det1.ToCode,
(select top 1 name from xxparty where partyid=job.clientid) as client,
case when YEAR(FromDate)<2010 then '' else convert(nvarchar(10),FromDate,103)+' '+FromTime end as ScheduleStartDate, 
(case when YEAR(isnull(WarehouseScheduleDate,0))<2010 then convert(nvarchar(10),getdate(),103) else convert(nvarchar(10),WarehouseScheduleDate,103) end) as NewScheduleDate,
BookingTime as ScheduleStartTime,
case when YEAR(ToDate)<2010 then '' else convert(nvarchar(10),ToDate,103)+' '+ToTime end as CompletionDate,
WarehouseStartDate as DateIn,
WarehouseEndDate as DateOut
from CTM_JobDet2 as det1 
left outer join ctm_job as Job on det1.JobNo=job.JobNo
where {0}", sql_where);
        sql += " order by BookingDate,JobNo";
        //throw new Exception(sql.ToString());
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

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
        gridExport.WriteXlsToResponse("ContainerSchedule_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"), true);
    }
    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string temp = e.Parameters;
        string[] ar = temp.Split('_');
        if (ar.Length == 3)
        {
            if (ar[0] == "UpdateInlineStatus")
            {
                contCfsStatusChange(sender, e, SafeValue.SafeInt(ar[1], -1), ar[2]);
            }
        }
        if (temp == "OK")
        {
            if (list.Count > 0)
            {
                string sql = " ";
                List<ConnectSql_mb.cmdParameters> list_cmd = new List<ConnectSql_mb.cmdParameters>();
                for (int i=0;i<list.Count;i++){
                    int _id = list[i].id;
                    DateTime _date = list[i].date;
                    string _time = list[i].time;
                    string sql_part= string.Format(@"update CTM_JobDet2 set WarehouseScheduleDate='{1}',FromTime='{2}',WarehouseStatus='Scheduled' where Id={0}", _id,_date,_time);
                    sql= sql + sql_part;
                }
                if (sql.Length > 0)
                {
                    int res = ConnectSql_mb.ExecuteNonQuery(sql);
                    if (res>0)
                    {
                        e.Result = "Success";
                    }
                }
            } 
        }
    }
    private void contCfsStatusChange(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex, string CfsStatus)
    {
        if (rowIndex < 0)
        {
            e.Result = "Save Error";
            return;
        }

        ASPxLabel lb_id = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lb_Id") as ASPxLabel;
        ASPxDateEdit txt_search_dateFrom = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "txt_search_dateFrom") as ASPxDateEdit;

        ASPxTextBox date_ScheduleStartTime = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "date_ScheduleStartTime") as ASPxTextBox;
        ASPxLabel lb_StatusCode = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lb_StatusCode") as ASPxLabel;
        ASPxLabel lbl_JobNo = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_JobNo") as ASPxLabel;
        ASPxLabel lbl_TripCode = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_TripCode") as ASPxLabel;
        string res = "Save Error";
        string StatusCode = "";
        if (lb_id != null)
        {
            string sql0 = "";
            if (CfsStatus == "Started")
            {
                sql0 = ",WarehouseStartDate=getdate()";
            }
            if (CfsStatus == "Completed")
            {
                //sql0 = ",WarehouseEndDate=getdate(), StatusCode=@StatusCode";
                sql0 = ",WarehouseEndDate=getdate()";
                //if (lbl_TripCode.Text.Contains("WGR"))
                //    StatusCode = "LCL-MT";
                //if (lbl_TripCode.Text.Contains("WDO"))
                //    StatusCode = "LCL-LD";
            }
            if (CfsStatus == "Scheduled")
            {
                if (txt_search_dateFrom.Date > new DateTime(1900, 1, 1))
                {
                    //sql0 = ",WarehouseScheduleDate=@ScheduleStartDate,StatusCode=@StatusCode";
                    sql0 = ",WarehouseScheduleDate=@ScheduleStartDate";
                    StatusCode = lb_StatusCode.Text;
                }
            }
            string sql = string.Format(@"update CTM_JobDet2 set WarehouseStatus=@ContStatus{0} 
where Id=@Oid", sql0);
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@Oid", lb_id.Text, SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@ContStatus", CfsStatus, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@StatusCode", StatusCode, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ScheduleStartDate", txt_search_dateFrom.Date.ToString("yyyy-MM-dd"), SqlDbType.DateTime));
            list.Add(new ConnectSql_mb.cmdParameters("@ScheduleStartTime", date_ScheduleStartTime.Text, SqlDbType.NVarChar));
            if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
            {
                res = "Save successful!";
                //C2.CtmJobDet1.contWarehouseStatusChanged(SafeValue.SafeInt(lb_id.Text, 0));
            }


        }
        e.Result = res;
    }
    public string ShowColor(string status)
    {
        string color = "";
        if (status == "New")
        {
            color = "orange";
        }
        if (status == "Start")
        {
            color = "orange";
        }
        if (status == "Import" || status == "Collection"
            || status == "Customer-MT" || status == "Customer-LD"
            || status == "WHS-LD" || status == "WHS-MT"
            || status == "Return" || status == "Export")
        {
            color = "green";
        }
        if (status == "LCL-MT" || status == "LCL-LD"
            || status == "Arrival" || status == "Depart"
            || status == "Delivered")
        {
            color = "green";
        }
        if (status == "Completed")
        {
            color = "blue";
        }
        if (status == "Canceled")
        {
            color = "gray";
        }

        //========== trucking

        if (status == "P"|| status == "S")
        {
            color = "orange";
        }
        if (status == "C")
        {
            color = "blue";
        }
        return color;
    }
    protected void txt_search_dateFrom_Init(object sender, EventArgs e)
    {
        ASPxDateEdit date = sender as ASPxDateEdit;
        GridViewDataItemTemplateContainer container = date.NamingContainer as GridViewDataItemTemplateContainer;
        date.Value = DateTime.Today;
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int id = 0;
        public DateTime date = DateTime.Now;
        public string time = "";
        public Record(int _id,DateTime _date,string _time)
        {
            id = _id;
            date = _date;
            time = _time;
        }
    }
    private void OnLoad()
    {
        int start = 0;
        int end = count;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxLabel lb_Id = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Id"], "lb_Id") as ASPxLabel;
            ASPxDateEdit txt_search_dateFrom = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["ScheduleStartDate"], "txt_search_dateFrom") as ASPxDateEdit;
            ASPxTextBox date_ScheduleStartTime = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["ScheduleStartDate"], "date_ScheduleStartTime") as ASPxTextBox;
            if (lb_Id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(lb_Id.Text, 0),txt_search_dateFrom.Date,date_ScheduleStartTime.Text));
            }
            else if (lb_Id == null)
                break;
        }
    }
}
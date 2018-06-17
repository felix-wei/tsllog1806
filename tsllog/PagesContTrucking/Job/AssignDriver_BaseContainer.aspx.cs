using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_AssignDriver_BaseContainer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ContStatus"] != null && Request.QueryString["ContStatus"].ToString() != "")
            {
                cbb_StatusCode.Text = Request.QueryString["ContStatus"].ToString();
            }
            else
            {
                cbb_StatusCode.Text = "All";
            }
            txt_search_dateFrom.Date = DateTime.Now;
            txt_search_dateTo.Date = DateTime.Now;
            btn_search_Click(null, null);


            EzshipHelper_Authority.Bind_Authority(this.Page);
        }
    }
    public string xmlChangeToHtml(object par, object contId, int VisibleIndex)
    {
        string res = par.ToString();
        res = res.Replace("&lt;", "<");
        res = res.Replace("&gt;", ">");
        if (res.Length < 2 && SafeValue.SafeInt(contId, 0) > 0)
        {
            res = string.Format(@"<span class='X'>
                                    <select onchange={0}NewTrip('{2}',{1},this);{0}>
                                        <option value={0}New{0}>New</option>
                                        <option value={0}COL{0}>COL</option>
                                        <option value={0}EXP{0}>EXP</option>
                                        <option value={0}IMP{0}>IMP</option>
                                        <option value={0}RET{0}>RET</option>
                                        <option value={0}SHF{0}>SHF</option>
                                        <option value={0}LOC{0}>LOC</option>
                                    </select></span>", "\"", VisibleIndex, contId);
        }
        else
        {
            res += string.Format(@"<span class='X'>
                                    <select onchange={0}NewTrip('{2}',{1},this);{0}>
                                        <option value={0}New{0}>New</option>
                                        <option value={0}COL{0}>COL</option>
                                        <option value={0}EXP{0}>EXP</option>
                                        <option value={0}IMP{0}>IMP</option>
                                        <option value={0}RET{0}>RET</option>
                                        <option value={0}SHF{0}>SHF</option>
                                        <option value={0}LOC{0}>LOC</option>
                                    </select></span>", "\"", VisibleIndex, contId);
        }
        return res;
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        string sql = string.Format(@"select det1.Id,det1.Id as ContId,job.JobNo,job.StatusCode as JobStatus,job.JobDate,job.YardRef as Depot,job.ClientRefNo,
job.PermitNo,job.Remark,job.SpecialInstruction,det1.ContainerNo,det1.ContainerType,job.EtaDate,job.EtdDate,
job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,det1.ScheduleDate,det1.StatusCode ,
'' as Haulier,
job.Terminalcode,job.JobType,det1.PortnetStatus,det1.Permit,
(select '<span class='''+det2.Statuscode+''' id=''trip_'+cast( det2.Id as nvarchar)+''' onclick=''OpenDetail(this);''>'+TripCode+'</span>' from CTM_JobDet2 as det2 where Det1Id=det1.Id for xml path('')) as trips,
'' as tripDetail
from CTM_Job as job left outer join CTM_JobDet1 as det1 on job.JobNo=det1.JobNo ");
        where = GetWhere(where, string.Format(@" det1.ContainerNo<>''"));
        if (txt_search_ContNo.Text.Trim() != "")
        {
            where = GetWhere(where, string.Format(@" isnull(det1.ContainerNo,'') like '%{0}%'", txt_search_ContNo.Text.Trim()));
        }
        if (txt_search_dateFrom.Date > new DateTime(1900, 1, 1))
        {
            where = GetWhere(where, " datediff(d,'" + txt_search_dateFrom.Date + "',job.jobDate)>=0");
        }
        if (txt_search_dateTo.Date > new DateTime(1900, 1, 1))
        {
            where = GetWhere(where, " datediff(d,'" + txt_search_dateTo.Date + "',job.jobDate)<=0");
        }
        if (cbb_StatusCode.Text != "All")
        {
            where = GetWhere(where, " det1.StatusCode='" + cbb_StatusCode.Text.Trim() + "'");
        }

        if (where.Length > 0)
        {
            sql += " where " + where + " order by job.EtaDate,job.JobNo,det1.Id,det1.StatusCode desc, job.JobDate asc";
        }

        DataTable dt = ConnectSql.GetTab(sql);
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



    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("NewTripInLine"))
            {
                NewTrip_inLine(e, SafeValue.SafeInt(ar[1], -1), ar[2]);
            }
            if (ar[0].Equals("OpenDetail"))
            {
                OpenDetail_GetData(e, SafeValue.SafeInt(ar[1], -1));
            }
            if (ar[0].Equals("UpdateInline"))
            {
                Update_Inline(e, SafeValue.SafeInt(ar[1], -1));
            }
            if (ar[0].Equals("ContChangeStatus"))
            {
                ContainerChangeStatus(e, SafeValue.SafeInt(ar[1], -1));
            }
        }
    }

    private void ContainerChangeStatus(DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex)
    {
        if (rowIndex < 0)
        {
            e.Result = "Save Error";
            return;
        }
        TextBox txt_contId = this.grid_Transport.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["tripDetail"], "txt_contId") as TextBox;
        string sql = string.Format(@"update ctm_jobdet1 set StatusCode=(case when StatusCode='Completed' then 'InTransit' else 'Completed' end) where Id={0}
select StatusCode from ctm_jobdet1 where Id={0}", SafeValue.SafeInt(txt_contId.Text, 0));
        string re = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        e.Result = "success:" + re + "&,&" + ShowColor(re);
    }
    private void Update_Inline(DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex)
    {
        if (rowIndex < 0)
        {
            e.Result = "Save Error";
            return;
        }
        TextBox txt_tripId = this.grid_Transport.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["tripDetail"], "txt_tripId") as TextBox;
        ASPxButtonEdit btn_Driver = this.grid_Transport.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["tripDetail"], "btn_Driver") as ASPxButtonEdit;
        ASPxButtonEdit txt_trailer = this.grid_Transport.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["tripDetail"], "txt_trailer") as ASPxButtonEdit;
        ASPxButtonEdit txt_parkingLot = this.grid_Transport.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["tripDetail"], "txt_parkingLot") as ASPxButtonEdit;
        ASPxMemo txt_toAddress = this.grid_Transport.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["tripDetail"], "txt_toAddress") as ASPxMemo;


        string sql = string.Format(@"select DriverCode from ctm_jobdet2 where Id=@Id");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", txt_tripId.Text, SqlDbType.Int));
        string Driver_old = ConnectSql_mb.ExecuteScalar(sql, list).context;
        string re = HttpContext.Current.User.Identity.Name + "," + txt_tripId.Text + "," + btn_Driver.Text;
        if (!btn_Driver.Text.Equals(Driver_old))
        {
            re += "," + Driver_old;
        }

        sql = string.Format(@"update ctm_jobdet2 set 
ToParkingLot=@ToParkingLot,ToCode=@ToCode,DriverCode=@DriverCode,ChessisCode=@ChessisCode
where Id=@Id");
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@Id", txt_tripId.Text, SqlDbType.Int));
        list.Add(new ConnectSql_mb.cmdParameters("@ToParkingLot", txt_parkingLot.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ToCode", txt_toAddress.Text, SqlDbType.NVarChar, 300));
        list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", btn_Driver.Text, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@ChessisCode", txt_trailer.Text, SqlDbType.NVarChar, 100));

        ConnectSql_mb.sqlResult res = ConnectSql_mb.ExecuteNonQuery(sql, list);
        if (res.status)
        {
            e.Result = "success:" + re;
        }
        else
        {
            e.Result = "Save Error:" + res.context;
        }

        //e.Result = txt_tripId.Text;

    }

    private void OpenDetail_GetData(DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex)
    {
        if (rowIndex < 0)
        {
            e.Result = "Error";
            return;
        }
        TextBox txt_tripId = this.grid_Transport.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["tripDetail"], "txt_tripId") as TextBox;
        string sql = string.Format(@"select * from ctm_jobdet2 where Id={0}", txt_tripId.Text);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            string driver = SafeValue.SafeString(dt.Rows[0]["DriverCode"]);
            string trailer = SafeValue.SafeString(dt.Rows[0]["ChessisCode"]);
            string parkinglot = SafeValue.SafeString(dt.Rows[0]["ToParkingLot"]);
            string toaddress = SafeValue.SafeString(dt.Rows[0]["ToCode"]);
            e.Result = "success:" + driver + "&,&" + trailer + "&,&" + parkinglot + "&,&" + toaddress;
        }
        else
        {
            e.Result = "Error:trip inexistence";
        }

        //e.Result = txt_tripId.Text;

    }
    private void NewTrip_inLine(DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e, int rowIndex, string tripType)
    {
        if (rowIndex < 0)
        {
            e.Result = "Save Error";
            return;
        }

        TextBox txt_contId = this.grid_Transport.FindRowCellTemplateControl(rowIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["tripDetail"], "txt_contId") as TextBox;


        string sql = string.Format(@"select job.JobNo,PickupFrom,DeliveryTo,YardRef,JobType,det1.ContainerNo  
from ctm_jobdet1 as det1 
left outer join ctm_job as job on job.jobno=det1.jobno
where det1.Id={0}", SafeValue.SafeString(txt_contId.Text, ""));
        DataTable job = ConnectSql.GetTab(sql);
        if (job.Rows.Count <= 0)
        {
            e.Result = "Save Error: Container inexistence";
            return;
        }
        sql = string.Format(@"select top 1 * from ctm_jobdet2 where Det1Id={0} order by Id desc", SafeValue.SafeString(txt_contId.Text, ""));
        DataTable trip = ConnectSql.GetTab(sql);

        string jobNo = SafeValue.SafeString(job.Rows[0]["JobNo"]);
        string ContainerNo = SafeValue.SafeString(job.Rows[0]["ContainerNo"]);
        string job_from = SafeValue.SafeString(job.Rows[0]["PickupFrom"]);
        string job_to = SafeValue.SafeString(job.Rows[0]["DeliveryTo"]);
        string job_Depot = SafeValue.SafeString(job.Rows[0]["YardRef"]);
        string P_From = "";
        string P_From_Pl = "";
        string P_To = "";// DeliveryTo.Text;
        string trailer = "";
        string JobType = SafeValue.SafeString(job.Rows[0]["JobType"]);
        string TripCode = "";
        DateTime FromDate = DateTime.Now;
        string FromTime = DateTime.Now.ToString("HH:mm");

        string newType = SafeValue.SafeString(tripType, "SHF");
        TripCode = newType;
        switch (newType)
        {
            case "COL":
                if (add_newTrip_CheckMultiple(e, newType, jobNo, SafeValue.SafeString(txt_contId.Text, ""))) { return; }
                P_From = job_Depot;
                P_To = job_from;
                break;
            case "EXP":
                if (add_newTrip_CheckMultiple(e, newType, jobNo, SafeValue.SafeString(txt_contId.Text, ""))) { return; }
                P_From = job_from;
                P_To = job_to;
                break;
            case "IMP":
                if (add_newTrip_CheckMultiple(e, newType, jobNo, SafeValue.SafeString(txt_contId.Text, ""))) { return; }
                P_From = job_from;
                P_To = job_to;
                break;
            case "RET":
                if (add_newTrip_CheckMultiple(e, newType, jobNo, SafeValue.SafeString(txt_contId.Text, ""))) { return; }
                P_To = job_Depot;
                break;
            case "SHF":
                P_To = job_from;
                break;
            case "LOC":
                P_From = job_from;
                P_To = job_to;
                break;
        }
        if (trip.Rows.Count > 0)
        {
            P_From = trip.Rows[0]["ToCode"].ToString();
            P_From_Pl = trip.Rows[0]["ToParkingLot"].ToString();
            trailer = trip.Rows[0]["ChessisCode"].ToString();
        }


        sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,ChessisCode,FromCode,FromDate,FromTime,ToCode,ToDate,ToTime,Det1Id,Statuscode,
BayCode,SubletFlag,StageCode,StageStatus,TripCode,Overtime,OverDistance,FromParkingLot,DoubleMounting) values ('{0}','{1}','','','{2}','{3}','{4}','{5}','{6}','{4}','{5}','{7}','P',
'','N','','','{8}','Normal','N','{9}','No')
select @@IDENTITY", jobNo, ContainerNo, trailer, P_From, FromDate, FromTime, P_To, SafeValue.SafeString(txt_contId.Text, ""),
                                            TripCode, P_From_Pl);
        string tripId = ConnectSql.ExecuteScalar(sql).ToString();
        sql = string.Format(@"select count(*) from ctm_jobdet2 where det1Id={0}", SafeValue.SafeString(txt_contId.Text, ""));
        int rowSum = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
        if (rowSum == 1)
        {
            sql = string.Format(@"update ctm_jobdet1 set StatusCode='{0}' where Id={1}", "InTransit", SafeValue.SafeString(txt_contId.Text, ""));
            ConnectSql.ExecuteSql(sql);
        }
        e.Result = "success:" + txt_contId.Text + "," + tripId + "," + newType;
    }
    public string ShowColor(string status)
    {
        string color = "";
        if (status == "New")
        {
            color = "orange";
        }
        if (status == "Scheduled")
        {
            color = "orange";
        }
        if (status == "InTransit")
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
        return color;
    }

    private bool add_newTrip_CheckMultiple(DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e, string Type, string jobNo, string contId)
    {
        bool res = false;
        string sql = string.Format(@"select Id,TripCode from ctm_jobdet2 where JobNo='{0}' and Det1Id={1} and TripCode='{2}' order by Id desc", jobNo, contId, Type);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0)
        {
            //throw new Exception("Exist trip:" + Type);
            e.Result = "Save Error:Exist trip " + Type;
            res = true;
        }
        return res;
    }
}
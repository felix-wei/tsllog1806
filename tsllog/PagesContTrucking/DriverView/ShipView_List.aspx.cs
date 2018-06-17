using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PagesContTrucking_DriverView_ShipView_List : System.Web.UI.Page
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
            txt_search_dateFrom.Date = DateTime.Now.AddDays(-15);
            txt_search_dateTo.Date = DateTime.Now.AddDays(8);
            btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {

        string sql1 = "select Role,CustId,xxp.Name from [User] as u left outer join XXParty as xxp on CustId=PartyId where u.Name='" + EzshipHelper.GetUserName() + "'";
        DataTable dt1 = ConnectSql.GetTab(sql1);
        string Role = "";
        if (dt1.Rows.Count > 0)
        {
            Role = SafeValue.SafeString(dt1.Rows[0][0]);
        }
        string sql = string.Format(@"select job.JobNo,job.JobDate,det1.ContainerNo,det1.ContainerType,job.EtaDate,job.EtdDate,job.Pol,job.Pod,job.Vessel,job.Voyage,job.PickupFrom,job.DeliveryTo,det1.ScheduleDate,det1.StatusCode ,(select top 1 code from XXParty where PartyId=job.ClientId) as client,(select top 1 code from XXParty where PartyId=job.HaulierId) as Haulier,job.Terminalcode,job.JobType, case when '"+Role+@"'='Client' and job.statuscode<>'USE' then 'false' else '0' end as Role 
from CTM_Job as job  
left outer join CTM_JobDet1 as det1 on job.JobNo=det1.JobNo 
where job.JobNo like '%{0}%' and isnull(det1.ContainerNo,'') like '%{1}%' ", txt_search_jobNo.Text, txt_search_ContNo.Text);
        if (txt_search_dateFrom.Date > new DateTime(1900, 1, 1))
        {
            sql += " and datediff(d,'" + txt_search_dateFrom.Date + "',job.jobDate)>=0";
        }
        if (txt_search_dateTo.Date > new DateTime(1900, 1, 1))
        {
            sql += " and datediff(d,'" + txt_search_dateTo.Date + "',job.jobDate)<=0";
        }
        if (cbb_StatusCode.Text != "All")
        {
            sql += " and det1.StatusCode='" + cbb_StatusCode.Text + "'";
        }
        if (btn_ClientId.Text.Length > 0)
        {
            sql += " and job.clientId='" + btn_ClientId.Text + "'";
        }
        if (search_JobType.Text.Length > 0)
        {
            sql += " and job.JobType='" + search_JobType.Text + "'";
        }
        //string sql1 = "select Role,CustId,xxp.Name from [User] as u left outer join XXParty as xxp on CustId=PartyId where u.Name='" + EzshipHelper.GetUserName() + "'";
        //DataTable dt1 = ConnectSql.GetTab(sql1);
        if (dt1 != null && dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString().Equals("Client"))
        {
            sql += " and ClientId='" + dt1.Rows[0][1] + "'";
            btn_ClientId.Value = dt1.Rows[0][1];
            txt_ClientName.Text = SafeValue.SafeString(dt1.Rows[0][2]);
            btn_ClientId.Enabled = false;
        }
        sql += " order by job.JobDate desc";
        DataTable dt = ConnectSql.GetTab(sql);
        this.grid_Transport.DataSource = dt;
        this.grid_Transport.DataBind();
    }
}
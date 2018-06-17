using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_DispatchPlanner_AddTrip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["DP_AddTrip"] = null;
            if (Request.QueryString["det1Id"] != null && Request.QueryString["JobNo"] != null && Request.QueryString["ContNo"] != null)
            {
                this.lb_det1Id.Text = Request.QueryString["det1Id"];
                this.lb_JobNo.Text = Request.QueryString["JobNo"];
                this.lb_ContNo.Text = Request.QueryString["ContNo"];
                this.dsCont.FilterExpression = "Id=" + lb_det1Id.Text;
                this.dsJob.FilterExpression = "JobNo='" + lb_JobNo.Text + "'";
                this.dsCtmDriverLog.FilterExpression = "datediff(d,Date,(select ScheduleDate from CTM_JobDet1 where Id=" + lb_det1Id.Text + "))=0";
                Session["DP_AddTrip"] = "det1Id='" + lb_det1Id.Text + "'";
                this.lb_ContDate.Text = SafeValue.SafeString(ConnectSql.GetTab("select ScheduleDate from CTM_JobDet1 where Id=" + lb_det1Id.Text).Rows[0][0], DateTime.Now.ToShortDateString());
            }
            threeRowsDataBind();
        }
        this.dsTrip.FilterExpression = Session["DP_AddTrip"].ToString();
    }
    #region Trip
    protected void grid_Trip_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmJobDet2));
        }
    }
    protected void grid_Trip_BeforePerformDataSelect(object sender, EventArgs e)
    {
        //this.dsTrip.FilterExpression = " JobNo='" + lb_JobNo.Text + "' and ContainerNo='" + lb_ContNo.Text + "'";
        //this.dsTrip.FilterExpression = "det1Id='" + lb_det1Id.Text + "'";
    }
    protected void grid_Trip_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
    }
    protected void grid_Trip_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        check_Trip_Status("0", e.NewValues["DriverCode"].ToString(), e.NewValues["Statuscode"].ToString());
        e.NewValues["JobNo"] = lb_JobNo.Text;
        e.NewValues["ContainerNo"] = lb_ContNo.Text;
        e.NewValues["TripCode"] = SafeValue.SafeString(e.NewValues["TripCode"], "");
        e.NewValues["Det1Id"] = SafeValue.SafeInt(lb_det1Id.Text, 0);
        e.NewValues["BayCode"] = SafeValue.SafeString(e.NewValues["BayCode"], "");
        e.NewValues["SubletFlag"] = SafeValue.SafeString(e.NewValues["SubletFlag"], "Y");
    }
    protected void grid_Trip_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["ContainerNo"] = lb_ContNo.Text;
        e.NewValues["Statuscode"] = "P";
        e.NewValues["FromDate"] = DateTime.Now;
        e.NewValues["ToDate"] = DateTime.Now;
        e.NewValues["SubletFlag"] = "N";
        e.NewValues["BayCode"] = "B1";
    }
    protected void grid_Trip_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

    protected void grid_Trip_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxLabel lb_tripId = grd.FindEditFormTemplateControl("lb_tripId") as ASPxLabel;
        check_Trip_Status(lb_tripId.Text.ToString(), e.NewValues["DriverCode"].ToString(), e.NewValues["Statuscode"].ToString());
        e.NewValues["TripCode"] = SafeValue.SafeString(e.NewValues["TripCode"], "");
        e.NewValues["BayCode"] = SafeValue.SafeString(e.NewValues["BayCode"], "");
        e.NewValues["SubletFlag"] = SafeValue.SafeString(e.NewValues["SubletFlag"], "Y");
        string temp = e.NewValues["FromTime"].ToString();
    }

    private void check_Trip_Status(string id, string driverCode, string status)
    {
        if (driverCode.Trim().Length == 0)
        {
            return;
        }
        if (status == "S" || status == "P")
        {
            string sql = string.Format(@"select COUNT(*) from CTM_JobDet2 where DriverCode='{0}' and Statuscode='{2}' and Id<>'{1}'", driverCode, id, status);
            int result = SafeValue.SafeInt(ConnectSql.GetTab(sql).Rows[0][0], 0);
            if (result > 0)
            {
                throw new Exception("Status:'" + status + "' is existing for " + driverCode);
            }
        }
    }
    protected void grid_Trip_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
    }
    protected void grid_Trip_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
    }
    protected void grid_Trip_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        updateJob_By_Date(SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
    }
    private void updateJob_By_Date(string Id)
    {
        string sql = string.Format(@"update CTM_Job set UpdateBy='{0}',UpdateDateTime=getdate() where Id='{1}'", HttpContext.Current.User.Identity.Name, Id);
        ConnectSql.ExecuteSql(sql);
    }

    #endregion
    private void threeRowsDataBind()
    {
        this.cbb_StatusCode1.SelectedIndex = 0;
        this.cbb_StatusCode2.SelectedIndex = 0;
        this.cbb_StatusCode3.SelectedIndex = 0;
        string timeNow = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
        this.txt_fromTime1.Text = timeNow;
        this.txt_fromTime2.Text = timeNow;
        this.txt_fromTime3.Text = timeNow;

        
    }

    protected void grid_Trip_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters == "Three")
        {
            check_Trip_Status("0", cbb_Driver1.Text, cbb_StatusCode1.Value.ToString());
            check_Trip_Status("0", cbb_Driver2.Text, cbb_StatusCode2.Value.ToString());
            check_Trip_Status("0", cbb_Driver3.Text, cbb_StatusCode3.Value.ToString());
            string sql = "";
            if (cb_Row1.Checked)
            {
                sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,Det1Id,Statuscode,TripCode,FromTime,FromCode,ToCode,SubletFlag) values ('{0}','{1}','{2}',(select top 1 Towhead from CTM_DriverLog where DATEDIFF(d,Date,(select ScheduleDate from CTM_JobDet1 where Id='{3}'))=0 and Driver='{2}'),'{3}','{4}','{5}','{6}','{7}','{8}','Y')", lb_JobNo.Text, lb_ContNo.Text, cbb_Driver1.Text, lb_det1Id.Text, cbb_StatusCode1.Value.ToString(), cbb_TripCode1.Text, txt_fromTime1.Text, txt_FromCode1.Text, txt_ToCode1.Text);
                ConnectSql.ExecuteSql(sql);
            }
            if (cb_Row2.Checked)
            {
                sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,Det1Id,Statuscode,TripCode,FromTime,FromCode,ToCode,SubletFlag) values ('{0}','{1}','{2}',(select top 1 Towhead from CTM_DriverLog where DATEDIFF(d,Date,(select ScheduleDate from CTM_JobDet1 where Id='{3}'))=0 and Driver='{2}'),'{3}','{4}','{5}','{6}','{7}','{8}','Y')", lb_JobNo.Text, lb_ContNo.Text, cbb_Driver2.Text, lb_det1Id.Text, cbb_StatusCode2.Value.ToString(), cbb_TripCode2.Text, txt_fromTime2.Text, txt_FromCode2.Text, txt_ToCode2.Text);
                ConnectSql.ExecuteSql(sql);
            }
            if (cb_Row3.Checked)
            {
                sql = string.Format(@"insert into CTM_JobDet2 (JobNo,ContainerNo,DriverCode,TowheadCode,Det1Id,Statuscode,TripCode,FromTime,FromCode,ToCode,SubletFlag) values ('{0}','{1}','{2}',(select top 1 Towhead from CTM_DriverLog where DATEDIFF(d,Date,(select ScheduleDate from CTM_JobDet1 where Id='{3}'))=0 and Driver='{2}'),'{3}','{4}','{5}','{6}','{7}','{8}','Y')", lb_JobNo.Text, lb_ContNo.Text, cbb_Driver3.Text, lb_det1Id.Text, cbb_StatusCode3.Value.ToString(), cbb_TripCode3.Text, txt_fromTime3.Text, txt_FromCode3.Text, txt_ToCode3.Text);
                ConnectSql.ExecuteSql(sql);
            }
            threeRowsDataBind();
        }
    }
}
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_DriverReporting_IssueReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            search_from.Date = DateTime.Now;
            search_to.Date = DateTime.Now;
            btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = string.Format(@"(ActionType='' or ActionType='Prime Mover' or ActionType='Trailer' or ActionType='Accident' or ActionType='Others') and datediff(d,ReportDate,'{0}')<=0 and datediff(d,ReportDate,'{1}')>=0", SafeValue.SafeDate(search_from.Date, DateTime.Now), SafeValue.SafeDate(search_to.Date, DateTime.Now));
        if (search_driver.Text.Trim().Length > 0)
        {
            where += " and CreateBy='" + search_driver.Text.Trim() + "' ";
        }
        if (search_vehicle.Text.Trim().Length > 0)
        {
            where += " and VehicleNo='" + search_vehicle.Text.Trim() + "' ";
        }
        this.dsTransport.FilterExpression = where;
    }
    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["VehicleNo"] = SafeValue.SafeString(e.NewValues["VehicleNo"], "");
        e.NewValues["Note"] = SafeValue.SafeString(e.NewValues["Note"], "");
        e.NewValues["CreateBy"] = SafeValue.SafeString(e.NewValues["CreateBy"], "");
        e.NewValues["ReportDate"] = SafeValue.SafeDate(e.NewValues["ReportDate"], DateTime.Now);
        e.NewValues["ActionType"] = SafeValue.SafeString(e.NewValues["ActionType"], "");
        e.NewValues["ActionTaken"] = SafeValue.SafeString(e.NewValues["ActionTaken"], "");
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"], "");
    }
    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["VehicleNo"] = SafeValue.SafeString(e.NewValues["VehicleNo"], "");
        e.NewValues["Note"] = SafeValue.SafeString(e.NewValues["Note"], "");
        e.NewValues["CreateBy"] = SafeValue.SafeString(e.NewValues["CreateBy"], "");
        e.NewValues["ReportDate"] = SafeValue.SafeDate(e.NewValues["ReportDate"], DateTime.Now);
        e.NewValues["ActionType"] = SafeValue.SafeString(e.NewValues["ActionType"], "");
        e.NewValues["ActionTaken"] = SafeValue.SafeString(e.NewValues["ActionTaken"], "");
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"], "");
    }
    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }


    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["ReportDate"] = DateTime.Now;
    }
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.VehicleIssueReport));
        }
    }



    #region photo
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmAttachment));
        }
    }
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid_Transport.FindEditFormTemplateControl("pageControl") as ASPxPageControl;

        //ASPxTextBox jobNo = pageControl.FindControl("txt_JobNo") as ASPxTextBox;
        ASPxTextBox jobNo = this.grid_Transport.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        int jobId = SafeValue.SafeInt(jobNo.Text, 0);
        string where = "1=0";
        if (jobId > 0)
        {
            where = "TripId=" + SafeValue.SafeInt(jobNo.Text, 0) + " and JobType='IssueRp'";
        }
        this.dsJobPhoto.FilterExpression = where;
    }
    protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Photo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grd_Photo_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["FileNote"] = " ";
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }

    #endregion
}
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_DriverReporting_Mileage : System.Web.UI.Page
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
        string where = string.Format(@"datediff(d,ReportDate,'{0}')<=0 and datediff(d,ReportDate,'{1}')>=0", SafeValue.SafeDate(search_from.Date, DateTime.Now), SafeValue.SafeDate(search_to.Date, DateTime.Now));
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
        e.NewValues["Value"] = SafeValue.SafeDecimal(e.NewValues["Value"]);
    }
    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }


    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["VehicleNo"] = SafeValue.SafeString(e.NewValues["VehicleNo"], "");
        e.NewValues["Note"] = SafeValue.SafeString(e.NewValues["Note"], "");
        e.NewValues["CreateBy"] = SafeValue.SafeString(e.NewValues["CreateBy"], "");
        e.NewValues["ReportDate"] = SafeValue.SafeDate(e.NewValues["ReportDate"], DateTime.Now);
        e.NewValues["Value"] = SafeValue.SafeDecimal(e.NewValues["Value"]);
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["ReportDate"] = DateTime.Now;
        e.NewValues["Value"] = 0;
    }
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.VehicleMileage));
        }
    }
}
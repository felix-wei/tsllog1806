using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Vehicle_Rent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["CTM_Rent"] = null;
            date_searchFrom.Date = DateTime.Now.AddDays(-7);
            date_searchTo.Date = DateTime.Now.AddDays(3);
            btn_search_Click(null, null);
        }
        if (Session["CTM_Rent"] != null)
        {
            this.dsTransport.FilterExpression = Session["CTM_Rent"].ToString();
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (date_searchFrom.Date < new DateTime(1900, 1, 1))
        {
            date_searchFrom.Date = DateTime.Now.AddDays(-7);
        }
        if (date_searchTo.Date < new DateTime(1900, 1, 1))
        {
            date_searchTo.Date = DateTime.Now.AddDays(3);
        }
        string where = "datediff(d,EventDate,'" + date_searchFrom.Date + "')<=0 and datediff(d,EventDate,'" + date_searchTo.Date + "')>=0 and EventCode='Rent'";
        Session["CTM_Rent"] = where;
        this.dsTransport.FilterExpression = where;
    }
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.RefVehicleLog));
        }
    }
    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["EventCode"] = "Rent";
        e.NewValues["EventDate"] = SafeValue.SafeDate(e.NewValues["EventDate"], new DateTime(1753, 1, 1));
        e.NewValues["DriverCode"] = SafeValue.SafeString(e.NewValues["DriverCode"], "");
        e.NewValues["VehicleCode"] = SafeValue.SafeString(e.NewValues["VehicleCode"], "");
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"], "");
        e.NewValues["EventType"] = SafeValue.SafeString(e.NewValues["EventType"], "");
        e.NewValues["TotalPrice"] = SafeValue.SafeDecimal(e.NewValues["TotalPrice"], 0);
        e.NewValues["DocNo"] = SafeValue.SafeString(e.NewValues["DocNo"], "");
        e.NewValues["SupplierCode"] = SafeValue.SafeString(e.NewValues["SupplierCode"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
    }
    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["EventDate"] = SafeValue.SafeDate(e.NewValues["EventDate"], new DateTime(1753, 1, 1));
        e.NewValues["DriverCode"] = SafeValue.SafeString(e.NewValues["DriverCode"], "");
        e.NewValues["VehicleCode"] = SafeValue.SafeString(e.NewValues["VehicleCode"], "");
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"], "");
        e.NewValues["EventType"] = SafeValue.SafeString(e.NewValues["EventType"], "");
        e.NewValues["TotalPrice"] = SafeValue.SafeDecimal(e.NewValues["TotalPrice"], 0);
        e.NewValues["DocNo"] = SafeValue.SafeString(e.NewValues["DocNo"], "");
        e.NewValues["SupplierCode"] = SafeValue.SafeString(e.NewValues["SupplierCode"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["EventDate"] = DateTime.Now;
        e.NewValues["TotalPrice"] = 0;
    }
    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
}
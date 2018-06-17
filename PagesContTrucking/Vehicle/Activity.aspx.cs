using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Vehicle_Activity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["CTM_Activity"] = null;
            date_searchFrom.Date = new DateTime(DateTime.Today.Year,1,1); //.Now.AddDays(-90);
            date_searchTo.Date = DateTime.Now.AddDays(3);
            if (Request.QueryString["no"] != null)
                txt_Code.Text = SafeValue.SafeString(Request.QueryString["no"]);
        }
        //btn_search_Click(null, null);
        if (Session["CTM_Activity"] != null)
        {
            this.dsTransport.FilterExpression = Session["CTM_Activity"].ToString();
        }
        else
        {
            btn_search_Click(null, null);
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

        string where = "datediff(d,EventDate,'" + date_searchFrom.Date + "')<=0 and datediff(d,EventDate,'" + date_searchTo.Date + "')>=0 and EventCode='Activity'";
        if (txt_Code.Text.Length > 0)
        {
            where += " and VehicleCode='"+txt_Code.Text+"'";
        }
        Session["CTM_Activity"] = where;
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
        e.NewValues["EventCode"] = "Activity";
        e.NewValues["EventDate"] = SafeValue.SafeDate(e.NewValues["EventDate"], new DateTime(1753, 1, 1));
        e.NewValues["DriverCode"] = SafeValue.SafeString(e.NewValues["DriverCode"], "");
        e.NewValues["VehicleCode"] = SafeValue.SafeString(e.NewValues["VehicleCode"], "");
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"], "");
        e.NewValues["EventType"] = SafeValue.SafeString(e.NewValues["EventType"], "");
        e.NewValues["TotalPrice"] = SafeValue.SafeDecimal(e.NewValues["TotalPrice"], 0);
        e.NewValues["DocNo"] = SafeValue.SafeString(e.NewValues["DocNo"], "");
        e.NewValues["SupplierCode"] = SafeValue.SafeString(e.NewValues["SupplierCode"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        e.NewValues["EventStatus"] = SafeValue.SafeString(e.NewValues["EventStatus"]);
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
        e.NewValues["EventStatus"] = SafeValue.SafeString(e.NewValues["EventStatus"]);
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
    protected void grid_Transport_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string temp = e.Parameters;
        string[] ar = temp.Split('_');
        if (ar.Length == 2)
        {
            if (ar[0] == "OpenInline")
            {
                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxLabel lbl_VehicleCode = this.grid_Transport.FindRowCellTemplateControl(rowIndex, null, "lbl_VehicleCode") as ASPxLabel;
                e.Result = lbl_VehicleCode.Text;
            }
        }
    }
}
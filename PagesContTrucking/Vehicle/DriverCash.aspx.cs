using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2;

public partial class PagesContTrucking_Vehicle_DriverCash : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["CTM_DriverCash"] = null;
            date_searchFrom.Date = DateTime.Now.AddDays(-7);
            date_searchTo.Date = DateTime.Now.AddDays(3);
            btn_search_Click(null, null);
        }
        if (Session["CTM_DriverCash"] != null)
        {
            this.dsTransport.FilterExpression = Session["CTM_DriverCash"].ToString();
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
        string where = "datediff(d,EventDate,'" + date_searchFrom.Date + "')<=0 and datediff(d,EventDate,'" + date_searchTo.Date + "')>=0";
        Session["CTM_DriverCash"] = where;
        this.dsTransport.FilterExpression = where;
    }
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.Job_Cost));
        }
    }
    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string eventType = SafeValue.SafeString(e.NewValues["EventType"], "");
        e.NewValues["LineSource"] = "M";
        e.NewValues["ChgCode"] = eventType;
        e.NewValues["ChgCodeDe"] = eventType;
        e.NewValues["EventDate"] = SafeValue.SafeDate(e.NewValues["EventDate"], new DateTime(1753, 1, 1));
        
        e.NewValues["EventType"] = eventType;
        e.NewValues["ExpiryDate"] = SafeValue.SafeDate(e.NewValues["ExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["DriverCode"] = SafeValue.SafeString(e.NewValues["DriverCode"], "");
        decimal amt= SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["Qty"] = SafeValue.SafeDecimal(1);
        e.NewValues["Price"] = amt;
        e.NewValues["LocAmt"]= SafeValue.ChinaRound(1* SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        e.NewValues["VehicleNo"] = SafeValue.SafeString(e.NewValues["VehicleNo"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        string jobNo = SafeValue.SafeString(e.NewValues["JobNo"], "");
        string contNo = SafeValue.SafeString(e.NewValues["ContNo"], "");
        e.NewValues["JobNo"] = jobNo;
        e.NewValues["ContNo"] = contNo;
        e.NewValues["TripNo"] = SafeValue.SafeString(e.NewValues["TripNo"], "");
        e.NewValues["TripType"] = SafeValue.SafeString(e.NewValues["TripType"], "");
        e.NewValues["LineType"] = "DC";
    }
    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["EventDate"] = SafeValue.SafeDate(e.NewValues["EventDate"], new DateTime(1753, 1, 1));
        string eventType = SafeValue.SafeString(e.NewValues["EventType"], "");
        e.NewValues["LineSource"] = "M";
        e.NewValues["ChgCode"] = eventType;
        e.NewValues["ChgCodeDe"] = eventType;
        e.NewValues["EventType"] = eventType;
        e.NewValues["ExpiryDate"] = SafeValue.SafeDate(e.NewValues["ExpiryDate"], new DateTime(1753, 1, 1));
        e.NewValues["DriverCode"] = SafeValue.SafeString(e.NewValues["DriverCode"], "");
        decimal amt = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["Price"] = amt;
        e.NewValues["LocAmt"] = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        e.NewValues["VehicleNo"] = SafeValue.SafeString(e.NewValues["VehicleNo"], "");
        e.NewValues["DocAmt"] = SafeValue.SafeDecimal(e.NewValues["DocAmt"], 0);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
        string jobNo = SafeValue.SafeString(e.NewValues["JobNo"], "");
        string contNo = SafeValue.SafeString(e.NewValues["ContNo"], "");
        e.NewValues["JobNo"] = jobNo;
        e.NewValues["ContNo"] = contNo;
        e.NewValues["TripNo"] = SafeValue.SafeString(e.NewValues["TripNo"], "");
        e.NewValues["TripType"] = SafeValue.SafeString(e.NewValues["TripType"], "");
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["EventDate"] = DateTime.Now;
        e.NewValues["EventExpiryDate"] = DateTime.Now;
        e.NewValues["TotalAmount"] = 0;
        e.NewValues["DocAmt"] = 0;
    }
    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

}
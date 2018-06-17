using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

public partial class WareHouse_Job_JobCount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobCount));
        }
        string _d = Request.QueryString["d"] ?? DateTime.Today.ToString("dd/MM/yyyy");
        txt_end.Date = DateTime.Parse(_d.Substring(6, 4) + "-" + _d.Substring(3, 2) + "-" + _d.Substring(0, 2));
        dsJobCount.FilterExpression = " DateFrom >= '"+txt_end.Date.ToString("yyyy-MM-dd")+"'";

    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        //e.NewValues["CreditDay"] = 0;
        e.NewValues["Count"] = 0;
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Count"] = SafeValue.SafeInt(e.NewValues["Count"], 0);
        e.NewValues["DateFrom"] = SafeValue.SafeDate(e.NewValues["DateFrom"], DateTime.Now);
        e.NewValues["DateTo"] = SafeValue.SafeDate(e.NewValues["DateTo"], DateTime.Now);
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DateTime dateFrom = SafeValue.SafeDate(e.NewValues["DateFrom"],DateTime.Now);
        DateTime dateTo = SafeValue.SafeDate(e.NewValues["DateTo"], DateTime.Now);
        if (dateFrom.Date > dateTo.Date &&SafeValue.SafeString(e.NewValues["DateTo"]) != "")
        {
            throw new Exception("Fail! the time is  not correct");
            return;
        }
        e.NewValues["Count"] = SafeValue.SafeInt(e.NewValues["Count"],0);
        e.NewValues["DateFrom"] = SafeValue.SafeDate(e.NewValues["DateFrom"], DateTime.Now);
        e.NewValues["DateTo"] =SafeValue.SafeDate(e.NewValues["DateTo"], DateTime.Now);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

public partial class Modules_Hr_Master_Overtime : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Overtime"] = "1=1";
        }
        if (Session["Overtime"] != null)
            this.dsHrOvertime.FilterExpression = Session["Overtime"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("Overtime", true);
    }
    #region Overtime
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HrOvertime));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["TypeId"] = "Overtime";
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeInt(e.NewValues["Person"], 0) == 0)
            throw new Exception("Employee can not be null !!!");

        e.NewValues["TypeId"] = "Overtime";
        decimal hours = SafeValue.SafeDecimal(e.NewValues["Hour"]);
        decimal tim = SafeValue.SafeDecimal(e.NewValues["Time"]);
        decimal rate = SafeValue.SafeDecimal(e.NewValues["HoursRate"]);
        e.NewValues["TotalAmt"] = SafeValue.ChinaRound(hours*rate*tim,2);
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeInt(e.NewValues["Person"], 0) == 0)
            throw new Exception("Employee can not be null !!!");
        e.NewValues["Person"] = SafeValue.SafeInt(e.NewValues["Person"], 0);
        decimal hours = SafeValue.SafeDecimal(e.NewValues["Hour"]);
        decimal tim = SafeValue.SafeDecimal(e.NewValues["Time"]);
        decimal rate = SafeValue.SafeDecimal(e.NewValues["HoursRate"]);
        e.NewValues["TotalAmt"] = SafeValue.ChinaRound(hours * rate * tim, 2);
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

    #endregion
}
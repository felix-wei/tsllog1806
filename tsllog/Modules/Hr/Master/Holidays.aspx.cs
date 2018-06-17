using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Hr_Master_Holidays : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Holidays"] = "1=1";
        }
        if (Session["Holidays"] != null)
            this.dsHolidays.FilterExpression = Session["Holidays"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("Holidays", true);
    }
    #region Overtime
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.Holiday));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Remark"] = "";
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["FromMonth"] = SafeValue.SafeInt(e.NewValues["FromMonth"],0);
        e.NewValues["FromDay"] = SafeValue.SafeInt(e.NewValues["FromDay"], 0);
        e.NewValues["ToMonth"] = SafeValue.SafeInt(e.NewValues["ToMonth"],0);
        e.NewValues["ToDay"] = SafeValue.SafeInt(e.NewValues["ToDay"], 0);
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["FromMonth"] = SafeValue.SafeInt(e.NewValues["FromMonth"], 0);
        e.NewValues["FromDay"] = SafeValue.SafeInt(e.NewValues["FromDay"], 0);
        e.NewValues["ToMonth"] = SafeValue.SafeInt(e.NewValues["ToMonth"], 0);
        e.NewValues["ToDay"] = SafeValue.SafeInt(e.NewValues["ToDay"], 0);
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

    #endregion
}
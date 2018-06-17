using System;
using System.Collections.Generic;
using System.Web;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class PagesHr_Job_Quote : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Quote"] = null;
            this.dsQuote.FilterExpression = "1=0";
        }
        if (Session["Quote"] != null)
        {
            this.dsQuote.FilterExpression = Session["Quote"].ToString();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string id = SafeValue.SafeString(txtSchName.Value,"");
        string where = "";
        if (id.Length > 0)
            where = String.Format("Person='{0}'", id);
        else
        {
            where = "1=1";
        }
        this.dsQuote.FilterExpression = where;
        Session["Quote"] = where;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("HR_Quote", true);
    }
    protected void grid_Quote_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrQuote));
    }
    protected void grid_Quote_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Amt"] = 0;
        e.NewValues["Person"] = 0;
    }
    protected void grid_Quote_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {

    }
    protected void grid_Quote_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {

    }
    protected void grid_Quote_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {

    }
    protected void grid_Quote_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Quote_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["PayItem"], "").Length < 1)
            throw new Exception("Item not be null !!!");
        e.NewValues["Person"] = SafeValue.SafeInt(e.NewValues["Person"], 0);
        e.NewValues["Date"] = SafeValue.SafeDate(e.NewValues["Date"], DateTime.Now);
        e.NewValues["CreateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDate"] = DateTime.Today;
    }
    protected void grid_Quote_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["PayItem"], "").Length < 1)
            throw new Exception("Item not be null !!!");
        e.NewValues["Person"] = SafeValue.SafeInt(e.NewValues["Person"], 0);
        e.NewValues["Amt"] = SafeValue.SafeDecimal(e.NewValues["Amt"], 0);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
    }
}

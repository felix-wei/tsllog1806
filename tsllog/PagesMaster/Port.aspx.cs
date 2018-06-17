using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class XX_Port : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Port"] = "Code<>''";
        }
        if (Session["Port"] != null)
            this.dsThePort.FilterExpression = Session["Port"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        if (this.txtName.Text.Trim() != "")
        {
            where = "Name like '" + this.txtName.Text.Trim() + "%'";
        }
        Session["Port"] = GetWhere(where,"Code<>''");
        this.dsThePort.FilterExpression = Session["Port"].ToString();
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("Port", true);
    }
    #region Port
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXPort));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
 

    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Code"], "") == "")
            throw new Exception("Code can not be null!!!");
        e.NewValues["UserId"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["EntryDate"] = DateTime.Now;
    }
    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        if (e.Exception == null)
        {
            Session["Port"] = "Code='"+e.NewValues["Code"]+"'";
            this.dsThePort.FilterExpression = "Code='" + e.NewValues["Code"] + "'";
        }
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"]);
        e.NewValues["CountryCode"] = SafeValue.SafeString(e.NewValues["CountryCode"]);
    }

    #endregion
    protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
    }

    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
}

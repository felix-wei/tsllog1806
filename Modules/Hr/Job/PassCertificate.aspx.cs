using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class Modules_Hr_Job_PassCertificate : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Item"] = "1=1";
        }
        if (Session["Item"] != null)
            this.dsPassCertificate.FilterExpression = Session["Item"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Session["Item"] = null;
        //    this.dsPayItem.FilterExpression = "1=0";
        //    btn_search_Click(null, null);
        //}
        //if (Session["Item"] != null)
        //{
        //    this.dsPayItem.FilterExpression = Session["Item"].ToString();
        //}
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        if (this.txtSchName.Text.Trim() != "")
        {
            where = "Employee= '" + this.txtSchName.Value + "'";
        }
        else
        {
            where = "1=1";
        }
        this.dsPassCertificate.FilterExpression = where;
        Session["Item"] = where;

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
        gridExport.WriteXlsToResponse("Pass Certificate", true);
    }
    #region Pass Certficate
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.PassCertificate));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string user = HttpContext.Current.User.Identity.Name;
        if (SafeValue.SafeString(e.NewValues["Employee"], "") == "")
            throw new Exception("Employee can not be null!!!");
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["TypeCode"] = SafeValue.SafeString(e.NewValues["TypeCode"]);
        e.NewValues["ExpiryDate"] = SafeValue.SafeDate(e.NewValues["ExpiryDate"],new DateTime(2000,01,01));
        e.NewValues["CreateBy"] = user;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = user;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        btn_search_Click(null, null);
    }
    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string user = HttpContext.Current.User.Identity.Name;
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["TypeCode"] = SafeValue.SafeString(e.NewValues["TypeCode"]);
        e.NewValues["ExpiryDate"] = SafeValue.SafeDate(e.NewValues["ExpiryDate"], new DateTime(2000, 01, 01));
        e.NewValues["UpdateBy"] = user;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }
    protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion
}
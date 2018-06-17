using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesMaster_Control_HelperAuthority : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string role = SafeValue.SafeString(this.search_role.Value);
        string where = "1=1";
        if (role != null && role.Length > 0)
        {
            where = "Role='" + role + "'";
        }
        this.dsAuthority.FilterExpression = where;
    }
    protected void gv_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HelperAuthority));
        }
    }
    protected void gv_BeforePerformDataSelect(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
    }
    protected void gv_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Status"] = "";
        e.NewValues["IsHid"] = "0";
    }
    protected void gv_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void gv_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["Frame"] = SafeValue.SafeString(e.NewValues["Frame"]);
        e.NewValues["Control"] = SafeValue.SafeString(e.NewValues["Control"]);
        e.NewValues["ControlType"] = SafeValue.SafeString(e.NewValues["ControlType"]);
        e.NewValues["Status"] = SafeValue.SafeString(e.NewValues["Status"]);
        e.NewValues["Role"] = SafeValue.SafeString(e.NewValues["Role"]);
        e.NewValues["IsHid"] = SafeValue.SafeString(e.NewValues["IsHid"], "0");
        e.NewValues["CreateDate"] = DateTime.Now;
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
    }
    protected void gv_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        e.NewValues["Frame"] = SafeValue.SafeString(e.NewValues["Frame"]);
        e.NewValues["Control"] = SafeValue.SafeString(e.NewValues["Control"]);
        e.NewValues["ControlType"] = SafeValue.SafeString(e.NewValues["ControlType"]);
        e.NewValues["Status"] = SafeValue.SafeString(e.NewValues["Status"]);
        e.NewValues["Role"] = SafeValue.SafeString(e.NewValues["Role"]);
        e.NewValues["IsHid"] = SafeValue.SafeString(e.NewValues["IsHid"], "0");
        e.NewValues["CreateDate"] = DateTime.Now;
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
    }
}
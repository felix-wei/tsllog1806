using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mobile_ChatGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.dsGroup.FilterExpression = "1=1";// "role_code='driver'";
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "1=1";
        if (txt_search.Text.Trim().Length > 0)
        {
            where += " and group_name like '%" + txt_search.Text.Trim() + "%' ";
        }

        this.dsGroup.FilterExpression = where;
    }
    protected void gv_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.MobileChatGroup));
        }
    }
    protected void gv_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = Helper.Safe.SafeString(e.Values["Id"]);
    }
    protected void gv_BeforePerformDataSelect(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
    }
    protected void gv_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["GroupName"] = SafeValue.SafeString(e.NewValues["GroupName"]);
        e.NewValues["Note1"] = SafeValue.SafeString(e.NewValues["Note1"]);
        e.NewValues["CreateDate"] = DateTime.Now;
    }
    protected void gv_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["GroupName"] = SafeValue.SafeString(e.NewValues["GroupName"]);
        e.NewValues["Note1"] = SafeValue.SafeString(e.NewValues["Note1"]);
    }
    protected void gv_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.gv.EditingRowVisibleIndex > -1)
        {
            string Id = SafeValue.SafeString(this.gv.GetRowValues(this.gv.EditingRowVisibleIndex, new string[] { "Id" }));
            dsGroupDet.FilterExpression = "group_name='" + Id + "'";
        }
    }
    protected void gv_Det_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.MobileChatGroupDet));
        }
    }
    protected void gv_Det_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = Helper.Safe.SafeString(e.Values["Id"]);
    }
    protected void gv_Det_BeforePerformDataSelect(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
    }
    protected void gv_Det_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string Id = SafeValue.SafeString(this.gv.GetRowValues(this.gv.EditingRowVisibleIndex, new string[] { "Id" }));
        e.NewValues["GroupName"] = Id;
        e.NewValues["Username"] = SafeValue.SafeString(e.NewValues["Username"]);
        e.NewValues["CreateDate"] = DateTime.Now;
    }
    protected void gv_Det_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Username"] = SafeValue.SafeString(e.NewValues["Username"]);
    }
}
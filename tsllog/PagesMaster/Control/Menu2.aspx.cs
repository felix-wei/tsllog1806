using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesMaster_Control_Menu2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string masterId = SafeValue.SafeString(Request.QueryString["masterId"], "");
            this.dsMenuSub.SelectParameters.Clear();
            this.dsMenuSub.SelectParameters.Add("MasterId", TypeCode.String, masterId);
        }
    }

    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["IsActive"] = true;
        e.NewValues["MasterId"] = SafeValue.SafeString(Request.QueryString["masterId"], "");
        e.NewValues["Color"] = "#cccccc";
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["Color"] = "#cccccc";
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }
}
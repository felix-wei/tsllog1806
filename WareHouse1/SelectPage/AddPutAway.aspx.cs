using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using System.Data;
using DevExpress.Web.ASPxGridView;
using Wilson.ORMapper;

public partial class WareHouse_SelectPage_AddPutAway : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
            dsPutAway.FilterExpression = "DodetId="+id+"";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void grid_PutAway_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.WhPutAway));
        }

    }
    protected void grid_PutAway_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        dsPutAway.FilterExpression = "DodetId=" + id + "";

    }
    protected void grid_PutAway_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"],0);
        string doType = SafeValue.SafeString(Request.QueryString["doType"]);
        e.NewValues["Code"] = "";
        e.NewValues["Name"] = "";
        e.NewValues["Qty"] = 1;
        e.NewValues["DodetId"] = id;
        e.NewValues["DoType"] = doType;

    }
    protected void grid_PutAway_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_PutAway_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        string doType = SafeValue.SafeString(Request.QueryString["doType"]);
        e.NewValues["DodetId"] = id;
        e.NewValues["DoType"] = doType;
        dsPutAway.FilterExpression = "DodetId=" + id + "";
    }
    protected void grid_PutAway_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (this.grid_PutAway.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox code = this.grid_PutAway.FindEditRowCellTemplateControl(null, "txt_Code") as ASPxTextBox;
            if ( code!=null)
            {
                code.ReadOnly = true;
                code.Border.BorderWidth = 0;
            }
        }
    }
}
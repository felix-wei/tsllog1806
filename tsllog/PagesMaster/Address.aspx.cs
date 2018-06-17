using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesMaster_AddressList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.RefAddress));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Address"] = "";
        e.NewValues["Address1"] = "";
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["Address"] = SafeValue.SafeString(e.NewValues["Address"]);
        e.NewValues["Address1"] = SafeValue.SafeString(e.NewValues["Address1"]);
        e.NewValues["Location"] = SafeValue.SafeString(e.NewValues["Location"]);
        e.NewValues["TypeId"] = SafeValue.SafeString(e.NewValues["TypeId"]);
        e.NewValues["PartyId"] = SafeValue.SafeString(e.NewValues["PartyId"]);
        e.NewValues["PartyName"] = SafeValue.SafeString(e.NewValues["PartyName"]);
        e.NewValues["Postcode"] = SafeValue.SafeString(e.NewValues["Postcode"]);
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Address"] = SafeValue.SafeString(e.NewValues["Address"]);
        e.NewValues["Address1"] = SafeValue.SafeString(e.NewValues["Address1"]);
        e.NewValues["Location"] = SafeValue.SafeString(e.NewValues["Location"]);
        e.NewValues["TypeId"] = SafeValue.SafeString(e.NewValues["TypeId"]);
        e.NewValues["PartyId"] = SafeValue.SafeString(e.NewValues["PartyId"]);
        e.NewValues["PartyName"] = SafeValue.SafeString(e.NewValues["PartyName"]);
        e.NewValues["Postcode"] = SafeValue.SafeString(e.NewValues["Postcode"]);
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);

    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string where = "";
        if (txt_Code.Text.Trim() != "")
        {
            where = GetWhere(where, " Address like '%" + txt_Code.Text.Trim() + "%'");
        }
        if (cbb_TypeId.Value != null)
        {
            where = GetWhere(where, " TypeId='" + SafeValue.SafeString(cbb_TypeId.Value) + "'");
        }
        dsAddress.FilterExpression = where;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
}
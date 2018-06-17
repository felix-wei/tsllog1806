using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_MasterData_ParkingZone : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["Size20"] = SafeValue.SafeDecimal(e.NewValues["Size20"], 0);
        e.NewValues["Size40"] = SafeValue.SafeDecimal(e.NewValues["Size40"], 0);
        e.NewValues["DoubleMT"] = SafeValue.SafeDecimal(e.NewValues["DoubleMT"], 0);
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"], "");
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Size20"] = SafeValue.SafeDecimal(e.NewValues["Size20"], 0);
        e.NewValues["Size40"] = SafeValue.SafeDecimal(e.NewValues["Size40"], 0);
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"], "");
        e.NewValues["DoubleMT"] = SafeValue.SafeDecimal(e.NewValues["DoubleMT"], 0);
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Size20"] = 0;
        e.NewValues["Size40"] = 0;
        e.NewValues["DoubleMT"] = 0;
        e.NewValues["Code"] = "";
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.PackingLot));
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        if (txt_search_Code.Text.Trim().Length > 0)
        {
            where += " Code like '%" + txt_search_Code.Text.Trim() + "%' ";
        }
        this.dsPackingLot.FilterExpression = where;
    }
}
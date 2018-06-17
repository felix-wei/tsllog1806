using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesMaster_AlertList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_dateFrom.Date = DateTime.Now.AddDays(-15);
            txt_dateTo.Date = DateTime.Now;
        }
        btn_search_Click(null, null);
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "1=1";
        if (txt_Code.Text.Length > 0)
        {
            where =GetWhere(where, "Subject like '%" + txt_Code.Text + "%' or AlertTo like '%" + txt_Code.Text + "%' or AlertBody like '%" + txt_Code.Text + "%'");
        }
        if (txt_dateFrom.Date > new DateTime(1900, 1, 1))
        {
            where = GetWhere(where, " datediff(d,'" + txt_dateFrom.Date + "',CreateTime)>=0");
        }

        if (txt_dateTo.Date > new DateTime(1900, 1, 1))
        {
            where = GetWhere(where, " datediff(d,'" + txt_dateTo.Date + "',CreateTime)<=0");
        }
        if (SafeValue.SafeString(cbb_AlertType.Value) != "All" && SafeValue.SafeString(cbb_AlertType.Value).Length>0)
        {
            where = GetWhere(where, "AlertType='" + SafeValue.SafeString(cbb_AlertType.Value) + "'");
        }
        this.dsSysAlert.FilterExpression = where;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("AlertList_" + DateTime.Now.ToString("yyyyMMdd_HHmmsss"), true);
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.SysAlert));
        }
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
}
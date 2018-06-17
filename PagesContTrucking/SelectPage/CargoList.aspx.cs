using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_CargoList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        }
    }
    #region Stock
    protected void grd_Stock_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobStock));
        }
    }
    protected void grd_Stock_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        e.NewValues["OrderId"] = id;
        e.NewValues["JobNo"] = "0";
        string sql = string.Format(@"select count(*) from job_stock where OrderId={0}", id);
        int n = SafeValue.SafeInt(Helper.Sql.One(sql), 0);
        if (n == 0)
        {
            e.NewValues["SortIndex"] = 1;
        }
        else
        {
            e.NewValues["SortIndex"] = n+1;
        }
    }
    protected void grd_Stock_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        this.dsJobStock.FilterExpression = "OrderId=" + id + "";// 

    }
    protected void grd_Stock_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        e.NewValues["OrderId"] = id;
        e.NewValues["SortIndex"] = SafeValue.SafeInt(e.NewValues["SortIndex"], 0);
        e.NewValues["Marks1"] = SafeValue.SafeString(e.NewValues["Marks1"]);
        e.NewValues["PipeNo"] = SafeValue.SafeString(e.NewValues["PipeNo"]);
        e.NewValues["HintNo"] = SafeValue.SafeString(e.NewValues["HintNo"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Price1"] = SafeValue.SafeDecimal(e.NewValues["Price1"]);
        e.NewValues["Uom2"] = SafeValue.SafeString(e.NewValues["Uom2"]);
        e.NewValues["Price2"] = SafeValue.SafeDecimal(SafeValue.SafeInt(e.NewValues["Qty2"], 0) * SafeValue.SafeDecimal(e.NewValues["Price1"]));
    }
    protected void grd_Stock_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
        e.NewValues["OrderId"] = id;
        e.NewValues["SortIndex"] = SafeValue.SafeInt(e.NewValues["SortIndex"], 0);
        e.NewValues["Marks1"] = SafeValue.SafeString(e.NewValues["Marks1"]);
        e.NewValues["PipeNo"] = SafeValue.SafeString(e.NewValues["PipeNo"]);
        e.NewValues["HintNo"] = SafeValue.SafeString(e.NewValues["HintNo"]);
        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Price1"] = SafeValue.SafeDecimal(e.NewValues["Price1"]);
        e.NewValues["Uom2"] = SafeValue.SafeString(e.NewValues["Uom2"]);
        e.NewValues["Price2"] = SafeValue.SafeDecimal(e.NewValues["Price2"]);
        e.NewValues["Price2"] = SafeValue.SafeDecimal(SafeValue.SafeInt(e.NewValues["Qty2"], 0) * SafeValue.SafeDecimal(e.NewValues["Price1"]));
    }
    protected void grd_Stock_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Stock_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("UpdateInline"))
            {
                ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
                int index = SafeValue.SafeInt(ar[1], 0);
                string Id = SafeValue.SafeString(grid.GetRowValues(index, "Id"));
                grid.UpdateEdit();


                int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
                string sql = string.Format(@"select sum(Price2) from job_house where OrderId={0}", id);
                decimal totalAmt = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
                e.Result = totalAmt;
            }
        }
    }
    #endregion
}
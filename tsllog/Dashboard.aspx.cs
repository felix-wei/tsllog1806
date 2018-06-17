using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //BindData();
    }
    private void BindData()
    {
        string sql = @" select * from (
select PartyId,Code,Name,WarningAmt,WarningQty,BlockQty,BlockAmt
,(select count(sequenceId) from XAArInvoice where PartyTo=XXParty.PartyId and BalanceAmt>0) as Cnt
,(SELECT CAST(sum(BalanceAmt*exRate) AS DECIMAL(20,2)) from XAArInvoice where PartyTo=XXParty.PartyId and BalanceAmt>0) as Amt
,Status,Case when upper(isnull(Status,''))='INACTIVE' then 'UnLock' when  upper(isnull(Status,''))='USE' then 'Lock'  ELSE '' END as BtnTxt  from XXParty
) as bb where upper(isnull(Status,''))='INACTIVE' Or cnt>WarningQty OR CNT>BlockQty or Amt>WarningAmt or Amt>BlockAmt";
        DataTable tab = ConnectSql.GetTab(sql);
        this.grid.DataSource = tab;
        this.grid.DataBind();
    }
    protected void grid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            string closeInd = SafeValue.SafeString(this.grid.GetRowValues(e.VisibleIndex, "Status"));
            if (closeInd == "InActive")
            {
                e.Row.BackColor = System.Drawing.Color.LightBlue;
            }
        }
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        int index = SafeValue.SafeInt(e.Parameters,-1);
        string partyId = SafeValue.SafeString((this.grid.GetDataRow(index))["PartyId"]);
        if (partyId.Length > 0)
        {
            string sql = string.Format("update xxparty set Status=(case when Status='USE' then 'InActive' when Status='InActive' then 'USE' else Status end) where PartyId='{0}'", partyId); 
            ConnectSql.ExecuteSql(sql);
        }
    }
    protected void grid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.VisibleIndex < 0) return;
        if (e.DataColumn.FieldName == "WarningQty")
        {
            DataRow row = this.grid.GetDataRow(e.VisibleIndex);
            int cnt = SafeValue.SafeInt(row["Cnt"], 0);
            if (SafeValue.SafeInt(e.CellValue, 0) < cnt)
                e.Cell.BackColor = System.Drawing.Color.LightPink;
        }
        if (e.DataColumn.FieldName == "WarningAmt")
        {
            DataRow row = this.grid.GetDataRow(e.VisibleIndex);
            decimal cnt = SafeValue.SafeDecimal(row["Amt"], 0);
            if (SafeValue.SafeDecimal(e.CellValue, 0) < cnt)
                e.Cell.BackColor = System.Drawing.Color.LightPink;
        }
        if (e.DataColumn.FieldName == "BlockQty")
        {
            DataRow row = this.grid.GetDataRow(e.VisibleIndex);
            int cnt = SafeValue.SafeInt(row["Cnt"], 0);
            if (SafeValue.SafeInt(e.CellValue, 0) < cnt)
                e.Cell.BackColor = System.Drawing.Color.LightPink;
        }
        if (e.DataColumn.FieldName == "BlockAmt")
        {
            DataRow row = this.grid.GetDataRow(e.VisibleIndex);
            decimal cnt = SafeValue.SafeDecimal(row["Amt"], 0);
            if (SafeValue.SafeDecimal(e.CellValue, 0) < cnt)
                e.Cell.BackColor = System.Drawing.Color.LightPink;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Wilson.ORMapper;

public partial class PagesFreight_Quote_ExpLclRate_Std : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "FclLclInd='Lcl'";
        Session["ExpLclRateStd"] = where;
        this.dsQuotationDet.FilterExpression = where;
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        this.grid_InvDet.AddNewRow();
    }
    #region quotation det
    protected void grid_InvDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.SeaQuoteDet1));
    }
    protected void grid_InvDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["PartyTo"] = "";// this.txt_AgtId.Text;
        e.NewValues["PartyName"] = "";// this.txt_AgtName.Text;
        e.NewValues["Currency"] ="SGD";
        e.NewValues["Price"] = (decimal)0;
        e.NewValues["Unit"] = "SET";
        e.NewValues["Rmk"] = " ";
        e.NewValues["MinAmt"] = (decimal)0;
        e.NewValues["Qty"] = (decimal)1;
        e.NewValues["Amt"] = (decimal)0;
        e.NewValues["GstType"] = "Z";
        e.NewValues["ExRate"] = (decimal)1;
    }
    protected void grid_InvDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string where = "FclLclInd='Lcl' and QuoteId='-1'";
        where += " and PartyTo='" + SafeValue.SafeString(e.NewValues["PartyTo"], "") + "'";

        string sql_detCnt = "select max(QuoteLineNo) from SeaQuoteDet1 where " + where;
        int lineNo = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
        e.NewValues["QuoteLineNo"] = lineNo;

        e.NewValues["QuoteId"] = "-1";
        e.NewValues["FclLclInd"] = "Lcl";
        e.NewValues["PartyType"] = "C";
        e.NewValues["PartyTo"] = "";
        if (SafeValue.SafeString(e.NewValues["GstType"], "") == "S")
            e.NewValues["Gst"] = (decimal)0.07;
        else
            e.NewValues["Gst"] = (decimal)0;
        e.NewValues["Amt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Price"], 0) * SafeValue.SafeDecimal(e.NewValues["Qty"], 1)*(1 + SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
    }
    protected void grid_InvDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["GstType"], "") == "S")
            e.NewValues["Gst"] = (decimal)0.07;
        else
            e.NewValues["Gst"] = (decimal)0;
        e.NewValues["Amt"] =SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Price"], 0)*SafeValue.SafeDecimal(e.NewValues["Qty"],1)*(1 + SafeValue.SafeDecimal(e.NewValues["Gst"], 0)),2);
    }
    #endregion

    #region invoice sql
    private string GetNo(string noType)
    {
        string sql = "select Counter from XXSetup where Category='" + noType + "'";
        int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0) + 1;

        return cnt.ToString();

    }
    private void SetNo(string no, string noType)
    {
        string sql = string.Format("update XXSetup set counter='{0}' where category='{1}'", no, noType);
        int res = C2.Manager.ORManager.ExecuteCommand(sql);
    }
    #endregion
}

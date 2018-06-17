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

public partial class PagesFreight_Quote_ExpFclRate_Std : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "FclLclInd='Fcl'";
        Session["ExpFclRateStd"] = where;
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
        e.NewValues["Currency"] = "SGD";
        e.NewValues["Qty"] = (decimal)1;
        e.NewValues["Price"] = (decimal)0;
        e.NewValues["Unit"] = "20GP";
        e.NewValues["Rmk"] = " ";
        e.NewValues["Gst"] = 0.00;
        e.NewValues["ExRate"] = (decimal)1;
        e.NewValues["GstType"] = "E";
    }
    protected void grid_InvDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string where = "FclLclInd='Fcl' and QuoteId='-1'";

        string sql_detCnt = "select max(QuoteLineNo) from SeaQuoteDet1 where " + where;
        int lineNo = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
        e.NewValues["QuoteLineNo"] = lineNo;

        e.NewValues["QuoteId"] = "-1";
        e.NewValues["FclLclInd"] = "Fcl";
        e.NewValues["PartyType"] = "C";
        e.NewValues["PartyTo"] = "";
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"]);
        e.NewValues["Qty"] = (decimal)1;
        if (SafeValue.SafeString(e.NewValues["GstType"], "") == "S")
            e.NewValues["Gst"] = (decimal)0.07;
        else
            e.NewValues["Gst"] = (decimal)0;
        e.NewValues["Amt"] = SafeValue.SafeDecimal(e.NewValues["Price"], 1) * (1 + SafeValue.SafeDecimal(e.NewValues["Gst"], 0));
        //e.NewValues["MinAmt"] = (decimal)0;
        //e.NewValues["ExRate"] = (decimal)1;
    }
    protected void grid_InvDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["GstType"], "") == "S")
            e.NewValues["Gst"] = (decimal)0.07;
        else
            e.NewValues["Gst"] = (decimal)0;
        e.NewValues["Amt"] = SafeValue.SafeDecimal(e.NewValues["Price"], 1) * (1 + SafeValue.SafeDecimal(e.NewValues["Gst"], 0));
    }
    #endregion
}

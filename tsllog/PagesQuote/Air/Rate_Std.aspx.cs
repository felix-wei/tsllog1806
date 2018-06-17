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

public partial class PagesQuote_Air_Rate_Std : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string typ = SafeValue.SafeString(Request.QueryString["typ"], "SI");
            this.txt_type.Text = typ;
            Session["AirRate_" + typ] = "ImpExpInd='" + typ + "' and QuoteId = '-1'";
        }
        if (Session["AirRate_" + this.txt_type.Text] != null)
            this.dsQuotationDet.FilterExpression = Session["AirRate_" + this.txt_type.Text].ToString();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
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
        //string where = "QuoteId='-1'";
        //where += " and PartyTo='" + SafeValue.SafeString(e.NewValues["PartyTo"], "") + "'";

        //string sql_detCnt = "select max(QuoteLineNo) from SeaQuoteDet1 where " + where;
        //int lineNo = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
        e.NewValues["QuoteLineNo"] = 1;

        e.NewValues["QuoteId"] = "-1";
        e.NewValues["FclLclInd"] = "";
        e.NewValues["ImpExpInd"] = this.txt_type.Text;
        e.NewValues["PartyType"] = "C";
        e.NewValues["PartyTo"] = "";
        //if (SafeValue.SafeString(e.NewValues["GstType"], "") == "S")
        //    e.NewValues["Gst"] = (decimal)0.07;
        //else
        //    e.NewValues["Gst"] = (decimal)0;
        //e.NewValues["Amt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Price"], 0) * SafeValue.SafeDecimal(e.NewValues["Qty"], 1)*(1 + SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
    }
    protected void grid_InvDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //if (SafeValue.SafeString(e.NewValues["GstType"], "") == "S")
        //    e.NewValues["Gst"] = (decimal)0.07;
        //else
        //    e.NewValues["Gst"] = (decimal)0;
        //e.NewValues["Amt"] =SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Price"], 0)*SafeValue.SafeDecimal(e.NewValues["Qty"],1)*(1 + SafeValue.SafeDecimal(e.NewValues["Gst"], 0)),2);
    }
    protected void grid_InvDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    #endregion
}

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

public partial class Page_CostRate : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
		if(!IsPostBack)
		{
			cbx_ImpExp.Text="-";
 
			cbx_Rate.Text="-";
			cbx_Client.Text="-";
		}
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
		string _impexp = Helper.Safe.SafeString(cbx_ImpExp.Value);
 
		string _client = Helper.Safe.SafeString(cbx_Client.Value);
		string _charge = Helper.Safe.SafeString(cbx_Rate.Value);
		string _port = Helper.Safe.SafeString(txt_Port.Text.Trim());

		string where = "Status1='COST'";
		
		if(_impexp.Trim() != "-")
		{
			where = where + " and Status2='"+_impexp+"' ";
		}
 
		if(_client.Length > 1)
		{
			where = where + " and Status5='"+_client+"' ";
		}
		if(_port.Length > 1)
		{
			where = where + " and Status7 like '"+_port+"%' ";
		}
		if(_charge.Length > 1)
		{
			where = where + " and ChgCode like '%"+_charge+"%' ";
		}
        Session["CFS-BillRate"] = where;
        this.ds1.FilterExpression = where;
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        this.grid1.AddNewRow();
    }
    #region quotation det
    protected void Grid1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.SeaQuoteDet1));
    }
    protected void Grid1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
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
        e.NewValues["Date1"] = new DateTime(2014,1,1);
    }
    protected void Grid1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
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

        e.NewValues["Status1"] = "COST";

        if (SafeValue.SafeString(e.NewValues["GstType"], "") == "S")
            e.NewValues["Gst"] = (decimal)0.07;
        else
            e.NewValues["Gst"] = (decimal)0;
        e.NewValues["Amt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Price"], 0) * SafeValue.SafeDecimal(e.NewValues["Qty"], 1)*(1 + SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
    }
    protected void Grid1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
		e.NewValues["Status7"] = Helper.Safe.SafeString(e.NewValues["Status7"]).Trim();
		e.NewValues["Rmk"] = Helper.Safe.SafeString(e.NewValues["Rmk"]).Trim();
    }
	
	protected void Grid1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    #endregion

   
}

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
			cbx_Type.Text="Local Move";
 
			cbx_Mode.Text="-";
		}
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
		string _impexp = Helper.Safe.SafeString(cbx_Type.Value);
 
		string _client = Helper.Safe.SafeString(cbx_Mode.Value);

		string where = "QuoteId=-1 ";
		
		if(_impexp.Trim() != "-")
		{
			where = where + " and Status1='"+_impexp+"' ";
		}
 
		if(_impexp.Trim()=="Outbound" && _client.Length > 1)
		{
			where = where + " and Status2='"+_client+"' ";
		}
        Session["CostingRate"] = where;
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
        e.NewValues["Price"] = (decimal)0;
        e.NewValues["Rmk"] = " ";
        e.NewValues["MinAmt"] = (decimal)0;
        e.NewValues["Qty"] = (decimal)1;
        e.NewValues["Amt"] = (decimal)0;
        e.NewValues["Date1"] = new DateTime(2014,1,1);
    }
    protected void Grid1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["QuoteLineNo"] = 1;
        e.NewValues["QuoteId"] = "-1";
    }
    protected void Grid1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
    }
	
	protected void Grid1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    #endregion

   
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;

using Wilson.ORMapper;

public partial class PagesQuote_FclQuote : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.dsQuotation.FilterExpression = "1=0";
            string typ = SafeValue.SafeString(Request.QueryString["typ"], "SI");
            this.txt_type.Text = typ;

            this.txt_from.Date = DateTime.Today.AddMonths(-6);
            this.txt_end.Date = DateTime.Today;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "FclLclInd='Fcl' and ImpExpInd='"+this.txt_type.Text+"'";
        if (txt_refNo.Text.Trim() != "")
            where += " and QuoteNo='" + txt_refNo.Text.Trim() + "'";
        else
        {
            if (this.txt_CustId.Text.Trim().Length > 0)
            {
                where += " and  PartyTo='" + this.txt_CustId.Text.Trim() + "'";
            }
            else if (this.txt_from.Value != null && this.txt_end.Value != null)
            {
                where += string.Format(" and QuoteDate >='{0}' and QuoteDate<'{1}'", this.txt_from.Date.ToString("yyyy-MM-dd"), this.txt_end.Date.AddDays(1).ToString("yyyy-MM-dd"));
            }
        }
        if (where.Length > 0)
        {
            this.dsQuotation.FilterExpression = where;
        }
    }
}

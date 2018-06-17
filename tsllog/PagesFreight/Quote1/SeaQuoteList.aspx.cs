using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesFreight_Quote1_SeaQuoteList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //dsSeaQuotation.FilterExpression = "1=0";
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "1=1";
        if (!txt_refNo.Text.Trim().Equals(""))
        {
            where += " and QuoteNo like '%" + txt_refNo.Text.Trim() + "%'";
        }
        if (!txt_CustId.Text.Trim().Equals(""))
        {
            where += " and PartyTo='" + txt_CustId.Text.Trim() + "'";
        }
        //if (!txt_CustName.Text.Trim().Equals(""))
        //{
        //    where += " and PartyName='" + txt_CustName.Text.Trim() + "'";
        //}
        this.dsSeaQuotation.FilterExpression = where;
    }
}
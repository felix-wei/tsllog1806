using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;

using Wilson.ORMapper;

public partial class PagesFreight_ExpFclApQuote : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.dsApQuotation.FilterExpression = "1=0";
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "FclLclInd='Fcl'";
        if (txt_refNo.Text.Trim() != "")
            where = "QuoteNo='" + txt_refNo.Text.Trim() + "'";
        else if(this.txt_CustId.Text.Trim().Length>0)
        {
                where = " PartyTo='" + this.txt_CustId.Text.Trim() + "'";
        }
        if (where.Length > 0)
        {

            string userName = HttpContext.Current.User.Identity.Name;
            string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select role from [user] where Name='" + userName + "'"), "").ToUpper();
            if (role == "SALESSTAFF")
            {
                DataTable tab = C2.Manager.ORManager.GetDataSet(@"SELECT q.QuoteNo, q.QuoteDate, q.ExpireDate, c.Name
FROM SeaApQuote1 AS q INNER JOIN XXParty AS c ON q.PartyTo = c.PartyId
WHERE     (c.SalesmanId = '" + userName + "') and " + where).Tables[0];
                string where1 = "( 1=0";

                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    where1 += string.Format(" or QuoteNo='{0}'", tab.Rows[i][0]);
                }
                where1 += ")";
                this.dsApQuotation.FilterExpression = where1;
            }
            else
                this.dsApQuotation.FilterExpression = where;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;

public partial class PagesFreight_SelectPage_ApQuoteList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string quoteNo = this.txt_QuoteNo.Text.Trim().ToUpper();
        string sql = "select q.SequenceId,p.Name,q.QuoteNo,q.ExpireDate from SeaApQuote1 AS q left JOIN XXParty AS p ON q.PartyTo = p.PartyId";
        if (quoteNo.Length > 0)
        {
            sql += string.Format(" where QuoteNo Like '{0}%'", quoteNo);
        }
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];

        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesFreight_Account_ArCnList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.dsArInvoice.FilterExpression = "1=0";
            this.txt_from.Date = DateTime.Today.AddDays(-7);
            this.txt_end.Date = DateTime.Today;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string dateFrom = "";
        string dateEnd = "";
        string where = "";
        if (txt_refNo.Text.Trim() != "")
            where = "DocNo='" + txt_refNo.Text.Trim() + "'";
        else if (this.txt_end.Value != null && this.txt_from.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateEnd = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
            where = "DocDate>='" + dateFrom + "' and DocDate<'" + dateEnd + "'";
        }
        if (this.cmb_PartyTo.Value != null)
        {
            if (where.Length > 0)
                where += " and PartyTo='" + this.cmb_PartyTo.Value + "'";
            else
                where = " PartyTo='" + this.cmb_PartyTo.Value + "'";
        }
        if (where.Length > 0)
        {
            where += " and DocType='CN' and  (MastType is not null)";
            this.dsArInvoice.FilterExpression = where;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web;

using Wilson.ORMapper;

public partial class Account_ArInvoice : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		txt_doctype.Text = Request.QueryString["t"] ?? "";
        if (!IsPostBack)
        {
            this.dsArInvoice.FilterExpression = "1=0";
            this.txt_from.Date = DateTime.Today.AddDays(-60);
            this.txt_end.Date = DateTime.Today;
            this.cbo_PostInd.Text = "All";
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

		string _t = Request.QueryString["t"] ?? "ALL";
		 _t = _t.ToUpper();
		if(_t != "ALL" && _t != "")
			where += " AND DocType='"+_t+"' ";
		
		
        if (this.cmb_PartyTo.Value!=null)
        {
            if (where.Length > 0)
                where += " and PartyTo='" + this.cmb_PartyTo.Value+ "'";
            else
                where = " PartyTo='" + this.cmb_PartyTo.Value + "'";
        }
        if (where.Length > 0)
        {
        if (this.cbo_PostInd.Text == "Y")
            where += " and ExportInd='Y'";
        else if (this.cbo_PostInd.Text == "N")
            where += " and ExportInd <> 'Y'";
            where += " and DocType in ('SIN','SDN','SCN') ";
            this.dsArInvoice.FilterExpression = where;
        }
    }
	public string get_pay_list(object id)
	{
		string ret = "";
		
		DataTable pl = D.List(string.Format("select p.DocNo,p.DocType from xaarreceipt p, xaarreceiptdet l where p.sequenceid=l.repid and l.docid={0}",id));
		for(int i=0; i< pl.Rows.Count; i++)
		{
			DataRow dr = pl.Rows[i];
			if(i==0)
				ret += string.Format("<a href='javascript:open_payment(\"{0}\",\"{1}\")'>{0}</a>",dr["DocNo"],dr["DocType"]);
			else
				ret += string.Format("<br><a href='javascript:open_payment(\"{0}\",\"{1}\")'>{0}</a>",dr["DocNo"],dr["DocType"]);
			
		}
			return ret;
	}
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("ARInvoiceList", true);
    }
}

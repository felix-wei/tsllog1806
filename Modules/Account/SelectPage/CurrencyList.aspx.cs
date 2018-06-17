using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;

public partial class SelectPage_CurrencyList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
		btn_Sch_Click(null,null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
		string dat1 = Request.QueryString["d"] ?? DateTime.Today.ToString("dd/MM/yyyy");
		string dat = dat1.Substring(6,4) + "-" + dat1.Substring(3,2) + "-" +dat1.Substring(0,2);
        string sql = "SELECT CurrencyId, (select top 1 ExRate1 from currencyrate where FromCurrencyId=CurrencyId and ToCurrencyId='SGD' and ExRateDate <= '"+dat+"' order by ExRateDate Desc) as CurrencyExRate from xxcurrency ";
        if (name.Length > 0)
        {
            sql += string.Format("  where CurrencyId='{0}'", name);
        }

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}

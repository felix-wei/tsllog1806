using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesFreight_Account_ApVoucherList : System.Web.UI.Page
{
    protected void page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.dsApPayable.FilterExpression = "1=0";
            this.txt_from.Date = DateTime.Today.AddDays(-7);
            this.txt_end.Date = DateTime.Today;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string dateFrom = "";
        string dateEnd = "";
        string where = "";
        if (txt_refNo.Text.Trim() != "")
            where = "DocNo='" + txt_refNo.Text.Trim() + "'";
        else if (this.txt_ChqNo.Text.Trim().Length > 0)
            where = " ChqNo='" + this.txt_ChqNo.Text.Trim() + "'";
        else if (this.txt_supplyBillNo.Text.Trim().Length > 0)
            where = " SupplierBillNo='" + this.txt_supplyBillNo.Text.Trim() + "'";
        else if (this.txt_end.Value != null && this.txt_from.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateEnd = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
            where = "DocDate>='" + dateFrom + "' and DocDate<'" + dateEnd + "'";
        }
        if (where.Length > 0)
        {
            where = GetWhere(where, "DocType='VO' and  (MastType is not null)");
            this.dsApPayable.FilterExpression = where;
        }
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;


public partial class Account_ArInvoice_Cn : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            this.dsArInvoice.FilterExpression="DocType='CN' and ExportInd='Y' and BalanceAmt!=0";
            this.txt_from.Date = DateTime.Now.AddDays(-7);
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
        else
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateEnd = txt_end.Date.ToString("yyyy-MM-dd");

            where = "DOCDATE>='" + dateFrom + "' and DOCDATE<='"+ dateEnd +"'";
        }
        if (where.Length > 0)
        {
            where += " and ExportInd='Y' and DocType='CN' and BalanceAmt!=0 ";
            this.dsArInvoice.FilterExpression = where;
        }
    }
}

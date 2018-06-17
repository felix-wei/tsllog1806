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

public partial class Account_ApPayableOpen : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.dsApPayable.FilterExpression = "1=0";
      
            this.txt_from.Date = new DateTime(2013,1,1);
            this.txt_end.Date = new DateTime(2013,10,31);
            this.cbo_DocType.Text = "All";
 
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
        else  if(this.txt_supplyBillNo.Text.Trim().Length>0)
            where = " SupplierBillNo='" + this.txt_supplyBillNo.Text.Trim() + "'";
        else if (this.txt_end.Value != null && this.txt_from.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateEnd = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
            where = "DocDate>='" + dateFrom + "' and DocDate<'" + dateEnd + "'";
        }
        if (this.cmb_PartyTo.Value!=null)
        {
            if (where.Length > 0)
                where += " and PartyTo='" + this.cmb_PartyTo.Value + "'";
            else
                where = " PartyTo='" + this.cmb_PartyTo.Value + "'";
        }
        if (where.Length > 0)
        {
            if (this.cbo_DocType.Text != "All")
                where += " AND DocType='"+this.cbo_DocType.Value+"'";
            else
                where += " AND (DocType='PL' or DocType='SC' or DocType='SD') ";
   
            this.dsApPayable.FilterExpression = where;
        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("APPL", true);
    }

}

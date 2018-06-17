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

public partial class Account_GlEntry : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
        // this.txt_refNo.Text = "280674";
        if (!IsPostBack)
        {
            this.txtSchType.Text = "All";
            this.txt_from.Date = DateTime.Today.AddDays(-7);
            this.txt_end.Date = DateTime.Today;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        //this.ASPxGridView1.FindRowTemplateControl(0, "txt").;

        string dateFrom = "";
        string dateEnd = "";
        string where = "1=1";
        if (this.txtDocNo.Text.Length > 0)
        {
            where = "DocNo='" + txtDocNo.Text + "'";
        }
        else if (this.txtBillNo.Text.Length > 0)
        {
            where = "SupplierBillNo='" + txtBillNo.Text + "'";
        }
        else if (this.txt_end.Value != null && this.txt_from.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateEnd = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
                where += " and DocDate>='" + dateFrom + "' and DocDate<'" + dateEnd + "'";
        }
        if (this.txtSchType.Text.Length > 0 && this.txtSchType.Text != "All")
        {
                where += " and DocType='" + this.txtSchType.Text + "'";
        }
        else
        {
            where += " and DocType!='GE'";
        }


        if (where.Length > 0)
        {
            this.dsGlEntry.FilterExpression = where;
        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("GlEntry", true);
    }

}

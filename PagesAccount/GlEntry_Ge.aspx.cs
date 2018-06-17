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

public partial class Account_GlEntry_Ge : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
        // this.txt_refNo.Text = "280674";
        if (!IsPostBack)
        {
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
        string where = "DocType='GE' ";
        if (this.txtDocNo.Text.Length > 0)
        {
            where += " and DocNo='" + txtDocNo.Text + "'";
        }
        else if (this.txt_end.Value != null && this.txt_from.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateEnd = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
            where += " and DocDate>='" + dateFrom + "' and DocDate<'" + dateEnd + "'";
        }


        if (where.Length > 0)
        {
            this.dsGlEntry.FilterExpression = where;
        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("GE", true);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;

using Wilson.ORMapper;

public partial class WareHouse_Job_EcBill : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.dsArInvoice.FilterExpression = "1=0";
            this.txt_from.Date = DateTime.Today.AddDays(-7);
            this.txt_end.Date = DateTime.Today;
            this.cbo_PostInd.Text = "All";
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string user = HttpContext.Current.User.Identity.Name;
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
        if (this.cmb_PartyTo.Value!=null)
        {
            if (where.Length > 0)
                where += " and PartyTo='" + user + "'";
            else
                where = " PartyTo='" + user + "'";
        }
        if (where.Length > 0)
        {
        if (this.cbo_PostInd.Text == "Y")
            where += " and ExportInd='Y'";
        else if (this.cbo_PostInd.Text == "N")
            where += " and ExportInd!='Y'";
            where += "and (DocType='IV' or DocType='DN')";
            this.dsArInvoice.FilterExpression = where;
        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("ARInvoiceList", true);
    }
}

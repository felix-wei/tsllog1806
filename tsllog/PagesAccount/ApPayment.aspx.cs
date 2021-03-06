﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Wilson.ORMapper;

public partial class Account_ApPayment : BasePage
{
	protected void page_Init(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			this.dsApPayment.FilterExpression = "1=0";
			this.txt_from.Date = DateTime.Today.AddDays(-7);
			this.txt_end.Date = DateTime.Today;
			this.cbo_PostInd.Text = "All";
		}

		string type1 = Request.QueryString["type"] ?? "Job";
		txt_DocType1.Text = type1;
	}

	protected void Page_Load(object sender, EventArgs e)
	{
	}

	protected void btn_search_Click(object sender, EventArgs e)
	{
		string dateFrom = "";
		string dateEnd = "";
		string type1 = Request.QueryString["type"] ?? "Job";
        string where = "";//"DocType1='"+type1+"' AND ";
		if (txt_refNo.Text.Trim() != "")
            where += "DocNo='" + txt_refNo.Text.Trim() + "'";
        else if (this.txt_end.Value != null && this.txt_from.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateEnd = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
            where += "DocDate>='" + dateFrom + "' and DocDate<'" + dateEnd + "'";
        }
        if (this.cmb_PartyTo.Value!=null)
        {
            if (where.Length > 0)
                where += " and PartyTo='" + this.cmb_PartyTo.Value + "'";
            else
                where = " PartyTo='" + this.cmb_PartyTo.Value + "'";
        }
        else if (this.txt_ChqNo.Text.Trim().Length > 0)
        {
            if (where.Length > 0)
                where += " and ChqNo='" + this.txt_ChqNo.Text + "'";
            else
                where = " ChqNo='" + this.txt_ChqNo.Text + "'";
        }

        if (this.txt_InvNo.Text.Trim().Length > 0)
        {
            string sql = "SELECT det.PayId FROM XAApPaymentDet AS det INNER JOIN XAApPayable AS inv ON det.Docid = inv.SequenceId where inv.SupplierBillNo='" + this.txt_InvNo.Text.Trim() + "'";
            DataTable tab = ConnectSql.GetTab(sql);
            if (tab.Rows.Count > 0)
            {
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (i == 0)
                        where = "( SequenceId='" + SafeValue.SafeInt(tab.Rows[i][0], 0) + "'";
                    else

                        where += " or SequenceId='" + SafeValue.SafeInt(tab.Rows[i][0], 0) + "'";

                }
                where += ")";
            }
            else
            {
                where = "";
            }
        }
        if (where.Length > 0)
        {
        if (this.cbo_PostInd.Text == "Y")
            where += " and ExportInd='Y'";
        else if (this.cbo_PostInd.Text == "N")
            where += " and ExportInd!='Y'";
           // where += " and DocType='PS'";
            this.dsApPayment.FilterExpression = where; // + " AND DocType1='"+type1+"'";
        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("APPI", true);
    }

}

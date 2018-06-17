using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Printing;

public partial class ReportTpt_Report_BillingList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
            this.txt_from.Date = DateTime.Today.AddDays(-15);
            this.txt_end.Date = DateTime.Today.AddDays(8);
            this.cmb_DocType.Value = "IV";
        }

    }
    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        string sql1 = string.Format(@"SELECT DocNo, convert(nvarchar,DocDate,103) as DocDate, PartyTo as PartyId,dbo.fun_GetPartyName(PartyTo) as PartyName, MastRefNo, MastType, inv.CurrencyId as Currency, 
cast(isnull(inv.ExRate,0) as numeric(21,6)) as ExRate,DocAmt, LocAmt,tpt.Pol,tpt.Pod FROM XAArInvoice as inv 
left outer  join TPT_Job as tpt on tpt.JobNo=inv.MastRefNo WHERE MastType='TPT'");
        string sql2 = string.Format(@"SELECT DocNo, convert(nvarchar,DocDate,103) as DocDate, PartyTo as PartyId,dbo.fun_GetPartyName(PartyTo) as PartyName, MastRefNo, MastType, inv.CurrencyId as Currency, 
cast(isnull(inv.ExRate,0) as numeric(21,6)) as ExRate,DocAmt, LocAmt,tpt.Pol,tpt.Pod FROM dbo.XAApPayable as inv 
left outer  join TPT_Job as tpt on tpt.JobNo=inv.MastRefNo WHERE MastType='TPT'");

        string sql="";
        string where = "";
        string custId = this.btn_CustId.Text.Trim();
        string dateFrom = "";
        string dateTo = "";
        string docType = this.cmb_DocType.Text.Trim();
        if (cmb_DocType.Value == "IV" || cmb_DocType.Value == "CN" || cmb_DocType.Value == "DN")
            sql = sql1;
        if (cmb_DocType.Value=="VO"||cmb_DocType.Value=="PL")
            sql = sql2;
        if (cmb_DocType.Value == "ALL")
            sql = sql1 + " UNION ALL " + sql2;
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.ToString("yyyy-MM-dd");
        }
        if (custId.Length > 0)
            where = GetWhere(where, " PartyTo='" + custId + "'");
        if (dateFrom.Length > 0 && dateTo.Length > 0)
            where = GetWhere(where, string.Format(" DocDate >= '{0}' and DocDate <= '{1}'", dateFrom, dateTo));
        if (docType.Length > 0)
            where = GetWhere(where, "DocType='" + this.cmb_DocType.Value.ToString()+ "'");
        if (where.Length > 0)
        {
            if (cmb_DocType.Value == "ALL")
            {
                sql1 += " and " + where;
                sql2 += " and " + where;
                sql = sql1 + " UNION ALL " + sql2;
            }
            else
                sql += " and " + where;
        }

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.grid_Import.DataSource = tab;
        this.grid_Import.DataBind();

    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btnRetrieve_Click(null, null);
        this.gridExport.WriteXlsToResponse("TptBillingList", true);
    }
}
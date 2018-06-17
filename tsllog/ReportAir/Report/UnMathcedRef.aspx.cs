using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Printing;

public partial class ReportFreightSea_Report_Import_UnMathcedRef : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {

        string sql = @"select * from air_ref as ref ";
        string where = "(EstSaleAmt<>(select SUM(case when  DocType='IV' then LocAmt else LocAmt end)+SUM(case when DocType='DN' then LocAmt else LocAmt end)-SUM(case when DocType='CN' then LocAmt else LocAmt end) from XAArInvoiceDet as det where MastType in ('AI', 'AE', 'ACT')  and det.MastRefNo=ref.RefNo)";
        where = where + " or EstCostAmt<>(select SUM(case when DocType='PL' then LocAmt else LocAmt end)+SUM(case when DocType='VO' then LocAmt else LocAmt end)+SUM(case when DocType='SD' then LocAmt else LocAmt end)-SUM(case when DocType='SC' then LocAmt else LocAmt end) from XAApPayableDet as det where MastType in ('AI', 'AE', 'ACT') and det.MastRefNo=ref.RefNo)) ";

        string departurePort = this.btn_DeparturePort.Text.Trim();
        string dateFrom = "";
        string arrivalPort = this.btn_ArrivalPort.Text.Trim();
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.ToString("yyyy-MM-dd");
        }
        if (departurePort.Length > 0)
            where = GetWhere(where, " AirportCode0='" + departurePort + "'");
        if (dateFrom.Length > 0 && dateTo.Length > 0)
            where = GetWhere(where, string.Format(" FlightDate0>= '{0}' and FlightDate0 <= '{1}'", dateFrom, dateTo));
        if (arrivalPort.Length > 0)
            where = GetWhere(where, " AirportCode1='" + arrivalPort + "'");

        if (where.Length > 0)
        {
            sql += " where ref.statuscode<>'CNL' and " + where;
            DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            this.grid_Import.DataSource = tab;
            this.grid_Import.DataBind();
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btnRetrieve_Click(null, null);
        this.gridExport.WriteXlsToResponse("AirImportRefList", true);
    }
}
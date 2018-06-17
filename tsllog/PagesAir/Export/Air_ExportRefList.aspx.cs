using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class PagesAir_Export_Air_ExportRefList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.Title = "ExportShipment";
        if (!IsPostBack)
        {
            this.txt_RefNo.Text = "";//"51856";// "55788";
            Session["ExpWhere"] = "1=0";
            this.txt_from.Date = DateTime.Today.AddDays(-15);
            this.txt_end.Date = DateTime.Today.AddDays(8);
            btn_search_Click(null, null);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        string sql = @"select job.Id,ref.RefNo,ref.MAWB,ref.FlightNo1,ref.AirportCode0,ref.AirportCode1,ref.FlightDate0 ,ref.FlightDate1,
job.JobNo,job.HAWB,(select Name from dbo.XXParty where PartyId=job.CustomerId) as CustomerName,ref.StatusCode,job.StatusCode as JobStatusCode,job.Qty,job.PackageType,job.Weight,job.Volume from air_ref ref left join air_job job on ref.RefNo=job.RefNo";
        if (txt_RefNo.Text.Trim() != "")
            where = GetWhere(where, " ref.RefNo='" + txt_RefNo.Text.Trim() + "'");
        if (txt_CustId.Text.Trim() != "")
        {
            where = GetWhere(where, " job.CustomerId='" + txt_CustId.Text.Trim() + "'");
        }
        if (this.txt_MAWB.Text.Trim() != "")
        {
            where = GetWhere(where, " ref.MAWB='" + this.txt_MAWB.Text.Trim() + "'");
        }
        if (this.txt_HAWB.Text.Trim() != "")
        {
            where = GetWhere(where, " job.HAWB='" + this.txt_HAWB.Text.Trim() + "'");
        }
        else
        {
            string agtId = this.txt_AgtId.Text.Trim();
            string dateFrom = "";
            string dateTo = "";

            if (txt_from.Value != null && txt_end.Value != null)
            {
                dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
                dateTo = txt_end.Date.ToString("yyyy-MM-dd");
            }
            if (agtId.Length > 0)
                where = GetWhere(where, " ref.AgentId='" + agtId + "'");
            if (dateFrom.Length > 0 && dateTo.Length > 0)
                where = GetWhere(where, string.Format("ref.FlightDate0 >= '{0}' and ref.FlightDate0<= '{1}'", dateFrom, dateTo));
        }
        if (where.Length > 0)
        {
            where = GetWhere(where, " ref.RefType='AE'");
            where = " where " + where;
            sql += where + " ORDER BY  ref.Id desc";
            DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            this.grid_ref.DataSource = tab;
            this.grid_ref.DataBind();
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
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("ExportRef", true);
    }
    protected void grid_ref_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            object er = grid_ref.GetRow(e.VisibleIndex);
            if (er != null)
            {
                string closeInd = SafeValue.SafeString(this.grid_ref.GetRowValues(e.VisibleIndex, "StatusCode"));
                if (closeInd == "CNL")
                {
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    DateTime eta = SafeValue.SafeDate(this.grid_ref.GetRowValues(0, "FlightDate0"), new DateTime(1900, 1, 1));
                    if ((DateTime.Today.Subtract(eta)).Days > 30)
                        e.Row.BackColor = System.Drawing.Color.LightPink;

                }
            }
        }
    }
}
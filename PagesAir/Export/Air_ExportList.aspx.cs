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
using DevExpress.Web.ASPxDataView;

public partial class PagesAir_Export_Air_ExportList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-7);
            this.txt_end.Date = DateTime.Today;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        string sql = @"SELECT RefNo,JobNo,(select Name from dbo.XXParty where PartyId=CustomerId) as CustomerName,HAWB,TsInd,TsBkgRef,TsBkgUser,DeliveryDate,TsBkgTime,Weight,volume,PackageType,Qty,CreateBy,CreateDateTime FROM air_job where ";

        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (txt_RefNo.Text.Trim() != "")
            where = GetWhere(where, "RefNo='" + txt_RefNo.Text.Trim() + "'");
        else if (this.txt_HouseNo.Text.Length > 0)
        {
            where = GetWhere(where, "JobNo='" + this.txt_HouseNo.Text.Trim() + "'");
        }
        else if (this.txt_HAWB.Text.Length > 0)
        {
            where = GetWhere(where, "HAWB='" + this.txt_HAWB.Text.Trim() + "'");
        }
        else if (this.txt_AgtId.Text.Length > 0)
        {
            where = GetWhere(where, "CustomerId='" + this.txt_AgtId.Text.Trim() + "'");

            if (dateFrom.Length > 0 && dateTo.Length > 0)
                where = GetWhere(where, " CreateDateTime >= '" + dateFrom + "' and CreateDateTime < '" + dateTo + "'");
        }
        else if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            where = GetWhere(where, " CreateDateTime >= '" + dateFrom + "' and CreateDateTime < '" + dateTo + "'");
        }

        if (where.Length > 0)
        {
            where = GetWhere(where, " (RefNo like 'AE%')");
            sql += where + " ORDER BY  Id";
            DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            this.grid_Import.DataSource = tab;
            this.grid_Import.DataBind();
        }
    }
    protected void btn_searchPending_Click(object sender, EventArgs e)
    {
        string where = "";
        string sql = @"SELECT RefNo,JobNo,(select Name from dbo.XXParty where PartyId=CustomerId) as CustomerName,HAWB,TsInd,TsBkgRef,TsBkgUser,DeliveryDate,TsBkgTime,Weight,volume,PackageType,Qty,CreateBy,CreateDateTime FROM air_job where ";

        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (txt_RefNo.Text.Trim() != "")
            where = GetWhere(where, "RefNo='" + txt_RefNo.Text.Trim() + "'");
        else if (this.txt_HouseNo.Text.Length > 0)
        {
            where = GetWhere(where, "JobNo='" + this.txt_HouseNo.Text.Trim() + "'");
        }
        else if (this.txt_HAWB.Text.Length > 0)
        {
            where = GetWhere(where, "HAWB='" + this.txt_HAWB.Text.Trim() + "'");
        }
        else if (this.txt_AgtId.Text.Length > 0)
        {
            where = GetWhere(where, "CustomerId='" + this.txt_AgtId.Text.Trim() + "'");

            if (dateFrom.Length > 0 && dateTo.Length > 0)
                where = GetWhere(where, " CreateDateTime >= '" + dateFrom + "' and CreateDateTime < '" + dateTo + "'");
        }
        else if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            where = GetWhere(where, " CreateDateTime >= '" + dateFrom + "' and CreateDateTime < '" + dateTo + "'");
        }

        if (where.Length > 0)
        {
            where = GetWhere(where, " (RefNo like 'AE%') and TsInd='N'");
            sql += where + " ORDER BY  Id";
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
}
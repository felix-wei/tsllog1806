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


public partial class WareHouse_Job_JobMcst : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today;
            this.txt_end.Date = DateTime.Today;
            txt_date.Date = txt_from.Date;
            txt_date2.Date = txt_end.Date;
            btn_search_Click(null, null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_search_Click(null, null);
        }
        else
        {
            string where = "";
            where += "CONVERT(varchar(100), CreateDateTime, 23) between '" + txt_date.Date.ToString("yyyy-MM-dd")
               + "' and '" + txt_date2.Date.ToString("yyyy-MM-dd") + "'";
            this.dsJobMCST.FilterExpression = where;
        }
    }	
	protected void btn_export_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("JobMcst", true);
    }	
    protected void btn_search_Click(object sender, EventArgs e)
    {
        
        string where = "";
        string sql = string.Format(@"select m.RefNo, m.CreateDateTime,m.Amount1,m.Amount2,m.McstNo, m.McstRemark1, m.Amount1, m.CondoTel, m.McstDate1, m.McstDate2, m.States, m.CreateBy, m.McstRemark3 from JobMcst m, JobInfo o where m.RefNo=o.QuotationNo ");
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(0).ToString("yyyy-MM-dd");
            txt_date.Date = txt_from.Date;
            txt_date2.Date = txt_end.Date;
        }
        if (dateFrom.Length > 0)
        {
            where = GetWhere(where, " CreateDateTime>= '" + dateFrom + " 00:00' and CreateDateTime<= '" + dateTo + " 23:59' ");
        }
        //if (where.Length > 0)
        //{
        //   sql += " and " + where; // + " and JobStage='Job Confirmation' order by JobNo";
        //}
		//throw new Exception(sql);
        dsJobMCST.FilterExpression = where;
        //DataTable tab = ConnectSql.GetTab(sql);
        ////this.grid_Mcst.DataSource = tab;
        ////this.grid_Mcst.DataBind();
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    #region MCST
    protected void grid_Mcst_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobMCST));
        }
    }
    protected void grid_Mcst_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_Mcst_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_Mcst_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Mcst_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        e.NewValues["McstDate1"] = SafeValue.SafeDate(e.NewValues["McstDate1"],DateTime.Now);
        e.NewValues["McstDate2"] = SafeValue.SafeDate(e.NewValues["McstDate2"], DateTime.Now);
        e.NewValues["States"] = SafeValue.SafeString(e.NewValues["States"]);
    }

    #endregion
}
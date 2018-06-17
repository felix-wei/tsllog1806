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

public partial class PagesClient_StockBalance : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-30);
            this.txt_to.Date = DateTime.Today;
			btn_Sch_Click(null,null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string userId=EzshipHelper.GetUserName();
//        string sql = string.Format(@"select  * from (
//select JobIn.*,job.WareHouseId,JobType,job.CustomerName,job.JobNo as RefNo from JobInventory JobIn
//inner join  JobInfo job on JobIn.JobNo=job.QuotationNo and JobIn.DoType='IN'
//) as tab where JobType='Storage' and  CustomerName like '%{0} %' ", userId);
        string sql = string.Format(@"select * from JobInventory where DoType='WI'");
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null&&txt_to.Value!=null)
        {
            dateFrom = this.txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_to.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        //if (dateTo.Length > 0)
        //{
        //    sql += string.Format(" and DoDate between '{0}' and '{1}' order by DoDate",dateFrom, dateTo);
        //}
        //throw new Exception(sql);
        this.grid.DataSource = ConnectSql.GetTab(sql);
        this.grid.DataBind();
//        sql = string.Format(@"select  JobIn.*,job.WareHouseId,JobType,job.CustomerName,job.JobNo as RefNo from JobInventory JobIn
//inner join  JobInfo job on JobIn.JobNo=job.QuotationNo and JobIn.DoType='OUT'
//where JobType='Storage' and  CustomerName like '%{0} %' ", userId);
        sql = string.Format(@"select * from JobInventory where DoType='WO'");
        if (txt_from.Value != null && txt_to.Value != null)
        {
            dateFrom = this.txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_to.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        //if (dateTo.Length > 0)
        //{
        //    sql += string.Format(" and DoDate between '{0}' and '{1}' order by DoDate", dateFrom, dateTo);
        //}
        this.grid_Released.DataSource = ConnectSql.GetTab(sql);
        this.grid_Released.DataBind();

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
        this.gridExport.WriteXlsToResponse("StockBalance");
    }
    #region Item Received Photo
    protected void grid_img_R_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender  as ASPxGridView;
        string sql = "select JobNo from JobInventory where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        string doNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        this.dsItemRImg.FilterExpression = "JobNo='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "' and FileType='Image'";
    }
    protected void grid_img_R_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_img_R_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobAttachment));
        }
    }
    protected void grid_img_R_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
    protected void grid_img_R_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    #endregion
    #region Item Released Photo
    protected void grid_img_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql = "select JobNo from JobInventory where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        string doNo = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        this.dsItemImg.FilterExpression = " JobNo='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "' and FileType='Image'";

    }
    protected void grid_img_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_img_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobAttachment));
        }
    }
    protected void grid_img_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
    protected void grid_img_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    #endregion
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WareHouse_Job_TransferHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dt_to = DateTime.Now;
            DateTime dt_from = dt_to.AddMonths(-1);
            search_datefrom.Date = dt_from;
            search_dateto.Date = dt_to;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql_where = "";
        DateTime minDate = new DateTime(1990, 1, 1);
        if (search_datefrom.Date <= minDate || search_dateto.Date <= minDate)
        {
            Response.Write("<script>alert('Please input From/To Date');</script>");
            return;
        }
        sql_where = " mast.TransferDate>='" + search_datefrom.Date + "' and mast.TransferDate<='" + search_dateto.Date + "'";
        string product = search_product.Text.Trim();
        if (product.Length > 0)
        {
            sql_where += " and Product like '" + product + "%'";
        }
        string lotno = search_lotno.Text.Trim();
        if (lotno.Length > 0)
        {
            sql_where += " and LotNo like '" + lotno + "%'";
        }

        string sql = string.Format(@"select ROW_NUMBER() over(order by mast.TransferDate desc) as Id, CONVERT(char(16),mast.TransferDate,121) as Date,
Product,LotNo,Des1 as Des,mast.CreateBy as [User],FromWarehouseId as Warehouse,ToLocId as [Set],det.TransferNo,Qty1
from wh_TransferDet as det 
left outer join wh_Transfer as mast on det.TransferNo=mast.TransferNo 
where {0} 
", sql_where);
        DataTable dt = ConnectSql.GetTab(sql);
        gv.DataSource = dt;
        gv.DataBind();
    }
}
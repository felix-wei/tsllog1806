using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_ContainerInquery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dtime = DateTime.Now;
            date_SearchFromDate.Date = dtime.AddDays(-30);
            date_SearchToDate.Date = dtime;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql_where = "";
        if (txt_containers.Text.Length > 0)
        {
            sql_where = "" + txt_containers.Text + "";
            sql_where = sql_where.Replace("\r\n", ",");
            string[] ar = sql_where.Split(',');
            sql_where = "";
            for (int i = 0; i < ar.Length; i++)
            {
                sql_where += (sql_where.Length > 0 ? "," : "") + "'" + ar[i].Trim() + "'";
            }
        }
        else
        {
            sql_where = "''";
        }

        string sql = string.Format(@" select det2.Id,det2.JobNo,det2.ContainerNo,det2.TripCode,det2.DriverCode,det2.TowheadCode,det2.ChessisCode,
det2.FromDate,det2.FromTime,det2.ToDate,det2.ToTime,ToCode,ToParkingLot  
from CTM_JobDet2 as det2 
where datediff(d,det2.ToDate,'{1}')<=0 and DATEDIFF(d,det2.ToDate,'{2}')>=0 and ContainerNo in({0}) and det2.ContainerNo<>''
order by ToDate desc", sql_where, date_SearchFromDate.Date, date_SearchToDate.Date);
        DataTable dt = ConnectSql.GetTab(sql);
        gv_context.DataSource = dt;
        gv_context.DataBind();

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_DispatchPlanner_TrailerHistory : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
		if(!IsPostBack) {
			string no = Request.QueryString["no"] ?? "";
			txt_TrailerNo.Text = no;
			date_To.Date =  DateTime.Today;
			date_From.Date =  DateTime.Today.AddDays(-7);
			btn_Refresh_Click(null, null);
		}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        //string driver = "";
		Response.Redirect("DispatchPlanner_Trailer.aspx");
	}
    protected void btn_Refresh_Click(object sender, EventArgs e)
    {
        //string driver = "";
        string tr_no = txt_TrailerNo.Text.Trim();
		DateTime d1 = date_From.Date;
		DateTime d2 = date_To.Date;
		
		string sql_where = string.Format(@"
		select d2.* from ctm_jobdet2 d2, ctm_jobdet1 d1, ctm_job o where o.jobno=d1.jobno and d1.Id=d2.Det1Id 
		and o.StatusCode<>'CNL' and ChessisCode='{0}' and fromdate >= '{1:yyyy-MM-dd}'
		and fromdate < '{2:yyyy-MM-dd}'
		order by fromdate desc, fromtime desc
		
		", tr_no, d1, d2.AddDays(1));

        DataTable dt_trip = ConnectSql.GetTab(sql_where);
		
  
        this.grid_Trailer.DataSource = dt_trip;
        this.grid_Trailer.DataBind();
  

    }
     

}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Job_DispatchPlanner_TrailerBorrow : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
		if(!IsPostBack) {
			//string no = Request.QueryString["no"] ?? "";
			//txt_TrailerNo.Text = no;
			date_To.Date =  DateTime.Today;
			date_From.Date =  DateTime.Today.AddDays(-14);
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
        string tr_no = "";//txt_TrailerNo.Text.Trim();
		DateTime d1 = date_From.Date;
		DateTime d2 = date_To.Date;
		
		string sql_where = string.Format(@"
		select * from ctm_mastdata where IsNull(note2,'')='N' and date6 >= '{0:yyyy-MM-dd}' and date6 < '{1:yyyy-MM-dd}' order by date6, note4, remark"
		, d1, d2.AddDays(1));

        DataTable dt_trip = ConnectSql.GetTab(sql_where);
		
  
        this.grid_Trailer.DataSource = dt_trip;
        this.grid_Trailer.DataBind();
  

    }
     

}
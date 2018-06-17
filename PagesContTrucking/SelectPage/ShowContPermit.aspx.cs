using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_ShowContPermit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string no = SafeValue.SafeString(Request.QueryString["no"]);
            string sql = string.Format(@"select distinct p.*,det1.Id as ContId from ref_permit p inner join job_house h on p.HblNo=h.HblNo inner join ctm_jobdet1 det1 on det1.Id=h.ContId where det1.Id={0} ",no);
            DataTable dt = ConnectSql_mb.GetDataTable(sql);

            this.grid_permit.DataSource = dt;
            this.grid_permit.DataBind();
        }

    }
}
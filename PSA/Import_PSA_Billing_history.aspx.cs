using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PSA_Import_PSA_Billing_history : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            search_from.Date = DateTime.Now.AddMonths(-1);
            search_to.Date = DateTime.Now;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        DateTime dt_from = SafeValue.SafeDate(search_from.Date, new DateTime(1900, 1, 1));
        DateTime dt_to = SafeValue.SafeDate(search_to.Date, DateTime.Now);

        string sql = string.Format(@"select * from CTM_JobEventLog
where JobType='PSA' and datediff(d,CreateDateTime,'{0}')<=0 and datediff(d,CreateDatetime,'{1}')>=0 
order by Id desc", dt_from, dt_to);
        DataTable dt = ConnectSql.GetTab(sql);
        gv.DataSource = dt;
        gv.DataBind();
    }
}
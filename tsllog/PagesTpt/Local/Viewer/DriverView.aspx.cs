using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesTpt_Job_DriverView : System.Web.UI.Page
{
    public string user_Role = "none";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            search_From.Date = DateTime.Now.AddDays(-15);
            search_To.Date = DateTime.Now.AddDays(8);
            driver_login();
            if (Request.QueryString["driver"] != null && Request.QueryString["date1"] != null && Request.QueryString["date2"] != null)
            {
                search_Driver.Text = Request.QueryString["driver"].ToString();
                search_From.Text = Request.QueryString["date1"].ToString();
                search_To.Text = Request.QueryString["date2"].ToString();
            }
            btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (search_From.Date < new DateTime(1900, 1, 1))
            search_From.Date = DateTime.Now.AddDays(-15);
        if (search_To.Date < new DateTime(1900, 1, 1))
            search_To.Date = DateTime.Now.AddDays(8);
        string sql = string.Format(@"select Id,JobNo,JobProgress,Driver,TptDate,TptTime,Cust,CustPic,BkgQty,BkgPkgtype,BkgWt,BkgM3,PickFrm1,DeliveryTo1 from tpt_job 
where Driver='{0}' and jobProgress<>'Completed' and jobProgress<>'Canceled' and TptDate  between '{1}' and '{2}' order by TptDate,TptTime", search_Driver.Text, search_From.Date, search_To.Date);
        //Driver='{0}' and 
        this.Repeater1.DataSource = ConnectSql.GetTab(sql);
        this.Repeater1.DataBind();
    }
    private void driver_login()
    {
        string sql = "select Role,CustId from [User] where Name='" + EzshipHelper.GetUserName() + "'";
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Equals("Driver"))
        {
            search_Driver.Text = EzshipHelper.GetUserName().ToUpper();
            user_Role = "";
            //sql = "select Code from ctm_driver where Id=" + dt.Rows[0][1];
            //dt = ConnectSql.GetTab(sql);
            //dsDriver.FilterExpression = "code='" + dt.Rows[0][0] + "'";
        }
        else
        {
            sql = "select Code from CTM_driver where statuscode='Active' order by Code";
            dt = ConnectSql.GetTab(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                search_Driver.Items.Add(dt.Rows[i][0].ToString());
                if (i == 0)
                {
                    search_Driver.Text = dt.Rows[0][0].ToString();
                }
            }

        }
    }
}
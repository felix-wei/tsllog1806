using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesTpt_Local_Viewer_DriverView_mp_Data : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string driver = Request.QueryString["driver"].ToString();
            string date1 = Request.QueryString["date1"].ToString();
            string date2 = Request.QueryString["date2"].ToString();
            getData(driver, date1, date2);
        }
    }

    private void getData(string driver, string date1, string date2)
    {

        string sql = string.Format(@"select Id,JobNo,JobProgress,Driver,TptDate,TptTime,Cust,CustPic,BkgQty,BkgPkgtype,BkgWt,BkgM3,PickFrm1,DeliveryTo1 from tpt_job 
where Driver='{0}' and jobProgress<>'Completed' and jobProgress<>'Canceled' and TptDate  between '{1}' and '{2}' order by TptDate,TptTime", driver, date1, date2);
        this.Repeater1.DataSource = ConnectSql.GetTab(sql);
        this.Repeater1.DataBind();
    }
}
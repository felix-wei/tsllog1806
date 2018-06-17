using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_GPSMonitor_DriverList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBind();
        }
    }
    private void DataBind()
    {
        string sql = string.Format(@"select * from CTM_Driver where StatusCode='Active' and isnull(Code,'')<>''");
        search_driver.DataSource = ConnectSql.GetTab(sql);
        search_driver.DataBind();

        DateTime dtime = DateTime.Now;
        search_date.Date = dtime;
        //search_date2.Date = dtime;

        string time=dtime.ToString("HH:mm");
        search_Time1.Text = (dtime.Hour >= 9 ? "09:00" : time);
        search_Time2.Text = time;
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        DataBind();
    }
}
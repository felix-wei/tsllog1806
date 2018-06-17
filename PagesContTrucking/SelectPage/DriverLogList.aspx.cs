using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_DriverLogList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime temp = DateTime.Now;
            if (Request.QueryString["Date"] != null && !Request.QueryString["Date"].ToString().Equals(""))
            {
                string aa = Request.QueryString["Date"].ToString();
                temp = SafeValue.SafeDate(Request.QueryString["Date"], DateTime.Now);
            }
            date_date.Date = temp;
        }
        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = string.Format(@"select log.Driver as Code,driver.Name,log.Towhead as TowheaderCode from CTM_DriverLog as log
left outer join CTM_Driver as driver on log.Driver=driver.Code
where DATEDIFF(d,log.date,'{1}')=0 and (driver.Code like '%{0}%' or driver.Name like '%{0}%') 
union all select '','',''order by log.Driver", name, date_date.Date);

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;

public partial class MastData_AccountPeriod : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.spin_Year.Value = DateTime.Today.Year;
            Session["AcPeriodWhere"] = "Year=" + DateTime.Today.Year;
        }
        if (Session["AcPeriodWhere"] != null)
            this.dsGlAccPeriod.FilterExpression = Session["AcPeriodWhere"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_filter_Click(object sender, EventArgs e)
    {
        if (sender.ToString() == "1")
        {
            Session["AcPeriodWhere"] = "1=1 AND Year=" + this.spin_Year.Number;
        }
        else
            Session["AcPeriodWhere"] = "Year=" + this.spin_Year.Number;

        this.dsGlAccPeriod.FilterExpression = Session["AcPeriodWhere"].ToString();
    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXAccPeriod));
        }
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
       int year=SafeValue.SafeInt(spin_Year.Text,2000);
        if (year<2009)
        {
            Response.Write("Pls ReKey In Account Year");
        }
        else
        {
            string sql = "select count(*) from XXACCPERIOD where year='"+ year +"'";
            int x = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            if (x > 0)
            {
                Response.Write("Finance Year Was Exist!");
            }
            else
            {
                int firstMonth=SafeValue.SafeInt(System.Configuration.ConfigurationManager.AppSettings["AccountFirstMonth"],1);
                DateTime dt1 = new DateTime(year, firstMonth, 1);
                for (int i = 1; i < 13; i++)
                {
                    DateTime dt2 = dt1.AddMonths(i - 1);
                    sql = "insert into XXACCPERIOD (YEAR, PERIOD, STARTDate, ENDDate, CLOSEIND) values(";
                    if (i == 13)
                        sql += string.Format("'{0}','{1}','{2}','{3}','N'", year, i.ToString(), dt2.AddYears(1).AddDays(-1).ToString("yyyy-MM-dd"), dt1.AddYears(1).AddDays(-1).ToString("yyyy-MM-dd"));
                    else
                        sql += string.Format("'{0}','{1}','{2}','{3}','N'", year, i.ToString(), dt2.ToString("yyyy-MM-dd"), dt2.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd"));
                    sql += ")";
                    int res = C2.Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    { }
                }
            }
            
            btn_filter_Click(1, null);
        }
    }
}

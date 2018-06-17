using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_GPSMonitor_ShowMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["DriverCode"] != null && Request["DriverCode"].ToString() != "")
            {
                lb_DriverCode.Text = Request["DriverCode"].ToString();
                string temp1 = Request["Date1"].ToString();
                string[] temp = temp1.Split('/');
                if (temp.Length == 3)
                {
                    temp1 = temp[2] + "-" + temp[1] + "-" + temp[0];
                }
                string temp2 = temp1;
                //string temp2 = Request["Date2"].ToString();
                //temp = temp2.Split('/');
                //if (temp.Length == 3)
                //{
                //    temp2 = temp[2] + "-" + temp[1] + "-" + temp[0];
                //}
                temp1 = temp1 + " " + Request["Time1"].ToString();
                temp2 = temp2 + " " + Request["Time2"].ToString();
                lb_date1.Text = temp1;
                lb_date2.Text = temp2;

                GetGPS(lb_DriverCode.Text, lb_date1.Text, lb_date2.Text);
            }
        }
    } 
    private double lat = 1.368;
    private double lang = 103.800;

    Random r = new Random();
    private double GetLat()
    {
        double pos = Convert.ToDouble(r.Next(-70, 70)) / Convert.ToDouble(1000);
        return lat + pos;
    }
    private double GetLang()
    {
        double pos = Convert.ToDouble(r.Next(-80, 80)) / Convert.ToDouble(1000);
        return lang + pos;
    }

    private void GetGPS(string driver,string date1,string date2)
    {
        string temp = "";
        //for (int i = 0; i < 4; i++)
        //{
        //    temp += GetLat() + "," + GetLang() + ";";
        //}
        string sql = "select geo_latitude as Lat,geo_longitude as Lng,convert(nvarchar(5),create_date_time,108) as dtime From ctm_location where [user]='" + driver + "' and datediff(hour,'" + date1 + "',create_date_time)>=0 and datediff(hour,create_date_time,'" + date2 + "')>=0 order by Id";
        DataTable dt = ConnectSql.GetTab(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            temp += dt.Rows[i][0] + "," + dt.Rows[i][1] + "," + dt.Rows[i][2] + ";";
        }
        if (temp.Length > 0)
        {
            temp = temp.Substring(0, temp.Length - 1);
        }
        txt_Gps.Text = temp;
    }
}
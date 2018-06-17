using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesTpt_Local_Viewer_DriverView_mp_SearchData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request.QueryString["Type"].ToString();
        if (type != null)
        {
            switch (type)
            {
                case "Driver":
                    getData_Driver();
                    break;
                case "Role":
                    getData_Role();
                    break;
                default:
                    break;
            }
        }
    }
    public void getData_Role()
    {
        string sql = "select Role from [User] where Name='" + EzshipHelper.GetUserName() + "'";
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0 )
        {
            Response.Write(dt.Rows[0][0].ToString());
        }
    }

    public void getData_Driver()
    {
        string sql = "select Role,CustId from [User] where Name='" + EzshipHelper.GetUserName() + "'";
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Equals("Driver"))
        {
            Response.Write( EzshipHelper.GetUserName().ToUpper());
        }
        else
        {
            sql = "select Code from CTM_driver where statuscode='Active' order by Code";
            dt = ConnectSql.GetTab(sql);
            string temp = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //search_Driver.Items.Add(dt.Rows[i][0].ToString());
                //if (i == 0)
                //{
                //    search_Driver.Text = dt.Rows[0][0].ToString();
                //}
                if (i == 0)
                {
                    temp = dt.Rows[i][0].ToString();
                }
                else
                {
                    temp += "@#@" + dt.Rows[i][0];
                }
            }
            Response.Write(temp);
        }
    }
}
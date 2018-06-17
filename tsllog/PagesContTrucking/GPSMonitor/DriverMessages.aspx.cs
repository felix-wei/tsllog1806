using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_GPSMonitor_DriverMessages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Send1_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"insert into CTM_DriverMessage (SendTo,Content,StatusCode,SendDate,CreateBy,CreateDate) values ('{0}','{1}','USE',GETDATE(),'{2}',GetDate())", txt_sendTo.Text, txt_Content.Text, HttpContext.Current.User.Identity.Name);
        int result=ConnectSql.ExecuteSql(sql);
        if (result > 0)
        {
            string temp = string.Format(@"Had sent to {0}:\n{1}" ,txt_sendTo.Text , txt_Content.Text);
            Response.Write("<script>alert('" + temp + "');</script>");
        }
    }
}
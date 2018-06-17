using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mobile_SelectPage_Users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
            string type = SafeValue.SafeString(Request.QueryString["p"]);
            if (type.Length > 0)
                this.lab_Type.Text = type;
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select u.Name as Code,u.Name as Name from [user] as u left outer join (
select * from Mobile_ChatGroup_Det where group_name='{0}'
) as c on u.Name=c.username
where c.Id is null and u.name like '{1}%'", this.lab_Type.Text, this.txt_Name.Text);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }
}
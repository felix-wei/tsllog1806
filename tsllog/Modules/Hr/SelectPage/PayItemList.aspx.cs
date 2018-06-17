using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Modules_Hr_SelectPage_PayItemList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql = string.Format(@"select  Code,REPLACE(REPLACE(Code,char(34),'\&#34;'),char(39),'\&#39;') as Code,REPLACE(REPLACE(Description,char(34),'\&#34;'),char(39),'\&#39;') as Description from Hr_PayItem");
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }

    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string code = this.txt_Code.Text.Trim();
        string des = this.txt_Name.Text.Trim();
        string sql = "select Code,Description from Hr_PayItem  where 1=1";
        if (code.Length > 0)
        {
            sql += string.Format(" and Code Like '{0}%'", code.Replace("'","''"));
        }
        else
        {
            sql += string.Format(" and Description Like '{0}%'", des.Replace("'", "''"));
        }
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }

}

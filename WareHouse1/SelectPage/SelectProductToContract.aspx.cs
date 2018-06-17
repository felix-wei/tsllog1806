using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WareHouse_SelectPage_SelectProductToContract : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
        }
        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = @"SELECT Code,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as Name,ProductClass,Description,Att1 from ref_product ";
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string where = "";
        if (name.Length > 0)
        {
            where = GetWhere(where, " Code Like '" + name + "%'");
        }
        sql += where + " ORDER BY Id ";

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
}
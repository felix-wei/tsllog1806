using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WareHouse_SelectPage_ProductList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string type =SafeValue.SafeString(Request.QueryString["type"]);
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = @"SELECT Code,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as Name,REPLACE(REPLACE(Description,char(34),'\&#34;'),char(39),'\&#39;') Description,
Name as Name1,Description as Description1,Att1,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Att11,Att12,Att13,'SKU:' + Code+',ML:'+
isnull(Att4,'')+',AC%:'+isnull(Att5,'')+',CO:'+isnull(Att6,'')+',NRF/REF:'+isnull(Att7,'')+',GBX:'+isnull(Att8,'')+',DECODE:'+isnull(Att9,'') as Attribute from ref_product ";
        string where = "";
        if (name.Length > 0)
        {
            where = GetWhere(where, " where Name Like '" + name.Replace("'", "''") + "%'");
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
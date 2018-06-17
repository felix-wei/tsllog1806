using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class SelectPage_SelectTemplete : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
            btn_Sch_Click(null,null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string refNo = this.txt_RefNo.Text.Trim();
        string content = this.txt_Content.Text.Trim();
        string sql = @"select Id, RefNo,REPLACE(REPLACE(REPLACE(LTRIM(REPLACE(CenterContent, CHAR(13) + CHAR(10), '\n')), CHAR(10), '\n'),char(34),'\&#34;'),char(39),'\&#39;') as CenterContent from Ref_Contract";
        string where = string.Format(" where RefNo<>'{0}'", no);

        if (refNo.Length > 0)
        {
            where = GetWhere(where, string.Format(" RefNo Like '%{0}%'", refNo));

        }
        if(content.Length>0){
            where = GetWhere(where, string.Format(" CenterContent Like '%{0}%'", content));
        }
        sql += where + " order by CreateDateTime desc ";
        //throw new Exception(sql);
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
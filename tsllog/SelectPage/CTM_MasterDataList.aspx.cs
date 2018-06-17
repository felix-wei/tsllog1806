using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SelectPage_CTM_MasterDataList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
            if (Request.QueryString["type"] != null)
            {
                lb_type.Text = Request.QueryString["type"].ToString().ToLower();
            }
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string info = this.txt_Info.Text.Trim().ToUpper();
        string sql = string.Format(@"select Id,Code,Name,Remark from CTM_MastData where type='{0}' ",lb_type.Text);
        if (lb_type.Text.ToLower() == "chessis")
        {
            sql += " and isnull(Type1,'')<>'InActive'";
        }
        if (name.Length > 0)
        {
            sql += string.Format(" and (Code like '%{0}%' or Name like '%{0}%')",name);
        }
        if (info.Length > 0)
        {
            sql +=string.Format(" and (Remark like '%{0}%')",info);
        }
        sql += " order by Code";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}
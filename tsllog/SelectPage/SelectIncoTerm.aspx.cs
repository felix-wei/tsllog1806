using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SelectPage_SelectIncoTerm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_Sch_Click(null,null);
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string code = this.txt_Code.Text.Trim().ToUpper();
        string sql = "select Code,Description from Wh_MastData where Type='IncoTerms'";
        if (code.Length > 0)
        {
            sql += string.Format(" and Code Like '{0}%'", code);
        }
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}
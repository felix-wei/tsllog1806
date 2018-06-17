using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WareHouse_SelectPage_SelectDoNo : System.Web.UI.Page
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
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = @"SELECT *,dbo.fun_GetPartyName(PartyId) as PartyName FROM Wh_DO where ";
        string party = SafeValue.SafeString(Request.QueryString["Party"]);
        string where = "  DoType='IN'";
        if (name.Length > 0)
        {
            where = GetWhere(where, " DoNo Like '" + name + "%'");
        }
        sql += where + " ORDER BY DoNo ";

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
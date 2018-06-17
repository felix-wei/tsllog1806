using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WareHouse_SelectPage_SelectContract : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
            btn_Sch_Click(null,null);
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = @"SELECT ContractNo,WhCode,dbo.fun_GetPartyName(PartyId) as PartyName FROM wh_Contract where ExpireDate>=getdate() and";
        string party = SafeValue.SafeString(Request.QueryString["party"]);
        string where = "  StatusCode='USE'";
        if (name.Length > 0)
        {
            where = GetWhere(where, " ContractNo Like '" + name.Replace("'", "''") + "%'");
        }
        if (party.Length > 0)
        {
            where = GetWhere(where, " (PartyId='" + party + "' or PartyId='')");
        }
        sql += where + " ORDER BY ContractNo ";

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
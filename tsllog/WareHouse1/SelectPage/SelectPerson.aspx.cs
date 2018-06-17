using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;
public partial class WareHouse_SelectPage_SelectPerson : System.Web.UI.Page
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
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string relaId = SafeValue.SafeString(Request.QueryString["relaId"]);
        string partyType = SafeValue.SafeString(Request.QueryString["partyType"]);
        string sql = @"SELECT PartyId, ICNo,Name,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as Name,REPLACE(REPLACE(REPLACE(LTRIM(REPLACE(Address, CHAR(13) + CHAR(10), '\n')), CHAR(10), '\n'),char(34),'\&#34;'),char(39),'\&#39;') as Address,Tel, Contact,Country,City,ZipCode,AccountNo fROM Ref_PersonInfo";
        string where = "";
        if (name.Length > 0)
        {
            where = GetWhere(where, string.Format("  Name Like '{0}%'", name.Replace("'", "''")));
        }
        if (relaId.Length > 0 && relaId != "0")
        {
            where = GetWhere(where, string.Format(" RelationId='{0}'", relaId));
        }
        if (partyType.Length > 0)
        {
            where = GetWhere(where, string.Format(" Type='{0}'", partyType));
        }
        if (where.Length > 0)
        {

            sql += " where " + where + " ORDER BY PartyId ";

            //throw new Exception(sql);
            DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            this.ASPxGridView1.DataSource = tab;
            this.ASPxGridView1.DataBind();
        }
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
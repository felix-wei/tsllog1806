using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;

public partial class WareHouse_SelectPage_PartyList : System.Web.UI.Page
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
        string sql = @"SELECT PartyId, Code,Name,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as PartyName,REPLACE(REPLACE(REPLACE(LTRIM(REPLACE(Address, CHAR(13) + CHAR(10), '\n')), CHAR(10), '\n'),char(34),'\&#34;'),char(39),'\&#39;') as Address,Tel1, Contact1,CountryId as Country,City,ZipCode fROM XXParty where Status='USE'";
            sql += "and 1=1";
        if (name.Length > 0)
        {
            sql += string.Format("  and Name Like '{0}%'", name.Replace("'", "''"));
        }
        sql += " ORDER BY PartyId ";

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
}
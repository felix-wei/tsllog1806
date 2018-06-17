using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;

public partial class WareHouse_SelectPage_SelectSalesOrderParty : System.Web.UI.Page
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
        string partyType = SafeValue.SafeString(Request.QueryString["partyType"]);
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = @"SELECT p.PartyId, Code,Name,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as PartyName,REPLACE(REPLACE(REPLACE(LTRIM(REPLACE(Address, CHAR(13) + CHAR(10), '\n')), CHAR(10), '\n'),char(34),'\&#34;'),char(39),'\&#39;') as Address1,
REPLACE(REPLACE(REPLACE(LTRIM(REPLACE(Address1, CHAR(13) + CHAR(10), '\n')), CHAR(10), '\n'),char(34),'\&#34;'),char(39),'\&#39;') as Address2,Tel1, Contact1,CountryId as Country,City,ZipCode,s.Salesman as SalesmanId fROM XXParty p
left outer join  XXPartySales s on s.PartyId=p.PartyId  and s.DefaultInd='Y'";
        string where = " where Status='USE'";
        if (name.Length > 0)
        {
            where = GetWhere(where, string.Format(" (p.PartyId LIKE '%{0}%' or  Name Like '%{0}%')", name.Replace("'", "''")));
        }
        if (partyType.Length > 0)
        {
            if (partyType == "C")
            {
                where = GetWhere(where, " IsCustomer='True'");
            }
            if (partyType == "V")
            {
                where = GetWhere(where, " IsVendor='True'");
            }
            if (partyType == "A")
            {
                where = GetWhere(where, " IsAgent='True' ");
            }
        }
        sql += where + " ORDER BY p.PartyId ";
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
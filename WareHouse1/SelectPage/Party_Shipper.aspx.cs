using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;

public partial class SelectPage_Party_Shipper : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = @"SELECT PartyId, Code, Name,Tel1,Fax1,Email1,Contact1 fROM XXParty where ";
        string partyType = SafeValue.SafeString(Request.QueryString["partyType"]).ToUpper();
        if (partyType.Length == 0)
        {
            sql += "1=1";
        }
        else
        {
            string where = "(";
            if (partyType.IndexOf("C") != -1)
            {
                if (where.Length == 1)
                    where += "IsCustomer='True'";
                else
                    where += " or IsCustomer='True'";

            }
            if (partyType.IndexOf("V") != -1)
            {
                if (where.Length == 1)
                    where += "IsVendor='True'";
                else
                    where += " or IsVendor='True'";
            }
            if (partyType.IndexOf("A") != -1)
            {
                if (where.Length == 1)
                    where += "IsAgent='True'";
                else
                    where += " or IsAgent='True'";
            }
            if (where.Length > 1)
                sql += where + ")";
        } sql += " ORDER BY Name ";

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}

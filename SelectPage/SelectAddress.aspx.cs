using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SelectPage_SelectAddress : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // btn_Sch_Click(null, null);
            string party = SafeValue.SafeString(Request.QueryString["party"]).Trim();
            if (party.Length > 0)
            {
                txt_party.Text = party;
                btn_Sch_Click(null, null);
            }
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string code = this.txt_Code.Text.Trim().ToUpper();
        string sql = @"SELECT Id, PartyId, PartyName,
REPLACE(REPLACE(REPLACE(Address,char(34),'\&#34;'),char(39),'\&#39;'),char(10),' ') as Address,
REPLACE(REPLACE(REPLACE(Address1,char(34),'\&#34;'),char(39),'\&#39;'),char(10),' ') as Address1 
FROM ref_address ";
        string where = "";

        if (code.Length > 0)
        {
            where = GetWhere(where, string.Format(" Address Like '%{0}%'", code.Replace("'", "''")));

        }
        if (SafeValue.SafeString(cbb_Type.Value) != "")
        {
            where = GetWhere(where, string.Format(" TypeId='{0}'", SafeValue.SafeString(cbb_Type.Value)));
        }
        if (SafeValue.SafeString(cbb_Location.Value) != "")
        {
            where = GetWhere(where, string.Format(" Location='{0}'", SafeValue.SafeString(cbb_Location.Value)));
        }
        string party = SafeValue.SafeString(txt_party.Text);
        if (party.Length > 0)
        {
            where = GetWhere(where, string.Format(" PartyId='{0}'", party));
        }
        if(where.Length>0)
          sql +="where"+ where + " ORDER BY Id ";

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
    protected void cbb_Location_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
       
    }
    protected void cbb_Type_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        string type = SafeValue.SafeString(cbb_Type.Value);
        if (type == "Location")
        {
            dsUom.FilterExpression = "CodeType='3'";
            cbb_Location.DataSource=dsUom;
        }
        if (type == "Owner")
        {
            dsUom.FilterExpression = "CodeType='4'";
            cbb_Location.DataSource = dsUom;
        }
    }
    protected void FillLocation(string typeId)
    {
        if (string.IsNullOrEmpty(typeId)) return;
        if (typeId == "Location")
        {
            dsUom.FilterExpression = "CodeType='3'";
            cbb_Location.DataSource = dsUom;
            cbb_Location.DataBind();
        }
        if (typeId == "Owner")
        {
            dsUom.FilterExpression = "CodeType='4'";
            cbb_Location.DataSource = dsUom;
            cbb_Location.DataBind();
        }
        
    }
    protected void cbb_Location_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        FillLocation(e.Parameter);
    }
}
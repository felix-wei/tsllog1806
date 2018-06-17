using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System.Configuration;

public partial class WareHouse_MastData_Party : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["NameWhere"] = null;
            this.dsXXParty.FilterExpression = "1=0";
        }
        if (Session["NameWhere"] != null)
        {
            this.dsXXParty.FilterExpression = Session["NameWhere"].ToString();
        }
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXParty));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string country = ConfigurationManager.AppSettings["Country"];
        string port = ConfigurationManager.AppSettings["LocalPort"];
        string currency = ConfigurationManager.AppSettings["Currency"];
        e.NewValues["PartyId"] = "NEW";
        e.NewValues["Address"] = " ";
        e.NewValues["Tel1"] = " ";
        e.NewValues["Tel2"] = " ";
        e.NewValues["Fax1"] = " ";
        e.NewValues["Fax2"] = " ";
        e.NewValues["Contact1"] = " ";
        e.NewValues["Contact2"] = " ";
        e.NewValues["Email1"] = " ";
        e.NewValues["Email2"] = " ";
        e.NewValues["CountryId"] = country;
        e.NewValues["PortId"] = port;
        e.NewValues["Remark"] = " ";
        e.NewValues["TermId"] = "CASH";
        e.NewValues["IsCustomer"] = true;
        e.NewValues["IsVendor"] = false;
        e.NewValues["IsAgent"] = false;
        e.NewValues["GroupId"] = "";
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        e.NewValues["Status"] = "USE";
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);

    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
    }
    protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;

        if (s == "Cancle")
            this.grid.CancelEdit();

    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string name = this.txt_Name.Text.Trim().ToUpper();
        string where = "";
        if (name.Length > 0)
        {
            where = string.Format("NAME LIKE '{0}%'", name);
        }
        else
        {
            where = "1=1";
        }
        this.dsXXParty.FilterExpression = where;
        Session["NameWhere"] = where;
    }

    protected void grid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txt_PartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
            txt_PartyId.Enabled = false;
            string partyId = SafeValue.SafeString(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "PartyId" }));
           
        }
    }

    #region Price

    #endregion
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

public partial class PagesMaster_PersonInfo : System.Web.UI.Page
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
            
            string partyType = SafeValue.SafeString(Request.QueryString["type"]);
            string where = "";
            if (partyType.Length > 0)
            {

                where = GetWhere(where, " Type='" + partyType + "'");
            }
            Session["NameWhere"] = where;
        }
        if (Session["NameWhere"] != null)
        {
            this.dsPersonInfo.FilterExpression = Session["NameWhere"].ToString();
        }
    }

    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.RefPersonInfo));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string partyType = SafeValue.SafeString(Request.QueryString["type"]);
        e.NewValues["PartyId"] = "NEW";
        e.NewValues["Address"] = " ";
        e.NewValues["Name"] = " ";
        e.NewValues["AccountNo"] = " ";
        e.NewValues["Type"] = partyType;
        e.NewValues["Address"] = " ";
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);

    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
    }
    protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Save")
            AddOrUpdate();
        else if (s == "Cancle")
            this.grid.CancelEdit();
    }
    protected void AddOrUpdate()
    {
        try
        {

            ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txt_Id = pageControl.FindControl("txt_Id") as ASPxTextBox;
            ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
            ASPxTextBox txtCode = pageControl.FindControl("txtCode") as ASPxTextBox;
            ASPxLabel lblMess = pageControl.FindControl("lblMessage") as ASPxLabel;
            string id = SafeValue.SafeString(txt_Id.Text, "");
            string partyId = SafeValue.SafeString(txtPartyId.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(RefPersonInfo), "Id='" + id + "'");
            RefPersonInfo party = C2.Manager.ORManager.GetObject(query) as RefPersonInfo;
            bool action = false;
            if (party == null)
            {
                action = true;
                party = new RefPersonInfo();
                    partyId = partyId.Replace(" ", "_");
                    string sql = string.Format("select count(PartyId) from Ref_PersonInfo where PartyId='{0}'", partyId);
                    int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
                    if (cnt > 0)
                    {
                        throw new Exception(string.Format("Duplicate Code!"));
                        return;
                    }
                    sql = string.Format("select count(ICNo) from Ref_PersonInfo where ICNo='{0}'", partyId);
                    cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
                    if (cnt > 0)
                    {
                        throw new Exception(string.Format("Duplicate ICNo!"));
                        return;
                    }
                party.PartyId = partyId;
            }
            ASPxTextBox txtName = pageControl.FindControl("txtName") as ASPxTextBox;
            string name = txtName.Text.Trim();
            party.Name = name;
            ASPxMemo memoAddress = pageControl.FindControl("txtAddress") as ASPxMemo;
            party.Address = memoAddress.Text;
            ASPxTextBox txtICNo = pageControl.FindControl("txtICNo") as ASPxTextBox;
            party.ICNo = txtICNo.Text;
            ASPxTextBox txtTel = pageControl.FindControl("txtTel") as ASPxTextBox;
            party.Tel = txtTel.Text;

            ASPxTextBox txtContact = pageControl.FindControl("txtContact") as ASPxTextBox;
            party.Contact = txtContact.Text;
            ASPxTextBox txt_ZipCode = pageControl.FindControl("txt_ZipCode") as ASPxTextBox;
            party.ZipCode = txt_ZipCode.Text;
            ASPxComboBox cbCountry = pageControl.FindControl("cbCountry") as ASPxComboBox;
            party.Country= SafeValue.SafeString(cbCountry.Value);
            ASPxComboBox cbCity = pageControl.FindControl("cbCity") as ASPxComboBox;
            party.City = SafeValue.SafeString(cbCity.Value);
            ASPxComboBox cmb_Type = pageControl.FindControl("cmb_Type") as ASPxComboBox;
            party.Type = SafeValue.SafeString(cmb_Type.Value);
            ASPxButtonEdit txt_Customer = pageControl.FindControl("txt_Customer") as ASPxButtonEdit;
            party.RelationId = txt_Customer.Text;
            ASPxTextBox txtAccountNo = pageControl.FindControl("txtAccountNo") as ASPxTextBox;
            party.AccountNo = txtAccountNo.Text;
            if (action)
            {

                Manager.ORManager.StartTracking(party, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(party);
            }
            else
            {

                Manager.ORManager.StartTracking(party, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(party);
            }
            Session["NameWhere"] = "PartyId='" + party.PartyId + "'";
            this.dsPersonInfo.FilterExpression = Session["NameWhere"].ToString();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string name = this.txt_Name.Text.Trim().ToUpper();
       string partyType = SafeValue.SafeString(Request.QueryString["type"]);
        string where = "";
        if (name.Length > 0)
        {
            where = string.Format("NAME LIKE '{0}%'", name.Replace("'", "''"));
        }
        if (partyType.Length > 0)
        {
           where = GetWhere(where, " Type='" + partyType + "'");
        }
        else
        {
            where = "1=1";
        }
        this.dsPersonInfo.FilterExpression = where;
        Session["NameWhere"] = where;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void grid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txt_PartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
            txt_PartyId.Enabled = false;
            ASPxTextBox txtICNo = pageControl.FindControl("txtICNo") as ASPxTextBox;
            ASPxTextBox txt_CustomerName = pageControl.FindControl("txt_CustomerName") as ASPxTextBox;
            txt_CustomerName.Text = EzshipHelper.GetPartyName(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "RelationId" }));
        }
    }
}
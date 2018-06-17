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

public partial class WareHouse_SelectPage_SelectParty : System.Web.UI.Page
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
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string partyType = SafeValue.SafeString(Request.QueryString["partyType"]);
        //string groupId = SafeValue.SafeString(Request.QueryString["groupId"]);
        string name = this.txt_Name.Text.Trim().ToUpper();
        //string sql = @"SELECT PartyId, Code,Name,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as PartyName,REPLACE(REPLACE(REPLACE(LTRIM(REPLACE(Address, CHAR(13) + CHAR(10), '\n')), CHAR(10), '\n'),char(34),'\&#34;'),char(39),'\&#39;') as Address,Tel1,Fax1,Email1,Contact1,CountryId,City,ZipCode fROM XXParty ";
        string where = " Status='USE'";

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
        //if (groupId.Length == 0)
        //{
        //    where = GetWhere(where, " GroupId<>'BANK'");
        //}
        //if (groupId.Length > 0)
        //{
        //    where = GetWhere(where, " GroupId='" + groupId + "'");
        //}
        if (name.Length > 0)
        {
            where = GetWhere(where, string.Format(" (Name='{0}%' or  Name Like '%{0}%')", name.Replace("'", "''")));

        }
        else
        {
            where = GetWhere(where,"1=0");
        }
        this.dsXXParty.FilterExpression = where;
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
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXParty));
        }
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (e.NewValues["PartyId"] == "")
        {
                throw new Exception("Party Id not be null!!!");
                return;            
        }
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void AddOrUpdate()
    {
        try
        {
            ASPxTextBox txt_SequenceId =this.ASPxGridView1.FindEditFormTemplateControl("txt_SequenceId") as ASPxTextBox;
            ASPxTextBox txtPartyId =this.ASPxGridView1.FindEditFormTemplateControl("txtPartyId") as ASPxTextBox;
            ASPxTextBox txtCode =this.ASPxGridView1.FindEditFormTemplateControl("txtCode") as ASPxTextBox;
            ASPxLabel lblMess =this.ASPxGridView1.FindEditFormTemplateControl("lblMessage") as ASPxLabel;
            string sequenceId = SafeValue.SafeString(txt_SequenceId.Text, "");
            string partyId = SafeValue.SafeString(txtPartyId.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(XXParty), "SequenceId='" + sequenceId + "'");
            XXParty party = C2.Manager.ORManager.GetObject(query) as XXParty;
            bool action = false;
            bool autoNo = false;
            if (party == null)
            {
                action = true;
                party = new XXParty();
                if (partyId.Length == 0 || partyId == "NEW")
                {
                    partyId = C2Setup.GetNextNo("BusinessParty");
                    autoNo = true;
                }
                else
                {
                    partyId = partyId.Replace(" ", "_");
                    string sql = string.Format("select count(PartyId) from XXParty where PartyId='{0}'", partyId);
                    int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
                    if (cnt > 0)
                    {
                        throw new Exception(string.Format("Duplicate PartyId!"));
                        return;
                    }
                }
                party.PartyId = partyId;
            }
            party.Code = txtCode.Text;
            ASPxTextBox txtName =this.ASPxGridView1.FindEditFormTemplateControl("txtName") as ASPxTextBox;
            string name = txtName.Text.Trim();
            if (txtName.Text == "")
            {
                throw new Exception("Full Name not be null!!!");
                return;
            }
            party.Name = name;
            ASPxLabel lblStatus =this.ASPxGridView1.FindEditFormTemplateControl("lblStatus") as ASPxLabel;
            party.Status = "USE";
            ASPxCheckBox Customer =this.ASPxGridView1.FindEditFormTemplateControl("Customer") as ASPxCheckBox;
            ASPxCheckBox Vendor =this.ASPxGridView1.FindEditFormTemplateControl("Vendor") as ASPxCheckBox;
            ASPxCheckBox Agent =this.ASPxGridView1.FindEditFormTemplateControl("Agent") as ASPxCheckBox;
            party.IsCustomer = SafeValue.SafeBool(Customer.Checked, false);

            party.IsVendor = SafeValue.SafeBool(Vendor.Checked, false);
            party.IsAgent = SafeValue.SafeBool(Agent.Checked, false);
            ASPxMemo memoAddress =this.ASPxGridView1.FindEditFormTemplateControl("txtAddress") as ASPxMemo;
            party.Address = memoAddress.Text;
            ASPxTextBox crNo =this.ASPxGridView1.FindEditFormTemplateControl("txtCrNo") as ASPxTextBox;
            party.CrNo = crNo.Text;
            ASPxTextBox txtTel1 =this.ASPxGridView1.FindEditFormTemplateControl("txtTel1") as ASPxTextBox;
            party.Tel1 = txtTel1.Text;
            ASPxTextBox txtFax1 =this.ASPxGridView1.FindEditFormTemplateControl("txtFax1") as ASPxTextBox;
            party.Fax1 = txtFax1.Text;
            ASPxTextBox txtTel2 =this.ASPxGridView1.FindEditFormTemplateControl("txtTel2") as ASPxTextBox;
            party.Tel2 = txtTel2.Text;
            ASPxTextBox txtFax2 =this.ASPxGridView1.FindEditFormTemplateControl("txtFax2") as ASPxTextBox;
            party.Fax2 = txtFax2.Text;
            ASPxTextBox txtContact1 =this.ASPxGridView1.FindEditFormTemplateControl("txtContact1") as ASPxTextBox;
            party.Contact1 = txtContact1.Text;
            ASPxTextBox txtContact2 =this.ASPxGridView1.FindEditFormTemplateControl("txtContact2") as ASPxTextBox;
            party.Contact2 = txtContact2.Text;
            ASPxTextBox txtEmail1 =this.ASPxGridView1.FindEditFormTemplateControl("txtEmail1") as ASPxTextBox;
            party.Email1 = txtEmail1.Text;
            ASPxTextBox txtEmail2 =this.ASPxGridView1.FindEditFormTemplateControl("txtEmail2") as ASPxTextBox;
            party.Email2 = txtEmail2.Text;
            ASPxTextBox txt_ZipCode =this.ASPxGridView1.FindEditFormTemplateControl("txt_ZipCode") as ASPxTextBox;
            party.ZipCode = txt_ZipCode.Text;
            ASPxComboBox cbCountry =this.ASPxGridView1.FindEditFormTemplateControl("cbCountry") as ASPxComboBox;
            party.CountryId = SafeValue.SafeString(cbCountry.Value);
            ASPxComboBox cbCity =this.ASPxGridView1.FindEditFormTemplateControl("cbCity") as ASPxComboBox;
            party.City = SafeValue.SafeString(cbCity.Value);
            ASPxComboBox cbPort =this.ASPxGridView1.FindEditFormTemplateControl("cbPort") as ASPxComboBox;
            party.PortId = SafeValue.SafeString(cbPort.Value);
            ASPxComboBox cbTerm =this.ASPxGridView1.FindEditFormTemplateControl("cbTerm") as ASPxComboBox;
            party.TermId = SafeValue.SafeString(cbTerm.Value);
            ASPxComboBox cbGroup =this.ASPxGridView1.FindEditFormTemplateControl("cbGroup") as ASPxComboBox;
            party.GroupId = SafeValue.SafeString(cbGroup.Value);
            ASPxMemo memoRemark =this.ASPxGridView1.FindEditFormTemplateControl("memoRemark") as ASPxMemo;
            party.Remark = memoRemark.Text;

            ASPxSpinEdit spin_WarningAmt =this.ASPxGridView1.FindEditFormTemplateControl("spin_WarningAmt") as ASPxSpinEdit;
            party.WarningAmt = SafeValue.SafeDecimal(spin_WarningAmt.Value);
            ASPxSpinEdit spin_WarningQty =this.ASPxGridView1.FindEditFormTemplateControl("spin_WarningQty") as ASPxSpinEdit;
            party.WarningQty = SafeValue.SafeInt(spin_WarningQty.Value, 0);
            ASPxSpinEdit spin_BlockAmt =this.ASPxGridView1.FindEditFormTemplateControl("spin_BlockAmt") as ASPxSpinEdit;
            party.BlockAmt = SafeValue.SafeDecimal(spin_BlockAmt.Value);
            ASPxSpinEdit spin_BlockQty =this.ASPxGridView1.FindEditFormTemplateControl("spin_BlockQty") as ASPxSpinEdit;
            party.BlockQty = SafeValue.SafeInt(spin_BlockQty.Value, 0);

            //ASPxButtonEdit txt_ParentId =this.ASPxGridView1.FindEditFormTemplateControl("txt_ParentId") as ASPxButtonEdit;
            //party.ParentId = txt_ParentId.Text;
            //ASPxTextBox txt_ParentName =this.ASPxGridView1.FindEditFormTemplateControl("txt_ParentName") as ASPxTextBox;
            //party.ParentName = txt_ParentName.Text;
            if (action)
            {
                party.CreateBy = HttpContext.Current.User.Identity.Name;
                party.CreateDateTime = DateTime.Now;
                if (autoNo)
                    C2Setup.SetNextNo("BusinessParty", partyId);
                Manager.ORManager.StartTracking(party, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(party);

                txtPartyId.Text = partyId.ToString();
                XXPartyAcc acc = new XXPartyAcc();
                acc.ApCode = System.Configuration.ConfigurationManager.AppSettings["LocalArCode"];
                acc.ArCode = System.Configuration.ConfigurationManager.AppSettings["LocalApCode"];
                acc.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                acc.PartyId = party.PartyId;
                Manager.ORManager.StartTracking(acc, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(acc);
            }
            else
            {
                party.UpdateBy = HttpContext.Current.User.Identity.Name;
                party.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(party, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(party);
            }
            Session["NameWhere"] = "PartyId='" + party.PartyId + "'";
            this.dsXXParty.FilterExpression = Session["NameWhere"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Save")
        {
            AddOrUpdate();
            this.ASPxGridView1.CancelEdit();
        }
        else if (s == "Cancle")
            this.ASPxGridView1.CancelEdit();
    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.ASPxGridView1.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox txt_PartyId = this.ASPxGridView1.FindEditFormTemplateControl("txtPartyId") as ASPxTextBox;
            txt_PartyId.Enabled = false;
            string partyId = SafeValue.SafeString(this.ASPxGridView1.GetRowValues(this.ASPxGridView1.EditingRowVisibleIndex, new string[] { "PartyId" }));
            string sql = "select Salesman from XXPartySales where PartyId='" + partyId + "' and DefaultInd='Y'";
            string salesMan = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
            ASPxTextBox txt_DefaultSale = this.ASPxGridView1.FindEditFormTemplateControl("txt_DefaultSale") as ASPxTextBox;
            txt_DefaultSale.Text = salesMan;
        }
    }
}
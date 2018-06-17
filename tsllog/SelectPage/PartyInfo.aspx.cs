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

public partial class SelectPage_PartyInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["CTM_Job"] = null;
            if (Request.QueryString["partyId"] != null && Request.QueryString["partyId"].ToString() != "0")
            {
                string partyId = Request.QueryString["partyId"].ToString();
                Session["NameWhere"] = "PartyId='" + partyId + "'";
                this.dsXXParty.FilterExpression = Session["NameWhere"].ToString();
                if (this.grid.GetRow(0) != null)
                    this.grid.StartEdit(0);
            }
            else
            {
                this.grid.AddNewRow();
            }
        }

        if (Session["NameWhere"] != null)
        {
            this.dsXXParty.FilterExpression = Session["NameWhere"].ToString();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);
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
        string group = SafeValue.SafeString(Request.QueryString["group"]);
        string country = ConfigurationManager.AppSettings["Country"].ToString();
        string port = ConfigurationManager.AppSettings["LocalPort"].ToString();
        string currency = ConfigurationManager.AppSettings["Currency"].ToString();
        e.NewValues["PartyId"] = "NEW";
        e.NewValues["Address"] = " ";
        e.NewValues["Address1"] = " ";
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
        e.NewValues["GroupId"] = group;
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        e.NewValues["Status"] = "USE";
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

        string sql = string.Format(@"select (select count(SequenceId) from XaArInvoice Where PartyTo='{0}')
+(select count(SequenceId) from XaArReceipt Where PartyTo='{0}')
+(select count(SequenceId) from XaApPayable Where PartyTo='{0}')
+(select count(SequenceId) from XaApPayment Where PartyTo='{0}')", SafeValue.SafeString(e.Values["PartyId"]));
        int cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
        if (cnt > 0)
            throw new Exception("Have transactions, can't delete!");
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);

    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        if (s == "Block")
        {
            #region block
            ASPxLabel lblStatus = pageControl.FindControl("lblStatus") as ASPxLabel;
            string sql = "select Status from XXParty where PartyId='" + txtPartyId.Text + "'";
            string user = HttpContext.Current.User.Identity.Name;
            string role = EzshipHelper.GetUseRole();
            if (role == "Admin" || role == "AccountManager" || role == "AccountStaff")
            {

                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// closeIndStr.Text;
                if (closeInd == "USE")
                {
                    sql = string.Format("update XXParty set Status='InActive' where PartyId='{0}'", txtPartyId.Text);
                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                    {
                        e.Result = "Success";
                    }
                    else
                    {
                        e.Result = "Fail";
                    }
                }
                else
                {
                    sql = string.Format("update XXParty set Status='USE' where PartyId='{0}'", txtPartyId.Text);

                    int res = Manager.ORManager.ExecuteCommand(sql);
                    if (res > 0)
                        e.Result = "Success";
                    else
                        e.Result = "Fail";

                }

            }
            else
            {
                e.Result = "";
            }
            #endregion
        }
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
            ASPxTextBox txt_SequenceId = pageControl.FindControl("txt_SequenceId") as ASPxTextBox;
            ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
            ASPxTextBox txtCode = pageControl.FindControl("txtCode") as ASPxTextBox;
            ASPxLabel lblMess = pageControl.FindControl("lblMessage") as ASPxLabel;
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
            if (txtCode.Text == "")
            {
                throw new Exception("Short Name not be null!!!");
                return;
            }
            party.Code = txtCode.Text;
            ASPxTextBox txtName = pageControl.FindControl("txtName") as ASPxTextBox;
            string name = txtName.Text.Trim();
            if (txtName.Text == "")
            {
                throw new Exception("Full Name not be null!!!");
                return;
            }
            party.Name = name;
            ASPxLabel lblStatus = pageControl.FindControl("lblStatus") as ASPxLabel;
            party.Status = "USE";
            ASPxCheckBox Customer = pageControl.FindControl("Customer") as ASPxCheckBox;
            ASPxCheckBox Vendor = pageControl.FindControl("Vendor") as ASPxCheckBox;
            ASPxCheckBox Agent = pageControl.FindControl("Agent") as ASPxCheckBox;
            party.IsCustomer = SafeValue.SafeBool(Customer.Checked, false);

            party.IsVendor = SafeValue.SafeBool(Vendor.Checked, false);
            party.IsAgent = SafeValue.SafeBool(Agent.Checked, false);
            ASPxMemo memoAddress = pageControl.FindControl("txtAddress") as ASPxMemo;
            party.Address = memoAddress.Text;
            ASPxMemo memoAddress1 = pageControl.FindControl("txtAddress1") as ASPxMemo;
            party.Address1 = memoAddress1.Text;
            ASPxTextBox crNo = pageControl.FindControl("txtCrNo") as ASPxTextBox;
            party.CrNo = crNo.Text;
            ASPxTextBox txtTel1 = pageControl.FindControl("txtTel1") as ASPxTextBox;
            party.Tel1 = txtTel1.Text;
            ASPxTextBox txtFax1 = pageControl.FindControl("txtFax1") as ASPxTextBox;
            party.Fax1 = txtFax1.Text;
            ASPxTextBox txtTel2 = pageControl.FindControl("txtTel2") as ASPxTextBox;
            party.Tel2 = txtTel2.Text;
            ASPxTextBox txtFax2 = pageControl.FindControl("txtFax2") as ASPxTextBox;
            party.Fax2 = txtFax2.Text;
            ASPxTextBox txtContact1 = pageControl.FindControl("txtContact1") as ASPxTextBox;
            party.Contact1 = txtContact1.Text;
            ASPxTextBox txtContact2 = pageControl.FindControl("txtContact2") as ASPxTextBox;
            party.Contact2 = txtContact2.Text;
            ASPxTextBox txtEmail1 = pageControl.FindControl("txtEmail1") as ASPxTextBox;
            party.Email1 = txtEmail1.Text;
            ASPxTextBox txtEmail2 = pageControl.FindControl("txtEmail2") as ASPxTextBox;
            party.Email2 = txtEmail2.Text;
            ASPxTextBox txt_ZipCode = pageControl.FindControl("txt_ZipCode") as ASPxTextBox;
            party.ZipCode = txt_ZipCode.Text;
            ASPxComboBox cbCountry = pageControl.FindControl("cbCountry") as ASPxComboBox;
            party.CountryId = SafeValue.SafeString(cbCountry.Value);
            ASPxComboBox cbCity = pageControl.FindControl("cbCity") as ASPxComboBox;
            party.City = SafeValue.SafeString(cbCity.Value);
            ASPxComboBox cbPort = pageControl.FindControl("cbPort") as ASPxComboBox;
            party.PortId = SafeValue.SafeString(cbPort.Value);
            ASPxComboBox cbTerm = pageControl.FindControl("cbTerm") as ASPxComboBox;
            party.TermId = SafeValue.SafeString(cbTerm.Value);
            ASPxComboBox cbGroup = pageControl.FindControl("cbGroup") as ASPxComboBox;
            party.GroupId = SafeValue.SafeString(cbGroup.Value);
            ASPxMemo memoRemark = pageControl.FindControl("memoRemark") as ASPxMemo;
            party.Remark = memoRemark.Text;

            ASPxComboBox cbb_ClientType = pageControl.FindControl("cbb_ClientType") as ASPxComboBox;
            party.ClientType = SafeValue.SafeString(cbb_ClientType.Text);
            ASPxButtonEdit btn_ParentCode = pageControl.FindControl("btn_ParentCode") as ASPxButtonEdit;
            party.ParentCode = btn_ParentCode.Text;

            ASPxSpinEdit spin_WarningAmt = pageControl.FindControl("spin_WarningAmt") as ASPxSpinEdit;
            party.WarningAmt = SafeValue.SafeDecimal(spin_WarningAmt.Value);
            ASPxSpinEdit spin_WarningQty = pageControl.FindControl("spin_WarningQty") as ASPxSpinEdit;
            party.WarningQty = SafeValue.SafeInt(spin_WarningQty.Value, 0);
            ASPxSpinEdit spin_BlockAmt = pageControl.FindControl("spin_BlockAmt") as ASPxSpinEdit;
            party.BlockAmt = SafeValue.SafeDecimal(spin_BlockAmt.Value);
            ASPxSpinEdit spin_BlockQty = pageControl.FindControl("spin_BlockQty") as ASPxSpinEdit;
            party.BlockQty = SafeValue.SafeInt(spin_BlockQty.Value, 0);
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
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
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
            string partyId = SafeValue.SafeString(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "PartyId" }));
            string sql = "select Salesman from XXPartySales where PartyId='" + partyId + "' and DefaultInd='Y'";
            string salesMan = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
            ASPxTextBox txt_DefaultSale = pageControl.FindControl("txt_DefaultSale") as ASPxTextBox;
            txt_DefaultSale.Text = salesMan;
        }
    }
    #region Account Info
    protected void grid_Account_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXPartyAcc));
        }
    }
    protected void grid_Account_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["ArCode"] = System.Configuration.ConfigurationManager.AppSettings["LocalArCode"];
        e.NewValues["ApCode"] = System.Configuration.ConfigurationManager.AppSettings["LocalApCode"];
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox partyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        e.NewValues["PartyId"] = partyId.Text;
    }
    protected void grid_Account_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void grid_Account_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox partyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        e.NewValues["PartyId"] = partyId.Text;

    }
    protected void grid_Account_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

    }
    protected void grid_Account_DataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox partyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        ASPxGridView grd = sender as ASPxGridView;
        this.dsPartyAcc.FilterExpression = "PartyId='" + SafeValue.SafeString(partyId.Text, "-1") + "'";
    }
    #endregion

    #region Sale
    protected void grid_Sale_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXPartySale));
        }
    }
    protected void grid_Sale_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox partyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        e.NewValues["PartyId"] = partyId.Text;
        string count = GetPartyDefaultSaleMan(partyId);
        if (count.Length > 0)
        {
            e.NewValues["DefaultInd"] = "N";
        }

    }
    protected void grid_Sale_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Sale_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox partyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        e.NewValues["PartyId"] = partyId.Text;
        string defaultMan = SafeValue.SafeString(e.NewValues["DefaultInd"]);
        if (defaultMan == "Y")
        {
            UpdatePartyDefaultSaleMan();
        }

    }
    protected void grid_Sale_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string defaultMan = SafeValue.SafeString(e.NewValues["DefaultInd"]);
        if (defaultMan == "Y")
        {
            UpdatePartyDefaultSaleMan();
        }
    }
    protected void grid_Sale_DataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox partyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        ASPxGridView grd = sender as ASPxGridView;
        this.dsPartySale.FilterExpression = "PartyId='" + SafeValue.SafeString(partyId.Text, "-1") + "'";
    }
    private static string GetPartyDefaultSaleMan(object partyId)
    {
        string sql = "select count(*) from XXPartySales where PartyId='" + partyId + "' and DefaultInd='Y'";
        return SafeValue.SafeString(C2.Manager.ORManager.ExecuteCommand(sql));
    }
    private static string UpdatePartyDefaultSaleMan()
    {
        string sql = "update XXPartySales set DefaultInd='N' where DefaultInd='Y'";
        return SafeValue.SafeString(C2.Manager.ORManager.ExecuteCommand(sql));
    }
    #endregion

    #region Log
    protected void grid_Log_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.RefPartyLog));
        }
    }
    protected void grid_Log_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox partyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        e.NewValues["PartyId"] = partyId.Text;
        e.NewValues["LogDateTime"] = DateTime.Now;

    }
    protected void grid_Log_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Log_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox partyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        e.NewValues["PartyId"] = partyId.Text;
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_Log_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_Log_DataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox partyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        ASPxGridView grd = sender as ASPxGridView;
        this.dsPartyLog.FilterExpression = "PartyId='" + SafeValue.SafeString(partyId.Text, "-1") + "'";
    }
    #endregion

    #region photo
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhAttachment));
        }
    }
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;

        this.dsAttachment.FilterExpression = "RefNo='" + SafeValue.SafeString(txtPartyId.Text, "") + "'";
    }
    protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Photo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
    }
    protected void grd_Photo_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["FileNote"] = " ";
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }

    #endregion
    #region Address
    protected void grid_address_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.RefAddress));
        }
    }
    protected void grid_address_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        ASPxTextBox txtName = pageControl.FindControl("txtName") as ASPxTextBox;
        e.NewValues["Address"] = "";
        e.NewValues["Address1"] = "";
        e.NewValues["PartyId"] = txtPartyId.Text;
        e.NewValues["PartyName"] = txtName.Text;
    }
    protected void grid_address_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        ASPxTextBox txtName = pageControl.FindControl("txtName") as ASPxTextBox;
        e.NewValues["PartyId"] = txtPartyId.Text;
        e.NewValues["PartyName"] = txtName.Text;
    }
    protected void grid_address_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Address"] = SafeValue.SafeString(e.NewValues["Address"]);
        e.NewValues["Address1"] = SafeValue.SafeString(e.NewValues["Address1"]);
        e.NewValues["Location"] = SafeValue.SafeString(e.NewValues["Location"]);
        e.NewValues["TypeId"] = SafeValue.SafeString(e.NewValues["TypeId"]);
        e.NewValues["PartyId"] = SafeValue.SafeString(e.NewValues["PartyId"]);
        e.NewValues["PartyName"] = SafeValue.SafeString(e.NewValues["PartyName"]);
        e.NewValues["Postcode"] = SafeValue.SafeString(e.NewValues["Postcode"]);
    }
    protected void grid_address_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_address_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        this.dsAddress.FilterExpression = "PartyId='" + SafeValue.SafeString(txtPartyId.Text, "") + "' and len(PartyId)>0";
    }
    #endregion

    #region Contact
    protected void grid_contact_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.RefContact));
        }
    }
    protected void grid_contact_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        ASPxTextBox txtName = pageControl.FindControl("txtName") as ASPxTextBox;
        e.NewValues["Address"] = "";
        e.NewValues["PartyId"] = txtPartyId.Text;
        e.NewValues["PartyName"] = txtName.Text;
    }
    protected void grid_contact_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        ASPxTextBox txtName = pageControl.FindControl("txtName") as ASPxTextBox;
        e.NewValues["PartyId"] = txtPartyId.Text;
        e.NewValues["PartyName"] = txtName.Text;
        e.NewValues["IsDefault"] = SafeValue.SafeBool(e.NewValues["IsDefault"], true);

    }
    protected void grid_contact_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        ASPxTextBox txtName = pageControl.FindControl("txtName") as ASPxTextBox;
        e.NewValues["PartyId"] = txtPartyId.Text;
        e.NewValues["PartyName"] = txtName.Text;
        e.NewValues["IsDefault"] = SafeValue.SafeBool(e.NewValues["IsDefault"], true);
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"]);
        e.NewValues["Tel"] = SafeValue.SafeString(e.NewValues["Tel"]);
        e.NewValues["Fax"] = SafeValue.SafeString(e.NewValues["Fax"]);
        e.NewValues["Address"] = SafeValue.SafeString(e.NewValues["Address"]);
        e.NewValues["Email"] = SafeValue.SafeString(e.NewValues["Email"]);
        e.NewValues["Mobile"] = SafeValue.SafeString(e.NewValues["Mobile"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
    }
    protected void grid_contact_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_contact_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        this.dsContact.FilterExpression = "PartyId='" + SafeValue.SafeString(txtPartyId.Text, "") + "' and len(PartyId)>0";
    }
    public string IsDefault(bool isDefault)
    {
        string res = "NO";
        if (isDefault)
            res = "YES";
        return res;
    }
    protected void grid_contact_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        string partyId = SafeValue.SafeString(e.NewValues["PartyId"]);
        string id = SafeValue.SafeString(e.Keys["Id"]);
        bool isDefault = SafeValue.SafeBool(e.NewValues["IsDefault"], true);
        if (isDefault == true)
        {
            string sql = string.Format(@"update ref_contact set IsDefault=0 where PartyId='{0}' and Id!={1}", partyId, id);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
        else
        {
            string sql = string.Format(@"update ref_contact set IsDefault=1 where PartyId='{0}' and Id=(select top 1 Id from ref_contact where PartyId='{0}')", partyId);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
    }
    protected void grid_contact_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string partyId = SafeValue.SafeString(e.NewValues["PartyId"]);
        string sql = string.Format(@"select top 1 Id from ref_contact where PartyId='{0}' order by Id desc", partyId);
        string id = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql), "0");
        bool isDefault = SafeValue.SafeBool(e.NewValues["IsDefault"], true);
        if (isDefault == true)
        {
            sql = string.Format(@"update ref_contact set IsDefault=0 where PartyId='{0}' and Id!={1}", partyId, id);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
        else
        {
            sql = string.Format(@"update ref_contact set IsDefault=1 where PartyId='{0}' and Id=(select top 1 Id from ref_contact where PartyId='{0}')", partyId);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
    }
    #endregion
}
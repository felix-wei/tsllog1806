using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
public partial class PagesMaster_BankEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string partyType = SafeValue.SafeString(Request.QueryString["type"]);
            this.txt_Type.Text = partyType;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string partyType = SafeValue.SafeString(Request.QueryString["type"]);
        string where = "";
        if (!IsPostBack)
        {
            where = "GroupId='BANK'";
            this.dsXXParty.FilterExpression = where;
            Session["NameWhere"] = where;
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
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string partyType = SafeValue.SafeString(Request.QueryString["type"]);
        string name = this.txt_Name.Text.Trim().ToUpper();
        string where = "";
        if (name.Length > 0)
        {
            where = string.Format("NAME LIKE '{0}%'", name.Replace("'", "''"));
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
        else
        {
            where = "1=1";
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
            ASPxButton btn_Block = this.grid.FindEditFormTemplateControl("btn_Block") as ASPxButton;
            ASPxLabel status = pageControl.FindControl("lblStatus") as ASPxLabel;
            if (partyId.Length > 0)
            {
                sql = string.Format("select Status from XXParty  where PartyId='{0}'", partyId);
                string closeInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));// closeIndStr.Text
                if (closeInd == "InActive")
                {
                    status.Text = "InActive";
                    btn_Block.Text = "UnLock";
                }
                else
                {
                    status.Text = "USE";
                    btn_Block.Text = "Lock";
                }
            }
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
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhAttachment));
        }
    }
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_SequenceId = pageControl.FindControl("txt_SequenceId") as ASPxTextBox;

        this.dsAttachment.FilterExpression = "RefNo='" + SafeValue.SafeString(txt_SequenceId.Text, "") + "'";
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

    protected void cmb_Status_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb_Status = grid.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
        ASPxTextBox txt_LcNo = grid.FindEditFormTemplateControl("txt_LcNo") as ASPxTextBox;
        string doNo = SafeValue.SafeString(txt_LcNo.Text);
        string sql = string.Format(@"select StatusCode from Wh_Trans where LcNo='{0}'", doNo);
        string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
        if (status == "Confirmed")
        {
            cmb_Status.Text = "Confirmed";
        }
        if (status == "Draft")
        {
            cmb_Status.Text = "Draft";
        }
    }

    #region Note
    protected void grid_Note_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;

        dsLcActivity.FilterExpression = "RefNo='"+txtPartyId.Text+"'";
    }
    protected void grid_Note_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }
    protected void grid_Note_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.LcActivity));
        }
    }
    protected void grid_Note_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CreateDateTime"] = DateTime.Now;

    }
    protected void grid_Note_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        e.NewValues["RefNo"] = SafeValue.SafeString(txtPartyId.Text);
        e.NewValues["CreateDateTime"] = SafeValue.SafeDate(e.NewValues["CreateDateTime"], DateTime.Now);
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["ActionNote"] = SafeValue.SafeString(e.NewValues["ActionNote"]);
        e.NewValues["InfoNote"] = SafeValue.SafeString(e.NewValues["InfoNote"]);
        e.NewValues["Status"] = SafeValue.SafeString(e.NewValues["Status"]);
        e.NewValues["RefType"] = SafeValue.SafeString(e.NewValues["RefType"]);
    }
    protected void grid_Note_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txtPartyId = pageControl.FindControl("txtPartyId") as ASPxTextBox;
        e.NewValues["RefNo"] = SafeValue.SafeString(txtPartyId.Text);
        e.NewValues["CreateDateTime"] = SafeValue.SafeDate(e.NewValues["CreateDateTime"], DateTime.Now);
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["ActionNote"] = SafeValue.SafeString(e.NewValues["ActionNote"]);
        e.NewValues["InfoNote"] = SafeValue.SafeString(e.NewValues["InfoNote"]);
        e.NewValues["Status"] = SafeValue.SafeString(e.NewValues["Status"]);
        e.NewValues["RefType"] = SafeValue.SafeString(e.NewValues["RefType"]);
    }
    #endregion
}
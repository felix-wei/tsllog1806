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

public partial class PagesMaster_LCEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["RefLC"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"] != "0")
            {
                Session["RefLC"] = "LcNo='" + Request.QueryString["no"] + "'";
                this.dsLc.FilterExpression = Session["RefLC"].ToString();
                this.txt_SchRefNo.Text = Request.QueryString["no"];
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"] == "0")
            {
                this.grid.AddNewRow();
            }
            else
                this.dsLc.FilterExpression = "1=0";

        }
        if (Session["RefLC"] != null)
        {
            this.dsLc.FilterExpression = Session["RefLC"].ToString();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["LcNo"] = "NEW";
        e.NewValues["StatusCode"] = "";
        e.NewValues["LcAppDate"] = DateTime.Now;
        e.NewValues["LcCurrency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["LcExRate"] = 1.000000;
        e.NewValues["LcType"] = "IMPORT LC";
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.RefLc));
        }
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if(s=="Save"){
           e.Result= SaveLc();
           ASPxComboBox cmb_Type = grid.FindEditFormTemplateControl("cmb_Type") as ASPxComboBox;
           ASPxTextBox txt_LcNo = this.grid.FindEditFormTemplateControl("txt_LcNo") as ASPxTextBox;
           string type = SafeValue.SafeString(cmb_Type.Text);
           bool isAddLog = false;
           if (type == "Confirmed")
           {
               ASPxButtonEdit btn_BankCode = grid.FindEditFormTemplateControl("btn_BankCode") as ASPxButtonEdit;
               string bankCode = SafeValue.SafeString(btn_BankCode.Text);
               string sql=string.Format(@"select LoanFund from Lc_BankFunds where BankCode='{0}'",bankCode);
               decimal loanFund=SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql));
               ASPxSpinEdit spin_LcAmount = grid.FindEditFormTemplateControl("spin_LcAmount") as ASPxSpinEdit;
               decimal amount=SafeValue.SafeDecimal(spin_LcAmount.Text);
               sql = string.Format(@"select sum(LoanFund) from Ref_LC where BankCode='{0}'",bankCode);
               decimal totalAmount = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql));
               decimal space=SafeValue.SafeDecimal((loanFund-(amount + totalAmount)));
               if (space<0)
               {
                   e.Result = "No Enough Amount";
               }

           }
           if (cmb_Type.Text == SafeValue.SafeString(ConnectSql.ExecuteScalar("Select RefType from LcActivity where RefNo='" + txt_LcNo.Text + "'")))
           {
           }
           else
           {
               isAddLog = true;
           }
           if (isAddLog)
           {

               if (type == "IMPORT LC")
               {
                   EzshipLog.Activity(txt_LcNo.Text, "", "IMPORT LC", "RECEIVING");
               }
               if (type == "EXPORT LC")
               {
                   EzshipLog.Activity(txt_LcNo.Text, "", "EXPORT LC", "ISSUING");
               }
               if (type == "STANDBY LC")
               {
                   EzshipLog.Activity(txt_LcNo.Text, "", "EXPORT LC", "COLLECTION");
               }
               if (type == "BANK GUARANTEE")
               {
                   EzshipLog.Activity(txt_LcNo.Text, "", "BANK GUARANTEE", "REMARK");
               }
               if (type == "SHIPPING GUARANTEE")
               {
                   EzshipLog.Activity(txt_LcNo.Text, "", "SHIPPING GUARANTEE", "REMARK");
               }
               if (type == "OTHERS")
               {
                   EzshipLog.Activity(txt_LcNo.Text, "", "OTHERS", "OTHERS");
               }
           }
        }
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
    }
    protected string SaveLc()
    {
        try
        {
            ASPxTextBox txt_LcNo = grid.FindEditFormTemplateControl("txt_LcNo") as ASPxTextBox;
            ASPxTextBox txt_Id = grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            string id = SafeValue.SafeString(txt_Id.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(RefLc), "Id='" + id + "'");
            RefLc lc = C2.Manager.ORManager.GetObject(query) as RefLc;
            ASPxDateEdit date_AppDate = grid.FindEditFormTemplateControl("date_AppDate") as ASPxDateEdit;
            bool isNew = false;
            //const string runType = "DOOUT";
            string lcNo = "";
            if (lc == null)
            {
                lc = new RefLc();
                isNew = true;
                lcNo = C2Setup.GetNextNo("", "LetterOfCredit", date_AppDate.Date);
                lc.LcAppDate = date_AppDate.Date;
            }
            ASPxComboBox cmb_Type = grid.FindEditFormTemplateControl("cmb_Type") as ASPxComboBox;
            lc.LcType = cmb_Type.Text;

            ASPxDateEdit date_LcExpirtDate = grid.FindEditFormTemplateControl("date_LcExpirtDate") as ASPxDateEdit;
            lc.LcExpirtDate = date_LcExpirtDate.Date;
            //Main Info
            ASPxTextBox txt_TempNo = grid.FindEditFormTemplateControl("txt_TempNo") as ASPxTextBox;
            lc.LcTempNo = txt_TempNo.Text;
            ASPxTextBox txt_LcRefNo = grid.FindEditFormTemplateControl("txt_LcRefNo") as ASPxTextBox;
            lc.LcRef = txt_LcRefNo.Text;
            ASPxTextBox txt_LcExpirtPlace = grid.FindEditFormTemplateControl("txt_LcExpirtPlace") as ASPxTextBox;
            lc.LcExpirtPlace = txt_LcExpirtPlace.Text;

            ASPxButtonEdit txt_EntityCode = grid.FindEditFormTemplateControl("txt_EntityCode") as ASPxButtonEdit;
            lc.LcEntityCode = txt_EntityCode.Text;
            ASPxTextBox txt_EntityName = grid.FindEditFormTemplateControl("txt_EntityName") as ASPxTextBox;
            lc.LcEntityName = txt_EntityName.Text;
            ASPxMemo memo_EntityAddress = grid.FindEditFormTemplateControl("memo_EntityAddress") as ASPxMemo;
            lc.LcEntityAddress = memo_EntityAddress.Text;

            ASPxButtonEdit btn_BeneCode = grid.FindEditFormTemplateControl("btn_BeneCode") as ASPxButtonEdit;
            lc.LcBeneCode = btn_BeneCode.Text;
            ASPxTextBox txt_LcBeneName = grid.FindEditFormTemplateControl("txt_LcBeneName") as ASPxTextBox;
            lc.LcBeneName = txt_LcBeneName.Text;
            ASPxMemo memo_BeneAddress = grid.FindEditFormTemplateControl("memo_BeneAddress") as ASPxMemo;
            lc.LcBeneAddress = memo_BeneAddress.Text;

            ASPxButtonEdit btn_BankCode = grid.FindEditFormTemplateControl("btn_BankCode") as ASPxButtonEdit;
            lc.BankCode = btn_BankCode.Text;
            ASPxTextBox txt_BankName = grid.FindEditFormTemplateControl("txt_BankName") as ASPxTextBox;
            lc.BankName = txt_BankName.Text;
            ASPxMemo memo_BankAddress = grid.FindEditFormTemplateControl("memo_BankAddress") as ASPxMemo;
            lc.BankAddress = memo_BankAddress.Text;
            ASPxTextBox txt_Branch = grid.FindEditFormTemplateControl("txt_Branch") as ASPxTextBox;
            lc.BankBranch = txt_Branch.Text;
            ASPxTextBox txt_BankRef = grid.FindEditFormTemplateControl("txt_BankRef") as ASPxTextBox;
            lc.BankRef = txt_BankRef.Text;
            ASPxSpinEdit spin_BankTenor = grid.FindEditFormTemplateControl("spin_BankTenor") as ASPxSpinEdit;
            lc.BankTenor = SafeValue.SafeInt(spin_BankTenor.Text, 0);

            ASPxSpinEdit spin_LcAmount = grid.FindEditFormTemplateControl("spin_LcAmount") as ASPxSpinEdit;
            lc.LcAmount= SafeValue.SafeDecimal(spin_LcAmount.Value, 1);
            ASPxButtonEdit currency = grid.FindEditFormTemplateControl("txt_Currency") as ASPxButtonEdit;
            lc.LcCurrency = currency.Text;
            ASPxSpinEdit exRate = grid.FindEditFormTemplateControl("spin_ExRate") as ASPxSpinEdit;
            lc.LcExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            ASPxTextBox txt_LcMode = grid.FindEditFormTemplateControl("txt_LcMode") as ASPxTextBox;
            lc.LcMode = txt_LcMode.Text;
            ASPxComboBox cmb_Status = grid.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            lc.StatusCode = cmb_Status.Text;
            string userId = HttpContext.Current.User.Identity.Name;
            if (isNew)
            {
                lc.CreateBy = userId;
                lc.CreateDateTime = DateTime.Now;
                lc.LcNo = lcNo;
                Manager.ORManager.StartTracking(lc, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(lc);
                C2Setup.SetNextNo("", "LetterOfCredit", lcNo, date_AppDate.Date);
            }
            else
            {
                lc.UpdateBy = userId;
                lc.UpdateDateTime = DateTime.Now;
                bool isAddLog = false;
                if (lc.StatusCode == SafeValue.SafeString(ConnectSql.ExecuteScalar("Select StatusCode from Ref_LC where LcNo='" + lc.LcNo + "'")))
                {
                }
                else
                {
                    isAddLog = true;
                }
                Manager.ORManager.StartTracking(lc, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(lc);
                if (isAddLog)
                    EzshipLog.Log(lc.LcNo, "", lc.LcType, lc.StatusCode);
            }
            Session["RefLC"] = "LcNo='" + lc.LcNo + "'";
            this.dsLc.FilterExpression = Session["RefLC"].ToString();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);

            return lc.LcNo;
        }
        catch { }
        return "";
    }

    #region Activity
    protected void grid_Activity_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox refN = this.grid.FindEditFormTemplateControl("txt_LcNo") as ASPxTextBox;
        ASPxGridView grid_Ac = (sender as ASPxGridView) as ASPxGridView;
        string sql = string.Format(@"select *,dbo.fun_GetPartyName(PartyId) as PartyName,(select SUM(Qty1) from Wh_TransDet det where det.DoNo=d.DoNo) as TotalQty, 
(select SUM(DocAmt) from Wh_TransDet det where det.DoNo=d.DoNo) as TotalAmt from Wh_Trans d where LcNo='{0}' and DoStatus='Confirmed'", refN.Text);
        DataTable tab = ConnectSql.GetTab(sql);       
       grid_Ac.DataSource = tab;
    }
    #endregion
    #region log
    protected void grid_Log_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox txt_LcNo = this.grid.FindEditFormTemplateControl("txt_LcNo") as ASPxTextBox;
        this.dsLog.FilterExpression = String.Format("RefNo='{0}'", txt_LcNo.Text);//
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
        ASPxTextBox txtRefNo = this.grid.FindEditFormTemplateControl("txt_LcNo") as ASPxTextBox;
        this.dsAttachment.FilterExpression = "RefNo='" + SafeValue.SafeString(txtRefNo.Text, "") + "'";
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

}
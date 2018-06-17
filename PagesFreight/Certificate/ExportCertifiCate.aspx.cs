using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Wilson.ORMapper;
public partial class PagesFreight_Account_ExportCertifiCate : System.Web.UI.Page
{
    protected void page_Init(object sender, EventArgs e)
    {
        // this.txt_refNo.Text = "280674";
        if (!IsPostBack)
        {
            //this.txtSchNo.Focus();
            this.form1.Focus();
            Session["ExportCertificate"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                Session["ExportCertificate"] = "Id=" + Request.QueryString["no"] + "";
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
            {
                if (Session["ExportCertificate"] == null)
                {
                    this.ASPxGridView1.AddNewRow();
                }
            }
            else
                this.dsCertificate.FilterExpression = "1=0";
        }
        if (Session["ExportCertificate"] != null)
        {
            this.dsCertificate.FilterExpression = Session["ExportCertificate"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region Certificate
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.SeaCertificate));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["GstPermitNo"] = "";
        e.NewValues["GstPaidAmt"] = 0;
        e.NewValues["HandingAgent"] = "";
        e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = 1;
        e.NewValues["CerDate"] = DateTime.Today;

        if (Request.QueryString["JobType"] != null && Request.QueryString["RefN"] != null && Request.QueryString["JobN"] != null)
        {
            e.NewValues["RefType"] = Request.QueryString["JobType"].ToString();
            e.NewValues["RefNo"] = Request.QueryString["RefN"].ToString();
            string houseNo = Request.QueryString["JobN"].ToString();
            e.NewValues["JobNo"] = Request.QueryString["JobN"].ToString();
            //if (houseNo.Length > 1)
            //{
            //    string sql = "SELECT AgentId FROM  SeaExportRef WHERE (RefNo= '" + houseNo + "')";
            //    e.NewValues["HandingAgent"] = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql), "");
            //}
        }
        e.NewValues["CerDate"] = DateTime.Now;
    }
    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        SaveAndUpdate();
    }
    private void SaveAndUpdate()
    {
        ASPxTextBox cerId = this.ASPxGridView1.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        ASPxComboBox cmb_PartyTo = this.ASPxGridView1.FindEditFormTemplateControl("cmb_PartyTo") as ASPxComboBox;
        ASPxDateEdit txt_CerDt = this.ASPxGridView1.FindEditFormTemplateControl("txt_CerDt") as ASPxDateEdit;
        ASPxTextBox txt_GstPermitNo = this.ASPxGridView1.FindEditFormTemplateControl("txt_GstPermitNo") as ASPxTextBox;
        ASPxSpinEdit spin_GstPaidAmt = this.ASPxGridView1.FindEditFormTemplateControl("spin_GstPaidAmt") as ASPxSpinEdit;
        ASPxTextBox serialNo = this.ASPxGridView1.FindEditFormTemplateControl("txt_SerialNo") as ASPxTextBox;
        ASPxTextBox mastRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastRefNo") as ASPxTextBox;
        ASPxTextBox jobRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_JobRefNo") as ASPxTextBox;
        ASPxTextBox jobType = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastType") as ASPxTextBox;
        ASPxButtonEdit btn_RefCurrency = this.ASPxGridView1.FindEditFormTemplateControl("btn_RefCurrency") as ASPxButtonEdit;
        ASPxSpinEdit txt_RefExRate = this.ASPxGridView1.FindEditFormTemplateControl("txt_RefExRate") as ASPxSpinEdit;
        C2.SeaCertificate cer = Manager.ORManager.GetObject(typeof(SeaCertificate), SafeValue.SafeInt(cerId.Text, 0)) as SeaCertificate;

        if (cer == null)// first insert invoice
        {
            cer = new SeaCertificate();
            cer.GstPermitNo = txt_GstPermitNo.Text;
            cer.GstPaidAmt = SafeValue.SafeDecimal(spin_GstPaidAmt.Value);
            cer.HandingAgent = SafeValue.SafeString(cmb_PartyTo.Value);
            cer.CerDate = txt_CerDt.Date;
            cer.RefNo = mastRefNCtr.Text;
            cer.JobNo = jobRefNCtr.Text;
            cer.RefType = jobType.Text;
            cer.SerialNo = serialNo.Text;
            cer.Currency = btn_RefCurrency.Text;
            cer.ExRate = SafeValue.SafeDecimal(txt_RefExRate.Value);
            try
            {
                C2.Manager.ORManager.StartTracking(cer, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(cer);
            }
            catch
            {
            }
        }
        else
        {
            cer.GstPermitNo = txt_GstPermitNo.Text;
            cer.GstPaidAmt = SafeValue.SafeDecimal(spin_GstPaidAmt.Value);
            cer.HandingAgent = SafeValue.SafeString(cmb_PartyTo.Value);
            cer.CerDate = txt_CerDt.Date;
            cer.RefNo = mastRefNCtr.Text;
            cer.JobNo = jobRefNCtr.Text;
            cer.RefType = jobType.Text;
            cer.SerialNo = serialNo.Text;
            cer.Currency = btn_RefCurrency.Text;
            cer.ExRate = SafeValue.SafeDecimal(txt_RefExRate.Value);
            try
            {
                Manager.ORManager.StartTracking(cer, InitialState.Updated);
                Manager.ORManager.PersistChanges(cer);
            }
            catch
            {
            }

        }
        Session["ExportCertificate"] = "Id=" + cer.Id;
        this.dsCertificate.FilterExpression = Session["ExportCertificate"].ToString();
        if (this.ASPxGridView1.GetRow(0) != null)
            this.ASPxGridView1.StartEdit(0);
    }
    #endregion

    #region Certificate det
    protected void grid_CerDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.SeaCertificateDet));
    }
    protected void grid_CerDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        this.dsCertificateDet.FilterExpression = "CerNo='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
    }
    protected void grid_CerDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Qty"] = 0;
        e.NewValues["PackageType"] = "";
        e.NewValues["Amt"] = 0;
        e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = 1;
    }
    protected void grid_CerDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox mastRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastRefNo") as ASPxTextBox;
        ASPxTextBox jobRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_JobRefNo") as ASPxTextBox;
        ASPxTextBox mastType = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastType") as ASPxTextBox;
        ASPxTextBox cerId = this.ASPxGridView1.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["RefType"] = mastType.Text;
        e.NewValues["RefNo"] = mastRefNCtr.Text;
        e.NewValues["JobNo"] = jobRefNCtr.Text;
        e.NewValues["CerNo"] = cerId.Text;
    }
    protected void grid_CerDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
    }
    protected void grid_CerDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion
}
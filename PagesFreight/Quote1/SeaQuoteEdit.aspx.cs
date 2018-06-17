using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesFreight_Quote1_SeaQuoteEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            Session["SeaQuoteEditWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                this.txtSchNo.Text = Request.QueryString["no"].ToString();
                Session["SeaQuoteEditWhere"] = "QuoteNo='" + SafeValue.SafeString(Request.QueryString["no"], "") + "'";
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
            {
                if (Session["SeaQuoteEditWhere"] == null)
                {
                    this.ASPxGridView1.AddNewRow();
                }
            }
            else
                this.dsSeaQuotation.FilterExpression = "1=0";

        }
        if (Session["SeaQuoteEditWhere"] != null)
        {
            this.dsSeaQuotation.FilterExpression = Session["SeaQuoteEditWhere"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
        }
    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.SeaQuotation));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["QuoteDate"] = DateTime.Today;
        e.NewValues["ExpireDate"] = DateTime.Today.AddMonths(1);
        e.NewValues["CurrencyId"] = "USD";
        e.NewValues["Pol"] = "SGSIN";
        e.NewValues["ExRate"] = 1;
        e.NewValues["CreateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDate"] = DateTime.Today;
        e.NewValues["QuoteType"] = "LCL";
    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        ASPxTextBox invNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        ASPxDateEdit ExpireDate = this.ASPxGridView1.FindEditFormTemplateControl("txt_ToDt") as ASPxDateEdit;
        ASPxDateEdit QuoteDate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocDt") as ASPxDateEdit;
        ASPxComboBox QuoteType = this.ASPxGridView1.FindEditFormTemplateControl("cbx_QuoteType") as ASPxComboBox;
        ASPxTextBox PartyTo = this.ASPxGridView1.FindEditFormTemplateControl("txt_CustId") as ASPxTextBox;
        ASPxTextBox PartyName = this.ASPxGridView1.FindEditFormTemplateControl("txt_CustName") as ASPxTextBox;
        ASPxTextBox Tel = this.ASPxGridView1.FindEditFormTemplateControl("txt_Tel") as ASPxTextBox;
        ASPxTextBox Fax = this.ASPxGridView1.FindEditFormTemplateControl("txt_Fax") as ASPxTextBox;
        ASPxTextBox Contact = this.ASPxGridView1.FindEditFormTemplateControl("txt_Contact") as ASPxTextBox;

        ASPxTextBox Pol = this.ASPxGridView1.FindEditFormTemplateControl("txt_Pol") as ASPxTextBox;
        ASPxTextBox Pod = this.ASPxGridView1.FindEditFormTemplateControl("txt_Pod") as ASPxTextBox;
        ASPxTextBox FinDest = this.ASPxGridView1.FindEditFormTemplateControl("txt_FinDest") as ASPxTextBox;
        ASPxTextBox Vessel = this.ASPxGridView1.FindEditFormTemplateControl("txt_Vessel") as ASPxTextBox;
        ASPxTextBox Voyage = this.ASPxGridView1.FindEditFormTemplateControl("txt_Voyage") as ASPxTextBox;
        ASPxDateEdit Eta = this.ASPxGridView1.FindEditFormTemplateControl("txt_Eta") as ASPxDateEdit;
        ASPxDateEdit Etd = this.ASPxGridView1.FindEditFormTemplateControl("txt_Etd") as ASPxDateEdit;
        ASPxDateEdit EtaDest = this.ASPxGridView1.FindEditFormTemplateControl("txt_EtaDest") as ASPxDateEdit;

        ASPxTextBox CurrencyId = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxTextBox;
        ASPxSpinEdit ExRate = this.ASPxGridView1.FindEditFormTemplateControl("spin_exRate") as ASPxSpinEdit;
        ASPxButtonEdit SalesmanId = this.ASPxGridView1.FindEditFormTemplateControl("txt_SalesmanId") as ASPxButtonEdit;
        ASPxTextBox Subject = this.ASPxGridView1.FindEditFormTemplateControl("txt_Subject") as ASPxTextBox;
        ASPxMemo Rmk = this.ASPxGridView1.FindEditFormTemplateControl("txt_Remarks1") as ASPxMemo;
        ASPxMemo Note1 = this.ASPxGridView1.FindEditFormTemplateControl("txt_Note1") as ASPxMemo;
        ASPxMemo Note2 = this.ASPxGridView1.FindEditFormTemplateControl("txt_Note2") as ASPxMemo;
        ASPxMemo Note3 = this.ASPxGridView1.FindEditFormTemplateControl("txt_Note3") as ASPxMemo;
        ASPxMemo Note4 = this.ASPxGridView1.FindEditFormTemplateControl("txt_Note4") as ASPxMemo;

        string invN = docN.Text;
        C2.SeaQuotation inv = C2.Manager.ORManager.GetObject(typeof(C2.SeaQuotation), SafeValue.SafeInt(invNCtr.Text, 0)) as C2.SeaQuotation;
        if (inv == null)
        {
            string counterType = "SeaFcl-Quotation";

            inv = new C2.SeaQuotation();
            invN = C2Setup.GetNextNo(counterType);
            inv.QuoteNo = invN;
            inv.ExpireDate = ExpireDate.Date;
            inv.QuoteDate = QuoteDate.Date;
            inv.QuoteType = QuoteType.Text;
            inv.PartyTo = PartyTo.Text;
            inv.PartyName = PartyName.Text;
            inv.Tel = Tel.Text;
            inv.Fax = Fax.Text;
            inv.Contact = Contact.Text;

            inv.Pol = Pol.Text;
            inv.Pod = Pod.Text;
            inv.FinDest = FinDest.Text;
            inv.Vessel = Vessel.Text;
            inv.Voyage = Voyage.Text;
            inv.Eta = Eta.Date;
            inv.Etd = Etd.Date;
            inv.EtaDest = EtaDest.Date;

            inv.CurrencyId = CurrencyId.Text;
            inv.ExRate = SafeValue.SafeDecimal(ExRate.Value, 1);
            inv.SalesmanId = SalesmanId.Text;
            inv.Subject = Subject.Text;
            inv.Rmk = Rmk.Text;
            inv.Note1 = Note1.Text;
            inv.Note2 = Note2.Text;
            inv.Note3 = Note3.Text;
            inv.Note4 = Note4.Text;

            inv.CreateUser = HttpContext.Current.User.Identity.Name;
            inv.CreateDate = DateTime.Now;
            inv.UpdateUser = HttpContext.Current.User.Identity.Name;
            inv.UpdateDate = DateTime.Now;
            try
            {
                C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(inv);
                C2Setup.SetNextNo(counterType, inv.QuoteNo);
            }
            catch
            {
            }
        }
        else
        {
            inv.ExpireDate = ExpireDate.Date;
            inv.QuoteDate = QuoteDate.Date;
            inv.QuoteType = QuoteType.Text;
            inv.PartyTo = PartyTo.Text;
            inv.PartyName = PartyName.Text;
            inv.Tel = Tel.Text;
            inv.Fax = Fax.Text;
            inv.Contact = Contact.Text;

            inv.Pol = Pol.Text;
            inv.Pod = Pod.Text;
            inv.FinDest = FinDest.Text;
            inv.Vessel = Vessel.Text;
            inv.Voyage = Voyage.Text;
            inv.Eta = Eta.Date;
            inv.Etd = Etd.Date;
            inv.EtaDest = EtaDest.Date;

            inv.CurrencyId = CurrencyId.Text;
            inv.ExRate = SafeValue.SafeDecimal(ExRate.Value, 1);
            inv.SalesmanId = SalesmanId.Text;
            inv.Subject = Subject.Text;
            inv.Rmk = Rmk.Text;
            inv.Note1 = Note1.Text;
            inv.Note2 = Note2.Text;
            inv.Note3 = Note3.Text;
            inv.Note4 = Note4.Text;

            inv.UpdateUser = HttpContext.Current.User.Identity.Name;
            inv.UpdateDate = DateTime.Now;
            try
            {
                C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(inv);
            }
            catch
            {
            }
        }
        Session["SeaQuoteEditWhere"] = "Id=" + inv.Id;
        this.dsSeaQuotation.FilterExpression = Session["SeaQuoteEditWhere"].ToString();
        if (this.ASPxGridView1.GetRow(0) != null)
            this.ASPxGridView1.StartEdit(0);
    }


    #region Costing
    protected void grid_Cost_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select QuoteNo from SeaQuotation where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsSeaQuotationDet1.FilterExpression = "QuoteNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void grid_Cost_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.SeaQuotationDet1));
        }
    }
    protected void grid_Cost_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["SaleQty"] = 1;
        e.NewValues["CostQty"] = 1;
        e.NewValues["SalePrice"] = 0;
        e.NewValues["CostPrice"] = 0;
        e.NewValues["SaleLocAmt"] = 0;
        e.NewValues["CostLocAmt"] = 0;
        e.NewValues["ChgCode"] = " ";
        e.NewValues["ChgCodeDes"] = " ";
        e.NewValues["Remark"] = " ";
        e.NewValues["SaleCurrency"] = "SGD";
        e.NewValues["SaleExRate"] = 1;
        e.NewValues["CostCurrency"] = "SGD";
        e.NewValues["CostExRate"] = 1;

    }
    protected void grid_Cost_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox sequenceID = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;

        ASPxTextBox refNo = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        e.NewValues["QuoteNo"] = refNo.Text;
        e.NewValues["SaleDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
        e.NewValues["SaleLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["SaleExRate"], 0), 2);

        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_Cost_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxTextBox refNo = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        UpdateEstAmt(refNo.Text);
    }
    protected void grid_Cost_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["SaleDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleQty"], 0) * SafeValue.SafeDecimal(e.NewValues["SalePrice"], 0), 2);
        e.NewValues["SaleLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["SaleDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["SaleExRate"], 0), 2);

        e.NewValues["CostDocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostQty"], 0) * SafeValue.SafeDecimal(e.NewValues["CostPrice"], 0), 2);
        e.NewValues["CostLocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CostDocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["CostExRate"], 0), 2);
    }
    protected void grid_Cost_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Cost_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxTextBox refNo = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        UpdateEstAmt(refNo.Text);
    }
    protected void grid_Cost_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        
    }
    private void UpdateEstAmt(string refNo)
    {
        //string sql = string.Format("Update SeaImportRef set EstSaleAmt=(select sum(SaleLocAmt) from SeaCosting where JobType='SI' and RefNo=SeaImportRef.refNo),EstCostAmt=(select sum(CostLocAmt) from SeaCosting where JobType='SI' and RefNo=SeaImportRef.refNo) where RefNo='{0}'", refNo);
        //ConnectSql.ExecuteSql(sql);
    }
    #endregion


    protected void grid_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {

    }
    protected void grid_Det2_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        if (grd.GetMasterRowKeyValue() != null)
        {
            string sql = "select QuoteNo from SeaQuotation where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
            this.dsSeaQuotationDet2.FilterExpression = "QuoteNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
        }
    }
    protected void grid_Det2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Det2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox refNo = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        e.NewValues["QuoteNo"] = refNo.Text;
    }
    protected void grid_Det2_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
            if(grid!=null){
                grid.ForceDataRowType(typeof(C2.SeaQuotationDet2));
            }
    }
    protected void grid_Det2_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Term"]="";
    }
}
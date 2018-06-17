using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Wilson.ORMapper;

public partial class PagesQuote_FclQuoteEdit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string typ = SafeValue.SafeString(Request.QueryString["typ"], "SI");
        this.txt_type.Text = typ;
        if (!IsPostBack)
        {
            Session["FclQuote_" + this.txt_type.Text] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                this.txtSchNo.Text = Request.QueryString["no"].ToString();
                Session["FclQuote_" + typ] = string.Format("QuoteNo='{0}' and FclLclInd='Fcl' and ImpExpInd='{1}'", SafeValue.SafeString(Request.QueryString["no"], ""), typ);

            }
            else if (SafeValue.SafeString(Request.QueryString["no"], "0") == "0")
            {
                this.ASPxGridView1.AddNewRow();
            }
            else
                this.dsQuotation.FilterExpression = "1=0";
        }
        if (Session["FclQuote_" + this.txt_type.Text] != null)
        {
            this.dsQuotation.FilterExpression = Session["FclQuote_" + this.txt_type.Text].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
        }
    }
    #region invoice
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.SeaQuote1));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Title"] = "FCL Sea Quotation";
        e.NewValues["QuoteDate"] = DateTime.Today;
        e.NewValues["ExpireDate"] = DateTime.Today.AddMonths(1);
        e.NewValues["CurrencyId"] = "USD";
        e.NewValues["Term"] = "CASH";
        e.NewValues["Gp20"] = 950;
        e.NewValues["Gp40"] = 1800;
        e.NewValues["Hc40"] = 1900;
        e.NewValues["Frequency"] = "Weekly";
        if(this.txt_type.Text.IndexOf("SI")>-1)
        e.NewValues["Pod"] = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        else if(this.txt_type.Text.IndexOf("SE")>-1)
            e.NewValues["Pol"] = System.Configuration.ConfigurationManager.AppSettings["LocalPort"];
        e.NewValues["ContType"] = "20GP";
        e.NewValues["ExRate"] = 1 ;
        e.NewValues["ContPrice"] = 0;
        e.NewValues["CreateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDate"] = DateTime.Today;
    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
            ASPxTextBox invNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;

            ASPxTextBox title = this.ASPxGridView1.FindEditFormTemplateControl("txt_Title") as ASPxTextBox;
            ASPxButtonEdit partyTo = this.ASPxGridView1.FindEditFormTemplateControl("txt_CustId") as ASPxButtonEdit;
            ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
            ASPxDateEdit docDate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocDt") as ASPxDateEdit;
            ASPxMemo note = this.ASPxGridView1.FindEditFormTemplateControl("txt_Note") as ASPxMemo;
            ASPxMemo rmk = this.ASPxGridView1.FindEditFormTemplateControl("txt_Remarks1") as ASPxMemo;
            // ASPxComboBox termId = this.ASPxGridView1.FindEditFormTemplateControl("txt_TermId") as ASPxComboBox;
            ASPxDateEdit expireDt = this.ASPxGridView1.FindEditFormTemplateControl("txt_toDt") as ASPxDateEdit;
            ASPxButtonEdit docCurr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxButtonEdit;
            ASPxButtonEdit pol = this.ASPxGridView1.FindEditFormTemplateControl("txt_Pol") as ASPxButtonEdit;
            ASPxButtonEdit pod = this.ASPxGridView1.FindEditFormTemplateControl("txt_Pod") as ASPxButtonEdit;
            ASPxButtonEdit viaPort = this.ASPxGridView1.FindEditFormTemplateControl("txt_ViaPort") as ASPxButtonEdit;


            ASPxTextBox frequency = this.ASPxGridView1.FindEditFormTemplateControl("txt_Frequency") as ASPxTextBox;
            ASPxTextBox attention = this.ASPxGridView1.FindEditFormTemplateControl("txt_Attention") as ASPxTextBox;
            ASPxTextBox tsDay = this.ASPxGridView1.FindEditFormTemplateControl("spin_TsDay") as ASPxTextBox;
            //ASPxSpinEdit gp20 = this.ASPxGridView1.FindEditFormTemplateControl("spin_Gp20") as ASPxSpinEdit;
            // ASPxSpinEdit gp40 = this.ASPxGridView1.FindEditFormTemplateControl("spin_Gp40") as ASPxSpinEdit;
            //ASPxSpinEdit hc40 = this.ASPxGridView1.FindEditFormTemplateControl("spin_Hc40") as ASPxSpinEdit;
            ASPxComboBox contType = this.ASPxGridView1.FindEditFormTemplateControl("cbx_ContType") as ASPxComboBox;
            ASPxSpinEdit contPrice = this.ASPxGridView1.FindEditFormTemplateControl("spin_contPrice") as ASPxSpinEdit;
            ASPxSpinEdit exRate = this.ASPxGridView1.FindEditFormTemplateControl("spin_exRate") as ASPxSpinEdit;

            string invN = docN.Text;
            C2.SeaQuote1 inv = Manager.ORManager.GetObject(typeof(SeaQuote1), SafeValue.SafeInt(invNCtr.Text, 0)) as SeaQuote1;
            if (inv == null)// first insert invoice
            {
                string runType = "SeaFcl-Quotation";

                inv = new SeaQuote1();
                invN = C2Setup.GetNextNo("", runType, docDate.Date);
                inv.Title = title.Text;
                inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");
                inv.QuoteNo = invN.ToString();
                inv.QuoteDate = docDate.Date;
                inv.ImpExpInd = this.txt_type.Text;
                inv.FclLclInd = "Fcl";
                // inv.Term = termId.Text;

                //inv.DocFromDate =SafeValue.SafeDate(fromDt.Text, DateTime.Now);
                inv.ExpireDate = SafeValue.SafeDate(expireDt.Text, DateTime.Now);
                inv.Rmk = rmk.Text;
                inv.Note = note.Text;
                inv.CurrencyId = docCurr.Text.ToString();
                inv.Frequency = frequency.Text;

                inv.Pol = pol.Text;
                inv.Pod = pod.Text;
                inv.ViaPort = viaPort.Text;
                inv.Attention = attention.Text;
                inv.TransmitDay = tsDay.Text;
                inv.Gp20 = 0;// SafeValue.SafeDecimal(gp20.Value, 0);
                inv.Gp40 = 0;//SafeValue.SafeDecimal(gp40.Value, 0);
                inv.Hc40 = 0;//SafeValue.SafeDecimal(hc40.Value, 0);
                inv.ContType = contType.Text;
                inv.ContPrice = SafeValue.SafeDecimal(contPrice.Value, 0);
                inv.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
                inv.CreateUser = HttpContext.Current.User.Identity.Name;
                inv.CreateDate = DateTime.Now;
                inv.UpdateUser = HttpContext.Current.User.Identity.Name;
                inv.UpdateDate = DateTime.Now;
                inv.Status = "USE";
                try
                {
                    C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(inv);
                    C2Setup.SetNextNo("", runType, invN, docDate.Date);
                }
                catch
                {
                }
            }
            else
            {
                inv.Title = title.Text;
                inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");

                inv.ExpireDate = SafeValue.SafeDate(expireDt.Text, DateTime.Now);
                inv.Rmk = rmk.Text;
                inv.Note = note.Text;
                inv.CurrencyId = docCurr.Text.ToString();

                inv.Pol = pol.Text;
                inv.Pod = pod.Text;
                inv.ViaPort = viaPort.Text;
                inv.Attention = attention.Text;
                inv.TransmitDay = tsDay.Text;
                inv.Gp20 = 0;// SafeValue.SafeDecimal(gp20.Value, 0);
                inv.Gp40 = 0;//SafeValue.SafeDecimal(gp40.Value, 0);
                inv.Hc40 = 0;//SafeValue.SafeDecimal(hc40.Value, 0);
                inv.ContType = contType.Text;
                inv.ContPrice = SafeValue.SafeDecimal(contPrice.Value, 0);
                inv.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
                inv.Frequency = frequency.Text;

                inv.UpdateUser = HttpContext.Current.User.Identity.Name;
                inv.UpdateDate = DateTime.Now;
                try
                {
                    Manager.ORManager.StartTracking(inv, InitialState.Updated);
                    Manager.ORManager.PersistChanges(inv);
                }
                catch
                {
                }

            }
            Session["FclQuote_" + this.txt_type.Text] = "SequenceId=" + inv.SequenceId;
            this.dsQuotation.FilterExpression = Session["FclQuote_" + this.txt_type.Text].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
        
    }
    #endregion


    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
      

    }
    protected void ASPxGridView1_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        
    }

    #region invoice det
    protected void grid_InvDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.SeaQuoteDet1));
    }
    protected void grid_InvDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        this.dsQuotationDet.FilterExpression = "QuoteId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
    }
    protected void grid_InvDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["QuoteLineNo"] = 0;
        e.NewValues["Currency"] = "SGD";
        e.NewValues["Price"] = (decimal)0;
        e.NewValues["Qty"] = (decimal)1;
        e.NewValues["Unit"] = "20GP";
        e.NewValues["Rmk"] = " ";
        e.NewValues["GstType"] = "E";
        e.NewValues["ExRate"] = (decimal)1;
    }
    protected void grid_InvDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql_detCnt = "select max(QuoteLineNo) from SeaQuoteDet1 where QuoteId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        int lineNo = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
        e.NewValues["QuoteId"] = SafeValue.SafeString(grd.GetMasterRowKeyValue(), "");
        e.NewValues["QuoteLineNo"] = lineNo;

        e.NewValues["ImpExpInd"] = this.txt_type.Text;
        e.NewValues["FclLclInd"] = "Fcl";
        e.NewValues["Qty"] = (decimal)1;
        if (SafeValue.SafeString(e.NewValues["GstType"], "") == "S")
            e.NewValues["Gst"] = (decimal)0.07;
        else
            e.NewValues["Gst"] = (decimal)0;
        e.NewValues["Amt"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0) * SafeValue.SafeDecimal(e.NewValues["Qty"], 1) * (1 + SafeValue.SafeDecimal(e.NewValues["Gst"], 0));
    }
    protected void grid_InvDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["GstType"], "") == "S")
            e.NewValues["Gst"] = (decimal)0.07;
        else
            e.NewValues["Gst"] = (decimal)0;
        e.NewValues["Amt"] = SafeValue.SafeDecimal(e.NewValues["Price"],0) * SafeValue.SafeDecimal(e.NewValues["Qty"], 1) * (1 + SafeValue.SafeDecimal(e.NewValues["Gst"], 0));
    }
    protected void grid_InvDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    #endregion
}

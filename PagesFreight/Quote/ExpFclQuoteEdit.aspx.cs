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

public partial class PagesFreight_ExpFclQuoteEdit : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
                string userName = HttpContext.Current.User.Identity.Name;
                string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select role from [user] where Name='" + userName + "'"), "").ToUpper();
            Session["SeaQuoteEditWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                this.txtSchNo.Text = Request.QueryString["no"].ToString();
                Session["SeaQuoteEditWhere"] = "QuoteNo='" + SafeValue.SafeString(Request.QueryString["no"], "")+"'";

                if (role == "SALESSTAFF")
                {
                    DataTable tab = C2.Manager.ORManager.GetDataSet(@"SELECT q.QuoteNo FROM SeaQuote1 AS q INNER JOIN XXParty AS c ON q.PartyTo = c.PartyId
WHERE     (c.SalesmanId = '" + userName + "') and QuoteNo='" + SafeValue.SafeString(Request.QueryString["no"], "") + "'").Tables[0];
                    if (tab.Rows.Count > 0)
                    {
                        Session["SeaQuoteEditWhere"] = "QuoteNo='" + SafeValue.SafeString(Request.QueryString["no"], "") + "'";
                    }
                    else
                        Session["SeaQuoteEditWhere"] = "1=0";
                }
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
            {
                if (Session["SeaQuoteEditWhere"] == null)
                {
                    this.ASPxGridView1.AddNewRow();
                }
            }
            else
                this.dsQuotation.FilterExpression = "1=0";
            if (role == "SALESSTAFF")
            {
                this.dsCustomerMast.FilterExpression = "SalesmanId='" + userName + "'";
            }
        }
        if (Session["SeaQuoteEditWhere"] != null)
        {
            this.dsQuotation.FilterExpression = Session["SeaQuoteEditWhere"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
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
        e.NewValues["Pol"] = "SGSIN";
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
            string counterType = "SeaFcl-Quotation";

            inv = new SeaQuote1();
            invN = C2Setup.GetNextNo(counterType);
            inv.Title = title.Text;
            inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");
            inv.QuoteNo = invN.ToString();
            inv.QuoteDate = docDate.Date;
            inv.ImpExpInd = "E";
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
            inv.ContPrice=SafeValue.SafeDecimal(contPrice.Value,0);
            inv.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
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
        Session["SeaQuoteEditWhere"] = "SequenceId=" + inv.SequenceId;
        this.dsQuotation.FilterExpression = Session["SeaQuoteEditWhere"].ToString();
        if (this.ASPxGridView1.GetRow(0) != null)
            this.ASPxGridView1.StartEdit(0);
    }
    #endregion


    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
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
        this.dsQuotationDet.FilterExpression = "QuoteId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
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
    #endregion

    protected void ASPxGridView1_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        
    }
    protected void grid_InvDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
}

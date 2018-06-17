using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.XtraReports.UI;
using System.IO;
using C2;

public partial class Modules_RptPrintView : BasePage
{
    private string docType = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Request.Params["doc"] != null)
        {
            string reportName = Request.Params["doc"].ToString();
            string partyTo = SafeValue.SafeString(Request.Params["p"], "");
            string date1 = SafeValue.SafeString(Request.Params["d1"], "");
            string date2 = SafeValue.SafeString(Request.Params["d2"], "");
            string date3 = SafeValue.SafeString(Request.Params["d3"], "");
            string currency = SafeValue.SafeString(Request.Params["cury"], "I");
           // string printCury = SafeValue.SafeString(Request.Params["prtCury"], "0");
            docType = SafeValue.SafeString(Request.Params["docType"], "0");
            //1.ar receipt
            //2.payment voucher
            userId = HttpContext.Current.User.Identity.Name;
            MemoryStream ms = new MemoryStream();
            string fileName = "";
            try
            {
                // ar
                if (reportName == "1")//customer trans
                {
                    ms = PrintArCustomerTrans(date1, date2, partyTo, currency, userId);
                    fileName = "ARCustomerTrans";
                }
                else if (reportName == "2")//statement 
                {
                    ms = PrintArStatement(date1, date2,partyTo, currency, userId);
                    fileName = "ArStatement";
                }
                else if (reportName == "2d")//statement 
                {
                    ms = PrintArStatementDN(date1, date2,partyTo, currency, userId);
                    fileName = "ArStatementDN";
                }
                else if (reportName == "6")//statement 
                {
                    ms = PrintArStatement_ForeignLocal(date1, date2,partyTo, currency, userId);
                    fileName = "ArStatement";
                }
                else if (reportName == "11")//statement 
                {
                    ms = PrintArStatement_ForeignForeign(date1, date2,partyTo, currency, userId);
                    fileName = "ArStatement";
                }

                else if (reportName == "3")//aging summary 
                {
                    ms = PrintArAgingSummary(date1, date2, currency, userId);
                    fileName = "ArAgingSummary";
                }
                else if (reportName == "7")//aging summary base local currency
                {
                    ms = PrintArAgingSummary_ForeignLocal(date1, date2, currency, userId);
                    fileName = "ArAgingSummary";
                }
                else if (reportName == "9")//aging summary base doc currency
                {
                    ms = PrintArAgingSummary_ForeignForeign(date1, date2, currency, userId);
                    fileName = "ArAgingSummary";
                }


                else if (reportName == "4")//aging detail 
                {
                    ms = PrintArAgingDetail(date1, date2, currency,partyTo, userId);
                    fileName = "ArAgingDetail";
                }
                else if (reportName == "8")//aging detail 
                {
                    ms = PrintArAgingDetail_ForeignLocal(date1, date2, currency,partyTo, userId);
                    fileName = "ArAgingDetail";
                }
                else if (reportName == "10")//aging detail 
                {
                    ms = PrintArAgingDetail_ForeignForeign(date1, date2, currency,partyTo, userId);
                    fileName = "ArAgingDetail";
                }
                else if (reportName == "5")//ar gs
                {
                    ms = PrintArGst(date1, date2, userId);
                    fileName = "ArGst";
                }






                //ap

                else if (reportName == "21")//customer trans
                {
                    ms = PrintApCustomerTrans(date1, date2, partyTo, currency, userId);
                    fileName = "ApVendorTrans";
                }
                else if (reportName == "22")//outstanding by local
                {
                    ms = PrintApStatement(date1,date2, partyTo, currency, userId);
                    fileName = "ApStatement";
                }
                else if (reportName == "26")//outstanding by usd
                {
                    ms = PrintApStatement_ForeignLocal(date1, date2, partyTo, currency, userId);
                    fileName = "ApStatement";
                }
                else if (reportName == "31")//outstanding by usd
                {
                    ms = PrintApStatement_ForeignForeign(date1, date2, partyTo, currency, userId);
                    fileName = "ApStatement";
                }
                else if (reportName == "23")//aging summary 
                {
                    ms = PrintApAgingSummary(date1, date2, currency, userId);
                    fileName = "ApAgingSummary";
                }
                else if (reportName == "27")//aging summary 
                {
                    ms = PrintApAgingSummary_ForeignLocal(date1, date2, currency, userId);
                    fileName = "ApAgingSummary";
                }
                else if (reportName == "29")//aging summary 
                {
                    ms = PrintApAgingSummary_ForeignForeign(date1, date2, currency, userId);
                    fileName = "ApAgingSummary";
                }
                else if (reportName == "24")//aging detail 
                {
                    ms = PrintApAgingDetail(date1, date2, currency, userId);
                    fileName = "ApAgingDetail";
                }
                else if (reportName == "28")//aging detail 
                {
                    ms = PrintApAgingDetail_ForeignLocal(date1, date2, currency, userId);
                    fileName = "ApAgingDetail";
                }
                else if (reportName == "30")//aging detail 
                {
                    ms = PrintApAgingDetail_ForeignForeign(date1, date2, currency, userId);
                    fileName = "ApAgingDetail";
                }
                else if (reportName == "25")//ap gst
                {
                    ms = PrintApGst(date1, date2, userId);
                    fileName = "ApGst";
                }


                //gl
                else if (reportName == "41")//Trial Balance
                {
                    ms = PrintGlTrialBalance(date1, date2, userId);
                    fileName = "TrialBalance";
                }
                else if (reportName == "42")//Balance sheet
                {
                    ms = PrintGlBalanceSheet(date1, date2, userId);
                    fileName = "BalanceSheet";
                }
                else if (reportName == "43")//Balance sheet
                {
                    if (date2 == "13")
                    {
                        ms = PrintGlPlStatement_year(date1, userId);
                        fileName = "PlStatement_year";
                    }
                    else
                    {
                        ms = PrintGlPlStatement(date1, date2, userId);
                        fileName = "PlStatement";
                    }
                }
                else if (reportName == "44")//audit trail by party
                {
                    ms = PrintGlAuditTrial_Party(date1, date2, date3, partyTo, currency, userId);
                    fileName = "AduitTrialByParty";
                }
                else if (reportName == "45")//audit trail by chart of account
                {
                    ms = PrintGlAuditTrial_AccCode(date1, date2, date3, partyTo, userId);
                    fileName = "AuditTrialByAcCode";
                }
                else if (reportName == "46")//jouranl listing
                {
                    ms = PrintGlJournalList(date1, date2, partyTo, userId);
                    fileName = "JournalListing";
                }
                else if (reportName == "47")//Gl gst
                {
                    ms = PrintGlGst(date1, date2, userId);
                    fileName = "GlGst";
                }

                else if (reportName == "48")//aging summary 
                {
                    ms = PrintGlAgingSummary(date1, date2, currency, userId);
                    fileName = "GlAgingSummary";
                }
                else if (reportName == "49")//aging summary base local currency
                {
                    ms = PrintGlAgingSummary_ForeignLocal(date1, date2, currency, userId);
                    fileName = "GlAgingSummary";
                }
                else if (reportName == "50")//aging summary base doc currency
                {
                    ms = PrintGlAgingSummary_ForeignForeign(date1, date2, currency, userId);
                    fileName = "GlAgingSummary";
                }


                else if (reportName == "51")//aging detail 
                {
                    ms = PrintGlAgingDetail(date1, date2, currency, partyTo, userId);
                    fileName = "GlAgingDetail";
                }
                else if (reportName == "52")//aging detail 
                {
                    ms = PrintGlAgingDetail_ForeignLocal(date1, date2, currency, partyTo, userId);
                    fileName = "GlAgingDetail";
                }
                else if (reportName == "53")//aging detail 
                {
                    ms = PrintGlAgingDetail_ForeignForeign(date1, date2, currency, partyTo, userId);
                    fileName = "GlAgingDetail";
                }


                //other
                else if (reportName == "61")//Bank receipt report
                {
                    ms = PrintBankReceipt(date1, date2,partyTo, userId);
                    fileName = "BankReceiptReport";
                }
                else if (reportName == "62")//Bank payment report
                {
                    ms = PrintBankPayment(date1, date2,partyTo, userId);
                    fileName = "BankPaymenttReport";
                }
                else if (reportName == "63")//Bank reconciliation report
                {
                    ms = PrintBankRecon(date1, date2,partyTo, userId);
                    fileName = "BankReconciliationReport";
                }
                else if (reportName == "64")//ar doc listing sum
                {
                    ms = PrintArDocListing_Sum(date1, date2, partyTo,currency, userId);
                    fileName = "ArDocListingSummary";
                }
                else if (reportName == "65")//ar doc listing detail
                {
                    ms = PrintArDocListing_Detail(date1, date2, partyTo, currency, userId);
                    fileName = "ArDocListingDetail";
                }
                else if (reportName == "66")//ar doc listing sum
                {
                    ms = PrintApDocListing_Sum(date1, date2, partyTo,currency, userId);
                    fileName = "ApDocListingSummary";
                }
                else if (reportName == "67")//ar doc listing detail
                {
                    ms = PrintApDocListing_Detail(date1, date2, partyTo, currency, userId);
                    fileName = "ApDocListingDetail";
                }
                else if (reportName == "68")//ACCRUED INCOME REPORT
                {
                    ms = PrintAccruedIncome(date1, date2, userId);
                    fileName = "AccruedIncome";
                }
                else if (reportName == "69")//Deferred INCOME REPORT
                {
                    ms = PrintDeferredIncome(date1, date2, userId);
                    fileName = "DeferredIncome";
                }

            }
            catch (Exception ex)
            {
				throw new Exception(ex.Message + ex.StackTrace);
                Response.Write(ex.Message);
            }
            if (docType == "0")
            {
                ExChange(ms, fileName);
            }
            else if (docType == "1")//expor excel
            {
                byte[] bt = ms.GetBuffer();
                if (bt.Length > 0)
                {
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.Buffer = false;
                    Response.ContentType = "application/xls";
                    Response.AddHeader("Content-Length", bt.Length.ToString());
                    Response.AddHeader("Content-Disposition", "inline; filename=" + fileName + ".xls");
                    Response.BinaryWrite(bt);
					Response.End();
                }
            }
        }
    }
    private string userId = "";
    private void ExChange(MemoryStream ms, string fileName)
    {
        MemoryStream str = ms;

        byte[] bt = str.GetBuffer();

        try
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Buffer = false;
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Length", bt.Length.ToString());
            Response.AddHeader("Content-Disposition", "inline; filename=" + fileName + ".pdf");
            //Response.Output.Write(bt);
            Response.BinaryWrite(bt);
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.Write(ex.Message + "/" + ex.StackTrace);
        }
        str.Dispose();
		Response.End();
    }

    #region ar
    private MemoryStream PrintArCustomerTrans(string date1, string date2, string partyTo, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArCustomerTrans.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsArCustomerTrans(d1, d2, partyTo, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArStatement(string date1, string date2,string partyTo, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArStatement.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsArStatement(d1,d2, partyTo, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArStatementDN(string date1, string date2,string partyTo, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArStatement.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsArStatementDN(d1,d2, partyTo, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArStatement_ForeignLocal(string date1,string date2, string partyTo, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArStatement.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsArStatement_ForeignLocal(d1, d2,partyTo, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArStatement_ForeignForeign(string date1,string date2, string partyTo, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArStatement.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsArStatement_ForeignForeign(d1, d2,partyTo, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArAgingSummary(string date1, string date2, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArAgingSummary.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsArAgingSummary(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArAgingSummary_ForeignLocal(string date1, string date2, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArAgingSummary.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsArAgingSummary_ForeignLocal(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArAgingSummary_ForeignForeign(string date1, string date2, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArAgingSummary.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsArAgingSummary_ForeignForeign(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArAgingDetail(string date1, string date2, string cury, string sales,string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArAgingDetail.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsArAgingDetail(d1, d2, cury,sales, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArAgingDetail_ForeignLocal(string date1, string date2, string cury, string sales, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArAgingDetail.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsArAgingDetail_ForeignLocal(d1, d2, cury,sales, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArAgingDetail_ForeignForeign(string date1, string date2, string cury, string sales, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArAgingDetail.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsArAgingDetail_ForeignForeign(d1, d2, cury,sales, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArGst(string date1, string date2, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArGst.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsArGst(d1, d2, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    #endregion

    #region ap
    private MemoryStream PrintApCustomerTrans(string date1, string date2, string partyTo, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ApCustomerTrans.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsApCustomerTrans(d1, d2, partyTo, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintApStatement(string date1,string date2, string partyTo, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ApStatement.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsApStatement(d1,d2, partyTo, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintApStatement_ForeignLocal(string date1,string date2, string partyTo, string cury, string userId) //curry=usd  print base on local
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ApStatement.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsApStatement_ForeignLocal(d1,d2, partyTo, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintApStatement_ForeignForeign(string date1, string date2, string partyTo, string cury, string userId) //curry=usd  print base on local
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ApStatement.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsApStatement_ForeignForeign(d1, d2, partyTo, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintApAgingSummary(string date1, string date2, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ApAgingSummary.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsApAgingSummary(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }

    private MemoryStream PrintApAgingSummary_ForeignLocal(string date1, string date2, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ApAgingSummary.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsApAgingSummary_ForeignLocal(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintApAgingSummary_ForeignForeign(string date1, string date2, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ApAgingSummary.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsApAgingSummary_ForeignForeign(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }

    private MemoryStream PrintApAgingDetail(string date1, string date2, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ApAgingDetail.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsApAgingDetail(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintApAgingDetail_ForeignLocal(string date1, string date2, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ApAgingDetail.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsApAgingDetail_ForeignLocal(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintApAgingDetail_ForeignForeign(string date1, string date2, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ApAgingDetail.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsApAgingDetail_ForeignForeign(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintApGst(string date1, string date2, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ApGst.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsApGst(d1, d2, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    #endregion

    #region gl
    private MemoryStream PrintGlAgingSummary(string date1, string date2, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlAgingSummary.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsGlAgingSummary(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintGlAgingSummary_ForeignLocal(string date1, string date2, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlAgingSummary.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsGlAgingSummary_ForeignLocal(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintGlAgingSummary_ForeignForeign(string date1, string date2, string cury, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlAgingSummary.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsGlAgingSummary_ForeignForeign(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintGlAgingDetail(string date1, string date2, string cury, string sales, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlAgingDetail.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsGlAgingDetail(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintGlAgingDetail_ForeignLocal(string date1, string date2, string cury, string sales, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlAgingDetail.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsGlAgingDetail_ForeignLocal(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintGlAgingDetail_ForeignForeign(string date1, string date2, string cury, string sales, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlAgingDetail.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsGlAgingDetail_ForeignForeign(d1, d2, cury, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }

    private MemoryStream PrintGlTrialBalance(string date1, string date2, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlTrialBalance.repx"));
        rpt.DataSource = AccountPrint.DsGlTrialBalance(date1, date2, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintGlBalanceSheet(string date1, string date2, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlBalanceSheet.repx"));
        rpt.DataSource = AccountPrint.DsGlBalanceSheet(date1, date2, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    //private MemoryStream PrintGlPlStatementNew(string date1, string date2, string userId)
    //{
    //    XtraReport rpt = new XtraReport();
    //    rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlPlStatement_Old.repx"));
    //    rpt.DataSource = AccountPrint.DsGlPlStatement(date1, date2, userId);

    //    System.IO.MemoryStream str = new MemoryStream();
    //    if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);
    //    return str;
    //}
    private MemoryStream PrintGlPlStatement_year(string date1, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlPlStatement_year_0.repx"));

        DataSet set = AccountPrint.DsGlPlStatement_year(date1, userId);
        DataTable tab_mast = set.Tables[0];
        DataTable tab_Pl = set.Tables[1];
        DataTable tab_Other = set.Tables[2];
        DataTable tab_Revenue = set.Tables[3];
        DataTable tab_Expense = set.Tables[4];

        if (tab_Pl.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Pl = rpt.Report.Bands["GroupFooter_Pl"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Pl = new XRSubreport();
            subReport_Pl.Name = "groupFooter_Pl";
            groupFooter_Pl.Controls.Add(subReport_Pl);
            XtraReport rpt_Pl = new XtraReport();
            rpt_Pl.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlPlStatement_year_1.repx"));
            subReport_Pl.ReportSource = rpt_Pl;
            rpt_Pl.DataSource = tab_Pl;
        }
        if (tab_Other.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Other = rpt.Report.Bands["GroupFooter_Other"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Other = new XRSubreport();
            subReport_Other.Name = "groupFooter_Other";
            groupFooter_Other.Controls.Add(subReport_Other);
            XtraReport rpt_Other = new XtraReport();
            rpt_Other.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlPlStatement_year_2.repx"));
            subReport_Other.ReportSource = rpt_Other;
            rpt_Other.DataSource = tab_Other;


            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Revenue = rpt.Report.Bands["GroupFooter_Revenue"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Revenue = new XRSubreport();
            subReport_Revenue.Name = "groupFooter_Revenue";
            groupFooter_Revenue.Controls.Add(subReport_Revenue);
            XtraReport rpt_Revenue = new XtraReport();
            rpt_Revenue.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlPlStatement_year_3.repx"));
            subReport_Revenue.ReportSource = rpt_Revenue;
            rpt_Revenue.DataSource = tab_Revenue;

        }
        if (tab_Expense.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Expense = rpt.Report.Bands["GroupFooter_Expense"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Expense = new XRSubreport();
            subReport_Expense.Name = "groupFooter_Expense";
            groupFooter_Expense.Controls.Add(subReport_Expense);
            XtraReport rpt_Expense = new XtraReport();
            rpt_Expense.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlPlStatement_year_4.repx"));
            subReport_Expense.ReportSource = rpt_Expense;
            rpt_Expense.DataSource = tab_Expense;
        }

        rpt.DataSource = tab_mast;
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintGlPlStatement(string date1, string date2, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlPlStatement_0.repx"));

        DataSet set = AccountPrint.DsGlPlStatement_1(date1, date2, userId);
        DataTable tab_mast = set.Tables[0];
        DataTable tab_Pl = set.Tables[1];
        DataTable tab_Other = set.Tables[2];
        DataTable tab_Revenue = set.Tables[3];
        DataTable tab_Expense = set.Tables[4];

        if (tab_Pl.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Pl = rpt.Report.Bands["GroupFooter_Pl"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Pl = new XRSubreport();
            subReport_Pl.Name = "groupFooter_Pl";
            groupFooter_Pl.Controls.Add(subReport_Pl);
            XtraReport rpt_Pl = new XtraReport();
            rpt_Pl.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlPlStatement_1.repx"));
            subReport_Pl.ReportSource = rpt_Pl;
            rpt_Pl.DataSource = tab_Pl;
        }
        if (tab_Other.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Other = rpt.Report.Bands["GroupFooter_Other"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Other = new XRSubreport();
            subReport_Other.Name = "groupFooter_Other";
            groupFooter_Other.Controls.Add(subReport_Other);
            XtraReport rpt_Other = new XtraReport();
            rpt_Other.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlPlStatement_2.repx"));
            subReport_Other.ReportSource = rpt_Other;
            rpt_Other.DataSource = tab_Other;


            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Revenue = rpt.Report.Bands["GroupFooter_Revenue"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Revenue = new XRSubreport();
            subReport_Revenue.Name = "groupFooter_Revenue";
            groupFooter_Revenue.Controls.Add(subReport_Revenue);
            XtraReport rpt_Revenue = new XtraReport();
            rpt_Revenue.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlPlStatement_3.repx"));
            subReport_Revenue.ReportSource = rpt_Revenue;
            rpt_Revenue.DataSource = tab_Revenue;

        }
        if (tab_Expense.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Expense = rpt.Report.Bands["GroupFooter_Expense"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Expense = new XRSubreport();
            subReport_Expense.Name = "groupFooter_Expense";
            groupFooter_Expense.Controls.Add(subReport_Expense);
            XtraReport rpt_Expense = new XtraReport();
            rpt_Expense.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlPlStatement_4.repx"));
            subReport_Expense.ReportSource = rpt_Expense;
            rpt_Expense.DataSource = tab_Expense;
        }

        rpt.DataSource = tab_mast;
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintGlAuditTrial_Party(string date1, string date2, string date3,string partyTo, string partyType, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlAuditTrial_Party.repx"));
        //rpt.DataSource = AccountPrint.DsGlAuditTrial_Party(date1, date2, date3, partyTo, partyType.Substring(0, 1), userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintGlAuditTrial_AccCode(string date1, string date2, string date3, string accCode, string userId)
    {
        XtraReport rpt = new XtraReport();
        if (accCode == "" || accCode == "null")
        {
            rpt.CreateDocument();
            string sql = "SELECT Code FROM XXChartAcc order by code ";
            DataTable tab = Helper.Sql.List(sql);
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                XtraReport subRpt = new XtraReport();
                subRpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlAuditTrial_AccCode.repx"));
                subRpt.DataSource = AccountPrint.DsGlAuditTrial_AccCode(date1, date2, date3, tab.Rows[i][0].ToString(), userId);
                subRpt.CreateDocument();
                rpt.Pages.AddRange(subRpt.Pages);
            }
        }
        else
        {
            rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlAuditTrial_AccCode.repx"));
            rpt.DataSource = AccountPrint.DsGlAuditTrial_AccCode(date1, date2, date3, accCode, userId);
        }
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
        {
            //DevExpress.XtraPrinting.XlsExportOptions opt = new DevExpress.XtraPrinting.XlsExportOptions();
            //opt.ExportMode = DevExpress.XtraPrinting.XlsExportMode.SingleFile;
            rpt.ExportToXls(str);
        }
        else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintGlJournalList(string date1, string date2, string prtType, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlJournalListing.repx"));
        rpt.AfterPrint += new EventHandler(rpt_AfterPrint);
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsGlJournalList(d1, d2, prtType, userId);
        Session["d1"] = date1;
        Session["d2"] = date2;
        Session["prtType"] = prtType;
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }

    void rpt_AfterPrint(object sender, EventArgs e)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlJournalListing_1.repx"));
        string date1 = Session["d1"].ToString();
        string date2 = Session["d2"].ToString();
        string docType = Session["prtType"].ToString();
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsGlJournalList_1(d1, d2, docType, userId);
        rpt.CreateDocument();

        XtraReport MastReport = sender as XtraReport;
        for (int i = 0; i < rpt.Pages.Count; i++)
        {
            MastReport.Pages.Add(rpt.Pages[i]);
        }
    }
    private MemoryStream PrintGlGst(string date1, string date2, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\GlGst.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsGlGst(d1, d2, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    #endregion
    #region other report
    private MemoryStream PrintBankReceipt(string date1, string date2, string accCode, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\BankReceiptRpt.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsBankReceipt(d1, d2, accCode, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintBankPayment(string date1, string date2, string accCode, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\BankPaymentRpt.repx"));
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AccountPrint.DsBankPayment(d1, d2, accCode, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintBankRecon(string date1, string balAmt, string accCode, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\BankReconRpt.repx"));
        string[] s1 = date1.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        rpt.DataSource = AccountPrint.DsBankRecon(d1, balAmt, accCode, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }


    private MemoryStream PrintArDocListing_Sum(string date1, string date2, string partyTo,string docType, string userId)
    {
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArDocListing_Sum.repx"));

        DevExpress.XtraReports.UI.XRSubreport subReport = rpt.Report.Bands["ReportFooter"].Controls["xrSubreport1"] as DevExpress.XtraReports.UI.XRSubreport;

        XtraReport subRpt=new XtraReport();
        subRpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArDocListing_Sum1.repx"));
       // subRpt.DataSource=;
        subReport.ReportSource = subRpt;
        DataSet set=AccountPrint.DsArDocListing_Sum(d1, d2, partyTo, docType, userId);
        rpt.DataSource = set;
        subRpt.DataSource = set.Tables[1];

        //ArDocListingSum rpt = new ArDocListingSum(d1, d2, partyTo, docType, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArDocListing_Detail(string date1, string date2, string partyTo, string docType, string userId)
    {
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArDocListing_Detail.repx"));

        DevExpress.XtraReports.UI.XRSubreport subReport = rpt.Report.Bands["ReportFooter"].Controls["xrSubreport1"] as DevExpress.XtraReports.UI.XRSubreport;

        XtraReport subRpt = new XtraReport();
        subRpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArDocListing_Sum1.repx"));
        // subRpt.DataSource=;
        subReport.ReportSource = subRpt;
        DataSet set = AccountPrint.DsArDocListing_Detail(d1, d2, partyTo, docType, userId);
        rpt.DataSource = set;
        subRpt.DataSource = set.Tables[1];


        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintApDocListing_Sum(string date1, string date2, string partyTo,string docType, string userId)
    {
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArDocListing_Sum.repx"));

        DevExpress.XtraReports.UI.XRSubreport subReport = rpt.Report.Bands["ReportFooter"].Controls["xrSubreport1"] as DevExpress.XtraReports.UI.XRSubreport;

        XtraReport subRpt=new XtraReport();
        subRpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArDocListing_Sum1.repx"));
       // subRpt.DataSource=;
        subReport.ReportSource = subRpt;
        DataSet set=AccountPrint.DsApDocListing_Sum(d1, d2, partyTo, docType, userId);
        rpt.DataSource = set;
        subRpt.DataSource = set.Tables[1];

        //ArDocListingSum rpt = new ArDocListingSum(d1, d2, partyTo, docType, userId);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintApDocListing_Detail(string date1, string date2, string partyTo, string docType, string userId)
    {
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArDocListing_Detail.repx"));

        DevExpress.XtraReports.UI.XRSubreport subReport = rpt.Report.Bands["ReportFooter"].Controls["xrSubreport1"] as DevExpress.XtraReports.UI.XRSubreport;

        XtraReport subRpt = new XtraReport();
        subRpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\ArDocListing_Sum1.repx"));
        // subRpt.DataSource=;
        subReport.ReportSource = subRpt;
        DataSet set = AccountPrint.DsApDocListing_Detail(d1, d2, partyTo, docType, userId);
        rpt.DataSource = set;
        subRpt.DataSource = set.Tables[1];


        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }


    private MemoryStream PrintAccruedIncome(string date1, string date2, string userId)
    {
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\AccruedIncome.repx"));

        DevExpress.XtraReports.UI.XRSubreport subReport = rpt.Report.Bands["ReportFooter"].Controls["xrSubreport1"] as DevExpress.XtraReports.UI.XRSubreport;
        DataSet set = AccountPrint.DsAccruedIncome(d1, d2, userId);
        rpt.DataSource = set;

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintDeferredIncome(string date1, string date2, string userId)
    {
        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Repx\AccruedIncome.repx"));

        DevExpress.XtraReports.UI.XRSubreport subReport = rpt.Report.Bands["ReportFooter"].Controls["xrSubreport1"] as DevExpress.XtraReports.UI.XRSubreport;
        DataSet set = AccountPrint.DsDeferredIncome(d1, d2, userId);
        rpt.DataSource = set;

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);

        return str;
    }
    #endregion
}

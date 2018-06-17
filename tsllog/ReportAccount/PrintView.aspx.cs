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

public partial class Modules_Import_PrintView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["doc"] == null) return;
            string reportName = Request.Params["doc"].ToString();
            string refN = Request.Params["docId"].ToString();
            //1.ar receipt
            //2.payment voucher

            //7:journal entry
            MemoryStream ms = new MemoryStream();
            string fileName = "";
            try
            {
                if (reportName == "1")// AR RECEIPT
                {
                    ms = PrintArReceipt(refN);
                }
                else if (reportName == "2")//auth letter
                {
                    ms = PrintApVoucher(refN);
                }
                else if (reportName == "3")//ap Payment
                {
                    ms = PrintApPayment(refN);
                }
                else if (reportName == "4")//ap Payment
                {
                    ms = PrintApPaymentSr(refN);
                }
                else if (reportName == "5")//PAYMENT AR CN
                {
                    ms = PrintArReceipt_Cn(refN);
                }
                else if (reportName == "6")//PAYMENT AR credit
                {
                    ms = PrintArReceipt_Cn(refN);
                }
                else if (reportName == "7")//journal entry
                {
                    ms = PrintJournalEntry(refN);
                }
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
		throw new Exception(ex.Message + ex.StackTrace);
            }
            ExChange(ms,fileName);


        }
    }
    private void ExChange(MemoryStream ms,string fileName)
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
            Response.AddHeader("Content-Disposition", "inline; filename="+ fileName +".pdf");
            //Response.Output.Write(bt);
            Response.BinaryWrite(bt);
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.Write(ex.Message+"/"+ex.StackTrace);
        }
        str.Dispose();
		Response.End();
    }
   
    private MemoryStream PrintArReceipt(string docId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Document\ArReceipt.repx"));
        rpt.DataSource = AccountPrint.DsArReceipt(docId);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArReceipt_Cn(string docId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Document\ArReceipt_Cn.repx"));
        rpt.DataSource = AccountPrint.DsArReceipt(docId);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArReceipt_Cm(string docId)
    {
        //XtraReport rpt = new XtraReport();
        //rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Document\ArReceipt_Cm.repx"));
        //rpt.DataSource = AccountPrint.DsArReceipt(docId);

        System.IO.MemoryStream str = new MemoryStream();
        //rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintApVoucher(string docId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Document\ApVoucher.repx"));
        rpt.DataSource = AccountPrint.DsApVoucher(docId);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintApPayment(string docId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Document\ApPayment.repx"));
        rpt.DataSource = AccountPrint.DsApPayment(docId);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintApPaymentSr(string docId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Document\ApPayment_Sr.repx"));
        rpt.DataSource = AccountPrint.DsApPayment(docId);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintJournalEntry(string docId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAccount\Document\JournalEntry.repx"));
        rpt.DataSource = AccountPrint.DsJournalEntry(docId);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }

}

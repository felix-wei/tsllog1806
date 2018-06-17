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

public partial class ReportFreightSea_AccountPrintView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string id = Request.Params["location"].ToString();
            //string type = Request.Params["printtype"].ToString();
            if (Request.Params["document"] == null) return;
            string reportName = Request.Params["document"].ToString();
            string billNo = Request.Params["no"].ToString();
            //1.invoice
            //2.cr note
            //3.dr note
            //4.voucher

            //21:invoice a4
            MemoryStream ms = new MemoryStream();
            string fileName = "";
            try
            {
                if (reportName == "1")
                {
                    ms = PrintInvoice(billNo);
                }
                else if (reportName == "2")
                {
                    ms = PrintCrNote(billNo);
                }
                else if (reportName == "3")
                {
                    ms = PrintDrNote(billNo);
                }
                else if (reportName == "4")
                {
                    ms = PrintVoucher(billNo);
                }
                else if (reportName == "5")
                {
                    ms = PrintPayable(billNo);
                }
                else if (reportName == "21")
                {
                    ms = PrintInvoiceA4(billNo);
                }
                else if (reportName == "22")
                {
                    ms = PrintCrNoteA4(billNo);
                }
                else if (reportName == "23")
                {
                    ms = PrintDrNoteA4(billNo);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
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
            Response.Buffer = true;
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
    }
 
    private MemoryStream PrintInvoice(string billNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Invoice.repx"));
        rpt.DataSource = AccountFreightPrint.PrintInvoice(billNo,"IV");
        rpt.CreateDocument();
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }

    private MemoryStream PrintInvoiceA4(string billNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Invoice_A4.repx"));
        rpt.DataSource = AccountFreightPrint.PrintInvoice(billNo, "IV");
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintCrNote(string billNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Invoice.repx"));
        rpt.DataSource = AccountFreightPrint.PrintInvoice(billNo,"CN");
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintCrNoteA4(string billNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Invoice_A4.repx"));
        rpt.DataSource = AccountFreightPrint.PrintInvoice(billNo,"CN");
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintDrNote(string billNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Invoice.repx"));
        rpt.DataSource = AccountFreightPrint.PrintInvoice(billNo,"DN");
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintDrNoteA4(string billNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Invoice_A4.repx"));
        rpt.DataSource = AccountFreightPrint.PrintInvoice(billNo,"DN");
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintVoucher(string billNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Voucher.repx"));
        DataSet set= AccountFreightPrint.PrintVoucher(billNo);


        DevExpress.XtraReports.UI.GroupFooterBand groupFooter1 = rpt.Report.Bands["GroupFooter1"] as DevExpress.XtraReports.UI.GroupFooterBand;
        DevExpress.XtraReports.UI.XRSubreport subReport1 = new XRSubreport();
        subReport1.Name = "groupFooter1";
        groupFooter1.Controls.Add(subReport1);
        XtraReport rpt1 = new XtraReport();
        rpt1.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Voucher_detail.repx"));
        subReport1.ReportSource = rpt1;
        rpt1.DataSource = set;

        //DevExpress.XtraReports.UI.GroupFooterBand groupFooter2 = rpt.Report.Bands["GroupFooter2"] as DevExpress.XtraReports.UI.GroupFooterBand;
        //DevExpress.XtraReports.UI.XRSubreport subReport2 = new XRSubreport();
        //subReport2.Name = "groupFooter2";
        //groupFooter2.Controls.Add(subReport2);
        //XtraReport rpt2 = new XtraReport();
        //rpt2.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Voucher_detail.repx"));
        //subReport2.ReportSource = rpt2;
        //rpt2.DataSource = set;

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintPayable(string billNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Payable.repx"));
        DataSet set = AccountFreightPrint.PrintPayable(billNo);


        DevExpress.XtraReports.UI.GroupFooterBand groupFooter1 = rpt.Report.Bands["GroupFooter1"] as DevExpress.XtraReports.UI.GroupFooterBand;
        DevExpress.XtraReports.UI.XRSubreport subReport1 = new XRSubreport();
        subReport1.Name = "groupFooter1";
        groupFooter1.Controls.Add(subReport1);
        XtraReport rpt1 = new XtraReport();
        rpt1.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Payable_detail.repx"));
        subReport1.ReportSource = rpt1;
        rpt1.DataSource = set;

        //DevExpress.XtraReports.UI.GroupFooterBand groupFooter2 = rpt.Report.Bands["GroupFooter2"] as DevExpress.XtraReports.UI.GroupFooterBand;
        //DevExpress.XtraReports.UI.XRSubreport subReport2 = new XRSubreport();
        //subReport2.Name = "groupFooter2";
        //groupFooter2.Controls.Add(subReport2);
        //XtraReport rpt2 = new XtraReport();
        //rpt2.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Voucher_detail.repx"));
        //subReport2.ReportSource = rpt2;
        //rpt2.DataSource = set;

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
   
}

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

public partial class ReportFreightSea_RptPrintView : System.Web.UI.Page
{

    private string docType = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["doc"] == null) return;
            string reportName = Request.Params["doc"].ToString();
            docType = SafeValue.SafeString(Request.Params["docType"], "0");
            string d1 = Request.Params["d1"].ToString();
            string d2 = Request.Params["d2"].ToString();
            string party = SafeValue.SafeString(Request.Params["p"],"");
            string type = SafeValue.SafeString(Request.Params["type"], "");


            //1:bill list by number sort by date
            //2:bill list by date   sort by no
            ////////////import
            //3:import voulme by doc date
            //4:import voulme by agt 
            //5:import voulme by port
            //6:  profit by customer
            //7:  profit by salesman
            //8: profit  for salesman import and export

            ///////////////export
            //33:export voulme by doc date
            //34:export voulme by agt 
            //35:export voulme by port
            //36: profit by customer
            //37: profit by salesman
            MemoryStream ms = new MemoryStream();
            string fileName = "";
            try
            {
                string userName = HttpContext.Current.User.Identity.Name;
                if (reportName == "1")
                {
                    ms = PrintBillList(type,party,d1,d2,"DocDate", userName);
                    fileName = "BillList" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "2")
                {
                    ms = PrintBillList(type, party, d1, d2, "DocNo", userName);
                    fileName = "BillList" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "3")
                {
                    ms = PrintImportVolumeByDate(type, d1, d2, userName);
                    fileName = "ImportVolumeByDate" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "4")
                {
                    ms = PrintImportVolumeByAgent(type, d1, d2,party, userName);
                    fileName = "ImportVolumeByAgent" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "5")
                {
                    ms = PrintImportVolumeByPort(type, d1, d2,party, userName);
                    fileName = "ImportVolumeByPort" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "6")//profit by customer
                {
                    ms = PrintProfitByCustomer(type, d1, d2, party, userName);
                    fileName = "ImportProfit" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "7")//profit by salesman
                {
                    ms = PrintProfitBySales(type, d1, d2, party, userName);
                    fileName = "ImportProfit" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "8")//profit by salesman
                {
                    ms = PrintProfit(d1, d2, party, userName);
                    fileName = "Profit" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "33")
                {
                    ms = PrintExportVolumeByDate(type, d1, d2, userName);
                    fileName = "ExportVolumeByDate" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "34")
                {
                    ms = PrintExportVolumeByAgent(type, d1, d2, party, userName);
                    fileName = "ExportVolumeByAgent" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "35")
                {
                    ms = PrintExportVolumeByPort(type, d1, d2, party, userName);
                    fileName = "ExportVolumeByPort" + DateTime.Now.ToString("yyyyMMdd");
                }
            }
            catch (Exception ex)
            {
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
                    Response.Buffer = true;
                    Response.ContentType = "application/xls";
                    Response.AddHeader("Content-Length", bt.Length.ToString());
                    Response.AddHeader("Content-Disposition", "inline; filename=" + fileName + ".xls");
                    Response.BinaryWrite(bt);
                }
            }
        }
    }
    private void ExChange(MemoryStream ms, string fileName)
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
    }

    private MemoryStream PrintBillList(string billType,string party,string date1,string date2, string sortFiled,string userName)
    {
        XtraReport rpt = new XtraReport();
        if (billType == "VO")
        {
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\ReportRepx\Account\VoucherList.repx"));

            if (date1.IndexOf("/") != -1 && date2.IndexOf("/") != -1)
            {
                string[] s1 = date1.Split('/');
                string[] s2 = date2.Split('/');
                DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
                DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
                rpt.DataSource = SeaFreightRptPrint.dsVoucherByDate(billType, party, d1, d2, sortFiled, userName);
            }
            else
            {
                rpt.DataSource = SeaFreightRptPrint.dsVoucherByNo(billType, party, date1, date2, sortFiled, userName);
            }
        }
        else
        {
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\ReportRepx\Account\InvoiceList.repx"));

            if (date1.IndexOf("/") != -1 && date2.IndexOf("/") != -1)
            {
                string[] s1 = date1.Split('/');
                string[] s2 = date2.Split('/');
                DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
                DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
                rpt.DataSource = SeaFreightRptPrint.dsInvoiceByDate(billType, party, d1, d2, sortFiled, userName);
            }
            else
            {
                rpt.DataSource = SeaFreightRptPrint.dsInvoiceByNo(billType, party, date1, date2, sortFiled, userName);
            }
        }
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
        rpt.ExportToPdf(str);

        return str;
    }

    #region import
    private MemoryStream PrintImportVolumeByDate(string refType, string date1, string date2, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\ReportRepx\Volume.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = SeaFreightRptPrint.dsImportVolumeByDate(refType, d1, d2, userName);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintImportVolumeByAgent(string refType, string date1, string date2, string agentId, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\ReportRepx\VolumeByAgent.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = SeaFreightRptPrint.dsImportVolumeByAgent(refType, d1, d2, agentId, userName);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintImportVolumeByPort(string refType, string date1, string date2, string port, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\ReportRepx\VolumeByPort.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = SeaFreightRptPrint.dsImportVolumeByPort(refType, d1, d2, port, userName);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }


    private MemoryStream PrintProfitByCustomer(string refType, string date1, string date2, string cust, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\ReportRepx\ProfitByCust.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = SeaFreightRptPrint.dsProfitByCust(refType, d1, d2, cust, userName);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintProfitBySales(string refType, string date1, string date2, string sales, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\ReportRepx\ProfitBySales.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = SeaFreightRptPrint.dsProfitBySales(refType, d1, d2, sales, userName);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintProfit(string date1, string date2, string sales, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\ReportRepx\Profit.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = SeaFreightRptPrint.dsProfit(d1, d2, sales, userName);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    #endregion

    #region export
    private MemoryStream PrintExportVolumeByDate(string refType, string date1, string date2, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\ReportRepx\Volume.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = SeaFreightRptPrint.dsExportVolumeByDate(refType, d1, d2, userName);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintExportVolumeByAgent(string refType, string date1, string date2, string agentId, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\ReportRepx\VolumeByAgent.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = SeaFreightRptPrint.dsExportVolumeByAgent(refType, d1, d2, agentId, userName);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintExportVolumeByPort(string refType, string date1, string date2, string port, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\ReportRepx\VolumeByPort.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = SeaFreightRptPrint.dsExportVolumeByPort(refType, d1, d2, port, userName);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    } 
    #endregion
}

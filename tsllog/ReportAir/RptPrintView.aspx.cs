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

public partial class ReportAir_RptPrintView :System.Web.UI.Page
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
            string refType = SafeValue.SafeString(Request.Params["refType"], "");

            //1:bill list by number sort by date
            //2:bill list by date   sort by no
            ////////////import
            //3:import voulme by doc date
            //4:import voulme by agt 
            //5:import voulme by port
            //6:  profit by customer
            //7:  profit by salesman

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
                    ms = PrintBillList(type,party,d1,d2,"DocDate", userName,refType);
                    fileName = "BillList" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "2")
                {
                    ms = PrintBillList(type, party, d1, d2, "DocNo", userName, refType);
                    fileName = "BillList" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "3")
                {
                    ms = PrintVolumeByDate(type, d1, d2, userName);
                    fileName = "VolumeByDate" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "4")
                {
                    ms = PrintVolumeByAgent(type, d1, d2, party, userName);
                    fileName = "VolumeByAgent" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "5")
                {
                    ms = PrintVolumeByPort(type, d1, d2,party, userName);
                    
                    fileName = "VolumeByPort" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "6")//profit by customer
                {
                    ms = PrintProfitByCustomer(type, d1, d2, party, userName);
                    fileName = "Profit" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "7")//profit by salesman
                {
                    ms = PrintProfitBySales(type, d1, d2, party, userName);
                    fileName = "Profit" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "33")
                {
                    ms = PrintVolumeByDate(type, d1, d2, userName);
                    fileName = "ExportVolumeByDate" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "34")
                {
                    ms = PrintVolumeByAgent(type, d1, d2, party, userName);
                    fileName = "ExportVolumeByAgent" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "35")
                {
                    ms = PrintVolumeByPort(type, d1, d2, party, userName);
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

    private MemoryStream PrintBillList(string billType,string party,string date1,string date2, string sortFiled,string userName,string refType)
    {
        XtraReport rpt = new XtraReport();
        if (billType == "VO" || billType == "PL")
        {
            rpt.LoadLayout(Server.MapPath(@"~\ReportAir\ReportRepx\Account\VoucherList.repx"));

            if (date1.IndexOf("/") != -1 && date2.IndexOf("/") != -1)
            {
                string[] s1 = date1.Split('/');
                string[] s2 = date2.Split('/');
                DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
                DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
                rpt.DataSource = AirFreightRptPrint.dsVoucherByDate(billType, party, d1, d2, sortFiled, userName, refType);
            }
            else
            {
                rpt.DataSource = AirFreightRptPrint.dsVoucherByNo(billType, party, date1, date2, sortFiled, userName, refType);
            }
        }
        else
        {
            rpt.LoadLayout(Server.MapPath(@"~\ReportAir\ReportRepx\Account\InvoiceList.repx"));

            if (date1.IndexOf("/") != -1 && date2.IndexOf("/") != -1)
            {
                string[] s1 = date1.Split('/');
                string[] s2 = date2.Split('/');
                DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
                DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
                rpt.DataSource = AirFreightRptPrint.dsInvoiceByDate(billType, party, d1, d2, sortFiled, userName, refType);
            }
            else
            {
                rpt.DataSource = AirFreightRptPrint.dsInvoiceByNo(billType, party, date1, date2, sortFiled, userName, refType);
            }
        }
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
        rpt.ExportToPdf(str);

        return str;
    }

    #region import/export
    private MemoryStream PrintVolumeByDate(string refType, string date1, string date2, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAir\ReportRepx\Volume.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource =AirFreightRptPrint.dsVolumeByDate(refType, d1, d2, userName);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintVolumeByAgent(string refType, string date1, string date2, string agentId, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAir\ReportRepx\VolumeByAgent.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AirFreightRptPrint.dsVolumeByAgent(refType, d1, d2, agentId, userName);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintVolumeByPort(string refType, string date1, string date2, string port, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAir\ReportRepx\VolumeByPort.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AirFreightRptPrint.dsVolumeByPort(refType, d1, d2, port, userName);
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
        rpt.LoadLayout(Server.MapPath(@"~\ReportAir\ReportRepx\ProfitByCust.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AirFreightRptPrint.dsProfitByCust(refType, d1, d2, cust, userName);
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
        rpt.LoadLayout(Server.MapPath(@"~\ReportAir\ReportRepx\ProfitBySales.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = AirFreightRptPrint.dsProfitBySales(refType, d1, d2, sales, userName);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    #endregion

}

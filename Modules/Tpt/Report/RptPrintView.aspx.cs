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
using System.Drawing;
using System.Collections.Generic;

public partial class Modules_Tpt_Report_RptPrintView : System.Web.UI.Page
{
    private string docType = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["doc"] == null) return;
            string reportName = Request.Params["doc"].ToString();
            docType = SafeValue.SafeString(Request.Params["docType"], "0");
            string d1 = SafeValue.SafeString(Request.Params["d1"]);
            string d2 = SafeValue.SafeString(Request.Params["d2"]);
            string party = SafeValue.SafeString(Request.Params["p"], "");
            string type = SafeValue.SafeString(Request.Params["type"], "");
            string refType = SafeValue.SafeString(Request.Params["refType"], "");
            string no = SafeValue.SafeString(Request.Params["no"], "");
            MemoryStream ms = new MemoryStream();
            string fileName = "";
            try
            {
                string userName = HttpContext.Current.User.Identity.Name;
                if (reportName == "stock_summary")
                {
                    ms = PrintStockSummary(no, refType, d1, d2);
                    fileName = "StockSummary" + DateTime.Now.ToString("yyyyMMdd");
                }
                if (reportName == "stock_bonded")
                {
                    ms = PrintInventoryBalance(no, refType, d1, d2);
                    fileName = "StockBonded" + DateTime.Now.ToString("yyyyMMdd");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                throw new Exception(ex.Message);
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
    private MemoryStream PrintStockSummary(string orderNo, string jobType, string date1, string date2)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Tpt\Report\repx\SummaryStockList.repx"));
        DataSet set = null;
        if (date1.Length > 0)
        {
            string[] s1 = date1.Split('/');
            DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
            DateTime d2 = DateTime.Today;

            if (date1 != null)
            {
                set = DocPrint.PrintStockSummary(orderNo, d1);
            }
        }
        rpt.DataSource = set;
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintInventoryBalance(string orderNo, string jobType, string date1, string date2)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Tpt\Report\repx\InventoryBalance.repx"));
        DataSet set = null;
        if (date1.Length > 0)
        {
            string[] s1 = date1.Split('/');
            DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
            DateTime d2 = DateTime.Today;

            if (date1 != null)
            {
                set = DocPrint.PrintInventoryBalance(orderNo, d1);
            }
        }
        rpt.DataSource = set;
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1") rpt.ExportToXls(str); else rpt.ExportToPdf(str);
        return str;
    }
}
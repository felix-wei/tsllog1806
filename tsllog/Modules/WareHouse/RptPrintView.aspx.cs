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
            string refType = SafeValue.SafeString(Request.Params["refType"], "");

            //1:DO det
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
                if (reportName == "0")
                {
                    ms = PrintSummary(type, d1, d2, userName);
                    fileName = "Summary" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "1")//DO det
                {
                    ms = PrintDoDet(type, d1, d2, userName);
                    fileName = "DO_Detail" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "2")
                {
                    ms =PrintWhFCL(type, d1, d2, userName);
                    fileName = "FCL" + DateTime.Now.ToString("yyyyMMdd");
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

    private MemoryStream PrintSummary(string billType,string date1, string date2, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportWarehouse\repx\Summary.repx"));

        if (date1.IndexOf("/") != -1 && date2.IndexOf("/") != -1)
        {
            string[] s1 = date1.Split('/');
            string[] s2 = date2.Split('/');
            DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
            DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
            rpt.DataSource = WarehouseRptPrint.dsSummary(billType, d1, d2, userName);
        }
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintWhFCL(string billType, string date1, string date2, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportWarehouse\repx\FCL.repx"));

        if (date1.IndexOf("/") != -1 && date2.IndexOf("/") != -1)
        {
            string[] s1 = date1.Split('/');
            string[] s2 = date2.Split('/');
            DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
            DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
            rpt.DataSource = WarehouseRptPrint.dsFCL(billType, d1, d2, userName);
        }
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintDoDet(string type, string date1, string date2, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportWarehouse\repx\DoDet.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = WarehouseRptPrint.dsDoDet(type, d1, d2, userName);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }

}

using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Hr_Report_PrintView : System.Web.UI.Page
{
    private string docType = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["document"] == null) return;
            string reportName = Request.Params["document"].ToString();
            docType = SafeValue.SafeString(Request.Params["doc"], "0");
            string master = "" ;
            string date1 = "";
            string date2 = "";
            string person = "";
            if (Request.Params["master"] != null)
                master = Request.Params["master"].ToString();
            if (Request.Params["from"]!=null)
                date1=Request.Params["from"].ToString();
            if(Request.Params["to"]!=null)
                date2 = Request.Params["to"].ToString();
            if(Request.Params["person"]!=null)
               person = Request.Params["person"].ToString();
            string fileName="";
            MemoryStream ms = new MemoryStream();
            try
            {
                if (reportName == "PayrollSlip") {
                    fileName = "PayrollSlip_" + DateTime.Now.ToString("yyyyMMdd");
                    ms = PrintPayrollSilp(master, date1, date2,person);
                }
                if (reportName == "CPF") {
                    fileName = "PayrollSlip_" + DateTime.Now.ToString("yyyyMMdd");
                    ms = PrintCPFContribution( date1, date2);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                throw new Exception(ex.Message + ex.StackTrace);

            }
            ExChange(ms, fileName);
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
            Response.Buffer = false;
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Length", bt.Length.ToString());
            Response.AddHeader("Content-Disposition", "inline; filename=" + fileName + ".pdf");
            //Response.Output.Write(bt);
            Response.BinaryWrite(bt);
			Response.End();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.Write(ex.Message + "/" + ex.StackTrace);
		throw new Exception(ex.Message + ex.StackTrace);
        }
        str.Dispose();
    }
    private MemoryStream PrintCPFContribution(string date1, string date2)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Hr\Report\repx\CPF_Contribution.repx"));

        string[] s1 = date1.Split('/');
        string[] s2 = date2.Split('/');
        DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
        DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
        rpt.DataSource = HrPrint.PrintCPFContribution(d1, d2);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintPayrollSilp(string master, string dateTime1, string dateTime2,string person)
    {
        string dateFrom = Helper.Safe.SafeDateStr(dateTime1);
        string dateTo = Helper.Safe.SafeDateStr(dateTime2);
        DateTime from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        string user = HttpContext.Current.User.Identity.Name;
        XtraReport rpt = new XtraReport();
        string role = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(string.Format(@"select HrRole from Hr_Person where Id={0}",person)));
        if (role.ToLower() == "driver") {
            rpt.LoadLayout(Server.MapPath(@"~\Modules\Hr\Report\repx\PayrollSlip_driver.repx"));
        }
        else
        {
            rpt.LoadLayout(Server.MapPath(@"~\Modules\Hr\Report\repx\PayrollSlip.repx"));
        }
        DataSet set = HrPrint.PrintPaySlip(master, from, to);
        DataTable tab_mast = set.Tables[0];
        DataTable tab_det = set.Tables[1];
        DataTable tab_det1 = set.Tables[2];
        if (tab_det.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.PageHeaderBand header = rpt.Report.Bands["PageHeader"] as DevExpress.XtraReports.UI.PageHeaderBand;
            if (header != null)
            {

                DevExpress.XtraReports.UI.XRSubreport subReport_Ot = header.FindControl("Overtime_sub",true) as DevExpress.XtraReports.UI.XRSubreport;
                subReport_Ot.Name = "Overtime_sub";
                XtraReport rpt_Inv = new XtraReport();
                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Hr\Report\repx\PayrollSlip_sub.repx"));
                subReport_Ot.ReportSource = rpt_Inv;
                rpt_Inv.DataSource = tab_det;
            }
        }
        if (tab_det1.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.PageHeaderBand header = rpt.Report.Bands["PageHeader"] as DevExpress.XtraReports.UI.PageHeaderBand;
            if (header != null)
            {

                DevExpress.XtraReports.UI.XRSubreport subReport_Ot = header.FindControl("Overtime_sub2", true) as DevExpress.XtraReports.UI.XRSubreport;
                subReport_Ot.Name = "Overtime_sub2";
                XtraReport rpt_Inv = new XtraReport();
                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Hr\Report\repx\PayrollSlip_sub.repx"));
                subReport_Ot.ReportSource = rpt_Inv;
                rpt_Inv.DataSource = tab_det1;
            }
        }
        rpt.DataSource = set;
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    } 
}
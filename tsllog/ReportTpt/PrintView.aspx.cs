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

public partial class ReportFreightTpt_PrintView : System.Web.UI.Page
{
    private string docType = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["document"] == null) return;
            string reportName = Request.Params["document"].ToString();
            string no = Request.Params["no"].ToString();
            docType = SafeValue.SafeString(Request.Params["docType"], "0");
            MemoryStream ms = new MemoryStream();
            string fileName = "";
            string userName = HttpContext.Current.User.Identity.Name;
            try
            {
                if (reportName == "1")
                {
                    ms = PrintDn(no, userName);
                }
                if (reportName == "2")
                {
                    ms = PrintJobOrder(no, userName);
                }
                else if (reportName == "10")
                {
                    ms = PrintPl(no, userName);
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

    private MemoryStream PrintDn(string no,string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportTpt\repx\dn.repx"));
        rpt.DataSource = TptDocPrint.PrintDN(no,"TPT", userName);
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintJobOrder(string no, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportTpt\repx\JobOrder.repx"));
        rpt.DataSource = TptDocPrint.PrintJobOrder(no, "TPT");
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }

    private MemoryStream PrintPl(string refN, string userId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportTpt\repx\PL.repx"));
        DataSet set = TptDocPrint.PrintPlImport(refN, userId);
        DataTable tab_mast = set.Tables[0];
        DataTable tab_Inv = set.Tables[1];
        DataTable tab_Dn = set.Tables[2];
        DataTable tab_Cn = set.Tables[3];
        DataTable tab_Pl = set.Tables[4];
        DataTable tab_Vo = set.Tables[5];
        DataTable tab_Cost = set.Tables[6];

        if (tab_Inv.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Inv = rpt.Report.Bands["GroupFooter_Inv"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Inv = new XRSubreport();
            subReport_Inv.Name = "groupFooter_Inv";
            groupFooter_Inv.Controls.Add(subReport_Inv);
            XtraReport rpt_Inv = new XtraReport();
            rpt_Inv.LoadLayout(Server.MapPath(@"~\ReportTpt\repx\Pl_1_Inv.repx"));
            subReport_Inv.ReportSource = rpt_Inv;

            rpt_Inv.DataSource = tab_Inv;
        }
        if (tab_Dn.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Dn = rpt.Report.Bands["GroupFooter_Dn"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Dn = new XRSubreport();
            subReport_Dn.Name = "groupFooter_Dn";
            groupFooter_Dn.Controls.Add(subReport_Dn);
            XtraReport rpt_Dn = new XtraReport();
            rpt_Dn.LoadLayout(Server.MapPath(@"~\ReportTpt\repx\Pl_2_Dn.repx"));
            subReport_Dn.ReportSource = rpt_Dn;
            rpt_Dn.DataSource = tab_Dn;
        }

        if (tab_Cn.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Cn = rpt.Report.Bands["GroupFooter_Cn"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Cn = new XRSubreport();
            subReport_Cn.Name = "groupFooter_Cn";
            groupFooter_Cn.Controls.Add(subReport_Cn);
            XtraReport rpt_Cn = new XtraReport();
            rpt_Cn.LoadLayout(Server.MapPath(@"~\ReportTpt\repx\Pl_4_Cn.repx"));
            subReport_Cn.ReportSource = rpt_Cn;
            rpt_Cn.DataSource = tab_Cn;
        }

        if (tab_Pl.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Pl = rpt.Report.Bands["GroupFooter_Pl"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Pl = new XRSubreport();
            subReport_Pl.Name = "groupFooter_Pl";
            groupFooter_Pl.Controls.Add(subReport_Pl);
            XtraReport rpt_Pl = new XtraReport();
            rpt_Pl.LoadLayout(Server.MapPath(@"~\ReportTpt\repx\Pl_5_Pl.repx"));
            subReport_Pl.ReportSource = rpt_Pl;
            rpt_Pl.DataSource = tab_Pl;
        }
        if (tab_Vo.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Vo = rpt.Report.Bands["GroupFooter_Vo"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Vo = new XRSubreport();
            subReport_Vo.Name = "groupFooter_Vo";
            groupFooter_Vo.Controls.Add(subReport_Vo);
            XtraReport rpt_Vo = new XtraReport();
            rpt_Vo.LoadLayout(Server.MapPath(@"~\ReportTpt\repx\Pl_6_Vo.repx"));
            subReport_Vo.ReportSource = rpt_Vo;
            rpt_Vo.DataSource = tab_Vo;
        }
        if (tab_Cost.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Cost = rpt.Report.Bands["GroupFooter_Costing"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Cost = new XRSubreport();
            subReport_Cost.Name = "groupFooter_Cost";
            groupFooter_Cost.Controls.Add(subReport_Cost);
            XtraReport rpt_Cost = new XtraReport();
            rpt_Cost.LoadLayout(Server.MapPath(@"~\ReportTpt\repx\Pl_7_Cost.repx"));
            subReport_Cost.ReportSource = rpt_Cost;
            rpt_Cost.DataSource = tab_Cost;
        }

        rpt.DataSource = tab_mast;
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);
        return str;
    }
   
}

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

public partial class ReportFreightSea_PrintView : System.Web.UI.Page
{
    private string docType = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["document"] == null) return;
            string reportName = Request.Params["document"].ToString();
            string refN = Request.Params["master"].ToString();
            string jobN = Request.Params["house"].ToString();
            docType = SafeValue.SafeString(Request.Params["docType"], "0");
            //0 AWB
            //10 P&L
            //30: MAINFEST
            //31: DO
            //32:DN

            //50: LOADPLAN
            //51: BOOKING CONFIRMATION 
            //52: SHIPPING ORDER

            MemoryStream ms = new MemoryStream();
            string fileName = "";
            try
            {
                string userName = HttpContext.Current.User.Identity.Name;
                if (reportName == "0")//p&l
                {
                    ms = PrintOrgAWB(refN, jobN, userName);
                }
                if (reportName == "1")//p&l
                {
                    ms = PrintDraftAWB(refN, jobN, userName);
                }
                else if (reportName == "10")
                {
                    ms = PrintAirPL(refN,jobN, userName);
                    fileName = "";
                } 
                ///////////import
                else if (reportName == "30")
                {
                    ms = PrintManifest(refN);
                    fileName = "Import_DO_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "31")
                {
                    ms = PrintDO(jobN);
                    fileName = "Import_DO_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "32")
                {
                    ms = PrintDN(refN, jobN);
                    fileName = "Import_DN_" + DateTime.Now.ToString("yyyyMMdd");
                }

                //export sch
                else if (reportName == "50")
                {
                    ms = PrintLoadPlan(refN, "WH");
                }
                else if (reportName == "51")
                {
                    ms = PrintBkgConfirm(jobN);
                }
                else if (reportName == "52")
                {
                    ms = PrintSo(jobN);
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
            };
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
    private MemoryStream PrintOrgAWB(string refN, string jobN,string userName)
    {
        XtraReport rpt = new XtraReport();
        DataTable set = AirFreightDocPrint.PrintAWB(refN, jobN);
        if (jobN.Length > 2)
        {
            rpt.LoadLayout(Server.MapPath(@"~\ReportAir\repx\Air\HAWB_Org.repx"));
        }
        else
        {
            rpt.LoadLayout(Server.MapPath(@"~\ReportAir\repx\Air\MAWB_Org.repx"));
        }
        rpt.DataSource = set;
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintDraftAWB(string refN, string jobN, string userName)
    {
        XtraReport rpt = new XtraReport();
        DataTable set = AirFreightDocPrint.PrintAWB(refN, jobN);
        if (jobN.Length > 2)
        {
            rpt.LoadLayout(Server.MapPath(@"~\ReportAir\repx\Air\HAWB_Draft.repx"));
        }
        else
        {
            rpt.LoadLayout(Server.MapPath(@"~\ReportAir\repx\Air\MAWB_Draft.repx"));
        }
        rpt.DataSource = set;
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    #region P&L
    private MemoryStream PrintAirPL(string refN, string JobNo,string userId)
    {
        if (JobNo.Length < 2)
            return PrintPL_AirRef(refN, JobNo, userId);
        else
            return PrintPL_Airhouse(refN, JobNo, userId);
    }
    private MemoryStream PrintPL_AirRef(string refN, string jobN, string userId)
    {
        string user = HttpContext.Current.User.Identity.Name;
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAir\repx\Import\PL.repx"));
        DataSet set = AirFreightDocPrint.PrintPl_AirRef(refN, userId);
        DataTable tab_mast = set.Tables[0];
        DataTable tab_Inv = set.Tables[1];
        DataTable tab_Dn = set.Tables[2];
        DataTable tab_Ts = set.Tables[3];
        DataTable tab_Cn = set.Tables[4];
        DataTable tab_Pl = set.Tables[5];
        DataTable tab_Vo = set.Tables[6];
        DataTable tab_Cost = set.Tables[7];

        if (tab_Inv.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Inv = rpt.Report.Bands["GroupFooter_Inv"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Inv = new XRSubreport();
            subReport_Inv.Name = "groupFooter_Inv";
            groupFooter_Inv.Controls.Add(subReport_Inv);
            XtraReport rpt_Inv = new XtraReport();
            rpt_Inv.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_1_Inv.repx"));
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
            rpt_Dn.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_2_Dn.repx"));
            subReport_Dn.ReportSource = rpt_Dn;
            rpt_Dn.DataSource = tab_Dn;
        }

        if (tab_Ts.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Ts = rpt.Report.Bands["GroupFooter_Ts"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Ts = new XRSubreport();
            subReport_Ts.Name = "groupFooter_Ts";
            groupFooter_Ts.Controls.Add(subReport_Ts);
            XtraReport rpt_Ts = new XtraReport();
            rpt_Ts.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_3_Ts.repx"));
            subReport_Ts.ReportSource = rpt_Ts;
            rpt_Ts.DataSource = tab_Ts;
        }

        if (tab_Cn.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Cn = rpt.Report.Bands["GroupFooter_Cn"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Cn = new XRSubreport();
            subReport_Cn.Name = "groupFooter_Cn";
            groupFooter_Cn.Controls.Add(subReport_Cn);
            XtraReport rpt_Cn = new XtraReport();
            rpt_Cn.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_4_Cn.repx"));
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
            rpt_Pl.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_5_Pl.repx"));
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
            rpt_Vo.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_6_Vo.repx"));
            subReport_Vo.ReportSource = rpt_Vo;
            rpt_Vo.DataSource = tab_Vo;
        }
        if (tab_Cost.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Cost = rpt.Report.Bands["GroupFooter_Costing"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Cost = new XRSubreport();
            subReport_Cost.Name = "subReport_Cost";
            groupFooter_Cost.Controls.Add(subReport_Cost);
            XtraReport rpt_Cost = new XtraReport();
            rpt_Cost.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_7_Cost.repx"));
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
    private MemoryStream PrintPL_Airhouse(string refN, string jobN, string userId)
    {
        string user = HttpContext.Current.User.Identity.Name;
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAir\repx\Import\PL.repx"));
        DataSet set = AirFreightDocPrint.PrintPl_house(refN, jobN, userId);
        DataTable tab_mast = set.Tables[0];
        DataTable tab_Inv = set.Tables[1];
        DataTable tab_Dn = set.Tables[2];
        DataTable tab_Ts = set.Tables[3];
        DataTable tab_Cn = set.Tables[4];
        DataTable tab_Pl = set.Tables[5];
        DataTable tab_Vo = set.Tables[6];
        DataTable tab_Cost = set.Tables[7];

        if (tab_Inv.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Inv = rpt.Report.Bands["GroupFooter_Inv"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Inv = new XRSubreport();
            subReport_Inv.Name = "groupFooter_Inv";
            groupFooter_Inv.Controls.Add(subReport_Inv);
            XtraReport rpt_Inv = new XtraReport();
            rpt_Inv.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_1_Inv.repx"));
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
            rpt_Dn.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_2_Dn.repx"));
            subReport_Dn.ReportSource = rpt_Dn;
            rpt_Dn.DataSource = tab_Dn;
        }

        if (tab_Ts.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Ts = rpt.Report.Bands["GroupFooter_Ts"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Ts = new XRSubreport();
            subReport_Ts.Name = "groupFooter_Ts";
            groupFooter_Ts.Controls.Add(subReport_Ts);
            XtraReport rpt_Ts = new XtraReport();
            rpt_Ts.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_3_Ts.repx"));
            subReport_Ts.ReportSource = rpt_Ts;
            rpt_Ts.DataSource = tab_Ts;
        }

        if (tab_Cn.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Cn = rpt.Report.Bands["GroupFooter_Cn"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Cn = new XRSubreport();
            subReport_Cn.Name = "groupFooter_Cn";
            groupFooter_Cn.Controls.Add(subReport_Cn);
            XtraReport rpt_Cn = new XtraReport();
            rpt_Cn.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_4_Cn.repx"));
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
            rpt_Pl.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_5_Pl.repx"));
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
            rpt_Vo.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_6_Vo.repx"));
            subReport_Vo.ReportSource = rpt_Vo;
            rpt_Vo.DataSource = tab_Vo;
        }
        if (tab_Cost.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Cost = rpt.Report.Bands["GroupFooter_Costing"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Cost = new XRSubreport();
            subReport_Cost.Name = "subReport_Cost";
            groupFooter_Cost.Controls.Add(subReport_Cost);
            XtraReport rpt_Cost = new XtraReport();
            rpt_Cost.LoadLayout(Server.MapPath(@"~\ReportAir\repx\import\Pl_7_Cost.repx"));
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
    #endregion

    #region IMPORT

    private MemoryStream PrintManifest(string ImportN)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAir\repx\Import\rptImpManifest.repx"));
        rpt.DataSource = AirFreightDocPrint.PrintManifest(ImportN);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintDO(string ImportN)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAir\repx\Import\rptDO.repx"));
        rpt.DataSource = AirFreightDocPrint.dsDo(ImportN);

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintDN(string refN, string jobN)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAir\repx\Import\DN.repx"));

        rpt.DataSource = AirFreightDocPrint.PrintDN(refN, jobN);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);
        return str;
    }

    #endregion

    #region export SCH
    private MemoryStream PrintLoadPlan(string RefN, string reportType)
    {
        string user = HttpContext.Current.User.Identity.Name;
        XtraReport rpt = new XtraReport();

        rpt.LoadLayout(Server.MapPath(@"~\ReportAir\repx\Export\rptLoadPlan.repx"));
        //rpt.DataSource = AirFreightDocPrint.dsLoadPlan(RefN);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);
        return str;
    }

    private MemoryStream PrintBkgConfirm(string refId)
    {
        XtraReport rpt = new XtraReport();

        rpt.LoadLayout(Server.MapPath(@"~\ReportAir\repx\Export\BookingConfirm.repx"));
        rpt.DataSource = AirFreightDocPrint.PrintBookingConfirm(refId);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintSo(string refId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportAir\repx\Export\rptSo.repx"));
        rpt.DataSource = AirFreightDocPrint.dsSo(refId);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);
        return str;
    }
    #endregion

}

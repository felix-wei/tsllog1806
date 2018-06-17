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

public partial class Modules_Warehouse_PrintView : System.Web.UI.Page
{
    private string docType = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["document"] == null) return;
            docType = SafeValue.SafeString(Request.Params["docType"], "0");
            string reportName = Request.Params["document"].ToString().ToLower();
            string refN = Request.Params["master"].ToString();
            string jobN = "";
            if (Request.Params["house"] != null)
                jobN = Request.Params["house"].ToString();
            string refType = "";
            string userName = HttpContext.Current.User.Identity.Name;
            string oid = "";
            if (Request.Params["oid"] != null)
                oid = Request.Params["oid"].ToString();
            MemoryStream ms = new MemoryStream();
            string fileName = "";
            try
            {
                if (reportName == "iv"||reportName=="cn"||reportName=="dn")
                {
                    ms = PrintWh_invoice(refN, reportName, userName);
                    fileName = reportName + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "wh_po" || reportName == "wh_putaway" || reportName == "wh_so" || 
                    reportName == "wh_inv" || reportName == "wh_doout" || reportName == "wh_picking"
                    || reportName == "wh_shipdo" || reportName == "wh_Invoie" || reportName == "wh_tallysheet"
                    || reportName == "wh_stockcount" || reportName == "wh_stockmove")
                {
                    ms = PrintWh_doc(refN, reportName, userName);
                    fileName = reportName + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "wh_sq" || reportName == "wh_pq")
                {
                    ms = PrintWh_SQ(refN, reportName, userName);
                    fileName = reportName + DateTime.Now.ToString("yyyyMMdd");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
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

    private MemoryStream PrintWh_invoice(string refN, string refType, string userId)
    {
        string user = HttpContext.Current.User.Identity.Name;
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\invoice.repx"));


        string strsql = string.Format(@"exec proc_PrintInvoice_wh '{0}','{1}','{2}','{3}','{4}'", refN, "", refType, userId, "");
        DataSet ds_temp = ConnectSql.GetDataSet(strsql);
        DataTable Mast = ds_temp.Tables[0].Copy();
        Mast.TableName = "Mast";
        DataTable Detail = ds_temp.Tables[1].Copy();
        Detail.TableName = "Detail";
        DataTable Detail2 = new DataTable();
        if (ds_temp.Tables.Count > 2)
        {
            Detail2 = ds_temp.Tables[2].Copy();
            Detail2.TableName = "Detail";
        }


        if (Detail.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Inv = rpt.Report.Bands["GroupFooter_Wine"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Inv = new XRSubreport();
            subReport_Inv.Name = "GroupFooter_Wine1";
            groupFooter_Inv.Controls.Add(subReport_Inv);
            XtraReport rpt_Inv = new XtraReport();
            rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\invoice_1.repx"));
            subReport_Inv.ReportSource = rpt_Inv;
            rpt_Inv.DataSource = Detail;
        }
        if (Detail2.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Inv = rpt.Report.Bands["GroupFooter_Other"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Inv = new XRSubreport();
            subReport_Inv.Name = "GroupFooter_Other1";
            groupFooter_Inv.Controls.Add(subReport_Inv);
            XtraReport rpt_Inv = new XtraReport();
            rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\invoice_2.repx"));
            subReport_Inv.ReportSource = rpt_Inv;
            rpt_Inv.DataSource = Detail2;
        }

        rpt.DataSource = Mast;
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintWh_doc(string refN, string refType, string userId)
    {
        System.IO.MemoryStream str = new MemoryStream();
        string user = HttpContext.Current.User.Identity.Name;
        XtraReport rpt = new XtraReport();
        string path = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select path from sys_rpt where name='{0}'", refType)));
        if (path.Length == 0)
            return str;

            //rpt.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\do.repx"));

        rpt.LoadLayout(Server.MapPath(path));
       

        string strsql = string.Format(@"exec proc_PrintWh_Po '{0}','{1}','{2}','{3}','{4}'", refN, "", refType, userId, "");
        DataSet ds_temp = ConnectSql.GetDataSet(strsql);
        DataTable Mast = ds_temp.Tables[0].Copy();
        Mast.TableName = "Mast";
        DataTable Detail = ds_temp.Tables[1].Copy();
        Detail.TableName = "Detail";
        DataTable Detail2 = new DataTable();
        if (ds_temp.Tables.Count > 2)
        {
            Detail2 = ds_temp.Tables[2].Copy();
            Detail2.TableName = "Detail";
        }



        if (Detail.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Inv = rpt.Report.Bands["GroupFooter_Wine"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Inv = new XRSubreport();
            subReport_Inv.Name = "GroupFooter_Wine1";
            groupFooter_Inv.Controls.Add(subReport_Inv);
            XtraReport rpt_Inv = new XtraReport();
            if (refType == "wh_putaway" || refType == "wh_picking")
                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\Picking_1.repx"));
            else if (refType == "wh_doout")
                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\do_1.repx"));
            else if (refType == "wh_tallysheet")
                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\TallySheet_1.repx"));
            else if (refType == "wh_stockcount")
                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\StockCountSheet_1.repx"));
            else if (refType == "wh_stockmove")
                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\StockMove_1.repx"));
            else

                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\invoice_1.repx"));
            subReport_Inv.ReportSource = rpt_Inv;
            rpt_Inv.DataSource = Detail;
        }
        if (Detail2.Rows.Count > 0)
        {
            DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Inv = rpt.Report.Bands["GroupFooter_Other"] as DevExpress.XtraReports.UI.GroupFooterBand;
            DevExpress.XtraReports.UI.XRSubreport subReport_Inv = new XRSubreport();
            subReport_Inv.Name = "GroupFooter_Other1";
            groupFooter_Inv.Controls.Add(subReport_Inv);
            XtraReport rpt_Inv = new XtraReport();
            if (refType == "wh_putaway" || refType == "wh_picking")
                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\Picking_1.repx"));
            else if (refType == "wh_doout")
                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\do_1.repx"));
            else if (refType == "tallysheet")
                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\TallySheet_1.repx"));
            else if (refType == "wh_stockcount")
                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\StockCountSheet_1.repx"));
            else if (refType == "wh_stockmove")
                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\StockMove_1.repx"));
            else

                rpt_Inv.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\invoice_1.repx"));
            subReport_Inv.ReportSource = rpt_Inv;
            rpt_Inv.DataSource = Detail2;
        }

        rpt.DataSource = Mast;
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintWh_SQ(string refN, string refType, string userId)
    {
        System.IO.MemoryStream str = new MemoryStream();
        string user = HttpContext.Current.User.Identity.Name;
        XtraReport rpt = new XtraReport();
        string path = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select path from sys_rpt where name='{0}'", refType)));
        if (path.Length == 0)
            return str;
        //rpt.LoadLayout(Server.MapPath(@"~\Modules\Warehouse\repx\po.repx"));
        rpt.LoadLayout(Server.MapPath(path));

        string strsql = string.Format(@"exec proc_PrintWh_Po '{0}','{1}','{2}','{3}','{4}'", refN, "", refType, userId, "");
        DataSet ds_temp = ConnectSql.GetDataSet(strsql);
        DataTable Mast = ds_temp.Tables[0].Copy();
        Mast.TableName = "Mast";
        DataTable Detail = ds_temp.Tables[1].Copy();
        Detail.TableName = "Detail";
        DataSet set = new DataSet();
        set.Tables.Add(Mast);
        set.Tables.Add(Detail);
        set.Relations.Add("Rela", Mast.Columns["Relation"], Detail.Columns["Relation"]);


        rpt.DataSource = set;
        rpt.CreateDocument();
        rpt.ExportToPdf(str);

        return str;
    }
}

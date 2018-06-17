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

public partial class PagesContTrucking_Report_RptPrintView : System.Web.UI.Page
{

    private string docType = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["doc"] == null) return;
            string reportName = Request.Params["doc"].ToString();
            docType = SafeValue.SafeString(Request.Params["docType"], "0");
            string d1 =SafeValue.SafeString( Request.Params["d1"]);
            string d2 = SafeValue.SafeString(Request.Params["d2"]);
            string haulierCode = SafeValue.SafeString(Request.Params["h"], "");
            string party = SafeValue.SafeString(Request.Params["p"], "");
            string type = SafeValue.SafeString(Request.Params["type"], "");
            string refType = SafeValue.SafeString(Request.Params["refType"], "");
            string no = SafeValue.SafeString(Request.Params["no"], "");
            string id = SafeValue.SafeString(Request.Params["id"], "0");
            MemoryStream ms = new MemoryStream();
            string fileName = "";
            try
            {
                string userName = HttpContext.Current.User.Identity.Name;
                if (reportName == "1")
                {
                    ms = PrintIncentive(party, d1, d2);
                    fileName = "ReportIncentive_" + party + "_" + DateTime.Now.ToString("yyyyMMdd");
                }
                if (reportName == "2")
                {
                    ms = PrintClaims(party, d1, d2);
                    fileName = "ReportClaims_" + party + "_" + DateTime.Now.ToString("yyyyMMdd");
                }
                if (reportName == "3")
                {
                    ms = PrintIncentiveMonthly(party, d1, d2);
                    fileName = "ReportMonthly_" + party + "_" + DateTime.Now.ToString("yyyyMMdd");
                }
               if (reportName == "4")
                {
                    ms = PrintCfsImpHaulier(no);
                    fileName = "HaulierTruckingAdvice" + no;
                }
                if (reportName == "100")
                {
                    ms = PrintBillList(type, party, d1, d2, "DocDate", userName, refType);
                    fileName = "BillList" + DateTime.Now.ToString("yyyyMMdd");
                }
                if (reportName == "haulier")
                {
                    ms = PrintHaulier(no,refType);
                    fileName = "HaulierTrucking" + DateTime.Now.ToString("yyyyMMdd");
                }
                if (reportName == "hauliersub")
                {
                    ms = PrintHaulierSub(no,refType,haulierCode);
                    fileName = "HaulierTruckingContractor" + DateTime.Now.ToString("yyyyMMdd");
                }
                if (reportName == "delivery")
                {
                    ms = PrintDeliveryOrder(no, id,refType);
                    fileName = "Delivery" + DateTime.Now.ToString("yyyyMMdd");
                }
                if (reportName == "tallysheet_indented")
                {
                    ms = PrintTallySheetIndented(no, refType);
                    fileName = "TallySheet" + DateTime.Now.ToString("yyyyMMdd");
                }
                if (reportName == "tallysheet_confirmed")
                {
                    ms = PrintTallySheetConfirmed(no, refType, type);
                    fileName = "TallySheet" + DateTime.Now.ToString("yyyyMMdd");
                }
                if (reportName == "stock_tallysheet")
                {
                    ms = PrintStockTallySheet(no, refType, d1, d2);
                    fileName = "TallySheet" + DateTime.Now.ToString("yyyyMMdd");
                }
                if (reportName == "job_sheet")
                {
                    ms = PrintJobSheet(no, refType,id);
                    fileName = "TallySheet" + DateTime.Now.ToString("yyyyMMdd");
                }
                if (reportName == "TPT")
                {
                    ms = PrintDeliveryOrder(no,id, refType);
                    if (refType == "gr") {
                       
                        fileName = "Goods_Receipt_Note" + DateTime.Now.ToString("yyyyMMdd");
                    }
                    if (refType == "do")
                    {
                        fileName = "Delivery_Order" + DateTime.Now.ToString("yyyyMMdd");
                    }
                    if (refType == "tp")
                    {
                        fileName = "Transport_Instruction" + DateTime.Now.ToString("yyyyMMdd");
                    }
                    if (refType == "tr")
                    {
                        fileName = "Transfer_Instruction" + DateTime.Now.ToString("yyyyMMdd");
                    }
                    
                }
                if (reportName == "DN")
                {
                    ms = PrintDN(no, refType);
                    fileName = "DELIVERY_NOTE" + DateTime.Now.ToString("yyyyMMdd");
                }
                if (reportName == "Move")
                {
                    string sku = SafeValue.SafeString(Request.Params["sku"], "");
                    fileName = "MovementDetail_" + DateTime.Now.ToString("yyyyMMdd");
                    ms = PrintMovementDetail(no,sku);
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
    private MemoryStream PrintBillList(string billType, string party, string date1, string date2, string sortFiled, string userName, string refType)
    {
        XtraReport rpt = new XtraReport();
        if (billType == "VO" || billType == "PL")
        {
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\ReportRepx\Account\VoucherList.repx"));

            if (date1.IndexOf("/") != -1 && date2.IndexOf("/") != -1)
            {
                string[] s1 = date1.Split('/');
                string[] s2 = date2.Split('/');
                DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
                DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
                rpt.DataSource = SeaFreightRptPrint.dsVoucherByDate(billType, party, d1, d2, sortFiled, userName, refType);
            }
            else
            {
                rpt.DataSource = SeaFreightRptPrint.dsVoucherByNo(billType, party, date1, date2, sortFiled, userName, refType);
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
                rpt.DataSource = SeaFreightRptPrint.dsInvoiceByDate(billType, party, d1, d2, sortFiled, userName, refType);
            }
            else
            {
                rpt.DataSource = SeaFreightRptPrint.dsInvoiceByNo(billType, party, date1, date2, sortFiled, userName, refType);
            }
        }
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintIncentive(string driver, string dt_from, string dt_to)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\Incentive.repx"));
        string sql_where = string.Format("and DATEDIFF(d,ToDate,'{0}')<=0 and DATEDIFF(d,ToDate,'{1}')>=0", dt_from, dt_to);
        if (driver.Trim().Length > 0)
        {
            sql_where += " and det2.DriverCode='" + driver + "'";
        }
        string sql = string.Format(@"select det2.Id,det2.JobNo,Det1.ContainerNo,det1.ContainerType,det2.TowheadCode,det2.ChessisCode,det2.DriverCode,
det2.ToCode,det2.ToDate,det2.ToTime,det2.FromCode,det2.FromDate,det2.FromTime,convert(varchar(10),ToDate,103) as Date,FromTime+'-'+ToTime Time,
isnull(det2.Incentive1,0) as Incentive1,isnull(det2.Incentive2,0) as Incentive2,isnull(det2.Incentive3,0) as Incentive3,isnull(det2.Incentive4,0) as Incentive4,
isnull(Incentive1,0)+isnull(Incentive2,0)+isnull(Incentive3,0)+isnull(Incentive4,0) as TotalIncentive
from ctm_jobdet2 as det2
left outer join ctm_jobdet1 as det1 on det2.det1Id=det1.Id
where det2.Statuscode='C' {0}", sql_where);
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Detail";
        rpt.DataSource = dt;
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintCfsImpHaulier(string orderNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\HaulierTruckingAdvice_Imp.repx"));
        rpt.DataSource = DocPrint.PrintImpHaulier(orderNo);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintClaims(string driver, string dt_from, string dt_to)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\Claims.repx"));

        string sql_where = string.Format("and DATEDIFF(d,ToDate,'{0}')<=0 and DATEDIFF(d,ToDate,'{1}')>=0", dt_from, dt_to);
        if (driver.Trim().Length > 0)
        {
            sql_where += " and det2.DriverCode='" + driver + "'";
        }
        string sql = string.Format(@"select det2.Id,det2.JobNo,Det1.ContainerNo,det1.ContainerType,det2.TowheadCode,det2.ChessisCode,det2.DriverCode,
det2.ToCode,det2.ToDate,det2.ToTime,det2.FromCode,det2.FromDate,det2.FromTime,convert(varchar(10),ToDate,103) as Date,FromTime+'-'+ToTime Time,
isnull(det2.Charge1,0) as Charge1,isnull(det2.Charge2,0) as Charge2,isnull(det2.Charge3,0) as Charge3,isnull(det2.Charge4,0) as Charge4,isnull(det2.Charge5,0) as Charge5,
isnull(det2.Charge6,0) as Charge6,isnull(det2.Charge7,0) as Charge7,isnull(det2.Charge8,0) as Charge8,isnull(det2.Charge9,0) as Charge9,
isnull(Charge1,0)+isnull(Charge2,0)+isnull(Charge3,0)+isnull(Charge4,0)+isnull(Charge5,0)+isnull(Charge6,0)+isnull(Charge7,0)+isnull(Charge8,0)+isnull(Charge9,0) as TotalCharge
from ctm_jobdet2 as det2
left outer join ctm_jobdet1 as det1 on det2.det1Id=det1.Id
where det2.Statuscode='C' {0}", sql_where);
        DataTable dt = ConnectSql.GetTab(sql);
        rpt.DataSource = dt;
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintIncentiveMonthly(string driver, string dt_from, string dt_to)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\IncentiveMonthly.repx"));
        string sql_where = "";// string.Format("and DATEDIFF(d,ToDate,'{0}')<=0 and DATEDIFF(d,ToDate,'{1}')>=0", dt_from, dt_to);
        if (driver.Trim().Length > 0)
        {
            sql_where += " and det2.DriverCode='" + driver + "'";
        }
        string t_date = "";
        try
        {
            t_date = dt_from.Substring(6, 2) + "/" + dt_from.Substring(4, 2) + "/" + dt_from.Substring(0, 4);
            t_date += " - " + dt_to.Substring(6, 2) + "/" + dt_to.Substring(4, 2) + "/" + dt_to.Substring(0, 4);
        }
        catch (Exception ex) { }
        string sql = string.Format(@"select temp.*,driver.TowheaderCode as TowheadCode,Incentive1+Incentive2+Incentive3+Incentive4 as TotalIncentive,'{3}' as Date 
from (
select DriverCode,sum(isnull(det2.Incentive1,0)) as Incentive1,sum(isnull(det2.Incentive2,0)) as Incentive2,
sum(isnull(det2.Incentive3,0)) as Incentive3,sum(isnull(det2.Incentive4,0)) as Incentive4
from ctm_jobdet2 as det2 
where det2.Statuscode='C' and DATEDIFF(d,det2.ToDate,'{0}')<=0 and DATEDIFF(d,det2.ToDate,'{1}')>=0 {2}
group by DriverCode
) as temp
left outer join CTM_Driver as driver on temp.DriverCode=driver.Code ", dt_from, dt_to, sql_where, t_date);
        DataTable dt = ConnectSql.GetTab(sql);
        dt.TableName = "Detail";
        rpt.DataSource = dt;
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintHaulier(string orderNo,string jobType)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\HaulierTruckingAdvice.repx"));
        rpt.DataSource = DocPrint.PrintHaulier(orderNo,jobType);


        QR q=new QR();
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + orderNo + "'");
        C2.CtmJob job = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;
        string text = string.Format(@"JobNo:" + orderNo + ",JobDate:"+job.JobDate.ToString("dd/MM/yyyy"));
        Bitmap bt = q.Create_QR(text);
        string path = MapPath("~/files/barcode/");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        string fileName=orderNo+ ".png";
        string filePath = path + fileName;      
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        bt.Save(Server.MapPath("~/files/barcode/") + fileName);
        DevExpress.XtraReports.UI.XRPictureBox qr_code = rpt.Report.FindControl("barcode", true) as DevExpress.XtraReports.UI.XRPictureBox;
        if (qr_code != null)
        {
            qr_code.ImageUrl = "/files/barcode/" + fileName;
        }

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
	
	private MemoryStream PrintHaulierSub(string orderNo,string jobType, string haulierCode)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\HaulierTruckingAdviceSub.repx"));
        rpt.DataSource = DocPrint.PrintHaulierSub(orderNo,jobType, haulierCode);


        QR q=new QR();
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + orderNo + "'");
        C2.CtmJob job = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;
        string text = string.Format(@"JobNo:" + orderNo + ",JobDate:"+job.JobDate.ToString("dd/MM/yyyy"));
        Bitmap bt = q.Create_QR(text);
        string path = MapPath("~/files/barcode/");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        string fileName=orderNo+ ".png";
        string filePath = path + fileName;      
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        bt.Save(Server.MapPath("~/files/barcode/") + fileName);
        DevExpress.XtraReports.UI.XRPictureBox qr_code = rpt.Report.FindControl("barcode", true) as DevExpress.XtraReports.UI.XRPictureBox;
        if (qr_code != null)
        {
            qr_code.ImageUrl = "/files/barcode/" + fileName;
        }

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
	
    private MemoryStream PrintDelivery(string orderNo, string jobType)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\DeliveryOrder.repx"));
        rpt.DataSource = DocPrint.PrintHaulier(orderNo, jobType);


        QR q = new QR();
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + orderNo + "'");
        C2.CtmJob job = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;
        string text = string.Format(@"JobNo:" + orderNo + ",JobDate:" + job.JobDate.ToString("dd/MM/yyyy"));
        Bitmap bt = q.Create_QR(text);
        string path = MapPath("~/files/barcode/");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        string fileName = orderNo + ".png";
        string filePath = path + fileName;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        bt.Save(Server.MapPath("~/files/barcode/") + fileName);
        DevExpress.XtraReports.UI.XRPictureBox qr_code = rpt.Report.FindControl("barcode", true) as DevExpress.XtraReports.UI.XRPictureBox;
        if (qr_code != null)
        {
            qr_code.ImageUrl = "/files/barcode/" + fileName;
        }
        string sql_trip = string.Format(@"select top 1 Id from ctm_jobdet2 where JobNo='{0}'", orderNo);

        string tripId = ConnectSql_mb.ExecuteScalar(sql_trip);

        string Signature_Consignee = "";
        string Signature_Driver = "";

        string sql_signature = string.Format(@"select Id,FileType,FileName,FilePath,FileNote From CTM_Attachment where FileType='Signature' and RefNo=@RefNo and charindex(@sType, FileNote,0)>0 and TripId=@tripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@RefNo", orderNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@sType", "Consignee", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql_signature, list);
        if (dt.Rows.Count > 0)
        {
            Signature_Consignee = dt.Rows[0]["FilePath"].ToString();
        }
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@RefNo", orderNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@sType", "Driver", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
        dt = ConnectSql_mb.GetDataTable(sql_signature, list);
        if (dt.Rows.Count > 0)
        {
            Signature_Driver = dt.Rows[0]["FilePath"].ToString();
        }


        DevExpress.XtraReports.UI.XRPictureBox signature = rpt.Report.FindControl("signature", true) as DevExpress.XtraReports.UI.XRPictureBox;
        if (signature != null)
        {
            signature.ImageUrl = Signature_Consignee;
        }
        DevExpress.XtraReports.UI.XRPictureBox signature1 = rpt.Report.FindControl("signature1", true) as DevExpress.XtraReports.UI.XRPictureBox;
        if (signature1 != null)
        {
            signature1.ImageUrl = Signature_Driver;
        }
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintTallySheetIndented(string orderNo, string jobType)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\TallySheet.repx"));

        DataSet set = DocPrint.PrintTallySheet(orderNo, jobType, "Indented");
        DataTable Mast = set.Tables[0].Copy();
        Mast.TableName = "Mast";
        DataTable Detail = set.Tables[1].Copy();
        Detail.TableName = "Details";
        //if (Detail.Rows.Count > 0)
        //{
        //    DevExpress.XtraReports.UI.DetailReportBand details = rpt.Report.Bands["DetailReport"] as DevExpress.XtraReports.UI.DetailReportBand;
        //    if (details != null)
        //    {
        //        DevExpress.XtraReports.UI.DetailBand gridLine_sub = details.Bands["Detail"] as DevExpress.XtraReports.UI.DetailBand;
        //        DevExpress.XtraReports.UI.XRSubreport subReport_Line = new XRSubreport();
        //        subReport_Line.Name = "GridLine_sub";
        //        gridLine_sub.Controls.Add(subReport_Line);
        //        XtraReport rpt_Inv = new XtraReport();
        //        rpt_Inv.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\TallySheet_sub.repx"));
        //        subReport_Line.ReportSource = rpt_Inv;
        //        rpt_Inv.DataSource = Detail;
        //    }
        //}
        rpt.DataSource = set;
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintTallySheetConfirmed(string orderNo, string jobType,string type)
    {
        XtraReport rpt = new XtraReport();
        if (type == "IN")
        {
            rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\TallySheet_IN.repx"));
        }
        if (type == "OUT")
        {
            rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\TallySheet_OUT.repx"));
        }
        DataSet set = DocPrint.PrintTallySheet(orderNo, jobType, type);
        //DataTable Mast = set.Tables[0].Copy();
        //Mast.TableName = "Mast";
        //DataTable Detail = set.Tables[1].Copy();
        //Detail.TableName = "Details";
        //if (Detail.Rows.Count > 0)
        //{
        //    DevExpress.XtraReports.UI.DetailReportBand details = rpt.Report.Bands["DetailReport"] as DevExpress.XtraReports.UI.DetailReportBand;
        //    if (details != null)
        //    {
        //        DataTable Det = set.Tables[2].Copy();
        //        Det.TableName = "Det";
        //        DevExpress.XtraReports.UI.DetailReportBand detail = details.Bands["DetailReport1"] as DevExpress.XtraReports.UI.DetailReportBand;
        //        if (detail != null)
        //        {
        //            DevExpress.XtraReports.UI.DetailBand det = detail.Bands["Detail1"] as DevExpress.XtraReports.UI.DetailBand;
        //            if (det != null)
        //            {
        //                DevExpress.XtraReports.UI.XRSubreport subReport_Line = new XRSubreport();
        //                subReport_Line.Name = "gridLine_sub";
        //                det.Controls.Add(subReport_Line);
        //                XtraReport rpt_Inv = new XtraReport();
        //                rpt_Inv.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\TallySheet_IN_Sub.repx"));
        //                subReport_Line.ReportSource = rpt_Inv;
        //                rpt_Inv.DataSource = Det;
        //            }
        //        }
        //    }
        //}
        rpt.DataSource = set;
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintStockTallySheet(string orderNo, string jobType, string date1, string date2)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\StockTallySheet.repx"));
        DataSet set = null;
        if (date1.Length > 0 && date2.Length > 0)
        {
            string[] s1 = date1.Split('/');
            string[] s2 = date2.Split('/');
            DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));
            DateTime d2 = new DateTime(SafeValue.SafeInt(s2[2], 0), SafeValue.SafeInt(s2[1], 0), SafeValue.SafeInt(s2[0], 0));
            
            if (date1 != null && date2 != null)
            {
                set = DocPrint.PrintStockTallySheet(orderNo, d1, d2);
            }
        }
        else
        {
            set = DocPrint.PrintJobStockTallySheet(orderNo, jobType);
        }
        rpt.DataSource = set;
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintDeliveryOrder(string orderNo,string id, string jobType)
    {

        XtraReport rpt = new XtraReport();
        string hardCopy = "";
        if (SafeValue.SafeInt(id, 0) > 0)
        {
            rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\DO.repx"));
           
            string sql = string.Format(@"select epodHardCopy from ctm_jobdet2 where Id={0}", id);
            hardCopy = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
            if (hardCopy == "HardCopy")
            {
                rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\DO_HardCopy.repx"));
            }
            set_signed_barcode_byid(rpt, orderNo, id, "", "");
        }
        else if (jobType == "tr")
        {
            rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\Transfer.repx"));
        }
        else if (jobType.ToLower() == "cra")
        {
            rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\DeliveryOrder_CRA.repx"));
            if (hardCopy == "HardCopy")
            {
                rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\DO_HardCopy.repx"));
                set_signed_barcode_byid(rpt, orderNo, id, "", "");
            }
        }
        else
        {      
            string path1 = string.Format("~/files/photos/");
            string path2 = path1.Replace(' ', '_').Replace('\'', '_');
            string pathx = path2.Substring(1);
            string path3 = MapPath(path2);
            string filename = string.Format(@"{0}.jpg",orderNo);
            if (!Directory.Exists(path3))
                Directory.CreateDirectory(path3);
            string p = string.Format(@"~\files\photos\{0}", filename);

            string e_file = HttpContext.Current.Server.MapPath(p);
            DateTime now = DateTime.Now;
            string file = string.Format(@"~\html\{0}", jobType.ToUpper());
            string htmlName = string.Format(@"{0}.html", orderNo);
            string httpPath = HttpContext.Current.Request.Url.Host.ToString() + "/html/" + jobType.ToUpper() + "/" + htmlName;
            XtraReport rpt_barcode = new XtraReport();
            if (jobType.ToLower() == "tp")
            {
                rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\TransportOrder.repx"));
            }
            else
            {
                rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\DO.repx"));

                
            }


            if (SafeValue.SafeInt(id, 0) > 0)
            {
                rpt_barcode.DataSource = DocPrint.PrintDeliveryTrip(orderNo, id, jobType);
                set_signed_barcode_byid(rpt_barcode, orderNo, id, httpPath, filename);
            }
            else
            {
                rpt_barcode.DataSource = DocPrint.PrintDeliveryOrder(orderNo, jobType);
                set_signed_barcode(rpt_barcode, orderNo, httpPath, filename);
            }
            
            rpt_barcode.CreateDocument();
            rpt_barcode.ExportToImage(e_file);

            Dictionary<string, string> d = new Dictionary<string, string>();
            string http_Photo_Path ="http://"+ HttpContext.Current.Request.Url.Host.ToString() + "/files/photos/" + filename;
            string value = string.Format(@"<img src='{0}' alt=''/>", http_Photo_Path);
            d.Add("title", orderNo);
            d.Add("content", value);

            string temp =string.Format(@"~\html\template.html");

            
            html.CreateHtml(temp, file, htmlName, d, "");
            if (jobType.ToLower() == "tp")
            {
                rpt_barcode.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\TransportOrder.repx"));
            }
            else
            {
                rpt_barcode.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\DO.repx"));
            }
            if (SafeValue.SafeInt(id, 0) > 0)
            {
                set_signed_barcode_byid(rpt, orderNo, id, httpPath, filename);
            }
            else {
                set_signed_barcode(rpt, orderNo, httpPath, filename);
            }
        }
        if (SafeValue.SafeInt(id, 0) > 0)
        {
            rpt.DataSource = DocPrint.PrintDeliveryTrip(orderNo, id, jobType);
            
        }
        else
        {
            rpt.DataSource = DocPrint.PrintDeliveryOrder(orderNo, jobType);
        }
        System.IO.MemoryStream str = new MemoryStream();
        rpt.CreateDocument();
        rpt.ExportToPdf(str);


        return str;
    }
    private MemoryStream PrintJobSheet(string orderNo, string jobType,string tripId)
    {
        XtraReport rpt = new XtraReport();

        string path1 = string.Format("~/files/photos/");
        string path2 = path1.Replace(' ', '_').Replace('\'', '_');
        string pathx = path2.Substring(1);
        string path3 = MapPath(path2);
        string filename = string.Format(@"{0}.jpg", orderNo);
        if (!Directory.Exists(path3))
            Directory.CreateDirectory(path3);
        string p = string.Format(@"~\files\photos\{0}", filename);

        string e_file = HttpContext.Current.Server.MapPath(p);
        DateTime now = DateTime.Now;
        string file = string.Format(@"~\html\{0}", jobType.ToUpper());
        string htmlName = string.Format(@"{0}.html", orderNo);
        string httpPath = HttpContext.Current.Request.Url.Host.ToString() + "/html/" + jobType.ToUpper() + "/" + htmlName;
        XtraReport rpt_barcode = new XtraReport();
        rpt_barcode.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\DeliveryOrder_CRA.repx"));
        rpt_barcode.DataSource = DocPrint.PrintJobSheet(orderNo, jobType, tripId);
        set_signed_barcode(rpt_barcode, orderNo, httpPath, filename);
        rpt_barcode.CreateDocument();
        rpt_barcode.ExportToImage(e_file);

        Dictionary<string, string> d = new Dictionary<string, string>();
        string http_Photo_Path = "http://" + HttpContext.Current.Request.Url.Host.ToString() + "/files/photos/" + filename;
        string value = string.Format(@"<img src='{0}' alt=''/>", http_Photo_Path);
        d.Add("title", orderNo);
        d.Add("content", value);

        string temp = string.Format(@"~\html\template.html");


        html.CreateHtml(temp, file, htmlName, d, "");
        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\DeliveryOrder_CRA.repx"));
        string sql = string.Format(@"select epodHardCopy from ctm_jobdet2 where Id={0}", tripId);
        string hardCopy = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (hardCopy == "HardCopy")
        {
            rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\DO_HardCopy.repx"));
        }

        set_signed_barcode_byid(rpt, orderNo,tripId, httpPath, filename);

        DataSet set = DocPrint.PrintJobSheet(orderNo, jobType, tripId);
        DataTable Mast = set.Tables[0].Copy();
        Mast.TableName = "Mast";
        if (Mast.Rows.Count > 0)
        {
            string epodCB1 = SafeValue.SafeString(Mast.Rows[0]["EpodCB1"]);
            string epodCB2 = SafeValue.SafeString(Mast.Rows[0]["EpodCB2"]);
            string epodCB3 = SafeValue.SafeString(Mast.Rows[0]["EpodCB3"]);
            string epodCB4 = SafeValue.SafeString(Mast.Rows[0]["EpodCB4"]);
            string epodCB5 = SafeValue.SafeString(Mast.Rows[0]["EpodCB5"]);
            string epodCB6 = SafeValue.SafeString(Mast.Rows[0]["EpodCB6"]);
            DevExpress.XtraReports.UI.DetailBand details = rpt.Report.Bands["detailBand1"] as DevExpress.XtraReports.UI.DetailBand;
            if (details != null)
            {
                DevExpress.XtraReports.UI.XRCheckBox ckb1 = details.FindControl("ckb1",false) as DevExpress.XtraReports.UI.XRCheckBox;
                if (epodCB1 == "Yes")
                    ckb1.Checked = true;
                DevExpress.XtraReports.UI.XRCheckBox ckb2 = details.FindControl("ckb2", false) as DevExpress.XtraReports.UI.XRCheckBox;
                if (epodCB2 == "Yes")
                    ckb2.Checked = true;
                DevExpress.XtraReports.UI.XRCheckBox ckb3 = details.FindControl("ckb3", false) as DevExpress.XtraReports.UI.XRCheckBox;
                if (epodCB3 == "Yes")
                    ckb3.Checked = true;
                DevExpress.XtraReports.UI.XRCheckBox ckb4 = details.FindControl("ckb4", false) as DevExpress.XtraReports.UI.XRCheckBox;
                if (epodCB4 == "Yes")
                    ckb4.Checked = true;
                DevExpress.XtraReports.UI.XRCheckBox ckb5 = details.FindControl("ckb5", false) as DevExpress.XtraReports.UI.XRCheckBox;
                if (epodCB5 == "Yes")
                    ckb5.Checked = true;
                DevExpress.XtraReports.UI.XRCheckBox ckb6 = details.FindControl("ckb6", false) as DevExpress.XtraReports.UI.XRCheckBox;
                if (epodCB6 == "Yes")
                    ckb6.Checked = true;

            }
        }
        rpt.DataSource = set;

        System.IO.MemoryStream str = new MemoryStream();
        rpt.CreateDocument();
        rpt.ExportToPdf(str);


        return str;
    } 
    private void set_signed_barcode(XtraReport rpt, string orderNo, string httpPath,string filename)
    {
        QR q = new QR();
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + orderNo + "'");
        C2.CtmJob job = C2.Manager.ORManager.GetObject(query) as C2.CtmJob;
        //string text = string.Format(@"JobNo:" + orderNo);

        Bitmap bt = q.Create_QR(httpPath);
        string path = MapPath("~/files/barcode/");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        string fileName = orderNo + ".png";
        string filePath = path + fileName;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        bt.Save(Server.MapPath("~/files/barcode/") + fileName);

        DevExpress.XtraReports.UI.XRPictureBox qr_code = rpt.Report.FindControl("barcode", true) as DevExpress.XtraReports.UI.XRPictureBox;
        if (qr_code != null)
        {
            qr_code.ImageUrl = "/files/barcode/" + fileName;
        }
        string sql_trip = string.Format(@"select top 1 Id from ctm_jobdet2 where JobNo='{0}'", orderNo);

        string tripId = ConnectSql_mb.ExecuteScalar(sql_trip);

        string Signature_Consignee = "";
        string Signature_Driver = "";
        string signature_time = "";
        string signature_time1 = "";

        string sql_signature = string.Format(@"select Id,FileType,FileName,FilePath,FileNote,CreateDateTime From CTM_Attachment where FileType='Signature' and RefNo=@RefNo and charindex(@sType, FileNote,0)>0 and TripId=@tripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@RefNo", orderNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@sType", "Consignee", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql_signature, list);
        if (dt.Rows.Count > 0)
        {

            Signature_Consignee = dt.Rows[0]["FilePath"].ToString();

            signature_time = dt.Rows[0]["CreateDateTime"].ToString();
        }
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@RefNo", orderNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@sType", "Driver", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", tripId, SqlDbType.Int));
        dt = ConnectSql_mb.GetDataTable(sql_signature, list);
        if (dt.Rows.Count > 0)
        {
            Signature_Driver = dt.Rows[0]["FilePath"].ToString();
            signature_time1 = dt.Rows[0]["CreateDateTime"].ToString();
        }
        DevExpress.XtraReports.UI.XRPictureBox signature = rpt.Report.FindControl("signature", true) as DevExpress.XtraReports.UI.XRPictureBox;
        if (signature != null)
        {
            signature.ImageUrl = Signature_Consignee;
        }
        DevExpress.XtraReports.UI.XRLabel time = rpt.Report.FindControl("lbl_time", true) as DevExpress.XtraReports.UI.XRLabel;
        if (time != null)
        {
            time.Text = signature_time;
        }
        DevExpress.XtraReports.UI.XRPictureBox signature1 = rpt.Report.FindControl("signature1", true) as DevExpress.XtraReports.UI.XRPictureBox;
        if (signature1 != null)
        {
            signature1.ImageUrl = Signature_Driver;
        }
        DevExpress.XtraReports.UI.XRLabel time1 = rpt.Report.FindControl("lbl_time1", true) as DevExpress.XtraReports.UI.XRLabel;
        if (time1 != null)
        {
            time1.Text = signature_time1;
        }
    }
    private void set_signed_barcode_byid(XtraReport rpt, string orderNo, string id, string httpPath, string filename)
    {
        QR q = new QR();
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJobDet2), "Id=" + id + "");
        C2.CtmJobDet2 job = C2.Manager.ORManager.GetObject(query) as C2.CtmJobDet2;
        //string text = string.Format(@"JobNo:" + orderNo);

        Bitmap bt = q.Create_QR(httpPath);
        string path = MapPath("~/files/barcode/");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        string fileName = id + ".png";
        string filePath = path + fileName;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        bt.Save(Server.MapPath("~/files/barcode/") + fileName);

        DevExpress.XtraReports.UI.XRPictureBox qr_code = rpt.Report.FindControl("barcode", true) as DevExpress.XtraReports.UI.XRPictureBox;
        if (qr_code != null)
        {
            qr_code.ImageUrl = "/files/barcode/" + fileName;
        }

        string Signature_Consignee = "";
        string Signature_Driver = "";
        string signature_time = "";
        string signature_time1 = "";

        string sql_signature = string.Format(@"select Id,FileType,FileName,FilePath,FileNote,CreateDateTime From CTM_Attachment where FileType='Signature' and RefNo=@RefNo and charindex(@sType, FileNote,0)>0 and TripId=@tripId");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@RefNo", orderNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@sType", "Consignee", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", id, SqlDbType.Int));
        DataTable dt = ConnectSql_mb.GetDataTable(sql_signature, list);
        if (dt.Rows.Count > 0)
        {
            if (job.EpodHardCopy == "HardCopy")
                Signature_Consignee = dt.Rows[0]["FilePath"].ToString().Replace(@"/500", "");
            else {
                Signature_Consignee = dt.Rows[0]["FilePath"].ToString();
            }
            signature_time = dt.Rows[0]["CreateDateTime"].ToString();
        }
        list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@RefNo", orderNo, SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@sType", "Driver", SqlDbType.NVarChar, 100));
        list.Add(new ConnectSql_mb.cmdParameters("@tripId", id, SqlDbType.Int));
        dt = ConnectSql_mb.GetDataTable(sql_signature, list);
        if (dt.Rows.Count > 0)
        {
            Signature_Driver = dt.Rows[0]["FilePath"].ToString();
            signature_time1 = dt.Rows[0]["CreateDateTime"].ToString();
        }
        DevExpress.XtraReports.UI.XRPictureBox signature = rpt.Report.FindControl("signature", true) as DevExpress.XtraReports.UI.XRPictureBox;
        if (signature != null)
        {
            signature.ImageUrl = Signature_Consignee;
        }
        DevExpress.XtraReports.UI.XRLabel time = rpt.Report.FindControl("lbl_time", true) as DevExpress.XtraReports.UI.XRLabel;
        if (time != null)
        {
            time.Text = signature_time;
        }
        DevExpress.XtraReports.UI.XRPictureBox signature1 = rpt.Report.FindControl("signature1", true) as DevExpress.XtraReports.UI.XRPictureBox;
        if (signature1 != null)
        {
            signature1.ImageUrl = Signature_Driver;
        }
        DevExpress.XtraReports.UI.XRLabel time1 = rpt.Report.FindControl("lbl_time1", true) as DevExpress.XtraReports.UI.XRLabel;
        if (time1 != null)
        {
            time1.Text = signature_time1;
        }
    }
    private MemoryStream PrintDN(string orderNo, string jobType)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\Receipt_A4_2014.repx"));
        rpt.DataSource = DocPrint.DsReceipt(orderNo);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintMovementDetail(string no,string sku)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\PagesContTrucking\Report\repx\MovementDetail.repx"));

        rpt.DataSource = DocPrint.PrintMovementDetail(no,sku);
        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "1")
            rpt.ExportToXls(str);
        else
            rpt.ExportToPdf(str);

        return str;
    }
}

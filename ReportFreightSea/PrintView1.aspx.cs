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

public partial class ReportFreightSea_PrintView1 : System.Web.UI.Page
{
    private string docType = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["document"] == null) return;
            docType = SafeValue.SafeString(Request.Params["docType"], "0");
            string reportName = Request.Params["document"].ToString();
            string refN = Request.Params["master"].ToString();
            string jobN = Request.Params["house"].ToString();
            string oid = SafeValue.SafeString(Request.Params["oid"]);

            //0: Quotation
            //00: Quotation
            //////////////////////////////////// import ref
            //1: import letter and manifest
            //2: haulier
            //3.d/o w/o Permit carrier
            //4.d/o w/o Permit Nvocc
            //5.Auth Letter carrier
            //6.Auth Letter  nvocc
            //7.permitlist carrier
            //8.permitlist nvocc
            //9.Tranship list
            //10.import P&L
            //11.batch print do
            //12.batch print invoice
            //13.conver page

            ////////////////////////////////////import
            //30.D/O
            //31.PreAdvise
            //32.DN
            //33.ArrivalNotice
            //34.ArrivalNotice for agent

            ////////////////////////////////////export sch
            //50: loadplan
            //51: booking confirmation
            //52: shipping order

            //////////////////////////////////// export  ref
            //60: oceanbl(carrier)
            //61.PermitList (carrier)
            //62.PermitList (Nvocc)
            //63.Haulier
            //64.PreAdvise
            //65 export P&L
            //66.batch print bl
            //67.batch print invoice
            //68: oceanbl(Nvocc)

            ////////////////////////////////////export
            //70.bl
            //71.Draft BL
            //72.export permit
            //73.haulier Sheet
            //74.commercial
            //75.packing
            //76.shippingrequest

            MemoryStream ms = new MemoryStream();
            string fileName = "";
            try
            {
                string userName = HttpContext.Current.User.Identity.Name;
                if (reportName == "0")
                {
                    ms = PrintQuotation(refN, userName);
                    fileName = "Quotation" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "00")
                {
                    ms = PrintQuote(refN, userName);
                    fileName = "Quotation" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "100")
                {
                    ms = PrintQuote_Fcl(refN, userName);
                    fileName = "Quotation" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "101")
                {
                    ms = PrintQuote_Lcl(refN, userName);
                    fileName = "QuotationLcl" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "200")
                {
                    ms = PrintQuote_AirFcl(refN, userName);
                    fileName = "Quotation" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "201")
                {
                    ms = PrintQuote_AirLcl(refN, userName);
                    fileName = "QuotationLcl" + DateTime.Now.ToString("yyyyMMdd");
                }

                else if (reportName == "1")
                {
                    ms = PrintLetter(refN, userName);
                    fileName = "Import_Manifest_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "2")
                {
                    ms = PrintImpHaulier(refN, "0");
                    fileName = "Import_Haulier_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "3")//permit carrier
                {
                    ms = PrintImpPermit(refN, "1");
                    fileName = "Import_Permit_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "4")//nvocc permit
                {
                    ms = PrintImpPermit(refN, "2");
                    fileName = "Import_Permit_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "5")//auth letter
                {
                    ms = PrintAuthLetter(refN, "1");
                    fileName = "Import_Auth_Letter" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "6")
                {
                    ms = PrintAuthLetter(refN, "2");
                    fileName = "Import_Auth_Letter" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "7")
                {
                    ms = PrintPermitList(refN, "1");
                    fileName = "Import_Permit_List_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "8")
                {
                    ms = PrintPermitList(refN, "2");
                    fileName = "Import_Permit_List_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "9")
                {
                    ms = PrintTs(refN);
                    fileName = "Transship_Bkg_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "10" || reportName == "35")//p&l
                {
                    ms = PrintPL(refN,jobN, "SI", userName);
                    fileName = "";
                }
                else if (reportName == "11")//bath print DO
                {
                    ms = PrintBatchDo(refN, userName);
                    fileName = "";
                }
                else if (reportName == "12")//bath print Import Invoice
                {
                    ms = PrintBatchInvoice(refN, "I", userName);
                    fileName = "";
                }
                else if (reportName == "13")//conver page
                {
                    ms = PrintConverPage(refN, "0");
                    fileName = "Import_CoverPage_" + DateTime.Now.ToString("yyyyMMdd");
                }






                ///////////import
                else if (reportName == "30")
                {
                    ms = PrintDO(jobN);
                    fileName = "Import_DO_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "31")
                {
                    ms = PrintAdvise(refN, jobN);
                    fileName = "Import_PreAdvise_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "32")
                {
                    ms = PrintDN(refN, jobN,"0");
                    fileName = "Import_DN_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "32a")
                {
                    ms = PrintDN(refN, jobN,oid);
                    fileName = "Import_DN_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "33")
                {
                    ms = PrintArrivalNotice(refN, jobN);
                    fileName = "Arrival_Notice_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "34")
                {
                    ms = PrintArrivalNoticeAg(refN, jobN);
                    fileName = "Arrival_Notice_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "36")
                {
                    ms = PrintCertificate(refN, jobN,"SI",userName);
                    fileName = "Certificate_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "37") //for imf
                {
                    ms = PrintJobOrder1(refN, jobN, "SI", userName);
                    fileName = "JobOrder_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "38")//Import JobOrder for skk
                {
                    ms = PrintJobOrder(jobN,"SI");
                    fileName = "Import_JobOrder_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "39")//for imf
                {
                    ms = PrintJobOrder1(refN, jobN, "SE", userName);
                    fileName = "JobOrder_" + DateTime.Now.ToString("yyyyMMdd");
                }

                //export sch
                else if (reportName == "50")
                {
                    ms = PrintLoadPlan(refN, "WH");
                }
                else if (reportName == "51")
                {
                    ms = PrintBkgConfirm(refN, jobN);
                }
                else if (reportName == "52")
                {
                    ms = PrintSo(refN, jobN);
                }

                // export ref
                else if (reportName == "60")
                {
                    ms = PrintOceanBl(refN);
                }
                else if (reportName == "68")
                {
                    ms = PrintOceanBlNvocc(refN);
                }
                else if (reportName == "60m")
                {
                    ms = PrintOceanBl_multiple(refN);
                }
                else if (reportName == "68m")
                {
                    ms = PrintOceanBlNvocc_multiple(refN);
                }
                else if (reportName == "61")
                {
                    ms = PrintExpPermitList(refN, "0");//permit carrier
                }
                else if (reportName == "62")
                {
                    ms = PrintExpPermitList(refN, "1");//permit nvocc
                }
                else if (reportName == "63")
                {
                    ms = PrintExpHaulier(refN, "0");
                }
                else if (reportName == "64")
                {
                    ms = PrintExpPreAdvise(refN);
                }
                else if (reportName == "65")
                {
                    ms = PrintPL(refN, jobN, "SE", userName);
                    fileName = "";
                }
                else if (reportName == "66")//bath print Import Invoice
                {
                    ms = PrintBatchBL(refN, userName);
                    fileName = "";
                }
                else if (reportName == "67")//bath print Import Invoice
                {
                    ms = PrintBatchInvoice(refN, "E", userName);
                    fileName = "";
                }
                else if (reportName == "69")//cargo manifest
                {
                    ms = PrintCargoManifest(refN, "E", userName);
                    fileName = "";
                }

                //export
                else if (reportName == "70")
                {
                    ms = PrintBL(refN, jobN);
                }
                else if (reportName == "71")
                {
                    ms = PrintDraftBL(refN, jobN);
                }
                else if (reportName == "72")
                {
                    ms = PrintExpPermit(refN, jobN);
                }
                else if (reportName == "73")
                {
                    ms = PrintExpHaulier(refN, jobN);
                }
                else if (reportName == "74")//Commercial
                {
                    ms = PrintCommercial(jobN);
                    fileName = "Export_Commercial_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "75")//packing
                {
                    ms = PrintPacking(jobN);
                    fileName = "Export_Packing_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "76")//shippingrequest
                {
                    ms = PrintShippingRequest(jobN);
                    fileName = "Export_ShippingRequest_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "77")//Export Job Order
                {
                    ms = PrintJobOrder(jobN,"SE");
                    fileName = "Export_JobOrder_" + DateTime.Now.ToString("yyyyMMdd");
                }
                else if (reportName == "78")
                {
                    ms = PrintCertificate(refN, jobN, "SE", userName);
                    fileName = "Certificate_" + DateTime.Now.ToString("yyyyMMdd");
                }


                //else if (reportName == "170")
                //{
                //    ms = PrintAWB(refN, "", "Master", "Org");
                //}
                //else if (reportName == "170a")
                //{
                //    ms = PrintAWB(refN, "", "Master", "Draft");
                //}
                //else if (reportName == "171")
                //{
                //    ms = PrintAWB(refN, jobN, "House", "Org");
                //}
                //else if (reportName == "171a")
                //{
                //    ms = PrintAWB(refN, jobN, "House", "Draft");
                //}
                //else if (reportName == "172")
                //{
                //    ms = PrintAirImportPL(refN, userName);
                //}
                //else if (reportName == "172a")
                //{
                //    ms = PrintAirPL_house(refN, jobN, userName);
                //}
                //else if (reportName == "173")
                //{
                //    ms = PrintAirExportPL(refN, userName);
                //}
                //else if (reportName == "173a")
                //{
                //    ms = PrintAirPL_house(refN, jobN, userName);
                //}
                //else if (reportName == "54")//exp manifest
                //{
                //    ms = PrintExportManifest(refN);
                //}
                //else if (reportName == "55")
                //{
                //    ms = PrintSOCombine(refN);
                //}


                else if (reportName == "80")
                {
                    ms = PrintSeaQuotation_Detail(refN, userName);
                    fileName = "Quotation" + DateTime.Now.ToString("yyyyMMdd");
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            ExChange(ms, fileName);
        }
    }
    private void GenerateRpt(string reportName,string refN,string jobN,string userName)
    {

        MemoryStream ms = new MemoryStream();
        string fileName = "";

        string pdfName = "";
        XtraReport rpt = new XtraReport();
        string sql = string.Format("SELECT Name, RepxPath, Sql1, Sql2, RelationField, ReportIndex, BatchSql FROM ref_rpt1 where ReportType='{0}' and ReportId='{1}' order by ReportIndex", "SI", reportName);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {

            string repxPath = Server.MapPath(tab.Rows[i]["RepxPath"].ToString());
            string name = tab.Rows[i]["Name"].ToString();
            if (i == 0)
                pdfName = name;
            string sql1 = string.Format(SafeValue.SafeSqlString(tab.Rows[i]["Sql1"]), refN, jobN, userName);
            string sql2 = string.Format(SafeValue.SafeSqlString(tab.Rows[i]["Sql2"]), refN, jobN, userName);
            string relaField = SafeValue.SafeString(tab.Rows[i]["RelationField"]);
            DataSet set = new DataSet();
            DataTable mast = C2.Manager.ORManager.GetDataSet(sql1).Tables[0];
            DataTable det = new DataTable();
            string sql_sub = string.Format("SELECT Field, Sql, IsAdd,IsMastSql FROM ref_rpt2 where Name='{0}'", name);
            DataTable tab_sub = C2.Manager.ORManager.GetDataSet(sql_sub).Tables[0];

            for (int j = 0; j < tab_sub.Rows.Count; j++)
            {
                string fieldName = tab_sub.Rows[j]["Field"].ToString();
                string sql_field = string.Format(SafeValue.SafeSqlString(tab_sub.Rows[j]["Sql"]), refN, jobN, userName);
                bool isAdd = SafeValue.SafeBool(tab_sub.Rows[j]["IsAdd"], false);
                bool isMastSql = SafeValue.SafeBool(tab_sub.Rows[j]["IsMastSql"], true);
                string fieldValue = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_field));
                if (isAdd && isMastSql)
                {
                    mast.Columns.Add(fieldName);
                    for (int m = 0; m < mast.Rows.Count; m++)
                    {
                        mast.Rows[m][fieldName] = fieldValue;
                    }
                }
                else if (isAdd && !isMastSql)
                {
                    for (int m = 0; m < mast.Rows.Count; m++)
                    {
                        mast.Rows[m][fieldName] = fieldValue;
                    }
                }


            }
            if (relaField.Length > 0)
            {
                det = C2.Manager.ORManager.GetDataSet(sql2).Tables[0];
                set.Tables.Add(mast);
                set.Tables.Add(det);
                set.Relations.Add("Rela", mast.Columns[relaField], det.Columns[relaField]);
            }
            if (i == 0)
            {
                rpt.LoadLayout(repxPath);
                if (relaField.Length > 0)
                    rpt.DataSource = set;
                else
                    rpt.DataSource = mast;

                rpt.CreateDocument();
            }
            else
            {
                XtraReport subRpt = new XtraReport();
                subRpt.LoadLayout(repxPath);
                if (relaField.Length > 0)
                    subRpt.DataSource = set;
                else
                    subRpt.DataSource = mast;
                subRpt.CreateDocument();

                for (int j = 0; j < subRpt.Pages.Count; j++)
                {
                    rpt.Pages.Add(subRpt.Pages[j]);
                }
            }
        }
        rpt.ExportToPdf(ms);
        fileName = pdfName + DateTime.Now.ToString("yyyyMMdd");
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
    private MemoryStream PrintQuotation(string no, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Quotation.repx"));

        rpt.DataSource = SeaFreightDocPrint.dsQuotation(no,userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintQuote(string no, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Quote.repx"));

        //rpt.DataSource = SeaFreightDocPrint.dsQuote(no,userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    #region IMPORT ref
    private MemoryStream PrintLetter(string no, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\rptImpLetter.repx"));
        rpt.DataSource = SeaFreightDocPrint.dsImpLetter(no);
        rpt.CreateDocument();
        XtraReport subRpt = new XtraReport();
        subRpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\rptImpManifest.repx"));
        subRpt.DataSource = SeaFreightDocPrint.dsImpManifest(no);
        subRpt.CreateDocument();
        for (int i = 0; i < subRpt.Pages.Count; i++)
        {
            rpt.Pages.Add(subRpt.Pages[i]);
        }

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintImpHaulier(string RefN, string jobNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\Haulier.repx"));
        rpt.DataSource = SeaFreightDocPrint.PrintHaulier_Import(RefN, jobNo, "I");
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintImpPermit(string RefN, string ReportType)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\Permit.repx"));
        if (ReportType == "1")
            rpt.DataSource = SeaFreightDocPrint.PrintImpPermit(RefN);
        else
            rpt.DataSource = SeaFreightDocPrint.PrintImpPermitNvocc(RefN);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintAuthLetter(string RefN, string reportType)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\AuthLetter.repx"));
        if (reportType == "1")
            rpt.DataSource = SeaFreightDocPrint.PrintAuth(RefN);
        else
            rpt.DataSource = SeaFreightDocPrint.PrintAuthNvocc(RefN);

        System.IO.MemoryStream str = new MemoryStream();

        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintPermitList(string RefN, string ReportType)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\PermitList.repx"));
        if (ReportType == "1")
            rpt.DataSource = SeaFreightDocPrint.PrintPermitList(RefN);
        else
            rpt.DataSource = SeaFreightDocPrint.PrintPermitListNvocc(RefN);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintTs(string RefN)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\Tranship.repx"));
        rpt.DataSource = SeaFreightDocPrint.PrintTrans(RefN);
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }

    private MemoryStream PrintBatchDo(string no, string userName)
    {
        XtraReport rpt1 = new XtraReport();
        rpt1.CreateDocument();
        DataTable tab = C2.Manager.ORManager.GetDataSet("SELECT  JobNo FROM SeaImport WHERE (RefNo = '"+no+"')").Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            string impNo = tab.Rows[i][0].ToString();
            XtraReport rpt = new XtraReport();
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\rptDO.repx"));
            rpt.DataSource = SeaFreightDocPrint.dsDo(impNo);
            rpt.CreateDocument();
            for (int j = 0; j < rpt.Pages.Count; j++)
            {
                rpt1.Pages.Add(rpt.Pages[j]);
            }
        }
        System.IO.MemoryStream str = new MemoryStream();
        rpt1.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintBatchInvoice(string no,string jobType, string userName)
    {
        string sql = "SELECT DocNo FROM XAArInvoice WHERE (DocType = 'IV') AND (MastRefNo = '"+no+"') AND (MastType = 'SI')";
        if (jobType == "E")
        {
            sql = "SELECT DocNo FROM XAArInvoice WHERE (DocType = 'IV') AND (MastRefNo = '" + no + "') AND (MastType = 'SE')";
        }
        XtraReport rpt1 = new XtraReport();
        rpt1.CreateDocument();
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            string billNo = tab.Rows[i][0].ToString();
            XtraReport rpt = new XtraReport();
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\Invoice.repx"));
            rpt.DataSource = AccountFreightPrint.PrintInvoice(billNo, "IV");
            rpt.CreateDocument();


            for (int j = 0; j < rpt.Pages.Count; j++)
            {
                rpt1.Pages.Add(rpt.Pages[j]);
            }
        }
        System.IO.MemoryStream str = new MemoryStream();
        rpt1.ExportToPdf(str);

        return str;
    }

    private MemoryStream PrintCargoManifest(string no, string jobType, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\CargoManifest.repx"));

        rpt.DataSource = SeaFreightDocPrint.PrintCargoManifest(no);


        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }

    private MemoryStream PrintConverPage(string no, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\ConverPage.repx"));

        rpt.DataSource = SeaFreightDocPrint.dsImpConverPage(no);


        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    #endregion

    #region IMPORT

    private MemoryStream PrintDO(string ImportN)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\rptDO.repx"));
        rpt.DataSource = SeaFreightDocPrint.dsDo(ImportN);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintAdvise(string refN, string jobN)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\PreAdvise.repx"));
        rpt.DataSource = SeaFreightDocPrint.PrintPreAdvise(refN, jobN);
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintDN(string refN, string jobN,string dnId)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\DN.repx"));

        rpt.DataSource = SeaFreightDocPrint.PrintDN(refN, jobN,dnId);
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArrivalNotice(string refN, string JobN)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\ArrivalNotice.repx"));
        rpt.DataSource = SeaFreightDocPrint.PrintArrivalNotice(refN, JobN);
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintArrivalNoticeAg(string refN, string JobN)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\ArrivalNoticeAg.repx"));
        rpt.DataSource = SeaFreightDocPrint.PrintArrivalNotice(refN, JobN);
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintCertificate(string refN, string JobN, string impExp, string userId)
    {
        XtraReport rpt = new XtraReport();
        if (impExp == "SI")
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\Certificate.repx"));
        else if (impExp == "SE")
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\Certificate.repx"));

        rpt.DataSource = SeaFreightDocPrint.PrintCertificate(refN, JobN, impExp, userId);
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintJobOrder1(string refN, string JobN,string refType, string userId)
    {
        XtraReport rpt = new XtraReport();
        if(refType=="SI")
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\SeaImport.repx"));
        else
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\SeaImport.repx"));

        rpt.DataSource = SeaFreightDocPrint.PrintSeaImport(refN, JobN,refType, userId);
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }

    #endregion

    #region sea mast p&L
    private MemoryStream PrintPL(string refN, string JobNo,string refType, string userId)
    {
        if (JobNo.Length < 2)
            return PrintPL_SeaRef(refN, refType, userId);
        else
            return PrintPL_Seahouse(refN, JobNo, refType, userId);
    }
    private MemoryStream PrintPL_SeaRef(string refN, string refType, string userId)
    {
        string user = HttpContext.Current.User.Identity.Name;
        XtraReport rpt = new XtraReport();
        if (refType == "SI")
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\PL.repx"));
        else if (refType == "SE")
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\PL.repx"));

        DataSet set = SeaFreightDocPrint.PrintPl_SeaRef(refN, refType, userId);
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
            if(refType=="SI")
                rpt_Inv.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_1_Inv.repx"));
            else if (refType == "SE")
                rpt_Inv.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\export\Pl_1_Inv.repx"));
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
            rpt_Dn.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_2_Dn.repx"));
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
            rpt_Ts.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_3_Ts.repx"));
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
            rpt_Cn.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_4_Cn.repx"));
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
            rpt_Pl.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_5_Pl.repx"));
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
            rpt_Vo.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_6_Vo.repx"));
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
            rpt_Cost.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_7_Cost.repx"));
            subReport_Cost.ReportSource = rpt_Cost;
            rpt_Cost.DataSource = tab_Cost;
        }

        rpt.DataSource = tab_mast;
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }    
    #endregion
    #region import export house p&L
    private MemoryStream PrintPL_Seahouse(string refN, string jobN,string refType, string userId)
    {
        string user = HttpContext.Current.User.Identity.Name;
        XtraReport rpt = new XtraReport();
        if(refType=="SI")
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Import\PL_house.repx"));
        else if (refType == "SE")
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\PL_house.repx"));

        DataSet set = SeaFreightDocPrint.PrintPl_house(refN, jobN, refType, userId);
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
            rpt_Inv.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_house_Bill.repx"));
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
            rpt_Dn.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_house_Bill.repx"));
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
            rpt_Ts.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_3_Ts.repx"));
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
            rpt_Cn.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_house_Bill.repx"));
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
            rpt_Pl.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_house_Bill.repx"));
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
            rpt_Vo.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_house_Bill.repx"));
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
            rpt_Cost.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\import\Pl_7_Cost.repx"));
            subReport_Cost.ReportSource = rpt_Cost;
            rpt_Cost.DataSource = tab_Cost;
        }

        rpt.DataSource = tab_mast;
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }

    #endregion
    #region export SCH
    private MemoryStream PrintLoadPlan(string RefN, string reportType)
    {
        string user = HttpContext.Current.User.Identity.Name;
        XtraReport rpt = new XtraReport();

        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\rptLoadPlan.repx"));
        rpt.DataSource = SeaFreightDocPrint.dsLoadPlan(RefN);
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintBkgConfirm(string RefN, string jobN)
    {
        XtraReport rpt = new XtraReport();
        string jobType = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select JobType from SeaExportRef where RefNo='" + RefN + "'"));
        if (jobType == "FCL")
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\BookingConfirm_fcl.repx"));
        else
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\BookingConfirm_lcl.repx"));
        if (jobType == "FCL")
        {
            rpt.DataSource = SeaFreightDocPrint.PrintBookingConfirm_FCL(RefN, jobN);
        }
        else
        {
            rpt.DataSource = SeaFreightDocPrint.PrintBookingConfirm(RefN, jobN);
        }
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }

    private MemoryStream PrintSo(string RefN, string jobN)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\rptSo.repx"));
        rpt.DataSource = SeaFreightDocPrint.dsSo(RefN, jobN);
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    #endregion

    #region export  ref  _multiple
    private MemoryStream PrintOceanBl(string refN)
    {
        XtraReport rptOBL = new XtraReport();
        DataSet ds = SeaFreightDocPrint.PrintOceanBl(refN,true);        
        rptOBL.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\OceanBL.repx"));
        rptOBL.DataSource = ds;
        rptOBL.CreateDocument();

        XtraReport subRpt = new XtraReport();
        subRpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\OceanBLAttach.repx"));
        subRpt.DataSource = SeaFreightDocPrint.PrintOceanBlAttach(refN);
        subRpt.CreateDocument();

        for (int j = 0; j < subRpt.Pages.Count; j++)
        {
            rptOBL.Pages.Add(subRpt.Pages[j]);
        }
        System.IO.MemoryStream str = new MemoryStream();
        rptOBL.ExportToPdf(str);

        return str;
    }

    private MemoryStream PrintOceanBlNvocc(string refN)
    {
        XtraReport rptOBL = new XtraReport();
        DataSet ds = SeaFreightDocPrint.PrintOceanBl(refN,false);
        rptOBL.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\OceanBL.repx"));
        rptOBL.DataSource = ds;
        rptOBL.CreateDocument();

        XtraReport subRpt = new XtraReport();
        subRpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\OceanBLAttach.repx"));
        subRpt.DataSource = SeaFreightDocPrint.PrintOceanBlAttach(refN);
        subRpt.CreateDocument();

        for (int j = 0; j < subRpt.Pages.Count; j++)
        {
            rptOBL.Pages.Add(subRpt.Pages[j]);
        }
        System.IO.MemoryStream str = new MemoryStream();
        rptOBL.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintOceanBl_multiple(string refN)
    {
        string[] arr = refN.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        string sql = "select count(RefNo) from seaexportref where {0} group by pol,pod,vessel,eta,etd,cragentid";
        string where = "";
        for (int i = 0; i < arr.Length; i++)
        {
            if (i == 0)
            {
                where = string.Format("(RefNo='{0}'", arr[i].Trim());
                refN = arr[i].Trim();
            }
            else
                where += string.Format(" or RefNo='{0}'", arr[i].Trim());
           if (i == arr.Length - 1)
                where += ")";
        }
        int cnt = SafeValue.SafeInt(C2.Manager.ORManager.GetDataSet(string.Format(sql, where)).Tables[0].Rows.Count, 0);
        if (cnt > 1)
        {
            Response.Write("Different Vessel or Port or Carrier Agent");
            Response.End();
            Response.Clear();
        }
        XtraReport rptOBL = new XtraReport();
        rptOBL.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\OceanBL.repx"));
        rptOBL.DataSource = SeaFreightDocPrint.PrintOceanBl_multiple(refN, true,where);
        rptOBL.CreateDocument();

        XtraReport subRpt = new XtraReport();
        subRpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\OceanBLAttach.repx"));
        subRpt.DataSource = SeaFreightDocPrint.PrintOceanBlAttach_multiple(refN,where);
        subRpt.CreateDocument();

        for (int j = 0; j < subRpt.Pages.Count; j++)
        {
            rptOBL.Pages.Add(subRpt.Pages[j]);
        }
        System.IO.MemoryStream str = new MemoryStream();
        rptOBL.ExportToPdf(str);

        return str;
    }

    private MemoryStream PrintOceanBlNvocc_multiple(string refN)
    {
        XtraReport rptOBL = new XtraReport();
        DataSet ds = SeaFreightDocPrint.PrintOceanBl(refN, false);
        rptOBL.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\OceanBL.repx"));
        rptOBL.DataSource = ds;
        rptOBL.CreateDocument();

        XtraReport subRpt = new XtraReport();
        subRpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\OceanBLAttach.repx"));
        subRpt.DataSource = SeaFreightDocPrint.PrintOceanBlAttach(refN);
        subRpt.CreateDocument();

        for (int j = 0; j < subRpt.Pages.Count; j++)
        {
            rptOBL.Pages.Add(subRpt.Pages[j]);
        }
        System.IO.MemoryStream str = new MemoryStream();
        rptOBL.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintExpPermitList(string RefN, string reportType)//permit carrier
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\PermitList.repx"));
        if (reportType == "0")
            rpt.DataSource = SeaFreightDocPrint.PrintExpPermitList(RefN);
        else
            rpt.DataSource = SeaFreightDocPrint.PrintExpPermitListNvocc(RefN);
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintExpHaulier(string RefN, string jobNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\Haulier.repx"));
        rpt.DataSource = SeaFreightDocPrint.PrintHaulier_Export(RefN, jobNo, "E");
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintExpPreAdvise(string RefN)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\PreAdvise.repx"));
        rpt.DataSource = SeaFreightDocPrint.PrintExpPreAdvise(RefN);
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }

 
    private MemoryStream PrintBatchBL(string refN, string userName)
    {
        XtraReport rpt1 = new XtraReport();
        rpt1.CreateDocument();
        //DataTable tab = C2.Manager.ORManager.GetDataSet("SELECT  JobNo FROM SeaExport WHERE (RefNo = '" + refN + "')").Tables[0];
        DataTable tab = ConnectSql.GetTab("SELECT  JobNo FROM SeaExport WHERE (RefNo = '" + refN + "')");
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            string jobN = tab.Rows[i][0].ToString();
            XtraReport rpt = new XtraReport();
            DataSet set = SeaFreightDocPrint.PrintBLDraft(refN, jobN);
            int x = set.Tables[1].Rows.Count;
            if (x ==1)
                rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\BLDraft.repx"));
            else
            {
                rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\BLDraft_multiple.repx"));
            }
            rpt.DataSource = set;
            if (x > 1)
            {
                XtraReport subRpt = new XtraReport();
                subRpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\BlAttachList.repx"));
                subRpt.DataSource = SeaFreightDocPrint.PrintBlAttachList(refN, jobN);
                subRpt.CreateDocument();
                for (int m = 0; m < subRpt.Pages.Count; m++)
                {
                    rpt.Pages.Add(subRpt.Pages[m]);
                }
            }
            rpt.CreateDocument();


            for (int j = 0; j < rpt.Pages.Count; j++)
            {
                rpt1.Pages.Add(rpt.Pages[j]);
            }
        }
        System.IO.MemoryStream str = new MemoryStream();
        rpt1.ExportToPdf(str);

        return str;
    }
    #endregion

    #region export
    private MemoryStream PrintBL(string refN, string jobN)
    {
        XtraReport rpt = new XtraReport();
        DataSet set = SeaFreightDocPrint.PrintBL(refN, jobN);
        int x = set.Tables[1].Rows.Count;
        if (x < 2)
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\BL.repx"));
        else
        {
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\BL_multiple.repx"));
        }
        rpt.DataSource = set;

        if (x > 2)
        {
            XtraReport subRpt = new XtraReport();
            subRpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\BlAttachList.repx"));
            subRpt.DataSource = SeaFreightDocPrint.PrintBlAttachList(refN, jobN);
            subRpt.CreateDocument();
            for (int i = 0; i < subRpt.Pages.Count; i++)
            {
                rpt.Pages.Add(subRpt.Pages[i]);
            }
        }
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintDraftBL(string refN, string jobN)
    {
        XtraReport rpt = new XtraReport();
        DataSet set = SeaFreightDocPrint.PrintBLDraft(refN, jobN);
        int x = set.Tables[1].Rows.Count;
        if (x < 2)
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\BLDraft.repx"));
        else
        {
            rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\BLDraft_multiple.repx"));
        }
        rpt.DataSource = set;
        if (x > 2)
        {
            XtraReport subRpt = new XtraReport();
            subRpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\BlAttachList.repx"));
            subRpt.DataSource = SeaFreightDocPrint.PrintBlAttachList(refN, jobN);
            subRpt.CreateDocument();
            for (int i = 0; i < subRpt.Pages.Count; i++)
            {
                rpt.Pages.Add(subRpt.Pages[i]);
            }
        }
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }

    private MemoryStream PrintExpPermit(string refN, string JobN)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\ApprovedPermit.repx"));
        rpt.DataSource = SeaFreightDocPrint.PrintExpPermit(refN, JobN);
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }

    private MemoryStream PrintCommercial(string ExportN)//Commerical
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\Commercial.repx"));
        rpt.DataSource = SeaFreightDocPrint.PrintExpCommercial(ExportN);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }

    private MemoryStream PrintPacking(string ExportN)//Packing
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\Packing.repx"));
        rpt.DataSource = SeaFreightDocPrint.PrintExpPacking(ExportN);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }

    private MemoryStream PrintShippingRequest(string ExportN)//ShippingRequest
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\ShippingRequest.repx"));
        rpt.DataSource = SeaFreightDocPrint.PrintExpShippingRequest(ExportN);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintJobOrder(string ExportN,string JobType)//Job Order
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\JobOrder.repx"));
        rpt.DataSource = SeaFreightDocPrint.PrintJobOrder(ExportN,JobType);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    #endregion


    #region quotation
    private MemoryStream PrintQuote_Fcl(string no, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Quote_Fcl.repx"));

        rpt.DataSource = SeaFreightDocPrint.dsQuote(no, userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintQuote_Lcl(string no, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Quote_Lcl.repx"));

        rpt.DataSource = SeaFreightDocPrint.dsQuote(no, userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintQuote_AirFcl(string no, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Quote_Fcl.repx"));

        rpt.DataSource = SeaFreightDocPrint.dsQuote_AirFcl(no, userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintQuote_AirLcl(string no, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Quote_Lcl.repx"));

        rpt.DataSource = SeaFreightDocPrint.dsQuote_AirLcl(no, userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    private MemoryStream PrintSeaQuotation_Detail(string no, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Quote.repx"));

        rpt.DataSource = SeaFreightDocPrint.PrintSeaQuotation(no, userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    #endregion

    #region Air Import&Export
    //private MemoryStream PrintAWB(string refN, string jobN, string typ, string kind)
    //{
    //    XtraReport rpt = new XtraReport();
    //    DataTable set = SeaFreightDocPrint.PrintAWB(refN, jobN);
    //    if (typ == "Master" && kind == "Org")
    //        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\MAWB_Org.repx"));
    //    if (typ == "Master" && kind == "Draft")
    //        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\MAWB_Draft.repx"));
    //    if (typ == "House" && kind == "Org")
    //        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\HAWB_Org.repx"));
    //    if (typ == "House" && kind == "Draft")
    //        rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\HAWB_Draft.repx"));

    //    Session["REFN"] = refN;
    //    Session["JOBN"] = jobN;

    //    rpt.DataSource = set;
    //    System.IO.MemoryStream str = new MemoryStream();
    //    rpt.ExportToPdf(str);

    //    return str;
    //}
    //private MemoryStream PrintAirImportPL(string refN, string userId)
    //{
    //    string user = HttpContext.Current.User.Identity.Name;

    //    //rptImportPL rpt = new rptImportPL(refN, user);


    //    XtraReport rpt = new XtraReport();
    //    rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\PL.repx"));
    //    DataSet set = SeaFreightDocPrint.Print_AirPlImport(refN, userId);
    //    DataTable tab_mast = set.Tables[0];
    //    DataTable tab_Inv = set.Tables[1];
    //    DataTable tab_Dn = set.Tables[2];
    //    DataTable tab_Ts = set.Tables[3];
    //    DataTable tab_Cn = set.Tables[4];
    //    DataTable tab_Pl = set.Tables[5];
    //    DataTable tab_Vo = set.Tables[6];
    //    DataTable tab_Cost = set.Tables[7];

    //    if (tab_Inv.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Inv = rpt.Report.Bands["GroupFooter_Inv"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Inv = new XRSubreport();
    //        subReport_Inv.Name = "groupFooter_Inv";
    //        groupFooter_Inv.Controls.Add(subReport_Inv);
    //        XtraReport rpt_Inv = new XtraReport();
    //        rpt_Inv.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_1_Inv.repx"));
    //        subReport_Inv.ReportSource = rpt_Inv;

    //        rpt_Inv.DataSource = tab_Inv;
    //    }
    //    if (tab_Dn.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Dn = rpt.Report.Bands["GroupFooter_Dn"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Dn = new XRSubreport();
    //        subReport_Dn.Name = "groupFooter_Dn";
    //        groupFooter_Dn.Controls.Add(subReport_Dn);
    //        XtraReport rpt_Dn = new XtraReport();
    //        rpt_Dn.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_2_Dn.repx"));
    //        subReport_Dn.ReportSource = rpt_Dn;
    //        rpt_Dn.DataSource = tab_Dn;
    //    }

    //    if (tab_Ts.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Ts = rpt.Report.Bands["GroupFooter_Ts"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Ts = new XRSubreport();
    //        subReport_Ts.Name = "groupFooter_Ts";
    //        groupFooter_Ts.Controls.Add(subReport_Ts);
    //        XtraReport rpt_Ts = new XtraReport();
    //        rpt_Ts.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_3_Ts.repx"));
    //        subReport_Ts.ReportSource = rpt_Ts;
    //        rpt_Ts.DataSource = tab_Ts;
    //    }

    //    if (tab_Cn.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Cn = rpt.Report.Bands["GroupFooter_Cn"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Cn = new XRSubreport();
    //        subReport_Cn.Name = "groupFooter_Cn";
    //        groupFooter_Cn.Controls.Add(subReport_Cn);
    //        XtraReport rpt_Cn = new XtraReport();
    //        rpt_Cn.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_4_Cn.repx"));
    //        subReport_Cn.ReportSource = rpt_Cn;
    //        rpt_Cn.DataSource = tab_Cn;
    //    }

    //    if (tab_Pl.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Pl = rpt.Report.Bands["GroupFooter_Pl"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Pl = new XRSubreport();
    //        subReport_Pl.Name = "groupFooter_Pl";
    //        groupFooter_Pl.Controls.Add(subReport_Pl);
    //        XtraReport rpt_Pl = new XtraReport();
    //        rpt_Pl.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_5_Pl.repx"));
    //        subReport_Pl.ReportSource = rpt_Pl;
    //        rpt_Pl.DataSource = tab_Pl;
    //    }
    //    if (tab_Vo.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Vo = rpt.Report.Bands["GroupFooter_Vo"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Vo = new XRSubreport();
    //        subReport_Vo.Name = "groupFooter_Vo";
    //        groupFooter_Vo.Controls.Add(subReport_Vo);
    //        XtraReport rpt_Vo = new XtraReport();
    //        rpt_Vo.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_6_Vo.repx"));
    //        subReport_Vo.ReportSource = rpt_Vo;
    //        rpt_Vo.DataSource = tab_Vo;
    //    }
    //    if (tab_Cost.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Cost = rpt.Report.Bands["GroupFooter_Costing"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Cost = new XRSubreport();
    //        subReport_Cost.Name = "subReport_Cost";
    //        groupFooter_Cost.Controls.Add(subReport_Cost);
    //        XtraReport rpt_Cost = new XtraReport();
    //        rpt_Cost.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_7_Cost.repx"));
    //        subReport_Cost.ReportSource = rpt_Cost;
    //        rpt_Cost.DataSource = tab_Cost;
    //    }

    //    rpt.DataSource = tab_mast;
    //    System.IO.MemoryStream str = new MemoryStream();
    //    rpt.ExportToPdf(str);
    //    return str;
    //}
    //private MemoryStream PrintAirExportPL(string refN, string userId)
    //{
    //    string user = HttpContext.Current.User.Identity.Name;

    //    //rptImportPL rpt = new rptImportPL(refN, user);


    //    XtraReport rpt = new XtraReport();
    //    rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\PL.repx"));
    //    DataSet set = SeaFreightDocPrint.Print_AirPlExport(refN, userId);
    //    DataTable tab_mast = set.Tables[0];
    //    DataTable tab_Inv = set.Tables[1];
    //    DataTable tab_Dn = set.Tables[2];
    //    DataTable tab_Ts = set.Tables[3];
    //    DataTable tab_Cn = set.Tables[4];
    //    DataTable tab_Pl = set.Tables[5];
    //    DataTable tab_Vo = set.Tables[6];
    //    DataTable tab_Cost = set.Tables[7];

    //    if (tab_Inv.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Inv = rpt.Report.Bands["GroupFooter_Inv"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Inv = new XRSubreport();
    //        subReport_Inv.Name = "groupFooter_Inv";
    //        groupFooter_Inv.Controls.Add(subReport_Inv);
    //        XtraReport rpt_Inv = new XtraReport();
    //        rpt_Inv.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_1_Inv.repx"));
    //        subReport_Inv.ReportSource = rpt_Inv;

    //        rpt_Inv.DataSource = tab_Inv;
    //    }
    //    if (tab_Dn.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Dn = rpt.Report.Bands["GroupFooter_Dn"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Dn = new XRSubreport();
    //        subReport_Dn.Name = "groupFooter_Dn";
    //        groupFooter_Dn.Controls.Add(subReport_Dn);
    //        XtraReport rpt_Dn = new XtraReport();
    //        rpt_Dn.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_2_Dn.repx"));
    //        subReport_Dn.ReportSource = rpt_Dn;
    //        rpt_Dn.DataSource = tab_Dn;
    //    }

    //    if (tab_Ts.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Ts = rpt.Report.Bands["GroupFooter_Ts"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Ts = new XRSubreport();
    //        subReport_Ts.Name = "groupFooter_Ts";
    //        groupFooter_Ts.Controls.Add(subReport_Ts);
    //        XtraReport rpt_Ts = new XtraReport();
    //        rpt_Ts.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_3_Ts.repx"));
    //        subReport_Ts.ReportSource = rpt_Ts;
    //        rpt_Ts.DataSource = tab_Ts;
    //    }

    //    if (tab_Cn.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Cn = rpt.Report.Bands["GroupFooter_Cn"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Cn = new XRSubreport();
    //        subReport_Cn.Name = "groupFooter_Cn";
    //        groupFooter_Cn.Controls.Add(subReport_Cn);
    //        XtraReport rpt_Cn = new XtraReport();
    //        rpt_Cn.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_4_Cn.repx"));
    //        subReport_Cn.ReportSource = rpt_Cn;
    //        rpt_Cn.DataSource = tab_Cn;
    //    }

    //    if (tab_Pl.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Pl = rpt.Report.Bands["GroupFooter_Pl"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Pl = new XRSubreport();
    //        subReport_Pl.Name = "groupFooter_Pl";
    //        groupFooter_Pl.Controls.Add(subReport_Pl);
    //        XtraReport rpt_Pl = new XtraReport();
    //        rpt_Pl.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_5_Pl.repx"));
    //        subReport_Pl.ReportSource = rpt_Pl;
    //        rpt_Pl.DataSource = tab_Pl;
    //    }
    //    if (tab_Vo.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Vo = rpt.Report.Bands["GroupFooter_Vo"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Vo = new XRSubreport();
    //        subReport_Vo.Name = "groupFooter_Vo";
    //        groupFooter_Vo.Controls.Add(subReport_Vo);
    //        XtraReport rpt_Vo = new XtraReport();
    //        rpt_Vo.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_6_Vo.repx"));
    //        subReport_Vo.ReportSource = rpt_Vo;
    //        rpt_Vo.DataSource = tab_Vo;
    //    }
    //    if (tab_Cost.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Cost = rpt.Report.Bands["GroupFooter_Costing"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Cost = new XRSubreport();
    //        subReport_Cost.Name = "subReport_Cost";
    //        groupFooter_Cost.Controls.Add(subReport_Cost);
    //        XtraReport rpt_Cost = new XtraReport();
    //        rpt_Cost.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_7_Cost.repx"));
    //        subReport_Cost.ReportSource = rpt_Cost;
    //        rpt_Cost.DataSource = tab_Cost;
    //    }

    //    rpt.DataSource = tab_mast;
    //    System.IO.MemoryStream str = new MemoryStream();
    //    rpt.ExportToPdf(str);
    //    return str;
    //}
    //private MemoryStream PrintAirPL_house(string refN, string jobNo, string userId)
    //{
    //    string user = HttpContext.Current.User.Identity.Name;

    //    //rptImportPL rpt = new rptImportPL(refN, user);


    //    XtraReport rpt = new XtraReport();
    //    rpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\PL.repx"));
    //    DataSet set = SeaFreightDocPrint.PrintAirPl_house(refN, jobNo, userId);
    //    DataTable tab_mast = set.Tables[0];
    //    DataTable tab_Inv = set.Tables[1];
    //    DataTable tab_Dn = set.Tables[2];
    //    DataTable tab_Ts = set.Tables[3];
    //    DataTable tab_Cn = set.Tables[4];
    //    DataTable tab_Pl = set.Tables[5];
    //    DataTable tab_Vo = set.Tables[6];
    //    DataTable tab_Cost = set.Tables[7];

    //    if (tab_Inv.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Inv = rpt.Report.Bands["GroupFooter_Inv"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Inv = new XRSubreport();
    //        subReport_Inv.Name = "groupFooter_Inv";
    //        groupFooter_Inv.Controls.Add(subReport_Inv);
    //        XtraReport rpt_Inv = new XtraReport();
    //        rpt_Inv.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_1_Inv.repx"));
    //        subReport_Inv.ReportSource = rpt_Inv;

    //        rpt_Inv.DataSource = tab_Inv;
    //    }
    //    if (tab_Dn.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Dn = rpt.Report.Bands["GroupFooter_Dn"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Dn = new XRSubreport();
    //        subReport_Dn.Name = "groupFooter_Dn";
    //        groupFooter_Dn.Controls.Add(subReport_Dn);
    //        XtraReport rpt_Dn = new XtraReport();
    //        rpt_Dn.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_2_Dn.repx"));
    //        subReport_Dn.ReportSource = rpt_Dn;
    //        rpt_Dn.DataSource = tab_Dn;
    //    }

    //    if (tab_Ts.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Ts = rpt.Report.Bands["GroupFooter_Ts"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Ts = new XRSubreport();
    //        subReport_Ts.Name = "groupFooter_Ts";
    //        groupFooter_Ts.Controls.Add(subReport_Ts);
    //        XtraReport rpt_Ts = new XtraReport();
    //        rpt_Ts.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_3_Ts.repx"));
    //        subReport_Ts.ReportSource = rpt_Ts;
    //        rpt_Ts.DataSource = tab_Ts;
    //    }

    //    if (tab_Cn.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Cn = rpt.Report.Bands["GroupFooter_Cn"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Cn = new XRSubreport();
    //        subReport_Cn.Name = "groupFooter_Cn";
    //        groupFooter_Cn.Controls.Add(subReport_Cn);
    //        XtraReport rpt_Cn = new XtraReport();
    //        rpt_Cn.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_4_Cn.repx"));
    //        subReport_Cn.ReportSource = rpt_Cn;
    //        rpt_Cn.DataSource = tab_Cn;
    //    }

    //    if (tab_Pl.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Pl = rpt.Report.Bands["GroupFooter_Pl"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Pl = new XRSubreport();
    //        subReport_Pl.Name = "groupFooter_Pl";
    //        groupFooter_Pl.Controls.Add(subReport_Pl);
    //        XtraReport rpt_Pl = new XtraReport();
    //        rpt_Pl.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_5_Pl.repx"));
    //        subReport_Pl.ReportSource = rpt_Pl;
    //        rpt_Pl.DataSource = tab_Pl;
    //    }
    //    if (tab_Vo.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Vo = rpt.Report.Bands["GroupFooter_Vo"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Vo = new XRSubreport();
    //        subReport_Vo.Name = "groupFooter_Vo";
    //        groupFooter_Vo.Controls.Add(subReport_Vo);
    //        XtraReport rpt_Vo = new XtraReport();
    //        rpt_Vo.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_6_Vo.repx"));
    //        subReport_Vo.ReportSource = rpt_Vo;
    //        rpt_Vo.DataSource = tab_Vo;
    //    }
    //    if (tab_Cost.Rows.Count > 0)
    //    {
    //        DevExpress.XtraReports.UI.GroupFooterBand groupFooter_Cost = rpt.Report.Bands["GroupFooter_Costing"] as DevExpress.XtraReports.UI.GroupFooterBand;
    //        DevExpress.XtraReports.UI.XRSubreport subReport_Cost = new XRSubreport();
    //        subReport_Cost.Name = "subReport_Cost";
    //        groupFooter_Cost.Controls.Add(subReport_Cost);
    //        XtraReport rpt_Cost = new XtraReport();
    //        rpt_Cost.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Air\Pl_7_Cost.repx"));
    //        subReport_Cost.ReportSource = rpt_Cost;
    //        rpt_Cost.DataSource = tab_Cost;
    //    }

    //    rpt.DataSource = tab_mast;
    //    System.IO.MemoryStream str = new MemoryStream();
    //    rpt.ExportToPdf(str);
    //    return str;
    //}

    #endregion
}

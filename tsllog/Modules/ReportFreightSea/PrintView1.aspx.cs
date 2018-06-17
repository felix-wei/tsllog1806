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
using DevExpress.XtraReports.Web;
using C2;
using DevExpress.Web.ASPxUploadControl;
using LumiSoft.Net.Mime;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.Text;

public partial class ReportFreightSea_PrintView1 : System.Web.UI.Page
{
    private string docType = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)//!IsPostBack
        {
            if (Request.Params["document"] == null) return;
            docType = SafeValue.SafeString(Request.Params["docType"], "0");
            string reportName = Request.Params["document"];
            string refN = Request.Params["master"];
            string jobN = "";
            if (Request.Params["house"] != null)
                jobN = Request.Params["house"].ToString();
            string refType = "";
            string userName = HttpContext.Current.User.Identity.Name;
            string oid = "";
            if (Request.Params["oid"] != null)
                oid = Request.Params["oid"].ToString();
            Page.Title = reportName;
            MemoryStream ms = new MemoryStream();
            try
            {
                //if (reportName == "SI_PL")
                //    ms = PrintPL_SI(refN, jobN, "SI", userName);
                //else if (reportName == "SE_PL")
                //    ms = PrintPL_SE(refN, jobN, "SE", userName);
                //else if (reportName == "Air_PL")
                //    ms = PrintAirPL(refN, jobN, userName);
                //else if (reportName == "OceanBl_multiple")
                //    ms = PrintOceanBl_multiple(refN);
                //else if (reportName == "OceanBlNvocc_multiple")
                //    ms = PrintOceanBlNvocc_multiple(refN);
                //else
                //    ms = PrintRpt(reportName, refN, jobN, refType, userName, oid);

                Session["ReportName"] = reportName;
                if (!String.IsNullOrEmpty(reportName))
                {
                    if (reportName == "ArInvoiceSummary" && !String.IsNullOrEmpty(Request.Params["d1"]) && !String.IsNullOrEmpty(Request.Params["d2"]) && !String.IsNullOrEmpty(Request.Params["d1"]) && !String.IsNullOrEmpty(Request.Params["d1"]))
                    {
                        string d1=Request.Params["d1"];
                        string d2 = Request.Params["d2"];
                        DateTime date1 = DateTime.Parse(d1.Substring(6, 4) + "-" + d1.Substring(3, 2) + "-" + d1.Substring(0, 2));
                        DateTime date2 = DateTime.Parse(d2.Substring(6, 4) + "-" + d2.Substring(3, 2) + "-" + d2.Substring(0, 2));
                        ASPxDocumentViewer1.Report = PrintArInvoiceSummary(date1, date2, SafeValue.SafeString(Request.Params["docType"]), SafeValue.SafeString(Request.Params["partyId"]));
                    }
                    if (reportName == "ApInvoiceSummary" && !String.IsNullOrEmpty(Request.Params["d1"]) && !String.IsNullOrEmpty(Request.Params["d2"]) && !String.IsNullOrEmpty(Request.Params["d1"]) && !String.IsNullOrEmpty(Request.Params["d1"]))
                    {
                        string d1 = Request.Params["d1"];
                        string d2 = Request.Params["d2"];
                        DateTime date1 = DateTime.Parse(d1.Substring(6, 4) + "-" + d1.Substring(3, 2) + "-" + d1.Substring(0, 2));
                        DateTime date2 = DateTime.Parse(d2.Substring(6, 4) + "-" + d2.Substring(3, 2) + "-" + d2.Substring(0, 2));
                        ASPxDocumentViewer1.Report = PrintApInvoiceSummary(date1, date2, SafeValue.SafeString(Request.Params["docType"]), SafeValue.SafeString(Request.Params["partyId"]));
                    }
                    else
                        ASPxDocumentViewer1.Report = GetFullReport(reportName, refN, jobN, refType, userName, oid);
                }
            }
            catch (Exception ex)
            {
                        throw new Exception(ex.Message + ex.StackTrace);
						Response.Write(ex.Message);
            }
            //ExChange(ms, getFileName(reportName));
        }
    }


    public byte[] ToByteArray(string str)
    {
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        return encoding.GetBytes(str);
    }
    protected void btn_send_Click(object sender, EventArgs e)
    {
        string er = "";
        string[] fs = { "" };//atts.Split(new char[] { '*' });


        string path1 = string.Format("~/files/email/{0}/", DateTime.Now.ToString("dd-MM-yy HHmmss"));
        string path2 = path1.Replace(' ', '_').Replace('\'', '_');
        string pathx = path2.Substring(1);
        string path3 = MapPath(path2);
        if (!Directory.Exists(path3))
            Directory.CreateDirectory(path3);
        UploadedFile[] files = { this.file_upload1.UploadedFiles[0], this.file_upload2.UploadedFiles[0], this.file_upload3.UploadedFiles[0] };

        string[] conf = System.Configuration.ConfigurationManager.AppSettings["SMTP"].Split(new char[] { '|' });
        string host = conf[0];
        int port = int.Parse(conf[1]);
        bool ssl = conf[2] == "SSL";
        string usr = conf[3];
        string pas = conf[4];
        string top = conf[5];


        Mime m = new Mime();
        m.MainEntity.From = new AddressList();
        m.MainEntity.From.Parse(top + "<" + usr + ">");
        m.MainEntity.Subject = txt_subject.Text.Trim();
        m.MainEntity.ContentType = MediaType_enum.Multipart_mixed;

        m.MainEntity.To = new AddressList();
        m.MainEntity.To.Parse(txt_to.Text.Trim());

        m.MainEntity.Cc = new AddressList();
        m.MainEntity.Cc.Parse(txt_cc.Text.Trim());

        m.MainEntity.Bcc = new AddressList();
        m.MainEntity.Bcc.Parse("");

        MimeEntity me = m.MainEntity.ChildEntities.Add();
        me.ContentType = MediaType_enum.Text_html;//Text_plain;
        me.ContentType_CharSet = "utf-8";
        me.ContentTransferEncoding = ContentTransferEncoding_enum.QuotedPrintable;


        MemoryStream ms = new MemoryStream();
        ASPxDocumentViewer1.Report.ExportToHtml(ms);


        ms.Position = 0;
        StreamReader reader = new StreamReader(ms);
        string html = reader.ReadToEnd();
        me.DataText = html;



        try
        {
            for (int i = 0; i < files.Length; i++)
            {
                if (SafeValue.SafeString(files[i].FileName).Length == 0) continue;
                string __name = (files[i].FileName ?? "").ToLower().Trim().Replace(' ', '_').Replace('\'', '_');
                string path4 = path3 + __name;
                bool isOk = false;

                FileStream fileStream = new FileStream(path4, FileMode.Create);

                BinaryWriter bw = new BinaryWriter(fileStream);
                bw.Write(files[0].FileBytes);
                bw.Close();
                //fs.Close();


                string fn = path4.Substring(path4.LastIndexOf("\\") + 1);

                MimeEntity ame = m.MainEntity.ChildEntities.Add();
                ame.ContentType = MediaType_enum.Application_octet_stream;
                ame.ContentDisposition = ContentDisposition_enum.Attachment;
                ame.ContentTransferEncoding = ContentTransferEncoding_enum.Base64;
                ame.ContentDisposition_FileName = fn;
                ame.Data = (byte[])File.ReadAllBytes(path4);

            }
            //for (int i = 0; i < fs.Length; i++)
            //{
            //    string f = fs[i];
            //    if (f.IndexOf("\\") > -1 && File.Exists(f))
            //    {
            //    }
            //}
            LumiSoft.Net.SMTP.Client.SmtpClientEx.QuickSendSmartHost(
                host,
                port,
                ssl,
                "", usr, pas, m);

            lbl_message.Text = "SUCCESS !";
        }
        catch (Exception ex)
        {
            er = ex.Message + ex.StackTrace;
            lbl_message.Text = er;
        }
    }
    private XtraReport GetFullReport(string reportName, string refN, string jobN, string refType, string userName, string ParameterV)
    {
        //rptName,path,reftype,procName
        // from sys_rpt: path, procName by rptName , refType
        XtraReport rpt = new XtraReport();
        DataTable tab = C2.Manager.ORManager.GetDataSet("select RefType,IsBatch, BatchSQL, IsCheck, CheckSQL, Value1, Value2, RepxName1, RepxName2  from sys_rpt where Name='" + reportName + "'").Tables[0];
        if (tab.Rows.Count == 0)
            return null;
        refType = tab.Rows[0]["RefType"].ToString();
        bool isBatch = SafeValue.SafeBool(tab.Rows[0]["IsBatch"], false);
        string batchSQL = tab.Rows[0]["BatchSQL"].ToString();
        bool isCheck = SafeValue.SafeBool(tab.Rows[0]["IsCheck"], false);
        string checkSQL = tab.Rows[0]["CheckSQL"].ToString();
        string value1 = tab.Rows[0]["Value1"].ToString();
        string value2 = tab.Rows[0]["Value2"].ToString();
        string repxName1 = tab.Rows[0]["RepxName1"].ToString(); //batchReportName也重用这个字段
        string repxName2 = tab.Rows[0]["RepxName2"].ToString();


        if (isBatch)
        {
            DataTable tab2 = C2.Manager.ORManager.GetDataSet("select RefType,IsCheck, CheckSQL, Value1, Value2, RepxName1, RepxName2  from sys_rpt where Name='" + repxName1 + "'").Tables[0];
            isCheck = SafeValue.SafeBool(tab2.Rows[0]["IsCheck"], false);
            checkSQL = tab2.Rows[0]["CheckSQL"].ToString();
            value1 = tab2.Rows[0]["Value1"].ToString();
            value2 = tab2.Rows[0]["Value2"].ToString();

            //SELECT JOBNO FROM SEAIMPORT WHERE REFNO='{0}'
            //SELECT DocNo FROM XAArInvoice WHERE (DocType = 'IV') AND (MastRefNo = '{0}') AND (MastType = '{2}')
            DataTable tab3 = C2.Manager.ORManager.GetDataSet(string.Format(batchSQL, refN, jobN, refType)).Tables[0];
            for (int i = 0; i < tab3.Rows.Count; i++)
            {
                refN = tab3.Rows[i]["RefNo"].ToString();
                jobN = tab3.Rows[i]["JobNo"].ToString();

                XtraReport rpt1 = new XtraReport();
                {
                    if (isCheck)
                    {
                        repxName1 = tab2.Rows[0]["RepxName1"].ToString();
                        repxName2 = tab2.Rows[0]["RepxName2"].ToString();
                        DataTable tab4 = C2.Manager.ORManager.GetDataSet(string.Format(checkSQL, refN, jobN, refType)).Tables[0];
                        if (tab4.Rows[0][0].ToString() == value1)
                            rpt1 = GetReport(repxName1, refN, jobN, refType, userName, ParameterV);
                        if (tab4.Rows[0][0].ToString() == value2)
                            rpt1 = GetReport(repxName2, refN, jobN, refType, userName, ParameterV);
                    }
                    else
                        rpt1 = GetReport(repxName1, refN, jobN, refType, userName, ParameterV);
                }
                rpt1.CreateDocument();
                rpt.Pages.AddRange(rpt1.Pages);
            }
        }

        else
        {
            if (!isCheck)
                rpt = GetReport(reportName, refN, jobN, refType, userName, ParameterV);
            else
            {
                //SEH_ShippingOrder→select JobType from SeaExportRef where RefNo='{0}'
                //SELECT CASE WHEN COUNT(*)<2 THEN 'Single' else ' Multiple' end FROM SeaExport det INNER JOIN SeaExportRef AS mast ON det.RefNo = mast.RefNo WHERE (det.RefNo = '{0}') and det.JobNo='{1}'
                DataTable tab2 = C2.Manager.ORManager.GetDataSet(string.Format(checkSQL, refN, jobN, refType)).Tables[0];
                if (tab2.Rows[0][0].ToString() == value1)
                    rpt = GetReport(repxName1, refN, jobN, refType, userName, ParameterV);
                if (tab2.Rows[0][0].ToString() == value2)
                    rpt = GetReport(repxName2, refN, jobN, refType, userName, ParameterV);
            }
        }
        return rpt;
    }
    protected void btPreview_Click(object sender, EventArgs e)
    {
        //string reportName = (string)cbReportType.Value;

        //ASPxDocumentViewer1.Report = GetReportByName(reportName);
        //Session["ReportName"] = reportName;
    } 
    #region exchange and getfileName
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
    private string getFileName(string reportName)
    {
        string fileName = "";
        string sql = string.Format("select reftype from sys_rpt where name='{0}'", reportName);
        string refType = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        if (refType.ToUpper() == "SI")
            fileName = "SeaImport_";
        else if (refType.ToUpper() == "SE")
            fileName = "SeaExport_";
        else if (refType.ToUpper() == "AI" || refType.ToUpper() == "AE" || refType.ToUpper() == "ACT")
            fileName = "Air_";
        else if (refType.ToUpper() == "TPT")
            fileName = "Transport_";
        else
            fileName = "";
        if (reportName.Length > 3 && reportName.Substring(3, 1) == "_")
            fileName = fileName + reportName.Substring(3, reportName.Length - 3);
        else
            fileName = fileName + reportName;
        return fileName + DateTime.Now.ToString("yyyyMMdd");
    }
    #endregion

    #region print rpt
    private MemoryStream PrintRpt(string reportName, string refN, string jobN, string refType, string userName, string ParameterV)
    {
        //rptName,path,reftype,procName
        // from sys_rpt: path, procName by rptName , refType
        XtraReport rpt = new XtraReport();
        DataTable tab = C2.Manager.ORManager.GetDataSet("select RefType,IsBatch, BatchSQL, IsCheck, CheckSQL, Value1, Value2, RepxName1, RepxName2  from sys_rpt where Name='" + reportName + "'").Tables[0];
        if (tab.Rows.Count == 0)
            return null;
        refType = tab.Rows[0]["RefType"].ToString();
        bool isBatch = SafeValue.SafeBool(tab.Rows[0]["IsBatch"], false);
        string batchSQL = tab.Rows[0]["BatchSQL"].ToString();
        bool isCheck = SafeValue.SafeBool(tab.Rows[0]["IsCheck"], false);
        string checkSQL = tab.Rows[0]["CheckSQL"].ToString();
        string value1 = tab.Rows[0]["Value1"].ToString();
        string value2 = tab.Rows[0]["Value2"].ToString();
        string repxName1 = tab.Rows[0]["RepxName1"].ToString(); //batchReportName也重用这个字段
        string repxName2 = tab.Rows[0]["RepxName2"].ToString();


        if (isBatch)
        {
            DataTable tab2 = C2.Manager.ORManager.GetDataSet("select RefType,IsCheck, CheckSQL, Value1, Value2, RepxName1, RepxName2  from sys_rpt where Name='" + repxName1 + "'").Tables[0];
            isCheck = SafeValue.SafeBool(tab2.Rows[0]["IsCheck"], false);
            checkSQL = tab2.Rows[0]["CheckSQL"].ToString();
            value1 = tab2.Rows[0]["Value1"].ToString();
            value2 = tab2.Rows[0]["Value2"].ToString();

            //SELECT JOBNO FROM SEAIMPORT WHERE REFNO='{0}'
            //SELECT DocNo FROM XAArInvoice WHERE (DocType = 'IV') AND (MastRefNo = '{0}') AND (MastType = '{2}')
            DataTable tab3 = C2.Manager.ORManager.GetDataSet(string.Format(batchSQL, refN, jobN, refType)).Tables[0];
            for (int i = 0; i < tab3.Rows.Count; i++)
            {
                refN = tab3.Rows[i]["RefNo"].ToString();
                jobN = tab3.Rows[i]["JobNo"].ToString();

                XtraReport rpt1 = new XtraReport();
                {
                    if (isCheck)
                    {
                        repxName1 = tab2.Rows[0]["RepxName1"].ToString();
                        repxName2 = tab2.Rows[0]["RepxName2"].ToString();
                        DataTable tab4 = C2.Manager.ORManager.GetDataSet(string.Format(checkSQL, refN, jobN, refType)).Tables[0];
                        if (tab4.Rows[0][0].ToString() == value1)
                            rpt1 = GetReport(repxName1, refN, jobN, refType, userName, ParameterV);
                        if (tab4.Rows[0][0].ToString() == value2)
                            rpt1 = GetReport(repxName2, refN, jobN, refType, userName, ParameterV);
                    }
                    else
                        rpt1 = GetReport(repxName1, refN, jobN, refType, userName, ParameterV);
                }
                rpt1.CreateDocument();
                rpt.Pages.AddRange(rpt1.Pages);
            }
        }

        else
        {
            if (!isCheck)
                rpt = GetReport(reportName, refN, jobN, refType, userName, ParameterV);
            else
            {
                //SEH_ShippingOrder→select JobType from SeaExportRef where RefNo='{0}'
                //SELECT CASE WHEN COUNT(*)<2 THEN 'Single' else ' Multiple' end FROM SeaExport det INNER JOIN SeaExportRef AS mast ON det.RefNo = mast.RefNo WHERE (det.RefNo = '{0}') and det.JobNo='{1}'
                DataTable tab2 = C2.Manager.ORManager.GetDataSet(string.Format(checkSQL, refN, jobN, refType)).Tables[0];
                if (tab2.Rows[0][0].ToString() == value1)
                    rpt = GetReport(repxName1, refN, jobN, refType, userName, ParameterV);
                if (tab2.Rows[0][0].ToString() == value2)
                    rpt = GetReport(repxName2, refN, jobN, refType, userName, ParameterV);
            }
        }

        System.IO.MemoryStream str = new MemoryStream();
        if (docType == "0")
            rpt.ExportToPdf(str);
        else
            rpt.ExportToXls(str);


        return str;
    }

    private XtraReport GetReport(string reportName, string refN, string jobN, string refType, string userName, string ParameterV)
    {
        string repxPath = "";
        string procName = "";
        string subRepxPath = "";
        string subProcName = "";
        DataTable tab = C2.Manager.ORManager.GetDataSet("select Path, RefType, ProcName, SubPath,SubProcName  from sys_rpt where Name='" + reportName + "'").Tables[0];
        repxPath = tab.Rows[0]["Path"].ToString();
        procName = tab.Rows[0]["ProcName"].ToString();
        subRepxPath = tab.Rows[0]["SubPath"].ToString();
        subProcName = tab.Rows[0]["SubProcName"].ToString();
        refType = tab.Rows[0]["RefType"].ToString();
        XtraReport rpt = new XtraReport();
        if (File.Exists(Server.MapPath(repxPath)))
        {
            rpt.LoadLayout(Server.MapPath(repxPath));
            string strsql = string.Format(@"exec {5} '{0}','{1}','{2}','{3}','{4}'", refN, jobN, refType, userName, ParameterV, procName);
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            if (ds_temp.Tables.Count == 1)
            {
                rpt.DataSource = ds_temp.Tables[0];
                ds_temp.Tables[0].TableName = "Mast";
            }
            else if (ds_temp.Tables.Count == 2)
            {
                DataSet set = new DataSet();
                DataTable mast = ds_temp.Tables[0].Copy();
                mast.TableName = "Mast";
                DataTable det = ds_temp.Tables[1].Copy();
                det.TableName = "Detail";
                set.Tables.Add(mast);
                set.Tables.Add(det);
                set.Relations.Add("Rela", mast.Columns["Relation"], det.Columns["Relation"]);
                rpt.DataSource = set;
            }
            rpt.CreateDocument();
        }
        if (File.Exists(Server.MapPath(subRepxPath)))
        {
            XtraReport subRpt = new XtraReport();
            subRpt.LoadLayout(Server.MapPath(subRepxPath));
            string strsql = string.Format(@"exec {5} '{0}','{1}','{2}','{3}','{4}'", refN, jobN, refType, userName, ParameterV, subProcName);
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            if (ds_temp.Tables.Count == 1)
            {
                subRpt.DataSource = ds_temp.Tables[0];
                ds_temp.Tables[0].TableName = "Mast";
            }
            else if (ds_temp.Tables.Count == 2)
            {
                DataSet set = new DataSet();
                DataTable mast = ds_temp.Tables[0].Copy();
                mast.TableName = "Mast";
                DataTable det = ds_temp.Tables[1].Copy();
                det.TableName = "Detail";
                set.Tables.Add(mast);
                set.Tables.Add(det);
                set.Relations.Add("Rela", mast.Columns["Relation"], det.Columns["Relation"]);
                subRpt.DataSource = set;
            }
            subRpt.CreateDocument();
            rpt.Pages.AddRange(subRpt.Pages);
        }
        return rpt;
    }

    #endregion


    #region sea p&L
    //SIM--->Sea Import Mast ; SEH--->Sea Export House
    private MemoryStream PrintPL_SI(string refN, string JobNo, string refType, string userId)
    {
        if (JobNo.Length < 2)
            return PrintPL_SIM(refN, refType, userId);
        else
            return PrintPL_SIH(refN, JobNo, refType, userId);
    }

    private MemoryStream PrintPL_SE(string refN, string JobNo, string refType, string userId)
    {
        if (JobNo.Length < 2)
            return PrintPL_SEM(refN, refType, userId);
        else
            return PrintPL_SEH(refN, JobNo, refType, userId);
    }
    private MemoryStream PrintPL_SIM(string refN, string refType, string userId)
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
            if (refType == "SI")
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

    private MemoryStream PrintPL_SIH(string refN, string jobN, string refType, string userId)
    {
        string user = HttpContext.Current.User.Identity.Name;
        XtraReport rpt = new XtraReport();
        if (refType == "SI")
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

    private MemoryStream PrintPL_SEM(string refN, string refType, string userId)
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
            if (refType == "SI")
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
    private MemoryStream PrintPL_SEH(string refN, string jobN, string refType, string userId)
    {
        string user = HttpContext.Current.User.Identity.Name;
        XtraReport rpt = new XtraReport();
        if (refType == "SI")
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
    #region air p&l
    private MemoryStream PrintAirPL(string refN, string JobNo, string userId)
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
    #region print export ocean bl by refnos
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
        rptOBL.DataSource = SeaFreightDocPrint.PrintOceanBl_multiple(refN, true, where);
        rptOBL.CreateDocument();

        XtraReport subRpt = new XtraReport();
        subRpt.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Export\OceanBLAttach.repx"));
        subRpt.DataSource = SeaFreightDocPrint.PrintOceanBlAttach_multiple(refN, where);
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
    private XtraReport PrintArInvoiceSummary(DateTime d1, DateTime d2, string docType, string partyId)
    {
        XtraReport rptAccArInv = new XtraReport();
        DataSet ds_temp = Manager.ORManager.GetDataSet(string.Format(@"select case when '{2}'='IV' then 'Invoice N' when '{2}'='CN' then 'Credit N' when '{2}'='DN' then 'Debit N' else 'No' end as No select DocNo,convert(nvarchar(10),DocDate,103) as DocDate,(select name from XXParty where PartyId=m.PartyTo) as CustomerName,PartyTo,MastRefNo,
case when MastType='SI' then (select Pol+'-'+Pod from SeaImportRef where RefNo=m.MastRefNo) when MastType='SE' then (select Pol+'-'+Pod from SeaExportRef where RefNo=m.MastRefNo) else '' end as PolPod,
Term,LocAmt
from XAArInvoice m where DocType='{2}' and DocDate>='{0}' and DocDate<='{1}'", d1, d2, docType) + (String.IsNullOrEmpty(partyId) ? "" : (" and PartyTo='" + partyId + "'")));
        rptAccArInv.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\ArInvoiceSummary.repx"));
        if (ds_temp.Tables.Count == 1)
        {
            rptAccArInv.DataSource = ds_temp.Tables[0];
            ds_temp.Tables[0].TableName = "Mast";
        }
        else if (ds_temp.Tables.Count == 2)
        {
            DataSet set = new DataSet();
            DataTable mast = ds_temp.Tables[0].Copy();
            mast.TableName = "Mast";
            DataTable det = ds_temp.Tables[1].Copy();
            det.TableName = "Detail";
            set.Tables.Add(mast);
            set.Tables.Add(det);
            //set.Relations.Add("Rela", mast.Columns["Relation"], det.Columns["Relation"]);
            rptAccArInv.DataSource = set;
        }
        rptAccArInv.CreateDocument();

        return rptAccArInv;
    }
    private XtraReport PrintApInvoiceSummary(DateTime d1, DateTime d2, string docType, string partyId)
    {
        XtraReport rptAccArInv = new XtraReport();
        DataSet ds_temp = Manager.ORManager.GetDataSet(string.Format(@"select DocNo,convert(nvarchar(10),DocDate,103) as DocDate,(select name from XXParty where PartyId=m.PartyTo) as VendorName,PartyTo,case when MastType='SI' THEN 'I ' +MastRefNo WHEN m.MastType='SE' THEN 'E '+MastRefNo END AS MastRefNo,
m.ChqNo,LocAmt,m.CurrencyId+'@'+CAST(m.ExRate AS NVARCHAR(50)) AS Currency
from dbo.XAApPayable m where DocDate>='{0}' and DocDate<='{1}'", d1, d2, docType) + (String.IsNullOrEmpty(partyId) ? "" : (" and PartyTo='" + partyId + "'")));
        rptAccArInv.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\ApInvoiceSummary.repx"));
        if (ds_temp.Tables.Count == 1)
        {
            rptAccArInv.DataSource = ds_temp.Tables[0];
            ds_temp.Tables[0].TableName = "Mast";
        }
        else if (ds_temp.Tables.Count == 2)
        {
            DataSet set = new DataSet();
            DataTable mast = ds_temp.Tables[0].Copy();
            mast.TableName = "Mast";
            DataTable det = ds_temp.Tables[1].Copy();
            det.TableName = "Detail";
            set.Tables.Add(mast);
            set.Tables.Add(det);
            //set.Relations.Add("Rela", mast.Columns["Relation"], det.Columns["Relation"]);
            rptAccArInv.DataSource = set;
        }
        rptAccArInv.CreateDocument();

        return rptAccArInv;
    }
    private MemoryStream GetArInvoiceSummary(DateTime d1, DateTime d2, string docType, string partyId)
    {
        XtraReport rptAccArInv = new XtraReport();
        DataSet ds_temp = Manager.ORManager.GetDataSet(string.Format(@"select case when '{2}'='IV' then 'Invoice N' when '{2}'='CN' then 'Credit N' when '{2}'='DN' then 'Debit N' else 'No' end as No select DocNo,convert(nvarchar(10),DocDate,103) as DocDate,(select name from XXParty where PartyId=m.PartyTo) as CustomerName,PartyTo,MastRefNo,
case when MastType='SI' then (select Pol+'-'+Pod from SeaImportRef where RefNo=m.MastRefNo) when MastType='SE' then (select Pol+'-'+Pod from SeaExportRef where RefNo=m.MastRefNo) else '' end as PolPod,
Term,LocAmt
from XAArInvoice m where DocType='{2}' and DocDate>='{0}' and DocDate<='{1}'", d1, d2, docType) + (String.IsNullOrEmpty(partyId) ? "" : (" and PartyTo='" + partyId + "'")));
        rptAccArInv.LoadLayout(Server.MapPath(@"~\ReportFreightSea\repx\Account\ArInvoiceSummary.repx"));
        if (ds_temp.Tables.Count == 1)
        {
            rptAccArInv.DataSource = ds_temp.Tables[0];
            ds_temp.Tables[0].TableName = "Mast";
        }
        else if (ds_temp.Tables.Count == 2)
        {
            DataSet set = new DataSet();
            DataTable mast = ds_temp.Tables[0].Copy();
            mast.TableName = "Mast";
            DataTable det = ds_temp.Tables[1].Copy();
            det.TableName = "Detail";
            set.Tables.Add(mast);
            set.Tables.Add(det);
            //set.Relations.Add("Rela", mast.Columns["Relation"], det.Columns["Relation"]);
            rptAccArInv.DataSource = set;
        }
        rptAccArInv.CreateDocument();

        System.IO.MemoryStream str = new MemoryStream();
        rptAccArInv.ExportToPdf(str);

        return str;
    }
    #endregion
    #region mast-detail print
    private MemoryStream Print_MastDetail(string reportName, string refN, string jobN, string refType, string userName, string ParameterV)
    {
        string repxPath = "";
        string procName = "";
        string subRepxPath = "";
        string subProcName = "";
        DataTable tab = C2.Manager.ORManager.GetDataSet("select Path, RefType, ProcName, SubPath,SubProcName  from sys_rpt where Name='" + reportName + "'").Tables[0];
        repxPath = tab.Rows[0]["Path"].ToString();
        procName = tab.Rows[0]["ProcName"].ToString();
        XtraReport rpt = new XtraReport();
        if (File.Exists(Server.MapPath(repxPath)))
        {
            rpt.LoadLayout(Server.MapPath(repxPath));
            string strsql = string.Format(@"exec {5} '{0}','{1}','{2}','{3}','{4}'", refN, jobN, refType, userName, ParameterV, procName);
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            if (ds_temp.Tables.Count == 2)
            {
                DataSet set = new DataSet();
                DataTable mast = ds_temp.Tables[0].Copy();
                mast.TableName = "Mast";
                if (mast.Rows.Count > 0)
                {
                    DataTable det = ds_temp.Tables[1].Copy();
                    det.TableName = "Detail";
                    set.Tables.Add(mast);
                    set.Tables.Add(det);
                    set.Relations.Add("Rela", mast.Columns["Relation"], det.Columns["Relation"]);
                    rpt.DataSource = set;
                    rpt.DataMember = "Mast";

                    DevExpress.XtraReports.UI.DetailReportBand detRpt = rpt.Report.Bands["DetailReport"] as DevExpress.XtraReports.UI.DetailReportBand;
                    detRpt.DataSource = set;
                    detRpt.DataMember = "Rela";
                }
            }
            rpt.CreateDocument();
        }
        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);

        return str;
    }
    
    #endregion
}

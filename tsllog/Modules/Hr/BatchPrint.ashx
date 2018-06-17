<%@ WebHandler Language="C#" Class="BatchPrint" %>

using DevExpress.Web.ASPxEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class BatchPrint : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.Params["no"] != null)
        {
            string strIds = SafeValue.SafeString(context.Request.Params["no"]);
            string[] arrIds = strIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            XtraReport rpt = new XtraReport();

            for (int i = 0; i < arrIds.Length; i++)
            {
                XtraReport rpt1 = new XtraReport();
                string[] str_list = arrIds[i].Split('_');
                string no = str_list[0];
                string person = str_list[1];
                string dateFrom = str_list[2];
                string dateTo = str_list[3];
                DateTime from = DateTime.ParseExact(dateFrom, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime to = DateTime.ParseExact(dateTo, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string role = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(string.Format(@"select HrRole from Hr_Person where Id={0}", person)));
                string repxPath = "/Modules/Hr/Report/repx/PayrollSlip.repx";
                if (role.ToLower() == "driver")
                {
                    repxPath = "/Modules/Hr/Report/repx/PayrollSlip_driver.repx";
                }
                if (File.Exists(context.Server.MapPath(repxPath)))
                {
                    rpt1.LoadLayout(context.Server.MapPath(repxPath));
                    DataSet set = HrPrint.PrintPaySlip(no, from, to);
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
                            rpt_Inv.LoadLayout(context.Server.MapPath(@"~\Modules\Hr\Report\repx\PayrollSlip_sub.repx"));
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
                            rpt_Inv.LoadLayout(context.Server.MapPath(@"~\Modules\Hr\Report\repx\PayrollSlip_sub.repx"));
                            subReport_Ot.ReportSource = rpt_Inv;
                            rpt_Inv.DataSource = tab_det1;
                        }
                    }
                    rpt1.DataSource = set;
                    rpt1.CreateDocument();
                }
                //rpt1.CreateDocument();//Notice:if do not comment this line,it will not show the sub report.--20160127
                rpt.Pages.AddRange(rpt1.Pages);
            }

            MemoryStream ms = new MemoryStream();
            rpt.ExportToPdf(ms);
            ExChange(ms, string.Format("Payroll_{0:yyyy-MM-dd}",DateTime.Now), context);

        }
    }
    private void ExChange(MemoryStream ms, string fileName,HttpContext context)
    {
        MemoryStream str = ms;

        byte[] bt = str.GetBuffer();

        try
        {
            context.Response.Clear();
            context.Response.ClearHeaders();
            context.Response.ClearContent();
            context.Response.Buffer = false;
            context.Response.ContentType = "application/pdf";
            context.Response.AddHeader("Content-Length", bt.Length.ToString());
            context.Response.AddHeader("Content-Disposition", "inline; filename=" + fileName + ".pdf");
            //Response.Output.Write(bt);
            context.Response.BinaryWrite(bt);
            //context.Response.End();
        }
        catch (Exception ex)
        {
            context.Response.Clear();
            context.Response.Write(ex.Message + "/" + ex.StackTrace);
        }
        str.Dispose();
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}
<%@ WebHandler Language="C#" Class="BatchPrintInvoice" %>

using System;
using System.Web;
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

public class BatchPrintInvoice : IHttpHandler {

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
                string docNo = str_list[0];
                string docType = str_list[1];
                string sql = string.Format(@"select Discount from XAArInvoice where SequenceId='{0}'",docNo);
                decimal res = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
                string repxPath = "/ReportFreightSea/repx/Account/Invoice_ell.repx";
                if (res > 0)
                    repxPath = "/ReportFreightSea/repx/Account/Invoice_ell_discount.repx";
                if (File.Exists(context.Server.MapPath(repxPath)))
                {
                    rpt1.LoadLayout(context.Server.MapPath(repxPath));
                    DataSet dtTmp = InvoiceReport.DsImpTs(docNo, docType);

                    rpt1.DataSource = dtTmp;
                    rpt1.CreateDocument();
                }
                //rpt1.CreateDocument();//Notice:if do not comment this line,it will not show the sub report.--20160127
                rpt.Pages.AddRange(rpt1.Pages);
            }

            MemoryStream ms = new MemoryStream();
            rpt.ExportToPdf(ms);
            ExChange(ms, string.Format("Invoice{0:yyyy-MM-dd}",DateTime.Now), context);

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
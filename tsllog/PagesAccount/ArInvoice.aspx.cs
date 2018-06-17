using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;

using Wilson.ORMapper;
using System.IO;
using Aspose.Cells;

public partial class Account_ArInvoice : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.dsArInvoice.FilterExpression = "1=0";
            this.txt_from.Date = DateTime.Today.AddDays(-7);
            this.txt_end.Date = DateTime.Today;
            this.cbo_PostInd.Text = "All";
            this.cbo_DocType.Text = "All";
            this.cbo_PaidInd.Text = "All";
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string dateFrom = "";
        string dateEnd = "";
        string where = "";
        if (txt_refNo.Text.Trim() != "")
            where = "DocNo like '%" + txt_refNo.Text.Trim() + "'";
        else if (this.txt_end.Value != null && this.txt_from.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateEnd = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
            where = "DocDate>='" + dateFrom + "' and DocDate<'" + dateEnd + "'";
        }
        if (this.cmb_PartyTo.Value != null)
        {
            if (where.Length > 0)
                where += " and PartyTo='" + this.cmb_PartyTo.Value + "'";
            else
                where = " PartyTo='" + this.cmb_PartyTo.Value + "'";
        }
        if (where.Length > 0)
        {
            if (this.cbo_DocType.Text != "All")
                where += " and DocType='"+this.cbo_DocType.Text+"' ";
			else
				where += "and (DocType='IV' or DocType='DN')";
			
            if (this.cbo_PostInd.Text == "Y")
                where += " and ExportInd='Y'";
            else if (this.cbo_PostInd.Text == "N")
                where += " and ExportInd!='Y'";
				
if (this.cbo_PaidInd.Text == "Y")
            where += " and isnull(BalanceAmt,0)=0";
        else if (this.cbo_PaidInd.Text == "N")
            where += " and isnull(BalanceAmt,0)<>0";
					
				
            this.dsArInvoice.FilterExpression = where;
        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("ARInvoiceList", true);
    }

    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
        string dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
        string dateEnd = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        string temp = invoice_download_excel(dateFrom, dateEnd);
        Response.Write("<script>window.open('" + temp + "');</script>");
    }


    public string invoice_download_excel(string DateFrom, string DateTo)
    {

        string rootPath = Directory.GetParent(HttpContext.Current.Server.MapPath("")).FullName;
        string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond + "";
        string to_file = Path.Combine(rootPath, "files", "Excel_DailyTrips", "inv_" + fileName + ".csv");

        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@DateFrom", DateFrom, SqlDbType.NVarChar, 10));
        list.Add(new ConnectSql_mb.cmdParameters("@DateTo", DateTo, SqlDbType.NVarChar, 10));
        string sql = string.Format(@"select dense_rank()over(order by det.DocNo)-1 as rowId,
convert(nvarchar,inv.DocDate,103) as DocDate,inv.DocType,inv.DocNo,inv.PartyTo,inv.CurrencyId,det.GstType,det.GstAmt,0 as zero,det.DocAmt,det.DocAmt+det.GstAmt as Total,det.ChgDes1,det.AcCode 
from XAArInvoiceDet as det
left outer join XAArInvoice as inv on det.DocNo=inv.DocNo
where IsNull(inv.cancelind,'N')='N' and inv.DocDate>=@DateFrom and inv.DocDate<@DateTo");
        DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
        string DataList = Common.DataTableToJson(dt);

        Aspose.Cells.License license = new Aspose.Cells.License();
        license.SetLicense(HttpContext.Current.Server.MapPath("~/Aspose.lic"));
        Workbook wb = new Workbook();
        //wb.Open(file);
        Worksheet ws = wb.Worksheets[0];
        int baseRow = 0;
        int i = 0;
        for (; i < dt.Rows.Count;)
        {
            ws.Cells[baseRow + i, 0].PutValue(dt.Rows[i]["rowId"]);
            ws.Cells[baseRow + i, 1].PutValue(dt.Rows[i]["DocDate"]);
            ws.Cells[baseRow + i, 2].PutValue(invoice_download_excel_DocType(dt.Rows[i]["DocType"].ToString()));
            ws.Cells[baseRow + i, 3].PutValue(dt.Rows[i]["DocNo"]);
            ws.Cells[baseRow + i, 4].PutValue(dt.Rows[i]["PartyTo"]);
            ws.Cells[baseRow + i, 5].PutValue(dt.Rows[i]["CurrencyId"]);
            ws.Cells[baseRow + i, 6].PutValue(invoice_download_excel_GSTTYpe(dt.Rows[i]["GstType"].ToString()));
            ws.Cells[baseRow + i, 7].PutValue(dt.Rows[i]["GstAmt"]);
            ws.Cells[baseRow + i, 8].PutValue(dt.Rows[i]["zero"]);
            ws.Cells[baseRow + i, 9].PutValue(dt.Rows[i]["DocAmt"]);
            ws.Cells[baseRow + i, 10].PutValue(dt.Rows[i]["Total"]);
            ws.Cells[baseRow + i, 11].PutValue(dt.Rows[i]["ChgDes1"]);
            ws.Cells[baseRow + i, 12].PutValue(dt.Rows[i]["AcCode"]);

            i++;
        }
        wb.Save(to_file);

        //string context = Common.StringToJson(Path.Combine("files", "Excel_DailyTrips", "inv_"  + fileName + ".csv"));
        //Common.WriteJson(true, context);
        string context = "../files/Excel_DailyTrips/inv_" + fileName + ".csv";// Path.Combine("../files", "Excel_DailyTrips", "inv_" + fileName + ".csv");
        return context;
    }

    private string invoice_download_excel_DocType(string par)
    {
        string res = par;
        switch (res)
        {
            case "IV":
                res = "IN";
                break;
        }
        return res;
    }
    private string invoice_download_excel_GSTTYpe(string par)
    {
        string res = par;
        switch (res)
        {
            case "S":
                res = "GST";
                break;
            case "Z":
                res = "ZER";
                break;
            default:
                res = "NA";
                break;
        }
        return res;
    }
}

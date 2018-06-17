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

public partial class Rpt_PrintView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Request.Params["doc"] != null)
        {
            string reportName = Request.Params["doc"].ToString();
            string no = Request.Params["no"].ToString();
            

            //doc
            //Cfs Import
            //1:Tallysheet
            //2.Haulier trucking advice
            //3.Reimbursement
            //4.Tallysheet prepare
            //40:for haulier

            // cfs export
            //5.Tallysheet
            //6.Haulier trucking advice
            //7.barcode
            //8.sticker

            //Receipt
            //9.Receipt
            //10.old Receipt
            //11. receipt cn
            //12. receipt dn

            //Tpt
            //12. Dn
            //13: so
            //14: daily schedule
            //15: tpt driver schedule
            //16. Dn barcode
            //17: so barcode
            //18: so barcode
            //19: client report
            //bill
            //invoice 50
            //51:CN

            //100 :import sn no
            //101:export sn no

            string userName =HttpContext.Current.User.Identity.Name;
            MemoryStream ms = new MemoryStream();
            string fileName = "";
            try
            {
                if (reportName == "1")//import tallysheet
                {
                    ms = PrintCfsImpTs(no,userName);
                    fileName = "TallySheet" + no;
                }
                else if (reportName == "4")//import tallysheet prepare
                {
                    ms = PrintCfsImpTs_Pre(no,userName);
                    fileName = "TallySheet" + no;
                }
                else if (reportName == "4c")//import tallysheet prepare
                {
                    ms = PrintCfsImpTs_Pre3(no,userName);
                    fileName = "TallySheet3" + no;
                }
                else if (reportName == "4a")//import tallysheet prepare
                {
                    ms = PrintCfsImpTs_Pre2(no,userName);
                    fileName = "TallySheet2" + no;
                }
                else if (reportName == "2")
                {
                    ms = PrintCfsImpHaulier(no);
                    fileName = "HaulierTruckingAdvice" + no;
                }
                else if (reportName == "40")
                {
                    //ms = PrintCfsImpHaulier1(no);
                    ms = PrintAuthLetter(no);
                    fileName = "HaulierTruckingAdvice" + no;
                }
                else if (reportName == "3")
                {
                    //ms = PrintCfsImpReimbursement(no,userName);
                    //fileName = "Reimbursement" + no;
                }
                else if (reportName == "5")
                {
                    ms = PrintCfsExpTs(no,userName);
                    fileName = "TallySheet" + no;
                }
                else if (reportName == "5a")
                {
                    ms = PrintCfsExpTs2(no,userName);
                    fileName = "TallySheet" + no;
                }
                else if (reportName == "5b")
                {
                    ms = PrintCfsExpTs3(no,userName);
                    fileName = "TallySheet" + no;
                }
                else if (reportName == "5b")
                {
                    ms = PrintCfsExpTs3(no,userName);
                    fileName = "BookingList" + no;
                }
                else if (reportName == "6")
                {
                    ms = PrintCfsExpHaulier(no);
                    fileName = "HaulierTruckingAdvice" + no;
                }
                else if (reportName == "9")
                {
                    ms = PrintReceipt(no);
                    fileName = "Receipt" + no;
                }
                else if (reportName == "9a")
                {
                    ms = PrintReceipt2(no);
                    fileName = "Receipt" + no;
                }
                else if (reportName == "9b")
                {
                    ms = PrintReceiptAcc(no);
                    fileName = "Receipt" + no;
                }
                else if (reportName == "11")
                {
                    ms = PrintReceiptCn(no);
                    fileName = "DnCredit" + no;
                }
                else if (reportName == "12")
                {
                    ms = PrintReceiptDn(no);
                    fileName = "DnDebit" + no;
                }
                else if (reportName == "100")
                {
                    ms = PrintSn(no,"I");
                    fileName = "ReceiptCn" + no;
                }
                else if (reportName == "101")
                {
                    ms = PrintSn(no,"E");
                    fileName = "ReceiptCn" + no;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message+ex.StackTrace+ex.InnerException);
		throw new Exception("Error: " + ex.Message + ex.StackTrace);
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
            Response.Write(string.Format("error : {0} \r\n size: {3} \r\n file : [{1}]\r\n trace : {2}",ex.Message,fileName,ex.StackTrace, bt.Length));
        }
        ms.Dispose();
        str.Dispose();
    }
    #region psakd report
    private MemoryStream PrintCfsImpTs(string orderNo,string userName)
    {
        XtraReport rpt = new XtraReport();
        
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\TallySheet_Imp.repx"));
        //rpt.DataSource = PrintFreightDoc.DsImpTs(orderNo,userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintCfsImpTs_Pre(string orderNo, string userName)
    {
        XtraReport rpt = new XtraReport();
        
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\TallySheet_Imp_Pre.repx"));
        rpt.DataSource = PrintFreightDoc.DsImpTs_Pre(orderNo, userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }

    private MemoryStream PrintCfsImpTs_Pre3(string orderNo, string userName)
    {
        XtraReport rpt = new XtraReport();
        
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\TallySheet_Imp_Pre3.repx"));
        rpt.DataSource = PrintFreightDoc.DsImpTs_Pre(orderNo,userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }

    private MemoryStream PrintCfsImpTs_Pre2(string orderNo, string userName)
    {
        XtraReport rpt = new XtraReport();
        
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\TallySheet_Imp_Pre2.repx"));
        //rpt.DataSource = PrintFreightDoc.DsImpTs_Pre2(orderNo,userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintCfsExpTs(string orderNo, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\TallySheet_Exp_Pre.repx"));
        //rpt.DataSource = PrintFreightDoc.DsExpTs(orderNo,userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }

    private MemoryStream PrintCfsExpTs3(string orderNo, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\TallySheet_Exp_Advice.repx"));
       // rpt.DataSource = PrintFreightDoc.DsExpAdvice(orderNo,userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }

	private MemoryStream PrintCfsExpTs2(string orderNo, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\TallySheet_Exp_Pre3.repx"));
       // rpt.DataSource = PrintFreightDoc.DsExpTs(orderNo,userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }

    private MemoryStream PrintCfsExpTs3a(string orderNo, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\TallySheet_Exp_List.repx"));
        //rpt.DataSource = PrintFreightDoc.DsExpTs(orderNo,userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }

    private MemoryStream PrintCfsImpHaulier(string orderNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\HaulierTruckingAdvice_Imp.repx"));
        rpt.DataSource = PrintFreightDoc.PrintImpHaulier(orderNo);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintCfsImpHaulier1(string orderNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\HaulierTruckingAdvice_Imp1.repx"));
        //rpt.DataSource = PrintFreightDoc.PrintImpHaulier1(orderNo);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }

    private MemoryStream PrintCfsExpHaulier(string orderNo)
    {
        XtraReport rpt = new XtraReport();
         rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\HaulierTruckingAdvice_Exp.repx"));
        //rpt.DataSource = PrintFreightDoc.PrintExpHaulier(orderNo);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintCfsImpReimbursement(string orderNo, string userName)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\Rebate_CfsImp.repx"));
        //rpt.DataSource = PrintFreightDoc.dsReimbursement(orderNo, userName);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintReceipt(string recNo)
    {
        XtraReport rpt = new XtraReport();
 
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\Receipt_A4_2014.repx"));
        //rpt.DataSource = PrintFreightDoc.DsReceipt(recNo);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintReceiptAcc(string recNo)
    {
        XtraReport rpt = new XtraReport();
 
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\Receipt_A4_Acc.repx"));
        //rpt.DataSource = PrintFreightDoc.DsReceiptAcc(recNo);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintReceipt2(string recNo)
    {
        XtraReport rpt = new XtraReport();
 
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\Receipt_A4_2014.repx"));
        //rpt.DataSource = PrintFreightDoc.DsReceipt2(recNo);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintReceiptCn(string recNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\Receipt.repx"));
        //rpt.DataSource = PrintFreightDoc.DsReceiptCn(recNo);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    private MemoryStream PrintReceiptDn(string recNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\Receipt.repx"));
        //rpt.DataSource = PrintFreightDoc.DsReceiptDn(recNo);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
    #endregion
    private MemoryStream PrintSn(string snNo,string jobType)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\SnBarCode .repx"));
        DataTable tab = new DataTable();
        tab.Columns.Add("SnNo");
        DataRow row = tab.NewRow();
        if (jobType == "I")
        {
            row["SnNo"] = "03-"+snNo+"-00";
        }
        else
        {
            row["SnNo"] = "23-" + snNo + "-00";
        }
        tab.Rows.Add(row);
        rpt.DataSource = tab;

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }

    private MemoryStream PrintAuthLetter(string orderNo)
    {
        XtraReport rpt = new XtraReport();
        rpt.LoadLayout(Server.MapPath(@"~\Modules\Freight\Report\repx\AuthLetter.repx"));
        //rpt.DataSource = PrintFreightDoc.PrintAuthLetter(orderNo);

        System.IO.MemoryStream str = new MemoryStream();
        rpt.ExportToPdf(str);
        return str;
    }
}

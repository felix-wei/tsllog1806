using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using Wilson.ORMapper;
using System.Data.SqlClient;

public partial class PagesAccount_CheckError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.cmb_Year.Text = DateTime.Today.AddMonths(-1).Year.ToString();
            this.cmb_Period.Text = "1";// DateTime.Today.AddMonths(-1).Month.ToString();
        }
    }
    protected void btn_ar_click(object sender, EventArgs e)
    {
            if (this.cmb_Year.Text.Trim().Length > 0 && this.cmb_Period.Text.Trim().Length > 0)
        {
            CheckArBillError(this.cmb_Year.Text, this.cmb_Period.Text);
            CheckArReceiptError(this.cmb_Year.Text, this.cmb_Period.Text);
            Response.Write("</br></br></br></br></br>Check complete");
            Response.Write("</br><input type='button' value='Go Back' onclick=\"window.location = '/PagesAccount/Control/checkerror.aspx'\" />");
            Response.Flush();
            Response.End();
        }
    }
    protected void btn_ap_click(object sender, EventArgs e)
    {
        if (this.cmb_Year.Text.Trim().Length > 0 && this.cmb_Period.Text.Trim().Length > 0)
        {
            CheckApBillError(this.cmb_Year.Text, this.cmb_Period.Text);
            CheckApPaymentError(this.cmb_Year.Text, this.cmb_Period.Text);
            Response.Write("</br></br></br></br></br>Check complete");
            Response.Write("</br><input type='button' value='Go Back' onclick=\"window.location = '/PagesAccount/Control/checkerror.aspx'\" />");
            Response.Flush();
            Response.End();
        }
    }
    protected void btn_gl_click(object sender, EventArgs e)
    {
        if (this.cmb_Year.Text.Trim().Length > 0 && this.cmb_Period.Text.Trim().Length > 0)
        {
            CheckGLError(this.cmb_Year.Text, this.cmb_Period.Text);
            Response.Write("</br></br></br></br></br>Check complete");
            Response.Write("</br><input type='button' value='Go Back' onclick=\"window.location = '/PagesAccount/Control/checkerror.aspx'\" />");
            Response.Flush();
            Response.End();
        }
    }
    #region Check XaArInvoice Error
    private void CheckArBillError(string year, string period)
    {
        string sql = string.Format(@"select * from (select mast.docno,mast.doctype,mast.locamt,sum(round((case when det.AcSource='CR' then det.locamt else -det.locamt end)*mast.exrate,2)) as detamt
from xaarinvoicedet det inner join xaarinvoice mast on mast.sequenceid=det.docid
where mast.acyear='{0}' and mast.acperiod='{1}' and mast.exportind='Y'
group by mast.docno,mast.doctype,mast.locamt) aa
where abs(locamt)<>abs(detamt)", year, period);
        //string sql = @"select SequenceId,AcYear,AcPeriod,AcSource,DocNo,DocType,ExRate,DocAmt,LocAmt from xaarinvoice " + where;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docNo = reader["DocNo"].ToString();

            string docType = reader["DocType"].ToString();

            Response.Write(string.Format("</br>Type:{0},Bill No :{1},have error", docType, docNo));
        }
    }

    private void CheckArReceiptError(string year,string period)
    {
        string sql = string.Format(@"select * from (select mast.docno,mast.doctype,mast.locamt, sum(case when det.AcSource='CR' then det.locamt else -det.locamt end) as detamt
from xaarreceiptdet det inner join xaarreceipt mast on mast.sequenceid=det.repid
where mast.acyear='{0}' and mast.acperiod='{1}' and mast.exportind='Y'
group by mast.docno,mast.doctype,mast.locamt) aa
where abs(locamt)<>abs(detamt)", year, period);
        //string sql = @"select SequenceId,AcYear,AcPeriod,AcSource,DocNo,DocType,LocAmt from xaarreceipt " + where;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docNo = reader["DocNo"].ToString();
            string docType = reader["DocType"].ToString();
            Response.Write(string.Format("</br>Type:{0},Bill No :{1},have error", docType, docNo));
        }
    }
    #endregion
    #region Check AP
    private void CheckApBillError(string year,string period)
    {

        string sql = string.Format(@"select * from (select mast.docno,mast.doctype,mast.locamt,sum(round((case when det.AcSource='CR' then det.locamt else -det.locamt end)*mast.exrate,2)) as detamt
from xaappayabledet det inner join xaappayable mast on mast.sequenceid=det.docid
where mast.acyear='{0}' and mast.acperiod='{1}'
group by mast.docno,mast.doctype,mast.locamt) aa
where abs(locamt)<>abs(detamt)", year, period);
        //string sql = @"select SequenceId,AcYear,AcPeriod,AcSource,DocNo,DocType,ExRate,DocAmt,LocAmt from XaApPayable " + where;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docNo = reader["DocNo"].ToString();
            string docType = reader["DocType"].ToString();
            Response.Write(string.Format("</br>Type:{0},Bill No :{1},have error", docType, docNo));
        }
    }

    private void CheckApPaymentError(string year,string peroid)
    {


        string sql = string.Format(@"select * from (select mast.docno,mast.doctype,mast.locamt, sum(case when det.AcSource='DB' then det.locamt else -det.locamt end) as detamt
from xaappaymentdet det inner join xaappayment mast on mast.sequenceid=det.payid
where mast.acyear='{0}' and mast.acperiod='{1}' and mast.exportind='Y'
group by mast.docno,mast.doctype,mast.locamt) aa
where abs(locamt)<>abs(detamt)", year, peroid);
        //string sql = @"select SequenceId,AcYear,AcPeriod,AcSource,DocNo,DocType,LocAmt from XAApPayment " + where;
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docNo = reader["DocNo"].ToString();
            string docType = reader["DocType"].ToString();
            Response.Write(string.Format("</br>Type:{0},Bill No :{1},have error", docType, docNo));
        }
    }
    #endregion
    #region Check gl Error
    private void CheckGLError(string year, string period)
    {
        string sql = string.Format(@"select * from (select GlNo,sum(currencycramt) cramt,sum(currencydbamt) dbamt from xaglentrydet
where acyear='{0}' and acperiod='{1}' group by glno) as aa where cramt<>dbamt", year, period);
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docId = reader["GlNo"].ToString();

            string sql1 = string.Format("SELECT DocNo, DocType,SupplierBillNo FROM XAGlEntry WHERE (SequenceId = '{0}')", docId);

            string docNo = GetObj(string.Format("SELECT DocNo FROM XAGlEntry WHERE (SequenceId = '{0}')", docId));
            string docType = GetObj(string.Format("SELECT DocType FROM XAGlEntry WHERE (SequenceId = '{0}')", docId));
            Response.Write(string.Format("</br>Type:{0},Bill No :{1} have error", docType, docNo));
        }
    }
    #endregion

    private static string GetObj(string sql)
    {
        string s = "";
        try
        {
            s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        }
        catch
        { }
        return s;
    }
}

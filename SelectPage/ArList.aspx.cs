using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class SelectPage_ArList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string invNo = this.txt_No.Text.Trim();
        string sql = "";
        if (invNo.Length > 0)
        {
            sql = string.Format("select sequenceid from XaArInvoice where DocNo='{0}'", invNo);
        }
        if (sql.Length > 0)
        {
            string sequenceId = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0");
            this.dsArDet.FilterExpression = "DocId='" + sequenceId + "'";
        }
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["id"] != null )
        {
            int invId = SafeValue.SafeInt(Request.QueryString["id"],0);
            int index = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(string.Format("Select max(QuoteLineNo) FROM SeaQuoteDet1 where QuoteId='{0}'", invId)), 1);
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    index++;
                    int sequenceId = list[i].docId;

                    string sql = string.Format(@"SELECT ChgCode,ChgDes1, Currency,ExRate, Price, Unit, GstType, Gst FROM XaArInvoiceDet where SequenceId='{0}' ", sequenceId);
                    DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                    if (tab.Rows.Count == 1)
                    {
                        string chgCode = tab.Rows[0]["ChgCode"].ToString();
                        string chgDes = tab.Rows[0]["ChgDes1"].ToString();
                        string currencyDes = tab.Rows[0]["Currency"].ToString();
                        decimal exRate = SafeValue.SafeDecimal(tab.Rows[0]["ExRate"], 1);
                        decimal price = SafeValue.SafeDecimal(tab.Rows[0]["Price"], 0);
                        string unit = tab.Rows[0]["Unit"].ToString();
                        decimal gst = SafeValue.SafeDecimal(tab.Rows[0]["Gst"], 0);
                        string gstType = tab.Rows[0]["GstType"].ToString();

                        SeaQuoteDet1 det = new SeaQuoteDet1();
                        det.Amt =0;
                        det.ChgCode=chgCode;
                        det.ChgDes = chgDes;
                        det.Currency = currencyDes;
                        det.ExRate = exRate;
                        det.Gst = gst;
                        det.GstType = gstType;
                        det.MinAmt = 0;
                        det.Price = price;
                        det.Qty = 1;
                        det.QuoteId = invId.ToString();
                        det.QuoteLineNo = index;
                        det.Rmk = " ";
                        det.Unit = unit;
                        C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(det);
                    }
                }
                catch { }
            }
        }
        else
        {
            e.Result = "Error, Pls refresh your invoice";
        }
    }

    List<Record> list = new List<Record>();
    internal class Record
    {
        public int docId = 0;
        public bool isPay = false;
        public Record(int _docId)
        {
            docId = _docId;
        }

    }
    public decimal totPayAmt = 0;
    private void OnLoad()
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 100;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox docId = this.ASPxGridView1.FindRowTemplateControl(i, "txt_docId") as ASPxTextBox;
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (docId != null&& isPay != null && isPay.Checked)
            {
                    list.Add(new Record(SafeValue.SafeInt(docId.Text, 0)
                        ));
            }
        }
    }
}

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

public partial class PagesQuote_LclQuoteList : System.Web.UI.Page
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
        if (invNo.Length > 0)
        {
            string sql = string.Format("select sequenceid from SeaQuote1 where  FclLclInd='FCL' and QuoteNo='{0}'", invNo);
            string sequenceId = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0");
            string repId = Request.QueryString["no"].ToString();
            string impExpInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select ImpExpInd from SeaQuote1 where SequenceId='" + repId + "'"));
            this.dsQuotationDet.FilterExpression = "FclLclInd='FCL' and QuoteId='" + sequenceId + "' and  ImpExpInd='" + impExpInd + "'";
        }

    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["no"] != null)
        {
            int invId = SafeValue.SafeInt(Request.QueryString["no"], 0);
            string impExpInd = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select ImpExpInd from SeaQuote1 where SequenceId='" + invId + "'"));
            int index = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(string.Format("Select max(QuoteLineNo) FROM SeaQuoteDet1 where QuoteId='{0}'", invId)), 1);
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    index++;
                    int sequenceId = list[i].docId;

                    string sql = string.Format(@"SELECT ChgCode, Currency,ExRate, Price, Unit, MinAmt, Rmk, Qty, Amt, GstType, Gst, GroupTitle, 
                      ChgDes FROM SeaQuoteDet1 where SequenceId='{0}' order by QuoteLineNo", sequenceId);
                    DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                    if (tab.Rows.Count == 1)
                    {
                        string chgCode = tab.Rows[0]["ChgCode"].ToString();
                        string chgDes = tab.Rows[0]["ChgDes"].ToString();
                        string currencyDes = tab.Rows[0]["Currency"].ToString();
                        decimal exRate = SafeValue.SafeDecimal(tab.Rows[0]["ExRate"], 1);
                        decimal price = SafeValue.SafeDecimal(tab.Rows[0]["Price"], 0);
                        string unit = tab.Rows[0]["Unit"].ToString();
                        decimal minAmt = SafeValue.SafeDecimal(tab.Rows[0]["MinAmt"], 0);
                        decimal amt = SafeValue.SafeDecimal(tab.Rows[0]["Amt"], 0);
                        string rmk = tab.Rows[0]["Rmk"].ToString();
                        decimal qty = SafeValue.SafeDecimal(tab.Rows[0]["Qty"], 1);
                        decimal gst = SafeValue.SafeDecimal(tab.Rows[0]["Gst"], 0);
                        string gstType = tab.Rows[0]["GstType"].ToString();
                        string groupTitle = SafeValue.SafeString(tab.Rows[0]["GroupTitle"],"");

                        SeaQuoteDet1 det = new SeaQuoteDet1();
                        det.Amt =amt;
                        det.ChgCode=chgCode;
                        det.ChgDes = chgDes;
                        det.Currency = currencyDes;
                        det.ExRate = exRate;
                        det.GroupTitle = groupTitle;
                        det.Gst = gst;
                        det.GstType = gstType;
                        det.MinAmt = minAmt;
                        det.Price = price;
                        det.Qty = qty;
                        det.QuoteId = invId.ToString();
                        det.QuoteLineNo = index;
                        det.Rmk = rmk;
                        det.Unit = unit;
                        det.ImpExpInd = impExpInd;
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

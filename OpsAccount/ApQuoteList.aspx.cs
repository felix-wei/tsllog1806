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

public partial class PagesFreight_Account_ApQuoteList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.form1.Focus();
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
            string sql = string.Format("select sequenceid from SeaApQuote1 where QuoteNo='{0}'", invNo);
            string sequenceId=SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql),"0");
            this.dsQuoteDet.FilterExpression = "QuoteId='" + sequenceId + "'";
        }

    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["no"] != null)
        {
            string invNo = Request.QueryString["no"].ToString();
            int invId = SafeValue.SafeInt(Request.QueryString["id"], 0);
            string refNo = "";
            string jobNo = "";
            string mastType = "";
            string docType = "";
            string sql_mast = string.Format("select DocType,MastRefNo,JobRefNo,MastType from XAApPayable where SequenceId='{0}'", invId);
            DataTable tab_mast = C2.Manager.ORManager.GetDataSet(sql_mast).Tables[0];
            if (tab_mast.Rows.Count == 1)
            {
                docType = SafeValue.SafeString(tab_mast.Rows[0]["DocType"]);
                refNo = SafeValue.SafeString(tab_mast.Rows[0]["MastRefNo"]);
                jobNo = SafeValue.SafeString(tab_mast.Rows[0]["JobRefNo"]);
                mastType = SafeValue.SafeString(tab_mast.Rows[0]["MastType"]);
            }

            if (mastType == "SI")
                sql_mast = string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM SeaImport where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
            else if (mastType == "SE")
                sql_mast = string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM SeaExport where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
            decimal qty = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql_mast), 0);

            int index = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(string.Format("Select max(DocLineNo) FROM XAApPayableDet where DocId='{0}'", invId)), 0);
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    index++;
                    int sequenceId = list[i].docId;

                    string sql = string.Format(@"SELECT  ChgCode, ChgDes, Currency, ExRate,Price, Unit, MinAmt, Rmk, Qty, Amt, GstType, Gst
 FROM SeaApQuoteDet1 where SequenceId='{0}' order by QuoteLineNo", sequenceId);
                    DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                    if (tab.Rows.Count == 1)
                    {
                        string chgCode = tab.Rows[0]["ChgCode"].ToString();
                        string acCode = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select ApCode from XXchgcode where ChgCodeId='{0}'", chgCode)));
                        string acSource = "DB";
                        if (docType == "SC")
                            acSource = "CR";
                        string chgDes1 = tab.Rows[0]["ChgDes"].ToString();
                        decimal price = SafeValue.SafeDecimal(tab.Rows[0]["Price"], 0);
                        string unit = tab.Rows[0]["Unit"].ToString().ToUpper();
                        string currencyDes = tab.Rows[0]["Currency"].ToString();
                        decimal exRateDes = SafeValue.SafeDecimal(tab.Rows[0]["ExRate"], 0);
                        decimal gst = SafeValue.SafeDecimal(tab.Rows[0]["Gst"], 0);
                        string gstType = tab.Rows[0]["GstType"].ToString();

                        if (qty == 0)
                            qty = SafeValue.SafeInt(tab.Rows[i]["Qty"], 1);

                        XAApPayableDet det = new XAApPayableDet();
                        det.AcCode = acCode;
                        det.AcSource = acSource;
                        det.ChgCode = chgCode;
                        det.ChgDes1 = chgDes1;
                        det.ChgDes2 = "";
                        det.ChgDes3 = "";
                        det.Currency = currencyDes;
                        det.ExRate = exRateDes;

                        det.Price = price;
                        det.Qty = qty;
                        det.Unit = unit;

                        det.Gst = gst;
                        det.GstType = gstType;

                        decimal amt = SafeValue.ChinaRound(qty * price, 2);
                        decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                        decimal docAmt = amt + gstAmt;
                        decimal locAmt = SafeValue.ChinaRound(docAmt * exRateDes, 2);
                        det.GstAmt = gstAmt;
                        det.DocAmt = docAmt;
                        det.LocAmt = locAmt;
                        det.LineLocAmt = 0;
                        det.DocId = invId;
                        det.DocLineNo = index;
                        det.DocNo = invNo;
                        det.DocType = docType;
                        det.MastRefNo = refNo;
                        det.JobRefNo = jobNo;
                        det.MastType = mastType;
                        det.SplitType = "SET";
                        C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(det);
                    }
                }
                catch { }
            }
            UpdateApMaster(invId,docType);
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
    private void UpdateApMaster(int docId, string docType)
    {
        string sql = string.Format("update XaApPayableDet set LineLocAmt=locAmt* (select ExRate from XaApPayable where SequenceId=XaApPayableDet.docid) where DocId='{0}'", docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
        decimal docAmt = 0;
        decimal locAmt = 0;
        sql = string.Format("select AcSource,LocAmt,LineLocAmt from XAApPayableDet where DocId='{0}'", docId);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            if (tab.Rows[i]["AcSource"].ToString() == "DB")
            {
                docAmt += SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                locAmt += SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
            }
            else
            {
                docAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                locAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
            }

        }
        if (docType == "SC")
        {
            docAmt = -docAmt;
            locAmt = -locAmt;
        }
        //locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.DocAmt)
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", docId)), 0);
        balAmt += SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.DocAmt)
FROM XAArReceiptDet AS det INNER JOIN  XAArInvoice AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", docId)), 0);

        sql = string.Format("Update XAApPayable set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
}

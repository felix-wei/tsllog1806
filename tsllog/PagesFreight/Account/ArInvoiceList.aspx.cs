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

public partial class PagesFreight_Account_ArInvoiceList : System.Web.UI.Page
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
            string sql = string.Format("select sequenceid from XaArInvoice where DocType='IV' and DocNo='{0}'",invNo);
            string sequenceId=SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql),"0");
            this.dsArInvDet.FilterExpression = "DocId='" + sequenceId + "'";
        }

    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["no"] != null)
        {
            string invNo = Request.QueryString["no"].ToString();
            int invId = SafeValue.SafeInt(Request.QueryString["id"],0);
            string refNo = "";
            string jobNo = "";
            string mastType = "";
            string sql_mast = string.Format("select MastRefNo,JobRefNo,MastType from XAArInvoice where SequenceId='{0}'",invId);
            DataTable tab_mast = C2.Manager.ORManager.GetDataSet(sql_mast).Tables[0];
            if (tab_mast.Rows.Count == 1)
            {
                refNo = SafeValue.SafeString(tab_mast.Rows[0]["MastRefNo"]);
                jobNo = SafeValue.SafeString(tab_mast.Rows[0]["JobRefNo"]);
                mastType = SafeValue.SafeString(tab_mast.Rows[0]["MastType"]);
            }
            if (mastType == "SI")
                sql_mast = string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM SeaImport where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
            else if (mastType == "SE")
                sql_mast = string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM SeaExport where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
            decimal qty = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql_mast), 0);

            int index = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(string.Format("Select max(DocLineNo) FROM XAArInvoiceDet where DocId='{0}'", invId)), 0);
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    index++;
                    int sequenceId = list[i].docId;

                    string sql = string.Format(@"SELECT AcCode, AcSource, ChgCode, ChgDes1, GstType, Qty, Price, Unit, Currency,
                       ExRate, Gst, GstAmt, DocAmt, LocAmt FROM XAArInvoiceDet where SequenceId='{0}' order by DocLineNo", sequenceId);
                    DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                    if (tab.Rows.Count == 1)
                    {
                        string chgCode = tab.Rows[0]["ChgCode"].ToString();
                        string acCode = tab.Rows[0]["AcCode"].ToString();
                        string acSource = tab.Rows[0]["AcSource"].ToString();
                        string chgDes1 = tab.Rows[0]["ChgDes1"].ToString();
                        //decimal qty = SafeValue.SafeDecimal(tab.Rows[0]["Qty"], 1);
                        decimal price = SafeValue.SafeDecimal(tab.Rows[0]["Price"], 0);
                        string unit = tab.Rows[0]["Unit"].ToString();
                        string currencyDes = tab.Rows[0]["Currency"].ToString();
                        decimal exRateDes = SafeValue.SafeDecimal(tab.Rows[0]["ExRate"], 0);
                        decimal gst = SafeValue.SafeDecimal(tab.Rows[0]["Gst"], 0);
                        string gstType = tab.Rows[0]["GstType"].ToString();

                        XAArInvoiceDet det = new XAArInvoiceDet();
                        det.AcCode =acCode;
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

                        decimal gstAmt = SafeValue.SafeDecimal(tab.Rows[0]["GstAmt"], 0);
                        decimal docAmt = SafeValue.SafeDecimal(tab.Rows[0]["DocAmt"], 0);
                        decimal locAmt = SafeValue.SafeDecimal(tab.Rows[0]["LocAmt"], 0);
                        det.GstAmt = gstAmt;
                        det.DocAmt = docAmt;
                        det.LocAmt = locAmt;

                        det.DocId = invId;
                        det.DocLineNo = index;
                        det.DocNo = invNo;
                        det.DocType = "IV";
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
            UpdateMaster(invId);
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
    private void UpdateMaster(int docId)
    {
        string sql = "select SUM(LocAmt) from XAArInvoiceDet where DocId='" + docId + "'";
        sql = "select ExRate from XAArInvoice where SequenceId ='" + docId + "'";
        decimal exRate = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 1);

        decimal docAmt = 0;
        decimal locAmt = 0;
        sql = string.Format("select AcSource,LocAmt from XAArInvoiceDet where DocId='{0}'", docId);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            docAmt += SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
            locAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0) * exRate, 2);
        }

        sql = string.Format("Update XAArInvoice set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt, docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
}

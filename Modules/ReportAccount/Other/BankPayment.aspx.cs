using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;

public partial class PagesAccount_Other_BankPayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddMonths(-1);
            this.txt_end.Date = DateTime.Today;
            this.cmb_acCode.Value = "2000";
            this.dateClear.Date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
        }
        OnLoad();
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        if (null == this.txt_from.Value)
            throw new Exception("Please select the begin date");
        else if (null == this.txt_end.Value)
            throw new Exception("Please select the end date");
        else if (null == this.cmb_acCode.Value)
            throw new Exception("Please select the chart of account");
        else
            BindData(this.txt_from.Date, this.txt_end.Date, this.cmb_acCode.Value.ToString());
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string docId = "";
        public string docType = "";
        public DateTime bankDate = new DateTime(1900, 1, 1);
        public bool bankRec = false;
        public Record(string _docId, string docType, bool _bankRec, DateTime _bankDate)
        {
            docId = _docId;
            bankRec = _bankRec;
            bankDate = _bankDate;
        }

    }
    private void OnLoad()
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 10000;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox docId = this.ASPxGridView1.FindRowTemplateControl(i, "txt_oid") as ASPxTextBox;
            ASPxTextBox docType = this.ASPxGridView1.FindRowTemplateControl(i, "txt_docType") as ASPxTextBox;
            ASPxCheckBox bankRec = this.ASPxGridView1.FindRowTemplateControl(i, "ck_bankRec") as ASPxCheckBox;
            ASPxDateEdit bankDate = this.ASPxGridView1.FindRowTemplateControl(i, "date_bankDate") as ASPxDateEdit;
            if (null != docId && null != docType && null != bankDate && null != bankRec)
            {
                list.Add(new Record(docId.Text,
                    docType.Text,
                    bankRec.Checked,
                    bankDate.Date)
                    );
            }
        }
    }
    private void BindData(DateTime d1, DateTime d2, string acCode)
    {
        string sql = string.Format(@"SELECT SequenceId, DocNo,DocType, DocDate, ChqNo, ChqDate, BankRec, BankDate
FROM XAArReceipt WHERE DocType = 'PC' AND (DocDate > ='{0}') AND (DocDate < '{1}') AND (ChqDate > '2000-10-01') and (AcCode='{2}')", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), acCode);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        DataTable rptTab = new DataTable();
        rptTab.Columns.Add("Oid");
        rptTab.Columns.Add("SequenceId");
        rptTab.Columns.Add("DocNo");
        rptTab.Columns.Add("DocType");
        rptTab.Columns.Add("DocDate", typeof(DateTime));
        rptTab.Columns.Add("ChqNo");
        rptTab.Columns.Add("ChqDate", typeof(DateTime));
        rptTab.Columns.Add("BankRec", typeof(bool));
        rptTab.Columns.Add("BankDate", typeof(DateTime));
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            DataRow row = rptTab.NewRow();
            row["Oid"] = i;
            row["SequenceId"] = tab.Rows[i]["SequenceId"];
            row["DocNo"] = tab.Rows[i]["DocNo"];
            row["DocType"] = tab.Rows[i]["DocType"];
            row["DocDate"] = SafeValue.SafeDate(tab.Rows[i]["DocDate"], new DateTime(1900, 1, 1));
            row["ChqNo"] = tab.Rows[i]["ChqNo"];
            row["ChqDate"] = SafeValue.SafeDate(tab.Rows[i]["ChqDate"], new DateTime(1900, 1, 1));
            row["BankRec"] = false;
            if (SafeValue.SafeString(tab.Rows[i]["BankRec"], "N") == "Y")
                row["BankRec"] = true;
            row["BankDate"] = SafeValue.SafeDate(tab.Rows[i]["BankDate"], new DateTime(1900, 1, 1));

            rptTab.Rows.Add(row);
        }

        sql = string.Format(@"SELECT SequenceId, DocNo,DocType, DocDate, ChqNo, ChqDate, BankRec, BankDate
FROM XAApPayment WHERE DocType = 'PS' AND (DocDate > ='{0}') AND (DocDate < '{1}') AND (ChqDate > '2000-10-01') and (AcCode='{2}')", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), acCode);
        DataTable tab1 = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        int cnt = rptTab.Rows.Count;
        for (int i = 0; i < tab1.Rows.Count; i++)
        {
            DataRow row = rptTab.NewRow();
            row["Oid"] = cnt + i;
            row["SequenceId"] = tab1.Rows[i]["SequenceId"];
            row["DocNo"] = tab1.Rows[i]["DocNo"];
            row["DocType"] = tab1.Rows[i]["DocType"];
            row["DocDate"] = SafeValue.SafeDate(tab1.Rows[i]["DocDate"], new DateTime(1900, 1, 1));
            row["ChqNo"] = tab1.Rows[i]["ChqNo"];
            row["ChqDate"] = SafeValue.SafeDate(tab1.Rows[i]["ChqDate"], new DateTime(1900, 1, 1));
            row["BankRec"] = false;
            if (SafeValue.SafeString(tab1.Rows[i]["BankRec"], "N") == "Y")
                row["BankRec"] = true;
            row["BankDate"] = SafeValue.SafeDate(tab1.Rows[i]["BankDate"], new DateTime(1900, 1, 1));

            rptTab.Rows.Add(row);
        }
        this.ASPxGridView1.DataSource = rptTab;
        this.ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (null == this.txt_from.Value)
            throw new Exception("Please select the begin date");
        else if (null == this.txt_end.Value)
            throw new Exception("Please select the end date");
        else if (null == this.cmb_acCode.Value)
            throw new Exception("Please select the chart of account");
        else
            BindData(this.txt_from.Date, this.txt_end.Date, this.cmb_acCode.Value.ToString());
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string dates = e.Parameters;
        string[] dateArr = dates.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        if (dateArr.Length == list.Count)
        {
            e.Result = "Save success";
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string docId = list[i].docId;
                    string docType = list[i].docType;
                    bool bankRec = list[i].bankRec;
                    string[] s1 = dateArr[i].Split('/');
                    //  DateTime d1 = new DateTime(SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));

                    string bankDate = string.Format("{0}-{1}-{2}", SafeValue.SafeInt(s1[2], 0), SafeValue.SafeInt(s1[1], 0), SafeValue.SafeInt(s1[0], 0));

                    if (docType == "PC")
                    {
                        string sql = string.Format("Update XAArReceipt set BankRec='{0}',BankDate='{1}' where SequenceId='{2}'", bankRec ? "Y" : "N", bankDate, docId);
                        C2.Manager.ORManager.ExecuteCommand(sql);
                    }
                    else
                    {
                        string sql = string.Format("Update XAApPayment set BankRec='{0}',BankDate='{1}' where SequenceId='{2}'", bankRec ? "Y" : "N", bankDate, docId);
                        C2.Manager.ORManager.ExecuteCommand(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }
        else
        {
            e.Result = "Please reopen this page";
        }
    }

}

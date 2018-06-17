using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxEditors;

public partial class PagesFreight_SelectPage_AirStdRateLcl_multi : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            System.Windows.Forms.SendKeys.SendWait("{TAB}");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["no"] != null)
            {
                this.dsAirQuoteDet.FilterExpression = " FclLclInd='Lcl' and  QuoteId='-1'";
            }
            else
                this.dsAirQuoteDet.FilterExpression = "1=0";
        }
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["no"] != null)
        {
            string repId = Request.QueryString["no"].ToString();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string docId = list[i].docId;

                    string sql = string.Format("SELECT ChgCode,ChgDes,ExRate, Currency,Qty, Price, Unit, MinAmt, Rmk,GstType,Gst,Amt FROM AirQuoteDet1 where SequenceId='{0}'", docId);
                    DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                    if (tab.Rows.Count == 1)
                    {
                        string chgCode = tab.Rows[0]["ChgCode"].ToString();
                        string chgDes = tab.Rows[0]["ChgDes"].ToString();
                        decimal qty = SafeValue.SafeDecimal(tab.Rows[0]["Qty"], 1);
                        decimal price = SafeValue.SafeDecimal(tab.Rows[0]["Price"], 0);
                        decimal minAmt = SafeValue.SafeDecimal(tab.Rows[0]["MinAmt"], 0);
                        string currency = tab.Rows[0]["Currency"].ToString();
                        decimal exRate = SafeValue.SafeDecimal(tab.Rows[0]["ExRate"], 1);
                        string unit = tab.Rows[0]["Unit"].ToString();
                        string rmk = tab.Rows[0]["Rmk"].ToString();
                        decimal amt = SafeValue.SafeDecimal(tab.Rows[0]["Amt"], 0);
                        decimal gst = SafeValue.SafeDecimal(tab.Rows[0]["Gst"], 0);
                        string gstType = tab.Rows[0]["GstType"].ToString();

                        C2.AirQuoteDet1 repDet = new AirQuoteDet1();
                        repDet.ChgCode = chgCode;
                        repDet.ChgDes = chgDes;
                        repDet.Currency = currency;
                        repDet.ExRate = exRate;
                        repDet.FclLclInd = "Lcl";
                        repDet.MinAmt = minAmt;
                        repDet.Price = price;
                        repDet.QuoteId = repId;
                        repDet.Qty = qty;
                        repDet.GstType = gstType;
                        repDet.Gst = gst;
                        repDet.Amt = amt;
                        string sql_detCnt = "select max(QuoteLineNo) from AirQuoteDet1 where QuoteId='" + repId + "'";
                        int lineNo = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
                        repDet.QuoteLineNo = lineNo;
                        repDet.Rmk = rmk;
                        repDet.Unit = unit;
                        repDet.GroupTitle = "";
                        C2.Manager.ORManager.StartTracking(repDet, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(repDet);

                    }

                }
                e.Result = "";
            }
            catch { }

        }
        else
        {
            e.Result = "Please keyin select party ";
        }
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string docId = "";
        public Record(string _docId)
        {
            docId = _docId;
        }

    }
    public decimal totPayAmt = 0;
    private void OnLoad()
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 10000;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox docId = this.ASPxGridView1.FindRowTemplateControl(i, "txt_docId") as ASPxTextBox;
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (docId != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(docId.Text
                    ));
            }
        }
    }
}

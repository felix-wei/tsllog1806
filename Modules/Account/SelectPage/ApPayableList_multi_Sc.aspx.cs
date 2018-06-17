using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web;
using C2;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;

public partial class PagesAccount_SelectPage_ApPayableList_multi_Sc : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
               string partyTo = "";
                string repNo ="10001";
        if (!IsPostBack)
        {
            if (Request.QueryString["partyTo"] != null && Request.QueryString["no"] != null)
            {
                partyTo = Request.QueryString["partyTo"].ToString();
                repNo = Request.QueryString["no"].ToString();
                this.dsApPayable.FilterExpression = "BalanceAmt!=0 and ExportInd='Y' and DocType='SC' and PartyTo='" + partyTo + "'";
            }
            else
                this.dsApPayable.FilterExpression = "1=1";
        }
        OnLoad(repNo);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
    }
    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        int index = SafeValue.SafeInt(s, 0);
        ASPxSpinEdit balanceAmt = this.ASPxGridView1.FindRowTemplateControl(index, "spin_BalanceAmt") as ASPxSpinEdit;
        ASPxSpinEdit amt = this.ASPxGridView1.FindRowTemplateControl(index, "spin_Amt") as ASPxSpinEdit;
        ASPxCheckBox isPay = this.ASPxGridView1.FindRowTemplateControl(index, "ack_IsPay") as ASPxCheckBox;
        if (isPay.Checked)
            amt.Value = balanceAmt.Value;
        else
            amt.Value = 0;
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["partyTo"] != null && Request.QueryString["no"] != null)

        {
            string payId = Request.QueryString["no"].ToString();
            //decimal locAmt = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar("select locAmt from XAApPayment where SequenceId='" + payId + "'"), 0);
            //decimal locAmt_det = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar("select sum(locAmt) from XAApPaymentDet where PayId='" + payId + "'"), 0);
            //locAmt = locAmt - locAmt_det;
            if (true)//locAmt == totPayAmt)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    try
                    {
                        int docId = list[i].docId;
                        decimal payAmt = list[i].payAmt;

                        string sql = string.Format("SELECT SequenceId,AcCode, AcSource, DocType,DocNo,DocDate,SupplierBillNo,SupplierBillDate,PartyTo, CurrencyId, ExRate,DocAmt,LocAmt,BalanceAmt FROM XAApPayable where SequenceId='{0}'", docId);
                        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                        if (tab.Rows.Count == 1)
                        {
                            string oid = tab.Rows[0]["SequenceId"].ToString();
                            string docNo = tab.Rows[0]["DocNo"].ToString();
                            string docType = tab.Rows[0]["DocType"].ToString();
                            string acCode = tab.Rows[0]["AcCode"].ToString();
                            string acSource = tab.Rows[0]["AcSource"].ToString();
                            string currency = tab.Rows[0]["CurrencyId"].ToString();
                            decimal exRate = SafeValue.SafeDecimal(tab.Rows[0]["ExRate"], 0);
                            DateTime docDate = SafeValue.SafeDate(tab.Rows[0]["DocDate"], DateTime.Now);
                            string partyTo = tab.Rows[0]["PartyTo"].ToString();
                            string supplierBillNo = tab.Rows[0]["SupplierBillNo"].ToString();
                            decimal billDocAmt=SafeValue.SafeDecimal(tab.Rows[0]["DocAmt"],0);
                            decimal billBalaceAmt=SafeValue.SafeDecimal(tab.Rows[0]["BalanceAmt"],0);
                            decimal billLocAmt=SafeValue.SafeDecimal(tab.Rows[0]["LocAmt"],0);
                            DateTime supplierBillDate = SafeValue.SafeDate(tab.Rows[0]["SupplierBillDate"], DateTime.Now);
                            C2.XAApPaymentDet repDet = new XAApPaymentDet();
                            repDet.AcCode = acCode;
                            repDet.AcSource = "CR";
                            repDet.Currency = currency;
                            repDet.DocAmt = payAmt;
                            repDet.DocDate = docDate;
                            repDet.DocId = docId;
                            repDet.DocNo = docNo;
                            repDet.DocType = docType;
                            repDet.ExRate = exRate;
                            if (exRate == 1)
                            {
                                repDet.LocAmt = payAmt * exRate;
                            }
                            else
                            {
                                if (payAmt == billDocAmt)// full payment
                                    repDet.LocAmt = billLocAmt;
                                else if (payAmt == billBalaceAmt)//pay all outstanding amt
                                {
                                    repDet.LocAmt = billLocAmt - PayLocAmt(oid);
                                }
                                else
                                    repDet.LocAmt = payAmt * exRate;//partal payment
                            }
                            repDet.PayLineNo = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar("select count(*) from XAApPaymentDet where PayId='" + payId + "'"), 0) + 1;
                            repDet.PayId = SafeValue.SafeInt(payId,0);
                            repDet.PayNo = SafeValue.SafeString(Manager.ORManager.ExecuteScalar("select DocNo from xaappayable where sequenceid=" + payId), "");
                            repDet.PayType = "SR";
                            repDet.Remark1 = "Payment for SC-" + supplierBillNo;
                            repDet.Remark2 = "Payment made for AcCode -" + acCode;
                            repDet.Remark3 = " ";
                            C2.Manager.ORManager.StartTracking(repDet, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(repDet);

                            //update to doc

                            int res = UpdateBalance(docId,docType);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                e.Result = "The amount must be match with the receipt amount!";
            }
        }
    }
    private decimal PayLocAmt(string siId)
    {
       decimal payLocAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.LocAmt)
FROM  XAApPaymentDet AS det INNER JOIN XAApPayment AS mast ON det.PayId  = mast.SequenceId
WHERE (det.DocId = '{0}')and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", siId)),0);
       payLocAmt += SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.LocAmt)
FROM  XAArReceiptDet AS det INNER JOIN XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}')and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", siId)),0);
        return payLocAmt;
    }

    private int UpdateBalance( int docId,string docType)
    {
        //update invoice balacnce amt

        string sql = "select SUM(docAmt) from XAApPaymentDet where DocId='" + docId + "' and DocType='" + docType + "'";
        decimal locAmt = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        sql = string.Format("Update XAApPayable set BalanceAmt=DocAmt-'{0}' where SequenceId='{1}'", locAmt, docId);
        return C2.Manager.ORManager.ExecuteCommand(sql);
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int docId = 0;
        public decimal payAmt = 0;
        public bool isPay = false;
        public string docType = "";
        public Record(int _docId, decimal _payAmt)
        {
            docId = _docId;
            payAmt = _payAmt;
        }

    }
    public decimal totPayAmt = 0;
    private void OnLoad(string r)
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 10000;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox docId = this.ASPxGridView1.FindRowTemplateControl(i, "txt_docId") as ASPxTextBox;
            ASPxSpinEdit payAmt = this.ASPxGridView1.FindRowTemplateControl(i, "spin_Amt") as ASPxSpinEdit;
            ASPxSpinEdit balanceAmt = this.ASPxGridView1.FindRowTemplateControl(i, "spin_BalanceAmt") as ASPxSpinEdit;
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (docId != null && payAmt != null && isPay != null && isPay.Checked)
            {
                if (SafeValue.SafeDecimal(payAmt.Value, 0) > 0 && SafeValue.SafeDecimal(payAmt.Value, 0) <= SafeValue.SafeDecimal(balanceAmt.Value, 0))
                {
                    totPayAmt += SafeValue.SafeDecimal(payAmt.Value, 0);
                    list.Add(new Record(SafeValue.SafeInt(docId.Text, 0),
                        SafeValue.SafeDecimal(payAmt.Value, 0)
                        ));
                }
            }
        }
    }
}

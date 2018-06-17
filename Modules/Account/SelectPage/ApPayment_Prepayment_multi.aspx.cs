using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class PagesAccount_SelectPage_ApPayment_Prepayment_multi : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string partyTo = "";
        string repNo = "";
		string prepayment =  System.Configuration.ConfigurationManager.AppSettings["Prepayment"] ?? "-";
        if (!IsPostBack)
        {
            this.txt_FromDt.Date = new DateTime(2010, 1, 1);
            this.txt_toDt.Date = DateTime.Today;
            if (Request.QueryString["partyTo"] != null && Request.QueryString["no"] != null)
            {
                partyTo = Request.QueryString["partyTo"].ToString();
                repNo = Request.QueryString["no"].ToString();
                string excetpPl = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptPl"], "");
                //this.dsApPayable.FilterExpression = "BalanceAmt!=0 and ExportInd='Y' and (DocType='PL' OR DocType='SD' OR DocType='SC') and PartyTo='" + partyTo + "'";
                //if (excetpPl.Length > 0)
                //    this.dsApPayable.FilterExpression += " and DocNo Not In('" + excetpPl.Replace(",", "','") + "0')";

                 string sql = string.Format(@" 		
select rl.SequenceId, rd.DocType, rd.DocNo, (select top 1 name from xxparty where partyid=rd.partyto) as PartyName, rd.DocDate,
'' as MastType, rd.DocNo as SupplierBillNo, rd.DocDate as SupplierBillDate, rd.CurrencyId, rd.ExRate, rl.DocAmt, rl.LocAmt, 
(rl.LocAmt - (select IsNull(sum(rl0.LocAmt),0) from xaappaymentdet rl0, xaappayment rd0 where rd0.sequenceid=rl0.payid 
and rl0.acsource='CR' and  rl0.accode='{1}' and rl0.docid=rl.sequenceid)) as BalanceAmt
from xaappaymentdet rl, xaappayment rd where rd.sequenceid=rl.payid and rl.acsource='DB' and rd.ExportInd='Y' and rl.accode='{1}' 
and rd.partyto='{0}' and (rl.LocAmt - (select IsNull(sum(rl0.LocAmt),0) from xaappaymentdet rl0, xaappayment rd0  where rd0.sequenceid=rl0.payid and rl0.acsource='CR' and rl0.accode='{1}' and rl0.docid=rl.sequenceid))<>0
", partyTo, prepayment);
//throw new Exception(sql);
                DataTable tab1 = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                this.ASPxGridView1.DataSource = tab1;
                this.ASPxGridView1.DataBind();
            }
            else
                this.dsApPayable.FilterExpression = "1=1";
        }
        OnLoad(repNo);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string billNo = this.txt_No.Text;
string prepayment =  System.Configuration.ConfigurationManager.AppSettings["Prepayment"] ?? "-";
        string partyTo = Request.QueryString["partyTo"].ToString();
        string sql = string.Format(@"SELECT TOP (100) SequenceId, DocType, DocNo, DocDate,MastType,SupplierBillNo,SupplierBillDate, CurrencyId, ExRate, DocAmt, LocAmt, BalanceAmt
FROM XAApPayable WHERE (ExportInd = 'Y') AND (BalanceAmt <> 0) and (DocType='PL' OR DocType='SD' OR DocType='SC') and PartyTo='{0}'", partyTo);
                 sql = string.Format(@" 		
select rl.SequenceId, rd.DocType, rd.DocNo, (select top 1 name from xxparty where partyid=rd.partyto) as PartyName, rd.DocDate,'' as MastType, rd.DocNo as SupplierBillNo, rd.DocDate as SupplierBillDate,rd.CurrencyId, rd.ExRate, rl.DocAmt, rl.LocAmt, 
(rl.LocAmt - (select IsNull(sum(rl0.LocAmt),0) from xaarreceiptdet rl0, xaarreceipt rd0 where rd0.sequenceid=rl0.repid and rl0.acsource='CR' and rd0.ExportInd='Y' and rl0.accode='{1}' and rl0.docid=rl.sequenceid)) as BalanceAmt
from xaappaymentdet rl, xaappayment rd where rd.sequenceid=rl.payid and rl.acsource='CR' and rd.ExportInd='Y' and rl.accode='{1}' and rd.partyto='{0}' and (rl.LocAmt - (select IsNull(sum(rl0.LocAmt),0) from xaappaymentdet rl0, xaappayment rd0  where rd0.sequenceid=rl0.payid and rl0.acsource='CR' and rl0.accode='{1}' and rl0.docid=rl.sequenceid))<>0
", partyTo, prepayment);


        if (billNo.Length > 0)
        {
            sql += string.Format(" and SupplierBillNo like '{0}%'",billNo);
        }
        else if (this.txt_FromDt.Value != null && this.txt_toDt.Value != null)
        {
            sql += string.Format(" and DocDate > ='{0}' and DocDate<'{1}'", this.txt_FromDt.Date,this.txt_toDt.Date.AddDays(1));
        }
        DataTable tab1 = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab1;
        this.ASPxGridView1.DataBind();

    }
    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //string s = e.Parameters;
        //int index = SafeValue.SafeInt(s, 0);
        //ASPxSpinEdit balanceAmt = this.ASPxGridView1.FindRowTemplateControl(index, "spin_BalanceAmt") as ASPxSpinEdit;
        //ASPxSpinEdit amt = this.ASPxGridView1.FindRowTemplateControl(index, "spin_Amt") as ASPxSpinEdit;
        //ASPxCheckBox isPay = this.ASPxGridView1.FindRowTemplateControl(index, "ack_IsPay") as ASPxCheckBox;
        //if (isPay.Checked)
        //    amt.Value = balanceAmt.Value;
        //else
        //    amt.Value = 0;
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["partyTo"] != null && Request.QueryString["no"] != null)
        {
            string payId = Request.QueryString["no"].ToString();
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    int docId = list[i].docId;
                    decimal payAmt = list[i].payAmt;

                    string  sql = string.Format("SELECT rl.SequenceId,rl.DocAmt,rl.LocAmt, (rl.LocAmt - (select IsNull(sum(rl0.LocAmt),0) from xaappaymentdet rl0, xaappayment rd0 where rd0.sequenceid=rl0.payid and rl0.acsource='CR' and rl0.accode='2090' and rl0.docid=rl.sequenceid)) as BalanceAmt,rl.AcCode, rl.AcSource, rd.DocType,rd.DocNo,rd.DocDate,rd.PartyTo,rd.CurrencyId as CurrencyId, rd.ExRate as ExRate FROM XAappaymentdet rl, xaappayment rd where rl.payid=rd.SequenceId and rl.SequenceId='{0}'", docId);
                    DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                    if (tab.Rows.Count == 1)
                    {
                        string docNo = tab.Rows[0]["DocNo"].ToString();
                        string docType = tab.Rows[0]["DocType"].ToString();
                        string acCode = tab.Rows[0]["AcCode"].ToString();

                        string acSource = tab.Rows[0]["AcSource"].ToString();
                        string currency = tab.Rows[0]["CurrencyId"].ToString();
                        decimal exRate = SafeValue.SafeDecimal(tab.Rows[0]["ExRate"], 0);

                        DateTime docDate = SafeValue.SafeDate(tab.Rows[0]["DocDate"], DateTime.Now);
                        string partyTo = tab.Rows[0]["PartyTo"].ToString();
                        string supplierBillNo = tab.Rows[0]["DocNo"].ToString();
                        DateTime supplierBillDate = SafeValue.SafeDate(tab.Rows[0]["DocDate"], DateTime.Now);

                        string oid = tab.Rows[0]["SequenceId"].ToString();
                        decimal billDocAmt = SafeValue.SafeDecimal(tab.Rows[0]["DocAmt"], 0);
                        decimal billBalaceAmt = SafeValue.SafeDecimal(tab.Rows[0]["BalanceAmt"], 0);
                        decimal billLocAmt = SafeValue.SafeDecimal(tab.Rows[0]["LocAmt"], 0);
                        C2.XAApPaymentDet repDet = new XAApPaymentDet();
                        repDet.AcCode = acCode;
                        if (acSource == "CR")
                            repDet.AcSource = "DB";
                        else
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
                            repDet.LocAmt = SafeValue.ChinaRound(payAmt * exRate, 2);
                        }
                        else
                        {
							repDet.LocAmt = SafeValue.ChinaRound(payAmt * exRate, 2);//partal payment
                        }
						//repDet.LineDocAmt = repDet.LocAmt;

                        repDet.PayLineNo = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar("select count(*) from XAApPaymentDet where PayId='" + payId + "'"), 0) + 1;
                        repDet.PayId = SafeValue.SafeInt(payId, 0);
                        repDet.PayNo = D.Text("select DocNo from XaAppAyment where SequenceId=" + payId.ToString());
                        repDet.PayType = D.Text("select DocType from XaAppAyment where SequenceId=" + payId.ToString());
                        repDet.Remark1 = "PrePayment for " + supplierBillNo;
                        repDet.Remark2 = "Payment made for AcCode -" + acCode;
                        repDet.Remark3 = " ";
                        C2.Manager.ORManager.StartTracking(repDet, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(repDet);

                        //update to doc

                        int res = UpdateBalance(docId, docType);
                    }
                }
                catch(Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
            }
        }
        else
        {
            e.Result = "The amount must be match with the receipt amount!";
        }
    }
    private decimal PayLocAmt(string siId)
    {
        decimal payLocAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.LocAmt)
FROM  XAApPaymentDet AS det INNER JOIN XAApPayment AS mast ON det.PayId  = mast.SequenceId
WHERE (det.DocId = '{0}')and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", siId)), 0);
        payLocAmt += SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.LocAmt)
FROM  XAArReceiptDet AS det INNER JOIN XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}')and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", siId)), 0);
        return payLocAmt;
    }
    private int UpdateBalance(int docId, string docType)
    {
        //update invoice balacnce amt
		return 0;
        string sql = "select SUM(docAmt) from XAArReceiptDet where DocId='" + docId + "' and DocType='" + docType + "'";
        decimal locAmt = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
       
         sql = "select SUM(docAmt) from XAApPaymentDet where DocId='" + docId + "' and DocType='" + docType + "'";
        locAmt += SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
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

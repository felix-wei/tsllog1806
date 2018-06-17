using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web;
using DevExpress.Web.ASPxEditors;

public partial class PagesAccount_SelectPage_ArReceipt_Prepayment_multi : BasePage
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
                //this.dsArInvoice.FilterExpression = "ExportInd='Y' and BalanceAmt!=0 and PartyTo='" + partyTo + "'";

                string sql = string.Format(@" 		
select rl.SequenceId, rd.DocType, rd.DocNo, (select top 1 name from xxparty where partyid=rd.partyto) as PartyName, rd.DocDate,
'' as MastType, rd.DocNo as SupplierBillNo, rd.DocDate as SupplierBillDate, rd.DocCurrency as CurrencyId, rd.DocExRate as ExRate, rl.DocAmt, rl.LocAmt, 
(rl.LocAmt - (select IsNull(sum(rl0.LocAmt),0) from xaarreceiptdet rl0, xaarreceipt rd0 where rd0.sequenceid=rl0.repid 
and rl0.acsource='DB' and  rl0.accode='{1}' and rl0.docid=rl.sequenceid)) as BalanceAmt
from xaarreceiptdet rl, xaarreceipt rd where rd.sequenceid=rl.repid and rl.acsource='CR' and rd.ExportInd='Y' and rl.accode='{1}' 
and rd.partyto='{0}' and (rl.LocAmt - (select IsNull(sum(rl0.LocAmt),0) from xaarreceiptdet rl0, xaarreceipt rd0  where rd0.sequenceid=rl0.repid and rl0.acsource='DB' and rl0.accode='{1}' and rl0.docid=rl.sequenceid))<>0
", partyTo, prepayment);
                DataTable tab1 = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                this.ASPxGridView1.DataSource = tab1;
                this.ASPxGridView1.DataBind();
            }
        }
        OnLoad(repNo);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string billNo = this.txt_No.Text;
					string prepayment =  System.Configuration.ConfigurationManager.AppSettings["Prepayment"] ?? "-";

        string partyTo = Request.QueryString["partyTo"].ToString();
        string sql = string.Format(@"SELECT TOP (120) SequenceId, DocType, DocNo, (select top 1 name from xxparty where partyid=partyto) as PartyName, DocDate,MastType, CurrencyId, ExRate, DocAmt, LocAmt, BalanceAmt
FROM XAArInvoice WHERE (ExportInd = 'Y') AND (BalanceAmt <> 0) ", partyTo);

               sql = string.Format(@" 		
select rl.SequenceId, rd.DocType, rd.DocNo, (select top 1 name from xxparty where partyid=rd.partyto) as PartyName, rd.DocDate,'' as MastType, rd.DocCurrency, rd.DocExRate, rl.DocAmt, rl.LocAmt, 
(rl.LocAmt - (select IsNull(sum(rl0.LocAmt),0) from xaarreceiptdet rl0, xaarreceipt rd0 where rd0.sequenceid=rl0.repid and rl0.acsource='CR' and rd0.ExportInd='Y' and rl0.accode='{1}' and rl0.docid=rl.sequenceid)) as BalanceAmt
from xaarreceiptdet rl, xaarreceipt rd where rd.sequenceid=rl.repid and rl.acsource='CR' and rd.ExportInd='Y' and rl.accode='1-2300' and rd.partyto='{0}' and (rl.LocAmt - (select IsNull(sum(rl0.LocAmt),0) from xaarreceiptdet rl0, xaarreceipt rd0 where rd0.sequenceid=rl0.repid and rl0.acsource='DB' and rl0.accode='1-2300' and rl0.docid=rl.sequenceid))<>0
", partyTo, prepayment);


        if (billNo.Length > 0)
        {
            sql += string.Format(" and DocNo like '{0}%'", billNo);
        }
        else if (this.txt_FromDt.Value != null && this.txt_toDt.Value != null)
        {
            sql += string.Format(" and DocDate >= '{0}' and DocDate<'{1}'", this.txt_FromDt.Date, this.txt_toDt.Date.AddDays(1));
        }
        DataTable tab1 = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab1;
        this.ASPxGridView1.DataBind();

    }

    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
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

        //throw new Exception("123");
        if (Request.QueryString["partyTo"] != null && Request.QueryString["no"] != null)
        {
            int repId = SafeValue.SafeInt(Request.QueryString["no"], 0);
            //decimal locAmt = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar("select locAmt from XAArReceipt where SequenceId='" + repId + "'"), 0);
            //decimal locAmt_det = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar("select sum(locAmt) from XAArReceiptDet where RepId='" + repId + "'"), 0);
            //locAmt = locAmt - locAmt_det;
            if (true)
            {
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string docId = list[i].docId;
                        decimal payAmt = list[i].payAmt;
						string docT = list[i].docType;

                        string sql = string.Format("SELECT SequenceId,DocAmt,LocAmt,BalanceAmt,AcCode, AcSource, DocType,DocNo,DocDate,PartyTo,CurrencyId, ExRate FROM XAArInvoice where SequenceId='{0}'", docId);
						sql = string.Format("SELECT rl.SequenceId,rl.DocAmt,rl.LocAmt, (rl.LocAmt - (select IsNull(sum(rl0.LocAmt),0) from xaArReceiptdet rl0, xaArReceipt rd0 where rd0.sequenceid=rl0.repid and rl0.acsource='DB' and rl0.accode='2090' and rl0.docid=rl.sequenceid)) as BalanceAmt,rl.AcCode, rl.AcSource, rd.DocType,rd.DocNo,rd.DocDate,rd.PartyTo,rd.DocCurrency as CurrencyId, rd.DocExRate as ExRate FROM XAArReceiptdet rl, xaArReceipt rd where rl.repid=rd.SequenceId and rl.SequenceId='{0}'", docId);
						if(docT == "RE")
						{
                        sql = string.Format("SELECT rl.SequenceId,rl.DocAmt,rl.LocAmt, (rl.LocAmt - (select IsNull(sum(rl0.LocAmt),0) from xaarreceiptdet rl0, xaarreceipt rd0 where rd0.sequenceid=rl0.repid and rl0.acsource='CR' and rl0.accode='1-2300' and rl0.docid=rl.sequenceid)) as BalanceAmt,rl.AcCode, rl.AcSource, rd.DocType,rd.DocNo,rd.DocDate,rd.PartyTo,rd.DocCurrency as CurrencyId, rd.DocExRate as ExRate FROM XAarreceiptdet rl, xaarreceipt rd where rl.repid=rd.SequenceId and rl.SequenceId='{0}'", docId);
						}
                        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                        if (tab.Rows.Count == 1)
                        {
                            string docNo = tab.Rows[0]["DocNo"].ToString();
                            string docType = tab.Rows[0]["DocType"].ToString();
                            string acCode = tab.Rows[0]["AcCode"].ToString();
                            string acSource = tab.Rows[0]["AcSource"].ToString();
                            string currency = tab.Rows[0]["CurrencyId"].ToString();
                            decimal exRate = SafeValue.SafeDecimal(tab.Rows[0]["ExRate"], 1);
                            if (exRate == 0)
                                exRate = 1;
                            DateTime docDate = SafeValue.SafeDate(tab.Rows[0]["DocDate"], DateTime.Now);

                            string oid = tab.Rows[0]["SequenceId"].ToString();
                            decimal billDocAmt = SafeValue.SafeDecimal(tab.Rows[0]["DocAmt"], 0);
                            decimal billBalaceAmt = SafeValue.SafeDecimal(tab.Rows[0]["BalanceAmt"], 0);
                            decimal billLocAmt = SafeValue.SafeDecimal(tab.Rows[0]["LocAmt"], 0);

                            C2.XAArReceiptDet repDet = new XAArReceiptDet();
                            repDet.AcCode = acCode;
                            if (acSource == "CR")
                                repDet.AcSource = "DB";
                            else {
                                repDet.AcSource = "CR";
								if(docType == "RE")
									repDet.AcSource = "DB";
							}
                            repDet.Currency = currency;
                            repDet.DocAmt = payAmt;// payAmt;
                            repDet.DocDate = docDate;
                            repDet.DocId = SafeValue.SafeInt(docId, 0);
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

                            repDet.RepLineNo = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar("select count(*) from XAArReceiptDet where RepId='" + repId + "'"), 0) + 1;
                            repDet.RepId = repId;

                            repDet.Remark1 = "Receipt for " + docType + "-" + docNo;
                            repDet.Remark2 = "Receipt made for party - " + acCode;
                            repDet.Remark3 = " ";
							if(docType == "RE")
							{
                            repDet.Remark1 = "Prepayment from " + docType + "-" + docNo;
                            repDet.Remark2 = "Prepayment from party - " + acCode;
							}
                            C2.Manager.ORManager.StartTracking(repDet, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(repDet);

                            //update to doc

                            int res = UpdateBalance(docId,docType);
                        }

                    }
                    e.Result = "";
                }
                catch (Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
            }
            else
                e.Result = "The total amount must be match with the receipt amount!";
        }
        else
        {
            e.Result = "Please keyin select party ";
        }
    }
    private decimal PayLocAmt_cn(string siId)
    {
        decimal payLocAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.LocAmt) 
FROM         XAArReceiptDet AS det INNER JOIN  XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE     (det.DocId = '{0}') and (det.DocType='CN')", siId)), 0);
        payLocAmt += SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.LocAmt)
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='CN')", siId)), 0);
        return payLocAmt;
    }
    private decimal PayLocAmt(string siId)
    {
        decimal payLocAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.LocAmt)
FROM  XAArReceiptDet AS det INNER JOIN XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", siId)), 0);
        payLocAmt += SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.LocAmt)
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", siId)), 0);
        return payLocAmt;
    }
    private int UpdateBalance(string docId, string docType)
    {
        //update invoice balacnce amt

        string sql = "select SUM(docAmt) from XAArReceiptDet where DocId='" + docId + "' and DocType='" + docType + "'";
        decimal locAmt = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);

        sql = "select SUM(docAmt) from XAApPaymentDet where DocId='" + docId + "' and DocType='" + docType + "'";
        locAmt += SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        sql = string.Format("Update XAArInvoice set BalanceAmt=DocAmt-'{0}' where SequenceId='{1}'", locAmt, docId);
        return C2.Manager.ORManager.ExecuteCommand(sql);
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string docId = "";
        public string docType = "";
        public decimal payAmt = 0;
        public bool isPay = false;
        public Record(string _docId, decimal _payAmt, string _docType)
        {
            docId = _docId;
            payAmt = _payAmt;
			docType = _docType;
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
            ASPxTextBox docType = this.ASPxGridView1.FindRowTemplateControl(i, "txt_docType") as ASPxTextBox;
            ASPxSpinEdit payAmt = this.ASPxGridView1.FindRowTemplateControl(i, "spin_Amt") as ASPxSpinEdit;
            ASPxSpinEdit balanceAmt = this.ASPxGridView1.FindRowTemplateControl(i, "spin_BalanceAmt") as ASPxSpinEdit;
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (docId != null && payAmt != null && isPay != null && isPay.Checked)
            {
                if (SafeValue.SafeDecimal(payAmt.Value, 0) > 0 && SafeValue.SafeDecimal(payAmt.Value, 0) <= SafeValue.SafeDecimal(balanceAmt.Value, 0))
                {
                    totPayAmt += SafeValue.SafeDecimal(payAmt.Value, 0);
                    list.Add(new Record(docId.Text,
                        SafeValue.SafeDecimal(payAmt.Value, 0),
						docType.Text
                        ));
                }
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxEditors;

public partial class PagesAccount_SelectPage_ArInvoiceList_Cn_multi : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string partyTo = "";
        string repNo = "";
        if (!IsPostBack)
        {
            if (Request.QueryString["partyTo"] != null && Request.QueryString["no"] != null)
            {
                partyTo = Request.QueryString["partyTo"].ToString();
                repNo = Request.QueryString["no"].ToString();
                this.dsArInvoice.FilterExpression = "DocType='CN' and ExportInd='Y' and BalanceAmt!=0 and PartyTo='" + partyTo + "'";
            }
            else
                this.dsArInvoice.FilterExpression = "1=1";
        }
        OnLoad(repNo);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
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
        if (Request.QueryString["partyTo"] != null && Request.QueryString["no"] != null)
        {
            int repId = SafeValue.SafeInt(Request.QueryString["no"], 0);
            if (true)//locAmt == totPayAmt)
            {
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string docId = list[i].docId;
                        decimal payAmt = list[i].payAmt;

                        string sql = string.Format("SELECT SequenceId,DocAmt,LocAmt,BalanceAmt,AcCode, AcSource, DocType,DocNo,DocDate,PartyTo,CurrencyId, ExRate FROM XAArInvoice where SequenceId='{0}'", docId);
                        DataTable tab = Helper.Sql.List(sql);
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
                            repDet.AcSource = "DB";
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
                                if (payAmt == billDocAmt)// full payment
                                    repDet.LocAmt = billLocAmt;
                                else if (payAmt == billBalaceAmt)//pay all outstanding amt
                                {
                                        repDet.LocAmt = billLocAmt - PayLocAmt_cn(oid);
                                }
                                else
                                    repDet.LocAmt = SafeValue.ChinaRound(payAmt * exRate, 2);//partal payment
                            }

                            repDet.RepLineNo = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar("select count(*) from XAArReceiptDet where RepId='" + repId + "'"), 0) + 1;
                            repDet.RepId = repId;
                            repDet.RepNo = SafeValue.SafeString(Manager.ORManager.ExecuteScalar("select DocNo from xaarreceipt where sequenceid=" + repId),"") ;
                            repDet.RepType = "PC";
                            repDet.Remark1 = "Pay for " + docType + "-" + docNo;
                            repDet.Remark2 = "Pay made for party -" + acCode;
                            repDet.Remark3 = " ";
                            C2.Manager.ORManager.StartTracking(repDet, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(repDet);

                            //update to doc

                            int res = UpdateBalance(docId,docType);
                        }

                    }
                    e.Result = "";
                }
                catch { }
            }
            else
                e.Result = "The total amount must be match with the Cheque amount!";
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
    private int UpdateBalance( string invId,string docType)
    {
        //update invoice balacnce amt

        string sql = "select SUM(docAmt) from XAArReceiptDet where DocId='" + invId + "' and DocType='" + docType + "'";
        decimal locAmt = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        sql = string.Format("Update XAArInvoice set BalanceAmt=DocAmt-'{0}' where SequenceId='{1}'", locAmt, invId);
        return C2.Manager.ORManager.ExecuteCommand(sql);
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string docId = "";
        public decimal payAmt = 0;
        public bool isPay = false;
        public Record(string _docId, decimal _payAmt)
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
                    list.Add(new Record(docId.Text,
                        SafeValue.SafeDecimal(payAmt.Value, 0)
                        ));
                }
            }
        }
    }
}

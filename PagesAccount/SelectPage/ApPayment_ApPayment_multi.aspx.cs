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

public partial class PagesAccount_SelectPage_ApPayment_ApPayment_multi : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string partyTo = "";
        string repNo = "";
        if (!IsPostBack)
        {
            this.txt_FromDt.Date = new DateTime(2018, 5, 1);
            this.txt_toDt.Date = DateTime.Today;
            if (Request.QueryString["partyTo"] != null && Request.QueryString["no"] != null)
            {
                partyTo = "CASH"; //Request.QueryString["partyTo"].ToString();
                repNo = Request.QueryString["no"].ToString();
                string excetpPl = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptPl"], "");
                //this.dsApPayable.FilterExpression = "BalanceAmt!=0 and ExportInd='Y' and (DocType='PL' OR DocType='SD' OR DocType='SC') and PartyTo='" + partyTo + "'";
                //if (excetpPl.Length > 0)
                //    this.dsApPayable.FilterExpression += " and DocNo Not In('" + excetpPl.Replace(",", "','") + "0')";
				GetData();
//              string sql = string.Format(@"SELECT TOP (60) 
//		SequenceId, DocType, DocNo, DocDate, '' as MastType,DocNo as SupplierBillNo,DocDate as SupplierBillDate,  Remark,CurrencyId, ExRate, DocAmt, LocAmt, 0 as BalanceAmt
//FROM XAApPayment WHERE (ExportInd = 'Y') AND docno like 'TSL-PC-%' and  (DocType='PS') and PartyTo='{0}' and DocNo not in (select MastRefNo from xaappaymentdet where MastType='PC')", partyTo);


  //              DataTable tab1 = D.List(sql);
    //            this.ASPxGridView1.DataSource = tab1;
      //          this.ASPxGridView1.DataBind();
            }
            else
                this.dsApPayable.FilterExpression = "1=1";
        }
        OnLoad(repNo);
    }
	
	public void GetData()
	{
	        string billNo = this.txt_No.Text;

        string partyTo = "CASH"; // Request.QueryString["partyTo"].ToString();
        string sql = string.Format(@"SELECT TOP (60) 
		SequenceId, DocType, DocNo, DocDate, '' as MastType,DocNo as SupplierBillNo,DocDate as SupplierBillDate,Remark, CurrencyId, ExRate, DocAmt, LocAmt, 0 as BalanceAmt
FROM XAApPayment WHERE (ExportInd = 'Y') AND docno like 'TSL-PC-%' and  (DocType='PS') and PartyTo='{0}' and DocNo not in (select MastRefNo from xaappaymentdet where MastType='PC')", partyTo);


        if (billNo.Length > 0)
        {
            sql += string.Format(" and DocNo like '{0}%'",billNo);
        }
        else if (this.txt_FromDt.Value != null && this.txt_toDt.Value != null)
        {
            sql += string.Format(" and DocDate > ='{0}' and DocDate<'{1}'", this.txt_FromDt.Date,this.txt_toDt.Date.AddDays(1));
        }
        DataTable tab1 = Helper.Sql.List(sql);
        this.ASPxGridView1.DataSource = tab1;
        this.ASPxGridView1.DataBind();
	}
	
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
		GetData();
    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
  
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["partyTo"] != null && Request.QueryString["no"] != null)
        {
            string payId = Request.QueryString["no"].ToString();
			//throw new Exception(list.Count.ToString());
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    int docId = list[i].docId;
                    decimal payAmt = list[i].payAmt;

                    string sql = string.Format("SELECT SequenceId,DocAmt,LocAmt, DocDate, AcCode, AcSource, DocType,DocNo, PartyTo, CurrencyId, ExRate FROM XAApPayment where SequenceId='{0}'", docId);
                    DataTable tab = Helper.Sql.List(sql);
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

                        string oid = tab.Rows[0]["SequenceId"].ToString();
                        decimal billDocAmt = SafeValue.SafeDecimal(tab.Rows[0]["DocAmt"], 0);
                        decimal billLocAmt = SafeValue.SafeDecimal(tab.Rows[0]["LocAmt"], 0);
                        C2.XAApPaymentDet repDet = new XAApPaymentDet();
                        repDet.AcCode = acCode;
                         
                        repDet.AcSource = "DB";
                        repDet.Currency = currency;
                        repDet.DocAmt = payAmt;
                        repDet.LocAmt = payAmt;
                        repDet.DocDate = docDate;
                        repDet.DocId = 0;
                        repDet.DocNo = docNo;
                        repDet.DocType = docType;
                        repDet.ExRate = exRate;
						repDet.MastType = "PC";
						repDet.MastRefNo= docNo;
                       

                        repDet.PayLineNo = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar("select count(*) from XAApPaymentDet where PayId='" + payId + "'"), 0) + 1;
                        repDet.PayId = SafeValue.SafeInt(payId, 0);
                        repDet.PayNo = "";
                        repDet.PayType = "";
                        repDet.Remark1 = "Payment for CLAIM-" + docNo;
                        repDet.Remark2 = "Payment made for AcCode -" + acCode;
                        repDet.Remark3 = " ";
                        C2.Manager.ORManager.StartTracking(repDet, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(repDet);

                        //update to doc

                        //int res = UpdateBalance(docId, docType);
                    }
                }
                catch(Exception ex) {
					throw new Exception(ex.Message + ex.StackTrace);
				}
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
        int end = 200;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox docId = this.ASPxGridView1.FindRowTemplateControl(i, "txt_docId") as ASPxTextBox;
            ASPxSpinEdit payAmt = this.ASPxGridView1.FindRowTemplateControl(i, "spin_Amt") as ASPxSpinEdit;
            //ASPxSpinEdit balanceAmt = this.ASPxGridView1.FindRowTemplateControl(i, "spin_BalanceAmt") as ASPxSpinEdit;
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (docId != null && payAmt != null && isPay != null && isPay.Checked)
            {
                //if (SafeValue.SafeDecimal(payAmt.Value, 0) > 0 && SafeValue.SafeDecimal(payAmt.Value, 0) <= SafeValue.SafeDecimal(balanceAmt.Value, 0))
                if (SafeValue.SafeDecimal(payAmt.Value, 0) != 0 )
                {
                    //totPayAmt += SafeValue.SafeDecimal(payAmt.Value, 0);
                    list.Add(new Record(SafeValue.SafeInt(docId.Text, 0),
                        SafeValue.SafeDecimal(payAmt.Value, 0)
                        ));
                }
            }
        }
    }
}

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

public partial class PagesFreight_Account_BillList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.form1.Focus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.cmb_ArAp.Text = "AR Invoice";
            BindData();
        }
        OnLoad();
    }
    private void BindData()
    {
        if (Request.QueryString["id"] != null && Request.QueryString["typ"] != null)
        {
            decimal qty = 0;
            int invId = SafeValue.SafeInt(Request.QueryString["id"], 0);
            string arAp = SafeValue.SafeString(Request.QueryString["typ"]).ToUpper();
            string sql = "";
            if (arAp == "AR")
                sql = string.Format("select MastRefNo,JobRefNo,MastType  from xaarinvoice where SequenceId='{0}'", invId);
            else if (arAp == "AP")
                sql = string.Format("select MastRefNo,JobRefNo,MastType  from XaApPayable where SequenceId='{0}'", invId);
            if (sql.Length > 0)
            {
                DataTable tab = ConnectSql.GetTab(sql);
                if (tab.Rows.Count == 1)
                {
                    string mastRefNo = SafeValue.SafeString(tab.Rows[0]["MastRefNo"]);
                    string jobRefNo = SafeValue.SafeString(tab.Rows[0]["JobRefNo"]);
                    string mastType = SafeValue.SafeString(tab.Rows[0]["MastType"]);
                    if (mastType.Length > 0 && mastRefNo.Length > 0)
                    {
                        if (mastType == "SI")
                        {
                            qty = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM SeaImport where RefNo='{0}' and JobNo='{1}'", mastRefNo, jobRefNo)));
                        }
                        else if (mastType == "SE")
                        {
                            qty = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM SeaExport where RefNo='{0}' and JobNo='{1}'", mastRefNo, jobRefNo)));
                        }
                        else if (mastType == "AI")
                        {
                            qty = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM air_job where RefNo='{0}' and JobNo='{1}'", mastRefNo, jobRefNo)));
                        }
                        else if (mastType == "AE" || mastType == "ACT")
                        {
                            qty = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM air_job where RefNo='{0}' and JobNo='{1}'", mastRefNo, jobRefNo)));
                        }
                        else if (mastType == "TPT")
                        {
                            qty = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(string.Format("Select round(case when wt/1000>m3 then wt/1000 else m3 end,3) FROM tpt_Job where JobNo='{0}'", mastRefNo)));
                        }
                            if (qty == 0)
                                qty = 1;
                            this.txt_Qty.Text = qty.ToString("0.000");

                    }
                }
            }

        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string invNo = this.txt_No.Text.Trim();
        string where = "";
        string sql = "";
        if (invNo.Length > 0)
        {
            if (this.cmb_ArAp.Text == "AR Invoice")
                sql = string.Format("select SequenceId,ChgCode,case when unit='Set' or unit='SHPT' then '1' else '{1}' end as Qty,Price,Currency,ExRate,GstType,Locamt from XaArInvoiceDet where DocNo='{0}'", invNo, this.txt_Qty.Text);
            else if (this.cmb_ArAp.Text == "AP Invoice")
                sql = string.Format("select SequenceId,ChgCode,case when unit='Set' or unit='SHPT' then '1' else '{1}' end as Qty,Price,Currency,ExRate,GstType,Locamt from XaApPayableDet where DocNo='{0}'", invNo, this.txt_Qty.Text);
            else if (this.cmb_ArAp.Text == "AR Quote")
                sql = string.Format("select SequenceId,ChgCode,case when unit='Set' or unit='SHPT' then '1' else '{1}' end as Qty,Price,Currency,ExRate,GstType,Amt as LocAmt from SeaQuoteDet1 where QuoteId=(select Sequenceid from SeaQuote1 where QuoteNo='{0}')", invNo, this.txt_Qty.Text);
            else if (this.cmb_ArAp.Text == "AP Quote")
                sql = string.Format("select SequenceId,ChgCode,case when unit='Set' or unit='SHPT' then '1' else '{1}' end as Qty,Price,Currency,ExRate,GstType,Amt as LocAmt from SeaApQuoteDet1 where QuoteId=(select Sequenceid from SeaApQuote1 where QuoteNo='{0}')", invNo, this.txt_Qty.Text);

            DataTable tab = ConnectSql.GetTab(sql + where);
            this.ASPxGridView1.DataSource = tab;
            this.ASPxGridView1.DataBind();

        }
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["no"] != null && Request.QueryString["typ"] != null)
        {
            string invNo = Request.QueryString["no"].ToString();
            string arApInd = Request.QueryString["typ"].ToString().ToUpper();
            int invId = SafeValue.SafeInt(Request.QueryString["id"], 0);
            string invId_old = e.Parameters;
            string refNo = "";
            string jobNo = "";
            string mastType = "";
            string docType = "";
            string arApInd_sch = this.cmb_ArAp.Text.ToUpper();

            if (arApInd == "AR")
            {
                string sql_mast = string.Format("select DocType,MastRefNo,JobRefNo,MastType from XAArInvoice where SequenceId='{0}'", invId);
                DataTable tab_mast = C2.Manager.ORManager.GetDataSet(sql_mast).Tables[0];
                if (tab_mast.Rows.Count == 1)
                {
                    docType = SafeValue.SafeString(tab_mast.Rows[0]["DocType"]);
                    refNo = SafeValue.SafeString(tab_mast.Rows[0]["MastRefNo"]);
                    jobNo = SafeValue.SafeString(tab_mast.Rows[0]["JobRefNo"]);
                    mastType = SafeValue.SafeString(tab_mast.Rows[0]["MastType"]);
                }

                int index = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(string.Format("Select max(DocLineNo) FROM XAArInvoiceDet where DocId='{0}'", invId)), 0);
                #region ar pulling data
                for (int m = 0; m < list.Count; m++)
                {
                    try
                    {
                        int sequenceId = list[m].docId;
                        decimal qty = list[m].qty;
                        decimal price = list[m].price;
                        if (qty * price == 0)
                            continue;
                        decimal exRateDes = list[m].exRate;
                        string sql = "";
                        if(arApInd_sch=="AR INVOICE")
                            sql = string.Format("select * from XaArInvoiceDet where SequenceId='{0}'", sequenceId);
                        if (arApInd_sch == "AP INVOICE")
                            sql = string.Format("select * from XaApPayableDet where SequenceId='{0}'", sequenceId);
                        if (arApInd_sch == "AR QUOTE")
                            sql = string.Format("select ChgCode,'' AS AcCode,'' AS AcSource,ChgDes AS ChgDes1,Unit,Currency,Gst,GstType from SeaQuoteDet1 where SequenceId='{0}'", sequenceId);
                        if (arApInd_sch == "AP QUOTE")
                            sql = string.Format("select ChgCode,'' AS AcCode,'' AS AcSource,ChgDes AS ChgDes1,Unit,Currency,Gst,GstType from SeaApQuoteDet1 where SequenceId='{0}'", sequenceId);
                        DataTable tab = ConnectSql.GetTab(sql);

                        for (int i = 0; i < tab.Rows.Count; i++)
                        {
                            string chgCode = tab.Rows[i]["ChgCode"].ToString();
                            //string acCode = tab.Rows[i]["AcCode"].ToString();
                            //string acSource = tab.Rows[i]["AcSource"].ToString();
                            string chgDes1 = tab.Rows[i]["ChgDes1"].ToString();
                            string unit = tab.Rows[i]["Unit"].ToString().ToUpper();
                            string currencyDes = tab.Rows[i]["Currency"].ToString();
                            decimal gst = SafeValue.SafeDecimal(tab.Rows[i]["Gst"], 0);
                            string gstType = tab.Rows[i]["GstType"].ToString();
                            
                            
                            XAArInvoiceDet det = new XAArInvoiceDet();
                            det.AcCode = GetArCodeByChgCode(chgCode);

                            det.AcSource = "CR";
                            if (docType == "CN")
                                det.AcSource = "DB";
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

                            det.DocId = invId;
                            index++;
                            det.DocLineNo = index;
                            det.DocNo = invNo;
                            det.DocType = docType;
                            det.MastRefNo = refNo;
                            det.JobRefNo = jobNo;
                            det.MastType = mastType;
                            det.SplitType = "Set";
                            C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(det);
                        }
                        UpdateArMaster(invId, docType);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                #endregion
            }
            else
            {
                string sql_mast = string.Format("select DocType,MastRefNo,JobRefNo,MastType from XAApPayable where SequenceId='{0}'", invId);
                DataTable tab_mast = C2.Manager.ORManager.GetDataSet(sql_mast).Tables[0];
                if (tab_mast.Rows.Count == 1)
                {
                    docType = SafeValue.SafeString(tab_mast.Rows[0]["DocType"]);
                    refNo = SafeValue.SafeString(tab_mast.Rows[0]["MastRefNo"]);
                    jobNo = SafeValue.SafeString(tab_mast.Rows[0]["JobRefNo"]);
                    mastType = SafeValue.SafeString(tab_mast.Rows[0]["MastType"]);
                }

                int index = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(string.Format("Select max(DocLineNo) FROM XAApPayableDet where DocId='{0}'", invId)), 0);
                #region ap pulling data

                for (int m = 0; m < list.Count; m++)
                {
                    try
                    {
                        int sequenceId = list[m].docId;
                        decimal qty = list[m].qty;
                        decimal price = list[m].price;
                        if (qty * price == 0)
                            continue;
                        decimal exRateDes = list[m].exRate;
                        string sql = "";
                        if (arApInd_sch == "AR INVOICE")
                            sql = string.Format("select * from XaArInvoiceDet where SequenceId='{0}'", sequenceId);
                        if (arApInd_sch == "AP INVOICE")
                            sql = string.Format("select * from XaApPayableDet where SequenceId='{0}'", sequenceId);
                        if (arApInd_sch == "AR QUOTE")
                            sql = string.Format("select ChgCode,'' AS AcCode, Qty, Price, ExRate, '' AS AcSource,ChgDes AS ChgDes1,Unit,Currency,Gst,GstType from SeaQuoteDet1 where SequenceId='{0}'", sequenceId);
                        if (arApInd_sch == "AP QUOTE")
                            sql = string.Format("select ChgCode,'' AS AcCode, Qty, Price, ExRate, '' AS AcSource,ChgDes AS ChgDes1,Unit,Currency,Gst,GstType from SeaApQuoteDet1 where SequenceId='{0}'", sequenceId);
                        DataTable tab = ConnectSql.GetTab(sql);

                        for (int i = 0; i < tab.Rows.Count; i++)
                        {
                            string chgCode = tab.Rows[i]["ChgCode"].ToString();
                            //string acCode = tab.Rows[i]["AcCode"].ToString();
                            //string acSource = tab.Rows[i]["AcSource"].ToString();
                            string chgDes1 = tab.Rows[i]["ChgDes1"].ToString();
                            string unit = tab.Rows[i]["Unit"].ToString().ToUpper();
                            string currencyDes = tab.Rows[i]["Currency"].ToString();
                            //decimal exRateDes = SafeValue.SafeDecimal(tab.Rows[i]["ExRate"], 0);
                            decimal gst = SafeValue.SafeDecimal(tab.Rows[i]["Gst"], 0);
                            string gstType = tab.Rows[i]["GstType"].ToString();
                            
                            if (qty == 0)
                                qty = SafeValue.SafeInt(tab.Rows[i]["Qty"], 1);
                            XAApPayableDet det = new XAApPayableDet();
                            det.AcCode = GetApCodeByChgCode(chgCode);
                            det.AcSource = "DB";
                            if (docType == "SC")
                                det.AcSource = "CR";
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

                            det.DocId = invId;
                            index++;
                            det.DocLineNo = index;
                            det.DocNo = invNo;
                            det.DocType = docType;
                            det.MastRefNo = refNo;
                            det.JobRefNo = jobNo;
                            det.MastType = mastType;
                            det.SplitType = "Set";
                            C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(det);
                        }
                        UpdateApMaster(invId, docType);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                #endregion
            }
        }
        else
        {
            e.Result = "Error, Pls refresh your invoice";
        }
    }
    #region chgcode update master
    private string GetArCodeByChgCode(string chgCode)
    {
        string sql = string.Format("Select ArCode from XXChgCode where ChgCodeId='{0}'", chgCode);
        string s = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (s.Length == 0)
            s = System.Configuration.ConfigurationManager.AppSettings["LocalArCode"];
        return s;
    }
    private string GetApCodeByChgCode(string chgCode)
    {
        string sql = string.Format("Select ApCode from XXChgCode where ChgCodeId='{0}'", chgCode);
        string s = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        if (s.Length == 0)
            s = System.Configuration.ConfigurationManager.AppSettings["LocalApCode"];
        return s;
    }

    private void UpdateArMaster(int docId, string docType)
    {

        string sql = string.Format("update XaArInvoiceDet set LineLocAmt=locAmt* (select ExRate from XAArInvoice where SequenceId=XaArInvoiceDet.docid) where DocId='{0}'", docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
        decimal docAmt = 0;
        decimal locAmt = 0;
        sql = string.Format("select AcSource,LocAmt,LineLocAmt from XAArInvoiceDet where DocId='{0}'", docId);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            if (tab.Rows[i]["AcSource"].ToString() == "CR")
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

        if (docType == "CN")
        {
            docAmt = -docAmt;
            locAmt = -locAmt;
        }
        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.DocAmt)
FROM  XAArReceiptDet AS det INNER JOIN XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

        balAmt += SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.DocAmt) 
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

        sql = string.Format("Update XAArInvoice set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
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
    #endregion


    List<Record> list = new List<Record>();
    internal class Record
    {
        public int docId = 0;
        public bool isPay = false;
        public decimal qty = 1;
        public decimal price = 1;
        public decimal exRate = 1;
        public Record(int _docId, decimal _qty, decimal _price, decimal _exRate)
        {
            docId = _docId;
            qty = _qty;
            price = _price;
            exRate = _exRate;
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
            ASPxSpinEdit qty = this.ASPxGridView1.FindRowTemplateControl(i, "spin_Qty") as ASPxSpinEdit;
            ASPxSpinEdit price = this.ASPxGridView1.FindRowTemplateControl(i, "spin_Price") as ASPxSpinEdit;
            ASPxSpinEdit exRate = this.ASPxGridView1.FindRowTemplateControl(i, "spin_ExRate") as ASPxSpinEdit;
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (docId != null && isPay != null && isPay.Checked && SafeValue.SafeDecimal(qty.Value, 0) > 0)
            {
                list.Add(new Record(SafeValue.SafeInt(docId.Text, 0), SafeValue.SafeDecimal(qty.Value, 0), SafeValue.SafeDecimal(price.Value, 0), SafeValue.SafeDecimal(exRate.Value, 0)
                    ));
            }
        }
    }
}

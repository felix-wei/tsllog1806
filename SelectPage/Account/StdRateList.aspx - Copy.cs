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

public partial class PagesSelectpge_StdRateList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.form1.Focus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
        OnLoad();
    }
    private void BindData()
    {
        if (Request.QueryString["id"] != null && Request.QueryString["typ"] != null)
        {
            string filter_ImpExpInd="";
            string filter_FclLclInd="";
            decimal qty = 0;
            int invId = SafeValue.SafeInt(Request.QueryString["id"], 0);
            string arAp = SafeValue.SafeString(Request.QueryString["typ"]).ToUpper();
            string sql = "";
            if (arAp == "AR")
                sql = string.Format("select MastRefNo,JobRefNo,MastType  from xaarinvoice where SequenceId='{0}'", invId);
            else if(arAp=="AP")
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
                        string sql_ref = "";
                        string sql_job = "";
                        if (mastType == "SI")
                        {
                            filter_ImpExpInd = "SI";
                            sql_ref = string.Format("select Pol,Pod,JobType from SeaImportRef where RefNo='{0}'", mastRefNo);
                            sql_job = string.Format("Select CustomerId,round(case when Weight/1000>volume then Weight/1000 else Volume end,3) as Qty FROM SeaImport where RefNo='{0}' and JobNo='{1}'", mastRefNo, jobRefNo);
                        }
                        else if (mastType == "SE")
                        {
                            filter_ImpExpInd = "SE";
                            sql_ref = string.Format("select Pol,Pod,JobType from SeaExportRef where RefNo='{0}'", mastRefNo);
                            sql_job = string.Format("Select CustomerId,round(case when Weight/1000>volume then Weight/1000 else Volume end,3) as Qty FROM SeaExport where RefNo='{0}' and JobNo='{1}'", mastRefNo, jobRefNo);
                        }
                        //else if (mastType == "AI")
                        //{
                        //    filter_ImpExpInd = "AI";
                        //    filter_FclLclInd = "";
                        //    qty = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM air_job where RefNo='{0}' and JobNo='{1}'", mastRefNo, jobRefNo)));
                        //}
                        //else if (mastType == "AE"||mastType=="ACT")
                        //{
                        //    filter_ImpExpInd = "AE";
                        //    filter_FclLclInd = "";
                        //    qty = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM air_job where RefNo='{0}' and JobNo='{1}'", mastRefNo, jobRefNo)));
                        //}
                        //else if (mastType == "TPT")
                        //{
                        //    filter_ImpExpInd = "TPT";
                        //    filter_FclLclInd = "";
                        //    qty = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(string.Format("Select round(case when wt/1000>m3 then wt/1000 else m3 end,3) FROM tpt_Job where JobNo='{0}'", mastRefNo)));
                        //}
                        if (sql_ref.Length > 0 && sql_job.Length > 0)
                        {
                            DataTable mast = ConnectSql.GetTab(sql_ref);
                            DataTable det = ConnectSql.GetTab(sql_job);
                            string pol = "";
                            string pod = "";
                            string partyTo = "";
                            if (mast.Rows.Count == 1)
                            {
                                pol = SafeValue.SafeString(mast.Rows[0]["Pol"]);
                                pod = SafeValue.SafeString(mast.Rows[0]["Pod"]);
                                filter_FclLclInd = SafeValue.SafeString(mast.Rows[0]["JobType"]);
                                if (filter_FclLclInd == "CONSOL")
                                    filter_FclLclInd = "Lcl";
                            }
                            if (det.Rows.Count == 1)
                            {
                                qty = SafeValue.SafeInt(det.Rows[0]["Qty"], 0);
                                partyTo = SafeValue.SafeString(det.Rows[0]["CustomerId"]);
                            }
                            if (qty == 0)
                                qty = 1;
                            sql = string.Format(@"select Sequenceid,
case when unit='set' or unit='SHPT' then 
1 else  (case when {4}*price> amt then {4} else 1 end) end as qty
,case when unit='set' or unit='SHPT' then (case when price> amt then Price else amt end)
else (case when {4}*price> amt then Price else amt end)   end as PRICE
,ChgCode,Currency,GstType,ExRate
from seaquotedet1 where QuoteId='-1' and FclLclInd='{3}' and (isnull(PartyTo,'')='{0}' or isnull(PartyTo,'')='') and (isnull(Pol,'')='{1}' or isnull(Pol,'')='') and (isnull(Pod,'')='{2}' or isnull(Pod,'')='')
and ImpExpInd='{5}'", 
partyTo,pol,pod, filter_FclLclInd, qty,filter_ImpExpInd);
                            DataTable tab1 = ConnectSql.GetTab(sql);
                            this.ASPxGridView1.DataSource = tab1;
                            this.ASPxGridView1.DataBind();
                        }
                    }
                }
            }

        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
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
            string sql_mast = string.Format("select DocType,MastRefNo,JobRefNo,MastType from XAArInvoice where SequenceId='{0}'", invId);
            DataTable tab_mast = C2.Manager.ORManager.GetDataSet(sql_mast).Tables[0];
            if (tab_mast.Rows.Count == 1)
            {
                docType = SafeValue.SafeString(tab_mast.Rows[0]["DocType"]);
                refNo = SafeValue.SafeString(tab_mast.Rows[0]["MastRefNo"]);
                jobNo = SafeValue.SafeString(tab_mast.Rows[0]["JobRefNo"]);
                mastType = SafeValue.SafeString(tab_mast.Rows[0]["MastType"]);
            }

            //if (mastType == "SI")
            //    sql_mast = string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM SeaImport where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
            //else if (mastType == "SE")
            //    sql_mast = string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM SeaExport where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
            //decimal qty = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql_mast), 0);

            int index = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(string.Format("Select max(DocLineNo) FROM XAArInvoiceDet where DocId='{0}'", invId)), 0);
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    index++;
                    int sequenceId = list[i].docId;
                    decimal qty = list[i].qty;
                    decimal price = list[i].price;
                    decimal exRateDes = list[i].exRate;
                    if (qty * price == 0)
                        continue;
                    string sql = string.Format(@"SELECT  ChgCode, ChgDes, Currency, ExRate,Price, Unit, MinAmt, Rmk, Qty, Amt, GstType, Gst
 FROM SeaQuoteDet1 where SequenceId='{0}' order by QuoteLineNo", sequenceId);
                    DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                    if (tab.Rows.Count == 1)
                    {
                        string chgCode = tab.Rows[0]["ChgCode"].ToString();
                        string acCode = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select ArCode from XXchgcode where ChgCodeId='{0}'", chgCode)));
                        string acSource = "CR";
                        string chgDes1 = tab.Rows[0]["ChgDes"].ToString();
                        //decimal price = SafeValue.SafeDecimal(tab.Rows[0]["Price"], 0);
                        string unit = tab.Rows[0]["Unit"].ToString().ToUpper();
                        string currencyDes = tab.Rows[0]["Currency"].ToString();
                        //decimal exRateDes = SafeValue.SafeDecimal(tab.Rows[0]["ExRate"], 0);
                        decimal gst = SafeValue.SafeDecimal(tab.Rows[0]["Gst"], 0);
                        string gstType = tab.Rows[0]["GstType"].ToString();

                        //if (qty == 0)
                        //    qty = SafeValue.SafeInt(tab.Rows[i]["Qty"], 1);

                        XAArInvoiceDet det = new XAArInvoiceDet();
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
            UpdateArMaster(invId,docType);
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
        public decimal qty = 1;
        public decimal price = 1;
        public decimal exRate = 1;
        public Record(int _docId,decimal _qty,decimal _price,decimal _exRate)
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
            if (docId != null && isPay != null && isPay.Checked&&SafeValue.SafeDecimal(qty.Value,0)>0)
            {
                list.Add(new Record(SafeValue.SafeInt(docId.Text, 0), SafeValue.SafeDecimal(qty.Value, 0), SafeValue.SafeDecimal(price.Value, 0), SafeValue.SafeDecimal(exRate.Value, 0)
                    ));
            }
        }
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
}

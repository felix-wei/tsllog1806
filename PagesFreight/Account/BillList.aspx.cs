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
            this.cmb_ArAp.Text = "AR";
        }
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string invNo = this.txt_No.Text.Trim();
        string where = "";
        string sql = "";
        if (invNo.Length > 0)
        {
            if (this.cmb_ArAp.Text == "AR")
                sql = "select SequenceId,ChgCode,Qty,Price,Currency,GstType,Locamt from XaArInvoiceDet";
            else if (this.cmb_ArAp.Text == "AP")
                sql = "select SequenceId,ChgCode,Qty,Price,Currency,GstType,Locamt from XaApPayableDet";

            if (invNo.Length > 0)
            {
                where = " where DocNo='" + invNo + "'";
            }
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
                sql_mast = "";
                if (mastType == "SI")
                    sql_mast = string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM SeaImport where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
                else if (mastType == "SE")
                    sql_mast = string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM SeaExport where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
                else if (mastType.Length > 1 && mastType.Substring(0, 1) == "A")
                    sql_mast = string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM air_job where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
                else if (mastType == "TPT" || mastType == "LH")
                    sql_mast = string.Format("Select round(case when wt/1000>m3 then wt/1000 else m3 end,3) FROM tpt_Job where JobNo='{0}'", refNo, jobNo);
                decimal qty = 0;
                if (sql_mast.Length > 0)
                    qty = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql_mast), 0);

                int index = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(string.Format("Select max(DocLineNo) FROM XAArInvoiceDet where DocId='{0}'", invId)), 0);
                #region ar pulling data
                for (int m = 0; m < list.Count; m++)
                {
                    try
                    {
                        int sequenceId = list[m].docId;

                        string sql = string.Format("select * from XaArInvoiceDet where SequenceId='{0}'", sequenceId);
                        if (arApInd_sch == "AP")
                            sql = string.Format("select * from XaApPayableDet where SequenceId='{0}'", sequenceId);
                        DataTable tab = ConnectSql.GetTab(sql);

                        for (int i = 0; i < tab.Rows.Count; i++)
                        {
                            string chgCode = tab.Rows[i]["ChgCode"].ToString();
                            string acCode = tab.Rows[i]["AcCode"].ToString();
                            string acSource = tab.Rows[i]["AcSource"].ToString();
                            string chgDes1 = tab.Rows[i]["ChgDes1"].ToString();
                            decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"], 0);
                            string unit = tab.Rows[i]["Unit"].ToString().ToUpper();
                            string currencyDes = tab.Rows[i]["Currency"].ToString();
                            decimal exRateDes = SafeValue.SafeDecimal(tab.Rows[i]["ExRate"], 0);
                            decimal gst = SafeValue.SafeDecimal(tab.Rows[i]["Gst"], 0);
                            string gstType = tab.Rows[i]["GstType"].ToString();
                            
                            if (qty == 0)
                                qty = SafeValue.SafeInt(tab.Rows[i]["Qty"], 1);
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
                            det.SplitType = "SET";
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
                sql_mast = "";
                if (mastType == "SI")
                    sql_mast = string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM SeaImport where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
                else if (mastType == "SE")
                    sql_mast = string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM SeaExport where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
                else if (mastType.Length > 1 && mastType.Substring(0, 1) == "A")
                    sql_mast = string.Format("Select round(case when Weight/1000>volume then Weight/1000 else Volume end,3) FROM air_job where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
                else if (mastType == "TPT" || mastType == "LH")
                    sql_mast = string.Format("Select round(case when wt/1000>m3 then wt/1000 else m3 end,3) FROM tpt_Job where JobNo='{0}'", refNo, jobNo);
                decimal qty = 0;
                if (sql_mast.Length > 0)
                    qty=SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql_mast), 0);

                int index = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(string.Format("Select max(DocLineNo) FROM XAApPayableDet where DocId='{0}'", invId)), 0);
                #region ap pulling data

                for (int m = 0; m < list.Count; m++)
                {
                    try
                    {
                        int sequenceId = list[m].docId;
                        string sql = string.Format("select * from XaArInvoiceDet where SequenceId='{0}'", sequenceId);
                        if (arApInd_sch == "AP")
                            sql = string.Format("select * from XaApPayableDet where SequenceId='{0}'", sequenceId);
                        DataTable tab = ConnectSql.GetTab(sql);

                        for (int i = 0; i < tab.Rows.Count; i++)
                        {
                            string chgCode = tab.Rows[i]["ChgCode"].ToString();
                            string acCode = tab.Rows[i]["AcCode"].ToString();
                            string acSource = tab.Rows[i]["AcSource"].ToString();
                            string chgDes1 = tab.Rows[i]["ChgDes1"].ToString();
                            decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"], 0);
                            string unit = tab.Rows[i]["Unit"].ToString().ToUpper();
                            string currencyDes = tab.Rows[i]["Currency"].ToString();
                            decimal exRateDes = SafeValue.SafeDecimal(tab.Rows[i]["ExRate"], 0);
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
                            det.SplitType = "SET";
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
        public Record(int _docId)
        {
            docId = _docId;
        }

    }
    private void OnLoad()
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 100;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox docId = this.ASPxGridView1.FindRowTemplateControl(i, "txt_docId") as ASPxTextBox;
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (docId != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(docId.Text, 0)
                    ));
            }
        }
    }
}

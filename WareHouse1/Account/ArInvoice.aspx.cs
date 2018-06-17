using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2;

public partial class Warehouse_Account_ArInvoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql = "";
            string soNo = SafeValue.SafeString(Request.QueryString["RefN"]);
            string mastType = SafeValue.SafeString(Request.QueryString["JobType"]);
            string typ = SafeValue.SafeString(Request.QueryString["JobN"]).ToUpper();
            string docType = SafeValue.SafeString(Request.QueryString["DocType"]).ToUpper();
            if (typ == "SO")//do it by so 
            {
                sql = string.Format("select count(*) from XaArInvoice where MastRefNo='{0}' and MastType='WH' and DocType='{1}'", soNo, docType);
                if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) > 0)
                {
                    Response.Write("Already have invoice, can't generate !");
                    Response.End();
                }
                if (docType == "IV")
                {
                    sql = string.Format(@"select count(*) from Wh_TransDet where DoNo='{0}'", soNo);
                    if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) ==0)
                    {
                        Response.Write("NO SKU Line, can't generate !");
                        Response.End();
                    }
                    else
                    {
                        string invNo = InsertInv(mastType, soNo, "", docType);
                        EzshipLog.Log(soNo, "", "SO", "Create SO Inv");
                        Response.Redirect("/opsAccount/ArInvoiceEdit.aspx?no=" + invNo);
                    }
                }
                if(docType=="CN")
                {
                    sql = string.Format(@"select count(*) from Wh_TransDet where DoNo='{0}'", soNo);
                    if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) == 0)
                    {
                        Response.Write("NO SKU Line, can't generate !");
                        Response.End();
                    }
                    else
                    {
                        string invNo = InsertInv(mastType, soNo, "", docType);
                        EzshipLog.Log(soNo, "", "SO", "Create CN");
                        Response.Redirect("/opsAccount/ArCnEdit.aspx?no=" + invNo);
                    }
                }
            }
            else if (typ == "DO")// do it by so
            {
                sql = string.Format(@"select count(*) from Wh_TransDet where DoNo='{0}'", soNo);
                if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) == 0)
                {
                    Response.Write("NO SKU Line, can't generate !");
                    Response.End();
                }
                sql = string.Format("select DoNo from wh_do where PoNo='{0}'", soNo);
                DataTable tab = ConnectSql.GetTab(sql);
                string invNo = "";
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    string doNo = tab.Rows[i][0].ToString();
                    string sql1 = string.Format("select count(*) from XaArInvoice where (MastRefNo='{0}' or JobRefNo='{0}') and MastType='WH'", doNo);
                    if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql1), 0) == 0)
                    {
                        invNo = InsertInv(mastType,soNo, doNo, docType);

                    }
                }
                if (invNo.Length > 0)
                {
                    EzshipLog.Log(soNo, "", "DO", "Create DO Inv");
                    Response.Redirect("/opsAccount/ArInvoiceEdit.aspx?no=" + invNo);
                }
                else if (tab.Rows.Count == 0)
                {
                    Response.Write("Don't have DO !");
                    Response.End();
                }
                else
                {
                    Response.Write("DO have the Invoice!");
                    Response.End();
                }
            }
            else if(typ=="PO")
            {
                sql = string.Format(@"select count(*) from Wh_TransDet where DoNo='{0}'", soNo);
                if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) == 0)
                {
                    Response.Write("NO SKU Line, can't generate !");
                    Response.End();
                }
                sql = string.Format("select count(*) from XAApPayable where (MastRefNo='{0}' or JobRefNo='{0}')  and MastType='WH' and DocType='PL'", soNo);
                if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) > 0)
                {
                    Response.Write("Already have PO Payable, can't generate !");
                    Response.End();
                }
                if (docType == "CN")
                {
                    string invNo = InsertPl(mastType, soNo, "", docType);
                    EzshipLog.Log(soNo, "", "PO", "Create CN");
                    Response.Redirect("/opsAccount/ArCnEdit.aspx?no=" + invNo);
                }
                if (docType == "PL")
                {
                    string invNo = InsertPl(mastType, soNo, "", "PL");
                    EzshipLog.Log(soNo, "", "PO", "Create Payable");
                    Response.Redirect("/opsAccount/ApPayableEdit.aspx?no=" + invNo);
                }
            }
            else if (typ == "DI")
            {
                sql = string.Format(@"select count(*) from Wh_TransDet where DoNo='{0}'", soNo);
                if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0) == 0)
                {
                    Response.Write("NO SKU Line, can't generate !");
                    Response.End();
                }
                sql = string.Format("select DoNo from wh_do where PoNo='{0}'", soNo);
                DataTable tab = ConnectSql.GetTab(sql);
                string invNo = "";
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    string doNo = tab.Rows[i][0].ToString();
                    string sql1 = string.Format("select count(*) from XaArInvoice where (MastRefNo='{0}' or JobRefNo='{0}') and MastType='WH'", doNo);
                    if (SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql1), 0) == 0)
                    {
                        invNo = InsertPl(mastType, soNo, doNo,"PL");
                    }
                }
                if (invNo.Length > 0&&docType=="PL")
                {
                    EzshipLog.Log(soNo, "", "DI", "Create Payable");
                    Response.Redirect("/opsAccount/ApPayableEdit.aspx?no=" + invNo);
                }
                else if (tab.Rows.Count == 0)
                {
                    Response.Write("Don't have DO !");
                    Response.End();
                }
                else
                {
                    Response.Write("DO have the Invoice!");
                    Response.End();
                }
            }
        }
    }
    private string InsertInv(string mastType, string soNo, string doNo,string docType)
    {
        string sql = string.Format(@"select mast.PartyId,p.termid as Term
,det.ProductCode as Sku,det.LotNo,det.Qty1 as Qty,det.Price,mast.Currency,mast.ExRate,det.Gst
,det.Des1
from wh_transDet det inner join wh_trans mast on mast.DoNO=det.DoNo and mast.DoType=det.DoType
left join xxparty p on p.partyid=mast.partyid 
where mast.DoNO='{0}' ", soNo);
        if (doNo.Length > 0)
        {
            sql = string.Format(@"select mast.PartyId,p.termid as Term
,det.ProductCode as Sku,det.LotNo,det.Qty1 as Qty,tsDet.Price,tsMast.Currency,tsMast.ExRate,tsDet.Gst
,det.Des1
from wh_DoDet det inner join wh_do mast on mast.DoNO=det.DoNo and mast.DoType=det.DoType
left join xxparty p on p.partyid=mast.partyid
left join wh_transdet tsDet on tsDet.ProductCode=det.ProductCode and tsDet.LotNo=det.LotNo
left join wh_trans tsMast on tsMast.doNO=tsDet.DoNo and tsMast.DoType=tsDet.DoType
where mast.DoNO='{1}'  and tsDet.DoNO='{0}' ", soNo,doNo);
        }
        string invN = "";
        int invId = 0;
        DataTable tab = ConnectSql.GetTab(sql);
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            if (i == 0)
            {
                string partyId = SafeValue.SafeString(tab.Rows[0]["PartyId"]);
                string term = SafeValue.SafeString(tab.Rows[0]["Term"]);
                #region invoice mast
                string counterType = "AR-IV";
                if(docType=="CN"){
                    counterType = "AR-CN";
                }

                C2.XAArInvoice inv = new C2.XAArInvoice();

                inv.DocDate = DateTime.Today;
                invN = C2Setup.GetNextNo("", counterType, inv.DocDate);
                inv.PartyTo =partyId;
                inv.DocType = docType;
                inv.DocNo = invN.ToString();
                string[] currentPeriod = EzshipHelper.GetAccPeriod(inv.DocDate);

                inv.AcYear = SafeValue.SafeInt(currentPeriod[1], inv.DocDate.Year);
                inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], inv.DocDate.Month);
                inv.Term = term;

                //
                int dueDay = SafeValue.SafeInt(inv.Term.ToUpper().Replace("DAYS", "").Trim(), 0);
                inv.DocDueDate = inv.DocDate.AddDays(dueDay);//SafeValue.SafeDate(dueDt.Text, DateTime.Now);
                inv.Description = "";
                inv.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                inv.ExRate = 1;
                inv.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId);
                inv.AcSource = "DB";
                inv.SpecialNote = "";

                inv.MastType = mastType;
                inv.MastRefNo = soNo;
                inv.JobRefNo = doNo;
                if (doNo.Length > 0)
                {
                    inv.MastRefNo = doNo;
                    inv.JobRefNo = "";
                }
                inv.ExportInd = "N";
                inv.UserId = HttpContext.Current.User.Identity.Name;
                inv.EntryDate = DateTime.Now;
                inv.CancelDate = new DateTime(1900, 1, 1);
                inv.CancelInd = "N";
                inv.InvType = "ACCOUNT";
                try
                {
                    C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(inv);
                    C2Setup.SetNextNo("", counterType, invN, inv.DocDate);
                    invId = inv.SequenceId;
                }
                catch
                {
                }
                #endregion
                if (invId<1)
                    return "";
            }
            string sku = SafeValue.SafeString(tab.Rows[i]["Sku"]);
            string des = SafeValue.SafeString(tab.Rows[i]["Des1"]);
            string lotNo = SafeValue.SafeString(tab.Rows[i]["LotNo"]);
            int qty = SafeValue.SafeInt(tab.Rows[i]["Qty"], 0);
            decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"], 0);
            string cur = SafeValue.SafeString(tab.Rows[i]["Currency"]);
            decimal exRate = SafeValue.SafeDecimal(tab.Rows[i]["ExRate"], 0);
            decimal gst = SafeValue.SafeDecimal(tab.Rows[i]["Gst"], 0);
            InsertInv_Det(invId, invN, i+1, sku, lotNo,des, qty, price, cur, exRate, gst, soNo, doNo, mastType,docType);
        }
        if (invId > 0)
        {
            if(docType=="IV"){
                UpdateMaster(invId);
            }
            if(docType=="CN"){
                UpdateCN_Master(invId);
            }
            sql = string.Format("update Wh_Trans set DoStatus='Closed',UpdateBy='{1}',UpdateDateTime='{2}' where DoNo='{0}'", soNo, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            int res = Manager.ORManager.ExecuteCommand(sql);
            if (res > 0)
            {
                EzshipLog.Log(soNo, "", "SO", "Closed");
            }
        }
        return invN;
    }
    private void InsertInv_Det(int docId, string docNo, int index, string sku, string lotNo,string des, int qty, decimal price, string cur, decimal exRate, decimal gst, string soNo, string doNo, string mastType,string docType)
    {
        try
        {
            C2.XAArInvoiceDet det = new C2.XAArInvoiceDet();
            det.DocId = docId;
            det.DocLineNo = index;
            det.DocNo = docNo;
            det.DocType = docType;
            det.AcCode = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select ArCode from ref_product where Code='{0}'", sku)), System.Configuration.ConfigurationManager.AppSettings["ItemArCode"]);
            det.AcSource = "CR";
            det.MastRefNo = soNo;
            det.JobRefNo = lotNo;
            if (doNo.Length > 0)
            {
                det.MastRefNo = doNo;
                det.JobRefNo = "";
            }
            det.MastType = mastType;
            det.SplitType = "";



            det.ChgCode = sku;
            det.ChgDes1 = des;
            det.ChgDes2 = "";
            det.ChgDes3 = "";

            det.Price = price;
            det.Qty = qty;
            det.Unit = "";
            det.Currency = cur;
            det.ExRate = 1;
            det.Gst = gst;
            if (det.ExRate == 0)
                det.ExRate = 1;
            if (det.Gst > 0)
                det.GstType = "S";
            else if (det.Currency == System.Configuration.ConfigurationManager.AppSettings["Currency"])
                det.GstType = "E";
            else
                det.GstType = "Z";
            decimal amt = SafeValue.ChinaRound(det.Qty * det.Price, 2);
            decimal gstAmt = SafeValue.ChinaRound((amt * det.Gst), 2);
            decimal docAmt = amt + gstAmt;
            decimal locAmt = SafeValue.ChinaRound(docAmt * det.ExRate, 2);
            det.GstAmt = gstAmt;
            det.DocAmt = docAmt;
            det.LocAmt = locAmt;
            det.OtherAmt = 0;
            C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(det);
        }
        catch
        {
        }
    }
   private void UpdateMaster(int docId)
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


        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.DocAmt)
FROM  XAArReceiptDet AS det INNER JOIN XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

        balAmt += SafeValue.SafeDecimal(Manager.ORManager.GetDataSet(string.Format(@"SELECT sum(det.DocAmt) 
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

        sql = string.Format("Update XAArInvoice set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
     private string InsertPl(string mastType, string soNo, string doNo,string type)
    {
        string sql = string.Format(@"select mast.PartyId,p.termid as Term
,det.ProductCode as Sku,det.LotNo,det.Qty1 as Qty,det.Price,mast.Currency,mast.ExRate,det.Gst
,det.Des1
from wh_transDet det inner join wh_trans mast on mast.DoNO=det.DoNo and mast.DoType=det.DoType
left join xxparty p on p.partyid=mast.partyid 
where mast.DoNO='{0}' ", soNo);
        if (doNo.Length > 0)
        {
            sql = string.Format(@"select mast.PartyId,p.termid as Term
,det.ProductCode as Sku,det.LotNo,det.Qty1 as Qty,tsDet.Price,tsMast.Currency,tsMast.ExRate,tsDet.Gst
,det.Des1
from wh_DoDet det inner join wh_do mast on mast.DoNO=det.DoNo and mast.DoType=det.DoType
left join xxparty p on p.partyid=mast.partyid
left join wh_transdet tsDet on tsDet.ProductCode=det.ProductCode and tsDet.LotNo=det.LotNo
left join wh_trans tsMast on tsMast.doNO=tsDet.DoNo and tsMast.DoType=tsDet.DoType
where mast.DoNO='{1}'  and tsDet.DoNO='{0}' ", soNo, doNo);
        }
        string invN = "";
        int invId = 0;
        DataTable tab = ConnectSql.GetTab(sql);
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            if (i == 0)
            {
                string partyId = SafeValue.SafeString(tab.Rows[0]["PartyId"]);
                string term = SafeValue.SafeString(tab.Rows[0]["Term"]);
                #region invoice mast
                string counterType = "";
                if (type == "PL")
                    counterType = "AP-PAYABLE";
                else
                    counterType = "AP-Voucher";
                C2.XAApPayable inv = new C2.XAApPayable();

                inv.DocDate = DateTime.Today;
                invN = C2Setup.GetNextNo("", counterType, inv.DocDate);
                inv.PartyTo = partyId;
                inv.DocType = type;
                inv.DocNo = invN.ToString();
                string[] currentPeriod = EzshipHelper.GetAccPeriod(inv.DocDate);

                inv.AcYear = SafeValue.SafeInt(currentPeriod[1], inv.DocDate.Year);
                inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], inv.DocDate.Month);
                inv.Term = term;

                //
                int dueDay = SafeValue.SafeInt(inv.Term.ToUpper().Replace("DAYS", "").Trim(), 0);
                inv.Description = "";
                inv.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                inv.ExRate = 1;
                inv.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId);
                inv.AcSource = "CR";

                inv.MastType = mastType;
                inv.MastRefNo = soNo;
                inv.JobRefNo = doNo;

                inv.ExportInd = "N";
                inv.UserId = HttpContext.Current.User.Identity.Name;
                inv.EntryDate = DateTime.Now;
                inv.CancelDate = new DateTime(1900, 1, 1);
                inv.CancelInd = "N";
                try
                {
                    C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(inv);
                    C2Setup.SetNextNo("", counterType, invN, inv.DocDate);
                    invId = inv.SequenceId;
                }
                catch
                {
                }
                #endregion
                if (invId < 1)
                    return "";
            }
            string sku = SafeValue.SafeString(tab.Rows[i]["Sku"]);
            string des = SafeValue.SafeString(tab.Rows[i]["Des1"]);
            string lotNo = SafeValue.SafeString(tab.Rows[i]["LotNo"]);
            int qty = SafeValue.SafeInt(tab.Rows[i]["Qty"], 0);
            decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"], 0);
            string cur = SafeValue.SafeString(tab.Rows[i]["Currency"]);
            decimal exRate = SafeValue.SafeDecimal(tab.Rows[i]["ExRate"], 0);
            decimal gst = SafeValue.SafeDecimal(tab.Rows[i]["Gst"], 0);
            InsertPl_Det(invId, invN, type, i + 1, sku, lotNo,des, qty, price, cur, exRate, gst, soNo, doNo, mastType);
        }
        if (invId > 0)
            UpdateMaster1(invId);
        return invN;
    }
    private void InsertPl_Det(int docId,string docNo, string docType,int index, string sku, string lotNo, string des,int qty, decimal price, string cur, decimal exRate, decimal gst, string soNo, string doNo, string mastType)
    {
        try
        {
            C2.XAApPayableDet det = new C2.XAApPayableDet();
            det.DocId = docId;
            det.DocLineNo = index;
            det.DocNo = docNo;
            det.DocType = docType;
            det.AcCode = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select ApCode from ref_product where Code='{0}'", sku)), System.Configuration.ConfigurationManager.AppSettings["ItemApCode"]);
            det.AcSource = "DB";
            det.MastRefNo = soNo;
            det.JobRefNo = lotNo;
            det.MastType = mastType;
            det.SplitType = "";



            det.ChgCode = sku;
            det.ChgDes1 = des;
            det.ChgDes2 = "";
            det.ChgDes3 = "";
            det.Price = price;
            det.Qty = qty;
            det.Unit = "";
            det.Currency = cur;
            det.ExRate = 1;
            det.Gst = gst;
            if (det.ExRate == 0)
                det.ExRate = 1;
            if (det.Gst > 0)
                det.GstType = "S";
            else if (det.Currency == System.Configuration.ConfigurationManager.AppSettings["Currency"])
                det.GstType = "E";
            else
                det.GstType = "Z";
            decimal amt = SafeValue.ChinaRound(det.Qty * det.Price, 2);
            decimal gstAmt = SafeValue.ChinaRound((amt * det.Gst), 2);
            decimal docAmt = amt + gstAmt;
            decimal locAmt = SafeValue.ChinaRound(docAmt * det.ExRate, 2);
            det.GstAmt = gstAmt;
            det.DocAmt = docAmt;
            det.LocAmt = locAmt;
            C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(det);
        }
        catch
        {
        }
    }
    private void UpdateMaster1(int docId)
    {
        string sql = string.Format("update XAApPayableDet set LineLocAmt=locAmt* (select ExRate from XAApPayable where SequenceId=XAApPayableDet.docid) where DocId={0}", docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
        decimal docAmt = 0;
        decimal locAmt = 0;
        sql = string.Format("select AcSource,LocAmt,LineLocAmt from XAApPayableDet where DocId={0}", docId);
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
        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.DocAmt)
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", docId)), 0);
        balAmt += SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.DocAmt)
FROM XAArReceiptDet AS det INNER JOIN  XAArInvoice AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", docId)), 0);

        sql = string.Format("Update XAApPayable set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    private void UpdateCN_Master(int docId)
    {
        string sql = string.Format("update XaArInvoiceDet set LineLocAmt=locAmt* (select ExRate from XAArInvoice where SequenceId=XaArInvoiceDet.docid) where DocId='{0}'", docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
        decimal docAmt = 0;
        decimal locAmt = 0;
        //sql = string.Format("select AcSource,LocAmt,LineLocAmt from XAArInvoiceDet where DocId='{0}'", docId);
        //DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        //for (int i = 0; i < tab.Rows.Count; i++)
        //{
        //    if (tab.Rows[i]["AcSource"].ToString() == "DB")
        //    {
        //        docAmt += SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
        //        locAmt += SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
        //    }
        //    else
        //    {
        //        docAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
        //        locAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
        //    }
        //}
        sql = string.Format("select sum(case when AcSource='CR' then LocAmt else -LocAmt end) as LocAmt,sum(case when AcSource='CR' then LineLocAmt else -LineLocAmt end) as LineLocAmt from XAArInvoiceDet where DocId='{0}' ", docId);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (tab.Rows.Count == 0) return;
        docAmt = -SafeValue.SafeDecimal(tab.Rows[0][0], 0);
        locAmt = -SafeValue.SafeDecimal(tab.Rows[0][1], 0);

        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.DocAmt)
FROM  XAArReceiptDet AS det INNER JOIN  XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE     (det.DocId = '{0}') and (det.DocType='CN')", docId)), 0);

        balAmt += SafeValue.SafeDecimal(Manager.ORManager.GetDataSet(string.Format(@"SELECT sum(det.DocAmt) 
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='CN')", docId)), 0);

        sql = string.Format("Update XAArInvoice set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
}
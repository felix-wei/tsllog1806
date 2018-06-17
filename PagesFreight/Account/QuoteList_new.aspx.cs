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

public partial class PagesFreight_Account_QuoteList_new : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["JobType"] != null && Request.QueryString["RefN"] != null && Request.QueryString["JobN"] != null)
            {
                string refType = Request.QueryString["JobType"].ToString();
                string refNo = Request.QueryString["RefN"].ToString();
                string jobNo = Request.QueryString["JobN"].ToString();
                string sql = string.Format("select count(*) from XAArInvoice where Doctype='IV' and  MastRefNo='{0}' and JobRefNo='{1}' and  MastType='{2}'", refNo, jobNo, refType);
                int cnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql), 0);
                if (cnt > 0)//have invoice ,
                {
                    Response.Redirect(string.Format("ArInvoiceEdit.aspx?no=0&JobType={0}&RefN={1}&JobN={2}", refType, refNo, jobNo));
                }
                else
                {
                    DateTime eta = DateTime.Now;
                    string pol = "";
                    string pod = "";
                    string partyTo = "";
                    string jobType = "";
                    string sql_Job = "";
                    if (refType == "SI")
                    {
                        sql = string.Format("SELECT Eta, Pol, Pod, JobType FROM SeaImportRef where RefNo='{0}'", refNo);
                        sql_Job = string.Format("SELECT CustomerId FROM SeaImport where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
                    }
                    else
                    {
                        sql = string.Format("SELECT Eta, Pol, Pod, JobType FROM SeaExportRef where RefNo='{0}'", refNo);
                        sql_Job = string.Format("SELECT CustomerId FROM SeaExport where RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
                    }
                    partyTo = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql_Job), "");
                    DataTable tab = Manager.ORManager.GetDataSet(sql).Tables[0];
                    if (tab.Rows.Count == 1)
                    {
                        eta = SafeValue.SafeDate(tab.Rows[0]["Eta"], new DateTime(1900, 1, 1));
                        pol = SafeValue.SafeString(tab.Rows[0]["Pol"], "");
                        pod = SafeValue.SafeString(tab.Rows[0]["Pod"], "");
                        jobType = SafeValue.SafeString(tab.Rows[0]["JobType"], "");

                    }
                    if (jobType == "FCL")
                    {
                        sql = string.Format(@"SELECT  SeaQuote1.SequenceId, SeaQuote1.Pol, SeaQuote1.Pod, SeaQuote1.Title, SeaQuote1.ViaPort, SeaQuote1.FclLclInd, SeaQuote1.QuoteNo, SeaQuote1.Status, 
                      SeaQuote1.QuoteDate, SeaQuote1.ExpireDate, SeaQuote1.Attention, SeaQuote1.SalesmanId, SeaQuote1.CurrencyId, XXParty.Name AS PartyName
FROM SeaQuote1 left join XXParty on SeaQuote1.PartyTo=XXParty.PartyId where FclLclInd='FCL'  and  (PartyTo='{0}' or PartyTo='') and ExpireDate>='{1}'", partyTo, eta.ToString("yyyy-MM-dd"));
                        if (refType == "SI")
                        {
                            sql += string.Format(" and (Pol='{0}' or Pol='') and Pod='{1}'",pol, pod);
                        }
                        else
                        {
                            sql += string.Format(" and Pol='{0}' and (Pod='{1}' or Pod='')", pol, pod);
                        }
                    }
                    else
                    {
                        sql = string.Format(@"SELECT  SeaQuote1.SequenceId, SeaQuote1.Pol, SeaQuote1.Pod, SeaQuote1.Title, SeaQuote1.ViaPort, SeaQuote1.FclLclInd, SeaQuote1.QuoteNo, SeaQuote1.Status, 
                      SeaQuote1.QuoteDate, SeaQuote1.ExpireDate, SeaQuote1.Attention, SeaQuote1.SalesmanId, SeaQuote1.CurrencyId, XXParty.Name AS PartyName
FROM SeaQuote1 left join XXParty on SeaQuote1.PartyTo=XXParty.PartyId where FclLclInd!='FCL' and (PartyTo='{0}' or PartyTo='') and ExpireDate>='{1}'", partyTo, eta.ToString("yyyy-MM-dd"));
                        if (refType == "SI")
                        {
                            sql += string.Format(" and (Pol='{0}' or Pol='') and Pod='{1}'",pol, pod);
                        }
                        else
                        {
                            sql += string.Format(" and Pol='{0}' and (Pod='{1}' or Pod='')", pol, pod);
                        }
                    }
                    DataTable tab_quote = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                    this.grid_Sch.DataSource = tab_quote;
                    this.grid_Sch.DataBind();
                }
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["JobType"] != null && Request.QueryString["RefN"] != null && Request.QueryString["JobN"] != null)
        {
            string refType = "SI";
            string refNo = Request.QueryString["RefN"].ToString();
            string jobNo = Request.QueryString["JobN"].ToString();
            Response.Redirect(string.Format("ArInvoiceEdit.aspx?no=0&JobType={0}&RefN={1}&JobN={2}", refType, refNo, jobNo));
        }
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters!= null)
        {
            string quoteId = e.Parameters;

            string sql = string.Format("SELECT SequenceId,DocNo,  DocFromDate, DocToDate, CurrencyId, Term,  Pol, Pod, Description FROM SeaQuote where SequenceId='{0}'", quoteId);
            string currency = "SGD";
            decimal exRate = 1;
            string term = "CASH";

            string refType = Request.QueryString["JobType"].ToString();
            string refNo = Request.QueryString["RefN"].ToString();
            string jobNo = Request.QueryString["JobN"].ToString();

            string partyTo = "";
            string sql_Job = "";
            if (refType == "SI")
            {
                sql_Job = string.Format(@"SELECT job.CustomerId, cust.TermId, ref.ExRate FROM SeaImport AS job INNER JOIN  XXParty AS cust ON job.CustomerId = cust.PartyId INNER JOIN
                      SeaImportRef AS ref ON ref.RefNo = job.RefNo
WHERE (job.RefNo = '{0}') AND (job.JobNo = '{1}')", refNo, jobNo);
            }
            else
            {
                sql_Job = string.Format(@"SELECT job.CustomerId, cust.TermId, ref.ExRate FROM SeaExport AS job INNER JOIN  XXParty AS cust ON job.CustomerId = cust.PartyId 
INNER JOIN SeaExportRef AS ref ON ref.RefNo = job.RefNo
WHERE (job.RefNo = '{0}') AND (job.JobNo = '{1}')", refNo, jobNo);
            }
            DataTable tab = C2.Manager.ORManager.GetDataSet(sql_Job).Tables[0];
            if (tab.Rows.Count == 1)
            {
                partyTo = SafeValue.SafeString(tab.Rows[0][0], "");
                term = SafeValue.SafeString(tab.Rows[0][1], "");
                exRate = SafeValue.SafeDecimal(tab.Rows[0][2], 1);
            }

            string counterType = "AR-IV";

           XAArInvoice inv = new XAArInvoice();
            string invN =C2Setup.GetNextNo(counterType);
            inv = new XAArInvoice();
            inv.PartyTo = partyTo;
            inv.DocType = "IV";
            inv.DocNo = invN.ToString();
            inv.DocDate = DateTime.Today;
            string[] currentPeriod = EzshipHelper.GetAccPeriod(inv.DocDate);

            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], inv.DocDate.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], inv.DocDate.Month);

            inv.Term =term;
            //
            int dueDay = SafeValue.SafeInt(term.ToUpper().Replace("DAYS", "").Trim(), 0);
            inv.DocDueDate = inv.DocDate.AddDays(dueDay);//SafeValue.SafeDate(dueDt.Text, DateTime.Now);
            inv.Description = "";
            inv.CurrencyId = currency;
            inv.ExRate = SafeValue.SafeDecimal(exRate, 1);
            if (inv.ExRate <= 0)
                inv.ExRate = 1;
            inv.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId);
            if (inv.AcCode == "")
            {
                throw new Exception("Please frist set account code!");
            }
            inv.AcSource = "DB";

            inv.MastType = refType;
            inv.MastRefNo = refNo;
            inv.JobRefNo = jobNo;

            inv.ExportInd = "N";
            inv.UserId = HttpContext.Current.User.Identity.Name;
            inv.EntryDate = DateTime.Now;
            inv.CancelDate = new DateTime(1900, 1, 1);
            inv.CancelInd = "N";

            try
            {
                C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(inv);
                C2Setup.SetNextNo(invN, counterType);

                sql = string.Format(@"SELECT QuoteLineNo, ChgCode, Currency, Price, Unit, MinAmt, Rmk, Qty, Amt, gsttype, gst FROM SeaQuoteDet1 Where QuoteId='{0}' order by QuoteLineNo", quoteId);
                tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                int index = 1;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    try
                    {
                        string chgCode = SafeValue.SafeString(tab.Rows[i]["ChgCode"],"").Trim();
                        if (chgCode.Length == 0) continue;

                        string chgDes1 = " ";
                        decimal qty = SafeValue.SafeDecimal(tab.Rows[i]["Qty"], 1);
                        decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"], 0);
                        string unit = tab.Rows[i]["Unit"].ToString();
                        string currencyDes = tab.Rows[i]["Currency"].ToString();
                        decimal gst = SafeValue.SafeDecimal(tab.Rows[i]["Gst"], 0);
                        string gstType = tab.Rows[i]["GstType"].ToString();

                        XAArInvoiceDet det = new XAArInvoiceDet();
                        string impExpInd = "Import";
                        if (refType == "SE")
                            impExpInd = "Export";
                        string sql_chgCode = string.Format("SELECT ArCode, ChgcodeDes,GstTypeId, GstP FROM XXChgCode WHERE (ImpExpInd = '{1}' or ImpExpInd='Full') AND (ChgcodeId = '{0}')", chgCode, impExpInd);
                       // DataTable 
                        DataTable tab_chgCode = C2.Manager.ORManager.GetDataSet(sql_chgCode).Tables[0];
                        if (tab_chgCode.Rows.Count == 1)
                        {
                            det.AcCode = SafeValue.SafeString(tab_chgCode.Rows[0]["ArCode"], "5001");
                            chgDes1 = SafeValue.SafeString(tab_chgCode.Rows[0]["ChgcodeDes"], " ");
                            det.Gst = SafeValue.SafeDecimal(tab_chgCode.Rows[0]["GstP"], 0);
                            det.GstType = SafeValue.SafeString(tab_chgCode.Rows[0]["GstTypeId"], "Z");
                        }
                        else
                        {
                            det.Gst = gst;
                            det.GstType = gstType;
                            string sql_acCode = "select AcCode from XXGstAcount where GstSrc='AR'";
                            det.AcCode =SafeValue.SafeString( C2.Manager.ORManager.ExecuteScalar(sql_acCode));
                        }
                        det.AcSource = "CR";
                        det.ChgCode = chgCode;
                        det.ChgDes1 = chgDes1;
                        det.ChgDes2 = "";
                        det.ChgDes3 = "";
                        det.Currency = currencyDes;
                        if (currency == currencyDes)
                            det.ExRate = 1;
                        else
                            det.ExRate = exRate;

                        det.Price = price;
                        det.Qty = qty;
                        det.Unit = unit;

                        decimal amt = SafeValue.ChinaRound(det.Qty * det.Price, 2);
                        decimal gstAmt = SafeValue.ChinaRound(amt * det.Gst, 2);
                        decimal docAmt = amt + gstAmt;
                        decimal locAmt = SafeValue.ChinaRound(docAmt * det.ExRate, 2);
                        det.GstAmt = gstAmt;
                        det.DocAmt = docAmt;
                        det.LocAmt = locAmt;

                        det.DocId = inv.SequenceId;
                        det.DocLineNo = index;
                        det.DocNo = invN;
                        det.DocType = "IV";
                        det.MastType = refType;
                        det.MastRefNo = refNo;
                        det.JobRefNo = jobNo;
                        C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(det);
                        index++;
                    }
                    catch { }
                    
                }
                UpdateMaster(inv.SequenceId);
                e.Result = inv.DocNo;
            }
            catch
            {
                e.Result = "Fail";
            }
        }
    }
    #region invoice sql
    private void UpdateMaster(int docId)
    {
        string sql = "select ExRate from XAArInvoice where SequenceId ='" + docId + "'";
        decimal exRate = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 1);
        //decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

        decimal docAmt = 0;
        decimal locAmt = 0;
        sql = string.Format("select AcSource,LocAmt from XAArInvoiceDet where DocId='{0}'", docId);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            docAmt += SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
            locAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0) * exRate, 2);
        }

        sql = string.Format("Update XAArInvoice set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt, docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    #endregion

}

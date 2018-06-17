using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using C2;
using Wilson.ORMapper;

public partial class edi_bill : BasePage
{
	public string site = "SqlConnectString1";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-7);
            this.txt_end.Date = DateTime.Today;
        }
    }
    #region search
    protected void btn_search_Click(object sender, EventArgs e)
    {
	
        string billNo = this.txt_ExpRefNo.Text.Trim();
        string date1 = this.txt_from.Date.ToString("yyyy-MM-dd");
        string date2 = this.txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");

        string sql = @"SELECT sequenceid, DocNo, DocDate, DocType, CurrencyId,ExRate,DocAmt,LocAmt,BalanceAmt, MastRefNo, JobRefNo, MastType, UserId FROM xaarinvoice WHERE ";
        sql += string.Format(" exportind='Y' and cancelInd='N' and PartyTo='5002' and DocDate>='{0}' and DocDate<='{1}'", date1,date2);
		
        DataTable master = Helper.Sql.List(sql,site);
        this.grd.DataSource = master;
        this.grd.DataBind();

    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    #endregion
    protected void grd_Det_BeforePerformDataSelect(object sender, EventArgs e)
    {
	string site = Request.QueryString["site"] ?? "PNS";

    }

    protected void grd_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
 
        string trxNo = e.Parameters;
        if (trxNo.Length > 2 && trxNo.Substring(0, 2) == "re")
        {
            string sn = TransferApMast(trxNo.Substring(2));
            e.Result = sn;
        }
        else
        {
            string userName = HttpContext.Current.User.Identity.Name;
            string sql = string.Format(@"select InvoiceNo,InvoiceDate FROM ivcr1 WHERE TrxNo='{0}'", trxNo);
            DataTable tab = Helper.Sql.List(sql, site);
            if (tab.Rows.Count == 1)
            {
                DateTime billDate = SafeValue.SafeDate(tab.Rows[0]["InvoiceDate"], new DateTime(1900, 1, 1));
                string[] acPeriod = GetCurrrentPeriod(billDate);
                if (acPeriod[0].Length == 0)
                {
                    e.Result = "This Period already Closed";
                    return;
                }

                string billNo = SafeValue.SafeString(tab.Rows[0]["InvoiceNo"]);
        string sql2 = string.Format("select ApCode from SysfrtChgCode where ChgCode='LocalId' and SiteCode='"+site+"'");
        string partyTo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql2), "");

                string sql1 = string.Format("select DocNo from XaApPayable where PartyTo='"+partyTo+"' and SupplierBillNo='{0}'", billNo);
                string docNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql1));
                if (docNo.Length > 0)
                {
                    e.Result = "Already Download, Doc No is " + docNo;
                    return;
                }
                string sn = TransferApMast(trxNo);
                e.Result = sn;
            }
        }
    }
   

    private string TransferApMast(string trxNo)
    {
        string sql = string.Format(@"select * FROM xaarinvoice WHERE Sequenceid='{0}'",trxNo);
        string s = "";
        DataTable tab = Helper.Sql.List(sql, site);

        string partyTo = "5043";

        #region fields
        if (tab.Rows.Count == 1)
        {
            DataRow row = tab.Rows[0];
            OPathQuery query = new OPathQuery(typeof(C2.XAApPayable), "PartyTo='"+partyTo+"' and SupplierBillNo='" + SafeValue.SafeString(tab.Rows[0]["InvoiceNo"]) + "'");
            C2.XAApPayable inv = C2.Manager.ORManager.GetObject(query) as C2.XAApPayable;
            bool isNew = false;
			string invN = "";
            if (inv == null)
            {
                inv = new C2.XAApPayable();
                isNew = true;

				inv.SupplierBillNo = SafeValue.SafeString(tab.Rows[0]["InvoiceNo"]);
				inv.SupplierBillDate = SafeValue.SafeDate(row["InvoiceDate"], DateTime.Today);
				inv.DocDate = inv.SupplierBillDate;
				
				invN = C2Setup.GetNextNo(inv.DocType, "AP-PAYABLE", inv.DocDate);
				inv.DocNo = invN.ToString();
            }
			else
			{
				inv.SupplierBillNo = SafeValue.SafeString(tab.Rows[0]["InvoiceNo"]);
				inv.SupplierBillDate = SafeValue.SafeDate(row["InvoiceDate"], DateTime.Today);
				inv.DocDate = inv.SupplierBillDate;
			}
            inv.PartyTo = partyTo;
            inv.MastType = "";
            string docType = SafeValue.SafeString(row["DocType"], "");
            if (docType == "CN")
                docType = "SC";
            else if (docType == "DN")
                docType = "SD";
            else
                docType = "PL";
            inv.DocType = docType;
            inv.Term = "CASH"; //SafeValue.SafeString(row["TermCode"], "");
            inv.Description = "";
            inv.CurrencyId = SafeValue.SafeString(row["CurrencyId"], "");
            inv.ExRate = SafeValue.SafeDecimal(row["ExRate"], 1);
            if (inv.ExRate <= 0)
                inv.ExRate = 1;
            inv.AcCode = "3000"; //AP Code SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select VendorAccId from XXVendor where VendorId ='" + inv.PartyTo + "'"), "");
            if (docType == "SC")
                inv.AcSource = "DB";
            else
                inv.AcSource = "CR";

            inv.ExportInd = "N";
            inv.UserId = HttpContext.Current.User.Identity.Name;
            inv.EntryDate = DateTime.Now;
            inv.CancelDate = new DateTime(1900, 1, 1);
            inv.CancelInd = "N";

            inv.ChqNo = "";
            inv.ChqDate = new DateTime(1900, 1, 1);

            inv.Eta = new DateTime(1900, 1, 1);
            string[] currentPeriod = GetCurrrentPeriod(inv.DocDate);

            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], inv.DocDate.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], inv.DocDate.Month);
            string custRefNo = "";//SafeValue.SafeString(row["jobNo"]);
            string mastRefNo = "";
            string mastType = "";
            inv.MastRefNo = mastRefNo;
            inv.JobRefNo = "";
            inv.MastType = row["MastType"].ToString();
            inv.Description = custRefNo;
            inv.DocAmt = SafeValue.SafeDecimal(row["DocAmt"], 0);
            inv.LocAmt = SafeValue.SafeDecimal(row["LocAmt"], 0);
            inv.BalanceAmt = inv.DocAmt;
            inv.OtherPartyName = "";

            try
            {
                if (isNew)
                {
					C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
					C2.Manager.ORManager.PersistChanges(inv);
					C2Setup.SetNextNo(inv.DocType,"AP-PAYABLE", invN,inv.DocDate);
                }
                else
                {
                    C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Updated);
                    C2.Manager.ORManager.PersistChanges(inv);
                    string sql2x = string.Format("delete from XaApPayableDet where DocId=" + inv.SequenceId);
                    C2.Manager.ORManager.ExecuteCommand(sql2x);
                }
                s = inv.DocNo;
                TransferApDet(trxNo, inv.SequenceId, inv.DocNo, inv.DocType);
            }
            catch
            {
            }
        }
        #endregion

        return s;

    }

    private void TransferApDet(string trxNo, int mastId, string docNo, string docType)
    { 
        string sql = string.Format("select d.*, c.ImpExpInd as EdiCode from xaarinvoicedet d, xxchgcode c where DocNo='{0}' and c.chgcodeid=d.chgcode ", docNo);
        DataTable tab = Helper.Sql.List(sql,site);
        #region fields
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            DataRow row = tab.Rows[i];
            C2.XAApPayableDet plDet = new C2.XAApPayableDet();
            plDet.DocId = mastId;
            plDet.DocLineNo = i+1; //SafeValue.SafeInt(row["LineItemNo"], 1);
            plDet.ChgCode = "MISC";
            plDet.ChgDes1 = SafeValue.SafeString(row["Description"]); ;
            plDet.ChgDes2 = "";
            plDet.ChgDes3 = "";
            plDet.ChgDes4 = "";

            plDet.AcCode = SafeValue.SafeString(row["EdiCode"], "5090");
            plDet.AcSource = "DB";
            if (docType == "SC")
                plDet.AcSource = "CR";
            plDet.Currency = SafeValue.SafeString(row["CurrencyId"]);
            plDet.ExRate = SafeValue.SafeDecimal(row["ExRate"], 0);
            plDet.DocAmt = SafeValue.SafeDecimal(row["DocAmt"], 0);
            plDet.DocNo = docNo;
            plDet.DocType = docType;
            plDet.Gst = 0;
            plDet.GstAmt = SafeValue.SafeDecimal(row["GstAmt"], 0);

            string gstType = SafeValue.SafeString(row["GstType"]);
            if (gstType.Length > 0)
            {
                gstType = gstType.Substring(0, 1);
            }
            plDet.GstType = gstType;
            plDet.LocAmt = SafeValue.SafeDecimal(row["LocAmt"], 0);
            plDet.LineLocAmt = SafeValue.SafeDecimal(row["LineLocAmt"], 0);
            plDet.Price = SafeValue.SafeDecimal(row["Price"], 0);
            plDet.Qty = SafeValue.SafeDecimal(row["Qty"], 0);
            plDet.Unit = SafeValue.SafeString(row["Unit"]);
            C2.Manager.ORManager.StartTracking(plDet, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(plDet);
        }
        #endregion
    }

    private string GetAcCodeByChgcode(string chgCode, string site)
    {
        string sql = string.Format("select ApCode from SysfrtChgCode where SiteCode='"+site+"' and ChgCode='{0}'", chgCode);
        string apCode=SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
        if (apCode.Length == 0)
        {
            sql = string.Format("select ApCode from SysfrtChgCode where ChgCode='SysFrtApCode'");
            apCode = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql),"6004");
        }
        return apCode;
    }
    private string[] GetCurrrentPeriod(DateTime d)
    {
        string acPeriod = "";
        string acYear = "";

        string sql = "SELECT Year, Period,CloseInd FROM XXAccPeriod WHERE StartDate<='" + d.ToString("yyyy-MM-dd") + "' and EndDate>='" + d.ToString("yyyy-MM-dd") + "'";
        DataTable dt = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            if (SafeValue.SafeString(dt.Rows[0]["CloseInd"], "Y") == "N")
            {
                acYear = dt.Rows[0][0].ToString();
                acPeriod = dt.Rows[0][1].ToString();
            }
        }

        string[] period = { acPeriod, acYear };
        return period;
    }


}

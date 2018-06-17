using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;


    public class A
    {
	
public static void Callback(string docno, string doctype)
	{
		string urlTemp = "";
		if(doctype == "IV" || doctype == "DN")
		{
			urlTemp = "/PagesAccount/EditPage/ArInvoiceEdit.aspx?no={0}&type={1}";
		}
		if(doctype == "CN")
		{
			urlTemp = "/PagesAccount/EditPage/ArCnEdit.aspx?no={0}&type={1}";
		}
		if(doctype == "RE")
		{
			urlTemp = "/PagesAccount/EditPage/ArReceiptEdit.aspx?no={0}&type={1}";
		}
		if(doctype == "PL" || doctype == "SD" || doctype == "SC" )
		{
			urlTemp = "/PagesAccount/EditPage/ApPayableEdit.aspx?no={0}&type={1}";
		}
		if(doctype == "VO" )
		{
			urlTemp = "/PagesAccount/EditPage/ApVoucherEdit.aspx?no={0}&type={1}";
		}
		if(doctype == "PS")
		{
			urlTemp = "/PagesAccount/EditPage/ApPaymentEdit.aspx?no={0}&type={1}";
		}
		if(doctype == "GE")
		{
			urlTemp = "/PagesAccount/EditPage/GlEntryEdit_Ge.aspx?no={0}&type={1}";
		}
		string url = string.Format(urlTemp,docno,doctype);
		DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(url);

	}

	public static string HomeCurrency()
	{
	    return "SGD"; //System.Configuration.ConfigurationManager.AppSettings["GainLoseAcCode"];
	}
	public static string AccountBankFee()
	{
	    return System.Configuration.ConfigurationManager.AppSettings["BankFee"];
	}
	public static string AccountGainLoss()
	{
	    return System.Configuration.ConfigurationManager.AppSettings["GainLoseAcCode"];
	}
	
        public static string ArApCode(string curr, string arap)
        {
            string ret = "";	
			ret = 	D.Text("select top 1 code from xxchartacc where AcCurrency='SGD' And AcSubPl='"+arap+"'");
			if(ret=="")
					ret = D.Text("select top 1 code from xxchartacc where AcCurrency='SGD' And AcSubPl='"+arap+"'");
				return ret;
		}

		public static string PayStatus(decimal doc, decimal bal)
		{
			string ret = "<span class=InfoCancel>Not Paid</span>";
			if(bal == 0 && doc > 0)
				ret = "<span class=InfoConfirm>Fully Paid</span>";
			if(doc >0 && bal < doc && bal > 0)
				ret = "<span class=InfoPending>Partially Paid</span>";
			if (doc==0 && bal == 0)
				ret = "";

			return ret;			

		}
		
		public static string ShowStatus(string docno, string doctype)
		{
			string ret = "";
			string tab = "";
			string fld = "";
			if(doctype == "IV" || doctype == "CN" || doctype == "DN")
			{
				tab = "xaarinvoice";
				fld = "ExportInd";
			}
			if(doctype == "RE")
			{
				tab = "xaarreceipt";
				fld = "ExportInd";
			}
			if(doctype == "PL" || doctype == "SC" || doctype == "SD" || doctype == "VO")
			{
				tab = "xaappayable";
				fld = "ExportInd";
			}
			if(doctype == "PS")
			{
				tab = "xaappayment";
				fld = "ExportInd";
			}
			if(doctype == "GE")
			{
				tab = "xajournalentry";
				fld = "PostInd";
			}
			
			if(fld=="")
				return "";
			DataTable dt = D.List(string.Format("select top 1 {3},CancelInd from {0} where docno='{1}' and doctype='{2}'",tab,docno,doctype,fld));
			if(dt.Rows.Count == 1)
			{
				string cancel = S.Text(dt.Rows[0]["CancelInd"]);
				string export = S.Text(dt.Rows[0][fld]);
				if(cancel == "Y")
				{
					ret = "<span class=InfoCancel>Cancelled</span>";
				} else
				{
					if(export != "Y")
					{
						ret = "<span class=InfoPending>Not Posted</span>";
					} 
					else
					{
						ret = "<span class=InfoConfirm>Posted</span>";
						if(doctype=="RE" || doctype=="PS" || doctype=="VO")	
						{
							string recon = 	D.Text(string.Format("select top 1 Status1 from {0} where docno='{1}' and doctype='{2}'","xaglentrydet",docno,doctype));
							if(recon=="Y")
								ret += "<span class=InfoConfirm>Reconciled</span>";
							else
								ret += "<span class=InfoPending>Not Reconciled</span>";
						}
					}
				
				}
			}
			return ret;
		}
		
		public static decimal DrCr(object code)
		{
			decimal drcr = 1;
			string dbcr = D.Text(string.Format("select top 1 acdorc from xxchartacc where code='{0}'",code));
			if(dbcr=="CR")
				drcr = -1;
			return drcr;
		}
		
		public static string ShowAccount(object code)
		{
			string ret = "";
			string desc = D.Text(string.Format("select top 1 acdesc from xxchartacc where code='{0}'",code));
			
			ret = string.Format("<span class=InfoText>{0}</span> {1}",code,desc);
 
			return ret;
		}
		public static string ShowAccount2(object code)
		{
			string ret = "";
			string desc = D.Text(string.Format("select top 1 acdesc from xxchartacc where code='{0}'",code));
			
			ret = string.Format("<span class=InfoText>{0}</span><br>{1}",code,desc);
 
			return ret;
		}

		public static string ShowAccountSource(string code, string src)
		{
			string ret = "";
			string desc = D.Text(string.Format("select top 1 acdesc from xxchartacc where code='{0}'",code));
			
			ret = string.Format("<span class=InfoText>{0}</span><span class=Info{2}>{2}</span> {1}",code,desc,src);
 
			return ret;
		}
 

		
        public static decimal TsRate(string curr)
        {
            decimal ret = 0;	
			ret = Helper.Safe.SafeDecimal(	Helper.Sql.One("select top 1 ExRate2 from CurrencyRate where FromCurrencyId='"+curr+"' Order By ExRateDate desc") );
			return ret;
		}
        public static decimal AccRate(string curr)
        {
            decimal ret = 0;	
			ret = Helper.Safe.SafeDecimal(	Helper.Sql.One("select top 1 ExRate1 from CurrencyRate where FromCurrencyId='"+curr+"' Order By ExRateDate desc") );
			return ret;
		}

        public static string ArTerm(string party, string job)
        {
            string ret = "";	
			ret = 	Helper.Sql.Text("select top 1 TermId from xxparty where partyid='"+party+"'");
			if(ret=="")
					ret = "CASH"; 
				return ret;
		}

        public static string PartyCurrency(string party)
        {
            string ret = "";	
			ret = 	Helper.Sql.Text("select top 1 CarrierCode from xxparty where partyid='"+party+"'");
			if(ret=="")
					ret = "SGD"; 
			return ret;
		}

        public static string ValidCurrency(string curr)
        {
            string ret = "";	
			ret = 	Helper.Sql.Text("select top 1 currencyId from xxcurrency where currencyId='"+curr+"'");
			return ret;
		}
	
		public static string Account(object o)
	{

		return D.Text(string.Format(@"select top 1 AcDesc from XxChartAcc where Code='{0}'", o));
	}

		
    }

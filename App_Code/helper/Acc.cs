using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Helper
{
    public class Acc
    {
        public static string ArApCode(string curr, string arap)
        {
            string ret = "";	
			ret = 	D.Text("select top 1 code from xxchartacc where AcCurrency='SGD' And AcSubPl='"+arap+"'");
			if(ret=="")
					ret = D.Text("select top 1 code from xxchartacc where AcCurrency='SGD' And AcSubPl='"+arap+"'");
				return ret;
		}

		public static string ShowStatus(string docno, string doctype)
		{
			string ret = "";
			string tab = "";
			if(doctype == "IV" || doctype == "CN" || doctype == "DN")
			{
				tab = "xaarinvoice";
			}
			if(doctype == "RE")
			{
				tab = "xaarreceipt";
			}
			if(doctype == "PL" || doctype == "SC" || doctype == "SD")
			{
				tab = "xaappayable";
			}
			if(doctype == "PS")
			{
				tab = "xaappayment";
			}
			DataTable dt = D.List(string.Format("select top 1 ExportInd,CancelInd from {0} where docno='{1}' and doctype='{2}'",tab,docno,doctype));
			if(dt.Rows.Count == 1)
			{
				string cancel = S.Text(dt.Rows[0]["CancelInd"]);
				string export = S.Text(dt.Rows[0]["ExportInd"]);
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
		
		public static string ShowAccount(string accode)
		{
			return "";
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
	
		
    }
}
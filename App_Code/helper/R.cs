using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public static class R
{
	public static string LetterHead(string title)
	{
		string temp = @"
		<table>
			<tr class=onlyprint>
				<td width=65% valign=top><img src=/custom/logo-doc.jpg  width=50% ></td>
				<td>
				<div class=csscompany>Collin’s Movers Pte. Ltd.</div>
				<div class=cssaddress>22 Jurong Port Road, Tower A #04-01, Singapore 619114</div>
				<div class=cssaddress>Tel: (65) 6873 9595 Fax: (65) 6774 4156</div>
				<div class=cssaddress>Email: info@collinsmovers.com.sg</div>
				<div class=cssaddress>Billing: account@collinsmovers.com.sg</div>
				<div class=cssaddress>Co Regn No.: 200208650G GST Regn No.: 20-0208650-G</div>
				</td>
			</tr>
			<tr>
				<td colspan=2 align=center class=csstitle>{0}</td>
			</tr>
		</table>
		";
		return string.Format(temp, title);
	}



    public static string UrlPath(string url)
    {
		return url.Split(new char[] {'?'})[0];
    }   

    public static string UrlFile(string url)
    {
		string[] part = url.Split(new char[] {'?'})[0].Split(new char[] {'/'}); 
		return part[part.Length-1];
    }   
	
	public static decimal Value(object o)
	{
		return Helper.Safe.SafeDecimal(o);
	}
	
	public static string Amount(object o)
	{
		return string.Format("{0:#,##0.00}",o);
	}

	public static string AmountZ(object o)
	{
		string _r = string.Format("{0:#,##0.00}",o);
		if(_r=="0.00")
			_r = "";
		return _r;
	}

	public static string CountZ(object o)
	{
		string _r = string.Format("{0:0}",o);
		if(_r=="0")
			_r = "";
		return _r;
	}	
	
	public static string Num2(object o)
	{
		return string.Format("{0:#,##0.00}",o);
	}

	public static string Num3(object o)
	{
		return string.Format("{0:#,##0.000}",o);
	}



	public static string ExRate(object o)
	{
		return string.Format("{0:#,##0.0000}",o);
	}

	public static string Date3(object o)
	{
		return string.Format("{0:dd/MMM/yyyy}",o);
	}
	public static string Date(object o)
	{
		return string.Format("{0:dd/MM/yyyy}",o);
	}
	public static string Time(object o)
	{
		return string.Format("{0:dd/MM/yyyy HH:mm}",o);
	}

	public static string Text(object o)
	{
		return string.Format("{0}",o).Replace("\r\n","<BR>").Replace("\n","<BR>");
	}

	public static string Space(int cnt)
	{
		string code = ""; //Helper.Safe.SafeString(o);
		for(int i=0; i<cnt; i++)
			code += "&nbsp;";
		return code;
	}
	
	public static string PortName(object o)
	{
		string code = Helper.Safe.SafeString(o);
		return Helper.Safe.SafeString(Helper.Sql.One("select top 1 name from xxport where code='"+code+"'"));
	}

	public static string AccGroup(object o)
	{
		string code = Helper.Safe.SafeString(o);
		return Helper.Safe.SafeString(Helper.Sql.One("select top 1 name from xxchartgroup where code='"+code+"'"));
	}

	public static string AccCashFlow(object o)
	{
		string code = Helper.Safe.SafeString(o);
		return Helper.Safe.SafeString(Helper.Sql.One("select top 1 name from xxchartgroup where code='"+code+"'"));
	}

	
	public static string Port(object o)
	{
		string code = Helper.Safe.SafeString(o);
		string pn = D.Text("select top 1 name from xxport where code='"+code+"'");
		return 	pn=="" ? code : pn;
	}

	public static string Charge(object o)
	{
		string code = Helper.Safe.SafeString(o);
		return Helper.Safe.SafeString(Helper.Sql.One("select top 1 ChgCodeDes from XXChgCode where ChgcodeId='"+code+"'"));
	}
	
	public static string PartyId(object o)
	{
		string code = Helper.Safe.SafeString(o);
		string id = Helper.Safe.SafeString(Helper.Sql.One("select top 1 partyid from xxparty where name='"+code.Replace("'","'"+"'")+"'"));
		if( id.Trim()=="")
			id = "UNKNOWN";
		return id;
	}
	public static string Account(object o)
	{
		return D.Text(string.Format(@"select top 1 AcDesc from XxChartAcc where Code='{0}'", o));
	}

	public static string MatName(object o)
	{
		string code = S.Text(o).Trim();
		if(code == "")
			return "";
		string _sql = string.Format("select top 1 name from ref_Material where Note1='SPJ' and code='{0}'",code);
		return D.Text(_sql);
	}

	public static string MatLoose(object o, object q)
	{
		string code = S.Text(o).Trim();
		decimal qty = S.Decimal(q);
		if(code == "")
			return "";
		DataTable dt = D.List(string.Format("Select top 1 *  from ref_Material where Note1='SPJ' and code='{0}'",code));
		if(dt.Rows.Count == 0)
			return "";
		if(S.Decimal(dt.Rows[0]["WholeLoose"]) == 0 || S.Decimal(dt.Rows[0]["WholeLoose"]) == 1)
			return "";
		string ret = string.Format(@" {0:0}x{1} = {2:0}x{3} ",q,dt.Rows[0]["Unit"],
		S.Decimal(q) * S.Decimal(dt.Rows[0]["WholeLoose"]),
		dt.Rows[0]["LooseUnit"]);
		return ret;
//		string _sql = string.Format("select top 1 name from ref_Material where Note1='ALL' and code='{0}'",code);
//		return D.Text(_sql);
	}

	public static string MatLooseUnit(object o, object q)
	{
		string code = S.Text(o).Trim();
		decimal qty = S.Decimal(q);
		if(code == "")
			return "";
		DataTable dt = D.List(string.Format("Select top 1 *  from ref_Material where Note1='SPJ' and code='{0}'",code));
		if(dt.Rows.Count == 0)
			return "";
		if(S.Decimal(dt.Rows[0]["WholeLoose"]) == 0 || S.Decimal(dt.Rows[0]["WholeLoose"]) == 1 )
			return "";
		string ret = string.Format(@"( {2:0}x{3} )",q,dt.Rows[0]["Unit"],
		S.Decimal(q) * S.Decimal(dt.Rows[0]["WholeLoose"]),
		dt.Rows[0]["LooseUnit"]);
		return ret;
//		string _sql = string.Format("select top 1 name from ref_Material where Note1='ALL' and code='{0}'",code);
//		return D.Text(_sql);
	}	
	
	public static string MatUnit(object o)
	{
		string code = S.Text(o).Trim();
		if(code == "")
			return "";
		string _sql = string.Format("select top 1 Unit from ref_Material where Note1='SPJ' and code='{0}'",code);
		return D.Text(_sql);
	}

	public static decimal MatBalance(object o, object ow)
	{
		string code = S.Text(o).Trim();
		string owner = S.Text(ow).Trim();
		if(code == "" || owner=="")
			return 0;
		string _sql = string.Format("select sum(CASE DoType WHEN 'IN3' THEN qty ELSE -1 * qty END) from Material where owner='{1}' and code='{0}'",code,owner);
		return D.Dec(_sql);
	}
	public static string Party2(object o)
	{
		string code = Helper.Safe.SafeString(o);
		return Helper.Safe.SafeString(Helper.Sql.One("select top 1 name from xxparty2 where partyid='"+code+"'"));
	}
	public static string Address2(object o)
	{
		string code = Helper.Safe.SafeString(o);
		return Helper.Safe.SafeString(Helper.Sql.One("select top 1 address from xxparty2 where partyid='"+code+"'")).Replace("\r\n","<br>");
	}
	public static string Tel2(object o)
	{
		string code = Helper.Safe.SafeString(o);
		return Helper.Safe.SafeString(Helper.Sql.One("select top 1 tel1 from xxparty2 where partyid='"+code+"'"));
	}
	public static string Fax2(object o)
	{
		string code = Helper.Safe.SafeString(o);
		return Helper.Safe.SafeString(Helper.Sql.One("select top 1 tel1 from xxparty2 where partyid='"+code+"'"));
	}
	public static string Contact2(object o)
	{
		string code = Helper.Safe.SafeString(o);
		return Helper.Safe.SafeString(Helper.Sql.One("select top 1 contact1 from xxparty2 where partyid='"+code+"'")).Replace("\r\n","<br>");
	}

	public static string Party(object o)
	{
		string code = Helper.Safe.SafeString(o);
		return Helper.Safe.SafeString(Helper.Sql.One("select top 1 name from xxparty where partyid='"+code+"'"));
	}
	public static string Address(object o)
	{
		string code = Helper.Safe.SafeString(o);
		return Helper.Safe.SafeString(Helper.Sql.One("select top 1 address from xxparty where partyid='"+code+"'")).Replace("\r\n","<br>");
	}
	public static string AddressFull(object o)
	{
		string code = Helper.Safe.SafeString(o);
		return Helper.Safe.SafeString(Helper.Sql.One("select top 1 address from xxparty where partyid='"+code+"'")).Replace("\r\n","<br>");
	}

	public static string SalesCode(string code)
	{
		return D.Text(string.Format("Select top 1 Short from xxsalesman where code='{0}'", code));
	}

	public static string CompanyName()
	{
	    return System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
	}

	public static string CompanyLogo()
	{
	    return System.Configuration.ConfigurationManager.AppSettings["CompanyLogoWeb"];
	}

	public static string CompanyAddress()
	{
		return string.Format("{0}<br>{1}<br>{2}<br>{3}",
	    System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"],
	    System.Configuration.ConfigurationManager.AppSettings["CompanyAddress2"],
	    System.Configuration.ConfigurationManager.AppSettings["CompanyAddress3"],System.Configuration.ConfigurationManager.AppSettings["CompanyAddress4"]);
	}

	public static string CompanyAddressOnly()
	{
		return string.Format("{0}<br>{1}<br>{2}",
	    System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"],"","");
	}

	public static string User()
	{
	    return HttpContext.Current.User.Identity.Name;
	}

	public static string Role()
	{
	    return HttpContext.Current.User.Identity.Name;
	}


	public static string Today()
	{
		return string.Format("{0:dd/MM/yyyy}",DateTime.Today);
	}

	public static string Now()
	{
		return string.Format("{0:dd/MM/yyyy HH:mm}",DateTime.Now);
	}

	public static string Transport(object tt, object tr)
	{
		 
		string tpt = Helper.Safe.SafeString(tt); 
		string trans = (tpt == "Send Direct") ? tpt : string.Format("{0} ({1})",tt,tr);
		return trans;
	}
    public static string SubString(object o)
    {
        string str = "";
        string s = Helper.Safe.SafeString(o);
        string s1 = "";
        for (int i = 0; i <s.Length;i++ )
        {
            if (s.Length > 5&&i==0)
            {
                s1 = s.Substring(0, 5);
                str += s1 + "<BR>";
            }
            else if (s.Length <= 5)
            {
                str = s;
            }
            else if(s.Length-(5*(i+1))>0)
            {
                s1 = s.Substring(5*i+1, 5);
                str += s1 + "<BR>";
            }

        }
        return str;
    }
	
}
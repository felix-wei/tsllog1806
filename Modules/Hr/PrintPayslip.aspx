<%@ page language="C#" autoeventwireup="true"  %>

<script runat="server">
    string CompanyName = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
    string ReportName = "PAYSLIP ADVICE";

 </script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-2.1.1.js"></script>
    <style>
    body {font-size:14px;font-family:Arial;}
    td {font-size:14px; padding:4px; }  
    th {text-align:left;}
    .head td {background:#eeeeee;font-weight:bold;text-align:left}  
    .hide {display:none;}
    .right {text-align:right;}
    .pt table {border-collapse:collapse;}
    .ps2 {margin:6px;margin-left:50px;}
    .ps {width:960px;}
    .ps3 {width:500px;}
    .ps3 {width:500px;}

    .w20 {width:20px;}
    .w40 {width:40px;}
    .w60 {width:60px;}
    .w80 {width:80px;}
    .w100 {width:100px;}
    .w120 {width:120px;}
    .w160 {width:160px;}
    .w200 {width:200px;}
    .w240 {width:240px;}
    .w300 {width:300px;}

    .w2p {width:50%;}
    .w3p {width:35%;}
    .w4p {width:25%;}
    .w5p {width:20%;}
    .w6p {width:18%;}
    .al {text-align:left;}
    .ar {text-align:right;}
    
    </style>
</head>
<body style="background:white;">
    <form id="form1" runat="server">

    <%
        string pay_ids = Request["id"] ?? "0";
		string[] pay_ida = pay_ids.Split(new char[] {','});
		
		for(int l=0; l<pay_ida.Length; l++) 
{
		
		string pay_id = pay_ida[l];
		
		if(pay_id.Trim()=="")
			continue;
		
         %>

    <div>
    <div class="pt" >

<table class="ps">
<tr>
<td>
<table cellspacing=4>
<tr>
<td width=80><img src=/custom/logo.jpg width=100></td>
<td width=400>
<b><%= R.CompanyName() %></b><br>
<%= R.CompanyAddress() %>
</td>

</tr>
</table>
</td>
</tr>
<tr>

<td class="w2p ar" style="border:solid 2px black;text-align:center">
<b><%= ReportName%></b>
</td>
</tr>
</table>
<br />
<%
    DataTable dt_pay = D.List("select * from hr_payroll where id=" + pay_id);
	DataTable dt_line = D.List("select * from hr_payrolldet where payrollid=" + pay_id);
	DataTable dt_emp = D.List(string.Format("select * from hr_person where id='{0}'",dt_pay.Rows[0]["Person"]));

       DataRow dr_emp=dt_emp.Rows[0];
       DataRow dr_pay=dt_pay.Rows[0];
	   DateTime date_pay = S.Date(dr_emp["Date3"]);
	   DateTime date_from = S.Date(dr_pay["FromDate"]);
   
    if (dt_emp.Rows.Count > 0)
    {
        %>

        <table class=ps>
        <tr>
        <th  class=w120>Employee Name</th>
        <td class=w200><%= dr_emp["Name"] %></td>
        <th class=w120></th>
        <td class=w200></td>
        <th class=w120>Salary Month</th>
        <td class=w200><%= string.Format("{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",dr_pay["FromDate"],dr_pay["ToDate"]) %></td>
        </tr>
        <tr>
        <th  class=w120>Position</th>
        <td class=w200><%= dr_emp["HrRole"] %></td>
        <th class=w120></th>
        <td class=w200></td>
        <th class=w120>DOB</th>
        <td class=w200><%= string.Format("{0:dd/MM/yyyy}",dr_emp["BirthDay"]) %></td>
        </tr>
        <tr>
        <th  class=w120>Department</th>
        <td class=w200><%= dr_emp["Department"] %></td>
        <th class=w120></th>
        <td class=w200></td>
        <th class=w120>IC / FIN No</th>
        <td class=w200><%= S.Text(dr_emp["PassType"]) == "NRIC" ? S.Text(dr_emp["IcNo"]) : S.Text(dr_emp["IcNo"]) %></td>
        </tr>
        <tr>
        <th class=w120>Join Date</th>
        <td class=w200><%= string.Format("{0:dd/MM/yyyy}",dr_emp["ProbationFromDate"]) %></td>
        <th class=w120></th>
        <td class=w200></td>
        <th class=w120>Pay Date</th>
        <td class=w200><%= string.Format("{0:dd/MM/yyyy}",(new DateTime(date_from.Year, date_from.Month, date_pay.Day)).AddMonths(1)) %></td>
        </tr>
         
        
        </table>

        <%
    }
	
	decimal total1 = 0;
	decimal total2 = 0;
	decimal unpaid = 0;
	decimal reim = 0;
	decimal cpf1 = 0;
	decimal cpf2 = 0;
	decimal nett = 0;
	
    %>
    <br />

	<table class=ps border=1>
	<tr>
	<td width=400>
	<center><b>Earnings</b></center>
	</td>
	<td width=400>
	<center><b>Deductions</b></center>
	</td>
	</tr>
	<tr>
	<td valign=top >
		<table width=100% padding=4>
		<% 
		DataTable dte = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('AB','AL','AO','AR','AX','AY','AZ') order by ProcessType,Code",pay_id));
		for(int i=0; i<dte.Rows.Count; i++)
		{
		DataRow dre = dte.Rows[i];
		total1 += S.Dec(dre["Amount"]);
		
		if(S.Text(dre["Code"]) ==  "PR25")
			reim += S.Dec(dre["Amount"]);
		
		%>
		<tr>
		<td width=40% ><%= dre["Description"] %></td>
		<td width=45% ><%= dre["Remark"] %></td>
		<td width=15% align=right><%= S.Dec(dre["Amount"]) == 0 ? "0.00" : string.Format("{0:#,##0.00}",dre["Amount"]) %></td>
		</tr>
		<%
		string rem = S.Text(dre["Remark"]).Trim(); 
		if(rem.Length == 123) { 
		%>
		<tr>
		<td colspan=2 style="padding-left:20px;"><i><%= rem.Replace("\r\n","<br>") %></i></td>
		</tr>
		
		<% } %>
		<%}%>
		</table>
	
	</td>
	<td valign=top >
			<table width=100% padding=4>
		<% 
		DataTable dtd;
		string pass = S.Text(dr_emp["PassType"]);
		string race  = S.Text(dr_emp["Race"]);
		if(pass=="NRIC" && race=="CHINESE")
			dtd = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('DD','DU','DZ') and p.Code not in ('PR20','PR21') order by ProcessType,Code",pay_id));
		else if(pass=="NRIC" && race=="MALAY")
			dtd = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('DD','DU','DZ') and p.Code not in ('PR19','PR20') order by ProcessType,Code",pay_id));
		else if(pass=="NRIC" && race=="TAMIL")
			dtd = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('DD','DU','DZ') and p.Code not in ('PR19','PR21') order by ProcessType,Code",pay_id));
		else if(pass=="NRIC" && race=="HINDI")
			dtd = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('DD','DU','DZ') and p.Code not in ('PR19','PR21') order by ProcessType,Code",pay_id));
		else if(pass=="NRIC" && race=="INDIAN")
			dtd = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('DD','DU','DZ') and p.Code not in ('PR19','PR21') order by ProcessType,Code",pay_id));
		else	
			dtd = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('DD','DU','DZ') and p.Code not in ('PR19','PR20','PR21') order by ProcessType,Code",pay_id));
		for(int i=0; i<dtd.Rows.Count; i++)
		{
		DataRow drd = dtd.Rows[i];
		total2 += S.Dec(drd["Amount"]);
		if(S.Text(drd["Code"]) == "PR16" || S.Text(drd["Code"]) == "PR25")
			unpaid += S.Dec(drd["Amount"]);
		%>
		<tr>
		<td width=40% ><%= drd["Description"] %></td>
		<td width=45% ><%= drd["Remark"] %></td>
		<td width=15% align=right><%= S.Dec(drd["Amount"]) == 0 ? "0.00" : string.Format("{0:#,##0.00}",drd["Amount"]) %></td>
		</tr>
		<%
		string rem1 = S.Text(drd["Remark"]).Trim(); 
		if(rem1.Length == 123) { 
		%>
		<tr>
		<td colspan=2 style="padding-left:20px;"><i><%= rem1.Replace("\r\n","<br>") %></i></td>
		</tr>
		
		<% } %>
		<%}%>
		
		<%
		//throw new Exception(S.Text(dr_emp["IsCPF"]));
		decimal rate1 = 0;
		decimal rate2 = 0;
		DateTime tod = S.Date(dr_pay["FromDate"]);
		DateTime dob = S.Date(dr_emp["BirthDay"]);
		string _cpf = S.Text(dr_emp["IsCPF"]);
		DataTable dt_cpf = D.List("select top 1 * from hr_rate where RateType='"+_cpf+"'");
		if(dt_cpf.Rows.Count > 0) {
		DataRow dr_cpf = dt_cpf.Rows[0];
		   
            TimeSpan actualAge = tod.Subtract(dob);
			int age = actualAge.Days / 365;
			
			rate1 = S.Dec(dr_cpf["EmployerRate"]);
			rate2 = S.Dec(dr_cpf["EmployeeRate"]);
			if(age > 55 && age <= 60) {
			 rate1 = S.Dec(dr_cpf["EmployerRate55"]);
			 rate2 = S.Dec(dr_cpf["EmployeeRate55"]);
			}
			if(age > 60 && age <= 65) {
			 rate1 = S.Dec(dr_cpf["EmployerRate60"]);
			 rate2 = S.Dec(dr_cpf["EmployeeRate60"]);
			}
			if(age > 65) {
			 rate1 = S.Dec(dr_cpf["EmployerRate65"]);
			 rate2 = S.Dec(dr_cpf["EmployeeRate65"]);
			}
		}	
				
			if(	dr_emp["Name"].ToString().ToUpper() == "TEO ANG THO1")
			{
				rate1 = S.Dec("0.09");
				rate2 = 0;
			}
			
		
			decimal cpf_base = total1 - unpaid - reim;  //total1-unpaid - reim
			if(cpf_base > 6000)
				cpf_base = 6000;

			if(pass != "NRIC")
				{
				cpf_base = 0;
				rate1 = 0;
				rate2 = 0;
				}
				
				
				
			if(	dr_emp["Name"].ToString().ToUpper() == "CHEW CHAI CHIN 11 ")
			{
				cpf_base = 350;
				rate1 = S.Dec("0.04");
				rate2 = 0;
			}

				
			string ymd = string.Format("{0:yyMMdd}",tod);
			string ym = string.Format("{0:yyMM}",tod);


			if(	dr_emp["Name"].ToString().ToUpper().Trim() == "LU ZHIJIE" && ym == "1712")
			{
				rate1 = S.Dec("0.09");
				rate2 = S.Dec("0.075");
			}

			if(S.Int(ym) > 1704) {
			cpf1 = Math.Round(rate1 * (cpf_base),0);
			decimal cpf1a = Math.Round(rate1 * (cpf_base),2);
			if(cpf1a > cpf1)
				cpf1 = cpf1 + 1;
			cpf2 = Math.Round(rate2 * (cpf_base),0);
			decimal cpf2a = Math.Round(rate2 * (cpf_base),2);
			if(cpf2a < cpf2 )
				cpf2=cpf2-1;
			} else {
			cpf1 = Math.Round(rate1 * (cpf_base),2);
			cpf2 = Math.Round(rate2 * (cpf_base),2);

			}
			
			//throw new Exception(rate1.ToString() + " / " + rate2.ToString() + " / " + cpf_base.ToString() + " / " + cpf1.ToString() );
			
			
			// new way
			decimal new_cpf_add = 0;
			if(cpf_base < 750)
				rate2 = S.Dec("0.1");
			if(cpf_base < 500)
				rate2 = 0;
			if(cpf_base < 50)
				rate2 = 0;
			//if(cpf_base >= 500 && cpf_base < 750)
			//	new_cpf_add = S.Dec("0.6");
			//if(cpf_base >= 50 && cpf_base < 500)
			//	new_cpf_add = S.Dec("0.6");
				
			
			if(	dr_emp["Name"].ToString().ToUpper().Trim() == "LU ZHIJIE" && ym == "1712")
			{
				new_cpf_add = 0;
				rate2 = S.Dec("0.075");
			}
	
				
			decimal new_cpf0 = 0; 
			new_cpf0 = Math.Round(cpf_base * (rate1+rate2), 0);
			if(cpf_base==450)
				new_cpf0 = 77;
			//throw new Exception(new_cpf0.ToString() + "/" + (rate1+rate2).ToString() + cpf_base.ToString());
			decimal new_cpf2 = Math.Round(cpf_base * (rate2), 0);
			//throw new Exception(string.Format("{0}/{1}/{2}",cpf_base, new_cpf0, new_cpf2));
			
			if(new_cpf2 > (cpf_base * rate2))
				new_cpf2 = new_cpf2 - 1;
			decimal new_cpf1 = new_cpf0 - new_cpf2;
			
			cpf1 = new_cpf1 + new_cpf_add;
			cpf2 = new_cpf2 + new_cpf_add;
			
			

			if(	dr_emp["Name"].ToString().ToUpper() == "LAY TSE SHEN 11")
			{
				//cpf_base = 350;
				cpf1 = 89;
				cpf2 = 15;
				//rate1 = S.Dec("0.04");
				//rate2 = 0;
			}

			if(	dr_emp["Name"].ToString().ToUpper() == "TAN CHOON LAN" && string.Format("{0:dd/MM/yyyy}",dr_pay["FromDate"])=="01/11/2017")
			{
				//cpf_base = 350;
				cpf1 = 92;
				cpf2 = 23;
				//rate1 = S.Dec("0.04");
				//rate2 = 0;
			}
			if(	dr_emp["Id"].ToString().ToUpper() == "142" && S.Int(string.Format("{0:yyyyMM}",dr_pay["FromDate"])) < 201706)
			{
				//cpf_base = 350;
				cpf_base = 0;
				cpf1 = 0;
				cpf2 = 0;
				rate1 = 0;
				rate2 = 0;
				//rate1 = S.Dec("0.04");
				//rate2 = 0;
			}
			if(	dr_emp["Id"].ToString().ToUpper() == "142" && S.Int(string.Format("{0:yyyyMM}",dr_pay["FromDate"])) == 201706)
			{
				//cpf_base = 350;
				//cpf_base = 0;
				cpf1 = 14;
				cpf2 = 0;
				rate1 = 0;
				rate2 = 0;
				rate1 = S.Dec("0.04");
				//rate2 = 0;
			}
			if(	dr_emp["Id"].ToString().ToUpper() == "162" && S.Int(string.Format("{0:yyyyMM}",dr_pay["FromDate"])) == 201707)
			{
				//cpf_base = 350;
				//cpf_base = 0;
				cpf1 = 89;
				cpf2 = 15;
				rate1 = S.Dec("0.17");
				rate1 = S.Dec("0.20");
				//rate2 = 0;
			}

			
			rate1 = S.Dec(dr_pay["Cpf1"]);
			cpf1 = S.Dec(dr_pay["Cpf1Amt"]);
			rate2 = S.Dec(dr_pay["Cpf2"]);
			cpf2 = S.Dec(dr_pay["Cpf2Amt"]);
			
			nett = total1 - cpf2 - total2;

		//	D.Exec(string.Format("update hr_payroll set cpf1='{1}', cpf1amt='{2}',cpf2='{3}',cpf2amt='{4}',total1='{5}',total2='{6}',cpf0='{7}' where Id='{0}'",pay_id,
		//	rate1, cpf1, rate2, cpf2, total1, total2, cpf_base));

			
		%>
		
		
		<tr><td>Employee CPF (<%= string.Format("{0:#,##0.00}",rate2  * S.Dec("100")) %>%)</td><td></td><td align=right><%= string.Format("{0:#,##0.00}",cpf2) %></td></tr>
		
		</table>
	</td>
	</tr>
	<tr>
	<td valign=top>
		
		<table width=100% padding=4 >
		<tr><td>Total Gross Earnings</td><td align=right><%= string.Format("{0:#,##0.00}",total1) %></td></tr>
		</table>
	</td>
	<td valign=top>
		<table width=100% padding=4 valign=top>
		<tr><td>Total Deduction</td><td align=right><%= string.Format("{0:#,##0.00}",total2+cpf2) %></td></tr>
		</table>
	</td>
	</tr>
	<tr>
	<td valign=top>
		
		<table width=100% padding=4 >
		<tr><td>Total Gross Salary (CPF)</td><td align=right><%= string.Format("{0:#,##0.00}",cpf_base) %></td></tr>
		</table>
	</td>
	<td valign=top>
		<table width=100% padding=4 valign=top>
		<tr><td> </td><td align=right> </td></tr>
		</table>
	</td>
	</tr>
	<tr>
	<td valign=top>
		
		<table width=100% padding=4 >
		<tr><td>Employer CPF (<%= string.Format("{0:#,##0.00}",rate1 * S.Dec("100")) %>%)</td><td align=right><%= string.Format("{0:#,##0.00}",cpf1) %></td></tr>
		</table>
	</td>
	<td valign=top>
		<table width=100% padding=4 valign=top>
		<tr><td><b><u>Net Pay</u></b></td><td align=right><b><u><%= string.Format("{0:#,##0.00}",nett) %><u></b></td></tr>
		<tr><td><b><u>Total CPF</u></b></td><td align=right><b><u><%= string.Format("{0:#,##0.00}",cpf1+cpf2) %><u></b></td></tr>
		</table>
	</td>
	</tr>
	<tr>
	<td colspan=2><b>Remark</b>
	</td>
	</tr>
	<tr>
	<td colspan=2>
	<%= S.Text(dr_pay["Remark"]).Replace("\r\n","<br>").Replace("\n","<br>") %><br><br>
	</td>
	</tr>
	<%
	DataTable dt_bank = D.List(string.Format("select top 1 * from hr_persondet3 where person='{0}'", dr_emp["Id"]));
	if(dt_bank.Rows.Count > 0)
	{
		DataRow dr_bank = dt_bank.Rows[0];
		%>
			<tr>
	<td colspan=2><b>Salary Credit To (Bank Acount):</b> <%= dr_bank["BankName"]%> : <%= dr_bank["AccNo"]%>
	</td>
	</tr>

			<%
	}
	%>
	
	</table>
	
	
    

<br />

<br><br>
<table width=960 border=0 style='<%= l<pay_ida.Length-1 ?  "page-break-after: always;" : "" %>' >
<tr>
<td  align=center style="width:250px;border:solid 2px black">
<br>
<br>
<br>
<br>
<br>
_____________________________
<br>
Employer Signature
</td>
<td style="width:200px" >
 &nbsp;
</td>
<td  align=center style="width:250px;border:solid 2px black">
<br>
<br>
<br>
<br>
<br>
_____________________________
<br>
Employee Signature
</td>
</tr>
 
</table>


</div>
    </div >
	
	
<% } %>	
	
    </form>
</body>
</html>

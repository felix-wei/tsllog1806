<%@ page language="C#" autoeventwireup="true"  %>

<script runat="server">
    string CompanyName = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
    string ReportName = "JOB SUMMARY";

 </script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-2.1.1.js"></script>
    <style>
    body {font-size:11px;font-family:Arial;}
    td {font-size:11px; padding:4px; }  
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
        string job_no = Request["o"] ?? "";
         %>

    <div>
    <div class="pt">

<table class="ps">
<tr>
<td class="w2p al">
<h2><%= CompanyName%></h2>
</td>
<td class="w2p ar">
<h2><%= ReportName%></h2>
</td>
</tr>
</table>
<br />
<%
    DataTable dt_ref0 = ConnectSql.GetTab("select m.jobtype as ordertype,m.clientrefno as ImpRefNo,IsNull((select top 1 name from xxparty where partyid=m.clientId),'') as CustName, m.vessel as vesselno, m.voyage as voyno, m.etadate as eta, m.etadate as etd, m.jobno as joborderno, m.jobtype, (select top 1 name from xxport where code=m.pol)  as polname,(select top 1 name from xxport where code=m.pod)  as podname from ctm_job m where  jobno='" + job_no + "'");
    DataTable dt_con0 = ConnectSql.GetTab("select c.containerno as contno, c.sealno, 0 as ft20, 0 as ft40, 0 as ft45, c.containertype as fttype from ctm_jobdet1 c where c.jobno='"+job_no+"'");
    decimal exac = 0;
    decimal exts = 0;
    if (dt_ref0.Rows.Count > 0)
    {
        exts = 0; //Helper.Safe.SafeDecimal(dt_ref0.Rows[0]["Value1"]);
        exac = 0; //Helper.Safe.SafeDecimal(dt_ref0.Rows[0]["Value2"]);
        %>

        <table class=ps border=1>
        <tr>
        <th  class=w120>Client</th>
        <td class=w200><%= Helper.Safe.SafeString( dt_ref0.Rows[0]["CustName"]) %></td>
        <th class=w120>Vessel / Voy</th>
        <td class=w200><%= dt_ref0.Rows[0]["VesselNo"] %> / <%= dt_ref0.Rows[0]["VoyNo"] %></td>
        <th class=w120>Ref / Type</th>
        <td class=w200><span style="font-size:16px;font-weight:bold;"><%= dt_ref0.Rows[0]["JobOrderNo"] %></span></td>
        </tr>
        <tr>
        <th  class=w120>Client Ref</th>
        <td class=w200> <%= Helper.Safe.SafeString( dt_ref0.Rows[0]["ImpRefNo"]) %></td>
   <th class=w120 valign=top>ETA / ETD</th>
        <td class=w200 valign=top><%= string.Format("{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",dt_ref0.Rows[0]["Eta"],dt_ref0.Rows[0]["Etd"]) %></td>
             <th class=w120>Job Type</th>
        <td class=w200> <%= dt_ref0.Rows[0]["JobType"] %> / <%= dt_ref0.Rows[0]["OrderType"] %></td>
        </tr>
        <tr>
  <th  class=w120 valign=top>Container</th>
        <td class=w200> 
    	    <% for (int c=0; c< dt_con0.Rows.Count; c++) {%>
				<%= string.Format("{0} / {1} / {2}", dt_con0.Rows[c]["ContNo"],dt_con0.Rows[c]["SealNo"],dt_con0.Rows[c]["FtType"]) %><br>
			<% } %>

		</td>
        <th class=w120>POL / POD</th>
        <td class=w200><%= string.Format("{0} / {1}",dt_ref0.Rows[0]["PolName"],dt_ref0.Rows[0]["PodName"]) %></td>
        <th class=w120>Prepared By</th>
        <td class=w200><%= string.Format("{0}   {1:dd/MM/yyyy HH:mm}", HttpContext.Current.User.Identity.Name, DateTime.Now) %></td>
        </tr>
       
        
        </table>

        <%
    }
    %>
    <br />

    <table class=ps cellpadding=0 cellspacing=0 style="padding:0px;">
    <tr>
    <td valign=top>

    
<%
    DataTable dt_job0 = ConnectSql.GetTab("select c.containerno as ContNo, c.SealNo, 0 as Ft20, 0 as Ft40, 0 as Ft45, c.ContainerType as FtType,  c.Remark from ctm_jobdet1 c where c.JobNo='" + job_no + "'");

    if (dt_job0.Rows.Count > 0)
    {
        %>
<table class="ps3" border=1>
<tr>
<td colspan=7>
<span style="font-size:14px;font-weight:bold">Container Details</span>
</td>
</tr>
<tr class=head>
<td class="w100">
Cont No</td>
<td class="w80">
Seal No
</td>
<td class="w20">
FT20
</td>
<td class="w20">
FT40
</td>
<td class="w20">
FT45
</td>
<td class="w60">
Type
</td>
<td class="w80">
Remark
</td>
 
</tr>
<% 
int _ft20 = 0;
int _ft40 = 0;
int _ft45 = 0;
int ft20 = 0;
int ft40 = 0;
int ft45 = 0;
decimal total_wt = 0;
decimal total_m3 = 0;
int total_qty = 0;

for (int i = 0; i < dt_job0.Rows.Count; i++)
{ 
string _size = S.Text(dt_job0.Rows[i]["FtType"]).Substring(0,2);

_ft20= 0;
_ft40= 0;
_ft45= 0;

if(_size=="20")
  _ft20=1;
if(_size=="40")
  _ft40=1;
if(_size=="45")
  _ft45=1;
  


%>
<tr>
<td>
    <u><b><%= dt_job0.Rows[i]["ContNo"]%></b></u>
</td>
<td>
<%= dt_job0.Rows[i]["SealNo"]%>
</td>
<td>
<%= R.CountZ(_ft20)%>
</td>
<td>
<%= R.CountZ(_ft40)%>
</td>
<td>
<%= R.CountZ(_ft45)%>
</td>
<td>
<%= dt_job0.Rows[i]["FtType"]%>
</td>
<td>
<%= dt_job0.Rows[i]["Remark"]%>
</td>

 
</tr>
    <%
ft20 += _ft20;
ft40 += _ft40;
ft45 += _ft45;

} %>

<tr>
<td colspan=2 class=right>
<b>Total</b>
</td>
<td class=right>
<b><%= R.CountZ(ft20)%></b>
</td>
<td class=right>
<b><%= R.CountZ(ft40)%></b>
</td>
<td class=right>
<b><%= R.CountZ(ft45)%></b>
</td>
 
<td colspan=2></td>
</tr>
</table>
<% } %>


<br />

    
    </td>
    <td valign=top align=right>

<%

    string sql_sum  =  @"select Category, sum(LocAmt1) as LocAmt1, Sum(GstAmt) as GstAmt, Sum(Gst2Amt) as Gst2Amt, Sum(LocAmt2) as LocAmt2 from (
            select '1.Income' as Category, sum( Case DocType When 'CN' Then -1*LocAmt ELSE LocAmt END) as LocAmt1, 0 as LocAmt, 0 as LocAmt2, 0 as GstAmt,0 AS Gst2Amt from xaarinvoice where mastrefno='" + job_no + "' " 
            + " union all " + 
            " select '2.Expense' as Category, sum( Case d.DocType When 'SC' Then 1*l.LocAmt ELSE -1*l.LocAmt END) as LocAmt1, 0 as LocAmt, 0 as LocAmt2, 0 as GstAmt, 0 AS Gst2Amt from xaappayable d, xaappayabledet l where d.SequenceId=l.DocId and l.MastRefNo='" + job_no + "' " 
            + " union all " + 
            "select '2.Expense' as Category, 0 as LocAmt1, 0 as LocAmt, 0 as LocAmt2, sum(-1 * Amount) as GstAmt, 0 as Gst2Amt from psa_bill l where [tariff code]<>'7999' and amount>0 and l.job_no='" + job_no + "' " 
            + " union all " + 
            "select '1.Income' as Category, 0 as LocAmt1, 0 as LocAmt, 0 as LocAmt2, sum(-1 * Amount) as GstAmt, 0 as Gst2Amt from psa_bill l where [tariff code]<>'7999' and amount<0 and l.job_no='" + job_no + "' " 
            + " union all " +
            "select '2.Expense' as Category,0 as LocAmt1, 0 as LocAmt,  0 as LocAmt2, 0 as GstAmt, -1*(select  sum(price) as price from job_cost where  LineType='CL' and JobNo='" + job_no + "' group by JobNo) as Gst2Amt from ctm_jobdet2 l where l.jobno='" + job_no + "' "
            + " union all " +
            "select '2.Expense' as Category, 0 as LocAmt1,0 as LocAmt, -1*(select  sum(price) as price from job_cost where  LineType='DP' and JobNo='" + job_no + "' group by JobNo) as LocAmt2, 0 as GstAmt, 0 as Gst2Amt from ctm_jobdet2 l where l.jobno='" + job_no + "' ) as Tmp group by Category  "
		;

		//throw new Exception(sql_sum);
    DataTable dt_sum0 = ConnectSql.GetTab(sql_sum);
    if (dt_sum0.Rows.Count > 0)
    {
        %>
<table class="ps4" border=1>
<tr>
<td colspan=6 >
<span style="font-size:14px;font-weight:bold">Profit & Loss</span>
 
</td>
</tr>
<tr class=head>
<td class="w100">
Category</td>
<td class="w60">
Client</td>
<td class="w60">
PSA
</td>
<td class="w60">
Claim
</td>
<td class="w60">
Driver
</td>
<td class="w60">
Total
</td>
</tr>
<% 
decimal total_gst = 0;
decimal total_gst2 = 0;
decimal total_local = 0;
decimal total_local1 = 0;
decimal total_local2 = 0;

decimal gst = 0;
decimal gst2 = 0;
decimal local1 = 0;
decimal local2 = 0;

//for (int i = 0; i < dt_sum0.Rows.Count; i++)
for (int i = 0; i < dt_sum0.Rows.Count; i++)
{

local1 = S.Decimal(dt_sum0.Rows[i]["LocAmt1"]);
local2 = S.Decimal(dt_sum0.Rows[i]["LocAmt2"]);
gst = S.Decimal(dt_sum0.Rows[i]["GstAmt"]);
gst2 = S.Decimal(dt_sum0.Rows[i]["Gst2Amt"]);

 %>
<tr>
<td>
    <b><%= dt_sum0.Rows[i]["Category"]%></b>
</td>
<td class=right>
<%= R.AmountZ(local1)%>
</td>
<td class=right>
<%= R.AmountZ(gst)%>
</td>
<td class=right>
<%= R.AmountZ(gst2)%>
</td>
<td class=right>
<%= R.AmountZ(local2)%>
</td>
<td class=right>
<%= R.AmountZ( local1+gst+gst2+local2 )%>
</td>
</tr>
    <%
        
total_gst += gst;  
total_gst2 += gst2;
total_local1 += local1;
total_local2 += local2;
total_local = total_gst+total_gst2 + total_local1 + total_local2;
}
        %>

<tr>
<td colspan=1 class=right>
<b>Total</b>
</td>
<td class=right>
<b><%= R.AmountZ( total_local1)%></b>
</td>
<td class=right>
<b><%=R.AmountZ( total_gst)%></b>
</td>
<td class=right>
<b><%= R.AmountZ( total_gst2)%></b>
</td>
<td class=right>
<b><%= R.AmountZ( total_local2)%></b>
</td>
<td class=right>
<b><%= R.AmountZ(total_local)%></b>
</td>

</tr>
</table>
<% } %>


<br />

    </td>
    </tr>
    </table>



<br />


<%
    DataTable dt_co0 = ConnectSql.GetTab("select d.DocNo,d.DocType,d.DocDate,(CASE d.DocType When 'SC' Then -1*d.LocAmt Else d.LocAmt End) as LocAmt,d.Description,p.Name as PartyName, d.EntryDate from xaarinvoice d, xxparty p where d.partyto=p.partyid and d.mastrefno='" + job_no + "' order by docno");

    if (dt_co0.Rows.Count > 0)
    {
         %>

<table class="ps" border=1>
<tr>
<td colspan=14 >
<span style="font-size:14px;font-weight:bold">Invoices / Credit Notes</span>
 
</td>
</tr>
<tr class=head>
    <td class="w120">Doc No</td>
    <td class="w80">Doc Type</td>
    <td class="w80">Doc Date</td>
    <td class="w200">Customer</td>
    <td class="w200">Description</td>
    <td class="w120">Total</td>
    <td class="w120">Updated</td>
   

</tr>
<% 
decimal total_gst = 0;
decimal total_local = 0;

for (int i = 0; i < dt_co0.Rows.Count; i++)
{
 
    decimal exr = 1;
            
    %>
<tr>
    <td><%= dt_co0.Rows[i]["DocNo"]%></td>
    <td><%= dt_co0.Rows[i]["DocType"]%></td>
    <td class=right><%= string.Format("{0:dd/MM/yy}",dt_co0.Rows[i]["DocDate"])%></td>
    <td><%= dt_co0.Rows[i]["PartyName"]%></td>
    <td><%= dt_co0.Rows[i]["Description"]%></td>
    <td class=right><%= string.Format("{0:0.00}", SafeValue.SafeDecimal(dt_co0.Rows[i]["LocAmt"]))%></td>
    <td class=right><%= string.Format("{0:dd/MM HH:mm}",dt_co0.Rows[i]["EntryDate"])%></td>
</tr>
    <%
        
total_local += SafeValue.SafeDecimal(dt_co0.Rows[i]["LocAmt"]) ;

} %>

<tr>
<td colspan=5 class=right>
<b>Total</b>
</td>
 
<td class=right>
<b><%= string.Format("{0:#,##0.00}", total_local)%></b>
</td>
<td  class=right>
</td>
 
</tr>
</table>
<% } %>


<br />



<%
    dt_co0 = ConnectSql.GetTab("select d.DocNo,d.DocType,d.DocDate,(CASE d.DocType When 'SC' Then -1*l.LocAmt Else l.LocAmt End) as LocAmt,l.ChgDes1,p.Name as PartyName, d.EntryDate, l.JobRefNo from xaappayable d, xaappayabledet l, xxparty p where d.sequenceid=l.docid and d.partyto=p.partyid and l.mastrefno='" + job_no + "' order by docno");

    if (dt_co0.Rows.Count > 0)
    {
         %>

<table class="ps" border=1>
<tr>
<td colspan=14 >
<span style="font-size:14px;font-weight:bold">Supplier Bills</span>
 
</td>
</tr>
<tr class=head>
    <td class="w120">Doc No</td>
    <td class="w120">Doc Type</td>
    <td class="w80">Doc Date</td>
    <td class="w200">Customer</td>
    <td class="w200">Description</td>
    <td class="w120">Cont/HBL No</td>
    <td class="w120">Total</td>
    <td class="w120">Updated</td>
</tr>
<% 
decimal total_gst = 0;
decimal total_local = 0;

for (int i = 0; i < dt_co0.Rows.Count; i++)
{
 
    decimal exr = 1;
            
    %>
<tr>
    <td><%= dt_co0.Rows[i]["DocNo"]%></td>
    <td><%= dt_co0.Rows[i]["DocType"]%></td>
    <td class=right><%= string.Format("{0:dd/MM/yy}",dt_co0.Rows[i]["DocDate"])%></td>
    <td><%= dt_co0.Rows[i]["PartyName"]%></td>
    <td><%= dt_co0.Rows[i]["ChgDes1"]%></td>
    <td><%= dt_co0.Rows[i]["JobRefNo"]%></td>
    <td class=right><%= string.Format("{0:0.00}", SafeValue.SafeDecimal(dt_co0.Rows[i]["LocAmt"]))%></td>
    <td class=right><%= string.Format("{0:dd/MM HH:mm}",dt_co0.Rows[i]["EntryDate"])%></td>
</tr>
    <%
        
total_local += SafeValue.SafeDecimal(dt_co0.Rows[i]["LocAmt"]) ;

} %>

<tr>
<td colspan=6 class=right>
<b>Total</b>
</td>
 
<td class=right>
<b><%= string.Format("{0:#,##0.00}", total_local)%></b>
</td>
<td class=right>
</td>
 
</tr>
</table>
<% } %>


<br />



<%
    dt_co0 = ConnectSql.GetTab(@"select 
		containerno, drivercode,towheadcode, chessiscode,tripcode,
	fromdate,fromtime,todate,totime,
	(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Trip') incentive1,
	(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime') as incentive2,
	(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Standby') as incentive3,
	(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='PSA') as incentive4,
	(select isnull(sum(price),0) from job_cost where TripNo=det2.Id and LineType='DP' ) as driver,
	(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DHC') as charge1,(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WEIGHING') as Charge2,
    (select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='WASHING') as Charge3,
    (select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='REPAIR') as Charge4,
    (select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DETENTION') as Charge5,
    (select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='DEMURRAGE') as Charge6,
    (select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='LIFT_ON_OFF') as Charge7,
    (select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='C_SHIPMENT') as Charge8,
    (select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='EMF') as Charge9,
    (select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='CL' and ChgCode='OTHER') as Charge10,
	(select isnull(sum(price),0) from job_cost where TripNo=det2.Id and LineType='CL' ) as claim
	from ctm_jobdet2 det2 where jobno='" + job_no + "' order by containerno, id");

	
    if (dt_co0.Rows.Count > 0)
    {
    
decimal total_gst = 0;
decimal total_local = 0;
	%>

<table class="ps" border=1>
<tr>
<td colspan=21 >
<span style="font-size:14px;font-weight:bold">Trip / Incentive / Claims</span>
 
</td>
</tr>
<tr class=head>
    <td class="w80">Cont</td>
    <td class="w60">Type</td>
    <td class="w80">Driver</td>
    <td class="w80">Vehicle</td>
    <td class="w120">Time</td>
    <td class="w60">Trip$</td>
    <td class="w60">OT$</td>
    <td class="w60">Standby$</td>
    <td class="w60">PSA$</td>
    <td class="w60" style="background:#CCCCCC"><b>Driver$</b></td>
    <td class="w60">DHC$</td>
    <td class="w60">WEIGH$</td>
    <td class="w60">WASH$</td>
    <td class="w60">REP$</td>
    <td class="w60">DET$</td>
    <td class="w60">DEM$</td>
    <td class="w60">LOLO$</td>
    <td class="w60">C/S$</td>
    <td class="w60">EMF$</td>
    <td class="w60">OTHER$</td>
    <td class="w60" style="background:#CCCCCC"><b>Claim$</b></td>
</tr>
<% 
//decimal trip_total[] =   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

for (int i = 0; i < dt_co0.Rows.Count; i++)
{
 
    decimal exr = 1;
	DataRow dr1 = dt_co0.Rows[i];
            
    %>
<tr >
    <td><%= dr1["ContainerNo"] %></td>
    <td><%= dr1["TripCode"] %></td>
    <td><%= dr1["DriverCode"] %></td>
    <td><%= dr1["TowheadCode"] %></td>
    <td><%= dr1["ChessisCode"] %></td>

    <td ><%= R.AmountZ(dr1["incentive1"])%></td>
    <td ><%= R.AmountZ(dr1["incentive2"])%></td>
    <td ><%= R.AmountZ(dr1["incentive3"])%></td>
    <td ><%= R.AmountZ(dr1["incentive4"])%></td>
    <td style="background:#CCCCCC"><%= R.AmountZ(dr1["Driver"])%></b></td>
    <td ><%= R.AmountZ(dr1["charge1"])%></td>
    <td ><%= R.AmountZ(dr1["charge2"])%></td>
    <td ><%= R.AmountZ(dr1["charge3"])%></td>
    <td ><%= R.AmountZ(dr1["charge4"])%></td>
    <td ><%= R.AmountZ(dr1["charge5"])%></td>
    <td ><%= R.AmountZ(dr1["charge6"])%></td>
    <td ><%= R.AmountZ(dr1["charge7"])%></td>
    <td ><%= R.AmountZ(dr1["charge8"])%></td>
    <td ><%= R.AmountZ(dr1["charge9"])%></td>
    <td ><%= R.AmountZ(dr1["charge10"])%></td>
    <td style="background:#CCCCCC"><%= R.AmountZ(dr1["Claim"])%></b></td>
    
</tr>
 
    <%
        total_gst += S.Decimal(dr1["Driver"]);
        total_local += S.Decimal(dr1["Claim"]);
 
} %>

<tr>
<td colspan=9 class=right>
<b>Total</b>
</td>
    <td style="background:#CCCCCC"><%= R.AmountZ(total_gst)%></b></td>
<td colspan=10 class=right>
<b>Total</b>
</td>
    <td style="background:#CCCCCC"><%= R.AmountZ(total_local)%></b></td>
 
 
</tr>
</table>
<% } %>

<br />

<br><br>
<table width=960 border=0>
<tr>
<td style="width:700px" >
 &nbsp;
</td>
<td  align=center style="width:260px;border:solid 2px black">
<br>
<br>
<br>
<br>
<br>
_____________________________
<br>
Approved By
</td>
</tr>
 
</table>


</div>
    </div>
    </form>
</body>
</html>

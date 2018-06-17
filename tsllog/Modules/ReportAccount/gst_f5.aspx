<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gst_f5.aspx.cs" Inherits="ReportFreightSea_aspx_pl_imp" %>

<script runat="server">
    string CompanyName = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
    string ReportName = "GST RETURN REPORT";

 </script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="/script/jquery.js"></script>

   <style media="print">
	.noprint {display:none;}
	.doprint {font-size:10pt;}
	.onlyprint {font-size:10pt;}
	</style>	
   <style media="screen">
	.onlyprint {display:none;}
	td {background-color:white;}
	</style>

    <style>
    body {font-size:11px;font-family:Arial;}
    td {font-size:11px; padding:4px; }  
    .head td {background:#eeeeee;font-weight:bold;text-align:center}  
    .hide {display:none;}
    .right {text-align:right;}
    .pt table {border-collapse:collapse;}
    .ps2 {margin:6px;margin-left:50px;}
    .ps {width:960px;}
    .ps3 {width:600px;}

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
    .w600 {width:600px;}

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
        string year1 = Request["start_year"] ?? "";
        string month1 = Request["start_month"] ?? "";
        string year2 = Request["end_year"] ?? "";
        string month2 = Request["end_month"] ?? "";
        
        DateTime d1 = DateTime.Parse(year1 + "-" + month1 + "-1");
        DateTime d2a = DateTime.Parse(year2 + "-" + month2 + "-1");
        DateTime d2 = d2a.AddMonths(1).AddDays(-1);
                 %>

    <div>
    <div class="pt">

<table class="ps">
<tr>
<td class="w2p al">
<h2><%= CompanyName%> - <%= ReportName%></h2>
</td>
<td class="w2p ar">

<h2>
<a class="noprint" href="javascript:window.print();">Print ..</a>
&nbsp;
&nbsp;
&nbsp;
TAX PERIOD (<%= string.Format("{0:yyyy/MMM} - {1:yyyy/MMM}",d1,d2)%>)</h2>
</td>
</tr>
</table>
<br />
<%
    decimal t_source_ar = 0;
    decimal t_gst_ar = 0;
    decimal t_source_ap = 0;
    decimal t_gst_ap = 0;
    decimal t_source = 0;
    decimal t_gst = 0;
    %>

<table class=ps border=1>


    <%
    
    
    string temp1 = "select d.docno, d.doctype, d.docdate, l.locamt, l.gstamt, l.accode from xaarinvoicedet l, xaarinvoice d where l.docno=d.docno and d.docdate>='{0:yyyy-MM-dd}' and (l.gsttype='S' or l.accode='2036') and d.docdate <='{1:yyyy-MM-dd}' order by docdate, doctype, docno ";
    string sql1 = string.Format(temp1,d1,d2);
    DataTable dt_gst1 = ConnectSql.GetTab(sql1);
    t_source = 0;
    t_gst = 0;
    for (int i = 0; i < dt_gst1.Rows.Count; i++)
    {
        string typ = dt_gst1.Rows[i]["DocType"].ToString();
        string acc = dt_gst1.Rows[i]["AcCode"].ToString();
        if (typ == "CN")
        {
            t_source -= SafeValue.SafeDecimal(dt_gst1.Rows[i]["LocAmt"]) - SafeValue.SafeDecimal(dt_gst1.Rows[i]["GstAmt"]);
            t_gst -= SafeValue.SafeDecimal(dt_gst1.Rows[i]["GstAmt"]);
	    if(acc == "2036")
	            t_gst -= SafeValue.SafeDecimal(dt_gst1.Rows[i]["LocAmt"]);
        }
        else
        {
            t_source += SafeValue.SafeDecimal(dt_gst1.Rows[i]["LocAmt"]) - SafeValue.SafeDecimal(dt_gst1.Rows[i]["GstAmt"]);
            t_gst += SafeValue.SafeDecimal(dt_gst1.Rows[i]["GstAmt"]);
	    if(acc == "2036")
	            t_gst += SafeValue.SafeDecimal(dt_gst1.Rows[i]["LocAmt"]);
        }
    }

    t_source_ar += t_source;
    t_gst_ar += t_gst;
    
    %>

<tr>
<th class=w80 onclick='$("#box1_det").toggle();'>
<h2>BOX 1</h2>
</th>
<th class=w600 align="center">
Total Value of Standard-rated Supplies (excluding GST) (SR Standard-rated supplies with GST charged)
<br />
<h2><%= string.Format("{0:#,###0.00}",t_source) %></h2>
</th>
<th class=w240>
Total GST Amount
<br />
<h2><%= string.Format("{0:#,###0.00}",t_gst) %></h2>
</th>
</tr>
<tr id="box1_det" class=hide >
<td colspan=3>
<%
 if (dt_gst1.Rows.Count > 0)
    {
        %>
<table class="ps2" border=1>
<tr class=head>
<td class="w160">
Doc No</td>
<td class="w100">
Doc Type
</td>
<td class="w100">
Doc Date
</td>
<td class="w200">
Source Amount</td>
<td class="w200">
Gst Amount</td>
</tr>
<% 
for (int i = 0; i < dt_gst1.Rows.Count; i++)
{ %>
<tr>
<td>
    <%= dt_gst1.Rows[i]["DocNo"]%>
</td>
<td>
    <%= dt_gst1.Rows[i]["DocType"]%>
</td>
<td align="center">
    <%= string.Format("{0:dd/MM/yyyy}",dt_gst1.Rows[i]["DocDate"])%>
</td>
<td class=right>
<%= string.Format("{0:#,##0.00}", SafeValue.SafeDecimal(dt_gst1.Rows[i]["LocAmt"]) - SafeValue.SafeDecimal(dt_gst1.Rows[i]["GstAmt"]))%>
</td>
<td class=right>
<%= string.Format("{0:#,##0.00}", dt_gst1.Rows[i]["GstAmt"])%>
</td>

</tr>
<%
} %>


</table>
<% } %>

</td>
</tr>

    <%
    
    
    string temp2 = "select d.docno, d.doctype, d.docdate, l.locamt, l.gstamt from xaarinvoicedet l, xaarinvoice d where l.docno=d.docno and d.docdate>='{0:yyyy-MM-dd}' and (gsttype='Z') and d.docdate <='{1:yyyy-MM-dd}' order by docdate, doctype, docno ";
    string sql2 = string.Format(temp2,d1,d2);
    DataTable dt_gst2 = ConnectSql.GetTab(sql2);
    t_source = 0;
    t_gst = 0;
    for (int i = 0; i < dt_gst2.Rows.Count; i++)
    {
        string typ = dt_gst2.Rows[i]["DocType"].ToString();
        if (typ == "CN")
        {
            t_source -= SafeValue.SafeDecimal(dt_gst2.Rows[i]["LocAmt"])-SafeValue.SafeDecimal(dt_gst2.Rows[i]["GstAmt"]);
            t_gst -= SafeValue.SafeDecimal(dt_gst2.Rows[i]["GstAmt"]);
        }
        else
        {
            t_source += SafeValue.SafeDecimal(dt_gst2.Rows[i]["LocAmt"])-SafeValue.SafeDecimal(dt_gst2.Rows[i]["GstAmt"]);
            t_gst += SafeValue.SafeDecimal(dt_gst2.Rows[i]["GstAmt"]);
        }
    }

    t_source_ar += t_source;
    t_gst_ar += t_gst;

    
    %>

<tr>
<th class=w80 onclick='$("#box2_det").toggle();'>
<h2>BOX 2</h2>
</th>
<th class=w600 align="center">
Total Value of Zero-rated Supplies, ZR Zero-rated supplies
<br />
<h2><%= string.Format("{0:#,###0.00}",t_source) %></h2>
</th>
<th class=w240>
Total GST Amount
<br />
<h2><%= string.Format("{0:#,###0.00}",t_gst) %></h2>
</th>
</tr>
<tr id="box2_det" class=hide >
<td colspan=3>
<%
 if (dt_gst2.Rows.Count > 0)
    {
        %>
<table class="ps2" border=1>
<tr class=head>
<td class="w160">
Doc No</td>
<td class="w100">
Doc Type
</td>
<td class="w100">
Doc Date
</td>
<td class="w200">
Source Amount</td>
<td class="w200">
Gst Amount</td>
</tr>
<% 
for (int i = 0; i < dt_gst2.Rows.Count; i++)
{ %>
<tr>
<td>
    <%= dt_gst2.Rows[i]["DocNo"]%>
</td>
<td>
    <%= dt_gst2.Rows[i]["DocType"]%>
</td>
<td align="center">
    <%= string.Format("{0:dd/MM/yyyy}",dt_gst2.Rows[i]["DocDate"])%>
</td>
<td class=right>
<%= string.Format("{0:#,##0.00}", SafeValue.SafeDecimal(dt_gst2.Rows[i]["LocAmt"])-SafeValue.SafeDecimal(dt_gst2.Rows[i]["GstAmt"]))%>
</td>
<td class=right>
<%= string.Format("{0:#,##0.00}", dt_gst2.Rows[i]["GstAmt"])%>
</td>

</tr>
<%
} %>


</table>
<% } %>

</td>
</tr>


    <%
    
    string temp3 = "select d.docno, d.doctype, d.docdate, l.locamt, l.gstamt from xaarinvoicedet l, xaarinvoice d where l.docno=d.docno and d.docdate>='{0:yyyy-MM-dd}' and (gsttype='E') and d.docdate <='{1:yyyy-MM-dd}' order by docdate, doctype, docno ";
    string sql3 = string.Format(temp3,d1,d2);
    DataTable dt_gst3 = ConnectSql.GetTab(sql3);
    t_source = 0;
    t_gst = 0;
    for (int i = 0; i < dt_gst3.Rows.Count; i++)
    {
        string typ = dt_gst3.Rows[i]["DocType"].ToString();
        if (typ == "CN")
        {
            t_source -= SafeValue.SafeDecimal(dt_gst3.Rows[i]["LocAmt"]);
            t_gst -= SafeValue.SafeDecimal(dt_gst3.Rows[i]["GstAmt"]);
        }
        else
        {
            t_source += SafeValue.SafeDecimal(dt_gst3.Rows[i]["LocAmt"]);
            t_gst += SafeValue.SafeDecimal(dt_gst3.Rows[i]["GstAmt"]);
        }
    }

    t_source_ar += t_source;
    t_gst_ar += t_gst;

    %>

<tr>
<th class=w80 onclick='$("#box3_det").toggle();'>
<h2>BOX 3</h2>
</th>
<th class=w600 align="center">
Total Value of Zero-rated Supplies, ZR Zero-rated supplies
<br />
<h2><%= string.Format("{0:#,###0.00}",t_source) %></h2>
</th>
<th class=w240>
Total GST Amount
<br />
<h2><%= string.Format("{0:#,###0.00}",t_gst) %></h2>
</th>
</tr>
<tr id="box3_det" class=hide >
<td colspan=3>
<%
 if (dt_gst3.Rows.Count > 0)
    {
        %>
<table class="ps2" border=1>
<tr class=head>
<td class="w160">
Doc No</td>
<td class="w100">
Doc Type
</td>
<td class="w100">
Doc Date
</td>
<td class="w200">
Source Amount</td>
<td class="w200">
Gst Amount</td>
</tr>
<% 
for (int i = 0; i < dt_gst3.Rows.Count; i++)
{ %>
<tr>
<td>
    <%= dt_gst3.Rows[i]["DocNo"]%>
</td>
<td>
    <%= dt_gst3.Rows[i]["DocType"]%>
</td>
<td align="center">
    <%= string.Format("{0:dd/MM/yyyy}",dt_gst3.Rows[i]["DocDate"])%>
</td>
<td class=right>
<%= string.Format("{0:#,##0.00}", dt_gst3.Rows[i]["LocAmt"])%>
</td>
<td class=right>
<%= string.Format("{0:#,##0.00}", dt_gst3.Rows[i]["GstAmt"])%>
</td>

</tr>
<%
} %>


</table>
<% } %>

</td>
</tr>

<tr>
<th class=w80 onclick='$("#box3_det").toggle();'>
<h2>BOX 4</h2>
</th>
<th class=w600 align="center">
Total Value of Box 1,2 and 3
<br />
<h2><%= string.Format("{0:#,###0.00}",t_source_ar) %></h2>
</th>
<th class=w240>
Total GST Amount
<br />
<h2><%= string.Format("{0:#,###0.00}",t_gst_ar) %></h2>
</th>
</tr>


    <%
    
    string temp5 = "select d.docno, d.doctype, d.docdate, l.locamt, l.gsttype, l.gstamt, l.accode from xaappayabledet l, xaappayable d where l.docno=d.docno and d.docdate>='{0:yyyy-MM-dd}'  and d.docdate <='{1:yyyy-MM-dd}' order by l.gsttype, d.docdate, doctype, docno ";
    string temp5ps = "select d.docno, d.doctype, d.docdate, l.locamt, 'S' as gsttype, 0 as gstamt, l.accode from xaappaymentdet l, xaappayment d where l.payid =d.sequenceid and d.docdate>='{0:yyyy-MM-dd}'  and l.accode='4053' and d.docdate <='{1:yyyy-MM-dd}' order by  d.docdate, doctype, docno ";
    string temp5ge = "select d.docno, d.doctype, d.docdate, l.dbamt as locamt, 'G' as gsttype, 0 as gstamt, l.accode from xaglentrydet l, xaglentry d where d.sequenceid=l.glno and d.docdate>='{0:yyyy-MM-dd}'  and l.accode='4053' and d.doctype='GE' and d.docdate <='{1:yyyy-MM-dd}' order by  d.docdate, doctype, docno ";
    string sql5 = string.Format(temp5,d1,d2);
    DataTable dt_gst5= ConnectSql.GetTab(sql5);
    string sql5ps = string.Format(temp5ps,d1,d2);
    DataTable dt_gst5ps= ConnectSql.GetTab(sql5ps);
    string sql5ge = string.Format(temp5ge,d1,d2);
    DataTable dt_gst5ge= ConnectSql.GetTab(sql5ge);
    t_source = 0;
    t_gst = 0;
    for (int i = 0; i < dt_gst5.Rows.Count; i++)
    {
        string typ = dt_gst5.Rows[i]["DocType"].ToString();
        string acc = dt_gst5.Rows[i]["AcCode"].ToString();
        if (typ == "SC")
        {
            t_source -= SafeValue.SafeDecimal(dt_gst5.Rows[i]["LocAmt"]) - SafeValue.SafeDecimal(dt_gst5.Rows[i]["GstAmt"]);
            t_gst -= SafeValue.SafeDecimal(dt_gst5.Rows[i]["GstAmt"]);
	    if(acc == "4053")
	            t_gst -= SafeValue.SafeDecimal(dt_gst5.Rows[i]["LocAmt"]);
	   	
        }
        else
        {
            t_source += SafeValue.SafeDecimal(dt_gst5.Rows[i]["LocAmt"]) - SafeValue.SafeDecimal(dt_gst5.Rows[i]["GstAmt"]);
            t_gst += SafeValue.SafeDecimal(dt_gst5.Rows[i]["GstAmt"]);
	    if(acc == "4053")
	            t_gst += SafeValue.SafeDecimal(dt_gst5.Rows[i]["LocAmt"]);
        }
    }


    for (int i = 0; i < dt_gst5ps.Rows.Count; i++)
    {
        string typ = dt_gst5ps.Rows[i]["DocType"].ToString();
        string acc = dt_gst5ps.Rows[i]["AcCode"].ToString();
            t_source += SafeValue.SafeDecimal(dt_gst5ps.Rows[i]["LocAmt"]);
            t_gst += SafeValue.SafeDecimal(dt_gst5ps.Rows[i]["LocAmt"]);
    }

    for (int i = 0; i < dt_gst5ge.Rows.Count; i++)
    {
        string typ = dt_gst5ge.Rows[i]["DocType"].ToString();
        string acc = dt_gst5ge.Rows[i]["AcCode"].ToString();
            t_source += SafeValue.SafeDecimal(dt_gst5ge.Rows[i]["LocAmt"]);
            t_gst += SafeValue.SafeDecimal(dt_gst5ge.Rows[i]["LocAmt"]);
    }


    t_source_ap += t_source;
    t_gst_ap += t_gst;

    %>

<tr>
<th class=w80 onclick='$("#box5_det").toggle();'>
<h2>BOX 5</h2>
</th>
<th class=w600 align="center">
Total Value of Taxable Purchases (excluding GST)
<br />
<h2><%= string.Format("{0:#,###0.00}",t_source) %></h2>
</th>
<th class=w240>
Total GST Amount
<br />
<h2><%= string.Format("{0:#,###0.00}",t_gst) %></h2>
</th>
</tr>
<tr id="box5_det" class=hide >
<td colspan=3>
<%
 if (dt_gst5.Rows.Count > 0)
    {
        %>
<table class="ps2" border=1>
<tr class=head>
<td class="w120">
GST Type</td>
<td class="w160">
Doc No</td>
<td class="w100">
Doc Type
</td>
<td class="w100">
Doc Date
</td>
<td class="w200">
Source Amount</td>
<td class="w200">
Gst Amount</td>
</tr>
<% 
for (int i = 0; i < dt_gst5.Rows.Count; i++)
{ %>
<tr>
<td>
    <%= dt_gst5.Rows[i]["GstType"].ToString() == "S" ? "Standard Rated TX7" : "Zero Rated" %>
</td>
<td>
    <%= dt_gst5.Rows[i]["DocNo"]%>
</td>
<td>
    <%= dt_gst5.Rows[i]["DocType"]%>
</td>
<td align="center">
    <%= string.Format("{0:dd/MM/yyyy}",dt_gst5.Rows[i]["DocDate"])%>
</td>
<td class=right>
<%= string.Format("{0:#,##0.00}", SafeValue.SafeDecimal(dt_gst5.Rows[i]["LocAmt"]) - SafeValue.SafeDecimal(dt_gst5.Rows[i]["GstAmt"]))%>
</td>
<td class=right>
<%= string.Format("{0:#,##0.00}", dt_gst5.Rows[i]["GstAmt"])%>
</td>

</tr>
<%
} %>


<% 
for (int i = 0; i < dt_gst5ps.Rows.Count; i++)
{ %>
<tr>
<td>
    <%= "Standard Rated TX7"  %>
</td>
<td>
    <%= dt_gst5ps.Rows[i]["DocNo"]%>
</td>
<td>
    <%= dt_gst5ps.Rows[i]["DocType"]%>
</td>
<td align="center">
    <%= string.Format("{0:dd/MM/yyyy}",dt_gst5ps.Rows[i]["DocDate"])%>
</td>
<td class=right>
<%= string.Format("{0:#,##0.00}", dt_gst5ps.Rows[i]["LocAmt"])%>
</td>
<td class=right>
<%= string.Format("{0:#,##0.00}", dt_gst5ps.Rows[i]["LocAmt"])%>
</td>

</tr>
<%
} %>



<% 
for (int i = 0; i < dt_gst5ge.Rows.Count; i++)
{ %>
<tr>
<td>
    <%= "Standard Rated TX7"  %>
</td>
<td>
    <%= dt_gst5ge.Rows[i]["DocNo"]%>
</td>
<td>
    <%= dt_gst5ge.Rows[i]["DocType"]%>
</td>
<td align="center">
    <%= string.Format("{0:dd/MM/yyyy}",dt_gst5ge.Rows[i]["DocDate"])%>
</td>
<td class=right>
<%= string.Format("{0:#,##0.00}", dt_gst5ge.Rows[i]["LocAmt"])%>
</td>
<td class=right>
<%= string.Format("{0:#,##0.00}", dt_gst5ge.Rows[i]["LocAmt"])%>
</td>

</tr>
<%
} %>


</table>
<% } %>

</td>
</tr>


<tr>
<th class=w80 >
<h2>BOX 6</h2>
</th>
<th class=w600 align="center">
Output Tax Due
</th>
<th class=w240>
<h2><%= string.Format("{0:#,###0.00}",t_gst_ar  ) %></h2>
</th>
</tr>

<tr>
<th class=w80 >
<h2>BOX 7</h2>
</th>
<th class=w600 align="center">
Input Tax and Refunds Claimed
</th>
<th class=w240>
<h2><%= string.Format("{0:#,###0.00}",t_gst_ap) %></h2>
</th>
</tr>


<tr>
<th class=w80  >
<h2>BOX 8</h2>
</th>
<th class=w600 align="center">
Net GST to be Paid to IRAS
</th>
<th class=w240>
<h2><%= string.Format("{0:#,###0.00}",t_gst_ar - t_gst_ap) %></h2>
</th>
</tr>
<tr>
<th class=w80  >
<h2>BOX 9</h2>
</th>
<th class=w600 align="center">
 Total Value of Goods Imported Under MES/Approved 3PL Scheme

</th>
<th class=w240>

<h2><%= string.Format("{0:#,###0.00}",0) %></h2>
</th>
</tr>
<tr>
<th class=w80  >
<h2>BOX 10</h2>
</th>
<th class=w600 align="center">
Total Value of Tourist Refund Claimed, Tourist GST Refund Claimed Account
</th>
<th class=w240>
<h2><%= string.Format("{0:#,###0.00}",0) %></h2>
</th>
</tr>
<tr>
<th class=w80 >
<h2>BOX 11</h2>
</th>
<th class=w600 align="center">
Total Value of Bad Debt Relief Claims, GST Claimed Account for Bad Debts
</th>
<th class=w240>
<h2><%= string.Format("{0:#,###0.00}",0) %></h2>
</th>
</tr>
<tr>
<th class=w80 >
<h2>BOX 13</h2>
</th>
<th class=w600 align="center">
Total Revenue
</th>
<th class=w240>
<h2><%= string.Format("{0:#,###0.00}",t_source_ar - t_source_ap) %></h2>
</th>
</tr>

</table>






</div>
    </div>
    </form>
</body>
</html>

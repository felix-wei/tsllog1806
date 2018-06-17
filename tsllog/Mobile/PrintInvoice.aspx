<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintInvoice.aspx.cs" Inherits="Mobile_PrintInvoice" %>

<script runat="server">
	public string no = "";
	public DataTable dt0;
	public DataRow dr0;
	public DataTable dt1;
	public DataRow dr1;
	public DataTable dt2;
	public DataRow dr2;
	public string doc = "";
	public string job = "";
	public decimal total = 0;
	public decimal gst = 0;
</script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
		<style>
		td {font-size:13px}
		th {font-size:13px;border-bottom:solid 1px black;}
		</style>
    <script type="text/javascript">
        function doPrint() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            pagesetup_null();
            window.print();
        }
</script>
 
</head>
<body >
<%
	no = Request.QueryString["no"];
	dt0 = ConnectSql_mb.GetDataTable("select * from XaArInvoice where DocNo='"+no+"'");
	dr0 = dt0.Rows[0];
	doc = SafeValue.SafeString(dr0["SequenceId"]);
    job = SafeValue.SafeString(dr0["MastRefNo"]);
    dt1 = ConnectSql_mb.GetDataTable("select * from XaArInvoiceDet where DocId='" + doc + "'");
    //dt2 = ConnectSql_mb.GetDataTable("select * from JobInfo where QuotationNo='" + job + "'");
    //dr2 = dt2.Rows[0];
%>
    <form id="form1" runat="server">       
        <!--startprint-->
    <div style="margin:0 auto;width: 210mm;font-size:11px;">
	
                       <% if( true ) { %>
	
        <table  class="head">
        <tr>
                       <td noprint style="width:60%"><!-- <img src="/custom/logo-doc.jpg" /> --></td>
            <td style="width:400px">
                <p>
                    <span style="font-size:12px"><%=System.Configuration.ConfigurationManager.AppSettings["CompanyName"] %></span><br />
                    <%=System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"] %><br />
                    <%=System.Configuration.ConfigurationManager.AppSettings["Tel"] %><br />
                    <%=System.Configuration.ConfigurationManager.AppSettings["Email"] %><br />
                    <%=System.Configuration.ConfigurationManager.AppSettings["CoRegnNo"] %><br />
                    <%=System.Configuration.ConfigurationManager.AppSettings["GSTRegnNo"] %><br />
                </p>
            </td>
        </tr>
    </table>
<% } %>        

                    <table class="A4">
                       <tr>
                            <td><u><font style="font-size:20px;"><%= true ? "TAX INVOICE" : "INTERNAL BILLING INSTRUCTION"%></font></u><br><br>
                            </td>
                        </tr>
                        
                    </table>
      
                    <table style="font-size:13px;">
                        <tr>
							<td  valign=top style="width:500px;font-size:13px;text-align:left;">
							<table style="font-size:13px;">
							<tr>
							
							<td colspan=2><%= SafeValue.SafeString(dr0["PartyTo"]) %></td>
							</tr>
							<tr>
							<td colspan=2><%--<%= SafeValue.SafeString(dr2["CustomerAdd"]) %>--%><br><br></td>
							</tr>
							<tr>
							<td>Attention:-</td>
							<td><%--<%= SafeValue.SafeString(dr2["Contact"]) %>--%></td>
							</tr>
							<tr>
							<td>Tel No:-</td>
							<td><%--<%= SafeValue.SafeString(dr2["Tel"]) + "/" + SafeValue.SafeString(dr2["Fax"]) %>--%></td>
							</tr>
							</table>
							</td>
                            <td  valign=top style="width:300px;font-size:13px;text-align:right;">
							<table style="font-size:13px;">
							<tr>
							<td width=150>Inv No</td>
							<td width=150><%= SafeValue.SafeString(dr0["DocNo"]) %></td>
							</tr>
							<tr>
							<td>Date</td>
							<td><%= SafeValue.SafeDateStr(dr0["DocDate"]) %></td>
							</tr>
							<tr>
							<td>Terms</td>
							<td><B><%= SafeValue.SafeString(dr0["Term"]) %></B></td>
							</tr>
							<tr>
							<td>Job Ref #</td>
							<td><%--<%= SafeValue.SafeString(dr2["JobNo"])  %>--%></td>
							</tr>
							<tr>
							<td>PO #</td>
							<td><%= SafeValue.SafeString(dr0["JobRefNo"])  %></td>
							</tr>
							</table>
							</td>
                        </tr>
                         
                        
                    </table>
                   
                   
                    <!--  Begin-->
                    <div>
                        <%--<table class="table_A4" border=1>
						<tr>
						<td class="td_bottom_right" style="font-size:13px;"  width=150>Move Date</td>
						<td class="td_bottom_right" style="font-size:13px;"  width=250><%= SafeValue.SafeString(dr2["MoveRmk"])%></td>
						<td class="td_bottom_right" style="font-size:13px;"  width=150>Origin</td>
						<td class="td_bottom_right" style="font-size:13px;"  width=350><%= SafeValue.SafeString(dr2["OriginAdd"])%></td>
						</tr>
						<tr>
						<td class="td_bottom_right" style="font-size:13px;"  width=150>Volume</td>
						<td class="td_bottom_right" style="font-size:13px;"  width=250><%= SafeValue.SafeString(dr2["VolumneRmk"])%></td>
						<td class="td_bottom_right" style="font-size:13px;"  width=150>Destination</td>
						<td class="td_bottom_right" style="font-size:13px;"  width=350><%= SafeValue.SafeString(dr2["DestinationAdd"])%></td>
						</tr>
						</table>--%>
						<br>
                        <table style="border:solid 1px black" cellspacing="0" border=0>
                            <tr class="tr1" style="border-bottom:solid 1px black;">
								<th   style="font-size:13px;text-align:left" width=500>DESCRIPTION</th>
								<th   style="font-size:13px;text-align:center" width=80>GST Code</th>
								<th   style="font-size:13px;text-align:center" width=100>Amount</th>
								<th   style="font-size:13px;text-align:center" width=120>Total</th>
                            </tr>
							<% 
								for(int i=0; i< dt1.Rows.Count; i++){
									dr1 = dt1.Rows[i];
							%>
							                            <tr class="tr">
                                <td   style="font-size:13px;"><%= SafeValue.SafeString(dr1["ChgDes1"])%></td>
                                <td  style="font-size:13px;"><%= SafeValue.SafeString(dr1["GstType"]) == "S" ? "GST:7%" : "NT" %></td>
                                <td   style="font-size:13px;" align=right><%= SafeValue.SafeString(SafeValue.SafeDecimal(dr1["DocAmt"])-SafeValue.SafeDecimal(dr1["GstAmt"]))%></td>
                                <td  style="font-size:13px;" align=right><%= SafeValue.SafeString(dr1["LocAmt"])%></td>
                            </tr>
							<%
                                    total += SafeValue.SafeDecimal(dr1["DocAmt"]);
                                    gst += SafeValue.SafeDecimal(dr1["GstAmt"]);
								}
							
							%>
						</table>
						<table>
                            <tr class="tr">
                                <td   style="width:680px;border-left:0px;border-bottom:0px;font-size:13px;;text-align:right">Sub Total :</td>
                                <td   style="width:120px;font-size:13px;text-align:right;"> <%=  SafeValue.SafeString(total-gst) %> </td>
                            </tr>
                            <tr class="tr">
                                <td    style="font-size:13px;text-align:right;border-left:0px;border-bottom:0px;">Total GST :</td>
                                <td   style="font-size:13px;;text-align:right"> <%=  SafeValue.SafeString(gst) %> </td>
                            </tr>
                            <tr class="tr">
                                <td    style="font-size:13px;;text-align:right;border-left:0px;border-bottom:0px;">Total Charges Inclusive of GST (in <%= SafeValue.SafeString(dr0["CurrencyId"])%>) :</td>
                                <td   style="font-size:13px;;text-align:right"> <%=  SafeValue.SafeString(total) %> </td>
                            </tr>
                
                        </table>
                       
                        <table style="font-size:13px;">
                             
                            <tr>
                                <td><br><br><br>
								Amount in words (<%= SafeValue.SafeString(dr0["CurrencyId"])%>):
								<%= (new NumberToMoney()).NumberToCurrency(SafeValue.ChinaRound(total, 2)).ToUpper() + " ONLY" %><br>


								<br>
                       <%--<% if( true ) { %>

								<table border=1 width="450">
								<tr>
								<td>
	 <u>For Telegraphice Transfer, please remit to</u>:<br>								
     Bank: DBS Bank Ltd, Jurong Point Branch	<br>							
     Address: 63 Jurong West Central 3				<br>				
     A/C No: 002-900696-5&nbsp;&nbsp;&nbsp;               Swift Code: DBSSSGSG<br>								
     A/C Name: Collin’s Movers Pte Ltd								
								
								</td>
								</tr>
								</table>
								<br>
								
<table border=0>
<tr>
<td width=500 nowrap>
Cheques should be crossed & made payable to : <br> 
<span style="font-size:18px;font-weight:bold;">“Collin’s Movers Pte Ltd”</span>								<br>
Please quote invoice number & ref at the back of the cheque.<br>								
<br>

This is a computer generated form hence no signature is required	<br>								
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;E. & O.E.								

</td>
<td width=300 align=right>
<br>
<br>
	__________________________<br>
	Collin's Movers Pte Ltd&nbsp;&nbsp;&nbsp;&nbsp;	<br>
</td>
</tr>								
</table>					

<% } %>			--%>

								
								
								</td>
                                
                            </tr>
                        
                        </table>
                    </div>
    

            </table>
    </div>
                <!--endprint-->
    </form>
</body>
</html>

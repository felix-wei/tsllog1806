<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" %>
<script runat="server">
	public string title = "";
	public string id = "";
	public string job = "";
	public string jobtype = "";
		public DataTable dt0;
		public DataTable dt1;
		public DataRow dr0;
		public DataRow dr1;
		public bool isRead = false;
		
		protected void Page_Init(object o, EventArgs e)
		{
				isRead = (Request.Params["read"] ?? "0") == "0" ? false : true;

		}
		
	protected void btn_Save_Click(object sender, EventArgs e)
    {
		string allkey = Request.Params["allkey"] ?? "";
		
		
		id = Request.Params["id"] ?? "";
		
		string[] keys = allkey.Split(new char[] {'#'});
		
		for(int i=0; i<keys.Length-1; i++)
		{
			decimal qty = Helper.Safe.SafeDecimal(Request.Params["qty-" + keys[i]]);
			decimal price = Helper.Safe.SafeDecimal(Request.Params["price-" + keys[i]]);
			decimal min = Helper.Safe.SafeDecimal(Request.Params["min-" + keys[i]]);
			decimal total = Helper.Safe.SafeDecimal(Request.Params["total-" + keys[i]]);
			decimal local = Helper.Safe.SafeDecimal(Request.Params["local-" + keys[i]]);
			string rmk = Helper.Safe.SafeString(Request.Params["remark-" + keys[i]]);
		
			string update_sql = string.Format(@"Update CostDet Set SaleQty='{1}', SalePrice='{2}', SaleDocAmt='{3}',SaleLocAmt='{6}', CostPrice='{4}', Remark='{5}' where SequenceId={0}",
				keys[i], qty, price, total, min, rmk.Replace("'"," "),local );
				
			//	throw new Exception(update_sql);
			Helper.Sql.Exec(update_sql);
		}

			decimal v1 = Helper.Safe.SafeDecimal(Request.Params["cuft"]);
			decimal v2 = Helper.Safe.SafeDecimal(Request.Params["gross"]);
			decimal v3 = Helper.Safe.SafeDecimal(Request.Params["lbs"]);
			decimal v4 = Helper.Safe.SafeDecimal(Request.Params["kgs"]);
			decimal v5 = Helper.Safe.SafeDecimal(Request.Params["cbm"]);
			decimal v6 = Helper.Safe.SafeDecimal(Request.Params["cbmr"]);
			decimal v7 = Helper.Safe.SafeDecimal(Request.Params["cbmn"]);

			decimal ex1 = Helper.Safe.SafeDecimal(Request.Params["exr-of"]);
			decimal ex2 = Helper.Safe.SafeDecimal(Request.Params["exr-ds"]);
			string curr1 = Helper.Safe.SafeString(Request.Params["curr-of"]);
			string curr2 = Helper.Safe.SafeString(Request.Params["curr-ds"]);

			decimal amt1 = Helper.Safe.SafeDecimal(Request.Params["total-os"]);
			decimal amt2 = Helper.Safe.SafeDecimal(Request.Params["total-of"]);
			decimal amt3 = Helper.Safe.SafeDecimal(Request.Params["total-ds"]);
			decimal amt4 = Helper.Safe.SafeDecimal(Request.Params["total-ts"]);
			//decimal amount2 = Helper.Safe.SafeDecimal(Request.Params["total-dis"]);
			decimal amount2 = Helper.Safe.SafeDecimal(Request.Params["total2"]);
			decimal profitmargin = Helper.Safe.SafeDecimal(Request.Params["total-pro"]);
			decimal amount = Helper.Safe.SafeDecimal(Request.Params["total"]);
			string costrmk = Helper.Safe.SafeString(Request.Params["cost-remark"]);

			
			string update_sql_total = string.Format(@"Update Cost Set 
			amount='{1}', amount2='{2}', profitmargin='{3}', 
			amt1='{4}' ,amt2='{5}' ,amt3='{6}' ,amt4='{7}' ,
			exrate1='{8}' ,exrate2='{9}' ,curr1='{10}' ,curr2='{11}' ,
			value1 = '{12}',value2 = '{13}',value3 = '{14}',
			value4 = '{15}',value5 = '{16}',value6 = '{17}', value7 = '{18}',marking='{19}'
			where SequenceId={0}",
				id,
				amount,amount2,profitmargin,
				amt1,amt2,amt3,amt4,
				ex1,ex2,curr1,curr2,
				v1,v2,v3,v4,v5,v6,v7,costrmk.Replace("'"," "));
				
		//throw new Exception(update_sql_total);
			Helper.Sql.Exec(update_sql_total);
		
        this.lab.Text = "Success";
		
	
	
		

    }

</script>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
	    <style media="print">
		input {border:0px;background:white;}
	.noprint {display:none;}
	.noprintb {border:0px;}
	.doprint {font-size:10pt;}
	.onlyprint {font-size:10pt;}
    </style>	
	<style>
		tr.costline td {border-bottom:solid 1px #888888}
		.hidenow {display:none;}
		.csstotal {background:#cccccc}
		.csslocal {background:#cccccc}
		.cssunit {background:#cccccc}
		.cssmin {background:#cccccc}
		.csscuft {background:#cccccc}
	</style>

    <script type="text/javascript" src="/Script/jquery.js" />

    <script type="text/javascript">
        $.noConflict();
    </script>

    <title></title>
    <script type="text/javascript">
	
var currList = { 
'AUD': 1.1000, 
'CAD': 0.0000, 
'CHF': 0.0000, 
'CNY': 0.0000, 
'EUR': 1.5404, 
'GBP': 0.0000, 
'HKD': 0.0000, 
'JPY': 0.0000, 
'MYR': 0.3771, 
'NZD': 0.0000, 
'SGD': 0.0000, 
'USD': 1.3605, 
 'XXX' : 0 }; 
 
        function FormatNumber(srcStr, nAfterDot) {
            var srcStr, nAfterDot;
            var resultStr, nTen;
            srcStr = "" + srcStr + "";
            strLen = srcStr.length;
            dotPos = srcStr.indexOf(".", 0);
            if (dotPos == -1) {
                resultStr = srcStr + ".";
                for (i = 0; i < nAfterDot; i++) {
                    resultStr = resultStr + "0";
                }
            }
            else {
                if ((strLen - dotPos - 1) >= nAfterDot) {
                    nAfter = dotPos + nAfterDot + 1;
                    nTen = 1;
                    for (j = 0; j < nAfterDot; j++) {
                        nTen = nTen * 10;
                    }
                    resultStr = Math.round(parseFloat(srcStr) * nTen) / nTen;
                }
                else {
                    resultStr = srcStr;
                    for (i = 0; i < (nAfterDot - strLen + dotPos + 1) ; i++) {
                        resultStr = resultStr + "0";
                    }

                }
            }
            return resultStr;
        }
    </script>
	

	<style>
		table {font-size:12px; font-family:arial; width:800px; }		
	</style>	
		 
</head>
<body>

	<%
		id = Request.QueryString["id"] ?? "0";
		dt0 = Helper.Sql.List("select * from cost where SequenceId=" + id);
		dr0 = dt0.Rows[0];
		jobtype = dr0["RefType"].ToString();
		title = string.Format("Accural/Costing Sheet : {0}/{1}-{2}    Client Name: {3}",dr0["RefType"],dr0["RefNo"],dr0["CostIndex"], D.Text("select top 1 CustomerName from JobInfo where QuotationNo='"+S.Text(dr0["RefNo"])+"'"));
		int idx = S.Int(dr0["CostIndex"]);
		if(R.Text(dr0["RefType"]) == "Outbound")
		{
			title = string.Format("Accural/Costing Sheet : Outbound/{0}/{1}-{2}  Client Name: {3}",dr0["JobNo"],dr0["RefNo"],dr0["CostIndex"],D.Text("select top 1 CustomerName from JobInfo where QuotationNo='"+S.Text(dr0["RefNo"])+"'"));
		}
		if(idx>1)
			title = "Revised " + title;
	%>
    <form id="form1" runat="server">
		<input type='hidden' name='costmode' id='costmode' value='<%= dr0["JobNo"] %>' />
		<input type='hidden' name='costtype' id='costtype' value='<%= dr0["RefType"] %>' />
        <div><dxe:ASPxTextBox runat="server" ID="txt_Id" ClientVisible="false"></dxe:ASPxTextBox></div>
        <div>

		<table style="border-bottom:solid 2px #CCCCCC;">
		<tr>
		<td style="font-size:14px;font-weight:bold">
		<%= title%>
		</td>
		<td align=right class=noprint style='<%= isRead==true ? "display:none" : "" %>'>
                        <dxe:ASPxButton runat="server" ID="btn_Save" OnClick="btn_Save_Click" Text="Save" Width="70">
                           
                        </dxe:ASPxButton>
		</td>
		<td align=right class=noprint>
                        <dxe:ASPxButton runat="server" ID="btn_Print" AutoPostBack="false" UseSubmitBehavior="false" Text="Print" Width="70">
                            <ClientSideEvents Click="function(){
                            window.print();
                            }" />
                        </dxe:ASPxButton>
		</td>
		</tr>
                <tr class=noprint>
                    <td colspan=2 align=right>
                        <dxe:ASPxLabel ID="lab" ClientInstanceName="lab" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
        
		</table>

		<script>
			

			function change_kgs(el)
			{
					var kgs = parseFloat($(el).val());

					var lbs = FormatNumber(kgs * 2.2,2);
					var gross = FormatNumber(lbs / 6.5,2);
					var vol = FormatNumber(gross / 1.2,2);
					
					var mod= $('#costmode').val();
					var cbmn = FormatNumber(vol / 35.312,3);
					var cbm = FormatNumber(gross / 35.312,3);
					var cbmr = Math.ceil(cbm);
					cbmr = parseInt(cbm);
					if(cbm > cbmr)
						cbmr ++;
					$("#gross").val(gross);
					$("#cbmn").val(cbmn);
					$("#cbm").val(cbm);
					$("#lbs").val(lbs);
					$("#cuft").val(vol);
					if(mod=='LCL') {
						$("#cbmr").val(cbmr);
					}
			$(".cssunit").each(function () {
					var ids = this.id.split('-');
					var k=ids[1];
						var u = $("#unit-" + k).val().toUpperCase();
						var g = $("#linegrp-" + k).val();
						var myvol = gross;
						if(g == "OS")
							myvol = vol;
						//alert(u);
						if(u == "KGS") {
							$("#qty-" + k).val(kgs);
							change_row(k);
						}
						if(u == "CUFT") {
							$("#qty-" + k).val(myvol);
							change_row(k);
						}
						if(u == "CBM") {
							$("#qty-" + k).val(cbm);
							change_row(k);
						}
						if(u == "CBMR") {
							$("#qty-" + k).val(cbmr);
							change_row(k);
						}
							change_row(k);

					});
					
					calc_total();
					
					
			}

			function change_vol(el)
			{
					var vol = parseFloat($(el).val());
					var gross = FormatNumber(vol * 1.2,2);
					var lbs = FormatNumber(gross * 6.5,2);
					var kgs = FormatNumber(lbs / 2.2,2);
					
					var cbmn = FormatNumber(vol / 35.312,3);
					var cbm = FormatNumber(gross / 35.312,3);
					var cbmr = Math.ceil(cbm);
					cbmr = parseInt(cbm);
					var mod= $('#costmode').val();
					if(cbm > cbmr)
						cbmr ++;
					$("#gross").val(gross);
					$("#cbmn").val(cbmn);
					$("#cbm").val(cbm);
					$("#lbs").val(lbs);
					$("#kgs").val(kgs);
					if(mod == "20CON" || mod=="40CON") {
					} else {
						$("#cbmr").val(cbmr);
					}


			$(".cssunit").each(function () {
					var ids = this.id.split('-');
					var k=ids[1];
						var u = $("#unit-" + k).val().toUpperCase();
						var g = $("#linegrp-" + k).val();
						var myvol = gross;
						if(g == "OS")
							myvol = vol;
						//alert(u);
						if(u == "CUFT") {
							$("#qty-" + k).val(myvol);
							change_row(k);
						}
						if(u == "CBM") {
							$("#qty-" + k).val(cbm);
							change_row(k);
						}
						if(u == "CBMR" && mod=="LCL") {
							$("#qty-" + k).val(cbmr);
							change_row(k);
						}
						if(u == "KGS") {
							$("#qty-" + k).val(kgs);
							change_row(k);
						}
							change_row(k);

					});
					
					calc_total();
					
			}

			function recur(n)
			{
				var rr = 1;
				var c1 = $('#curr-of').val();
				var c2 = $('#curr-ds').val();
				if(n==1) {
					$('#exr-of').val(currList[c1]); 
					if(c1 == 'SGD')
						$('#exr-of').val('1.0000');
				}
				if(n==2) {
					$('#exr-ds').val(currList[c2]); 
					if(c2 == 'SGD')
						$('#exr-ds').val('1.0000');
				}
				recalc();
			}
			function recalc()
			{
			$(".cssunit").each(function () {
					var ids = this.id.split('-');
					var k=ids[1];
						change_row(k);

					});
					
					calc_total();
					var typ= $('#costtype').val();
					if(typ=="Inbound")
						calc_total_usd();
			}
			
			function change_line(id)
			{
				//alert($("#qty-" + id).val());
					
					var qty =	parseFloat($("#qty-" + id).val());
					var price =	parseFloat($("#price-" + id).val());
					var exr =	1;
					var grp = $("#linegrp-" + id).val();
					var code = $("#linecode-" + id).val();
					if(grp == "OF")
					   exr = parseFloat($("#exr-of").val());
					if(grp == "DS")
					   exr = parseFloat($("#exr-ds").val());
					var min =	parseFloat($("#min-" + id).val());
					//alert(qty * price);
					var tot = qty * price;
					var mod= $('#costmode').val();
					var u = $("#unit-" + id).val().toUpperCase();
					if(mod == "20CON" || mod=="40CON") {
							if(grp=='OF' || grp=='TS'){
					var s1 = parseFloat($('#cbmn').val());
						var s2 = parseFloat($('#cbmr').val());
						if(s2==0)
							s2 = 1;
						tot = tot * s1 / s2;
						}
					}

					if(tot < min && tot != 0)
						tot = min;
					var loc = tot * exr;
					$("#total-" + id).val(FormatNumber(tot,2));
					$("#local-" + id).val(FormatNumber(loc,2));
			}
			function change_row(id)
			{
				//alert($("#qty-" + id).val());
					var qty =	parseFloat($("#qty-" + id).val());
					var price =	parseFloat($("#price-" + id).val());
					var exr =	1;
					var grp = $("#linegrp-" + id).val();
					var code = $("#linecode-" + id).val();
					if( code == "IB11" || code == "OF01" || code == "OF02" || code == "OF03" || code == "OF04" || code == "OF05" )
					   exr = parseFloat($("#exr-of").val());
					if(grp == "DS")
					   exr = parseFloat($("#exr-ds").val());
					 
					var min =	parseFloat($("#min-" + id).val());
					//alert(qty * price);
					var tot = qty * price;
					if(tot < min && tot != 0)
						tot = min;
						
						var mod= $('#costmode').val();
					var u = $("#unit-" + id).val().toUpperCase();
					if(mod == "20CON" || mod=="40CON") {
						if(grp=='OF' || grp=='TS'){
						var s1 = parseFloat($('#cbmn').val());
						var s2 = parseFloat($('#cbmr').val());
						if(s2==0)
							s2 = 1;
						tot = tot * s1 / s2;
						}
					}	
						
					
					var loc = tot * exr;
					$("#total-" + id).val(FormatNumber(tot,2));
					$("#local-" + id).val(FormatNumber(loc,2));

					if(code=="OF01")
					{
						var v1 =parseFloat($("#total-" + (parseInt(id)+0)).val());
						var v2 =parseFloat($("#total-" + (parseInt(id)+1)).val());
						var v3 =parseFloat($("#total-" + (parseInt(id)+2)).val());
						var v4 =parseFloat($("#total-" + (parseInt(id)+3)).val());
						if(!v1) v1=0;
						$("#price-" + (parseInt(id)+4)).val(FormatNumber((v1+v2+v3+v4) * 0.03,2));
						change_line(parseInt(id)+4);
					}
					if(code=="OF02")
					{
						var v1 =parseFloat($("#total-" + (parseInt(id)-1)).val());
						var v2 =parseFloat($("#total-" + (parseInt(id)+0)).val());
						var v3 =parseFloat($("#total-" + (parseInt(id)+1)).val());
						var v4 =parseFloat($("#total-" + (parseInt(id)+2)).val());
						$("#price-" + (parseInt(id)+3)).val(FormatNumber((v1+v2+v3+v4) * 0.03,2));
						change_line(parseInt(id)+3);
					}
					if(code=="OF03")
					{
						var v1 =parseFloat($("#total-" + (parseInt(id)-2)).val());
						var v2 =parseFloat($("#total-" + (parseInt(id)-1)).val());
						var v3 =parseFloat($("#total-" + (parseInt(id)+0)).val());
						var v4 =parseFloat($("#total-" + (parseInt(id)+1)).val());
						$("#price-" + (parseInt(id)+2)).val(FormatNumber((v1+v2+v3+v4) * 0.03,2));
						change_line(parseInt(id)+2);
					}
					if(code=="OF04")
					{
						var v1 =parseFloat($("#total-" + (parseInt(id)-3)).val());
						var v2 =parseFloat($("#total-" + (parseInt(id)-2)).val());
						var v3 =parseFloat($("#total-" + (parseInt(id)-1)).val());
						var v4 =parseFloat($("#total-" + (parseInt(id)+0)).val());
						$("#price-" + (parseInt(id)+1)).val(FormatNumber((v1+v2+v3+v4) * 0.03,2));
						change_line(parseInt(id)+1);
					}
					calc_total();
			}

			
			function calc_total_profit()
			{
			var amt1 = 0;
				var amt2 = 0;
				var amt3 = 0;
				var amt4 = 0;
				var total = 0;
				var pro = 0;
		
				
					amt1 = parseFloat($("#total-os").val());
					amt2 = parseFloat($("#total-of").val());
					amt3 = parseFloat($("#total-ds").val());
					amt4 = parseFloat($("#total-ts").val());
					total = parseFloat($("#total").val());
					pro = total - amt1-amt2-amt3- amt4 ;
					
				
					$("#total-pro").val(FormatNumber(pro,2));
			}	
			function calc_total()
			{
				var amt1 = 0;
				var amt2 = 0;
				var amt3 = 0;
				var amt4 = 0;
				var dis = 0;
				var pro = 0;
				var total = 0;
				var local = 0;
 
			$(".grp-OS").each(function () {
					var ids = this.id.split('-');
					var l = $("#local-" + ids[1]).val();
					if(l == "")
						l = "0";
					amt1 += parseFloat(l);
				});
			$(".grp-OF").each(function () {
					var ids = this.id.split('-');
					var l = $("#local-" + ids[1]).val();
					if(l == "")
						l = "0";
					amt2 += parseFloat(l);
				});
			$(".grp-AF").each(function () {
					var ids = this.id.split('-');
					var l = $("#local-" + ids[1]).val();
					if(l == "")
						l = "0";
					amt2 += parseFloat(l);
				});
			$(".grp-IB").each(function () {
					var ids = this.id.split('-');
					var l = $("#local-" + ids[1]).val();
					if(l == "")
						l = "0";
					amt3 += parseFloat(l);
				});
			$(".grp-DS").each(function () {
					var ids = this.id.split('-');
					var l = $("#local-" + ids[1]).val();
					if(l == "")
						l = "0";
					amt3 += parseFloat(l);
				});
			$(".grp-TS").each(function () {
					var ids = this.id.split('-');
					var l = $("#local-" + ids[1]).val();
					if(l == "")
						l = "0";
					amt4 += parseFloat(l);
				});
					dis = parseFloat($("#total-dis").val());
					pro = parseFloat($("#total-pro").val());

					
					total = amt1+amt2+amt3+amt4+pro+dis
					
					$("#total-os").val(FormatNumber(amt1,2));
					$("#total-of").val(FormatNumber(amt2,2));
					$("#total-ds").val(FormatNumber(amt3,2));
					$("#total-ts").val(FormatNumber(amt4,2));
					$("#total-dis").val(FormatNumber(dis,2));
					$("#total-pro").val(FormatNumber(pro,2));
					
					var typt= $('#costtype').val();
					if(typt!="Inbound")
						$("#total").val(FormatNumber(total,2));
					if(typt=="Inbound") {
						var itotal = parseFloat($("#total").val());
		
						$("#total-pro").val(FormatNumber(itotal-amt1-amt2-amt3-amt4,2));
					}
			}

						function calc_total_usd()
			{
				var amt1 = 0;
				var amt2 = 0;
				var amt3 = 0;
				var amt4 = 0;
				var dis = 0;
				var pro = 0;
				var total = 0;
				var local = 0;
			
			$(".grp-OS").each(function () {
					var ids = this.id.split('-');
					var l = $("#local-" + ids[1]).val();
					if(l == "")
						l = "0";
					amt1 += parseFloat(l);
				});
			$(".grp-OF").each(function () {
					var ids = this.id.split('-');
					var l = $("#local-" + ids[1]).val();
					if(l == "")
						l = "0";
					amt2 += parseFloat(l);
				});
			$(".grp-AF").each(function () {
					var ids = this.id.split('-');
					var l = $("#local-" + ids[1]).val();
					if(l == "")
						l = "0";
					amt2 += parseFloat(l);
				});
			$(".grp-IB").each(function () {
					var ids = this.id.split('-');
					var l = $("#local-" + ids[1]).val();
					if(l == "")
						l = "0";
					amt3 += parseFloat(l);
				});
			$(".grp-DS").each(function () {
					var ids = this.id.split('-');
					var l = $("#local-" + ids[1]).val();
					if(l == "")
						l = "0";
					amt3 += parseFloat(l);
				});
			$(".grp-TS").each(function () {
					var ids = this.id.split('-');
					var l = $("#local-" + ids[1]).val();
					if(l == "")
						l = "0";
					amt4 += parseFloat(l);
				});
					dis = parseFloat($("#total-dis").val());
					tot2 = parseFloat($("#total2").val());
					// pro = parseFloat($("#total-pro").val());
					var exr = parseFloat($("#exr-ds").val());
					$("#total").val(FormatNumber(tot2 * exr ,2));
					total = parseFloat($("#total").val());
					pro = total - amt1-amt2-amt3- amt4 - dis
					
					$("#total-os").val(FormatNumber(amt1,2));
					$("#total-of").val(FormatNumber(amt2,2));
					$("#total-ds").val(FormatNumber(amt3,2));
					$("#total-ts").val(FormatNumber(amt4,2));
					$("#total-dis").val(FormatNumber(dis,2));
					$("#total-pro").val(FormatNumber(pro,2));
					
			}

			
			function calc_group()
			{
				for(var g=1; g<5; g++) {
						var total = 0;
					
						$(".grp-" + g).each(function () {
							var ids = this.id.split('-');
							var l = $("#local-" + ids[1]).val();
							if(l == "")
								l = "0";
							total += parseFloat(l);
						}
						
						);
						$("#total-grp-" + g).val(FormatNumber(total,2));
				}		
					
			}

			</script>

			<table class=noprint1>
           
			<tr>
			<td width=120>
			Net Vol. (Cuft)
			</td>
			<td width=120>
		<input type="text" style="width:120px;background:LightGreen;" class="cssnet" name='cuft' id='cuft' onchange="change_vol(this);" value='<%= dr0["Value1"]%>' />
			</td>
				<td width=120>
			Gross Vol. (Cuft)
			</td>
			<td width=120>
		<input type="text" style="width:120px" class="csscuft" name='gross' id='gross'  value='<%= dr0["Value2"]%>' />
			</td>
			<td width=120>
			Gross Wt. (lbs)
			</td>
			<td width=120>
<input type="text" style="width:120px" class="csscuft" name='lbs' id='lbs'  value='<%= dr0["Value3"]%>' />
			</td>
			<td></td>
			</tr>
	 
			<tr>
			<td width=120>
			Net Vol. (M3)
			</td>
			<td width=120>
<input type="text" style="width:120px" class="csscuft" name='cbmn' id='cbmn'  value='<%= dr0["Value7"]%>' />
			</td>
			<td width=120>
			Gross Vol. (M3)
			</td>
			<td width=120>
<input type="text" style="width:120px" class="csscuft" name='cbm' id='cbm'  value='<%= dr0["Value5"]%>' />
			</td>
			<td>
			Gross Wt. (kgs)
			</td>
			<td>
<input type="text"  class="cssnet" style="width:120px;background:LightGreen;" name='kgs' id='kgs' onchange="change_kgs(this);" value='<%= dr0["Value4"]%>' />
			</td>
			
			</tr>
			<tr>
			<td colspan=3 align=right>
			<%= (S.Text(dr0["JobNo"]).IndexOf("CON") > 0) ? "Total Consol Vol. (M3)" : "Rounding Vol. (M3) For Shipping" %> 
			</td>
			<td width=120>
<input type="text" style="width:120px" class="csscuft" name='cbmr' id='cbmr'  value='<%= dr0["Value6"]%>' />
			</td>
			<td></td>
			<td></td>
					</tr>
					<% if(jobtype!="Inbound1") {%>
					<tr>
			<td>Rmarks</td>
			<td colspan=6>
			<input type="text" style="width:650px" class="cssnet" name='cost-remark' id='cost-remark'  value='<%= dr0["Marking"]%>' />
			</td>

</tr>
<% } %>
</table>
<table>
<tr>
<% if(jobtype=="Inbound") {%>
			<td width=120>Origin Curr/Ex</td>

<% } else {%>
			<td width=120>Freight Curr/Ex</td>
<% } %>



			<td align=left>
<input type="text" style="width:60px" class="cssnet" name='curr-of' id='curr-of'  onchange="recur(1);" value='<%= dr0["Curr1"]%>' />
			</td>
			<td>
<input type="text" style="width:80px" class="cssnet exrate" name='exr-of' id='exr-of' onchange="recalc();"  value='<%= dr0["ExRate1"]%>' />
			</td>
			
<% if(jobtype=="Inbound") {%>
			<td>Billing Curr/Ex</td>

<% } else {%>
			<td>Dest. Curr/Ex</td>
<% } %>
			<td align=left>
<input type="text" style="width:60px" class="cssnet" name='curr-ds' id='curr-ds' onchange="recur(2);"  value='<%= dr0["Curr2"]%>' />
			</td>
			<td>
<input type="text" style="width:80px" class="cssnet exrate" name='exr-ds' id='exr-ds'  onchange="recalc();"  value='<%= dr0["ExRate2"]%>' />
			</td>
			</tr>
			</table>
<br>
			<table border=1>
<tr>

<% if(jobtype=="Inbound") {%>
			<td>
			Service Type
			</td>

<% } %>
<td align=center style='display:<%= jobtype=="Inbound"?"none":""%>'>Origin Service</td>
<td align=center style='display:<%= jobtype=="Inbound"?"none":""%>'>Trucking Services</td>
<td align=center style='display:<%= jobtype=="Inbound"?"none":""%>'>Freight Charges</td>
<% if(jobtype=="Inbound") {%>
<td align=center>Inbound Service</td>

<% } else {%>
			<td>
			Dest. Service
			</td>
<% } %>
<td align=center>Profit</td>
<td align=center style="display:none">Discount/Add</td>
<td align=center style='display:<%= jobtype!="Inbound"?"none":""%>'>Total Chargeable</td>
<td align=center>Total SGD</td>
</tr>
<tr>

<% if(jobtype=="Inbound") {%>
			<td>
<select class="cssnet" name='cost-remark' id='cost-remark' style="width:160px" >
  <option value="Port To Door" <%= S.Text(dr0["Marking"])=="Port To Door" ? "Selected" : ""%>>Port To Door</option>
  <option value="Door To Door" <%= S.Text(dr0["Marking"])=="Door To Door" ? "Selected" : ""%>>Door To Door</option>
</select>
			</td>

<% } %>

<td align=center style='display:<%= jobtype=="Inbound"?"none":""%>'>
<input type="text" style="width:100px;text-align:center;" readonly="true" class="csscuft" name='total-os' id='total-os'  value='<%= dr0["Amt1"]%>' />
</td>
<td align=center style='display:<%= jobtype=="Inbound"?"none":""%>'>
<input type="text" style="width:100px;text-align:center;" readonly="true" class="csscuft" name='total-ts' id='total-ts'  value='<%= dr0["Amt4"]%>' />
</td>
<td align=center style='display:<%= jobtype=="Inbound"?"none":""%>'>
<input type="text" style="width:100px;text-align:center;" readonly="true" class="csscuft" name='total-of' id='total-of'  value='<%= dr0["Amt2"]%>' />
</td>
<td align=center>
<input type="text" style="width:100px;text-align:center;" readonly="true" class="csscuft" name='total-ds' id='total-ds'  value='<%= dr0["Amt3"]%>' />
</td>
<td align=center>
<input type="text" style="width:100px;text-align:center;background:lightgreen;"  class="csscuft" onchange="calc_total();" name='total-pro' id='total-pro'  value='<%= dr0["ProfitMargin"]%>' />
</td>
<td align=center style="display:none;">
<input type="text" style="width:100px;text-align:center;background:lightgreen;"   class="csscuft" onchange="calc_total();" name='total-dis' id='total-dis'  value='0' />
</td>
<td align=center style='display:<%= jobtype!="Inbound"?"none":""%>'>
<input type="text" style="width:100px;text-align:center;background:lightgreen;"   class="csscuft" onchange="calc_total_usd();" name='total2' id='total2'  value='<%= dr0["Amount2"]%>' />
</td>
<td align=center>
	<input type="text" style="width:120px;text-align:center;font-size:18px;font-weight:bold;border:0px;" class="cssnet"  onchange="calc_total_profit();" name='total' id='total'  value='<%= dr0["Amount"]%>' />
</td>

</tr>
</table>
<br>

			<table border=0 cellspacing=0>
		<%

        string _id = Request.Params["id"] ?? "0";
		string[] grp = {"IB","CB","OS","TS","OF","AF","DS"};
		string[] lbl = {
			"Inbound Services",
			"Consignee Beared Cost",
			"Origin Services",
			"Trucking / To Port",
			"Ocean Freight",
			"Air Freight",
			"Dest. Services"
		};
		
		string allkey = "";
		
		for(int g=0; g<grp.Length; g++)
		{
        string _sql1 = string.Format("Select * from  CostDet where ParentId='{0}' and Status1='"+grp[g]+"'", _id);
		DataTable dt1 = Helper.Sql.List(_sql1);
	
		string showgroup = "";
			if(dt1.Rows.Count==0)
				showgroup="class=hidenow";
			
		%>
		
		
	
		
			<tr <%= showgroup%>>
			<!-- <td colspan=8 id="div-grp-<%= g+1%>"><%= lbl[g] %></td> -->
			</tr>
                <tr <%= showgroup%>>
                    <td colspan=8 align=right>
                        <hr>
                    </td>
                </tr>
                <tr <%= showgroup%>>
                    <td align=left style="padding-left:25px;font-size:16px;"><b><%= lbl[g]%></b></td>
                    <td align=center>Price</td>
                    <td align=center style="display:none">Total</td>
                    <td align=center>Total SGD</td>
                    <td align=center>Remark</td>
                    <td align=center class=noprint>Qty/Vol</td>
                    <td align=center class=noprint>Unit</td>
                    <td align=center class=noprint style="display:none;">Min Charge</td>
                </tr>
                <tr <%= showgroup%>>
                    <td colspan=8 align=right>
                        <hr>
                    </td>
                </tr>

	
			<%
			string grpkey = "";
			int grpcnt = 0;
			
			for(int c=0; c<dt1.Rows.Count; c++)
			{
				grpcnt ++;
				DataRow dr = dt1.Rows[c];
				grpkey += dr["SequenceId"].ToString() + "#" ;
				allkey += dr["SequenceId"].ToString() + "#" ;
			%>
			<tr class=costline>
			<td width=150 style="padding-left:25px;">
			<%= dr["ChgCodeDes"] %>
			<input type=hidden id='linegrp-<%= dr["SequenceId"]%>' value='<%= dr["Status1"]%>'>
			<input type=hidden id='linecode-<%= dr["SequenceId"]%>' value='<%= dr["ChgCode"]%>'>
			</td>
			<td align=center>
			<input type="text" style="width:70px" class="cssprice" onchange="change_row(<%= dr["SequenceId"]%>)" name='price-<%= dr["SequenceId"]%>' id='price-<%= dr["SequenceId"]%>' value='<%= dr["SalePrice"]%>' />
			</td>
			<td align=center style="display:none;">
			<input type="text"  style="width:70px;text-align:right;" readonly="true" class="csstotal" name='total-<%= dr["SequenceId"]%>' id='total-<%= dr["SequenceId"]%>' value='<%= S.Decimal(dr["SaleDocAmt"])%>' />
			</td>
			<td align=center>
			<input type="text" style="width:70px;text-align:right;" readonly="true" class="csslocal grp-<%= g+1 %> grp-<%= dr["Status1"]%>" name='local-<%= dr["SequenceId"]%>' id='local-<%= dr["SequenceId"]%>' value='<%= S.Decimal(dr["SaleLocAmt"])%>' />
			</td>
			<td align=center>
			<input type="text" style="width:220px;background:lightgreen;"  class="cssremark grp-<%= g+1 %>" name='remark-<%= dr["SequenceId"]%>' id='remark-<%= dr["SequenceId"]%>' value='<%= dr["Remark"]%>' />
			</td>
			<td align=center  class=noprint>
			<input type="text" style="width:50px" class="cssqty" onchange="change_row(<%= dr["SequenceId"]%>)" name='qty-<%= dr["SequenceId"]%>' id='qty-<%= dr["SequenceId"]%>' value='<%= dr["SaleQty"]%>' />
			</td>
			<td align=center  class=noprint>
			<input type="text" style="width:50px" readonly="true" class="cssunit" name='unit-<%= dr["SequenceId"]%>' id='unit-<%= dr["SequenceId"]%>' value='<%= dr["Unit"]%>' />
			</td>
			<td align=center  class=noprint style="display:none;">
			<input type="text" style="width:50px" readonly="true" class="cssmin" onchange="change_row(<%= dr["SequenceId"]%>)" name='min-<%= dr["SequenceId"]%>' id='min-<%= dr["SequenceId"]%>' value='<%= dr["CostPrice"]%>' />
			</td>
			</tr>
         
			<%
			}
			%>
		
		<tr <%= showgroup%> style="display:none;">
			<td colspan=2></td>
			<td align=center>
			<input type="text" style="width:80px" class="cssprice" name='total-grp-<%= g+1%>' id='total-grp-<%= g+1%>' value='0.00' />
			</td>
			<td colspan=5>
			<input  type="text" style="width:240px;display:none;" class="cssprice" name='key-grp-<%= g+1%>' id='total-grp-<%= g+1%>' value='<%= grpkey%>' />
			</td>
		</tr>
		
	
		<% } %>
		</table>
		<br>
		<br>
		<br>
		<div class=hidenow>
<br>		<input type="text" style="width:120px;" class="csscuft" name='grp' id='grp'  value='<%= grp.Length%>' />
<br>		<input type="text" style="width:120px" class="csscuft" name='allkey' id='allkey'  value='<%= allkey%>' />

</div>	

		

            
    </form>
</body>
	   
</html>

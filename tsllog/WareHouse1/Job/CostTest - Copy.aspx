<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeFile="CostTest.aspx.cs" Inherits="WareHouse_Job_CostTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <script type="text/javascript" src="/Script/jquery.js" />

    <script type="text/javascript">
        $.noConflict();
    </script>

    <title></title>
    <script type="text/javascript">
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
</head>
<body>
    <form id="form1" runat="server">
        <div><dxe:ASPxTextBox runat="server" ID="txt_Id" ClientVisible="false"></dxe:ASPxTextBox></div>
        <div>
 <h2>LCL ACCRUAL SHEET</h2>
	
			<table>
                <tr>
                    <td>
                       
                    </td>
                    <td width="200"></td>
                    <td>
                        <dxe:ASPxButton runat="server" ID="btn_Save" OnClick="btn_Save_Click" Text="Save All Changes">
                            <ClientSideEvents Click="function(){
                            lab.SetText('Saving...');
                            }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="lab" ClientInstanceName="lab" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
            </table>
		<script>
			
			function change_vol(el)
			{
					var vol = parseFloat($(el).val());
					var gross = vol * 1.2;
					var cbm = FormatNumber(gross / 35.312,3);
					var cbmr = Math.ceil(cbm);
					cbmr = parseInt(cbm);
					if(cbm > cbmr)
						cbmr ++;
					$("#gross").val(gross);
					$("#cbm").val(cbm);
					$("#cbmr").val(cbmr);

$(".cssunit").each(function () {
					var ids = this.id.split('-');
					var k=ids[1];
						var u = $("#unit-" + k).val().toUpperCase();
						//alert(u);
						if(u == "CUFT") {
							$("#qty-" + k).val(vol);
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

			
			function change_row(id)
			{
				//alert($("#qty-" + id).val());
					var qty =	parseFloat($("#qty-" + id).val());
					var price =	parseFloat($("#price-" + id).val());
					var exr =	parseFloat($("#exr-" + id).val());
					var min =	parseFloat($("#min-" + id).val());
					//alert(qty * price);
					var tot = qty * price * exr;
					if(tot < min && tot != 0)
						tot = min;
					$("#total-" + id).val(FormatNumber(tot,2));
						calc_total();
			}

			
			function calc_total()
			{
				var total = 0;
					$(".csstotal").each(function () {
					var ids = this.id.split('-');
					var l = $("#total-" + ids[1]).val();
					if(l == "")
						l = "0";
                total += parseFloat(l);
            });
					
					$("#total").val(FormatNumber(total,2));
					
			}

			function calc_group()
			{
				var total = 0;
					$(".csstotal").each(function () {
					var ids = this.id.split('-');
					var l = $("#total-" + ids[1]).val();
					if(l == "")
						l = "0";
                total += parseFloat(l);
            });
					
					$("#total").val(FormatNumber(total,2));
					
			}

			</script>

		<br>		<input type="text" style="width:120px" class="csscuft" name='cuft' id='cuft' onchange="change_vol(this);" value='0.00' />
Cuft
		<br>		<input type="text" style="width:120px" class="csscuft" name='gross' id='gross'  value='0.00' />
Gross
		<br>		<input type="text" style="width:120px" class="csscuft" name='cbm' id='cbm'  value='0.00' />
Cbm
		<br>		<input type="text" style="width:120px" class="csscuft" name='cbmr' id='cbmr'  value='0.00' />
Cbm Round
<br>		<input type="text" style="width:120px" class="csscuft" name='total' id='total'  value='0.00' />
Total Chargeable
			<%

        string _id = Request.Params["id"] ?? "0";
		string[] grp = {"OS","OF","DS","MS","DD","OH"};
		string[] lbl = {
			"Origin Services",
			"Ocean Freight",
			"Destination Services",
			"Misc Services",
			"Door to Door",
			"Overheight / General"
		};
		
		string allkey = "";
		
		for(int g=0; g<grp.Length; g++)
		{
        string _sql1 = string.Format("Select * from  CostDet where ParentId='{0}' and Status1='"+grp[g]+"'", _id);
		DataTable dt1 = Helper.Sql.List(_sql1);
	
		
		%>
		
		
		<div id="div-grp-<%= g+1%>">
		<h2><%= lbl[g]%></h2><br>
		

		<table border=1>

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
			<tr>
			<td width=250><%= dr["ChgCodeDes"] %></td>
			<td>
			<input type="text" style="width:60px" class="csstotal" name='total-<%= dr["SequenceId"]%>' id='total-<%= dr["SequenceId"]%>' value='<%= dr["SaleDocAmt"]%>' />
			</td>
			<td>
			<input type="text" style="width:60px" class="cssqty" onchange="change_row(<%= dr["SequenceId"]%>)" name='qty-<%= dr["SequenceId"]%>' id='qty-<%= dr["SequenceId"]%>' value='<%= dr["SaleQty"]%>' />
			</td>
			<td>
			<input type="text" style="width:60px" class="cssprice" onchange="change_row(<%= dr["SequenceId"]%>)" name='price-<%= dr["SequenceId"]%>' id='price-<%= dr["SequenceId"]%>' value='<%= dr["SalePrice"]%>' />
			</td>
			<td>
			<input type="text" style="width:60px" class="cssexr" onchange="change_row(<%= dr["SequenceId"]%>)" name='exr-<%= dr["SequenceId"]%>' id='exr-<%= dr["SequenceId"]%>' value='<%= dr["SaleExRate"]%>' />
			</td>
			<td>
			<input type="text" style="width:60px" class="cssmin" onchange="change_row(<%= dr["SequenceId"]%>)" name='min-<%= dr["SequenceId"]%>' id='min-<%= dr["SequenceId"]%>' value='<%= dr["CostPrice"]%>' />
			</td>
			<td>
			<input type="text" style="width:60px" class="cssunit" name='unit-<%= dr["SequenceId"]%>' id='unit-<%= dr["SequenceId"]%>' value='<%= dr["Unit"]%>' />
			</td>
			</tr>
			<%
			}
			%>
		
		</table>

		<input type="text" style="width:120px" class="cssprice" name='total-grp-<%= g+1%>' id='total-grp-<%= g+1%>' value='0.00' />
			<input type="text" style="width:240px" class="cssprice" name='key-grp-<%= g+1%>' id='total-grp-<%= g+1%>' value='<%= grpkey%>' />
		
	
		</div>
		<% } %>
<br>		<input type="text" style="width:120px" class="csscuft" name='grp' id='grp'  value='<%= grp.Length%>' />
<br>		<input type="text" style="width:120px" class="csscuft" name='allkey' id='allkey'  value='<%= allkey%>' />

	

		

            
    </form>
</body>
</html>

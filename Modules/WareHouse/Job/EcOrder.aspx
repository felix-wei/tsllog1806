<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="EcOrder.aspx.cs" Inherits="Modules_EcOrder" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> </title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style>
        th
        {
            font-weight: bold;
            text-align:center;
            padding:4px;
	 background-color:#EEEEEE;
        }
        td
        {
            padding:2px;
        }
        td.qty
        {
            background-color:lightgreen;
            text-align:center;
        }
        th.qty
        {
            background-color:lightgreen;

        }
        input
        {
            text-align:center;
            width:40px;
        }
    </style>
	<script type="text/javascript" src="/_lib/base/jquery.js"></script>
	<script type="text/javascript" src="/_lib/dx/grid.js"></script>
	<script type="text/javascript">
	    $.noConflict();
	    jQuery(document).ready(function ($) {

	    });
	    function ClosePopupCtr() {
	        popubCtr.Hide();
	        popubCtr.SetContentUrl('about:blank');
	    }

	    function OpenAttach(id,typ) {
	        pop.SetHeaderText('Attachments / Notes');
	        pop.SetContentUrl('/modules/Attach.aspx?type=' + typ + '&id=' + id);
	        pop.Show();
	    }
	    function CloseAttach() {
	        pop.Hide();
	        pop.SetContentUrl('about:blank');
	        grid1.Refresh();
	    }
	    function PopupInventory(code) {
	        popubCtr.SetHeaderText('SKU Inventory');
	        popubCtr.SetContentUrl('/Modules/WareHouse/SelectPage/SkuInventory.aspx?Sku=' + code);
	        popubCtr.Show();
	    }
	    function CreateNewOrder(code,custId) {
	        popubCtr.SetHeaderText('SKU Inventory');
	        popubCtr.SetContentUrl('/Modules/WareHouse/Job/CreateNewOrder.aspx?Sku=' + code+'&custId='+custId);
	        popubCtr.Show();
	    }
	    function AfterPopubMultiInv() {
	        popubCtr.Hide();
	        popubCtr.SetContentUrl('about:blank');
	    }
	    function AddOrder() {
	        //btn_search.click();
	        document.getElementById("btn_search").click();
	    }
	</script>
</head>
<body style="margin:8px;">
    <form id="form1" runat="server">
    <div>
 
        <%
//             string sql = string.Format(@"select DISTINCT p.Att4 as Att1, p.Att5 as Att2, p.Att6 as Att3, p.Att7 as Att4, p.Att8 as Att5, p.Att9 as Att6, p.Description,p.Code as ProductCode, d1.Qty5 as IncomingQty, d2_In.Qty as InQty,
// d1.QtyPackWhole, ISNULL(d2_Out.qty,0) as OutQty, '' as LotNo,0 as Qty1,
//ISNULL((select sum(det.Qty1) from Wh_TransDet det left join Wh_Trans mast on det.DoNo=mast.DoNo and mast.DoStatus='Confirmed' where det.ProductCode=d1.ProductCode and mast.DoType='SO' group by ProductCode),0) as WipQty, 
//ISNULL((select sum(det.Qty1) from Wh_TransDet det left join Wh_Trans mast on det.DoNo=mast.DoNo and mast.DoStatus='Confirmed' where det.ProductCode=d1.ProductCode  and mast.DoType='PO' group by ProductCode),0) as WipQtyIn, 
//(select sum(wd.Qty1) from Wh_TransDet wd, Wh_Trans wt where wt.DoType='PO' and wt.DoStatus='Draft' and wt.DoNo=wd.DoNo and wd.ProductCode=d1.ProductCode) as PoQty,
//(select sum(wd.Qty1) from Wh_TransDet wd, Wh_Trans wt where wt.DoType='SO' and wt.DoStatus='Draft' and wt.DoNo=wd.DoNo and wd.ProductCode=d1.ProductCode) as SoQty
//from Wh_DoDet as d1
//inner join Wh_DO d3 on d1.DoNo=d3.DoNo
//left join ref_product p on p.Code=d1.ProductCode
//left outer join (select Product,sum(qty1) qty from Wh_DoDet2 where DoType='In' group by Product) as d2_In on d1.ProductCode=d2_In.Product
//left outer join (select Product,sum(qty1) qty from Wh_DoDet2 where DoType='OUT' group by Product) as d2_Out on d1.ProductCode=d2_Out.Product 
//where d1.DoType='IN' Union All
//select p.Att4 as Att1, p.Att5 as Att2, p.Att6 as Att3, p.Att7 as Att4, p.Att8 as Att5, p.Att9 as Att6,
//p.description as Des1, p.Code as ProductCode,
//(select sum(Qty5) from Wh_DoDet where DOType='IN' and ProductCode=p.Code) as IncomingQty, 0 as InQty, p.QtyPackingWhole as QtyPackWhole, 0 as OutQty, '' as LotNo, 0 as Qty1,
//(select sum(det.Qty1) from Wh_TransDet det left join Wh_Trans mast on det.DoNo=mast.DoNo and mast.DoStatus='Confirmed' where det.ProductCode=p.Code and mast.DoType='SO' group by ProductCode) as WipQty,
//(select sum(det.Qty1) from Wh_TransDet det left join Wh_Trans mast on det.DoNo=mast.DoNo and mast.DoStatus='Confirmed' where det.ProductCode=p.Code and mast.DoType='PO' group by ProductCode) as WipQtyIn,
//(select sum(wd.Qty1) from Wh_TransDet wd, Wh_Trans wt where wt.DoType='PO' and wt.DoStatus='Draft' and wt.DoNo=wd.DoNo and wd.ProductCode=p.Code) as PoQty,
//(select sum(wd.Qty1) from Wh_TransDet wd, Wh_Trans wt where wt.DoType='SO' and wt.DoStatus='Draft' and wt.DoNo=wd.DoNo and wd.ProductCode=p.Code) as SoQty
//from ref_Product p 
//			");
            string user = HttpContext.Current.User.Identity.Name;
            string sql = string.Format(@"select DISTINCT p.Att4 as Att1, p.Att5 as Att2, p.Att6 as Att3, p.Att7 as Att4, p.Att8 as Att5, p.Att9 as Att6, p.Description,p.Code as ProductCode, tab_Incoming.Qty as IncomingQty, d2_In.Qty as InQty,
  p.QtyPackingWhole as QtyPackWhole, ISNULL(d2_Out.qty,0) as OutQty, '' as LotNo,0 as Qty1,
ISNULL((select sum(det.Qty1) from Wh_TransDet det left join Wh_Trans mast on det.DoNo=mast.DoNo and mast.DoStatus='Confirmed' where det.ProductCode=d1.ProductCode and mast.DoType='SO' group by ProductCode),0) as WipQty, 
ISNULL((select sum(det.Qty1) from Wh_TransDet det left join Wh_Trans mast on det.DoNo=mast.DoNo and mast.DoStatus='Confirmed' where det.ProductCode=d1.ProductCode  and mast.DoType='PO' group by ProductCode),0) as WipQtyIn, 
(select sum(wd.Qty1) from Wh_TransDet wd, Wh_Trans wt where wt.DoType='PO' and wt.DoStatus='Draft' and wt.DoNo=wd.DoNo and wd.ProductCode=d1.ProductCode) as PoQty,
(select sum(wd.Qty1) from Wh_TransDet wd, Wh_Trans wt where wt.DoType='SO' and wt.DoStatus='Draft' and wt.DoNo=wd.DoNo and wd.ProductCode=d1.ProductCode) as SoQty
,tab_user.CustId
from Wh_DoDet as d1
inner join Wh_DO d3 on d1.DoNo=d3.DoNo
left join ref_product p on p.Code=d1.ProductCode
left outer join (select Product,sum(qty1) qty from Wh_DoDet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  where det.DoType='IN' and len(det.DoNo)>0  group by Product) as d2_In on d1.ProductCode=d2_In.Product
left outer join (select Product,sum(qty1) qty from Wh_DoDet2 det inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' and len(det.DoNo)>0 group by Product) as d2_Out on d1.ProductCode=d2_Out.Product 
left join (select ProductCode,sum(Qty5) as Qty from Wh_DoDet where DoType='IN' group by ProductCode) as tab_Incoming on tab_Incoming.ProductCode=d1.ProductCode 
left join (select CustId,Name from [dbo].[User] ) as tab_user on  tab_user.Name='{0}'
where d1.DoType='IN' order by p.Code asc",user);
		//throw new Exception(sql);			
            DataTable dt = Helper.Sql.List(sql);
            
             %>

        <table width="900">
            <tr>
                <td>
                </td>
                <td style="display:none">
                    <input type="hidden" value="<%= _page %>" id="ctl_page"/>                                       
                </td>
                <td>
               
                    <h1>Quick Order Entry Form</h1>
                    <hr />
                            <%
                                string sql_sum = string.Format(@"select DISTINCT p.Att4 as Att1, p.Att5 as Att2, p.Att6 as Att3, p.Att7 as Att4, p.Att8 as Att5, p.Att9 as Att6, p.Description,p.Code as ProductCode, tab_Incoming.Qty as IncomingQty, d2_In.Qty as InQty,
  p.QtyPackingWhole as QtyPackWhole, ISNULL(d2_Out.qty,0) as OutQty, '' as LotNo,0 as Qty1,tab_Po.PoQty,tab_So.SoQty,tab_block.WipQty,tab_WipQtyIn.WipQtyIn,tab_user.CustId from Wh_DoDet as d1
inner join Wh_DO d3 on d1.DoNo=d3.DoNo
left join ref_product p on p.Code=d1.ProductCode
left outer join (select Product,sum(qty1) qty from Wh_DoDet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  where det.DoType='IN' and len(det.DoNo)>0  group by Product) as d2_In on d1.ProductCode=d2_In.Product
left outer join (select Product,sum(qty1) qty from Wh_DoDet2 det inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' and len(det.DoNo)>0 group by Product) as d2_Out on d1.ProductCode=d2_Out.Product 
left join (select ProductCode,sum(Qty5) as Qty from Wh_DoDet where DoType='IN' group by ProductCode) as tab_Incoming on tab_Incoming.ProductCode=d1.ProductCode
left join (select sum(wd.Qty1)as PoQty,max(ProductCode) as ProductCode from Wh_TransDet wd, Wh_Trans wt where wt.DoType='PO' and wt.DoStatus='Draft' and wt.DoNo=wd.DoNo )as tab_Po on tab_Po.ProductCode=d1.ProductCode
left join (select sum(wd.Qty1)as SoQty,max(ProductCode) as ProductCode from Wh_TransDet wd, Wh_Trans wt where wt.DoType='SO' and wt.DoStatus='Draft' and wt.DoNo=wd.DoNo )as tab_So on tab_So.ProductCode=d1.ProductCode
left join (select sum(det.Qty1) WipQty,ProductCode from Wh_TransDet det left join Wh_Trans mast on det.DoNo=mast.DoNo and mast.DoStatus='Confirmed' where mast.DoType='SO' group by ProductCode) as tab_block on tab_block.ProductCode=d1.ProductCode
left join (select sum(det.Qty1) as WipQtyIn,ProductCode from Wh_TransDet det left join Wh_Trans mast on det.DoNo=mast.DoNo and mast.DoStatus='Confirmed' where  mast.DoType='PO' group by ProductCode) as tab_WipQtyIn on tab_WipQtyIn.ProductCode=d1.ProductCode
left join (select CustId,Name from [dbo].[User] ) as tab_user on  tab_user.Name='{0}'
where d1.DoType='IN' and (PoQty>0 or SoQty>0 or tab_Incoming.Qty>0 or WipQty>0) order by p.Code asc", user);
                                                                                                    
            DataTable dt_sum = Helper.Sql.List(sql_sum); 
         %>
                    <table border="2" style="border-collapse:collapse" width="900">
           
            <tr>
                <th colspan="10">Product Catalogue</th>
                <th colspan="2">Pending Order</th>
                <th colspan="5">Stock Availability</th>
            </tr>
            <tr>
                <th>#</th>
                <th>CODE</th>
                <%--<th>CUSTOMER ID</th>--%>
                <th>DECRIPTION</th>
                <th>PKG</th>
                <th>ML</th>
                <th>AOL</th>
                <th>CO</th>
                <th>NRF/REF</th>
                <th>GBX</th>
                <th>DECODED</th>
                <th>PO</th>
                <th>SO</th>
                <th>ON HAND</th>
                <th>INCOMING</th>
                <th>BLOCKED</th>
                <th>AVAILABLE</th>

            </tr>
            <%
                for (int i = 0; i < dt_sum.Rows.Count; i++)
                {
                    DataRow dr_sum = dt_sum.Rows[i];
                 %>
            <tr >
                <td><%= i+1 %></td>
                <td ><a  href="#" onclick="PopupInventory('<%= dr_sum["ProductCode"] %>');"><%= dr_sum["ProductCode"] %></a>    </td>
                <%--<td><%= dr_sum["CustId"] %></td>--%>
                <td><%= dr_sum["Description"] %></td>
                <td><%= dr_sum["QtyPackWhole"] %></td>
                <td><%= dr_sum["Att1"] %></td>
                <td><%= dr_sum["Att2"] %></td>
                <td><%= dr_sum["Att3"] %></td>
                <td><%= dr_sum["Att4"] %></td>
                <td><%= dr_sum["Att5"] %></td>
                <td><%= dr_sum["Att6"] %></td>
                <td><%= dr_sum["PoQty"] %></td>
                <td><%= dr_sum["SoQty"] %></td>
                <td><%= Helper.Safe.SafeInt(dr_sum["InQty"]) - Helper.Safe.SafeInt(dr_sum["OutQty"]) %></td>
                <td style='<%= Helper.Safe.SafeInt(dr_sum["WipQtyIn"]) > 0 ? "background-color:lightblue":"" %>'><%= Helper.Safe.SafeInt(dr_sum["WipQtyIn"]) %></td>
                <td style='<%= Helper.Safe.SafeInt(dr_sum["WipQty"]) > 0 ? "background-color:orange":"" %>'><%= Helper.Safe.SafeInt(dr_sum["WipQty"]) %></td>
                <td style='<%= Helper.Safe.SafeInt(Helper.Safe.SafeInt(dr_sum["InQty"])  - Helper.Safe.SafeInt(dr_sum["OutQty"]) + Helper.Safe.SafeInt(dr_sum["WipQtyIn"]) - Helper.Safe.SafeInt(dr_sum["WipQty"])) < 0 ? "background-color:red":"" %>'><%= Helper.Safe.SafeInt(Helper.Safe.SafeInt(dr_sum["InQty"])  - Helper.Safe.SafeInt(dr_sum["OutQty"]) + Helper.Safe.SafeInt(dr_sum["WipQtyIn"]) - Helper.Safe.SafeInt(dr_sum["WipQty"]),0) %></td>
                <td style=" display:none">
                   <%-- <input type="hidden" name="line<%= i+1 %>p" value='<%= dr_sum["ProductCode"] %>' />
                    <input type="hidden" name="line<%= i+1 %>b" value='<%= dr_sum["LotNo"] %>' />
                    <input type="hidden" name="line<%= i+1 %>q" value='<%= dr_sum["Qty1"] %>' />
                    <input type="hidden" name="line<%= i+1 %>w" value='<%= dr_sum["QtyPackWhole"] %>' />--%>
                </td>
            </tr>
            <% } %>
    

        </table>
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" Width="150" runat="server" Text="Create New Order" onclick="submit_order" />
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_refresh" Width="150" runat="server" Text="Refresh" >
                                    <ClientSideEvents Click="function(s,e){
                        window.location='EcOrder.aspx';
                    }" />
                                    </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Label runat="server" ForeColor="Green" ID="msg" Font-Size="Medium"></asp:Label>

        <table border="2" style="border-collapse:collapse" width="900">
           
            <tr>
                <th colspan="10">Product Catalogue</th>
                <th colspan="2">Pending Order</th>
                <th colspan="4">Stock Availability</th>
                <th class="qty" colspan="2">To Order</th>
            </tr>
            <tr>
                <th>#</th>
                <th>CODE</th>
                <%--<th>CUSTOMER ID</th>--%>
                <th>DECRIPTION</th>
                <th>PKG</th>
                <th>ML</th>
                <th>AOL</th>
                <th>CO</th>
                <th>NRF/REF</th>
                <th>GBX</th>
                <th>DECODED</th>
                <th>PO</th>
                <th>SO</th>
                <th>ON HAND</th>
                <th>INCOMING</th>
                <th>BLOCKED</th>
                <th>AVAILABLE</th>
                <th class="qty">Qty</th>

            </tr>
            <%
                for(int i=0; i< dt.Rows.Count; i++) {
                    DataRow dr = dt.Rows[i];
                 %>
            <tr >
                <td><%= i+1 %></td>
                <td ><a  href="#" onclick="PopupInventory('<%= dr["ProductCode"] %>');"><%= dr["ProductCode"] %></a>    </td>
                <%--<td><a  href="#" onclick="AddOrder();"><%= dr["CustId"] %></a></td>--%>
                <td><%= dr["Description"] %></td>
                <td><%= dr["QtyPackWhole"] %></td>
                <td><%= dr["Att1"] %></td>
                <td><%= dr["Att2"] %></td>
                <td><%= dr["Att3"] %></td>
                <td><%= dr["Att4"] %></td>
                <td><%= dr["Att5"] %></td>
                <td><%= dr["Att6"] %></td>
                <td><%= dr["PoQty"] %></td>
                <td><%= dr["SoQty"] %></td>
                <td><%= Helper.Safe.SafeInt(dr["InQty"]) - Helper.Safe.SafeInt(dr["OutQty"]) %></td>
                <td style='<%= Helper.Safe.SafeInt(dr["WipQtyIn"]) > 0 ? "background-color:lightblue":"" %>'><%= Helper.Safe.SafeInt(dr["WipQtyIn"]) %></td>
                <td style='<%= Helper.Safe.SafeInt(dr["WipQty"]) > 0 ? "background-color:orange":"" %>'><%= Helper.Safe.SafeInt(dr["WipQty"]) %></td>
                <td style='<%= Helper.Safe.SafeInt(Helper.Safe.SafeInt(dr["InQty"])  - Helper.Safe.SafeInt(dr["OutQty"]) + Helper.Safe.SafeInt(dr["WipQtyIn"]) - Helper.Safe.SafeInt(dr["WipQty"])) < 0 ? "background-color:red":"" %>'><%= Helper.Safe.SafeInt(Helper.Safe.SafeInt(dr["InQty"])  - Helper.Safe.SafeInt(dr["OutQty"]) + Helper.Safe.SafeInt(dr["WipQtyIn"]) - Helper.Safe.SafeInt(dr["WipQty"]),0) %></td>
                <td class="qty" style="display:none;"><input type="text" name="line<%= i+1 %>" width="6" /></td>
                <td class="qty"><input type="text" name="line<%= i+1 %>a" width="6" />
                    <input type="hidden" name="line<%= i+1 %>p" value='<%= dr["ProductCode"] %>' />
                    <input type="hidden" name="line<%= i+1 %>b" value='<%= dr["LotNo"] %>' />
                    <input type="hidden" name="line<%= i+1 %>q" value='<%= dr["Qty1"] %>' />
                    <input type="hidden" name="line<%= i+1 %>w" value='<%= dr["QtyPackWhole"] %>' />
                </td>
            </tr>
            <% } %>
    

        </table>


            </div>

        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="400"
            AllowResize="True" Width="700" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {

                }" />
        </dxpc:ASPxPopupControl>

    </form>
</body>
</html>

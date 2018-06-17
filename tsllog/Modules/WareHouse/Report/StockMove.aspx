<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockMove.aspx.cs" Inherits="WareHouse_Report_StockMove" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Stock Move</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <style type="text/css">
        body {
        font-family:Arial;
        font-size:12px;
        }
  .td_border{
  border-top:solid #999999 1px;
  border-right:solid #999999 1px;}
   .td_border_1{
  border-top:solid #000000 1px;
  border-right:solid #999999 1px;}
</style>
</head>

<body>
    <form runat="server" id="form1">
        <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefWarehouse" KeyMember="Id" />
        <table>
            <tr>
                <td>SKU
                </td>
                <td>
                    <dxe:ASPxButtonEdit ID="txt_SKULine_Product" ClientInstanceName="txt_SKULine_Product" runat="server" Width="120">
                        <Buttons>
                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s, e) {
                                PopupProduct(txt_SKULine_Product,null);
                            }" />
                    </dxe:ASPxButtonEdit>
                </td>
                <td>LotNo</td>
                <td>
                    <dxe:ASPxTextBox runat="server" Width="100px" ID="txt_LotNo"
                        ClientInstanceName="txt_LotNo">
                    </dxe:ASPxTextBox>
                </td>
                <td>From
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>To
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                
            </tr>
            <tr>
                <td>WareHouse</td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_WareHouse" ClientInstanceName="cmb_WareHouse" runat="server"
                        Width="120px" DropDownWidth="200" DropDownStyle="DropDownList" DataSourceID="dsRefWarehouse"
                        ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                        </Columns>
                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                 cmb_loc.PerformCallback();
                                 }" />
                    </dxe:ASPxComboBox>
                </td>
                <td>Location</td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_loc" ClientInstanceName="cmb_loc" runat="server" OnCallback="cmb_loc_Callback"
                        Width="100px" DropDownWidth="200" DropDownStyle="DropDownList">
                    </dxe:ASPxComboBox>
                </td>
                <td>Customer
                </td>
                <td>
                    <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="120" HorizontalAlign="Left" AutoPostBack="False">
                        <Buttons>
                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupParty(txt_CustId,txt_CustName,null,null,null,null,null,null,'C');
                                }" />
                    </dxe:ASPxButtonEdit>
                </td>
                <td colspan="8">
                    <table width="100%">
                        <tr>
                            <td>
                                <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="100%" runat="server">
                                </dxe:ASPxTextBox>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                                </dxe:ASPxButton>
                            </td>
                            <td></td>
                            <td>
                                <dxe:ASPxButton ID="btnPrint" runat="server" Text="Export To Excel" Width="120" AutoPostBack="False" OnClick="btnPrint_Click">
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%" cellspacing="0" cellpadding="0" style="border-bottom:solid #000000 1px;">
            <tr style="background-color: #999999; font-size: 18px; font-weight: bold; text-align: center">
                <td colspan="20">Stock Movement Report</td>
            </tr>
            <tr style="background-color: #CCCCCC; text-align: center">
                <td width="40" rowspan="2" class="td_border_1">#</td>
                <td width="80" rowspan="2" class="td_border_1">Location Code </td>
                <td rowspan="2" class="td_border_1" width="100">Permit No</td>
                <td rowspan="2" class="td_border_1" width="100">Inv.No</td>
                <td rowspan="2" class="td_border_1" width="100">LotNo</td>
                <td rowspan="2" class="td_border_1" width="100">SKU</td>
                <td rowspan="2" class="td_border_1" width="200">Description</td>
                <td rowspan="2" class="td_border_1" width="100">Case/Pallet</td>
                <td rowspan="2" class="td_border_1" width="100">Size</td>
                <td colspan="2" class="td_border_1">Weight</td>
                <td colspan="3" class="td_border_1">Incoming</td>
                <td colspan="3" class="td_border_1">Outgoing</td>
                <td rowspan="2" class="td_border_1" width="150">Supplier DO </td>
                <td rowspan="2" class="td_border_1" width="100">Bal Qty </td>
                <td rowspan="2" class="td_border_1" width="100">Remark</td>
            </tr>
            <tr style="background-color: #CCCCCC; text-align: center">
                <td class="td_border_1" width="100">Gross</td>
                <td class="td_border_1" width="100">Nett</td>
                <td class="td_border_1" width="100">Date</td>
                <td class="td_border_1" width="100">Pallet</td>
                <td class="td_border_1" width="100">Grum/Bag/Pc</td>
                <td class="td_border_1" width="100">Date</td>
                <td class="td_border_1" width="100">Pallet</td>
                <td class="td_border_1" width="100">Brum/Bag/Pc</td>
            </tr>
            <%
                DataTable tab=GetData();
                int n = 0;
                decimal handQty = 0;
                for (int i = 0; i < tab.Rows.Count;i++ )
                {
                   int no = SafeValue.SafeInt(tab.Rows[i]["No"],0);
                   decimal inQty = SafeValue.SafeDecimal(tab.Rows[i]["InQty"]);
                   decimal outQty = SafeValue.SafeDecimal(tab.Rows[i]["OutQty"]);
                   if (n!=no)
                   {
                       handQty = inQty - outQty;                                    
              %>

            <tr>
                <td class="td_border_1" style="text-align:center"><%=SafeValue.SafeString(tab.Rows[i]["No"]) %></td>
                <td class="td_border_1" style="text-align:center"><%=SafeValue.SafeString(tab.Rows[i]["Location"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PermitNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PartyInvNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["LotNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Product"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Des1"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PalletNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Att3"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["GrossWeight"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["NettWeight"]) %></td>
                <td class="td_border_1"><%=Helper.Safe.SafeDateTimeStr(tab.Rows[i]["InDate"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["InPallet"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["InQty"]) %></td>
                <td class="td_border_1"><%=Helper.Safe.SafeDateTimeStr(tab.Rows[i]["OutDate"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["OutPallet"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["OutQty"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PoNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(handQty) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Remark"]) %></td>
            </tr>
            <%}else{
                  handQty = handQty - outQty;
                  %>
            <tr>
                <td class="td_border">&nbsp;</td>
                <td class="td_border">&nbsp;-</td>
                <td class="td_border">&nbsp;-</td>
                <td class="td_border">&nbsp;-</td>
                <td class="td_border">&nbsp;-</td>
                <td class="td_border">&nbsp;-</td>
                <td class="td_border">&nbsp;-</td>
                <td class="td_border">&nbsp;-</td>
                <td class="td_border">&nbsp;-</td>
                <td class="td_border">&nbsp;-</td>
                <td class="td_border">&nbsp;-</td>
                <td class="td_border">&nbsp;-</td>
                <td class="td_border">&nbsp;-</td>
                <td class="td_border">&nbsp;-</td>
                <td class="td_border"><%=Helper.Safe.SafeDateTimeStr(tab.Rows[i]["OutDate"]) %></td>
                <td class="td_border"><%=SafeValue.SafeDecimal(tab.Rows[i]["OutPallet"]) %></td>
                <td class="td_border"><%=SafeValue.SafeDecimal(tab.Rows[i]["OutQty"]) %></td>
                <td class="td_border"></td>
                <td class="td_border"><%=SafeValue.SafeDecimal(handQty) %></td>
                <td class="td_border"></td>
            </tr>
            <%}
                    n = no;
            } %>
        </table>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
            AllowResize="True" Width="900" EnableViewState="False">
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>

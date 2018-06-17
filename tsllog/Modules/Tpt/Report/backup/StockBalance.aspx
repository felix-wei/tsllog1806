<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockBalance.aspx.cs" Inherits="Modules_Tpt_Report_StockBalance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 12px;
        }
         .td_border {
            border-top: solid #999999 1px;
            border-right: solid #999999 1px;
        }
          .td_border_2 {
            border-top: solid #000000 1px;
        }
        .td_border_1 {
            border-top: solid #000000 1px;
            border-right: solid #999999 1px;
        }
        td.sn {
            text-align: center;
            width: 60px;
        }

        td.text {
            text-align: left;
            width: 260px;
        }

        td.text1 {
            text-align: left;
            width: 80px;
        }

        td.text2 {
            text-align: left;
            width: 130px;
        }

        td.text3 {
            text-align: left;
            width: 200px;
        }

        td.head {
            text-align: center;
            font-weight: bold;
            background-color: #CCCCCC;
            border-right: solid #999999 1px;
        }
    </style>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function process_inline(id){
            popubCtr.SetHeaderText('Process ');
            popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/ProcessForCargo.aspx?id=' + id+'&no=0');
            popubCtr.Show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
         <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefWarehouse" KeyMember="Id" />
        <div>
            <table>
                <tr>
                    <td>Job No</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_JobNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>LotNo</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_LotNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Hbl No</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_HblNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Inventory ID</td>
                    <td> <dxe:ASPxTextBox ID="txt_InventoryId" Width="120" runat="server">
                        </dxe:ASPxTextBox></td>
                    </tr>
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
                    <td>
                        Cargo Type
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_CargoType" runat="server" Width="120">
                            <Items>
                                <dxe:ListEditItem Text="" Value="" Selected="true" />
                                <dxe:ListEditItem Text="Storage" Value="Storage" />
                                <dxe:ListEditItem Text="Delivery" Value="Delivery" />
                                <dxe:ListEditItem Text="Transit" Value="Transit" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_Date" Width="120" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>Marking</td>
                    <td> <dxe:ASPxTextBox ID="txt_Marking" Width="120" runat="server">
                        </dxe:ASPxTextBox></td>
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
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Location</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Location" Width="120" runat="server">
                        </dxe:ASPxTextBox>
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
                    <td colspan="2">
                        <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="150" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    

                    
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnPrint" runat="server" Text="Export To Excel" Width="120" AutoPostBack="False" OnClick="btnPrint_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <div>
                <table width="100%" cellspacing="0" cellpadding="0" style="border-bottom:solid #000000 1px; width:1500px">
            <tr style="background-color: #999999; font-size: 18px; font-weight: bold; text-align: center;line-height:35px">
                <td colspan="25">Stock Balance Report</td>
            </tr>
            <tr style="background-color: #CCCCCC; text-align: center">
                <td rowspan="2" class="td_border_1 sn">#</td>
                <td rowspan="2" class="td_border_1 text1 head">Client</td>
                <td rowspan="2" class="td_border_1 text3 head">JobNo</td>
                <td rowspan="2" class="td_border_1 text1 head">Warehouse</td>
                <td rowspan="2" class="td_border_1 text1 head">Whs Type</td>
                <td rowspan="2" class="td_border_1 text1 head">Permit No</td>
                <td rowspan="2" class="td_border_1 text1 head">Location</td>
                <td rowspan="2" class="td_border_1 text1 head">Lot No</td>
                <td rowspan="2" class="td_border_1 text head">Hbl No</td>
                <td class="td_border_1 text2 head" rowspan="2">Cont No</td>
                <td class="td_border_1 text1 head"" rowspan="2">Type</td>
                <td class="td_border_1 text1 head" rowspan="2">Date</td>
                <td class="td_border_1 text3 head" rowspan="2">SKU Code</td>
                <td class="td_border_1" colspan="7">Balance Info</td>
                
                <td class="td_border_1 text3 head" rowspan="2">Marking</td>
                <td class="td_border_1 text3 head" rowspan="2">Description</td>
                <td class="td_border_1 text3 head" rowspan="2">Remark</td>
            </tr>
            <tr style="background-color: #CCCCCC; text-align: center">
<%--                <td class="td_border_1" width="100">Qty</td>
                <td class="td_border_1" width="100">Uom</td>
                <td class="td_border_1" width="100">Weight</td>
                <td class="td_border_1" width="100">Volume</td>
                <td class="td_border_1" width="150">SKU Qty</td>
                <td class="td_border_1" width="100">Uom</td>--%>
                <td class="td_border_1 text1">Qty</td>
                <td class="td_border_1 text1">Uom</td>
                <td class="td_border_1 text1">Weight</td>
                <td class="td_border_1 text1">Volume</td>
                <td class="td_border_1 text1">SKU Qty</td>
                <td class="td_border_1 text1">Uom</td>
                <td class="td_border_1 text1">#</td>
            </tr>
            <%
                string dateTo = "";
                decimal totalQty = 0;
                decimal totalWeight= 0;
                decimal totalVolume = 0;
                decimal totalSkuQty = 0;
                if (txt_Date.Value != null)
                {
                    dateTo = txt_Date.Date.ToString("yyyy-MM-dd");
                }
                DataTable tab = DataTab;
                if (DataTab != null)
                {
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
              %>
            <tr style="line-height:30px;">
                <td class="td_border_1"><%=i + 1 %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PartyName"]) %></td>
                <td class="td_border_1"><a href='javascript: parent.navTab.openTab("<%=Helper.Safe.SafeString(tab.Rows[i]["JobNo"]) %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%=Helper.Safe.SafeString(tab.Rows[i]["JobNo"]) %>",{title:"<%=Helper.Safe.SafeString(tab.Rows[i]["JobNo"]) %>", fresh:false, external:true});'><%=Helper.Safe.SafeString(tab.Rows[i]["JobNo"]) %></a></td>
                <td class="td_border_1"><%=Helper.Safe.SafeString(tab.Rows[i]["WareHouseCode"]) %></td>
                <td class="td_border_1"><%=Helper.Safe.SafeString(tab.Rows[i]["WhsType"]) %></td>
                <td class="td_border_1"><%=Helper.Safe.SafeString(tab.Rows[i]["PermitNo"]) %></td>
                <td class="td_border_1"><%=Helper.Safe.SafeString(tab.Rows[i]["Location"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["BookingNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["HblNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["ContNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["OpsType"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDateStr(tab.Rows[i]["StockDate"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["SkuCode"]) %></td>
<%--                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["QtyOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackTypeOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["WeightOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["VolumeOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["PackQty"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackUom"]) %></td>--%>
                <td class="td_border_1"><%=SafeValue.ChinaRound(SafeValue.SafeDecimal(tab.Rows[i]["BalQty"]),1) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackTypeOrig"]) %></td>
                <td class="td_border_1"><%=BalanceWeight(tab.Rows[i]["LineId"]) %></td>
                <td class="td_border_1"><%=BalanceVolume(tab.Rows[i]["LineId"]) %></td>
                <td class="td_border_1"><%=SafeValue.ChinaRound(SafeValue.SafeDecimal(tab.Rows[i]["SkuQty"]),1)  %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackUom"]) %></td>
                <td class="td_border_1"><input type="button" class="button" value="Process" onclick="process_inline(<%=tab.Rows[i]["LineId"] %>)" /></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Marking1"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Marking2"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Remark1"]) %></td>
            </tr>           
            <% 
        totalQty += SafeValue.SafeDecimal(tab.Rows[i]["BalQty"]);
        totalWeight += BalanceWeight(tab.Rows[i]["LineId"]);
        totalVolume += BalanceVolume(tab.Rows[i]["LineId"]);
        totalSkuQty += SafeValue.SafeDecimal(tab.Rows[i]["SkuQty"]);
    } %>
                    <tr>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_1"></td>
                        <td class="td_border_1"><%=SafeValue.ChinaRound(totalQty,1)  %></td>
                        <td class="td_border_1"></td>
                        <td class="td_border_1"><%=totalWeight  %></td>
                        <td class="td_border_1"><%=totalVolume  %></td>
                        <td class="td_border_1"><%=SafeValue.ChinaRound(totalSkuQty,1)  %></td>
                        <td class="td_border_1"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                        <td class="td_border_2"></td>
                    </tr>
                </table>
                <%} %>
                <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                    HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                    AllowResize="True" Width="900" EnableViewState="False">
                </dxpc:ASPxPopupControl>
            </div>
        </div>
    </form>
</body>
</html>

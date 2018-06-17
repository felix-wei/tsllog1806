<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransferReport.aspx.cs" Inherits="Modules_Tpt_Report_TransferReport" %>

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
</head>
<body>
    <form runat="server" id="form1">
        <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefWarehouse" KeyMember="Id" />
        <table>
            <tr>
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
                <td>Cont No</td>
                <td>
                    <dxe:ASPxTextBox ID="txt_ContNo" Width="120" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>SKU
                </td>
                <td>
                    <dxe:ASPxButtonEdit ID="txt_SKULine_Product" ClientInstanceName="txt_SKULine_Product" runat="server" Width="120">
                        <Buttons>
                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s, e) {
                                PopupSku(txt_SKULine_Product,null);
                            }" />
                    </dxe:ASPxButtonEdit>
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
                    <dxe:ASPxComboBox ID="cmb_WareHouse" ClientInstanceName="txt_WareHouseId" runat="server"
                        Width="120px" DropDownWidth="159" DropDownStyle="DropDownList" DataSourceID="dsRefWarehouse"
                        ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td>
                <td>Location</td>
                <td>
                    <dxe:ASPxButtonEdit ID="txt_Location" ClientInstanceName="txt_Location" runat="server" AutoPostBack="False" Width="120">
                        <Buttons>
                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupLoc(txt_Location);
                                                                        }" />
                    </dxe:ASPxButtonEdit>
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
                                <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="150" runat="server">
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
        <table cellspacing="0" cellpadding="0" style="border-bottom: solid #000000 1px; width: 2200px; height: auto">
            <tr style="background-color: #999999; font-size: 18px; font-weight: bold; text-align: center; line-height: 35px">
                <td colspan="28">Stock Transfer Report</td>
            </tr>
            <tr style="background-color: #CCCCCC; text-align: center">
                <td rowspan="2" class="td_border_1 sn">#</td>
                <td rowspan="2" class="td_border_1 text2 head">JobNo</td>
                <td rowspan="2" class="td_border_1 text1 head">JobDate</td>
                <td rowspan="2" class="td_border_1 text2 head">Transfer No</td>
                <td rowspan="2" class="td_border_1 text2 head">Ref No</td>
                <td rowspan="2" class="td_border_1 text1 head">Customer</td>
                <td rowspan="2" class="td_border_1 text1 head">Warehouse</td>
                <td rowspan="2" class="td_border_1 text1 head">Location</td>
                <td class="td_border_1 text1 head" rowspan="2">Lot No</td>
                <td class="td_border_1 text head" rowspan="2">Hbl No</td>
                <td class="td_border_1 text2 head" rowspan="2" >Cont No</td>
                <td class="td_border_1 text1 head" rowspan="2">Type</td>
                <td class="td_border_1 text1 head" rowspan="2">SKU</td>
                <td class="td_border_1 head" colspan="6">Stock</td>
                <td class="td_border_1 head" colspan="6">Balance</td>
                <td class="td_border_1 text3 head" rowspan="2">Marking</td>
                <td class="td_border_1 text3 head" rowspan="2">Description</td>
                <td class="td_border_1 text3 head" rowspan="2">Remark</td>
            </tr>
            <tr style="background-color: #CCCCCC; text-align: center">
                <td class="td_border_1" width="100">Qty</td>
                <td class="td_border_1" width="100">Uom</td>
                <td class="td_border_1" width="100">Weight</td>
                <td class="td_border_1" width="100">Volume</td>
                <td class="td_border_1" width="150">Sku Qty</td>
                <td class="td_border_1" width="100">Uom</td>

                <td class="td_border_1" width="100">Qty</td>
                <td class="td_border_1" width="100">Uom</td>
                <td class="td_border_1" width="100">Weight</td>
                <td class="td_border_1" width="100">Volume</td>
                <td class="td_border_1" width="150">Sku Qty</td>
                <td class="td_border_1" width="100">Uom</td>
            </tr>
            <%
                DataTable tab = GetData(sql);
                int n = 0;

                string jobNo = "";
                string lastContNo = "";
                string lastBookingNo = "";
                string lastSkuCode = "";
                string lastHblNo = "";

                decimal handQty = 0;
                decimal skuQty = 0;

                decimal inQty = 0;
                decimal outQty = 0;
                decimal skuIn = 0;
                decimal skuOut = 0;

                decimal inWeight = 0;
                decimal outWeight = 0;
                decimal inVolume = 0;
                decimal outVolume = 0;

                decimal handWeight = 0;
                decimal handVolume = 0;
                string transferNo = "";
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    transferNo = SafeValue.SafeString(tab.Rows[i]["TransferNo"]);
                    string refNo = SafeValue.SafeString(tab.Rows[i]["RefNo"]);
                    string hblNo = SafeValue.SafeString(tab.Rows[i]["HblNo"]);
                    string type = SafeValue.SafeString(tab.Rows[i]["CargoType"]);
                    string bookingNo = SafeValue.SafeString(tab.Rows[i]["BookingNo"]);
                    string skuCode = SafeValue.SafeString(tab.Rows[i]["SkuCode"]);
                    int no = SafeValue.SafeInt(tab.Rows[i]["No"], 0);
                    if (type == "IN")
                    {
                        inQty = SafeValue.SafeDecimal(tab.Rows[i]["QtyOrig"]);
                        skuIn = SafeValue.SafeDecimal(tab.Rows[i]["PackQty"]);
                        inWeight = SafeValue.SafeDecimal(tab.Rows[i]["WeightOrig"]);
                        inVolume = SafeValue.SafeDecimal(tab.Rows[i]["VolumeOrig"]);
                    }
                    else
                    {
                        outQty = SafeValue.SafeDecimal(tab.Rows[i]["QtyOrig"]);
                        skuOut = SafeValue.SafeDecimal(tab.Rows[i]["PackQty"]);
                        outWeight = SafeValue.SafeDecimal(tab.Rows[i]["WeightOrig"]);
                        outVolume = SafeValue.SafeDecimal(tab.Rows[i]["VolumeOrig"]);
                    }
                    string contNo = SafeValue.SafeString(tab.Rows[i]["ContNo"]);
                    if (refNo == jobNo && skuCode == lastSkuCode && bookingNo == lastBookingNo && hblNo == lastHblNo)
                    {

                        if (n == 0)
                        {
                            handQty = inQty - outQty;
                            skuQty = skuIn - skuOut;
                            handWeight = inWeight - outWeight;
                            handVolume = inVolume - outVolume;
                        }
                        else
                        {
                            handQty = handQty - outQty;
                            skuQty = skuQty - skuOut;
                            handWeight = handWeight - outWeight;
                            handVolume = handVolume - outVolume;
                        }                           
            %>
            <tr style="line-height: 30px;">
                <td class="td_border_1 sn"><%=i+1 %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["JobNo"]) %></td>
                <td class="td_border_1"><%=Helper.Safe.SafeDateStr(tab.Rows[i]["JobDate"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["TransferNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["RefNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["ClientId"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["WareHouseCode"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Location"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["BookingNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["HblNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["ContNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["OpsType"]) %></td>

                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["SkuCode"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["QtyOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackTypeOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["WeightOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["VolumeOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["PackQty"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackUom"]) %></td>

                <td class="td_border_1"><%=SafeValue.SafeDecimal(handQty) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackTypeOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(handWeight) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(handVolume) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(skuQty) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackUom"]) %></td>

                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Marking1"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Marking2"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Remark1"]) %></td>
            </tr>
            <%  
                            n++;
                        }
                        else
                        {
                            n = 0;
                            jobNo = SafeValue.SafeString(tab.Rows[i]["JobNo"]);
            %>
            <tr style="line-height: 30px;">
                <td class="td_border_1 sn" ><%=i+1 %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["JobNo"]) %></td>
                <td class="td_border_1"><%=Helper.Safe.SafeDateStr(tab.Rows[i]["JobDate"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["TransferNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["RefNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["ClientId"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["WareHouseCode"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Location"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["BookingNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["HblNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["ContNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["OpsType"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["SkuCode"]) %></td>

                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["QtyOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackTypeOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["WeightOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["VolumeOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["PackQty"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackUom"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(inQty) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackTypeOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["WeightOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["VolumeOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(skuIn) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackUom"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Marking1"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Marking2"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Remark1"]) %></td>
            </tr>
            <% 
                    }
                        lastBookingNo = bookingNo;
                        lastHblNo = hblNo;
                        lastSkuCode = skuCode;
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

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
                    <td>Onhold</td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_OnHold" runat="server" Width="120">
                            <Items>
                                <dxe:ListEditItem Text="Yes" Value="Y" />
                                <dxe:ListEditItem Text="No" Value="N" />
                            </Items>
                        </dxe:ASPxComboBox>
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
                        <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="100%" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                     <td>Part No</td>
                    <td>
                       <dxe:ASPxButtonEdit ID="btn_PartNo" ClientInstanceName="btn_PartNo" runat="server" Width="120" HorizontalAlign="Left" AutoPostBack="False">
                        <Buttons>
                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupPartNo(btn_PartNo);
                                }" />
                    </dxe:ASPxButtonEdit>
                    </td>
                   </tr>
                <tr>
                     <td>Mft LotNo</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Mft_LotNo" ClientInstanceName="txt_Mft_LotNo" runat="server" AutoPostBack="False" Width="120">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Mft LotDate</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_From_Date" Width="120" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_To_Date" Width="120" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                     <td>Mft Expiry Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_From_ExpiryDate" Width="120" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_To_ExpiryDate" Width="120" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
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
                <td class="td_border_1 text3 head" rowspan="2">Item Code</td>
                <td class="td_border_1" colspan="6">Balance Info</td>
                
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
            </tr>
            <%
                 string dateFrom = "";
        string dateTo = "";
        string expiryFrom = "";
        string expiryTo = "";
        if (date_From_Date.Value != null && date_To_Date.Date != null)
        {
            dateFrom = date_From_Date.Date.ToString("yyyy-MM-dd");
            dateTo= date_To_Date.Date.ToString("yyyy-MM-dd");
        }
        if (date_From_ExpiryDate.Value != null && date_To_ExpiryDate.Date != null)
        {
            expiryFrom = date_From_ExpiryDate.Date.ToString("yyyy-MM-dd");
            expiryTo = date_To_ExpiryDate.Date.ToString("yyyy-MM-dd");
        }
                DataTable tab  = GetData(txt_JobNo.Text, this.txt_CustId.Text,dateFrom, dateTo,expiryFrom,expiryTo, txt_HblNo.Text, txt_LotNo.Text, this.txt_SKULine_Product.Text, this.cmb_WareHouse.Text, txt_Location.Text
            ,txt_Mft_LotNo.Text, btn_PartNo.Text,SafeValue.SafeString(cmb_OnHold.Value));

                for (int i = 0; i < tab.Rows.Count; i++)
                {                         
              %>
            <tr style="line-height:30px; ">
                <td class="td_border_1"><%=i+1 %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PartyName"]) %></td>
                <td class="td_border_1">
                    <div style="<%=show(tab.Rows[i]["JobType"]) %>;">
                        <a href='javascript: parent.navTab.openTab("<%=Helper.Safe.SafeString(tab.Rows[i]["JobNo"]) %>","/Modules/Tpt/TransferEdit.aspx?no=<%=Helper.Safe.SafeString(tab.Rows[i]["JobNo"]) %>",{title:"<%=Helper.Safe.SafeString(tab.Rows[i]["JobNo"]) %>", fresh:false, external:true});'><%=Helper.Safe.SafeString(tab.Rows[i]["JobNo"]) %></a>
                    </div>
                    <div style="<%=show1(tab.Rows[i]["JobType"]) %>;">
                        <a href='javascript: parent.navTab.openTab("<%=Helper.Safe.SafeString(tab.Rows[i]["JobNo"]) %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%=Helper.Safe.SafeString(tab.Rows[i]["JobNo"]) %>",{title:"<%=Helper.Safe.SafeString(tab.Rows[i]["JobNo"]) %>", fresh:false, external:true});'><%=Helper.Safe.SafeString(tab.Rows[i]["JobNo"]) %></a>
                    </div>
                </td>
                <td class="td_border_1"><%=Helper.Safe.SafeString(tab.Rows[i]["WareHouseCode"]) %></td>
                <td class="td_border_1"><%=Helper.Safe.SafeString(tab.Rows[i]["WhsType"]) %></td>
                <td class="td_border_1"><%=Helper.Safe.SafeString(tab.Rows[i]["PermitNo"]) %></td>
                <td class="td_border_1"><%=Helper.Safe.SafeString(tab.Rows[i]["Location"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["BookingNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["HblNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["ContNo"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["OpsType"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDateStr(tab.Rows[i]["JobDate"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["ActualItem"]) %></td>
<%--                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["QtyOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackTypeOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["WeightOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["VolumeOrig"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["PackQty"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackUom"]) %></td>--%>
                <td class="td_border_1" style="<%=trBackColor(tab.Rows[i]["BalQty"]) %>">
                    <%=SafeValue.SafeDecimal(tab.Rows[i]["BalQty"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackTypeOrig"]) %></td>
                <td class="td_border_1"><%=BalanceWeight(tab.Rows[i]["LineId"]) %></td>
                <td class="td_border_1"><%=BalanceVolume(tab.Rows[i]["LineId"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeDecimal(tab.Rows[i]["SkuQty"])  %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackUom"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Marking1"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Marking2"]) %></td>
                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Remark1"]) %></td>
            </tr>           
            <% 

                } %>
        </table>
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

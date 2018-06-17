<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectBlanketOrder.aspx.cs" Inherits="WareHouse_SelectPage_SelectBlanketOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="DoNo" Width="100%" AutoGenerateColumns="False" Styles-Cell-HorizontalAlign="Left">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="Inline"/>
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>                      
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="40" Visible="false">
                        <EditButton Visible="true" Text="Edit"></EditButton>
                    </dxwgv:GridViewCommandColumn>       
                    <dxwgv:GridViewDataTextColumn Caption="Order No" FieldName="DoNo" VisibleIndex="1" Width="200">
                         <DataItemTemplate>
                            <a onclick='parent.PutValue("<%# Eval("DoNo") %>");'><%# Eval("DoNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Order Time" FieldName="DoDate" VisibleIndex="2" Width="50">
                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Party" FieldName="PartyName" VisibleIndex="1" Width="200">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="TotalQty" FieldName="TotalQty" VisibleIndex="4" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="RemainingQty" FieldName="RemainingQty" VisibleIndex="4" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>

                <SettingsDetail  ShowDetailRow="true"/>
                <Templates>
                    <DetailRow>
                        <dxwgv:ASPxGridView ID="grid_Active" ClientInstanceName="grid_Active" runat="server"
                            KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnBeforePerformDataSelect="grid_Active_BeforePerformDataSelect">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Columns>
                                 <dxwgv:GridViewDataTextColumn Caption="SO/PO No" FieldName="DoNo" VisibleIndex="1" Width="100">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Product" FieldName="ProductCode" VisibleIndex="1" Width="100">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Des1" VisibleIndex="2" Width="150">
                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="LotNo" FieldName="LotNo" VisibleIndex="2" Width="80">
                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="UOM" FieldName="Uom1" VisibleIndex="3" Width="100">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Qty1" FieldName="Qty1" VisibleIndex="4" Width="100">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" VisibleIndex="4" Width="100">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Total Amount" FieldName="DocAmt" VisibleIndex="4" Width="100">
                                </dxwgv:GridViewDataTextColumn>
                                 <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="LocationCode" VisibleIndex="4" Width="100">
                                 </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowFooter="True" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="DocAmt" SummaryType="Sum" DisplayFormat="{0:00.00}"/>
                                <dxwgv:ASPxSummaryItem FieldName="Qty1" SummaryType="Sum" DisplayFormat="{0:00.0000}"/>
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </DetailRow>
                </Templates>
            </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>

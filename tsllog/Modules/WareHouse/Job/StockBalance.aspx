<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockBalance.aspx.cs" Inherits="PagesClient_StockBalance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
      <title>Stock Balance</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>

    <script type="text/javascript" src="/Script/pages.js"></script>
</head>
<body>
   <form id="form1" runat="server">
       <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
           TypeName="C2.RefWarehouse" KeyMember="Id" />
       <wilson:DataSource ID="dsItemRImg" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobAttachment"
           KeyMember="Id" FilterExpression="1=0" />
       <wilson:DataSource ID="dsItemImg" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobAttachment"
           KeyMember="Id" FilterExpression="1=0" />
        <div>
            <table style="display:none">
                <tr>
                    <td>from</td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>to</td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_to" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
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
                <table class="noprint" width="100%">
                    <tr>
                        <td align="left">
                            <h2>Inventory In</h2>
                        </td>
                    </tr>
                </table>
                <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid"
                    KeyFieldName="Id" AutoGenerateColumns="False" Width="100%">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="No" FieldName="JobNo" VisibleIndex="1" Width="120">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Item" FieldName="ItemNo" VisibleIndex="1" Width="120">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2" Width="200">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="3" Width="60">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Unit" VisibleIndex="4" Width="60">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="6" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="6" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="6" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="6" Width="100"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="6"
                            Width="260">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Pallet No" FieldName="PalletNo" VisibleIndex="6" Width="100"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Stock Date" FieldName="DoDate" VisibleIndex="10" Width="140" SortIndex="0" SortOrder="Descending">
                            <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="dd/MM/yyyy HH:mm" EditFormatString="dd/MM/yyyy HH:mm" Width="140"></PropertiesDateEdit>
                        </dxwgv:GridViewDataDateColumn>
                    </Columns>
                    <Settings ShowFooter="true" />
                    <SettingsDetail  ShowDetailRow="true" ShowDetailButtons="true"/>
                    <Templates>
                        <DetailRow>
                            <dxwgv:ASPxGridView ID="grid_img_R" ClientInstanceName="grid_img_R" runat="server" DataSourceID="dsItemRImg"
                                KeyFieldName="Id" Width="900px" EnableRowsCache="False" OnBeforePerformDataSelect="grid_img_R_BeforePerformDataSelect"
                                AutoGenerateColumns="false" OnRowDeleting="grid_img_R_RowDeleting" OnInit="grid_img_R_Init" OnInitNewRow="grid_img_R_InitNewRow" OnRowUpdating="grid_img_R_RowUpdating">
                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                <Columns>
<%--                                    <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                        <DataItemTemplate>
                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_img_R.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                            </dxe:ASPxButton>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataColumn>--%>
                                    <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                        <DataItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a href='<%# Eval("Path")%>' target="_blank">
                                                            <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                            </dxe:ASPxImage>
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="FileName" FieldName="FileName" Width="200px"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="FileNote"></dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </dxwgv:ASPxGridView>
                        </DetailRow>
                    </Templates>
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0}" />
                        <dxwgv:ASPxSummaryItem FieldName="ItemNo" SummaryType="Count" DisplayFormat="{0}" />
                    </TotalSummary>
                </dxwgv:ASPxGridView>
                <table class="noprint" width="100%">
                    <tr>
                        <td align="left">
                            <h2>Inventory Out</h2>
                        </td>
                    </tr>
                </table>
                <dxwgv:ASPxGridView ID="grid_Released" ClientInstanceName="grid_Released" runat="server"
                    KeyFieldName="Id" Width="100%" AutoGenerateColumns="False">
                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                    <SettingsEditing Mode="Inline" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="No" FieldName="JobNo" VisibleIndex="1" Width="120">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Item" FieldName="ItemNo" VisibleIndex="1" Width="120">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2" Width="200">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="3" Width="60">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Unit" VisibleIndex="4" Width="60">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="6" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="6" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="6" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="6" Width="100"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="6"
                            Width="260">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Pallet No" FieldName="PalletNo" VisibleIndex="6" Width="100"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Stock Date" FieldName="DoDate" VisibleIndex="10" Width="140" SortIndex="0" SortOrder="Descending">
                            <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="dd/MM/yyyy HH:mm" EditFormatString="dd/MM/yyyy HH:mm" Width="140"></PropertiesDateEdit>
                        </dxwgv:GridViewDataDateColumn>
                    </Columns>
                    <SettingsDetail ShowDetailRow="true" ShowDetailButtons="true" />
                    <Templates>
                        <DetailRow>
                            <dxwgv:ASPxGridView ID="grid_img" ClientInstanceName="grid_img" runat="server" DataSourceID="dsItemImg"
                                KeyFieldName="Id" Width="900px" EnableRowsCache="False" OnBeforePerformDataSelect="grid_img_BeforePerformDataSelect"
                                AutoGenerateColumns="false" OnRowDeleting="grid_img_RowDeleting" OnInit="grid_img_Init" OnInitNewRow="grid_img_InitNewRow" OnRowUpdating="grid_img_RowUpdating">
                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                <Columns>
                                    <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                        <DataItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a href='<%# Eval("Path")%>' target="_blank">
                                                            <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                            </dxe:ASPxImage>
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="FileName" FieldName="FileName" Width="200px"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="FileNote"></dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </dxwgv:ASPxGridView>
                        </DetailRow>
                    </Templates>
                    <Settings ShowFooter="True" />
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0}" />
                        <dxwgv:ASPxSummaryItem FieldName="ItemNo" SummaryType="Count" DisplayFormat="{0}" />
                    </TotalSummary>
                </dxwgv:ASPxGridView>
                <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid" >
                </dxwgv:ASPxGridViewExporter>
                <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                    HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                    AllowResize="True" Width="600" EnableViewState="False">
                </dxpc:ASPxPopupControl>
            </div>
        </div>
    </form>
</body>
</html>

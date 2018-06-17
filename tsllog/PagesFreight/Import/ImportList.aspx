<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportList.aspx.cs" Inherits="Pages_ImportList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Import Shipment</title>
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowHouse(masterNo, jobNo) {
            window.location = "ImportEdit.aspx?masterNo=" + masterNo + "&no=" + jobNo;
        } </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
 
        <wilson:DataSource ID="dsExport" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaImport" KeyMember="SequenceId" FilterExpression="1=0" />
        <table>
            <tr>
                <td>Reference No
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_RefNo" Width="120" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>Import No
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_HouseNo" Width="120" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>HBL No
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_HblN" Width="110" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                ETA From
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                To
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                </tr>
                <tr>
                    <td>Customer
                    </td>
                <td colspan="5">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButtonEdit ID="txt_AgtId" ClientInstanceName="txt_AgtId" runat="server" Width="77" HorizontalAlign="Left" AutoPostBack="False">
                                    <Buttons>
                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                    </Buttons>
                                    <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCust(txt_AgtId,txt_AgtName);
                                }" />
                                </dxe:ASPxButtonEdit>
                            </td>
                            <td>
                                <dxe:ASPxTextBox ID="ASPxTextBox1" ClientInstanceName="txt_AgtName" ReadOnly="true" BackColor="Control" Width="411" runat="server">
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                 </td>
                <td>
                    <dxe:ASPxButton ID="btn_search" Width="80" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                    </dxe:ASPxButton>
                </td>
                <td colspan="3">
                    <dxe:ASPxButton ID="ASPxButton1" Width="220" runat="server" Text="Retrieve Pending T/S Cargo" OnClick="btn_searchPending_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
                    <dxwgv:ASPxGridView ID="grid_Export" ClientInstanceName="detailGrid" runat="server"
                        KeyFieldName="SequenceId" Width="900px" AutoGenerateColumns="False" DataSourceID="dsExport">
                        <SettingsCustomizationWindow Enabled="True" />
                        <SettingsEditing Mode="EditForm" />
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                                        <DataItemTemplate>
                                            <a onclick="ShowHouse('<%# Eval("RefNo") %>','<%# Eval("JobNo") %>');">Edit</a>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Ref No" FieldName="RefNo" VisibleIndex="1"
                                SortIndex="1" SortOrder="Ascending" Width="30">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Import No" FieldName="JobNo" VisibleIndex="1"
                                SortIndex="1" SortOrder="Ascending" Width="150">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="CustomerName" VisibleIndex="1"
                                SortIndex="1">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Hbl No" FieldName="HblNo" VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="4" Width="30">
                                <PropertiesTextEdit DisplayFormatString="0.000">
                                </PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="5" Width="30" UnboundType="Decimal">
                                <PropertiesTextEdit DisplayFormatString="0.000">
                                </PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="6" Width="30">
                                <PropertiesTextEdit DisplayFormatString="f0">
                                </PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="T/S" FieldName="TsInd" VisibleIndex="9" Width="10"> 
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Bkg No" FieldName="TsBkgNo" VisibleIndex="9" Width="60">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowFooter="True" />
                        <TotalSummary>
                            <dxwgv:ASPxSummaryItem FieldName="JobNo" SummaryType="Count" DisplayFormat="{0}" />
                            <dxwgv:ASPxSummaryItem FieldName="Weight" SummaryType="Sum" DisplayFormat="{0:#,##0.000}"  />
                            <dxwgv:ASPxSummaryItem FieldName="Volume" SummaryType="Sum" DisplayFormat="{0:#,##0.000}"  />
                            <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0}"  />
                        </TotalSummary>
                    </dxwgv:ASPxGridView>
         <dxpc:ASPxPopupControl id="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" height="500"
            AllowResize="True" width="600" EnableViewState="False">
        </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>

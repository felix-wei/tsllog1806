<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportList.aspx.cs" Inherits="Pages_ExportList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Export Shipment</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowHouse(masterNo, jobNo) {
            window.location = "ExportEdit.aspx?masterNo=" + masterNo + "&no=" + jobNo;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <wilson:DataSource ID="dsExport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaExport" KeyMember="SequenceId" FilterExpression="1=0" />
            <table>
                <tr>
                    <td>RefNo
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_RefNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Export No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_HouseNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Hbl
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_HblN" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>ETD From
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
                    <td>Customer
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_AgtId" ClientInstanceName="txt_AgtId" runat="server" Width="120" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCust(txt_AgtId,txt_AgtName);
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="7">
                        <dxe:ASPxTextBox ID="ASPxTextBox1" ClientInstanceName="txt_AgtName" ReadOnly="true" BackColor="Control" Width="500" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
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
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="30">
                        <DataItemTemplate>
                            <a onclick="ShowHouse('<%# Eval("RefNo") %>','<%# Eval("JobNo") %>');">Edit</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Ref No" FieldName="RefNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Ascending" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Export No" FieldName="JobNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Ascending" Width="150">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="CustomerName" VisibleIndex="1"
                        SortIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Hbl No" FieldName="HblNo" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="4" Width="40">
                        <PropertiesTextEdit DisplayFormatString="0.000">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="5" UnboundType="Decimal" Width="40">
                        <PropertiesTextEdit DisplayFormatString="0.000">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="6" Width="40">
                        <PropertiesTextEdit DisplayFormatString="f0">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusCode" VisibleIndex="9" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="User Id" FieldName="CreateBy" VisibleIndex="9" Width="40">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="JobNo" SummaryType="Count" DisplayFormat="{0}" />
                    <dxwgv:ASPxSummaryItem FieldName="Weight" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                    <dxwgv:ASPxSummaryItem FieldName="Volume" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                    <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>

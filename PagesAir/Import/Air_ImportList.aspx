<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Air_ImportList.aspx.cs" Inherits="PagesAir_Import_Air_ImportList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowJob(masterId) {
            window.location = "Air_ImportRefEdit.aspx?no=" + masterId;

        }
        function ShowHouse(masterNo, jobNo) {
            window.location = "Air_ImportEdit.aspx?masterNo=" + masterNo + "&no=" + jobNo;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <wilson:DataSource ID="dsImport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.AirImport" KeyMember="SequenceId" FilterExpression="1=0" />
            <table>
                <tr>
                    <td>Reference No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_RefNo" Width="80" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Import No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_HouseNo" Width="80" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>HAWB
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_HAWB" Width="110" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Departure Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Customer
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_AgtId" ClientInstanceName="txt_AgtId" runat="server" Width="80"
                            HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCust(txt_AgtId,txt_AgtName);
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="4">
                        <dxe:ASPxTextBox ID="ASPxTextBox1" ClientInstanceName="txt_AgtName" ReadOnly="true" BackColor="Control" Width="303" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="80" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td colspan="3">
                        <dxe:ASPxButton ID="ASPxButton1" Width="225" runat="server" Text="Retrieve Pending T/S Cargo" OnClick="btn_searchPending_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Import" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Ref No" FieldName="RefNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Ascending" Width="130">
                        <DataItemTemplate>
                            <a onclick='ShowJob("<%# Eval("RefNo") %>");'><%# Eval("RefNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Import No" FieldName="JobNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Ascending" Width="150">
                        <DataItemTemplate>
                            <a onclick='ShowHouse("<%# Eval("RefNo") %>","<%# Eval("JobNo") %>");'><%# Eval("JobNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="CustomerName" VisibleIndex="1"
                        SortIndex="1" Width="300">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="HAWB No" FieldName="HAWB" VisibleIndex="4" Width="30">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="HAWB No" FieldName="HAWB" VisibleIndex="4" Width="30">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="4" Width="30">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="volume" VisibleIndex="4" Width="30">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="4" Width="30">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="T/S" FieldName="TsInd" VisibleIndex="5" Width="30">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Create By" FieldName="CreateBy" VisibleIndex="4" Width="30">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Create Date" FieldName="CreateDateTime" VisibleIndex="4" Width="30"  PropertiesTextEdit-DisplayFormatString="MM/dd/yyyy">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
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

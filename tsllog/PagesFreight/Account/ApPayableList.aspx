<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApPayableList.aspx.cs" Inherits="PagesFreight_Account_ApPayableList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowApPayable(invN) {
            window.location = 'ApPayableEdit.aspx?no=' + invN;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsApPayable" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsVendorMast" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsVendor='true'" />
            <table border="0">
                <tr>
                    <td>Doc No
                    </td>
                    <td>
                        <asp:TextBox ID="txt_refNo" Width="150" runat="server"></asp:TextBox>
                    </td>
                    <td>Party To
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                            Width="100%" DropDownWidth="380" DropDownStyle="DropDownList" DataSourceID="dsVendorMast"
                            ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                            EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="40px" />
                                <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Supplier Bill No
                    </td>
                    <td colspan="3">
                        <dxe:ASPxTextBox ID="txt_supplyBillNo" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Date From 
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>to
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Add" runat="server" Text="Add New" AutoPostBack="False" UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s,e){
                        ShowApPayable('0');
                        }" />

                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
                DataSourceID="dsApPayable" Width="800px" KeyFieldName="SequenceId"
                AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsCustomizationWindow Enabled="True" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick='ShowApPayable("<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="SupplierBillNo" FieldName="SupplierBillNo" VisibleIndex="1"
                        Width="8%">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1"
                        Width="8%">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="DocType" FieldName="DocType" VisibleIndex="1"
                        Width="5%">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PartyTo" FieldName="PartyName" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="5"
                        Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="6"
                        Width="8%">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Doc Amt" FieldName="DocAmt" VisibleIndex="7"
                        Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Loc Amt" FieldName="LocAmt" VisibleIndex="8"
                        Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Cancel" FieldName="CancelInd" VisibleIndex="10"
                        Width="8%">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
            </dxwgv:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>

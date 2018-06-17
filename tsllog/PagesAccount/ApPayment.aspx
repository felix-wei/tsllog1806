<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApPayment.aspx.cs" Inherits="Account_ApPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Payment</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function ShowDoc(docNo) {
            window.location = 'EditPage/ApPaymentEdit.aspx?no=' + docNo + '&type=' + txt_DocType1.GetText();
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsApPayment" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAApPayment" KeyMember="SequenceId" FilterExpression="1=0" />
         <wilson:DataSource ID="dsVendorMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsVendor='true'" />
			<div style="display:none;">
<dxe:ASPxTextBox runat="server" ID="txt_DocType1" BackColor="Control" 
                                    ClientInstanceName="txt_DocType1" Width="100" Text=''>
                                </dxe:ASPxTextBox>			</div>
        <table border="0">
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Doc No"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_refNo" Width="100" runat="server"></asp:TextBox>
                </td>
                <td>
                    Party To
                </td>
                <td colspan="3">
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
                <td>Cheque No</td>
                <td>
                    <asp:TextBox ID="txt_ChqNo" Width="100" runat="server"></asp:TextBox></td>
                <td>
                    PostInd
                </td>
                <td>
                        <dxe:ASPxComboBox runat="server" ID="cbo_PostInd" width="50" >
                            <Items>
                                <dxe:ListEditItem Value="All" Text="All"/>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>SupplierBill No</td>
                <td>
                    <asp:TextBox ID="txt_InvNo" Width="100" runat="server"></asp:TextBox></td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Date From"></asp:Label>
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="To"></asp:Label>
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td colspan="4">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_Add" runat="server" AutoPostBack="false" UseSubmitBehavior="false"
                                    Text="Add Payment">
                                    <ClientSideEvents Click="function(s, e) {
    ShowDoc('0');}" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_Export" Width="100" runat="server" Text="Save Excel" OnClick="btn_Export_Click">
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
            DataSourceID="dsApPayment" Width="950" KeyFieldName="SequenceId" AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='ShowDoc("<%# Eval("DocNo") %>");'>Edit</a>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Category" FieldName="DocType1" VisibleIndex="3"
                    Width="5%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="AcCode" FieldName="AcCode" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="PartyTo" FieldName="PartyName" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Other Party" FieldName="OtherPartyName" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="5"
                    Width="8%">
                    <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="6"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Loc Amt" FieldName="LocAmt" VisibleIndex="8"
                    Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Doc Amt" FieldName="DocAmt" VisibleIndex="7"
                    Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="PostInd" FieldName="ExportInd" VisibleIndex="6"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="10"
                    Width="12%">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
    </div>
    <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
        HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
        Width="800" EnableViewState="False">
        <contentcollection>
            <dxpc:PopupControlContentControl runat="server">
            </dxpc:PopupControlContentControl>
        </contentcollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
